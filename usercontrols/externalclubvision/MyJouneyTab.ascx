<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyJouneyTab.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.externalclubvision.MyJouneyTab" %>
<%@ Register TagPrefix="uc" TagName="Measurements" src="~/usercontrols/VVT/VVT Initial Steps/Measurements.ascx" %>
<%@ Register TagPrefix="uc" TagName="Goals" src="~/usercontrols/VVT/VVT Initial Steps/MyGoals.ascx" %>
<%@ Register TagPrefix="uc" TagName="Progress" src="~/usercontrols/clubvision/ProgressAndGoal.ascx" %>
<%@ Register TagPrefix="uc" TagName="Chart" src="~/usercontrols/clubvision/ProgressGraph.ascx" %>
<%@ Register TagPrefix="uc" TagName="DailyInspiration" Src="~/usercontrols/general/DailyInspiration.ascx" %>
<style type="text/css">
    .textbox
    {
        width:30px;
    }
    .bold
    {
        font-weight:bold;
    }
</style>

<script type="text/javascript">

    $(document).ready(function () {
        var sessionValue = '<%= Session["MemberType"] %>';
       // setEqualHeight($('#<%=eMyProgress.ClientID%>'), $('#RightPanel'));
        
        alert(sessionValue);
        var tab = getUrlVars()["tab"];

        switch (tab) {
            case 'myprogress':
                myJourneyTab_reset(); myJourneyTab_myProgress();
                callbreadcrumb('tabMyProgress'); break;
            case 'mymeasurements':
                myJourneyTab_reset(); myJourneyTab_myMeasurements();
                callbreadcrumb('tabMeasurements'); break;
            case 'mygoals':
                myJourneyTab_reset(); myJourneyTab_myGoals();
                callbreadcrumb('tabMyGoals'); break;
            default:
                myJourneyTab_reset(); myJourneyTab_myProgress();
                callbreadcrumb('tabMyProgress'); 
                break;
        }
        var popup = getUrlVars()["resetpop"];
        if (popup == "true") {
            popupTrainingPlanner();
        } else {
          //  alert("hahaha");
        }

    });
</script>

