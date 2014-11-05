<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MealPictureEdit.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.clubvision.MealPictureEdit" %>
<div class="leftCol mealPictureEdit" style="position: relative">
<h2>Change Meal Image</h2>
<br />
<p>Current meal image</p>
        <asp:Literal ID="literalImage" runat="server"></asp:Literal>
<p style="padding-top: 170px">Upload meal image:</p><asp:FileUpload ID="fileUpload" runat="server" />
<p>Note: The maximum size of your  image is 
XX by XX.</p>
<p><asp:ImageButton ImageUrl="/images/buttonSave.gif" runat=server id="buttonSave"
        onclick="buttonSave_Click" /> <a href="#" id="cancel" runat="server" style="font-weight: bold">or Cancel</a></p>
</div>