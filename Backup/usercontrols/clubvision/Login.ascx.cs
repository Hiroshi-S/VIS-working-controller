using System;

namespace VisionPersonalTrainingProject.usercontrols.clubvision
{
    public partial class Login : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Request.QueryString["fail"] == "true")
            {
                error.Visible = true;
            }
            else
            {
                error.Visible = false;
            }

            if (this.Session["MemberNo"] != null)
            {
                if (Convert.ToInt32(this.Session["MemberNo"]) > 1000000 && Convert.ToInt32(this.Session["MemberNo"]) < 1000000001)
                {
                    Response.Redirect("/club-vision/my-profile/my%20profile%20vvt%20external.aspx", false);
                }
                else
                {
                    Response.Redirect("/club-vision/my-profile", false);
                }
            }
        }

    }
}