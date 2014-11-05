using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VisionPersonalTrainingProject.usercontrols.externalclubvision
{
    public partial class ProfileEditMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Session["MemberType"].Equals("VVT")) return;
            int memberId = (int)Session["MemberNo"];

            ClubVisionDataContext cvdc = new ClubVisionDataContext();

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
            {//mod 27/09
                literalImage.Text = "<img src=\"/images/profile/" + customerImage.ProfileImage + "?refresh=" + random.Next(1000000).ToString() + "\" style=\"position: relative; top: 0px !important;\">";                       
                //literalImage.Text = "<div style=\"position: absolute; top: -181px; left: 7px; height: 152px; width: 254px; overflow: hidden;\"><img src=\"/images/profile/" + customerImage.ProfileImage + "?refresh=" + random.Next(1000000).ToString() + "\" style=\"position: relative;\"></div>";
            }
            bool isUserCompleted = (from iuc in cvdc.Customer_Externals
                                    where iuc.iID == (int)Session["MemberNo"]
                                    select iuc.bCompleteInitialState).SingleOrDefault();
            if (isUserCompleted || Session["MemberType"].Equals("VPT"))
            {
                bulletsteps.Visible = false;
            }
            cvdc.Dispose();
        }
    }
}