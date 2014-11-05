using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using uComponents.Core;
using umbraco.cms.businesslogic.web;
using umbraco.presentation.nodeFactory;
using umbraco.BusinessLogic;
using umbraco.cms.businesslogic.template;

namespace VisionPersonalTrainingProject.usercontrols.studiopages
{
    public partial class EditSuccessStories : System.Web.UI.UserControl
    {
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

            currentURL = "/studioedit/success-stories-edit/";

            //if (!Page.IsPostBack)
            //{
                Node studioNode;

                //this is temporary
                if ((string) Session["StudioUser"] == "Andrew Simmons" && Convert.ToInt32(Session["StudioId"]) == 1)//considered HQ
                {
                    studioNode = new Node(26723);
                    ViewState["nodeId"] = 26723;

                }
                else studioNode = FindStudio();
                
                lblNewPostOrEdit.Text = "Create New Success Story";

                // show studio data only when we have found a valid node
                if (studioNode != null)
                {
                    //LoadMediaItems();
                    EditArticle ea = new EditArticle();
                    lblStudioTitle.Text = ea.ShowStudioData(studioNode);

                    //edit mode
                    if(!Page.IsPostBack){
                        if (Request.QueryString["articleid"] != null)
                        {
                            ShowSuccessStoriesData(Convert.ToInt32(Request.QueryString.Get("articleid")));
                            RequiredFieldValidator3.Enabled = false;
                            RequiredFieldValidator6.Enabled = false;
                            RequiredFieldValidator8.Enabled = false;
                            //litPostLists.Text = "<div class=\"row\"><div class=\"col-md-12\"><button class=\"btn btn-lg btn-primary btn-visionred\">Create New Post</button></div></div>";
                        }
                    }

                    litFOLists.Text = ShowDocumentListChildren(Convert.ToInt32(ViewState["nodeId"]), 
                                                                "StudioSuccessStories", currentURL);
              //  }

                //updateTitleAndRepublish();

                if (Request.QueryString["msg"] != null)
                {
                    Literal1.Text = EditStudioDetailsMsg(Request.QueryString["msg"]);
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

        public string ShowDocumentListChildren(int studioId, string contentType, string icurrentURL)
        {
            string ret = "";
            
            var docRoot = new Document(studioId);

            try
            {
                foreach (Document doc in docRoot.Children.Where(x => x.ContentType.Alias == contentType).OrderByDescending(x => x.UpdateDate))
                {

                    ret += "<div class='row articleList'><div class='col-md-12'><h4>" + doc.Text + "</h4>" +
                        "<p>Created on " + doc.CreateDateTime.ToString("ddd dd/MM/yy") + "</p>" +
                        "<p>" + (doc.Published ? "<span style=\"color:green;\"><i class=\"fa fa-check-square\"></i></span>" : "<span style=\"color:red;\"><i class=\"fa fa-square\"></i></span>") + " | " +
                        (doc.HasPendingChanges() ? "<span style=\"color:orange;\"><i class=\"fa fa-circle-o\"></i></span> | " : "") +
                        "<a href=\"" + icurrentURL + "?articleid=" + doc.Id + "\">Edit</a>" +
                            (doc.Published ? " | <a href=\"" + umbraco.library.NiceUrl(doc.Id) + "\">Preview</a>" : "") + "</p></div></div>";
                }
            }catch(Exception ex)
            {
                ret = ex.ToString();
            }
            
            
            if (ret.Length < 1)
            {
                return "<div class='row'><div class='col-md-12'><p>No Owner Profiles</p>" +
                       "</div></div>";
            }
            return ret;
        }

        protected void PublishPost(object sender, EventArgs e)
        {
            string toEmail = ConfigurationManager.AppSettings["publishemails"];
            string visionweb = ConfigurationManager.AppSettings["VisionWebUrl"];

            int articleid = (Request.QueryString["articleid"] == null
                                 ? 0
                                 : Convert.ToInt32(Request.QueryString["articleid"]));

            VPTFacilities helper = new VPTFacilities();

            if(SaveSuccessStories(articleid))
            {
                // Send email to admin
                string body = Session["StudioUser"] + " sent a SUCCESS STORY below for approval.<br/><br/>" +
                              "----------------------------------------------------------------------" +
                              "<h1>" + txtName.Text + "</h1>" +
                              "<p style='font-style:italic;'>" + txtResult.Text + "</p>" +
                              "Before Photo: <br/><img src='" + visionweb + "/images/success_stories/" + "/" + ViewState["beforePhoto"] + "'/><br/>" +
                              "After Photo: <br/> <img src='" + visionweb + "/images/success_stories/" + "/" + ViewState["afterPhoto"] + "'/><br/>" +
                              "Video Url : " + (ViewState["videoUrl"].ToString().Equals("")? "N/A" : ViewState["videoUrl"]) + "<br/>" +
                              txtStory.Text + "<br/><br/>" +
                              "----------------------------------------------------------------------<br/>" +
                              "Please click on the following link to publish : " +
                              "" + visionweb + "/umbraco/actions/editcontent.aspx?id=" + ViewState["articlenode"];

                helper.MailWithoutCCEnq(Session["FoEmail"].ToString(), toEmail, "Success Story to publish", body, false, true);
                Response.Redirect(currentURL + "?msg=1");
            }
            else
            {
                helper.MailWithoutCCEnq(Session["FoEmail"].ToString(), toEmail, "FAILED: Success Story to publish", "", false, true);
                Response.Redirect(currentURL + "?msg=2");
            }
        }

        protected void SaveDraft(object sender, EventArgs e)
        {
            if (SaveSuccessStories((Request.QueryString["articleid"] == null ? 0: Convert.ToInt32(Request.QueryString["articleid"]))))
                Response.Redirect(currentURL + "?msg=3");
            else
                Response.Redirect(currentURL + "?msg=4");   
        }

        protected bool SaveSuccessStories(int articleid)
        {
            try
            {
                string title = txtName.Text;

                Document doc;//= new Document(articleid);
                doc = articleid == 0 ? Document.MakeNew(title, DocumentType.GetByAlias("StudioSuccessStories"), User.GetUser(0), (int)ViewState["nodeId"]) :
                                        new Document(articleid);

                doc.Text = title;
                doc.getProperty("name").Value = txtName.Text;
                doc.getProperty("clientId").Value = txtClientId.Text;
                doc.getProperty("story").Value = txtStory.Text;
                doc.getProperty("result").Value = txtResult.Text;
                doc.getProperty("videoUrl").Value = txtVideoUrl.Text;
                doc.getProperty("studioId").Value = Session["StudioId"].ToString();

                //dewi 2/10/2014 -- studio success story set template
                if ((int)ViewState["nodeId"] != 26723)
                {
                    doc.Template = Template.GetByAlias("Studio Success Story").Id;
                }

                ViewState["videoUrl"] = doc.getProperty("videoUrl").Value;
                ViewState["articlenode"] = doc.Id;
                string path = "/images/success_stories/" + Session["StudioId"] + "/";

                if (FileUploadBeforePhoto.HasFile)
                {
                    string rawurl = Request.RawUrl.Replace("&msg=photofailed", "");

                    if ((FileUploadBeforePhoto.PostedFile.ContentType.Equals("image/jpeg") ||
                        FileUploadBeforePhoto.PostedFile.ContentType.Equals("image/jpg") ||
                        FileUploadBeforePhoto.PostedFile.ContentType.Equals("image/png") ||
                        FileUploadBeforePhoto.PostedFile.ContentType.Equals("image/gif")) &&
                        FileUploadBeforePhoto.PostedFile.ContentLength < 5243000
                        )
                    {
                        string fileNameBefore = txtName.Text.Replace(" ","") + doc.Id + "-Before" + Path.GetExtension(FileUploadBeforePhoto.FileName);
                       
                        Bitmap originalBMPBefore = new Bitmap(FileUploadBeforePhoto.FileContent);

                        if ((SaveImgAndResize(fileNameBefore, originalBMPBefore, path, 420, 640)))
                        {
                            doc.getProperty("imageUrlBefore").Value = Session["StudioId"] + "/" + fileNameBefore;
                        }
                    }
                }

                if (FileUploadAfterPhoto.HasFile)
                {
                    if ((FileUploadAfterPhoto.PostedFile.ContentType.Equals("image/jpeg") ||
                        FileUploadAfterPhoto.PostedFile.ContentType.Equals("image/jpg") ||
                        FileUploadAfterPhoto.PostedFile.ContentType.Equals("image/png") ||
                        FileUploadAfterPhoto.PostedFile.ContentType.Equals("image/gif")) &&
                        FileUploadAfterPhoto.PostedFile.ContentLength < 5243000)
                    {
                        string fileNameAfter = txtName.Text.Replace(" ", "") + doc.Id + "-After" + Path.GetExtension(FileUploadAfterPhoto.FileName);
                        //string path = "/images/success_stories/" + Session["StudioId"].ToString() + "/";

                        Bitmap originalBMPAfter = new Bitmap(FileUploadAfterPhoto.FileContent);

                        if ((SaveImgAndResize(fileNameAfter, originalBMPAfter, path, 420, 640)))
                        {
                            doc.getProperty("imageUrlAfter").Value = Session["StudioId"] + "/" + fileNameAfter;
                        }

                    }
                }

                if(ModelReleaseFileUpload.HasFile)
                {
                    string modelReleaseName = txtName.Text.Replace(" ", "") + doc.Id + "-ModelRelease" + Path.GetExtension(ModelReleaseFileUpload.FileName);
                    ModelReleaseFileUpload.SaveAs(Server.MapPath(path + modelReleaseName));
                    doc.getProperty("modelReleaseForm").Value = Session["StudioId"] + "/" + modelReleaseName;
                }

                ViewState["beforePhoto"] = doc.getProperty("imageUrlBefore").Value;
                ViewState["afterPhoto"] = doc.getProperty("imageUrlAfter").Value;

                doc.Save();
                return true;
            }
            catch (Exception ex)
            {
                VPTFacilities helper = new VPTFacilities();
                helper.MailErrorMsg(Session["FoEmail"].ToString(), "ERROR in EDIT Success Story", Request.RawUrl + "<br/>" + ex, false, true);
                return false;
            }
            
            //doc.Publish(User.GetCurrent());
            //umbraco.library.UpdateDocumentCache(doc.Id);
            //Response.Redirect(redirection);
        }

        protected void ShowSuccessStoriesData(int nodeid)
        {
            try
            {
                Document article = new Document(nodeid);

                txtName.Text = article.getProperty("name").Value.ToString();

                txtClientId.Text = article.getProperty("clientId").Value.ToString();

                txtResult.Text = article.getProperty("result").Value.ToString();

                txtStory.Text = article.getProperty("story").Value.ToString();

                txtVideoUrl.Text = article.getProperty("videoUrl").Value.ToString();

                litBeforePhoto.Text = "<img alt=\"Picture\" src=\"/images/success_stories/" + article.getProperty("imageUrlBefore").Value +
                                  "\" style=\"max-height:200px;\"/>";

                litAfterPhoto.Text = "<img alt=\"Picture\" src=\"/images/success_stories/" + article.getProperty("imageUrlAfter").Value +
                                  "\" style=\"max-height:200px;\"/>";

                LiteralModelRelease.Text = article.getProperty("modelReleaseForm").Value.ToString() != "" ? "<br/><a href=\"/images/success_stories/" + article.getProperty("modelReleaseForm").Value + "\">Submitted Form</a>" : "";

                lblNewPostOrEdit.Text = "Edit Success Story  |  <a class=\"btn gtn-sm btn-primary\" href=\"" + currentURL + "\">Create New Success Story</a>";

            }
            catch(Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }

        public bool SaveImgAndResize(string filename, Bitmap originalBMP, string path, int width, int height)// REPETITION FROM MENUS
        {
            try
            {
                // Find the fileUpload control
                //string filename = mealImageFileUpload.FileName;

                // Find the fileUpload control
                //string path = "/images/success_stories/" + Session["StudioId"].ToString() + "/";

                // Check if the directory we want the image uploaded to actually exists or not
                if (!Directory.Exists(Server.MapPath(path)))
                {
                    Directory.CreateDirectory(Server.MapPath(path));
                }

                // Specify the upload directory
                string directory = Server.MapPath(path);

                // Create a bitmap of the content of the fileUpload control in memory
                //Bitmap originalBMP = new Bitmap(FileUploadBeforePhoto.FileContent);

                EditArticle ea = new EditArticle();
                //Image saveCroppedImage = ea.SaveCroppedImage(originalBMP, width, height);
                Image saveCroppedImage = ea.SaveCroppedImage(originalBMP, 350);
                //Image resizeImageDistort = ResizeImageDistort(originalBMP, 250, true);

                // Save the new graphic file to the server
                saveCroppedImage.Save(directory + filename);
                //resizeImageDistort.Save(directory + "resizeImageDistort" + filename);

                // Deallocate them once finish
                originalBMP.Dispose();
                saveCroppedImage.Dispose();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void updateTitleAndRepublish()
        {
            DocumentType dt = DocumentType.GetByAlias("StudioPage");

            List<Document> docs = Document.GetDocumentsOfDocumentType(dt.Id).Where(x => x.IsTrashed == false &&
                                            x.Published).ToList();


            foreach (Document document in docs)
            {
                string title = document.getProperty("contentTitle").Value.ToString();

                title = title.Replace("Vision", "Vision Personal Training");

                document.getProperty("contentTitle").Value = title;
                //var alternateTemplate = 0;
                //var templates = umbraco.cms.businesslogic.template.Template.GetAllAsList();
                //foreach (var t in templates.Where(t => t.Alias == "VPT Studio Page"))
                //    alternateTemplate = t.Id;

                //document.Template = alternateTemplate;

                document.Save();
                //document.Publish(User.GetCurrent());
            }
        }

        public string EditStudioDetailsMsg(string msg)
        {
            string text = "";
            switch (msg)
            {
                case "1":
                    text =
                        "<div class=\"row ArticleResultMsg\"><div class=\"col-md-12\"><h4>*** Content has successfully saved and sent to HQ for approval ***</h4></div></div>"; break;
                case "2":
                    text =
                        "<div class=\"row ArticleResultMsg\"><div class=\"col-md-12\"><h4>*** Content has failed to sent to publish. Please contact HQ. ***</h4></div></div>"; break;
                case "3":
                    text =
                        "<div class=\"row ArticleResultMsg\"><div class=\"col-md-12\"><h4>*** Content has successfully saved but has not been sent to HQ for approval ***</h4></div></div>"; break;
                case "4":
                    text =
                        "<div class=\"row ArticleResultMsg\"><div class=\"col-md-12\"><h4>*** Content has failed to saved. Please contact HQ. ***</h4></div></div>"; break;

            }
            return text;
        }

    }
}