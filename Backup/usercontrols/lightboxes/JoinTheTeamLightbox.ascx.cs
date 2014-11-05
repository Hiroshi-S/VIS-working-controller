using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

namespace VisionPersonalTrainingProject.usercontrols.lightboxes
{
    public partial class JoinTheTeamLightbox : System.Web.UI.UserControl
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
                 */
            }
        }


        protected override void Render(HtmlTextWriter writer)
        {
            string validatorOverrideScripts = "<script src=\"/scripts/validators.js\" type=\"text/javascript\"></script>";
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ValidatorOverrideScripts", validatorOverrideScripts, false);
            base.Render(writer);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            foreach (ListItem item in ddlStudio.Items)
            {
                if (item.Selected)
                {
                    string MessageBody;
                    string FromEmail = tbEnqEmail.Text;
                    //string ToEmail = ConfigurationManager.AppSettings["jointeamemails"];
                    string Subject;
                    HiddenField hdnPageName = (HiddenField)this.Page.FindControl("hdnPageName");
                    if (hdnPageName != null)
                        Subject = hdnPageName.Value;
                    else
                        Subject = "Vision PT Employment Form";

                    MessageBody = "From: " + tbEnqFirstName.Text + " " + tbEnqSurname.Text + "<br/>";
                    MessageBody += "Phone: " + tbEnqPhone.Text + "<br/>";
                    MessageBody += "Mobile: " + tbEnqMobile.Text + "<br/>";
                    MessageBody += "Email: " + tbEnqEmail.Text + "<br/>";
                    MessageBody += "Address: " + tbEnqAddress.Text + "<br/>";
                    MessageBody += "State: " + ddlEnqState.SelectedValue + "<br/>";
                    MessageBody += "Postcode: " + tbEnqPostcode.Text + "<br/>";
                    MessageBody += "Certificate IV: " + tbEnqCertIV.Text + "<br/>";
                    MessageBody += "Certified Boxing Instructor: " + tbEnqBoxing.Text + "<br/>";
                    MessageBody += "Other Qualifications: " + tbEnqQualifications.Text + "<br/>";
                    MessageBody += "Qualifications from: " + tbEnqQualificationsWhere.Text + "<br/>";
                    MessageBody += "How Did You Hear about role: " + tbEnqHowDidYouHear.Text + "<br/>";
                    MessageBody += "Studio: " + item.Text + "<br/>";
                    MessageBody += "Available to Start: " + tbEnqAvailableToStart.Text + "<br/>";
                    MessageBody += "What do you know about VisionPT: " + tbEnqVisionPT.Text + "<br/>";
                    MessageBody += "Why Personal Trainer: " + tbEnqWhyPersonalTrainer.Text + "<br/>";
                    MessageBody += "Why Vision PT: " + tbEnqWhyVisionPT.Text + "<br/>";
                    MessageBody += "Goals Next Year: " + tbEnqGoalsNextYear.Text + "<br/>";
                    MessageBody += "Goals Next 3 Years: " + tbEnqGoalsNext3Years.Text + "<br/>";
                    MessageBody += "Early Starts, Late Finishes: " + tbEnqEarlyStartsLateFinishes.Text + "<br/>";
                    MessageBody += "How do you keep yourself fit: " + tbEnqFit.Text + "<br/>";
                    MessageBody += "Hours Available: " + tbEnqHoursAvailable.Text + "<br/>";
                    MessageBody += "Other Commitments: " + tbEnqOtherCommitments.Text + "<br/>";
                    MessageBody += "What Do You Offer: " + tbEnqWhatDoYouOffer.Text + "<br/>";

                    HttpPostedFile file = fileUploadResume.PostedFile;

                    int StudioId;
                    int.TryParse(item.Value, out StudioId);

                    VPTFacilities mailObj = new VPTFacilities();
                    //join the team enquiry type =2
                    mailObj.MailStudio(StudioId, 2, FromEmail, Subject, MessageBody, false, true);
                    mailObj = null;
                }
            }

        }


    }
}