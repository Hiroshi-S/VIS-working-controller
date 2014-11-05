<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyDetailsProfileTab.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.externalclubvision.MyDetailsProfileTab" %>
<%@ Register TagPrefix="uc" TagName="myDetails" Src="initialscreens/MyDetails.ascx" %>
<%@ Register TagPrefix="uc" TagName="LifeStyleScreen" Src="initialscreens/LifeStyleScreen.ascx" %>
<%@ Register TagPrefix="uc" TagName="BodyType" Src="initialscreens/BodyType.ascx" %>


<div id="eProfileTab" class="element" style="overflow: visible"><!-- 605 -->
        <div class="replace" id="profileTab" runat="server" style="background-image: url(/images/ExtClubVision/eHdrProfileTabMyDetails.gif); ">
            <div id="tabBodyType" style="cursor: pointer; position: absolute; top: 0px; left: 404px; width: 201px; height: 45px" onclick="document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyDetailsProfileTab_4_profileTab').style.backgroundImage = 'url(/images/ExtClubVision/eHdrProfileTabBodyType.gif)';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyDetailsProfileTab_4_eMyDetails').style.display = 'none';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyDetailsProfileTab_4_eLifestyleScreen').style.display = 'none';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyDetailsProfileTab_4_eBodyType').style.display = 'block';"></div>
            <div id="tabLifestyleScreen" style="cursor: pointer; position: absolute; top: 0px; left: 202px; width: 201px; height: 45px" onclick="document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyDetailsProfileTab_4_profileTab').style.backgroundImage = 'url(/images/ExtClubVision/eHdrProfileTabLifestyleScreen.gif)';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyDetailsProfileTab_4_eMyDetails').style.display = 'none';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyDetailsProfileTab_4_eLifestyleScreen').style.display = 'block';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyDetailsProfileTab_4_eBodyType').style.display = 'none';"></div>
            <div id="tabMyDetails" style="cursor: pointer; position: absolute; top: 0px; left: 0px; width: 201px; height: 45px" onclick="document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyDetailsProfileTab_4_profileTab').style.backgroundImage = 'url(/images/ExtClubVision/eHdrProfileTabMyDetails.gif)';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyDetailsProfileTab_4_eMyDetails').style.display = 'block';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyDetailsProfileTab_4_eLifestyleScreen').style.display = 'none';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyDetailsProfileTab_4_eBodyType').style.display = 'none';"></div>
        </div>
        <div class="clear"></div>

        <div class="eContent eOrange" id="eMyDetails" style="height: auto;" runat="server">
           <uc:myDetails ID="myDetail" runat="server" />
        </div>
        
        <div class="eContent eOrange" id="eLifestyleScreen" style="display: none;height: auto;" runat="server">
            <div id="tabLifestyleScreen1" style="display: block;" runat="server">
                <h3>Step 2</h3>
                <p>This section will be available after you have completed <a onclick="document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyDetailsProfileTab_4_profileTab').style.backgroundImage = 'url(/images/ExtClubVision/eHdrProfileTabMyDetails.gif)';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyDetailsProfileTab_4_eMyDetails').style.display = 'block';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyDetailsProfileTab_4_eLifestyleScreen').style.display = 'none';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyDetailsProfileTab_4_eBodyType').style.display = 'none';">My Details</a></p>
            </div>
            <div id="tabLifestyleScreen2" style="display: none;" runat="server">
                <uc:LifeStyleScreen ID="lifeStyleScreen" runat="server" />
            </div>
        </div>  

        <div class="eContent eOrange" id="eBodyType" style="display: none;height: auto;"  runat="server">
            <div id="tabBodyType1" runat="server" style="display: block;" >
                <h3>Step 3</h3>
                <p>This section will be available after you have completed <a onclick="document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyDetailsProfileTab_4_profileTab').style.backgroundImage = 'url(/images/ExtClubVision/eHdrProfileTabLifestyleScreen.gif)';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyDetailsProfileTab_4_eMyDetails').style.display = 'none';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyDetailsProfileTab_4_eLifestyleScreen').style.display = 'block';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyDetailsProfileTab_4_eBodyType').style.display = 'none';">Lifestyle Screen</a></p>
            </div>
            <div id="tabBodyType2" runat="server" style="display: none;" >
                <uc:BodyType ID="bodyType" runat="server"/>
            </div>
        </div>
</div><!-- /eProfileTab -->