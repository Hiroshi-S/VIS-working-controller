using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using umbraco.NodeFactory;

namespace VisionPersonalTrainingProject.usercontrols.studiopages
{
    public partial class StudioPartnerVOS : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Node currentNode = Node.GetCurrent();

            String myValue = currentNode.GetProperty("studioId").Value;

            Session["studioID"] = Convert.ToInt32(myValue);

            VOSWebService.Service service = new VOSWebService.Service();
            service.AuthenticationHeaderValue = new VOSWebService.AuthenticationHeader();
            service.AuthenticationHeaderValue.UserName = "vosABC";
            service.AuthenticationHeaderValue.Password = "vosABCpass1";


            ListView1.DataSource = service.GetPartners(Convert.ToInt32(Session["studioID"]));
            ListView1.DataBind();
        }

        protected void ListView1_DataBound(object sender, EventArgs e)
        {
            foreach (ListViewDataItem item in ListView1.Items)
            {
                
                HiddenField h1 = (HiddenField) item.FindControl("HiddenField1");
                Literal lit1 = (Literal) item.FindControl("Literal1");

                if (h1.Value != "")
                {
                    if (!h1.Value.Substring(0, 7).Equals("http://"))
                    {
                        h1.Value = "http://" + h1.Value;
                    }
                    lit1.Text = " <a href=\"" + h1.Value + "\" target=\"_blank\">" + lit1.Text + "</a>";
                }
                
                if(ListView1.Items.IndexOf(item) < ListView1.Items.Count - 1)
                {
                    lit1.Text += ", ";
                }
                else
                {
                    lit1.Text.Remove(lit1.Text.Length-1);
                    lit1.Text += ". ";
                }

                
            }
        }
    }
}