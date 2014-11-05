using System;
using System.Net.Mime;
using System.Web;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Data;
using SpinningCube.DAL;
using System.Configuration;

/*
    [1]Hiroshi 17/12 - added for sending email to trainer for questionnarie completion 
    [2]Dewi 16/01/14 - modified autoresponder email to client when they hit lightbox
*/

namespace VisionPersonalTrainingProject
{
    public class VPTFacilities
    {

        public void Mail(string From, string To, string Subject, string Body, bool SSL, bool HTML, HttpPostedFile file = null)
        {
            MailMessage Message = new MailMessage(From, To);

            SmtpClient Smtp = new SmtpClient();

            Message.Subject = Subject;
            Message.Body = Body;
            Message.CC.Add(ConfigurationManager.AppSettings["enquiryemails"]);
            //Message.CC.Add(To);
            Message.CC.Add("max@tssh.com.au");

            Smtp.EnableSsl = SSL;
            Message.IsBodyHtml = HTML;
            if (file != null)
            {
                Message.Attachments.Add(new Attachment(file.InputStream, file.FileName));
            }
            Smtp.Send(Message);


            if (Subject.Equals("Vision PT Consultation Form"))
            {
                //send auto responder email
                //From is now To as email above is sent from enquirer to studio, this one is sent from Vision to enquirer
                MailMessage MessageRespond = new MailMessage("enquiry@visionpt.com.au", From);
                MessageRespond.Subject = "Welcome to Vision Personal Training";
                MessageRespond.Body = "Welcome to Vision Personal Training.<br/><br/>" +
                                      "Congratulations on making a proactive step to improving your health and fitness, we can't wait to help you start your journey.<br/>" +
                                      "We really appreciate you contacting us and will be in touch with you shortly.<br/>" +
                                      "Looking forward to meeting you and hearing your story, aspirations and dreams.<br/><br/>" +
                                      "The team at Vision Personal Training";
                Smtp.EnableSsl = SSL;
                MessageRespond.IsBodyHtml = true;
                Smtp.Send(MessageRespond);
            }
            else
            {
                //send auto responder email
                //From is now To as email above is sent from enquirer to studio, this one is sent from Vision to enquirer
                MailMessage MessageRespond = new MailMessage("enquiry@visionpt.com.au", From);
                MessageRespond.Subject = "Thank you for your enquiry";
                MessageRespond.Body = "Hi, \r\n\r\n Thank you for your enquiry. We will be in touch with you shortly.\r\n\r\n Vision Personal Training \r\n 1300 181 786";
                Smtp.EnableSsl = SSL;
                MessageRespond.IsBodyHtml = false;
                Smtp.Send(MessageRespond);

            }
        }

        public void MailWithoutCCEnq(string From, string To, string Subject, string Body, bool SSL, bool HTML, HttpPostedFile file = null)
        {
            MailMessage Message = new MailMessage(From, To);

            SmtpClient Smtp = new SmtpClient();

            Message.Subject = Subject;
            Message.Body = Body;

            Smtp.EnableSsl = SSL;
            Message.IsBodyHtml = HTML;
            if (file != null)
            {
                Message.Attachments.Add(new Attachment(file.InputStream, file.FileName));
            }
            Smtp.Send(Message);

        }

        public void MailErrorMsg(string From, string Subject, string Body, bool SSL, bool HTML, HttpPostedFile file = null)
        {
            string toErrorEmail = ConfigurationManager.AppSettings["erroremailsnotif"];
            MailMessage Message = new MailMessage(From, toErrorEmail);

            SmtpClient Smtp = new SmtpClient();

            Message.Subject = Subject;
            Message.Body = Body;

            Smtp.EnableSsl = SSL;
            Message.IsBodyHtml = HTML;
            if (file != null)
            {
                Message.Attachments.Add(new Attachment(file.InputStream, file.FileName));
            }
            Smtp.Send(Message);
        }

