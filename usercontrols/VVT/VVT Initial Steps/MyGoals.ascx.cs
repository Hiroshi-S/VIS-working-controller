using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.VisualBasic;

namespace VisionPersonalTrainingProject.usercontrols.VVT.VVT_Initial_Steps
{
    public partial class MyGoals : System.Web.UI.UserControl
    {
        private static DateTime _lastGoalRecord;
        private static Goal _lastGoal;
        private static int _bodyType;
        private static double _currentBodyWeight;
        public static int _totalCardio;
        public static int _lmCardio;
        public static int _hardCardio;
        private static double _previousWeight;

        //macronutrients
        public static double _CHO;
        public static double _FAT;
        public static double _PTN;
        private static double _FatFreeBodyWeight;
        private static bool _isMetric;

        protected void Page_Load(object sender, EventArgs e)
        {
            var cvdc = new ClubVisionDataContext();
            if ((string)Session["MemberType"] == "VVT")
            {
                var chk = (from ce in cvdc.Customer_Externals
                           where ce.iID == Convert.ToInt32(Session["MemberNo"])
                           select ce).FirstOrDefault();
                if (chk.bCompleteInitialState)
                {
                    MyGoalImagebuttonBack.Style["display"] = "none";
                    //MyGoalImagebuttonNext.ImageUrl = "/images/buttonSave.gif";
                    MyGoalImagebuttonNext.Style["display"] = "none";
                    weightGoalDropDownListMetric.Enabled = false;
                    weightGoalDropDownListImperial.Enabled = false;
                }
            }
            var mg = (from mgl in cvdc.Goals
                      where mgl.iCustomerID == Convert.ToInt32(Session["MemberNo"])
                      select mgl).OrderByDescending(x => x.dDateCreated).FirstOrDefault();
            string currentUrl = HttpContext.Current.Request.Url.AbsolutePath;
            
            if (currentUrl != "/club-vision/my-journey/") goalHeaderButton.Visible = false;
              

            if (mg != null)
            {
                _lastGoal = mg;
                _lastGoalRecord = mg.dDateCreated;
            }

            var bw = (from cm in cvdc.Measurements
                      where cm.iCustomerID == Convert.ToInt32(Session["MemberNo"])
                      select cm);//.OrderByDescending(x => x.dMeasured).FirstOrDefault();

            var bwCurrent = bw.OrderByDescending(x => x.dMeasured).FirstOrDefault();

            if (bwCurrent != null)
            {
                _currentBodyWeight = bwCurrent.fBodyWeight;
                if (bwCurrent.bIsMetric)
                {
                    txtIsMetric.Text = "Metric (cm/kg)";
                    _FatFreeBodyWeight = Math.Round((bwCurrent.fBodyWeight) * ((100 - bwCurrent.fNutCalc) / 100), 1);
                    trMetric.Style["display"] = "table-row";
                    metricddlRequiredFieldValidator.Enabled = true;
                    _isMetric = true;
                }
                else
                {
                    txtIsMetric.Text = "Imperial (inches/pounds)";
                    _FatFreeBodyWeight = Math.Round((bwCurrent.fBodyWeight / 2.2) * ((100 - bwCurrent.fNutCalc) / 100), 1);
                    trImperial.Style["display"] = "table-row";
                    imperialddlRequiredFieldValidator.Enabled = true;
                    _isMetric = false;
                }

                var bwFirst = bw.OrderBy(x => x.dMeasured).FirstOrDefault();

                if (bwFirst != null)
                {
                    if (bwFirst.bIsMetric == bwCurrent.bIsMetric)
                    {
                        _previousWeight = bwFirst.fBodyWeight;
                    }
                    else
                    {
                        if (bwCurrent.bIsMetric)
                        {
                            //from pounds to kg --> divided by 2.2
                            _previousWeight = bwFirst.fBodyWeight / 2.2;
                        }
                        else
                        {
                            _previousWeight = bwFirst.fBodyWeight * 2.2;
                        }
                    }
                }
            }

            if (!Page.IsPostBack)
            {
                try
                {
                    string getBodyType = (from bt in cvdc.PersonalProfile_Externals
                                          where bt.iCustomerID == Convert.ToInt32(Session["MemberNo"])
                                          select bt.cBodyType).SingleOrDefault();

                    _bodyType = GetBodyType(getBodyType);
                    bodytypeLabel.Text = _bodyType + "";
                    PopulateScreen(mg);
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
            }
        }

        protected void PopulateScreen(Goal gs = null)
        {
            if (gs != null)
            {
                txtCurrentBW.Text = gs.fCurrentBodyWeight.ToString();
                GoalDateLabel.Text = gs.dDateCreated.ToString("dd/MM/yyyy");

                txtBWGoal.Text = gs.fBodyWeightGoal.ToString();

                if (_isMetric)
                {
                    weightGoalDropDownListMetric.SelectedValue = gs.fWeightLossGoal.ToString();
                }
                else
                {
                    weightGoalDropDownListImperial.SelectedValue = gs.fWeightLossGoal.ToString();
                }
                activityLvlDropDownList.SelectedValue = gs.iActivityLevel.ToString();
                DisplayRecommendation(gs.fWeightLossGoal.ToString());
                DisplaySpecificGoalandMacro(gs.iActivityLevel);
                myGoalHeader1.Style["display"] = "block";
                myGoalHeader2.Style["display"] = "block";
                myGoalHeaderNext.Style["display"] = "block";//added as "Next" Button added. 06/09/2013 Hiroshi
                myGoalsContentDiv.Style["margin-top"] = "0px";
            }
            else
            {
                txtCurrentBW.Text = _currentBodyWeight.ToString();
                GoalDateLabel.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtBWGoal.Text = string.Empty;
                myGoalHeader1.Style["display"] = "none";
                myGoalHeader2.Style["display"] = "none";
                myGoalHeaderNext.Style["display"] = "none";//added as "Next" Button added. 06/09/2013 Hiroshi
                myGoalsContentDiv.Style["margin-top"] = "0px";
            }
        }

        protected int GetBodyType(string bodyType)
        {
            int bt = 0;
            switch (bodyType)
            {
                case "Ectomorph":
                    bt = 1; break;
                case "Ecto / Mesomorph":
                    bt = 2; break;
                case "Mesomorph":
                    bt = 3; break;
                case "Meso / Endomorph":
                    bt = 4; break;
                case "Endomorph":
                    bt = 5; break;
                default:
                    bt = 3;
                    break;
            }
            return bt;
        }

        protected int CalculateCardio(int weightLoss, int bodyType, int cardioType)
        {
            try
            {
                int result = 0;

                int cardioMinutes = 30 * (weightLoss + (bodyType - 1));

                if (cardioMinutes < 60)
                {
                    cardioMinutes = 60;
                }

                switch (cardioType)
                {
                    case 1:
                        result = cardioMinutes;
                        break;
                    case 2:
                        result = cardioMinutes * 3 / 20 * 5;
                        break;
                    case 3:
                        result = cardioMinutes - (cardioMinutes * 3 / 20) * 5;
                        break;
                }

                return result;
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
                return 0;
            }
        }

        protected void MyGoalImagebuttonNextClick(object sender, ImageClickEventArgs e)
        {
            nextbuttonclick();
        }

        protected void MyGoalLinkbuttonNextClick(object sender, EventArgs e)
        {
            nextbuttonclick();
        }

        protected void nextbuttonclick()
        {
            SaveToDatabase(GoalDateLabel.Text.Equals("Label") ? DateTime.Now : Convert.ToDateTime(GoalDateLabel.Text));
            //if (HttpContext.Current.Request.Url.AbsolutePath == "/club-vision/my-profile/edit-measurements/")//mod
            Response.Redirect("/club-vision/account-setup/exercise-planner/", false);
        }

        protected void SaveToDatabase(DateTime dm)
        {
            var cvdc = new ClubVisionDataContext();

            var custGoals = (from csg in cvdc.Goals
                             where csg.iCustomerID == Convert.ToInt32(Session["MemberNo"])
                             where csg.dDateCreated == dm
                             select csg);

            var custWeights = (from cwe in cvdc.CustomerWeights
                               where cwe.CustomerId == Convert.ToInt32(Session["MemberNo"])
                               where cwe.WeightDate == dm
                               select cwe);

            var custTrainingSums = (from cts in cvdc.WeeklyTrainingSummaries
                                    where cts.iCustomerID == Convert.ToInt32(Session["MemberNo"])
                                    where cts.iWeekNumber == 0
                                    select cts);

            var custGoal = new Goal();
            var custWeight = new CustomerWeight();
            var custTrainingSum = new WeeklyTrainingSummary();
            bool isNew = true;
            bool isCwNew = true;
            bool isCtsNew = true;

            foreach (Goal gu in custGoals)
            {
                custGoal = gu;
                isNew = false;
            }
            custGoal.iCustomerID = Convert.ToInt32(Session["MemberNo"]);
            custGoal.fCurrentBodyWeight = _currentBodyWeight;
            custGoal.fWeightLossGoal = Convert.ToDouble(_isMetric ? weightGoalDropDownListMetric.SelectedValue : weightGoalDropDownListImperial.SelectedValue);
            custGoal.fBodyWeightGoal = custGoal.fCurrentBodyWeight - custGoal.fWeightLossGoal;
            custGoal.iTotalCardio = _totalCardio;
            custGoal.iModCardio = _lmCardio;
            custGoal.iHardCardio = _hardCardio;
            custGoal.cGoalProgram = "Weight Loss";
            custGoal.iActivityLevel = Convert.ToInt32(activityLvlDropDownList.SelectedValue);
            custGoal.CHO = _CHO;
            custGoal.PTN = _PTN;
            custGoal.FAT = _FAT;
            custGoal.bIsMetric = _isMetric;
            custGoal.fProgressWeight = _previousWeight - _currentBodyWeight;

            if (isNew)
            {
                custGoal.dDateCreated = dm;
                cvdc.Goals.InsertOnSubmit(custGoal);
            }

            //customer weights
            foreach (CustomerWeight cu in custWeights)
            {
                custWeight = cu;
                isCwNew = false;
            }

            custWeight.CustomerId = Convert.ToInt32(Session["MemberNo"]);
            custWeight.Weight = Convert.ToDecimal(_currentBodyWeight);
            custWeight.IsOfficial = true;

            if (isCwNew)
            {
                custWeight.WeightDate = dm;
                cvdc.CustomerWeights.InsertOnSubmit(custWeight);
            }

            foreach (WeeklyTrainingSummary weeklyTrainingSummary in custTrainingSums)
            {
                custTrainingSum = weeklyTrainingSummary;
                isCtsNew = false;
            }

            custTrainingSum.iCustomerID = (int)Session["MemberNo"];
            custTrainingSum.iWeekNumber = 0;
            custTrainingSum.iActualHardCardio = 0;
            custTrainingSum.iActualTotCardio = 0;
            custTrainingSum.iActualWeights = 0;
            custTrainingSum.iHardCardioReq = _hardCardio;
            custTrainingSum.iTotCardioReq = _totalCardio;
            custTrainingSum.iWeightsReq = 60;
            custTrainingSum.bHardCardioAchieved = false;
            custTrainingSum.bTotCardioAchieved = false;
            custTrainingSum.bWeightsAchieved = false;

            if (isCtsNew)
            {
                custTrainingSum.dDateSaved = DateTime.Now;
                cvdc.WeeklyTrainingSummaries.InsertOnSubmit(custTrainingSum);
            }

            //delete Goals with 0 from InitialSteps
            var goalZero = (from gll in cvdc.Goals
                            where gll.iCustomerID == (int) Session["MemberNo"]
                            where gll.fCurrentBodyWeight == 0
                            where gll.FAT == 0
                            where gll.PTN == 0
                            where gll.CHO == 0
                            select gll).SingleOrDefault();

            if(goalZero != null)
            {
                cvdc.Goals.DeleteOnSubmit(goalZero);
            }

            cvdc.SubmitChanges();
            cvdc.Dispose();
        }

        protected void DisplayRecommendation(string selectedValue)//weightloss
        {
            int weightLoss = (int)Math.Round(Convert.ToDouble(selectedValue));
            _totalCardio = CalculateCardio(weightLoss, _bodyType, 1);
            _lmCardio = CalculateCardio(weightLoss, _bodyType, 2);
            _hardCardio = CalculateCardio(weightLoss, _bodyType, 3);
            int hourTotal = _totalCardio + 60;

            double bwgoal = _currentBodyWeight - Convert.ToDouble(_isMetric
                                                                   ? weightGoalDropDownListMetric.SelectedValue
                                                                   : weightGoalDropDownListImperial.SelectedValue);

            txtBWGoal.Text = bwgoal.ToString();
            //trActLvl.Visible = true;

            switch (activityLvlDropDownList.SelectedValue)
            {
                case "2":
                    lblActivityLevel.Text = "Fairly Active";
                    break;
                case "3":
                    lblActivityLevel.Text = "Moderately Active";
                    break;
                case "4":
                    lblActivityLevel.Text = "Very Active";
                    break;
            }

            myGoalLiteral.Text = "<p>Based on this information we recommend that you do <span id='hourtotspan'>" + hourTotal.ToString() + "</span> minutes of training per week being:</p>" +
                                    "<ul> " +
                                        "<li><span>60</span> minutes of Weights</li>" +
                                        "<li><span id='lmcardiospan'>" + _lmCardio.ToString() + "</span> minutes of Low to Moderate Cardio</li>" +
                                        "<li><span id='hardcardiospan'>" + _hardCardio.ToString() + "</span> minutes of Hard Cardio</li>" +
                                    "</ul>";


            RecommendationDiv.Style["display"] = "block";

        }

        protected void PreviousGoalButton_Click(object sender, EventArgs e)
        {
            txtMyGoalsWarning.Text = "";

            var cvdc = new ClubVisionDataContext();

            var cms = (from cm in cvdc.Goals
                       where cm.iCustomerID == Convert.ToInt32(Session["MemberNo"])
                       where cm.dDateCreated < Convert.ToDateTime(GoalDateLabel.Text)
                       select cm).OrderByDescending(x => x.dDateCreated).FirstOrDefault();

            PopulateScreen(cms ?? _lastGoal);
            GoBackToMyGoalScreen();
        }

        protected void NextGoalButton_Click(object sender, EventArgs e)
        {
            txtMyGoalsWarning.Text = "";

            using (ClubVisionDataContext cvdc = new ClubVisionDataContext())
            {
                var cms = (from cm in cvdc.Goals
                           where cm.iCustomerID == Convert.ToInt32(Session["MemberNo"])
                           && cm.dDateCreated > Convert.ToDateTime(GoalDateLabel.Text)
                           select cm).OrderBy(x => x.dDateCreated).FirstOrDefault();

                PopulateScreen(cms ?? _lastGoal);
                GoBackToMyGoalScreen();
            }
        }

        protected void CreateNewGoalButton_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(_lastGoalRecord, DateTime.Today) == 0)
            {
                txtMyGoalsWarning.Text =
                    "You are able to edit today's entry. Should you wish to create a new goal please wait 24 hours.";
                PopulateScreen(_lastGoal);
            }
            else
            {
                PopulateScreen();
            }
            GoBackToMyGoalScreen();
        }

