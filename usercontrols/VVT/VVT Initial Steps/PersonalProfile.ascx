<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PersonalProfile.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.VVT.VVT_Initial_Steps.PersonalProfile" %>

<div class="pprofile" style="margin-right: 10px !important;">
        <table>
            <col style="width:300px;" />
            <col style="width:250px;" class="regoform" />
            <col style="width:150px;" />

            <tr>
                <td>Salutation</td>
                <td>
                    <asp:DropDownList ID="titleDropDownList" runat="server" DataTextField="Value" DataValueField="Value">
                        <asp:ListItem Value="0" Text=""></asp:ListItem>
                        <asp:ListItem Value="1">Mr</asp:ListItem>
                        <asp:ListItem Value="2">Mrs</asp:ListItem>
                        <asp:ListItem Value="2">Miss</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="pprofile" ControlToValidate="titleDropDownList" InitialValue="0" runat="server" ErrorMessage="Title" Text="*" ForeColor="red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td>First Name </td>
                <td><asp:TextBox ID="fNameText" runat="server" ReadOnly="True" ></asp:TextBox></td>
                <td>
                    
                </td>
            </tr>

            <tr>
                <td>Last Name</td>
                <td><asp:TextBox ID="lNameText" runat="server" ReadOnly="True"></asp:TextBox></td>
                <td>
                   
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
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="pprofile" ControlToValidate="genderRadioButtonList" runat="server" ErrorMessage="Gender" Text="*" ForeColor="red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td>Date of Birth</td>
                <td><asp:TextBox ID="txtDOB" runat="server" placeholder="dd/mm/yyyy" CssClass="datepicker"></asp:TextBox><br /></td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ValidationGroup="pprofile" ControlToValidate="txtDOB" runat="server" ErrorMessage="Date of Birth" Text="*" ForeColor="red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" Type="Date" ValidationGroup="pprofile" ControlToValidate="txtDOB" Operator="DataTypeCheck" runat="server" ErrorMessage="Date is not in correct format" ForeColor="red" Text="*" SetFocusOnError="True" ></asp:CompareValidator></td>
            </tr>

            <tr id="trAge" runat="server" Visible="False">
                <td><asp:TextBox ID="ageText" runat="server" Visible="False" ></asp:TextBox></td>
            </tr>

            <tr>
                <td>Email</td>
                <td><asp:TextBox ID="emailText" runat="server" ReadOnly="True" ></asp:TextBox></td>
                <td>
                </td>
            </tr>

            <tr>
                <td class="style1">Address Line</td>
                <td class="style1"><asp:TextBox ID="addressLine1Text" TextMode="MultiLine" runat="server" Rows="3"></asp:TextBox></td>
                <td class="style1">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="pprofile"
                        ControlToValidate="addressLine1Text" runat="server" ErrorMessage="*" ForeColor="red" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
            </tr>

            <tr>
                <td>Suburb</td>
                <td><asp:TextBox ID="suburbText" runat="server"></asp:TextBox></td>
                <td><asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="pprofile"
                        ControlToValidate="suburbText" runat="server" ErrorMessage="*" ForeColor="red" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
            </tr>


            <tr>
                <td>State/Province</td>
                <td><asp:TextBox ID="stateText" runat="server"></asp:TextBox></td>
                <td><asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="pprofile"
                        ControlToValidate="stateText" runat="server" ErrorMessage="*" ForeColor="red" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
            </tr>

            <tr>
                <td>Post Code</td>
                <td><asp:TextBox ID="postCodeText" runat="server"></asp:TextBox></td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="pprofile"
                        ControlToValidate="postCodeText" runat="server" ErrorMessage="*" ForeColor="red" SetFocusOnError="True"></asp:RequiredFieldValidator></td>    
            </tr>


            <tr>
                <td>Country</td>
                <td><asp:DropDownList ID="CountryddList" runat="server" DataTextField="name_en" DataValueField="name_en">
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
        <div class="footy" style="display: none;">
            <div class="footy-right">
                <asp:imagebutton ID="Imagebutton1" ImageUrl="/images/buttonSaveAndNext.gif" ClientIDMode="Static" ValidationGroup="pprofile"
                    runat="server" onclick="Imagebutton1_Click"></asp:imagebutton>
                <asp:Label ID="ResultLabel1" runat="server" ForeColor="red" Visible="false" Text="Data is saved"></asp:Label>
            </div>
        </div>
        
        <div class="istepsDiv">
            <div class="istepsWrapper">
                <div style="border-left: none;" onclick="skipInitialSteps(1);return false;">
                    <div><img src="/images/icons/web/skip.png" alt="picture"/></div>
                    <div>Skip</div>
                </div>
                <div id="nextDiv" onclick="$('#Imagebutton1').click();return false;">
                    <div><img src="/images/icons/web/nextarrow.png" alt="picture"/></div>
                    <div>Next</div>
                </div>
            </div>
            
        </div>
</div>

<script type="text/javascript">
    $("#flat1").addClass("active");
</script>