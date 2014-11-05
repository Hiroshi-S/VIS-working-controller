using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using VisionPersonalTrainingProject.usercontrols.VVT;
using ProfileTab = VisionPersonalTrainingProject.usercontrols.clubvision.ProfileTab;

namespace VisionPersonalTrainingProject.usercontrols.externalclubvision.initialscreens
{
    public partial class NewTrainingPlan : System.Web.UI.UserControl
    {
        private static List<RowadDropDownList> rowadDropDownLists;
        private static int _reqHardCardio;
        private static int _reqTotalCario;
        private readonly List<int> _vvtExList = new List<int>() { 257, 256, 250, 249, 248, 231, 232, 233, 234, 235, 237, 238, 222, 223, 224 };
        private readonly List<int> _vvtIntList = new List<int>() { 54, 55, 56, 57, 242, 243, 244, 246, 252, 253, 259 }; //this is the iid not intvalue

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Response.Redirect("www.google.com");
                ClubVisionDataContext cvdc = new ClubVisionDataContext();

                var weightExercise = (from el in cvdc.EnumTables
                                      where el.ID == 9
                                      where el.OptGroup == "Resistance"
                                      select el).OrderBy(x => x.Value);
                var legEL = weightExercise.Where(x => x.floatValue == 1);
                var upperPushEL = weightExercise.Where(x => x.floatValue == 2);
                var upperPullEL = weightExercise.Where(x => x.floatValue == 3);
                var coreEL = weightExercise.Where(x => x.floatValue == 4);

                var daysList = (from il in cvdc.EnumTables
                                where il.ID == 10
                                where il.intValue != 0
                                select il).OrderBy(x => x.intValue);

                var daysDDList = new List<ListBox>() { daysBeginner, daysIntermediate, daysAdvanced };
                foreach (ListBox listBox in daysDDList)
                {
                    listBox.DataTextField = "Value";
                    listBox.DataValueField = "intValue";
                    listBox.DataSource = daysList;
                    listBox.DataBind();
                }
                
                var legDDList = new List<ListBox>() { exListBoxLegInterm, exListBoxLegBeginner };
                foreach (ListBox listBox in legDDList)
                {
                    listBox.DataTextField = "Value";
                    listBox.DataValueField = "intValue";
                    listBox.DataSource = legEL;
                    listBox.DataBind();
                }

                var upushList = new List<ListBox>() { exListBoxUpPushInterm, exListBoxUpPushBeginner };
                foreach (ListBox listBox in upushList)
                {
                    listBox.DataTextField = "Value";
                    listBox.DataValueField = "intValue";
                    listBox.DataSource = upperPushEL;
                    listBox.DataBind();
                }

                var upullList = new List<ListBox>() { exListBoxUpPullBeginner, exListBoxUpPullInterm };
                foreach (ListBox listBox in upullList)
                {
                    listBox.DataTextField = "Value";
                    listBox.DataValueField = "intValue";
                    listBox.DataSource = upperPullEL;
                    listBox.DataBind();
                }

                var coreList = new List<ListBox>() { exListBoxCoreBeginner, exListBoxCoreInterm };
                foreach (ListBox listBox in coreList)
                {
                    listBox.DataTextField = "Value";
                    listBox.DataValueField = "intValue";
                    listBox.DataSource = coreEL;
                    listBox.DataBind();
                }


                for (int i = 1; i < 100; i += 5)
                {
                    DropDownList ddl = this.FindControl("DropDownList" + i.ToString()) as DropDownList;
                    if (ddl != null) ddl.Attributes["OnChange"] = "RestDayFunction(this);";
                }

                WeightSessions ws = new WeightSessions();

                currentWeekNum.InnerText = "" + ws.GetWeekNumber(DateTime.Today);