        protected void CreateNewGoalImageButtonClick(object sender, ImageClickEventArgs e)
        {
            if (DateTime.Compare(_lastGoalRecord, DateTime.Today) == 0)
            {
                txtMyGoalsWarning.Text =
                    "You are able to edit today's entry. Should you wish to create a new goal please wait 24 hours.";
                PopulateScreen(_lastGoal);
            }
            else
            {
                PopulateScreen();
            }
            GoBackToMyGoalScreen();
        }

        protected void PreviousGoalImageButtonClick(object sender, ImageClickEventArgs e)
        {
            txtMyGoalsWarning.Text = "";

            var cvdc = new ClubVisionDataContext();

            var cms = (from cm in cvdc.Goals
                       where cm.iCustomerID == Convert.ToInt32(Session["MemberNo"])
                       where cm.dDateCreated < Convert.ToDateTime(GoalDateLabel.Text)
                       select cm).OrderByDescending(x => x.dDateCreated).FirstOrDefault();

            PopulateScreen(cms ?? _lastGoal);
            GoBackToMyGoalScreen();
        }
        
        //added on 30/08/2013 Hiroshi******************************************************
        protected void NextGoalImageButtonClick(object sender, ImageClickEventArgs e)
        {
            txtMyGoalsWarning.Text = "";

            using (ClubVisionDataContext cvdc = new ClubVisionDataContext())
            {
                var cms = (from cm in cvdc.Goals
                           where cm.iCustomerID == Convert.ToInt32(Session["MemberNo"])
                           && cm.dDateCreated > Convert.ToDateTime(GoalDateLabel.Text)
                           select cm).OrderBy(x => x.dDateCreated).FirstOrDefault();

                PopulateScreen(cms ?? _lastGoal);
                GoBackToMyGoalScreen();
            }
        }

