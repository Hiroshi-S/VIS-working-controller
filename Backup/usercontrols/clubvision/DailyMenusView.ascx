<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DailyMenusView.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.clubvision.DailyMenusView" %>
<div id="eDailyMenusView" class="element" runat="server"><!-- 605 -->
  <h3 class="replace">Daily Menus</h3>
  <div class="eContent eOrange"> 
  <a id="mycarousel-prev" href="#"><img src="/images/buttonPrev.png" alt="Prev" /></a>
    <a id="mycarousel-next" href="#"><img src="/images/buttonNext.png" alt="Next" /></a>
        <ul id="menus">
        <asp:Literal id="literalMenus" runat="server"></asp:Literal>
    </ul>    
    <div class="clear">&nbsp;</div>
  </div><!-- /eContent -->
  <div class="clear">&nbsp;</div>
</div><!-- /eDailyMenus -->

<script type="text/javascript">
    /**
    * We use the initCallback callback
    * to assign functionality to the controls
    */
    function mycarousel_initCallback(carousel) {
        jQuery('#mycarousel-next').bind('click', function () {
            carousel.next();
            return false;
        });
        jQuery('#mycarousel-prev').bind('click', function () {
            carousel.prev();
            return false;
        });
    };

    // Ride the carousel...
    jQuery(document).ready(function () {
        jQuery("#menus").jcarousel({
            scroll: 1,
            initCallback: mycarousel_initCallback,
            // This tells jCarousel NOT to autobuild prev/next buttons
            buttonNextHTML: null,
            buttonPrevHTML: null,
            wrap: "circular"
        });
    });
</script>