        //------------------------------------------[1]-------------------------------------------------------------------------------------------------
        public void MailTrainerQuestionnarieComp(int studioId, string To, string Subject, string Body, bool SSL, bool HTML, HttpPostedFile file = null)
        {
            MailMessage Message = new MailMessage(" admin@visionvirtualtraining.com.au", To);

            SmtpClient Smtp = new SmtpClient();

            Message.Subject = Subject;
            Message.Body = Body;

            DataTable dtStudios = StudioContactEnquiryGet(studioId, 1);
            string CCemails = "webdesign@visionpt.com.au,";
            for (int Count = 0; Count < dtStudios.Rows.Count; Count++)
            {
                if (Count > 0)
                    CCemails = CCemails + ",";

                CCemails = CCemails + dtStudios.Rows[Count]["contact"].ToString();
            }

            Message.Bcc.Add(CCemails);
            // Message.CC.Add(Cc);

            Smtp.EnableSsl = SSL;
            Message.IsBodyHtml = HTML;
            if (file != null)
            {
                Message.Attachments.Add(new Attachment(file.InputStream, file.FileName));
            }
            Smtp.Send(Message);
        }
        //------------------------------------------[/1]-------------------------------------------------------------------------------------------------

        public void MailTrainerClient(int studioId, string To, string Cc, string Subject, string Body, bool SSL, bool HTML, HttpPostedFile file = null)
        {
            MailMessage Message = new MailMessage("admin@visionvirtualtraining.com.au", To);

            SmtpClient Smtp = new SmtpClient();

            Message.Subject = Subject;
            Message.Body = Body;

            //DataTable dtStudios = StudioContactEnquiryGet(studioId, 1);
            string CCemails = "webdesign@hq.visionpersonaltraining.com";

            //below is to copy to all studio correspondence
            /*for (int Count = 0; Count < dtStudios.Rows.Count; Count++)
            {
                if (Count > 0)
                    CCemails = CCemails + ",";

                CCemails = CCemails + dtStudios.Rows[Count]["contact"].ToString();
            }
            */
            Message.Bcc.Add(CCemails);

            Smtp.EnableSsl = SSL;
            Message.IsBodyHtml = HTML;

            if (file != null)
            {
                Message.Attachments.Add(new Attachment(file.InputStream, file.FileName));
            }

            Smtp.Send(Message);

            //send auto responder email
            //From is now To as email above is sent from enquirer to studio, this one is sent from Vision to enquirer
            MailMessage MessageRespond = new MailMessage("admin@visionvirtualtraining.com.au", Cc);
            MessageRespond.Subject = "Thank you sharing your food plan";
            MessageRespond.Body = "Hi, <br/><br/> your food plan has been sent to destination email.<br/><br/> Vision Personal Training <br/> 1300 181 786 <br/><br/> Here's the copy of your email <br/><br/>"
                + Body;
            Smtp.EnableSsl = SSL;
            MessageRespond.IsBodyHtml = true;
            Smtp.Send(MessageRespond);
        }

        public void MailToCJ(string Subject, string Body, bool SSL, bool HTML, HttpPostedFile file = null)
        {
            MailMessage Message = new MailMessage("admin@visionvirtualtraining.com.au", "webdesign@hq.visionpersonaltraining.com");

            SmtpClient Smtp = new SmtpClient();

            Message.Subject = Subject;
            Message.Body = Body;

            Smtp.EnableSsl = SSL;
            Message.IsBodyHtml = HTML;
            if (file != null)
            {
                Message.Attachments.Add(new Attachment(file.InputStream, file.FileName));
            }
            Smtp.Send(Message);
        }

        public void FoodMail(string From, string To, string Subject, string Body, bool SSL, bool HTML, HttpPostedFile file = null)
        {
            MailMessage Message = new MailMessage(From, To);

            SmtpClient Smtp = new SmtpClient();

            Message.Subject = Subject;
            Message.Body = Body;

            Smtp.EnableSsl = SSL;
            Message.IsBodyHtml = HTML;
            if (file != null)
            {
                Message.Attachments.Add(new Attachment(file.InputStream, file.FileName));
            }
            Smtp.Send(Message);


            //send auto responder email
            //From is now To as email above is sent from enquirer to studio, this one is sent from Vision to enquirer
            MailMessage MessageRespond = new MailMessage("foodrequest@visionpt.com.au", From);
            MessageRespond.Subject = "Thank you for your Food Request";
            MessageRespond.Body = "Hello and thank you for using the Vision Virtual Training food diary and also submitting your food request to our data base.\r\n\r\n" +
                                  "We’ll be in touch with you shortly to let you know when it has been finalised.\r\n\r\n" +
                                  "Until then, you may have chosen already to select the 'Add Your Own Food' button which allows you to instantly enter your foods into your diary to use right away.\r\n\r\n" +
                                  "Our clients constantly tell us that their results have come from using the food diary so keep up the great work and enjoy watching your own goals be accomplished.\r\n\r\n" +
                                  "Yours in health and fitness,\r\nClaire Ratapu\r\nVision Virtual Training";
            Smtp.EnableSsl = SSL;
            MessageRespond.IsBodyHtml = false;
            Smtp.Send(MessageRespond);
        }

