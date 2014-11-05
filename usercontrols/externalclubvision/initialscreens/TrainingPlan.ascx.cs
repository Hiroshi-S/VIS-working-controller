using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using OptionDropDownList;
using VisionPersonalTrainingProject.usercontrols.clubvision;

namespace VisionPersonalTrainingProject.usercontrols.externalclubvision.initialscreens
{
    public partial class TrainingPlan : System.Web.UI.UserControl
    {
        private static List<RowadDropDownList> rowadDropDownLists;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ClubVisionDataContext cvdc = new ClubVisionDataContext();

                var exerciseListRaw = (from el in cvdc.EnumTables
                                       where el.ID == 9
                                       where el.OptGroup != null
                                       where el.OptGroup != "Resistance"
                                       
                                       select el).OrderBy(x => x.Value);

                var weightExercise = (from el in cvdc.EnumTables
                                      where el.ID == 9
                                      where el.OptGroup == "Resistance"
                                      select el).OrderBy(x => x.Value);
                var legEL = weightExercise.Where(x => x.floatValue == 1);
                var upperPushEL = weightExercise.Where(x => x.floatValue == 2);
                var upperPullEL = weightExercise.Where(x => x.floatValue == 3);
                var coreEL = weightExercise.Where(x => x.floatValue == 4);

                var exerciseList = LINQToDataTable(exerciseListRaw);

                var whenList = (from wl in cvdc.EnumTables
                                where wl.ID == 5
                                select wl).OrderBy(x => x.intValue);

                var durList = (from dl in cvdc.EnumTables
                               where dl.ID == 7
                               select dl).OrderBy(x => x.intValue);

                var intensityList = (from il in cvdc.EnumTables
                                     where il.ID == 12
                                     select il).OrderBy(x => x.intValue);
                /*
                var daysList = (from il in cvdc.EnumTables
                                where il.ID == 10
                                where il.intValue != 0
                                select il).OrderBy(x => x.intValue);

                var daysDDList = new List<ListBox>() { ListBox5 };
                foreach (ListBox listBox in daysDDList)
                {
                    listBox.DataTextField = "Value";
                    listBox.DataValueField = "intValue";
                    listBox.DataSource = daysList;
                    listBox.DataBind();
                }
                */
                var legDDList = new List<ListBox>() { exListBoxBeginner1, ListBox1 };
                foreach (ListBox listBox in legDDList)
                {
                    listBox.DataTextField = "Value";
                    listBox.DataValueField = "intValue";
                    listBox.DataSource = legEL;
                    listBox.DataBind();
                }

                var upushList = new List<ListBox>() { exListBoxBeginner2, ListBox2 };
                foreach (ListBox listBox in upushList)
                {
                    listBox.DataTextField = "Value";
                    listBox.DataValueField = "intValue";
                    listBox.DataSource = upperPushEL;
                    listBox.DataBind();
                }

                var upullList = new List<ListBox>() { ListBox3, exListBoxBeginner3 };
                foreach (ListBox listBox in upullList)
                {
                    listBox.DataTextField = "Value";
                    listBox.DataValueField = "intValue";
                    listBox.DataSource = upperPullEL;
                    listBox.DataBind();
                }

                var coreList = new List<ListBox>() { ListBox4, exListBoxBeginner4 };
                foreach (ListBox listBox in coreList)
                {
                    listBox.DataTextField = "Value";
                    listBox.DataValueField = "intValue";
                    listBox.DataSource = coreEL;
                    listBox.DataBind();
                }

                var eList = new List<OptionGroupSelect>() {DropDownList6, DropDownList31, DropDownList36, DropDownList11, DropDownList41,
                                                        DropDownList16, DropDownList56, DropDownList1, DropDownList66, 
                                                        DropDownList21, DropDownList76, DropDownList26, DropDownList86,
                                                        DropDownList96};

                foreach (var dropDownList in eList)
                {

                    dropDownList.DataTextField = "Value";
                    dropDownList.DataValueField = "intValue";
                    dropDownList.OptionGroupField = "OptGroup";
                    dropDownList.DataSource = exerciseList; 
                    dropDownList.DataBind();
                    dropDownList.CssClass = "tprow1";
                }

