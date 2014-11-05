using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using umbraco.NodeFactory;
using umbraco.cms.businesslogic.media;


namespace VisionPersonalTrainingProject.usercontrols.general
{
    /*
     * [1] Dewi 20/01/2014 - Add code for taking specific meal picture
     */
    public partial class MetaTagGenerator : System.Web.UI.UserControl
    {
        private static readonly List<Node> ListNode = new List<Node>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Page.IsPostBack) return;
            if (Request.QueryString["code"] != null) //share plan
            {
                MetaTagLiteral.Text = GenerateMetaTags("food");
            }
            
            if(Request.QueryString["articleid"] != null) //article
            {
                MetaTagLiteral.Text = GenerateMetaTags("community");
            }
        }

        protected string GenerateMetaTags(string pageType)
        {
            string tags = "";
            switch (pageType)
            {
                case "food":
                    tags = GenerateMetaTagsFoodShare();
                    break;
                case "community":
                    tags = GenerateMetaTagsFitnessAdvisor();
                    break;
            }

            return tags;
        }

        protected string GenerateMetaTagsFitnessAdvisor()
        {
            try
            {
                Node articleNode = new Node(Convert.ToInt32(Request.QueryString["articleid"]));

                string desc = articleNode.GetProperty("bodyText").Value.Substring(0, 500);
                desc = desc.Replace("<p>", "");
                desc = desc.Replace("</p>", "");
                desc = desc.Replace("<br />", "");
                desc = desc.Replace("<strong>", "");
                desc = desc.Replace("</strong>", "");
                desc = desc.Replace("<ul>", ""); 
                desc = desc.Replace("</ul>", "");
                desc = desc.Replace("<li>", "");
                desc = desc.Replace("</li>", "");
                desc = desc.Replace("\"", "");


                string tagsToAdd = GenerateMetaTag("og:title", articleNode.GetProperty("contentTitle").Value) + "\n"
                          + GenerateMetaTag("og:description", desc + "\n") 
                          + GenerateMetaTag("og:image", "http://www.visionpt.com.au" + GetImageUrlFromNodeId(Convert.ToInt32(articleNode.GetProperty("thumbnailImage").Value)));

                return tagsToAdd;
            }catch(Exception ex)
            {
                return "<meta property=\"og:type\" content=\"articleid\"/>";
            }
        }

        protected string GenerateMetaTagsFoodShare()
        {
            string title = "";
            string description = "";
            string tagsToAdd = "";
            string sharetype = Request.QueryString["code"].Substring(0, 2);
            string imageUrl = "http://www.visionpt.com.au/media/219378/serious2.jpg";

            switch (sharetype)
            {
                case "dp":
                    {
                        title = "Check out my new food diary";
                        description = "I have created a new food diary that I want to share with you.";
                    }
                    break;
                case "mn":
                    {
                        title = "Check out my new daily plan";
                        description = "I have created a new daily plan that I want to share with you.";
                    }
                    break;
                case "ml":
                    {
                        title = "Check out my new meal";
                        description = "I have created a new meal that I want to share with you";

                        ClubVisionDataContext cvdc = new ClubVisionDataContext();

                        var mealsing = (from mnid in cvdc.ShareDetails
                                               where mnid.cCode == Request.QueryString["code"]
                                               select mnid.iMealId).SingleOrDefault();
                        if (mealsing != null)
                        {
                            int mealid = mealsing.Value;

                            var mldet = (from mldt in cvdc.Meals
                                            where mldt.Id == mealid
                                            select mldt).SingleOrDefault();
                            description = mldet.Name;
                            if(mldet.ImageUrl != null)
                            {
                                imageUrl = "http://www.visionpt.com.au/images/meals/" + mldet.ImageUrl;
                            }

                            if(mldet.cDescription != null)
                            {
                                title = mldet.Name;
                                description = mldet.cDescription;
                            }
                        }
                    }
                    break;
            }
            tagsToAdd = GenerateMetaTag("og:title", title) + "\n"
                      + GenerateMetaTag("og:description", description) + "\n"
                      + GenerateMetaTag("og:image", imageUrl);

            return tagsToAdd;
        }

        protected string GenerateMetaTag(string tagName, string content)
        {
            return "<meta property=\"" + tagName + "\" content=\"" + content + "\" />";
        }

        public static List<Node> GetDescendantOrSelfNodeList(Node node, string nodeTypeAlias)
        {
            if (node.NodeTypeAlias == nodeTypeAlias)
                ListNode.Add(node);

            foreach (Node childNode in node.Children)
            {
                GetDescendantOrSelfNodeList(childNode, nodeTypeAlias);
            }

            return ListNode;
        }

        public string GetImageUrlFromNodeId(int mediaId)
        {
            Media mediaFile = new Media(mediaId);
            return mediaFile.getProperty("umbracoFile").Value.ToString();
        }
       
    }
}