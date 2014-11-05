<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WhatsOnView.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.clubvision.WhatsOnView" %>
<div id="eWhatsOnView" class="element"><!-- 290 -->
  <!--<h3>What's On</h3> -->
  <div class="tabHeading">
      <div style="width: 278px;text-align: left;padding-left: 10px;cursor: default;color: #008ca7;" class="divHeadingActive">What's On</div>
  </div>
  <div class="eContent eClean" style="height: 340px;"> 
    
      <asp:GridView ID="GridViewGroupExercise" runat="server" 
        AutoGenerateColumns="False"   ShowFooter="True" GridLines="None">
            <Columns>
                <asp:TemplateField>
                   <ItemTemplate>   
                       <asp:Label ID="WhatsOnSummary" runat="server" Text='<%# Eval("WhatsOnSummary")%>'></asp:Label>
                   </ItemTemplate>                     

                    <HeaderTemplate>
                        <div style="float: left"><h5><a  href="/club-vision/community/whats-on/#category-GroupExercises">Group Exercise</a></h5></div>
                    </HeaderTemplate>
                
                    <FooterTemplate>
                        <div class="eWhatsOnDivider">&nbsp;</div>
                    </FooterTemplate>

                </asp:TemplateField>
            </Columns>
            
      </asp:GridView>
      
      <asp:GridView ID="GridViewSeminar" runat="server" 
          AutoGenerateColumns="False" ShowFooter="True" GridLines="None" >
            <Columns>
                <asp:TemplateField>
                   <ItemTemplate>   
                       <asp:Label ID="WhatsOnSummary2" runat="server" Text='<%# Eval("WhatsOnSummary")%>'></asp:Label>
                   </ItemTemplate>                     

                    <HeaderTemplate>
                        <div style="float: left"><h5><a href="/club-vision/community/whats-on/#category-Seminars">Nutritional Seminar</a></h5></div>
                    </HeaderTemplate>
                
                    <FooterTemplate>
                        <div class="eWhatsOnDivider">&nbsp;</div>
                    </FooterTemplate>

                </asp:TemplateField>
            </Columns>
      </asp:GridView>
      

      
      <asp:GridView ID="GridViewShoppingTour" runat="server" 
          AutoGenerateColumns="False" ShowFooter="True" GridLines="None">
            <Columns>
                <asp:TemplateField>
                   <ItemTemplate>   
                       <asp:Label ID="WhatsOnSummary3" runat="server" Text='<%# Eval("WhatsOnSummary")%>'></asp:Label>
                   </ItemTemplate>                     

                    <HeaderTemplate>
                        <div style="float: left"><h5><a href="/club-vision/community/whats-on/#category-ShoppingTours" >Shopping Tour</a></h5></div>
                    </HeaderTemplate>
                
                    <FooterTemplate>
                        <div class="eWhatsOnDivider">&nbsp;</div>
                    </FooterTemplate>

                </asp:TemplateField>
            </Columns>
      </asp:GridView>
      
      
      <asp:GridView ID="GridViewEvents" runat="server" 
          AutoGenerateColumns="False"  ShowFooter="True" GridLines="None" >
            <Columns>
                <asp:TemplateField>
                   <ItemTemplate>   
                       <asp:Label ID="WhatsOnSummary4" runat="server" Text='<%# Eval("WhatsOn")%>'></asp:Label>
                   </ItemTemplate>                     

                    <HeaderTemplate>
                        <div style="float: left"><h5><a href="/club-vision/community/whats-on/#category-Events">Events</a></h5></div>
                    </HeaderTemplate>
                
                    <FooterTemplate>
                        <div class="eWhatsOnDivider">&nbsp;</div>
                    </FooterTemplate>

                </asp:TemplateField>
            </Columns>
            
            <HeaderStyle HorizontalAlign="Left" />
            
      </asp:GridView>
     
    <h5 style="float:right; padding-right: 14px"><a href="/club-vision/community/whats-on/">View more &gt;</a></h5>
    <div class="clear">&nbsp;</div>
  </div><!-- /eContent -->
  <div class="clear">&nbsp;</div>
</div><!-- /eWhatsOnView -->
