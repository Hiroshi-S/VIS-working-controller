<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProfileEditMenu.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.externalclubvision.ProfileEditMenu" %>

<div id="eProfileEdit" class="element"><!-- 290 -->
  <h3 class="replace">Edit Profile</h3>
  <div id="rightsection" class="eContent eOrange" style="height: 514px;">
    <div class="profileCopy3">
        <div class="imageFrame">
            <div class="imageHolder">
                <asp:Literal ID="literalImage" runat="server"></asp:Literal>
            </div>
        </div>    
        <asp:Panel id="bulletsteps" runat="server">
        <div id="steps">
            Step 1   &nbsp; &nbsp; &nbsp; <a href="/club-vision/my-profile/edit-profile/?alttemplate=Ext%20Edit%20Profile">My Details > </a><br /><br />
            Step 2   &nbsp; &nbsp; &nbsp; <a href="/club-vision/my-profile/edit-profile/?alttemplate=Ext%20Edit%20Profile&tab=lifestylescreen">Lifestyle Screen > </a><br /><br />
            Step 3   &nbsp; &nbsp; &nbsp; <a href="/club-vision/my-profile/edit-profile/?alttemplate=Ext%20Edit%20Profile&tab=bodytype">Body Type > </a><br /><br />
            Step 4   &nbsp; &nbsp; &nbsp; <a href="/club-vision/my-profile/edit-measurements/">My Measurements > </a><br /><br />
            Step 5   &nbsp; &nbsp; &nbsp; <a href="/club-vision/my-profile/edit-measurements/?tab=goals">My Goals > </a><br /><br />
            Step 6   &nbsp; &nbsp; &nbsp; <a href="/club-vision/my-profile/edit-measurements/?tab=trainingplan">My Exercise Planner > </a><br /><br />
            Step 7   &nbsp; &nbsp; &nbsp; <a href="/club-vision/my-profile/edit-my-eating-planner/">My Eating Planner > </a><br /><br />
        
            <a href="/club-vision/my-profile/edit-picture-password/">Edit proﬁle picture > </a><br /><br />
            <a href="/club-vision/my-profile/edit-picture-password/?tab=password">Change password ></a><br /><br />
            <a href="/media/203403/how_to_complete_intitial_steps.pdf" target="_blank">How to Get Started Guide ></a><br /><br />
        </div></asp:Panel><br />
            
        
    </div>
  </div><!-- /eContent -->
  <div class="clear">&nbsp;</div>
</div><!-- /eProfileEdit -->
<script type="text/javascript">
    $(document).ready(function () {
        var sessionValue = '<%= Session["MemberType"] %>';
        if (sessionValue == 'VPT') {
            document.getElementById('rightsection').style.height = '400px';
        }
    });
</script>