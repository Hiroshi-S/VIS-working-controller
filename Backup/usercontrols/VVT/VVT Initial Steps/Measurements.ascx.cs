using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VisionPersonalTrainingProject.usercontrols.lightboxes;
using Image = System.Drawing.Image;

namespace VisionPersonalTrainingProject.usercontrols.VVT.VVT_Initial_Steps
{
    public partial class Measurements : System.Web.UI.UserControl
    {
        private DateTime _lastRecord;
        private Measurement _lastMeasured;
        public static string _custGender;

        //for table specific goal
        public static string _waistToHipRatioCurrent;
        public static string _bloodPressCurrentSystolic;
        public static string _bloodPressCurrentDiastolic;

        protected void Page_Load(object sender, EventArgs e)
        {
            var cvdc = new ClubVisionDataContext();

            var cms = (from cm in cvdc.Measurements
                       where cm.iCustomerID == Convert.ToInt32(Session["MemberNo"])
                       select cm).OrderByDescending(x => x.dMeasured).FirstOrDefault();
            string currentUrl = HttpContext.Current.Request.Url.AbsolutePath;
            if (currentUrl != "/club-vision/my-journey/")measHeaderButton.Visible = false;
                //StepLabel.Text = "";
            
            if (cms != null)
            {
                //if ((string)Session["MemberType"] == "VVT")
                //{
                //    if (cms.bIsMetric == false)
                //        setUnitLabels("False");
                //    else if (cms.bIsMetric == true)
                //        setUnitLabels("True");
                //}
                _lastRecord = (DateTime)cms.dMeasured;
                _lastMeasured = cms;
                //for table specific goal
                //String.Format("{0:0.00}", double someValue);
                _waistToHipRatioCurrent = String.Format("{0:0.00}", cms.fWaistToHipRatio);
                if (cms.fBPSystolic != null && cms.fBPDiastolic != null)
                {
                    _bloodPressCurrentSystolic = cms.fBPSystolic.ToString();
                    _bloodPressCurrentDiastolic = cms.fBPDiastolic.ToString();
                }
                else
                {
                    _bloodPressCurrentSystolic = string.Empty;
                    _bloodPressCurrentDiastolic = string.Empty;
                }
            }

            if (!Page.IsPostBack)
            {
                _custGender = (from cu in cvdc.PersonalProfile_Externals
                               where cu.iCustomerID == Convert.ToInt32(Session["MemberNo"])
                               select cu.cGender).SingleOrDefault();
                HiddenField1.Value = _custGender;

                PopulateScreen(cms);

                var photoFlickr = (from pf in cvdc.Measurements
                                   where pf.iCustomerID == Convert.ToInt32(Session["MemberNo"])
                                   select pf).OrderByDescending(x => x.iID);

                bool isFirst = true;
                int countPhoto = 0;

                foreach (var fore in photoFlickr)
                {
                    if (fore.cPhoto != null)
                    {
                        Random random = new Random();
                        if (isFirst)
                        {
                            literalmainphotoimg.Text = "<img id=\"mainphotoimg\" src=\"/images/ExtClubVision/measurements/" + Convert.ToInt32(Session["MemberNo"]) + "/" + fore.cPhoto + "?refresh=" + random.Next(1000000).ToString() + "\" alt=\"main photo\"/>";
                            isFirst = false;
                        }

                        literalPhotoFlickr.Text += "<li class=\"menu\">";
                        literalPhotoFlickr.Text += "<a href=\"#\"  onclick=\"document.getElementById('mainphotoimg').src = '/images/ExtClubVision/measurements/" + Convert.ToInt32(Session["MemberNo"]) + "/" + fore.cPhoto + "?refresh=" + random.Next(1000000).ToString() + "';return false;\" >";

                        literalPhotoFlickr.Text += "<img class=\"menu_image\" src=\"/images/ExtClubVision/measurements/" + Convert.ToInt32(Session["MemberNo"]) + "/" + fore.cPhoto + "?refresh=" + random.Next(1000000).ToString() + "\" style=\"height: 64px;left: -4px;position: relative;top: 8px;width: 64px;\" />";
                        // TODO: fix: http://selbie.wordpress.com/2011/01/23/scale-crop-and-center-an-image-with-correct-aspect-ratio-in-html-and-javascript/

                        literalPhotoFlickr.Text += "<p class=\"title\">" + fore.dMeasured.ToShortDateString() + "</p></a>";
                        literalPhotoFlickr.Text += "</li>";
                        countPhoto++;
                    }
                }

                LiteralFlickrHeading.Text = countPhoto == 0 ? "You do not have any photo to display. Please close this screen and populate the photo from My Measurements Page in Visual Motivation section" : "Review Your Progress Photo";

                PopulateScreen(cms);
            }
        }