        protected void GoBackToMyGoalScreen()
        {
            //modified to call corresponding javascript depends on which page
            //06/09/2013 Hiroshi
            string path = HttpContext.Current.Request.Url.AbsolutePath;
            string s = "";
            switch (path)
            {
                case "/club-vision/my-profile/edit-measurements/":
                    s = "<script Type=\"text/javascript\">" +
                             "document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_profileTab').style.backgroundImage = 'url(/images/ExtClubVision/eHdrProfileTabMyGoals.gif)';"
                            + "document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_eMeasurementsTabMeasure').style.display = 'none';"
                            + "document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_eMeasurementsTabGoals').style.display = 'block';"
                            + "document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_eMeasurementsTabTrain').style.display = 'none';"
                            + "document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tabGoal2').style.display = 'block';"
                      + "</script>";
                    break;

                case "/club-vision/my-journey/":
                case "/club-vision/my-journey/?tab=mymeasurements":
                    s = "<script Type=\"text/javascript\">"
                            + "document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_MyJouneyTab_2_profileTab').style.backgroundImage = 'url(/images/eHdrMyJourney-MyGoals.gif)';"
                            + "document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_MyJouneyTab_2_eMyProgress').style.display = 'none';"
                            + "document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_MyJouneyTab_2_eMeasurements').style.display = 'none';"
                            + "document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_MyJouneyTab_2_eMyGoals').style.display = 'block';"
                      + "</script>";
                    break;
            }

            Page.ClientScript.RegisterStartupScript(GetType(), "test", s);
        }

