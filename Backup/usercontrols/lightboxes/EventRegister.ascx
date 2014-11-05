<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EventRegister.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.lightboxes.EventRegister" %>

<div id="cConsultation">		
  <div class="contactBox">
  	<div class="cTop"></div>
  	<div class="cContent">
    	<asp:Literal ID="literalBack" runat="server"></asp:Literal>
    	<h2>Register For Event</h2>
    	<h4><asp:Label ID="lblHeader" runat="server"></asp:Label> </h4>
   
      	<div class="row">
      		<label>First Name</label>
        	<asp:TextBox ID="tbFirstName" CausesValidation="true" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="valFirstName" runat="server" ControlToValidate="tbFirstName" ForeColor="Red" ErrorMessage="*"  ValidationGroup="BookConsultation"></asp:RequiredFieldValidator>
        </div><!-- /row -->
      	<div class="row">
      		<label>Surname</label>
        	<asp:TextBox ID="tbSurname" CausesValidation="true" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="valSurname" runat="server" ControlToValidate="tbSurname" ForeColor="Red" ErrorMessage="*"  ValidationGroup="BookConsultation"></asp:RequiredFieldValidator>
    		</div><!-- /row -->
      	<div class="row">
      		<label>Mobile</label>
        	<asp:TextBox ID="tbMobile" CausesValidation="true" runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="valValidNumber" runat="server" ValidationExpression="[0-9]+" ForeColor="Red" Display="Dynamic" ControlToValidate="tbMobile" ErrorMessage="*"  ValidationGroup="BookConsultation"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="valMobile" runat="server" ControlToValidate="tbMobile" ForeColor="Red" ErrorMessage="*"  ValidationGroup="BookConsultation"></asp:RequiredFieldValidator>
   		</div><!-- /row -->
      	<div class="row">
      		<label>Email</label>
        	<asp:TextBox ID="tbEmail" runat="server" CausesValidation="true"></asp:TextBox>
            <asp:RegularExpressionValidator ID="valEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ForeColor="Red" Display="Dynamic" ControlToValidate="tbEmail" ErrorMessage="*"  ValidationGroup="BookConsultation"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="valEmail" runat="server" ControlToValidate="tbEmail" ForeColor="Red" ErrorMessage="*"  ValidationGroup="BookConsultation"></asp:RequiredFieldValidator>
    		</div><!-- /row -->
      	<div class="row">
      		<label>Comments /<br />Questions</label>
        	<asp:TextBox ID="tbComments" runat="server" TextMode="MultiLine" rows="4" cols="35"></asp:TextBox>
    		</div><!-- /row -->
      	<div class="row">
  		  	<asp:Button ID="btnBook" runat="server" OnClick="btnBook_Click" Text="Register"/>
        </div><!-- /row -->
        <div class="row">
  		        <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                
        </div><!-- /row -->
        
        <div class="clear"></div>
  	</div><!-- /cContent -->
  	<div class="cBase"></div>
  </div><!-- /contactBox -->
</div><!-- /contactOverlay cConsultation -->