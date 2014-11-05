using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data;
using System.Data.SqlClient;
using SpinningCube.DAL;
using System.Configuration;
using uComponents.Core;
using umbraco.presentation.nodeFactory;
using umbraco.BusinessLogic;
using umbraco.cms.businesslogic.web;
using umbraco.cms.businesslogic.media;
using System.Text;

namespace VisionPersonalTrainingProject.usercontrols.studiopages
{
    public partial class StudioMap : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Node currentNode = umbraco.presentation.nodeFactory.Node.GetCurrent();
            string studioId = currentNode.GetProperty("studioId").Value.Trim();
            MapStudio(Convert.ToInt32(studioId));
        }


        public void MapStudio(int StudioID)
        {
            Type cstype = this.GetType();
            SqlParameter[] spList = new SqlParameter[1];
            spList[0] = new SqlParameter("studioid", StudioID);
            DataTable dtStudio = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["con"].ConnectionString, CommandType.StoredProcedure, "USP_StudioByID", spList).Tables[0];
            //plot maps
            if (dtStudio.Rows.Count > 0)
                this.Page.ClientScript.RegisterStartupScript(cstype, "mapplot", "showAddressNew('map1','" + dtStudio.Rows[0]["LL"].ToString() + "');", true);

        }

    }
}