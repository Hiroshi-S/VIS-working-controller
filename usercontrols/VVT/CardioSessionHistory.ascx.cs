using System;
using System.Data;
using System.Linq;
using VisionPersonalTrainingProject.VOSWebService;
using VisionPersonalTrainingProject.usercontrols.clubvision;

namespace VisionPersonalTrainingProject.usercontrols.VVT
{
    public partial class CardioSessionHistory : System.Web.UI.UserControl
    {
        readonly WeightSessions _ws = new WeightSessions();
        private readonly System.DateTime _when = System.DateTime.Today;
        private int _weeknumber = 0;
        private DataTable _dtt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if ((string)Session["MemberType"] == "VVT")
                {
                    updateCardioSessTab(_when);
                }
                else
                {
                    updateCardioSessTabVPT();
                }

            }

        }

        protected void updateCardioSessTab(DateTime when)
        {
            try
            {
                var memberId = (int)Session["MemberNo"];

                _weeknumber = _ws.GetWeekNumber(when);

                int weeknumflag = Convert.ToInt32(_weeknumber + "80");

                var cvdc = new VisionPersonalTrainingProject.ClubVisionDataContext();

                //get day
                int currDay = (int)DateTime.Today.DayOfWeek;
                if (currDay == 0) currDay = 7;

                var alltdset = (from css in cvdc.TrainingDiaries
                                where css.iCustomerId == memberId
                                where css.intValue <= weeknumflag
                                where css.bIsCardioHasProg == true
                                where css.iWeekNumber != 0
                                select css).Select(x => new{ x.iId, x.iWeekNumber, x.iWeekDay});

                var alltdsetid = alltdset.Select(c => c.iId).ToList();

                var tdwClosestSet = (from tdwcss in cvdc.TrainingDiaryCardioPrograms
                                     where alltdsetid.Contains(tdwcss.iTrainingDiaryRefId)
                                     select tdwcss.iTrainingDiaryRefId).ToList();

                var closestSet = alltdset.Where(c => tdwClosestSet.Contains(c.iId))
                                .Select(x => new {p = Convert.ToInt32(x.iWeekNumber.ToString() + x.iWeekDay.ToString())})
                                .Distinct().OrderByDescending(x => x.p).Take(4);

                if (closestSet.Any())
                {
                    litCardioHistory.Text = "<div id=\"WeightsSessHis_frame\"><div id=\"WeightsSessHis_scroller\">";

                    int count = 0;
                    int left = 0;

                    foreach (var trainingDiaryset in closestSet)
                    {
                        string strp = trainingDiaryset.p.ToString();
                        int day = Convert.ToInt32(strp.Substring(6)); 
                        DateTime dayex = _ws.GetFirstDayOfWeek(Convert.ToInt32(strp.Substring(4, 2)),
                                                                Convert.ToInt32(strp.Substring(0, 4)));
                        dayex = dayex.AddDays(day - 1);

                        if (count == 0)
                        {
                            literalDay.Text = dayex.ToLongDateString();
                            //litProgramSummary.Text = "<script>$(\"#programSummary\").html($(\"#WeightsSessHis_scroller #WeightsSessHis:nth-child(1) .summval\").html());</script>";
                        }

                        litCardioHistory.Text +=
                            "<div id=\"WeightsSessHis\" style=\"position:absolute;top:0px;left:" + left + "px;\" data-intvalue=\"" + strp + "0\">" +
                            DisplayCardioSessHistory(Convert.ToInt32(strp.Substring(0, 6)), Convert.ToInt32(strp.Substring(6))) +
                            //"You gout it" +
                            "<span class=\"dateval\" style=\"color:#FFFFFF;\">" + dayex.ToLongDateString() + "</span></div>";

                        count++;
                        left -= 600;
                    }
                    left = -(left + 600);
                    litCardioHistory.Text += "</div></div><script type=\"text/javascript\">var scroller_width = " + left + ";var cursor = 1;</script>";

                }
                else
                {
                    weightSessDiv.Visible = false;
                    wsNotAVailable.Visible = true;
                }

                
            }catch(Exception ex)
            {
                Response.Write("");
            }
        }

        protected void updateCardioSessTabVPT()
        {
            try
            {
                //litCardioHistory.Text = "this is internal internal clients";
                var service = Session["WebService"] as Service;

                if (service == null) return;
                _dtt = service.GetMemberPTSessionHistory((int)Session["MemberNo"], "c", "");
                Session["VPTWeightHisDatatable"] = _dtt;
                Session["VPTWeightHisDatatable_dates"] = _dtt.DefaultView.ToTable(true, new String[] { "dDateExercise" });

                DateTime mostRecentDate = Convert.ToDateTime((_dtt.Compute("MAX(dDateExercise)", string.Empty)));

                literalDay.Text = mostRecentDate.ToLongDateString();

                litCardioHistory.Text = "<div id=\"WeightsSessHis_frame\"><div id=\"WeightsSessHis_scroller\">";
                litCardioHistory.Text +=
                            "<div id=\"WeightsSessHis\" style=\"position:absolute;top:0px;left:0px;\" data-date=\"" + mostRecentDate + "\">" +
                            DisplayCardioSessHistoryVPT(mostRecentDate) +
                            "<span class=\"dateval\" style=\"color:#FFFFFF;\">" + mostRecentDate.ToLongDateString() + "</span></div>";
                litCardioHistory.Text += "</div></div><script  type=\"text/javascript\">var scroller_width = 0;var cursor = 1;</script>";

            }
            catch (Exception ex)
            {
                //Response.Write("");
                wsNotAVailable.Visible = true;
                weightSessDiv.Visible = false;
            }
        }

        public string DisplayCardioSessHistory(int weeknum, int weekday)
        {
            string sqldata = "";
            clubvision.ProfileTab ptn = new clubvision.ProfileTab();

            using (ClubVisionDataContext cvdc = new ClubVisionDataContext())
            {
                var cardiosess = (from css in cvdc.TrainingDiaries
                                  where css.iCustomerId == (int) Session["MemberNo"]
                                  where css.iWeekNumber == weeknum
                                  where css.iWeekDay == weekday
                                  select css)
                                  .Select(x => new{ x.cExercise, x.iIntensity, x.iDuration, x.TrainingDiaryCardioPrograms});

                sqldata += "<span class=\"summval\" style=\"color:#FFFFFF;display:none;\">Cardio Session History</span>";
                int count = 0;

                foreach (var trainingDiary in cardiosess)
                {
                    if(count == 0)
                    {
                        sqldata += "<table id=\"tableWeightSession\"><tr style=\"font-weight:bold;\"><td>Exercise</td><td>Intensity</td><td>Duration</td><td>Pace/Distance</td></tr>";
                    }
                    sqldata += "<tr><td>" + trainingDiary.cExercise + "</td>" +
                                   "<td>" + ptn.RenderIntensity(trainingDiary.iIntensity) + "</td>" +
                                   "<td>" + ptn.RenderDurs(trainingDiary.iDuration) + "</td>" +
                                   "<td>" + (trainingDiary.TrainingDiaryCardioPrograms.FirstOrDefault() != null ? trainingDiary.TrainingDiaryCardioPrograms.FirstOrDefault().cPaceOrDistance : "") + "</td></tr>";

                    count++;
                }

                if(count > 0)
                {
                    sqldata += "</table>";
                }
            }
            return sqldata;
        }

        public string DisplayCardioSessHistoryVPT(DateTime date)
        {
            try
            {
                var newdtt = Session["VPTWeightHisDatatable"] as DataTable;

                if (newdtt != null)
                    newdtt = newdtt.Select("dDateExercise = '" + date + "'").CopyToDataTable();
                string sqldata = "";
                sqldata += "<span class=\"summval\" style=\"color:#FFFFFF;display:none;\">" + newdtt.Rows[0][3] + "</span>";

                sqldata +=
                   "<table id=\"tableWeightSession\"><tr style=\"font-weight:bold;\"><td>Exercise</td><td>Intensity</td><td>Duration</td><td>Pace/Distance</td></tr>";

                if (newdtt != null)
                {
                    string exercise = "";//newdtt.Rows[0][2].ToString();
                    int count = 0;
                    bool anyMethod = false;
                    foreach (DataRow drow in newdtt.Rows)
                    {
                        string columnExercise = "";
                        string columnMethod = "";
                        string columnWeight = "";
                        if (!exercise.Equals(drow["cExercise"]))
                        {
                            sqldata = sqldata.Replace("toberowspan", count + "");
                            columnExercise = "<td rowspan=\"toberowspan\">" + drow["cExercise"] + "</td>";
                            exercise = drow["cExercise"] + "";
                            count = 0;
                        }
                        if (!drow["cMethodInfo"].Equals(""))
                        {
                            columnMethod = drow["cMethodInfo"].ToString();
                            anyMethod = true;
                        }
                        columnWeight = drow["iWeight"].ToString();
                        columnWeight = columnWeight.Equals("0.00") ? "-" : columnWeight.Replace(".00", "");

                        sqldata += "<tr>" + columnExercise + "<td>" + drow["iSet"] + "</td><td>" + drow["iReps"] + "</td><td>" + columnWeight + "</td></tr>";
                        count++;
                    }
                    sqldata = sqldata.Replace("toberowspan", count + "");
                    //sqldata = sqldata.Replace("displaymethod", anyMethod ? "" : "style=\"display:none;\"");
                }

                sqldata += "</table>";
                return sqldata;
            }
            catch (Exception exception)
            {
                return exception.ToString();
            }
        }
    }
}