        protected void PopulateScreen(Measurement cms = null)
        {
            if (cms != null)
            {
                measurementHeader1.Style["display"] = "block";
                Div1.Style["display"] = "block";//added as "Next" Button added. 06/09/2013 Hiroshi
                measurementHeader2.Style["display"] = "block";
                myMeasurementsContentDiv.Style["margin-top"] = "0px";
                DateTime date = (DateTime)cms.dMeasured;
                DateMeasuredLabel.Text = date.ToString("dd/MM/yyyy");

                txtChest.Text = cms.fChest.ToString();
                txtWaist.Text = cms.fWaist.ToString();
                txtHips.Text = cms.fHips.ToString();

                txtForeArm.Text = cms.fForeArm.ToString();
                txtWrist.Text = cms.fWrist.ToString();
                if (cms.fForeArm != null && cms.fWrist != null)
                {
                    radioNo.Checked = true;
                    calcFat.Style["display"] = "block";//24/09 Hiroshi
                    fatRate.Style["display"] = "none";//24/09 Hiroshi
                }
                else
                {
                    radioYes.Checked = true;
                    calcFat.Style["display"] = "none";//24/09 Hiroshi
                    fatRate.Style["display"] = "block";//24/09 Hiroshi
                    txtFatRate.Text = cms.fNutCalc.ToString();//24/09 Hiroshi
                }

                txtBodyWeight.Text = cms.fBodyWeight.ToString();
                txtNutritionCalc.Text = String.Format("{0:0.0}", cms.fNutCalc);
                txtWaistHip.Text = String.Format("{0:0.00}", cms.fWaistToHipRatio);
                txtBPSystolic.Text = cms.fBPSystolic.ToString();
                txtBPDiastolyc.Text = cms.fBPDiastolic.ToString();
                txtDesiredItemClothing.Text = cms.cDesiredClothing;
                isMetricDdl.Enabled = false;
                isMetricDdl.SelectedValue = Convert.ToString(cms.bIsMetric);
                setUnitLabels(cms.bIsMetric.ToString());

                using (ClubVisionDataContext db = new ClubVisionDataContext())
                {
                    var customer = (from ec in db.Customer_Externals
                                    where ec.iID == Convert.ToInt32(Session["MemberNo"])
                                    select ec).FirstOrDefault();
                    if (customer.bCompleteInitialState == true)
                    {
                        txtBodyWeight.Enabled = false;
                        txtChest.Enabled = false;
                        txtWaist.Enabled = false;
                        txtHips.Enabled = false;
                        txtForeArm.Enabled = false;
                        txtWrist.Enabled = false;
                        txtFatRate.Enabled = false;
                        radioYes.Disabled = true;
                        radioNo.Disabled = true;
                        //added25/09
                        MeasureImagebuttonBack.Visible = false;
                        MeasureImagebuttonNext.ImageUrl = "/images/buttonSave.gif";
                    }
                    else if (customer.bCompleteInitialState == false)
                        isMetricDdl.Enabled = true;
                }

                Random random = new Random();
                if (cms.cPhoto != null)
                {
                    measurementsPhotoLiteral.Text = "<div style=\"height: 152px; width: 254px; overflow: hidden;\"><img src=\"/images/ExtClubVision/measurements/" + Convert.ToInt32(Session["MemberNo"]) + "/" + cms.cPhoto + "?refresh=" + random.Next(1000000).ToString() + "\" style=\"position: relative;\"></div>";
                }
                else
                {
                    measurementsPhotoLiteral.Text = "";
                }
            }
            else
            {
                measurementHeader1.Style["display"] = "none";
                Div1.Style["display"] = "none";//added as "Next" Button added. 06/09/2013 Hiroshi
                measurementHeader2.Style["display"] = "none";
                myMeasurementsContentDiv.Style["margin-top"] = "-50px";
                //DateMeasuredLabel.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtChest.Text = string.Empty;
                txtWaist.Text = string.Empty;
                txtHips.Text = string.Empty;
                txtForeArm.Text = string.Empty;
                txtWrist.Text = string.Empty;

                txtBodyWeight.Text = string.Empty;
                txtNutritionCalc.Text = string.Empty;
                txtWaistHip.Text = string.Empty;
                txtBPSystolic.Text = string.Empty;
                txtBPDiastolyc.Text = string.Empty;
                isMetricDdl.Enabled = true;
                setUnitLabels("True");
                measurementsPhotoLiteral.Text = string.Empty;
                txtDesiredItemClothing.Text = string.Empty;
            }
        }

