using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace VisionPersonalTrainingProject.usercontrols.lightboxes
{
    public partial class EnquiryLightbox : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected override void Render(HtmlTextWriter writer)
        {
            string validatorOverrideScripts = "<script src=\"/scripts/validators.js\" type=\"text/javascript\"></script>";
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ValidatorOverrideScripts", validatorOverrideScripts, false);
            base.Render(writer);
        }
        
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string MessageBody;
            string FromEmail = tbEnqEmail.Text;
            string ToEmail = ConfigurationManager.AppSettings["enquiryemails"];
            string Subject;
            HiddenField hdnPageName = (HiddenField)this.Page.FindControl("hdnPageName");
            if (hdnPageName != null)
                Subject = hdnPageName.Value;
            else
                Subject = "Vision PT Franchise Form";

            MessageBody = "From: " + tbEnqFirstName.Text + " " + tbEnqSurname.Text + "<br/>";
            MessageBody += "Phone: " + tbEnqPhone.Text + "<br/>";
            MessageBody += "Mobile: " + tbEnqMobile.Text + "<br/>";
            MessageBody += "Email: " + tbEnqEmail.Text + "<br/>";
            MessageBody += "Address: " + tbEnqAddress.Text + "<br/>";
            MessageBody += "State: " + ddlEnqState.SelectedValue + "<br/>";
            MessageBody += "Postcode: " + tbEnqPostcode.Text + "<br/>";
            MessageBody += "Comments: " + tbEnqComments.Text;

            VPTFacilities mailObj = new VPTFacilities();
            mailObj.Mail(FromEmail, ToEmail, Subject, MessageBody, false, true);
            mailObj = null;
        }

       
    }
}