<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsletterSubscribe.ascx.cs"
    Inherits="VisionPersonalTrainingProject.usercontrols.elements.NewsletterSubscribe" %>
<h2>Subscribe Now</h2>
<div class="row">
    <label>Name Surname</label>
    <asp:TextBox ID="tbName" runat="server"></asp:TextBox>
</div>
<!-- /row -->
<div class="row">
    <label>Email</label>
    <asp:TextBox ID="tbEmail" runat="server"></asp:TextBox>
    <asp:RegularExpressionValidator ID="valEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ForeColor="Red" Display="Dynamic" ControlToValidate="tbEmail" ErrorMessage="*"  ValidationGroup="BookConsultation"></asp:RegularExpressionValidator>
     <asp:RequiredFieldValidator ID="valEmail" runat="server" ControlToValidate="tbEmail" ForeColor="Red" ErrorMessage="*"  ValidationGroup="BookConsultation"></asp:RequiredFieldValidator>
</div>
<!-- /row -->
<asp:LinkButton class="replace buttonSubscribe" runat="server" ID="btnSubscribe"
    OnClick="btnSubscribe_Click">Subscribe</asp:LinkButton>
<div class="row">
    <asp:Label ID="lblMessage" runat="server"></asp:Label>
</div>