        protected void SaveToDatabase(DateTime dm)
        {
            var cvdc = new ClubVisionDataContext();

            var custMeasures = (from cm in cvdc.Measurements
                                where cm.iCustomerID == Convert.ToInt32(Session["MemberNo"])
                                where cm.dMeasured == dm
                                select cm);

            var custMeasure = new Measurement();
            bool isNew = true;

            foreach (Measurement mLU in custMeasures)
            {
                custMeasure = mLU;
                isNew = false;
            }

            custMeasure.iCustomerID = Convert.ToInt32(Session["MemberNo"]);
            custMeasure.fChest = Convert.ToDouble(txtChest.Text);
            custMeasure.fWaist = Convert.ToDouble(txtWaist.Text);

            custMeasure.fHips = Convert.ToDouble(txtHips.Text);

            custMeasure.fBodyWeight = Convert.ToDouble(txtBodyWeight.Text);
            //*********************28/08/2013 Hiroshi********************************************
            //if radio button yes is checked save fat rate directly from text box
            //if no, fat rate is calculated and saved
            if (radioNo.Checked)//this is to detect html check button
            {
                custMeasure.fForeArm = Convert.ToDouble(txtForeArm.Text);
                custMeasure.fWrist = Convert.ToDouble(txtWrist.Text);
                custMeasure.fNutCalc = BodyFatFormula(_custGender);
            }

            if (radioYes.Checked)//this is to detect html check button
            {
                custMeasure.fForeArm = null;//23/09 Hiroshi
                custMeasure.fWrist = null;//23/09 Hiroshi
                custMeasure.fNutCalc = Convert.ToDouble(txtFatRate.Text);
            }
            //*****************************************************************

            custMeasure.fWaistToHipRatio = Convert.ToDouble(txtWaist.Text) / Convert.ToDouble(txtHips.Text);

            if (BloodPressureConverter(txtBPSystolic.Text) > 0)
            {
                custMeasure.fBPSystolic = BloodPressureConverter(txtBPSystolic.Text);
            }

            if (BloodPressureConverter(txtBPDiastolyc.Text) > 0)
            {
                custMeasure.fBPDiastolic = BloodPressureConverter(txtBPDiastolyc.Text);
            }
            custMeasure.cDesiredClothing = txtDesiredItemClothing.Text;
            custMeasure.bIsMetric = Convert.ToBoolean(isMetricDdl.SelectedValue);

            if (isNew)
            {
                custMeasure.dMeasured = dm;
                cvdc.Measurements.InsertOnSubmit(custMeasure);
            }

            cvdc.SubmitChanges();

            if (fileUpload.HasFile) //this should have been done after savechanges
            {
                string photoName = Convert.ToString(custMeasure.iID) + ".jpg";
                SaveImage(photoName);
                custMeasure.cPhoto = photoName;
                //added 18/11/2013 ///////////////////////////////////////////////
                var progphoto = (from p in cvdc.CustomerProgressPhotos
                                 where p.CustomerId == Convert.ToInt32(Session["MemberNo"])
                                 && p.ProgressDate.Date == dm.Date
                                 && p.PhotoTypeId == 2 //2 is for measurement
                                 select p).FirstOrDefault();

                if (progphoto != null)
                {
                    progphoto.ProgressPhoto = photoName;
                }
                else
                {
                    CustomerProgressPhoto cp = new CustomerProgressPhoto();
                    cp.CustomerId = Convert.ToInt32(Session["MemberNo"]);
                    cp.ProgressDate = dm;
                    cp.ProgressPhoto = photoName;
                    cp.PhotoTypeId = 2; // 2 for photo from measurement
                    cvdc.CustomerProgressPhotos.InsertOnSubmit(cp);
                }
                cvdc.SubmitChanges();
                //////////////////////////////////////////////////////////////////
            }
            //**********this block is to update current weight and unit in Goal Table
            var mg = (from mgl in cvdc.Goals//gets the newest Goal record
                      where mgl.iCustomerID == Convert.ToInt32(Session["MemberNo"])
                      select mgl).OrderByDescending(x => x.dDateCreated).FirstOrDefault();
            if (mg != null)
            {
                if (mg.fCurrentBodyWeight != Convert.ToDouble(custMeasure.fBodyWeight))
                {
                    //if the current weight in Goal table doesn't mutch with the updated weight in Measurements,
                    //weight in Goal is updated
                    mg.fCurrentBodyWeight = Convert.ToDouble(custMeasure.fBodyWeight);
                    mg.bIsMetric = Convert.ToBoolean(isMetricDdl.SelectedValue);
                }
                ////**********end update Goal Table*******************************************
                cvdc.SubmitChanges();
            }
            cvdc.Dispose();
        }

