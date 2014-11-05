using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VisionPersonalTrainingProject.usercontrols.lightboxes
{
    public partial class NewGoalMeasurementsLightBox : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //cleanUserControlsinMeasurements();
            //cleanUserControlsinGoals();
            if(!IsPostBack)
            {
                FileUpload fileUpload = ms.FindControl("fileUpload") as FileUpload;
                if(fileUpload != null)
                {
                    Session["FileUpload"] = fileUpload;
                }
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            string validatorOverrideScripts = "<script src=\"/scripts/validators.js\" type=\"text/javascript\"></script>";
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ValidatorOverrideScripts", validatorOverrideScripts, false);
            base.Render(writer);
        }

        protected void cleanUserControlsinMeasurements()
        {
            // Find control on page.
            Control header1 = ms.FindControl("measHeaderButton");
            header1.Visible = false;
            
            Control progPhoto = ms.FindControl("rowShowProgressPhoto");
            progPhoto.Visible = false;
            
            DropDownList ddlc = ms.FindControl("isMetricDdl") as DropDownList;
            ddlc.Enabled = true;
            ddlc.SelectedIndex = 0;

            List<TextBox> ltbx = new List<TextBox>();
            TextBox txtWaist = ms.FindControl("txtWaist") as TextBox;
            ltbx.Add(txtWaist);
            
            TextBox txtChest = ms.FindControl("txtChest") as TextBox;
            ltbx.Add(txtChest);

            TextBox txtHips = ms.FindControl("txtHips") as TextBox;
            ltbx.Add(txtHips);

            TextBox txtFatRate = ms.FindControl("txtFatRate") as TextBox;
            ltbx.Add(txtFatRate);

            TextBox txtForeArm = ms.FindControl("txtForeArm") as TextBox;
            ltbx.Add(txtForeArm);

            TextBox txtWrist = ms.FindControl("txtWrist") as TextBox;
            ltbx.Add(txtWrist);

            TextBox txtBodyWeight = ms.FindControl("txtBodyWeight") as TextBox;
            ltbx.Add(txtBodyWeight);

            TextBox txtNutritionCalc = ms.FindControl("txtNutritionCalc") as TextBox;
            ltbx.Add(txtNutritionCalc);

            TextBox txtBPSystolic = ms.FindControl("txtBPSystolic") as TextBox;
            ltbx.Add(txtBPSystolic);

            TextBox txtBPDiastolyc = ms.FindControl("txtBPDiastolyc") as TextBox;
            ltbx.Add(txtBPDiastolyc);

            TextBox txtDesiredItemClothing = ms.FindControl("txtDesiredItemClothing") as TextBox;
            ltbx.Add(txtDesiredItemClothing);

            ImageButton buttonback = ms.FindControl("MeasureImagebuttonBack") as ImageButton;
            buttonback.Visible = false;

            ImageButton buttonNext = ms.FindControl("MeasureImagebuttonNext") as ImageButton;
            buttonNext.Visible = false;

            //ImageButton buttonNextUC = ms.FindControl("MeasureImagebuttonNextUC") as ImageButton;
            //buttonNextUC.Visible = true;

            Literal ltphotoImg = ms.FindControl("measurementsPhotoLiteral") as Literal;
            ltphotoImg.Text = "";

            foreach (TextBox textBox in ltbx)
            {
                textBox.Text = "";
            }

            
        }

        protected void cleanUserControlsinGoals()
        {
            
            Control goalHeaderButton = gl.FindControl("goalHeaderButton");
            goalHeaderButton.Visible = false;
            
            ImageButton buttonback = gl.FindControl("MyGoalImagebuttonBack") as ImageButton;
            buttonback.Visible = false;

            ImageButton buttonNext = gl.FindControl("MyGoalImagebuttonNext") as ImageButton;
            buttonNext.Visible = false;

            ImageButton buttonNextUC = gl.FindControl("MyGoalImagebuttonNextUC") as ImageButton;
            buttonNextUC.Visible = true;

            Label txtIsMetric = gl.FindControl("txtIsMetric") as Label;
            txtIsMetric.Text = "";

            TextBox txtCurrentBW = gl.FindControl("txtCurrentBW") as TextBox;
            txtCurrentBW.Text = "";

            DropDownList weightGoalDropDownListImperial = gl.FindControl("weightGoalDropDownListImperial") as DropDownList;
            weightGoalDropDownListImperial.Enabled = true;
            weightGoalDropDownListImperial.SelectedIndex = 0;

            DropDownList weightGoalDropDownListMetric = gl.FindControl("weightGoalDropDownListMetric") as DropDownList;
            weightGoalDropDownListMetric.Enabled = true;
            weightGoalDropDownListMetric.SelectedIndex = 0;





        }
    }
}