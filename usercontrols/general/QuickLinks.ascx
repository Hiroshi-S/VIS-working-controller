<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QuickLinks.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.general.QuickLinks" %>
<div style="display: none;">
    <h2>Quick Links</h2>
    <br />
    <asp:Repeater ID="Repeater1" runat="server" DataSourceID="LinqDataSource1">
        <ItemTemplate>
            <asp:HyperLink ID="HyperLink1" runat="server">testing</asp:HyperLink><br />
        </ItemTemplate>
    </asp:Repeater>
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
        ContextTypeName="VisionPersonalTrainingProject.ClubVisionDataContext" 
        EntityTypeName="" Select="new (Title, LinkUrl)" TableName="QuickLinks">
    </asp:LinqDataSource>
</div>
