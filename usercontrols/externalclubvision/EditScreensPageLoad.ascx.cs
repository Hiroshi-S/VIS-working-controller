using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VisionPersonalTrainingProject.usercontrols.externalclubvision
{
    public partial class EditScreensPageLoad : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var cvdc = new ClubVisionDataContext();

            var custStatus = (from cs in cvdc.Customer_Externals
                              where cs.iID == Convert.ToInt32(Session["MemberNo"])
                              select cs).SingleOrDefault();

            if (custStatus != null)
            {
                if (!custStatus.bCompleteInitialState)
                {
                    if (Request.QueryString["initmessage"] != null && Request.QueryString["initmessage"].Equals("true"))
                    {
                        int memberid = Convert.ToInt32(Session["MemberNo"]);
                        int eventsDone = 0;
                        string ullist = "<ul>";


                        var pprofile = (from pp in cvdc.PersonalProfile_Externals
                                        where pp.iCustomerID == memberid
                                        select pp).SingleOrDefault();
                        if (pprofile == null)
                        {
                            ullist += "<li>STEP 1   My Details</li>";
                            eventsDone++;
                        }

                        var lsscreen = (from ls in cvdc.FormResults
                                        where ls.iCustomerID == memberid
                                        where ls.iFormID == 1
                                        select ls).FirstOrDefault();
                        if (lsscreen == null)
                        {
                            ullist += "<li>STEP 2   Lifestyle Screen</li>";
                            eventsDone++;
                        }

                        var lsscreen2 = (from ls2 in cvdc.FormResults
                                         where ls2.iCustomerID == memberid
                                         where ls2.iFormID == 2
                                         select ls2).FirstOrDefault();
                        if (lsscreen2 == null)
                        {
                            ullist += "<li>STEP 3   Body Type</li>";
                            eventsDone++;
                        }

                        var measurements = (from ms in cvdc.Measurements
                                            where ms.iCustomerID == memberid
                                            select ms).FirstOrDefault();
                        if (measurements == null)
                        {
                            ullist += "<li>STEP 4   My Measurements</li>";
                            eventsDone++;
                        }

                        var goals = (from gl in cvdc.Goals
                                     where gl.iCustomerID == memberid
                                     select gl).FirstOrDefault();
                        if (goals == null)
                        {
                            ullist += "<li>STEP 5   My Goals</li>";
                            eventsDone++;
                        }

                        var explanner = (from td in cvdc.TrainingDiaries
                                         where td.iCustomerId == memberid
                                         where td.bFromVOS == true
                                         where td.iExValue != 45
                                         select td).FirstOrDefault();
                        if (explanner == null)
                        {
                            ullist += "<li>STEP 6   My Exercise Planner</li><li>STEP 7   My Eating Planner</li>";
                            eventsDone++;
                        }

                        ullist += "</ul>";

                        if (eventsDone > 0)
                        {
                            DisplayInitMessage(ullist);
                        }

                    }
                    else
                    {
                        HideNaveBar();
                    }

                }

            }

        }

        public void DisplayInitMessage(string ullist = null)
        {
            string s = "<script type=\"text/javascript\">" +
                              "document.getElementById('navContainer').style.display = 'none';" + //ContentPlaceHolderDefault_help
                              "document.getElementById('ContentPlaceHolderDefault_help').style.display = 'none';" +
                              "document.getElementById('title').innerHTML = \"Welcome to Vision Virtual Training\";" +
                              "document.getElementById('h4words').innerHTML = \"To activate your personal profile and start using Vision Virtual Training <br /> " +
                              "you will need to follow the 7 step process to get started:  " + ullist +
                              "<br />There is a How to Get Started Guide located in the navigation panel which is displayed on the right side of the screen.<br />This will assist you complete these initial steps.\";" +
                              " $(\".contactBox\").css(\"margin-top\", ((($(window).height() - 464) / 2) + $(window).scrollTop() + 0) + \"px\");" +
                              "$(\"#cErrorPopup\").fadeIn();</script>";

            Page.ClientScript.RegisterStartupScript(GetType(), "test", s);

        }

        public void HideNaveBar()
        {//app
            string s = "<script type=\"text/javascript\">" +
                             "document.getElementById('navContainer').style.display = 'none';" +
                             "document.getElementById('ContentPlaceHolderDefault_help').style.display = 'none';" +
                             "</script>";

            Page.ClientScript.RegisterStartupScript(GetType(), "test", s);
        }
    }
}