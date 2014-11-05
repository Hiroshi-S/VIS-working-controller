using System;
using CookComputing.XmlRpc;
using System.Collections;

// Replace the hostname in this example with your system Virtual Host
[XmlRpcUrl("http://www.vision6.com.au/api/xmlrpcserver.php?version=1.2")]
public class NewsLetterAPI : XmlRpcClientProtocol
{

    // Login/Authentication Methods

    [XmlRpcMethod("login")]
    public Object login(string username, string password)
    {
        string response;

        try
        {
            response = (string)Invoke("login", new Object[] { username, password });
        }
        catch (Exception ex)
        {
            // Login failed

            return false;
        }

        this.Url = response;
        return true;
    }

    [XmlRpcMethod("isLoggedIn")]
    public Object isLoggedIn()
    {
        XmlRpcStruct response;
        try
        {
            response = (XmlRpcStruct)Invoke("isLoggedIn", new Object[] { });
        }
        catch
        {
            return false;
        }

        if (response["0"].GetType() == typeof(bool))
        {
            bool success = (bool)response["0"];
            return success;
        }
        else
        {
            return false;
        }
    }


    // Folders methods

    [XmlRpcMethod("searchFolders")]
    public Object searchFolders(string type, Object[] criteria)
    {
        return Invoke("searchFolders", new Object[] { type, criteria });
    }


    // Database Methods

    [XmlRpcMethod("addDatabase")]
    public Object addDatabase(XmlRpcStruct database)
    {
        return Invoke("addDatabase", new Object[] { database });
    }

    [XmlRpcMethod("addField")]
    public Object addField(int database_id, XmlRpcStruct field)
    {
        return Invoke("addField", new Object[] { database_id, field });
    }


    // Contacts Methods

    [XmlRpcMethod("addContacts")]
    public Object addContacts(int database_id, XmlRpcStruct contacts)
    {
        return Invoke("addContacts", new Object[] { database_id, contacts });
    }

    [XmlRpcMethod("unsubscribeContact")]
    public Object unsubscribeContact(string email, int database_id)
    {
        return Invoke("unsubscribeContact", new Object[] { email, database_id });
    }


    /*
    [XmlRpcMethod("searchContacts")]
    public Object searchContacts(int db_id, APISearch searches)
    {
        try
        {
            return Invoke("searchContacts", new Object[] {db_id, searches, offset, limit, order, order_direction});
        }
        catch (Exception e)
        {
            return null;
        }

    }
    */


    // Emails Methods

    [XmlRpcMethod("searchEmails")]
    public Object searchEmails(XmlRpcStruct criteria, int limit, int offset)
    {
        return Invoke("searchEmails", new Object[] { criteria, limit, offset });
    }


    // Campaigns Methods

    [XmlRpcMethod("getCampaignStatus")]
    public Object getCampaignStatus(int campaign_id)
    {
        return Invoke("getCampaignStatus", new Object[] { campaign_id });
    }

    [XmlRpcMethod("getCampaignStatistics")]
    public Object getCampaignStatistics(int campaign_id)
    {
        return Invoke("getCampaignStatistics", new Object[] { campaign_id });
    }

    [XmlRpcMethod("getCampaignLinkStatistics")]
    public Object getCampaignLinkStatistics(int campaign_id)
    {
        return Invoke("getCampaignLinkStatistics", new Object[] { campaign_id });
    }

    [XmlRpcMethod("getCampaignContactLinks")]
    public Object getCampaignContactLinks(int campaign_id, int send_id)
    {
        return Invoke("getCampaignContactLinks", new Object[] { campaign_id, send_id });
    }

    [XmlRpcMethod("getCampaignOpened")]
    public Object getCampaignOpened(int campaign_id)
    {
        return Invoke("getCampaignOpened", new Object[] { campaign_id });
    }

}
