using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using uComponents.Core;
using umbraco.cms.businesslogic.web;
using umbraco.presentation.nodeFactory;
using umbraco.BusinessLogic;

namespace VisionPersonalTrainingProject.usercontrols.studiopages
{
    public partial class EditArticle : System.Web.UI.UserControl
    {
        private const int blodNodeId = 26621;
        private string currentURL;
        
        protected void Page_Init(object sender, EventArgs e)
        {
            ((umbraco.UmbracoDefault)this.Page).ValidateRequest = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.CacheControl = "no-cache";

            if (Session["StudioId"] == null || Session["StudioUser"] == null || Session["RoleId"] == null)
            {
                Response.Redirect("/franchise-login/");
            }

            currentURL = "/studioedit/studio-articles-edit/";
            
            if(!Page.IsPostBack)
            {
                Node studioNode = FindStudio();

                List<Document> articlesNode = FindStudioArticleDoc(Convert.ToInt32(studioNode.GetProperty("StudioId")));

                litPostLists.Text = ShowArticleList(articlesNode);

                lblNewPostOrEdit.Text = "Create New Post";
                // show studio data only when we have found a valid node
                if (studioNode != null)
                {
                    //LoadMediaItems();
                    lblStudioTitle.Text = ShowStudioData(studioNode);

                    //edit mode
                    if (Request.QueryString["articleid"] != null)
                    {
                        ShowArticleData(Convert.ToInt32(Request.QueryString.Get("articleid")));
                        RequiredFieldValidator3.Enabled = false;
                    }
                }

                if (Request.QueryString["msg"] != null)
                {
                    EditSuccessStories ess = new EditSuccessStories();
                    Literal1.Text = ess.EditStudioDetailsMsg(Request.QueryString["msg"]);
                }
            }
       }

        protected Node FindStudio()
        {
            Node studioNode = null;

            List<Node> nodes = uQuery.GetNodesByType("StudioPage");

            // Find the studio with the right studio id
            foreach (Node item in nodes)
            {
                
                if (item.GetProperty("studioId").Value.Trim() == Session["StudioId"].ToString())
                {
                    studioNode = item;
                    // add node id to viewstate to use when saving our data
                    ViewState["nodeId"] = item.Id;
                }
            }

            return studioNode;
        }
        
        protected List<Document> FindStudioArticleDoc(int studioId)
        {
            DocumentType dt = DocumentType.GetByAlias("BlogPost");

            List<Document> docs = Document.GetDocumentsOfDocumentType(dt.Id).Where(x => x.IsTrashed == false &&
                                            (string) x.getProperty("studioId").Value == Session["StudioId"].ToString()).OrderByDescending(x => x.UpdateDate).ToList();

            //List<Document> docs = Document.GetDocumentsOfDocumentType(dt.Id).ToList();
            
            List<Document> artNodeOfStudioDoc = new List<Document>();

            foreach (Document document in docs)
            {
                artNodeOfStudioDoc.Add(document);

            }
            return artNodeOfStudioDoc;
        }

        protected string ShowArticleList(List<Document> studArticles)
        {
            string ret = "";
            foreach (Document article in studArticles)
            {
                ret += "<div class='row articleList'><div class='col-md-12'><h4>" + article.Text + "</h4>" + 
                        "<p>Written by " + article.getProperty("writer").Value + " on " + article.CreateDateTime.ToString("ddd dd/MM/yy") + "</p>" +
                        "<p>" + (article.Published ? "<span style=\"color:green;\"><i class=\"fa fa-check-square\"></i></span>" : "<span style=\"color:red;\"><i class=\"fa fa-square\"></i></span>") + " | " +
                        (article.HasPendingChanges() ? "<span style=\"color:orange;\"><i class=\"fa fa-circle-o\"></i></span> | " : "") +
                        "<a href=\"" + currentURL + "?articleid=" + article.Id + "\">Edit</a>" + 
                            (article.Published ? " | <a href=\"" + umbraco.library.NiceUrl(article.Id) + "\">Preview</a>" : "") + "</p></div></div>";
            }

            if (ret.Length < 1)
            {
                return "<div class='row'><div class='col-md-12'><p>No Articles</p>" +
                       "</div></div>";
            }
            return ret;
        }

