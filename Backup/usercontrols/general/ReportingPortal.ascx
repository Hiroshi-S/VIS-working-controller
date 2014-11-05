<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportingPortal.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.general.ReportingPortal" %>

<div class="leftCol" style="width: 902px;">
    
    <div id="gateDiv" runat="server" ClientIDMode="Static">
        <h2>VVT Reporting Portal</h2> <br/><br/>
        
        <i>Strictly restricted access area</i><br/>
            
        <b>Password</b> 
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
        <asp:Button ID="EnterButton" runat="server" Text="Enter" onclick="EnterButton_Click"  />
        <asp:Literal ID="litDivGate" runat="server"></asp:Literal>

    </div>
    <div id="gateMenu" runat="server" Visible="False" style="width: 900px;" >
        <div class="replace" id="ReportingTab" runat="server" ClientIDMode="Static" style="height: 45px;">
            <div id="tabExtVVTStat" style="cursor: pointer; top: 0px; left: 0px; width: 201px; height: 45px;float: left;" onclick="ReloadextVVTStatDiv();"></div>
            <div id="tabShareStat" style="cursor: pointer; top: 0px; left: 202px; width: 201px; height: 45px; float: left;" onclick="ReloadShareStat();"></div>
            <!--<div id="tabNewProgram" style="cursor: pointer; position: absolute; top: 0px; left: 404px; width: 201px; height: 45px" onclick="ReloadCreateWeights();"></div>-->
        </div>
 
    </div>

    <div id="extVVTStatDiv" runat="server" Visible="False" ClientIDMode="Static" style="width: 900px;display: none; padding:10px;">
               
        From <input type="text" placeholder="dd/mm/yyyy" class="datepicker" id="fromDate1" runat="server"/>
        To <input type="text" placeholder="dd/mm/yyyy" class="datepicker" id="toDate1" runat="server"/>
        <asp:Button ID="GetSummaryStatButton" runat="server" Text="Get Summary" 
            onclick="GetSummaryStatClick" />
        <asp:Button ID="GetListButton" runat="server" Text="Get List" 
            onclick="GetListClick" />
        
        <asp:Literal ID="extVVTStatLit" runat="server"></asp:Literal>
    </div>
    <div id="ShareStatDiv" runat="server" Visible="False" ClientIDMode="Static" style="width: 900px;display:none;padding:10px;">
                   
        From <input type="text" placeholder="dd/mm/yyyy" class="datepicker" id="fromDate2" runat="server"/>
        To <input type="text" placeholder="dd/mm/yyyy" class="datepicker" id="toDate2" runat="server"/>
        <asp:Button ID="GetSummaryShareButton" runat="server" Text="Get Summary" 
            onclick="GetSummaryShareClick" />
            
        <asp:Literal ID="ShareStatLit" runat="server"></asp:Literal>
    </div>
</div>
