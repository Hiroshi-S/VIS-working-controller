using System;
using System.Collections.Generic;
using System.Linq;
using uComponents.Core;
using umbraco.presentation.nodeFactory;

namespace VisionPersonalTrainingProject.usercontrols.studiopages
{
    public partial class StudioOwner : System.Web.UI.UserControl
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ((umbraco.UmbracoDefault)this.Page).ValidateRequest = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.CacheControl = "no-cache";

            if (!Page.IsPostBack)
            {
                Node studio = Node.GetCurrent();

                Literal1.Text = ShowFranchiseOwnerList(studio.Id);
            }
        }
        
        protected string ShowFranchiseOwnerList(int studioNodeId)
        {
            string ret = "";
            int count = 0;

            var node = new Node(studioNodeId);

            foreach (Node childNode in node.Children)
            {
                // Document document = new Document(childNode.Id);
                // string alias = document.ContentType.Alias;
                if (childNode.NodeTypeAlias == "FranchiseOwner")
                {
                    count++;
                    string firstdiv = "<div class=\"col-md-3 col-sm-3 hidden-xs\"><img class=\"img-thumbnail\" style=\"margin-top:15px;\" src=\"/images/franchise_owner/" + childNode.GetProperty("photo").Value + "\"/></div>";
                    string seconddiv = "<div class=\"col-md-9 col-sm-9\"><h4>" + childNode.GetProperty("fullName").Value + "</h4><p>" + childNode.GetProperty("blurb").Value + "</p></div>";

                    ret += "<div class='row franchiseOwnerList'><div class='col-md-12'>" +
                        //"</div></div>";
                            "<div class=\"row\">" +
                            (count % 2 == 0 ? firstdiv + seconddiv : seconddiv + firstdiv) +
                            "</div></div></div>";
                    
                }
            }

            if(count > 0)
            {
                if(count == 1)
                {
                    ret = "<div class=\"row studioProfile\"><div class=\"col-md-12\"><h3>Meet The Owner</h3>" +
                        ret + "</div></div>"; 
                }
                else
                {
                    ret = "<div class=\"row studioProfile\"><div class=\"col-md-12\"><h3>Meet The Owners</h3>" +
                        ret + "</div></div>"; 
                }
            }

            return ret;
        }

    }
}