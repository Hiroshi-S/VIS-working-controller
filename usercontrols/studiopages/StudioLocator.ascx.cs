using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SpinningCube.DAL;
using System.Configuration;
using uComponents.Core;
using umbraco.presentation.nodeFactory;

namespace VisionPersonalTrainingProject.usercontrols.studiopages
{
    public partial class StudioLocator : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //figure out if you got a postcode or a studio id
            if (!Page.IsPostBack)
            {
                int StudioID = 0;
                int Postcode = 0;
                DataTable dtStudios = null;
                int.TryParse(Request.QueryString["sid"], out StudioID);
                if (StudioID > 0)
                {
                    //get studio link and redirect
                    Node studioNode = null;
                    studioNode = FindStudio(StudioID.ToString());
                    Response.Redirect(umbraco.library.NiceUrl(studioNode.Id));

                    //dtStudios = VPTFacilities.StudioGetById(StudioID);
                    //if (dtStudios.Rows.Count == 0)
                    //{
                    //    lblErrorMessage.Text = "Invalid studio number";
                    //    return;
                    //}
                }
                else
                {
                    int.TryParse(Request.QueryString["pc"], out Postcode);
                    if (Postcode > 0)
                    {
                        //get studios near
                        if (Request.QueryString["country"] != null)
                        {
                            dtStudios = GetAddressByPostcode(Postcode.ToString(), 0, Request.QueryString["country"]);
                        }
                        else
                        {
                            dtStudios = GetAddressByPostcode(Postcode.ToString(), 0, "AU");
                        }

                        //check to see if proc has returned actual results and not a 0 count
                        if (dtStudios.Columns.Count <= 1)
                        {
                            lblErrorMessage.Text = "No studios found near postcode";
                            return;
                        }
                    }
                    else
                    {
                       // lblErrorMessage.Text = "Please enter a postcode";
                        return;
                    }
                }
                lblStudioList.Text = LoadStudios(dtStudios);
            }

        }

        [System.Web.Script.Services.ScriptMethod]
        [System.Web.Services.WebMethod]
        public static string[] GetPostcodeSuburbList(string prefixText, int count)
        {
            string[] returnValue = new string[] { "2759, St Clair", "2759, Eskine Park" };
            return null;
        }
        
        private string LoadStudios(DataTable dtStudios)
        {
            Type cstype = this.GetType();

            string studioListOutput = string.Empty;
            string studioLocations = string.Empty;
            Node studioNode = null;

            //build up studio locations array so that we can zoom out and see all nearby studios
            for (int i = 0; i < dtStudios.Rows.Count; i++)
            {
                if (i > 0)
                    studioLocations = studioLocations + "|";

                studioLocations = studioLocations + dtStudios.Rows[i]["LL"].ToString() + "," + dtStudios.Rows[i]["StudioName"].ToString().Replace("'", " ");
            }

            for (int i = 0; i < dtStudios.Rows.Count; i++)
            {
                //for Chris - I think here is where you will want to retrieve the content for each studio - you can get the studio id from dtStudios.Rows[i][StudioID]

                studioNode = FindStudio(dtStudios.Rows[i]["StudioID"].ToString());

                if (studioNode != null)
                {
                    studioListOutput += CreateStudioListItem(studioNode, i);

                }

                //plot maps
                this.Page.ClientScript.RegisterStartupScript(cstype, "mapplot" + i, "showAddressNew('map" + i + "','" + dtStudios.Rows[i]["LL"].ToString() + "','" + dtStudios.Rows[i]["StudioName"].ToString().Replace("'", " ") + "','" + studioLocations + "');", true);
                //this.Page.Form.FindControl("map" + i).Visible = true;

            }

            return studioListOutput;

        }

        public DataTable GetAddressByPostcode(String postcode, Int32 startPoint, string country)
        {
            string[] NearestMarkers = new string[4];
            SqlParameter[] spList = new SqlParameter[3];
            spList[0] = new SqlParameter("postcode", postcode);
            spList[1] = new SqlParameter("startPoint", startPoint);
            spList[2] = new SqlParameter("countrycode", country);
            DataTable dtStudios = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["con"].ConnectionString, CommandType.StoredProcedure, "usp_getdistance_studioMain2", spList).Tables[0];
            if (dtStudios.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return dtStudios;
            }
        }

        public DataTable GetStudiosNearPostcode(String postcode, Int32 Kilometres, Int32 startPoint)
        {
            string[] NearestMarkers = new string[4];
            SqlParameter[] spList = new SqlParameter[3];
            spList[0] = new SqlParameter("postcode", postcode);
            spList[1] = new SqlParameter("startPoint", startPoint);
            spList[2] = new SqlParameter("kilometres", Kilometres);
            return SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["con"].ConnectionString, CommandType.StoredProcedure, "usp_GetPostcodeInDistance", spList).Tables[0];
        }

        /// <summary>
        /// Finds the right studio based on the querystring param
        /// </summary>
        /// <returns></returns>
        Node FindStudio(string studioId)
        {
            Node studioNode = null;

            List<Node> nodes = uQuery.GetNodesByType("StudioPage");

            // Find the studio with the right studio id
            foreach (Node item in nodes)
            {
                if (item.GetProperty("studioId").Value.Trim() == studioId)
                {
                    studioNode = item;
                }
            }

            return studioNode;
        }


        /// <summary>
        /// Html output for one studio list item
        /// </summary>
        /// <param name="studioNode"></param>
        /// <returns></returns>
        string CreateStudioListItemOld(Node studioNode, int mapId)
        {
            string output =
                 "<div class='resultsBox'>" +
                     "<div class='resultsLeft'>" +
                         "<h3><a href='" + umbraco.library.NiceUrl(studioNode.Id) + "'>" + studioNode.GetProperty("contentTitle").Value + "</a></h3>" +
                         "<h4>" + studioNode.GetProperty("studioAddressLine1").Value + "<br />" + studioNode.GetProperty("studioAddressLine2").Value + "</h4>" +
                         "<h4>Contact: " + studioNode.GetProperty("studioContactPerson").Value + "<br />" +
                         "Phone: " + studioNode.GetProperty("studioPhone").Value + "<br />" +
                         "Fax: " + studioNode.GetProperty("studioFax").Value + "<br />" +
                         "<h4><a href='" + umbraco.library.NiceUrl(studioNode.Id) + "'>View studio page ></a></h4>" +
                         "<div class='hoursBox'><h4>Opening Hours</h4>";

            for (int i = 1; i < 8; i++)
            {
                if (studioNode.GetProperty("openingHoursDay" + i).Value.Trim() != string.Empty && studioNode.GetProperty("openingHours" + i).Value.Trim() != string.Empty)
                {
                    output += "<p class='day'>" + studioNode.GetProperty("openingHoursDay" + i).Value + "</p><p>" + studioNode.GetProperty("openingHours" + i).Value + "</p>";
                }
            }

            output += "</div><div class='clear'></div></div><div class='imgBox249px264px'>" +
                      "<div id='map" + mapId + "' style='width: 249px; height: 264px'></div>" +
                      "</div><div class='clear'></div></div>";

            return output.ToString();
        }

        string CreateStudioListItem(Node studioNode, int mapId)
        {
            string output =
                 "<div class='row'>" +
                     "<div class='col-md-12'>" +
                         "<h3><a href='" + umbraco.library.NiceUrl(studioNode.Id) + "'>" + studioNode.GetProperty("contentTitle").Value + "</a></h3>" +
                         "</div><div class='col-md-4'>" +
                         "<h4>" + studioNode.GetProperty("studioAddressLine1").Value + "<br />" + studioNode.GetProperty("studioAddressLine2").Value + "</h4>" +
                         "<h4>Contact: " + studioNode.GetProperty("studioContactPerson").Value + "<br />" +
                         "Phone: " + studioNode.GetProperty("studioPhone").Value + "<br />" +
                         "Fax: " + studioNode.GetProperty("studioFax").Value + "<br />" +
                         "<h4><a href='" + umbraco.library.NiceUrl(studioNode.Id) + "'>View studio page ></a></h4>" +
                         "</div>" +
                         "<div class='col-md-4'><h4>Opening Hours</h4>";

            for (int i = 1; i < 8; i++)
            {
                if (studioNode.GetProperty("openingHoursDay" + i).Value.Trim() != string.Empty && studioNode.GetProperty("openingHours" + i).Value.Trim() != string.Empty)
                {
                    output += "<p>" + studioNode.GetProperty("openingHoursDay" + i).Value + "&nbsp; : &nbsp;" + studioNode.GetProperty("openingHours" + i).Value + "</p>";
                }
            }

            output += "</div>" +
                      "<div class='col-md-4'>" +
                      "<div class='imgBox249px264px'>" +
                      "<div id='map" + mapId + "' style='width: 249px; height: 264px'></div>" +
                      "</div>" +
                      "</div>" +
                      "</div>";

            return output.ToString();
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            /*
            lblErrorMessage.Text = "";
            int Postcode;
            int Kilometres = 50;
            DataTable dtStudios;
            if (tbPostcode.Text.Length > 0 && tbPostcode.Text != "Enter Postcode")
            {
                int.TryParse(tbPostcode.Text, out Postcode);
                
                if (Postcode > 0)
                {
                    //get studios near
                    dtStudios = GetStudiosNearPostcode(Postcode.ToString(), Kilometres, 0);
                    //check to see if proc has returned actual results and not a 0 count
                    if (dtStudios.Columns.Count <= 1 || dtStudios.Rows.Count == 0)
                    {
                        //lblErrorMessage.Text = "No studios found within " + ddlKilometres.SelectedValue + "kms near postcode";
                    }
                    else
                       // lblStudioList.Text = "This is from btnFind_Click";
                        lblStudioList.Text += LoadStudios(dtStudios);
                }
                else
                {
                    lblErrorMessage.Text = "Please enter a postcode";
                    return;
                }
            }
             */
            Response.Redirect("/studio-finder/?pc=" + tbPostcode.Text);
        }

        protected void btnFindNZ_Click(object sender, EventArgs e)
        {
            Response.Redirect("/studio-finder/?pc=" + tbPostcode.Text + "&country=NZ");
        }

    }
}