        public void ReferralMail(string @from, string to, string subject, string body, bool ssl, bool html, HttpPostedFile file = null, LinkedResource[] lr = null)
        {
            var message = new MailMessage(@from, to);

            var smtp = new SmtpClient();

            AlternateView av = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);

            message.Subject = subject;
            message.Body = body;
            message.Bcc.Add("webdesign@hq.visionpersonaltraining.com");

            message.AlternateViews.Add(av);

            if (lr != null)
                foreach (LinkedResource t in lr)
                {
                    av.LinkedResources.Add(t);
                }

            smtp.EnableSsl = ssl;
            message.IsBodyHtml = html;

            if (file != null)
            {
                message.Attachments.Add(new Attachment(file.InputStream, file.FileName));
            }
            smtp.Send(message);
        }

        public void MailStudio(int StudioID, int EnquiryType, string From, string Subject, string Body, bool SSL, bool HTML)
        {

            DataTable dtStudios = StudioContactEnquiryGet(StudioID, EnquiryType);
            string emails = "";
            for (int Count = 0; Count < dtStudios.Rows.Count; Count++)
            {
                if (Count > 0)
                    emails = emails + ",";

                emails = emails + dtStudios.Rows[Count]["contact"].ToString();
            }
            Mail(From, emails, Subject, Body, SSL, HTML);
        }

        public void MailStudioWithoutCCEnq(int StudioID, int EnquiryType, string From, string Subject, string Body, bool SSL, bool HTML)
        {
            DataTable dtStudios = StudioContactEnquiryGet(StudioID, EnquiryType);
            string emails = "";
            for (int Count = 0; Count < dtStudios.Rows.Count; Count++)
            {
                if (Count > 0)
                    emails = emails + ",";

                emails = emails + dtStudios.Rows[Count]["contact"].ToString();
            }

            MailMessage Message = new MailMessage("admin@visionvirtualtraining.com.au", emails);

            SmtpClient Smtp = new SmtpClient();

            Message.Subject = Subject;
            Message.Body = Body;
            Message.CC.Add("web@visionpt.com.au");

            Smtp.EnableSsl = SSL;
            Message.IsBodyHtml = HTML;

            Smtp.Send(Message);
        }

        public void StarShotsMail(string @from, string to, string subject, string body, bool ssl, bool html)
        {
            var message = new MailMessage(@from, to);

            var smtp = new SmtpClient();

            AlternateView av = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);

            message.Subject = subject;
            message.Body = body;
            message.Bcc.Add("web@visionpt.com.au");

            message.AlternateViews.Add(av);

            smtp.EnableSsl = ssl;
            message.IsBodyHtml = html;

