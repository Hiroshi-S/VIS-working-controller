<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditStudio.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.studiopages.EditStudio" %>
    <link rel="stylesheet" type="text/css" media="all" href="/css/jsDatePick_ltr.min.css" />
    <script type="text/javascript" src="/scripts/jsDatePick.min.1.3.js"></script>
  
<style>
fieldset {
  padding: 10px;
  }
label {
  float:left;
  width:140px;
  text-align:right;
  font-weight:bold;
  margin-right:5px;
  }
</style>

<asp:Panel ID="pnlFields" runat="server">
<!--
<asp:ValidationSummary ID="ValSummaryAddEvent" CssClass="error" ValidationGroup="ValGrpAddEvent" EnableClientScript="true" runat="server" HeaderText="Add New Event : The following need to be provided:" />
-->
<h1><asp:Label ID="lblTitle" runat="server" Text="Label"></asp:Label></h1>

<fieldset>
    <legend>Content</legend>
    
    <label>Intro Text:</label>
    <asp:TextBox ID="txtIntro" runat="server" TextMode="MultiLine"></asp:TextBox>
    <br />
</fieldset>

<fieldset>
    <legend>Contact Details</legend>
    <label>Address Line 1:</label>
    <asp:TextBox ID="txtAddress1" runat="server"></asp:TextBox>
    <br />
    <label>Address Line 2:</label>
    <asp:TextBox ID="txtAddress2" runat="server"></asp:TextBox>
    <br />
    <label>Contact Person:</label>
    <asp:TextBox ID="txtContact" runat="server"></asp:TextBox>
    <br />
    <label>Phone:</label>
    <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
    <br />
    <label>Fax:</label>
    <asp:TextBox ID="txtFax" runat="server"></asp:TextBox>
    <br />
    <label>Email:</label>
    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
    <br />
    <label>Directions:</label>
    <asp:TextBox ID="txtDirections" runat="server" TextMode="MultiLine"></asp:TextBox>
    <br />
    <label>Accessibility:</label>
    <asp:TextBox ID="txtAccess" runat="server" TextMode="MultiLine"></asp:TextBox>
    <br />
    <label>Partners:</label>
    Entry is coming from VOS
    <br />
      <br />
    <label style="display: none;">Partner Links:</label>
    <table style="display: none;">
        <tr><td><asp:TextBox ID="txtPartnerName1" runat="server" Width="150px" MaxLength="40"></asp:TextBox><asp:TextBox ID="txtPartnerLink1" Width="250px" runat="server" Text="http://"></asp:TextBox></td></tr>
        <tr><td><asp:TextBox ID="txtPartnerName2" runat="server" Width="150px" MaxLength="40"></asp:TextBox><asp:TextBox ID="txtPartnerLink2" Width="250px" runat="server" Text="http://"></asp:TextBox></td></tr>
        <tr><td><asp:TextBox ID="txtPartnerName3" runat="server" Width="150px" MaxLength="40"></asp:TextBox><asp:TextBox ID="txtPartnerLink3" Width="250px" runat="server" Text="http://"></asp:TextBox></td></tr>
        <tr><td><asp:TextBox ID="txtPartnerName4" runat="server" Width="150px" MaxLength="40"></asp:TextBox><asp:TextBox ID="txtPartnerLink4" Width="250px" runat="server" Text="http://"></asp:TextBox></td></tr>
        <tr><td><asp:TextBox ID="txtPartnerName5" runat="server" Width="150px" MaxLength="40"></asp:TextBox><asp:TextBox ID="txtPartnerLink5" Width="250px" runat="server" Text="http://"></asp:TextBox></td></tr>
        <tr><td><asp:TextBox ID="txtPartnerName6" runat="server" Width="150px" MaxLength="40"></asp:TextBox><asp:TextBox ID="txtPartnerLink6" Width="250px" runat="server" Text="http://"></asp:TextBox></td></tr>
        <tr><td><asp:TextBox ID="txtPartnerName7" runat="server" Width="150px" MaxLength="40"></asp:TextBox><asp:TextBox ID="txtPartnerLink7" Width="250px" runat="server" Text="http://"></asp:TextBox></td></tr>
        <tr><td><asp:TextBox ID="txtPartnerName8" runat="server" Width="150px" MaxLength="40"></asp:TextBox><asp:TextBox ID="txtPartnerLink8" Width="250px" runat="server" Text="http://"></asp:TextBox></td></tr>
        <tr><td><asp:TextBox ID="txtPartnerName9" runat="server" Width="150px" MaxLength="40"></asp:TextBox><asp:TextBox ID="txtPartnerLink9" Width="250px" runat="server" Text="http://"></asp:TextBox></td></tr>
        <tr><td><asp:TextBox ID="txtPartnerName10" runat="server" Width="150px" MaxLength="40"></asp:TextBox><asp:TextBox ID="txtPartnerLink10" Width="250px" runat="server" Text="http://"></asp:TextBox></td></tr>
    </table>
    <br />
