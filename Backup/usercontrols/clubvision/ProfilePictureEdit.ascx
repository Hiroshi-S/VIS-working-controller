<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProfilePictureEdit.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.clubvision.ProfilePictureEdit" %>
<div class="leftCol profilePictureEdit" style="position: relative;background: none;">
<h1>Edit profile picture</h1>
<br />
<h3>Your current profile picture</h3> <br/>
<div class="clear"></div>
        <asp:Literal ID="literalImage" runat="server"></asp:Literal>
<h3 style="padding-top: 170px">Upload profile picture:</h3><asp:FileUpload ID="fileUpload" runat="server" CssClass="button-small vision_red_reverse rounded3" />
<p>Note: The maximum size of your custom image is 
254 by 152 pixels or 100KB (whichever is smaller).</p>
<p><a class="button-small grey_dark_reverse rounded3" href="#" style="font-weight: bold"> Cancel</a> or 
    <asp:Button runat=server id="buttonSave" CssClass="button-small vision_red rounded3" Text="Save Profile Photo"
        onclick="buttonSave_Click" /> 
</p>
</div>