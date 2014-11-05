using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VisionPersonalTrainingProject.usercontrols.externalclubvision
{
    public partial class PasswordPictureEditTab : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    string tab = Request.QueryString["tab"];
                    if (tab != null)
                    {
                        switch (tab)
                        {
                            case "password":
                                DisplayTabPassword();
                                break;
                            default:
                                break;
                        }
                    }
                }
                catch (Exception exception)
                {
                    Response.Write(exception.ToString());
                }
            }

        }

        public void DisplayTabPassword()
        {
            const string s = "<script type=\"text/javascript\">" +
                             "document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_PasswordPictureEditTab_4_profileTab').style.backgroundImage = 'url(/images/ExtClubVision/eHdrPasswordEditTab.gif)';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_PasswordPictureEditTab_4_eEditTabProfilePicture').style.display = 'none';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_PasswordPictureEditTab_4_eEditTabPassword').style.display = 'block';</script>";

            Page.ClientScript.RegisterStartupScript(GetType(), "test", s);

        }
    }
}