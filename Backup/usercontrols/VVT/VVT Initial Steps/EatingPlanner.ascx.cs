using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VisionPersonalTrainingProject.usercontrols.VVT.VVT_Initial_Steps
{
    public partial class EatingPlanner : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;
            var cvdc = new ClubVisionDataContext();

            var macros = (from mc in cvdc.Goals
                          where mc.iCustomerID == Convert.ToInt32(Session["MemberNo"])
                          select mc).OrderByDescending(x => x.dDateCreated).FirstOrDefault();

            if (macros != null)
            {
                lblCHO.Text = macros.CHO.ToString();
                lblPTN.Text = macros.PTN.ToString();
                lblFAT.Text = macros.FAT.ToString();
            }
        }

        protected void EatingPlanButtonNextClick(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("/club-vision/my-profile/");
        }

        protected void EatingPlanButtonBack_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("/club-vision/my-profile/edit-measurements/?tab=trainingplan");
        }
    }
}