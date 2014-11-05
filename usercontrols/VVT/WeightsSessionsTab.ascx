<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WeightsSessionsTab.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.VVT.WeightsSessionsTab" %>
<%@ Register TagPrefix="uc" TagName="CurrentWeights" src="~/usercontrols/VVT/WeightSessions.ascx" %>
<%@ Register TagPrefix="uc" TagName="WeightsHistory" src="~/usercontrols/VVT/WeightsSessionsHistory.ascx" %>
<%@ Register TagPrefix="uc" TagName="CreateNew" src="~/usercontrols/VVT/VVT Initial Steps/ExercisePlanner.ascx" %>


<div id="eWeightsSessDiv" class="element">
    <div class="replace" id="WeightsSessTab" runat="server" ClientIDMode="Static">
        <div id="tabCurrentProgram" onclick="ReloadCurrentWeight();">Current Program</div>
        <div id="tabSessHistory" onclick="ReloadWeightHistory();">Session History</div>
        <div id="tabNewProgram" onclick="ReloadCreateWeights();">Create New Program</div>
        <div style="width: 304px;" class="lastDivHeading"></div>  
    </div>

    <div class="eContent eClean noBorder" style="margin-top: 45px;">
            <div style="display: block; padding-left: 5px; padding-bottom: 5px;">
                <% switch (Request.QueryString["tab"])
                {case "history": %>
                        <uc:WeightsHistory ID="WeightsHis" runat="server"/>
                <% ;break;
                    case "create":%>
                    <div style="margin: 0 auto;padding: 10px;">
                        <uc:CreateNew Id="createWeightsProg" runat="server"/>
                    </div>
                    <script type="text/javascript">
                        $("#trainingPlanTitle").remove();
                        $(".target").css("width", "");
                        $("#ContentPlaceHolderDefault_ContentPlaceHolderMain_WeightsSessionsTab_2_createWeightsProg_MyGoalImagebuttonNext").parent().parent().prev().remove();
                        $("#ContentPlaceHolderDefault_ContentPlaceHolderMain_WeightsSessionsTab_2_createWeightsProg_MyGoalImagebuttonNext").parent().parent().prev().remove();
                        $("#ContentPlaceHolderDefault_ContentPlaceHolderMain_WeightsSessionsTab_2_createWeightsProg_MyGoalImagebuttonNext").parent().parent().prev().remove();
                        $("#ContentPlaceHolderDefault_ContentPlaceHolderMain_WeightsSessionsTab_2_createWeightsProg_MyGoalImagebuttonNext").parent().parent().remove();

                        window.onbeforeunload = updateWeeklyPlannerSet;
                        
                    </script>
                <% ;break;
                    default: %>
                    <uc:CurrentWeights ID="currWeihgts" runat="server"/>
                <% ; break;
                }%>
            </div>
    </div> 
    <!-- eContent -->
    
    <div class="clear">&nbsp;</div>

</div>


<script type="text/javascript">
    $(document).ready(function () {
        <asp:Literal runat="server" id="weightsSessScript"></asp:Literal>;
        weightsSessionsLoadingScripts();
        $("#ContentPlaceHolderDefault_help").remove();
    });
</script>