                var wList = new List<DropDownList>()
                                {   DropDownList7, DropDownList32, DropDownList37, DropDownList12, DropDownList42,
                                    DropDownList17, DropDownList57, DropDownList2, DropDownList67, 
                                    DropDownList22, DropDownList77, DropDownList27, DropDownList87,
                                    DropDownList97};

                foreach (var dropDownList in wList)
                {
                    dropDownList.DataSource = whenList;
                    dropDownList.DataTextField = "Value";
                    dropDownList.DataValueField = "intValue";
                    dropDownList.DataBind();
                }

                var dList = new List<DropDownList>()
                                {   DropDownList9, DropDownList34, DropDownList39, DropDownList14, DropDownList44,
                                    DropDownList19, DropDownList59, DropDownList4, DropDownList69, 
                                    DropDownList24, DropDownList79, DropDownList29, DropDownList89,
                                    DropDownList99};

                foreach (var dropDownList in dList)
                {
                    dropDownList.DataSource = durList;
                    dropDownList.DataTextField = "Value";
                    dropDownList.DataValueField = "intValue";
                    dropDownList.DataBind();
                }

                var iList = new List<DropDownList>()
                                {   DropDownList10, DropDownList35, DropDownList40, DropDownList15, DropDownList45,
                                    DropDownList20, DropDownList60, DropDownList5, DropDownList70, 
                                    DropDownList25, DropDownList80, DropDownList30, DropDownList90,
                                    DropDownList100};

                foreach (var dropDownList in iList)
                {
                    dropDownList.DataSource = intensityList;
                    dropDownList.DataTextField = "Value";
                    dropDownList.DataValueField = "intValue";
                    dropDownList.DataBind();
                }

                for (int i = 1; i < 100; i += 5)
                {
                    DropDownList ddl = this.FindControl("DropDownList" + i.ToString()) as DropDownList;
                    if (ddl != null) ddl.Attributes["OnChange"] = "RestDayFunction(this);";
                }

                CheckExistingPlan();
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

