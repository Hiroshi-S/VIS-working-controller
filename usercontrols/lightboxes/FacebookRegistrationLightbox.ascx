<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FacebookRegistrationLightbox.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.lightboxes.FacebookRegistrationLightbox" %>

<div class="contactOverlay" id="cErrorPopup" >				
  <div class="contactBox">
  	<div class="cTop"></div>
  	<div class="cContent">
    	<a href="" target="_blank" class="replace cCross cErrorPopupClose" title="Close">Close</a>
        <h2 id="title"></h2>
    	<h4 id="h4words"></h4>
        
            <div id="FBReg" runat="server">
            
            <!-- <iframe src="https://www.facebook.com/plugins/registration.php?
             client_id=506055752837281&
             redirect_uri=http://localhost:48433/vision-virtual-training/join-now-registration&
             fields=[
                 {'name':'name'},
                 {'name':'first_name'},
                 {'name':'last_name'},
                 {'name':'email'},
                 {'name':'location'},
                 {'name':'gender'},
                 {'name':'birthday'},
                 {'name':'password'},
                 {'name':'like',       'description':'Do you like this plugin?', 'type':'checkbox',  'default':'checked'},
                 {'name':'phone',      'description':'Phone Number',             'type':'text'},
                 {'name':'anniversary','description':'Anniversary',              'type':'date'},
                 {'name':'captain',    'description':'Best Captain',             'type':'select',    'options':{'P':'Jean-Luc Picard','K':'James T. Kirk'}},
                 {'name':'force',      'description':'Which side?',              'type':'select',    'options':{'jedi':'Jedi','sith':'Sith'}, 'default':'sith'},
                 {'name':'live',       'description':'Best Place to Live',       'type':'typeahead', 'categories':['city','country','state_province']},
                 {'name':'captcha'}
                ]"
                        scrolling="auto"
                        frameborder="no"
                        style="border:none"
                        allowTransparency="true"
                        width="100%"
                        height="620">
                    </iframe>-->

                    <fb:registration redirect_uri="http://localhost:48433/vision-virtual-training/join-now-registration" 
                    width="500px"
                    border_color="#ffffff"
                     fields="[
                     {'name':'name'},
                     {'name':'first_name'},
                     {'name':'last_name'},
                     {'name':'email'},
                     {'name':'code',       'description':'Voucher code',             'type':'text'},
                     {'name':'hearboutus', 'description':'How did you hear about us?',  'type':'select',    'options':{'1':'Google','2':'Web Search','3':'Body and Soul Award Voucher','4':'Friend','5':'Youtube','6':'Paper','7':'Magazine','8':'Business Plaza','9':'Email Promotion','10':'Previous Vision Client','11':'Facebook','12':'Other'}, 'default':'11'},
                     {'name':'16years',    'description':'I am at least 16 years of age?', 'type':'checkbox',  'default':'checked'},
                     {'name':'tandc',      'description':'I have read the T&Cs previously', 'type':'checkbox',  'default':'checked'},
                    ]" /> 

                </div>
    	    <div class="row">
      	    <br/>
            <br/>
        </div><!-- /row -->
  		<div class="clear"></div>
  	</div><!-- /cContent -->
  	<div class="cBase"></div>
  </div><!-- /contactBox  -->
</div><!-- /contactOverlay cEnquire -->