using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace VisionPersonalTrainingProject.usercontrols.elements
{
    public partial class FindLocalStudioFooter : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataTable dtStudiosFoot = VPTFacilities.StudioLocationsGet();
                ddlStudioFoot.DataSource = dtStudiosFoot;
                ddlStudioFoot.DataValueField = dtStudiosFoot.Columns["StudioID"].ToString();
                ddlStudioFoot.DataTextField = dtStudiosFoot.Columns["StudioName"].ToString();
                ddlStudioFoot.DataBind();
                ddlStudioFoot.Items.Insert(0, "");
                ddlStudioFoot.Items[0].Selected = true;
            }
        }

        protected void btnFindFoot_Click(object sender, EventArgs e)
        {
            int StudioID;
            int.TryParse(ddlStudioFoot.SelectedValue, out StudioID);
            if (StudioID > 0)
                Response.Redirect("~/studio-finder/?sid=" + ddlStudioFoot.SelectedValue);
            else
                if (tbPostcodeFoot.Text.Length > 0 && tbPostcodeFoot.Text != "Enter Postcode")
                    Response.Redirect("~/studio-finder/?pc=" + tbPostcodeFoot.Text);

        }

        protected void ddlStudioFoot_SelectedIndexChanged(object sender, EventArgs e)
        {
            int StudioID;
            int.TryParse(ddlStudioFoot.SelectedValue, out StudioID);
            if (StudioID > 0)
                Response.Redirect("~/studio-finder/?sid=" + ddlStudioFoot.SelectedValue);
                //Response.Redirect("/StudioFinder.aspx?sid=" + ddlStudioFoot.SelectedValue);
        }
    }
}