        protected double BloodPressureConverter(string bp)
        {
            try
            {
                return Convert.ToDouble(bp);
            }
            catch (Exception)
            {

                return 0.00;
            }

        }

        protected double BodyFatFormula(string gender)
        {
            double bodyFatPercentage;

            if (isMetricDdl.SelectedValue.Equals("False"))
            {
                if (gender.Equals("Female"))
                {
                    //this is imperial calculation
                    double factor1 = (Convert.ToDouble(txtBodyWeight.Text) * 0.732) + 8.987;
                    double factor2 = Convert.ToDouble(txtWrist.Text) / 3.140;
                    double factor3 = Convert.ToDouble(txtWaist.Text) * 0.157;
                    double factor4 = Convert.ToDouble(txtHips.Text) * 0.249;
                    double factor5 = Convert.ToDouble(txtForeArm.Text) * 0.434;

                    double leanBodyMass = factor1 + factor2 - factor3 - factor4 + factor5;
                    double bodyFatWeight = Convert.ToDouble(txtBodyWeight.Text) - leanBodyMass;

                    bodyFatPercentage = (bodyFatWeight * 100) / Convert.ToDouble(txtBodyWeight.Text);
                }
                else
                {   //MALE
                    double factor1 = (Convert.ToDouble(txtBodyWeight.Text) * 1.082) + 94.42;
                    double factor2 = Convert.ToDouble(txtWaist.Text) * 4.15;

                    double leanBodyMass = factor1 - factor2;
                    double bodyFatWeight = Convert.ToDouble(txtBodyWeight.Text) - leanBodyMass;

                    bodyFatPercentage = (bodyFatWeight * 100) / Convert.ToDouble(txtBodyWeight.Text);
                }
            }
            else
            {
                if (gender.Equals("Female"))
                {
                    //this is metric calculation
                    double factor1 = (Convert.ToDouble(txtBodyWeight.Text) * 2.2 * 0.732) + 8.987;
                    double factor2 = Convert.ToDouble(txtWrist.Text) * 0.4 / 3.140;
                    double factor3 = Convert.ToDouble(txtWaist.Text) * 0.4 * 0.157;
                    double factor4 = Convert.ToDouble(txtHips.Text) * 0.4 * 0.249;
                    double factor5 = Convert.ToDouble(txtForeArm.Text) * 0.4 * 0.434;

                    double leanBodyMass = factor1 + factor2 - factor3 - factor4 + factor5;
                    double bodyFatWeight = (Convert.ToDouble(txtBodyWeight.Text) * 2.2) - leanBodyMass;

                    bodyFatPercentage = (bodyFatWeight * 100) / (Convert.ToDouble(txtBodyWeight.Text) * 2.2);
                }
                else
                {
                    double factor1 = (Convert.ToDouble(txtBodyWeight.Text) * 2.2 * 1.082) + 94.42;
                    double factor2 = Convert.ToDouble(txtWaist.Text) * 0.4 * 4.15;

                    double leanBodyMass = factor1 - factor2;
                    double bodyFatWeight = (Convert.ToDouble(txtBodyWeight.Text) * 2.2) - leanBodyMass;

                    bodyFatPercentage = (bodyFatWeight * 100) / (Convert.ToDouble(txtBodyWeight.Text) * 2.2);
                }
            }
            return bodyFatPercentage;
        }

