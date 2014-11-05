<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProfileView.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.VVT.ProfileView" %>
<div id="eProfileView" class="element"><!-- 290 
  <!--<h3 class="replace">Profile View</h3>-->
  <div class="tabHeading">
       <div style="width: 278px;text-align: left;padding-left: 10px;cursor: default;" class="divHeadingActive">
           <asp:Literal ID="ProfileNameLiteral" runat="server"></asp:Literal></div>
  </div>
   <div class="clear"></div>
  <div class="eContent eClean" id="profileViewContent" runat="server" > 
    <div class="profileCopy">
        <asp:Literal ID="literalImage" runat="server"></asp:Literal>
        <span class="title" id="title" runat="server"></span><br />
        <table cellpadding="2" cellspacing="0" border="0">
            <!-- <tr><td width="100">My Member No:</td><td width="150" runat="server" id="memberNo"></td></tr> -->
            <tr><td width="100">My Studio:</td><td width="150" runat="server" id="studio"></td></tr>
            <tr><td width="100">My Trainer:</td><td width="150" runat="server" id="trainer"></td></tr>
        </table><br />

<div style="float: left; width: 150px;">
<span class="title">My Vision Journey</span><br />
<span class="lost" style="color:#008CA7 !important;" id="change" runat="server"></span> <b><span id="change_text" runat="server">kg lost</span></b><br /><br />
</div>
<div style="float: left; width: 120px;">
<span class="title">My Goal Weight</span><br />
<span class="lost" style="color:#E27423 !important;" id="target" runat="server"></span> <b><span id="target_text" runat="server">kg</span></b><br /><br />
</div> 

<!-- Begin Weekly Goal section, default to unvisible -->
<div id="WeeklyGoals" runat="server" Visible="False">
<br/>
<span class="title">Weekly Weigh-In</span><br />
<span id="weeklyWeight" runat="server"></span>
<br/><br />
<span class="title">Weekly Goal</span><br />
<span id="weeklyTarget" runat="server"></span><br /><br />
</div>
<!-- End Weekly Goal section -->

<span class="title">My Long Term Goal</span><br />
<span id="goal" runat="server"></span><br /><br />
</div>
    <div id="buttonEditProfile" style="display: none;"><a href="/club-vision/my-profile/edit-profile/"><img src="/images/buttonEditProfile.gif" alt="Edit Profile" height="28" width="122" border="0" /></a></div>
    <div class="clear">&nbsp;</div>
  </div><!-- /eContent -->
  <div class="clear">&nbsp;</div>
</div><!-- /eProfileView -->
