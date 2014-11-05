<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShareDailyPlan.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.VVT.ShareDailyPlan" %>

<div id="eFoodDiary" class="element" runat="server" ClientIDMode="Static">
    
    <div id="visionLogoTabDay" style="float: left"></div>
    
    
<div class="mealMenuHeaderDiv">
    <div id="mealImgDiv" runat="server" class="mealMenuHeaderLefttSide">
        <img id="mealImg" alt="Meal Image" runat="server" src="/images/meals/meal_generic.jpg" width="200" height="200" />
    </div>
    <div id="mealTitleDiv" class="mealMenuHeaderRightSide">
        <div id="mealNameDiv" runat="server" class="mealOrMenuTitle" style="height:40%">Meal Name: </div>
        <div id="serveAndMacrodiv" class="mealOrMenuDetail" style="width: 52%;height:64%;">
            <div id="servediv">
                <img src="/images/icons/web/people.png"/>
                
                Serves <span id="servespan" runat="server" clientidmode="Static">15</span>
                <img src="/images/icons/web/edit-red.png" runat="server" Visible="False" id="iconEditServe" ClientIDMode="Static"
                     onclick="editMealServe(); return false;" 
                     alt="edit serve" class="editicons" title="Edit Serve"/>
            </div>
            <div id="macroperservediv">
                <img src="/images/icons/web/tag.png" />
                Macronutrient per serve (grams)
            </div>
            <div>
                <img src="/images/icons/web/tag.png" style="opacity: 0.0;" />
                Carbs:<span id="SpanCHOPortion" runat="server" clientidmode="Static">15</span>
                Protein:<span id="SpanPTNPortion" runat="server" clientidmode="Static">43</span>
                Fat:<span id="SpanFATPortion" runat="server" clientidmode="Static">12</span>
            </div>
        </div>
        <div id="descriptiondiv" class="mealOrMenuDetail" style="width: 44%;height:64%;">
            <div style="padding-left: 20px;">
                <img src="/images/icons/web/description.png" alt="description" /> Description 
                <img src="/images/icons/web/edit-red.png" id="iconEditDesc" runat="server" Visible="False"  ClientIDMode="Static" onclick="editMealDescription(); return false;" class="editicons" alt="edit description" title="Edit Meal Description"/>
                <div id="mealDescription" runat="server" clientidmode="Static">
                </div>
            </div>
        </div>
    </div>
</div>
<div class="mealMenuOptionsDiv">
    <div class="mealMenuOptionsWrapper">
        <div onclick="hide_one_of_nobuttons();$('.menuChosenDiv').slideUp();sharePlanOnFacebook();" style="border-left: none;">
            <div><img src="/images/icons/web/f.png" alt="picture"/></div>
            <div>Share to Facebook</div>
        </div>
        <div onclick="hide_one_of_nobuttons();$('.menuChosenDiv').slideUp();sharePlanOnTwitter();">
            <div><img src="/images/icons/web/twitter.png" alt="picture"/></div>
            <div>Share to Twitter</div>
        </div>
        <div ID="lnkDL" target="_blank" runat="server" Visible="False">
            <div><img src="/images/icons/web/download.png" alt="picture"/></div>
            <div>Download Recipe</div>
        </div>
        <div onclick="openMealMenuDialog('helpdiv_select', this.id); return false;">
            <div><img src="/images/icons/web/imark.png" alt="help"/></div>
            <div>Help</div>
        </div>
        <div onclick="popup_and_print()">
            <div><img src="/images/icons/web/print.png" alt="print"/></div>
            <div>Print</div>
        </div>
        <div onclick="openMealMenuDialog('copyToMyAccount_Select', this.id); return false;">
            <div><img src="/images/icons/web/savetomyaccount.png" alt="save to my account"/></div>
            <div>Copy to My Account</div>
        </div>
    </div>
