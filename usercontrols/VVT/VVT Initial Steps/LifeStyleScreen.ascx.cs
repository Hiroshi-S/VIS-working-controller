using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VisionPersonalTrainingProject.usercontrols.VVT.VVT_Initial_Steps
{
    public partial class LifeStyleScreen : System.Web.UI.UserControl
    {
        private bool[] myArray = new bool[7];

        private int _init;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    PopulateScreen();
                }
                catch (Exception exception)
                {
                    Response.Write(exception.ToString());
                }

            }
        }

        protected void PopulateArray()
        {
            _init = 0;

            for (int i = 1; i <= 14; i++)
            {
                RadioButton myButton = (RadioButton)this.FindControl("RadioButton" + i) as RadioButton;

                if (myButton.Checked)
                {
                    myArray[_init] = bool.Parse(myButton.Text);

                    _init++;
                }
            }
        }

        protected void SaveAndNextImageButtonClick(object sender, ImageClickEventArgs e)
        {
            try
            {
                PopulateArray();

                if (_init == 7 && CheckBox1.Checked)
                {
                    var cvdc = new ClubVisionDataContext();

                    var formcheck = (from fc in cvdc.FormResults
                                     where fc.iFormID == 1
                                     where fc.iCustomerID == Convert.ToInt32(Session["MemberNo"])
                                     select fc);

                    if (formcheck.Any())
                    {
                        cvdc.FormResults.DeleteAllOnSubmit(formcheck);
                        cvdc.SubmitChanges();
                    }

                    var frsList = new List<FormResult>();


                    for (int i = 0; i < 7; i++)
                    {
                        var frs = new FormResult
                        {
                            iFormID = 1,
                            iCustomerID = Convert.ToInt32(Session["MemberNo"]),
                            iQuestionID = i + 1,
                            bResult = myArray[i],
                            dDateCaptured = DateTime.Now
                        };
                        frsList.Add(frs);
                    }

                    cvdc.FormResults.InsertAllOnSubmit(frsList);

                    cvdc.SubmitChanges();

                    if ((string)Session["MemberType"] == "VVT")
                    {
                        var customer = (from cust in cvdc.Customer_Externals
                                        where cust.iID == Convert.ToInt32(Session["MemberNo"])
                                        select cust).FirstOrDefault();
                        if (!customer.bCompleteInitialState)
                        {
                            Response.Redirect("/club-vision/account-setup/body-type/", false);
                        }
                        else if (customer.bCompleteInitialState)
                            Response.Redirect("/club-vision/account-setup/life-style-screen/", false);
                    }
                }
                else
                {
                    ErrorPopup();
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected void BackButtonClick(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("/club-vision/account-setup/personal-profile/", false);
        }

        protected void PopulateScreen()
        {
            var cvdc = new ClubVisionDataContext();
            if ((string)Session["MemberType"] == "VVT")
            {
                var chk = (from ce in cvdc.Customer_Externals
                           where ce.iID == Convert.ToInt32(Session["MemberNo"])
                           select ce).FirstOrDefault();
                if (chk.bCompleteInitialState)
                {
                    //stepLabel.Visible = false;
                    backButton.Visible = false;
                    saveAndNextButton.ImageUrl = "/images/buttonSave.gif";
                }
            }
            var formQuestions = (from qs in cvdc.FormQuestions
                                 where qs.iFormID == 1
                                 select qs.cQuestion);

            int num = 1;

            foreach (var formQuestion in formQuestions)
            {
                Label myLabel = this.FindControl("Label" + num.ToString()) as Label; // this is your Page class

                if (myLabel != null)
                {
                    myLabel.Text = formQuestion + "\t";
                }
                num++;
            }

            var formQuestionsResult = (from fqrs in cvdc.FormResults
                                       where fqrs.iFormID == 1
                                       where fqrs.iCustomerID == Convert.ToInt32(Session["MemberNo"])
                                       select fqrs);

            foreach (var formResult in formQuestionsResult)
            {
                int whichbutton;
                if (formResult.bResult == true)
                {
                    whichbutton = (formResult.iQuestionID * 2) - 1;
                }
                else
                {
                    whichbutton = formResult.iQuestionID * 2;
                }

                RadioButton myButton = this.FindControl("RadioButton" + whichbutton.ToString()) as RadioButton;
                if (myButton != null) myButton.Checked = true;
            }

            if (formQuestionsResult.Any())
            {
                CheckBox1.Checked = true;
            }
        }

        protected void ErrorPopup()
        {
            const string s = "<script type=\"text/javascript\">" +
                                 "document.getElementById('navContainer').style.display = 'none';document.getElementById('title').innerHTML = \"Please Note\";" +
                                 "document.getElementById('h4words').innerHTML = \"All sections of the Lifestyle Screen must be completed before moving to the next step\";" +
                                 "$(\".contactBox\").css(\"margin-top\", ((($(window).height() - 464) / 2) + $(window).scrollTop() + 0) + \"px\");" +
                                 "$(\"#cErrorPopup\").fadeIn();</script>";

            Page.ClientScript.RegisterStartupScript(GetType(), "test", s);
        }

        protected void GoBackToMyScreen()
        {
            const string s = "<script cardioType=\"text/javascript\">" +
                             "document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyDetailsProfileTab_4_profileTab').style.backgroundImage = 'url(/images/ExtClubVision/eHdrProfileTabLifestyleScreen.gif)';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyDetailsProfileTab_4_eMyDetails').style.display = 'none';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyDetailsProfileTab_4_eLifestyleScreen').style.display = 'block';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyDetailsProfileTab_4_eBodyType').style.display = 'none';</script>";

            Page.ClientScript.RegisterStartupScript(GetType(), "test", s);
        }

        /*
        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //args.IsValid = (CheckBox1.Checked && _init == 7);
            args.IsValid = true;
        }*/
    }
}