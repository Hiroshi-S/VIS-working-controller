using System;
using System.Linq;

namespace VisionPersonalTrainingProject.usercontrols.clubvision
{
    public partial class ProgressAndGoal : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            switch (Session["MemberType"].ToString())
            {
                case "VPT":
                    loadDetailInternal((int)Session["MemberNo"]);
                    break;
                case "VVT":
                    loadDetailExternal((int)Session["MemberNo"]);
                    break;
            }
        }
        protected void loadDetailInternal(int memberId)
        {
            try
            {
                using (ClubVisionDataContext db = new ClubVisionDataContext())
                {
                    var detail = (from c in db.Customers
                                  where c.Id == memberId
                                  orderby c.StartDate
                                  select c).FirstOrDefault();

                    var weight = (from w in db.CustomerWeights
                                  where w.CustomerId == memberId
                                  orderby w.WeightDate descending
                                  select w).FirstOrDefault();

                    string str = "";
                    DateTime startDate = detail.StartDate;
                    decimal weightDifference = Convert.ToDecimal(detail.StartWt.Remove(detail.StartWt.Length - 2)) - weight.Weight;

                    if (weightDifference < 0)
                        str = "kg gained";
                    else if (weightDifference >= 0)
                        str = "kg lost";

                    weightDifferenceLable.Text = Math.Abs(weightDifference).ToString("0.0");
                    sinceDateLabel.Text = startDate.ToShortDateString();
                    goalWeightLabel.Text = detail.GoalWt.Remove(detail.GoalWt.Length - 2).ToString();
                    goalDateLabel.Text = detail.NextGS.ToShortDateString();
                    gainLostLabel.Text = str;
                    unitLabel.Text = "kg";
                    goalLabel.Text = detail.Goal;
                }
            }
            catch (Exception e)
            {
                Response.Write(e.ToString());
            }
        }
        protected void loadDetailExternal(int memberId)
        {
            try
            {
                using (ClubVisionDataContext db = new ClubVisionDataContext())
                {
                    var detail = (from c in db.Goals
                                  where c.iCustomerID == memberId
                                  where c.fBodyWeightGoal > 0
                                  orderby c.dDateCreated descending
                                  select c).FirstOrDefault();

                    var firstweight = (from w in db.CustomerWeights
                                       where w.CustomerId == memberId
                                       where w.Weight > 0
                                       orderby w.WeightDate
                                       select w).FirstOrDefault();

                    var weight = (from w in db.CustomerWeights
                                  where w.CustomerId == memberId
                                  where w.Weight > 0
                                  orderby w.WeightDate descending
                                  select w).FirstOrDefault();

                    DateTime startDate = detail.dDateCreated;
                    string unit = "kg";
                    string str = "";
                    decimal weightDifference = firstweight.Weight - weight.Weight;

                    if (detail.bIsMetric == false)
                        unit = "lb";
                    if (weightDifference < 0)
                        str = unit + " gained";
                    else if (weightDifference >= 0)
                        str = unit + " lost";

                    weightDifferenceLable.Text = Math.Abs(weightDifference).ToString("0.0");
                    sinceDateLabel.Text = startDate.ToShortDateString();
                    goalWeightLabel.Text = detail.fBodyWeightGoal.ToString();
                    goalDateLabel.Text = startDate.AddDays(62).ToShortDateString();//starting day + 62 days makes 9 weeks.
                    gainLostLabel.Text = str;
                    unitLabel.Text = unit;
                    goalLabel.Text = detail.cGoalProgram;
                }
            }
            catch (Exception e)
            {
                weightDifferenceLable.Text = "N/A";
                sinceDateLabel.Text = "N/A";
                goalWeightLabel.Text = "N/A";
                goalDateLabel.Text = "N/A";
                gainLostLabel.Text = "N/A";
                unitLabel.Text = "N/A";
                goalLabel.Text = "N/A";
            }
        }
    }
}