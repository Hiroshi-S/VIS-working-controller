<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProfileView.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.externalclubvision.ProfileView" %>
<%@ Register TagPrefix="uc" TagName="progNgoal" Src="~/usercontrols/clubvision/ProgressAndGoal.ascx" %>

<div id="eProfileView" class="element"><!-- 290 -->
  <!--<h3 class="replace">Profile View</h3>-->
  <div class="tabHeading">
       <div style="width: 278px;text-align: left;padding-left: 10px;cursor: default;" class="divHeadingActive">
           <asp:Literal ID="ProfileNameLiteral" runat="server"></asp:Literal></div>
  </div>
   <div class="clear"></div>
  <div class="eContent eClean"> 
    <div class="profileCopy">
        <asp:Literal ID="literalImage" runat="server"></asp:Literal>
        <span class="title" id="title" runat="server"></span><br />
        <table cellpadding="2" cellspacing="0" border="0">
            <tr><td width="100">My Member No:</td><td width="150" runat="server" id="memberNo"></td></tr>
            <tr><td width="100">My Country:</td><td width="150" runat="server" id="country"></td></tr>
        </table>
        <br />
        <uc:progNgoal ID="progs" runat="server" />
    </div>
    <div id="buttonEditProfile" style="display: none;"><a href="/club-vision/my-profile/edit-profile/ext%20edit%20profile.aspx"><img src="/images/buttonEditProfile.gif" alt="Edit Profile" height="28" width="122" border="0" /></a></div>
    <div class="clear">&nbsp;</div>
  </div><!-- /eContent -->
  <div class="clear">&nbsp;</div>
</div><!-- /eProfileView -->
