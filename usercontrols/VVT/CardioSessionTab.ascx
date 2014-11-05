<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CardioSessionTab.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.VVT.CardioSessionTab" %>
<%@ Register TagPrefix="uc" TagName="CardioHistory" src="~/usercontrols/VVT/CardioSessionHistory.ascx" %>
<%@ Register TagPrefix="uc" TagName="TodayCardio" src="~/usercontrols/VVT/CardioSession.ascx" %>

<div id="eWeightsSessDiv" class="element">
    <div class="replace" id="WeightsSessTab" runat="server" ClientIDMode="Static">
        <div id="tabCurrentProgram" onclick="ReloadCurrentCardio();">Today's Session</div>
        <div id="tabSessHistory" onclick="ReloadCardioHistory();">Cardio History</div>
        <div style="width: 505px;" class="lastDivHeading"></div>  
        <!--<div id="tabNewProgram" style="cursor: pointer; position: absolute; top: 0px; left: 404px; width: 201px; height: 45px" onclick="ReloadCreateWeights();"></div>-->
    </div>

    <div class="eContent eClean" style="margin-top: 45px;border-bottom: none;border-left: none;border-right: none;">
            <div style="display: block; padding-left: 5px; padding-bottom: 5px;">
                <% switch (Request.QueryString["tab"])
                {case "history": %>
                        <uc:CardioHistory Id="CardioHis" runat="server"/>
                <% ;break;
                    default: %>
                        <uc:TodayCardio Id="CardioToday" runat="server"/>
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
        cardioSessionsLoadingScripts();
        $("#ContentPlaceHolderDefault_help").remove();
    });
</script>