<div id="eProfileTab" class="element" style="overflow: visible;width: 907px;"><!-- 605 -->
        <div class="replace" id="profileTab" runat="server" style="background-image: none;width: 907px;">         
           <!-- <div id="tabMyProgress1" runat="server" style="cursor: pointer; position: absolute; top: 0px; left: 0px; width: 201px; height: 45px"
                 onclick="document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_MyJouneyTab_2_profileTab').style.backgroundImage = 'url(/images/eHdrMyJourney-MyProgress.gif)';
                          document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_MyJouneyTab_2_eMyProgress').style.display = 'block';
                          document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_MyJouneyTab_2_eMeasurements').style.display = 'none';
                          document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_MyJouneyTab_2_eMyGoals').style.display = 'none';
                          document.getElementById('ProgressAndGoal').style.display = 'none';
                          callbreadcrumb('tabMyProgress');"></div>
            <div id="tabMeasurements1" runat="server" style="cursor: pointer; position: absolute; top: 0px; left: 202px; width: 201px; height: 45px"
                 onclick="document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_MyJouneyTab_2_profileTab').style.backgroundImage = 'url(/images/eHdrMyJourney-MyMeasurements.gif)';
                          document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_MyJouneyTab_2_eMyProgress').style.display = 'none';
                          document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_MyJouneyTab_2_eMeasurements').style.display = 'block';
                          document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_MyJouneyTab_2_eMyGoals').style.display = 'none';
                          document.getElementById('ProgressAndGoal').style.display = 'block';
                          callbreadcrumb('tabMeasurements');"></div>
            <div id="tabMyGoals1" runat="server" style="cursor: pointer; position: absolute; top: 0px; left: 404px; width: 201px; height: 45px"
                 onclick="document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_MyJouneyTab_2_profileTab').style.backgroundImage = 'url(/images/eHdrMyJourney-MyGoals.gif)';
                          document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_MyJouneyTab_2_eMyProgress').style.display = 'none';
                          document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_MyJouneyTab_2_eMeasurements').style.display = 'none';
                          document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_MyJouneyTab_2_eMyGoals').style.display = 'block';
                          document.getElementById('ProgressAndGoal').style.display = 'block';
                          callbreadcrumb('tabMyGoals');"></div> -->
            <div id="tabMyProgress" runat="server" onclick="window.open('/club-vision/my-journey/','_self');return false;">My Progress</div>
            <div id="tabMyProgressPhoto" onclick="window.open('/club-vision/my-journey/my-transformation/','_self');return false;">My Transformation</div>
            <div id="tabMeasurements" runat="server" onclick="window.open('/club-vision/my-journey/?tab=mymeasurements','_self');return false;">My Measurements</div>             
            <div id="tabMyGoals" runat="server" onclick="window.open('/club-vision/my-journey/?tab=mygoals','_self');return false;">My Goals</div>
            
            <div style="width: 103px;" class="lastDivHeading"></div>  
        </div>
        <div class="clear"></div>
        <div class="eContent eClean" id="eMyProgress" style="height: auto; overflow:hidden;width: 907px;border-bottom: none;border-left: none;border-right: none;" runat="server">
           
            <div style="position:relative;width: 63%; float: left;">
                <uc:Chart ID="a" runat="server" />
            </div>

                <div style="width:100%;float: left;margin-top: 30px;">
                    

                <%if (Session["MemberType"].Equals("VVT")) {%>
                    <div style="width:50%;float: left;display: block;">
                <%}else {%>
                    <div style="width:100%;float: left;display: block;">
                <%}%>
                    <br />
                
                
                    <uc:Progress ID="prog" runat="server"></uc:Progress>
                </div>
            

                <asp:UpdatePanel ID="ExtPanel" runat="server">
                    <ContentTemplate>
                        <br />
                        <span  class="head">My Current Weight</span>
                        <br />
                        <asp:Label ID="currentWeightLabel" runat="server" Text="0.0" CssClass="num orange" /> <asp:Label ID="unitLabel" runat="server" CssClass="bold" />
                        <br />
                        <span style="font-weight:bold; display:block;">as at <asp:Label ID="currentWeightDateLabel" runat="server" Text="N/A" CssClass="orange" /></span><br />
                        <span  class="head">Today's weight</span>
                        <br />
                        <asp:RequiredFieldValidator ID="weightTextBoxRequiredFieldValidator" runat="server"
                                                    ControlToValidate="weightTextBox"
                                                    ValidationGroup="newWeight"
                                                    ErrorMessage="This field is required.<br />"
                                                    ForeColor="Red"
                                                    Display="Dynamic"/>
                        <asp:CompareValidator ID="weightTextBoxCompareValidator" runat="server"
                                                ControlToValidate="weightTextBox"
                                                ValidationGroup="newWeight"
                                                Type="Currency"
                                                Operator="DataTypeCheck"
                                                ErrorMessage="Please enter number only.<br />"
                                                ForeColor="Red"
                                                Display="Dynamic"/>
                        <asp:TextBox ID="weightTextBox" runat="server" CssClass="textbox" /> <asp:Label ID="unitLabel2" runat="server" />
                        <asp:Button ID="SaveWeightButton" runat="server" Text="Save" onclick="SaveWeightButton_Click" ValidationGroup="newWeight" />              
                        
                    </ContentTemplate>
                </asp:UpdatePanel>
                </div>
                <div style="width:100%;float: left;display: block;margin-top: 15px;">
                        <uc:DailyInspiration ID="uc1" runat="server" />
                </div>
            </div>
            
        
        
        <div class="eContent eClean" id="eMeasurements" runat="server" style="display: none;height: auto;width: 907px;border-bottom: none;border-left: none;border-right: none;">
           <uc:Measurements ID="MeasurementsUsercontrol" runat="server" />
        </div>  
        <div class="eContent eClean" id="eMyGoals" style="display: none;height: auto;width: 907px;border-bottom: none;border-left: none;border-right: none;"  runat="server">
            <uc:Goals ID="MyGoalsUsercontrol" runat="server" />
        </div>

</div>
<!-- /eProfileTab -->