        protected void TrainerButtonCalculate_Click(object sender, EventArgs e)
        {  
            try
            {   
                ClassSorting();
                List<TrainingDiary> tdList = new List<TrainingDiary>();

                foreach (RowadDropDownList rwl in rowadDropDownLists)
                {
                    CompileTrainingList(rwl.IntDay, Convert.ToInt32(rwl.When.SelectedValue), rwl.Exercise.SelectedItem.Text, Convert.ToInt32(rwl.Duration.SelectedValue),
                                            Convert.ToInt32(rwl.Intensity.SelectedValue), rwl.IntValue, Convert.ToInt32(rwl.Exercise.SelectedValue), tdList);
                }

                ClubVisionDataContext cvdc = new ClubVisionDataContext();
                
                var deleteOldTD = (from tdtoDelete in cvdc.TrainingDiaries
                                   where tdtoDelete.bFromVOS == true
                                   where tdtoDelete.iCustomerId == Convert.ToInt32(Session["MemberNo"])
                                   select tdtoDelete);
                //delete all exercise that has been previously deleted
                var oldPlanExs = (from delep in cvdc.TrainingDiaryPlanner_Externals
                                  where !(cvdc.TrainingDiaries.Where(
                                      abcex => abcex.iCustomerId == Convert.ToInt32(Session["MemberNo"])).Select(
                                          abcex => abcex.iId)).Contains(delep.iTrainingDiaryRefId)
                                  select delep);

                cvdc.TrainingDiaryPlanner_Externals.DeleteAllOnSubmit(oldPlanExs);

                cvdc.SubmitChanges();
                cvdc.TrainingDiaries.DeleteAllOnSubmit(deleteOldTD);
                cvdc.TrainingDiaries.InsertAllOnSubmit(tdList);

                cvdc.SubmitChanges();
                
                var exPlanExs = (from tds in cvdc.TrainingDiaries
                                 where tds.iWeekNumber == 0
                                 where tds.iCustomerId == Convert.ToInt32(Session["MemberNo"])
                                 where tds.bFromVOS == true
                                 where tds.iExValue >= 90
                                 select tds);

                foreach (TrainingDiary trainingDiary in exPlanExs)
                {
                    List<TrainingDiaryPlanner_External> tdexList = new List<TrainingDiaryPlanner_External>();

                    switch (trainingDiary.iExValue)
                    {
                        case 90:
                            foreach (Control c in fatlossbeginner.Controls)
                            {
                                if (c.GetType() == typeof(ListBox))
                                {
                                    TrainingDiaryPlanner_External tep = new TrainingDiaryPlanner_External();
                                    tep.iTrainingDiaryRefId = trainingDiary.iId;
                                    tep.iTargetReps = 15;
                                    tep.iExerciseId = Convert.ToInt32(((ListBox)c).SelectedValue);
                                    tep.cExercise = ((ListBox)c).SelectedItem.Text;
                                    tep.cTargetSets = "1-2";
                                    tdexList.Add(tep);
                                }
                            }
                            break;
                        case 91:
                            foreach (Control c in fatlossintermediate.Controls)
                            {
                                if (c.GetType() == typeof(ListBox))
                                {
                                    foreach (ListItem li in ((ListBox)c).Items)
                                    {
                                        if (li.Selected)
                                        {
                                            TrainingDiaryPlanner_External tep = new TrainingDiaryPlanner_External();
                                            tep.iTrainingDiaryRefId = trainingDiary.iId;
                                            tep.cExercise = li.Text;
                                            tep.iTargetReps = 12;
                                            tep.iExerciseId = Convert.ToInt32(li.Value);
                                            tep.cTargetSets = "2-3";
                                            tdexList.Add(tep);
                                        }
                                    }
                                }
                            }
                            break;
                        case 92:
                            {
                                var allExs = (from alex in cvdc.EnumTables
                                              where alex.ID == 9
                                              where alex.floatValue != null
                                              select alex).OrderBy(x => x.floatValue);

                                foreach (EnumTable enumTable in allExs)
                                {
                                    TrainingDiaryPlanner_External tep = new TrainingDiaryPlanner_External();
                                    tep.iTrainingDiaryRefId = trainingDiary.iId;
                                    tep.cExercise = enumTable.Value;
                                    tep.iTargetReps = 12;
                                    tep.iExerciseId = (int)enumTable.intValue;
                                    tep.cTargetSets = "2-3";
                                    tdexList.Add(tep);
                                }
                            }

                            break;
                            
                    }
                    cvdc.TrainingDiaryPlanner_Externals.InsertAllOnSubmit(tdexList);
                    cvdc.SubmitChanges();
                    
                }   
                tdList.Clear();
                FillUpRestDays();
                CheckExistingPlan();
                cvdc.Dispose();
            }
            catch (Exception ex)
            {
                Label1.Text = ex.ToString();
            }
            
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

        protected void ClassSorting()
        {
            RowadDropDownList rowmonday = new RowadDropDownList(monday, 1, DropDownList6, DropDownList7, DropDownList9, 11, DropDownList10);
            RowadDropDownList rowmonday2 = new RowadDropDownList(monday2row, 1, DropDownList36, DropDownList37, DropDownList39, 12, DropDownList40);
            RowadDropDownList rowtuesday = new RowadDropDownList(tuesday, 2, DropDownList11, DropDownList12, DropDownList14, 21, DropDownList15);
            RowadDropDownList rowtuesday2 = new RowadDropDownList(tuesday2row, 2, DropDownList41, DropDownList42, DropDownList44, 22, DropDownList45);
            RowadDropDownList rowwednesday = new RowadDropDownList(wednesday, 3, DropDownList16, DropDownList17, DropDownList19, 31, DropDownList20);
            RowadDropDownList rowwednesday2 = new RowadDropDownList(wednesday2row, 3, DropDownList56, DropDownList57, DropDownList59, 32, DropDownList60);
            RowadDropDownList rowthursday = new RowadDropDownList(thursday, 4, DropDownList1, DropDownList2, DropDownList4, 41, DropDownList5);
            RowadDropDownList rowthursday2 = new RowadDropDownList(thursday2row, 4, DropDownList66, DropDownList67, DropDownList69, 42, DropDownList70);
            RowadDropDownList rowfriday = new RowadDropDownList(friday, 5, DropDownList21, DropDownList22, DropDownList24, 51, DropDownList25);
            RowadDropDownList rowfriday2 = new RowadDropDownList(friday2row, 5, DropDownList76, DropDownList77, DropDownList79, 52, DropDownList80);
            RowadDropDownList rowsaturday = new RowadDropDownList(saturday, 6, DropDownList26, DropDownList27, DropDownList29, 61, DropDownList30);
            RowadDropDownList rowsaturday2 = new RowadDropDownList(saturday2row, 6, DropDownList86, DropDownList87, DropDownList89, 62, DropDownList90);
            RowadDropDownList rowsunday = new RowadDropDownList(sunday, 7, DropDownList31, DropDownList32, DropDownList34, 71, DropDownList35);
            RowadDropDownList rowsunday2 = new RowadDropDownList(sunday2row, 7, DropDownList96, DropDownList97, DropDownList99, 72, DropDownList100);

            rowadDropDownLists = new List<RowadDropDownList>
                    {rowmonday,rowmonday2,rowtuesday,rowtuesday2, rowwednesday, rowwednesday2, rowthursday, rowthursday2, 
                    rowfriday, rowfriday2, rowsaturday, rowsaturday2,rowsunday, rowsunday2};
        }

        protected void CheckExistingPlan()
        {
            ClassSorting();
            int totExercise = 0;

            ClubVisionDataContext cvdc = new ClubVisionDataContext();

            var cep = (from ceps in cvdc.TrainingDiaries
                       where ceps.iCustomerId == Convert.ToInt32(Session["MemberNo"])
                       where ceps.iWeekNumber == 0
                       where ceps.bFromVOS == true
                       select ceps);
            int exPlanNumber = 0;
            int lastExPlanId = 0;

            foreach (RowadDropDownList rw in rowadDropDownLists)
            {
                foreach (TrainingDiary trainingDiary in cep)
                {
                    if (rw.IntValue == trainingDiary.intValue && trainingDiary.iExValue != 45)
                    {
                        rw.Exercise.SelectedValue = trainingDiary.iExValue.ToString();
                        rw.When.SelectedValue = trainingDiary.iAMPM.ToString();
                        rw.Duration.SelectedValue = trainingDiary.iDuration.ToString();
                        rw.Intensity.SelectedValue = trainingDiary.iIntensity.ToString();
                        rw.Trow.Style["display"] = "table-row";
                        totExercise++;
                        if (trainingDiary.iExValue == 45)
                        {
                            rw.Trow.Cells[2].Style["display"] = "none";
                            rw.Trow.Cells[3].Style["display"] = "none";
                            rw.Trow.Cells[4].Style["display"] = "none";
                            rw.Trow.Cells[1].ColSpan = 4;
                        }
                    }
                    if (trainingDiary.iExValue > 89) //weights program 90,91,92
                    {
                        exPlanNumber = (int)trainingDiary.iExValue;
                        lastExPlanId = (int)trainingDiary.iId;
                    }
                }
            }

            if (exPlanNumber > 89)
            {
                switch (exPlanNumber)
                {
                    case 90:
                        //this will not broken assuming the code execute it really from top to down
                        List<ListBox> beginnerLB = new List<ListBox>() { ListBox1, ListBox2, ListBox3, ListBox4 };
                        var exList = (from el in cvdc.TrainingDiaryPlanner_Externals
                                      where el.iTrainingDiaryRefId == lastExPlanId
                                      select el).OrderBy(x => x.iID);
                        int index = 0;
                        foreach (TrainingDiaryPlanner_External trainingDiaryExPlan in exList)
                        {
                            beginnerLB[index].SelectedValue = trainingDiaryExPlan.iExerciseId.ToString();
                            index++;
                        }
                        break;
                    case 91:
                        //this will not broken assuming the code execute it really from top to down
                        List<ListBox> IntermLB = new List<ListBox>() { exListBoxBeginner1, exListBoxBeginner2, exListBoxBeginner3, exListBoxBeginner4 };
                        var exListInterm = (from el in cvdc.TrainingDiaryPlanner_Externals
                                            where el.iTrainingDiaryRefId == lastExPlanId
                                            select el).OrderBy(x => x.iID);
                        int indexB = 0;
                        int indexC = 0;
                        foreach (TrainingDiaryPlanner_External trainingDiaryExPlan in exListInterm)
                        {
                            IntermLB[indexB].Items.FindByValue(trainingDiaryExPlan.iExerciseId.ToString()).Selected = true;
                            indexC++;
                            if (indexC != 2) continue;
                            indexB++;
                            indexC = 0;
                        }
                        break;
                }
            }

            if (cep.Any())
            {
                DropDownList3.SelectedValue.Equals("1");
                Label1.Text = totExercise + " exercise(s) calculated";

            }

            int aHardCardio = ProfileTab.CalculateActualHardCardio(0, Convert.ToInt32(Session["MemberNo"]));
            int aTotalCardio = ProfileTab.CalculateActualTotalCardio(0, Convert.ToInt32(Session["MemberNo"]));
            int aLMCardio = aTotalCardio - aHardCardio;
            int aWeights = ProfileTab.CalculateActualWeights(0, Convert.ToInt32(Session["MemberNo"]));

            int rHardCardio = MyGoals._hardCardio;
            int rTotalCardio = MyGoals._totalCardio;
            int rLMCardio = rTotalCardio - rHardCardio;
            int rWeights = 60;

            ActualHardCardioTextBox.Text = aHardCardio + "";
            //ActualHardCardioTextBox.Text +=  "s";
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
            LMCardioAchievedTextBox.ForeColor = rLMCardio < +aLMCardio ? Color.Green : Color.Red;

            WeightsPtAchievedTextBox.Text = rWeights <= aWeights ? "YES" : "NO";
            WeightsPtAchievedTextBox.ForeColor = rWeights <= aWeights ? Color.Green : Color.Red;


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

        protected void MyGoalImagebuttonNext_Click(object sender, ImageClickEventArgs e)
        {
            ClubVisionDataContext cvdc = new ClubVisionDataContext();

            var checkTD = (from td in cvdc.TrainingDiaries
                           where td.iCustomerId == Convert.ToInt32(Session["MemberNo"])
                           where td.bFromVOS == true
                           where td.iExValue != 45
                           select td);

            var setComplete = (from sc in cvdc.Customer_Externals
                               where sc.iID == Convert.ToInt32(Session["MemberNo"])
                               select sc).SingleOrDefault();

            if (DropDownList3.SelectedValue == "1" && checkTD.Any())
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
                if (checkTD.Any())
                {
                    p1.Style["display"] = "none";
                }
                if (DropDownList3.SelectedValue == "1")
                {
                    p2.Style["display"] = "none";
                }
            }
        }

        protected void MyGoalImagebuttonBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect("/club-vision/my-profile/edit-measurements/?tab=goals", false);
        }
        
