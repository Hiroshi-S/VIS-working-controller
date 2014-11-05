using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VisionPersonalTrainingProject.usercontrols.externalclubvision.initialscreens
{
    public partial class BodyType : System.Web.UI.UserControl
    {
        private double _bodyTypeScore;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                PopulateScreen();
            }
        }
        public void PopulateScreen()
        {
            //clean up the children question labels
            for (int i = 11; i < 21; i++)
            {
                Label myLabel = this.FindControl("BodyTypeLabel" + i.ToString()) as Label;
                myLabel.Text = "";
            }

            var cvdc = new ClubVisionDataContext();

            if ((string)Session["MemberType"] == "VVT")
            {
                var chk = (from ce in cvdc.Customer_Externals
                           where ce.iID == Convert.ToInt32(Session["MemberNo"])
                           select ce).FirstOrDefault();
                if (chk.bCompleteInitialState)
                {
                    stepLabel.Visible = false;
                    Imagebutton4.Visible = false;
                    ImagebuttonNext.ImageUrl = "/images/buttonSave.gif";
                }
            }

            //left hand side questions
            var formQuestions = (from fqs in cvdc.FormQuestions
                                 where fqs.iFormID == 2
                                 select fqs.cQuestion);
            int num = 1;

            foreach (var formQuestion in formQuestions)
            {
                Label myLabel = this.FindControl("BodyTypeLabel" + num.ToString()) as Label; // this is your Page class

                if (myLabel != null)
                    myLabel.Text = formQuestion + "\t";

                num++;
            }

            //drop down list population
            double[] ddlists = new double[] { 0, 1, 1.5, 2, 2.5, 3 };

            for (int i = 1; i < 11; i++)
            {
                DropDownList ddls = this.FindControl("BodyTypeDropDownList" + i.ToString()) as DropDownList;
                if (ddls == null) continue;
                ddls.DataSource = ddlists;
                ddls.DataBind();
                ddls.Width = 50;
            }

            //dropdownlist check previous record
            var ddlrs = (from ddlr in cvdc.FormResults
                         where ddlr.iFormID == 2
                         where ddlr.iCustomerID == Convert.ToInt32(Session["MemberNo"])
                         select ddlr);

            foreach (var ddlr in ddlrs)
            {
                DropDownList ddls = this.FindControl("BodyTypeDropDownList" + ddlr.iQuestionID.ToString()) as DropDownList;
                if (ddls != null) ddls.SelectedValue = Convert.ToString(ddlr.fResult);
            }

            //right hand side questions
            var formChildQuestions = (from fcq in cvdc.FormQuestionChildrens
                                      where fcq.ifqID >= 8
                                      where fcq.ifqID <= 17
                                      select fcq);

            //integer helper
            int count = 0, num2 = 11; ;

            foreach (var formQuestionChildren in formChildQuestions)
            {
                Label myLabel2 = this.FindControl("BodyTypeLabel" + num2.ToString()) as Label; // this is your Page class
                if (myLabel2 != null)
                {
                    myLabel2.Text += formQuestionChildren.cQuestion + "<br />";
                    myLabel2.Font.Size = 8;
                }
                if (count == 2)
                {
                    num2++;
                    count = -1;
                }
                count++;
            }

            //populate the header of body type detail
            var custProfile = (from cp in cvdc.PersonalProfile_Externals
                               where cp.iCustomerID == Convert.ToInt32(Session["MemberNo"])
                               select cp).SingleOrDefault();
            if (custProfile != null)
            {
                BodyTypescoreLabel.Text = custProfile.fTotBodyTypeScore.ToString(CultureInfo.InvariantCulture);
                BodyTypelblBodyType.Text = custProfile.cBodyType;
                string pic;

                pic = custProfile.cGender.Equals("Female") ? "female-body-types2.gif" : "malebodytypes.jpg";

                Literalboodytype.Text = "<img src=\"/images/ExtClubVision/" + pic + "\" style=\"max-width: 582px;\" />";
            }
        }

        protected void ImagebuttonNextClick(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (IsComplete())
                {
                    SaveDataToDatabase();
                    BodyTypeLabel25.Visible = false;

                    using (ClubVisionDataContext cvdc = new ClubVisionDataContext())
                    {
                        var customer = (from cust in cvdc.Customer_Externals
                                        where cust.iID == Convert.ToInt32(Session["MemberNo"])
                                        select cust).FirstOrDefault();
                        if ((string)Session["MemberType"] == "VVT")
                        {
                            if (!customer.bCompleteInitialState)
                            {
                                Response.Redirect("/club-vision/my-profile/edit-measurements/", false);
                            }
                            else if (customer.bCompleteInitialState)
                                Response.Redirect("/club-vision/my-profile/edit-profile/?alttemplate=Ext%20Edit%20Profile&tab=bodytype", false);
                        }
                    }
                }
                else
                {
                    BodyTypeLabel25.Text = "Please answer to all question";
                    BodyTypeLabel25.Visible = true;
                    GoBackToBosyTypeScreen();
                }
            }
            catch (Exception exception)
            {
                Response.Write("Exception : " + exception);
            }
        }

        protected void SaveDataToDatabase()
        {
            var cvdc = new ClubVisionDataContext();

            var formcheck = (from fc in cvdc.FormResults
                             where fc.iFormID == 2
                             where fc.iCustomerID == Convert.ToInt32(Session["MemberNo"])
                             select fc);

            if (formcheck.Any())
            {
                cvdc.FormResults.DeleteAllOnSubmit(formcheck);
                cvdc.SubmitChanges();
            }

            var frsList = new List<FormResult>();

            _bodyTypeScore = 0;

            for (int i = 1; i < 11; i++)
            {
                DropDownList ddl = this.FindControl("BodyTypeDropDownList" + i.ToString()) as DropDownList;

                if (ddl != null)
                {
                    var frs = new FormResult
                    {
                        iFormID = 2,
                        iCustomerID = Convert.ToInt32(Session["MemberNo"]),
                        iQuestionID = i,
                        fResult = Convert.ToDouble(ddl.SelectedValue),
                        dDateCaptured = DateTime.Now
                    };
                    frsList.Add(frs);

                    _bodyTypeScore = _bodyTypeScore + Convert.ToDouble(ddl.SelectedValue);
                }
            }

            cvdc.FormResults.InsertAllOnSubmit(frsList);

            var custProfile = (from cs in cvdc.PersonalProfile_Externals
                               where cs.iCustomerID == Convert.ToInt32(Session["MemberNo"])
                               select cs).SingleOrDefault();

            if (custProfile != null)
            {
                custProfile.fTotBodyTypeScore = _bodyTypeScore;
                custProfile.cBodyType = CalcBodyType(_bodyTypeScore);
                custProfile.dDateModified = DateTime.Now;
            }

            cvdc.SubmitChanges();
        }

        protected string CalcBodyType(double btype)
        {
            if ((btype >= 10) && (btype < 13))
            {
                BodyTypelblBodyType.Text = "Ectomorph";
                return "Ectomorph";
            }
            if ((btype >= 13) && (btype < 18))
            {
                BodyTypelblBodyType.Text = "Ecto / Mesomorph";
                return "Ecto / Mesomorph";
            }
            if ((btype >= 18) && (btype < 23))
            {
                BodyTypelblBodyType.Text = "Mesomorph";
                return "Mesomorph";
            }
            if ((btype >= 23) && (btype < 27))
            {
                BodyTypelblBodyType.Text = "Meso / Endomorph";
                return "Meso / Endomorph";
            }
            if (btype >= 27)
            {
                BodyTypelblBodyType.Text = "Endomorph";
                return "Endomorph";
            }

            return "N/A";
        }

        protected bool IsComplete()
        {
            try
            {
                int ddlistCount = 0;

                for (int i = 1; i < 11; i++)
                {
                    DropDownList myddl = this.FindControl("BodyTypeDropDownList" + i.ToString()) as DropDownList;
                    if (myddl != null && myddl.SelectedValue != "0")
                    {
                        ddlistCount++;
                    }
                }

                return ddlistCount == 10;
            }
            catch (Exception)
            {
                return false;
            }

        }

        protected void Imagebutton4_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("/club-vision/my-profile/edit-profile/?alttemplate=Ext%20Edit%20Profile&tab=lifestylescreen", false);
        }

        //protected void bodyTypeCalculateButton_Click(object sender, ImageClickEventArgs e)
        //{
        //    try
        //    {
        //        if (IsComplete())
        //        {
        //            SaveDataToDatabase();
        //            BodyTypeLabel25.Visible = false;

        //            Response.Redirect("/club-vision/my-profile/edit-profile/?alttemplate=Ext%20Edit%20Profile&tab=bodytype", false);
        //        }
        //        else
        //        {
        //            BodyTypeLabel25.Text = "Please answer all questions";
        //            BodyTypeLabel25.Visible = true;
        //            GoBackToBosyTypeScreen();
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        Response.Write("Exception : " + exception);
        //    }
        //}

        protected void GoBackToBosyTypeScreen()
        {
            const string s = "<script type=\"text/javascript\">" +
                             "document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyDetailsProfileTab_4_profileTab').style.backgroundImage = 'url(/images/ExtClubVision/eHdrProfileTabMyDetails.gif)';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyDetailsProfileTab_4_eMyDetails').style.display = 'block';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyDetailsProfileTab_4_eLifestyleScreen').style.display = 'none';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyDetailsProfileTab_4_eBodyType').style.display = 'none';</script>";

            Page.ClientScript.RegisterStartupScript(GetType(), "test", s);
        }
    }
}