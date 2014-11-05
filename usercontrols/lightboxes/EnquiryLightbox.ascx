<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EnquiryLightbox.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.lightboxes.EnquiryLightbox" %>
<div class="contactOverlay" id="cEnquire">				
  <div class="contactBox">
  	<div class="cTop"></div>
  	<div class="cContent">
    	<a href="" target="_blank" class="replace cCross cEnquireClose" title="Close">Close</a>
    	<h2>Enquire Today</h2>
    	<h4>Enquire about a franchising opportunity</h4>
      	<div class="row">
      		<label>Name</label>
         	<asp:TextBox ID="tbEnqFirstName" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="valEnqFirstName" runat="server" ControlToValidate="tbEnqFirstName" ForeColor="Red" ErrorMessage="*" ValidationGroup="EnquiryLightBox"></asp:RequiredFieldValidator>
   		</div><!-- /row -->
      	<div class="row">
      		<label>Surname</label>
        	<asp:TextBox ID="tbEnqSurname" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="valEnqSurname" runat="server" ControlToValidate="tbEnqSurname" ForeColor="Red" ErrorMessage="*" ValidationGroup="EnquiryLightBox"></asp:RequiredFieldValidator>
    		</div><!-- /row -->
        <div class="row">
      		<label>Mobile</label>
        	<asp:TextBox ID="tbEnqMobile" runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="valEnqValidMobile" runat="server" ValidationExpression="[0-9]+" ForeColor="Red" Display="Dynamic" ControlToValidate="tbEnqMobile" ErrorMessage="*"  ValidationGroup="EnquiryLightBox"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="valEnqMobile" runat="server" ControlToValidate="tbEnqMobile" ForeColor="Red" ErrorMessage="*" ValidationGroup="EnquiryLightBox"></asp:RequiredFieldValidator>
    		</div><!-- /row -->
      	<div class="row">
      		<label>Email</label>
        	<asp:TextBox ID="tbEnqEmail" runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="valEnqEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ForeColor="Red" Display="Dynamic" ControlToValidate="tbEnqEmail" ErrorMessage="Invalid email" ValidationGroup="EnquiryLightBox"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="valEnqEmail" runat="server" ControlToValidate="tbEnqEmail" ForeColor="Red" ErrorMessage="*" ValidationGroup="EnquiryLightBox"></asp:RequiredFieldValidator>
    	</div><!-- /row -->
      	<div class="row">
      		<label>Address</label>
        	<asp:TextBox ID="tbEnqAddress" runat="server" class="inputLong"></asp:TextBox>
        	  	<div class="clear"></div>
    			<p class="note">Street address and number</p>
    		</div><!-- /row -->
      	<div class="row">
      		<label>State</label>
            <asp:DropDownList ID="ddlEnqState"  class="selectLong" runat="server">
                <asp:ListItem Value="" Text=""></asp:ListItem>
                <asp:ListItem Value="ACT" Text="ACT"></asp:ListItem>
                <asp:ListItem Value="NSW" Text="NSW"></asp:ListItem>    
                <asp:ListItem Value="NT" Text="NT"></asp:ListItem>    
                <asp:ListItem Value="QLD" Text="QLD"></asp:ListItem>
                <asp:ListItem Value="SA" Text="SA"></asp:ListItem>    
                <asp:ListItem Value="TAS" Text="TAS"></asp:ListItem>    
                <asp:ListItem Value="VIC" Text="VIC"></asp:ListItem>
                <asp:ListItem Value="WA" Text="WA"></asp:ListItem>                
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="valEnqState" runat="server" ControlToValidate="ddlEnqState" ForeColor="Red" ErrorMessage="*" ValidationGroup="EnquiryLightBox"></asp:RequiredFieldValidator>
    		</div><!-- /row -->
      	<div class="row">
      		<label>Postcode</label>
        	<asp:TextBox ID="tbEnqPostcode" runat="server"></asp:TextBox>
        	 <asp:RegularExpressionValidator ID="valEnqValidPostcode" runat="server" ValidationExpression="[0-9]+" ForeColor="Red" Display="Dynamic" ControlToValidate="tbEnqPostcode" ErrorMessage="*"  ValidationGroup="EnquiryLightBox"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="valEnqPostcode" runat="server" ControlToValidate="tbEnqPostcode" ForeColor="Red" ErrorMessage="*" ValidationGroup="EnquiryLightBox"></asp:RequiredFieldValidator>
    		</div><!-- /row -->
      	<div class="row">
      		<label>Telephone</label>
        	<asp:TextBox ID="tbEnqPhone" runat="server"></asp:TextBox>
        	<asp:RegularExpressionValidator ID="valEnqValidPhone" runat="server" ValidationExpression="[0-9]+" ForeColor="Red" Display="Dynamic" ControlToValidate="tbEnqPhone" ErrorMessage="*"  ValidationGroup="EnquiryLightBox"></asp:RegularExpressionValidator>
        		<div class="clear"></div>
    			<p class="note">Include area code</p>
    		</div><!-- /row -->
      	<div class="row">
      		<label>Comments /<br />Questions</label>
        	<asp:TextBox ID="tbEnqComments" runat="server" TextMode="MultiLine" rows="4" cols="35"></asp:TextBox>
    		</div><!-- /row -->
      	<div class="row">
  		  	<asp:LinkButton ID="btnSubmit" ValidationGroup="EnquiryLightBox" class="replace cSend" runat="server" OnClick="btnSubmit_Click">Send Enquiry</asp:LinkButton>
            
    		</div><!-- /row -->
  		<div class="clear"></div>
  	</div><!-- /cContent -->
  	<div class="cBase"></div>
  </div><!-- /contactBox  -->
</div><!-- /contactOverlay cEnquire -->