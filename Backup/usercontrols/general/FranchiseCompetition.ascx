<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FranchiseCompetition.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.general.FranchiseCompetition" %>

<script type="text/javascript">
    function browserDetect() {
        var isChrome = navigator.userAgent.toLowerCase().indexOf('chrome') > -1;
        var str = "ContentPlaceHolderDefault_ContentPlaceHolderMain_ctl00_FranchiseCompetition_8_FileUpload";
        if (isChrome) {
            for (var i = 1; i < 7; i++) {
                document.getElementById(str + i.toString()).style.width = "288px";
                document.getElementById(str + i.toString()).style.padding = "0px 30px 15px 30px";
                document.getElementById(str + i.toString()).style.height = "32px";

            }
            return false;
        }
        if (navigator.userAgent.indexOf("Firefox") != -1) {
            for (var iss = 1; iss < 7; iss++) {
                document.getElementById(str + iss.toString()).style.width = "288px";
                document.getElementById(str + iss.toString()).style.padding = "30px 30px 15px 30px";
                document.getElementById(str + iss.toString()).style.margin = "30px 30px 0px 0px";
                document.getElementById(str + iss.toString()).style.height = "32px";

            }
            return false;
        }
        return false;
    }
</script>

<div id="message" style="display: none" runat="server">
    <h3>Thank you. Your form has been submitted.</h3>
</div>

