<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LandingPageInfoLightbox.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.lightboxes.LandingPageInfoLightbox" %>

<div class="contactOverlay" id="cErrorPopup" >				
  <div class="contactBox">
  	<div class="cTop"></div>
  	<div class="cContent">
    	<a href="" target="_blank" class="replace cCross cErrorPopupClose" title="Close">Close</a>
    	<h2 id="title">Not Already a Member? Please Enter Your Details For Personal Training Package Information</h2>
    	<h4 id="h4words"></h4>
      	
        <div id="normaldiv" style="border-right: 1px dotted #cccccc;width: 300px;float: left;">
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
                <asp:RequiredFieldValidator ID="valStudio" runat="server" ControlToValidate="ddlStudio" ForeColor="Red" ErrorMessage="*"  ValidationGroup="BookConsultation"></asp:RequiredFieldValidator>
    	    </div><!-- /row -->
      	    <div class="row">
  		  	    <asp:LinkButton ID="btnBook" ValidationGroup="BookConsultation" 
                class="replace cBook" runat="server" OnClick="btnBook_Click" 
                OnClientClick="_gaq.push(['_trackEvent', 'Consultation', 'Submit', document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_BookConsultationLightbox_12_ddlStudio').value]);">Book consultation</asp:LinkButton>
            </div><!-- /row -->
        </div>
        <div id="fbdiv" style="border-right: 1px dotted #cccccc;width: 300px;float: left;display:none;">
            <fb:registration redirect_uri="http://www.visionpt.com.au/" 
                    width="500px"
                    border_color="#ffffff"
                     fields="[
                     {'name':'name'},
                     {'name':'first_name'},
                     {'name':'last_name'},
                     {'name':'mobile','description':'Mobile', 'type':'text'},
                     {'name':'email'},
                     {'name':'studio', 'description':'Choose Studio',  'type':'select',    'options':{'36':'Balgowlah','21':'Bangor','39':'Baulkham Hills','38':'Blakehurst','3':'Bondi Junction','37':'Brighton','5':'Brookvale','40':'Bundall','31':'Burleigh Heads','1':'Caringbah','45':'Castle Hill','12':'Clarence Street','9':'Crows Nest','10':'Darlinghurst','14':'Double Bay','8':'Drummoyne','49':'Engadine','27':'Five Dock','47':'Frenchs Forest','42':'Gladesville','35':'Gymea','29':'Hamilton','48':'Hawthorn','7':'Kogarah','43':'Lane Cove','23':'Lindfield','30':'Mermaid Beach','16':'Mona Vale','4':'Mosman','19':'Neutral Bay','11':'North Ryde','28':'North Sydney','6':'OConnell Street','24':'Parramatta','46':'Prahran','26':'Pyrmont','2':'Randwick','33':'Rose Bay','18':'Runaway Bay','44':'Southport','17':'St Ives','20':'Stanmore','25':'Surry Hills','34':'Sylvania','50':'Takapuna','32':'Taringa','41':'Wahroonga','13':'Willoughby','15':'Wollongong'}, 'default':'3'},
                        ]" /> 
        </div>
        <div id="submitdiv" style="float: left;width: 220px; height: 189px; display: block">
            <div style="width: 100px; display: block; margin: 0 auto;cursor: pointer" id="facebookDiv" onclick="$('#normaldiv').hide();$('#fbdiv').show();">
            <h3>Submit using </h3>
            <img src="/images/facebooklogo.png" width="100"/>
            </div>
        </div>

  		<div class="clear"></div>
  	</div><!-- /cContent -->
  	<div class="cBase"></div>
  </div><!-- /contactBox  -->
</div><!-- /contactOverlay cEnquire -->
<asp:Literal ID="contactOverlayResult" runat="server"></asp:Literal>