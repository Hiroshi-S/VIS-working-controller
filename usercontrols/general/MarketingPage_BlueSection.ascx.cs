using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using umbraco.NodeFactory;
using umbraco.cms.businesslogic.media;
using umbraco.cms.businesslogic.web;

namespace VisionPersonalTrainingProject.usercontrols.general
{
    public partial class MarketingPage_BlueSection : System.Web.UI.UserControl
    {
        private static List<Node> listNode = new List<Node>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                LitRecipes.Text = GenerateRecipesGrid();
                LitArticles.Text = GenerateArticles();
                LitVideos.Text = GenerateRecipesGrid();
            }
        }

        public static List<Node> GetDescendantOrSelfNodeList(Node node, string nodeTypeAlias)
        {
            if (node.NodeTypeAlias == nodeTypeAlias)
                listNode.Add(node);

            foreach (Node childNode in node.Children)
            {
                GetDescendantOrSelfNodeList(childNode, nodeTypeAlias);
            }

            return listNode;
        }

        protected  string GenerateRecipesGrid()
        {
            ClubVisionDataContext cvdc = new ClubVisionDataContext();
            var mealsToDisplay = (from meal in cvdc.MealFromBrightCoves
                                  select meal).Take(9);
            string mealstr = "<div style=\"height:33.3%;width:100%;\">";
            int count = 0;
            int count2 = 0;
            foreach (var mealdis in mealsToDisplay)
            {
                mealstr += "<div style=\"width:33.3%;\"><img height=\"100%\" width=\"100%\"  src=\"/images/meals/" + mealdis.ImageUrl + "\" title=\"\" width=\"100%\"/></div>";
                
                if (count == 2 && count2 != 2)
                {
                    mealstr += "</div><div style=\"height:33.3%;width:100%;\">";
                    count = -1;
                    count2++;
                }

                count++;
            }
            mealstr += "</div>";

            return mealstr;
        }
        
        protected string GenerateArticles()
        {
            // 1234 would be root node id
            Node rootNode = new Node(20069);

            // we are passing root node so that it can search through nodes with alias as DiaryEventItems
            List<Node> fitnessAdviceItems = GetDescendantOrSelfNodeList(rootNode, "FitnessAdvise");
            string fastr = "<div style=\"height:33.3%;width:100%;\">";
            int count = 0;
            int count2 = 0;
            foreach (var fitnessAdviceItem in fitnessAdviceItems.Take(9))
            {
                fastr += "<div style=\"width:33.3%;\"><img height=\"100%\" width=\"100%\" src=\"" + GetImageUrlFromNodeId(Convert.ToInt32(fitnessAdviceItem.GetProperty("thumbnailImage").Value)) +
                         "\" alt=\"fitness picture\"/></div>";

                if (count == 2 && count2 != 2)
                {
                    fastr += "</div><div style=\"height:33.3%;width:100%;\">";
                    count = -1;
                    count2++;
                }

                count++;
            }
            fastr += "</div>";
            return fastr;
        }

        public string GetImageUrlFromNodeId(int mediaId)
        {
            Media mediaFile = new Media(mediaId);
            return mediaFile.getProperty("umbracoFile").Value.ToString();
        }

        protected string GenerateVideos()
        {
            return "";
        }


    }
}