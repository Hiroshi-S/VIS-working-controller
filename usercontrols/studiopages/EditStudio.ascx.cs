using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using uComponents.Core;
using umbraco.presentation.nodeFactory;
using umbraco.BusinessLogic;
using umbraco.cms.businesslogic.web;
using umbraco.cms.businesslogic.media;
using System.Data;
using System.Xml;
using System.IO;

namespace VisionPersonalTrainingProject.usercontrols.studiopages
{
    public partial class EditStudio : System.Web.UI.UserControl
    {
        private int StudioId;

        private string _publishEmail;

        public string publishEmail 
        {
            get { return _publishEmail; }
            set { _publishEmail = value;} 
        }
        
        protected void Page_Init(object sender, EventArgs e)
        {
            ((umbraco.UmbracoDefault)this.Page).ValidateRequest = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.CacheControl = "no-cache";

            if (Session["StudioId"] == null || Session["StudioUser"] == null || Session["RoleId"] == null)
            {
                this.Response.Redirect("/franchise-login/");
            }
            
            if (!Page.IsPostBack)
            {
                //if (Session["StudioId"].ToString() == null)
                //{
                //    Response.Redirect("~/");
                //}
                                  
                Node studioNode = FindStudio();

                // show studio data only when we have found a valid node
                if (studioNode != null)
                    {
                        LoadMediaItems();
                        ShowStudioData(studioNode);
                        LoadFranchiseOwnerItems();
                    }

                LoadEnquiryTypes();

                //load up studio contact email details
                if (int.TryParse(Session["StudioId"].ToString(), out StudioId))
                {
                    ViewState.Add("studioid", StudioId);
                    LoadStudioContacts(StudioId);
                }
            }

        }
        
        #region "Events"

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveStudioData(uQuery.GetNode(Convert.ToInt32(ViewState["nodeId"])));
        }
        
        #endregion

        #region "functions and methods"

        void LoadEnquiryTypes()
        {
            DataTable dtEnquiryTypes = VPTFacilities.LoadEnquiryTypes();
            ListItem lstEnquiryType;
            CheckBoxList cbEnquiryTypes = null;
            for (int CheckboxListCount = 1; CheckboxListCount < 4; CheckboxListCount++)
            {
                cbEnquiryTypes = (CheckBoxList)FindControl("cblStudioContact" + (CheckboxListCount));
                for (int Count = 0; Count < dtEnquiryTypes.Rows.Count; Count++)
                {
                    lstEnquiryType = new ListItem();
                    lstEnquiryType.Value = dtEnquiryTypes.Rows[Count]["enquirytypeid"].ToString();
                    lstEnquiryType.Text = dtEnquiryTypes.Rows[Count]["enquirytypedescription"].ToString();
                    cbEnquiryTypes.Items.Add(lstEnquiryType);
                }
            }
        }

        void LoadStudioContacts(int StudioId)
        {
            TextBox tbEmail;
            CheckBoxList cbEnquiryTypes = null;
            DataTable dtStudioContacts;
            dtStudioContacts = VPTFacilities.StudioContactGet(StudioId);
            string Email = "";
            int EmailCount = 1;

            for (int Count = 0; Count < dtStudioContacts.Rows.Count; Count++)
            {
                if (Email != dtStudioContacts.Rows[Count]["contact"].ToString())
                {
                    tbEmail = (TextBox)FindControl("tbStudioEmail" + (EmailCount));
                    cbEnquiryTypes = (CheckBoxList)FindControl("cblStudioContact" + (EmailCount));
                    tbEmail.Text = dtStudioContacts.Rows[Count]["contact"].ToString();
                    EmailCount++;
                    Email = tbEmail.Text;
                }
                //set checked state
                cbEnquiryTypes.Items.FindByValue(dtStudioContacts.Rows[Count]["enquirytypeid"].ToString()).Selected = true;
            }
        }

        void LoadMediaItems() 
        {
            Media mediaFolder = new Media(2046, false); // TODO: Remove hard coded MediaFolderId

            Dictionary<string, int> mediaList = new Dictionary<string, int>();

            foreach (Media item in mediaFolder.Children)
            {
                mediaList.Add(item.Text, item.Id);
            }
            /*
            ddlIntroImage.DataSource = mediaList;
            ddlIntroImage.DataTextField = "key";
            ddlIntroImage.DataValueField = "value";
            ddlIntroImage.DataBind();

            ddlBubbleImage.DataSource = mediaList;
            ddlBubbleImage.DataTextField = "key";
            ddlBubbleImage.DataValueField = "value";
            ddlBubbleImage.DataBind();
            

            ddlWhatsOn1.Items.Clear();
            ddlWhatsOn1.Items.Add(new ListItem("choose one", string.Empty));
            ddlWhatsOn1.AppendDataBoundItems = true;
            ddlWhatsOn1.DataSource = mediaList;
            ddlWhatsOn1.DataTextField = "key";
            ddlWhatsOn1.DataValueField = "value";
            ddlWhatsOn1.DataBind();
 */
            
        }

