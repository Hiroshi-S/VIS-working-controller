<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyEatingPlanTab.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.externalclubvision.MyEatingPlanTab" %>
<%@ Register TagPrefix="uc" TagName="eating" Src="~/usercontrols/externalclubvision/initialscreens/EatingPlanner.ascx" %>

<style>
    #EatingPlannerButtonsDiv {
        display: none;
    }
    
</style>

<div id="eProfileTab" class="element" style="overflow: visible"><!-- 605 -->
        <div class="replace" id="profileTab" runat="server" style="background-image: url(/images/ExtClubVision/eHdrMyEatingPlanner.gif); ">
            <div id="tabBodyType" style="cursor: pointer; position: absolute; top: 0px; left: 404px; width: 201px; height: 45px" ></div>
        </div>
        <div class="clear"></div>

        <div class="eContent eOrange" id="eMyDetails" style="height: auto;" runat="server">
            
           <div id="tabeating1" style="display: block;" runat="server">
                <h3>Step 7</h3>
                <p>This section will be available after you have completed <a onclick="window.open('/club-vision/my-profile/edit-measurements/?tab=trainingplan','_self')">Training Plan</a></p>
            </div>
            <div id="tabeating2" style="display: none;" runat="server">
                <uc:eating ID="eat" runat="server"/>
            </div>
        </div>
        
        
</div><!-- /eProfileTab -->