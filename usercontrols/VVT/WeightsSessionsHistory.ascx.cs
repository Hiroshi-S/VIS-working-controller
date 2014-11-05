using System;
using System.Data;
using System.Linq;
using VisionPersonalTrainingProject.VOSWebService;

namespace VisionPersonalTrainingProject.usercontrols.VVT
{
    public partial class WeightsSessionsHistory : System.Web.UI.UserControl
    {
        readonly WeightSessions _ws = new WeightSessions();
        private readonly System.DateTime _when = System.DateTime.Today;
        private int _weeknumber = 0;
        private string _programSummary = "";
        private DataTable _dtt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if((string)Session["MemberType"] == "VVT")
                {
                    updateWeightsSessTab(_when);
                }
                else
                {
                    updateWeightsSessTabVPT();
                }
                
            }
        }

        protected void updateWeightsSessTabVPT()
        {
            try
            {
                //litWeightsHistory.Text = "this is internal internal clients";
                var service = Session["WebService"] as Service;

                if (service == null) return;
                _dtt = service.GetMemberPTSessionHistory((int)Session["MemberNo"], "w", "");
                Session["VPTWeightHisDatatable"] = _dtt;
                Session["VPTWeightHisDatatable_dates"] = _dtt.DefaultView.ToTable(true, new String[] { "dDateExercise" });

                DateTime mostRecentDate = Convert.ToDateTime((_dtt.Compute("MAX(dDateExercise)", string.Empty)));
                
                literalDay.Text = mostRecentDate.ToLongDateString();

                litWeightsHistory.Text = "<div id=\"WeightsSessHis_frame\"><div id=\"WeightsSessHis_scroller\">";
                litWeightsHistory.Text +=
                            "<div id=\"WeightsSessHis\" style=\"position:absolute;top:0px;left:0px;\" data-date=\""+mostRecentDate+"\">" +
                            DisplayWeightsSessHistoryVPT(mostRecentDate) +
                            "<span class=\"dateval\" style=\"color:#FFFFFF;\">" + mostRecentDate.ToLongDateString() + "</span></div>";
                litWeightsHistory.Text += "</div></div><script  type=\"text/javascript\">var scroller_width = 0;var cursor = 1;</script>";
                
            }
            catch(Exception ex)
            {
                wsNotAVailable.Visible = true;
                weightSessDiv.Visible = false;
            }
        }

        protected void updateWeightsSessTab(System.DateTime when)
        {
            try
            {
                var memberId = (int)Session["MemberNo"];

                _weeknumber = _ws.GetWeekNumber(when);

                int weeknumflag = Convert.ToInt32(_weeknumber + "80");

                var cvdc = new VisionPersonalTrainingProject.ClubVisionDataContext();

                //get day
                //NOTE : needs to go back and change this logic
                int currDay = (int)DateTime.Today.DayOfWeek;
                if (currDay == 0) currDay = 7;

                var tdwClosestSet = (from tdwcss in cvdc.TrainingDiaryWeightPrograms
                                     where tdwcss.iWeight != null || tdwcss.iReps != null
                                     select tdwcss.iTrainingDiaryRefId).Distinct().ToList();

                var closestSet = (from css in cvdc.TrainingDiaries
                                  where css.iCustomerId == memberId
                                  where css.intValue <= weeknumflag
                                  where css.bIsWPExAttached == true
                                  where tdwClosestSet.Contains(css.iId)
                                  where css.iWeekNumber != 0 //not the master set
                                  select css)
                                  .Select(x => new {x.iId, x.iWeekNumber, x.iWeekDay, x.intValue})
                                  .OrderByDescending(x => x.intValue)
                                  .Take(4);

                if (closestSet.Any())
                {
                    litWeightsHistory.Text = "<div id=\"WeightsSessHis_frame\"><div id=\"WeightsSessHis_scroller\">";

                    int count = 0;
                    int left = 0;
                    foreach (var trainingDiary in closestSet)
                    {   
                        int day = trainingDiary.iWeekDay;
                            DateTime dayex = _ws.GetFirstDayOfWeek(Convert.ToInt32((trainingDiary.iWeekNumber.ToString().Substring(3))),
                                                           Convert.ToInt32((trainingDiary.iWeekNumber.ToString().Substring(0, 4))));
                            dayex = dayex.AddDays(day - 1);

                        if (count == 0)
                        {
                            literalDay.Text = dayex.ToLongDateString();
                            //litProgramSummary.Text = "<script>$(\"#programSummary\").html($(\"#WeightsSessHis_scroller #WeightsSessHis:nth-child(1) .summval\").html());</script>";
                        }

                        litWeightsHistory.Text +=
                            "<div id=\"WeightsSessHis\" style=\"position:absolute;top:0px;left:" + left + "px;\" data-intvalue=\""+trainingDiary.intValue+"\">" +
                            DisplayWeightsSessHistory(trainingDiary.iId) +
                            "<span class=\"dateval\" style=\"color:#FFFFFF;\">" + dayex.ToLongDateString() + "</span></div>";

                        count++;
                        left -= 600;
                    }
                    left = -(left + 600);
                    litWeightsHistory.Text += "</div></div><script  type=\"text/javascript\">var scroller_width = " + left + ";var cursor = 1;</script>";
                     
                }
                else
                {
                    weightSessDiv.Visible = false;
                    wsNotAVailable.Visible = true;
                }


                cvdc.Dispose();
                
            }
            catch(Exception ex)
            {
                Response.Write("caught error");
            }
            
        }

        public string DisplayWeightsSessHistory(int tdid)
        {
            string abc = "";

            try
            {
                abc += _ws.UpdateSqlWeightsSessData(_ws.CheckTrainingDiaryWeightProgram(tdid), 2);
            }
            catch (Exception exception)
            {
                abc += exception.ToString();
            }
            return abc;
        }

        public string DisplayWeightsSessHistoryVPT(DateTime date)
        {
            try
            {
                var newdtt = Session["VPTWeightHisDatatable"] as DataTable;
               
                if (newdtt != null)
                    newdtt = newdtt.Select("dDateExercise = '" + date + "'").CopyToDataTable();
                string sqldata = "";
                sqldata += "<span class=\"summval\" style=\"color:#FFFFFF;display:none;\">" + newdtt.Rows[0][3] + "</span>";

                sqldata +=
                   "<table id=\"tableWeightSession\"><tr style=\"font-weight:bold;\"><td>Exercise</td><td>Set</td><td>Reps</td><td>Weight (kg)</td><td displaymethod>Method</td></tr>";

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

                        sqldata += "<tr>" + columnExercise + "<td>" + drow["iSet"] + "</td><td>" + drow["iReps"] + "</td><td>" + columnWeight + "</td><td displaymethod>" + columnMethod + "</td></tr>";
                        count++;
                    }
                    sqldata = sqldata.Replace("toberowspan", count + "");
                    sqldata = sqldata.Replace("displaymethod", anyMethod ? "" : "style=\"display:none;\"");
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