<div id="franchiseCompetition" runat="server" class="regoform">
    <table>
        <tr>
            <td>
                <b>First Name</b>
            </td>
            <td>
                <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtFirstName" ValidationGroup="FC" runat="server" ErrorMessage="First Name is required" Text="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <b>Last Name</b>
            </td>
            <td>
                <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtLastName" ValidationGroup="FC" runat="server" ErrorMessage="Last Name is required" Text="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td><b>Address</b></td>
            <td>
                <asp:TextBox ID="txtAddress" TextMode="MultiLine" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtAddress" ValidationGroup="FC" runat="server" ErrorMessage="Address is required" Text="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td><b>Phone</b></td>
            <td><asp:TextBox ID="txtPhone" runat="server"></asp:TextBox></td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4"  ControlToValidate="txtPhone" ValidationGroup="FC" runat="server" ErrorMessage="Phone is required" Text="*"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtPhone" ValidationGroup="FC" ValidationExpression="^[0-9]+$" runat="server" Text="*" ErrorMessage="Phone is not in correct format"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td><b>Email</b></td>
            <td><asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtEmail" ValidationGroup="FC" runat="server" ErrorMessage="Email is required" Text="*"> </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="red" ControlToValidate="txtEmail" 
                        ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" runat="server" ErrorMessage="Email is not in correct format" ValidationGroup="FC" Text="*"/>
            </td>
        </tr>
        <tr>
            <td><b>Date of Birth</b></td>
            <td><asp:TextBox ID="txtDOB" placeholder="DD/MM/YYYY" runat="server"></asp:TextBox></td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtDOB" ValidationGroup="FC" runat="server" ErrorMessage="Date of Birth is required" Text="*"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" Type="Date" ControlToValidate="txtDOB" Operator="DataTypeCheck" ValidationGroup="FC" runat="server" Text="*" ErrorMessage="Date is not in correct format" ></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td><b>Experience</b>
                <br/>(Upload resume. <i>Must be a previous business owner or 2+ years Personal Trainer</i>)
            </td>
            <td><asp:FileUpload ID="FileUpload1" runat="server" Width="347" ></asp:FileUpload></td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="FileUpload1" ValidationGroup="FC" runat="server" ErrorMessage="This file is required" Text="*"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidator1" ValidationGroup="FC" 
                    ControlToValidate="FileUpload1" runat="server"  Text="*"
                    ErrorMessage="File must be document or PDF" onservervalidate="CustomValidator1_ServerValidate1" 
                    ></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td><b>Business Plan</b> <br/>
                (<i>Download this <a href="/FranchiseCompetition/BusinessPlan.rtf">file</a> and then upload it</i>)
            </td>
            <td><asp:FileUpload ID="FileUpload2" runat="server" Width="347" ></asp:FileUpload></td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="FileUpload2" ValidationGroup="FC" runat="server" ErrorMessage="This file is required" Text="*"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidator2" ValidationGroup="FC" 
                    ControlToValidate="FileUpload2" runat="server" 
                    ErrorMessage="File must be document or PDF" onservervalidate="CustomValidator2_ServerValidate" Text="*"
                    ></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td><b>Marketing Plan</b>
                <br/>(<i>Upload a 12 month marketing plan</i>)
            </td>
            <td><asp:FileUpload ID="FileUpload3" runat="server" Width="347" ></asp:FileUpload></td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" Text="*"  ControlToValidate="FileUpload3" ValidationGroup="FC" runat="server" ErrorMessage="This file is required"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidator3" ControlToValidate="FileUpload3" 
                    ValidationGroup="FC" runat="server" Text="*"
                    ErrorMessage="File must be document or PDF" onservervalidate="CustomValidator3_ServerValidate" 
                    ></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td><b>Why You Should be The Winner</b>
                <br/>(<i>Max 600 words</i>)</td>
            <td><asp:FileUpload ID="FileUpload4" runat="server" Width="347" ></asp:FileUpload></td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="FileUpload4" ValidationGroup="FC" runat="server" Text="*" ErrorMessage="This file is required"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidator4" ControlToValidate="FileUpload4" 
                    ValidationGroup="FC" runat="server" ErrorMessage="File must be document or PDF" Text="*"
                    onservervalidate="CustomValidator4_ServerValidate"></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td><b>Please provide evidence of financial readiness to open a studio in 2013</b></td>
            <td><asp:FileUpload ID="FileUpload5" runat="server" Width="347"></asp:FileUpload></td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="FileUpload5" ValidationGroup="FC" runat="server" Text="*" ErrorMessage="This file is required"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidator5" ControlToValidate="FileUpload5" 
                    ValidationGroup="FC" runat="server" ErrorMessage="File must be document or PDF" Text="*"
                    onservervalidate="CustomValidator5_ServerValidate"></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td><b>Signed copy of the terms and conditions</b>
            <br/>
                (<i>Download this <a href="/FranchiseCompetition/TermsandConditions.pdf">file</a>, sign and scan it and then upload it. PDF only.</i>)
            </td>
            <td><asp:FileUpload ID="FileUpload6" runat="server"  Width="347"  ></asp:FileUpload>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="FileUpload6" ValidationGroup="FC" runat="server"  Text="*" ErrorMessage="This file is required"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidator6" runat="server" 
                    ControlToValidate="FileUpload6"
                    ErrorMessage="File must be document or PDF" Text="*"
                     ValidationGroup="FC" onservervalidate="CustomValidator6_ServerValidate"></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td><b>Are you currently living in Brisbane or able to move there if successful</b></td>
            <td><asp:DropDownList ID="DropDownList1" runat="server"  Width="347" >
                    <asp:ListItem runat="server" Value="0" Text=""></asp:ListItem>
                    <asp:ListItem runat="server" Value="1" Text="Yes"></asp:ListItem>
                </asp:DropDownList></td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="DropDownList1" InitialValue="0" ValidationGroup="FC" runat="server" ErrorMessage="This field is required" Text="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="3"><br/><br/>
                <asp:ImageButton ID="imgButtonSubmit" ImageUrl="/images/buttonSubmit.gif" 
                    ValidationGroup="FC" runat="server" onclick="imgButtonSubmit_Click"></asp:ImageButton>
            <br/>
            Please note files accepted are in PDF or Microsoft Word type document only with size less than 5MB.     
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="FC"  HeaderText="Please note the following" ForeColor="red" DisplayMode="BulletList" runat="server" />
</div>


<script type="text/javascript">    browserDetect();</script>
    