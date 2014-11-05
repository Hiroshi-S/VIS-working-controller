<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WhatsOnEvents.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.externalclubvision.WhatsOnEvents" %>
<div id="eWhatsOnMainWithNews" class="element">
    <h3 class="replace">What's On</h3>
        <div class="eContent eOrange">
                
        <div id="tab-Events1" style="display: block; /* margin: 30px; */"> 
             <div>
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
                    <HeaderStyle BackColor="#E27423" Font-Bold="True" ForeColor="White" 
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

        <div class="clear">&nbsp;</div>
        </div>
    </div>