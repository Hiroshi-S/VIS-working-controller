using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using uComponents.Core;
using umbraco;
using umbraco.cms.businesslogic.web;
using umbraco.presentation.nodeFactory;
using umbraco.BusinessLogic;

namespace VisionPersonalTrainingProject.usercontrols.studiopages
{
    public partial class StudioDetailOnStudioPost : System.Web.UI.UserControl
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
                Node currPage = Node.GetCurrent();
               
                Node studioNode = FindStudioOfCurentArticle(currPage.GetProperty("studioId").Value);

                if (studioNode != null)
                { 
                    Literal1.Text = "<div class=\"row joinstudio\" onclick=\"window.open('" + studioNode.NiceUrl + "vpt studio page.aspx','_self')\"><div class=\"col-md-12\">" +
                                    "<h2>Join " + studioNode.GetProperty("contentTitle").Value + "</h2>" +
                                    "</div></div>";

                    //show list of articles
                    StudioArticles srr = new StudioArticles();
                    List<Node> articlesNode = srr.FindStudioArticleNode(studioNode.GetProperty("studioId").Value);

                    StudioSuccessStories sss = new StudioSuccessStories();
                    List<Node> studTestisNode = sss.FindStudioTestis(studioNode.GetProperty("studioId").Value);
                    
                    string blogPostList = DisplayStudioArticles(articlesNode);

                    if(blogPostList.Length > 1)
                    {
                        Literal1.Text += "<div class=\"row titleMoreArticle\">" +
                                         "<div class=\"col-md-12\">" +
                                         "<h4>Articles</h4>" +
                                         "</div>" +
                                         "</div>";
                    }

                    Literal1.Text += blogPostList;

                    string studioTestiList = DisplayStudioTestimonial(studTestisNode);

                    if (studioTestiList.Length > 1)
                    {
                        Literal1.Text += "<div class=\"row titleMoreArticle\">" +
                                         "<div class=\"col-md-12\">" +
                                         "<h4>Testimonials</h4>" +
                                         "</div>" +
                                         "</div>";
                    }

                    Literal1.Text += studioTestiList;

                    //facebook box
                    Literal1.Text += "<div class=\"row titleMoreArticle\">" +
                                        "<div class=\"col-md-12\">" +
                                            "<h4>Connect With Us</h4>" + 
                                        "</div>" +
                                    "</div>" +
                                    "<div class=\"row\">" +
                                        "<div class=\"col-md-12\">" +
                                            "<div class=\"fb-like-box\" data-href=\"" + studioNode.GetProperty("facebookURL").Value + "\" data-width=\"292\" data-show-faces=\"true\" data-border-color=\"#fff\" data-stream=\"true\" data-show-border=\"false\" data-header=\"false\"></div>" +
                                        "</div>" +
                                    "</div>";


                    // Literal1.Text = Literal1.Text.Replace("<h4>", "<h5>").Replace("</h4>", "</h5>");

                }
                else
                {
                    Literal1.Text = "<div class=\"row\"><div class=\"col-md-12\">" +
                                    "<h2>NO STUDIO FOUND"  + "</h2>" +
                                    "</div></div>";
                }
            }
        }

        protected Node FindStudioOfCurentArticle(string studioId)
        {
            return uQuery.GetNodesByType("StudioPage").FirstOrDefault(x => x.GetProperty("studioId").Value.Trim() == studioId);
            //return uQuery.GetNodesByType("StudioPage").FirstOrDefault();
        }

        public string DisplayStudioArticles(List<Node> studArticles)
        {
            string ret = "";

            foreach (Node studArticle in studArticles)
            {
                ret += "<div class=\"row blogPostListOnStudioPage\" onclick=\"window.open('" + studArticle.NiceUrl + "','_self');\">" +
                    "<div class=\"col-md-2 col-sm-3 col-xs-5 imgdivArticle\"><img src=\"/images/studio_blog_thumb/" + studArticle.GetProperty("studioId").Value +
                       "/" + (studArticle.GetProperty("illustrationImage").Value == string.Empty ? "male_headshot.jpg" : studArticle.GetProperty("illustrationImage").Value) + "\"/></div>" +
                       "<div class=\"col-md-10 col-sm-10 col-xs-7\">" +
                            "<p class=\"entry-title\">" + studArticle.Name + "</p>" +
                            "<p class=\"category\">" + studArticle.GetProperty("category").Value + "</p>" +
                            "</div>" +
                       "</div>";
            }
            return ret;
        }

        public string DisplayStudioTestimonial(List<Node> studTestis)
        {
            string ret = "";

            foreach (Node studTesti in studTestis)
            {
                ret += "<div class=\"row blogPostListOnStudioPage\" onclick=\"window.open('" + studTesti.NiceUrl + "','_self');\">" +
                    "<div class=\"col-md-4 col-sm-3 col-xs-5\">" +
                       "<div class=\"BAPictureOnStudioTesti\">" +
                       "<img src=\"/images/success_stories/" + studTesti.GetProperty("imageUrlBefore").Value + "\" />" +
                       "<img src=\"/images/success_stories/" + studTesti.GetProperty("imageUrlAfter").Value + "\" />" +
                       "</div></div>" +
                       "<div class=\"col-md-8 col-sm-10 col-xs-7\">" +
                            "<p class=\"entry-title\">" + studTesti.Name + "</p>" +
                            "<p class=\"result\">" + studTesti.GetProperty("result").Value + "</p>" +
                            "</div>" +
                       "</div>";
            }
            return ret;
        }
    }
}