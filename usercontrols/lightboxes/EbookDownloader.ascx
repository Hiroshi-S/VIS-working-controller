<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EbookDownloader.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.lightboxes.EbookDownloader" %>
<div class="contactOverlay" id="cEnquire">				
  <div class="contactBox">
  	<div class="cTop"></div>
  	<div class="cContent">
    	<a href="" target="_blank" class="replace cCross cEnquireClose" title="Close">Close</a>
    	<h2>Not a ClubVision or Vision Client?</h2>
    	<p>Sign up now to receive 2 free chapters of Ready Set Go plus be notified of our quarterly newsletter and other special offers</p>
        <br/>
      	<div class="row">
      		<label>Name</label>
         	<asp:TextBox ID="tbEnqFirstName" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="valEnqFirstName" runat="server" ControlToValidate="tbEnqFirstName" ForeColor="Red" ErrorMessage="*" ValidationGroup="EnquiryLightBox"></asp:RequiredFieldValidator>
   		</div><!-- /row -->
      	<div class="row">
      		<label>Email</label>
        	<asp:TextBox ID="tbEnqEmail" runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="valEnqEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ForeColor="Red" Display="Dynamic" ControlToValidate="tbEnqEmail" ErrorMessage="Invalid email" ValidationGroup="EnquiryLightBox"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="valEnqEmail" runat="server" ControlToValidate="tbEnqEmail" ForeColor="Red" ErrorMessage="*" ValidationGroup="EnquiryLightBox"></asp:RequiredFieldValidator>
    	</div><!-- /row -->
      	<div class="row">
  		  	<asp:LinkButton ID="btnSubmit" ValidationGroup="EnquiryLightBox" class="replace cSend" runat="server" OnClick="btnSubmit_Click">Send Enquiry</asp:LinkButton>
    		</div><!-- /row -->
            <br/> <br/> <br/>
        <p>Current ClubVision or Vision Personal Training Clients please access this through your <a href="/club-vision/">ClubVision log in.</a></p>
  		<div class="clear"></div>
  	</div><!-- /cContent -->
  	<div class="cBase"></div>
  </div><!-- /contactBox  -->
</div><!-- /contactOverlay cEnquire -->