<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RightPanel.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.clubvision.RightPanel" %>
<%@ Register TagPrefix="uc" TagName="DailyInspiration" Src="~/usercontrols/general/DailyInspiration.ascx" %>
<%@ Register TagPrefix="uc" TagName="QuickLinks" Src="~/usercontrols/general/QuickLinks.ascx" %>
<%@ Register TagPrefix="uc" Namespace="System.Reflection" Assembly="mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" %>

<div id="eProfileView" class="element"><!-- 290 -->
  <h3 class="replace">Profile View</h3>
  <div id="RightPanel"  class="eContent2 eOrange"> 
    <div class="profileCopy2">
        <div class="imageFrame">
            <div class="imageHolder">
                <asp:Literal ID="literalImage" runat="server"></asp:Literal>
            </div>
        </div>
        <div class="rightBottom">
           
            <br /><br />
            <uc:DailyInspiration ID="uc1" runat="server" />
            <uc:QuickLinks Id="uc2" runat="server" />
        </div>      
    </div>
   <div class="clear"></div>
  </div><!-- /eContent -->
  <div class="clear"></div>
</div><!-- /eProfileView -->