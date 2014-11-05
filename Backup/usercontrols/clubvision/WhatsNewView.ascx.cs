using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Configuration;

using VisionPersonalTrainingProject;
using BrightcoveAPI;


namespace VisionPersonalTrainingProject.usercontrols.clubvision
{
    public partial class WhatsNewView : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
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

            if (!this.IsPostBack)
            {
                if (VideoList != null && VideoList.Items != null)
                {
                    for (int count = 0; count < 3; count++)
                    {
                        BCVideo Video = VideoList.Items[count];

                        literalWhatsNew.Text += "<div class=\"eImgBoxWhatsNew\"><img src=\"" + Video.Thumbnail + "\" width=\"72\" height=\"54\" alt=\"" + Video.VideoName + "\" border=\"0\"></div>";
                        literalWhatsNew.Text += "<h5><a href=\"/club-vision/education/vision-tv?vid=" + Video.ID + "\">" + Video.VideoName + "</a></h5>";
                        literalWhatsNew.Text += "<div class=\"eWhatsOnDivider\">&nbsp;</div>";
                    }
                }
            }
        }

        private BCItemCollection BrightCoveRequest(string Request)
        {
            string Token = ConfigurationManager.AppSettings["brightcoveToken"].ToString();
            string reqUrl = "http://api.brightcove.com/services/library?command=search_videos&page_size=100&token=" + Token + Request;

            try
            {
                WebRequest webRequest = WebRequest.Create(reqUrl) as HttpWebRequest;
                HttpWebResponse response = webRequest.GetResponse() as HttpWebResponse;
                DataContractJsonSerializer JsonReader = new DataContractJsonSerializer(typeof(BCItemCollection));
                return (BCItemCollection)JsonReader.ReadObject(response.GetResponseStream());
            }
            catch (Exception)
            {
                //the web request to Brightcove may fail, and the code here needs to deal with it gracefully.
            }

            return new BCItemCollection();
        }
    }
}