using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using Image = System.Drawing.Image;

namespace VisionPersonalTrainingProject.usercontrols.VVT
{
    public partial class MyJourneyProgressPhoto : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                LiteralTranformationPhoto.Text = getProgressPhoto((int)Session["MemberNo"]);
            }

        }

        public string getProgressPhoto(int memberId)
        {
            ClubVisionDataContext cvdc = new ClubVisionDataContext();
            string imagePath = "";
            
            var pics = (from p in cvdc.CustomerProgressPhotos
                        where p.CustomerId == memberId
                        && p.PhotoTypeId != 1 //take this off after bmp thing sorted
                        orderby p.ProgressDate
                        select p).ToList();

            int countPhoto = pics.Count()*-400;

            var cus = (from c in cvdc.Customers
                       where c.Id == memberId
                       select c).FirstOrDefault();

            string resultStr = "<ul id=\"ulphoto\" style=\"transition: all 0s ease; -webkit-transition: all 0s ease; -webkit-transform: translate3d(" + countPhoto + "px, 0px, 0px);\">";

            foreach (var p in pics)
            {
                switch (p.PhotoTypeId)
                {
                    case 1: imagePath = "http://vos.visionpt.com.au/clientphotos/" + cus.Studio + "/" + p.ProgressPhoto; break;
                    case 2: imagePath = "/images/ExtClubVision/measurements/" + memberId + "/" + p.ProgressPhoto; break;
                    case 3: imagePath = "/images/ProgressPhotos/" + memberId + "/" + p.ProgressPhoto; break;
                    case 4: imagePath = "/images/ProgressPhotos/" + memberId + "/" + p.ProgressPhoto; break;
                }
                resultStr += "<li data-id=\"" + p.Id + "\" data-date=\"" + p.ProgressDate.ToString("dddd dd/MM/yyyy") + "\" " +
                             "style=\"background: url('" + imagePath + "');background-size: contain; background-position: center 0%;background-repeat: no-repeat;\">" +
                             "<div class=\"ulphotoliDetail\"><h2 style=\"color: #666666;\">Photo submitted on <span style=\"color: #000000;\">" + p.ProgressDate.ToString("dddd, dd MMMM yyyy") + "</span>.</h2>" +
                             "<img class=\"buttonIcon\" title=\"Delete This Photo exercise\" onclick=\"deleteTransformationPhotoAlert(" + p.Id + ");return false;\" src=\"/images/icons/web/delete.png\" data-alt-src=\"/images/icons/web/delete-red.png\" width=\"18px\">" + 
                             "</div>" +
                             "</li>";
            }

            resultStr += "</ul>";

            string dummyStr = "<ul style=\"transition: all 0s ease; -webkit-transition: all 0s ease; -webkit-transform: translate3d(-2400px, 0px, 0px);\">" +
                        "<li style=\"background: url('/images/samplephoto.jpg');background-size: contain; background-position: center 0%;background-repeat: no-repeat;\"></li>" +
                        "<li style=\"background: url('/images/samplephoto.jpg');background-size: contain; background-position: center 0%;background-repeat: no-repeat;\"></li>" +
                        "<li style=\"background: url('/images/samplephoto.jpg');background-size: contain; background-position: center 0%;background-repeat: no-repeat;\"></li>" +
                        "<li style=\"background: url('/images/samplephoto.jpg');background-size: contain; background-position: center 0%;background-repeat: no-repeat;\"></li>" +
                        "<li style=\"background: url('/images/samplephoto.jpg');background-size: contain; background-position: center 0%;background-repeat: no-repeat;\"></li>" +
                        "<li style=\"background: url('/images/samplephoto.jpg');background-size: contain; background-position: center 0%;background-repeat: no-repeat;\"></li></ul>";

            return imagePath.Equals("") ? dummyStr : resultStr;

        }
        
        public void uploadTransformationPhoto()
        {
            if (FileUpload1.HasFile) //this should have been done after savechanges
            {
                ClubVisionDataContext cvdc = new ClubVisionDataContext();
                string photoName = "VVT-WEB-" + DateTime.Today.ToString("yyyyMMdd") + ".jpg";
                this.SaveImageProgressPhoto(photoName);
                //added 18/11/2013 ///////////////////////////////////////////////
                var progphoto = (from p in cvdc.CustomerProgressPhotos
                                 where p.CustomerId == Convert.ToInt32(Session["MemberNo"])
                                 && p.ProgressDate.Date == DateTime.Today
                                 && p.PhotoTypeId == 4 //2 is for measurement
                                 select p).FirstOrDefault();

                if (progphoto != null)
                {
                    progphoto.ProgressPhoto = photoName;
                }
                else
                {
                    CustomerProgressPhoto cp = new CustomerProgressPhoto();
                    cp.CustomerId = Convert.ToInt32(Session["MemberNo"]);
                    cp.ProgressDate = DateTime.Today;
                    cp.ProgressPhoto = photoName;
                    cp.PhotoTypeId = 4; // 2 for photo from measurement
                    cvdc.CustomerProgressPhotos.InsertOnSubmit(cp);
                }
                cvdc.SubmitChanges();
                //////////////////////////////////////////////////////////////////
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void SaveImageProgressPhoto(string name)
        {
            Image image = Image.FromStream(new System.IO.MemoryStream(FileUpload1.FileBytes));


            string path = "/images/ProgressPhotos/" + Convert.ToInt32(Session["MemberNo"]) + "/";

            // if directory doesn't exist - create it. 
            if (!Directory.Exists(Server.MapPath(path)))
            {
                Directory.CreateDirectory(Server.MapPath(path));
            }

            this.SaveJpeg(Server.MapPath(path + name), new Bitmap(image), 85L);
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

        protected void ButtonUpload_Click(object sender, EventArgs e)
        {
            uploadTransformationPhoto();
        }
    }
}