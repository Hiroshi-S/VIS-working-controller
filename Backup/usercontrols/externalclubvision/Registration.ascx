<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Registration.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.externalclubvision.Registration" %>
<%@ Register TagPrefix="recaptcha" Namespace="Recaptcha.Web.UI.Controls" Assembly="Recaptcha.Web" %>

<style>
.article
{
    color: #666;
    letter-spacing: 1px;
    font-size:12px;
}
.tds
{
    height:100px;
    padding-left:10px;
    padding-right:10px;
}
.tds img
{
    width:100px;
    margin-right:5px;
    border-radius:5px;
    border:1px solid #aaa;
}
    
</style>

<script type="text/javascript">
    function reloadRecaptcha() {
        Recaptcha._init_options(RecaptchaOptions);
        if (RecaptchaOptions && 'custom' == RecaptchaOptions.theme) {
            if (RecaptchaOptions.custom_theme_widget) {
                Recaptcha.widget = Recaptcha.$(RecaptchaOptions.custom_theme_widget);
                Recaptcha.challenge_callback();
            }
        } else {
            if (Recaptcha.widget == null || !document.getElementById('recaptcha_widget_div')) {
                $('#pbTarget').show().html('<div id="recaptcha_widget_div" style="display:none"></div>');
                Recaptcha.widget = Recaptcha.$('recaptcha_widget_div');
            }
            Recaptcha.reload();
            Recaptcha.challenge_callback();
        }
    }
</script>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<div style="float: left; width: 450px;" >
    <asp:UpdatePanel ID="upMessage" runat="Server" UpdateMode="Conditional">
        <ContentTemplate>
        <div class="regoform" style="display: block;" runat="server" id="regodiv">
        <asp:Label ID="validatorlabel" CssClass="validation" runat="server" Text="" ForeColor="red" Height=""></asp:Label><br/><br/>
    
        <span style="display: inline"><asp:Image ID="Image1" runat="server" src="/images/ExtClubVision/icons/regStep1.gif" />
        <h3>About you</h3>
        <br/>
        </span>

        <asp:TextBox runat="server" ID="txtFirstName" placeholder="First Name"></asp:TextBox> 
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
            ControlToValidate="txtFirstName" runat="server" ForeColor="red" 
            ErrorMessage="First name"  Text="*" ValidationGroup="A"></asp:RequiredFieldValidator>

        <br/>
        <asp:TextBox runat="server" ID="txtSurname" placeholder="Last Name"></asp:TextBox> 
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" 
            ControlToValidate="txtSurname" runat="server" ForeColor="red" 
            ErrorMessage="Last name" Text="*" ValidationGroup="A"></asp:RequiredFieldValidator>

        <br/>
    
        <asp:TextBox runat="server" ID="txtEmail"  placeholder="Email" AutoPostBack="true" OnTextChanged="txtEmail_Changed"></asp:TextBox> 
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ForeColor="red"  
            ControlToValidate="txtEmail" runat="server" ValidationGroup="A" 
            ErrorMessage="Email" Text="*"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
            ForeColor="red" ControlToValidate="txtEmail" 
            ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" runat="server" 
            ErrorMessage="Email is not in correct format" Text="*" ValidationGroup="A"/>
            
        <asp:Label ID="lblForEmail" Visible="False" runat="server" Text="" ForeColor="red" Font-Bold="True" ></asp:Label>
                  
        <br/>
        <asp:TextBox runat="server" ID="txtremail" placeholder="Confirm Email" ></asp:TextBox> 
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ForeColor="red"  
            runat="server" ControlToValidate="txtEmail" ValidationGroup="A" 
            ErrorMessage="Email" Text="*"></asp:RequiredFieldValidator>
        <asp:CompareValidator ID="CompareValidator1" runat="server" 
            ControlToValidate="txtremail" ControlToCompare="txtEmail" ForeColor="red" 
            ErrorMessage="Email is not in correct format" Text="*" ValidationGroup="A"></asp:CompareValidator>
        <br/>

        <asp:TextBox ID="txtVoucher"  AutoPostBack="true" placeholder="Voucher code. Do you have one?" runat="server" OnTextChanged="txtVoucher_Changed"></asp:TextBox>
        <asp:Label ID="LabeltxtVoucher" Visible="True" runat="server" Text="" ForeColor="green" Font-Bold="True"></asp:Label>
        <br/>

        <asp:DropDownList ID="DropDownList1" runat="server" Width="347" 
            AutoPostBack="True" TabIndex="-1" 
            onselectedindexchanged="DropDownList1_SelectedIndexChanged">
            <asp:ListItem Value="0">How did you hear about us?</asp:ListItem>
            <asp:ListItem Value="1">Google</asp:ListItem>
            <asp:ListItem Value="2">Web Search</asp:ListItem>
            <asp:ListItem Value="3">Body and Soul Award Voucher</asp:ListItem>
            <asp:ListItem Value="4">Friend</asp:ListItem>
            <asp:ListItem Value="5">Youtube</asp:ListItem>
            <asp:ListItem Value="6">Paper</asp:ListItem>
            <asp:ListItem Value="7">Magazine</asp:ListItem>
            <asp:ListItem Value="8">Business Plaza</asp:ListItem>
            <asp:ListItem Value="9">Email Promotion</asp:ListItem>
            <asp:ListItem Value="10">Previous Vision Client</asp:ListItem> 
            <asp:ListItem Value="11">Other</asp:ListItem>        
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ForeColor="red"  
            ValidationGroup="A" ControlToValidate="DropDownList1" InitialValue="0"  
            runat="server" ErrorMessage="Source is required" Text="*"></asp:RequiredFieldValidator>
        <br/>  
        <div> 
            
        <asp:TextBox ID="txtOther" Visible="False" placeholder="Please specify other" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ForeColor="red"  
            ValidationGroup="A" ControlToValidate="txtOther"  Enabled="False"
            runat="server" ErrorMessage="Source is required" Text="*"></asp:RequiredFieldValidator>
                
        </div>
        <br/>
        <br/>


        <span style="display: inline">
        <asp:Image ID="Image2" runat="server" src="/images/ExtClubVision/icons/regStep2.gif" />
        <h3>Register now with PayPal</h3>
        <br/>
        </span>
        <asp:CheckBox ID="CheckBox3" runat="server" />&nbsp;&nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Font-Size="12" >I agree to the <a target="_blank" style="color:#002147;" href="/club-vision/TERMS_AND_CONDITIONS.pdf">terms and conditions</a></asp:Label>
                 <br /><br />
        <asp:CheckBox ID="CheckBox4" runat="server" />&nbsp;&nbsp;&nbsp;<asp:Label ID="Label2" runat="server"  Font-Size="12" >I am at least 16 years of age</asp:Label>
                <br /><br />
    
        <br/>
        <br/>
   
   
        <asp:label runat="server" ID="LabelValidation" ForeColor="red" Visible="False" text="Please ensure all sections are filled correctly"></asp:label>
    
        <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="A" HeaderText="Please check the following points" EnableClientScript="True" DisplayMode="BulletList" runat="server" />
    


    </div>

    <div class="regoform" runat="server" style="display: none;"  id="usernamediv">
        <br/><br/>
     <asp:Label ID="Label3" runat="server" Font-Size="12">Normally we would like to allocate your FirstNameSurname as your username.
     This already exists please choose your preferred username.</asp:Label>
        <br/><br/>
        <asp:TextBox ID="txtUserName"  runat="server"></asp:TextBox>
        <br/><br/>
        <asp:ImageButton ID="ImageButton2"  runat="server" 
            ImageUrl="/images/joinnow.gif" 
            onclick="ImageButton2Click"/>

    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    
    
    <div runat="server" id="pbTarget" ClientIDMode="Static">
        <recaptcha:Recaptcha ID="recaptcha" runat="server" Theme="White" Width="347"/>
    
        <div style="margin-left: -5px; display: block;">
                <asp:ImageButton ID="ImageButton1"  runat="server" Width="360px"
                    ImageUrl="/images/joinnow.gif"
                    onclick="ImageButton1Click" ValidationGroup="A" />    
        </div>
    </div>  
