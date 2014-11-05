using System;

namespace VisionPersonalTrainingProject.usercontrols.VVT
{
    public partial class WeightsSessionsTab : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                switch (Request.QueryString["tab"])
                {
                    case "history":
                        WeightsSessTab.Style["background"] = 
                            "url(\"/images/eHdrSessHistory.gif\") no-repeat scroll 0 0 transparent";break;
                    case "create":
                        WeightsSessTab.Style["background"] =
                            "url(\"/images/eHdrCreateNewProg.gif\") no-repeat scroll 0 0 transparent";break;
                    default:
                        WeightsSessTab.Style["background"] = 
                            "url(\"/images/eHdrCurrProg.gif\") no-repeat scroll 0 0 transparent";break;
                }
            }
        }
    }
}