        protected void ErrorPopup()
        {
            const string s = "<script type=\"text/javascript\">" +
                                 "alert('hahahahahh');</script>";

            Page.ClientScript.RegisterStartupScript(GetType(), "test", s);
        }

        protected void DropDownList6_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList thisddl = sender as DropDownList;
            thisddl.Visible = false;
        }

        protected void DropDownList6_ValueChanged(object sender, EventArgs e)
        {

        }

        public DataTable LINQToDataTable<T>(IEnumerable<T> varlist)
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

        protected void ListBoxValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            // ListBox thislb = sender 
            int selectionCount = 0;
            foreach (ListItem item in ListBox1.Items)
            {
                if (item.Selected) selectionCount++;
            }
            args.IsValid = (selectionCount >= 1 && selectionCount <= 5);
        }

    }

    class RowadDropDownList
    {
        public RowadDropDownList(HtmlTableRow trow, int iday, OptionGroupSelect exercise, DropDownList when, DropDownList duration, int intValue, DropDownList intensity)
        {
            this.Trow = trow;
            this.IntDay = iday;
            this.Exercise = exercise;
            this.When = when;
            this.Duration = duration;
            this.Intensity = intensity;
            this.IntValue = intValue;
        }

        public HtmlTableRow Trow { get; set; }
        public OptionGroupSelect Exercise { get; set; }
        public DropDownList When { get; set; }
        public DropDownList Duration { get; set; }
        public DropDownList Intensity { get; set; }
        public int IntDay { get; set; }
        public int IntValue { get; set; }


        // private DateTime _StartDate;
        // public DateTime StartDate { get { return _StartDate; } }
    }
}