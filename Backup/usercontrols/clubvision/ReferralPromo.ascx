<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReferralPromo.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.clubvision.ReferralPromo" %>
<style type="text/css">
    .dropbox {
        width: 198px !important;
        margin-left: 2px;
    }
</style>
<script type="text/javascript">
    //Studio Picker Dropdown show and hide
    $(document).ready(function () {
        $('#studioPicker').hide(); //hidden on page load
        $("#refChoiceDropDownList").change(function () {//gets fired when #refChoiceDropDownList is changed
            if ($('#refChoiceDropDownList option:selected').val() == "1") {
                $('#studioPicker').fadeIn(500); //Studio Picker appears is "Studio Session" is selected
            }
            else
                $('#studioPicker').fadeOut(300); //hides the Studio Picker if any other is selected
        });
    });
    //when submit button clicked, this validation is called
    //if "Studio Session" is selected and studio selection is blank, returns false
    function StudioValidation(sender, args) {
        if ($('#refChoiceDropDownList').val() == "1" && $('#refStudioDropDownList').val() == "0") {
            args.IsValid = false;
            return;
        }
        args.IsValid = true;
        return;
    }

    function ValidateAndProcess() {

        var res = Page_ClientValidate("refbox"); //this calls asp validation control on client side
        if (res == true) {//if request passes all validation rules, javascript "referralPromo" in "vvt-scripts.js" is called
            referralPromo($('#reftxtName').val(),
                        $('#reftxtEmail').val(),
                        $('#refChoiceDropDownList').val(),
            //$('#refStudioDropDownList').val(),
                        $('#refStudioDropDownList option:selected').text(),
                        $('#reftxtMobile').val());
        }
    }

</script>
<div id="eMedia" class="element"><!-- 970 -->
    <!-- <h3 class="replace" style="background: url('/images/eHdrReferral.gif') no-repeat scroll 0 0 transparent !important;">In the Media</h3> -->
  <div class="tabHeading">
       <div style="width: 570px;text-align: left;padding-left: 10px;cursor: default;" class="divHeadingActive">
           Be an Influence on Helping Your Friends Transform Their Lives
       </div>
       <div style="width: 336px;" class="lastDivHeading"></div>  
  </div>
  <div class="clear"></div>
    <div class="eContent eClean" style="height: 380px !important;" >
        <div style="width: 253px;float: left;margin-top: 60px;">
            <asp:Image ID="Image1" runat="server" src="/images/referralimage.gif" />
        </div>
        
        <!-- This is where the dancing starts -->
        <div style="width: 600px;margin-left: 30px;float: left">
        <p>We have two options for you should you want to help motivate a friend to start their own health and fitness program.
         </p>   
         <p>   
            Simply choose to give either two complimentary Personal Training sessions at any of our studios
            or alternatively you can gift them their first month of Vision Virtual Training access for just $1.</p>
        
        <br/>
        
        <p>Please enter your friend's details below and we will contact them directly.</p>
        <br/>     
        <table>
            <tr>
                <td>Recipient Name</td>
                <td><asp:TextBox ID="reftxtName" runat="server" ClientIDMode="Static"></asp:TextBox></td>
                <td>
                    <asp:RequiredFieldValidator ValidationGroup="refbox" ID="refRequiredFieldValidator2" ControlToValidate="reftxtName" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Your Choice of Gift
                </td>
                <td>
                    <asp:DropDownList ID="refChoiceDropDownList" runat="server" ClientIDMode="Static" CssClass="dropbox">
                        <asp:ListItem runat="server" Value="0" Text=""/> 
                        <asp:ListItem runat="server" Value="1" Text="PT sessions"/>
                        <asp:ListItem runat="server" Value="2" Text="VVT access"/>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="refRequiredFieldValidator1" runat="server"
                                                ValidationGroup="refbox"
                                                ControlToValidate="refChoiceDropDownList"
                                                InitialValue="0" 
                                                ErrorMessage="*">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr id="studioPicker">
                <td>Recommended Studio</td>
                <td><asp:DropDownList ID="refStudioDropDownList" runat="server" ClientIDMode="Static" CssClass="dropbox">
                    </asp:DropDownList>
                </td>
                <td>
                <!-- added on 23/08/2013 Hiroshi-->
                    <!--this is to validate the Studio Picker dropdown box
                        the validation code is placed on top of this page
                        with function named "ValidateAndProcess()"-->
                <asp:CustomValidator id="StudioCustomValidator" runat="server" 
                                     ControlToValidate="refStudioDropDownList"
                                     ClientValidationFunction="StudioValidation"
                                     ValidationGroup="refbox"
                                     InitialValue="0"
                                     ErrorMessage="*"></asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td>Recipient Email Address</td>
                <td>
                    <asp:TextBox ID="reftxtEmail" runat="server" ClientIDMode="Static"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ValidationGroup="refbox" ID="refRequiredFieldValidator3" ControlToValidate="reftxtEmail" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="red" ControlToValidate="reftxtEmail" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" runat="server" 
                            ErrorMessage="*" Text="*" ValidationGroup="refbox"/>
                </td>
            </tr>
            <tr>
                <td>Confirm Recipient Email Address</td>
                <td>
                    <asp:TextBox ID="reftxtEmailConfirm" runat="server" ClientIDMode="Static"></asp:TextBox>
                </td>
                <td>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" 
                        ControlToValidate="reftxtEmailConfirm" ControlToCompare="reftxtEmail" ForeColor="red" ErrorMessage="*"
                        Text="*" ValidationGroup="refbox"></asp:CompareValidator>
                    <asp:RequiredFieldValidator ID="refRequiredFieldValidator4" ControlToValidate="reftxtEmailConfirm" ValidationGroup="refbox" runat="server" ErrorMessage="*" Text="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Recipient Mobile Number</td>
                <td>
                    <asp:TextBox ID="reftxtMobile" runat="server" ClientIDMode="Static" placeholder="optional"></asp:TextBox>
                </td>
                <td>
                    <asp:CompareValidator ValidationGroup="refbox" Operator="DataTypeCheck" ID="CompareValidator2" runat="server" ControlToValidate="reftxtMobile" Type="Integer" ErrorMessage="*"></asp:CompareValidator>
                    <%--<asp:RequiredFieldValidator ValidationGroup="refbox" ID="refRequiredFieldValidator5" ControlToValidate="reftxtMobile" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                --%></td>
            </tr>
            <tr>
                <td colspan="3">
                    <div style="float: right;margin-right:16px;text-align: right">
                    <!-- commented out as replaced with normal html button -->             
                        <%--<asp:ImageButton ValidationGroup="refbox"  ImageUrl="/images/buttonSendInvitationRed.gif"  
                            ID="ImageButton1" runat="server" CssClass="refbutton" OnClick="ImageButton1_Click" /><br/>--%>

                        <input id="Button1" type="image" value="test" src="/images/buttonSendInvitationRed.gif"
                               onclick="ValidateAndProcess();return false;" />
                        <br />
                       
                    </div>
                </td>
            </tr>   
        </table> 
        <br/>
        <p>Thank you so much for being a positive influence on your friends and helping transform their lives by introducing them to health and fitness</p>
          </div>
        <!-- This is where the dancing ends -->
    </div>  
    <div class="clear"></div>
</div>
