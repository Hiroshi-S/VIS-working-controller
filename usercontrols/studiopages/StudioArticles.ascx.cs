using System;
using System.Collections.Generic;
using System.Linq;
using uComponents.Core;
using umbraco.presentation.nodeFactory;

namespace VisionPersonalTrainingProject.usercontrols.studiopages
{
    public partial class StudioArticles : System.Web.UI.UserControl
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ((umbraco.UmbracoDefault)this.Page).ValidateRequest = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.CacheControl = "no-cache";

            if(!Page.IsPostBack)
            {
                Node studio = Node.GetCurrent();
                List<Node> blogpostsNode = FindStudioArticleNode(studio.GetProperty("studioId").Value);

                Literal1.Text = DisplayStudioArticles(blogpostsNode);

                if(Literal1.Text.Length > 0)
                {
                    Literal1.Text = "<div class=\"row studioProfile blogPostList\"><div class=\"col-md-12\"><h3>Article Posts</h3>" +
                        Literal1.Text + "</div></div>";
                }

            }

            
        }

        /// <summary>
        /// Finds the right studio based on the querystring param
        /// </summary>
        /// <returns></returns>
        Node FindArticle()
        {
            Node blogpostnode = null;

            List<Node> nodes = uQuery.GetNodesByType("BlogPost");

            // Find the studio with the right studio id
            foreach (Node item in nodes)
            {
                //if (item.GetProperty("studioId").Value.Trim() == Request.QueryString.Get("sid"))
                {
                    blogpostnode = item;
                    // add node id to viewstate to use when saving our data
                    ViewState["nodeId"] = item.Id;
                }
            }

            return blogpostnode;
        }

        void ShowBlogPostDataInStudioPage(Node blogpostNode)
        {
            Literal1.Text =  "<h3>" + blogpostNode.Name + "</h3>" +
                             "<p>" + blogpostNode.GetProperty("bodyText").Value + "</p>";
        }
        
        Node FindStudio()
        {
            Node studioNode = null;

            List<Node> nodes = uQuery.GetNodesByType("StudioPage");

            // Find the studio with the right studio id
            foreach (Node item in nodes)
            {
                //item..GetProperty("title").Value = "ajdjd";//.Replace("Vision", "Vision Personal Training");

                if (item.GetProperty("studioId").Value.Trim() == Request.QueryString.Get("sid"))
                {
                    studioNode = item;
                    // add node id to viewstate to use when saving our data
                    ViewState["nodeId"] = item.Id;
                }
            }

            return studioNode;
        }

        public List<Node> FindStudioArticleNode(string studioId)
        {
            return uQuery.GetNodesByType("BlogPost").Where(x => x.GetProperty("studioId").Value == studioId).ToList();
              //  return uQuery.GetNodesByType("BlogPost");
        }

        public string DisplayStudioArticles(List<Node> studArticles)
        {
            string ret = "";
            
            foreach (Node studArticle in studArticles)
            {
                ret += "<div class=\"row\" onclick=\"window.open('" + studArticle.NiceUrl + "', '_self');\">" +
                    "<div class=\"col-md-2 col-sm-3 col-xs-5 imgdivArticle\"><img src=\"/images/studio_blog_thumb/" + studArticle.GetProperty("studioId").Value +
                       "/" + (studArticle.GetProperty("illustrationImage").Value == string.Empty ? "male_headshot.jpg" : studArticle.GetProperty("illustrationImage").Value) + "\"/></div>" +
                       "<div class=\"col-md-9 col-sm-10 col-xs-7\">" +
                            "<h4 class=\"entry-title\">" + studArticle.Name + "</h4>" +
                            "<p>" + studArticle.GetProperty("blurb").Value + "</p>" +
                            "<p class=\"category\">" + studArticle.GetProperty("category").Value + "</p>" +
                            "</div>" +
                       "</div>";
            }
             

            return ret;

        }

        
    }
}