        protected void ShowArticleData(int nodeid)
        {
            Document article = new Document(nodeid);

            txtTitle.Text = article.Text;

            txtBlurb.Text = (string) article.getProperty("blurb").Value;

            txtContent.Text = (string) article.getProperty("bodyText").Value;

            txtTags.Text = (string) article.getProperty("tags").Value;

            litPicture.Text = "<img alt=\"Illustration Picture\" src=\"/images/studio_blog/" + Session["StudioId"] + "/" + (string)article.getProperty("illustrationImage").Value +
                              "\" style=\"max-height:200px;\"/>";

            try
            {
                ddlCategory.SelectedValue = (string) article.getProperty("category").Value;
            }catch(Exception exception)
            {
                //do nothing
            }
            

            lblNewPostOrEdit.Text = "Edit Post  |  <a class=\"btn gtn-sm btn-primary\" href=\"" + currentURL + "\">Create New Post</a>";

        }

        public string ShowStudioData(Node studioNode)
        {
            return studioNode.GetProperty("contentTitle").Value;
        }

        protected bool SaveArticle(int articleid)
        {
            try
            {
                string title = txtTitle.Text;
                string content = txtContent.Text;
                // string[] tags = txtTags.Text.Split(',');

                string redirection = "";
                Document doc;//= new Document(articleid);


                if (articleid == 0)
                {
                    doc = Document.MakeNew(title, DocumentType.GetByAlias("BlogPost"), User.GetUser(0), blodNodeId);
                    doc.getProperty("writer").Value = Session["StudioUser"].ToString(); //writer
                }
                else
                {
                    doc = new Document(articleid);
                }

                doc.Text = title;
                doc.getProperty("blurb").Value = txtBlurb.Text; //blurb
                doc.getProperty("bodyText").Value = content;  //content
                doc.getProperty("studioId").Value = Session["StudioId"].ToString(); //studioid
                doc.getProperty("tags").Value = txtTags.Text; //blurb
                doc.getProperty("category").Value = ddlCategory.SelectedItem.Text; //blurb
                ViewState["articlenode"] = doc.Id;

                if (FileUploadPicture.HasFile)
                {
                    string rawurl = Request.RawUrl.Replace("&msg=photofailed", "");

                    if ((FileUploadPicture.PostedFile.ContentType.Equals("image/jpeg") ||
                        FileUploadPicture.PostedFile.ContentType.Equals("image/jpg") ||
                        FileUploadPicture.PostedFile.ContentType.Equals("image/png") ||
                        FileUploadPicture.PostedFile.ContentType.Equals("image/gif")) &&
                        FileUploadPicture.PostedFile.ContentLength < 5243000)
                    {
                        string fileName = doc.Id + Path.GetExtension(FileUploadPicture.FileName);
                        

                        if ((SaveImgAndResize(fileName)))
                        {
                            doc.getProperty("illustrationImage").Value = fileName;
                            redirection = currentURL + "&msg=success";
                        }
                        else
                        {
                            redirection = currentURL + "&msg=photofailed";
                        }
                    }
                }
                ViewState["picName"] = doc.getProperty("illustrationImage").Value;

                doc.Save();

                return true;
            }catch(Exception ex)
            {
                VPTFacilities helper = new VPTFacilities();
                helper.MailErrorMsg(Session["FoEmail"].ToString(), "ERROR in Edit Article", Request.RawUrl + "<br/>" + ex, false, true);

                return false;
            }
        }

        protected void PublishPost(object sender, EventArgs e)
        {
            string toEmail = ConfigurationManager.AppSettings["publishemails"];
            string visionweb = ConfigurationManager.AppSettings["VisionWebUrl"];

            int articleid = (Request.QueryString["articleid"] == null
                                 ? 0
                                 : Convert.ToInt32(Request.QueryString["articleid"]));

            VPTFacilities helper = new VPTFacilities();

            if( SaveArticle(articleid))
            {
                // Send email to admin
                string body = Session["StudioUser"] + " sent an article below for approval.<br/><br/>" +
                              "----------------------------------------------------------------------" +
                              "<h1>" + txtTitle.Text + "</h1>" +
                              "<p>Category : " + ddlCategory.SelectedItem.Text + "</p>" +
                              "<p style='font-style:italic;'>" + txtBlurb.Text + "</p>" +
                              "<img src='" + visionweb + "/images/studio_blog_thumb/" + 
                              Session["StudioId"] + "/" + ViewState["picName"] + "'/>" +
                              txtContent.Text + "<br/><br/>" +
                              "----------------------------------------------------------------------<br/>" +
                              "Please click on the following link to publish : " +
                              "" + visionweb + "/umbraco/actions/editcontent.aspx?id=" + ViewState["articlenode"];

                helper.MailWithoutCCEnq(Session["FoEmail"].ToString(), toEmail, "Studio article to publish", body, false, true);
                Response.Redirect(currentURL + "?msg=1");
               
            }else
            {
                helper.MailWithoutCCEnq(Session["FoEmail"].ToString(), toEmail, "FAILED: Studio article to publish", "", false, true);
                Response.Redirect(currentURL + "?msg=2");
            }
        }

