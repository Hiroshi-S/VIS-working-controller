<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VisionTV.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.clubvision.VisionTV" %>
<div id="contentContainer">

<div id="eVTV" class="element"><%--<h3 class="replace">Vision TV</h3>--%>
<h1>Vision TV</h1>
<div class="clear"></div>
<div class="eContent eRed">
<%-- start of orange panel --%>

    <table id="tblPlayerAndSearch">
        <tr>
            <td>
                    <!-- Start of Brightcove Player -->
                    <script language="JavaScript" type="text/javascript" src="http://admin.brightcove.com/js/BrightcoveExperiences.js"></script>
                    <script language="JavaScript" type="text/javascript" src="http://admin.brightcove.com/js/APIModules_all.js"></script>
                <div id="player">

                    <object id="myExperienceSSS" class="BrightcoveExperience">
                      <param name="bgcolor" value="#FFFFFF" />
                      <param name="width" value="480" />
                      <param name="height" value="270" />
                      <param name="playerID" value="1081504048001" />
                      <param name="playerKey" value="AQ~~,AAAA6xwg44E~,ysHMNUKUQO51nUTSl4HmEFQjMsQI8Sli" />
                      <param name="isVid" value="true" />
                      <param name="isUI" value="true" />
                      <param name="dynamicStreaming" value="true" />
                        <param name="wmode" value="transparent" />
                      <param name="@videoPlayer" value="<%=BCVideoID %>" />
                     </object>
                    </div>
                     <!-- End of Brightcove Player -->
            </td>
            <td>
                <h4 class="videoTitle"><asp:Label runat="server" ID="lbFeaturedVideoTitle" /></h4>
                <asp:Label runat="server" id="lbFeaturedVideoDesc" />
                <br />
                <br />
                <a runat="server" id="lnkRelated"></a>
                <br />
                <br />
                <a runat="server" id="lnkFoodDiary"></a>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblSearch" Text="Search by keyword" AssociatedControlID="tbSearch" CssClass="searchlabel" runat="server" />&nbsp;
                <div id="VisionTvSearchArea">
                    <asp:ImageButton ID="imgbtnSearch" runat="server" ImageUrl="~/images/btnsearch.png" Width="22" Height="22" AlternateText="Search" onclick="imgbtnSearch_Click" CssClass="btnSearchWithinTextInput" />
                    <asp:TextBox ID="tbSearch" runat="server" />
                </div>
            </td>
            <td>&nbsp;
            </td>
        </tr>
            
    </table>

    <div>
        <asp:UpdatePanel ID="upScroller" runat="server">
        
            <ContentTemplate>
                <div id="tabs" style="padding: 0px;">
                    <!--
                    <asp:Button ID="Button1" CssClass="tabOrange139" Text="Most Recent" runat="server" OnClick="btnRecent_Click" />
                    <asp:Button ID="Button2" CssClass="tabOrange139" Text="Recipes" runat="server" OnClick="btnRecipes_Click" />
                    <asp:Button ID="Button3" CssClass="tabOrange139" Text="Education" runat="server" OnClick="btnEducation_Click" />
                    <asp:Button ID="Button4" CssClass="tabOrange139" Text="Exercises" runat="server" OnClick="btnExercises_Click" />
                    <asp:Button ID="Button5" CssClass="tabOrange139" Text="Success Stories" runat="server" OnClick="btnSuccessStories_Click" />
                    <asp:Button ID="Button6" CssClass="tabOrange139" Text="Lifestyle" runat="server" OnClick="btnLifestyle_Click" /> 
                    <asp:Button ID="Button7" CssClass="tabOrange139" Text="Tutorials" runat="server" OnClick="btnTutorials_Click" /> 
                    -->

                    <asp:Button ID="btnRecent" CssClass="tabRed139" Text="Most Recent" runat="server" OnClick="btnRecent_Click" />
                    <asp:Button ID="btnLifestyle" CssClass="tabRed139" Text="Emotions" runat="server" OnClick="btnLifestyle_Click" /> 
                    <asp:Button ID="btnEducation" CssClass="tabRed139" Text="Education" runat="server" OnClick="btnEducation_Click" />
                    <asp:Button ID="btnRecipes" CssClass="tabRed139" Text="Eating" runat="server" OnClick="btnRecipes_Click" />
                    <asp:Button ID="btnExercises" CssClass="tabRed139" Text="Exercises" runat="server" OnClick="btnExercises_Click" />
                    <asp:Button ID="btnSuccessStories" CssClass="tabRed139" Text="Success Stories" runat="server" OnClick="btnSuccessStories_Click" />
                    <asp:Button ID="btnTutorials" CssClass="tabRed139" Text="Tutorials" runat="server" OnClick="btnTutorials_Click" /> 
                </div>
    
                <div id="eVisionTv906px"><!-- 906 -->
                    <script type="text/javascript">
                        //image slideshow controls

                        var attachScrollerEvents = (function () {
                            //reset the viewer's inner html - otherwise the number of spans will reduce by 1.
                            $(".viewer").html($(".viewer").html());

                            $(".viewer").imageScroller({
                                next: "prevSuccess906px",
                                prev: "nextSuccess906px",
                                frame: "ContentPlaceHolderDefault_ContentPlaceHolderMain_VisionTV_2_viewerVisionTV",
                                width: 904,
                                child: "span",
                                auto: false
                            });

                        });
                    </script>
                    <div class="viewer">
                        <div runat="server" id="viewerVisionTV" class="vFrame">
                        </div>
                    </div>
                    <a href="#" class="replace slidePrev" id="prevSuccess906px" onclick="return false;">Prev</a>
                    <a href="#" class="replace slideNext" id="nextSuccess906px" onclick="return false;">Next</a>
                    <div class="clear"> </div>
                </div><!-- #eVisionTv906px -->
            </ContentTemplate>
        </asp:UpdatePanel>
    </div><%-- end div.eContent.eOrange --%>


<%-- end of orange panel --%>
<div class="clear"> </div></div>
<div class="clear"> </div>
<%-- end of orange panel --%>

</div><%-- end eVTV --%>
</div><%-- end contentContainer --%>