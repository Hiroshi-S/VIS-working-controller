using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using uComponents.Core;
using umbraco.presentation.nodeFactory;

namespace VisionPersonalTrainingProject.usercontrols.studiopages
{
    public partial class StudioSuccessStories : System.Web.UI.UserControl
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
                Literal1.Text = studio.NodeTypeAlias == "NationalSuccessStories" ?
                                                    ShowNationalSuccessStoriesList(FindStudioTestis("0")) :

                                                    ShowSuccessStoriesList(FindStudioTestis(studio.GetProperty("studioId").Value)) +
                                                    "<div class=\"row studioTestimonial\"><div class=\"col-md-12\"><h3>More Testimonials From The Vision Network</h3>" +
                                                    ShowSuccessStoriesList(FindStudioTestis("0")) + "</div></div>";
                
            }
        }

        protected string ShowSuccessStoriesList(List<Node> studTestis)
        {
            try
            {
                string ret = "";
                string studioId = "";
                string studioName = "";


                foreach (Node childNode in studTestis)
                {
                    if (studioId == "")
                    {
                        studioId = childNode.GetProperty("studioId").Value;
                    }
                    if (studioName == "")
                    {
                        studioName = "Studio";// childNode.GetProperty("Title").Value.Replace("Personal Training ", "");
                    }
                    // Document document = new Document(childNode.Id);
                    // string alias = document.ContentType.Alias;
                    if (childNode.NodeTypeAlias == "StudioSuccessStories")
                    {
                        ret += "<div class='row successStoriesDisplayList'><div class='col-md-12'>" +
                            //"</div></div>";
                                "<div class=\"row\">" +
                                "<div class=\"col-md-5 col-sm-5\"><div class=\"row no-gutter\">" +
                                            "<div class=\"col-md-6 col-sm-6 col-xs-6\"><img class=\"img-thumbnail\" src=\"/images/success_stories/" + childNode.GetProperty("imageUrlBefore").Value + "\"/></div>" +
                                            "<div class=\"col-md-6 col-sm-6 col-xs-6\"><img class=\"img-thumbnail\" src=\"/images/success_stories/" + childNode.GetProperty("imageUrlAfter").Value + "\"/></div></div>" +
                                            (childNode.GetProperty("videoUrl").Value == "" ? "" : "<div class=\"row\"><div class=\"col-md-12 col-sm-12 col-xs-12\">" +
                                                                                                  "<div class=\"video-container\"><iframe style=\"display:block;\" width=\"283\" height=\"200\" src=\"" + childNode.GetProperty("videoUrl").Value + "&wmode=transparent&showinfo=0&autohide=1\" wmode=\"Opaque\" frameborder=\"0\" allowfullscreen></iframe></div>" +
                                                                                                  "</div></div>") +
                                            "</div>" +
                                "<div class=\"col-md-7 col-sm-7\"><h2><a href=\"" + childNode.NiceUrl + "\">" + childNode.GetProperty("name").Value + "</a></h2><h2 class=\"featurette-heading testimonial studiopage\">" + childNode.GetProperty("result").Value + "</h2>" +
                                            "<p><a href=\"" + childNode.NiceUrl + "\">read full article ></a></p></div>" +
                                "</div></div></div>";

                    }
                }
                //Node studio = Node.GetCurrent();
                //studioName = studio.GetProperty("title").Value.Replace("Personal Training ", "");
                using (ClubVisionDataContext cvdc = new ClubVisionDataContext())
                {
                    studioName = (from s in cvdc.EnumTables
                                 where s.ID == 11
                                 && s.intValue.ToString() == studioId
                                 select s.Value).FirstOrDefault();
                }
                if (ret.Length > 0 && studioId != "0")
                {
                    ret = "<div class=\"row studioTestimonial\"><div class=\"col-md-12\"><h3>Vision " + studioName + " Testimonials</h3>" +
                       ret + "</div></div>";
                }

                return ret;
            
            }catch(Exception ex)
            {
                return ex.ToString();
            }
        }

        protected string ShowNationalSuccessStoriesList(List<Node> studTestis)
        {
            try
            {
                string ret = "";

                foreach (Node childNode in studTestis)
                {
                    // Document document = new Document(childNode.Id);
                    // string alias = document.ContentType.Alias;
                    if (childNode.NodeTypeAlias == "StudioSuccessStories")
                    {
                        ret += "<div class='row successStoriesDisplayList'><div class='col-md-12'>" +
                            //"</div></div>";
                                "<div class=\"row\">" +
                                "<div class=\"col-md-4 col-sm-4\"><div class=\"row no-gutter\">" +
                                            "<div class=\"col-md-6 col-sm-6 col-xs-6\"><img class=\"img-thumbnail\" src=\"/images/success_stories/" + childNode.GetProperty("imageUrlBefore").Value + "\"/></div>" +
                                            "<div class=\"col-md-6 col-sm-6 col-xs-6\"><img class=\"img-thumbnail\" src=\"/images/success_stories/" + childNode.GetProperty("imageUrlAfter").Value + "\"/></div></div>" +
                                            (childNode.GetProperty("videoUrl").Value == "" ? "" : "<div class=\"row\"><div class=\"col-md-12 col-sm-12 col-xs-12\">" +
                                                                                                  "<div class=\"video-container\"><iframe style=\"display:block;\" width=\"283\" height=\"200\" src=\"" + childNode.GetProperty("videoUrl").Value + "&wmode=transparent&showinfo=0&autohide=1\" wmode=\"Opaque\" frameborder=\"0\" allowfullscreen></iframe></div>" +
                                                                                                  "</div></div>") +
                                            "</div>" +
                                "<div class=\"col-md-8 col-sm-8\"><h2><a href=\"" + childNode.NiceUrl + "\">" + childNode.GetProperty("name").Value + "</a></h2><h2 class=\"featurette-heading testimonial\">" + childNode.GetProperty("result").Value + "</h2>" +
                                            "<p><a href=\"" + childNode.NiceUrl + "\">read full article ></a></p></div>" +
                                "</div></div></div>";

                    }
                }

                if (ret.Length > 0)
                {
                    ret = "<div class=\"row studioTestimonial\"><div class=\"col-md-12\">" +
                       ret + "</div></div>";
                }

                return ret;


            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public List<Node> FindStudioTestis(string studioId)
        {
            return uQuery.GetNodesByType("StudioSuccessStories").Where(x => x.GetProperty("studioId").Value == studioId).ToList();
        }

    }
}