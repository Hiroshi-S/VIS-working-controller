<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ResendConfirmEmail.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.externalclubvision.ResendConfirmEmail" %>

<div class="leftCol" style="position: relative">
<h2>Have Not Received an Email Yet?</h2>
<br />
<p class="error" runat="server" id="emailerror" style="display: none;color:red;">Your email address is not recognised. Please contact admin@clubvision.com.au to reset your details.</p>
<p runat="server" id="emailsuccess" style="display: none;color:red;">Your login details have been resent to your email.</p>
<p>Please type in your email address below:</p>
<asp:TextBox ID="emailTextBox" runat="server"></asp:TextBox><br /><br />
<asp:ImageButton ID="ImageButton1" runat="server" 
        ImageUrl="/images/buttonSubmit.gif" onclick="ImageButton1_Click" />

</div>
