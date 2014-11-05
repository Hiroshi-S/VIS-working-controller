using System;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.UI;
using VisionPersonalTrainingProject.usercontrols.VVT;

namespace VisionPersonalTrainingProject.usercontrols.lightboxes
{
    public class FBData
    {
        public string name { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string code { get; set; }
        public string hearboutus { get; set; }
        public string years { get; set; }
        public string tandc { get; set; }
        public string mobile { get; set; }
        public string studio { get; set; }
    }

    public class FBResponse
    {
        public FBData registration { get; set; }
        public string algorithm { get; set; }
    }

    public partial class FacebookRegistrationLightbox : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["signed_request"] != null)
            {
                string[] requestArray = Request.Form["signed_request"].ToString().Split('.');
                string dataString = base64Decode(requestArray[1]);

                JavaScriptSerializer js = new JavaScriptSerializer();
                FBResponse fb = js.Deserialize<FBResponse>(dataString);

                if(isEmailAvailable(fb.registration.email))
                {
                   
                    var cvedc = new ClubVisionDataContext();

                    var customer = new Customer_External()
                    {
                        cLoginName = getLoginString(fb.registration.name).Replace(" ","").ToLower(),
                        cPassword = "1234",
                        cFirstName = fb.registration.first_name,
                        cLastName = fb.registration.last_name,
                        cEmail = fb.registration.email,
                        cSource = fb.registration.hearboutus,
                        cVoucherCode = fb.registration.code,
                        dDateCaptured = DateTime.Now,
                        bActive = false,
                        bCompleteInitialState = false,
                        dDateTerminate = DateTime.Now
                    };

                    cvedc.Customer_Externals.InsertOnSubmit(customer);
                    cvedc.SubmitChanges();

                    Response.Redirect(Registration.SendThemToPayPal(customer.iID.ToString(), customer.cVoucherCode));
                }
                else
                {
                    Response.Redirect("/vision-virtual-training/join-now-registration?msg=emailfacebookfail");
                }

            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            string validatorOverrideScripts = "<script src=\"/scripts/validators.js\" type=\"text/javascript\"></script>";
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ValidatorOverrideScripts", validatorOverrideScripts, false);
            base.Render(writer);
        }

        public bool isEmailAvailable(string email)
        {
            var cvdc = new ClubVisionDataContext();

            var checkEmail = (from emails in cvdc.Customer_Externals
                              where emails.cEmail == email
                              where emails.dDateTerminate > DateTime.Now || (emails.dDateCanceled != null && !emails.cPassword.Equals("1234"))
                              select emails);

            return !checkEmail.Any();
        }

        public string getLoginString(string login)
        {
            var cvdc = new ClubVisionDataContext();

            var checkEmail = (from logins in cvdc.Customer_Externals
                              where logins.cLoginName == login
                              where logins.dDateTerminate > DateTime.Now || (logins.dDateCanceled != null && !logins.cPassword.Equals("1234"))
                              select logins);

            return checkEmail.Any() ? checkEmail.SingleOrDefault().cEmail : login;

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
    }
}