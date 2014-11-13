using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

/*
    [1] 269, 270, 271 added to exclude list(App's workout sessions:Weight Session,Cardio Session,Cardio Challenge) Hiroshi
*/

namespace VisionPersonalTrainingProject.usercontrols.VVT
{
    public partial class TrainingDiaryScreen : System.Web.UI.UserControl
    {
        private DateTime _when = System.DateTime.Today;
        private int _weeknumber = 0;
        private static List<int?> _cardiosWithProg;

        private readonly List<int> _vvtExList = new List<int>() { 257, 256, 250, 249, 248, 231, 232, 233, 234, 235, 237, 238, 222, 223, 224, 269, 270, 271 };//[1]
        private readonly List<int> _vvtIntList = new List<int>() { 54, 55, 56, 57, 242, 243, 244, 246, 252, 253, 259, 269, 270, 271 };//[1]

        private DateTime _startDay;
        private int _intStartDay = 1;
        private string _currentCategory = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["when"] != null)
                {
                    _when = System.DateTime.Parse(Request.QueryString["when"]);
                }

                currentDate.Value = _when.ToString("dd/MM/yyyy");
                bdpDay.SelectedDate = _when;
                UpdateTrainingTab(_when);
            }
        }

        protected void ButtonWeekPrevClick(object sender, EventArgs e)
        {
            if (Request.QueryString["when"] != null)
            {
                _when = System.DateTime.Parse(Request.QueryString["when"]);
            }
            else
            {
                _when = Convert.ToDateTime(literalDay.Text);
            }

            _when = _when.AddDays(-7);
            Response.Redirect("/club-vision/my-exercise/training-diary/?when=" + _when.ToString("dd/MM/yyyy") + "&fromBPDay=true");
        }

        protected void ButtonWeekNextClick(object sender, EventArgs e)
        {
            if (Request.QueryString["when"] != null)
            {
                _when = System.DateTime.Parse(Request.QueryString["when"]);
            }
            else
            {
                _when = Convert.ToDateTime(literalDay.Text);
            }

            _when = _when.AddDays(7);


            Response.Redirect("/club-vision/my-exercise/training-diary/?when=" + _when.ToString("dd/MM/yyyy") + "&fromBPDay=true");

        }

        protected void BdpDaySelectionChangeStartDay(object sender, EventArgs e)
        {
            _when = bdpDay.SelectedDate;
            Response.Redirect("/club-vision/my-exercise/training-diary/?when=" + _when.ToString("dd/MM/yyyy") + "&fromBPDay=true");
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

        protected void GenerateTDUserControls(int weekNum)
        {
            //currentDate.Value = when.ToString("dd/MM/yyyy");
            /*****************************************SQL DATA SOURCE***********************************************/
            /*Create dropdown list for exercise*/
            using (ClubVisionDataContext cvdc = new ClubVisionDataContext())
            {

                var exesvalue = (from ex in cvdc.EnumTables
                                 where ex.ID == 9
                                 where ex.OptGroup != "Weight Program"
                                 where (string)Session["MemberType"] == "VPT" ? !_vvtExList.Contains(ex.iID) : !_vvtIntList.Contains(ex.iID)
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

                /*Create dropdown list type  ACHTUNG : INTERNAL ONLY*/
                var typevalue = (from tv in cvdc.EnumTables
                                 where tv.ID == 8
                                 select tv).OrderBy(x => x.intValue);
                string types = Enumerable.Aggregate(typevalue, "", (current, enumTable) => current + ("<option value='" + enumTable.intValue + "'>" + enumTable.Value + "</option>"));
                /*****************************************SQL DATA SOURCE***********************************************/


                var tdsThisWeek = (from dt in cvdc.TrainingDiaries
                                   where dt.iCustomerId == (int)Session["MemberNo"]
                                   where dt.iWeekNumber == weekNum
                                   select dt).OrderBy(x => x.intValue);

                if (Session["MemberType"].Equals("VPT"))
                {
                    if (Request.QueryString["fromBPDay"] != null)
                    {
                        tdsThisWeek = (from twtd in cvdc.TrainingDiaries
                                       where twtd.When >= Convert.ToDateTime(Request.QueryString["when"])
                                       where twtd.When < Convert.ToDateTime(Request.QueryString["when"]).AddDays(7)
                                       where twtd.iCustomerId == (int)Session["MemberNo"]
                                       select twtd).OrderBy(x => x.When);
                    }
                    else
                    {
                        tdsThisWeek = (from twtd in cvdc.TrainingDiaries
                                       where twtd.When >= _startDay
                                       where twtd.When < _startDay.AddDays(7)
                                       where twtd.iCustomerId == (int)Session["MemberNo"]
                                       select twtd).OrderBy(x => x.When);
                    }
                }

                int day = 0;
                foreach (TrainingDiary td in tdsThisWeek)
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

                    string typeddl = "<select class='intenslist' id='typeddl-" + td.iId + "' onchange=\"saveChangesTrainingRow(" + td.iId + ", 'type', $(this).val());return false;\">" + types + "</select>";
                    typeddl = typeddl.Replace("<option value='" + td.iExerType + "'>",
                                              "<option value='" + td.iExerType + "' selected='selected'>");
                    string daycv = "";
                    string imageurl = td.bCompleteState
                                          ? "/images/checkbox_refine_checked_over_red.gif"
                                          : "/images/checkbox_refine_over_red.gif";

                    if (day != td.iWeekDay)
                    {
                        daycv = ((DateTime)td.When).ToString("dddd dd/MM");
                        litTrainingDiary.Text += "<tr id='" + td.iWeekNumber + "-" + td.iWeekDay + "' style='height:10px;border-bottom: 1px solid #666666;' ><td colspan='8'></td></tr>";
                    }

                    litTrainingDiary.Text += td.When == DateTime.Today ?
                        "<tr class='todaytr' id='" + td.iId + "00'><td style='font-weight:bold;' >" + daycv + "</td><td>" :
                        "<tr id='" + td.iId + "00'><td style='font-weight:bold;' >" + daycv + "</td><td>";


                    if (day != td.iWeekDay)
                    {
                        litTrainingDiary.Text += "<img class=\"buttonIcon\" title='Add more exercise to " + daycv + "' id='add-" + td.iId + "' " +
                                                 "onclick=\"addTrainingDiaryRow(" + td.iWeekNumber + "," + td.iWeekDay + ", '" + Convert.ToDateTime(td.When).ToString("dd/MM/yyyy") + "', 'diary');return false;\" src='/images/icons/web/plus.jpg' data-alt-src='/images/icons/web/plus-red.jpg'/><br/>";
                    }

                    string typeddlc = "";
                    if ((string)Session["MemberType"] == "VPT")
                    {
                        typeddlc = "<td>" + typeddl + "</td>";
                    }

                    //check if it's programmed weights sess
                    string barbelimg = "";
                    if (td.bIsWPExAttached == true)
                    {
                        barbelimg = "<img id='sess-" + td.iId + "' src=\"/images/barbel.jpg\" title=\"Click here to go to session details\" onclick=\"goToWeightsCurrentProgram(" + td.iId + ");return false;\"/>";
                    }

                    if (td.bIsCardioHasProg == true && Session["MemberType"].ToString().Equals("VVT"))
                    {
                        barbelimg = "<img id='sess-" + td.iId + "' src=\"/images/cardioicon.jpg\" title=\"Click here to go to session details\" onclick=\"goToCardioCurrentProgram(" + td.iId + ", '" + _startDay.AddDays(td.iWeekDay - 1).ToString("dd/MM/yyyy") + "');return false;\"/>";
                    }

                    litTrainingDiary.Text += "</td><td>" + exsddl + "</td><td>" +
                                             whenddl + "</td><td>" + intensddl + "</td><td>" + durddl
                                             + "</td>" + typeddlc +
                                             "<td><img id='tick-" + td.iId + "' src='" + imageurl + "' onclick=\"saveChangesTrainingRow(" + td.iId + ", 'tickdone');return false;\"  width='15px' style='padding-top:1px;'/></td>" +
                                             "<td><img class=\"buttonIcon\" title='Delete this exercise' id='del-" + td.iId + "' onclick=\"deleteTrainingRow(" + td.iId + ", " + td.iWeekNumber + ", " + td.iWeekDay + ");return false;\" src='/images/icons/web/delete.png' data-alt-src='/images/icons/web/delete-red.png' width='18px'/></td>" +
                                             "<td>" + barbelimg + "</td></tr>";
                    day = td.iWeekDay;
                }

                var tdsVerLastTr = tdsThisWeek.OrderByDescending(x => x.intValue).Take(1).SingleOrDefault();

                if (tdsVerLastTr != null)
                {
                    litTrainingDiary.Text += "<tr id='" + tdsVerLastTr.iWeekNumber + "-" + (tdsVerLastTr.iWeekDay + 1) + "' style='height:10px;border-bottom: 1px solid #666666;' ><td colspan='8'></td></tr>";
                }
            }
        }

        protected void UpdateTrainingTab(System.DateTime when)
        {
            var cvdc = new ClubVisionDataContext();

            var singleOrDefault = (from csts in cvdc.TrainingDiaries
                                   where csts.iCustomerId == (int)Session["MemberNo"]
                                   where csts.iWeekNumber == 0
                                   select csts.iWeekStart).FirstOrDefault();
            if (singleOrDefault != null)
            {
                _intStartDay = (int)singleOrDefault;
            }

            int currentDayOfWeek = (int)(when.DayOfWeek); //because c# day of week start from 0 - 6 
            if (currentDayOfWeek == 0) { currentDayOfWeek = 7; } //validation for sunday

            _weeknumber = GetWeekNumber(when);

            if (Session["MemberType"].Equals("VVT"))
            {
                _startDay = GetFirstDayOfWeek(_weeknumber);
            }
            else
            {
                _startDay = when;

                if (_intStartDay > currentDayOfWeek) //it start from last week
                {
                    _startDay = _startDay.AddDays(0 - currentDayOfWeek - (7 - _intStartDay));
                    _weeknumber = _weeknumber - 1;
                }
                else
                {
                    _startDay = _startDay.AddDays(_intStartDay - currentDayOfWeek);
                }
                FirstLoadNotifLiteral.Text = "<br/><h2 style=\"color:#999999;width:575px; height:35px;display:inline-table;\">Your week with your trainer starts on <span style=\"color:#002147;\">" + _startDay.ToString("dddd") + "</span></h2>";
                if (Session["FromTrainer"].Equals("YES") && ((int)_startDay.DayOfWeek == _intStartDay))
                {
                    if (Request.QueryString["when"] == null)//pushback one week earlier
                    {
                        Response.Redirect("/club-vision/my-exercise/training-diary/?when=" + _startDay.AddDays(-7).ToString("dd/MM/yyyy") + "&tab=week");
                    }
                    FirstLoadNotifLiteral.Text +=
                        "<button onclick=\"updateWeeklyReportTD();return false;\">Update Weekly Report</button>";
                }

            }
            currentWeekNumber.Value = _weeknumber.ToString(CultureInfo.InvariantCulture);
            literalDay.Text = _startDay.ToLongDateString();
            if (Request.QueryString["when"] != null)
            {
                literalDay.Text = Convert.ToDateTime(Request.QueryString["when"]).ToLongDateString();
            }
            currentDate.Value = when.ToString("dd/MM/yyyy");
            currentStartDay.Value = _startDay.ToString("dd/MM/yyyy");

            UpdateSqlTrainingData((int)Session["MemberNo"], _weeknumber, (string)Session["MemberType"], _intStartDay, _startDay);

            if (Request.QueryString["fromBPDay"] != null)
            {
                UpdateSqlTrainingData((int)Session["MemberNo"], _weeknumber + 1, (string)Session["MemberType"], _intStartDay, _startDay.AddDays(7));
            }

            GenerateTDUserControls(_weeknumber);

            LitSummaryPanel.Text = UpdateTrainingSummary(_weeknumber, (int)Session["MemberNo"], (string)Session["MemberType"], Request.QueryString["fromBPDay"] != null ? when : _startDay, false);

            cvdc.Dispose();

            const string s = "<script type=\"text/javascript\">" +
                             "callMotivationCaptainOnTrainingDiary();</script>";
            Page.ClientScript.RegisterStartupScript(GetType(), "test", s);

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

        public static void UpdateSqlTrainingData(int memberId, int currentWeek, string memberType, int weekStartDay, DateTime startDay)
        {
            var cvdc = new VisionPersonalTrainingProject.ClubVisionDataContext();
            IQueryable<TrainingDiary> custTdExisxt;
            // CardiosWithProg.Clear();

            _cardiosWithProg = (from bcs in cvdc.EnumTables
                                where bcs.ID == 9
                                where bcs.OptGroup.Equals("Cardio")
                                where bcs.cField == 1
                                select bcs.intValue).ToList();

            if (memberType.Equals("VVT"))
            {
                custTdExisxt = (from ctde in cvdc.TrainingDiaries
                                where ctde.iCustomerId == memberId
                                where ctde.bFromVOS == true
                                where ctde.iWeekNumber == currentWeek
                                select ctde);

                if (!custTdExisxt.Any()) //just when it does not exist the disco starts!!
                {
                    GenerateWeeklyExercises(currentWeek, memberId, weekStartDay, startDay);
                }
            }
            else
            {
                custTdExisxt = (from ctde in cvdc.TrainingDiaries
                                where ctde.iCustomerId == memberId
                                //where ctde.bFromVOS == true
                                where ctde.When >= startDay
                                where ctde.When < startDay.AddDays(7)
                                select ctde);

                if (custTdExisxt.GroupBy(x => x.When).Select(group => group.First()).Count() < 7) //just when it does not exist the disco starts!!
                {
                    //cvdc.TrainingDiaries.DeleteAllOnSubmit(custTdExisxt);
                    //cvdc.SubmitChanges();
                    GenerateWeeklyExercises(currentWeek, memberId, weekStartDay, startDay); //change the weekstart day
                }
            }

            cvdc.Dispose();
        }

        public static void GenerateWeeklyExercises(int weekNumber, int memberId, int weekStartDay, DateTime startDay)
        {
            var cvdc = new ClubVisionDataContext();
            //int count = 0;
            var ctdList = new List<TrainingDiary>();

            var td = cvdc.GetTrainingDiaryW0InOrder(weekStartDay, memberId);

            DateTime dwhen = startDay;

            foreach (GetTrainingDiaryW0InOrderResult customerTraining in td)
            {
                bool isInList = _cardiosWithProg.IndexOf(customerTraining.iExValue) != -1;

                if (weekStartDay != customerTraining.iWeekDay)
                {
                    if (weekStartDay > customerTraining.iWeekDay)
                    {
                        weekNumber = weekNumber + 1;
                    }

                    weekStartDay = customerTraining.iWeekDay;
                    dwhen = dwhen.AddDays(1);
                }

                //checking similar exercise
                var simex = (from exes in cvdc.TrainingDiaries
                             where exes.iCustomerId == memberId
                             /*
                             where exes.iWeekNumber == weekNumber
                             where exes.iWeekDay == customerTraining.iWeekDay
                             where exes.iAMPM == customerTraining.iAMPM
                              */
                             where exes.intValue == GetIntValue(weekNumber, customerTraining.iWeekDay, customerTraining.iAMPM)
                             where exes.iDuration == customerTraining.iDuration
                             where exes.iExerType == customerTraining.iExerType
                             where exes.iIntensity == customerTraining.iIntensity
                             where exes.iExValue == customerTraining.iExValue
                             select exes);

                if (!simex.Any())
                {
                    var newTD = new TrainingDiary();
                    newTD.iCustomerId = memberId;
                    newTD.iWeekNumber = weekNumber;
                    newTD.iWeekDay = customerTraining.iWeekDay;
                    newTD.iAMPM = customerTraining.iAMPM;
                    newTD.cExercise = customerTraining.cExercise;
                    newTD.iDuration = customerTraining.iDuration;
                    newTD.iExerType = customerTraining.iExerType;
                    newTD.iIntensity = customerTraining.iIntensity;
                    newTD.dDateCreated = DateTime.Now;
                    newTD.bCompleteState = false;
                    newTD.intValue = GetIntValue(weekNumber, customerTraining.iWeekDay, customerTraining.iAMPM);
                    newTD.bFromVOS = true;
                    newTD.iExValue = customerTraining.iExValue;
                    newTD.bIsWPExAttached = customerTraining.bIsWPExAttached;
                    newTD.bIsCardioHasProg = isInList;
                    //newTD.When = dwhen;
                    DateTime? dwhento = DateTime.Now;
                    cvdc.GetDateFromWeekNumberResult(Convert.ToInt32(weekNumber.ToString().Substring(4, 2)),
                                                     Convert.ToInt32(weekNumber.ToString().Substring(0, 4)),
                                                     customerTraining.iWeekDay, ref dwhento);
                    newTD.When = dwhento;
                    newTD.iWeekStart = customerTraining.iWeekStart;
                    //count++;
                    newTD.iSessionId = customerTraining.iSessionId;
                    //newTD.iSessionId = count;
                    ctdList.Add(newTD);
                }
            }

            cvdc.TrainingDiaries.InsertAllOnSubmit(ctdList);

            cvdc.SubmitChanges();
            cvdc.Dispose();
        }

        protected static int GetIntValue(int week, int weekday, int ampm)
        {
            var wk = Convert.ToString(week);
            var wkday = Convert.ToString(weekday);
            var ampm2 = Convert.ToString(ampm);

            string ival = wk + wkday + ampm2;

            int intval = Convert.ToInt32(ival);

            return intval;
        }

        public static DateTime GetFirstDayOfWeek(int weeknumber)
        {
            int year = Convert.ToInt32(weeknumber.ToString().Substring(0, 4));
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

        public static string UpdateTrainingSummary(int currentWeek, int memberId, string membertype, DateTime startday, bool isForAPI)
        {
            string summarybuilder = "";
            string summaryforapi = "";
            int actualTotalCardio = CalculateActualTotalCardio(currentWeek, memberId, membertype, startday);
            int actualHardCar = CalculateActualHardCardio(currentWeek, memberId, membertype, startday);
            int actualWeights = CalculateActualWeights(currentWeek, memberId, membertype, startday);
            bool hardCarAch = false, weightAch = false, totCarAch = false;
            int iWeightsReq = 0;
            decimal iHardCardioReq = 0; //this one has to be decimal
            int iTotcardioReq = 0;

            ClubVisionDataContext cvdc = new ClubVisionDataContext();


            var ifWTSexists = (from wtscheck in cvdc.WeeklyTrainingSummaries
                               where wtscheck.iCustomerID == memberId
                               where wtscheck.iWeekNumber == currentWeek
                               select wtscheck);

            WeeklyTrainingSummary wts = new WeeklyTrainingSummary();
            bool isNew = true;

            var tdIids = (from tdi in cvdc.TrainingDiaries
                          where tdi.When >= startday
                          where tdi.When < startday.AddDays(7)
                          where tdi.iCustomerId == memberId
                          where tdi.bIsExtra == true
                          select tdi.iId);

            var extraCardios = (from ecas in cvdc.TrainingDiaryExtraMinutes
                                where tdIids.Contains(ecas.iTrainingDiaryRefId)
                                select ecas);

            foreach (WeeklyTrainingSummary weeklyTrainingSummary in ifWTSexists)
            {
                wts = weeklyTrainingSummary;
                isNew = false;
            }

            switch (membertype) //getting the requirements here
            {
                case "VPT":
                    {
                        var custdet = (from ct in cvdc.Customers
                                       where ct.Id == memberId
                                       select ct).SingleOrDefault();

                        iWeightsReq = Convert.ToInt32(custdet.Weights);
                        iHardCardioReq = Convert.ToDecimal(custdet.CardioHard);
                        //iTotcardioReq = isNew ? Convert.ToInt32(custdet.Cardio) : wts.iTotCardioReq;
                        iTotcardioReq = Convert.ToInt32(custdet.Cardio);
                        iTotcardioReq = Enumerable.Aggregate(extraCardios, iTotcardioReq, (current, trainingDiaryExtraM) => current + trainingDiaryExtraM.iMinutes);
                    } break;
                case "VVT":
                    {
                        //todo : dewi please clean up this mess, it should have taken the required amount from goals not weeklytrainingsummary
                        /*
                         * var custdet = (from ct in cvdc.WeeklyTrainingSummaries
                                       where ct.iCustomerID == memberId
                                       where ct.iWeekNumber == 0
                                       select ct).Select(x => new { x.iActualHardCardio, x.iActualTotCardio }).SingleOrDefault();
                        */
                        var custdet = (from cst in cvdc.Goals
                                       where cst.dDateCreated <= startday
                                       where cst.iCustomerID == memberId
                                       orderby cst.dDateCreated descending, cst.iID
                                       select cst).FirstOrDefault();

                        iWeightsReq = 60;
                        if (custdet != null)
                        {
                            //iHardCardioReq = custdet.iActualHardCardio; //this one has to be decimal
                            //iTotcardioReq = isNew ? custdet.iActualTotCardio : wts.iTotCardioReq;

                            iHardCardioReq = custdet.iHardCardio; //this one has to be decimal
                            iTotcardioReq = isNew ? custdet.iTotalCardio : wts.iTotCardioReq;
                        }
                        else
                        {
                            var custdet2 = (from cst in cvdc.Goals
                                            where cst.iCustomerID == memberId
                                            orderby cst.dDateCreated descending, cst.iID
                                            select cst).FirstOrDefault();

                            iHardCardioReq = custdet2.iHardCardio; //this one has to be decimal
                            iTotcardioReq = isNew ? custdet2.iTotalCardio : wts.iTotCardioReq;
                        }
                    } break;
            }

            //assign value to summarybuilder
            summarybuilder +=
                "<div class='WeeklySummary'><br/><table style=\"width: 100%;\"><tr style=\"height: 30px;\"><td colspan=\"3\"><span style=\"font-weight: bold\">Weekly Training Summary</span></td></tr>" +
                "<tr><td>Actual Total Cardio (mins)<input readonly type='text' id='ActualTotalCardioTextBox' value='" + actualTotalCardio + "' /></td>" +
                    "<td>Total Cardio Required (mins)<input readonly type='text' id='TotalCardioReqTextBox' value='" + iTotcardioReq + "' /></td>" +
                    "<td>Total Cardio Achieved";

            if (actualTotalCardio >= iTotcardioReq)
            {
                summarybuilder += "<input readonly style='color:green;' type='text' id='TotalCardioAchievedTextBox' value='YES' /></td>";
                totCarAch = true;
            }
            else
            {
                summarybuilder += "<input readonly style='color:red;' type='text' id='TotalCardioAchievedTextBox' value='NO' /></td>";
            }

            summarybuilder += "</tr><tr><td>Actual Hard Cardio (mins)<input readonly type='text' id='ActualHardCardioTextBox' value='" + actualHardCar + "' /></td>" +
                              "<td>Hard Cardio Required (mins)<input readonly type='text' id='HardCardioReqTextBox' value='" + Convert.ToInt32(iHardCardioReq) + "' /></td>" +
                              "<td>Hard Cardio Achieved";

            if (actualHardCar >= iHardCardioReq)
            {
                summarybuilder += "<input readonly style='color:green;' type='text' id='HardCardioAchievedTextBox' value='YES' /></td></tr>";
                hardCarAch = true;
            }
            else
            {
                summarybuilder += "<input readonly style='color:red;' type='text' id='HardCardioAchievedTextBox' value='NO' /></td></tr>";
            }

            summarybuilder += "<tr><td>Actual Weights (mins)<input readonly type='text' id='ActualWeightsTextBox' value='" + actualWeights + "' /></td>" +
                              "<td>Weights Required (mins)<input readonly type='text' id='WeightsReqTextBox' value='" + iWeightsReq + "' /></td>" +
                              "<td>Weights Achieved";


            if (actualWeights >= iWeightsReq)
            {
                summarybuilder += "<input readonly style='color:green;' type='text' id='WeightsPtAchievedTextBox' value='YES' /></td></tr>";
                weightAch = true;
            }
            else
            {
                summarybuilder += "<input readonly style='color:red;' type='text' id='WeightsPtAchievedTextBox' value='NO' /></td></tr>";
            }

            bool allAchieved = (totCarAch && hardCarAch && weightAch);

            summarybuilder += " </table></div>";

            summaryforapi = "?hardcardio=" + actualHardCar + "&totcardio=" + actualTotalCardio + "&lmcardio=" + (actualTotalCardio - actualHardCar) +
                            "&weights=" + actualWeights + "&bhardcardio=" + hardCarAch + "&btotcardio=" + totCarAch + "&bweights=" + weightAch +
                            "&totcardioreq=" + iTotcardioReq + "&allyes=" + allAchieved;

            wts.iCustomerID = memberId;
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

            return isForAPI ? summaryforapi : summarybuilder;

        }

        public static int CalculateActualTotalCardio(int wknow, int membernumber, string membertype, DateTime startday) //It needs to change
        {
            var cvdc = new ClubVisionDataContext();
            IQueryable<TrainingDiary> totcarEntity;

            if (membertype.Equals("VVT"))
            {
                totcarEntity = (from trdiary in cvdc.TrainingDiaries
                                join tenum in cvdc.EnumTables on trdiary.cExercise equals tenum.Value
                                where trdiary.iCustomerId == membernumber &&
                                     trdiary.iWeekNumber == wknow && trdiary.bCompleteState == true &&
                                     tenum.OptGroup.Equals("Cardio")//tenum.cField.Equals("1")
                                select trdiary);
            }
            else
            {
                totcarEntity = (from trdiary in cvdc.TrainingDiaries
                                join tenum in cvdc.EnumTables on trdiary.cExercise equals tenum.Value
                                where trdiary.iCustomerId == membernumber
                                where trdiary.When >= startday
                                where trdiary.When < startday.AddDays(7)
                                where trdiary.bCompleteState
                                where tenum.OptGroup.Equals("Cardio")//tenum.cField.Equals("1")
                                select trdiary);
            }


            int totCar = 0;
            foreach (TrainingDiary diary in totcarEntity)
                totCar = totCar + diary.iDuration;

            cvdc.Dispose();
            return totCar;
        }

        public static int CalculateActualHardCardio(int wknow, int membernumber, string membertype, DateTime startday)
        {
            var cvdc = new ClubVisionDataContext();
            IQueryable<TrainingDiary> totcarEntity;

            if (membertype.Equals("VVT"))
            {
                totcarEntity = (from trdiary in cvdc.TrainingDiaries
                                join tenum in cvdc.EnumTables on trdiary.cExercise equals tenum.Value
                                where trdiary.iCustomerId == membernumber &&
                                      trdiary.iWeekNumber == wknow &&
                                      trdiary.iIntensity == 2 &&
                                      trdiary.bCompleteState == true //&& tenum.cField.Equals("1")
                                      && tenum.OptGroup.Equals("Cardio")
                                select trdiary);
            }
            else
            {
                totcarEntity = (from trdiary in cvdc.TrainingDiaries
                                join tenum in cvdc.EnumTables on trdiary.cExercise equals tenum.Value
                                where trdiary.iCustomerId == membernumber &&
                                      trdiary.When >= startday &&
                                      trdiary.When < startday.AddDays(7) &&
                                      trdiary.iIntensity == 2 &&
                                      trdiary.bCompleteState //&& tenum.cField.Equals("1")
                                      && tenum.OptGroup.Equals("Cardio")
                                select trdiary);
            }

            int totHarCar = 0;
            foreach (TrainingDiary diary in totcarEntity)
                totHarCar = totHarCar + diary.iDuration;

            cvdc.Dispose();
            return totHarCar;
        }

        public static int CalculateActualWeights(int wknow, int membernumber, string membertype, DateTime startday)
        {
            var cvdc = new ClubVisionDataContext();

            IQueryable<TrainingDiary> totWeightsEntity;

            if (membertype.Equals("VVT"))
            {
                totWeightsEntity = (from trdiary in cvdc.TrainingDiaries
                                    join tenum in cvdc.EnumTables on trdiary.cExercise equals tenum.Value
                                    where trdiary.iCustomerId == membernumber &&
                                          trdiary.iWeekNumber == wknow &&
                                          trdiary.bCompleteState &&
                                          (tenum.OptGroup == "Resistance" || tenum.OptGroup == "Weight Program")
                                    select trdiary);
            }
            else
            {
                totWeightsEntity = (from trdiary in cvdc.TrainingDiaries
                                    join tenum in cvdc.EnumTables on trdiary.cExercise equals tenum.Value
                                    where trdiary.iCustomerId == membernumber &&
                                          trdiary.When >= startday &&
                                          trdiary.When < startday.AddDays(7) &&
                                          trdiary.bCompleteState &&
                                          (tenum.OptGroup == "Resistance" || tenum.OptGroup == "Weight Program")
                                    select trdiary);
            }

            int totWeights = 0;
            foreach (TrainingDiary diary in totWeightsEntity)
                totWeights = totWeights + diary.iDuration;

            cvdc.Dispose();
            return totWeights;

        }

    }
}