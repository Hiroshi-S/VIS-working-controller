<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PasswordPictureEditTab.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.externalclubvision.PasswordPictureEditTab" %>
<%@ Register TagPrefix="uc" TagName="profilePicture" Src="~/usercontrols/clubvision/ProfilePictureEdit.ascx" %>
<%@ Register TagPrefix="uc" TagName="editPassword" Src="initialscreens/EditProfilePassword.ascx" %>

<div id="eProfileTab" class="element" style="overflow: visible"><!-- 605 -->
        <div class="replace" id="profileTab" runat="server" style="background-image: url(/images/ExtClubVision/eHdrProfilePictureEditTab.gif); ">
            <div id="tabProfilePicture" style="cursor: pointer; position: absolute; top: 0px; left: 0px; width: 201px; height: 45px" onclick="document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_PasswordPictureEditTab_4_profileTab').style.backgroundImage = 'url(/images/ExtClubVision/eHdrProfilePictureEditTab.gif)';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_PasswordPictureEditTab_4_eEditTabProfilePicture').style.display = 'block';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_PasswordPictureEditTab_4_eEditTabPassword').style.display = 'none';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_PasswordPictureEditTab_4_eUnsubscribeTab').style.display = 'none';"></div>
            <div id="tabPasswordEdit" style="cursor: pointer; position: absolute; top: 0px; left: 202px; width: 201px; height: 45px" onclick="document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_PasswordPictureEditTab_4_profileTab').style.backgroundImage = 'url(/images/ExtClubVision/eHdrPasswordEditTab.gif)';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_PasswordPictureEditTab_4_eEditTabProfilePicture').style.display = 'none';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_PasswordPictureEditTab_4_eEditTabPassword').style.display = 'block';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_PasswordPictureEditTab_4_eUnsubscribeTab').style.display = 'none';"></div>
            <div id="tabUnsubscribe" style="cursor: pointer; position: absolute; top: 0px; left: 404px; width: 201px; height: 45px" onclick="document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_PasswordPictureEditTab_4_profileTab').style.backgroundImage = 'url(/images/ExtClubVision/eHdrUnsubscribeTab.gif)';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_PasswordPictureEditTab_4_eEditTabProfilePicture').style.display = 'none';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_PasswordPictureEditTab_4_eEditTabPassword').style.display = 'none';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_PasswordPictureEditTab_4_eUnsubscribeTab').style.display = 'block';"></div>
        </div>

        <div class="clear"></div>

        <div class="eContent eOrange" id="eEditTabProfilePicture" style="height: 400px;" runat="server">
             <uc:profilePicture ID="ppicture" runat="server" />
        </div>
        
        <div class="eContent eOrange" id="eEditTabPassword" style="display: none; height: 400px;" runat="server">
            <uc:editPassword ID="epassword" runat="server"/>
         </div>  

          <div class="eContent eOrange" id="eUnsubscribeTab" style="display: none; height: 400px;" runat="server">
            <div class="leftCol" style="width: 500px !important;">
            <h2>Account Cancellation</h2>
            <p style="line-height: 2">
                We hope that you have been enjoying using Vision Virtual Training and getting your desired results.  
                We will be sorry to see you go and would disappointed for you to slip back into old habits so should you have any questions on 
                Vision Virtual Training please do contact us on <a href="mailto:admin@visionvirtualtraining.com">admin@visionvirtualtraining.com</a>, we will be happy to help you rather than have you cancel.
            <br/>
                However should you wish to cancel your subscription all you need to do is cancel your payments with Paypal.
            <br/>
                Please note that your subscription will remain active until the end of your current billing cycle. 
                <br/><br/>
                Healthy Regards,<br/>
                Vision Virtual Training Team
            </p>
                <br/><br/><br/>
                <A target="_blank" HREF="https://www.paypal.com/cgi-bin/webscr?cmd=_subscr-find&alias=D3XJTJYLZ5FMA">
                <IMG SRC="https://www.paypalobjects.com/en_AU/i/btn/btn_unsubscribe_LG.gif" BORDER="0">
                </A>
                </div>
         </div>        
</div><!-- /eProfileTab -->
<script type="text/javascript">
    $(document).ready(function () {
        var sessionValue = '<%= Session["MemberType"] %>';
        if (sessionValue == 'VPT') {
            document.getElementById('tabUnsubscribe').style.backgroundColor = 'white';
            document.getElementById('tabUnsubscribe').style.borderBottom = '1px solid #e27423';
            document.getElementById('tabUnsubscribe').style.cursor = 'default';
            document.getElementById('tabUnsubscribe').removeAttribute("onclick");
        }
    });
</script>