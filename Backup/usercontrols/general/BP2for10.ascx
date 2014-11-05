<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BP2for10.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.general.bp2for10" %>

<center><h1 style="margin: 0 auto;">2 Personal Training Sessions for $10</h1></center>
<br/>
<br/>
<br/>

<div>
    <div style="float: left;width :45%; margin-right: 5%">
        <img src="/images/bppc.png" width="100%"></img>
        <div>
            <p>Terms and Condition</p>
            <p style="font-size: 9px;">Visit pass is not transferable and must be redeemed within 6 weeks pf purchase. Offer for first time users over 18 years only. 2 sessions to be used within 7 days of redemption. (You will need to bring a towel and wear suitable attire and covered footwear). Not valid for current members.
Only one pass per person. Other conditions may apply.
</p>
        </div>
    </div>
    <div class="regoform">
        <table>
            <tr>
                <td>Name</td>
                <td>
                    <asp:TextBox ID="textName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ForeColor="red" ID="RequiredFieldValidator1" ControlToValidate="textName" runat="server" ErrorMessage="*" ValidationGroup="bp2"></asp:RequiredFieldValidator>
                    </td>
            </tr>

            <tr>
                <td>Suburb</td>
                <td>
                    <asp:TextBox ID="textSuburb" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ForeColor="red" ID="RequiredFieldValidator2" ControlToValidate="textName" runat="server" ErrorMessage="*" ValidationGroup="bp2"></asp:RequiredFieldValidator>
                    </td>
            </tr>
            <tr>
                <td>Mobile</td>
                <td>
                    <asp:TextBox ID="textMobile" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ForeColor="red" ID="RequiredFieldValidator3" ControlToValidate="textMobile" runat="server" ErrorMessage="*" ValidationGroup="bp2"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Email</td>
                <td>
                    <asp:TextBox ID="textEmail" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ForeColor="red" ID="RequiredFieldValidator4" ControlToValidate="textEmail" runat="server" ErrorMessage="*" ValidationGroup="bp2"></asp:RequiredFieldValidator>

                </td>
            </tr>
            <tr>
                <td>Studio</td>
                <td>
                    <asp:DropDownList ID="DropDownStudio" runat="server"  Width="347" >
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ForeColor="red" ID="RequiredFieldValidator5" ControlToValidate="DropDownStudio" runat="server" ErrorMessage="*" ValidationGroup="bp2"></asp:RequiredFieldValidator>

                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:ImageButton ID="ImageButton1" Width="150" ValidationGroup="bp2"
                        ImageUrl="http://blenderlady.com/files/2014/03/paypal-buy-now-button.png" 
                        runat="server" onclick="ImageButton1_Click" /><br/>
                   <p style="font-size: 9px;">$10 charge plus $2 admin fee applies</p>
                </td>
            </tr>
        </table>  
    </div>
</div>

