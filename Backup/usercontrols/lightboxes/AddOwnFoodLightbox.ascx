<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddOwnFoodLightbox.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.lightboxes.AddOwnFoodLightbox" %>

<div class="contactOverlay" id="cAddOwnFood" >				
  <div class="contactBox">
  	<div class="cTop"></div>
  	<div class="cContent">
    	<a href="" target="_blank" class="replace cCross cAddOwnFoodClose" title="Close">Close</a>
    	<h2>Please note</h2>
    	<h4>This item is available only in your personalised profile.  Would you like it to be verified by VisionHQ and permanently added to the main food database?</h4>
      	
      	<div class="row">
      	    <br/>
            <br/>
            <div style="display: block;padding-top: 20px;">
      	        <div id="bfastsection" style="width: 100px;float: left;margin-right: 50px;">
      	            <asp:ImageButton ID="YesImageButton" runat="server" ImageUrl="http://visionpt.com.au/media/137419/cyes.gif" Width="107px" Height="23px" BorderWidth="0px" OnClientClick="foodItemRequest('AddOwnFood--Brand= ' + $('#brand').val() + 
                                                                                                                        '. product = ' + $('#foodName').val() + 
                                                                                                                        '. ServSize= ' +  $('#amount').val() +
                                                                                                                        '. ServUnit = ' +$('#serve').val() +
                                                                                                                        '. Carb= ' + $('#carbs').val()+
                                                                                                                        '. Ptn= ' + $('#ptn').val() +
                                                                                                                        '. Fat= ' +  $('#fat').val(),'resultBreakfast','true');$('#cAddOwnFood').fadeOut();return false;"/> 
      	        </div>
                
                <div id="morningteasection" style="width: 100px;float: left;margin-right: 50px;">
      	            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="http://visionpt.com.au/media/137419/cyes.gif" Width="107px" Height="23px" BorderWidth="0px" OnClientClick="foodItemRequest('AddOwnFood--Brand= ' + $('#AOBrandName_MorningTea').val() + 
                                                                                                                        '. product = ' + $('#AOFoodName_MorningTea').val() + 
                                                                                                                        '. ServSize= ' +  $('#AOAmount_MorningTea').val() +
                                                                                                                        '. ServUnit = ' + $('#AOUnit_MorningTea').val() +
                                                                                                                        '. Carb= ' + $('#AOCarbs_MorningTea').val()+
                                                                                                                        '. Ptn= ' + $('#AOPtn_MorningTea').val() +
                                                                                                                        '. Fat= ' +  $('#AOFat_MorningTea').val(),'resultMorningTea','true');$('#cAddOwnFood').fadeOut();return false;"/> 
      	        </div>

                <div id="lunchsection" style="width: 100px;float: left;margin-right: 50px;">
      	                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="http://visionpt.com.au/media/137419/cyes.gif" Width="107px" Height="23px" BorderWidth="0px" OnClientClick="foodItemRequest('AddOwnFood--Brand= ' + $('#AOBrandName_Lunch').val() + 
                                                                                                                            '. product = ' + $('#AOFoodName_Lunch').val() + 
                                                                                                                            '. ServSize= ' +  $('#AOAmount_Lunch').val() +
                                                                                                                            '. ServUnit = ' + $('#AOUnit_Lunch').val() +
                                                                                                                            '. Carb= ' + $('#AOCarbs_Lunch').val()+
                                                                                                                            '. Ptn= ' + $('#AOPtn_Lunch').val() +
                                                                                                                            '. Fat= ' +  $('#AOFat_Lunch').val(),'resultLunch','true');$('#cAddOwnFood').fadeOut();return false;"/> 
      	        </div>

                <div id="afternoonteasection" style="width: 100px;float: left;margin-right: 50px;">
      	            <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="http://visionpt.com.au/media/137419/cyes.gif" Width="107px" Height="23px" BorderWidth="0px" OnClientClick="foodItemRequest('AddOwnFood--Brand= ' + $('#AOBrandName_AfternoonTea').val() + 
                                                                                                                        '. product = ' + $('#AOFoodName_AfternoonTea').val() + 
                                                                                                                        '. ServSize= ' +  $('#AOAmount_AfternoonTea').val() +
                                                                                                                        '. ServUnit = ' + $('#AOUnit_AfternoonTea').val() +
                                                                                                                        '. Carb= ' + $('#AOCarbs_AfternoonTea').val()+
                                                                                                                        '. Ptn= ' + $('#AOPtn_AfternoonTea').val() +
                                                                                                                        '. Fat= ' +  $('#AOFat_AfternoonTea').val(),'resultAfternoonTea','true');$('#cAddOwnFood').fadeOut();return false;"/> 
      	        </div>
                 
                <div id="dinnersection"style="width: 100px;float: left;margin-right: 50px;">
      	            <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="http://visionpt.com.au/media/137419/cyes.gif" Width="107px" Height="23px" BorderWidth="0px" OnClientClick="foodItemRequest('AddOwnFood--Brand= ' + $('#AOBrandName_Dinner').val() + 
                                                                                                                        '. product = ' + $('#AOFoodName_Dinner').val() + 
                                                                                                                        '. ServSize= ' +  $('#AOAmount_Dinner').val() +
                                                                                                                        '. ServUnit = ' + $('#AOUnit_Dinner').val() +
                                                                                                                        '. Carb= ' + $('#AOCarbs_Dinner').val()+
                                                                                                                        '. Ptn= ' + $('#AOPtn_Dinner').val() +
                                                                                                                        '. Fat= ' +  $('#AOFat_Dinner').val(),'resultDinner','true');$('#cAddOwnFood').fadeOut();return false;"/> 
      	        </div>

                <div id="suppersection" style="width: 100px;float: left;margin-right: 50px;">
      	            <asp:ImageButton ID="ImageButton9" runat="server" ImageUrl="http://visionpt.com.au/media/137419/cyes.gif" Width="107px" Height="23px" BorderWidth="0px" OnClientClick="foodItemRequest('AddOwnFood--Brand= ' + $('#AOBrandName_Supper').val() + 
                                                                                                                        '. product = ' + $('#AOFoodName_Supper').val() + 
                                                                                                                        '. ServSize= ' +  $('#AOAmount_Supper').val() +
                                                                                                                        '. ServUnit = ' + $('#AOUnit_Supper').val() +
                                                                                                                        '. Carb= ' + $('#AOCarbs_Supper').val()+
                                                                                                                        '. Ptn= ' + $('#AOPtn_Supper').val() +
                                                                                                                        '. Fat= ' +  $('#AOFat_Supper').val(),'resultSupper','true');$('#cAddOwnFood').fadeOut();return false;"/> 
      	        </div>

  		  	    <div style="width: 100px;float: left">
  		  	        <asp:ImageButton ID="NoImageButton" runat="server" ImageUrl="http://visionpt.com.au/media/137424/cno.gif" Width="107px" Height="23px" BorderWidth="0px" OnClientClick=" $('#cAddOwnFood').fadeOut();
                        return false;" />
  		  	    </div>
            </div>

        </div><!-- /row -->
  		<div class="clear"></div>
  	</div><!-- /cContent -->
  	<div class="cBase"></div>
  </div><!-- /contactBox  -->
</div><!-- /contactOverlay cEnquire -->