using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VisionPersonalTrainingProject.usercontrols.externalclubvision
{
    public partial class ProfileView : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            if (!Session["MemberType"].Equals("VVT")) return;
            int memberId = (int)Session["MemberNo"];

            ClubVisionDataContext cvdc = new ClubVisionDataContext();

            var customers = (from cu in cvdc.Customer_Externals
                             where cu.iID == memberId
                             select cu);
            Customer_External customer = new Customer_External();

            foreach (Customer_External customerLU in customers)
            {
                customer = customerLU;
            }


            title.InnerText = customer.PersonalProfile_Externals.cFirstName + " " + customer.PersonalProfile_Externals.cLastName;
            memberNo.InnerText = customer.iID.ToString();
            country.InnerText = customer.PersonalProfile_Externals.cCountry;

            var custLastGoal = customer.Goals.OrderByDescending(x => x.dDateCreated).FirstOrDefault();
            var custFirstGoal = customer.Goals.OrderBy(x => x.dDateCreated).FirstOrDefault();

            //progress string needs to be updated
            string progress_string = custLastGoal.fProgressWeight.ToString(); //"9kg";// customer.ProgressWt;

            var currBW = (from cbw in cvdc.Measurements
                          where cbw.iCustomerID == memberId
                          select cbw).OrderByDescending(x => x.dMeasured).FirstOrDefault();

            decimal progress_dec = decimal.Parse(progress_string) * -1;

            var custfirstjoin = (from cfj in cvdc.Goals
                                 where cfj.iCustomerID == memberId
                                 select cfj).OrderBy(x => x.dDateCreated).FirstOrDefault();

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
             */

            if (!Session["MemberType"].Equals("VVT")) return;
            int memberId = (int)Session["MemberNo"];

            ClubVisionDataContext cvdc = new ClubVisionDataContext();

            var customers = (from cu in cvdc.Customer_Externals
                             where cu.iID == memberId
                             select cu).SingleOrDefault();
           
            title.InnerText = customers.cFirstName + " " + customers.cLastName;
            ProfileNameLiteral.Text = customers.cFirstName + "'s Profile";
            memberNo.InnerText = customers.iID.ToString();
            country.InnerText = "N/A";

            if(customers.PersonalProfile_Externals != null)
            {
                country.InnerText = customers.PersonalProfile_Externals.cCountry;
            }
                
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