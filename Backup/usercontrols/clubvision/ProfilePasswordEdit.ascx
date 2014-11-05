<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProfilePasswordEdit.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.clubvision.ProfilePasswordEdit" %>
<div class="leftCol">
<h1>Edit password</h1>
<asp:Literal ID="editPasswordLiteral" runat="server" Text=""></asp:Literal>
<div style="width: 140px; display: inline-block;padding-top: 10px;">Old password:</div><input type="text" id="oldPassword" runat="server" /><br />
<div style="width: 140px; display: inline-block;padding-top: 10px;">New password:</div><input type="text" id="newPassword" runat="server" /><br />
<div style="width: 140px; display: inline-block;padding-top: 10px;">Comfirm new password:</div><input type="text" id="newPasswordConfirm" runat="server" /><br />
<p  style="padding-left: 140px; padding-top: 10px;">
   <a href="/club-vision/my-profile/edit-profile-password/" class="button-small grey_dark_reverse rounded3">Cancel</a> or <asp:Button ID="save" runat="server" CssClass="button-small vision_red rounded3" Text="Save Password"
        onclick="save_Click" /> </p>
</div>