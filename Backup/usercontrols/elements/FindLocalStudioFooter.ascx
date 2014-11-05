<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FindLocalStudioFooter.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.elements.FindLocalStudioFooter" %>
      <div class="ftrCol ftrColLast" id="ftrStudioCol">
        <h4 class="replace">Find a local Studio</h4>
            <asp:DropDownList AutoPostBack="true" ID="ddlStudioFoot" runat="server" 
              OnSelectedIndexChanged="ddlStudioFoot_SelectedIndexChanged">
            </asp:DropDownList>
          <p>Or...</p>
          <asp:TextBox onclick="this.value='';" onfocus="this.select()" ID="tbPostcodeFoot" Text="Enter Postcode" runat="server"></asp:TextBox>
            <asp:LinkButton CssClass="replace"  ID="btnFindFoot" runat="server" onclick="btnFindFoot_Click">Find Studio</asp:LinkButton>
      </div><!-- /ftrCol -->