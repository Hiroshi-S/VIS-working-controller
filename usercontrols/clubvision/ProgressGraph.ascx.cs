using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VisionPersonalTrainingProject.usercontrols.clubvision
{
    public partial class ProgressGraph : System.Web.UI.UserControl
    {
        int memberId;
        string chartProgressData_str = "";
        string lastGoalSession="",nextGoalSession = "", goalWeight = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            memberId = (int)Session["MemberNo"];
            loadWeightInfo(memberId);
            //drawChart();
        }
        protected void loadWeightInfo(int memberId)
        {
            using (ClubVisionDataContext db = new ClubVisionDataContext())
            {
                var weights = from ws in db.CustomerWeights
                              where ws.CustomerId == memberId
                              orderby ws.WeightDate
                              select ws;

                foreach (CustomerWeight weight in weights)
                {
                    chartProgressData_str += "['" + weight.WeightDate.ToString("yyyy-MM-dd h:mmt") + "M', " + weight.Weight.ToString() + "],";
                }

                if (chartProgressData_str.Length > 0)
                {
                    chartProgressData_str = chartProgressData_str.Remove(chartProgressData_str.Length - 1);
                }
                switch ((string)Session["MemberType"])
                {
                    case "VPT":
                        {
                            var customers = (from cu in db.Customers
                                             where cu.Id == memberId
                                             select cu);
                            var customer = new Customer();

                            foreach (Customer customerLU in customers)
                            {
                                customer = customerLU;
                            }
                            lastGoalSession = customer.LastGS.ToString("yyyy-MM-dd h:mmt");
                            nextGoalSession = customer.NextGS.ToString("yyyy-MM-dd h:mmt");
                            Session["lastGoalSession"] = lastGoalSession.Split(' ')[0];
                            Session["nextGoalSession"] = nextGoalSession.Split(' ')[0];
                            goalWeight = customer.GoalWt.Remove(customer.GoalWt.Length - 2);
                        }
                        break;
                    case "VVT":
                        {
                            var customers = (from cu in db.Customer_Externals
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
                                nextGoalSession = custGoalfirst.dDateCreated.AddMonths(3).ToString("yyyy-MM-dd h:mmt");
                                goalWeight = custGoalfirst.fBodyWeightGoal.ToString();
                            }
                        }
                        break;
                }

                chartProgressData.Text = chartProgressData_str;
                chartProgressDataGoal.Text = "['" + nextGoalSession + "M', " + goalWeight + "],";
            }
        }
        public void drawChart()
        {
            string s = "<script type=\"text/javascript\">" +
                             "var chartdata_weight = [<asp:Literal ID=\"chartProgressData\" runat=\"server\"></asp:Literal>];<br>"
                             + "var chardata_weight_goal = [<asp:Literal ID=\"chartProgressDataGoal\" runat=\"server\"></asp:Literal>];<br>"
                             + "var plot_weight = $.jqplot('test1', [chartdata_weight , chardata_weight_goal], {title: '',gridPadding: { right: 35 },axes: { xaxis: { renderer: $.jqplot.DateAxisRenderer,tickOptions: { formatString: '%b%y' },tickInterval: '3 month'}}, seriesDefaults: { showMarker:true,pointLabels: { show:true } }});"
                             + "</script>";
            string aaa = "<script type=\"text/javascript\">"
                        + "alert('sldkf');"
                        + "</script>";

            Page.ClientScript.RegisterStartupScript(this.GetType(), "test", aaa);
        }
    }
}