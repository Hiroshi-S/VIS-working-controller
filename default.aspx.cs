using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using VisionPersonalTrainingProject.usercontrols.VVT;
using VisionPersonalTrainingProject.usercontrols.clubvision;

namespace VisionPersonalTrainingProject
{
    public partial class copytable : System.Web.UI.Page
    {
        /*
        readonly WeightSessions _ws = new WeightSessions();
        private readonly System.DateTime _when = System.DateTime.Today;
        private int _weeknumber = 0;
        private string _programSummary = "";
        private DataTable _dtt;
        */
        protected void Page_Load(object sender, EventArgs e)
        {

            VisionPersonalTrainingProject.VOSWebService.Service service = new VisionPersonalTrainingProject.VOSWebService.Service();
            service.AuthenticationHeaderValue = new VisionPersonalTrainingProject.VOSWebService.AuthenticationHeader();
            service.AuthenticationHeaderValue.UserName = "vosABC";
            service.AuthenticationHeaderValue.Password = "vosABCpass1";
            //string username = "katyelliott";
            //string password = "alexandra";

            service.GetMemberGoals(41484);

            /*
            Session["MemberType"] = "VPT";

            //ClubVisionDataContext cvdc = new ClubVisionDataContext();


            Session["MemberNo"] = 57017;
            //Session["MemberType"] = "VVT";
            Session["Admin"] = "No";
            Session["Trainer"] = "No";
            Session["FromTrainer"] = "Yes";
            
            VisionPersonalTrainingProject.VOSWebService.Service service = new VisionPersonalTrainingProject.VOSWebService.Service();
            service.AuthenticationHeaderValue = new VisionPersonalTrainingProject.VOSWebService.AuthenticationHeader();
            service.AuthenticationHeaderValue.UserName = "vosABC";
            service.AuthenticationHeaderValue.Password = "vosABCpass1";
            //string username = "katyelliott";
            //string password = "alexandra";

            service.GetMemberGoals(41484);
            
            GridView1.DataSource = service.GetMemberTraining(41484);
            GridView1.DataBind();
           
            
            ClubVisionDataContext cvdc = new ClubVisionDataContext();
            var stud = (from sutd in cvdc.EnumTables
                        where sutd.ID == 11
                        where sutd.intValue != 0
                        orderby sutd.Value
                        select sutd);
            foreach (EnumTable enumTable in stud)
            {
                Literal1.Text += "'" + enumTable.intValue + "':'" + enumTable.Value + "',";
            }
            */
        }

        public void lalalala()
        {

            VisionPersonalTrainingProject.ClubVisionDataContext cvdc = new VisionPersonalTrainingProject.ClubVisionDataContext();
            var FoodDiaryAccels = (from fda in cvdc.WeeklyAccelDaySummaries
                                    where fda.CustomerId == (int)Session["MemberNo"]
                                    where fda.dAcceleratorDay >= Convert.ToDateTime(Request.QueryString["dateTrainingStart"])
                                    where fda.dAcceleratorDay < Convert.ToDateTime(Request.QueryString["dateTrainingStart"]).AddDays(7)
                                    select fda);

            if(FoodDiaryAccels.Any())
            {
                foreach (WeeklyAccelDaySummary weeklyAccelDaySummary in FoodDiaryAccels)
                {
                    cvdc.WeeklyAccelDaySummaries.DeleteOnSubmit(weeklyAccelDaySummary);
                }
                cvdc.SubmitChanges();
            }

            if (!Request.QueryString["date"].Equals("NA"))
            {
                VisionPersonalTrainingProject.WeeklyAccelDaySummary wads = new VisionPersonalTrainingProject.WeeklyAccelDaySummary();

                wads.CustomerId = (int)Session["MemberNo"];
                wads.iWeekNumber = Convert.ToInt32(Request.QueryString["week"]);
                wads.iYear = Convert.ToInt32(Request.QueryString["year"]);
                wads.dAcceleratorDay = Convert.ToDateTime(Request.QueryString["date"]);

                cvdc.WeeklyAccelDaySummaries.InsertOnSubmit(wads);
                cvdc.SubmitChanges();
            }
        }

        public void readLineFromText()
        {
            try
            {
                using (StreamReader sr = new StreamReader(@"c:\Users\dewi.candraningsih\Documents\bootstrap-combined.min.css"))
                {
                    String line = sr.ReadToEnd();
                    

                    // Write the string to a file.
                    System.IO.StreamWriter file = new System.IO.StreamWriter(@"c:\Users\dewi.candraningsih\Documents\bootstrap-combined-121.min.css");
                    file.WriteLine(line.Replace("{", "{" + System.Environment.NewLine + "  ").Replace("}", "}" + System.Environment.NewLine + System.Environment.NewLine).Replace(";", ";" + System.Environment.NewLine + "  "));

                    file.Close();

                    //Literal1.Text = "succeed";
                }
            }
            catch (Exception e)
            {
              //  Literal1.Text = "The file could not be read:";
            }
        }
        
    }
}