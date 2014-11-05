<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyMeasurementsTabs.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.externalclubvision.MyMeasurementsTabs" %>
<%@ Register TagPrefix="uc" TagName="measure" Src="initialscreens/Measurements.ascx" %>
<%@ Register TagPrefix="uc" TagName="myGoals" Src="initialscreens/MyGoals.ascx" %>
<%@ Register TagPrefix="uc" TagName="trainingPlan" Src="initialscreens/NewTrainingPlan.ascx" %>


<div id="eProfileTab" class="element" style="overflow: visible"><!-- 605 -->
        <div class="replace" id="profileTab" runat="server" style="background-image: url(/images/ExtClubVision/eHdrProfileTabMeasurements.gif); ">
            
            <div id="tabMeasure" style="cursor: pointer; position: absolute; top: 0px; left: 0px; width: 201px; height: 45px" onclick="document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_profileTab').style.backgroundImage = 'url(/images/ExtClubVision/eHdrProfileTabMeasurements.gif)';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_eMeasurementsTabMeasure').style.display = 'block';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_eMeasurementsTabGoals').style.display = 'none';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_eMeasurementsTabTrain').style.display = 'none';"></div>
            <div id="tabGoal" style="cursor: pointer; position: absolute; top: 0px; left: 202px; width: 201px; height: 45px" onclick="document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_profileTab').style.backgroundImage = 'url(/images/ExtClubVision/eHdrProfileTabMyGoals.gif)';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_eMeasurementsTabMeasure').style.display = 'none';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_eMeasurementsTabGoals').style.display = 'block';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_eMeasurementsTabTrain').style.display = 'none';"></div>
            <div id="tabTrain" style="cursor: pointer; position: absolute; top: 0px; left: 404px; width: 201px; height: 45px" onclick="document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_profileTab').style.backgroundImage = 'url(/images/ExtClubVision/eHdrProfileTabTrainingPlan.gif)';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_eMeasurementsTabMeasure').style.display = 'none';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_eMeasurementsTabGoals').style.display = 'none';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_eMeasurementsTabTrain').style.display = 'block';"></div>
            <!--
            <div id="tabMeasure" style="cursor: pointer; position: absolute; top: 0px; left: 0px; width: 201px; height: 45px" onclick="window.open('/ext-club-vision/account/my-profile/edit-measurements/','_self')"></div>
            <div id="tabGoal" style="cursor: pointer; position: absolute; top: 0px; left: 202px; width: 201px; height: 45px" onclick="window.open('/ext-club-vision/account/my-profile/edit-measurements/?tab=goals','_self')"></div>
            <div id="tabTrain" style="cursor: pointer; position: absolute; top: 0px; left: 404px; width: 201px; height: 45px" onclick="window.open('/ext-club-vision/account/my-profile/edit-measurements/?tab=trainingplan','_self')"></div>
            -->
        </div>

        <div class="clear"></div>

        <div class="eContent eOrange" id="eMeasurementsTabMeasure" style="height: auto;" runat="server">
            <div id="tabMeasure1" style="display: block;" runat="server">
                <h3>Step 4</h3>
                <p>This section will be available after you have completed <a onclick="window.open('/ext-club-vision/account/my-profile/edit-profile/?tab=bodytype','_self')">Body Type</a></p>
            </div>
            <div id="tabMeasure2" style="display: none;" runat="server">
                <uc:measure ID="mm2" runat="server"/>
            </div>
        </div>
        
        <div class="eContent eOrange" id="eMeasurementsTabGoals" runat="server" style="display: none;height: auto;" runat="server">
            <div id="tabGoal1" style="display: block;" runat="server">
                <h3>Step 5</h3>
                <p>This section will be available after you have completed <a onclick="document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_profileTab').style.backgroundImage = 'url(/images/ExtClubVision/eHdrProfileTabMeasurements.gif)';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_eMeasurementsTabMeasure').style.display = 'block';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_eMeasurementsTabGoals').style.display = 'none';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_eMeasurementsTabTrain').style.display = 'none';">Measurements</a></p>
            </div>
            <div id="tabGoal2" style="display: none;" runat="server">
                <uc:myGoals ID="mg" runat="server"/>
            </div>
         </div>  

        <div class="eContent eOrange" id="eMeasurementsTabTrain" style="display: none;height: auto;"  runat="server">
            <div id="tabTrainingPlan1" style="display: block" runat="server">
                <h3>Step 6</h3>
                <p>This section will be available after you have completed <a onclick="document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_profileTab').style.backgroundImage = 'url(/images/ExtClubVision/eHdrProfileTabMyGoals.gif)';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_eMeasurementsTabMeasure').style.display = 'none';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_eMeasurementsTabGoals').style.display = 'block';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_eMeasurementsTabTrain').style.display = 'none';">My Goals</a></p>
            </div>
            <div id="tabTrainingPlan2"  style="display: none;" runat="server">
                <uc:trainingPlan ID="tp" runat="server"/>
            </div>
        </div>
</div><!-- /eProfileTab -->

<script type="text/javascript">
    $("#ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_mm2_myMeasurementsContentDiv").css("margin-top", "0px");
</script>