        protected void SaveDraft(object sender, EventArgs e)
        {
            if (SaveArticle((Request.QueryString["articleid"] == null ? 0: Convert.ToInt32(Request.QueryString["articleid"]))))
                Response.Redirect(currentURL + "?msg=3");
            else
                Response.Redirect(currentURL + "?msg=4");
        }

        protected bool SaveImgAndResize(string filename)// REPETITION FROM MENUS
        {
            try
            {
                // Find the fileUpload control
                //string filename = mealImageFileUpload.FileName;

                // Find the fileUpload control
                string path = "/images/studio_blog/" + Session["StudioId"].ToString() + "/";
                string paththumb = "/images/studio_blog_thumb/" + Session["StudioId"].ToString() + "/";

                // Check if the directory we want the image uploaded to actually exists or not
                if (!Directory.Exists(Server.MapPath(path)))
                {
                    Directory.CreateDirectory(Server.MapPath(path));
                }

                // Check if the directory we want the image uploaded to actually exists or not
                if (!Directory.Exists(Server.MapPath(paththumb)))
                {
                    Directory.CreateDirectory(Server.MapPath(paththumb));
                }

                // Specify the upload directory
                string directory = Server.MapPath(path);
                string directorythumb = Server.MapPath(paththumb);

                // Create a bitmap of the content of the fileUpload control in memory
                Bitmap originalBMP = new Bitmap(FileUploadPicture.FileContent);
                
                Image saveCroppedImage = SaveCroppedImage(originalBMP, 650, 437);
                Image saveCroppedImageThumb = SaveCroppedImage(originalBMP, 75, 75);
                
                //Image resizeImageDistort = ResizeImageDistort(originalBMP, 250, true);
                
                // Save the new graphic file to the server
                saveCroppedImage.Save(directory + filename);
                saveCroppedImageThumb.Save(directorythumb + filename);
                //resizeImageDistort.Save(directory + "resizeImageDistort" + filename);

                // Deallocate them once finish
                originalBMP.Dispose();
                saveCroppedImage.Dispose();
                saveCroppedImageThumb.Dispose();
                
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        //Resize image and crop from center
        public Image SaveCroppedImage(Image image, int maxWidth, int maxHeight)
        {

            ImageCodecInfo jpgInfo = ImageCodecInfo.GetImageEncoders()
                                         .Where(codecInfo =>
                                         codecInfo.MimeType == "image/jpeg").First();
            Image finalImage = image;
            System.Drawing.Bitmap bitmap = null;

            int left = 0;
            int top = 0;
            int srcWidth = maxWidth;
            int srcHeight = maxHeight;
            bitmap = new System.Drawing.Bitmap(maxWidth, maxHeight);
            double croppedHeightToWidth = (double)maxHeight / maxWidth;
            double croppedWidthToHeight = (double)maxWidth / maxHeight;

            if (image.Width > image.Height)
            {
                srcWidth = (int)(Math.Round(image.Height * croppedWidthToHeight));
                if (srcWidth < image.Width)
                {
                    srcHeight = image.Height;
                    left = (image.Width - srcWidth) / 2;
                }
                else
                {
                    srcHeight = (int)Math.Round(image.Height * ((double)image.Width / srcWidth));
                    srcWidth = image.Width;
                    top = (image.Height - srcHeight) / 2;
                }
            }
            else
            {
                srcHeight = (int)(Math.Round(image.Width * croppedHeightToWidth));
                if (srcHeight < image.Height)
                {
                    srcWidth = image.Width;
                    top = (image.Height - srcHeight) / 2;
                }
                else
                {
                    srcWidth = (int)Math.Round(image.Width * ((double)image.Height / srcHeight));
                    srcHeight = image.Height;
                    left = (image.Width - srcWidth) / 2;
                }
            }
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(image, new Rectangle(0, 0, bitmap.Width, bitmap.Height), new Rectangle(left, top, srcWidth, srcHeight), GraphicsUnit.Pixel);
            }

            return finalImage = bitmap;
            /*
            try{
                
            }
            catch(Exception ex)
            {
                litPostLists.Text = "<p>" + ex + "</p>";
                Image finalImage = image;
                return finalImage;
            }
            */
        }

        public Image SaveCroppedImage(Image image, int toWidth)
        {

            ImageCodecInfo jpgInfo = ImageCodecInfo.GetImageEncoders()
                                         .Where(codecInfo =>
                                         codecInfo.MimeType == "image/jpeg").First();
            int width = toWidth;
            int height = toWidth * image.Height / image.Width;

            return resizeImage(image, new Size(width, height));
        }
        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }
    }
}