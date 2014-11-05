using System;
using System.Linq;

namespace VisionPersonalTrainingProject.usercontrols.VVT
{
    public partial class ProfileView : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Session["MemberType"].Equals("VPT")) return;
            int memberId = (int)Session["MemberNo"];

            ClubVisionDataContext cvdc = new ClubVisionDataContext();

            var customers = (from cu in cvdc.Customers
                             where cu.Id == memberId
                             select cu);

            Customer customer = new Customer();

            foreach (Customer customerLU in customers)
            {
                customer = customerLU;
            }

            title.InnerText = customer.Firstname + " " + customer.LastName;
            ProfileNameLiteral.Text = customer.Firstname + "'s Profile";
            //memberNo.InnerText = customer.Id.ToString();
            studio.InnerText = customer.Studio;
            trainer.InnerText = customer.Trainer;
            target.InnerText = customer.GoalWt.Remove(customer.GoalWt.Length - 2);
            target_text.InnerHtml = "kg <br />" +
                                    "by <span style=\"color:#E27423;\">" + customer.NextGS.ToString("dd/MM/yyyy") + "</span>";

            if (customer.WeeklyWeight != null && customer.WeeklyGoal != null)
            {
                weeklyWeight.InnerText = customer.WeeklyWeight;
                weeklyTarget.InnerText = customer.WeeklyGoal;
                WeeklyGoals.Visible = true;
                profileViewContent.Style["height"] = "520px";
            }

            string progress_string = customer.ProgressWt;

            int kg_pos = progress_string.IndexOf("kg");

            decimal progress_dec = decimal.Parse(progress_string.Remove(kg_pos));

            change_text.InnerHtml = "kg";

            if (progress_dec > 0)
            {
                change_text.InnerHtml += " gained";
            }
            if (progress_dec < 0)
            {
                change_text.InnerHtml += " lost";
                progress_dec = -progress_dec;
            }

            change_text.InnerHtml += "<br />since <span style=\"color:#008CA7;\">" +
                                     customer.StartDate.ToString("dd/MM/yyyy") +
                                     "</span>";

            change.InnerText = progress_dec.ToString();
            goal.InnerText = customer.Goal;

            var customerImages = (from ci in cvdc.CustomerImages
                                  where ci.CustomerId == (int)Session["MemberNo"]
                                  select ci);

            CustomerImage customerImage = new CustomerImage();
            foreach (CustomerImage customerImageLU in customerImages)
            {
                customerImage = customerImageLU;
            }

            Random random = new Random();

            if (customerImage.ProfileImage != null)
            {
                literalImage.Text = "<div style=\"position: absolute; top: -176px; left: 7px; height: 152px; width: 254px; overflow: hidden;\" class=\"thumb\"><img src=\"/images/profile/" + customerImage.ProfileImage + "?refresh=" + random.Next(1000000).ToString() + "\" style=\"position: relative; top: 0px !important;\"></div>";
            }

            cvdc.Dispose();
        }
    }
}