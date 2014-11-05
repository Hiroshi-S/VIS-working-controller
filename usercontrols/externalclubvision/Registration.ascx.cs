using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using Recaptcha.Web;
using VisionPersonalTrainingProject.usercontrols.general;

/*
 *   [1] Dewi 16/01/14  -- Change Paypal address to old paypal admin@visionpt.com.au
 *   [2] Dewi 16/01/14  -- Add Recaptcha to this page
 *   [3] Dewi 12/02/14  -- Change Paypal backto spalmer@visionpt.com.au
 */
namespace VisionPersonalTrainingProject.usercontrols.externalclubvision
{
    public partial class Registration : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Request.IsSecureConnection)
            {
                Response.Redirect("http://www.visionpt.com.au/vision-virtual-training/join-now-registration/");
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    if (Request.Browser.Browser.Equals("IE"))
                    {
                        RequiredFieldValidator1.InitialValue = "First Name";
                        RequiredFieldValidator2.InitialValue = "Last Name";
                        RequiredFieldValidator4.InitialValue = "Email";
                        RequiredFieldValidator5.InitialValue = "Confirm Email";
                    }
                }
            }
        }

        protected void ImageButton1Click(object sender, ImageClickEventArgs e)
        {
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            VisionPersonalTrainingProject.VOSWebService.Service service = new VisionPersonalTrainingProject.VOSWebService.Service();
            service.AuthenticationHeaderValue = new VisionPersonalTrainingProject.VOSWebService.AuthenticationHeader();
            service.AuthenticationHeaderValue.UserName = "vosABC";
            service.AuthenticationHeaderValue.Password = "vosABCpass1";

            if (CheckBox3.Checked && CheckBox4.Checked && lblForEmail.Text.Equals(""))
            {
                string loginName = ti.ToLower(txtFirstName.Text + txtSurname.Text);
                bool isLoginNotInVOS = service.GetMemberLoginAvailability(loginName, 1).LoginAvailable;

                if (String.IsNullOrEmpty(recaptcha.Response))
                {
                    LabelValidation.Text = "Captcha cannot be empty."; //[2]
                }
                else
                {
                    RecaptchaVerificationResult result = recaptcha.Verify(); //[2]

                    if (result == RecaptchaVerificationResult.Success) //[2]
                    {
                        //if succeed
                        if (IsLoginNotInExt(loginName) && isLoginNotInVOS)
                        {
                            string custid = SavingDatatoCustomerTable(loginName, ti.ToTitleCase(txtFirstName.Text), ti.ToTitleCase(txtSurname.Text),
                                                        txtEmail.Text, DropDownList1.SelectedItem.Text, ti.ToUpper(txtVoucher.Text));

                            SendThemToPayPal(custid, ConvertVoucherCode(txtVoucher.Text));

                        }
                        else
                        {
                            regodiv.Style["display"] = "none";
                            usernamediv.Style["display"] = "block";
                            pbTarget.Style["display"] = "none";
                        }
                    }
                    if (result == RecaptchaVerificationResult.IncorrectCaptchaSolution) //[2]
                    {
                        LabelValidation.Text = "Incorrect captcha response.";
                    }
                    else
                    {
                        LabelValidation.Text = "Some other problem with captcha.";
                    }
                }
                LabelValidation.Visible = true;

            }
            else
            {
                LabelValidation.Visible = true;
            }
        }

        protected void ImageButton2Click(object sender, ImageClickEventArgs e)
        {
            if (IsLoginNotInExt(txtUserName.Text))
            {
                string custid = SavingDatatoCustomerTable(txtUserName.Text, txtFirstName.Text, txtSurname.Text,
                                              txtEmail.Text, DropDownList1.SelectedItem.Text, txtVoucher.Text);

                SendThemToPayPal(custid, ConvertVoucherCode(txtVoucher.Text));
            }
            else
            {
                Label3.Text = "Sorry, this username is also not available, please choose again.";
            }
        }

        protected string SavingDatatoCustomerTable(string loginName, string fname, string lname,
            string email, string source, string voucherCode)
        {
            var cvedc = new ClubVisionDataContext();

            var customer = new Customer_External()
            {
                cLoginName = loginName,
                cPassword = "1234",
                cFirstName = char.ToUpper(fname[0]) + fname.Substring(1),
                cLastName = char.ToUpper(lname[0]) + lname.Substring(1),
                cEmail = email,
                cSource = source,
                cVoucherCode = ConvertVoucherCode(voucherCode),
                dDateCaptured = DateTime.Now,
                bActive = false,
                bCompleteInitialState = false,
                dDateTerminate = DateTime.Now
            };

            if (!string.IsNullOrEmpty(voucherCode))
            {
                customer.cVoucherCode = ConvertVoucherCode(voucherCode);
            }

            if (DropDownList1.SelectedValue.Equals("11"))
            {
                customer.cSource += " -- " + txtOther.Text;
            }

            cvedc.Customer_Externals.InsertOnSubmit(customer);
            cvedc.SubmitChanges();

            return customer.iID.ToString(CultureInfo.InvariantCulture);
        }

        protected bool IsLoginNotInExt(string loginName)
        {
            try
            {
                //MembershipUser user = Membership.GetUser(loginName);
                //return user == null;
                ClubVisionDataContext cvdc = new ClubVisionDataContext();
                var cust = (from cs in cvdc.Customer_Externals
                            where cs.cLoginName == loginName
                            where cs.dDateTerminate > DateTime.Now
                            select cs).Count();

                if (cust == 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        protected string ConvertVoucherCode(string voucher)
        {
            if (voucher.Equals("Voucher code. Do you have one?"))
            {
                return "";
            }
            else
            {
                return voucher.ToUpper();
            }

        }

        protected void SendThemToPayPal(string custId, string voucherCode)
        {
            string redirect;

            voucherCode = voucherCode.ToUpper();

            var cvdc = new ClubVisionDataContext();

            var codes = (from cds in cvdc.VVTPromotionalCodes
                         where cds.bActive
                         where cds.cCode.Equals(voucherCode)
                         select cds).SingleOrDefault();

            if (codes != null)
            {
                if (voucherCode.Equals("WW01"))
                {
                    WeightWatcherTrial(Convert.ToInt32(custId));
                    redirect = "/thank-you-page/";
                }
                else
                {
                    redirect = codes.PayPalLink.cPayPalLink + "&custom=" + custId;
                }
            }
            else
            {
                //default paypal where deafult at $29 from the start
                //redirect = "https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=UW37EE5DT23HJ&custom=" + custId; // [1]
                redirect = "https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=3PKPNAY5QSH8U&custom=" + custId; // [3]
            }

            Response.Redirect(redirect);
        }

        protected void WeightWatcherTrial(int custid)
        {
            try
            {
                ClubVisionDataContext cvdc = new ClubVisionDataContext();

                int countcode = (from cstt in cvdc.Customer_Externals
                                 where cstt.cVoucherCode.Equals("ww01") || cstt.cVoucherCode.Equals("WW01")
                                 where !cstt.cPassword.Equals("1234")
                                 select cstt.iID).Count();

                var cust = (from cst in cvdc.Customer_Externals
                            where cst.iID == custid
                            select cst).SingleOrDefault();

                if (DateTime.Now < Convert.ToDateTime("30/12/2013") && countcode <= 30)
                {
                    if (cust != null)
                    {
                        cust.dDateTerminate = cust.dDateTerminate.AddDays(30);
                        cust.cPassword = "wwatcher2013";
                        cust.cVoucherCode = "WW01";
                    }

                    cvdc.SubmitChanges();

                    if (cust != null)
                    {
                        SendEmailWeightWatcher(cust.cEmail, cust.cFirstName, cust.cLoginName, cust.cPassword);
                    }
                }
                else
                {
                    if (cust != null)
                    {
                        SendEmailWeightWatcherNotValid(cust.cEmail, cust.cFirstName);
                    }
                }

            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }

        protected void SendEmailWeightWatcher(string toEmail, string firstName, string userName, string password)
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
                Response.Write("fail");
            }
        }

        protected void SendEmailWeightWatcherNotValid(string toEmail, string firstName)
        {
            try
            {
                string ToEmail = toEmail;

                string fromEmail = ConfigurationManager.AppSettings["clubVisionPreSubscriber"];

                const string subject = "Vision Virtual Training Code Invalid";

                string htmlemailplain = "<h1>Hi " + firstName + ",</h1><br/>" +
                                        "Apology that this code is no longer valid, either it has passed 30 Dec 2013 or it has reached the maximum of 30 users.<br/><br/>" +
                                        "Regards,<br/>" +
                                        "Vision Virtual Training Team";

                var ees = new VPTFacilities();

                ees.MailExternal(fromEmail, ToEmail, subject, htmlemailplain, false, true, null, null);

            }
            catch
            {
                Response.Write("fail");
            }
        }

        protected void txtEmail_Changed(object sender, EventArgs e)
        {
            /* VisionPersonalTrainingProject.VOSWebService.Service service = new VisionPersonalTrainingProject.VOSWebService.Service();
             service.AuthenticationHeaderValue = new VisionPersonalTrainingProject.VOSWebService.AuthenticationHeader();
             service.AuthenticationHeaderValue.UserName = "vosABC";
             service.AuthenticationHeaderValue.Password = "vosABCpass1";
             bool isEmailNotInVOS = service.GetMemberLoginAvailability(txtEmail.Text, 2).LoginAvailable;
             */

            var cvedc = new ClubVisionDataContext();

            var checkEmail = (from emails in cvedc.Customer_Externals
                              where emails.cEmail == txtEmail.Text
                              where emails.dDateTerminate > DateTime.Now || (emails.dDateCanceled != null && !emails.cPassword.Equals("1234"))
                              select emails);

            if (checkEmail.Any()) //if (checkEmail.Any() || !isEmailNotInVOS)
            {
                lblForEmail.Text = "<br/>This email is already being used<br/>";
                lblForEmail.Visible = true;
            }
            else
            {
                lblForEmail.Text = "";
                lblForEmail.Visible = false;
            }

            cvedc.Dispose();
        }

        protected void txtVoucher_Changed(object sender, EventArgs e)
        {
            if (!txtVoucher.Text.Equals(""))
            {
                var cvdc = new ClubVisionDataContext();

                var codes = (from cds in cvdc.VVTPromotionalCodes
                             where cds.bActive
                             where cds.cCode.Equals(txtVoucher.Text.ToUpper())
                             select cds.iID);

                if (codes.Any())
                {
                    LabeltxtVoucher.Text = "<br/>Voucher code is accepted<br/>";
                    LabeltxtVoucher.ForeColor = System.Drawing.Color.Green;
                    LabeltxtVoucher.Visible = true;
                }
                else
                {
                    LabeltxtVoucher.Text = "<br/>This code is invalid<br/>";
                    LabeltxtVoucher.ForeColor = System.Drawing.Color.Red;
                    LabeltxtVoucher.Visible = true;
                }

                cvdc.Dispose();
            }
            else
            {
                LabeltxtVoucher.Text = "";
                LabeltxtVoucher.ForeColor = System.Drawing.Color.Green;
                LabeltxtVoucher.Visible = false;
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedValue.Equals("11"))
            {
                txtOther.Visible = true;
                RequiredFieldValidator6.Enabled = true;
            }
            else
            {
                txtOther.Visible = false;
                RequiredFieldValidator6.Enabled = false;
            }
        }
    }
}