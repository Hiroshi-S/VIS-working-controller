<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BookShoppingTourOrSeminarLightbox.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.lightboxes.BookShoppingTourOrSeminarLightbox" %>

<div class="contactOverlay" id="cBookingPopup" >				
  <div class="contactBox">
  	<div class="cTop"></div>
        <div class="cContent">
    	    <a href="" target="_blank" class="replace cCross cBookingPopupClose" title="Close">Close</a>
    	    <h2>Book Your Spot Today</h2>
    	    <h4>Book a complimentary Shopping Tour and Seminar in our studio</h4>
   
      	    <div class="row">
      		    <label style="width:180px;">First Name</label>
        	    <asp:TextBox ID="tbFirstName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="valFirstName" runat="server" ControlToValidate="tbFirstName" ForeColor="Red" ErrorMessage="*"  ValidationGroup="BookShopConsultation"></asp:RequiredFieldValidator>
            </div><!-- /row -->
      	    <div class="row">
      		    <label style="width:180px;">Last Name</label>
        	    <asp:TextBox ID="tbSurname" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="valSurname" runat="server" ControlToValidate="tbSurname" ForeColor="Red" ErrorMessage="*"  ValidationGroup="BookShopConsultation"></asp:RequiredFieldValidator>
    		    </div><!-- /row -->
      	    <div class="row">
      		    <label style="width:180px;">Mobile</label>
        	    <asp:TextBox ID="tbMobile" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="valMobileValid" runat="server"  ValidationExpression="^\d+$" ForeColor="Red" Display="Dynamic" ErrorMessage="* Numbers only"  ValidationGroup="BookShopConsultation" ControlToValidate="tbMobile" ></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="valMobile" runat="server" ControlToValidate="tbMobile" ForeColor="Red" ErrorMessage="*"  ValidationGroup="BookShopConsultation"></asp:RequiredFieldValidator>
   		    </div><!-- /row -->
      	    <div class="row">
      		    <label style="width:180px;">Email</label>
        	    <asp:TextBox ID="tbEmail" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="valEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ForeColor="Red" Display="Dynamic" ControlToValidate="tbEmail" ErrorMessage="*"  ValidationGroup="BookShopConsultation"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="valEmail" runat="server" ControlToValidate="tbEmail" ForeColor="Red" ErrorMessage="*"  ValidationGroup="BookShopConsultation"></asp:RequiredFieldValidator>
    		    </div><!-- /row -->
            <div class="row">
                <label style="width:174px;">Book me on</label>
                <div class="labelRight">
                    <asp:CheckBox ID="CheckBoxShopTour" name="chkShopSem" runat="server" Text="Shopping Tour" onclick="displayShoppingTourOpt()"/>
                    <asp:CheckBox ID="CheckBoxSeminar" name="chkShopSem" runat="server" Text="Seminar"  onclick="displaySeminarOpt()"/>
                </div>
                        
            </div>
      	    <div class="row" id="shoppingTourOpt" style="display:none;">
      		    <label style="width:180px;">Shopping Tour Date</label>
                <asp:DropDownList ID="ddlShoppingTour"  class="selectLong" runat="server">
                    </asp:DropDownList>
                <asp:RequiredFieldValidator ID="valStudio" runat="server" ControlToValidate="ddlShoppingTour" ForeColor="Red" ErrorMessage="*"  ValidationGroup="BookShopConsultation"></asp:RequiredFieldValidator>
    	    </div><!-- /row -->
            <div class="row" id="SeminarOpt" style="display:none;">
      		    <label style="width:180px;">Seminar Date</label>
                <asp:DropDownList ID="ddlSeminar"  class="selectLong" runat="server">
                    </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSeminar" ForeColor="Red" ErrorMessage="*"  ValidationGroup="BookShopConsultation"></asp:RequiredFieldValidator>
    	    </div><!-- /row -->
      	    <div class="row">
      	        <span onclick="alert('please choose at least one of the option');" id="fakeButton" style="margin-left:182px;cursor:hand;cursor:pointer;"><img src="http://visionpt.com.au/images/cBook.gif" /></span>
  		  	    
                <span style="margin-left:182px;cursor:hand;cursor:pointer;" id="realButton"><asp:ImageButton ID="ImageButton1" runat="server" onclick="btnBook_Click" ImageUrl="http://visionpt.com.au/images/cBook.gif"  ValidationGroup="BookShopConsultation" /></span>
  
            </div><!-- /row -->
            <div class="clear"></div>
  	    </div><!-- /cContent -->
        
    <div class="cBase"></div>
  </div><!-- /contactBox  -->
</div><!-- /contactOverlay cEnquire -->
<asp:Literal ID="contactOverlayResult" runat="server"></asp:Literal>