                CheckExistingPlan();
                UpdateTrainingSummary();
            }

        }

        protected void GoBackToTrainingScreen()
        {
            string s = "<script cardioType=\"text/javascript\">" +
                             "document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_profileTab').style.backgroundImage = 'url(/images/ExtClubVision/eHdrProfileTabTrainingPlan.gif)';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_eMeasurementsTabMeasure').style.display = 'none';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_eMeasurementsTabGoals').style.display = 'none';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_eMeasurementsTabTrain').style.display = 'block';" +
                             "document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tabTrainingPlan2').style.display = 'block';" +
                             "document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tabTrainingPlan1').style.display = 'none';" +
                             "</script>";

            Page.ClientScript.RegisterStartupScript(GetType(), "test", s);
        }

        protected void CompileTrainingList(int wkday, int ampm, string cexercise, int dur, int intensity, int intval, int iexvalue, List<TrainingDiary> list)
        {
            if ((wkday != 0 && ampm != 0 && dur != 0 && iexvalue != 0 && intensity != 0) ||
                (wkday != 0 && ampm == 1 && dur == 0 && iexvalue == 45 && intensity == 0))
            {
                TrainingDiary td = new TrainingDiary();

                td.iCustomerId = Convert.ToInt32(Session["MemberNo"]);
                td.iWeekNumber = 0;
                td.iWeekDay = wkday;
                td.iAMPM = ampm;
                td.cExercise = cexercise;
                td.iDuration = dur;
                td.iExerType = 0;
                td.iIntensity = intensity;
                td.dDateCreated = DateTime.Now;
                td.bCompleteState = true;
                td.bFromVOS = true;
                td.intValue = intval;
                td.iExValue = iexvalue;

                list.Add(td);
            }

        }

        protected void CheckExistingPlan()
        {
            try
            {
                /*****************************************SQL DATA SOURCE***********************************************/
                /*Create dropdown list for exercise*/
                ClubVisionDataContext cvdc = new ClubVisionDataContext();
                var exesvalue = (from ex in cvdc.EnumTables
                                 where ex.ID == 9
                                 where (ex.OptGroup == "Cardio" || ex.iID == 122 || ex.iID == 86)
                                 where !_vvtIntList.Contains(ex.iID)
                                 where ex.floatValue == null
                                 select ex).OrderBy(x => x.Value).OrderBy(x => x.OptGroup);
                string exes = "";
                string optgroup = "";
                foreach (EnumTable enumTable in exesvalue)
                {
                    if (!optgroup.Equals(enumTable.OptGroup))
                    {
                        if (!optgroup.Equals(""))
                        {
                            exes += "</optgroup>";
                        }
                        if (!optgroup.Equals("Weight Program"))
                        {
                            exes += "<optgroup label='" + enumTable.OptGroup + "'>";
                        }
                    }
                    exes += "<option value='" + enumTable.intValue + "'>" + enumTable.Value + "</option>";
                    optgroup = enumTable.OptGroup;
                }

                /*Create dropdown list for when*/
                var whenvalue = (from ex in cvdc.EnumTables
                                 where ex.ID == 5
                                 select ex).OrderBy(x => x.intValue);
                string whens = Enumerable.Aggregate(whenvalue, "", (current, enumTable) => current + ("<option value='" + enumTable.intValue + "'>" + enumTable.Value + "</option>"));

                /*Create dropdown list for duration*/
                var durvalue = (from dur in cvdc.EnumTables
                                where dur.ID == 7
                                select dur).OrderBy(x => x.intValue);
                string durs = Enumerable.Aggregate(durvalue, "", (current, enumTable) => current + ("<option value='" + enumTable.intValue + "'>" + enumTable.Value + "</option>"));

                /*Create dropdown list intensity*/
                var intensvalue = (from inss in cvdc.EnumTables
                                   where inss.ID == 6
                                   select inss).OrderBy(x => x.intValue);
                string ins = Enumerable.Aggregate(intensvalue, "", (current, enumTable) => current + ("<option value='" + enumTable.intValue + "'>" + enumTable.Value + "</option>"));

                /*****************************************SQL DATA SOURCE***********************************************/ 
          


                var cep = (from ceps in cvdc.TrainingDiaries
                           where ceps.iCustomerId == Convert.ToInt32(Session["MemberNo"])
                           where ceps.iWeekNumber == 0
                           where ceps.bFromVOS == true
                           select ceps).OrderBy(x => x.intValue);

                if(!cep.Any())
                {   
                    //STEP 1. CREATE TRAINING DIARY WEEK 0
                    List<TrainingDiary> ltd = new List<TrainingDiary>();
                    
                    for(int i = 1 ; i < 8; i++)
                    {//create entries automatically.. put restday!
                        TrainingDiary td = new TrainingDiary();
                        td.iCustomerId = (int) Session["MemberNo"];
                        td.iWeekNumber = 0;
                        td.iWeekDay = i;
                        td.iAMPM = 1;
                        td.cExercise = "Rest Day";
                        td.iDuration = 0;
                        td.iExerType = 0;
                        td.iIntensity = 0;
                        td.dDateCreated = DateTime.Now;
                        td.bCompleteState = true;
                        td.intValue = Convert.ToInt32(i + "" + 1);
                        td.bFromVOS = true;
                        td.iExValue = 45;
                        td.bIsExtra = false;
                        ltd.Add(td);
                    }

                    cvdc.TrainingDiaries.InsertAllOnSubmit(ltd);
                    cvdc.SubmitChanges();
                }

                var wkts = (from wttts in cvdc.WeeklyTrainingSummaries
                            where wttts.iCustomerID == (int) Session["MemberNo"]
                            where wttts.iWeekNumber == 0
                            select wttts).SingleOrDefault();
                if(wkts == null) //create weekly training summary if there is none
                {
                    WeeklyTrainingSummary wts2 = new WeeklyTrainingSummary();
                    wts2.iCustomerID = (int) Session["MemberNo"];
                    wts2.iWeekNumber = 0;
                    wts2.iActualHardCardio = 0;
                    wts2.iActualWeights = 0;
                    wts2.iActualTotCardio = 0;

                    //this should be save as the first time they executed would be from initial screens
                    wts2.iHardCardioReq = MyGoals._hardCardio;
                    wts2.iWeightsReq = 60;
                    wts2.iTotCardioReq = MyGoals._totalCardio;

                    wts2.bHardCardioAchieved = false;
                    wts2.bWeightsAchieved = false;
                    wts2.bTotCardioAchieved = false;

                    wts2.dDateSaved = DateTime.Now;

                    cvdc.WeeklyTrainingSummaries.InsertOnSubmit(wts2);
                    cvdc.SubmitChanges();
                }

                if (wkts != null)
                {
                    _reqHardCardio = wkts.iHardCardioReq;
                    _reqTotalCario = wkts.iTotCardioReq;
                }

                int iday = 0;
                foreach (TrainingDiary td in cep)
                {
                    string exsddl = "<select class='exslist' id='exsddl-" + td.iId + "' onchange=\"saveChangesTrainingRow(" + td.iId + ", 'exercise', $(this).val(), $('#exsddl-" + td.iId + " :selected').text() );return false;\">" + exes + "</select>";
                    exsddl = exsddl.Replace("<option value='" + td.iExValue + "'>",
                                            "<option value='" + td.iExValue + "' selected='selected'>");

                    string whenddl = "<select class='whenlist' id='whenddl-" + td.iId + "' onchange=\"saveChangesTrainingRow(" + td.iId + ", 'when', $(this).val());return false;\">" + whens + "</select>";
                    whenddl = whenddl.Replace("<option value='" + td.iAMPM + "'>",
                                              "<option value='" + td.iAMPM + "' selected='selected'>");

                    string durddl = "<select class='whenlist' id='durddl-" + td.iId + "' onchange=\"saveChangesTrainingRow(" + td.iId + ", 'duration', $(this).val());return false;\">" + durs + "</select>";
                    durddl = durddl.Replace("<option value='" + td.iDuration + "'>",
                                            "<option value='" + td.iDuration + "' selected='selected'>");

                    string intensddl = "<select class='intenslist' id='intensddl-" + td.iId + "' onchange=\"saveChangesTrainingRow(" + td.iId + ", 'intensity', $(this).val());return false;\">" + ins + "</select>";
                    intensddl = intensddl.Replace("<option value='" + td.iIntensity + "'>",
                                                  "<option value='" + td.iIntensity + "' selected='selected'>");
                    string sday = "";string imgbutton = "";
                    if (iday != td.iWeekDay)
                    {
                        sday = RenderDay(td.iWeekDay);
                        iday = td.iWeekDay;
                        imgbutton = "<img src=\"/images/icon.add.round.jpg\" title=\"Add more exercise to " + sday + "\" onclick=\"addTrainingDiaryRow(0," + td.iWeekDay + ", 'planner');return false;\"/>";
                        LitExPlanner.Text += "<tr id='" + td.iWeekNumber + "-" + td.iWeekDay + "' style='height:10px;border-bottom: 1px solid #666666;' ><td colspan='8'></td></tr>";
                    }
                    LitExPlanner.Text += "<tr id='" + td.iId + "00'><td style=\"font-weight: bold;\">" + sday + "</td><td>" + imgbutton + "</td>" +
                                            "<td>" + exsddl + "</td>" +
                                            "<td>" + whenddl + "</td>" +
                                            "<td>" + intensddl + "</td>" +
                                            "<td>" + durddl + "</td>" +
                                            "<td><img title='Delete this exercise' id='del-" + td.iId + "' onclick=\"deleteTrainingRow(" + td.iId + ", " + td.iWeekNumber + ", " + td.iWeekDay + ");return false;\" src=\"/images/icon.trash.jpg\" width=\"22px\"/></td></tr>";
                }

                //check if they have weights program installed
                var wprogram = (from wp in cvdc.TrainingDiaryWeightPrograms
                                where wp.iMasterSetOf == (int) Session["MemberNo"]
                                select wp);
                string wprogramchosen = "";
                string reviewProgOnClick = "";

                if (wprogram.Any())
                {
                    var trainingDiaryWeightProgram = wprogram.FirstOrDefault();
                    var weightdays = cep.Where(x => x.iExValue == 81);
                    int progType = 0;
                    if (trainingDiaryWeightProgram != null)
                    {
                        progType = (int) trainingDiaryWeightProgram.iTypeWeightsProg;
                    }
                    switch (progType)
                    {
                        case 1: //beginner
                            {
                                List<ListBox> beginnerLB = new List<ListBox>() { exListBoxLegBeginner, exListBoxUpPushBeginner, exListBoxUpPullBeginner, exListBoxCoreBeginner };
                                foreach(TrainingDiaryWeightProgram tdwp in wprogram)
                                {
                                    beginnerLB[tdwp.iBodyPart-1].SelectedValue = tdwp.iExValue.ToString();
                                }
                                foreach (TrainingDiary td in weightdays)
                                {
                                    daysBeginner.Items.FindByValue(td.iWeekDay.ToString()).Selected = true;
                                }
                                wprogramchosen = "<h2>Beginner Program</h2>";
                                reviewProgOnClick = "reviewSelectedProgram('fatlossbeginner');return false;";
                            }
                            break;
                        case 2://intermediate
                            {
                                //this will not broken assuming the code execute it really from top to down
                                List<ListBox> IntermLB = new List<ListBox>() { exListBoxLegInterm, exListBoxUpPushInterm, exListBoxUpPullInterm, exListBoxCoreInterm };
                                foreach (TrainingDiaryWeightProgram tdwp in wprogram)
                                {
                                    IntermLB[tdwp.iBodyPart-1].Items.FindByValue(tdwp.iExValue.ToString()).Selected =
                                        true;
                                }
                                foreach (TrainingDiary td in weightdays)
                                {
                                    daysIntermediate.Items.FindByValue(td.iWeekDay.ToString()).Selected = true;
                                }
                                wprogramchosen = "<h2>Intermediate Program</h2>";
                                reviewProgOnClick = "reviewSelectedProgram('fatlossintermediate');return false;";
                            }
                            break;
                        case 3:
                            {
                                foreach (TrainingDiary td in weightdays)
                                {
                                    daysAdvanced.Items.FindByValue(td.iWeekDay.ToString()).Selected = true;
                                }
                                wprogramchosen = "<h2>Advanced Program</h2>";
                                reviewProgOnClick = "reviewSelectedProgram('fatlossadvance');return false;";
                            }
                            break;
                    }

                    divSelectedProgram.Style["display"] = "block";
                    reviewProgram.Attributes["onclick"] = reviewProgOnClick;
                    chosenProgram.InnerHtml = wprogramchosen;
                    divSelectProgram.Style["display"] = "none";
                }
            }
            catch (Exception exception)
            {
                Response.Write(exception.ToString());
            }
        }

        protected void UpdateTrainingSummary()
        {
            try
            {
                int aHardCardio = ProfileTab.CalculateActualHardCardio(0, Convert.ToInt32(Session["MemberNo"]));
                int aTotalCardio = ProfileTab.CalculateActualTotalCardio(0, Convert.ToInt32(Session["MemberNo"]));
                int aLMCardio = aTotalCardio - aHardCardio;
                int aWeights = ProfileTab.CalculateActualWeights(0, Convert.ToInt32(Session["MemberNo"]));

                int rHardCardio = _reqHardCardio;
                int rTotalCardio = _reqTotalCario;
                int rLMCardio = rTotalCardio - rHardCardio;
                int rWeights = 60;

                ActualHardCardioTextBox.Text = aHardCardio + "";
                ActualTotalCardioTextBox.Text = aTotalCardio + "";
                ActualLMCardioTextBox.Text = aLMCardio + "";
                ActualWeightsTextBox.Text = aWeights + "";

                HardCardioReqTextBox.Text = rHardCardio + "";
                TotalCardioReqTextBox.Text = rTotalCardio + "";
                LMCardioReqTextBox.Text = rLMCardio + "";
                WeightsReqTextBox.Text = rWeights + "";

                HardCardioAchievedTextBox.Text = rHardCardio <= aHardCardio ? "YES" : "NO";
                HardCardioAchievedTextBox.ForeColor = rHardCardio <= aHardCardio ? Color.Green : Color.Red;

                TotalCardioAchievedTextBox.Text = rTotalCardio <= aTotalCardio ? "YES" : "NO";
                TotalCardioAchievedTextBox.ForeColor = rTotalCardio <= aTotalCardio ? Color.Green : Color.Red;

                LMCardioAchievedTextBox.Text = rLMCardio <= aLMCardio ? "YES" : "NO";
                LMCardioAchievedTextBox.ForeColor = rLMCardio <= aLMCardio ? Color.Green : Color.Red;

                WeightsPtAchievedTextBox.Text = rWeights <= aWeights ? "YES" : "NO";
                WeightsPtAchievedTextBox.ForeColor = rWeights <= aWeights ? Color.Green : Color.Red;
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        protected void FillUpRestDays()
        {
            List<int> missedDayExercise = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
            List<TrainingDiary> tde = new List<TrainingDiary>();

            ClubVisionDataContext cvdc = new ClubVisionDataContext();

            var mdeint = (from ceps in cvdc.TrainingDiaries
                          where ceps.iCustomerId == Convert.ToInt32(Session["MemberNo"])
                          where ceps.bFromVOS == true
                          select ceps.iWeekDay);

            foreach (var i in mdeint)
            {
                missedDayExercise.Remove(i);
            }

            foreach (int i in missedDayExercise)
            {
                CompileTrainingList(i, 1, "Rest Day ", 0, 0, i * 10 + 1, 45, tde);
            }

            cvdc.TrainingDiaries.InsertAllOnSubmit(tde);
            cvdc.SubmitChanges();
            cvdc.Dispose();
        }

        protected void MyGoalImagebuttonNextClick(object sender, ImageClickEventArgs e)
        {
            ClubVisionDataContext cvdc = new ClubVisionDataContext();
            
            var setComplete = (from sc in cvdc.Customer_Externals
                               where sc.iID == Convert.ToInt32(Session["MemberNo"])
                               select sc).SingleOrDefault();

            var checksum = (from wtsum in cvdc.WeeklyTrainingSummaries
                            where wtsum.iCustomerID == (int) Session["MemberNo"]
                            where wtsum.iWeekNumber == 0
                            select wtsum).SingleOrDefault();

            var checkProg = (from cp in cvdc.TrainingDiaryWeightPrograms
                             where cp.iMasterSetOf == (int) Session["MemberNo"]
                             select cp.iID);

            if (checksum != null && checksum.bHardCardioAchieved && checksum.bTotCardioAchieved && checksum.bWeightsAchieved && checkProg.Any()) //if they dont have program, lock them in 
            {
                if (setComplete != null) setComplete.bCompleteInitialState = true;
                cvdc.SubmitChanges();
                cvdc.Dispose();
                Label1.Text = "Success";
                Response.Redirect("/club-vision/my-profile/edit-my-eating-planner/", false);
                //Response.Redirect("/club-vision/my-profile/ext my profile.aspx", false);
            }
            else
            {
                Label1.Text = "";
                messageboard.Style["display"] = "block";
                if (checksum != null)
                {
                    p1.Style["display"] = "none";
                }
            }
        }

        protected void MyGoalImagebuttonBackClick(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect("/club-vision/my-profile/edit-measurements/?tab=goals", false);
        }
        
        protected void ErrorPopup()
        {
            const string s = "<script type=\"text/javascript\">" +
                                 "alert('hahahahahh');</script>";

            Page.ClientScript.RegisterStartupScript(GetType(), "test", s);
        }

        protected void DropDownList6SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList thisddl = sender as DropDownList;
            thisddl.Visible = false;
        }

        protected void DropDownList6ValueChanged(object sender, EventArgs e)
        {

        }

        public DataTable LinqToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names 
            PropertyInfo[] oProps = null;

            if (varlist == null) return dtReturn;

            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others  will follow 
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) ?? DBNull.Value;
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }

        protected void ListBoxValidatorServerValidate(object source, ServerValidateEventArgs args)
        {
            // ListBox thislb = sender 
            int selectionCount = 0;
            foreach (ListItem item in exListBoxLegBeginner.Items)
            {
                if (item.Selected) selectionCount++;
            }
            args.IsValid = (selectionCount >= 1 && selectionCount <= 5);
        }

        protected string RenderDay(int day)
        {
            switch (day)
            {
                case 1:
                    return "Monday";
                case 2:
                    return "Tuesday";
                case 3:
                    return "Wednesday";
                case 4:
                    return "Thursday";
                case 5:
                    return "Friday";
                case 6:
                    return "Saturday";
                case 7:
                    return "Sunday";
                default:
                    return "";
            }
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);

            if (Session["MemberType"].ToString().Equals("VVT") && Session["LockCreateNew"] != null)
            {
                 UpdateCurrentTrainingDiary();
            }
        }

        protected void UpdateCurrentTrainingDiary()
        {
            using (ClubVisionDataContext cvdc = new ClubVisionDataContext())
            {
                var custcompletestate = (from ccst in cvdc.Customer_Externals
                                         where ccst.iID == (int)Session["MemberNo"]
                                         select ccst.bCompleteInitialState).SingleOrDefault();

                //only happens in VVT External dont worry about the flexible date then
                if (custcompletestate)
                {
                    WeightSessions ws = new WeightSessions();

                    int currentWeekNum = ws.GetWeekNumber(DateTime.Today);
                    var currentday = (int)(DateTime.Today.DayOfWeek);

                    if (currentday == 0)
                    {
                        currentday = 7;
                    }

                    var futureTD = (from ftd in cvdc.TrainingDiaries
                                    where ((ftd.iWeekNumber > currentWeekNum) ||
                                            ftd.iWeekNumber == currentWeekNum && ftd.iWeekDay > currentday)
                                    where ftd.iCustomerId == (int)Session["MemberNo"]
                                    where ftd.bFromVOS == true
                                    select ftd);

                    var currAndFutureTDSummary = (from caftd in cvdc.WeeklyTrainingSummaries
                                                  where caftd.iCustomerID == (int)Session["MemberNo"]
                                                  where caftd.iWeekNumber >= currentWeekNum
                                                  select caftd);

                    cvdc.TrainingDiaries.DeleteAllOnSubmit(futureTD);
                    cvdc.WeeklyTrainingSummaries.DeleteAllOnSubmit(currAndFutureTDSummary);
                    cvdc.SubmitChanges();

                    var currentSet = (from css in cvdc.TrainingDiaries
                                      where css.iCustomerId == (int)Session["MemberNo"]
                                      where css.iWeekNumber == 0
                                      where css.iWeekDay > currentday
                                      where css.bFromVOS == true
                                      select css);

                    List<TrainingDiary> ltd = new List<TrainingDiary>();
                    foreach (var trainingDiary in currentSet)
                    {
                        TrainingDiary tdiary = new TrainingDiary();
                        tdiary.iCustomerId = trainingDiary.iCustomerId;
                        tdiary.iWeekNumber = currentWeekNum;
                        tdiary.iWeekDay = trainingDiary.iWeekDay;
                        tdiary.iAMPM = trainingDiary.iAMPM;
                        tdiary.cExercise = trainingDiary.cExercise;
                        tdiary.iDuration = trainingDiary.iDuration;
                        tdiary.iExerType = trainingDiary.iExerType;
                        tdiary.iIntensity = trainingDiary.iIntensity;
                        tdiary.dDateCreated = DateTime.Now;
                        tdiary.bCompleteState = false;
                        tdiary.intValue =
                            Convert.ToInt32(currentWeekNum + "" + trainingDiary.iWeekDay + "" + trainingDiary.iAMPM);
                        tdiary.bFromVOS = true;
                        //newTD.When = dwhen;
                        DateTime? dwhento = DateTime.Now;
                        cvdc.GetDateFromWeekNumberResult(Convert.ToInt32(currentWeekNum.ToString().Substring(4, 2)),
                                                         Convert.ToInt32(currentWeekNum.ToString().Substring(0, 4)),
                                                         trainingDiary.iWeekDay, ref dwhento);
                        tdiary.When = dwhento;
                        tdiary.iExValue = trainingDiary.iExValue;
                        tdiary.bIsWPExAttached = trainingDiary.bIsWPExAttached;
                        tdiary.bIsCardioHasProg = trainingDiary.bIsCardioHasProg;
                        ltd.Add(tdiary);
                    }
                    cvdc.TrainingDiaries.InsertAllOnSubmit(ltd);
                    cvdc.SubmitChanges();
                }

            }
        }
    }

    
}