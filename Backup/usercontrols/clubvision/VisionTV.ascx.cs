using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Web.UI.HtmlControls;
using System.Configuration;
using BrightcoveAPI;
using System.Collections;

namespace VisionPersonalTrainingProject.usercontrols.clubvision
{
    
    public partial class VisionTV : System.Web.UI.UserControl
    {

        protected string BCVideoID = "";
        protected ArrayList tabArray;

        protected void Page_Load(object sender, EventArgs e)
        {
            storeTabsIntoTabArray();

            if (!Page.IsPostBack)
            {
                BCVideoID = Request.QueryString["vid"];
                if (BCVideoID == null)
                {
                    BCVideoID = "";
                    GetVideosMostRecent(true);
                }
                else
                    GetVideosMostRecent(false);

                if (Request.QueryString["tab"] == "sos" )
                {
                    SelectedTabSet(btnLifestyle);
                    GetVideosByTag("sos");
                }
                else
                {
                    SelectedTabSet(btnRecent);
                }
                
            }
        }

        /// <summary>
        /// stores the tab buttons into the tabArray variable so that it is easier to loop through the buttons for css changes.
        /// </summary>
        private void storeTabsIntoTabArray()
        {

            tabArray = new ArrayList();
            tabArray = new ArrayList();
            tabArray.Add(btnRecent);
            tabArray.Add(btnRecipes);
            tabArray.Add(btnEducation);
            tabArray.Add(btnExercises);
            tabArray.Add(btnSuccessStories);
            tabArray.Add(btnLifestyle);
        }

        /// <summary>
        /// Returns all videos with the given tag
        /// </summary>
        /// <param name="Tag"></param>
        private void GetVideosByTag(string Tag)
        {
            string condition = "";
            switch ((string)Session["MemberType"])
            {
                case "VVT":
                    condition = "&all=tag:" + Tag + "&none=tag:internal"; break;
                case "VPT":
                    condition = "&all=tag:" + Tag + "&none=tag:external"; break;
            }
            BCItemCollection VideoList = BrightCoveRequest(condition);
            AddAllVideosToScroll(VideoList, false);
        }

        private void GetVideosByMultipleTags(string Tag)
        {
            string condition = "";
            switch ((string)Session["MemberType"])
            {
                case "VVT":
                    condition = "&any=tag:" + Tag + "&none=tag:internal"; break;
                case "VPT":
                    condition = "&any=tag:" + Tag + "&none=tag:external"; break;
            }
            BCItemCollection VideoList = BrightCoveRequest(condition);
            AddAllVideosToScroll(VideoList, false);
        }


        /// <summary>
        /// Returns all videos with the given tag
        /// </summary>
        /// <param name="Tag"></param>
        private void SearchVideos(string SearchTerm)
        {
            string condition = "";
            switch ((string) Session["MemberType"])
            {
                case "VVT":
                    condition = "&any=" + SearchTerm + "&any=tag:" + SearchTerm + "&none=tag:internal";break;
                case "VPT":
                    condition = "&any=" + SearchTerm + "&any=tag:" + SearchTerm + "&none=tag:external";break;
            }
            BCItemCollection VideoList = BrightCoveRequest(condition);
            AddAllVideosToScroll(VideoList, false);
        }

        private void GetVideosMostRecent(bool LoadFirstIntoPlayer)
        {
            string condition0 = "";
            switch ((string)Session["MemberType"])
            {
                case "VVT":
                    condition0 = "&none=tag:internal"; break;
                case "VPT":
                    condition0 = "&none=tag:external"; break;
            }
            BCItemCollection VideoList = BrightCoveRequest("&sort_by=CREATION_DATE:DESC" + condition0);

            if (LoadFirstIntoPlayer)
            {
                string condition = "";
                switch ((string)Session["MemberType"])
                {
                    case "VVT":
                        condition = "&any=tag:featured" + "&none=tag:internal"; break;
                    case "VPT":
                        condition = "&any=tag:featured" + "&none=tag:external"; break;
                }

                BCItemCollection VideoList2 = BrightCoveRequest(condition);
                if (VideoList2.Items.Count > 0)
                {
                    BCVideoID = VideoList2.Items[0].ID.ToString();
                }
                else
                {
                    BCVideoID = VideoList.Items[0].ID.ToString();
                }
            }

            AddAllVideosToScroll(VideoList, true);
        }

