<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IpnHandler.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.general.IpnHandler" %>

<h2>Test automation email to customer</h2> 
<br />
To email :
<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
<br />
<br/>
<asp:Button ID="Button1" runat="server" Text="Send activation email" 
    onclick="Button1Click" />