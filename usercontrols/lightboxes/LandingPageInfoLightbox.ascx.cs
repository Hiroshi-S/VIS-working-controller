using System;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.UI;


namespace VisionPersonalTrainingProject.usercontrols.lightboxes
{
    /*
    public class ConnectAuthentication : Facebook.Web.CanvasFBMLBasePage  
    {
        public ConnectAuthentication()
        {

        }

        public static bool isConnected()
        {
            return (SessionKey != null && UserID != -1);
        }

        public static string ApiKey
        {
            get
            {
                return ConfigurationManager.AppSettings["APIKey"];
            }
        }

        public static string SecretKey
        {
            get
            {
                return ConfigurationManager.AppSettings["Secret"];
            }
        }

        public static string SessionKey
        {
            get
            {
                return GetFacebookCookie("session_key");
            }
        }

        public static int UserID
        {
            get
            {
                int userID = -1;
                int.TryParse(GetFacebookCookie("user"), out userID);
                return userID;
            }
        }

        private static string GetFacebookCookie(string cookieName)
        {
            string retString = null;
            string fullCookie = ApiKey + "_" + cookieName;

            if (HttpContext.Current.Request.Cookies[fullCookie] != null)
                retString = HttpContext.Current.Request.Cookies[fullCookie].Value;

            return retString;
        }
    }
    */
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

    public partial class LandingPageInfoLightbox : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {/*
            if (ConnectAuthentication.isConnected())
            {
                FacebookSession ss;
                Facebook.Rest.Api api = new Facebook.Rest.Api(ss);

                api.ApplicationKey = ConnectAuthentication.ApiKey;
                api.SessionKey = ConnectAuthentication.SessionKey;
                api.Secret = ConnectAuthentication.SecretKey;
                api.uid = ConnectAuthentication.UserID;

                //Display user data captured from the Facebook API.  

                Facebook.Schema.user user = api.users.getInfo();
                string fullName = user.first_name + " " + user.last_name;

                lblName.Text = fullName;
            }
            else
            {
                //Facebook Connect not authenticated, proceed as usual.  
            }  
            */
            if (Request.Form["signed_request"] != null)
            {
                string[] requestArray = Request.Form["signed_request"].ToString().Split('.');
                string dataString = base64Decode(requestArray[1]);

                JavaScriptSerializer js = new JavaScriptSerializer();
                FBResponse fb = js.Deserialize<FBResponse>(dataString);

                saveToVOSDatabase(fb.registration.first_name, fb.registration.last_name, fb.registration.mobile,
                                  fb.registration.email,
                                  fb.registration.studio, Convert.ToInt32(fb.registration.studio));

            }
            if (!Page.IsPostBack)
            {
                VisionPersonalTrainingProject.ClubVisionDataContext cvdc = new VisionPersonalTrainingProject.ClubVisionDataContext();

                var studioList = (from stlist in cvdc.EnumTables
                                  where stlist.ID == 11
                                  select stlist).OrderBy(x => x.Value).ToList();

                ddlStudio.DataTextField = "Value";
                ddlStudio.DataValueField = "intValue";
                ddlStudio.DataSource = studioList;
                ddlStudio.DataBind();
                /*
                DataTable dtStudios = VPTFacilities.StudioLocationsGet();
                ddlStudio.DataSource = dtStudios;
                ddlStudio.DataValueField = dtStudios.Columns["StudioID"].ToString();
                ddlStudio.DataTextField = dtStudios.Columns["StudioName"].ToString();
                ddlStudio.DataBind();
                ddlStudio.Items.Insert(0, "");
                ddlStudio.Items[0].Selected = true;
                 * */
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            string validatorOverrideScripts = "<script src=\"/scripts/validators.js\" type=\"text/javascript\"></script>";
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ValidatorOverrideScripts", validatorOverrideScripts, false);
            base.Render(writer);
        }

        protected void btnBook_Click(object sender, EventArgs e)
        {
            saveToVOSDatabase(tbFirstName.Text, tbSurname.Text, tbMobile.Text, tbEmail.Text, ddlStudio.SelectedItem.Text, Convert.ToInt32(ddlStudio.SelectedValue));
        }

        protected void saveToVOSDatabase(string firstName, string lastName, string mobile, string email, string studio, int StudioId)
        {
            string MessageBody;
            string FromEmail = tbEmail.Text;

            MessageBody = "From: " + firstName + " " + lastName + "<br/>";
            MessageBody += "Mobile: " + mobile + "<br/>";
            MessageBody += "Email: " + email + "<br/>";
            MessageBody += "Studio: " + studio + "<br/>";
            MessageBody += "Comments: ";

            VPTFacilities mailObj = new VPTFacilities();
            //book a consultation enquiry type =1
            mailObj.MailStudio(StudioId, 1, FromEmail, "Vision PT Consultation Form", MessageBody, false, true);
            mailObj = null;

            try
            {
                VisionPersonalTrainingProject.VOSWebService.Service service = new VisionPersonalTrainingProject.VOSWebService.Service();
                service.AuthenticationHeaderValue = new VisionPersonalTrainingProject.VOSWebService.AuthenticationHeader();
                service.AuthenticationHeaderValue.UserName = "vosABC";
                service.AuthenticationHeaderValue.Password = "vosABCpass1";

                VOSWebService.stEnquiryUpdate enquiry = new VOSWebService.stEnquiryUpdate();
                enquiry.FirstName = firstName;
                enquiry.LastName = lastName;
                enquiry.MobilePhone = mobile;
                enquiry.Email = email;
                enquiry.StudioID = StudioId;
                enquiry.Note = string.Empty;

                enquiry = service.SubmitEnquiry(enquiry);
            }
            catch
            {
            }

            const string s = "<script type=\"text/javascript\">" +
                             "sendconfirmation();</script>";

            Page.ClientScript.RegisterStartupScript(GetType(), "test", s);

            contactOverlayResult.Text = "<!-- Google Code for Consultation Booking Conversion Page -->\r\n";
            contactOverlayResult.Text += "<script type=\"text/javascript\">\r\n";
            contactOverlayResult.Text += "/* <![CDATA[ */\r\n";
            contactOverlayResult.Text += "var google_conversion_id = 1012493292;\r\n";
            contactOverlayResult.Text += "var google_conversion_language = \"en\";\r\n";
            contactOverlayResult.Text += "var google_conversion_format = \"3\";\r\n";
            contactOverlayResult.Text += "var google_conversion_color = \"ffffff\";\r\n";
            contactOverlayResult.Text += "var google_conversion_label = \"bwxeCLzIyAIQ7Nfl4gM\";\r\n";
            contactOverlayResult.Text += "var google_conversion_value = 0;\r\n";
            contactOverlayResult.Text += "/* ]]> */\r\n";
            contactOverlayResult.Text += "</script>\r\n";
            contactOverlayResult.Text += "<script type=\"text/javascript\" src=\"http://www.googleadservices.com/pagead/conversion.js\">\r\n";
            contactOverlayResult.Text += "</script>\r\n";
            contactOverlayResult.Text += "<noscript>\r\n";
            contactOverlayResult.Text += "<div style=\"display:inline;\">\r\n";
            contactOverlayResult.Text += "<img height=\"1\" width=\"1\" style=\"border-style:none;\" alt=\"\" src=\"http://www.googleadservices.com/pagead/conversion/1012493292/?label=bwxeCLzIyAIQ7Nfl4gM&amp;guid=ON&amp;script=0\"/>\r\n";
            contactOverlayResult.Text += "</div>\r\n";
            contactOverlayResult.Text += "</noscript>\r\n";
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