using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VisionPersonalTrainingProject.usercontrols.externalclubvision
{
    public partial class MyMeasurementsTabs : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;
            try
            {
                string tab = Request.QueryString["tab"];
                if (tab != null)
                {
                    switch (tab)
                    {
                        case "goals":
                            DisplayTabGoals();
                            break;

                        case "trainingplan":
                            DisplayTabTraining();
                            break;

                        default:
                            break;
                    }
                }

                var cvdc = new ClubVisionDataContext();

                var pbodytype = (from pp in cvdc.FormResults
                                 where pp.iCustomerID == Convert.ToInt32(Session["MemberNo"])
                                 where pp.iFormID == 2
                                 select pp).FirstOrDefault();
                if (pbodytype != null)
                {
                    tabMeasure2.Style["display"] = "block";
                    tabMeasure1.Style["display"] = "none";
                }

                var pmeasure = (from lfs in cvdc.Measurements
                                where lfs.iCustomerID == Convert.ToInt32(Session["MemberNo"])
                                select lfs).FirstOrDefault();
                if (pmeasure != null)
                {
                    tabGoal2.Style["display"] = "block";
                    tabGoal1.Style["display"] = "none";
                }

                var pgoal = (from lfs in cvdc.Goals
                             where lfs.iCustomerID == Convert.ToInt32(Session["MemberNo"])
                             select lfs).FirstOrDefault();

                if (pgoal != null)
                {
                    tabTrainingPlan2.Style["display"] = "block";
                    tabTrainingPlan1.Style["display"] = "none";
                }

                cvdc.Dispose();

            }
            catch (Exception exception)
            {
                Response.Write(exception.ToString());
            }

        }

        public void DisplayTabGoals()
        {
            const string s = "<script type=\"text/javascript\">" +
                             "document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_profileTab').style.backgroundImage = 'url(/images/ExtClubVision/eHdrProfileTabMyGoals.gif)';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_eMeasurementsTabMeasure').style.display = 'none';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_eMeasurementsTabGoals').style.display = 'block';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_eMeasurementsTabTrain').style.display = 'none';</script>";

            Page.ClientScript.RegisterStartupScript(GetType(), "test", s);

        }

        public void DisplayTabTraining()
        {
            const string s = "<script type=\"text/javascript\">" +
                             "document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_profileTab').style.backgroundImage = 'url(/images/ExtClubVision/eHdrProfileTabTrainingPlan.gif)';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_eMeasurementsTabMeasure').style.display = 'none';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_eMeasurementsTabGoals').style.display = 'none';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_eMeasurementsTabTrain').style.display = 'block';</script>";

            Page.ClientScript.RegisterStartupScript(GetType(), "test", s);
        }
    }
}