<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StudioPartnerVOS.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.studiopages.StudioPartnerVOS" %>


<asp:ListView ID="ListView1" runat="server" OnDataBound="ListView1_DataBound">
    <EmptyDataTemplate>No studio partners have been listed yet.</EmptyDataTemplate>
    <ItemTemplate>
        <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("WEBSite") %>' />
        <asp:Literal ID="Literal1" runat="server" Text='<%# Eval("Partner") %>' ></asp:Literal>
    </ItemTemplate>
</asp:ListView>