</div>
<div class="menuChosenDiv">
    <div  style="display: none;"  id="helpdiv_select" class="one_of">
        <div class="quicktools_option" style="height:25px;text-align:left;vertical-align:middle;border: 0px;" onclick="window.open('/club-vision/recipe/portion_sizes.doc');">Portion Sizing</div>
        <div class="quicktools_option" style="height:25px;text-align:left;vertical-align:middle;border: 0px;" onclick="window.open('/club-vision/recipe/the_meaning_of_macros.docx');">Meaning of Macros</div>
    </div>
     <% if (Request.QueryString["code"].Substring(0,2) != "ml")
   { %>
    <div id="copyToMyAccount_Select" class="one_of"  style="display: none;" >
        <div class="quicktools_option" style="height:25px;text-align:left;vertical-align:middle;border: 0px;" onclick="copyShareEntryToMyFoodDiary();return false;">Copy to Food Diary</div>
        <div class="quicktools_option" style="height:25px;text-align:left;vertical-align:middle;border: 0px;" onclick="copyShareEntryToMenu();return false;">Copy to My Daily Plans</div>
    </div>
 <% }
   else
   { %>
   <div id="copyToMyAccount_Select" class="one_of"  style="display: none;" >
        <div class="quicktools_option" style="height:25px;text-align:left;vertical-align:middle;border: 0px;" onclick="copyShareEntryToMyFoodDiary();return false;">Copy to Food Diary</div>
        <div class="quicktools_option" style="height:25px;text-align:left;vertical-align:middle;border: 0px;" onclick="copyShareEntryToMeal();return false;">Copy to My Meals</div>
    </div>
<% } %>
    
    <div id="shareMealPlan_Select" style="display: none;" runat="server" clientidmode="Static" class="one_of">
        <p>Share to?</p>
        <div class="quicktools_option" style="height:25px;text-align:left;vertical-align:middle;" id="shareToFB" onclick="hide_one_of_nobuttons();sharePlanOnFacebook();"><img src="/images/fb.small.icon.jpg"/> &nbsp;&nbsp;&nbsp;Facebook</div>
        <div class="quicktools_option" style="height:25px;text-align:left;vertical-align:middle;" id="shareToEmail" onclick="hide_one_of_nobuttons();sharePlanOnTwitter();"><img src="/images/email_icon.png"/>&nbsp;&nbsp;&nbsp;Email</div>
    </div>
</div>
    
    
<div style="clear:both"></div>
<br />
<br />
<div class="result" id="resultPage" style="display: none"></div>
<table id="diary-entries" width="885" border="0" cellpadding="0" cellspacing="0">

<tr>
<td style="border-top: 1px solid #ffffff; padding-top: 15px; padding-bottom: 15px; font-size: 12px; font-weight: bold; text-align: center;">&nbsp;</td>
<td style="border-top: 1px solid #ffffff; padding-top: 15px; padding-bottom: 15px; font-size: 12px; font-weight: bold; text-align: center;">&nbsp;</td>
<td style="border-top: 1px solid #ffffff; padding-top: 15px; padding-bottom: 15px; font-size: 12px; font-weight: bold; text-align: center;">Amount</td>
<td style="border-top: 1px solid #ffffff; padding-top: 15px; padding-bottom: 15px; font-size: 12px; font-weight: bold; text-align: center;">Serve</td>
<td style="border-top: 1px solid #ffffff; padding-top: 15px; padding-bottom: 15px; font-size: 12px; font-weight: bold; text-align: center;">Carbohydrate(g)</td>
<td style="border-top: 1px solid #ffffff; padding-top: 15px; padding-bottom: 15px; font-size: 12px; font-weight: bold; text-align: center;">Protein(g)</td>
<td style="border-top: 1px solid #ffffff; padding-top: 15px; padding-bottom: 15px; font-size: 12px; font-weight: bold; text-align: center;">Fat(g)</td>
<td style="border-top: 1px solid #ffffff; padding-top: 15px; padding-bottom: 15px; font-size: 12px; font-weight: bold; text-align: center;">&nbsp;</td>
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


<tr id="macro-total" style="display:none">
<td style="padding-top: 15px;padding-left: 15px;" colspan="4">Total Macronutrient (from Food Diary Entry)</td>
<td style="padding-top: 15px;padding-left: 15px;text-align: center;font-weight: bold" class="carb-total"><asp:Literal ID="literalTotalCarb" runat="server"></asp:Literal></td>
<td style="padding-top: 15px;padding-left: 15px;text-align: center;font-weight: bold" class="protein-total"><asp:Literal ID="literalTotalProtein" runat="server"></asp:Literal></td>
<td style="padding-top: 15px;padding-left: 15px;text-align: center;font-weight: bold" class="fat-total"><asp:Literal ID="literalTotalFat" runat="server"></asp:Literal></td>
<td style="padding-top: 15px;padding-left: 15px;text-align: center;font-weight: bold">&nbsp;</td>
</tr>


</table>
    <div class="clear">&nbsp;</div>
</div>

<div id="divNotFound" class="element" runat="server" Visible="False" style="width: 902px;">
    <h1></h1>
</div>
  <div class="clear">&nbsp;</div>
<!--</div> /eFoodDiary -->

<asp:Literal ID="foodDiaryScript" runat="server"></asp:Literal>