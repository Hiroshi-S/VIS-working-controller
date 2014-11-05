using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VisionPersonalTrainingProject.usercontrols.studiopages
{
    public partial class JoinCVRegistration : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            ClubVisionDataContext cvdc = new ClubVisionDataContext();
            
            ClubVisionPreSub cvp = new ClubVisionPreSub();

            cvp.Name = NameTextBox.Text;
            cvp.Email = EmailTextBox.Text;

            cvdc.ClubVisionPreSubs.InsertOnSubmit(cvp);
            cvdc.SubmitChanges();
            cvdc.Dispose();
            SendEmailConfirmation();
            confirmLabel.Text = "Your data is saved.";
            confirmLabel.Visible = true;

        }

        public void SendEmailConfirmation()
        {
            MailMessage m = new MailMessage();
            SmtpClient sc = new SmtpClient();

            try
            {
                m.From = new MailAddress("admin@clubvision.com.au", "Club Vision");
                m.To.Add(new MailAddress(EmailTextBox.Text, NameTextBox.Text));
                m.CC.Add(new MailAddress("web@visionpt.com.au", "Dewi Candraningsih"));
                //similarly BCC
                m.Subject = "Welcome to Club Vision";
                m.IsBodyHtml = true;
                m.Body = "Hi " + NameTextBox.Text + ", <br /><br />" +
                        "Thanks for leaving your detail, we will notify you when it is launched.<br />"+
                        "Have a good day!<br />" +
                        "Vision Personal Training";

                sc.Host = "smtp.gmail.com";
                sc.Port = 587;
                sc.Credentials = new System.Net.NetworkCredential("dewwdew@gmail.com", "kunokbaping");
                sc.EnableSsl = true;
                sc.Send(m);
                Console.ReadLine();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}