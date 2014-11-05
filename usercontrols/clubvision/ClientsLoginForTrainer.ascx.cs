using System;
using System.Data;
using System.Web.UI.WebControls;

namespace VisionPersonalTrainingProject.usercontrols.clubvision
{
    public partial class ClientsLoginForTrainer : System.Web.UI.UserControl
    {
        private DataTable clientsCV;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack && Session["MemberType"].Equals("VPT"))
            {
                 VisionPersonalTrainingProject.VOSWebService.Service service = new VisionPersonalTrainingProject.VOSWebService.Service();
                service.AuthenticationHeaderValue = new VisionPersonalTrainingProject.VOSWebService.AuthenticationHeader();
                service.AuthenticationHeaderValue.UserName = "vosABC";
                service.AuthenticationHeaderValue.Password = "vosABCpass1";

                //test
                //string username = "dan";
                //string password = "Saints2012";
                //VisionPersonalTrainingProject.VOSWebService.MemberLogin member = service.GetMemberLogin(username, ref password);
                //clientsCV = service.GetUserMembers(member.TrainerID);
                //test
                clientsCV = service.GetUserMembers((int)Session["TrainerId"]);

                GridView1.DataSource = clientsCV;
                GridView1.DataBind();
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
            string expression = "Member like '%" + TextBox1.Text + "%'";
            DataView dataView = clientsCV.DefaultView;
            dataView.RowFilter = expression;
           
            GridView1.DataSource = dataView;
            GridView1.DataBind();
        }
    }
}