using System;
using System.Collections.Generic;
using System.Linq;
using VisionPersonalTrainingProject.usercontrols.clubvision;

namespace VisionPersonalTrainingProject.usercontrols.VVT
{
    public partial class CardioSession : System.Web.UI.UserControl
    {
        private System.DateTime _when = System.DateTime.Today;
        private int _weeknumber = 0;
        private string _programSummary = "";
        private WeightSessions _ws = new WeightSessions();
        private clubvision.ProfileTab _pt = new clubvision.ProfileTab();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                _programSummary = "<h3>Cardio</h3>"
                                  + "<p></p>";
                updateCardioSessTab(Request.QueryString["when"] != null
                                       ? Convert.ToDateTime(Request.QueryString["when"])
                                       : _when);
            }
        }

        protected void updateCardioSessTab(System.DateTime when)
        {
            _weeknumber = _ws.GetWeekNumber(when);

            var cvdc = new VisionPersonalTrainingProject.ClubVisionDataContext();

            List<TrainingDiary> tdList = new List<TrainingDiary>();

            //get day
            int currDay = (int)when.DayOfWeek;
            if (currDay == 0) currDay = 7;

            var closestSet = (from css in cvdc.TrainingDiaries
                              where css.iCustomerId == (int) Session["MemberNo"]
                              where css.iWeekNumber == _weeknumber
                              where css.iWeekDay == currDay
                              where css.bIsCardioHasProg == true
                              select css).Take(5);

            var firstOrDefault = closestSet.FirstOrDefault();
            if (firstOrDefault != null)
            {
                tdList.AddRange(closestSet);

                int day = firstOrDefault.iWeekDay;
                DateTime dayex = _ws.GetFirstDayOfWeek(Convert.ToInt32((firstOrDefault.iWeekNumber.ToString().Substring(3))),
                                                   Convert.ToInt32((firstOrDefault.iWeekNumber.ToString().Substring(0, 4))));
                dayex = dayex.AddDays(day - 1);
                literalDay.Text = dayex.ToLongDateString();
                litWeightsSess.Text = UpdateSqlCardioSessData(tdList, 1);
                litProgramSummary.Text = _programSummary;
            }
            else
            {
               // Response.Write(currDay + " " + _weeknumber);
                weightSessDiv.Visible = false;
                wsNotAVailable.Visible = true;
            }

            cvdc.Dispose();
            
        }

        protected List<TrainingDiaryCardioProgram> CheckTrainingDiaryCardioProgram(List<TrainingDiary> trainingDiaries)
        {
            List<TrainingDiaryCardioProgram> tdcplist = new List<TrainingDiaryCardioProgram>();
            ClubVisionDataContext cvdc = new ClubVisionDataContext();

            foreach (TrainingDiary trainingDiary in trainingDiaries)
            {
                var checktdcp = (from tdcp in cvdc.TrainingDiaryCardioPrograms
                                 where tdcp.iTrainingDiaryRefId == trainingDiary.iId
                                 select tdcp).SingleOrDefault();
                if(checktdcp == null)
                {
                    TrainingDiaryCardioProgram ttt = new TrainingDiaryCardioProgram();
                    ttt.iTrainingDiaryRefId = trainingDiary.iId;

                    cvdc.TrainingDiaryCardioPrograms.InsertOnSubmit(ttt);
                    cvdc.SubmitChanges();
                }
                tdcplist.Add(checktdcp); 
            }

            return tdcplist;
        } 

        protected string UpdateSqlCardioSessData(List<TrainingDiary> tdcardios, int stateOfDisplaying)
        {
            string sqldata = "";
            ClubVisionDataContext cvdc = new ClubVisionDataContext();

            int count = 0;
            foreach (TrainingDiary tdcardio in tdcardios)
            {
                if(count == 0)
                {
                    sqldata += "<table id=\"tableWeightSession\"><tr style=\"font-weight:bold;\"><td>Exercise</td><td>Intensity</td><td>Duration</td><td>Pace/Distance</td></tr>";
                }
                var trainingDiaryCardioProgram = tdcardio.TrainingDiaryCardioPrograms.SingleOrDefault();
                string value = "";
                if (trainingDiaryCardioProgram != null)
                {
                    value = trainingDiaryCardioProgram.cPaceOrDistance;

                }
                sqldata += "<tr><td>" + tdcardio.cExercise + "</td><td>" +
                            _pt.RenderIntensity(tdcardio.iIntensity)
                            + "</td><td>" + _pt.RenderDurs(tdcardio.iDuration) + "</td><td>"
                            + "<input style=\"width:200px;\" type=\"text\" Id=\"tdcardio-" + tdcardio.iId + "\" onblur=\"saveChangesCardioSess(" + tdcardio.iId + ", $(this).val());return false;\"" 
                            + "value=\"" + value + "\" />"
                            + "</td></tr>";
                
                count++;
            }

            if(count > 0)
            {
                sqldata += "</table>";
            }

            return sqldata;
        }
    }
}