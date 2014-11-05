<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WhatsOnVos.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.studiopages.WhatsOnVos" %>

<div id="eWhatsOn" class="element eWhatsOn605px" style="margin-bottom: 50px;" xmlns:dt="urn:schemas-microsoft-com:datatypes">
    
    <h3 class="replace"> </h3>

        <div class="eContent eBlue" style="padding: 0px; width: 603px;">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false"
        CellPadding="8" ForeColor="#333333" GridLines="None" Width="100%">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                
                <asp:TemplateField SortExpression="WeekDay">
                    <ItemTemplate>
                         <asp:Label ID="Label1" runat="server" Text='<%# Eval("WhatsOn")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderTemplate>
                        <div style="float: left">Group Exercise</div>
                    </HeaderTemplate>
                </asp:TemplateField>
                

                 <asp:TemplateField SortExpression="WeekDay">
                    <ItemTemplate>
                         <asp:Label ID="Label1" runat="server" Text='<%# RenderDays(DataBinder.Eval(Container.DataItem,"WeekDay"))%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderTemplate>
                        <div style="float: left">Day</div>
                    </HeaderTemplate>
                </asp:TemplateField>

                <asp:TemplateField SortExpression="WeekDay">
                    <ItemTemplate>
                         <asp:Label ID="Label1" runat="server" Text='<%# (DataBinder.Eval(Container.DataItem,"Time"))%>'></asp:Label>
                         <asp:Label ID="Label4" runat="server" Text='<%# (DataBinder.Eval(Container.DataItem,"WhenAMPM"))%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderTemplate>
                        <div style="float: left">Time</div>
                    </HeaderTemplate>
                </asp:TemplateField>
                
                
            </Columns>
            
            <EmptyDataTemplate>
                Please contact your studio for details
            </EmptyDataTemplate>
            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
            <HeaderStyle BackColor="#008CA7" Font-Bold="True" ForeColor="White" 
                HorizontalAlign="Left" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>

        <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="false"
        CellPadding="8" ForeColor="#333333" GridLines="None" Width="100%">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                
                <asp:TemplateField SortExpression="WeekDay">
                    <ItemTemplate>
                         <asp:Label ID="Label1" runat="server" Text='<%# Eval("WhatsOn")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderTemplate>
                        <div style="float: left">Events</div>
                    </HeaderTemplate>
                </asp:TemplateField>

                 <asp:TemplateField  SortExpression="WhatsOnDate">
                    <ItemTemplate>
                         <asp:Label ID="Label3" runat="server" Text='<%# RenderWhatsOnDate(DataBinder.Eval(Container.DataItem,"WhatsOnDate"))%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderTemplate>
                        <div style="float: left">Date</div>
                    </HeaderTemplate>
                </asp:TemplateField>
                
            </Columns>
            
            <EmptyDataTemplate>
                Please contact your studio for details
            </EmptyDataTemplate>
            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
            <HeaderStyle BackColor="#008CA7" Font-Bold="True" ForeColor="White" 
                HorizontalAlign="Left" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
        </div>
    </div>
    
<div id="eFitnessSeminars" class="element eFitnessSeminars" 
        style="margin: 0px 0px 0px 0px; float: left; padding-left: 0px;" xmlns:dt="urn:schemas-microsoft-com:datatypes">
    <h3 > </h3>
    <div class="eContent ePurple" style="padding: 0px;">
        <!-- Content starts here --> 
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
            CellPadding="8" ForeColor="#333333" 
            GridLines="None" Width="100%" CaptionAlign="Left" ShowHeader="True">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
               
                <asp:TemplateField HeaderText="<div style='float:left;text-align: left !important;'><img src='http://www.visionpt.com.au/media/212915/fotolia_30360703_xl.jpg'/></div><h4>Education seminars are held at the studios on various topics including nutrition, weight loss, health and fitness. Available for clients and non clients.</h4><br/> <a href='' class='cBookingPopupOpen'><img src='http://www.visionpt.com.au/images/buttonBookYourSpotToday.gif'/></a>"
                SortExpression="WhatsOnDate">
                    <ItemTemplate>
                       <span style="display: none"><asp:Label ID="Label3" runat="server" Text='<%# RenderWhatsOnDate(DataBinder.Eval(Container.DataItem,"WhatsOnDate"))%>'></asp:Label>
                       <asp:Label ID="Label1" runat="server" Text='<%# (DataBinder.Eval(Container.DataItem,"Time"))%>  '></asp:Label></span>
                       <asp:Label ID="Label4" runat="server" Text='<%# (DataBinder.Eval(Container.DataItem,"WhatsOnSummary"))%>'></asp:Label>
                    </ItemTemplate>
                    
                </asp:TemplateField>
                
            </Columns>
            
            <EditRowStyle BackColor="#2461BF" />
            <EmptyDataTemplate>
                <div style='float:left'><img src='http://www.visionpt.com.au/media/212915/fotolia_30360703_xl.jpg'/></div><h4>Education seminars are held at the studios on various topics including nutrition, weight loss, health and fitness. Available for clients and non clients, contact your closest studio to book your complimentary seat.</h4>
            </EmptyDataTemplate>
            
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#E9C4FF" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>

    </div>
</div>


<div id="eShoppingTours" class="element eFitnessSeminars" 
    style="float:right;" xmlns:dt="urn:schemas-microsoft-com:datatypes">
    <h3 > </h3>
    <div class="eContent eGreen" style="padding: 0px; width: 288px;" >
        <!-- Content starts here --> 
        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
            CellPadding="8"  ForeColor="#333333" 
            GridLines="None" Width="100%" CaptionAlign="Left">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                
                
                <asp:TemplateField SortExpression="WhatsOnDate" HeaderText="<div style='float:right;width: 59px;text-align: left;'><img src='http://www.visionpt.com.au/media/212910/fotolia_47464681_xl.jpg' style='display: inline;'/></div><p><h4>Regular shopping tours to the local supermarket will educate you on food labels, ingredients and what foods will help you achieve your goals. Available for clients and non clients.</h4><br/> <a href='' class='cBookingPopupOpen'><img src='http://www.visionpt.com.au/images/buttonBookYourSpotToday.gif'/></a></p>" >
                    <ItemTemplate>
                         <asp:Label ID="Label3" runat="server" Text='<%# RenderWhatsOnDate(DataBinder.Eval(Container.DataItem,"WhatsOnDate"))%>'></asp:Label>
                                                <asp:Label ID="Label1" runat="server" Text='<%# (DataBinder.Eval(Container.DataItem,"Time"))%>  '></asp:Label>
                         <asp:Label ID="Label4" runat="server" Text='<%# (DataBinder.Eval(Container.DataItem,"WhenAMPM"))%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
               
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <EmptyDataTemplate>
               <div style='float:right;'><img src='http://www.visionpt.com.au/media/212910/fotolia_47464681_xl.jpg' style='display: inline;'/></div><p><h4>Regular shopping tours to the local supermarket will educate you on food labels, ingredients and what foods will help you achieve your goals. Available for clients and non clients, book your spot today.</h4></p>
            </EmptyDataTemplate>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#D1F7B8" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>


        
    </div>
 </div>