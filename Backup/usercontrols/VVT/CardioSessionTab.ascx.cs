using System;

namespace VisionPersonalTrainingProject.usercontrols.VVT
{
    public partial class CardioSessionTab : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                /*
                switch (Request.QueryString["tab"])
                {
                    case "history":
                        WeightsSessTab.Style["background"] =
                            "url(\"/images/eHdrCardioSessTab-2.gif\") no-repeat scroll 0 0 transparent"; break;
                    case "create":
                        WeightsSessTab.Style["background"] =
                            "url(\"/images/eHdrCreateNewProg.gif\") no-repeat scroll 0 0 transparent"; break;
                    default:
                        WeightsSessTab.Style["background"] =
                            "url(\"/images/eHdrCardioSessTab-1.gif\") no-repeat scroll 0 0 transparent"; break;
                }
                 * */
            }
        }
    }
}