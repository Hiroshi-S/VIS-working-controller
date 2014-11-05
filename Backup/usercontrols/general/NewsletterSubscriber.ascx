<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsletterSubscriber.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.general.NewsletterSubscriber" %>

<div id="showForm" runat="server">
    <h3 style="background: none !important;height: 20px !important;">Sign up now to be notified of our quarterly newsletter and other special offers</h3>
    <table>
        <tr>
            <td>
                Name
            </td>
            <td>
                <asp:TextBox ID="TextBoxName" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="valEnqFirstName" runat="server" ControlToValidate="TextBoxName" ForeColor="Red" ErrorMessage="*" ValidationGroup="EnquiryLightBox"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Email
            </td>
            <td>
                <asp:TextBox ID="TextBoxEmail" runat="server"></asp:TextBox>
            </td>
            <td>
                 <asp:RegularExpressionValidator ID="valEnqEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ForeColor="Red" Display="Dynamic" ControlToValidate="TextBoxEmail" ErrorMessage="Invalid email" ValidationGroup="EnquiryLightBox"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="valEnqEmail" runat="server" ControlToValidate="TextBoxEmail" ForeColor="Red" ErrorMessage="*" ValidationGroup="EnquiryLightBox"></asp:RequiredFieldValidator>

            </td>
        </tr>
        <tr>
            <td>
                Country
            </td>
            <td colspan="2">
                <asp:DropDownList ID="DropDownListCountry" runat="server" Width="156">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:ImageButton ID="btnSubmit" ImageUrl="/images/buttonSubscribe.gif" ValidationGroup="EnquiryLightBox"  runat="server" OnClick="btnSubmit_Click"></asp:ImageButton>
            </td>
        </tr>
    </table>

    <p>Current Vision Virtual Training or Vision Personal Training Clients please access this through your <a href="/club-vision/">Vision Virtual Training log in.</a></p>
    
</div>
<div id="showMessage" runat="server" Visible="False">
    <h4>
        Thanks for submitting your detail, you will get notified when the new edition is coming.
    </h4>
</div>
