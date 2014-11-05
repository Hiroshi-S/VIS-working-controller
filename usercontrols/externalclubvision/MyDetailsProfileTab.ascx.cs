using System;
using System.Linq;

namespace VisionPersonalTrainingProject.usercontrols.externalclubvision
{
    public partial class MyDetailsProfileTab : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;
            try
            {
                string tab = Request.QueryString["tab"];
                if (tab != null)
                {
                    switch (tab)
                    {
                        case "lifestylescreen":
                            DisplayLifeStyleScreen();
                            break;
                        case "bodytype":
                            DisplayBodyType();
                            break;
                        default:
                            break;
                    }
                }

                var cvdc = new ClubVisionDataContext();

                var pprofile = (from pp in cvdc.PersonalProfile_Externals
                                where pp.iCustomerID == Convert.ToInt32(Session["MemberNo"])
                                select pp).SingleOrDefault();
                if (pprofile != null)
                {
                    tabLifestyleScreen2.Style["display"] = "block";
                    tabLifestyleScreen1.Style["display"] = "none";
                }

                var clfscreen = (from lfs in cvdc.FormResults
                                 where lfs.iCustomerID == Convert.ToInt32(Session["MemberNo"])
                                 where lfs.iFormID == 1
                                 select lfs).FirstOrDefault();
                if (clfscreen != null)
                {
                    tabBodyType2.Style["display"] = "block";
                    tabBodyType1.Style["display"] = "none";
                }

                cvdc.Dispose();
            }
            catch (Exception exception)
            {
                Response.Write(exception.ToString());
            }
        }

        public void DisplayLifeStyleScreen()
        {
            profileTab.Style["background-image"] = "url(/images/ExtClubVision/eHdrProfileTabLifestyleScreen.gif)";
            eMyDetails.Style["display"] = "none";
            eLifestyleScreen.Style["display"] = "block";
            eBodyType.Style["display"] = "none";
        }

        public void DisplayBodyType()
        {
            profileTab.Style["background-image"] = "url(/images/ExtClubVision/eHdrProfileTabBodyType.gif)";
            eMyDetails.Style["display"] = "none";
            eLifestyleScreen.Style["display"] = "none";
            eBodyType.Style["display"] = "block";
        }

    }
}