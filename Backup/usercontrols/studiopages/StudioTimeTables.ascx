<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StudioTimeTables.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.studiopages.StudioTimeTables" %>

<div class="row studioTimetable">
    <div class="col-md-12">
        <h3>Group Training Sessions Timetable</h3>
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        <br/>
        <a href="/group-training/">Click here for session descriptions ></a>
    </div>
</div>

<div class="row studioTimetable">
    <div class="col-md-12">
        <h3>Events</h3>
        <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="false" CssClass="timetableStude table"
        CellPadding="8" ForeColor="#333333" GridLines="None" Width="100%">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                
                <asp:TemplateField SortExpression="WeekDay">
                    <ItemTemplate>
                         <asp:Label ID="Label1" runat="server" Text='<%# Eval("WhatsOn")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderTemplate>
                        <div style="float: left">Events Name</div>
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
            
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFFFF" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
    </div>
</div>

<div class="row studioTimetable">
    <div class="col-md-6 col-xs-12">
        <div id="eFitnessSeminars" style="margin: 0px 0px 0px 0px; float: left; padding-left: 0px;" xmlns:dt="urn:schemas-microsoft-com:datatypes">
     <!-- Content starts here --> 
    <h3>Seminars</h3>
    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="timetableStud2 table"
        CellPadding="8" ForeColor="#333333" 
        GridLines="None" Width="100%" CaptionAlign="Left" ShowHeader="True">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
               
            <asp:TemplateField HeaderText="<div style='float:left;text-align: left !important;'><img src='http://www.visionpt.com.au/media/212915/fotolia_30360703_xl.jpg'/></div><p>Education seminars are held at the studios on various topics including nutrition, weight loss, health and fitness. Available for clients and non clients.</p><br/> <a href='' class='cBookingPopupOpen btn btn-small btn-primary btn-visionred'>Book Your Spot Today</a>"
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
            <p>Education seminars are held at the studios on various topics including nutrition, weight loss, health and fitness. Available for clients and non clients, contact your closest studio to book your complimentary seat.</p>
        <!--
            <div style='float:left'><img src='http://www.visionpt.com.au/media/212915/fotolia_30360703_xl.jpg'/></div><h4>Education seminars are held at the studios on various topics including nutrition, weight loss, health and fitness. Available for clients and non clients, contact your closest studio to book your complimentary seat.</h4>
         -->
        </EmptyDataTemplate>
           
    </asp:GridView>
    </div>
    </div>
    <div class="col-md-6 col-xs-12">
        <div id="eShoppingTours" style="float:right;" xmlns:dt="urn:schemas-microsoft-com:datatypes">
        <h3>Shopping Tour</h3>
        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" CssClass="timetableStud2 table" 
            CellPadding="8"  ForeColor="#333333" 
            GridLines="None" Width="100%" CaptionAlign="Left">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField SortExpression="WhatsOnDate" HeaderText="<div style='float:left;text-align: left;'><img src='http://www.visionpt.com.au/media/212910/fotolia_47464681_xl.jpg'/></div><p>Regular shopping tours to the local supermarket will educate you on food labels, ingredients and what foods will help you achieve your goals. Available for clients and non clients.</p><br/> <a href='' class='cBookingPopupOpen btn btn-small btn-primary btn-visionred'>Book Your Spot Today</a>" >
                    <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# RenderWhatsOnDate(DataBinder.Eval(Container.DataItem,"WhatsOnDate"))%>'></asp:Label>
                            <asp:Label ID="Label1" runat="server" Text='<%# (DataBinder.Eval(Container.DataItem,"Time"))%>  '></asp:Label>
                            <asp:Label ID="Label4" runat="server" Text='<%# (DataBinder.Eval(Container.DataItem,"WhenAMPM"))%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
               
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <EmptyDataTemplate>
                <p>Regular shopping tours to the local supermarket will educate you on food labels, ingredients and what foods will help you achieve your goals. Available for clients and non clients, book your spot today.</p>
            </EmptyDataTemplate>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFFFF" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
    </div>
    </div>
</div>