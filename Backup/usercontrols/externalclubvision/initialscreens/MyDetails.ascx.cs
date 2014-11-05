using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using umbraco.cms.businesslogic.member;

namespace VisionPersonalTrainingProject.usercontrols.externalclubvision.initialscreens
{
    public partial class MyDetails : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;
            PopulateScreen();

        }

        protected void PopulateScreen()
        {
            var cvdc = new ClubVisionDataContext();

            var customer = (from cust in cvdc.Customer_Externals
                            where cust.iID == Convert.ToInt32(Session["MemberNo"])
                            select cust).FirstOrDefault();
            if ((string)Session["MemberType"] == "VVT")
            {
                if (customer.bCompleteInitialState)
                {
                    stepLabel.Visible = false;
                    Imagebutton1.ImageUrl = "/images/buttonSave.gif";
                }
            }
            var countries = (from cotry in cvdc.Countries
                             select cotry);

            var customerProfile = (from cp in cvdc.PersonalProfile_Externals
                                   where cp.iCustomerID == Convert.ToInt32(Session["MemberNo"])
                                   select cp).FirstOrDefault();

            if (countries.Any())
            {
                CountryddList.DataSource = countries;
                CountryddList.DataBind();
                CountryddList.SelectedValue = "Australia";
            }

            if (customer != null)
            {
                emailText.Text = customer.cEmail;
                fNameText.Text = customer.cFirstName;
                lNameText.Text = customer.cLastName;
            }

            if (customerProfile != null)
            {
                titleDropDownList.SelectedIndex = titleDropDownList.Items.IndexOf(titleDropDownList.Items.FindByText(customerProfile.cTitle));
                genderRadioButtonList.SelectedIndex =
                    genderRadioButtonList.Items.IndexOf(genderRadioButtonList.Items.FindByText(customerProfile.cGender));
                txtDOB.Text = customerProfile.dDOB.ToString("dd/MM/yyyy");
                addressLine1Text.Text = customerProfile.cAddress;
                suburbText.Text = customerProfile.cSuburb;
                stateText.Text = customerProfile.cState;
                postCodeText.Text = customerProfile.iPostcode.ToString(CultureInfo.InvariantCulture);
                CountryddList.SelectedIndex =
                CountryddList.Items.IndexOf(CountryddList.Items.FindByText(customerProfile.cCountry));
                mobileText.Text = customerProfile.cMobile;
                homePhoneText.Text = customerProfile.cHomePhone;

            }
        }

        protected void Imagebutton1_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                SaveToDatabase();
                //Response.Redirect("/ext-club-vision/account/my-profile/edit-profile/?tab=lifestylescreen", false);
                Response.Redirect("/club-vision/my-profile/edit-profile/?alttemplate=Ext%20Edit%20Profile&tab=lifestylescreen", false);
            }
            catch (Exception ex)
            {
                ErrorPopup();
            }
        }

        protected void SaveToDatabase()
        {
            Member m = Member.GetCurrentMember();

            var cvdc = new ClubVisionDataContext();

            var customerProfiles = (from cp in cvdc.PersonalProfile_Externals
                                    where cp.iCustomerID == Convert.ToInt32(Session["MemberNo"])
                                    select cp);

            var customerProfile = new PersonalProfile_External();
            bool isNew = true;

            foreach (PersonalProfile_External customerProfileLU in customerProfiles)
            {
                customerProfile = customerProfileLU;
                isNew = false;
            }

            customerProfile.iCustomerID = Convert.ToInt32(Session["MemberNo"]);
            customerProfile.iUmbracoNodeID = m.Id;
            customerProfile.cFirstName = fNameText.Text;
            customerProfile.cLastName = lNameText.Text;
            customerProfile.cTitle = titleDropDownList.SelectedItem.Text;
            customerProfile.cGender = genderRadioButtonList.SelectedItem.Text;
            customerProfile.dDOB = Convert.ToDateTime(txtDOB.Text);
            customerProfile.iAge = 0;
            customerProfile.cAddress = addressLine1Text.Text;
            customerProfile.cSuburb = suburbText.Text;
            customerProfile.cState = stateText.Text;
            customerProfile.iPostcode = Convert.ToInt32(postCodeText.Text);
            customerProfile.cCountry = CountryddList.SelectedItem.Text;
            customerProfile.cMobile = mobileText.Text;
            customerProfile.cHomePhone = homePhoneText.Text;
            customerProfile.cEmail = emailText.Text;
            customerProfile.dDateModified = DateTime.Now;

            if (isNew)
            {
                customerProfile.dDateCreated = m.CreateDateTime;
                customerProfile.cBodyType = "N/A";
                customerProfile.fTotBodyTypeScore = 0;
                customerProfile.bIsActive = true;
                cvdc.PersonalProfile_Externals.InsertOnSubmit(customerProfile);
            }

            cvdc.SubmitChanges();
        }

        protected void ErrorPopup()
        {
            const string s = "<script type=\"text/javascript\">" +
                                 "document.getElementById('navContainer').style.display = 'none';document.getElementById('title').innerHTML = \"Please Note\";" +
                                 "document.getElementById('h4words').innerHTML = \"All sections need to be completed in full.  Your mobile and home phone numbers however are not mandatory.<br /> " +
                                 "   \";" +
                                 " $(\".contactBox\").css(\"margin-top\", ((($(window).height() - 464) / 2) + $(window).scrollTop() + 0) + \"px\");" +
                                 "$(\"#cErrorPopup\").fadeIn();</script>";

            Page.ClientScript.RegisterStartupScript(GetType(), "test", s);
        }
    }
}