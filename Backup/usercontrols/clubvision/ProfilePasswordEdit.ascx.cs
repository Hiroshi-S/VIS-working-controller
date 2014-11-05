using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VisionPersonalTrainingProject.usercontrols.clubvision
{
    public partial class ProfilePasswordEdit : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void save_Click(object sender, EventArgs e)
        {
            try
            {
                if ((string)Session["MemberType"] == "VVT")
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
                            //Response.Redirect(Request.RawUrl + "?tab=password&message=success", false);
                            //Response.Redirect("/ext-club-vision/account/my-profile/edit-picture-password/?tab=password&message=success", false);
                        }
                        else
                        {

                            editPasswordLiteral.Text = "<p style=\"color:red !important;\">Please enter the correct one and password must be at least 6 characters long</p>";
                           // DisplayTabPassword();
                        }
                    }
                }
                else
                {
                    VOSWebService.Service service = new VOSWebService.Service();
                    service.AuthenticationHeaderValue = new VOSWebService.AuthenticationHeader();
                    service.AuthenticationHeaderValue.UserName = "vosABC";
                    service.AuthenticationHeaderValue.Password = "vosABCpass1";

                    int memberId = (int)Session["MemberNo"];

                    string password = newPassword.Value;

                    service.UpdateMemberPasswordAccount(memberId, oldPassword.Value, ref password);

                    umbraco.cms.businesslogic.member.Member updateMember = umbraco.cms.businesslogic.member.Member.GetMemberFromLoginName(Request.RequestContext.HttpContext.User.Identity.Name);
                    updateMember.Password = password;
                    updateMember.Save();
                    Response.Redirect("/club-vision/my-profile/");
                }                
            }
            catch (Exception ex)
            {
                
                 editPasswordLiteral.Text = "<p style=\"color:red;\">" + ex + "</p>";
            }          
        }
    }
}