        /// <summary>
        /// Finds the right studio based on the querystring param
        /// </summary>
        /// <returns></returns>
        Node FindStudio() 
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


        /// <summary>
        /// Shows the studio data in the edit boxes
        /// </summary>
        /// <param name="studioNode"></param>
        void ShowStudioData(Node studioNode) 
        {
            // content
            lblTitle.Text = studioNode.GetProperty("contentTitle").Value;
            //txtTitle.Text = studioNode.GetProperty("contentTitle").Value;
            txtIntro.Text = studioNode.GetProperty("introContent").Value;
/*
            // introImage
            Media introImage = new Media(Convert.ToInt32(studioNode.GetProperty("introImage").Value));
            imgIntro.Src = introImage.getProperty("umbracoFile").Value.ToString();
            ViewState["introImage"] = introImage.Id;

            ddlIntroImage.SelectedValue = introImage.Id.ToString();

            

            // bubbleImage
            Media bubbleImage = new Media(Convert.ToInt32(studioNode.GetProperty("bubbleImage").Value));
            imgBubble.Src = bubbleImage.getProperty("umbracoFile").Value.ToString();
            ViewState["bubbleImage"] = bubbleImage.Id;

            ddlBubbleImage.SelectedValue = bubbleImage.Id.ToString();

            txtBubbleName.Text = studioNode.GetProperty("bubbleName").Value;
            txtBubbleText.Text = studioNode.GetProperty("bubbleText").Value;
          */  
            // contact details
            txtAddress1.Text = studioNode.GetProperty("studioAddressLine1").Value;
            txtAddress2.Text = studioNode.GetProperty("studioAddressLine2").Value;
            txtContact.Text = studioNode.GetProperty("studioContactPerson").Value;
            txtPhone.Text = studioNode.GetProperty("studioPhone").Value;
            txtFax.Text = studioNode.GetProperty("studioFax").Value;
            txtEmail.Text = studioNode.GetProperty("studioEmail").Value;
            txtDirections.Text = studioNode.GetProperty("studioDirections").Value;
            txtAccess.Text = studioNode.GetProperty("studioAccessibility").Value;
            //txtPartners.Text = studioNode.GetProperty("studioPartners").Value.Replace("<br>", "\r\n").Replace("<br/>", "\r\n").Replace("<br />", "\r\n");

            // Opening hours
            txtDay1.Text = studioNode.GetProperty("openingHoursDay1").Value;
            txtHours1.Text = studioNode.GetProperty("openingHours1").Value;
            txtDay2.Text = studioNode.GetProperty("openingHoursDay2").Value;
            txtHours2.Text = studioNode.GetProperty("openingHours2").Value;
            txtDay3.Text = studioNode.GetProperty("openingHoursDay3").Value;
            txtHours3.Text = studioNode.GetProperty("openingHours3").Value;
            txtDay4.Text = studioNode.GetProperty("openingHoursDay4").Value;
            txtHours4.Text = studioNode.GetProperty("openingHours4").Value;
            txtDay5.Text = studioNode.GetProperty("openingHoursDay5").Value;
            txtHours5.Text = studioNode.GetProperty("openingHours5").Value;
            txtDay6.Text = studioNode.GetProperty("openingHoursDay6").Value;
            txtHours6.Text = studioNode.GetProperty("openingHours6").Value;
            txtDay7.Text = studioNode.GetProperty("openingHoursDay7").Value;
            txtHours7.Text = studioNode.GetProperty("openingHours7").Value;
            /*
            string PartnerLinks = "";
            if (studioNode.GetProperty("partnerLinks") != null)
            {
                PartnerLinks = studioNode.GetProperty("partnerLinks").Value;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(new StringReader(PartnerLinks));
                System.Xml.XmlElement root = xmlDoc.DocumentElement;
                System.Xml.XmlNodeList nl = root.SelectNodes("link");
                int PartnerCount = 1;
                TextBox tbPartnerName;
                TextBox tbPartnerLink;
                foreach (System.Xml.XmlNode xnode in nl)
                {
                    if (PartnerCount < 11)  //only 10 partners can be edited through studio edit page
                    {
                        tbPartnerName = (TextBox)FindControl("txtPartnerName" + (PartnerCount));
                        tbPartnerLink = (TextBo x)FindControl("txtPartnerLink" + (PartnerCount));
                        tbPartnerName.Text = xnode.Attributes["title"].Value;
                        tbPartnerLink.Text = xnode.Attributes["link"].Value;
                        PartnerCount++;

                    }
                }

            }
       
            
            LoadWhatsOnItems();
           */
        }

