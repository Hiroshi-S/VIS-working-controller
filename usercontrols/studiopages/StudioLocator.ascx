<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StudioLocator.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.studiopages.StudioLocator" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

      	<div class="row">
      	    <div class="col-md-12 findStudioPostCode" style="text-align:center;">
                <asp:TextBox style="width:200px;padding-left:5px;height: 45px;line-height: 45px;" 
                    ID="tbPostcode" placeholder="Enter Postcode" runat="server"></asp:TextBox>

                <asp:LinkButton CssClass="btn btn-lg btn-primary btn-visionred cSearch noround" ID="btnFind" runat="server" 
                    onclick="btnFind_Click">Search AU</asp:LinkButton>
                    
                <asp:LinkButton CssClass="btn btn-lg btn-primary btn-visionnavy cSearch noround"  ID="LinkButton1" runat="server" 
                    onclick="btnFindNZ_Click">Search NZ</asp:LinkButton>
      	    </div>
    	</div><!-- /row -->
   
   
      	        
      	        <label class="within" style="width:220px;display: none;">Find nearest studios from postcode</label>
                <label class="within within2" style="margin-left:15px;width:80px;display: none;"></label> 	  
           		 		
  		<!-- START find studio results - remove this section when displaying map above 
  		<div class="findResults">-->
        
        <asp:Label ID="lblErrorMessage" runat="server" Text=""></asp:Label>
        <asp:Label ID="lblStudioList" runat="server" Text=""></asp:Label>    
       
  		<!--</div> /findResults -->
  		<!-- END find studio results -->
  		