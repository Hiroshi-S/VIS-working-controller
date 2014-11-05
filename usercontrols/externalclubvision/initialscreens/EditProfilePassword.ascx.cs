using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VisionPersonalTrainingProject.usercontrols.externalclubvision.initialscreens
{
    public partial class EditProfilePassword : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                editPasswordLiteral.Text = "";

                if (Request.QueryString["message"] != null && Request.QueryString["message"].Equals("success"))
                {
                    editPasswordLiteral.Text = "<p style=\"color:red !important;\">Congratulation you have succesfully changed your password.</p>";
                }
            }

        }

        protected void SaveClick(object sender, EventArgs e)
        {
            try
            {
                int memberId = (int)Session["MemberNo"];

                string password = newPassword.Value;

                ClubVisionDataContext cvdc = new ClubVisionDataContext();

                var custPwd = (from cp in cvdc.Customer_Externals
                               where cp.iID == memberId
                               select cp).SingleOrDefault();

                if (custPwd != null)
                {
                    if (custPwd.cPassword.Equals(oldPassword.Value) && password.Length >= 6)
                    {
                        custPwd.cPassword = password;

                        //update member in umbraco 
                        umbraco.cms.businesslogic.member.Member updateMember = umbraco.cms.businesslogic.member.Member.GetMemberFromLoginName(Request.RequestContext.HttpContext.User.Identity.Name);
                        updateMember.Password = password;
                        updateMember.Save();

                        cvdc.SubmitChanges();
                        cvdc.Dispose();
                        Response.Redirect(Request.RawUrl + "?tab=password&message=success", false);
                        //Response.Redirect("/ext-club-vision/account/my-profile/edit-picture-password/?tab=password&message=success", false);
                    }
                    else
                    {

                        editPasswordLiteral.Text = "<p style=\"color:red !important;\">Please enter the correct one and password must be at least 6 characters long</p>";
                        DisplayTabPassword();
                    }
                }
            }
            catch (Exception ex)
            {
                editPasswordLiteral.Text = "<p style=\"color:red !important;\">Please enter the correct value.</p>";
                DisplayTabPassword();
            }

        }

        public void DisplayTabPassword()
        {
            const string s = "<script cardioType=\"text/javascript\">" +
                             "document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_PasswordPictureEditTab_4_profileTab').style.backgroundImage = 'url(/images/ExtClubVision/eHdrPasswordEditTab.gif)';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_PasswordPictureEditTab_4_eEditTabProfilePicture').style.display = 'none';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_PasswordPictureEditTab_4_eEditTabPassword').style.display = 'block';</script>";

            Page.ClientScript.RegisterStartupScript(GetType(), "test", s);

        }
    }
}