        /// <summary>
        /// Saves the edited studio data to umbraco
        /// </summary>
        /// <param name="studioNode"></param> 
        void SaveStudioData(Node studioNode)
        {
            try
            {
                Document doc = new Document(studioNode.Id);

                // content
                //doc.getProperty("contentTitle").Value = txtTitle.Text.Trim();
                //doc.getProperty("introImage").Value = ViewState["introImage"];
                //doc.getProperty("introContent").Value = txtIntro.Text.Trim();
                //doc.getProperty("bubbleImage").Value = ViewState["bubbleImage"];
                //doc.getProperty("bubbleName").Value = txtBubbleName.Text.Trim();
                //doc.getProperty("bubbleText").Value = txtBubbleText.Text.Trim();

                // contact details
                doc.getProperty("studioAddressLine1").Value = txtAddress1.Text.Trim();
                doc.getProperty("studioAddressLine2").Value = txtAddress2.Text.Trim();
                doc.getProperty("studioContactPerson").Value = txtContact.Text.Trim();
                doc.getProperty("studioPhone").Value = txtPhone.Text.Trim();
                doc.getProperty("studioFax").Value = txtFax.Text.Trim();
                doc.getProperty("studioEmail").Value = txtEmail.Text.Trim();
                doc.getProperty("studioDirections").Value = txtDirections.Text.Trim();
                doc.getProperty("studioAccessibility").Value = txtAccess.Text.Trim();
                //doc.getProperty("studioPartners").Value = txtPartners.Text.Trim().Replace("<br>", "\r\n").Replace("<br/>", "\r\n").Replace("<br />", "\r\n");

                //get studio partners
                string StudioPartners = "<links>";
                TextBox tbPartnerName;
                TextBox tbPartnerLink;
                for (int Count = 1; Count < 11; Count++)
                {

                    tbPartnerName = (TextBox)FindControl("txtPartnerName" + (Count));
                    tbPartnerLink = (TextBox)FindControl("txtPartnerLink" + (Count));

                    if (tbPartnerName.Text.Length > 0)  //need a partner name at minimum
                    {
                        if (tbPartnerLink.Text == "http://") //no link entered so clear it out
                            tbPartnerLink.Text = "";
                        //check if a link is entered with no http or https prefix and add if so
                        if (tbPartnerLink.Text.Length > 0 && tbPartnerLink.Text.IndexOf("http://") < 0 && tbPartnerLink.Text.IndexOf("https://") < 0)
                            tbPartnerLink.Text = "http://" + tbPartnerLink.Text;
                        StudioPartners = StudioPartners + " <link title=\"" + tbPartnerName.Text + "\"";
                        StudioPartners = StudioPartners + " link=\"" + tbPartnerLink.Text + "\" type=\"external\" newwindow=\"1\" />";
                    }
                }
                StudioPartners = StudioPartners + "</links>";
                doc.getProperty("partnerLinks").Value = StudioPartners;

                // Opening hours
                doc.getProperty("openingHoursDay1").Value = txtDay1.Text;
                doc.getProperty("openingHours1").Value = txtHours1.Text;
                doc.getProperty("openingHoursDay2").Value = txtDay2.Text;
                doc.getProperty("openingHours2").Value = txtHours2.Text;
                doc.getProperty("openingHoursDay3").Value = txtDay3.Text;
                doc.getProperty("openingHours3").Value = txtHours3.Text;
                doc.getProperty("openingHoursDay4").Value = txtDay4.Text;
                doc.getProperty("openingHours4").Value = txtHours4.Text;
                doc.getProperty("openingHoursDay5").Value = txtDay5.Text;
                doc.getProperty("openingHours5").Value = txtHours5.Text;
                doc.getProperty("openingHoursDay6").Value = txtDay6.Text;
                doc.getProperty("openingHours6").Value = txtHours6.Text;
                doc.getProperty("openingHoursDay7").Value = txtDay7.Text;
                doc.getProperty("openingHours7").Value = txtHours7.Text;

                
                doc.Save();

                string Feedback = SaveStudioContacts();

                // Send email to admin
                string body = "The following page has been edited: http://www.visionpt.com.au/umbraco/actions/editcontent.aspx?id=" + studioNode.Id;
                VPTFacilities helper = new VPTFacilities();
                helper.Mail(txtEmail.Text.Trim(), _publishEmail, "Studio page edit", body, false, true);

                pnlFields.Visible = false;
                lblFeedback.Text = string.Format("studio '{0}' updates sent to HQ for approval.", studioNode.Name);
                if (Feedback.Length > 0)
                {
                    lblFeedback.Text = lblFeedback.Text + string.Format(" Messages '{0}'.", Feedback);
                }
                lblFeedback.Visible = true;

            }
            catch (Exception ex)
            {
                // Add exception to the umbraco log table
                umbraco.BusinessLogic.Log.Add(LogTypes.Error, studioNode.Id, ex.Message);
                lblFeedback.Text = "An error occured, please try again later or contact your administrator";
                lblFeedback.Visible = true;
                throw;
            }
        }

