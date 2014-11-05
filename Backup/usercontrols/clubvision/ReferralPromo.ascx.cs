using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Web.UI;
using System.Globalization;

namespace VisionPersonalTrainingProject.usercontrols.clubvision
{
    public partial class ReferralPromo : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                VisionPersonalTrainingProject.ClubVisionDataContext cvdc = new VisionPersonalTrainingProject.ClubVisionDataContext();

                var studioList = (from stlist in cvdc.EnumTables
                                  where stlist.ID == 11
                                  select stlist).OrderBy(x => x.Value).ToList();

                refStudioDropDownList.DataTextField = "Value";
                refStudioDropDownList.DataValueField = "intValue";
                refStudioDropDownList.DataSource = studioList;
                refStudioDropDownList.DataBind();

                if (Request.QueryString["emailconfirmation"] != null && Request.QueryString["emailconfirmation"].Equals("true"))
                {
                    const string s = "<script type=\"text/javascript\">" +
                             "sendconfirmationreferral();</script>";

                    Page.ClientScript.RegisterStartupScript(GetType(), "test", s);
                }
            }
        }

        protected void SavetoDatabase()
        {
            try
            {
                VisionPersonalTrainingProject.ClubVisionDataContext cvdc = new VisionPersonalTrainingProject.ClubVisionDataContext();

                CustomerReferral cr = new CustomerReferral();

                TextInfo ti = CultureInfo.CurrentCulture.TextInfo;

                cr.iCustomerID = (int)Session["MemberNo"];
                cr.cChosenGift = refChoiceDropDownList.SelectedValue;
                cr.cRefName = ti.ToTitleCase(reftxtName.Text);
                cr.cRefEmailAddress = reftxtEmail.Text;
                cr.cMobileNumber = reftxtMobile.Text;

                cr.cChosenStudio = refStudioDropDownList.SelectedValue;
                cr.dDate = System.DateTime.Now;

                cvdc.CustomerReferrals.InsertOnSubmit(cr);
                cvdc.SubmitChanges();
                cvdc.Dispose();
            }
            catch (Exception ex)
            {

            }

        }

        protected void SendEmailtoReferreeCVAccess(string toEmail, string firstName, string referrerFullName)
        {
            try
            {
                string fromEmail = ConfigurationManager.AppSettings["clubVisionPreSubscriber"];

                string subject = referrerFullName + " has given you ClubVision access for $1";

                string htmlemail = File.ReadAllText(Server.MapPath("/services/templates/torefferee-cvaccess.htm"));
                htmlemail = htmlemail.Replace("<!--FirstName-->", firstName);
                htmlemail = htmlemail.Replace("<!--ReferrerName-->", referrerFullName);

                //  htmlemail = htmlemail.Replace("'http://www.visionpt.com.au/images/edm/newmembers/vpt.jpg'", "\"cid:image1\"");
                //  htmlemail = htmlemail.Replace("'http://www.visionpt.com.au/images/edm/newmembers/facebook.jpg'", "\"cid:image3\"");
                //  htmlemail = htmlemail.Replace("'http://www.visionpt.com.au/images/edm/newmembers/youtube.jpg'", "\"cid:image4\"");


                // var lrarray = new LinkedResource[3];
                // lrarray[0] = new LinkedResource(Server.MapPath("/images/edm/newmembers/vpt.jpg"), MediaTypeNames.Image.Jpeg);
                // lrarray[1] = new LinkedResource(Server.MapPath("/images/edm/newmembers/facebook.jpg"), MediaTypeNames.Image.Jpeg);
                // lrarray[2] = new LinkedResource(Server.MapPath("/images/edm/newmembers/youtube.jpg"), MediaTypeNames.Image.Jpeg);

                // lrarray[0].ContentId = "image1";
                // lrarray[1].ContentId = "image3";
                // lrarray[2].ContentId = "image4";

                VPTFacilities ees = new VPTFacilities();

                // ees.ReferralMail(fromEmail, toEmail, subject, htmlemail, false, true, null, lrarray);
                ees.ReferralMail(fromEmail, toEmail, subject, htmlemail, false, true);
            }
            catch (Exception exception)
            {
                Response.Write(exception.ToString());
                Response.Write("fail");
                Console.WriteLine(exception.ToString());
            }
        }

        protected void SendEmailtoReferreePTSessions(string toEmail, string firstName, string referrerFullName, string studio)
        {
            try
            {
                string fromEmail = ConfigurationManager.AppSettings["clubVisionPreSubscriber"];

                string subject = referrerFullName + " has given you free Personal Training sessions";

                string htmlemail = File.ReadAllText(Server.MapPath("/services/templates/torefferee-ptsessions.htm"));
                htmlemail = htmlemail.Replace("<!--FirstName-->", firstName);
                htmlemail = htmlemail.Replace("<!--ReferrerName-->", referrerFullName);
                htmlemail = htmlemail.Replace("<!--studio-->", studio);
                /*
                htmlemail = htmlemail.Replace("'http://www.visionpt.com.au/images/edm/newmembers/vpt.jpg'", "\"cid:image1\"");
                htmlemail = htmlemail.Replace("'http://www.visionpt.com.au/images/edm/newmembers/facebook.jpg'", "\"cid:image3\"");
                htmlemail = htmlemail.Replace("'http://www.visionpt.com.au/images/edm/newmembers/youtube.jpg'", "\"cid:image4\"");

                var lrarray = new LinkedResource[3];
                lrarray[0] = new LinkedResource(Server.MapPath("/images/edm/newmembers/vpt.jpg"), MediaTypeNames.Image.Jpeg);
                lrarray[1] = new LinkedResource(Server.MapPath("/images/edm/newmembers/facebook.jpg"), MediaTypeNames.Image.Jpeg);
                lrarray[2] = new LinkedResource(Server.MapPath("/images/edm/newmembers/youtube.jpg"), MediaTypeNames.Image.Jpeg);

                lrarray[0].ContentId = "image1";
                lrarray[1].ContentId = "image3";
                lrarray[2].ContentId = "image4";
                */
                VPTFacilities ees = new VPTFacilities();
                ees.ReferralMail(fromEmail, toEmail, subject, htmlemail, false, true);
                // ees.ReferralMail(fromEmail, toEmail, subject, htmlemail, false, true, null, lrarray);
                // Response.Redirect(Server.MapPath("/images/edm/newmembers/vpt.jpg").ToString());
            }
            catch (Exception exception)
            {
                Response.Write(exception.ToString());
                Response.Write("fail");
                Console.WriteLine(exception.ToString());

            }
        }

        protected void SendEmailtoReferrer(string toEmail, string firstName)
        {
            try
            {
                string fromEmail = ConfigurationManager.AppSettings["clubVisionPreSubscriber"];
                //string fromEmail = "dewwdew@gmail.com";

                const string subject = "Thank You for Your Inspiration";

                string htmlemail = File.ReadAllText(Server.MapPath("/services/templates/torefferer.htm"));
                htmlemail = htmlemail.Replace("<!--FirstName-->", firstName);
                /*
                 htmlemail = htmlemail.Replace("'http://www.visionpt.com.au/images/edm/newmembers/vpt.jpg'", "\"cid:image1\"");
                 htmlemail = htmlemail.Replace("'http://www.visionpt.com.au/images/edm/newmembers/facebook.jpg'", "\"cid:image3\"");
                 htmlemail = htmlemail.Replace("'http://www.visionpt.com.au/images/edm/newmembers/youtube.jpg'", "\"cid:image4\"");


                 var lrarray = new LinkedResource[3];
                 lrarray[0] = new LinkedResource(Server.MapPath("/images/edm/newmembers/vpt.jpg"), MediaTypeNames.Image.Jpeg);
                 lrarray[1] = new LinkedResource(Server.MapPath("/images/edm/newmembers/facebook.jpg"), MediaTypeNames.Image.Jpeg);
                 lrarray[2] = new LinkedResource(Server.MapPath("/images/edm/newmembers/youtube.jpg"), MediaTypeNames.Image.Jpeg);

                 lrarray[0].ContentId = "image1";
                 lrarray[1].ContentId = "image3";
                 lrarray[2].ContentId = "image4";
                 */
                VPTFacilities ees = new VPTFacilities();
                ees.ReferralMail(fromEmail, toEmail, subject, htmlemail, false, true);
                // ees.ReferralMail(fromEmail, toEmail, subject, htmlemail, false, true, null, lrarray);
                // Response.Redirect(Server.MapPath("/images/edm/newmembers/vpt.jpg").ToString());
            }
            catch (Exception exception)
            {
                Response.Write(exception.ToString());
                Response.Write("fail");
                Console.WriteLine(exception.ToString());

            }

        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            SavetoDatabase();
            SendAllEmailCorrespondence();
            Response.Redirect("/club-vision/my-profile?emailconfirmation=true", false);
        }

        protected void SendAllEmailCorrespondence()
        {
            VisionPersonalTrainingProject.ClubVisionDataContext cvdc = new VisionPersonalTrainingProject.ClubVisionDataContext();

            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;

            var cust = (from cu in cvdc.Customers
                        where cu.Id == (int)Session["MemberNo"]
                        select cu).SingleOrDefault();

            if (cust != null)
            {
                SendEmailtoReferrer(cust.Email, cust.Firstname + " " + cust.LastName);

                if (refChoiceDropDownList.SelectedValue.Equals("1"))
                {
                    SendEmailtoReferreePTSessions(reftxtEmail.Text, ti.ToTitleCase(reftxtName.Text), cust.Firstname + " " + cust.LastName, refStudioDropDownList.SelectedItem.Text);
                }
                if (refChoiceDropDownList.SelectedValue.Equals("2"))
                {
                    SendEmailtoReferreeCVAccess(reftxtEmail.Text, ti.ToTitleCase(reftxtName.Text), cust.Firstname + " " + cust.LastName);
                }

                VPTFacilities ees = new VPTFacilities();
                ees.MailToCJ("Referral to your studio", "Hi, <br/><br/>" +
                                cust.Firstname + " " + cust.LastName + " from " + cust.Studio + " studio has referred someone to your studio for 2 free personal training sessions via the referral section in Vision Virtual Training.<br/><br/>" +
                                "Please find their contact details below. <br/>" +
                                "Name : " + ti.ToTitleCase(reftxtName.Text) + "<br/>" +
                                "Email : " + reftxtEmail.Text + "<br/>" +
                                "Mobile : " + reftxtMobile.Text + "<br/><br/> Thanks, <br/>Vision Personal Training."
                                , false, true);
                //mail to referree studio
                ees.MailStudioWithoutCCEnq(Convert.ToInt32(refStudioDropDownList.SelectedValue), 1, cust.Email, "Referral to your studio",
                                "Hi, <br/><br/>" +
                                cust.Firstname + " " + cust.LastName + " from " + cust.Studio + " studio has referred someone to your studio for 2 free personal training sessions via the referral section in Vision Virtual Training.<br/><br/>" +
                                "Please find their contact details below. <br/>" +
                                "Name : " + ti.ToTitleCase(reftxtName.Text) + "<br/>" +
                                "Email : " + reftxtEmail.Text + "<br/>" +
                                "Mobile : " + reftxtMobile.Text + "<br/><br/> Thanks, <br/>Vision Personal Training."
                                , false, true);
            }

            cvdc.Dispose();
        }

        protected void PageCleanUp()
        {
            refChoiceDropDownList.SelectedValue = "0";
            refStudioDropDownList.SelectedValue = "0";
            reftxtName.Text = "";
            reftxtEmail.Text = "";
            reftxtEmailConfirm.Text = "";
            reftxtMobile.Text = "";
        }

    }
}