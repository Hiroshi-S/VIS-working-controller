using System;
using System.Linq;

namespace VisionPersonalTrainingProject.usercontrols.general
{
    public partial class NewsletterSubscriber : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ClubVisionDataContext cvedc = new ClubVisionDataContext();

                var countryList = (from cl in cvedc.Countries
                                   select cl.name_en);

                DropDownListCountry.DataSource = countryList;
                DropDownListCountry.DataBind();

                DropDownListCountry.SelectedValue = "Australia";
                cvedc.Dispose();
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            bool isSaved = SaveToDatabase();
            if (isSaved)
            {
                showMessage.Visible = true;
                showForm.Visible = false;
            }
            else
            {
                showMessage.Visible = false;
                showForm.Visible = true;
            }

        }
        protected bool SaveToDatabase()
        {
            try
            {
                ClubVisionDataContext cvdc = new ClubVisionDataContext();

                NewsletterSubscriberTable ns = new NewsletterSubscriberTable();

                ns.Name = TextBoxName.Text;
                ns.Email = TextBoxEmail.Text;
                ns.DateCaptured = DateTime.Now;
                ns.Country = DropDownListCountry.SelectedValue;

                cvdc.NewsletterSubscriberTables.InsertOnSubmit(ns);               
                cvdc.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}