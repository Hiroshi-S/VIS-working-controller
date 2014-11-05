<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IpnSandboxHandler.aspx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.general.IpnSandboxHandler" %>

<h2>Test automation email to customer</h2> 
<br />
To email :
<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
<br />
<br/>
<asp:Button ID="Button1" runat="server" Text="Send activation email" 
    onclick="Button1Click" />
    
    <br />
<br/><br />
<br/><br />
<br/>
    <div style="background-color: #cccccc">
     <h2>Send Email to Unregistered Clients</h2> 
     Range of ID <br/>
     From:    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>  <br/><br/>
     
     Is it test mode? 
        <asp:CheckBox ID="CheckBox1" runat="server" Checked="True" /><br/><br/>

    <asp:Button ID="Button2" runat="server" Text="Send Emails"
            onclick="Button1_Click" />

        <br />
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    </div>