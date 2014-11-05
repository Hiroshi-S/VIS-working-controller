<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CardioSession.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.VVT.CardioSession" %>
<style>
    #ContentPlaceHolderDefault_help {
        display: none;
    }
</style>


<div class="pTrainingDiary" id="weightSessDiv" style="margin-right: 10px !important;padding:10px;" runat="server" ClientIDMode="Static">
    <div style="font-weight: bold; font-size: 14px;float: left; padding-top: 4px; padding-right: 10px">Date Exercise:</div>
            <div style="border: 1px solid #999999; padding: 1px;float: left; height: 21px; width: 300px;">
            <input type="hidden" id="currentDate" runat="server" value="" />
            <asp:ImageButton runat="server" ID="buttonDayNext" Text=">" Visible="False"
                        ImageUrl="~/images/calendar_forward.gif" style="float: right" />
            <asp:ImageButton runat="server" ID="buttonDayPrev" Text="<"  Visible="False"
                        ImageUrl="~/images/calendar_back.gif" style="float: left" />
            <p style="display: inline-block; padding: 2px 14px"><asp:Literal runat="server" ID="literalDay"></asp:Literal></p>
            </div>
            <div style="float: left; position: relative; top: -1px">  
                                
            </div>
    <br /><br />
    <div id="TDresponsemessage" style="color: #6BB33A; font-weight: bold;height: 30px; display: block;"></div>
    <asp:Label ID="weekLabel" runat="server" Text="" Visible="True" ForeColor="#FFFFFF"></asp:Label>
                    
                    
    <asp:Literal ID="litProgramSummary" runat="server"></asp:Literal>
    <br/>

    <!-- as table -->
        <asp:Literal ID="litWeightsSess" runat="server"></asp:Literal>    
    <!-- as table -->
                    
    <br/><br />

    <div id="WeightsSessParagraph">
       
    </div>
                    
</div>
                
<div id="wsNotAVailable" clientidmode="Static" runat="server" Visible="False">
    <div style="margin: 0 auto;width: ">
        <h2>You don't have any cardio session added today</h2>
    </div>
</div>
                
<script type="text/javascript">
    $(document).ready(function () {
        $("div").children('input').bind('keypress', function (e) {
            return (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57) && e.which != 46) ? false : true;
        });
    });
</script>
