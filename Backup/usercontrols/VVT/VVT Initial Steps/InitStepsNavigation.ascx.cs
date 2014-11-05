using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VisionPersonalTrainingProject.usercontrols.VVT.VVT_Initial_Steps
{
    public partial class InitStepsNavigation : System.Web.UI.UserControl
    {
        private int? _lastSkip = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                Dictionary<string, string> initLink = new Dictionary<string, string>();
                ClubVisionDataContext cvdc = new ClubVisionDataContext();

                cvdc.SkipInitialStep((int)Session["MemberNo"], ref _lastSkip);
                int count = 1;
                initLink = GetInitCompleteLink();
                LiteralBreadCrumb.Text = "<div class=\"breadcrumb flat\">";

                foreach (KeyValuePair<string, string> keyValuePair in initLink)
                {
                    LiteralBreadCrumb.Text += "<a id=\"flat" + count + "\" href=\"" + keyValuePair.Value + "\">" + keyValuePair.Key + "</a>";
                    count++;
                }

                LiteralBreadCrumb.Text += "</div><style>#breadcrumbNav{display: none;}</style>";

                if(_lastSkip != null && (int)_lastSkip > 6 && Session["MemberType"].Equals("VVT"))
                {
                    LiteralBreadCrumb.Text = "<script>$( document ).ready(function(){activateBreadcrumb();" +
                                             "$('.istepsWrapper > div').hide();" +
                                             "$('#nextDiv').show();$('#nextDiv').css('border', 'none');" +
                                             "$('#nextDiv').children().first().html('<img src=\"/images/icons/web/save.png\" />');" +
                                             "$('#nextDiv').children().last().text('Save');});";

                   /* 
                    string path = HttpContext.Current.Request.Url.AbsolutePath;
                    string extraCallingBreadcrumb = "";

                    switch (path)
                    {
                        case "/club-vision/account-setup/life-style-screen/":
                            {
                                extraCallingBreadcrumb = "alert('nananaaa');activateBreadcrumb();";
                            }break;
                        case "/club-vision/account-setup/personal-profile/":
                            {
                                extraCallingBreadcrumb = "alert('nananaaa');activateBreadcrumb();";
                            }break;
                        case "/club-vision/account-setup/body-type/":
                            {
                                extraCallingBreadcrumb = "alert('nananaaa');activateBreadcrumb();";
                            }break;
                    }
                    */
                    LiteralBreadCrumb.Text += "</script>";

                }


            }
        }

        protected Dictionary<string, string> GetInitCompleteLink()
        {
            var result = new Dictionary<string, string>(7);
            List<string> keys = GetTitleLink();
            List<string> values = GetHrefs();
            int lastOne = (int) _lastSkip - 1;

            for (int i = 0; i < 7; i++)
            {
                if (i > lastOne && lastOne >= 0 && lastOne < 5)
                {
                    result.Add(keys[i], "javascript:initStepsNoAccessAlert('" + values[lastOne] + "')");
                }
                else
                {
                    result.Add(keys[i], values[i]);
                }
            }
            return result;
        }

        public static List<string> GetHrefs()
        {
            var result = new List<string>
                             {
                                 "/club-vision/account-setup/personal-profile/",
                                 "/club-vision/account-setup/life-style-screen/",
                                 "/club-vision/account-setup/body-type/",
                                 "/club-vision/account-setup/measurements/",
                                 "/club-vision/account-setup/my-goals/",
                                 "/club-vision/account-setup/exercise-planner/",
                                 "/club-vision/account-setup/eating-planner/"
                             };
            return result;
        }

        protected List<string> GetTitleLink()
        {
            var result = new List<string>
                             {
                                "Personal Profile",
                                 "Life Style",
                                 "Body Type",
                                 "Measurements",
                                 "Goals",
                                 "Exercise Planner",
                                 "Eating Planner"
                             };
            return result;
        }
    }
}