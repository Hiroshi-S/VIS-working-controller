using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.UI;

namespace VisionPersonalTrainingProject.usercontrols.externalclubvision
{
    public partial class ResendConfirmEmail : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string toEmail = emailTextBox.Text;

                ClubVisionDataContext cvdc = new ClubVisionDataContext();

                var customerEmail = (from customers in cvdc.Customer_Externals
                                     where customers.cEmail == toEmail
                                     where customers.dDateTerminate >= DateTime.Now
                                     select customers).SingleOrDefault();

                if (customerEmail != null)
                {
                    emailsuccess.Style["display"] = "block";
                    emailerror.Style["display"] = "none";
                    SendEmail(toEmail, customerEmail.cFirstName, customerEmail.cLoginName, customerEmail.cPassword);
                }
                else
                {
                    emailerror.Style["display"] = "block";
                    emailsuccess.Style["display"] = "none";
                }

            }
            catch (Exception exception)
            {

                Response.Write(exception.ToString());
                Response.Write("fail");
            }
            
        }

        protected void SendEmail(string toEmail, string firstName, string userName, string password)
        {
            try
            {
                string ToEmail = toEmail;

                string fromEmail = ConfigurationManager.AppSettings["clubVisionPreSubscriber"];
                //string fromEmail = "dewwdew@gmail.com";

                const string subject = "Welcome to Vision Virtual Training";

                string htmlemailplain = "<h1>haaaaahhhhhahahhaha</h1>";

                string htmlemail = File.ReadAllText(Server.MapPath("/services/templates/newmember-vvt.htm"));
                htmlemail = htmlemail.Replace("<!--FirstName-->", firstName);
                htmlemail = htmlemail.Replace("<!--UserName-->", userName);
                htmlemail = htmlemail.Replace("<!--Password-->", password);
               /* htmlemail = htmlemail.Replace("'http://www.visionpt.com.au/images/edm/newmembers/vpt.jpg'", "\"cid:image1\"");
                htmlemail = htmlemail.Replace("'http://visionpt.com.au/media/126967/serious.jpg'", "\"cid:image2\"");
                htmlemail = htmlemail.Replace("'http://www.visionpt.com.au/images/edm/newmembers/facebook.jpg'", "\"cid:image3\"");
                htmlemail = htmlemail.Replace("'http://www.visionpt.com.au/images/edm/newmembers/youtube.jpg'", "\"cid:image4\"");
                htmlemail = htmlemail.Replace("'http://www.visionpt.com.au/images/edm/newmembers/bluehr.gif'", "\"cid:image5\"");
                htmlemail = htmlemail.Replace("'http://www.visionpt.com.au/images/edm/newmembers/ftr-short.jpg'", "\"cid:image6\"");
                htmlemail = htmlemail.Replace("'http://www.visionpt.com.au/images/edm/newmembers/logon.jpg'", "\"cid:image7\"");
                htmlemail = htmlemail.Replace("'http://www.visionpt.com.au/images/edm/newmembers/wat.jpg'", "\"cid:image8\"");
                htmlemail = htmlemail.Replace("'http://www.visionpt.com.au/images/edm/newmembers/watch.jpg'", "\"cid:image9\"");

                var lrarray = new LinkedResource[9];
                lrarray[0] = new LinkedResource(Server.MapPath("/images/edm/newmembers/vpt.jpg"), MediaTypeNames.Image.Jpeg);
                lrarray[1] = new LinkedResource(Server.MapPath("/media/126967/serious.jpg"), MediaTypeNames.Image.Jpeg);
                lrarray[2] = new LinkedResource(Server.MapPath("/images/edm/newmembers/facebook.jpg"), MediaTypeNames.Image.Jpeg);
                lrarray[3] = new LinkedResource(Server.MapPath("/images/edm/newmembers/youtube.jpg"), MediaTypeNames.Image.Jpeg);
                lrarray[4] = new LinkedResource(Server.MapPath("/images/edm/newmembers/bluehr.gif"), MediaTypeNames.Image.Gif);
                lrarray[5] = new LinkedResource(Server.MapPath("/images/edm/newmembers/ftr-short.jpg"), MediaTypeNames.Image.Jpeg);
                lrarray[6] = new LinkedResource(Server.MapPath("/images/edm/newmembers/logon.jpg"), MediaTypeNames.Image.Jpeg);
                lrarray[7] = new LinkedResource(Server.MapPath("/images/edm/newmembers/wat.jpg"), MediaTypeNames.Image.Jpeg);
                lrarray[8] = new LinkedResource(Server.MapPath("/images/edm/newmembers/watch.jpg"), MediaTypeNames.Image.Jpeg);

                lrarray[0].ContentId = "image1";
                lrarray[1].ContentId = "image2";
                lrarray[2].ContentId = "image3";
                lrarray[3].ContentId = "image4";
                lrarray[4].ContentId = "image5";
                lrarray[5].ContentId = "image6";
                lrarray[6].ContentId = "image7";
                lrarray[7].ContentId = "image8";
                lrarray[8].ContentId = "image9";
                */

                VPTFacilities ees = new VPTFacilities();

                ees.MailExternal(fromEmail, ToEmail, subject, htmlemail, false, true, null, null);
            }
            catch (Exception exception)
            {
                Response.Write(exception.ToString());
                Response.Write("fail");
            }
        }

    }
}