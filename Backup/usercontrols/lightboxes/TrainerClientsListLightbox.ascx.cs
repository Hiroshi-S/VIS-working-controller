using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VisionPersonalTrainingProject.usercontrols.lightboxes
{
    public partial class TrainerClientsListLightbox : System.Web.UI.UserControl
    {
        private DataTable clientsCV;
        
        protected override void Render(HtmlTextWriter writer)
        {
            string validatorOverrideScripts = "<script src=\"/scripts/validators.js\" type=\"text/javascript\"></script>";
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ValidatorOverrideScripts", validatorOverrideScripts, false);
            base.Render(writer);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                if (Request.QueryString["showEmail"] == "yes")
                {
                    VisionPersonalTrainingProject.VOSWebService.Service service = new VisionPersonalTrainingProject.VOSWebService.Service();
                    service.AuthenticationHeaderValue = new VisionPersonalTrainingProject.VOSWebService.AuthenticationHeader();
                    service.AuthenticationHeaderValue.UserName = "vosABC";
                    service.AuthenticationHeaderValue.Password = "vosABCpass1";
                    int istrainer = (string)Session["Trainer"] == "Yes" ? 2 : 1;

                    clientsCV = service.GetUserMemberEmails((int)Session["MemberNo"], istrainer);
                    GridView1.DataSource = clientsCV;
                    GridView1.DataBind();
                }
            }

        }

        private string ConvertSortDirectionToSql(SortDirection sortDirection)
        {
            string newSortDirection = String.Empty;

            switch (sortDirection)
            {
                case SortDirection.Ascending:
                    newSortDirection = "ASC";
                    break;

                case SortDirection.Descending:
                    newSortDirection = "DESC";
                    break;
            }

            return newSortDirection;
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }

        protected void gridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dataTable = GridView1.DataSource as DataTable;

            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                dataView.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);

                GridView1.DataSource = dataView;
                GridView1.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string expression = "Name like '%" + TextBox1.Text + "%'";
            DataView dataView = clientsCV.DefaultView;
            dataView.RowFilter = expression;

            GridView1.DataSource = dataView;
            GridView1.DataBind();
        }

        protected void GridView1ItemCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "sendEmail":
                    {
                        int index = Convert.ToInt32(e.CommandArgument.ToString());
                        Label sentLabel = (Label)GridView1.Rows[index].FindControl("LabelSent");
                        /*
                        Literal ltrlName = (Literal)GridView1.Rows[index].FindControl("ltrlName");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), 
                        "Message", "alert('" + ltrlName.Text+ "');", true);
                        */

                        sentLabel.Visible = true;

                    }
                    break;
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               
                // similarly write for other controls
            }
        }  

    }
}