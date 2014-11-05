using System;
using System.Linq;
using System.Globalization;

namespace VisionPersonalTrainingProject.usercontrols.general
{
    public partial class bp2for10 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //VisionPersonalTrainingProject.VisionStudioDataContext vsdc = new VisionStudioDataContext();
                ClubVisionDataContext cvdc = new ClubVisionDataContext();

                var studioList = (from sll in cvdc.EnumTables
                                  where sll.ID == 11
                                  orderby sll.Value
                                  select sll);

                DropDownStudio.DataSource = studioList;
                DropDownStudio.DataTextField = "Value";
                DropDownStudio.DataValueField = "intValue";
                DropDownStudio.DataBind();
            }
        }

        protected void ImageButton1_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            saveAndSendEmail();
            Response.Redirect("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=573VNG3LZBB56");
        }

        protected void saveAndSendEmail()
        {
            VisionPersonalTrainingProject.ClubVisionDataContext cvdc = new ClubVisionDataContext();
            BusinessPlaza10for2PT bp = new BusinessPlaza10for2PT();

            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            bp.cName = ti.ToTitleCase(textName.Text);
            bp.cEmail = textEmail.Text;
            bp.cMobile = textMobile.Text;
            bp.cSuburb = textSuburb.Text;
            bp.iStudio = Convert.ToInt32(DropDownStudio.SelectedValue);

            cvdc.BusinessPlaza10for2PTs.InsertOnSubmit(bp);
            cvdc.SubmitChanges();

            string bodyMessage = "Payment Status : Waiting for Payment<br/>" +
                                 "Waiting for PayPal Payment : <br/>" +
                                 "Name : " + bp.cName + "<br/>" +
                                 "Email : " + bp.cEmail + "<br/>" +
                                 "Mobile : " + bp.cMobile + "<br/>" +
                                 "Suburb : " + bp.cSuburb + "<br/>" +
                                 "Studio :" + DropDownStudio.SelectedItem.Text;

            VPTFacilities vptf = new VPTFacilities();

            //vptf.MailWithoutCCEnq(bp.cEmail, "abracks@hq.visionpersonaltraining.com" ,"Enquiry : Business Plaza $10 for 2 PT Sessions", bodyMessage, false, true);
            vptf.MailWithoutCCEnq(bp.cEmail, "web@visionpt.com.au", "Enquiry : Business Plaza $10 for 2 PT Sessions", bodyMessage, false, true);

        }

    }
}