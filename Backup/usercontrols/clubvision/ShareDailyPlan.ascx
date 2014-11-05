<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShareDailyPlan.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.clubvision.ShareDailyPlan" %>

<div id="eFoodDiary" class="element">
    
    <div id="visionLogoTabDay" style="float: left"></div>
    <div style="font-weight: bold; font-size: 18px;float: left; padding-top: 4px; padding-right: 10px">
        <asp:Label ID="nameLabel" runat="server" Text="Label"></asp:Label>
    </div>
    
    <div style="font-size: 12px; padding-top: 30px; padding-right: 10px">
        <asp:Label ID="detailLabel" runat="server" Text="Label"></asp:Label>
    </div>
    
    <div style="float: right; text-align: right; padding-right: 11px; position: relative; padding-top: 0px;">
       
        
        <img src="/images/sharefacebook.jpg" style="cursor: pointer" alt="Share on Facebook" onclick="hide_one_of_nobuttons();sharePlanOnFacebook();"/>
        <img src="/images/tweetbutton__1_.jpg" style="cursor: pointer" alt="Share on Twitter" onclick="hide_one_of_nobuttons();sharePlanOnTwitter();"/>
        <img src="/images/buttonHelp.gif" style="cursor: pointer" alt="Copy to My Account" onclick="var result = toggle_str('helpdiv_select');hide_one_of_nobuttons();switchto('helpdiv_select', result);"/> 
        <img src="/images/buttonPrint.png" style="cursor: pointer" alt="Print" onclick="popup_and_print()"/> 
        <img src="/images/buttonCopytoMyAccount.gif" style="cursor: pointer" alt="Copy to My Account" onclick="var result = toggle_str('copyToMyAccount_Select');hide_one_of_nobuttons();switchto('copyToMyAccount_Select', result);"/> 
    </div>
    <% if (Request.QueryString["code"].Substring(0,2) != "ml")
   { %>
    <div style="right: 181px; margin-top: 28px; border: 1px solid #a0a0a0; display: none; position: absolute; width: 169px; height: 62px; padding-top: 6px; background-color: #f1f1f1; font-weight: bold;text-align: left;" id="copyToMyAccount_Select" class="one_of">
        <div class="quicktools_option" style="height:25px;text-align:left;vertical-align:middle;border: 0px;" onclick="copyShareEntryToMyFoodDiary();return false;">Copy to Food Diary</div>
        <div class="quicktools_option" style="height:25px;text-align:left;vertical-align:middle;border: 0px;" onclick="copyShareEntryToMenu();return false;">Copy to My Daily Plans</div>
    </div>
 <% }
   else
   { %>
   <div style="right: 181px; margin-top: 28px; border: 1px solid #a0a0a0; display: none; position: absolute; width: 169px; height: 62px; padding-top: 6px; background-color: #f1f1f1; font-weight: bold;text-align: left;" id="copyToMyAccount_Select" class="one_of">
        <div class="quicktools_option" style="height:25px;text-align:left;vertical-align:middle;border: 0px;" onclick="copyShareEntryToMyFoodDiary();return false;">Copy to Food Diary</div>
        <div class="quicktools_option" style="height:25px;text-align:left;vertical-align:middle;border: 0px;" onclick="copyShareEntryToMeal();return false;">Copy to My Meals</div>
    </div>
<% } %>
<div style="right: 452px; margin-top: 30px; border: 1px solid #a0a0a0; display: none; position: absolute; width: 128px; height: 62px; padding-top: 6px; background-color: #f1f1f1; font-weight: bold;text-align: left;" id="helpdiv_select" class="one_of">
        <div class="quicktools_option" style="height:25px;text-align:left;vertical-align:middle;border: 0px;" onclick="window.open('/club-vision/recipe/portion_sizes.doc');">Portion Sizing</div>
        <div class="quicktools_option" style="height:25px;text-align:left;vertical-align:middle;border: 0px;" onclick="window.open('/club-vision/recipe/the_meaning_of_macros.docx');">Meaning of Macros</div>
