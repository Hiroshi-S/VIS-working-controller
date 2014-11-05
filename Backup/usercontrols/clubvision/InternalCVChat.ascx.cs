using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASPNETChatControl;

namespace VisionPersonalTrainingProject.usercontrols.clubvision
{
    public partial class InternalCVChat : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var memberid = (int)Session["MemberNo"];

            ClubVisionDataContext cvdc = new ClubVisionDataContext();

            var custdet = (from ct in cvdc.Customers
                           where ct.Id == (int)Session["MemberNo"]
                           select ct).SingleOrDefault();

            ChatControl.StartSession(memberid.ToString(), custdet.Firstname + " " +custdet.LastName,
                                     "http://www.visionpt.com.au/images/profile/" + memberid.ToString() + ".jpg");

        }
    }
}