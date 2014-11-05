using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace VisionPersonalTrainingProject.usercontrols.elements
{
    public partial class FindLocalStudioElement : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataTable dtStudios = VPTFacilities.StudioLocationsGet();
                ddlStudio.DataSource = dtStudios;
                ddlStudio.DataValueField = dtStudios.Columns["StudioID"].ToString();
                ddlStudio.DataTextField = dtStudios.Columns["StudioName"].ToString();
                ddlStudio.DataBind();
                ddlStudio.Items.Insert(0, "");
                ddlStudio.Items[0].Selected = true;
            }
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            if (tbPostcode.Text.Length>0 && tbPostcode.Text!="Enter Postcode")
                Response.Redirect("~/studio-finder/?pc=" + tbPostcode.Text);

        }

        protected void ddlStudio_SelectedIndexChanged(object sender, EventArgs e)
        {
            int StudioID;
            int.TryParse(ddlStudio.SelectedValue, out StudioID);
            if (StudioID > 0)
                Response.Redirect("~/studio-finder/?sid=" + ddlStudio.SelectedValue);
                //Response.Redirect("/StudioFinder.aspx?sid=" + ddlStudio.SelectedValue);
        }
    }
}