        protected double GetMacro(int iActiv, int iBody, string vMacro)
        {
            try
            {
                int iMacro;

                vMacro = vMacro.ToUpper();

                switch (vMacro)
                {
                    case "CHO":
                        iMacro = 1; break;
                    case "PTN":
                        iMacro = 2; break;
                    case "FAT":
                        iMacro = 3; break;
                    default:
                        iMacro = 0; break;
                }

                if (iActiv == 0 || iBody == 0 || iMacro == 0)
                {
                    return 0.00;
                }

                double dblActiv = Convert.ToDouble(Interaction.Choose(iMacro, 0.1, Interaction.IIf(iActiv == 5, 0.325, 0.4), 0.1));
                double dblBody = Convert.ToDouble(Interaction.Choose(iMacro, -0.1, 0, -0.1));
                double dblMacro = Convert.ToDouble(Interaction.Choose(iMacro, 2, 2, 0.8));

                double dMacro = Math.Round(dblMacro + dblActiv * (iActiv - 1) + dblBody * (iBody - 1), 1);

                return dMacro;
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
                throw;
            }

        }

        protected void DisplaySpecificGoalandMacro(int selectedValue) //value from activitylevel
        {
            _CHO = GetMacro(selectedValue, _bodyType, "CHO") * _FatFreeBodyWeight;
            _PTN = GetMacro(selectedValue, _bodyType, "PTN") * _FatFreeBodyWeight;
            _FAT = GetMacro(selectedValue, _bodyType, "FAT") * _FatFreeBodyWeight;

            lblCHO.Text = _CHO.ToString();
            lblPTN.Text = _PTN.ToString();
            lblFAT.Text = _FAT.ToString();

            //lblWaistHipRatioRating.Text = WaistToHipRatioRating();
            //lblWaistHipRatioGoal.Text = WaistToHipGoal();
            //trbp.Visible = false;
            if (!BloodPressureRating().Equals(""))
            {
                //lblBloodPressureRating.Text = BloodPressureRating();
                //lblBloodPressureGoal.Text = BloodPressureGoal();
                //trbp.Visible = true;
            }
        }

