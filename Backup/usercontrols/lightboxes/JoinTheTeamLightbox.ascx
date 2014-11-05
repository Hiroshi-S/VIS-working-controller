<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JoinTheTeamLightbox.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.lightboxes.JoinTheTeamLightbox" %>
<div class="contactOverlay" id="cEnquire">				
  <div class="contactBox">
  	<div class="cTop"></div>
  	<div class="cContent" style="overflow: scroll !important; height: 800px !important;overflow-x: hidden;">
    	<a href="" target="_blank" class="replace cCross cEnquireClose" title="Close">Close</a>
    	<h2>Enquire Today</h2>
    	<h4>Enquire about Joining the Vision Personal Training team.</h4>
      	<div class="row">
      		<label>First Name</label>
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
            <asp:RequiredFieldValidator ID="valEnqMobile" runat="server" ControlToValidate="tbEnqMobile" ForeColor="Red" ErrorMessage="*" ValidationGroup="EnquiryLightBox"></asp:RequiredFieldValidator>
    		</div><!-- /row -->
      	<div class="row">
      		<label>Telephone</label>
        	<asp:TextBox ID="tbEnqPhone" runat="server"></asp:TextBox>
        		<div class="clear"></div>
    			<p class="note">Include area code</p>
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
                <asp:ListItem Value="TAS" Text="TAS"></asp:ListItem>    
                <asp:ListItem Value="VIC" Text="VIC"></asp:ListItem>
                <asp:ListItem Value="WA" Text="WA"></asp:ListItem>   
                <asp:ListItem Value="SA" Text="SA"></asp:ListItem>             
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
      		<label>Do you have a Certificate IV in Fitness?</label>
            <asp:RadioButtonList ID="tbEnqCertIV" runat="server">
                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                <asp:ListItem Text="No" Value="No" Selected="True"></asp:ListItem>
            </asp:RadioButtonList>
    		</div><!-- /row -->
      	<div class="row">
      		<label>Are you a certified boxing instructor?</label>
            <asp:RadioButtonList ID="tbEnqBoxing" runat="server">
                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                <asp:ListItem Text="No" Value="No" Selected="True"></asp:ListItem>
            </asp:RadioButtonList>
    		</div><!-- /row -->
      	<div class="row">
      		<label>What other qualifications do you have?</label>
        	<asp:TextBox ID="tbEnqQualifications" runat="server" TextMode="MultiLine" rows="4" cols="35"></asp:TextBox>
    		</div><!-- /row -->
      	<div class="row">
      		<label>Where did you do your studies?</label>
        	<asp:TextBox ID="tbEnqQualificationsWhere" runat="server" TextMode="MultiLine" rows="4" cols="35"></asp:TextBox>
    		</div><!-- /row -->
      	<div class="row">
      		<label>How did you hear about our positions available?</label>
        	<asp:TextBox ID="tbEnqHowDidYouHear" runat="server" TextMode="MultiLine" rows="4" cols="35"></asp:TextBox>
    		</div><!-- /row -->
      	<div class="row">
      		<label>Studio(s) you are seeking employment at?</label>
            <asp:ListBox Height="64px" Width="306px" ID="ddlStudio"  runat="server" SelectionMode="Multiple" class="selectStudio">
            </asp:ListBox>
    		</div><!-- /row -->
      	<div class="row">
      		<label>When would you be available to start?</label>
        	<asp:TextBox ID="tbEnqAvailableToStart" runat="server"></asp:TextBox>
    		</div><!-- /row -->
      	<div class="row">
      		<label>What do you know about Vision Personal Training?</label>
        	<asp:TextBox ID="tbEnqVisionPT" runat="server" TextMode="MultiLine" rows="4" cols="35"></asp:TextBox>
    		</div><!-- /row -->
      	<div class="row">
      		<label>Why do you what to be a Personal Trainer?</label>
        	<asp:TextBox ID="tbEnqWhyPersonalTrainer" runat="server" TextMode="MultiLine" rows="4" cols="35"></asp:TextBox>
    		</div><!-- /row -->
      	<div class="row">
      		<label>Why do you wish to join Vision rather than anywhere else?</label>
        	<asp:TextBox ID="tbEnqWhyVisionPT" runat="server" TextMode="MultiLine" rows="4" cols="35"></asp:TextBox>
    		</div><!-- /row -->
      	<div class="row">
      		<label>What are your Goals for the next year?</label>
        	<asp:TextBox ID="tbEnqGoalsNextYear" runat="server" TextMode="MultiLine" rows="4" cols="35"></asp:TextBox>
    		</div><!-- /row -->
      	<div class="row">
      		<label>What are your Goals for the next 3 years?</label>
        	<asp:TextBox ID="tbEnqGoalsNext3Years" runat="server" TextMode="MultiLine" rows="4" cols="35"></asp:TextBox>
    		</div><!-- /row -->
      	<div class="row">
      		<label>How do you feel about early morning starts and late night finishes?</label>
        	<asp:TextBox ID="tbEnqEarlyStartsLateFinishes" runat="server" TextMode="MultiLine" rows="4" cols="35"></asp:TextBox>
    		</div><!-- /row -->
      	<div class="row">
      		<label>How do you keep yourself fit and healthy?</label>
        	<asp:TextBox ID="tbEnqFit" runat="server" TextMode="MultiLine" rows="4" cols="35"></asp:TextBox>
    		</div><!-- /row -->
      	<div class="row">
      		<label>What are your hours of availabilty?</label>
        	<asp:TextBox ID="tbEnqHoursAvailable" runat="server" TextMode="MultiLine" rows="4" cols="35"></asp:TextBox>
    		</div><!-- /row -->
      	<div class="row">
      		<label>What commitments do you have outside of work?</label>
        	<asp:TextBox ID="tbEnqOtherCommitments" runat="server" TextMode="MultiLine" rows="4" cols="35"></asp:TextBox>
    		</div><!-- /row -->
      	<div class="row">
      		<label>What can you offer our team?</label>
        	<asp:TextBox ID="tbEnqWhatDoYouOffer" runat="server" TextMode="MultiLine" rows="4" cols="35"></asp:TextBox>
    		</div><!-- /row -->
      	<div class="row">
      		<label>Attach Your Resume (2MB Max):</label>
        	<asp:FileUpload ID="fileUploadResume" runat="server"></asp:FileUpload>
            <asp:RequiredFieldValidator ID="valFileUploadResume" runat="server" ControlToValidate="fileUploadResume" ForeColor="Red" ErrorMessage="*" ValidationGroup="EnquiryLightBox"></asp:RequiredFieldValidator>
    		</div><!-- /row -->
      	<div class="row">
  		  	<asp:LinkButton ID="btnSubmit" ValidationGroup="EnquiryLightBox" class="replace cSend" runat="server" OnClick="btnSubmit_Click">Send Enquiry</asp:LinkButton>
           <br/><br/><br/><br/>
    		</div><!-- /row -->
  		<div class="clear"></div>
  	</div><!-- /cContent -->
  	<div class="cBase"></div>
  </div><!-- /contactBox  -->
</div><!-- /contactOverlay cEnquire -->