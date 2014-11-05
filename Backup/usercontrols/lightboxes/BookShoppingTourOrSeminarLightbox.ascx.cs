using System;
using System.Web.UI;
using umbraco.NodeFactory;

namespace VisionPersonalTrainingProject.usercontrols.lightboxes
{
    public partial class BookShoppingTourOrSeminarLightbox : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            VOSWebService.Service service = new VOSWebService.Service();
            service.AuthenticationHeaderValue = new VOSWebService.AuthenticationHeader();
            service.AuthenticationHeaderValue.UserName = "vosABC";
            service.AuthenticationHeaderValue.Password = "vosABCpass1";

            var eshoppingtour = service.GetWhatsOn((int)Session["studioID"], 3);
            ddlShoppingTour.DataSource = eshoppingtour;
            ddlShoppingTour.DataValueField = "WhatsOnDate";
            ddlShoppingTour.DataTextField = "WhatsOnDate";
            ddlShoppingTour.DataBind();

            var eseminars = service.GetWhatsOn((int)Session["studioID"], 2);
            ddlSeminar.DataSource = eseminars;
            ddlSeminar.DataTextField = "WhatsOnDate";
            ddlSeminar.DataValueField = "WhatsOnDate";
            ddlSeminar.DataBind();
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

            MessageBody = "From: " + tbFirstName.Text + " " + tbSurname.Text + "<br/>";
            MessageBody += "Mobile: " + tbMobile.Text + "<br/>";
            MessageBody += "Email: " + tbEmail.Text + "<br/>";
            MessageBody += "Book For: <br/>";

            string whatbook1 = "";
            if(CheckBoxShopTour.Checked)
            {
                whatbook1 += "a shopping tour ";
                MessageBody += "Shopping Tour on " + ddlShoppingTour.SelectedValue + "<br/>";
            }
            string whatbook2 = "";
            if (CheckBoxSeminar.Checked)
            {
                whatbook2 += "a lose fat fast seminar";
                MessageBody += "Seminar on " + ddlSeminar.SelectedValue;
            }

            string and = "";
            if(CheckBoxShopTour.Checked && CheckBoxSeminar.Checked)
            {
                and += " and ";
            }

            Node currentNode = Node.GetCurrent();

            String studioTitle = currentNode.GetProperty("contentTitle").Value;

            VPTFacilities mailObj = new VPTFacilities();
            //book a consultation enquiry type =1
            mailObj.MailStudio((int)Session["studioID"], 1, FromEmail, "Vision PT Shopping Tour and Seminar Booking Form", MessageBody, false, true);
            mailObj = null;

            string s = "<script type=\"text/javascript\">" +
                       "sendconfirmationtofacebook('" + whatbook1 + and + whatbook2 + " with " + studioTitle + "');" +
                        "</script>";

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