        protected void WeightGoalDropDownListSelectedIndexChanged(object sender, EventArgs e)
        {
            if (!activityLvlDropDownList.SelectedValue.Equals("0"))
            {
                DisplayRecommendation(_isMetric
                                      ? weightGoalDropDownListMetric.SelectedValue
                                      : weightGoalDropDownListImperial.SelectedValue);


                DisplaySpecificGoalandMacro(Convert.ToInt32(activityLvlDropDownList.SelectedValue));
                GoBackToMyGoalScreen();
                RecommendationDiv.Style["display"] = "block";
            }
            else
            {
                double bwgoal = _currentBodyWeight - Convert.ToDouble(_isMetric
                                                                   ? weightGoalDropDownListMetric.SelectedValue
                                                                   : weightGoalDropDownListImperial.SelectedValue);

                txtBWGoal.Text = bwgoal.ToString();
                GoBackToMyGoalScreen();
            }

        }

        protected void ActivityLvlDropDownListSelectedIndexChanged(object sender, EventArgs e)
        {
            if (!weightGoalDropDownListImperial.SelectedValue.Equals("0") || !weightGoalDropDownListMetric.SelectedValue.Equals("0"))
            {
                DisplayRecommendation(_isMetric
                                      ? weightGoalDropDownListMetric.SelectedValue
                                      : weightGoalDropDownListImperial.SelectedValue);

                DisplaySpecificGoalandMacro(Convert.ToInt32(activityLvlDropDownList.SelectedValue));
                GoBackToMyGoalScreen();
                RecommendationDiv.Style["display"] = "block";
            }
            else
            {
                GoBackToMyGoalScreen();
            }


        }

