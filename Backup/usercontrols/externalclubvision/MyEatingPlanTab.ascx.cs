using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VisionPersonalTrainingProject.usercontrols.externalclubvision
{
    public partial class MyEatingPlanTab : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;
            var cvdc = new ClubVisionDataContext();
            var iscomplete = (from cs in cvdc.Customer_Externals
                              where cs.iID == Convert.ToInt32(Session["MemberNo"])
                              where cs.bCompleteInitialState == true
                              select cs).SingleOrDefault();
            if (iscomplete != null)
            {
                tabeating2.Style["display"] = "block";
                tabeating1.Style["display"] = "none";
                //Response.Write("td entry state  ============> come here");
            }

            //Response.Write("td entry state");
        }
    }
}