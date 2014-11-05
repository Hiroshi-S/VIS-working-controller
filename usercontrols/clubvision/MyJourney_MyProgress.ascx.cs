using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VisionPersonalTrainingProject.usercontrols.externalclubvision.initialscreens;

namespace VisionPersonalTrainingProject.usercontrols.clubvision
{
    public partial class MyJourney_MyProgress : System.Web.UI.UserControl
    {
        private System.DateTime _when = System.DateTime.Today;

        private string _currentCategory = "";

        private int _weeknumber = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Request.QueryString["when"] != null)
                {
                    _when = System.DateTime.Parse(Request.QueryString["when"]);
                }

                var cvdc = new VisionPersonalTrainingProject.ClubVisionDataContext();

                //iIDLabel.Text = "-1";

                var memberId = (int)Session["MemberNo"];

                var customerWeights = (from cw in cvdc.CustomerWeights
                                       where cw.CustomerId == memberId
                                       orderby cw.WeightDate
                                       select cw
                );

                string chartProgressData_str = "";

                foreach (CustomerWeight customerWeight in customerWeights)
                {
                    chartProgressData_str += "['" + customerWeight.WeightDate.ToString("yyyy-dd-MM h:mmt") + "M', " + customerWeight.Weight.ToString() + "],";
                }

                if (chartProgressData_str.Length > 0)
                {
                    chartProgressData_str = chartProgressData_str.Remove(chartProgressData_str.Length - 1);
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

                //chartMacrosData_Target.Text = carb + "," + ptn + "," + fat;

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
            }
        }
    }
}