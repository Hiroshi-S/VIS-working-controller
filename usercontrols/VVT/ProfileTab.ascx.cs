using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace VisionPersonalTrainingProject.usercontrols.VVT
{
    public partial class ProfileTab : System.Web.UI.UserControl
    {
        private System.DateTime _when = System.DateTime.Today;
        private System.DateTime _startDay;
        private int _intStartDay = 1;
        private string _currentCategory = "";
        private int _weeknumber = 0;
        private static List<int?> _cardiosWithProg;

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
                    else
                    {
                        chartProgressData_str = "[[null]]";
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

        protected void ButtonWeekPrevClick(object sender, EventArgs e)
        {
            //_when = System.DateTime.Parse(currentDate.Value);
            _when = System.DateTime.Parse(literalDay.Text);

            _when = _when.AddDays(-7);

            bdpDay.SelectedValue = _when;
            bdpDay2.SelectedValue = _when;

            //UpdateTrainingTab(_when);
            UpdateTrainingTabPostback(_when);

            profileTab.Style["background-image"] = "url(/images/eHdrProfileTabTrain.gif)";
            eProfileTabMenu.Style["display"] = "none";
            eProfileTabGoal.Style["display"] = "none";
            eProfileTabTrain.Style["display"] = "block";
            eDailyMenusView.Style["display"] = "none";
        }

        protected void ButtonWeekNextClick(object sender, EventArgs e)
        {
            //_when = System.DateTime.Parse(currentDate.Value);
            _when = System.DateTime.Parse(literalDay.Text);
            _when = _when.AddDays(7);

            bdpDay.SelectedValue = _when;
            bdpDay2.SelectedValue = _when;

            //UpdateTrainingTab(_when); 
            UpdateTrainingTabPostback(_when);

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

            //UpdateTrainingTab(_when);
            UpdateTrainingTabPostback(_when);
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

            //UpdateTrainingTab(_when);
            UpdateTrainingTabPostback(_when);
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
            //UpdateTrainingTab(_when);
            UpdateTrainingTabPostback(_when);

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
            //UpdateTrainingTab(_when);
            UpdateTrainingTabPostback(_when);
            UpdateFoodTab(_when);

            profileTab.Style["background-image"] = "url(/images/eHdrProfileTabMenu.gif)";
            eProfileTabMenu.Style["display"] = "block";
            eProfileTabGoal.Style["display"] = "none";
            eProfileTabTrain.Style["display"] = "none";
            eDailyMenusView.Style["display"] = "block";
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
            var cvdc = new ClubVisionDataContext();

            var singleOrDefault = (from csts in cvdc.TrainingDiaries
                                   where csts.iCustomerId == (int)Session["MemberNo"]
                                   where csts.iWeekNumber == 0
                                   select csts.iWeekStart).FirstOrDefault();
            if (singleOrDefault != null)
            {
                _intStartDay = (int)singleOrDefault;
            }

            var currentDayOfWeek = (int)(when.DayOfWeek); //because c# day of week start from 0 - 6 
            if (currentDayOfWeek == 0) { currentDayOfWeek = 7; } //validation for sunday

            _weeknumber = TrainingDiaryScreen.GetWeekNumber(when);

            if (Session["MemberType"].Equals("VVT"))
            {
                _startDay = TrainingDiaryScreen.GetFirstDayOfWeek(_weeknumber);
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

                FirstLoadNotifLiteral.Text = "<br/><h3 style=\"color:#999999;width:500px; height:25px;display:inline-table;\">Your week with your trainer starts on <span style=\"color:#002147;\">" + _startDay.ToString("dddd") + "</span></h3>";

                if (Session["FromTrainer"].Equals("YES") && ((int)_startDay.DayOfWeek == _intStartDay))
                {
                    if (!Page.IsPostBack)//pushback one week earlier
                    {
                        _when = _startDay.AddDays(-7);

                        // _when = _when.AddDays(-7);

                        bdpDay.SelectedValue = _when;
                        bdpDay2.SelectedValue = _when;

                        //UpdateTrainingTab(_when);
                        UpdateTrainingTabPostback(_when);

                        profileTab.Style["background-image"] = "url(/images/eHdrProfileTabTrain.gif)";
                        eProfileTabMenu.Style["display"] = "none";
                        eProfileTabGoal.Style["display"] = "none";
                        eProfileTabTrain.Style["display"] = "block";
                        eDailyMenusView.Style["display"] = "none";
                    }
                    FirstLoadNotifLiteral.Text +=
                        "<button onclick=\"updateWeeklyReportTD();return false;\">Update Weekly Report</button>";

                }

            }

            currentWeekNumber.Value = _weeknumber.ToString(CultureInfo.InvariantCulture);
            literalDay.Text = _startDay.ToLongDateString();
            currentDate.Value = when.ToString("dd/MM/yyyy");
            currentStartDay.Value = _startDay.ToString("dd/MM/yyyy");

            TrainingDiaryScreen.UpdateSqlTrainingData((int)Session["MemberNo"], _weeknumber, (string)Session["MemberType"], _intStartDay, _startDay);
            GenerateTrainingDiaryToLiteral(_weeknumber);
            LitSummaryPanel.Text = TrainingDiaryScreen.UpdateTrainingSummary(_weeknumber, (int)Session["MemberNo"], (string)Session["MemberType"], _startDay, false);

            cvdc.Dispose();

            const string s = "<script type=\"text/javascript\">" +
                             "callMotivationCaptainOnTrainingDiary();</script>";

            Page.ClientScript.RegisterStartupScript(GetType(), "test", s);

        }

        protected void UpdateTrainingTabPostback(System.DateTime when)
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

            var currentDayOfWeek = (int)(when.DayOfWeek); //because c# day of week start from 0 - 6 
            if (currentDayOfWeek == 0) { currentDayOfWeek = 7; } //validation for sunday

            _weeknumber = TrainingDiaryScreen.GetWeekNumber(when);

            _startDay = Session["MemberType"].Equals("VVT") ? TrainingDiaryScreen.GetFirstDayOfWeek(_weeknumber) : when;

            currentWeekNumber.Value = _weeknumber.ToString(CultureInfo.InvariantCulture);
            literalDay.Text = _startDay.ToLongDateString();
            currentDate.Value = when.ToString("dd/MM/yyyy");
            currentStartDay.Value = _startDay.ToString("dd/MM/yyyy");

            TrainingDiaryScreen.UpdateSqlTrainingData((int)Session["MemberNo"], _weeknumber, (string)Session["MemberType"], _intStartDay, _startDay);
            TrainingDiaryScreen.UpdateSqlTrainingData((int)Session["MemberNo"], _weeknumber + 1, (string)Session["MemberType"], _intStartDay, _startDay.AddDays(7));

            GenerateTrainingDiaryToLiteral(_weeknumber);
            LitSummaryPanel.Text = TrainingDiaryScreen.UpdateTrainingSummary(_weeknumber, (int)Session["MemberNo"], (string)Session["MemberType"], _startDay, false);

            cvdc.Dispose();

            const string s = "<script type=\"text/javascript\">" +
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
                        {
                            thisWeekTD = (from twtd in cvdc.TrainingDiaries
                                          where twtd.When >= _startDay
                                          where twtd.When < _startDay.AddDays(7)
                                          where twtd.iCustomerId == (int)Session["MemberNo"]
                                          select twtd).OrderBy(x => x.When);
                            type = "<td>Type</td>";
                        }
                        break;
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
                        // daycv = RenderDay(trainingDiary.iWeekDay);
                        daycv = ((DateTime)trainingDiary.When).ToString("ddd dd/MM");
                    }

                    if (trainingDiary.cExercise.Equals("Rest Day") || trainingDiary.iExValue == 45)
                    {
                        litTrainingDiary.Text += "<tr>" +
                                             "<td>" + daycv + "</td>" +
                                             "<td colspan=\"4\">" + trainingDiary.cExercise + "</td>" +
                                             type +
                                             "<td><img id=\"tick-" + trainingDiary.iId + "\" src=\"" + RenderImageURl(trainingDiary.bCompleteState) + "\" onclick=\"saveChangesTrainingRowFromProfile(" + trainingDiary.iId + ",'tickdone');return false;\"/></td>" +
                                             "</tr>";
                    }
                    else
                    {
                        litTrainingDiary.Text += "<tr>" +
                                             "<td>" + daycv + "</td>" +
                                             "<td>" + trainingDiary.cExercise + "</td>" +
                                             "<td>" + RenderAMPM(trainingDiary.iAMPM) + "</td>" +
                                             "<td>" + RenderIntensity(trainingDiary.iIntensity) + "</td>" +
                                             "<td>" + RenderDurs(trainingDiary.iDuration) + "</td>" +
                                             type +
                                             "<td><img id=\"tick-" + trainingDiary.iId + "\" src=\"" + RenderImageURl(trainingDiary.bCompleteState) + "\" onclick=\"saveChangesTrainingRowFromProfile(" + trainingDiary.iId + ",'tickdone');return false;\"/></td>" +
                                             "</tr>";
                    }

                    day = trainingDiary.iWeekDay;
                }

                litTrainingDiary.Text += "</table>";
            }
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

        protected void ImageButton1Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect("/club-vision/my-exercise/training-diary/?when=" + _when);
        }
    }
}