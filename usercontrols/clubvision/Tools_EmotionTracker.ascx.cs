using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace VisionPersonalTrainingProject.usercontrols.clubvision
{
    public partial class Tools_EmotionTracker : System.Web.UI.UserControl
    {
        private System.DateTime _when = System.DateTime.Today;
        private System.DateTime _thisLastSunday = System.DateTime.Today;

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


                int offset = _when.DayOfWeek - DayOfWeek.Monday;

                if ((int)_when.DayOfWeek == 0)
                {
                    offset += 7;
                }

                System.DateTime startDate = _when.AddDays(-offset);
                System.DateTime endDate = startDate.AddDays(6);
                _thisLastSunday = endDate;

                literalWeek.Text = startDate.ToShortDateString() + " - " + endDate.ToShortDateString();

                UpdateEmotionsTable(startDate);
            }
        }

        protected void ButtonWeekNextClick(object sender, EventArgs e)
        {
            if (Request.QueryString["when"] != null)
            {
                _when = System.DateTime.Parse(Request.QueryString["when"]);
            }

            _when = _when.AddDays(7);
            if(_when > _thisLastSunday)
            {
                Response.Redirect("/club-vision/education/tools/emotions-tracker/?msg=yes");
            }
            else
            {
                Response.Redirect("/club-vision/education/tools/emotions-tracker/?when=" + _when.ToString("dd/MM/yyyy"));
            }
        }

        protected void BdpDaySelectionChanged(object sender, EventArgs e)
        {
            if (bdpDay.SelectedDate > _thisLastSunday)
            {
                Response.Redirect("/club-vision/education/tools/emotions-tracker/?msg=yes");
            }
            else
            {
                Response.Redirect("/club-vision/education/tools/emotions-tracker/?when=" + bdpDay.SelectedDate.ToString("dd/MM/yyyy"));
            }
            
        }

        protected void ButtonWeekPrevClick(object sender, EventArgs e)
        {
            if (Request.QueryString["when"] != null)
            {
                _when = System.DateTime.Parse(Request.QueryString["when"]);
            }

            _when = _when.AddDays(-7);
            Response.Redirect("/club-vision/education/tools/emotions-tracker/?when=" + _when.ToString("dd/MM/yyyy"));
        }

        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

        protected void UpdateEmotionsTable(DateTime startDate)
        {
            var memberId = (int)Session["MemberNo"];
            
            var cvdc = new VisionPersonalTrainingProject.ClubVisionDataContext();
            
            currentDate.Value = startDate.ToString("dd/MM/yyyy");

            var thisWeekEmotions = (from weekEmo in cvdc.CustomerMoods
                                    where weekEmo.CustomerId == (int) Session["MemberNo"]
                                    where weekEmo.When >= startDate
                                    where weekEmo.When <= startDate.AddDays(6)
                                    select weekEmo);
            
            /* Writing the header */
            litEmotionTrackerSummary.Text = "<table id='emotionsTable'><tr><td class='firstEmoTable'>Time/Date</td>";

            foreach (DateTime day in EachDay(startDate, startDate.AddDays(6)))
            {
                litEmotionTrackerSummary.Text += "<td>" + "<a id=\"diary-" + day.ToString("dd-MM-yyyy") + "\" class=\"diaryday-drag ui-draggable\" href=\"/club-vision/my-eating/food-diary/?when=" + day.ToString("dd-MM-yyyy") + "\"" +
                                                 "title=\"Go to Food Diary on " + day.ToString("ddd dd/MM") + "\">" + day.ToString("ddd dd/MM") + "</a>" + "</td>";
            }

            litEmotionTrackerSummary.Text += "</tr>";

            /* Writing the body*/

            var mealtimeName = (from mtname in cvdc.MealTimes
                                select mtname).OrderBy(x => x.Id);

            DateTime currDate = startDate;
            foreach (var mealTime in mealtimeName)
            {
                litEmotionTrackerSummary.Text += "<tr><td class='mealNameEmoTable'>" + mealTime.Name + "</td>";
                MealTime time = mealTime;
                var currentMealEmoWeek = thisWeekEmotions.Where(x => x.MealTimeId == time.Id);
                foreach (DateTime day in EachDay(startDate, startDate.AddDays(6)))
                {
                    var particularEmo = currentMealEmoWeek.SingleOrDefault(x => x.When == day);

                    string onclick = "onclick=\"foodDiaryLoadMoodPallete('" + day.ToString("dd/MM/yyy") + "', '" + mealTime.Id + "', '" + day.ToString("ddd") + "');return false;\"";

                    if(day > DateTime.Today)
                    {
                        onclick = "onclick=\"emotionsFutureAlert(); return false;\"";
                    }

                    if(particularEmo != null)
                    {
                        litEmotionTrackerSummary.Text += "<td><img id=\"mood-" + day.ToString("ddd") + "-" + mealTime.Id + "\" src=\"/images/icons/moods_small/" + particularEmo.Mood.fileName + "\" height=\"20\" " + onclick + " title=\"Your mood -- " + particularEmo.Mood.MoodString + "\"></td>";
                    }
                    else
                    {
                        litEmotionTrackerSummary.Text += "<td><img id=\"mood-" + day.ToString("ddd") + "-" + mealTime.Id + "\" src=\"/images/face-question.gif\" height=\"20\" " + onclick + "></td>";
                    }
                    
                }
                litEmotionTrackerSummary.Text += "</tr>";
             }

            litEmotionTrackerSummary.Text += "</table>";

            cvdc.Dispose();

            string s = "<script type=\"text/javascript\">" +
                        "callMotivationCaptainOnTrainingDiary();</script>";

            Page.ClientScript.RegisterStartupScript(GetType(), "test", s);
        }


    }
}