<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyJourneyProgressPhoto.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.VVT.MyJourneyProgressPhoto" %>
<!--Example assets -->
    <link rel="stylesheet" type="text/css" href="/scripts/sorgalla-mytransformationphoto/jcarousel.transitions.css"> 

    <script type="text/javascript" src="/scripts/sorgalla-mytransformationphoto/modernizr.js"></script> 
    <script type="text/javascript" src="/scripts/sorgalla-mytransformationphoto/jquery.js"></script>
    <script type="text/javascript" src="/scripts/sorgalla-mytransformationphoto/jquery.jcarousel.min.js"></script>

    <script type="text/javascript" src="/scripts/sorgalla-mytransformationphoto/jcarousel.transitions.js"></script>

    <style>
        #ContentPlaceHolderDefault_help{
            display: none;
        }
    </style>

<div id="eProfileTab" class="element" style="overflow: visible;width: 907px;"><!-- 605 -->
        <div class="replace" id="ContentPlaceHolderDefault_ContentPlaceHolderMain_MyJouneyTab_2_profileTab" style="background-image: none;width: 907px;">         

            <div onclick="window.open('/club-vision/my-journey/','_self');return false;">My Progress</div>
            <div class="divHeadingActive" style="color: #c60c30;">My Transformation</div>
            <div id="ContentPlaceHolderDefault_ContentPlaceHolderMain_MyJouneyTab_2_tabMeasurements" onclick="window.open('/club-vision/my-journey/?tab=mymeasurements','_self');return false;">My Measurements</div>             
            <div id="ContentPlaceHolderDefault_ContentPlaceHolderMain_MyJouneyTab_2_tabMyGoals" onclick="window.open('/club-vision/my-journey/?tab=mygoals','_self');return false;">My Goals</div>
          
            <div style="width: 103px;" class="lastDivHeading"></div>  
        </div>
        <div class="clear"></div>
        <div class="eContent eClean" id="eMyProgress" style="height: auto; overflow:hidden;width: 907px;border-bottom: none;border-left: none;border-right: none;padding-top: 40px;">

            <div class="wrapper">
            <h1>My Transformation Photos</h1>

            <div class="jcarousel-wrapper">
                <div class="jcarousel" data-jcarousel="true">
                   <asp:Literal ID="LiteralTranformationPhoto" runat="server"></asp:Literal>
                   </div>
                
                <a id="prevslide" href="#" class="jcarousel-control-prev" data-jcarouselcontrol="true">‹</a>
                <a id="nextslide" href="#" class="jcarousel-control-next" data-jcarouselcontrol="true">›</a>
            </div>

            <!-- <h2 style="color: #999999;">Photo submitted on <span id="dateTaken" style="color: #000000;">dd/MM/yyyy</span>.</h2>
            
            <input id="ButtonDeletePhoto" type="button" value="Delete This Photo" class="button-small grey_dark_reverse rounded3"/> -->
            
            <br/><br/><br/><br/>
            <h1>Upload Your Awesome Photo</h1>
            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="button-small vision_red_reverse rounded3" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="FileUpload1" ValidationGroup="uploadphoto" ForeColor="red"
                runat="server" ErrorMessage="RequiredFieldValidator" Text="**Required**" SetFocusOnError="True"></asp:RequiredFieldValidator>
            
            <asp:Button CssClass="button-small vision_red rounded3" ID="ButtonUpload" runat="server" ValidationGroup="uploadphoto"
                Text="Upload Photo" onclick="ButtonUpload_Click" />

      </div>
      

     </div>
</div>

<% if ((string)Session["MemberType"] == "VPT"){ %>
    <script>
        $(document).ready(function () {

            myTransformationLoadingScriptsVPT();
            $(".cErrorPopupClose").unbind().click(function () {
                $("#cErrorPopup").fadeOut();return false;
            });
        });
    </script>
    <%}%>
    <script>
        $(document).ready(function () {
            
            myTransformationLoadingScripts();
            $(".cErrorPopupClose").unbind().click(function () {
                $("#cErrorPopup").fadeOut();return false;
            });
            
        });
    </script>
    

<!-- /eProfileTab -->