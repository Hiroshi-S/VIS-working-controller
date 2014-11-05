<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BusinessPlaza.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.externalclubvision.BusinessPlaza" %>
<style type="text/css">
.image
{
    width:100px;
    margin-right:5px;
}
.article
{
    color: #666;
    letter-spacing: 1px;
    font-size:12px;
}
</style>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<h2 style="color: #c60c30 !important;">Help yourself while you help your club</h2>
<div style="float: left; width: 380px;" >

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
            
        <asp:Label ID="lblForEmail" Visible="False" runat="server" Text="" ForeColor="red" ></asp:Label>
                  
        <br/>
        <asp:TextBox runat="server" ID="txtremail" placeholder="Confirm Email" ></asp:TextBox> 
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ForeColor="red"  
            runat="server" ControlToValidate="txtEmail" ValidationGroup="A" 
            ErrorMessage="Email" Text="*"></asp:RequiredFieldValidator>
        <asp:CompareValidator ID="CompareValidator1" runat="server" 
            ControlToValidate="txtremail" ControlToCompare="txtEmail" ForeColor="red" 
            ErrorMessage="Email is not in correct format" Text="*" ValidationGroup="A"></asp:CompareValidator>
        <br/>
        <!-- START club name for business plaza -->
        <asp:TextBox runat="server" ID="txtClubName" placeholder="Club Name" ></asp:TextBox> 
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ForeColor="red"  
            ValidationGroup="A" ControlToValidate="txtClubName"   
            runat="server" ErrorMessage="Club name is required" Text="*"></asp:RequiredFieldValidator>
        <br/>  
        <!-- END club name for business plaza -->
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
        <asp:CheckBox ID="CheckBox3" runat="server" />&nbsp;&nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Font-Size="12" >I agree to the <a target="_blank" href="/club-vision/TERMS_AND_CONDITIONS.pdf">terms and conditions</a></asp:Label>
                 <br /><br />
        <asp:CheckBox ID="CheckBox4" runat="server" />&nbsp;&nbsp;&nbsp;<asp:Label ID="Label2" runat="server"  Font-Size="12" >I am at least 16 years of age</asp:Label>
                <br /><br />
    
        <br/>
        <br/>
    

        <asp:TextBox ID="txtVoucher" placeholder="Voucher code. Do you have one?" runat="server" Visible="False"></asp:TextBox><br/>
        <div style="margin-left: -5px; display: block;">
            <asp:ImageButton ID="ImageButton1"  runat="server" Width="360px"
                ImageUrl="/images/joinnow.gif"
                onclick="ImageButton1Click" ValidationGroup="A" />    
        </div>
      

   
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
</div>

<div id="whyjoinimg" style="float: left; width: 510px;margin-top: 42px;">       
    <h3 style="color: #c60c30; font-size: 20px;font-weight: normal">With no lengthy contracts and instant access why not get started today?</h3>
    
    <img src="/images/businessplaza/vvtbusinessblaza.jpg" width="350px" style="margin-left: 86px;"/>
    <div id="demo" style="width:510px;margin: 0px auto;display: none;" >
        <div class="box card"  style="margin: 0px auto;">
            <table>
                <tr>
                    <td>
                        <img class="image" src="/images/BusinessPlaza/plaza1.png" width="100px" height="65px"> 
                    </td>
                    <td>
                        <p class="article">
                         Want to lose weight? Gain strength? Train for a sport? Learn how to eat well? Business Plaza now have a partner to help you learn about nutrition, track your food, and discover exercise routines delivered to you by some of Australia's leading fitness experts. Thousands of Australians have lost weight and improved their fitness using these resources... Now it's your turn!
                        </p>
                    </td>
                </tr>
                <tr>
                    <td>
                        <img class="image" src="/images/BusinessPlaza/plaza2.png" width="80px" height="65px"> 
                    </td>
                    <td>
                        <p class="article">
                         Access the Vision Virtual Training online nutrition and exercise site this month and you will be able to utilise all of the goal planning, resource tips, recipes, nutrition accountability and exercise advise you need for <span style="color:Red;font-weight:bold;font-size:16px">ONLY $1</span>
                        </p>
                    </td>
                </tr>
                <tr>
                    <td>
                        <img class="image" src="/images/BusinessPlaza/plaza3.png" width="100px" height="65px">
                    </td>
                    <td>
                        <p class="article">
                         If you decide to stay for the second month the entire $29 normally charged to access the site will be refunded to Business Plaza who will distribute it to your club as part of their ongoing charity program
                        </p>
                    </td>
                </tr>
                <tr >
                    <td>
                        <img class="image" src="/images/BusinessPlaza/plaza4.png" width="100px" height="65px">
                    </td>
                    <td>
                        <p class="article">
                         Get started now by logging onto
                         <br />
                         <a style="color:Red; font-weight:bold;" href="http://www.visionvirtualtraining.com.au/businessplaza">visionvirtualtraining.com.au/businessplaza</a>
                         <br />
                         remembering to enter your club name and access some amazing resources to help you begin or continue your health and fitness journey
                        </p>
                    </td>
                </tr>
            
            </table>
      </div>
      
    </div>
    <!-- /demo -->
    
</div>