        protected void MyGoalImagebuttonBack_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("/club-vision/my-profile/edit-measurements/", false);
        }

        protected string WaistToHipRatioRating()
        {
            double currentRatio = Convert.ToDouble(Measurements._waistToHipRatioCurrent);
            string gender = Convert.ToString(Measurements._custGender);

            string rating = string.Empty;

            if (gender.Equals("Female"))
            {
                if (currentRatio <= 0.75)
                {
                    rating = "Excellent";
                }
                if (currentRatio > 0.75 && currentRatio <= 0.8)
                {
                    rating = "Good";
                }
                if (currentRatio > 0.80 && currentRatio <= 0.85)
                {
                    rating = "Average";
                }
                if (currentRatio > 0.85 && currentRatio <= 0.90)
                {
                    rating = "High";
                }
                if (currentRatio > 0.90)
                {
                    rating = "Poor";
                }
            }
            else //if Male
            {
                if (currentRatio <= 0.85)
                {
                    rating = "Excellent";
                }
                if (currentRatio > 0.85 && currentRatio <= 0.9)
                {
                    rating = "Good";
                }
                if (currentRatio > 0.90 && currentRatio <= 0.95)
                {
                    rating = "Average";
                }
                if (currentRatio > 0.95 && currentRatio <= 1)
                {
                    rating = "High";
                }
                if (currentRatio > 1)
                {
                    rating = "Poor";
                }

            }

            return rating;
        }

        protected string WaistToHipGoal()
        {
            double currentRatio = Convert.ToDouble(Measurements._waistToHipRatioCurrent);
            string gender = Measurements._custGender;
            string goal = string.Empty;

            if (gender.Equals("Female"))
            {
                if (currentRatio <= 0.75)
                {
                    goal = Measurements._waistToHipRatioCurrent;
                }
                if (currentRatio > 0.75 && currentRatio <= 0.80)
                {
                    goal = "0.75";
                }
                if (currentRatio > 0.80)
                {
                    currentRatio = currentRatio - 5;
                    goal = currentRatio.ToString();
                }
            }
            else //if Male
            {
                if (currentRatio <= 0.85)
                {
                    goal = Measurements._waistToHipRatioCurrent;
                }
                if (currentRatio > 0.85 && currentRatio <= 0.90)
                {
                    goal = "0.85";
                }
                if (currentRatio > 0.90)
                {
                    currentRatio = currentRatio - 5;
                    goal = currentRatio.ToString();
                }
            }

            return goal;
        }

        protected string BloodPressureRating()
        {
            if (Measurements._bloodPressCurrentDiastolic != "" && Measurements._bloodPressCurrentSystolic != "")
            {
                double currentRatio = Convert.ToDouble(Measurements._bloodPressCurrentDiastolic);

                string rating = string.Empty;


                if (currentRatio < 90)
                {
                    rating = "Normal";
                }
                if (currentRatio >= 90 && currentRatio < 95)
                {
                    rating = "Borderline Hypertension";
                }
                if (currentRatio >= 95)
                {
                    rating = "Hypertension";
                }

                return rating;
            }
            return "";
        }

        protected string BloodPressureGoal()
        {
            if (!BloodPressureRating().Equals(""))
            {
                double bpdia = Convert.ToDouble(Measurements._bloodPressCurrentDiastolic);
                string bpsys = Measurements._bloodPressCurrentSystolic;

                if (bpdia >= 85)
                {
                    bpdia = bpdia - 5;
                }
                if (bpdia >= 80 && bpdia < 85)
                {
                    bpdia = 80;
                }

                return bpsys + "/" + bpdia.ToString();

            }
            return "";
        }

        protected void NewInsertToDB(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "function", "resetMyPlanLightBox3();", true);

            //SaveToSession(DateTime.Now);
        }

    }
}