        private void SaveJpeg(string path, Bitmap img, long quality)
        {
            // Encoder parameter for image quality
            EncoderParameter qualityParam = new EncoderParameter(Encoder.Quality, quality);

            // Jpeg image codec
            ImageCodecInfo jpegCodec = this.getEncoderInfo("image/jpeg");

            if (jpegCodec == null)
                return;

            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;

            img.Save(path, jpegCodec, encoderParams);
        }

        private ImageCodecInfo getEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        }

        private static Image resizeImage(Image imgToResize, Size size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH > nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return (Image)b;
        }

        protected void SaveImage(string name)
        {
            Image image = Image.FromStream(new System.IO.MemoryStream(fileUpload.FileBytes));
            Size size = new Size(254, 152);

            Image new_image = resizeImage(image, size);

            string path = "/images/ExtClubVision/measurements/" + Convert.ToInt32(Session["MemberNo"]) + "/";

            // if directory doesn't exist - create it. 
            if (!Directory.Exists(Server.MapPath(path)))
            {
                Directory.CreateDirectory(Server.MapPath(path));
            }

            this.SaveJpeg(Server.MapPath(path + name), new Bitmap(new_image), 85L);
        }
        //updated from ImageButton to normal Button
        protected void NextImageButton_Click(object sender, EventArgs e)
        {
            txtMeasurementsWarning.Text = "";
            using (ClubVisionDataContext cvdc = new ClubVisionDataContext())
            {
                var cms = (from cm in cvdc.Measurements
                           where cm.iCustomerID == Convert.ToInt32(Session["MemberNo"])
                           where cm.dMeasured > Convert.ToDateTime(DateMeasuredLabel.Text)
                           select cm).OrderBy(x => x.dMeasured).FirstOrDefault();
                PopulateScreen(cms ?? _lastMeasured);
                GoBackToMyMeasurementsScreen();
            }
        }
        protected void MeasureButtonNext_Click(object sender, EventArgs e)
        { //dummy button
            try
            {
                SaveToDatabase(DateMeasuredLabel.Text.Equals("Label")
                                   ? DateTime.Now
                                   : Convert.ToDateTime(DateMeasuredLabel.Text));
                Response.Redirect(HttpContext.Current.Request.Url.AbsolutePath + "?tab=goals", false);
            }
            catch (Exception exception)
            {
                string s = "<script type=\"text/javascript\">alert('All mandatory fields must be in a correct format');</script>";
                Page.ClientScript.RegisterStartupScript(GetType(), "test", s);
            }
        }
        protected void MeasureImagebuttonNext_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                //SaveToDatabase(DateTime.Now);
                SaveToDatabase(DateMeasuredLabel.Text.Equals("Label")
                               ? DateTime.Now
                               : Convert.ToDateTime(DateMeasuredLabel.Text));

