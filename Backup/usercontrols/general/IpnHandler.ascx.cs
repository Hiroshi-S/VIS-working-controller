using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using umbraco.cms.businesslogic.member;

namespace VisionPersonalTrainingProject.usercontrols.general
{
    public partial class IpnHandler : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Post back to either sandbox or live
            string strSandbox = "https://www.sandbox.paypal.com/cgi-bin/webscr";
            string strLive = "https://www.paypal.com/cgi-bin/webscr";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(strLive);

            //Set values for the request back
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            byte[] param = Request.BinaryRead(HttpContext.Current.Request.ContentLength);
            string strRequest = Encoding.ASCII.GetString(param);
            strRequest += "&cmd=_notify-validate";
            req.ContentLength = strRequest.Length;

            //for proxy
            //WebProxy proxy = new WebProxy(new Uri("http://url:port#"));
            //req.Proxy = proxy;

            //Send the request to PayPal and get the response
            StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
            streamOut.Write(strRequest);
            streamOut.Close();
            StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream());
            string strResponse = streamIn.ReadToEnd();
            streamIn.Close();

            if (strResponse == "VERIFIED")
            {
                ClubVisionDataContext cvdc = new ClubVisionDataContext();
                NameValueCollection queryString = HttpUtility.ParseQueryString(strRequest);

                try
                {
                    var customer = (from cu in cvdc.Customer_Externals
                                    where cu.iID == int.Parse(queryString["custom"])
                                    select cu).SingleOrDefault();

                    string receiverEmail = queryString["receiver_email"];
                    if (customer != null)
                    {
                        switch (queryString["txn_type"])
                        {
                            case "subscr_payment":
                                {
                                    if ((receiverEmail.Equals("spalmer@visionpt.com.au")
                                        || receiverEmail.Equals("admin@visionpt.com.au")
                                        || receiverEmail.Equals("spalmer@hq.visionpersonaltraining.com")) && //check that receiver_email is your Primary PayPal email
                                    queryString["payment_status"].Equals("Completed") && //check the payment_status is Completed 
                                    IsTxnidHasNotBeenProcessed(queryString["txn_id"]) == false && //check that txn_id has not been previously processed
                                    IsPaidAmountCorrect(queryString["mc_gross"], queryString["subscr_id"])) //check that payment_amount/payment_currency are correct --> havent checked the currency
                                    {
                                        bool isNewMember = false;

                                        if (!customer.bActive) //the case where sign up notification arrives late
                                        {
                                            customer.bActive = true;
                                            customer.cPassword = queryString["ipn_track_id"];
                                            //create member in umbraco

                                            if (System.Web.Security.Membership.FindUsersByName(customer.cLoginName).Count == 0)
                                            {
                                                CreateMemberInUmbraco(customer.cFirstName + " " + customer.cLastName, customer.cEmail, queryString["ipn_track_id"], customer.cLoginName);
                                            }
                                            //send email confirmation
                                            SendEmail(customer.cEmail, customer.cFirstName, customer.cLoginName, queryString["ipn_track_id"]);
                                            isNewMember = true;
                                        }

                                        if (isNewMember == false)
                                        {
                                            //capture their first full payment
                                            if (customer.dFirstFullPayment == null &&
                                                (queryString["mc_gross"].Equals("29.00") || queryString["mc_gross"].Equals("25.00")))
                                            {
                                                customer.dFirstFullPayment = DateTime.Now;
                                            }
                                        }
                                        customer.dDateTerminate = DateTime.Now.AddDays(35);
                                        customer.dDateLastPayment = DateTime.Now;

                                        //get dewi notified
                                        SendNotificationEmailToDewi("VVT PayPal : Payment", "iCustomerID:" + queryString["custom"] + " Amount:" + queryString["mc_gross"] + " itemNumber:" + queryString["item_number"]);
                                    }

                                    SaveDatatoPayPalIpnTable(strRequest,
                                                            queryString["payment_status"],
                                                            queryString["txn_id"],
                                                            receiverEmail,
                                                            queryString["mc_gross"],
                                                            queryString["mc_currency"],
                                                            queryString["txn_type"],
                                                            queryString["payer_email"], //change it to payer id
                                                            queryString["item_number"],
                                                            queryString["ipn_track_id"],
                                                            queryString["subscr_id"],
                                                            queryString["custom"]);
                                    
                                }
                                break;

                            case "subscr_cancel":
                                {
                                    //do not cancel straight away
                                    customer.bActive = false;
                                    customer.dDateCanceled = DateTime.Now;
                                    SendCancellationEmail(customer.cEmail, customer.cFirstName, customer.cLoginName, customer.cPassword, customer.iID.ToString());
                                    
                                    //get dewi notified
                                    SendNotificationEmailToDewi("VVT PayPal : Cancellation", "iCustomerID " + queryString["custom"]);

                                    SaveDatatoPayPalIpnTable(strRequest,
                                                            "CANCEL",
                                                            "",
                                                            receiverEmail,
                                                            "",
                                                            queryString["mc_currency"],
                                                            queryString["txn_type"],
                                                            queryString["payer_email"], //change it to payer id
                                                            queryString["item_number"],
                                                            queryString["ipn_track_id"],
                                                            queryString["subscr_id"],
                                                            queryString["custom"]);
                                }
                                break;
                            case "subscr_signup":
                                {
                                    if (receiverEmail.Equals("spalmer@hq.visionpersonaltraining.com") ||
                                    receiverEmail.Equals("spalmer@visionpt.com.au") ||
                                    receiverEmail.Equals("admin@visionpt.com.au"))
                                    {
                                        customer.bActive = true;
                                        customer.cPassword = queryString["ipn_track_id"];
                                        customer.dDateTerminate = DateTime.Now.AddDays(42);

                                        //create member in umbraco
                                        if (System.Web.Security.Membership.FindUsersByName(customer.cLoginName).Count == 0)
                                        {
                                            CreateMemberInUmbraco(customer.cFirstName + " " + customer.cLastName, customer.cEmail, queryString["ipn_track_id"], customer.cLoginName);
                                        }

                                        //send email confirmation
                                        SendEmail(customer.cEmail, customer.cFirstName, customer.cLoginName, queryString["ipn_track_id"]);
                                        
                                        //get dewi notified
                                        SendNotificationEmailToDewi("VVT PayPal : Sign Up", "iCustomerID:" + queryString["custom"]);
                                    }
                                    SaveDatatoPayPalIpnTable(strRequest,
                                                            "SIGN UP",
                                                            "",
                                                            receiverEmail,
                                                            "",
                                                            "",
                                                            queryString["txn_type"],
                                                            queryString["payer_email"], //change it to payer id
                                                            queryString["item_number"],
                                                            queryString["ipn_track_id"],
                                                            queryString["subscr_id"],
                                                            queryString["custom"]);
                                }
                                break;

                            default:
                                SaveDatatoPayPalIpnTable(strRequest, strResponse, "default case", "", "", "", "", "", "1", "", "", "1");

                                //get dewi notified
                                SendNotificationEmailToDewi("VVT PayPal : Went to nowhere", "Request String: <br/>" + strRequest);
                                break;
                        }
                        cvdc.SubmitChanges();
                    }
                }
                catch (Exception ex)
                {
                    SaveDatatoPayPalIpnTable(strRequest, "", "", queryString["receiver_email"], "",
                        "", queryString["txn_type"], queryString["payer_email"], "1234", "", "", "1234", ex.ToString());

                    if(queryString["item_number"].Equals("191919"))
                    {
                        BP2for10SaveAndSendEmail(queryString["payer_email"]);
                    }
                    //get dewi notified
                    SendNotificationEmailToDewi("VVT PayPal : Went to exception", "Exception: <br/>" + strRequest);
                }
            }
            else if (strResponse == "INVALID")
            {
                SaveDatatoPayPalIpnTable(strRequest, strResponse, "", "", "", "", "", "", "1", "", "", "1");
            }
        }

        protected void SendEmail(string toEmail, string firstName, string userName, string password)
        {
            try
            {
                string ToEmail = toEmail;

                string fromEmail = ConfigurationManager.AppSettings["clubVisionPreSubscriber"];

                const string subject = "Welcome to Vision Virtual Training";

                string htmlemailplain = "<h1>test</h1>";

                string htmlemail = File.ReadAllText(Server.MapPath("/services/templates/newmember-vvt.htm"));
                htmlemail = htmlemail.Replace("<!--FirstName-->", firstName);
                htmlemail = htmlemail.Replace("<!--UserName-->", userName);
                htmlemail = htmlemail.Replace("<!--Password-->", password);

                var ees = new VPTFacilities();

                ees.MailExternal(fromEmail, ToEmail, subject, htmlemail, false, true, null, null);

            }
            catch
            {
                SendNotificationEmailToDewi("VVT : Send Email Failed", "Welcome email failed to send to" + userName);
            }
        }

        protected void SendCancellationEmail(string toEmail, string firstName, string userName, string password, string memberid)
        {
            try
            {
                string ToEmail = toEmail;

                string fromEmail = ConfigurationManager.AppSettings["clubVisionPreSubscriber"];

                const string subject = "Vision Virtual Training Cancellation";

                string htmlemail = File.ReadAllText(Server.MapPath("/services/templates/cancellation.htm"));
                htmlemail = htmlemail.Replace("<!--FirstName-->", firstName);
                htmlemail = htmlemail.Replace("<!--UserName-->", userName);
                htmlemail = htmlemail.Replace("<!--Password-->", password);
                htmlemail = htmlemail.Replace("<!--memberid-->", memberid);

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
                var ees = new VPTFacilities();

                ees.MailExternal(fromEmail, ToEmail, subject, htmlemail, false, true, null, null);

            }
            catch
            {
                SendNotificationEmailToDewi("VVT : Send Email Failed", "Cancellation email failed to send to" + userName);
            }
        }

        protected bool IsTxnidHasNotBeenProcessed(string txnid)
        {
            var cvdc = new ClubVisionDataContext();

            var txnidInDb = (from checkTxn in cvdc.PayPalIPNs
                             where checkTxn.cTXNID == txnid
                             select checkTxn).Any();

            return txnidInDb;
        }

        protected bool IsPaidAmountCorrect(string amount, string subscriberID)
        {
            /*
            var cvdc = new ClubVisionDataContext();

            var sbcrID = (from sbcrIDs in cvdc.PayPalIPNs
                          where sbcrIDs.cSubscriberId == subscriberID
                          where sbcrIDs.cTransactionType == "subscr_payment"
                          select sbcrIDs);
            
            return (sbcrID.Any() && amount.Equals("29.00")) ||
                    ((!sbcrID.Any()) && amount.Equals("0.00")) ||
                    ((!sbcrID.Any()) && amount.Equals("1.00"));
            
            */
            return true;
        }

        protected void SaveDatatoPayPalIpnTable(string rawRequest, string paymentStatus, string txnID, string receiverEmail,
            string paymentAmount, string paymentCurrency, string transactionType, string payerEmail, string itemNumber,
            string ipnTrackId, string subscriberID, string customerId, string exception = null)
        {
            ClubVisionDataContext cvdc = new ClubVisionDataContext();

            var payPalIpn = new PayPalIPN
            {
                dDateReceived = DateTime.Now,
                cRawRequest = rawRequest,
                cPaymentStatus = paymentStatus,
                cTXNID = txnID,
                cReceiverEmail = receiverEmail,
                cPaymentAmount = paymentAmount,
                cPaymentCurrency = paymentCurrency,
                cTransactionType = transactionType,
                cPayerEmail = payerEmail,
                iItemNumber = int.Parse(itemNumber),
                cIpnTrackId = ipnTrackId,
                cSubscriberId = subscriberID,
                iCustomerID = int.Parse(customerId)
            };

            if (exception != null)
            {
                payPalIpn.cException = exception;
            }

            cvdc.PayPalIPNs.InsertOnSubmit(payPalIpn);
            cvdc.SubmitChanges();
        }

        public void CreateMemberInUmbraco(string name, string email, string password, string loginname)
        {
            MemberType memberType = MemberType.GetByAlias("ClubVisionExternal");
            MemberGroup memberGroup = MemberGroup.GetByName("ClubVisionExternal");

            Member newMember = Member.MakeNew(name, memberType, new umbraco.BusinessLogic.User(0));

            newMember.Email = email;
            newMember.Password = password;
            newMember.LoginName = loginname;
            newMember.AddGroup(memberGroup.Id);

            newMember.Save();

        }

        protected void Button1Click(object sender, EventArgs e)
        {
            SendCancellationEmail(TextBox1.Text, "Dewi", "hahahah", "hihihi", "2000000");
        }

        protected void SendNotificationEmailToDewi(string subject, string htmlbody)
        {
            try
            {
                const string ToEmail = "dewi@hq.visionpersonaltraining.com";

                string fromEmail = ConfigurationManager.AppSettings["clubVisionPreSubscriber"];

                var ees = new VPTFacilities();

                ees.MailExternal(fromEmail, ToEmail, subject, htmlbody, false, true, null, null);

            }
            catch
            {
                Response.Write("fail");
            }
        }

        protected void BP2for10SaveAndSendEmail(string email)
        {
            string bodyMessage = "Payment Status : Waiting for Payment<br/>" +
                                 "Email : " + email + "<br/>";

            VPTFacilities vptf = new VPTFacilities();

            vptf.MailWithoutCCEnq(email, "abracks@hq.visionpersonaltraining.com", "PAID : Business Plaza $10 for 2 PT Sessions", bodyMessage, false, true);

        }
    }
}