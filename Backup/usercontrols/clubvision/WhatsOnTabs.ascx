<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WhatsOnTabs.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.clubvision.WhatsOnTabs" %>

<ul id="recipeNav">
    <li><a href="#category-GroupExercises" id="tab-GroupExercises" class="">Group Exercises</a></li>
    <li><a href="#category-Seminars" id="tab-Seminars" class="" >Seminars</a></li>
    <li><a href="#category-ShoppingTours" id="tab-ShoppingTours" class="">Shopping Tours</a></li>
    <li><a href="#category-Events" id="tab-Events" class="" >Events</a></li>
</ul>

</div>
<div id="tab-Events1" style="display: none;margin: 30px;"> 
     <asp:GridView ID="GridViewEvents" runat="server" AutoGenerateColumns="false"
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
            <HeaderStyle BackColor="#FFFFFF" Font-Bold="True" ForeColor="#666666" 
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
<div id="tab-ShoppingTours1" style="display: none; margin: 30px;">
     <asp:GridView ID="GridViewShoppingTour" runat="server" AutoGenerateColumns="False" 
            CellPadding="8"  ForeColor="#333333" 
            GridLines="None" Width="100%" CaptionAlign="Left" >
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                
                
                <asp:TemplateField SortExpression="WhatsOnDate">
                    <ItemTemplate>
                         <asp:Label ID="Label3" runat="server" Text='<%# RenderWhatsOnDate(DataBinder.Eval(Container.DataItem,"WhatsOnDate"))%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderTemplate>
                        <div style="float: left">Date</div>
                    </HeaderTemplate>
                </asp:TemplateField>
               
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <EmptyDataTemplate>
                Please contact your studio for details
            </EmptyDataTemplate>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
             <HeaderStyle BackColor="#FFFFFF" Font-Bold="True" ForeColor="#666666" 
                HorizontalAlign="Left" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#D1F7B8" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>

</div>
<div id="tab-Seminars1" style="display: none;margin: 30px;">
    <asp:GridView ID="GridViewSeminar" runat="server" AutoGenerateColumns="False" 
            CellPadding="8" ForeColor="#333333" 
            GridLines="None" Width="100%" CaptionAlign="Left" >
            <AlternatingRowStyle BackColor="White" />
            <Columns>
               
                <asp:TemplateField SortExpression="WhatsOnDate">
                    <ItemTemplate>
                         <asp:Label ID="Label3" runat="server" Text='<%# (DataBinder.Eval(Container.DataItem,"WhatsOnSummary"))%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderTemplate>
                        <div style="float: left">Date</div>
                    </HeaderTemplate>
                </asp:TemplateField>
                
                
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <EmptyDataTemplate>
                Please contact your studio for details
            </EmptyDataTemplate>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#FFFFFF" Font-Bold="True" ForeColor="#666666" 
                HorizontalAlign="Left" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#E9C4FF" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
</div>
<div id="tab-GroupExercises1" style="display: none;margin: 30px;">
    
        <asp:GridView ID="GridViewGroupExercise" runat="server" AutoGenerateColumns="false"
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
            <HeaderStyle BackColor="#FFFFFF" Font-Bold="True" ForeColor="#666666" 
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