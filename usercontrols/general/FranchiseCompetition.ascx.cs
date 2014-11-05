using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VisionPersonalTrainingProject.usercontrols.general
{
    public partial class FranchiseCompetition : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["tab"] != null && Request.QueryString["tab"].Equals("seetheapplicants"))
                {
                    ClubVisionDataContext cvedc = new ClubVisionDataContext();

                    var fcapplicants = (from fc in cvedc.FranchiseComptetitions
                                        select fc);


                }
            }

        }

        protected void imgButtonSubmit_Click(object sender, ImageClickEventArgs e)
        {
            if (Page.IsValid)
            {
                SaveToDataBase();
                message.Style["display"] = "block";
                franchiseCompetition.Style["display"] = "none";
                SendEmail(txtEmail.Text, txtFirstName.Text);
            }
        }

        protected void SaveToDataBase()
        {
            ClubVisionDataContext cvedc = new ClubVisionDataContext();

            FranchiseComptetition fc = new FranchiseComptetition();

            fc.cFirstName = txtFirstName.Text;
            fc.cLastName = txtLastName.Text;
            fc.cAddress = txtAddress.Text;
            fc.cPhone = txtPhone.Text;
            fc.cEmail = txtEmail.Text;
            fc.dDOB = Convert.ToDateTime(txtDOB.Text);

            cvedc.FranchiseComptetitions.InsertOnSubmit(fc);
            cvedc.SubmitChanges();

            fc.cExperience = "Experience-" + fc.iID.ToString(CultureInfo.InvariantCulture) + Path.GetExtension(FileUpload1.FileName);
            fc.cBusinessPlan = "BusinessPlan-" + fc.iID.ToString(CultureInfo.InvariantCulture) + Path.GetExtension(FileUpload2.FileName);
            fc.cMarketingPlan = "Marketing-" + fc.iID.ToString(CultureInfo.InvariantCulture) + Path.GetExtension(FileUpload3.FileName);
            fc.cWhyWin = "WhyWin-" + fc.iID.ToString(CultureInfo.InvariantCulture) + Path.GetExtension(FileUpload4.FileName);
            fc.cFinancialEvidence = "Financial-" + fc.iID.ToString(CultureInfo.InvariantCulture) + Path.GetExtension(FileUpload5.FileName);
            fc.cSignedTC = "SignedTC-" + fc.iID.ToString(CultureInfo.InvariantCulture) + Path.GetExtension(FileUpload6.FileName);

            string path = "/FranchiseCompetition/" + fc.iID + "/";

            // if directory doesn't exist - create it. 
            if (!Directory.Exists(Server.MapPath(path)))
            {
                Directory.CreateDirectory(Server.MapPath(path));
            }

            FileUpload1.SaveAs(Server.MapPath(path + "Experience-" + fc.iID.ToString(CultureInfo.InvariantCulture) + Path.GetExtension(FileUpload1.FileName)));
            FileUpload2.SaveAs(Server.MapPath(path + "BusinessPlan-" + fc.iID.ToString(CultureInfo.InvariantCulture) + Path.GetExtension(FileUpload2.FileName)));
            FileUpload3.SaveAs(Server.MapPath(path + "Marketing-" + fc.iID.ToString(CultureInfo.InvariantCulture) + Path.GetExtension(FileUpload3.FileName)));
            FileUpload4.SaveAs(Server.MapPath(path + "WhyWin-" + fc.iID.ToString(CultureInfo.InvariantCulture) + Path.GetExtension(FileUpload4.FileName)));
            FileUpload5.SaveAs(Server.MapPath(path + "Financial-" + fc.iID.ToString(CultureInfo.InvariantCulture) + Path.GetExtension(FileUpload5.FileName)));
            FileUpload6.SaveAs(Server.MapPath(path + "SignedTC-" + fc.iID.ToString(CultureInfo.InvariantCulture) + Path.GetExtension(FileUpload6.FileName)));

            cvedc.SubmitChanges();
            cvedc.Dispose();

        }

        protected void CustomValidator1_ServerValidate1(object source, ServerValidateEventArgs args)
        {

            args.IsValid = ((FileUpload1.PostedFile.ContentType.Equals("application/pdf") ||
                                FileUpload1.PostedFile.ContentType.Equals("application/msword") ||
                                FileUpload1.PostedFile.ContentType.Equals("application/vnd.openxmlformats-officedocument.wordprocessingml.document"))
                                && FileUpload1.PostedFile.ContentLength < 5243000);
        }

        protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = ((FileUpload2.PostedFile.ContentType.Equals("application/pdf") ||
                                FileUpload2.PostedFile.ContentType.Equals("application/msword") ||
                                FileUpload2.PostedFile.ContentType.Equals("application/vnd.openxmlformats-officedocument.wordprocessingml.document"))
                                && FileUpload2.PostedFile.ContentLength < 5243000);
        }

        protected void CustomValidator3_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = ((FileUpload3.PostedFile.ContentType.Equals("application/pdf") ||
                                FileUpload3.PostedFile.ContentType.Equals("application/msword") ||
                                FileUpload3.PostedFile.ContentType.Equals("application/vnd.openxmlformats-officedocument.wordprocessingml.document"))
                                && FileUpload3.PostedFile.ContentLength < 5243000);
        }

        protected void CustomValidator4_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = ((FileUpload4.PostedFile.ContentType.Equals("application/pdf") ||
                                FileUpload4.PostedFile.ContentType.Equals("application/msword") ||
                                FileUpload4.PostedFile.ContentType.Equals("application/vnd.openxmlformats-officedocument.wordprocessingml.document"))
                                && FileUpload4.PostedFile.ContentLength < 5243000);
        }

        protected void CustomValidator5_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = ((FileUpload5.PostedFile.ContentType.Equals("application/pdf") ||
                                FileUpload5.PostedFile.ContentType.Equals("application/msword") ||
                                FileUpload5.PostedFile.ContentType.Equals("application/vnd.openxmlformats-officedocument.wordprocessingml.document"))
                                && FileUpload5.PostedFile.ContentLength < 5243000);
        }

        protected void CustomValidator6_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = (FileUpload6.PostedFile.ContentType.Equals("application/pdf") &&
                FileUpload6.PostedFile.ContentLength < 5243000);
        }

        protected void SendEmail(string toEmail, string firstName)
        {
            try
            {
                string fromEmail = ConfigurationManager.AppSettings["newmemberemails"];
                //string fromEmail = "dewwdew@gmail.com";

                const string subject = "Vision Personal Training Studio Sponsorship";

                string htmlemail = File.ReadAllText(Server.MapPath("/services/templates/FranchiseCompetitionThankYou.htm"));
                htmlemail = htmlemail.Replace("<!--FirstName-->", firstName);
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

                VPTFacilities ees = new VPTFacilities();

                ees.MailExternal(fromEmail, toEmail, subject, htmlemail, false, true, null, lrarray);
                // Response.Redirect(Server.MapPath("/images/edm/newmembers/vpt.jpg").ToString());
            }
            catch (Exception exception)
            {
                Response.Write(exception.ToString());
                Response.Write("fail");
                Console.WriteLine(exception.ToString());

            }
        }
    }
}