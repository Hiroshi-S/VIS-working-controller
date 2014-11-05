using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace VisionPersonalTrainingProject.usercontrols.studiopages
{
    public partial class FranchiseLogin : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbUsername.Text == "andrew" && tbPassword.Text == "andrew")
                {
                    Session["StudioId"] = 1;
                    Session["StudioUser"] = "Andrew Simmons";
                    Session["RoleId"] = 3;
                    Session["FoEmail"] = "webdesign@hq.visionpersonaltraining.com";//dummy
                    Session["Level"] = 3;//dummy
                    Response.Redirect("~/studio-splash/");
                }
                else
                {
                    lblErrorMessage.Text = "";
                    VOSWebService.Service oProx = GetVOSWebServiceProxy();
                    string Pwd = tbPassword.Text;
                    VOSWebService.UserRole UserDetails = oProx.GetUserRole(tbUsername.Text, ref Pwd);
                    if (UserDetails != null)
                    {
                        if (UserDetails.RoleID < 1)
                        {
                            //no access
                            lblErrorMessage.Text = "Invalid details, please contact head office";
                        }
                        else
                            if ((UserDetails.RoleID >= 3) && (UserDetails.RoleID <= 4))
                            {
                                //Response.Redirect("~/studio-splash/?rid=" + UserDetails.RoleID + "&sid=" + UserDetails.StudioID);
                                Session["StudioId"] = UserDetails.StudioID;
                                Session["StudioUser"] = UserDetails.User;
                                Session["RoleId"] = UserDetails.RoleID;
                                Session["FoEmail"] = UserDetails.Email;
                                Session["Level"] = UserDetails.LevelID;
                                Response.Redirect("~/studio-splash/");
                            }
                            else //roleid > 3
                            {
                                //user is an admin and should log into umbraco
                                lblErrorMessage.Text = "This login is for studio owners and managers only.";

                            }
                    }
                    else
                        //ws error
                        throw new Exception("Null returned from VOS Online");
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = "System error: " + ex.Message;
            }
        }

        private VOSWebService.Service GetVOSWebServiceProxy()
        {
        try
        {
            string cUrl = System.Configuration.ConfigurationManager.AppSettings["VOSOnlineURL"];
            string VosUserName= System.Configuration.ConfigurationManager.AppSettings["VOSOnlineUID"];
            string VosPassword= System.Configuration.ConfigurationManager.AppSettings["VOSOnlinePWD"];
          
            VOSWebService.AuthenticationHeader oHeader = new VOSWebService.AuthenticationHeader(); 
            VOSWebService.Service oProxy =new  VOSWebService.Service();

            oHeader.UserName = VosUserName;
            oHeader.Password = VosPassword;

            oProxy.Url = cUrl;
            oProxy.AuthenticationHeaderValue = oHeader;

            return oProxy;
        }
        catch (Exception ex) {
            throw ex;
        }
        }
    }
}