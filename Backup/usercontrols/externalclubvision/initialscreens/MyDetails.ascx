<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyDetails.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.externalclubvision.initialscreens.MyDetails" %>

<style type="text/css">
    .style1
    {
        height: 29px;
    }
</style>

<div class="pprofile" style="margin-right: 10px !important;">
    <span id="stepLabel" runat="server" style="color: #E27423;"><h2>STEP 1</h2></span><br/>

        <table>
            <col style="width:500px;" />
            <col style="width:250px;" />
            <col style="width:150px;" />

            <tr>
                <td>Title</td>
                <td>
                    <asp:DropDownList ID="titleDropDownList" runat="server" DataTextField="Value" DataValueField="Value" Width="204">
                        <asp:ListItem Value="0" Text=""></asp:ListItem>
                        <asp:ListItem Value="1">Mr</asp:ListItem>
                        <asp:ListItem Value="2">Mrs</asp:ListItem>
                        <asp:ListItem Value="2">Miss</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1"  ControlToValidate="titleDropDownList" InitialValue="0" runat="server" ErrorMessage="Title" Text="*" ForeColor="red"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td>First Name </td>
                <td><asp:TextBox ID="fNameText" runat="server" ></asp:TextBox></td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="fNameText"  Text="*" ForeColor="red" runat="server" ErrorMessage="First Name"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td>Last Name</td>
                <td><asp:TextBox ID="lNameText" runat="server"></asp:TextBox></td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="lNameText" runat="server" ErrorMessage="Last Name" Text="*" ForeColor="red"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td>Gender</td>
                <td>
                    <asp:RadioButtonList ID="genderRadioButtonList" runat="server" DataTextField="Value" Width="280px" RepeatDirection="Horizontal" DataValueField="Value">
                        <asp:ListItem Value="0">Male</asp:ListItem>
                        <asp:ListItem Value="1">Female</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="genderRadioButtonList" runat="server" ErrorMessage="Gender" Text="*" ForeColor="red"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td>Date of Birth</td>
                <td><asp:TextBox ID="txtDOB" runat="server" placeholder="dd/mm/yyyy" ></asp:TextBox><br /></td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="txtDOB" runat="server" ErrorMessage="Date of Birth" Text="*" ForeColor="red"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" Type="Date" ControlToValidate="txtDOB" Operator="DataTypeCheck" runat="server" ErrorMessage="Date is not in correct format" ForeColor="red" Text="*" ></asp:CompareValidator></td>
            </tr>

            <tr>
                <td><asp:TextBox ID="ageText" runat="server" Visible="False"></asp:TextBox></td>
            </tr>

            <tr>
                <td>Email</td>
                <td><asp:TextBox ID="emailText" runat="server"></asp:TextBox></td>
                <td><asp:RequiredFieldValidator ID="RequiredFieldValidator6" ForeColor="red"  
                        ControlToValidate="emailText" runat="server" ValidationGroup="A" 
                        ErrorMessage="Email" Text="*" ></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
                        ForeColor="red" ControlToValidate="emailText" 
                        ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" runat="server" 
                        ErrorMessage="Email is not in correct format" Text="*" ValidationGroup="A"/>
                </td>
            </tr>

            <tr>
                <td class="style1">Address Line</td>
                <td class="style1"><asp:TextBox ID="addressLine1Text" TextMode="MultiLine" runat="server" Rows="3" Width="198px"></asp:TextBox></td>
                <td class="style1">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" 
                        ControlToValidate="addressLine1Text" runat="server" ErrorMessage="*" ForeColor="red"></asp:RequiredFieldValidator></td>
            </tr>

            <tr>
                <td>Suburb</td>
                <td><asp:TextBox ID="suburbText" runat="server"></asp:TextBox></td>
                <td><asp:RequiredFieldValidator ID="RequiredFieldValidator7" 
                        ControlToValidate="suburbText" runat="server" ErrorMessage="*" ForeColor="red"></asp:RequiredFieldValidator></td>
            </tr>


            <tr>
                <td>State/Province</td>
                <td><asp:TextBox ID="stateText" runat="server"></asp:TextBox></td>
                <td><asp:RequiredFieldValidator ID="RequiredFieldValidator8" 
                        ControlToValidate="stateText" runat="server" ErrorMessage="*" ForeColor="red"></asp:RequiredFieldValidator></td>
            </tr>

            <tr>
                <td>Post Code</td>
                <td><asp:TextBox ID="postCodeText" runat="server"></asp:TextBox></td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" 
                        ControlToValidate="postCodeText" runat="server" ErrorMessage="*" ForeColor="red"></asp:RequiredFieldValidator></td>    
            </tr>


            <tr>
                <td>Country</td>
                <td><asp:DropDownList ID="CountryddList" runat="server" DataTextField="name_en" DataValueField="name_en" Width="204">
                </asp:DropDownList></td>

            </tr>

            <tr>
                <td>Mobile</td>
                <td><asp:TextBox ID="mobileText" runat="server"></asp:TextBox></td>
                <td>&nbsp;</td>
            </tr>

            <tr>
                <td>Home Phone</td>
                <td><asp:TextBox ID="homePhoneText" runat="server"></asp:TextBox></td>
                <td>
                    &nbsp;</td>
            </tr>

                       
        </table>
        <br/><br/>
        <div class="footy">
            <div class="footy-right">
                <asp:imagebutton ID="Imagebutton1" ImageUrl="/images/buttonSaveAndNext.gif" 
                    runat="server" onclick="Imagebutton1_Click"></asp:imagebutton>
                <asp:Label ID="ResultLabel1" runat="server" ForeColor="red" Visible="false" Text="Data is saved"></asp:Label>
            </div>
        </div>
</div>