<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CardioSessionHistory.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.VVT.CardioSessionHistory" %>
<style type="text/css">
    img {
        cursor: hand;
        cursor: pointer;
    }
</style>
<div class="pTrainingDiary" id="weightSessDiv" style="margin-right: 10px !important;padding:10px;" runat="server" ClientIDMode="Static">
    <div style="font-weight: bold; font-size: 14px;float: left; padding-top: 4px; padding-right: 10px">Date Exercise:</div>
            <div style="border: 1px solid #999999; padding: 1px;float: left; height: 21px; width: 300px;">
            <input type="hidden" id="currentDate" runat="server" value="" />
                            
            <p style="display: inline-block; padding: 2px 14px" id="datep"><asp:Literal runat="server" ID="literalDay"></asp:Literal></p>
            </div>
            <div style="float: left; position: relative; top: -1px">  
                                
            </div>
    <br /><br />
    <div id="TDresponsemessage" style="color: #6BB33A; font-weight: bold;height: 30px; display: block;"></div>
    <asp:Label ID="weekLabel" runat="server" Text="" Visible="True" ForeColor="#FFFFFF"></asp:Label>
    
                    
    <span id="programSummary"><asp:Literal ID="litProgramSummary" runat="server"></asp:Literal></span>

    <div id="WeightsSessHistory">
          <a id="scroller-prev" data-skip="4" data-stop="NO"><img src="/images/buttonPrev.png" alt="Prev" /></a>
          <a id="scroller-next" data-skip="0" data-stop="NO"><img src="/images/buttonNext.png" alt="Next" /></a>
        <asp:Literal id="litCardioHistory" runat="server"></asp:Literal>
        <div style="clear: both"></div>
    </div>
    <div style="clear: both"></div>
</div>

<div id="wsNotAVailable" clientidmode="Static" runat="server" Visible="False">
    <h2>You don't have any Cardio Sessions history to display</h2>
</div>

<script type="text/javascript">
    $(document).ready(function () {
        $("#WeightsSessHis_scroller").height($("#WeightsSessHis_scroller > div").first().height());
        $("#WeightsSessHis_frame").height($("#WeightsSessHis_scroller").height());
        $("#WeightsSessHistory").height($("#WeightsSessHis_scroller").height() + 10);
        $("#scroller-prev").css("top", ($("#WeightsSessHistory").height() / 2) + "px");
        $("#scroller-next").css("top", ($("#WeightsSessHistory").height() / 2) + "px");
    });
</script>

