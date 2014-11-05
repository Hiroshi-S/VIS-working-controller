using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VisionPersonalTrainingProject.usercontrols.general
{
    public partial class RubyRadar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClubVisionDataContext cvdc = new ClubVisionDataContext();

                var studioList = (from sl in cvdc.EnumTables
                                  where sl.ID == 11
                                  orderby sl.Value
                                  select sl);

                DropDownListStudio.DataSource = studioList;
                DropDownListStudio.DataValueField = "intValue";
                DropDownListStudio.DataTextField = "Value";
                DropDownListStudio.DataBind();

            }
        }

        protected void SaveToDatabase()
        {
            ClubVisionDataContext cvedc = new ClubVisionDataContext();

            StarShotsBday ssb = new StarShotsBday();

            ssb.cStudio = DropDownListStudio.SelectedItem.Text;
            ssb.cName = TextBoxName.Text;
            ssb.cPhone = TextBoxPhone.Text;
            ssb.cEmail = TextBoxEmail.Text;
            ssb.cHealthPriority = TextBoxPriority.Text;

            cvedc.StarShotsBdays.InsertOnSubmit(ssb);
            cvedc.SubmitChanges();
            cvedc.Dispose();
        }

        protected void SendMailToAmanda()
        {
            VPTFacilities ees = new VPTFacilities();

            string emailBody = "Studio : " + DropDownListStudio.SelectedItem.Text +
                               "<br/>Name : " + TextBoxName.Text +
                               "<br/>Phone : " + TextBoxPhone.Text +
                               "<br/>Email : " + TextBoxEmail.Text +
                               "<br/>Health Priority : " + TextBoxPriority.Text;

            ees.StarShotsMail("web@visionpt.com.au", "abracks@visionpt.com.au", "Ruby Radar Enquiry", emailBody, false, true);
        }

        protected void SwitchDiv()
        {
            formPart.Visible = false;

            if(DropDownListOffer.SelectedValue.Equals("1"))
            {
                notificationPart.Visible = true;
            }

            if (DropDownListOffer.SelectedValue.Equals("2"))
            {
                notificationPart2.Visible = true;
            }

        }

        protected void ImageButton1Click(object sender, ImageClickEventArgs e)
        {
            SaveToDatabase();
            SendMailToAmanda();
            SwitchDiv();
        }

        protected void SaveToDatabaseTest()
        {
            ClubVisionDataContext cvedc = new ClubVisionDataContext();

            StarShotsBday ssb = new StarShotsBday();

            ssb.cStudio = DropDownListStudio.SelectedValue.ToString(CultureInfo.InvariantCulture);
            ssb.cName = DropDownListStudio.SelectedItem.Text;
            ssb.cPhone = DropDownListStudio.Text;
            ssb.cEmail = DropDownListStudio.SelectedIndex.ToString();
            ssb.cHealthPriority = DropDownListStudio.SelectedValue;

            cvedc.StarShotsBdays.InsertOnSubmit(ssb);
            cvedc.SubmitChanges();
            cvedc.Dispose();
        }

        protected void DropDownListOfferSelectedIndexChanged(object sender, EventArgs e)
        {
            if(DropDownListOffer.SelectedValue == "1")
            {
                studiotr.Visible = true;
                RequiredFieldValidator1.ValidationGroup = "Starshot";
            }
            else
            {
                studiotr.Visible = false;
                RequiredFieldValidator1.ValidationGroup = "";
            }
        }
    }
}