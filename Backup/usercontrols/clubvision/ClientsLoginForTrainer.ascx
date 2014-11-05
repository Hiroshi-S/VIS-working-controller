<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClientsLoginForTrainer.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.clubvision.ClientsLoginForTrainer" %>

<div id="eMedia" class="element"><!-- 970 -->
    <h3 class="replace" style="background: url('/images/eHdrRedMyClients.gif') no-repeat scroll 0 0 transparent !important;">Client's Login</h3>
    <div class="eContent eRed" style="height: auto;" >
        
        <div style="margin: -11px 0 0 -10px;">
        <div style="background-color: #c60c30;color: white;text-align: right;">
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:Button ID="Button1"
                runat="server" Text="Search Member" onclick="Button1_Click" />
        </div>
        
        <asp:Panel ID="panelContainer" runat="server" Height="300px" Width="100%"  ScrollBars="Vertical">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false"
        CellPadding="8" ForeColor="#333333" GridLines="None" Width="100%"  >
            <AlternatingRowStyle BackColor="White" />
            
            <Columns>
                
                <asp:TemplateField SortExpression="Member">
                    <ItemTemplate>
                      
                         <asp:HyperLink ID="NameHyperLink" runat="server" Target="_blank" 
                            NavigateUrl='<%# "/club-vision/trainerlogin.aspx?loginUser=" + Eval("CVUsername") + "&loginPass=" + Eval("CVPassword") +"&loginNo=" + Eval("MemberNo") %> ' Text='<%#Eval("Member") %>' > </asp:HyperLink>
                    </ItemTemplate>
                    <HeaderTemplate>
                        <div style="float: left">Member</div>
                    </HeaderTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField SortExpression="Member">
                    <ItemTemplate>
                         <asp:Label ID="Label1" runat="server" Text='<%# Eval("CHO")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderTemplate>
                        <div style="float: left">CHO</div>
                    </HeaderTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField SortExpression="Member">
                    <ItemTemplate>
                         <asp:Label ID="Label1" runat="server" Text='<%# Eval("PTN")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderTemplate>
                        <div style="float: left">PTN</div>
                    </HeaderTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField SortExpression="Member">
                    <ItemTemplate>
                         <asp:Label ID="Label1" runat="server" Text='<%# Eval("FAT")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderTemplate>
                        <div style="float: left">FAT</div>
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
        </asp:Panel>
        &nbsp;&nbsp;Note : Clicking on the name will log you out and redirect you to their account
        </div>

    </div> <!-- eContent eRed -->
    
    <div class="clear"></div>
</div>