</div>
<div style="clear:both"></div>
<br />
<br />
<div class="result" id="resultPage" style="display: none"></div>
<table id="diary-entries" width="885" border="0" cellpadding="0" cellspacing="0">

<tr id="macro-header">
<td style="border-bottom: 1px solid #e5e5e5; border-top: 1px solid #e5e5e5; padding-top: 15px; padding-bottom: 10px; font-size: 14px; font-weight: bold; padding-left: 11px;" width="185">Macronutrients Total</td>
<td style="border-bottom: 1px solid #e5e5e5;border-top: 1px solid #e5e5e5; padding-top: 15px; padding-bottom: 15px; font-size: 12px; font-weight: bold; text-align: center;" width="184">&nbsp;</td>
<td style="border-bottom: 1px solid #e5e5e5;border-top: 1px solid #e5e5e5; padding-top: 15px; padding-bottom: 15px; font-size: 12px; font-weight: bold; text-align: center;" width="100">&nbsp;</td>
<td style="border-bottom: 1px solid #e5e5e5;border-top: 1px solid #e5e5e5; padding-top: 15px; padding-bottom: 15px; font-size: 12px; font-weight: bold; text-align: center;" width="100">&nbsp;</td>

<td style="border-bottom: 1px solid #e5e5e5; border-top: 1px solid #e5e5e5; padding-top: 15px; padding-bottom: 15px; font-size: 12px; font-weight: bold; text-align: center;" width="100">Carbohydrate(g)</td>
<td style="border-bottom: 1px solid #e5e5e5; border-top: 1px solid #e5e5e5; padding-top: 15px; padding-bottom: 15px; font-size: 12px; font-weight: bold; text-align: center;" width="100">Protein(g)</td>
<td style="border-bottom: 1px solid #e5e5e5; border-top: 1px solid #e5e5e5; padding-top: 15px; padding-bottom: 15px; font-size: 12px; font-weight: bold; text-align: center;" width="100">Fat(g)</td>
<td style="border-bottom: 1px solid #e5e5e5; border-top: 1px solid #e5e5e5; padding-top: 15px; padding-bottom: 15px; font-size: 12px; font-weight: bold; text-align: center;" width="16">&nbsp;</td>
</tr>

<tr id="macro-total">
<td style="padding-top: 15px;padding-left: 15px;" colspan="4">Total Macronutrient (from Food Diary Entry)</td>
<td style="padding-top: 15px;padding-left: 15px;text-align: center;font-weight: bold" class="carb-total"><asp:Literal ID="literalTotalCarb" runat="server"></asp:Literal></td>
<td style="padding-top: 15px;padding-left: 15px;text-align: center;font-weight: bold" class="protein-total"><asp:Literal ID="literalTotalProtein" runat="server"></asp:Literal></td>
<td style="padding-top: 15px;padding-left: 15px;text-align: center;font-weight: bold" class="fat-total"><asp:Literal ID="literalTotalFat" runat="server"></asp:Literal></td>
<td style="padding-top: 15px;padding-left: 15px;text-align: center;font-weight: bold">&nbsp;</td>
</tr>