        /// <summary>
        /// Create whats on item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CreateFranchiseOwner()
        {
            // get the right studio page id
            int studioId = Convert.ToInt32(ViewState["nodeId"]);
            DocumentType dt = DocumentType.GetByAlias("FranchiseOwner");
            // create a new childnode
            Document FranchiseOwnerNode = Document.MakeNew(txtFOName.Text, dt, User.GetUser(0), studioId);

            FranchiseOwnerNode.getProperty("fullName").Value = txtFOName.Text;
            FranchiseOwnerNode.getProperty("email").Value = txtFOEmail.Text;
            FranchiseOwnerNode.getProperty("blurb").Value = txtFOBio.Text;

            FranchiseOwnerNode.Save();

            // Publish is only possible if the parent node is published
            try
            {
                FranchiseOwnerNode.Publish(User.GetUser(0));
                //Document.RePublishAll();
                umbraco.library.UpdateDocumentCache(FranchiseOwnerNode.Id);
                umbraco.library.RefreshContent();
            }
            catch
            { }

            LoadFranchiseOwnerItems();

        }

        /// <summary>
        /// Load the existing Whats On elements
        /// </summary>
        void LoadFranchiseOwnerItems()
        {
            int studioId = Convert.ToInt32(ViewState["nodeId"]);
            Node studioNode = uQuery.GetNode(studioId);
            
            var node = new Node(studioId);
            foreach (Node childNode in node.Children)
            {
                var child = childNode;
                if (child.NodeTypeAlias == "FranchiseOwner")
                {
                    //Do something
                    litStudioOwner.Text += child.Name;
                }
            }
        }

        protected void btnCreateWhatsOn_Click(object sender, EventArgs e)
        {
            Page.Validate("ValGrpAddStudio");
            if (!Page.IsValid)
            {
                return;
            }

            CreateFranchiseOwner();
        }

        protected string GetImageUrl(int mediaId)
        {
            Media whatsOnImage = new Media(mediaId);
            return whatsOnImage.getProperty("umbracoFile").Value.ToString();
        }

        #endregion

        protected string SaveStudioContacts()
        {
            //save studio contacts
            TextBox tbEmail;
            bool EmailSelected = false;
            CheckBoxList cbEnquiryTypes;
            DataRow drRow;
            int StudioId = Convert.ToInt32(ViewState["studioid"]);

            DataTable dtStudioContacts;
            dtStudioContacts = VPTFacilities.StudioContactGet(StudioId);
            dtStudioContacts.Clear();
            for (int ControlCount = 1; ControlCount < 4; ControlCount++)
            {
                tbEmail = (TextBox)FindControl("tbStudioEmail" + (ControlCount));
                cbEnquiryTypes = (CheckBoxList)FindControl("cblStudioContact" + (ControlCount));
                foreach (ListItem lstItem in cbEnquiryTypes.Items)
                {
                    if (tbEmail.Text.IndexOf("@") > 0 && lstItem.Selected)
                    {
                        EmailSelected = true;
                        drRow = dtStudioContacts.NewRow();
                        drRow[0] = StudioId;
                        drRow[1] = lstItem.Value;
                        drRow[2] = "email";
                        drRow[3] = tbEmail.Text;

                        dtStudioContacts.Rows.Add(drRow);

                    }
                }
            }
            if (EmailSelected)
            {
                VPTFacilities.StudioContactUpdate(dtStudioContacts, StudioId);
                return "";
            }
            else
                //display message to enter email address
                return "Please enter at least one email address with a selected enquiry type";

        }

    }
}