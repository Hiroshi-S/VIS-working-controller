using System;
using System.Linq;

namespace VisionPersonalTrainingProject.usercontrols.externalclubvision
{
    public partial class MyJouneyTab : System.Web.UI.UserControl
    {
        int memberId;
        decimal weight;
        DateTime weightDate;

        protected void Page_Load(object sender, EventArgs e)
        {
            /*if (!Page.IsPostBack)
            {
                string str = "<script type=\"text/javascript\">loadTabs()</script>";
                Page.ClientScript.RegisterStartupScript(GetType(), "test2", str);
            }*/
            if ((string)Session["MemberType"] == "VVT")  
            {
                ExtPanel.Visible = true;
                loadWeights();
            }
            else if ((string)Session["MemberType"] == "VPT")
            {
                ExtPanel.Visible = false;
                //24/09 Hiroshi*****************
                //tabMyProgress.Style["border-bottom"] = "1px solid #e27423";
                tabMeasurements.Style["background-color"] = "White";
               // tabMeasurements.Style["border-bottom"] = "1px solid #e27423";
                tabMeasurements.Style["cursor"] = "default";
                tabMeasurements.Attributes.Remove("onclick");
                tabMyGoals.Style["background-color"] = "White";
                //tabMyGoals.Style["border-bottom"] = "1px solid #e27423";
                tabMyGoals.Style["cursor"] = "default";
                tabMyGoals.Attributes.Remove("onclick");
                //added end**********************
            }
            string s = "<script type=\"text/javascript\">drawChart()</script>";//12/09 for graph fix
            Page.ClientScript.RegisterStartupScript(GetType(), "test", s); //12/09 for graph fix
                
        }
        
        protected void loadWeights()
        {
            using (ClubVisionDataContext db = new ClubVisionDataContext())
            {
                memberId = (int)Session["MemberNo"];
                var weight = (from w in db.CustomerWeights
                              where w.CustomerId == memberId
                              orderby w.WeightDate descending
                              select w).FirstOrDefault();
                switch (weight.bIsMetric)
                {
                    case true:
                        { unitLabel.Text = "kg"; }
                        break;
                    case false:
                        { unitLabel.Text = "lb"; }
                        break;
                }

                currentWeightLabel.Text = weight.Weight.ToString();
                currentWeightDateLabel.Text = weight.WeightDate.ToShortDateString();
            }
        }
        
        protected void SaveWeightButton_Click(object sender, EventArgs e)
        {
            memberId = (int)Session["MemberNo"];
            weightDate = DateTime.Now;
            weight = Convert.ToDecimal(weightTextBox.Text);

            SaveWeight(memberId, weightDate, weight, false, true);
            Response.Redirect(Request.RawUrl);
        }
        
        protected void SaveWeight(int memberId, DateTime weightDate, decimal weight, bool isOfficial, bool isbLsMetric)
        {
            using (ClubVisionDataContext db = new ClubVisionDataContext())
            {
                CustomerWeight cw = new CustomerWeight();
                cw.CustomerId = memberId;
                cw.WeightDate = weightDate;
                cw.Weight = weight;
                cw.IsOfficial = isOfficial;
                cw.bIsMetric = isbLsMetric;

                db.CustomerWeights.InsertOnSubmit(cw);
                db.SubmitChanges();
            }
        }

    }
}