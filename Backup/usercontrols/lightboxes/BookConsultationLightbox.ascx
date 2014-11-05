<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BookConsultationLightbox.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.lightboxes.BookConsultationLightbox" %>
<style type="text/css">
    .black_overlay{
      display: none;
      position: fixed;
      top: 0%;
      left: 0%;
      width: 100%;
      height: 100%;
      background-color:rgba(0,0,0,0.8);
      z-index:1050;
    }
    .white_content_temp {
        top: 18%;
        z-index:1051;
    }    
</style>

<!--<div class="contactOverlay" id="cConsultation">          
  <div class="contactBox">-->
<div class="black_overlay" id="cConsultation">               
  <div class="contactBox white_content_temp">

  	<div class="cTop"></div>
  	<div class="cContent">
    	<a href="" target="_blank" class="replace cCross cConsultationClose" title="Close">Close</a>
    	<h2>Book a Consultation</h2>
    	<h4>Book a free consultation with a Vision Personal Trainer</h4>
   
      	<div class="row">
      		<label>First Name</label>
        	<asp:TextBox ID="tbFirstName" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="valFirstName" runat="server" ControlToValidate="tbFirstName" ForeColor="Red" ErrorMessage="*"  ValidationGroup="BookConsultation"></asp:RequiredFieldValidator>
        </div><!-- /row -->
      	<div class="row">
      		<label>Surname</label>
        	<asp:TextBox ID="tbSurname" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="valSurname" runat="server" ControlToValidate="tbSurname" ForeColor="Red" ErrorMessage="*"  ValidationGroup="BookConsultation"></asp:RequiredFieldValidator>
    		</div><!-- /row -->
      	<div class="row">
      		<label>Mobile</label>
        	<asp:TextBox ID="tbMobile" runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="valMobileValid" runat="server"  ValidationExpression="^\d+$" ForeColor="Red" Display="Dynamic" ErrorMessage="* Numbers only"  ValidationGroup="BookConsultation" ControlToValidate="tbMobile" ></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="valMobile" runat="server" ControlToValidate="tbMobile" ForeColor="Red" ErrorMessage="*"  ValidationGroup="BookConsultation"></asp:RequiredFieldValidator>
   		</div><!-- /row -->
      	<div class="row">
      		<label>Email</label>
        	<asp:TextBox ID="tbEmail" runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="valEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ForeColor="Red" Display="Dynamic" ControlToValidate="tbEmail" ErrorMessage="*"  ValidationGroup="BookConsultation"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="valEmail" runat="server" ControlToValidate="tbEmail" ForeColor="Red" ErrorMessage="*"  ValidationGroup="BookConsultation"></asp:RequiredFieldValidator>
    		</div><!-- /row -->
      	<div class="row">
      		<label>Choose Studio</label>
            <asp:DropDownList ID="ddlStudio"  class="selectLong" runat="server">
             </asp:DropDownList>
            <asp:RequiredFieldValidator ID="valStudio" runat="server" ControlToValidate="ddlStudio" ForeColor="Red" ErrorMessage="*" InitialValue="0" ValidationGroup="BookConsultation"></asp:RequiredFieldValidator>
    	</div><!-- /row -->
      	<div class="row">
      		<label>Comments /<br />Questions</label>
        	<asp:TextBox ID="tbComments" runat="server" TextMode="MultiLine" rows="4" cols="35"></asp:TextBox>
            <asp:RequiredFieldValidator ID="valComments" runat="server" ControlToValidate="tbComments" ForeColor="Red" ErrorMessage="*" Enabled="False"  ValidationGroup="BookConsultation"></asp:RequiredFieldValidator>
    		</div><!-- /row -->
      	<div class="row">
  		  	<asp:LinkButton ID="btnBook" ValidationGroup="BookConsultation" 
            class="replace cBook" runat="server" OnClick="btnBook_Click" 
            OnClientClick="_gaq.push(['_trackEvent', 'Consultation', 'Submit', document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_BookConsultationLightbox_12_ddlStudio').value]);">Book consultation</asp:LinkButton>
        </div><!-- /row -->
        <div class="clear"></div>
  	</div><!-- /cContent -->
  	<div class="cBase"></div>
  </div><!-- /contactBox -->
</div><!-- /contactOverlay cConsultation -->
<asp:Literal ID="contactOverlayResult" runat="server"></asp:Literal>