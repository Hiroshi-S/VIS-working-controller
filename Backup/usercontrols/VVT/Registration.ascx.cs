using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using Recaptcha.Web;

/*
 *   [1] Dewi 16/01/14  -- Change Paypal address to old paypal admin@visionpt.com.au
 *   [2] Dewi 16/01/14  -- Add Recaptcha to this page
 *   [3] Dewi 12/02/14  -- Change Paypal backto spalmer@visionpt.com.au
 */
namespace VisionPersonalTrainingProject.usercontrols.VVT
{
    public partial class Registration : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Request.IsSecureConnection)
            {
                Response.Redirect("http://wwww.visionpt.com.au/vision-virtual-training/join-now-registration/");
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

                    if (Request.QueryString["msg"] != null && Request.QueryString["msg"].Equals("emailfacebookfail"))
                    {
                        // facebooksignup.Visible = false;
                    }
                }
            }
        }

        public static string base64Decode(string data)
        {
            data = data.Replace("-", "+").Replace("_", "/");
            //length of string needs to be in multiple of 4 so that it can be converted to base 64 string
            if (data.Length % 4 != 0)
            {
                while (data.Length % 4 != 0)
                    // character '=' is valueless and used for trailing padding
                    data = data + "=";
            }
            byte[] binary = Convert.FromBase64String(data);
            return Encoding.UTF8.GetString(binary);
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

                            Response.Redirect(SendThemToPayPal(custid, ConvertVoucherCode(txtVoucher.Text)));

                        }
                        else
                        {
                            regodiv.Style["display"] = "none";
                            usernamediv.Style["display"] = "block";
                            pbTarget.Style["display"] = "none";
                        }
                    }

                    LabelValidation.Text = result == RecaptchaVerificationResult.IncorrectCaptchaSolution ? "Incorrect captcha response." : "Some other problem with captcha.";
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

                Response.Redirect(SendThemToPayPal(custid, ConvertVoucherCode(txtVoucher.Text)));
            }
            else
            {
                Label3.Text = "Sorry, this username is also not available, please choose again.";
            }
        }

        public string SavingDatatoCustomerTable(string loginName, string fname, string lname,
            string email, string source, string voucherCode)
        {
            var cvdc = new ClubVisionDataContext();
            int custId = 0;
            string idToReturn;

            var isCustExists = (from custec in cvdc.Customer_Externals
                                where custec.cFirstName.Equals(fname)
                                where custec.cLastName.Equals(lname)
                                where custec.cEmail.Equals(email)
                                select custec).FirstOrDefault();

            if (isCustExists != null)
            {
                //it must be return query with invalid date
                custId = isCustExists.iID;
                isCustExists.cVoucherCode = voucherCode;
                idToReturn = isCustExists.dDateCanceled != null ?
                    "?custid=" + custId + "&msg=C&voucher=" + voucherCode :
                    "?custid=" + custId + "&msg=E&voucher=" + voucherCode;
            }
            else
            {
                var customer = new Customer_External()
                {
                    cLoginName = loginName.Replace(" ", ""),
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

                cvdc.Customer_Externals.InsertOnSubmit(customer);
                cvdc.SubmitChanges();

                custId = customer.iID;
                idToReturn = custId.ToString();
            }

            var custExEnv = (from cee in cvdc.CustomerExternalRegEnvironments
                             where cee.iCustomerID == custId
                             select cee).FirstOrDefault();

            if (custExEnv != null)
            {
                custExEnv.iCount = custExEnv.iCount + 1;
                custExEnv.dDateLastRetry = DateTime.Now;
            }
            else
            {
                var custExEnvNew = new CustomerExternalRegEnvironment();
                HttpBrowserCapabilities browser = Request.Browser;

                custExEnvNew.iCustomerID = custId;
                custExEnvNew.cBrowserType = browser.Type;
                custExEnvNew.cBrowserName = browser.Browser;
                custExEnvNew.cBrowserVersion = browser.Version;
                custExEnvNew.cPlatform = browser.Platform;
                custExEnvNew.bSupportsCookies = browser.Cookies;
                custExEnvNew.cSupportsJavaScript = browser.EcmaScriptVersion.ToString();
                custExEnvNew.bSupportJavaApplets = browser.JavaApplets;
                custExEnvNew.bSupportsActivexControls = browser.ActiveXControls;
                custExEnvNew.cSupportsJavascriptVersion = browser["JavaScriptVersion"];
                custExEnvNew.dDateLastRetry = DateTime.Now;
                custExEnvNew.iCount = 1;

                cvdc.CustomerExternalRegEnvironments.InsertOnSubmit(custExEnvNew);
            }

            cvdc.SubmitChanges();

            return idToReturn;
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

        public static string SendThemToPayPal(string custId, string voucherCode)
        {
            string redirect;

            if (custId.Contains("voucher"))
            {
                redirect = "/vision-virtual-training/registration-duplication/" + custId;
            }
            else
            {
                voucherCode = voucherCode.ToUpper();

                var cvdc = new ClubVisionDataContext();

                var codes = (from cds in cvdc.VVTPromotionalCodes
                             where cds.bActive
                             where cds.cCode.Equals(voucherCode)
                             select cds).SingleOrDefault();

                if (codes != null)
                {
                    redirect = codes.PayPalLink.cPayPalLink + "&custom=" + custId;
                }
                else
                {
                    //--------------default paypal where deafult at $29 from the start
                    //--------------redirect = "https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=UW37EE5DT23HJ&custom=" + custId; // [1]
                    redirect = "https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=3PKPNAY5QSH8U&custom=" + custId; // [3]
                }
            }


            return redirect;
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
                              //where emails.dDateTerminate > DateTime.Now || 
                              //(emails.dDateCanceled != null && !emails.cPassword.Equals("1234"))
                              select emails);
            var checkEmail1 =
                checkEmail.Where(x => !x.cPassword.Equals("1234") && x.dDateTerminate > DateTime.Now).Select(x => x.iID);
            if (checkEmail1.Any())
            {
                lblForEmail.Text = "<br/>You have an active account with us. <br/>Click <a href=\"/vision-virtual-training/login/\">here</a> to login.<br/>";
                lblForEmail.Visible = true;
            }

            var checkEmail2 =
                checkEmail.Where(
                    x => x.dDateCanceled != null && x.dDateTerminate < DateTime.Now && !x.cPassword.Equals("1234")).
                    Select(x => x.iID);
            if (checkEmail2.Any())
            {
                lblForEmail.Text = "<br/>You had an account with us. <br/>Click <a href=\"/vision-virtual-training/registration-duplication/?custid=" + checkEmail2.FirstOrDefault() + "&msg=C\">here</a> to reactivate it.<br/>";
                lblForEmail.Visible = true;
            }

            if (!checkEmail.Any()) //if (checkEmail.Any() || !isEmailNotInVOS)
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

        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            //  firstdiv.Visible = false;
            //  FBReg.Visible = true;
        }
    }
}