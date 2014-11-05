using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Web;
using System.Web.UI;
//using System.Web.UI.WebControls;


namespace VisionPersonalTrainingProject.usercontrols.clubvision
{
    public partial class ProfilePictureEdit : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ClubVisionDataContext cvdc = new ClubVisionDataContext();


            var customerImages = (from ci in cvdc.CustomerImages
                                  where ci.CustomerId == (int)Session["MemberNo"]
                                  select ci);

            CustomerImage customerImage = new CustomerImage();
            foreach (CustomerImage customerImageLU in customerImages)
            {
                customerImage = customerImageLU;
            }

            Random random = new Random();

            if (customerImage.ProfileImage != null)
            {
                literalImage.Text = "<div style=\"position: absolute; top: 110px; left: 168px; height: 152px; width: 254px; overflow: hidden;\"><img src=\"/images/profile/" + customerImage.ProfileImage + "?refresh=" + random.Next(1000000).ToString() + "\" style=\"position: relative;\"></div>";
            }

            cvdc.Dispose();
        }

        private void saveJpeg(string path, Bitmap img, long quality)
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

        protected void buttonSave_Click(object sender, EventArgs e)
        {
            Image image = Image.FromStream(new System.IO.MemoryStream(fileUpload.FileBytes));
            Size size = new Size(254, 152);
           // fileUpload.
            Image new_image = resizeImage(image, size);

            this.saveJpeg(Server.MapPath("/images/profile/" + Session["MemberNo"].ToString() + ".jpg"), new Bitmap(new_image), 85L);

            ClubVisionDataContext cvdc = new ClubVisionDataContext();

            var customerImages = (from ci in cvdc.CustomerImages
                                  where ci.CustomerId == (int)Session["MemberNo"]
                                  select ci);
            bool isNew = true;
            CustomerImage customerImage = new CustomerImage();
            foreach (CustomerImage customerImageLU in customerImages)
            {
                customerImage = customerImageLU;
                isNew = false;
            }

            customerImage.CustomerId = (int)Session["MemberNo"];
            customerImage.ProfileImage = (int)Session["MemberNo"] + ".jpg";

            if (isNew)
            {
                cvdc.CustomerImages.InsertOnSubmit(customerImage);
            }

            cvdc.SubmitChanges();


            cvdc.Dispose();
            
            //Response.Redirect("/club-vision/my-profile/edit-profile/");
            Response.Redirect(Request.RawUrl,false);
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
    }
}