using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CookComputing.XmlRpc;
using System.Configuration;

namespace VisionPersonalTrainingProject.usercontrols.elements
{
    public partial class NewsletterSubscribe : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubscribe_Click(object sender, EventArgs e)
        {
            NewsLetterAPI api = null;
            lblMessage.Text = "";
            XmlRpcStruct contact = null;
            // Create new API object
            if (api == null)
            {
                api = new NewsLetterAPI();
            }


            // Login
            try
            {
                if ((bool)api.login(ConfigurationManager.AppSettings["newsletterapiusername"].ToString(), ConfigurationManager.AppSettings["newsletterapipwd"].ToString()))
                {
                    // Add contacts
                    try
                    {
                        contact = new XmlRpcStruct();
                        contact["Email"] = tbEmail.Text;
                        contact["Full Name"] = tbName.Text;

                        XmlRpcStruct contacts = new XmlRpcStruct();
                        contacts.Add("1", contact);
                        api.addContacts(System.Convert.ToInt32(ConfigurationManager.AppSettings["newsletterdbid"]), contacts);
                        lblMessage.Text = "Successfully subscribed";

                    }
                    catch (Exception ex)
                    {
                        //display subscribe error
                        lblMessage.Text = "Erorr subscribing, please try again later. " + ex.Message;
                    }
                }
                else
                {
                    //display log in error
                    lblMessage.Text = "Could not log in to newsletter system, please try again later";
                }
            }
            catch (Exception ex)
            {
                //display general error
                lblMessage.Text = "Error encountered: " + ex.Message;
            }


            

        }
        
    }
}