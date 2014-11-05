using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace VisionPersonalTrainingProject.usercontrols.lightboxes
{
    public partial class BookConsultationLightbox : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
            string MessageBody;
            string FromEmail = tbEmail.Text;

            int StudioId;
            int.TryParse(ddlStudio.SelectedValue, out StudioId);

            MessageBody = "Book a Consultation Applicant:<br/><br/>";
            MessageBody += "From: " + tbFirstName.Text + " " + tbSurname.Text + "<br/>";
            MessageBody += "Mobile: " + tbMobile.Text + "<br/>";
            MessageBody += "Email: " + tbEmail.Text + "<br/>";
            MessageBody += "Studio: " + ddlStudio.SelectedItem.Text + "<br/>";
            MessageBody += "Comments: " + tbComments.Text;


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
                enquiry.FirstName = tbFirstName.Text;
                enquiry.LastName = tbSurname.Text;
                enquiry.MobilePhone = tbMobile.Text;
                enquiry.Email = tbEmail.Text;
                enquiry.StudioID = StudioId;
                enquiry.Note = tbComments.Text;
                //enquiry.LightBoxID

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
    }
}