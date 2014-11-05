using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace VisionPersonalTrainingProject.usercontrols.VVT
{
    public partial class WeightSessions : System.Web.UI.UserControl
    {
        private System.DateTime _when = System.DateTime.Today;
        private int _weeknumber = 0;
        private string _programSummary = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["tdid"] != null)
                {
                    updateWeightsSessTabById(Convert.ToInt32(Request.QueryString["tdid"]));
                }
                else
                {
                    updateWeightsSessTab(_when);
                }
            }
        }

        protected void updateWeightsSessTab(System.DateTime when)
        {
            var memberId = (int)Session["MemberNo"];

            _weeknumber = GetWeekNumber(when);
            
            var cvdc = new VisionPersonalTrainingProject.ClubVisionDataContext();

            //get day
            int currDay = (int)DateTime.Today.DayOfWeek;
            if (currDay == 0) currDay = 7;
                            
            var closestSet = (from css in cvdc.TrainingDiaries
                              where css.iCustomerId == (int) Session["MemberNo"]
                              where css.iWeekNumber == _weeknumber
                              where css.iWeekDay >= currDay
                              where css.bIsWPExAttached == true
                              select css).OrderBy(x => x.iWeekDay).FirstOrDefault();
            
            if(closestSet != null)
            {
                int day = closestSet.iWeekDay;
                DateTime dayex = GetFirstDayOfWeek(Convert.ToInt32((closestSet.iWeekNumber.ToString().Substring(3))),
                                                   Convert.ToInt32((closestSet.iWeekNumber.ToString().Substring(0, 4))));
                dayex = dayex.AddDays(day - 1);
                literalDay.Text = dayex.ToLongDateString();
                litWeightsSess.Text = UpdateSqlWeightsSessData(CheckTrainingDiaryWeightProgram(closestSet.iId), 1);
                litProgramSummary.Text = _programSummary;
            }
            else
            {
                weightSessDiv.Visible = false;
                wsNotAVailable.Visible = true;
            }

            cvdc.Dispose();

        }

        protected void updateWeightsSessTabById(int tdid)
        {
            var cvdc = new VisionPersonalTrainingProject.ClubVisionDataContext();

            var closestSet = (from css in cvdc.TrainingDiaries
                              where css.iId == tdid
                              select css).SingleOrDefault();

            if (closestSet != null)
            {
                int day = closestSet.iWeekDay;
                DateTime dayex = GetFirstDayOfWeek(Convert.ToInt32((closestSet.iWeekNumber.ToString().Substring(3))),
                                                   Convert.ToInt32((closestSet.iWeekNumber.ToString().Substring(0, 4))));
                dayex = dayex.AddDays(day - 1);
                literalDay.Text = dayex.ToLongDateString();
                litWeightsSess.Text = UpdateSqlWeightsSessData(CheckTrainingDiaryWeightProgram(closestSet.iId), 1);
                litProgramSummary.Text = _programSummary;
            }

            cvdc.Dispose();

        }

        public IQueryable<TrainingDiaryWeightProgram> CheckTrainingDiaryWeightProgram(int tdId)
        {
            ClubVisionDataContext cvdc = new ClubVisionDataContext();
            
            var checkTDWP = (from ctdwp in cvdc.TrainingDiaryWeightPrograms
                                where ctdwp.iTrainingDiaryRefId == tdId
                                select ctdwp);

            if (!checkTDWP.Any())
            { //if there is no any create new set of data
                var masterTDWP = (from mtdwp in cvdc.TrainingDiaryWeightPrograms
                                    where mtdwp.iMasterSetOf == (int)Session["MemberNo"]
                                    select mtdwp);
                List<TrainingDiaryWeightProgram> listTDWP = new List<TrainingDiaryWeightProgram>();
                foreach (TrainingDiaryWeightProgram trainingDiaryWeightProgram in masterTDWP)
                {
                    int count = 1;
                    int stopcount = trainingDiaryWeightProgram.iTypeWeightsProg == 1 ? 2 : 3;
                    do
                    {
                        TrainingDiaryWeightProgram tdwp = new TrainingDiaryWeightProgram();
                        tdwp.iTrainingDiaryRefId = tdId;
                        tdwp.iExValue = trainingDiaryWeightProgram.iExValue;
                        tdwp.cExercise = trainingDiaryWeightProgram.cExercise;
                        tdwp.iBodyPart = trainingDiaryWeightProgram.iBodyPart;
                        tdwp.iSet = count;
                        tdwp.iTypeWeightsProg = trainingDiaryWeightProgram.iTypeWeightsProg;

                        listTDWP.Add(tdwp);
                        count++;

                    } while (count <= stopcount);
                }
                cvdc.TrainingDiaryWeightPrograms.InsertAllOnSubmit(listTDWP);
                cvdc.SubmitChanges();
            }

            return checkTDWP;

        }

        public string UpdateSqlWeightsSessData(IQueryable<TrainingDiaryWeightProgram> checkTDWP, int stateOfDisplaying)
        {
            string sqldata = "";
            ClubVisionDataContext cvdc = new ClubVisionDataContext();

            string LegsExOps = "";
            string UpPushExOps = "";
            string UpPullExOps = "";
            string CoreOps = "";
            string StateInputText = "";
            string SummaryText = RenderProgramSummary((int) checkTDWP.FirstOrDefault().iTypeWeightsProg);
            string stateVideoIcon = "";
            
            if(stateOfDisplaying == 1)
            {
                //start create usercontrols
                /*****************************************SQL DATA SOURCE***********************************************/
                /*Create dropdown list for exercise*/
                var exesvalue = (from ex in cvdc.EnumTables
                                 where ex.ID == 9
                                 where ex.OptGroup == "Resistance"
                                 where ex.floatValue > 0
                                 select ex).OrderBy(x => x.Value);

                var LegsEx = exesvalue.Where(x => x.floatValue == 1);
                LegsExOps = Enumerable.Aggregate(LegsEx, "", (current, enumTable) => current + ("<option value=\"" + enumTable.intValue + "\">" + enumTable.Value + "</option>"));

                var UpPushEx = exesvalue.Where(x => x.floatValue == 2);
                UpPushExOps = Enumerable.Aggregate(UpPushEx, "", (current, enumTable) => current + ("<option value=\"" + enumTable.intValue + "\">" + enumTable.Value + "</option>"));

                var UpPullEx = exesvalue.Where(x => x.floatValue == 3);
                UpPullExOps = Enumerable.Aggregate(UpPullEx, "", (current, enumTable) => current + ("<option value=\"" + enumTable.intValue + "\">" + enumTable.Value + "</option>"));

                var CoreEx = exesvalue.Where(x => x.floatValue == 4);
                CoreOps = Enumerable.Aggregate(CoreEx, "", (current, enumTable) => current + ("<option value=\"" + enumTable.intValue + "\">" + enumTable.Value + "</option>"));
                /*****************************************SQL DATA SOURCE***********************************************/
                stateVideoIcon = "<td>Video</td>";
            }
            else
            {
                StateInputText = "disabled";
                sqldata += "<span class=\"summval\" style=\"color:#FFFFFF;display:none;\">" + SummaryText + "</span>";
            }

            //at this time the exercise should be available?
            //sqldata = "";
            int bodyArea = 0;
            int exerid = 0;
            int countex = 0;

            sqldata +=
                "<table id=\"tableWeightSession\"><tr style=\"font-weight:bold;\"><td>Area</td>" + stateVideoIcon + "<td>Exercise</td><td>Set</td><td>Reps</td><td>Weight</td></tr>";

            foreach (TrainingDiaryWeightProgram tdwp in checkTDWP)
            {
                //unlike training diary this would be one ddl ex at a time
                string chosenDll = "";
                
                if (stateOfDisplaying == 1)
                {
                    switch (tdwp.iBodyPart)
                    {
                        case 1:
                            {
                                chosenDll = "<select onchange=\"saveChangesWeightsSession(" + tdwp.iID + ", 'exs', $(this).val(), $(this).find('option:selected').text());return false;\">" + LegsExOps + "</select>";
                            } break;
                        case 2:
                            {
                                chosenDll = "<select onchange=\"saveChangesWeightsSession(" + tdwp.iID + ", 'exs', $(this).val(),$(this).find('option:selected').text());return false;\">" + UpPushExOps + "</select>";
                            } break;
                        case 3:
                            {
                                chosenDll = "<select onchange=\"saveChangesWeightsSession(" + tdwp.iID + ", 'exs', $(this).val(), $(this).find('option:selected').text());return false;\">" + UpPullExOps + "</select>";
                            } break;
                        case 4:
                            {
                                chosenDll = "<select onchange=\"saveChangesWeightsSession(" + tdwp.iID + ", 'exs', $(this).val(), $(this).find('option:selected').text());return false;\">" + CoreOps + "</select>";
                            } break;
                    }
                    chosenDll = chosenDll.Replace("value=\"" + tdwp.iExValue + "\"", "value=\"" + tdwp.iExValue + "\" selected='selected'");
                    stateVideoIcon =   "<td rowspan=\"rowspanvid\"><img src=\"/images/Video-icon-orange.png\" width=\"25\" style=\"padding-top:4px;\"/ title=\"Click here to see how-to video\" " +
                                        "onclick=\"" + RenderVideoExercise(tdwp.iExValue) + "();return false;\"></td>";
                }
                else
                {
                    stateVideoIcon = "";
                    chosenDll = tdwp.cExercise;
                    
                }
                
                int rowspan = tdwp.iTypeWeightsProg == 1 ? 2 : 3;

                if (bodyArea != tdwp.iBodyPart)
                {
                    sqldata += "<tr style=\"background-color:#ffffff;\"><td colspan='6' style=\"background-color: #ffffff;\"></td></tr>";
                }
                    
                sqldata += "<tr id=\""+ tdwp.iID + "\">";

                if(bodyArea != tdwp.iBodyPart) //render body area
                {
                    sqldata += "<td style=\"margin-bottom:10px;font-weight:bold;\" " +
                                            "rowspan=\"" + (rowspan * RenderExercisePerArea(tdwp.iBodyPart, (int) tdwp.iTypeWeightsProg)) + "\">" + RenderBodyArea(tdwp.iBodyPart) + "</td>";
                                                
                    bodyArea = tdwp.iBodyPart;
                }

                if(exerid != tdwp.iExValue || (rowspan == countex))
                {
                    sqldata +=  stateVideoIcon + "<td rowspan=\"" + rowspan + "\" style=\"text-align:left;\">" + chosenDll + "</td>";
                    exerid = tdwp.iExValue;
                    countex = 0;
                    sqldata = sqldata.Replace("rowspanvid", rowspan.ToString());
                }
                countex++;
 
                sqldata += "<td>"+ tdwp.iSet +"</td>"+
                                        "<td><input type=\"text\" " + StateInputText + " id=\"reps-" + tdwp.iID + "\" onblur=\"saveChangesWeightsSession(" + tdwp.iID + ", 'reps', $(this).val())\" value=\"" + tdwp.iReps + "\" /></td>" +
                                        "<td><input type=\"text\" " + StateInputText + " id=\"weights-" + tdwp.iID + "\" onblur=\"saveChangesWeightsSession(" + tdwp.iID + ", 'weight', $(this).val())\" value=\"" + tdwp.iWeight + "\" /></td></tr>"; 

            }
            sqldata += "</table>";
            _programSummary = SummaryText;
            return sqldata;
        }

        public int RenderExercisePerArea(int bodyArea, int iType)
        {
            switch (iType)
            {
                case 1:
                    return 1;
                case 2:
                    {
                        return bodyArea == 4 ? 1 : 2;
                    }
                case 3:
                    {
                        return bodyArea == 4 ? 2 : 3;
                    }
                default:
                    return 0;
            }
        }

        public string RenderVideoExercise(int ex)
        {
            switch (ex)
            {
                case 36:
                    return "lungesVid";
                case 62:
                    return "squatsVid";
                case 64:
                    return "stepUpsVid";
                case 44:
                    return "pushUpsVids";
                case 55:
                    return "shoulderPressesVid";
                case 89:
                    return "tricepDipsVid";
                case 93:
                    return "resistanceRowVid";
                case 43:
                    return "pullUpsVids";
                case 75:
                    return "uprightRowVid";
                case 87:
                    return "crunchesVid";
                case 88:
                    return "proneHoldsVid";
                default:
                    return "";
            }
        }

        public string RenderProgramSummary(int wprog)
        {
            switch (wprog)
            {
                case 1:
                    return "<h3>Weights Beginner</h3>" +
                            "<p>Program Summary - 2 sets of 10 reps at a speed of 2 second down per repetition. 60 seconds rest between each set.</p>";
                case 2:
                    return "<h3>Weights Intermediate</h3>" +
                            "<p>Program Summary - 3 sets of 12 reps at a speed of 2 second down per repetition. 60 seconds rest between each set.</p>";
                case 3:
                    return "<h3>Weights Advanced</h3>" +
                            "<p>Program Summary - 3 sets of 15 reps at a speed of 2 second down per repetition. 60 seconds rest between each set.</p>";
                default:
                    return "";
            }
        }

        public string RenderBodyArea(int bodyArea)
        {
            switch (bodyArea)
            {
                case 1 :
                    return "Legs";
                case 2 :
                    return "Upper Push";
                case 3 :
                    return "Upper Pull";
                case 4:
                    return "Core";
                default:
                    return "";
            }
        }

        public DateTime GetFirstDayOfWeek(int weeknumber, int year)
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

        public static int WeekNumber(int year, int mon, int day)
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

        public int GetWeekNumber(DateTime dtPassed)
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
    }
}