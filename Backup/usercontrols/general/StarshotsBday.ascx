<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StarshotsBday.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.general.StarshotsBday" %>

<div id="formPart" class="regoform" runat="server">
    <table>
        <tr>
            <td>
                Pick Your Studio
            </td>
            <td>
                <asp:DropDownList ID="DropDownListStudio" runat="server" Width="347" >
                </asp:DropDownList>
            </td>
            <td>
                <asp:RequiredFieldValidator ControlToValidate="DropDownListStudio" ID="RequiredFieldValidator1" ForeColor="Red" ValidationGroup="Starshot" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Name</td>
            <td>
                <asp:TextBox ID="TextBoxName" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ControlToValidate="TextBoxName" ID="RequiredFieldValidator2" ForeColor="Red" ValidationGroup="Starshot" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Phone Number</td>
            <td>
                <asp:TextBox ID="TextBoxPhone" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ControlToValidate="TextBoxPhone" ID="RequiredFieldValidator3" ForeColor="Red" ValidationGroup="Starshot" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Email Address</td>
            <td>
                <asp:TextBox ID="TextBoxEmail" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ControlToValidate="TextBoxEmail" ID="RequiredFieldValidator4" ForeColor="Red" ValidationGroup="Starshot" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Number 1 Health and Fitness Priority
            </td>
            <td>
                <asp:TextBox ID="TextBoxPriority" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ControlToValidate="TextBoxPriority" ID="RequiredFieldValidator5" ForeColor="Red" ValidationGroup="Starshot" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:ImageButton ID="ImageButton1" runat="server" ValidationGroup="Starshot" 
                    ImageUrl="/images/buttonActivate.gif" onclick="ImageButton1_Click"/>
            </td>
            <td>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Starshot" HeaderText="Please Check Starred Points" ShowSummary="False" />
            </td>
        </tr>
    </table>
</div>

<div id="notificationPart" runat="server" Visible="False">
    <h3>Thank you for submitting your voucher. You will be contacted soon by one of our trainers.</h3>
</div>