</div>

<div id="whyjoinimg" style="float: left; width: 445px;background-color: #F1F1F1;margin-top: 46px;">
    
        
    <h3 style="color: #C60C30; font-size: 20px;font-weight: normal">With no lengthy contracts and instant access why not get started today?</h3>
    
    <div id="demo" style="width:445px;margin: 0px auto;" >
        <div class="box card"  style="margin: 0px auto;">
            <table>
                <tr>
                    <td class="tds">
                        <img src="/images/ExtClubVision/weightlosssecret.jpg" width="100px" height="65px"> 
                    </td>
                    <td>
                        <h3 style="color: #666; letter-spacing: 1px;font-style: italic;">Easy to use food diary with extensive database of foods plus weight loss secrets from industry experts</h3>
                    </td>
                </tr>
                <tr>
                    <td class="tds">
                        <img src="/images/ExtClubVision/tastyrecipeidea.jpg" width="100px" height="65px"> 
                    </td>
                    <td>
                        <h3 style="color: #666; letter-spacing: 1px;font-style: italic;">Tasty recipe ideas & easy to follow cooking demonstrations</h3>
                    </td>
                </tr>
                <tr>
                    <td class="tds">
                        <img src="/images/ExtClubVision/exercisetracking.jpg" width="100px" height="65px">
                    </td>
                    <td>
                        <h3 style="color: #666; letter-spacing: 1px;font-style: italic;">Exercise tracking, tips & videos</h3>
                    </td>
                </tr>
                <tr>
                    <td class="tds">
                        <img src="/images/ExtClubVision/Fotolia_38128489_XL.jpg" width="100px" height="65px">
                    </td>
                    <td>
                        <h3 style="color: #666; letter-spacing: 1px;font-style: italic;">Informative articles and education library</h3>
                    </td>
                </tr>
            
            </table>
      </div>
      
    </div>
    <!-- /demo -->
    
</div>