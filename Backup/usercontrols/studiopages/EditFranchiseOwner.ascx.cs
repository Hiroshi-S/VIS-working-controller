using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using uComponents.Core;
using umbraco.cms.businesslogic.web;
using umbraco.presentation.nodeFactory;
using umbraco.BusinessLogic;

namespace VisionPersonalTrainingProject.usercontrols.studiopages
{
    public partial class EditFranchiseOwner : System.Web.UI.UserControl
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

            currentURL = "/studioedit/franchise-owner-edit/";

            if (!Page.IsPostBack)
            {
                Node studioNode = FindStudio();

                lblNewPostOrEdit.Text = "Create New Owner Profile";

                // show studio data only when we have found a valid node
                if (studioNode != null)
                {
                    //LoadMediaItems();
                    EditArticle ea = new EditArticle();
                    lblStudioTitle.Text = ea.ShowStudioData(studioNode);

                    //edit mode
                    if (Request.QueryString["articleid"] != null)
                    {
                        ShowFranchiseOwnerData(Convert.ToInt32(Request.QueryString.Get("articleid")));
                        RequiredFieldValidator3.Enabled = false;
                        //litPostLists.Text = "<div class=\"row\"><div class=\"col-md-12\"><button class=\"btn btn-lg btn-primary btn-visionred\">Create New Post</button></div></div>";
                    }
                }
                //updateTitleAndRepublish();
                EditSuccessStories ess = new EditSuccessStories();
                litFOLists.Text = ess.ShowDocumentListChildren(Convert.ToInt32(ViewState["nodeId"]), "FranchiseOwner", currentURL);

                if (Request.QueryString["msg"] != null)
                {
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
        /*
        protected string ShowFranchiseOwnerList()
        {
            string ret = "";
            int studioId = Convert.ToInt32(ViewState["nodeId"]);
            
            var node = new Node(studioId);
           
            foreach (Node childNode in node.Children)
            { 
               // Document document = new Document(childNode.Id);
               // string alias = document.ContentType.Alias;
                if (childNode.NodeTypeAlias == "FranchiseOwner")
                {
                    //Do something
                    ret += "<div class='row articleList'><div class='col-md-12'><h4>" + childNode.Name + "</h4>" +
                           //"</div></div>";
                           "<p>" + (childNode.GetProperty("hQApproved").Value == "1" ? "<span style=\"color:green;\"><i class=\"fa fa-check-square\"></i> Featured</span>" : "<span style=\"color:red;\"><i class=\"fa fa-square\"></i> Waiting for Approval</span>") + " | " +
                           "<a href=\"" + currentURL + "?articleid=" + childNode.Id + "\">Edit</a>" +
                           "</p></div></div>";

                }
            }
             

            if (ret.Length < 1)
            {
                return "<div class='row'><div class='col-md-12'><p>No Owner Profiles</p>" +
                       "</div></div>";
            }
            return ret;
        }
        */
        protected void PublishPost(object sender, EventArgs e)
        {
            string toEmail = ConfigurationManager.AppSettings["publishemails"];
            string visionweb = ConfigurationManager.AppSettings["VisionWebUrl"];

            int articleid = (Request.QueryString["articleid"] == null
                                 ? 0
                                 : Convert.ToInt32(Request.QueryString["articleid"]));

            VPTFacilities helper = new VPTFacilities();

            if (SaveFranchiseOwner(articleid))
            {
                // Send email to admin
                string body = Session["StudioUser"] + " sent a Franchise Owner Profile for approval.<br/><br/>" +
                              "----------------------------------------------------------------------" +
                              "<h1>" + txtName.Text + "</h1>" +
                              "<img src='" + visionweb + "/images/franchise_owner/" + 
                              "/" + ViewState["picName"] + "'/>" +
                              "<p>" + txtBlurb.Text + "</p>" +
                              "----------------------------------------------------------------------<br/>" +
                              "Please click on the following link to publish : " +
                              "" + visionweb + "/umbraco/actions/editcontent.aspx?id=" + ViewState["articlenode"];

                helper.MailWithoutCCEnq(Session["FoEmail"].ToString(), toEmail, "Franchise Owner profile to publish", body, false, true);
                Response.Redirect(currentURL + "?msg=1");

            }
            else
            {
                helper.MailWithoutCCEnq(Session["FoEmail"].ToString(), toEmail, "FAILED: Franchise Owner profile to publish", "", false, true);
                Response.Redirect(currentURL + "?msg=2");
            }
        }

        protected void SaveDraft(object sender, EventArgs e)
        {
            if (SaveFranchiseOwner((Request.QueryString["articleid"] == null ? 0
                                        : Convert.ToInt32(Request.QueryString["articleid"]))))
                Response.Redirect(currentURL + "?msg=3");
            else
                Response.Redirect(currentURL + "?msg=4");
        }

        protected bool SaveFranchiseOwner(int articleid)
        {
            try
            {
                string title = txtName.Text;

                string redirection = "";
                Document doc;//= new Document(articleid);
                if (articleid == 0)
                    doc = Document.MakeNew(title, DocumentType.GetByAlias("FranchiseOwner"), User.GetUser(0), (int) ViewState["nodeId"]);
                else doc = new Document(articleid);

                doc.Text = title;
                doc.getProperty("fullName").Value = title;
                doc.getProperty("blurb").Value = txtBlurb.Text; //blurb
                doc.getProperty("hQApproved").Value = "0";
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
                        string path = "/images/franchise_owner/" + Session["StudioId"].ToString() + "/";

                        Bitmap originalBMP = new Bitmap(FileUploadPicture.FileContent);

                        if ((SaveImgAndResize(fileName, originalBMP, path, 125, 165)))
                        {
                            doc.getProperty("photo").Value = Session["StudioId"].ToString() + "/" + fileName;
                            redirection = currentURL + "&msg=success";

                        }
                        else
                        {
                            redirection = currentURL + "&msg=photofailed";
                        }
                    }
                }
                
                ViewState["picName"] = doc.getProperty("photo").Value;

                doc.Save();
                return true;
            }
            catch(Exception ex)
            {
                VPTFacilities helper = new VPTFacilities();
                helper.MailErrorMsg(Session["FoEmail"].ToString(), "ERROR in Edit Franchise Owner", Request.RawUrl + "<br/>" + ex, false, true);
             
                return false;
            }
            
            //doc.Publish(User.GetCurrent());
            //umbraco.library.UpdateDocumentCache(doc.Id);
            //Response.Redirect(redirection);
        }

