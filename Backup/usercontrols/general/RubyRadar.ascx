<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RubyRadar.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.general.RubyRadar" %>


<div style="margin-left: 150px; margin-top: 30px;"><img src="/images/ruby.png" alt="" width="553" height="" />
<div style="color: red; font-style: italic; font-size: 19px; padding: 20px 0px; width: 553px;">As a valued RubyRadar member, you are entitled to the following privileges from our partner Vision Personal Training. Simply select the option you prefer from the menu - we'll get the offer to you straight away.</div>

    <div id="formPart" class="regoform" runat="server">
        <table>
        
            <tr>
                <td>
                    Pick Your Offer
                </td>
                <td>
                    <asp:DropDownList ID="DropDownListOffer" runat="server" Width="347" AutoPostBack="True" 
                        onselectedindexchanged="DropDownListOfferSelectedIndexChanged" >
                        <asp:ListItem runat="server" Value="0" Text=""></asp:ListItem>
                        <asp:ListItem runat="server" Value="1">$100 gift voucher to a Vision Personal Training studio</asp:ListItem>
                        <asp:ListItem runat="server" Value="2">Join Vision Virtual Training for 30 days for $1</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ControlToValidate="DropDownListOffer" InitialValue="0" ID="RequiredFieldValidator6" ForeColor="Red" ValidationGroup="Starshot" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr id="studiotr" runat="server" Visible="False">
                <td>
                    Pick Your Studio
                </td>
                <td>
                    <asp:DropDownList ID="DropDownListStudio" runat="server" Width="347" >
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ControlToValidate="DropDownListStudio" InitialValue="0" ID="RequiredFieldValidator1" ForeColor="Red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
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
                        ImageUrl="/images/buttonActivate.gif" onclick="ImageButton1Click"/>
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
    
    <div id="notificationPart2" runat="server" Visible="False">
        <h3>Thank you for submitting your voucher. Please proceed to <a href="/vision-virtual-training/join-now-registration/">registration page</a> and use voucher code <strong>RR01</strong> to get first month for only $1.</h3>
    </div>

</div>
