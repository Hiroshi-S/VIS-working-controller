<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProgressAndGoal.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.clubvision.ProgressAndGoal" %>

<div class="progNgoal">
    <div class="progLeft">
        <span class="head">My Vision Journey</span><br />
        <asp:Label ID="weightDifferenceLable" runat="server" CssClass="num blue" /> <b><asp:Label ID="gainLostLabel" runat="server" /></b><br />
        <b style="display:block;">since <asp:Label ID="sinceDateLabel" runat="server" CssClass="blue" /></b>
    </div>
    <div class="progRight">
        <span class="head">My Goal Weight</span><br />
        <asp:Label ID="goalWeightLabel" runat="server" CssClass="num orange" /> <b><asp:Label ID="unitLabel" runat="server" /></b><br />
        <b style="display:block;">by <asp:Label ID="goalDateLabel" runat="server" CssClass="orange" /></b>
    </div>
    <div style="clear:both;" id="myGoalDiv">
        <br/>
        <span class="head">My Goal</span><br />
        <asp:Label ID="goalLabel" runat="server"></asp:Label>
    </div>
</div>