<tr>
<td style="border-top: 1px solid #e5e5e5; padding-top: 15px; padding-bottom: 15px; font-size: 12px; font-weight: bold; text-align: center;">&nbsp;</td>
<td style="border-top: 1px solid #e5e5e5; padding-top: 15px; padding-bottom: 15px; font-size: 12px; font-weight: bold; text-align: center;">&nbsp;</td>
<td style="border-top: 1px solid #e5e5e5; padding-top: 15px; padding-bottom: 15px; font-size: 12px; font-weight: bold; text-align: center;">Amount</td>
<td style="border-top: 1px solid #e5e5e5; padding-top: 15px; padding-bottom: 15px; font-size: 12px; font-weight: bold; text-align: center;">Serve</td>
<td style="border-top: 1px solid #e5e5e5; padding-top: 15px; padding-bottom: 15px; font-size: 12px; font-weight: bold; text-align: center;">Carbohydrate(g)</td>
<td style="border-top: 1px solid #e5e5e5; padding-top: 15px; padding-bottom: 15px; font-size: 12px; font-weight: bold; text-align: center;">Protein(g)</td>
<td style="border-top: 1px solid #e5e5e5; padding-top: 15px; padding-bottom: 15px; font-size: 12px; font-weight: bold; text-align: center;">Fat(g)</td>
<td style="border-top: 1px solid #e5e5e5; padding-top: 15px; padding-bottom: 15px; font-size: 12px; font-weight: bold; text-align: center;">&nbsp;</td>
</tr>
<tr class="mealHeader">
<td style="border-bottom: 1px solid #e5e5e5; padding-top: 15px; padding-bottom: 10px; font-size: 14px; font-weight: bold; padding-left: 11px;" colspan="8" >Breakfast</td>
</tr>
<asp:Literal ID="literalBreakfastRows" runat="server"></asp:Literal>
<tr class="buttons mealtime1 mealHeader"  id="buttonsBreakfast">
<td>&nbsp;</td>
<td>&nbsp;</td>
<td>&nbsp;</td>
<td style="font-weight: bold; text-align: center">Subtotal</td>
<td style="font-weight: bold; text-align: center" id="carb-1"><asp:Literal ID="literalBreakfastCarb" runat="server"></asp:Literal></td>
<td style="font-weight: bold; text-align: center" id="protein-1"><asp:Literal ID="literalBreakfastProtein" runat="server"></asp:Literal></td>
<td style="font-weight: bold; text-align: center" id="fat-1"><asp:Literal ID="literalBreakfastFat" runat="server"></asp:Literal></td>
<td>&nbsp;</td>
</tr>
<tr class="mealHeader">
<td style="border-bottom: 1px solid #e5e5e5; padding-top: 15px; padding-bottom: 10px; font-size: 14px; font-weight: bold; padding-left: 11px;" colspan="8" >Morning Tea</td>
</tr>
<asp:Literal ID="literalMorningTeaRows" runat="server"></asp:Literal>
<tr class="buttons mealtime2 mealHeader"  id="buttonsMorningTea">
<td>&nbsp;</td>
<td>&nbsp;</td>
<td>&nbsp;</td>
<td style="font-weight: bold; text-align: center">Subtotal</td>
<td style="font-weight: bold; text-align: center" id="carb-2"><asp:Literal ID="literalMorningTeaCarb" runat="server"></asp:Literal></td>
<td style="font-weight: bold; text-align: center" id="protein-2"><asp:Literal ID="literalMorningTeaProtein" runat="server"></asp:Literal></td>
<td style="font-weight: bold; text-align: center" id="fat-2"><asp:Literal ID="literalMorningTeaFat" runat="server"></asp:Literal></td>
<td>&nbsp;</td>
</tr>
<tr class="mealHeader">
<td style="border-bottom: 1px solid #e5e5e5; padding-top: 15px; padding-bottom: 10px; font-size: 14px; font-weight: bold; padding-left: 11px;" colspan="8" >Lunch</td>
</tr>
<asp:Literal ID="literalLunchRows" runat="server"></asp:Literal>
<tr class="buttons mealtime3 mealHeader"  id="buttonsLunch">
<td>&nbsp;</td>
<td>&nbsp;</td>
<td>&nbsp;</td>
<td style="font-weight: bold; text-align: center">Subtotal</td>
<td style="font-weight: bold; text-align: center" id="carb-3"><asp:Literal ID="literalLunchCarb" runat="server"></asp:Literal></td>
<td style="font-weight: bold; text-align: center" id="protein-3"><asp:Literal ID="literalLunchProtein" runat="server"></asp:Literal></td>
<td style="font-weight: bold; text-align: center" id="fat-3"><asp:Literal ID="literalLunchFat" runat="server"></asp:Literal></td>
<td>&nbsp;</td>
</tr>
<tr class="mealHeader">
<td style="border-bottom: 1px solid #e5e5e5; padding-top: 15px; padding-bottom: 10px; font-size: 14px; font-weight: bold; padding-left: 11px;" colspan="8" >Afternoon Tea</td>
</tr>
<asp:Literal ID="literalAfternoonTeaRows" runat="server"></asp:Literal>
<tr class="buttons mealtime4 mealHeader"  id="buttonsAfternoonTea">
<td>&nbsp;</td>
<td>&nbsp;</td>
<td>&nbsp;</td>
<td style="font-weight: bold; text-align: center">Subtotal</td>
<td style="font-weight: bold; text-align: center" id="carb-4"><asp:Literal ID="literalAfternoonTeaCarb" runat="server"></asp:Literal></td>
<td style="font-weight: bold; text-align: center" id="protein-4"><asp:Literal ID="literalAfternoonTeaProtein" runat="server"></asp:Literal></td>
<td style="font-weight: bold; text-align: center" id="fat-4"><asp:Literal ID="literalAfternoonTeaFat" runat="server"></asp:Literal></td>
<td>&nbsp;</td>
</tr>
<tr class="mealHeader">
<td style="border-bottom: 1px solid #e5e5e5; padding-top: 15px; padding-bottom: 10px; font-size: 14px; font-weight: bold; padding-left: 11px;" colspan="8" >Dinner</td>
</tr>
<asp:Literal ID="literalDinnerRows" runat="server"></asp:Literal>
<tr class="buttons mealtime5 mealHeader"  id="buttonsDinner">
<td>&nbsp;</td>
<td>&nbsp;</td>
<td>&nbsp;</td>
<td style="font-weight: bold; text-align: center">Subtotal</td>
<td style="font-weight: bold; text-align: center" id="carb-5"><asp:Literal ID="literalDinnerCarb" runat="server"></asp:Literal></td>
<td style="font-weight: bold; text-align: center" id="protein-5"><asp:Literal ID="literalDinnerProtein" runat="server"></asp:Literal></td>
<td style="font-weight: bold; text-align: center" id="fat-5"><asp:Literal ID="literalDinnerFat" runat="server"></asp:Literal></td>
<td>&nbsp;</td>
</tr>
<tr class="mealHeader">
<td style="border-bottom: 1px solid #e5e5e5; padding-top: 15px; padding-bottom: 10px; font-size: 14px; font-weight: bold; padding-left: 11px;" colspan="8" >Supper</td>
</tr>
<asp:Literal ID="literalSupperRows" runat="server"></asp:Literal>
<tr class="buttons mealtime6 mealHeader"  id="buttonsSupper">
<td>&nbsp;</td>
<td>&nbsp;</td>
<td>&nbsp;</td>
<td style="font-weight: bold; text-align: center">Subtotal</td>
<td style="font-weight: bold; text-align: center" id="carb-6"><asp:Literal ID="literalSupperCarb" runat="server"></asp:Literal></td>
<td style="font-weight: bold; text-align: center" id="protein-6"><asp:Literal ID="literalSupperProtein" runat="server"></asp:Literal></td>
<td style="font-weight: bold; text-align: center" id="fat-6"><asp:Literal ID="literalSupperFat" runat="server"></asp:Literal></td>
<td>&nbsp;</td>
</tr>


</table>
    <div class="clear">&nbsp;</div>
</div>

  <div class="clear">&nbsp;</div>
<!--</div> /eFoodDiary -->

<asp:Literal ID="foodDiaryScript" runat="server"></asp:Literal>