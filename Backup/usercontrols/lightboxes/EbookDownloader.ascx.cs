using System;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VisionPersonalTrainingProject.usercontrols.lightboxes
{
    public partial class EbookDownloader : System.Web.UI.UserControl
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
            bool isSaved = SaveToDatabase();

            if (isSaved)
            {
                Response.Redirect("http://www.amazon.com/Ready-Steps-Better-Health-ebook/dp/B00BWHJI26/ref=sr_1_6?ie=UTF8&qid=1368500300&sr=8-6&keywords=ready+set+go", false);
                
            }
        }

        protected bool SaveToDatabase()
        {
            try
            {
                ClubVisionDataContext cvdc = new ClubVisionDataContext();

                var ebd = new ReadySetGoDownloader {Name = tbEnqFirstName.Text, Email = tbEnqEmail.Text, DateCaptured = DateTime.Now};

                cvdc.ReadySetGoDownloaders.InsertOnSubmit(ebd);
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