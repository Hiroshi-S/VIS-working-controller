<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewGoalMeasurementsLightBox.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.lightboxes.NewGoalMeasurementsLightBox" %>
<%@ Register TagPrefix="uc" TagName="trainingPlan" Src="~/usercontrols/VVT/VVT Initial Steps/ExercisePlanner.ascx" %>
<%@ Register TagPrefix="uc" TagName="measurements" Src="~/usercontrols/VVT/VVT Initial Steps/Measurements.ascx" %>
<%@ Register TagPrefix="uc" TagName="goals" Src="~/usercontrols/VVT/VVT Initial Steps/MyGoals.ascx" %>
<%@ Register TagPrefix="uc" TagName="eating" Src="~/usercontrols/VVT/VVT Initial Steps/EatingPlanner.ascx" %>


<style>
    .visuallyhidden {
    display: none;
    }

    .wizard-progress {
        list-style: none;
        list-style-image: none;
        margin: 0;
        padding: 0;
        margin-top: -35px;
        float: left;
        white-space: nowrap;
    }

    .wizard-progress li {
        float: left;
        margin-right: 50px;
        text-align: center;
        position: relative;
        width: 94px;
    }

    .wizard-progress .step-name {
        display: table-cell;
        height: 32px;
        vertical-align: bottom;
        text-align: center;
        width: 80px;
    }

    .wizard-progress .step-name-wrapper {
        display: table-cell;
        height: 100%;
        vertical-align: bottom;
    }

    .wizard-progress .step-num {
        font-size: 14px;
        font-weight: bold;
        border: 3px solid #000;
        border-radius: 50%;
        width: 18px;
        display: inline-block;
        margin-top: 10px;
    }

    .wizard-progress .step-num:after {
        content: "";
        display: block;
        background: #000;
        height: 5px;
        width: 119px;
        position: absolute;
        bottom: 10px;
        left: 60px;
    }

    .wizard-progress li:last-of-type .step-num:after {
        display: none;
    }

    .wizard-progress .active-step .step-num {
        background-color: #E27423;
    }
    
    #glPanel {
        display: none;
    }
    
    #tpPanel {
        display: none;
    }
    
    #eatingPanel {
        display: none;
    }
</style>

<div class="contactOverlay" id="cNewMeasGoalPopup" style="height: 3654px !important;">				
  <div class="contactBox" id="NewMeasGoalContactBox">
  	
  	<div class="cContent" id="NewMeasGoalcContent" style="width: 593px;margin-left:-30px;padding: 10px;">
  	    <br/>
    	 <a href="" target="_blank" class="replace cCross cNewMeasGoalPopupClose" title="Close">Close</a>
        <div style="margin-bottom: 60px;width: 583px;display: block;">
    	<ol id="resetPlanSteps" class="wizard-progress clearfix">
            <li class="active-step">
                <span class="step-name">
                    <h3>Measurements</h3>
                </span>
                <span class="visuallyhidden">Step </span><span class="step-num">1</span>
            </li>
            <li >
                <span class="step-name"><h3>Goals</h3></span>
                <span class="visuallyhidden">Step </span><span class="step-num">2</span>
            </li>
            <li>
                <span class="step-name"><h3>Training Planner</h3></span>
                <span class="visuallyhidden">Step </span><span class="step-num">3</span>
            </li>
            <li>
                <span class="step-name"><h3>Eating Planner</h3></span>
                <span class="visuallyhidden">Step </span><span class="step-num">4</span>
            </li>
        </ol>
        </div>
        <br/> <br/>
        
        <div style="max-height: 700px; overflow-y: scroll; display: block; margin-left: -1px;padding-left: 10px;overflow-x: hidden;">
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
              <ContentTemplate>
                  <!-- <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder> -->
                  <asp:Panel ID="msPanel" runat="server" ClientIDMode="Static">
                      <uc:measurements runat="server" ID="ms"/>
                  </asp:Panel>
                  <asp:Panel ID="glPanel" runat="server" ClientIDMode="Static">
                      <uc:goals ID="gl" runat="server"/>
                  </asp:Panel>
                  <asp:Panel ID="tpPanel" runat="server" ClientIDMode="Static">
                      <uc:trainingPlan ID="tp" runat="server"/>
                  </asp:Panel>
                  <asp:Panel ID="eatingPanel" runat="server" ClientIDMode="Static">
                      <uc:eating ID="eat" runat="server"/>
                  </asp:Panel>
                
              </ContentTemplate>
          </asp:UpdatePanel>
        </div>

  	</div><!-- /cContent -->
  	
  </div><!-- /contactBox  -->
</div><!-- /contactOverlay cEnquire -->