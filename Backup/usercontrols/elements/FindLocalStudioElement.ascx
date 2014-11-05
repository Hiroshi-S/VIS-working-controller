<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FindLocalStudioElement.ascx.cs"
    Inherits="VisionPersonalTrainingProject.usercontrols.elements.FindLocalStudioElement" %>
<div id="eFindStudio" class="element noPaddingLeft">
    <!-- 290 -->
    <h3 class="replace">Find a Local Studio</h3>
    <div class="eContent eGreen">

        <asp:DropDownList AutoPostBack="true" ID="ddlStudio" runat="server" 
            onselectedindexchanged="ddlStudio_SelectedIndexChanged">
        </asp:DropDownList>
        <p>Or...</p>
        <asp:TextBox onclick="this.value='';" onfocus="this.select()" ID="tbPostcode" Text="Enter Postcode" runat="server"></asp:TextBox>
        <asp:LinkButton CssClass="replace"  ID="btnFind" runat="server" 
            onclick="btnFind_Click">Find Studio</asp:LinkButton>

        <div class="clear"></div>
    </div>
    <!-- /eContent -->
    <div class="clear"></div>
</div>
<!-- /eFindStudio -->
