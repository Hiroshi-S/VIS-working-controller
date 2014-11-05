using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace VisionPersonalTrainingProject.usercontrols.clubvision
{
    public partial class TrainingDiaryScreen : System.Web.UI.UserControl
    {
        private System.DateTime _when = System.DateTime.Today;
        private int _weeknumber = 0;
        private List<int?> _cardiosWithProg;
        private DateTime _thismonday;
        private readonly List<int> _vvtExList = new List<int>(){257,256,250,249,248,231,232,233,234,235,237,238,222,223,224};
        private readonly List<int> _vvtIntList = new List<int>() { 54, 55, 56, 57, 242, 243, 244, 246, 252, 253, 259};

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

            _when = _when.AddDays(-7);
            Response.Redirect("/club-vision/my-exercise/training-diary/?when=" + _when.ToString("dd/MM/yyyy"));
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

        protected void ButtonWeekNextClick(object sender, EventArgs e)
        {
            if (Request.QueryString["when"] != null)
            {
                _when = System.DateTime.Parse(Request.QueryString["when"]);
            }

            _when = _when.AddDays(7);
            Response.Redirect("/club-vision/my-exercise/training-diary/?when=" + _when.ToString("dd/MM/yyyy"));
           
        }

        protected void BdpDaySelectionChanged(object sender, EventArgs e)
        {
            Response.Redirect("/club-vision/my-exercise/training-diary/?when=" + bdpDay.SelectedDate.ToString("dd/MM/yyyy"));
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


                var dewiTraining = (from dt in cvdc.TrainingDiaries
                                    where dt.iCustomerId == (int)Session["MemberNo"]
                                    where dt.iWeekNumber == weekNum
                                    select dt).OrderBy(x => x.intValue);
                int day = 0;
                foreach (TrainingDiary td in dewiTraining)
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
                        daycv = RenderDay(td.iWeekDay);
                        litTrainingDiary.Text += "<tr id='" + td.iWeekNumber + "-" + td.iWeekDay+ "' style='height:10px;border-bottom: 1px solid #666666;' ><td colspan='8'></td></tr>";
                    }

                    litTrainingDiary.Text +=
                        "<tr id='" + td.iId + "00'><td style='font-weight:bold;'>" + daycv + "</td><td>";


                    if (day != td.iWeekDay)
                    {
                        litTrainingDiary.Text += "<img title='Add more exercise to "+daycv+"' id='add-" + td.iId + "' onclick=\"addTrainingDiaryRow(" + td.iWeekNumber + "," + td.iWeekDay + ", 'diary');return false;\" src='/images/icon.add.round.jpg'/><br/>";
                    }

                    string typeddlc = "";
                    if((string)Session["MemberType"] == "VPT")
                    {
                        typeddlc = "<td>" + typeddl + "</td>";
                    }

                    //check if it's programmed weights sess
                    string barbelimg = "";
                    if(td.bIsWPExAttached == true)
                    {
                        barbelimg = "<img id='sess-" + td.iId + "' src=\"/images/barbel.jpg\" title=\"Click here to go to session details\" onclick=\"goToWeightsCurrentProgram("+td.iId+");return false;\"/>";
                    }

                    if(td.bIsCardioHasProg == true && Session["MemberType"].ToString().Equals("VVT"))
                    {
                        barbelimg = "<img id='sess-" + td.iId + "' src=\"/images/cardioicon.jpg\" title=\"Click here to go to session details\" onclick=\"goToCardioCurrentProgram(" + td.iId + ", '" + _thismonday.AddDays(td.iWeekDay - 1).ToString("dd/MM/yyyy") + "');return false;\"/>";
                    }

                    litTrainingDiary.Text += "</td><td>" + exsddl + "</td><td>" +
                                             whenddl + "</td><td>" + intensddl +"</td><td>" + durddl
                                             + "</td>" + typeddlc +
                                             "<td><img id='tick-" + td.iId + "' src='" + imageurl + "' onclick=\"saveChangesTrainingRow(" + td.iId + ", 'tickdone');return false;\"  width='15px' style='padding-top:1px;'/></td>" +
                                             "<td><img title='Delete this exercise' id='del-" + td.iId + "' onclick=\"deleteTrainingRow(" + td.iId + ", "+ td.iWeekNumber + ", " + td.iWeekDay +");return false;\" src='/images/icon.trash.jpg' width='22px'/></td>" +
                                             "<td>"+barbelimg+"</td></tr>";
                    day = td.iWeekDay;
                }

            }
        }

        protected void UpdateTrainingTab(System.DateTime when)
        {
            var memberId = (int)Session["MemberNo"];

            _weeknumber = GetWeekNumber(when);

            weekLabel.Text = _weeknumber.ToString(CultureInfo.InvariantCulture);

            var cvdc = new VisionPersonalTrainingProject.ClubVisionDataContext();

            UpdateSqlTrainingData(memberId, _weeknumber);

            _thismonday = GetFirstDayOfWeek(_weeknumber, when.Year);

            literalDay.Text = _thismonday.ToLongDateString();

            currentDate.Value = when.ToString("dd/MM/yyyy");

            GenerateTDUserControls(_weeknumber);

            UpdateTrainingSummary();
            cvdc.Dispose();

            string s = "<script type=\"text/javascript\">" +
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

        public void UpdateSqlTrainingData(int memberId, int currentWeek)
        {
            var cvdc = new VisionPersonalTrainingProject.ClubVisionDataContext();

            var custTdExisxt = (from ctde in cvdc.TrainingDiaries
                                where ctde.iCustomerId == memberId
                                where ctde.bFromVOS == true
                                where ctde.iWeekNumber == currentWeek
                                select ctde);
            
           // CardiosWithProg.Clear();

            _cardiosWithProg = (from bcs in cvdc.EnumTables
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

                bool isInList = _cardiosWithProg.IndexOf(exerciseval) != -1;

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
                bool isInList = _cardiosWithProg.IndexOf(customerTraining.iExValue) != -1;

                var newTD = new TrainingDiary
                                {
                                    iCustomerId = (int) Session["MemberNo"],
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

        protected int GetIntValue(int week, int weekday, int ampm)
        {
            var wk = Convert.ToString(week);
            var wkday = Convert.ToString(weekday);
            var ampm2 = Convert.ToString(ampm);

            string ival = wk + wkday + ampm2;

            int intval = Convert.ToInt32(ival);

            return intval;
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

        protected void UpdateTrainingSummary()
        {
            int currentWeek = Convert.ToInt32(weekLabel.Text);
            int actualTotalCardio = ProfileTab.CalculateActualTotalCardio(currentWeek, Convert.ToInt32(Session["MemberNo"]));
            int actualHardCar = ProfileTab.CalculateActualHardCardio(currentWeek, Convert.ToInt32(Session["MemberNo"]));
            int actualWeights = ProfileTab.CalculateActualWeights(currentWeek, Convert.ToInt32(Session["MemberNo"]));
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
                        var custdet = (from ct in cvdc.WeeklyTrainingSummaries
                                       where ct.iCustomerID == (int)Session["MemberNo"]
                                       where ct.iWeekNumber == 0
                                       select ct).Select(x => new { x.iActualHardCardio, x.iActualTotCardio }).SingleOrDefault();

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
            ActualWeightsLiteral.Text = "<input readonly type='text' id='ActualWeightsTextBox' value='" + actualWeights + "' />";
            ActualTotalCardioLiteral.Text = "<input readonly type='text' id='ActualTotalCardioTextBox' value='" + actualTotalCardio + "' />";

            HardCardioReqLiteral.Text = "<input readonly type='text' id='HardCardioReqTextBox' value='" + Convert.ToInt32(iHardCardioReq) + "' />";
            WeightsReqLiteral.Text = "<input readonly type='text' id='WeightsReqTextBox' value='" + iWeightsReq + "' />";
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
                WeightsPtAchievedLiteral.Text = "<input readonly style='color:green;' type='text' id='WeightsPtAchievedTextBox' value='YES' />";
                weightAch = true;
            }
            else
            {
                WeightsPtAchievedLiteral.Text = "<input readonly style='color:red;' type='text' id='WeightsPtAchievedTextBox' value='NO' />";
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

        protected int CalculateActualTotalCardio(int wknow, int membernumber) //It needs to change
        {
            var cvdc = new ClubVisionDataContext();

            var totcarEntity = (from trdiary in cvdc.TrainingDiaries
                                join tenum in cvdc.EnumTables on trdiary.cExercise equals tenum.Value
                                where trdiary.iCustomerId == membernumber &&
                                     trdiary.iWeekNumber == wknow && trdiary.bCompleteState == true &&
                                     tenum.cField.Equals("1")
                                select trdiary);
            int totCar = 0;
            foreach (TrainingDiary diary in totcarEntity)
                totCar = totCar + diary.iDuration;
            cvdc.Dispose();
            return totCar;
        }

        protected int CalculateActualHardCardio(int wknow, int membernumber)
        {
            var cvdc = new ClubVisionDataContext();

            var totcarEntity = (from trdiary in cvdc.TrainingDiaries
                                join tenum in cvdc.EnumTables on trdiary.cExercise equals tenum.Value
                                where trdiary.iCustomerId == membernumber &&
                                     trdiary.iWeekNumber == wknow &&
                                     trdiary.iIntensity == 2 && trdiary.bCompleteState == true &&
                                     tenum.cField.Equals("1")
                                select trdiary);
            int totHarCar = 0;
            foreach (TrainingDiary diary in totcarEntity)
                totHarCar = totHarCar + diary.iDuration;
            cvdc.Dispose();
            return totHarCar;
        }

        protected int CalculateActualWeights(int wknow, int membernumber)
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

    }
}