            smtp.Send(message);
        }

        public void MailExternal(string @from, string to, string subject, string body, bool ssl, bool html, HttpPostedFile file = null, LinkedResource[] lr = null)
        {
            var message = new MailMessage(@from, to);

            var smtp = new SmtpClient();

            AlternateView av = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);

            message.Subject = subject;
            message.Body = body;

            if (!subject.Equals("Vision Virtual Training Log-in Details") && !subject.Equals("VVT PayPal : Went to nowhere"))
            {
                message.Bcc.Add("webdesign@hq.visionpersonaltraining.com");
            }

            //message.Bcc.Add("enquiry@visionpt.com.au");

            message.AlternateViews.Add(av);

            if (lr != null)
                foreach (LinkedResource t in lr)
                {
                    av.LinkedResources.Add(t);
                }

            smtp.EnableSsl = ssl;
            message.IsBodyHtml = html;

            if (file != null)
            {
                message.Attachments.Add(new Attachment(file.InputStream, file.FileName));
            }
            smtp.Send(message);
        }

        public static DataTable StudioLocationsGet()
        {
            DataTable dtStudios = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["con"].ConnectionString, CommandType.StoredProcedure, "USP_BindStudioName").Tables[0];
            return dtStudios;
        }

        public static DataTable StudioGetById(int StudioId)
        {
            //get studio email addresses
            SqlParameter[] spList = new SqlParameter[1];
            spList[0] = new SqlParameter("@StudioID", StudioId);
            DataTable dtStudios = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["con"].ConnectionString, CommandType.StoredProcedure, "USP_StudioByID", spList).Tables[0];
            return dtStudios;
        }

        public static DataTable StudioGetByName(string StudioName)
        {
            //get studio email addresses
            SqlParameter[] spList = new SqlParameter[1];
            spList[0] = new SqlParameter("@Studio", StudioName);
            DataTable dtStudios = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["con"].ConnectionString, CommandType.StoredProcedure, "USP_StudioByName", spList).Tables[0];
            return dtStudios;
        }

        /// <summary>
        /// Populates the checkboxes for studio contact details
        /// </summary>
        public static DataTable LoadEnquiryTypes()
        {

            return SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["con"].ConnectionString, CommandType.StoredProcedure, "USP_EnquiryTypeGet").Tables[0];

        }

        /// <summary>
        /// retrieves all contacts for the given studio
        /// </summary>
        /// <param name="StudioId"></param>
        public static DataTable StudioContactGet(int StudioId)
        {
            //get studio email addresses
            SqlParameter[] spList = new SqlParameter[1];
            spList[0] = new SqlParameter("@StudioID", StudioId);
            return SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["con"].ConnectionString, CommandType.StoredProcedure, "USP_StudioContactGet", spList).Tables[0];

        }

        /// <summary>
        /// retrieves all contacts for the given studio for the given enquiry type
        /// </summary>
        /// <param name="StudioId"></param>
        public static DataTable StudioContactEnquiryGet(int StudioId, int EnquiryType)
        {
            //get studio email addresses
            SqlParameter[] spList = new SqlParameter[2];
            spList[0] = new SqlParameter("@StudioID", StudioId);
            spList[1] = new SqlParameter("@enquirytypeid", EnquiryType);
            return SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["con"].ConnectionString, CommandType.StoredProcedure, "USP_StudioContactEnquiryGet", spList).Tables[0];

        }

        public static void StudioContactUpdate(DataTable dtContactUpdate, int StudioId)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
            // initialte transaction
            connection.Open();
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    SqlCommand StudioDelete = new SqlCommand("USP_StudioContactDelete", connection);
                    StudioDelete.CommandType = CommandType.StoredProcedure;
                    StudioDelete.Connection = connection;
                    StudioDelete.Parameters.Add("@studioid", SqlDbType.Int, 0, "studioid");
                    StudioDelete.Parameters["@studioid"].Value = StudioId;
                    StudioDelete.Transaction = transaction;
                    StudioDelete.ExecuteNonQuery();

                    SqlCommand StudioInsert = new SqlCommand("USP_StudioContactInsert", connection);
                    StudioInsert.CommandType = CommandType.StoredProcedure;
                    StudioInsert.Connection = connection;

                    StudioInsert.Parameters.Add("@studioid", SqlDbType.Int, 0, "studioid");
                    StudioInsert.Parameters.Add("@enquirytypeid", SqlDbType.Int, 0, "enquirytypeid");
                    StudioInsert.Parameters.Add("@contacttype", SqlDbType.NVarChar, 50, "contacttype");
                    StudioInsert.Parameters.Add("@contact", SqlDbType.NVarChar, 255, "contact");
                    StudioInsert.Transaction = transaction;
                    for (int Count = 0; Count < dtContactUpdate.Rows.Count; Count++)
                    {
                        StudioInsert.Parameters["@studioid"].Value = dtContactUpdate.Rows[Count]["studioid"];
                        StudioInsert.Parameters["@enquirytypeid"].Value = dtContactUpdate.Rows[Count]["enquirytypeid"];
                        StudioInsert.Parameters["@contacttype"].Value = dtContactUpdate.Rows[Count]["contacttype"];
                        StudioInsert.Parameters["@contact"].Value = dtContactUpdate.Rows[Count]["contact"];
                        StudioInsert.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
        }
    }
}