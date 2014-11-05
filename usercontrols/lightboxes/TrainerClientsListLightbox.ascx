<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TrainerClientsListLightbox.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.lightboxes.TrainerClientsListLightbox" %>


<div class="contactOverlay" id="cTrainerClientsEmailPopup" >				<!--11111111111111111111111 -->
  <div class="contactBox"> <!--11111111111111111111111 -->
  	<div class="cTop"></div>
  	<div class="cContent">
    	<a href="" target="_blank" class="replace cCross cTrainerClientsEmailPopupClose" title="Close">Close</a>
    	<h2 id="title">My Clients Email</h2>
    	<h4 id="h4words"></h4>
      	
      	<div class="row">
             <div style="margin: -11px 0 0 -10px;">
        <div style="background-color: #c60c30;color: white;text-align: right;">
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:Button ID="Button1"
                runat="server" Text="Search Member" onclick="Button1_Click" />
        </div>
         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false"
        CellPadding="5" ForeColor="#333333" GridLines="None" Width="100%" 
            AllowSorting="True" AllowPaging="True" OnPageIndexChanging="gridView_PageIndexChanging" OnSorting="gridView_Sorting"
                PageSize="9" onrowcommand="GridView1ItemCommand">
            <AlternatingRowStyle BackColor="White" />
            
            <Columns>
                
               <asp:TemplateField SortExpression="Name">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server"  Text='<%#Eval("Name") %>'  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="sendEmail"></asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Label ID="LabelSent" runat="server"
                             Text='<%#Eval("Email") %>' Font-Bold="True" visible="false" />   
                    </ItemTemplate>
                    <HeaderTemplate>
                        <div style="float: left">Member Name</div>
                    </HeaderTemplate>
                </asp:TemplateField>
                <asp:TemplateField SortExpression="Name">
                    <ItemTemplate>
                         <asp:Label ID="Label1" runat="server" Text='<%# Eval("CHO")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderTemplate>
                        <div style="float: left">Carbohydrates</div>
                    </HeaderTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField SortExpression="Name">
                    <ItemTemplate>
                         <asp:Label ID="Label2" runat="server" Text='<%# Eval("PTN")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderTemplate>
                        <div style="float: left">Protein</div>
                    </HeaderTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField SortExpression="Name">
                    <ItemTemplate>
                         <asp:Label ID="Label3" runat="server" Text='<%# Eval("FAT")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderTemplate>
                        <div style="float: left">Fat</div>
                    </HeaderTemplate>
                </asp:TemplateField>
                
   
            </Columns>
            

             <EmptyDataTemplate>
                No clients found.
            </EmptyDataTemplate>
            <FooterStyle BackColor="#ffffff" ForeColor="#C60C30" Font-Bold="True" />
            <HeaderStyle BackColor="#C60C30" Font-Bold="True" ForeColor="White" 
                HorizontalAlign="Left" />
            <PagerStyle BackColor="#ffffff" ForeColor="#C60C30" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
            
        </asp:GridView>
        
        </div>

      	    <br/>
            <br/>
        </div><!-- /row -->
  		<div class="clear"></div>
  	</div><!-- /cContent -->
  	<div class="cBase"></div>
  </div><!-- /contactBox  -->
</div><!-- /contactOverlay cEnquire -->