        protected void ShowFranchiseOwnerData(int nodeid)
        {
            Document article = new Document(nodeid);

            txtName.Text = article.getProperty("fullName").Value.ToString();

            txtBlurb.Text = article.getProperty("blurb").Value.ToString();

            //.Text = (string)article.getProperty("bodyText").Value;

            //txtTags.Text = (string)article.getProperty("tags").Value;

            litPicture.Text = "<img alt=\"Illustration Picture\" src=\"/images/franchise_owner/" + (string)article.getProperty("photo").Value +
                              "\" style=\"max-height:200px;\"/>";

            lblNewPostOrEdit.Text = "Edit Owner Profile  |  <a class=\"btn gtn-sm btn-primary\" href=\"" + currentURL + "\">Create New Owner Profile</a>";

        }

        public bool SaveImgAndResize(string filename, Bitmap originalBMP, string path, int width, int height)// REPETITION FROM MENUS
        {
            try
            {
                if (!Directory.Exists(Server.MapPath(path)))
                {
                    Directory.CreateDirectory(Server.MapPath(path));
                }

                string directory = Server.MapPath(path);

                EditArticle ea = new EditArticle();
                Image saveCroppedImage = ea.SaveCroppedImage(originalBMP, width, height);
                
                saveCroppedImage.Save(directory + filename);
                
                originalBMP.Dispose();
                saveCroppedImage.Dispose();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        //only one time execution!!
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
    }
}