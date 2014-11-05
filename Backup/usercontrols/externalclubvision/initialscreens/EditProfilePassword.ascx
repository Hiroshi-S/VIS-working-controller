<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditProfilePassword.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.externalclubvision.initialscreens.EditProfilePassword" %>

<div class="leftCol">
<h1>Change password</h1>
<asp:Literal ID="editPasswordLiteral" runat="server" Text="hahah"></asp:Literal>
<div style="width: 140px; display: inline-block;padding-top: 10px;">Old password:</div><input type="text" id="oldPassword" runat="server" /><br />
<div style="width: 140px; display: inline-block;padding-top: 10px;">New password:</div><input type="text" id="newPassword" runat="server" /><br />
<div style="width: 140px; display: inline-block;padding-top: 10px;">Comfirm new password:</div><input type="text" id="newPasswordConfirm" runat="server" /><br />
<p  style="padding-left: 140px; padding-top: 10px;">
        <a href="/club-vision/my-profile/edit-profile/" class="button-small grey_dark_reverse rounded3">Cancel</a> or <asp:Button ID="Button1" runat="server" CssClass="button-small vision_red rounded3" Text="Save Password"
        onclick="SaveClick" /> </p>
</div>