        private void AddAllVideosToScroll(BCItemCollection VideoList, bool isRecentList)
        {
            //clear all of the current spans containing the thumbnails
            viewerVisionTV.Controls.Clear();

            //register a new client startup script block to re-attach events on scroller.
            //do not use Page.ClientScript.RegisterStartupScript, instead use ScriptManager.RegisterStartupScript
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), "$(document).ready(function () { attachScrollerEvents(); });", true);

            int MaxVideosInSpan = 8; //choose an even number for this
            HtmlGenericControl videoSpan = new HtmlGenericControl("span");

            for (int currentGroupIndex = 0; currentGroupIndex < VideoList.Items.Count; currentGroupIndex += MaxVideosInSpan/2)
            {
                if (currentGroupIndex % MaxVideosInSpan == 0)
                {
                    videoSpan = new HtmlGenericControl("span");
                    viewerVisionTV.Controls.Add(videoSpan);
                }
                else if (currentGroupIndex % (MaxVideosInSpan/2) == 0)
                {
                    //add divider
                    videoSpan.Controls.Add(CreateDividerControl());
                }

                for (int offsetInRow = 0; ((currentGroupIndex + offsetInRow) < VideoList.Items.Count) && (offsetInRow < (MaxVideosInSpan / 2)); offsetInRow++)
                {
                    BCVideo Video = VideoList.Items[currentGroupIndex + offsetInRow];
                    //add video
                    videoSpan.Controls.Add(VideoAddtoScroll(Video));

                    if (Video.ID.ToString() == BCVideoID)
                    {
                        lbFeaturedVideoTitle.Text = Video.VideoName;
                        lbFeaturedVideoDesc.Text = Video.LongDescription;
                        if (lbFeaturedVideoDesc.Text == "")
                        {
                            lbFeaturedVideoDesc.Text = Video.ShortDescription;
                        }
                        if (isRecentList)
                        {
                            lnkRelated.HRef = Video.LinkURL;
                            lnkRelated.InnerText = Video.LinkText;
                            lnkRelated.Target = "_blank";
                        }
                        ClubVisionDataContext cvdc = new ClubVisionDataContext();

                        var meals = (from m in cvdc.Meals
                                     where m.VideoId == Video.ID
                                     select m);

                        if (meals.Count() > 0)
                        {
                            lnkFoodDiary.Visible = true;
                            lnkFoodDiary.HRef = "/club-vision/my-eating/menus/?tab=view_meal&mealId=" + meals.First().Id;
                            lnkFoodDiary.InnerText = "Click here to view in Vision Meals.";
                        }
                        else
                        {
                            lnkFoodDiary.Visible = true;
                            lnkFoodDiary.HRef = "#";
                            lnkFoodDiary.InnerText = "";
                        }
                    }

                }

            }

        }

        private HtmlGenericControl CreateDividerControl()
        {
            HtmlGenericControl divClear = new HtmlGenericControl("div");
            divClear.Attributes["class"] = "clear graydivider";
            divClear.InnerText = " ";
            return divClear;
        }

        private BCItemCollection BrightCoveRequest(string Request)
        {
            string Token = ConfigurationManager.AppSettings["brightcoveToken"].ToString();
            string reqUrl = "http://api.brightcove.com/services/library?command=search_videos&page_size=100&token=" + Token + Request + "&sort_by=CREATION_DATE:DESC";
            WebRequest webRequest = WebRequest.Create(reqUrl) as HttpWebRequest;
            HttpWebResponse response = webRequest.GetResponse() as HttpWebResponse;
            DataContractJsonSerializer JsonReader = new DataContractJsonSerializer(typeof(BCItemCollection));
            return (BCItemCollection)JsonReader.ReadObject(response.GetResponseStream());
        }

        private HtmlGenericControl VideoAddtoScroll(BCVideo Video)
        {
            ClubVisionDataContext cvdc = new ClubVisionDataContext();

            var meals = (from m in cvdc.Meals
                         where m.VideoId == Video.ID
                         select m);

            int foodDiaryLnk = 0;

            if (meals.Count() > 0)
            {
                foodDiaryLnk = meals.First().Id;
            }


            HtmlGenericControl videoDiv = new HtmlGenericControl("div");
            videoDiv.Attributes["class"] = "eImgCell";
            videoDiv.InnerHtml = string.Format("<div class=\"eImgBox\"><div class=\"PlayVideoOverlay\" onclick=\"ShowVideo({1},'{2}','{3}','{4}','{5}',{6});\" title=\"play {2}\"></div><img src=\"{0}\" onclick=\"ShowVideo({1},'{2}','{3}','{4}','{5}',{6});\" title=\"play {2}\" alt=\"{2} video\" /></div><h5><a onclick=\"ShowVideo({1},'{2}','{3}','{4}','{5}',{6});\" title=\"play {2}\">{2}</a></h5>", Video.VideoStill, Video.ID, Video.VideoName, (Video.LongDescription == null ? "" : Video.LongDescription).Replace("\r\n", "<br>").Replace("\r", "<br>").Replace("\n", "<br>").Replace("'", "\\'"), Video.LinkURL, Video.LinkText, foodDiaryLnk);
            return videoDiv;
        }

        protected void btnRecipes_Click(object sender, EventArgs e)
        {
            GetVideosByTag("recipes");
            SelectedTabSet(btnRecipes);
        }
        protected void btnSuccessStories_Click(object sender, EventArgs e)
        {
            GetVideosByTag("success");
            SelectedTabSet(btnSuccessStories);
        }
        protected void btnEducation_Click(object sender, EventArgs e)
        {
            GetVideosByMultipleTags("education&any=tag:lifestyle");
            SelectedTabSet(btnEducation);
        }
        protected void btnLifestyle_Click(object sender, EventArgs e)
        {
            GetVideosByTag("sos");
            SelectedTabSet(btnLifestyle);
        }
        protected void btnTutorials_Click(object sender, EventArgs e)
        {
            GetVideosByTag("tutorials");
            SelectedTabSet(btnTutorials);
        }
        protected void btnRecent_Click(object sender, EventArgs e)
        {
            GetVideosMostRecent(false);
            SelectedTabSet(btnRecent);

        }
        protected void btnExercises_Click(object sender, EventArgs e)
        {
            GetVideosByTag("exercises");
            SelectedTabSet(btnExercises);
        }

        protected void imgbtnSearch_Click(object sender, ImageClickEventArgs e)
        {
            SearchVideos(tbSearch.Text);
            SelectedTabSet(btnRecent);
        }

        private void SelectedTabSet(Button SelectedButton)
        {

            string tabActiveClassName = "tabOrangeActive";

            //first clear active css class for all tabs
            foreach (object x in tabArray)
            {
                Button tabButton = x as Button;
                if (tabButton != null)
                {
                    tabButton.CssClass = tabButton.CssClass.Replace(" " + tabActiveClassName, "").Replace(tabActiveClassName + " ", "");
                }
            }

            //if (pnlTabs != null && pnlTabs.Controls != null)
            //{
            //    foreach (Control c in pnlTabs.Controls)
            //    {
            //        Button tabButton = c as Button;
            //        if (tabButton != null)
            //        {
            //            tabButton.CssClass = tabButton.CssClass.Replace(" " + tabActiveClassName, "").Replace(tabActiveClassName + " ", "");
            //        }
            //    }
            //}

            //then set active css class for the selected SelectedButton tab
            if (SelectedButton.CssClass.IndexOf(" " + tabActiveClassName) < 0
                && SelectedButton.CssClass.IndexOf(tabActiveClassName + " ") < 0)
            {
                SelectedButton.CssClass = SelectedButton.CssClass + " " + tabActiveClassName;
            }


/*
            SelectedButton.BackColor = System.Drawing.Color.Aqua;
            //set other buttons back to non selected
            if (SelectedButton.Text!=btnRecipes.Text)
                btnRecipes.BackColor = System.Drawing.Color.Azure;
            if (SelectedButton.Text != btnRecent.Text)
                btnRecent.BackColor = System.Drawing.Color.Azure;
            if (SelectedButton.Text != btnSuccessStories.Text)
                btnSuccessStories.BackColor = System.Drawing.Color.Azure;
            if (SelectedButton.Text != btnExercises.Text)
                btnExercises.BackColor = System.Drawing.Color.Azure;
*/
        }

        
        
       
    }
}