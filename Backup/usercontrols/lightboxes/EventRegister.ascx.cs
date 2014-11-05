using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace VisionPersonalTrainingProject.usercontrols.lightboxes
{
    public partial class EventRegister : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            

            if (!Page.IsPostBack)
            {
                if(Request.QueryString["tab"] != "")
                    literalBack.Text = "<a class=\"backToList\" href=\"/club-vision/whats-on/?tab=" + Request.QueryString["tab"] + "\">&lt; Back to list</a>";
                    
                //get event name from querystring
                string EventName = Request.QueryString["event"];
                if (string.IsNullOrEmpty(EventName))
                {
                    lblHeader.Text="Error: No event passed.";
                    btnBook.Enabled=false;
                }
                else
                    lblHeader.Text=EventName;
            }
        }

 


        //needs studio id to be populated before will work
        protected void btnBook_Click(object sender, EventArgs e)
        {
            try
            {
                lblResult.Text = "";
                string MessageBody;
                string FromEmail = tbEmail.Text;
                int StudioId = (int) Session["StudioId"]; 
                MessageBody = "Registration for: " + lblHeader.Text + "<br/>";
                MessageBody = "From: " + tbFirstName.Text + " " + tbSurname.Text + "<br/>";
                MessageBody += "Mobile: " + tbMobile.Text + "<br/>";
                MessageBody += "Email: " + tbEmail.Text + "<br/>";
                MessageBody += "Comments: " + tbComments.Text;

                VisionPersonalTrainingProject.VOSWebService.Service service = new VisionPersonalTrainingProject.VOSWebService.Service();
                service.AuthenticationHeaderValue = new VisionPersonalTrainingProject.VOSWebService.AuthenticationHeader();
                service.AuthenticationHeaderValue.UserName = "vosABC";
                service.AuthenticationHeaderValue.Password = "vosABCpass1";

                VOSWebService.stEventUpdate registerEvent = new VOSWebService.stEventUpdate();

                //registerEvent.Category = categoryId;
                //registerEvent.EventDate = eventDate;
                registerEvent.EventID = int.Parse(this.Request.QueryString["vosId"]);
                //registerEvent.EventTime = eventDate; // TODO: time is currently a string - fix before go live
                //registerEvent.EventTitle = tbEventName.Text;
                //registerEvent.Information = tbInformation.Text;
                //registerEvent.MeetingPlace = tbMeetingPlace.Text;
                registerEvent.MemberNo = (int) Session["MemberNo"];
                //registerEvent.Place = tbPlace.Text;
                //registerEvent.RegisterID = "";
                registerEvent.StudioID = (int)Session["StudioId"];

                registerEvent = service.SubmitEventRegister(registerEvent);

                //VPTFacilities mailObj = new VPTFacilities();

                //mailObj.MailStudio(StudioId, FromEmail, "Booking consultation enquiry", MessageBody, false, true);
                //mailObj = null;

                lblResult.Text = "Successfully registered";
            }
            catch(Exception ex)
            {
                lblResult.Text = "Error in registering.  Error: " + ex.Message;
            }
        }
    }
}