</fieldset>

<fieldset>
    <legend>Opening hours</legend>
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td><asp:TextBox ID="txtDay1" runat="server"></asp:TextBox></td>
            <td><asp:TextBox ID="txtHours1" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td><asp:TextBox ID="txtDay2" runat="server"></asp:TextBox></td>
            <td><asp:TextBox ID="txtHours2" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td><asp:TextBox ID="txtDay3" runat="server"></asp:TextBox></td>
            <td><asp:TextBox ID="txtHours3" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td><asp:TextBox ID="txtDay4" runat="server"></asp:TextBox></td>
            <td><asp:TextBox ID="txtHours4" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td><asp:TextBox ID="txtDay5" runat="server"></asp:TextBox></td>
            <td><asp:TextBox ID="txtHours5" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td><asp:TextBox ID="txtDay6" runat="server"></asp:TextBox></td>
            <td><asp:TextBox ID="txtHours6" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td><asp:TextBox ID="txtDay7" runat="server"></asp:TextBox></td>
            <td><asp:TextBox ID="txtHours7" runat="server"></asp:TextBox></td>
        </tr>
    </table>
</fieldset>


<fieldset>
    <legend>Email's Correspondence</legend>
    <table border="0" cellpadding="1">
        <tr>
            <td><b>Enquiry form email address 1</b></td>
            <td><asp:TextBox Width="200px" runat="server" ID="tbStudioEmail1"></asp:TextBox></td>
            <td align="left"><asp:CheckBoxList TextAlign="Right"  RepeatDirection="Horizontal" ID="cblStudioContact1" runat="server">            
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td><b>Enquiry form email address 2</b></td>
            <td><asp:TextBox Width="200px" runat="server" ID="tbStudioEmail2"></asp:TextBox></td>
            <td><asp:CheckBoxList RepeatDirection="Horizontal" ID="cblStudioContact2" runat="server">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td><b>Enquiry form email address 3</b></td>
            <td><asp:TextBox Width="200px" runat="server" ID="tbStudioEmail3"></asp:TextBox></td>
            <td><asp:CheckBoxList RepeatDirection="Horizontal" ID="cblStudioContact3" runat="server">
                </asp:CheckBoxList>
            </td>
        </tr>
    </table>
</fieldset>
    

<fieldset>
    <legend>Studio Owner's Profile</legend>
    <asp:Literal ID="litStudioOwner" runat="server"></asp:Literal>
    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>

    <table border="0" cellpadding="1">
        <tr>
            <td>Name</td>
            <td>
                <asp:TextBox ID="txtFOName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Bio</td>
            <td>
                <asp:TextBox ID="txtFOBio" Rows="3" TextMode="MultiLine" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Email</td>
            <td>
                <asp:TextBox ID="txtFOEmail" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Photo</td>
            <td>
                <asp:FileUpload ID="fileUploadFOPhoto" runat="server" />
            </td>
        </tr>
    </table>
    <!-- Place this in the body of the page content -->
    <asp:Button ID="Button1" runat="server" Text="Add New Owner" OnClick="btnCreateWhatsOn_Click" />
    
</fieldset>

<asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" />




</asp:Panel>
<asp:Label ID="lblFeedback" runat="server" Text="" Visible="false"></asp:Label>