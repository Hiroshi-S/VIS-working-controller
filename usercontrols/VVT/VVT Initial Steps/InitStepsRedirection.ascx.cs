using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VisionPersonalTrainingProject.usercontrols.VVT.VVT_Initial_Steps
{
    public partial class InitStepsRedirection : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                //check the last skip
                ClubVisionDataContext cvdc = new ClubVisionDataContext();

                int? lastskip = 0;
                cvdc.SkipInitialStep((int) Session["MemberNo"], ref lastskip);

                List<string> accountSetupAddresses = InitStepsNavigation.GetHrefs();

                if (lastskip != null && lastskip > 0) Response.Redirect(accountSetupAddresses[((int)lastskip-1)], false);
            }
        }
    }
}