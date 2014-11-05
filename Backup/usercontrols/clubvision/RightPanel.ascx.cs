using System;
using System.Linq;

namespace VisionPersonalTrainingProject.usercontrols.clubvision
{
    public partial class RightPanel : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            setImage();
        }
        protected void setImage()
        {
            try
            {
                using (ClubVisionDataContext db = new ClubVisionDataContext())
                {
                    var customerImages = (from ci in db.CustomerImages
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
                        literalImage.Text = "<img src=\"/images/profile/" + customerImage.ProfileImage + "?refresh=" + random.Next(1000000).ToString() + "\" style=\"position: relative; top: 0px !important; width : 256px;\">";
                        //literalImage.Text = "<div style=\"position: absolute; top: -176px; left: 7px; height: 152px; width: 254px; overflow: hidden;\" class=\"thumb\"><img src=\"/images/profile/" + customerImage.ProfileImage + "?refresh=" + random.Next(1000000).ToString() + "\" style=\"position: relative; top: 0px !important;\"></div>";
                    }
                }
            }
            catch (Exception e)
            { Response.Write(e.ToString()); }
        }
    }
}