                switch (HttpContext.Current.Request.Url.AbsolutePath)
                {
                    case "/club-vision/my-profile/edit-measurements/":
                        Response.Redirect(HttpContext.Current.Request.Url.AbsolutePath + "?tab=goals", false);
                        break;
                    case "/club-vision/my-journey/":
                        Response.Redirect(HttpContext.Current.Request.Url.AbsolutePath + "?tab=mymeasurements", false);
                        break;
                    case "/club-vision/account-setup/measurements/":
                        Response.Redirect("/club-vision/account-setup/my-goals/", false);
                        break;
                }
            }
            catch (Exception exception)
            {
                string s = "<script type=\"text/javascript\">alert('All mandatory fields must be in a correct format');</script>";
                Page.ClientScript.RegisterStartupScript(GetType(), "test", s);
            }
            //finally { GoBackToMyMeasurementsScreen(); }
        }

        protected void PreviousButton_Click(object sender, EventArgs e)
        {
            txtMeasurementsWarning.Text = "";

            var cvdc = new ClubVisionDataContext();

            var cms = (from cm in cvdc.Measurements
                       where cm.iCustomerID == Convert.ToInt32(Session["MemberNo"])
                       where cm.dMeasured < Convert.ToDateTime(DateMeasuredLabel.Text)
                       select cm).OrderByDescending(x => x.dMeasured).FirstOrDefault();

            PopulateScreen(cms ?? _lastMeasured);
            GoBackToMyMeasurementsScreen();
        }
        protected void CreateNewButton_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(_lastRecord, DateTime.Today) == 0)
            {
                txtMeasurementsWarning.Text =
                    "You are able to edit today's entry. Should you wish to create a new record please wait 24 hours.";
                PopulateScreen(_lastMeasured);
                GoBackToMyMeasurementsScreen();
            }
            else
            {
                PopulateScreen();
                GoBackToMyMeasurementsScreen();
            }
        }

        protected void PreviousImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            txtMeasurementsWarning.Text = "";

            var cvdc = new ClubVisionDataContext();

            var cms = (from cm in cvdc.Measurements
                       where cm.iCustomerID == Convert.ToInt32(Session["MemberNo"])
                       where cm.dMeasured < Convert.ToDateTime(DateMeasuredLabel.Text)
                       select cm).OrderByDescending(x => x.dMeasured).FirstOrDefault();

            PopulateScreen(cms ?? _lastMeasured);
            GoBackToMyMeasurementsScreen();
        }
        protected void CreateNewImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (DateTime.Compare(_lastRecord, DateTime.Today) == 0)
            {
                txtMeasurementsWarning.Text =
                    "You are able to edit today's entry. Should you wish to create a new record please wait 24 hours.";
                PopulateScreen(_lastMeasured);
                GoBackToMyMeasurementsScreen();
            }
            else
            {
                PopulateScreen();
                GoBackToMyMeasurementsScreen();
            }
        }
        protected void MeasureImagebuttonBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect("/club-vision/my-profile/edit-profile/?alttemplate=Ext%20Edit%20Profile&tab=bodytype", false);
        }
        protected void measurementsCalculateButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (DateMeasuredLabel.Text.Equals("Label"))
            {
                SaveToDatabase(DateTime.Now);
            }
            else
            {
                SaveToDatabase(Convert.ToDateTime(DateMeasuredLabel.Text));
            }
            Response.Redirect("/club-vision/my-profile/edit-measurements/", false);

        }

        protected void GoBackToMyMeasurementsScreen()
        {   //modified to call corresponding javascript depends on which page
            //06/09/2013 Hiroshi
            string path = HttpContext.Current.Request.Url.AbsolutePath;
            string s = "";

            switch (path)
            {
                case "/club-vision/my-profile/edit-measurements/":
                    s = "<script cardioType=\"text/javascript\">"
                            + "document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_profileTab').style.backgroundImage = 'url(/images/ExtClubVision/eHdrProfileTabMeasurements.gif)';"
                            + "document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_eMeasurementsTabMeasure').style.display = 'block';"
                            + "document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_eMeasurementsTabGoals').style.display = 'none';"
                            + "document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_eMeasurementsTabTrain').style.display = 'none';"
                            + "document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tabMeasure2').style.display = 'block';"
                      + "</script>";
                    break;

                case "/club-vision/my-journey/":
                    s = "<script cardioType=\"text/javascript\">"
                            + "document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_MyJouneyTab_2_profileTab').style.backgroundImage = 'url(/images/eHdrMyJourney-MyMeasurements.gif)';"
                            + "document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_MyJouneyTab_2_eMyProgress').style.display = 'none';"
                            + "document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_MyJouneyTab_2_eMeasurements').style.display = 'block';"
                            + "document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_MyJouneyTab_2_eMyGoals').style.display = 'none';"
                      + "</script>";
                    break;
            }
            Page.ClientScript.RegisterStartupScript(GetType(), "test", s);
        }
        protected void setUnitLabels(string unitBool)
        {
            string weightUnit = "", sizeUnit = "";
            //switch (isMetricDdl.SelectedValue)
            switch (unitBool)
            {
                case "True":
                    sizeUnit = "cm";
                    weightUnit = "kg";
                    break;
                case "False":
                    sizeUnit = "inches";
                    weightUnit = "pounds";
                    break;
            }
            WeightUnitLabel.Text = weightUnit;
            ChestUnitLabel.Text = sizeUnit;
            WaistUnitLabel.Text = sizeUnit;
            HipsUnitLabel.Text = sizeUnit;
            ForearmUnitLabel.Text = sizeUnit;
            WristUnitLabel.Text = sizeUnit;
        }

        protected void isMetricDdl_SelectedIndexChanged(object sender, EventArgs e)
        {
            //setUnitLabels();
            GoBackToMyMeasurementsScreen();
        }

        protected void CreateNewButton_Test(object sender, EventArgs e)
        {
            Session["LockIn"] = true;
            /*
            NewGoalMeasurementsLightBox ngml = new NewGoalMeasurementsLightBox();
            PlaceHolder t1 = (PlaceHolder)ngml.FindControl("PlaceHolder1");
            t1.Controls.Add(ngml.LoadControl("/usercontrols/externalclubvision/initialscreens/Measurements.ascx"));
            */
            string s = "<script type=\"text/javascript\">testNewLightBox()</script>";
            Page.ClientScript.RegisterStartupScript(GetType(), "test", s);
        }

        protected void NewInsertToDB(object sender, EventArgs e)
        {
            SaveToSession(DateTime.Now);
        }

        protected void SaveToSession(DateTime dm)
        {
            try
            {
                var custMeasure = new Measurement();

                custMeasure.iCustomerID = Convert.ToInt32(Session["MemberNo"]);
                custMeasure.fChest = Convert.ToDouble(txtChest.Text);
                custMeasure.fWaist = Convert.ToDouble(txtWaist.Text);
                custMeasure.fForeArm = Convert.ToDouble(txtForeArm.Text);
                custMeasure.fHips = Convert.ToDouble(txtHips.Text);
                custMeasure.fWrist = Convert.ToDouble(txtWrist.Text);
                custMeasure.fBodyWeight = Convert.ToDouble(txtBodyWeight.Text);
                //*********************28/08/2013 Hiroshi********************************************
                //if radio button yes is checked save fat rate directly from text box
                //if no, fat rate is calculated and saved
                if (Request.Form["fatRate"].ToString() == "N")
                {
                    custMeasure.fNutCalc = BodyFatFormula(_custGender);
                }
                else if (Request.Form["fatRate"].ToString() == "Y")
                {
                    txtForeArmRequiredFieldValidator.Enabled = false;
                    txtWristRequiredFieldValidator.Enabled = false;
                    custMeasure.fNutCalc = Convert.ToDouble(txtFatRate.Text);
                }
                //*****************************************************************

                custMeasure.fWaistToHipRatio = Convert.ToDouble(txtWaist.Text) / Convert.ToDouble(txtHips.Text);

                if (BloodPressureConverter(txtBPSystolic.Text) > 0)
                {
                    custMeasure.fBPSystolic = BloodPressureConverter(txtBPSystolic.Text);
                }

                if (BloodPressureConverter(txtBPDiastolyc.Text) > 0)
                {
                    custMeasure.fBPDiastolic = BloodPressureConverter(txtBPDiastolyc.Text);
                }
                custMeasure.cDesiredClothing = txtDesiredItemClothing.Text;
                custMeasure.bIsMetric = Convert.ToBoolean(isMetricDdl.SelectedValue);
                custMeasure.dMeasured = dm;

                if (fileUpload.HasFile) //this should have been done after savechanges
                {
                    string photoName = Convert.ToString(custMeasure.iID) + ".jpg";
                    SaveImage(photoName);
                    custMeasure.cPhoto = photoName;
                }

                Session["MeasurementsVal"] = custMeasure;

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "function", "resetMyPlanLightBox2();", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Could not process your entries');", true);

            }

        }
    }
}