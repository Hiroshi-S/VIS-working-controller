<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JoinCVRegistration.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.studiopages.JoinCVRegistration" %>
<div id="JoinClubVision">
    Name : <asp:TextBox ID="NameTextBox" runat="server"></asp:TextBox> 
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="NameTextBox" 
        ErrorMessage="Name is required"></asp:RequiredFieldValidator>
    <br/>
    Email : <asp:TextBox ID="EmailTextBox" runat="server"></asp:TextBox> 
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="EmailTextBox"
        ErrorMessage="Email is required"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="EmailTextBox" ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>
    
    <br />
    
    Retype Email : <asp:TextBox ID="RemailTextBox" runat="server"></asp:TextBox> 
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="RemailTextBox"
        ErrorMessage="Retype email is required"></asp:RequiredFieldValidator>
    <asp:CompareValidator ID="CompareValidator1"  ControlToValidate="RemailTextBox" Type="String" Operator="Equal"  ControlToCompare="EmailTextBox" runat="server" ErrorMessage="Emails do not match"></asp:CompareValidator>
    <br/><br/>
    <asp:ImageButton ID="ImageButton1" AlternateText="Subscribe" runat="server" ImageUrl="http://visionpt.com.au/media/137175/submit.gif" 
        onclick="ImageButton1_Click" />
    <br/>
    <asp:Label ID="confirmLabel" runat="server" Text="Label" Visible="False" ForeColor="red"></asp:Label>
</div>
