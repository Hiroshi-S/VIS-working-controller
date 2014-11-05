using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Drawing;
using VisionPersonalTrainingProject.usercontrols.externalclubvision.initialscreens;

namespace VisionPersonalTrainingProject.usercontrols.clubvision
{
    public partial class ProfileTab : System.Web.UI.UserControl
    {
        private System.DateTime _when = System.DateTime.Today;

        private string _currentCategory = "";

        private int _weeknumber = 0;

        private List<int?> CardiosWithProg; 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Request.QueryString["when"] != null)
                {
                    _when = System.DateTime.Parse(Request.QueryString["when"]);
                }

                using (ClubVisionDataContext cvdc = new ClubVisionDataContext())
                {
                    iIDLabel.Text = "-1";
                    var memberId = (int)Session["MemberNo"];
                    var customerWeights = (from cw in cvdc.CustomerWeights
                                           where cw.CustomerId == memberId
                                           orderby cw.WeightDate
                                           select cw);

                    string chartProgressData_str = "";

                    foreach (CustomerWeight customerWeight in customerWeights)
                    {
                        chartProgressData_str += "['" + customerWeight.WeightDate.ToString("yyyy-dd-MM h:mmt") + "M', " + customerWeight.Weight.ToString() + "],";
                    }

                    if (chartProgressData_str.Length > 0)
                    {
                        chartProgressData_str = chartProgressData_str.Remove(chartProgressData_str.Length - 1);
                    }

                    chartProgressData.Text = chartProgressData_str;



                    /*start make variable for both users*/
                    string nextgs = "", goalwt = "", currentWt = "", prgwt = "";
                    string carb = "", ptn = "", fat = "", cgoal = "";
                    decimal prgdec = 0;
                    /*end make variable for both users*/

                    /*start internal exclusive code*/
                    switch ((string)Session["MemberType"])
                    {
                        case "VPT":
                            {
                                var customers = (from cu in cvdc.Customers
                                                 where cu.Id == memberId
                                                 select cu);
                                var customer = new Customer();

                                foreach (Customer customerLU in customers)
                                {
                                    customer = customerLU;
                                }

                                nextgs = customer.NextGS.ToString("yyyy-dd-MM h:mmt");
                                Session["nextGoalSession"] = nextgs;
                                Session["lastGoalSession"] = customer.LastGS.ToString("yyyy-dd-MM h:mmt");
                                goalwt = customer.GoalWt.Remove(customer.GoalWt.Length - 2);
                                currentWt = customer.CurrentWt;
                                carb = customer.Carb.ToString();
                                ptn = customer.Protein.ToString();
                                fat = customer.Fat.ToString();
                                prgwt = customer.ProgressWt;
                                cgoal = customer.Goal;
                                int kg_pos = prgwt.IndexOf("kg");
                                prgdec = decimal.Parse(prgwt.Remove(kg_pos));
                            }
                            break;
                        case "VVT":
                            {
                                var customers = (from cu in cvdc.Customer_Externals
                                                 where cu.iID == memberId
                                                 select cu);
                                var customer = new Customer_External();

                                foreach (Customer_External customerLU in customers)
                                {
                                    customer = customerLU;
                                }

                                var custGoalfirst = customer.Goals.OrderByDescending(x => x.dDateCreated).FirstOrDefault();
                                if (custGoalfirst != null)
                                {
                                    nextgs = custGoalfirst.dDateCreated.AddMonths(3).ToString("yyyy-dd-MM h:mmt");
                                    goalwt = custGoalfirst.fBodyWeightGoal.ToString();
                                    currentWt = custGoalfirst.fCurrentBodyWeight.ToString();
                                    carb = custGoalfirst.CHO.ToString();
                                    ptn = custGoalfirst.PTN.ToString();
                                    fat = custGoalfirst.FAT.ToString();
                                    prgwt = custGoalfirst.fProgressWeight.ToString();
                                    cgoal = custGoalfirst.cGoalProgram;
                                    prgdec = decimal.Parse(prgwt);
                                }
                            }
                            break;
                    }

                    chartProgressDataGoal.Text = "['" + nextgs + "M', " + goalwt + "],";

                    weight.InnerText = currentWt;

                    chartMacrosData_Target.Text = carb + "," + ptn + "," + fat;

                    decimal progress_dec = prgdec;

                    change_text.InnerText = "kg";

                    if (progress_dec > 0)
                    {
                        change_text.InnerText += " gained";
                    }
                    if (progress_dec < 0)
                    {
                        change_text.InnerText += " lost";
                        progress_dec = -progress_dec;
                    }

                    change.InnerText = progress_dec.ToString();

                    goal.InnerText = cgoal;

                    //start edailymenusview

                    var _menus = (from menus in cvdc.Menus
                                  where menus.CustomerId == (int)Session["MemberNo"]
                                  orderby menus.Created descending
                                  select menus).Take(5);

                    foreach (Menu menu in _menus)
                    {
                        literalMenus.Text += "<li class=\"menu\">";
                        literalMenus.Text += "<a href=\"/club-vision/my-eating/menus/?tab=view&menuId=" + menu.Id + "\">";
                        if (menu.ImageUrl != null)
                        {
                            literalMenus.Text += "<img class=\"menu_image\" src=\"/images/menus/" + menu.ImageUrl.Replace("meal_generic", "menu_generic") + "\" style=\"height: 64px;left: -4px;position: relative;top: 8px;width: 64px;\" />";
                            // TODO: fix: http://selbie.wordpress.com/2011/01/23/scale-crop-and-center-an-image-with-correct-aspect-ratio-in-html-and-javascript/
                        }
                        literalMenus.Text += "<p class=\"title\">" + menu.Name + "</p></a>";
                        literalMenus.Text += "</li>";
                    }
                    //end of edailymenusview

                }
                if (Request.QueryString["when"] != null)
                {
                    _when = DateTime.Parse(Request.QueryString["when"]);
                }
                bdpDay.SelectedDate = _when;
                bdpDay2.SelectedDate = _when;
                UpdateTrainingTab(_when);
                UpdateFoodTab(_when);
            }
        }

        public void UpdateSqlTrainingData(int memberId, int currentWeek)
        {
            var cvdc = new VisionPersonalTrainingProject.ClubVisionDataContext();

            var custTdExisxt = (from ctde in cvdc.TrainingDiaries
                                where ctde.iCustomerId == memberId
                                where ctde.bFromVOS == true
                                where ctde.iWeekNumber == currentWeek
                                select ctde);

            CardiosWithProg = (from bcs in cvdc.EnumTables
                               where bcs.ID == 9
                               where bcs.OptGroup.Equals("Cardio")
                               where bcs.cField == 1
                               select bcs.intValue).ToList();

            if (!custTdExisxt.Any()) //just when it does not exist the disco starts!!
            {
                switch ((string)Session["MemberType"])
                {
                    case "VPT":
                        GenerateWeeklyExercise(currentWeek, memberId); break;
                    case "VVT":
                        GenerateWeeklyExercise_External(currentWeek, memberId); break;
                }

            }

            cvdc.Dispose();
        }

        public void GenerateWeeklyExercise(int weekNumber, int memberId)
        {
            var cvdc = new VisionPersonalTrainingProject.ClubVisionDataContext();

            var ctdList = new List<TrainingDiary>();

            //grab all the default training in CustomerTraining table
            var fromCustTraining = (from cct in cvdc.CustomerTrainings
                                    where cct.CustomerId == memberId
                                    select cct);

            foreach (var customerTraining in fromCustTraining)
            {
                //break down of exercise sentence
                string exwords = customerTraining.Exercise;
                string[] lines = Regex.Split(exwords, " ");

                //if its not restday
                if (lines.Length < 4) continue;
                var ctd = new TrainingDiary();
                var str = new string[4];

                str[0] = lines[lines.Length - 1]; //type
                str[1] = lines[lines.Length - 2]; //intensity
                str[2] = lines[lines.Length - 3]; //duration

                for (int j = 0; j < (lines.Length - 3); j++)
                {
                    str[3] += lines[j] + " ";//exercise
                }

                int ampm = customerTraining.WhenAMPM == "AM" ? 1 : 2;

                int extype;
                switch (str[0])
                {
                    case "PT":
                        extype = 1; break;
                    case "Alone":
                        extype = 2; break;
                    case "Studio":
                        extype = 3; break;
                    default:
                        extype = 0; break;
                }

                int intensity;
                switch (str[1])
                {
                    case "L-M":
                        intensity = 1; break;
                    case "Hard":
                        intensity = 2; break;
                    default:
                        intensity = 0; break;
                }

                int duration;
                switch (str[2])
                {
                    case null:
                        duration = 0; break;
                    case " ":
                        duration = 0; break;
                    case "":
                        duration = 0; break;
                    default:
                        duration = Convert.ToInt32(str[2]); break;
                }

                var exerciseval = (from ev in cvdc.EnumTables
                                   where ev.Value == str[3] && ev.ID == 9
                                   select ev.intValue).SingleOrDefault();

                bool isInList = CardiosWithProg.IndexOf(exerciseval) != -1;

                //start populate the data
                ctd.iId = -1;
                ctd.iCustomerId = memberId;
                ctd.iWeekNumber = weekNumber;
                ctd.iWeekDay = customerTraining.WeekDay;
                ctd.iAMPM = ampm;
                ctd.cExercise = Convert.ToString(str[3]);
                ctd.iDuration = duration;
                ctd.iExerType = extype;
                ctd.iIntensity = intensity;
                ctd.dDateCreated = DateTime.Now;
                ctd.bCompleteState = false;
                ctd.intValue = GetIntValue(weekNumber, customerTraining.WeekDay, ampm);
                ctd.bFromVOS = true;
                ctd.iExValue = Convert.ToInt32(exerciseval);
                ctd.bIsCardioHasProg = isInList;

                ctdList.Add(ctd);
            }

            cvdc.TrainingDiaries.InsertAllOnSubmit(ctdList);
            cvdc.SubmitChanges();
            cvdc.Dispose();
        }

        public void GenerateWeeklyExercise_External(int weekNumber, int memberId)
        {
            var cvdc = new ClubVisionDataContext();

            var ctdList = new List<TrainingDiary>();

            var td = (from cct in cvdc.TrainingDiaries
                      where cct.iCustomerId == memberId
                      where cct.bFromVOS == true
                      where cct.iWeekNumber == 0
                      select cct);

            int lastEPId = 0;

            foreach (TrainingDiary customerTraining in td)
            {
                bool isInList = CardiosWithProg.IndexOf(customerTraining.iExValue) != -1;

                var newTD = new TrainingDiary
                {
                    iCustomerId = (int)Session["MemberNo"],
                    iWeekNumber = weekNumber,
                    iWeekDay = customerTraining.iWeekDay,
                    iAMPM = customerTraining.iAMPM,
                    cExercise = customerTraining.cExercise,
                    iDuration = customerTraining.iDuration,
                    iExerType = customerTraining.iExerType,
                    iIntensity = customerTraining.iIntensity,
                    dDateCreated = DateTime.Now,
                    bCompleteState = false,
                    intValue =
                        GetIntValue(weekNumber, customerTraining.iWeekDay, customerTraining.iAMPM),
                    bFromVOS = true,
                    iExValue = customerTraining.iExValue,
                    bIsWPExAttached = customerTraining.bIsWPExAttached,
                    bIsCardioHasProg = isInList
                };
                ctdList.Add(newTD);
            }

            cvdc.TrainingDiaries.InsertAllOnSubmit(ctdList);
            cvdc.SubmitChanges();
            cvdc.Dispose();
        }

        protected void UpdateFoodTab(System.DateTime when)
        {
            var memberId = (int)Session["MemberNo"];

            VisionPersonalTrainingProject.ClubVisionDataContext cvdc = new VisionPersonalTrainingProject.ClubVisionDataContext();

            literalDay2.Text = when.ToLongDateString();

            var food = (from fd in cvdc.DiaryEntries
                        where fd.CustomerId == memberId
                        where fd.When == when
                        orderby fd.MealTimeId
                        orderby fd.FromMenuId
                        orderby fd.FromMealId
                        select fd
            );

            decimal carb = 0;
            decimal protein = 0;
            decimal fat = 0;

            foodDiary.InnerHtml = "";

            int count = 0;

            foreach (DiaryEntry de in food)
            {
                count++;
                if (count < 3)
                {
                    if (de.FromItemsAdd == null)
                    {
                        foodDiary.InnerHtml += "<p>" + de.Item.Name + "</p>";
                    }
                    else
                    {
                        foodDiary.InnerHtml += "<p style='font-style:italic'>*" + de.ItemAdd.Name + "</p>";
                    }
                }
                if (count == 6)
                {
                    foodDiary.InnerHtml += "<p>...</p>";
                }

                if (de.FromItemsAdd == null)
                {
                    carb += (de.Amount / de.Item.ServeAmount) * de.Item.Carbohydrate;
                    protein += (de.Amount / de.Item.ServeAmount) * de.Item.Protein;
                    fat += (de.Amount / de.Item.ServeAmount) * de.Item.Fat;
                }
                else
                {
                    carb += (de.Amount / de.ItemAdd.ServeAmount) * de.ItemAdd.Carbohydrate;
                    protein += (de.Amount / de.ItemAdd.ServeAmount) * de.ItemAdd.Protein;
                    fat += (de.Amount / de.ItemAdd.ServeAmount) * de.ItemAdd.Fat;
                }

            }

            if (foodDiary.InnerHtml == "")
            {
                foodDiary.InnerHtml = "<p style=\"font-weight: bold\">No food items have been entered for this day.</p>";
                foodDiary.InnerHtml += "<br><a href=\"/club-vision/my-eating/food-diary/?when=" + when.ToShortDateString() + "\"><img alt=\"Go to Today's Diary\" src=\"/images/buttonTodayDiary.gif\" border=\"0\" /></a>";
                foodDiary.InnerHtml += "<br><br><a href=\"/club-vision/my-eating/food-diary/?tab=week&when=" + when.ToShortDateString() + "\"><img alt=\"Go to This Week's Diary\" src=\"/images/buttonThisWeekDiary.gif\" border=\"0\" /></a>";
            }
            else
            {
                foodDiary.InnerHtml += "<br><a href=\"/club-vision/my-eating/food-diary/\"><img alt=\"Go to Food Diary\" src=\"/images/buttonGoToFoodDiary.gif\" border=\"0\" /></a>";
                foodDiary.InnerHtml += "<br><br><a href=\"/club-vision/my-eating/food-diary/?tab=week&when=" + when.ToShortDateString() + "\"><img alt=\"Go to This Week's Diary\" src=\"/images/buttonThisWeekDiary.gif\" border=\"0\" /></a>";
            }

            chartMacrosData_Actual.Text = carb + "," + protein + "," + fat;
            cvdc.Dispose();
        }

        protected void UpdateTrainingTab(System.DateTime when)
        {
            var memberId = (int)Session["MemberNo"];

            _weeknumber = GetWeekNumber(when);

            weekLabel.Text = _weeknumber.ToString(CultureInfo.InvariantCulture);

            var cvdc = new ClubVisionDataContext();

            UpdateSqlTrainingData(memberId, _weeknumber);

            DateTime thismonday = GetFirstDayOfWeek(_weeknumber, when.Year);

            literalDay.Text = thismonday.ToLongDateString();

            currentDate.Value = when.ToString("dd/MM/yyyy");

            //write the literal here
            GenerateTrainingDiaryToLiteral(_weeknumber);
            UpdateTrainingSummary();

            cvdc.Dispose();

            string s = "<script type=\"text/javascript\">" +
                        "callMotivationCaptainOnTrainingDiary();</script>";

            Page.ClientScript.RegisterStartupScript(GetType(), "test", s);

        }

        protected void GenerateTrainingDiaryToLiteral(int weeknum)
        {
            using (ClubVisionDataContext cvdc = new ClubVisionDataContext())
            {
                var thisWeekTD = (from twtd in cvdc.TrainingDiaries
                                  where twtd.iWeekNumber == weeknum
                                  where twtd.iCustomerId == (int)Session["MemberNo"]
                                  select twtd).OrderBy(x => x.intValue);
                int day = 0;
                string type = "";

                switch ((string)Session["MemberType"])
                {
                    case "VPT":
                        type = "<td>Type</td>"; break;
                    case "VVT":
                        type = ""; break;
                }

                litTrainingDiary.Text = "<table id=\"tableTrainingDiaryProfile\"><tr style=\"font-weight:bold;\"><td>Weekday</td><td>Exericise</td><td>When</td><td>Intensity</td><td>Duration</td>" +
                                        type +
                                        "<td>Done</td></tr>";

                foreach (TrainingDiary trainingDiary in thisWeekTD)
                {
                    string daycv = "";
                    if (!type.Equals(""))
                    {
                        type = "<td>" + RenderExerType(trainingDiary.iExerType) + "</td>";
                    }

                    if (day != trainingDiary.iWeekDay)
                    {
                        daycv = RenderDay(trainingDiary.iWeekDay);
                    }

                    litTrainingDiary.Text += "<tr>" +
                                             "<td>" + daycv + "</td>" +
                                             "<td>" + trainingDiary.cExercise + "</td>" +
                                             "<td>" + RenderAMPM(trainingDiary.iAMPM) + "</td>" +
                                             "<td>" + RenderIntensity(trainingDiary.iIntensity) + "</td>" +
                                             "<td>" + RenderDurs(trainingDiary.iDuration) + "</td>" +
                                             type +
                                             "<td><img id=\"tick-" + trainingDiary.iId + "\" src=\"" + RenderImageURl(trainingDiary.bCompleteState) + "\" onclick=\"saveChangesTrainingRowFromProfile(" + trainingDiary.iId + ",'tickdone');return false;\"/></td>" +
                                             "</tr>";
                    day = trainingDiary.iWeekDay;
                }

                litTrainingDiary.Text += "</table>";
            }
        }

        protected void ButtonWeekPrevClick(object sender, EventArgs e)
        {
            _when = System.DateTime.Parse(currentDate.Value);

            _when = _when.AddDays(-7);

            bdpDay.SelectedValue = _when;
            bdpDay2.SelectedValue = _when;

            UpdateTrainingTab(_when);
            UpdateFoodTab(_when);

            profileTab.Style["background-image"] = "url(/images/eHdrProfileTabTrain.gif)";
            eProfileTabMenu.Style["display"] = "none";
            eProfileTabGoal.Style["display"] = "none";
            eProfileTabTrain.Style["display"] = "block";
            eDailyMenusView.Style["display"] = "none";
        }

        protected void ButtonWeekNextClick(object sender, EventArgs e)
        {
            _when = System.DateTime.Parse(currentDate.Value);

            _when = _when.AddDays(7);

            bdpDay.SelectedValue = _when;
            bdpDay2.SelectedValue = _when;

            UpdateTrainingTab(_when);
            UpdateFoodTab(_when);

            profileTab.Style["background-image"] = "url(/images/eHdrProfileTabTrain.gif)";
            eProfileTabMenu.Style["display"] = "none";
            eProfileTabGoal.Style["display"] = "none";
            eProfileTabTrain.Style["display"] = "block";
            eDailyMenusView.Style["display"] = "none";

        }

        protected void ButtonDayPrev2Click(object sender, EventArgs e)
        {
            _when = System.DateTime.Parse(currentDate.Value);

            _when = _when.AddDays(-1);

            bdpDay.SelectedValue = _when;
            bdpDay2.SelectedValue = _when;

            UpdateTrainingTab(_when);
            UpdateFoodTab(_when);

            profileTab.Style["background-image"] = "url(/images/eHdrProfileTabMenu.gif)";
            eProfileTabMenu.Style["display"] = "block";
            eProfileTabGoal.Style["display"] = "none";
            eProfileTabTrain.Style["display"] = "none";
            eDailyMenusView.Style["display"] = "block";
        }

        protected void ButtonDayNext2Click(object sender, EventArgs e)
        {
            _when = System.DateTime.Parse(currentDate.Value);

            _when = _when.AddDays(1);

            bdpDay.SelectedValue = _when;
            bdpDay2.SelectedValue = _when;

            UpdateTrainingTab(_when);
            UpdateFoodTab(_when);

            profileTab.Style["background-image"] = "url(/images/eHdrProfileTabMenu.gif)";
            eProfileTabMenu.Style["display"] = "block";
            eProfileTabGoal.Style["display"] = "none";
            eProfileTabTrain.Style["display"] = "none";
            eDailyMenusView.Style["display"] = "block";
        }

        protected void BdpDaySelectionChanged(object sender, EventArgs e)
        {
            _when = (System.DateTime)bdpDay.SelectedValue;
            bdpDay2.SelectedValue = bdpDay.SelectedValue;
            UpdateTrainingTab(_when);
            UpdateFoodTab(_when);

            profileTab.Style["background-image"] = "url(/images/eHdrProfileTabTrain.gif)";
            eProfileTabMenu.Style["display"] = "none";
            eProfileTabGoal.Style["display"] = "none";
            eProfileTabTrain.Style["display"] = "block";
            eDailyMenusView.Style["display"] = "none";
        }

        protected void BdpDay2SelectionChanged(object sender, EventArgs e)
        {
            _when = (System.DateTime)bdpDay2.SelectedValue;
            bdpDay.SelectedValue = bdpDay2.SelectedValue;
            UpdateTrainingTab(_when);
            UpdateFoodTab(_when);


            profileTab.Style["background-image"] = "url(/images/eHdrProfileTabMenu.gif)";
            eProfileTabMenu.Style["display"] = "block";
            eProfileTabGoal.Style["display"] = "none";
            eProfileTabTrain.Style["display"] = "none";
            eDailyMenusView.Style["display"] = "block";
        }

        protected void CancelButtonClick(object sender, EventArgs e)
        {
            GoToFirstScreenTrainingDiary();
        }

        protected void GoToFirstScreenTrainingDiary()
        {
            //ClearForm();
            _when = (System.DateTime)bdpDay.SelectedValue;
            UpdateTrainingTab(_when);

            profileTab.Style["background-image"] = "url(/images/eHdrProfileTabTrain.gif)";
            eProfileTabMenu.Style["display"] = "none";
            eProfileTabGoal.Style["display"] = "none";
            eProfileTabTrain.Style["display"] = "block";
            eDailyMenusView.Style["display"] = "none";

        }

        protected void GoToSecondScreenTrainingDiary()
        {
            profileTab.Style["background-image"] = "url(/images/eHdrProfileTabTrain.gif)";
            eProfileTabMenu.Style["display"] = "none";
            eProfileTabGoal.Style["display"] = "none";
            eProfileTabTrain.Style["display"] = "block";
            eDailyMenusView.Style["display"] = "none";
        }

        protected void GoToThirdScreenTrainingDiary()
        {
            profileTab.Style["background-image"] = "url(/images/eHdrProfileTabTrain.gif)";
            eProfileTabMenu.Style["display"] = "none";
            eProfileTabGoal.Style["display"] = "none";
            eProfileTabTrain.Style["display"] = "block";
            eDailyMenusView.Style["display"] = "none";
        }

        protected void UpdateTrainingSummary()
        {
            int currentWeek = Convert.ToInt32(weekLabel.Text);
            int actualTotalCardio = CalculateActualTotalCardio(currentWeek, Convert.ToInt32(Session["MemberNo"]));
            int actualHardCar = CalculateActualHardCardio(currentWeek, Convert.ToInt32(Session["MemberNo"]));
            int actualWeights = CalculateActualWeights(currentWeek, Convert.ToInt32(Session["MemberNo"]));
            bool hardCarAch = false, weightAch = false, totCarAch = false;
            int iWeightsReq = 0;
            decimal iHardCardioReq = 0; //this one has to be decimal
            int iTotcardioReq = 0;

            var memberid = (int)Session["MemberNo"];

            ClubVisionDataContext cvdc = new ClubVisionDataContext();

            var ifexists = (from wtscheck in cvdc.WeeklyTrainingSummaries
                            where wtscheck.iCustomerID == memberid
                            where wtscheck.iWeekNumber == currentWeek
                            select wtscheck);

            WeeklyTrainingSummary wts = new WeeklyTrainingSummary();
            bool isNew = true;

            foreach (WeeklyTrainingSummary weeklyTrainingSummary in ifexists)
            {
                wts = weeklyTrainingSummary;
                isNew = false;
            }

            switch ((string)Session["MemberType"])
            {
                case "VPT":
                    {
                        var custdet = (from ct in cvdc.Customers
                                       where ct.Id == (int)Session["MemberNo"]
                                       select ct).SingleOrDefault();

                        iWeightsReq = Convert.ToInt32(custdet.Weights);
                        iHardCardioReq = Convert.ToDecimal(custdet.CardioHard);
                        iTotcardioReq = isNew ? Convert.ToInt32(custdet.Cardio) : wts.iTotCardioReq;
                    } break;
                case "VVT":
                    {
                        /*var custdet = (from ct in cvdc.Goals
                                       where ct.iCustomerID == (int)Session["MemberNo"]
                                       select ct).OrderByDescending(x => x.dDateCreated).FirstOrDefault();

                        */
                        var custdet = (from ct in cvdc.WeeklyTrainingSummaries
                                       where ct.iCustomerID == (int) Session["MemberNo"]
                                       where ct.iWeekNumber == 0
                                       select ct).Select(x => new {x.iActualHardCardio, x.iActualTotCardio}).SingleOrDefault();

                        iWeightsReq = 60;
                        if (custdet != null)
                        {
                            iHardCardioReq = custdet.iActualHardCardio; //this one has to be decimal
                            iTotcardioReq = isNew ? custdet.iActualTotCardio : wts.iTotCardioReq;
                        }
                    } break;
            }

            //assign value to textboxes
            ActualHardCardioLiteral.Text = "<input readonly type='text' id='ActualHardCardioTextBox' value='" + actualHardCar + "' />";
            ActualWeightsLiteral.Text = "<input readonly type='text' class='bottomNum' id='ActualWeightsTextBox' value='" + actualWeights + "' />";
            ActualTotalCardioLiteral.Text = "<input readonly type='text' id='ActualTotalCardioTextBox' value='" + actualTotalCardio + "' />";

            HardCardioReqLiteral.Text = "<input readonly type='text' id='HardCardioReqTextBox' value='" + Convert.ToInt32(iHardCardioReq) + "' />";
            WeightsReqLiteral.Text = "<input readonly type='text' class='bottomNum' id='WeightsReqTextBox' value='" + iWeightsReq + "' />";
            TotalCardioReqLiteral.Text = "<input readonly type='text' id='TotalCardioReqTextBox' value='" + iTotcardioReq + "' />";

            if (actualHardCar >= iHardCardioReq)
            {
                HardCardioAchievedLiteral.Text = "<input readonly style='color:green;' type='text' id='HardCardioAchievedTextBox' value='YES' />";
                hardCarAch = true;
            }
            else
            {
                HardCardioAchievedLiteral.Text = "<input readonly style='color:red;' type='text' id='HardCardioAchievedTextBox' value='NO' />";
            }

            if (actualWeights >= iWeightsReq)
            {
                WeightsPtAchievedLiteral.Text = "<input readonly class='bottomNum' style='color:green;' type='text' id='WeightsPtAchievedTextBox' value='YES' />";
                weightAch = true;
            }
            else
            {
                WeightsPtAchievedLiteral.Text = "<input readonly class='bottomNum' style='color:red;' type='text' id='WeightsPtAchievedTextBox' value='NO' />";
            }

            if (actualTotalCardio >= iTotcardioReq)
            {
                TotalCardioAchievedLiteral.Text = "<input readonly style='color:green;' type='text' id='TotalCardioAchievedTextBox' value='YES' />";
                totCarAch = true;
            }
            else
            {
                TotalCardioAchievedLiteral.Text = "<input readonly style='color:red;' type='text' id='TotalCardioAchievedTextBox' value='NO' />";
            }

            wts.iCustomerID = memberid;
            wts.iWeekNumber = currentWeek;
            wts.iActualHardCardio = actualHardCar;
            wts.iActualWeights = actualWeights;
            wts.iActualTotCardio = actualTotalCardio;
            wts.iHardCardioReq = Convert.ToInt32(iHardCardioReq);
            wts.iWeightsReq = iWeightsReq;
            wts.iTotCardioReq = iTotcardioReq;
            wts.bTotCardioAchieved = totCarAch;
            wts.bHardCardioAchieved = hardCarAch;
            wts.bWeightsAchieved = weightAch;
            wts.dDateSaved = DateTime.Today;

            if (isNew)
            {
                cvdc.WeeklyTrainingSummaries.InsertOnSubmit(wts);
            }

            cvdc.SubmitChanges();

            cvdc.Dispose();
        }

        protected void CalcexerciseButtonClick(object sender, EventArgs e)
        {

        }

        protected int InsteadOfNull(string val)
        {
            int value = 0;
            switch (val)
            {
                case "":
                    value = 0; break;
                case null:
                    value = 0; break;
                case " ":
                    value = 0; break;
                default:
                    value = Convert.ToInt32(val); break;
            }
            return value;
        }

        protected void AddButtonClick(object sender, EventArgs e)
        {
            //ClearForm();
            GoToSecondScreenTrainingDiary();
        }

        protected void SaveData()
        {
            ClubVisionDataContext cvdc = new ClubVisionDataContext();

            TrainingDiary trainingDiary = new TrainingDiary();

            int id = Convert.ToInt32(iIDLabel.Text);

            /*
            if (id < 0)
            {
                trainingDiary.iId = id;
                trainingDiary.iCustomerId = (int)Session["MemberNo"];
                trainingDiary.iWeekNumber = Convert.ToInt32(weekLabel.Text);
                trainingDiary.iWeekDay = Convert.ToInt32(DropDownList1.SelectedValue);
                trainingDiary.iAMPM = Convert.ToInt32(DropDownList7.SelectedValue);
                trainingDiary.cExercise = DropDownList21.SelectedItem.Text;
                trainingDiary.iDuration = Convert.ToInt32(DropDownList8.SelectedValue);
                trainingDiary.iExerType = Convert.ToInt32(DropDownList3.SelectedValue);
                trainingDiary.iIntensity = Convert.ToInt32(DropDownList4.SelectedValue);
                trainingDiary.dDateCreated = DateTime.Now;
                trainingDiary.bCompleteState = false;
                trainingDiary.intValue = GetIntValue(Convert.ToInt32(weekLabel.Text), Convert.ToInt32(DropDownList1.SelectedValue),
                                            Convert.ToInt32(DropDownList7.SelectedValue));
                trainingDiary.bFromVOS = false;//GetCompleteState(DropDownList5.SelectedValue);
                trainingDiary.iExValue = Convert.ToInt32(DropDownList21.SelectedValue);

                cvdc.TrainingDiaries.InsertOnSubmit(trainingDiary);
            }
            else
            {
                var item = (from itemupdate in cvdc.TrainingDiaries
                            where itemupdate.iId == id
                            select itemupdate).SingleOrDefault();

                if (item != null)
                {
                    item.iWeekDay = Convert.ToInt32(DropDownList1.SelectedValue);
                    item.iAMPM = Convert.ToInt32(DropDownList7.SelectedValue);
                    item.cExercise = DropDownList21.SelectedItem.Text;
                    item.iDuration = Convert.ToInt32(DropDownList8.SelectedValue);
                    item.iExerType = Convert.ToInt32(DropDownList3.SelectedValue);
                    item.iIntensity = Convert.ToInt32(DropDownList4.SelectedValue);
                    item.dDateCreated = DateTime.Now;
                    //item.bCompleteState = GetCompleteState(DropDownList5.SelectedValue);
                    item.intValue = GetIntValue(Convert.ToInt32(weekLabel.Text), Convert.ToInt32(DropDownList1.SelectedValue),
                                            Convert.ToInt32(DropDownList7.SelectedValue));

                    item.iExValue = Convert.ToInt32(DropDownList21.SelectedValue);
                }
            }
            */

            cvdc.SubmitChanges();
            cvdc.Dispose();
        }

        protected bool GetCompleteState(string val)
        {
            bool value = false;
            switch (val)
            {
                case "1":
                    value = true; break;
                default:
                    value = false; break;
            }
            return value;
        }

        protected string GetCompleteStateOnEdit(bool val)
        {
            string value;
            switch (val)
            {
                case true:
                    value = "1"; break;
                default:
                    value = "2"; break;
            }
            return value;
        }

        protected int GetIntValue(int week, int weekday, int ampm)
        {

            var wk = Convert.ToString(week);
            var wkday = Convert.ToString(weekday);
            var ampm2 = Convert.ToString(ampm);

            string ival = wk + wkday + ampm2;

            int intval = Convert.ToInt32(ival);

            return intval;
        }

        public string RenderDurs(Object iDur)
        {
            return Convert.ToString(iDur) + " mins";
        }

        public string RenderIntensity(Object inten)
        {
            string ins = "";
            var ins2 = Convert.ToInt32(inten);
            switch (ins2)
            {
                case 1:
                    ins = "Low intensity";
                    break;
                case 2:
                    ins = "Hard intensity";
                    break;
                default:
                    ins = "N/A";
                    break;
            }
            return ins;
        }

        public string RenderDay(Object day)
        {
            string dday = "";
            int iday = Convert.ToInt32(day);
            switch (iday)
            {
                case 1:
                    dday = "Monday";
                    break;
                case 2:
                    dday = "Tuesday";
                    break;
                case 3:
                    dday = "Wednesday";
                    break;
                case 4:
                    dday = "Thursday";
                    break;
                case 5:
                    dday = "Friday";
                    break;
                case 6:
                    dday = "Saturday";
                    break;
                case 7:
                    dday = "Sunday";
                    break;
                default:
                    dday = "N/A";
                    break;
            }
            return dday;
        }

        public string RenderExerType(Object exType)
        {
            string xtype = "";
            var xt = Convert.ToInt32(exType);
            switch (xt)
            {
                case 1:
                    xtype = "PT";
                    break;
                case 2:
                    xtype = "Alone";
                    break;
                case 3:
                    xtype = "Studio";
                    break;
                default:
                    xtype = "N/A";
                    break;
            }
            return xtype;
        }

        public string RenderMinute(Object min)
        {
            string mins = Convert.ToString(min);
            if (mins == "0")
            {
                return ":00";
            }
            else
            {
                return ":" + mins;
            }

        }

        public string RenderAMPM(Object amp)
        {
            string amp2 = "";
            int amp3 = Convert.ToInt32(amp);
            switch (amp3)
            {
                case 1:
                    amp2 = "AM"; break;
                case 2:
                    amp2 = "PM"; break;
                default:
                    amp2 = "N/A"; break;
            }
            return amp2;
        }

        public string RenderCompleteState(Object c)
        {
            bool cs = Convert.ToBoolean(c);
            string cs2 = "";

            if (cs)
                cs2 = "Done!";
            else
            {
                cs2 = "";
            }

            return cs2;
        }

        public string RenderImageURl(Object c)
        {
            bool cs = Convert.ToBoolean(c);
            string cs2 = "";

            cs2 = cs ? "/images/checkbox_refine_checked_over.gif" : "/images/checkbox_refine_over.gif";

            return cs2;
        }

        public string RenderExerciseValue(Object iEx)
        {
            int exValue = Convert.ToInt32(iEx);

            switch (exValue)
            {
                case 90:
                    return "Session Details";
                case 91:
                    return "Session Details";
                case 92:
                    return "Session Details";
                default:
                    return "";
            }

        }

        protected bool IsValidToSave()
        {
            /*
            bool valid = !(DropDownList1.SelectedValue == "0" ||
                           DropDownList21.SelectedValue == "0" ||
                           DropDownList3.SelectedValue == "0" ||
                           DropDownList4.SelectedValue == "0" ||
                           DropDownList7.SelectedValue == "0" ||
                           DropDownList8.SelectedValue == "0");


            return valid;*/
            return true;
        }

        protected string GetGroup(string template)
        {
            var category = (string)RenderDay(Eval("iWeekDay"));
            if (_currentCategory != category)
            {
                _currentCategory = category;
                return String.Format(template, category);
            }
            else
            {
                return "";
            }
        }

        public static int GetWeekNumber(DateTime dtPassed)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(dtPassed, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            int year = dtPassed.Year;
            string result = "";
            if (weekNum < 10)
            {
                result = Convert.ToString(year) + "0" + Convert.ToString(weekNum);
            }
            else
            {
                result = Convert.ToString(year) + Convert.ToString(weekNum);
            }

            return Convert.ToInt32(result);
        }

        protected static DateTime GetFirstDayOfWeek(int weeknumber, int year)
        {
            int wwwwkk = weeknumber % 100;
            DateTime res = new DateTime(year, 1, 1);
            res = res.AddDays((wwwwkk - 1) * 7 - 4);
            while (WeekNumber(res.Year, res.Month, res.Day) != wwwwkk
            || res.DayOfWeek != DayOfWeek.Monday)
            {
                res = res.AddDays(1);
            }
            return res;
        }

        protected static int WeekNumber(int year, int mon, int day)
        {
            int a = (14 - mon) / 12;
            int y = year + 4800 - a;
            int m = mon + 12 * a - 3;
            int JD = day + (153 * m + 2) / 5 + 365 * y + y / 4 - y / 100 +
            y / 400 - 32045;
            int d4 = (((JD + 31741 - JD % 7) % 146097) % 36524) % 1461;
            int L = d4 / 1460;
            int d1 = ((d4 - L) % 365) + L;
            return d1 / 7 + 1;
        }

        protected internal static int CalculateActualTotalCardio(int wknow, int membernumber) //It needs to change
        {
            var cvdc = new ClubVisionDataContext();

            var totcarEntity = (from trdiary in cvdc.TrainingDiaries
                                join tenum in cvdc.EnumTables on trdiary.cExercise equals tenum.Value
                                where trdiary.iCustomerId == membernumber &&
                                     trdiary.iWeekNumber == wknow && trdiary.bCompleteState == true &&
                                     tenum.OptGroup.Equals("Cardio")//tenum.cField.Equals("1")
                                select trdiary);
            int totCar = 0;
            foreach (TrainingDiary diary in totcarEntity)
                totCar = totCar + diary.iDuration;
            cvdc.Dispose();
            return totCar;
        }

        protected internal static int CalculateActualHardCardio(int wknow, int membernumber)
        {
            var cvdc = new ClubVisionDataContext();

            var totcarEntity = (from trdiary in cvdc.TrainingDiaries
                                join tenum in cvdc.EnumTables on trdiary.cExercise equals tenum.Value
                                where trdiary.iCustomerId == membernumber &&
                                     trdiary.iWeekNumber == wknow &&
                                     trdiary.iIntensity == 2 && 
                                     trdiary.bCompleteState == true //&& tenum.cField.Equals("1")
                                     && tenum.OptGroup.Equals("Cardio")
                                select trdiary);
            int totHarCar = 0;
            foreach (TrainingDiary diary in totcarEntity)
                totHarCar = totHarCar + diary.iDuration;
            cvdc.Dispose();
            return totHarCar;
        }

        protected internal static int CalculateActualWeights(int wknow, int membernumber)
        {
            var cvdc = new ClubVisionDataContext();

            var totWeightsEntity = (from trdiary in cvdc.TrainingDiaries
                                    join tenum in cvdc.EnumTables on trdiary.cExercise equals tenum.Value
                                    where trdiary.iCustomerId == membernumber &&
                                          trdiary.iWeekNumber == wknow &&
                                          trdiary.bCompleteState &&
                                          (tenum.OptGroup == "Resistance" || tenum.OptGroup == "Weight Program")
                                    select trdiary);
            int totWeights = 0;
            foreach (TrainingDiary diary in totWeightsEntity)
                totWeights = totWeights + diary.iDuration;
            cvdc.Dispose();
            return totWeights;

        }

        protected void ImageButton1_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect("/club-vision/my-exercise/training-diary/?when=" + _when);
        }
    }
}