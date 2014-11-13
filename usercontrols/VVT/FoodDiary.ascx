<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FoodDiary.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.VVT.FoodDiary" %>
<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls" TagPrefix="BDP" %>

<script type="text/javascript">
    /*
    window.onclick = function () {
        if ($(".search_results").is(':visible')
            && ($("#tabSearchByKeyword_Breakfast").is(':visible')
                || $("#tabSearchByKeyword_MorningTea").is(':visible')
                || $("#tabSearchByKeyword_Lunch").is(':visible')
                || $("#tabSearchByKeyword_AfternoonTea").is(':visible')
                || $("#tabSearchByKeyword_Dinner").is(':visible')
                || $("#tabSearchByKeyword_Supper").is(':visible'))) {
            $(".search_results").slideUp('fast');
        }
    };
    */
</script>
<%if (Session["MemberType"].Equals("VVT"))
  { %>
  <style>
      .quicktools_options
        {
         left: 241px; top: 29px; border: 1px solid #9b9b9b; position: absolute; width: 198px; height: 50px; background-color: #f1f1f1; display: none; z-index: 100;
        }
  </style>

<% } %>


<div id="eFoodDiary" class="element">
<% if (Request.QueryString["tab"] != "week")
   { %>
    <div class="replace" id="foodDiaryTab">
            <div id="tabDay" onclick="ReloadCurrentDate();">Day</div>
            <div id="tabWeek" onclick="ReloadCurrentWeek();">Week</div>
            <div style="width: 506px; border:none; border-bottom: 1px solid #cccccc;cursor: default;height:46px;background: none;"></div>
        </div>
  <div class="eContent eOrange"> 
  <div id="eFoodDiaryTabDay"style="padding-top: 55px; display: block; padding-left: 5px; padding-bottom: 5px;">
  <% }
   else
   { %>
    <div class="replace" id="foodDiaryTab">
            <div id="tabDay" onclick="ReloadCurrentDate();">Day</div>
            <div id="tabWeek" onclick="ReloadCurrentWeek();">Week</div>
            <div style="width: 506px; border:none; border-bottom: 1px solid #cccccc;cursor: default;height:46px;background: none;"></div>
        </div>
  <div class="eContent eOrange"> 
  <div id="eFoodDiaryTabDay"style="padding-top: 55px; display: block; padding-left: 5px; padding-bottom: 5px;display: none;">
   <% } %>
   <div id="visionLogoTabDay" style="float: left"></div>
   <div class="dateTitleFoodDiary">
       <div><asp:ImageButton runat="server" ID="buttonDayPrev" 
        onclick="buttonDayPrev_Click" ImageUrl="/images/icons/Web/prevarrow.png" style="float: left" height="30"/></div>
        <div><p style="display: inline-block; padding: 2px 14px; font-size: 20px;"><asp:Literal runat="server" ID="literalDay"></asp:Literal></p></div>
        <div><asp:ImageButton runat="server" ID="buttonDayNext" 
        onclick="buttonDayNext_Click" ImageUrl="/images/icons/Web/nextarrow.png" style="float: right" height="30"/></div>
        <div style="margin-left: 20px;" class="cal_controls"><BDP:BasicDatePicker ID="bdpDay" runat="server" DisplayType="Image" OnSelectionChanged="bdpDay_SelectionChanged"
        AutoPostBack="True" 
        ShowNoneButton="False" 
           ShowTodayButton="False" 
            ButtonImageFileName="calendar.png" ButtonImageHeight="30" 
            RenderImages="File" ResourcePath="/images/icons/web/" 
            DownYearSelectorImageFileName="" NextMonthImageFileName="nextarrow.png" 
            PrevMonthImageFileName="prevarrow.png" RenderStyleSheet="None" TextBoxStyle-BackColor="White" TextBoxStyle-BorderColor="#999999" TextBoxStyle-BorderStyle="Solid" TextBoxStyle-BorderWidth="1px" TextBoxStyle-Height="23px" TextBoxStyle-Width="123px">
</BDP:BasicDatePicker></div>

   </div>
   
   
<div class="mealMenuOptionsDiv">
    <div class="mealMenuOptionsWrapper">
    <div style="width: 145px;border-left: none;">
        <div class="fcimage">
            <a href="/club-vision/my-eating/menus/"><img src="/images/icons/web/viewmenu.png" style="cursor: pointer" alt="View Menu" /></a>
        </div>
        <div class="fctext">View Existing Daily Plans</div>
    </div>
    <!--<div onclick="var result = toggle_str('saveAsDailyMenu_Select');hide_one_of();switchto('saveAsDailyMenu_Select', result);" style="width: 145px;"> -->
    <div onclick="openMealMenuDialog('saveAsDailyMenu_Select', this.id); return false;" style="width: 145px;">
        <div class="fcimage">
            <img src="/images/icons/web/savetomyaccount.png" alt="View Menu" />
        </div>
        <div class="fctext">Save to My Daily Plans</div>
    </div>
    <div onclick="openMealMenuDialog('shareDailyPlan_Select', this.id); return false;">
        <div class="fcimage" >
            <img src="/images/icons/web/closedenvelope.png" alt="Share" id="imgShare"/>  
        </div>
        <div class="fctext">
            Share
        </div>
    </div>
    <div id="foodDiaryDayCheck" onclick="FoodDiaryDidYouYesOrNo();return false;" runat="server" >
        <div class="fcimage" >
            <img src="/images/icons/web/unchecked.jpg" alt="Check" id="foodDiaryDayCheckImg" runat="server" clientidmode="Static" data-value="False"/>  
        </div>
        <div class="fctext">
            Done This Plan?
        </div>
    </div>
    <div onclick="popup_and_print()">
        <div class="fcimage">
            <img src="/images/icons/web/print.png" alt="Print"  /> 
        </div>
        <div class="fctext">Print</div>
    </div>
    <div id="imgreset">
        <div class="fcimage">
            <img src="/images/icons/web/delete.png" alt="Reset"  />
        </div>
        <div class="fctext">Delete</div>
    </div>
    </div>
</div>
    
<div class="menuChosenDiv">
        <% if (Session["Trainer"].Equals("No") && Session["MemberType"].Equals("VPT"))
           { %>
        <div style="display:none;" id="shareDailyPlan_Select" class="one_of">
            <p>Share to?</p>
            <div class="quicktools_option" style="height:25px;text-align:left;vertical-align:middle;" onclick="ShareDailyPlanForClient('facebook')"><img src="/images/fb.small.icon.jpg"/> &nbsp;&nbsp;&nbsp;Facebook</div>
            <div class="quicktools_option" style="height:25px;text-align:left;vertical-align:middle;" onclick="ShareDailyPlanForClient('twitter')"><img src="/images/twitter-icon.png"/> &nbsp;&nbsp;&nbsp;Twitter</div>
            <div class="quicktools_option" style="height:25px;text-align:left;vertical-align:middle;" onclick="ShareDailyPlan('trainer')"><img src="/images/trainer.small.icon.jpg"/>&nbsp;&nbsp;&nbsp;My Trainer</div>
            <div class="quicktools_option" style="height:25px;text-align:left;vertical-align:middle;" onclick="ShareDailyPlan('VVTHQ')"><img src="/images/favicon002.png" height="21" width="21"/>&nbsp;&nbsp;&nbsp;Vision HQ</div>
            <div class="quicktools_option" style="height:25px;text-align:left;vertical-align:middle;" onclick="enterEmailToShare('ShareDailyPlan(email)')" ><img src="/images/email_icon.png"/>&nbsp;&nbsp;&nbsp;Email</div>
        </div>
         <% }
           if (Session["Trainer"].Equals("Yes"))
           { %>
        <div style="display:none;" id="shareDailyPlan_Select" class="one_of">
        <p>Share to?</p>
            <div class="quicktools_option" style="height:25px;text-align:left;vertical-align:middle;" onclick="ShareDailyPlan('facebook')"><img src="/images/fb.small.icon.jpg"/> &nbsp;&nbsp;&nbsp;Facebook</div>
            <div class="quicktools_option" style="height:25px;text-align:left;vertical-align:middle;" onclick="ShareDailyPlan('twitter')"><img src="/images/twitter-icon.png"/> &nbsp;&nbsp;&nbsp;Twitter</div>
            <div class="quicktools_option" style="height:25px;text-align:left;vertical-align:middle;" onclick="ShareDailyPlan('clients')"><img src="/images/trainer.small.icon.jpg"/>&nbsp;&nbsp;&nbsp;My Clients</div>
            <div class="quicktools_option" style="height:25px;text-align:left;vertical-align:middle;" onclick="ShareDailyPlan('VVTHQ')"><img src="/images/favicon002.png" height="21" width="21"/>&nbsp;&nbsp;&nbsp;Vision HQ</div>
            <div class="quicktools_option" style="height:25px;text-align:left;vertical-align:middle;" onclick="enterEmailToShare('ShareDailyPlan(email)')" ><img src="/images/email_icon.png"/>&nbsp;&nbsp;&nbsp;Email</div>
        </div>
        <% }
        if (Session["MemberType"].Equals("VVT"))
           { %>
        <div style="display:none;" id="shareDailyPlan_Select" class="one_of">
        <p>Share to?</p>
            <div class="quicktools_option" style="height:25px;text-align:left;vertical-align:middle;" onclick="ShareDailyPlanForClient('facebook')"><img src="/images/fb.small.icon.jpg"/> &nbsp;&nbsp;&nbsp;Facebook</div>
            <div class="quicktools_option" style="height:25px;text-align:left;vertical-align:middle;" onclick="ShareDailyPlan('clients')"><img src="/images/trainer.small.icon.jpg"/>&nbsp;&nbsp;&nbsp;My Clients</div>
            <div class="quicktools_option" style="height:25px;text-align:left;vertical-align:middle;" onclick="ShareDailyPlan('VVTHQ')"><img src="/images/favicon002.png" height="21" width="21"/>&nbsp;&nbsp;&nbsp;Vision HQ</div>
            <div class="quicktools_option" style="height:25px;text-align:left;vertical-align:middle;" onclick="enterEmailToShare('ShareDailyPlan(email)')" ><img src="/images/email_icon.png"/>&nbsp;&nbsp;&nbsp;Email</div>
        </div>
        <% } %>
        <div style="display: none;" id="saveAsDailyMenu_Select" class="one_of">
            Name this Daily Plan
            <input type="text" id="saveAsDailyMenu_Name" style="width: 172px; height: 23px; margin-top: 10px; margin-left: 10px; border: 1px solid #999999" />
            <br/>
            <div style="cursor: pointer;color: #e27423;"  onclick="hide('saveAsDailyMenu_Select');saveAsDailyMeal($('#saveAsDailyMenu_Name').val(),'resultPage');"><img src="/images/buttonSaveMenu.gif" /></div>
          
        </div>
        
    </div>

<input type="hidden" id="currentDate" runat="server" value="" />
<div id="captainIcon" runat="server" clientidmode="Static">
    <a title=" " style="cursor: pointer; position: absolute; top: 230px; left: 40px;">
        <img id="captainImage" src="/images/vpt_captainaccountabilityV2.jpg" alt="captain" /> 
    </a>
</div>

<div style="clear:both"></div>
<div class="result" id="resultPage" style="display: none"></div>
<table id="diary-entries" width="885" border="0" cellpadding="0" cellspacing="0" style="margin-top: 20px;">
<tr id="macro-header">
<td style="border-bottom: 1px solid #e5e5e5;border-top: none; padding-top: 15px; padding-bottom: 10px; font-size: 14px; font-weight: bold; padding-left: 11px;" width="185">Macronutrients Total</td>
<td style="border-bottom: 1px solid #e5e5e5;border-top: none; padding-top: 15px; padding-bottom: 15px; font-size: 12px; font-weight: bold; text-align: center;" width="184">&nbsp;</td>
<td style="border-bottom: 1px solid #e5e5e5;border-top: none; padding-top: 15px; padding-bottom: 15px; font-size: 12px; font-weight: bold; text-align: center;" width="100">&nbsp;</td>
<td style="border-bottom: 1px solid #e5e5e5;border-top: none; padding-top: 15px; padding-bottom: 15px; font-size: 12px; font-weight: bold; text-align: center;" width="100">&nbsp;</td>

<td style="border-bottom: 1px solid #e5e5e5;border-top: none; padding-top: 15px; padding-bottom: 15px; font-size: 12px; font-weight: bold; text-align: center;" width="100">Carbohydrate(g)</td>
<td style="border-bottom: 1px solid #e5e5e5;border-top: none; padding-top: 15px; padding-bottom: 15px; font-size: 12px; font-weight: bold; text-align: center;" width="100">Protein(g)</td>
<td style="border-bottom: 1px solid #e5e5e5;border-top: none; padding-top: 15px; padding-bottom: 15px; font-size: 12px; font-weight: bold; text-align: center;" width="100">Fat(g)</td>
<td style="border-bottom: 1px solid #e5e5e5;border-top: none; padding-top: 15px; padding-bottom: 15px; font-size: 12px; font-weight: bold; text-align: center;" width="16">&nbsp;</td>
</tr>
<tr id="macro-goal">
<td style="padding-top: 15px;padding-left: 15px;" colspan="4" id="dailyGoalText" runat="server">Daily Macronutrient Goals</td>
<td style="padding-top: 15px;padding-left: 15px;text-align: center;font-weight: bold" class="carb-goal"><asp:Literal ID="literalGoalCarb" runat="server"></asp:Literal></td>
<td style="padding-top: 15px;padding-left: 15px;text-align: center;font-weight: bold" class="protein-goal"><asp:Literal ID="literalGoalProtein" runat="server"></asp:Literal></td>
<td style="padding-top: 15px;padding-left: 15px;text-align: center;font-weight: bold" class="fat-goal"><asp:Literal ID="literalGoalFat" runat="server"></asp:Literal></td>
<td style="padding-top: 15px;padding-left: 15px;text-align: center;font-weight: bold">&nbsp;</td>
</tr>
<tr id="macro-goal-incompleteIS2" style="display: none;">
<td style="padding-top: 15px;padding-left: 15px;" colspan="4" id="Td2">Daily Macronutrient Goals</td>
<td style="padding-top: 15px;padding-left: 15px;text-align: center;font-weight: bold;color: red;">??</td>
<td style="padding-top: 15px;padding-left: 15px;text-align: center;font-weight: bold;color: red;">??</td>
<td style="padding-top: 15px;padding-left: 15px;text-align: center;font-weight: bold;color: red;">??</td>
<td style="padding-top: 15px;padding-left: 15px;text-align: center;font-weight: bold">&nbsp;</td>
</tr>
<tr id="macro-total">
<td style="padding-top: 15px;padding-left: 15px;" colspan="4">Total Macronutrient (from Food Diary Entry)</td>
<td style="padding-top: 15px;padding-left: 15px;text-align: center;font-weight: bold" class="carb-total"><asp:Literal ID="literalTotalCarb" runat="server"></asp:Literal></td>
<td style="padding-top: 15px;padding-left: 15px;text-align: center;font-weight: bold" class="protein-total"><asp:Literal ID="literalTotalProtein" runat="server"></asp:Literal></td>
<td style="padding-top: 15px;padding-left: 15px;text-align: center;font-weight: bold" class="fat-total"><asp:Literal ID="literalTotalFat" runat="server"></asp:Literal></td>
<td style="padding-top: 15px;padding-left: 15px;text-align: center;font-weight: bold">&nbsp;</td>
</tr>
<tr id="macro-diff" style="background-color: none;" >
<td style="padding-top: 15px;padding-left: 15px;" colspan="4">Difference to Macronutrient</td>
<td id="carb-diff" style="padding-top: 15px;padding-left: 15px;text-align: center;font-weight: bold" class="carb-diff"><asp:Literal ID="literalDiffCarb" runat="server"></asp:Literal></td>
<td id="ptn-diff" style="padding-top: 15px;padding-left: 15px;text-align: center;font-weight: bold" class="protein-diff"><asp:Literal ID="literalDiffProtein" runat="server"></asp:Literal></td>
<td id="fat-diff" style="padding-top: 15px;padding-left: 15px;text-align: center;font-weight: bold" class="fat-diff"><asp:Literal ID="literalDiffFat" runat="server"></asp:Literal></td>
<td style="padding-top: 15px;padding-left: 15px;text-align: center;font-weight: bold">&nbsp;</td>
</tr>
<tr id="macro-goal-incompleteIS3" style="display: none;">
<td style="padding-top: 15px;padding-left: 15px;" colspan="4" id="Td4">Difference to Macronutrient</td>
<td style="padding-top: 15px;padding-left: 15px;text-align: center;font-weight: bold;color: red;">??</td>
<td style="padding-top: 15px;padding-left: 15px;text-align: center;font-weight: bold;color: red;">??</td>
<td style="padding-top: 15px;padding-left: 15px;text-align: center;font-weight: bold;color: red;">??</td>
<td style="padding-top: 15px;padding-left: 15px;text-align: center;font-weight: bold">&nbsp;</td>
</tr>
<tr id="macro-accel" style="display: none;" >
<td style="padding-top: 15px;padding-left: 15px;color: #008CA7;font-weight: bold; font-size: 14px" colspan="8">Today is your Accelerator Day. Have you done it?</td>

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
<tr>
<td style="border-bottom: 1px solid #e5e5e5; padding-top: 15px; padding-bottom: 10px; font-size: 14px; font-weight: bold; padding-left: 11px;" colspan="8" >Breakfast<span class="balloonright"><asp:Literal ID="LiteralBreakfastMood" runat="server"></asp:Literal></span></td>
</tr>
<asp:Literal ID="literalBreakfastRows" runat="server"></asp:Literal>
<tr class="buttons mealtime1"  id="buttonsBreakfast">
<td style="padding-top: 5px" colspan="2"><div style="position: relative">
<div style="cursor: pointer; display: inline" id="addItemBreakfast" onclick="hide_one_of();show('addItemBreakfast_Select');hide('buttonsBreakfast');"><img src="/images/buttonAddItem.gif" /></div>
<div style="cursor: pointer; display: inline" id="addMealBreakfast" onclick="hide_one_of();show('addMealBreakfast_Select');hide('buttonsBreakfast');"><img src="/images/buttonAddMeal.gif" /></div>
<div style="cursor: pointer; display: inline; margin-left: 13px" id="quickToolsBreakfast" onclick="var result = toggle_str('quickToolsBreakfast_Select');hide_one_of();switchto('quickToolsBreakfast_Select', result);"><img src="/images/buttonQuickTools.gif" /></div>

<div class="quicktools_options one_of" id="quickToolsBreakfast_Select">
    <div class="quicktools_option" onclick="hide_one_of();show('quickToolsBreakfast_SaveAsMeal_Select');">Save as meal</div>
    <div class="quicktools_option" onclick="hide_one_of();show('quickToolsBreakfast_CopyAndPaste_Select');">Copy &amp; paste</div>
</div>

<div class="one_of" style="z-index: 100; left: 114px; top: 29px; border: 1px solid #a0a0a0; display: none; position: absolute; width: 186px; height: 92px; padding-top: 6px; padding-left: 12px; background-color: #f1f1f1; font-weight: bold" id="quickToolsBreakfast_SaveAsMeal_Select">
    <p>Name this meal</p>
    <input type="text" id="saveAsMealName_Breakfast" style="width: 172px; height: 23px; margin-top: 10px; border: 1px solid #999999" />
    <div style="float: left; cursor: pointer; padding-top: 5px;"  onclick="hide('quickToolsBreakfast_SaveAsMeal_Select');saveDiaryMealAsMeal(1,$('#saveAsMealName_Breakfast').val(),'resultBreakfast');"><img src="/images/buttonSaveMeal.gif" /></div>
    <div style="float: left; cursor: pointer; padding-top: 10px; padding-left: 21px; color: #e27423"  onclick="hide('quickToolsBreakfast_SaveAsMeal_Select');">or Cancel</div>
</div>
<div class="one_of" style="z-index: 100;left: 114px; top: 29px; border: 1px solid #a0a0a0; display: none; position: absolute; width: 198px; height: 200px; background-color: #f1f1f1" id="quickToolsBreakfast_CopyAndPaste_Select">
    <div class="quicktools_copyto_header">Copy to which date?</div>
    <% =CopyToDates(1, "quickToolsBreakfast_CopyAndPaste_Select", "resultBreakfast")%>
</div>
<div class="one_of" style="z-index: 100; left: 114px; top: 29px; border: 1px solid #a0a0a0; display: none; position: absolute; width: 236px; height: 328px;  padding-top: 6px; padding-left: 12px; background-color: #f1f1f1" id="quickToolsBreakfast_AddNewFood_Select">
    <p style="font-weight: bold">Food Item Request</p>
    <p style="padding-top: 10px">If you would like a food item to be added to the database please fill in the form below. Please note that only foods with a brand and product name can be added to the main food database.
</p>
    <input type="text" id="addNewFood_BreakfastText1" style="width: 224px" placeholder="Brand Name (optional)"/>
        <input type="text" id="addNewFood_BreakfastText2" style="width: 224px" placeholder="Product Name (Required)"/>
        <input type="text" id="addNewFood_BreakfastText3" style="width: 224px"  placeholder="Serving Size (optional)"/>
        <input type="text" id="addNewFood_BreakfastText4" style="width: 224px"  placeholder="Serving Unit (optional)" />
        <input type="text" id="addNewFood_BreakfastText5" style="width: 224px"  placeholder="Carbohydrates per serving (optional)" />
        <input type="text" id="addNewFood_BreakfastText6" style="width: 224px"  placeholder="Protein per serving (optional)"/>
        <input type="text" id="addNewFood_BreakfastText7" style="width: 224px"  placeholder="Fat per serving (optional)"/> 
    <div style="float: left; cursor: pointer; padding-top: 5px;" onclick="hide('quickToolsBreakfast_AddNewFood_Select');foodItemRequest('Brand= ' + $('#addNewFood_BreakfastText1').val()+ 
                                                                                                                    '. product = ' + $('#addNewFood_BreakfastText2').val()+ 
                                                                                                                    '. ServSize= ' +  $('#addNewFood_BreakfastText3').val() +
                                                                                                                    '. ServUnit = ' +$('#addNewFood_BreakfastText4').val() +
                                                                                                                    '. Carb= ' + $('#addNewFood_BreakfastText5').val()+
                                                                                                                    '. Ptn= ' + $('#addNewFood_BreakfastText6').val() +
                                                                                                                    '. Fat= ' + $('#addNewFood_BreakfastText7').val(),'resultBreakfast', 'false');"><img src="/images/buttonSendRequest.gif" /></div>
    <div style="float: left; cursor: pointer; padding-top: 10px; padding-left: 21px; color: #e27423"  onclick="hide('quickToolsBreakfast_AddNewFood_Select');">or Cancel</div>

</div>
</div>


</td>
<td>&nbsp;</td>
<td style="font-weight: bold; text-align: center">Subtotal</td>
<td style="font-weight: bold; text-align: center" id="carb-1"><asp:Literal ID="literalBreakfastCarb" runat="server"></asp:Literal></td>
<td style="font-weight: bold; text-align: center" id="protein-1"><asp:Literal ID="literalBreakfastProtein" runat="server"></asp:Literal></td>
<td style="font-weight: bold; text-align: center" id="fat-1"><asp:Literal ID="literalBreakfastFat" runat="server"></asp:Literal></td>
<td>&nbsp;</td>
</tr>
<tr><td colspan="8">
<div style="border: 1px solid #d1d1d1; display: none; width: 883px; background-color: #ececec" id="addItemBreakfast_Select" class="one_of">
<div style="cursor: pointer; display: inline" id="addItemBreakfastSelected" onclick="hide('addItemBreakfast_Select');show('buttonsBreakfast');"><img src="/images/buttonAddItemSelected.gif" /></div>
<div class="additem_options">
    <div class="additem_option additem_option_selected"
         onclick="additem_option_selected(this);
                  show('addItemBreakfast_Search_Select');
                  hide('addItemBreakfast_CopyPaste_Select');
                  hide('addItemBreakfast_Recent_Select');
                  hide('addItemBreakfast_AddFood_Select');
                  clearRight();">Search & Add</div>
    <div class="additem_option" onclick="additem_option_selected(this);
                                         hide('addItemBreakfast_Search_Select');
                                         show('addItemBreakfast_CopyPaste_Select');
                                         hide('addItemBreakfast_Recent_Select');
                                         hide('addItemBreakfast_AddFood_Select');">Copy &amp; Paste</div>
    <div class="additem_option" onclick="additem_option_selected(this);
                                         hide('addItemBreakfast_Search_Select');
                                         hide('addItemBreakfast_CopyPaste_Select');
                                         show('addItemBreakfast_Recent_Select');
                                         hide('addItemBreakfast_AddFood_Select');">Recent</div>
    <%--<div class="additem_option" onclick="additem_option_selected(this);hide('addItemBreakfast_Search_Select');hide('addItemBreakfast_CopyPaste_Select');hide('addItemBreakfast_Recent_Select');show('addItemBreakfast_AddFood_Select');">Add Your Own Food</div>--%>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;margin-bottom: 10px;padding-bottom: 10px" id="addItemBreakfast_Search_Select">
    <div id="tabSearchByKeyword_Breakfast">
        <div style="font-weight: bold">
           <div style="font-weight: bold;color: #008CA7;">Keyword</div>
        </div>
        <div style="float:left;">
            <input type="text" id="searchByKeyword_Breakfast" style="height: 23px; width: 345px; border: 1px solid #999999; margin-top: 11px;padding-left: 35px"
                   onkeyup="searchItemsByAdvancedKeyword('Breakfast', 1);"/>
            <div style="position: absolute; top: 45px; left: 23px; cursor: pointer;"
                 onclick="searchItemsByAdvancedKeyword('Breakfast', 1);">
                          <img src="/images/buttonSearchMag.gif" />
            </div>

            <div style="font-weight: bold;color: #008CA7;margin-top: 15px;">Macronutrients</div>
            <div style="font-weight: bold; padding-top: 12px;">
                Carbs:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchByCarbs_Breakfast" onkeyup="searchItemsByAdvanced('Breakfast', 1);"/> 
                Protein:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchByProtein_Breakfast" onkeyup="searchItemsByAdvanced('Breakfast', 1);" /> 
                Fat:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchByFat_Breakfast" onkeyup="searchItemsByAdvanced('Breakfast', 1);"/>
            </div>
            
            <div style="font-weight: bold;color: #008CA7;margin-top: 15px;">Category</div>
            <div style="font-weight: bold; padding-top: 12px;margin-bottom: 15px;">
                <asp:Literal ID="literalSearchByCategory_Breakfast" runat="server"></asp:Literal>
            </div>
        </div>
        <!--Hiroshi-->
        <!--Added newly-->
        <div class="rightSide" id="rightBreakfast"><!--button click takes to add new item panel-->
            <span style="display:inline-block;vertical-align:super;">Still can't find what you are looking for? Add new item</span>
            <input type="button" class="imgButtonAddItem" onclick="hide('addItemBreakfast_Search_Select');
                                                                   hide('addItemBreakfast_CopyPaste_Select');
                                                                   hide('addItemBreakfast_Recent_Select');
                                                                   show('addItemBreakfast_AddFood_Select');
                                                                   $('#foodName').val($('#searchByKeyword_Breakfast').val());
                                                                   $('.search_results').hide();"/>
        </div>
        <div style="clear:both;"></div>
        <!--end-->
        <div id="searchByKeyword_Breakfast_Result" class="search_results" style="display: none"></div>
    </div>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;display:none; margin-bottom: 10px;padding-bottom: 10px" id="addItemBreakfast_CopyPaste_Select">
    <div style="float: left; width: 200px"><span style="font-weight: bold">Select a date to copy from: </span><BDP:BasicDatePicker ID="bdpBreakfast" runat="server" DisplayType="Image" OnClientAfterSelectionChanged="getItemsCopyPaste"
        AutoPostBack="False" 
        ShowNoneButton="False" 
           ShowTodayButton="False" 
            ButtonImageFileName="calendar.gif" ButtonImageHeight="25" 
            ButtonImageWidth="26" RenderImages="File" ResourcePath="/images/" 
            DownYearSelectorImageFileName="" NextMonthImageFileName="/images/icons/Web/nextarrow.png" 
            PrevMonthImageFileName="/images/icons/Web/prevarrow.png" RenderStyleSheet="None" TextBoxStyle-BackColor="White" TextBoxStyle-BorderColor="#999999" TextBoxStyle-BorderStyle="Solid" TextBoxStyle-BorderWidth="1px" TextBoxStyle-Height="23px" TextBoxStyle-Width="123px">
    </BDP:BasicDatePicker>
    </div>
    <div style="float: left; width: 610px; border-left: 1px solid #e5e5e5; min-height: 200px; padding-left: 30px">
    <div id="addItemBreakfast_CopyPaste_Results"></div>
    <p style="padding-top: 18px">Copy to which date: <% =CopyToDatesDropDown("copyToDateBreakfast")%></p>
    <div style="cursor: pointer; padding-top: 12px"  onclick="addCheckedItemsToDate('result_area_1', 'copyToDateBreakfast');"><img src="/images/buttonCopyPaste.gif"></div>
    </div>
    <div style="clear: both"></div>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;display:none; margin-bottom: 10px;padding-bottom: 10px" id="addItemBreakfast_Recent_Select">
    <select onchange="getRecentByMealtime($('#recentSearchBreakfast').val(), 'addItemBreakfast_Recent_Results', 1)" id="recentSearchBreakfast">
        <option value="0"></option>
        <option value="1">Breakfast</option>
        <option value="2">Morning Tea</option>
        <option value="3">Lunch</option>
        <option value="4">Afternoon Tea</option>
        <option value="5">Dinner</option>
        <option value="6">Supper</option>
        <option value="-1">All Meals</option>
    </select>
    <div id="addItemBreakfast_Recent_Results" class="search_results_recent"></div>
    </div>
 
<div id="addItemBreakfast_AddFood_Select" style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;display:none; margin-bottom: 10px;padding-bottom: 10px">
    <table>
        <tr style="font-weight: bold">            
            <td>Food Name</td>
            <td style="padding-right: 10px">Brand Name</td>
            <td style="padding: 0px 25px">Amount</td>
            <td style="padding: 0px 21px">Serve</td>
            <td style="padding-left: 4px; padding-right: 20px;">Carbohydrate(g)</td> 
            <td>Protein(g)</td>
            <td style="padding-left: 38px ">Fat(g)</td>           
        </tr>
        <tr><td><input type="text" id="foodName" style="width: 180px" disabled/></td>
            <td style="padding-right: 10px"><input type="text" id="brand" placeholder="optional"/></td>           
            <td style="padding: 0px 25px"><input type="text" id="amount" style="width: 48px"/></td>
            <td style="padding: 0px 21px"><input type="text" id="serve" style="width: 50px" /></td>
            <td style="padding-left: 4px; padding-right: 20px; "><input type="text" id="carbs" style="width: 50px;margin-left: 10px"/></td>
            <td><input type="text" id="ptn" style="width: 50px"/></td>
            <td style="padding-left: 25px "><input type="text" id="fat" style="width: 50px"/></td>
        </tr>
    </table>
    <i>Please note: Amount, Carbs, Protein and Fat entries are numerical only</i>
    <br/>
    <div style="float:left;cursor: pointer; display: inline" id="addOwdFoodBreakfast"
         onclick="addOwnItem('1',$('#brand').val(),$('#foodName').val(), $('#amount').val(), $('#serve').val(), $('#carbs').val(),$('#ptn').val(), $('#fat').val());">
        <br/>
        <img src="/images/buttonSubmitOrange.gif" alt="Add Item" />
    </div>
    <div class="cancel" style="float:left;">
        <br />
        <img src="/images/buttonCancel.gif" alt="Cancel" />
    </div>
    <div style="clear:both;"></div>
</div>
</div>

<div style="border: 1px solid #d1d1d1; display: none; width: 883px; background-color: #ececec" id="addMealBreakfast_Select" class="one_of">
<div style="cursor: pointer; display: inline" id="addMealBreakfastSelected" onclick="hide('addMealBreakfast_Select');show('buttonsBreakfast');"><img src="/images/buttonAddMealSelected.gif" /></div>
<div class="additem_options">
    <div class="additem_option additem_option_selected" onclick="additem_option_selected(this);show('addMealBreakfast_Search_Select');hide('addMealBreakfast_Recent_Select');">Search</div>
    <div class="additem_option" onclick="additem_option_selected(this);hide('addMealBreakfast_Search_Select');show('addMealBreakfast_Recent_Select');">Recent</div>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;margin-bottom: 10px;padding-bottom: 10px" id="addMealBreakfast_Search_Select">
    <div id="tabSearchMealByKeyword_Breakfast">
        <div style="font-weight: bold;color: #008CA7;">Keyword</div>
        <input type="text" id="searchMealByKeyword_Breakfast" style="height: 23px; width: 345px; border: 1px solid #999999; margin-top: 11px;padding-left: 35px"
               onkeyup="searchMealsByAdvancedKeyword('Breakfast', 1);" />
        <div style="position: absolute; top: 45px; left: 23px; cursor: pointer;"
             onclick="searchMealsByAdvancedKeyword('Breakfast', 1);"><img src="/images/buttonSearchMag.gif" /></div>

        <div class="clear"></div>

        <div style="font-weight: bold;color: #008CA7;margin-top: 15px;">Macronutrients</div>

        <div style="font-weight: bold; padding-top: 12px;">
            Carbs:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByCarbs_Breakfast" onkeyup="searchMealsByAdvanced('Breakfast', 1);" /> 
            Protein:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByProtein_Breakfast" onkeyup="searchMealsByAdvanced('Breakfast', 1);" /> 
            Fat:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByFat_Breakfast" onkeyup="searchMealsByAdvanced('Breakfast', 1);"/>
        </div>

        <div style="font-weight: bold;color: #008CA7;margin-top: 15px;">Meal Classification</div>

        <div data-textbox="searchMealByKeyword_Breakfast" class="mealTagAdvancedSearch">
            <div><input id="gf_Breakfast" type="checkbox" name="mealtag" value="gf" onclick="searchMealsByAdvanced('Breakfast', 1);">Gluten Free<br></div>
            <div><input id="vegan_Breakfast" type="checkbox" name="mealtag" value="vegan" onclick="searchMealsByAdvanced('Breakfast', 1);">Vegan<br></div>
            <div><input id="veg_Breakfast" type="checkbox" name="mealtag" value="vegetarian" onclick="searchMealsByAdvanced('Breakfast', 1);">Vegetarian<br></div>
            <div><input id="lf_Breakfast" type="checkbox" name="mealtag" value="lf" onclick="searchMealsByAdvanced('Breakfast', 1);">Lactose Free<br></div>
            <div><input id="sf_Breakfast" type="checkbox" name="mealtag" value="sf" onclick="searchMealsByAdvanced('Breakfast', 1);">Seafood Free<br></div>
            <div><input id="nf_Breakfast" type="checkbox" name="mealtag" value="nf" onclick="searchMealsByAdvanced('Breakfast', 1);">Nut Free<br></div>
        </div>

        <div id="searchMealByKeyword_Breakfast_Result" class="search_results" style="display: none"></div>
    </div>

    <div id="tabSearchMealByMacronutrients_Breakfast" style="display: none">
        <span style="font-weight: bold">Search by Macronutrients</span>
        <a style="padding-left: 124px; font-weight: bold; color: #e27424" onclick="show('tabSearchMealByKeyword_Breakfast');hide('tabSearchMealByMacronutrients_Breakfast');">By Keyword ></a>
        <div style="font-weight: bold; padding-top: 12px;">Carbs:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByCarbs_Breakfast" /> Protein:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByProtein_Breakfast" /> Fat:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByFat_Breakfast" />
        <div style="display: inline-block; cursor: pointer; position: relative; left: -20px; top: 4px;"  onclick="getMealsByMacronutrients($('#searchMealByCarbs_Breakfast').val(),$('#searchMealByProtein_Breakfast').val(),$('#searchMealByFat_Breakfast').val(),'searchMealByMacronutrients_Breakfast_Result',1);"><img src="/images/buttonSearchMag.gif" /></div>
        </div>
        <div id="searchMealByMacronutrients_Breakfast_Result" class="search_results" style="display: none"></div>
    </div>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;display:none; margin-bottom: 10px;padding-bottom: 10px" id="addMealBreakfast_Recent_Select">
    <select onchange="getRecentMealsByMealtime($('#recentSearchMealBreakfast').val(), 'addMealBreakfast_Recent_Results', 1)" id="recentSearchMealBreakfast">
        <option value="0"></option>
        <option value="1">Breakfast</option>
        <option value="2">Morning Tea</option>
        <option value="3">Lunch</option>
        <option value="4">Afternoon Tea</option>
        <option value="5">Dinner</option>
        <option value="6">Supper</option>
        <option value="-1">All Meals</option>
    </select>
    <div id="addMealBreakfast_Recent_Results" class="search_results_recent"></div>
    </div>
</div>

<div class="result" id="resultBreakfast" style="display: none;"></div>
</td></tr>
<tr>
<td style="border-bottom: 1px solid #e5e5e5; padding-top: 15px; padding-bottom: 10px; font-size: 14px; font-weight: bold; padding-left: 11px;" colspan="8" >Morning Tea<span class="balloonright"><asp:Literal ID="LiteralMorningTeaMood" runat="server"></asp:Literal></span></td>
</tr>
<asp:Literal ID="literalMorningTeaRows" runat="server"></asp:Literal>
<tr class="buttons mealtime2"  id="buttonsMorningTea">
<td style="padding-top: 5px" colspan="2"><div style="position: relative">
<div style="cursor: pointer; display: inline" id="addItemMorningTea" onclick="hide_one_of();show('addItemMorningTea_Select');hide('buttonsMorningTea');"><img src="/images/buttonAddItem.gif" /></div>
<div style="cursor: pointer; display: inline" id="addMealMorningTea" onclick="hide_one_of();show('addMealMorningTea_Select');hide('buttonsMorningTea');"><img src="/images/buttonAddMeal.gif" /></div>
<div style="cursor: pointer; display: inline; margin-left: 13px;" id="quickToolsMorningTea" onclick="var result = toggle_str('quickToolsMorningTea_Select');hide_one_of();switchto('quickToolsMorningTea_Select', result);"><img src="/images/buttonQuickTools.gif" /></div>

<div class="quicktools_options one_of" id="quickToolsMorningTea_Select">
    <div class="quicktools_option" onclick="hide_one_of();show('quickToolsMorningTea_SaveAsMeal_Select');">Save as meal</div>
    <div class="quicktools_option" onclick="hide_one_of();show('quickToolsMorningTea_CopyAndPaste_Select');">Copy &amp; paste</div>
</div>

<div class="one_of" style="z-index: 100; left: 114px; top: 29px; border: 1px solid #a0a0a0; display: none; position: absolute; width: 186px; height: 92px; padding-top: 6px; padding-left: 12px; background-color: #f1f1f1; font-weight: bold" id="quickToolsMorningTea_SaveAsMeal_Select">
    <p>Name this meal</p>
    <input type="text" id="saveAsMealName_MorningTea" style="width: 172px; height: 23px; margin-top: 10px; border: 1px solid #999999" />
    <div style="float: left; cursor: pointer; padding-top: 5px;"  onclick="hide('quickToolsMorningTea_SaveAsMeal_Select');saveDiaryMealAsMeal(2,$('#saveAsMealName_MorningTea').val(),'resultMorningTea');"><img src="/images/buttonSaveMeal.gif" /></div>
    <div style="float: left; cursor: pointer; padding-top: 10px; padding-left: 21px; color: #e27423"  onclick="hide('quickToolsMorningTea_SaveAsMeal_Select');">or Cancel</div>
</div>
<div class="one_of" style="z-index: 100; left: 114px; top: 29px; border: 1px solid #a0a0a0; display: none; position: absolute; width: 198px; height: 200px; background-color: #f1f1f1" id="quickToolsMorningTea_CopyAndPaste_Select">
    <div class="quicktools_copyto_header">Copy to which date?</div>
    <% =CopyToDates(2, "quickToolsMorningTea_CopyAndPaste_Select", "resultMorningTea")%>
</div>
<div class="one_of" style="z-index: 100; left: 114px; top: 29px; border: 1px solid #a0a0a0; display: none; position: absolute; width: 236px; height: 328px;  padding-top: 6px; padding-left: 12px; background-color: #f1f1f1" id="quickToolsMorningTea_AddNewFood_Select">
    <p style="font-weight: bold">Food Item Request</p>
    <p style="padding-top: 10px">If you would like a food item to be added to the database please fill in the form below. Please note that only foods with a brand and product name can be added to the main food database.
</p>
    <input type="text" id="addNewFood_MorningTeaText1" style="width: 224px" placeholder="Brand Name (optional)"/>
        <input type="text" id="addNewFood_MorningTeaText2" style="width: 224px" placeholder="Product Name (Required)"/>
        <input type="text" id="addNewFood_MorningTeaText3" style="width: 224px"  placeholder="Serving Size (optional)"/>
        <input type="text" id="addNewFood_MorningTeaText4" style="width: 224px"  placeholder="Serving Unit (optional)" />
        <input type="text" id="addNewFood_MorningTeaText5" style="width: 224px"  placeholder="Carbohydrates per serving (optional)" />
        <input type="text" id="addNewFood_MorningTeaText6" style="width: 224px"  placeholder="Protein per serving (optional)"/>
        <input type="text" id="addNewFood_MorningTeaText7" style="width: 224px"  placeholder="Fat per serving (optional)"/>    
    <div style="float: left; cursor: pointer; padding-top: 5px;" onclick="hide('quickToolsMorningTea_AddNewFood_Select');foodItemRequest('Brand= ' + $('#addNewFood_MorningTeaText1').val()+ 
                                                                                                                    '. product = ' + $('#addNewFood_MorningTeaText2').val()+ 
                                                                                                                    '. ServSize= ' +  $('#addNewFood_MorningTeaText3').val() +
                                                                                                                    '. ServUnit = ' +$('#addNewFood_MorningTeaText4').val() +
                                                                                                                    '. Carb= ' + $('#addNewFood_MorningTeaText5').val()+
                                                                                                                    '. Ptn= ' + $('#addNewFood_MorningTeaText6').val() +
                                                                                                                    '. Fat= ' + $('#addNewFood_MorningTeaText7').val(),'resultMorningTea', 'false');"><img src="/images/buttonSendRequest.gif" /></div>
    <div style="float: left; cursor: pointer; padding-top: 10px; padding-left: 21px; color: #e27423"  onclick="hide('quickToolsMorningTea_AddNewFood_Select');">or Cancel</div>

</div>
</div>


</td>
<td>&nbsp;</td>
<td style="font-weight: bold; text-align: center">Subtotal</td>
<td style="font-weight: bold; text-align: center" id="carb-2"><asp:Literal ID="literalMorningTeaCarb" runat="server"></asp:Literal></td>
<td style="font-weight: bold; text-align: center" id="protein-2"><asp:Literal ID="literalMorningTeaProtein" runat="server"></asp:Literal></td>
<td style="font-weight: bold; text-align: center" id="fat-2"><asp:Literal ID="literalMorningTeaFat" runat="server"></asp:Literal></td>
<td>&nbsp;</td>
</tr>
<tr><td colspan="8">
<div style="border: 1px solid #d1d1d1; display: none; width: 883px; background-color: #ececec" id="addItemMorningTea_Select" class="one_of">
<div style="cursor: pointer; display: inline" id="addItemMorningTeaSelected" onclick="hide('addItemMorningTea_Select');show('buttonsMorningTea');"><img src="/images/buttonAddItemSelected.gif" /></div>
<div class="additem_options">
    <div class="additem_option additem_option_selected"
         onclick="additem_option_selected(this);
                  show('addItemMorningTea_Search_Select');
                  hide('addItemMorningTea_CopyPaste_Select');
                  hide('addItemMorningTea_Recent_Select');
                  hide('addItemMorningTea_AddFood_Select');
                  clearRight();">Search & Add</div>
    <div class="additem_option" onclick="additem_option_selected(this);
                                         hide('addItemMorningTea_Search_Select');
                                         show('addItemMorningTea_CopyPaste_Select');
                                         hide('addItemMorningTea_Recent_Select');
                                         hide('addItemMorningTea_AddFood_Select');">Copy &amp; Paste</div>
    <div class="additem_option" onclick="additem_option_selected(this);
                                         hide('addItemMorningTea_Search_Select');
                                         hide('addItemMorningTea_CopyPaste_Select');
                                         show('addItemMorningTea_Recent_Select');
                                         hide('addItemMorningTea_AddFood_Select');">Recent</div>
    <%--<div class="additem_option" onclick="additem_option_selected(this);
                                         hide('addItemMorningTea_Search_Select');
                                         hide('addItemMorningTea_CopyPaste_Select');
                                         hide('addItemMorningTea_Recent_Select');
                                         show('addItemMorningTea_AddFood_Select');">Add Your Own Food</div>--%>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;margin-bottom: 10px;padding-bottom: 10px" id="addItemMorningTea_Search_Select">
    <div id="tabSearchByKeyword_MorningTea">
        <div style="font-weight: bold">
           <div style="font-weight: bold;color: #008CA7;">Keyword</div>
        </div>

        <div style="float:left;">
            <input type="text" id="searchByKeyword_MorningTea" style="height: 23px; width: 345px; border: 1px solid #999999; margin-top: 11px;padding-left: 35px"
                   onkeyup="searchItemsByAdvancedKeyword('MorningTea', 2);"/>
            <div style="position: absolute; top: 45px; left: 23px; cursor: pointer;"
                 onclick="searchItemsByAdvancedKeyword('MorningTea', 2);"><img src="/images/buttonSearchMag.gif" />
        </div>

        <div style="font-weight: bold;color: #008CA7;margin-top: 15px;">Macronutrients</div>
        <div style="font-weight: bold; padding-top: 12px;">
            Carbs:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchByCarbs_MorningTea" onkeyup="searchItemsByAdvanced('MorningTea', 2);"/> 
            Protein:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchByProtein_MorningTea" onkeyup="searchItemsByAdvanced('MorningTea', 2);"/> 
            Fat:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchByFat_MorningTea" onkeyup="searchItemsByAdvanced('MorningTea', 2);"/>
        </div>

        <div style="font-weight: bold;color: #008CA7;margin-top: 15px;">Category</div>
        <div style="font-weight: bold; padding-top: 12px;margin-bottom: 15px;">
            <asp:Literal ID="literalSearchByCategory_MorningTea" runat="server"></asp:Literal>
        </div>
        </div>
        <div class="rightSide" id="rightMorningTea"><!--button click takes to add new item panel-->
            <span style="display:inline-block;vertical-align:super;">Still can't find what you are looking for? Add new item</span>
            <input type="button" class="imgButtonAddItem" onclick="hide('addItemMorningTea_Search_Select');
                                                                   hide('addItemMorningTea_CopyPaste_Select');
                                                                   hide('addItemMorningTea_Recent_Select');
                                                                   show('addItemMorningTea_AddFood_Select');
                                                                   $('#AOFoodName_MorningTea').val($('#searchByKeyword_MorningTea').val());
                                                                   $('.search_results').hide();"/>
        </div>
        <div style="clear:both;"></div>
        <div id="searchByKeyword_MorningTea_Result" class="search_results" style="display: none"></div>
    </div>
    
</div>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;display:none; margin-bottom: 10px;padding-bottom: 10px" id="addItemMorningTea_CopyPaste_Select">
    <div style="float: left; width: 200px"><span style="font-weight: bold">Select a date to copy from: </span><BDP:BasicDatePicker ID="bdpMorningTea" runat="server" DisplayType="Image" OnClientAfterSelectionChanged="getItemsCopyPaste"
        AutoPostBack="False" 
        ShowNoneButton="False" 
           ShowTodayButton="False" 
            ButtonImageFileName="calendar.gif" ButtonImageHeight="25" 
            ButtonImageWidth="26" RenderImages="File" ResourcePath="/images/" 
            DownYearSelectorImageFileName="" NextMonthImageFileName="calendar_forward.gif" 
            PrevMonthImageFileName="calendar_back.gif" RenderStyleSheet="None" TextBoxStyle-BackColor="White" TextBoxStyle-BorderColor="#999999" TextBoxStyle-BorderStyle="Solid" TextBoxStyle-BorderWidth="1px" TextBoxStyle-Height="23px" TextBoxStyle-Width="123px">
    </BDP:BasicDatePicker>
    </div>
    <div style="float: left; width: 610px; border-left: 1px solid #e5e5e5; min-height: 200px; padding-left: 30px">
    <div id="addItemMorningTea_CopyPaste_Results"></div>
    <p style="padding-top: 18px">Copy to which date: <% =CopyToDatesDropDown("copyToDateMorningTea")%></p>
    <div style="cursor: pointer; padding-top: 12px"  onclick="addCheckedItemsToDate('result_area_2', 'copyToDateMorningTea');"><img src="/images/buttonCopyPaste.gif"></div>
    </div>
    <div style="clear: both"></div>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;display:none; margin-bottom: 10px;padding-bottom: 10px" id="addItemMorningTea_Recent_Select">
    <select onchange="getRecentByMealtime($('#recentSearchMorningTea').val(), 'addItemMorningTea_Recent_Results', 2)" id="recentSearchMorningTea">
        <option value="0"></option>
        <option value="1">Breakfast</option>
        <option value="2">Morning Tea</option>
        <option value="3">Lunch</option>
        <option value="4">Afternoon Tea</option>
        <option value="5">Dinner</option>
        <option value="6">Supper</option>
        <option value="-1">All Meals</option>
    </select>
    <div id="addItemMorningTea_Recent_Results" class="search_results_recent"></div>
    </div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;display:none; margin-bottom: 10px;padding-bottom: 10px" id="addItemMorningTea_AddFood_Select" >
    <table>
        <tr style="font-weight: bold">
            <td>Food Name</td>
            <td style="padding-right: 10px">Brand Name</td>          
            <td style="padding: 0px 25px">Amount</td>
            <td style="padding: 0px 21px">Serve</td>
            <td style="padding-left: 4px; padding-right: 20px;">Carbohydrate(g)</td> 
            <td>Protein(g)</td>
            <td style="padding-left: 38px ">Fat(g)</td>           
        </tr>
        <tr>
            <td><input type="text" id="AOFoodName_MorningTea" style="width: 180px" disabled/></td>
            <td style="padding-right: 10px"><input type="text" id="AOBrandName_MorningTea" placeholder="optional"  /></td>        
            <td style="padding: 0px 25px"><input type="text" id="AOAmount_MorningTea" style="width: 48px"/></td>
            <td style="padding: 0px 21px"><input type="text" id="AOUnit_MorningTea" style="width: 50px" /></td>
            <td style="padding-left: 4px; padding-right: 20px; "><input type="text" id="AOCarbs_MorningTea" style="width: 50px;margin-left: 10px"/></td>
            <td><input type="text" id="AOPtn_MorningTea" style="width: 50px"/></td>
            <td style="padding-left: 25px "><input type="text" id="AOFat_MorningTea" style="width: 50px"/></td>
        </tr>
    </table>
    <i>Please note: Amount, Carbs, Protein and Fat entries are numerical only</i>
    <br/>
    <div style="float:left;cursor: pointer; display: inline" id="Div1" onclick="addOwnItem('2',$('#AOBrandName_MorningTea').val(),$('#AOFoodName_MorningTea').val(), $('#AOAmount_MorningTea').val(),
                                                                    $('#AOUnit_MorningTea').val(), $('#AOCarbs_MorningTea').val(),$('#AOPtn_MorningTea').val(),
                                                                    $('#AOFat_MorningTea').val())">
        <br/>
        <img src="/images/buttonSubmitOrange.gif" alt="Add Item" />
    </div>
    <div class="cancel" style="float:left;">
        <br />
        <img src="/images/buttonCancel.gif" alt="Cancel" />
    </div>
    <div style="clear:both;"></div>
</div>
</div>

<div style="border: 1px solid #d1d1d1; display: none; width: 883px; background-color: #ececec" id="addMealMorningTea_Select" class="one_of">
<div style="cursor: pointer; display: inline" id="addMealMorningTeaSelected" onclick="hide('addMealMorningTea_Select');show('buttonsMorningTea');"><img src="/images/buttonAddMealSelected.gif" /></div>
<div class="additem_options">
    <div class="additem_option additem_option_selected" onclick="additem_option_selected(this);show('addMealMorningTea_Search_Select');hide('addMealMorningTea_Recent_Select');">Search</div>
    <div class="additem_option" onclick="additem_option_selected(this);hide('addMealMorningTea_Search_Select');show('addMealMorningTea_Recent_Select');">Recent</div>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;margin-bottom: 10px;padding-bottom: 10px" id="addMealMorningTea_Search_Select">
    <div id="tabSearchMealByKeyword_MorningTea">
        <div style="font-weight: bold;color: #008CA7;">Keyword</div>
        <input type="text" id="searchMealByKeyword_MorningTea" style="height: 23px; width: 345px; border: 1px solid #999999; margin-top: 11px;padding-left: 35px"
               onkeyup="searchMealsByAdvancedKeyword('MorningTea', 2);" />
        <div style="position: absolute; top: 45px; left: 23px; cursor: pointer;"
             onclick="searchMealsByAdvancedKeyword('MorningTea', 2);"><img src="/images/buttonSearchMag.gif" /></div>
 
        <div class="clear"></div>
 
        <div style="font-weight: bold;color: #008CA7;margin-top: 15px;">Macronutrients</div>
 
        <div style="font-weight: bold; padding-top: 12px;">
            Carbs:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByCarbs_MorningTea" onkeyup="searchMealsByAdvanced('MorningTea', 2);" /> 
            Protein:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByProtein_MorningTea" onkeyup="searchMealsByAdvanced('MorningTea', 2);" /> 
            Fat:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByFat_MorningTea" onkeyup="searchMealsByAdvanced('MorningTea', 2);"/>
        </div>
 
        <div style="font-weight: bold;color: #008CA7;margin-top: 15px;">Meal Classification</div>
 
        <div data-textbox="searchMealByKeyword_MorningTea" class="mealTagAdvancedSearch">
            <div><input id="gf_MorningTea" type="checkbox" name="mealtag" value="gf" onclick="searchMealsByAdvanced('MorningTea', 2);">Gluten Free<br></div>
            <div><input id="vegan_MorningTea" type="checkbox" name="mealtag" value="vegan" onclick="searchMealsByAdvanced('MorningTea', 2);">Vegan<br></div>
            <div><input id="veg_MorningTea" type="checkbox" name="mealtag" value="vegetarian" onclick="searchMealsByAdvanced('MorningTea', 2);">Vegetarian<br></div>
            <div><input id="lf_MorningTea" type="checkbox" name="mealtag" value="lf" onclick="searchMealsByAdvanced('MorningTea', 2);">Lactose Free<br></div>
            <div><input id="sf_MorningTea" type="checkbox" name="mealtag" value="sf" onclick="searchMealsByAdvanced('MorningTea', 2);">Seafood Free<br></div>
            <div><input id="nf_MorningTea" type="checkbox" name="mealtag" value="nf" onclick="searchMealsByAdvanced('MorningTea', 2);">Nut Free<br></div>
        </div>
 
        <div id="searchMealByKeyword_MorningTea_Result" class="search_results" style="display: none"></div>
    </div>

    <div id="tabSearchMealByMacronutrients_MorningTea" style="display: none">
        <span style="font-weight: bold">Search by Macronutrients</span>
        <a style="padding-left: 124px; font-weight: bold; color: #e27424" onclick="show('tabSearchMealByKeyword_MorningTea');hide('tabSearchMealByMacronutrients_MorningTea');">By Keyword ></a>
        <div style="font-weight: bold; padding-top: 12px;">Carbs:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByCarbs_MorningTea" /> Protein:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByProtein_MorningTea" /> Fat:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByFat_MorningTea" />
        <div style="display: inline-block; cursor: pointer; position: relative; left: -20px; top: 4px;"  onclick="getMealsByMacronutrients($('#searchMealByCarbs_MorningTea').val(),$('#searchMealByProtein_MorningTea').val(),$('#searchMealByFat_MorningTea').val(),'searchMealByMacronutrients_MorningTea_Result',2);"><img src="/images/buttonSearchMag.gif" /></div>
        </div>
        <div id="searchMealByMacronutrients_MorningTea_Result" class="search_results" style="display: none"></div>
    </div>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;display:none; margin-bottom: 10px;padding-bottom: 10px" id="addMealMorningTea_Recent_Select">
    <select onchange="getRecentMealsByMealtime($('#recentSearchMealMorningTea').val(), 'addMealMorningTea_Recent_Results',2)" id="recentSearchMealMorningTea">
        <option value="0"></option>
        <option value="1">Breakfast</option>
        <option value="2">Morning Tea</option>
        <option value="3">Lunch</option>
        <option value="4">Afternoon Tea</option>
        <option value="5">Dinner</option>
        <option value="6">Supper</option>
        <option value="-1">All Meals</option>
    </select>
    <div id="addMealMorningTea_Recent_Results" class="search_results_recent"></div>
    </div>
</div>

<div class="result" id="resultMorningTea" style="display: none;"></div>
</td></tr>
<tr>
<td style="border-bottom: 1px solid #e5e5e5; padding-top: 15px; padding-bottom: 10px; font-size: 14px; font-weight: bold; padding-left: 11px;" colspan="8" >Lunch<span class="balloonright"><asp:Literal ID="LiteralLunchMood" runat="server"></asp:Literal></span></td>
</tr>
<asp:Literal ID="literalLunchRows" runat="server"></asp:Literal>
<tr class="buttons mealtime3"  id="buttonsLunch">
<td style="padding-top: 5px" colspan="2"><div style="position: relative">
<div style="cursor: pointer; display: inline" id="addItemLunch" onclick="hide_one_of();show('addItemLunch_Select');hide('buttonsLunch');"><img src="/images/buttonAddItem.gif" /></div> 
<div style="cursor: pointer; display: inline" id="addMealLunch" onclick="hide_one_of();show('addMealLunch_Select');hide('buttonsLunch');"><img src="/images/buttonAddMeal.gif" /></div> 
<div style="cursor: pointer; display: inline;margin-left: 13px" id="quickToolsLunch" onclick="var result = toggle_str('quickToolsLunch_Select');hide_one_of();switchto('quickToolsLunch_Select', result);"><img src="/images/buttonQuickTools.gif" /></div>

<div class="quicktools_options one_of" id="quickToolsLunch_Select">
    <div class="quicktools_option" onclick="hide_one_of();show('quickToolsLunch_SaveAsMeal_Select');">Save as meal</div>
    <div class="quicktools_option" onclick="hide_one_of();show('quickToolsLunch_CopyAndPaste_Select');">Copy &amp; paste</div>
</div>

<div class="one_of" style="z-index: 100; left: 114px; top: 29px; border: 1px solid #a0a0a0; display: none; position: absolute; width: 186px; height: 92px; padding-top: 6px; padding-left: 12px; background-color: #f1f1f1; font-weight: bold" id="quickToolsLunch_SaveAsMeal_Select">
    <p>Name this meal</p>
    <input type="text" id="saveAsMealName_Lunch" style="width: 172px; height: 23px; margin-top: 10px; border: 1px solid #999999" />
    <div style="float: left; cursor: pointer; padding-top: 5px;"  onclick="hide('quickToolsLunch_SaveAsMeal_Select');saveDiaryMealAsMeal(3,$('#saveAsMealName_Lunch').val(),'resultLunch');"><img src="/images/buttonSaveMeal.gif" /></div>
    <div style="float: left; cursor: pointer; padding-top: 10px; padding-left: 21px; color: #e27423"  onclick="hide('quickToolsLunch_SaveAsMeal_Select');">or Cancel</div>
</div>
<div class="one_of" style="z-index: 100; left: 114px; top: 29px; border: 1px solid #a0a0a0; display: none; position: absolute; width: 198px; height: 200px; background-color: #f1f1f1" id="quickToolsLunch_CopyAndPaste_Select">
    <div class="quicktools_copyto_header">Copy to which date?</div>
    <% =CopyToDates(3, "quickToolsLunch_CopyAndPaste_Select", "resultLunch")%>
</div>
<div class="one_of" style="z-index: 100; left: 114px; top: 29px; border: 1px solid #a0a0a0; display: none; position: absolute; width: 236px; height: 328px;  padding-top: 6px; padding-left: 12px; background-color: #f1f1f1" id="quickToolsLunch_AddNewFood_Select">
    <p style="font-weight: bold">Food Item Request</p>
    <p style="padding-top: 10px">If you would like a food item to be added to the database please fill in the form below. Please note that only foods with a brand and product name can be added to the main food database.
 </p>
    <input type="text" id="addNewFood_Lunch_brand" style="width: 224px" placeholder="Brand Name (optional)"/>
        <input type="text" id="addNewFood_Lunch_product" style="width: 224px" placeholder="Product Name (Required)"/>
        <input type="text" id="addNewFood_Lunch_serving" style="width: 224px"  placeholder="Serving Size (optional)"/>
        <input type="text" id="addNewFood_Lunch_serveunit" style="width: 224px"  placeholder="Serving Unit (optional)" />
        <input type="text" id="addNewFood_Lunch_carbs" style="width: 224px"  placeholder="Carbohydrates per serving (optional)" />
        <input type="text" id="addNewFood_Lunch_ptn" style="width: 224px"  placeholder="Protein per serving (optional)"/>
        <input type="text" id="addNewFood_Lunch_fat" style="width: 224px"  placeholder="Fat per serving (optional)"/>       
    <div style="float: left; cursor: pointer; padding-top: 5px;" onclick="hide('quickToolsLunch_AddNewFood_Select');foodItemRequest('Brand= ' + $('#addNewFood_Lunch_brand').val()+ '. product = ' + $('#addNewFood_Lunch_product').val()+ 
                                                                                                                    '. ServSize= ' +  $('#addNewFood_Lunch_serving').val() +
                                                                                                                    '. ServUnit = ' +$('#addNewFood_Lunch_serveunit').val() +
                                                                                                                    '. Carb= ' + $('#addNewFood_Lunch_carbs').val()+
                                                                                                                    '. Ptn= ' + $('#addNewFood_Lunch_ptn').val() +
                                                                                                                    '. Fat= ' + $('#addNewFood_Lunch_fat').val(),'resultLunch', 'false');"><img src="/images/buttonSendRequest.gif" /></div>
    <div style="float: left; cursor: pointer; padding-top: 10px; padding-left: 21px; color: #e27423"  onclick="hide('quickToolsLunch_AddNewFood_Select');">or Cancel</div>

</div>
</div>


</td>
<td>&nbsp;</td>
<td style="font-weight: bold; text-align: center">Subtotal</td>
<td style="font-weight: bold; text-align: center" id="carb-3"><asp:Literal ID="literalLunchCarb" runat="server"></asp:Literal></td>
<td style="font-weight: bold; text-align: center" id="protein-3"><asp:Literal ID="literalLunchProtein" runat="server"></asp:Literal></td>
<td style="font-weight: bold; text-align: center" id="fat-3"><asp:Literal ID="literalLunchFat" runat="server"></asp:Literal></td>
<td>&nbsp;</td>
</tr>
<tr><td colspan="8">
<div style="border: 1px solid #d1d1d1; display: none; width: 883px; background-color: #ececec" id="addItemLunch_Select" class="one_of">
<div style="cursor: pointer; display: inline" id="addItemLunchSelected" onclick="hide('addItemLunch_Select');show('buttonsLunch');"><img src="/images/buttonAddItemSelected.gif" /></div>
<div class="additem_options">
    <div class="additem_option additem_option_selected"
         onclick="additem_option_selected(this);show('addItemLunch_Search_Select');
                  hide('addItemLunch_CopyPaste_Select');
                  hide('addItemLunch_Recent_Select');
                  hide('addItemLunch_AddFood_Select');
                  clearRight();">Search & Add</div>
    <div class="additem_option" onclick="additem_option_selected(this);
                                         hide('addItemLunch_Search_Select');
                                         show('addItemLunch_CopyPaste_Select');
                                         hide('addItemLunch_Recent_Select');
                                         hide('addItemLunch_AddFood_Select');">Copy &amp; Paste</div>
    <div class="additem_option" onclick="additem_option_selected(this);
                                         hide('addItemLunch_Search_Select');
                                         hide('addItemLunch_CopyPaste_Select');
                                         show('addItemLunch_Recent_Select');
                                         hide('addItemLunch_AddFood_Select');">Recent</div>
    <%--<div class="additem_option" onclick="additem_option_selected(this);
                                         hide('addItemLunch_Search_Select');
                                         hide('addItemLunch_CopyPaste_Select');
                                         hide('addItemLunch_Recent_Select');
                                         show('addItemLunch_AddFood_Select');">Add Your Own Food</div>--%>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;margin-bottom: 10px;padding-bottom: 10px" id="addItemLunch_Search_Select">
    <div id="tabSearchByKeyword_Lunch">
        <div style="font-weight: bold">
           <div style="font-weight: bold;color: #008CA7;">Keyword</div>
        </div>

        <div style="float:left;">
            <input type="text" id="searchByKeyword_Lunch" style="height: 23px; width: 345px; border: 1px solid #999999; margin-top: 11px;padding-left: 35px"
                   onkeyup="searchItemsByAdvancedKeyword('Lunch', 3);"/>
        
            <div style="position: absolute; top: 45px; left: 23px; cursor: pointer;"
                 onclick="searchItemsByAdvancedKeyword('Lunch', 3);"><img src="/images/buttonSearchMag.gif" /></div>
        

       <div style="font-weight: bold;color: #008CA7;margin-top: 15px;width: 100%;">Macronutrients</div>
       <div style="font-weight: bold; padding-top: 12px;">
           <div style="font-weight: bold; padding-top: 12px;">
               Carbs:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchByCarbs_Lunch" onkeyup="searchItemsByAdvanced('Lunch', 3);"/> 
               Protein:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchByProtein_Lunch" onkeyup="searchItemsByAdvanced('Lunch', 3);"/> 
               Fat:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchByFat_Lunch" onkeyup="searchItemsByAdvanced('Lunch', 3);" />
           </div>
       </div>

        <div style="font-weight: bold;color: #008CA7;margin-top: 15px;">Category</div>
        <div style="font-weight: bold; padding-top: 12px;margin-bottom: 15px;">
            <asp:Literal ID="literalSearchByCategory_Lunch" runat="server"></asp:Literal>
        </div>
        </div>
        <div class="rightSide" id="rightLunch"><!--button click takes to add new item panel-->
            <span style="display:inline-block;vertical-align:super;">Still can't find what you are looking for? Add new item</span>
            <input type="button" class="imgButtonAddItem" onclick="hide('addItemLunch_Search_Select');
                                                                   hide('addItemLunch_CopyPaste_Select');
                                                                   hide('addItemLunch_Recent_Select');
                                                                   show('addItemLunch_AddFood_Select');
                                                                   $('#AOFoodName_Lunch').val($('#searchByKeyword_Lunch').val());
                                                                   $('.search_results').hide();"/>
        </div>
        <div style="clear:both;"></div>
        <div id="searchByKeyword_Lunch_Result" class="search_results" style="display: none"></div>
    </div>
    <div id="tabSearchByCategory_Lunch" style="display: none">
        <span style="font-weight: bold">Search by Category</span>
        <a style="padding-left: 124px; font-weight: bold; color: #e27424" onclick="show('tabSearchByKeyword_Lunch');hide('tabSearchByCategory_Lunch');hide('tabSearchByMacronutrients_Lunch');">By Keyword ></a>
        <a style="padding-left: 24px; font-weight: bold; color: #e27424" onclick="hide('tabSearchByKeyword_Lunch');hide('tabSearchByCategory_Lunch');show('tabSearchByMacronutrients_Lunch');">By Macronutrients ></a>
        
        <div id="searchByCategory_Lunch_Result" class="search_results" style="display: none"></div>
    </div>
    <div id="tabSearchByMacronutrients_Lunch" style="display: none">
        <span style="font-weight: bold">Search by Macronutrients</span>
        <a style="padding-left: 124px; font-weight: bold; color: #e27424" onclick="show('tabSearchByKeyword_Lunch');hide('tabSearchByCategory_Lunch');hide('tabSearchByMacronutrients_Lunch');">By Keyword ></a>
        <a style="padding-left: 24px; font-weight: bold; color: #e27424" onclick="hide('tabSearchByKeyword_Lunch');show('tabSearchByCategory_Lunch');hide('tabSearchByMacronutrients_Lunch');">By Category ></a>
        <div style="font-weight: bold; padding-top: 12px;">Carbs:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchByCarbs_Lunch" /> Protein:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchByProtein_Lunch" /> Fat:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchByFat_Lunch" />
        <div style="display: inline-block; cursor: pointer; position: relative; left: -20px; top: 4px;"  onclick="getItemsByMacronutrients($('#searchByCarbs_Lunch').val(),$('#searchByProtein_Lunch').val(),$('#searchByFat_Lunch').val(),'searchByMacronutrients_Lunch_Result',3);"><img src="/images/buttonSearchMag.gif" /></div>
        </div>
        <div id="searchByMacronutrients_Lunch_Result" class="search_results" style="display: none"></div>
    </div>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;display:none; margin-bottom: 10px;padding-bottom: 10px" id="addItemLunch_CopyPaste_Select">
    <div style="float: left; width: 200px"><span style="font-weight: bold">Select a date to copy from: </span><BDP:BasicDatePicker ID="bdpLunch" runat="server" DisplayType="Image" OnClientAfterSelectionChanged="getItemsCopyPaste"
        AutoPostBack="False" 
        ShowNoneButton="False" 
           ShowTodayButton="False" 
            ButtonImageFileName="calendar.gif" ButtonImageHeight="25" 
            ButtonImageWidth="26" RenderImages="File" ResourcePath="/images/" 
            DownYearSelectorImageFileName="" NextMonthImageFileName="calendar_forward.gif" 
            PrevMonthImageFileName="calendar_back.gif" RenderStyleSheet="None" TextBoxStyle-BackColor="White" TextBoxStyle-BorderColor="#999999" TextBoxStyle-BorderStyle="Solid" TextBoxStyle-BorderWidth="1px" TextBoxStyle-Height="23px" TextBoxStyle-Width="123px">
    </BDP:BasicDatePicker>
    </div>
    <div style="float: left; width: 610px; border-left: 1px solid #e5e5e5; min-height: 200px; padding-left: 30px">
    <div id="addItemLunch_CopyPaste_Results"></div>
    <p style="padding-top: 18px">Copy to which date: <% =CopyToDatesDropDown("copyToDateLunch")%></p>
    <div style="cursor: pointer; padding-top: 12px"  onclick="addCheckedItemsToDate('result_area_3', 'copyToDateLunch');"><img src="/images/buttonCopyPaste.gif"></div>
    </div>
    <div style="clear: both"></div>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;display:none; margin-bottom: 10px;padding-bottom: 10px" id="addItemLunch_Recent_Select">
    <select onchange="getRecentByMealtime($('#recentSearchLunch').val(), 'addItemLunch_Recent_Results', 3)" id="recentSearchLunch">
        <option value="0"></option>
        <option value="1">Breakfast</option>
        <option value="2">Morning Tea</option>
        <option value="3">Lunch</option>
        <option value="4">Afternoon Tea</option>
        <option value="5">Dinner</option>
        <option value="6">Supper</option>
        <option value="-1">All Meals</option>
    </select>
    <div id="addItemLunch_Recent_Results" class="search_results_recent"></div>
    </div>
    
<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;display:none; margin-bottom: 10px;padding-bottom: 10px" id="addItemLunch_AddFood_Select" >
    <table>
        <tr style="font-weight: bold">
            <td>Food Name</td>
            <td style="padding-right: 10px">Brand Name</td>
            <td style="padding: 0px 25px">Amount</td>
            <td style="padding: 0px 21px">Serve</td>
            <td style="padding-left: 4px; padding-right: 20px;">Carbohydrate(g)</td> 
            <td>Protein(g)</td>
            <td style="padding-left: 38px ">Fat(g)</td>           
        </tr>
        <tr>
            <td><input type="text" id="AOFoodName_Lunch" style="width: 180px" disabled/></td>
            <td style="padding-right: 10px"><input type="text" id="AOBrandName_Lunch" placeholder="optional"  /></td>
            <td style="padding: 0px 25px"><input type="text" id="AOAmount_Lunch" style="width: 48px"/></td>
            <td style="padding: 0px 21px"><input type="text" id="AOUnit_Lunch" style="width: 50px" /></td>
            <td style="padding-left: 4px; padding-right: 20px; "><input type="text" id="AOCarbs_Lunch" style="width: 50px;margin-left: 10px"/></td>
            <td><input type="text" id="AOPtn_Lunch" style="width: 50px"/></td>
            <td style="padding-left: 25px "><input type="text" id="AOFat_Lunch" style="width: 50px"/></td>
        </tr>
    </table>
    <i>Please note: Amount, Carbs, Protein and Fat entries are numerical only</i>
    <br/>
    <div style="float:left;cursor: pointer; display: inline" id="Div2" onclick="addOwnItem('3',$('#AOBrandName_Lunch').val(),$('#AOFoodName_Lunch').val(), $('#AOAmount_Lunch').val(),
                                                                    $('#AOUnit_Lunch').val(), $('#AOCarbs_Lunch').val(),$('#AOPtn_Lunch').val(),
                                                                    $('#AOFat_Lunch').val())">
        <br/>
        <img src="/images/buttonSubmitOrange.gif" alt="Add Item" />
    </div>
    <div class="cancel" style="float:left;">
        <br />
        <img src="/images/buttonCancel.gif" alt="Cancel" />
    </div>
    <div style="clear:both;"></div>
</div>
</div>

<div style="border: 1px solid #d1d1d1; display: none; width: 883px; background-color: #ececec" id="addMealLunch_Select" class="one_of">
<div style="cursor: pointer; display: inline" id="addMealLunchSelected" onclick="hide('addMealLunch_Select');show('buttonsLunch');"><img src="/images/buttonAddMealSelected.gif" /></div>
<div class="additem_options">
    <div class="additem_option additem_option_selected" onclick="additem_option_selected(this);show('addMealLunch_Search_Select');hide('addMealLunch_Recent_Select');">Search</div>
    <div class="additem_option" onclick="additem_option_selected(this);hide('addMealLunch_Search_Select');show('addMealLunch_Recent_Select');">Recent</div>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;margin-bottom: 10px;padding-bottom: 10px" id="addMealLunch_Search_Select">
    <div id="tabSearchMealByKeyword_Lunch">
        <div style="font-weight: bold;color: #008CA7;">Keyword</div>
        <input type="text" id="searchMealByKeyword_Lunch" style="height: 23px; width: 345px; border: 1px solid #999999; margin-top: 11px;padding-left: 35px"
               onkeyup="searchMealsByAdvancedKeyword('Lunch', 3);" />
        <div style="position: absolute; top: 45px; left: 23px; cursor: pointer;"
             onclick="searchMealsByAdvancedKeyword('Lunch', 3);"><img src="/images/buttonSearchMag.gif" /></div>
 
        <div class="clear"></div>
 
        <div style="font-weight: bold;color: #008CA7;margin-top: 15px;">Macronutrients</div>
 
        <div style="font-weight: bold; padding-top: 12px;">
            Carbs:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByCarbs_Lunch" onkeyup="searchMealsByAdvanced('Lunch', 3);" /> 
            Protein:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByProtein_Lunch" onkeyup="searchMealsByAdvanced('Lunch', 3);" /> 
            Fat:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByFat_Lunch" onkeyup="searchMealsByAdvanced('Lunch', 3);"/>
        </div>
 
        <div style="font-weight: bold;color: #008CA7;margin-top: 15px;">Meal Classification</div>
 
        <div data-textbox="searchMealByKeyword_Lunch" class="mealTagAdvancedSearch">
            <div><input id="gf_Lunch" type="checkbox" name="mealtag" value="gf" onclick="searchMealsByAdvanced('Lunch', 3);">Gluten Free<br></div>
            <div><input id="vegan_Lunch" type="checkbox" name="mealtag" value="vegan" onclick="searchMealsByAdvanced('Lunch', 3);">Vegan<br></div>
            <div><input id="veg_Lunch" type="checkbox" name="mealtag" value="vegetarian" onclick="searchMealsByAdvanced('Lunch', 3);">Vegetarian<br></div>
            <div><input id="lf_Lunch" type="checkbox" name="mealtag" value="lf" onclick="searchMealsByAdvanced('Lunch', 3);">Lactose Free<br></div>
            <div><input id="sf_Lunch" type="checkbox" name="mealtag" value="sf" onclick="searchMealsByAdvanced('Lunch', 3);">Seafood Free<br></div>
            <div><input id="nf_Lunch" type="checkbox" name="mealtag" value="nf" onclick="searchMealsByAdvanced('Lunch', 3);">Nut Free<br></div>
        </div>
 
        <div id="searchMealByKeyword_Lunch_Result" class="search_results" style="display: none"></div>
    </div>


    <div id="tabSearchMealByMacronutrients_Lunch" style="display: none">
        <span style="font-weight: bold">Search by Macronutrients</span>
        <a style="padding-left: 124px; font-weight: bold; color: #e27424" onclick="show('tabSearchMealByKeyword_Lunch');hide('tabSearchMealByMacronutrients_Lunch');">By Keyword ></a>
        <div style="font-weight: bold; padding-top: 12px;">Carbs:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByCarbs_Lunch" /> Protein:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByProtein_Lunch" /> Fat:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByFat_Lunch" />
        <div style="display: inline-block; cursor: pointer; position: relative; left: -20px; top: 4px;"  onclick="getMealsByMacronutrients($('#searchMealByCarbs_Lunch').val(),$('#searchMealByProtein_Lunch').val(),$('#searchMealByFat_Lunch').val(),'searchMealByMacronutrients_Lunch_Result',3);"><img src="/images/buttonSearchMag.gif" /></div>
        </div>
        <div id="searchMealByMacronutrients_Lunch_Result" class="search_results" style="display: none"></div>
    </div>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;display:none; margin-bottom: 10px;padding-bottom: 10px" id="addMealLunch_Recent_Select">
    <select onchange="getRecentMealsByMealtime($('#recentSearchMealLunch').val(), 'addMealLunch_Recent_Results',3)" id="recentSearchMealLunch">
        <option value="0"></option>
        <option value="1">Breakfast</option>
        <option value="2">Morning Tea</option>
        <option value="3">Lunch</option>
        <option value="4">Afternoon Tea</option>
        <option value="5">Dinner</option>
        <option value="6">Supper</option>
        <option value="-1">All Meals</option>
    </select>
    <div id="addMealLunch_Recent_Results" class="search_results_recent"></div>
    </div>
</div>

<div class="result" id="resultLunch" style="display: none;"></div>
</td></tr>

<tr>
<td style="border-bottom: 1px solid #e5e5e5; padding-top: 15px; padding-bottom: 10px; font-size: 14px; font-weight: bold; padding-left: 11px;" colspan="8" >Afternoon Tea<span class="balloonright"><asp:Literal ID="LiteralAfternoonTeaMood" runat="server"></asp:Literal></span></td>
</tr>
<asp:Literal ID="literalAfternoonTeaRows" runat="server"></asp:Literal>
<tr class="buttons mealtime4"  id="buttonsAfternoonTea">
<td style="padding-top: 5px" colspan="2"><div style="position: relative">
<div style="cursor: pointer; display: inline" id="addItemAfternoonTea" onclick="hide_one_of();show('addItemAfternoonTea_Select');hide('buttonsAfternoonTea');"><img src="/images/buttonAddItem.gif" /></div>
<div style="cursor: pointer; display: inline" id="addMealAfternoonTea" onclick="hide_one_of();show('addMealAfternoonTea_Select');hide('buttonsAfternoonTea');"><img src="/images/buttonAddMeal.gif" /></div>
<div style="cursor: pointer; display: inline; margin-left: 13px;" id="quickToolsAfternoonTea" onclick="var result = toggle_str('quickToolsAfternoonTea_Select');hide_one_of();switchto('quickToolsAfternoonTea_Select', result);"><img src="/images/buttonQuickTools.gif" /></div>

<div class="quicktools_options one_of" id="quickToolsAfternoonTea_Select">
    <div class="quicktools_option" onclick="hide_one_of();show('quickToolsAfternoonTea_SaveAsMeal_Select');">Save as meal</div>
    <div class="quicktools_option" onclick="hide_one_of();show('quickToolsAfternoonTea_CopyAndPaste_Select');">Copy &amp; paste</div>
</div>

<div class="one_of" style="z-index: 100; left: 114px; top: 29px; border: 1px solid #a0a0a0; display: none; position: absolute; width: 186px; height: 92px; padding-top: 6px; padding-left: 12px; background-color: #f1f1f1; font-weight: bold" id="quickToolsAfternoonTea_SaveAsMeal_Select">
    <p>Name this meal</p>
    <input type="text" id="saveAsMealName_AfternoonTea" style="width: 172px; height: 23px; margin-top: 10px; border: 1px solid #999999" />
    <div style="float: left; cursor: pointer; padding-top: 5px;"  onclick="hide('quickToolsAfternoonTea_SaveAsMeal_Select');saveDiaryMealAsMeal(4,$('#saveAsMealName_AfternoonTea').val(),'resultAfternoonTea');"><img src="/images/buttonSaveMeal.gif" /></div>
    <div style="float: left; cursor: pointer; padding-top: 10px; padding-left: 21px; color: #e27423"  onclick="hide('quickToolsAfternoonTea_SaveAsMeal_Select');">or Cancel</div>
</div>
<div class="one_of" style="z-index: 100; left: 114px; top: 29px; border: 1px solid #a0a0a0; display: none; position: absolute; width: 198px; height: 200px; background-color: #f1f1f1" id="quickToolsAfternoonTea_CopyAndPaste_Select">
    <div class="quicktools_copyto_header">Copy to which date?</div>
    <% =CopyToDates(4, "quickToolsAfternoonTea_CopyAndPaste_Select", "resultAfternoonTea")%>
</div>
<div class="one_of" style="z-index: 100; left: 114px; top: 29px; border: 1px solid #a0a0a0; display: none; position: absolute; width: 236px; height: 328px;  padding-top: 6px; padding-left: 12px; background-color: #f1f1f1" id="quickToolsAfternoonTea_AddNewFood_Select">
    <p style="font-weight: bold">Food Item Request</p>
    <p style="padding-top: 10px">If you would like a food item to be added to the database please fill in the form below. Please note that only foods with a brand and product name can be added to the main food database.
</p>
    <input type="text" id="addNewFood_AfternoonTeaText1" style="width: 224px" placeholder="Brand Name (optional)"/>
        <input type="text" id="addNewFood_AfternoonTeaText2" style="width: 224px" placeholder="Product Name (Required)"/>
        <input type="text" id="addNewFood_AfternoonTeaText3" style="width: 224px"  placeholder="Serving Size (optional)"/>
        <input type="text" id="addNewFood_AfternoonTeaText4" style="width: 224px"  placeholder="Serving Unit (optional)" />
        <input type="text" id="addNewFood_AfternoonTeaText5" style="width: 224px"  placeholder="Carbohydrates per serving (optional)" />
        <input type="text" id="addNewFood_AfternoonTeaText6" style="width: 224px"  placeholder="Protein per serving (optional)"/>
        <input type="text" id="addNewFood_AfternoonTeaText7" style="width: 224px"  placeholder="Fat per serving (optional)"/> 
    <div style="float: left; cursor: pointer; padding-top: 5px;" onclick="hide('quickToolsAfternoonTea_AddNewFood_Select');foodItemRequest('Brand= ' + $('#addNewFood_AfternoonTeaText1').val()+ 
                                                                                                                    '. product = ' + $('#addNewFood_AfternoonTeaText2').val()+ 
                                                                                                                    '. ServSize= ' +  $('#addNewFood_AfternoonTeaText3').val() +
                                                                                                                    '. ServUnit = ' +$('#addNewFood_AfternoonTeaText4').val() +
                                                                                                                    '. Carb= ' + $('#addNewFood_AfternoonTeaText5').val()+
                                                                                                                    '. Ptn= ' + $('#addNewFood_AfternoonTeaText6').val() +
                                                                                                                    '. Fat= ' + $('#addNewFood_AfternoonTeaText7').val(),'resultAfternoonTea', 'false');"><img src="/images/buttonSendRequest.gif" /></div>
    <div style="float: left; cursor: pointer; padding-top: 10px; padding-left: 21px; color: #e27423"  onclick="hide('quickToolsAfternoonTea_AddNewFood_Select');">or Cancel</div>

</div>
</div>


</td>
<td>&nbsp;</td>
<td style="font-weight: bold; text-align: center">Subtotal</td>
<td style="font-weight: bold; text-align: center" id="carb-4"><asp:Literal ID="literalAfternoonTeaCarb" runat="server"></asp:Literal></td>
<td style="font-weight: bold; text-align: center" id="protein-4"><asp:Literal ID="literalAfternoonTeaProtein" runat="server"></asp:Literal></td>
<td style="font-weight: bold; text-align: center" id="fat-4"><asp:Literal ID="literalAfternoonTeaFat" runat="server"></asp:Literal></td>
<td>&nbsp;</td>
</tr>
<tr><td colspan="8">
<div style="border: 1px solid #d1d1d1; display: none; width: 883px; background-color: #ececec" id="addItemAfternoonTea_Select" class="one_of">
<div style="cursor: pointer; display: inline" id="addItemAfternoonTeaSelected" onclick="hide('addItemAfternoonTea_Select');show('buttonsAfternoonTea');"><img src="/images/buttonAddItemSelected.gif" /></div>
<div class="additem_options">
    <div class="additem_option additem_option_selected"
         onclick="additem_option_selected(this);
                  show('addItemAfternoonTea_Search_Select');
                  hide('addItemAfternoonTea_CopyPaste_Select');
                  hide('addItemAfternoonTea_Recent_Select');
                  hide('addItemAfternoonTea_AddFood_Select');
                  clearRight();">Search & Add</div>
    <div class="additem_option" onclick="additem_option_selected(this);
                                         hide('addItemAfternoonTea_Search_Select');
                                         show('addItemAfternoonTea_CopyPaste_Select');
                                         hide('addItemAfternoonTea_Recent_Select');
                                         hide('addItemAfternoonTea_AddFood_Select');">Copy &amp; Paste</div>
    <div class="additem_option" onclick="additem_option_selected(this);
                                         hide('addItemAfternoonTea_Search_Select');
                                         hide('addItemAfternoonTea_CopyPaste_Select');
                                         show('addItemAfternoonTea_Recent_Select');
                                         hide('addItemAfternoonTea_AddFood_Select');">Recent</div>
    <%--<div class="additem_option" onclick="additem_option_selected(this);
                                         hide('addItemAfternoonTea_Search_Select');
                                         hide('addItemAfternoonTea_CopyPaste_Select');
                                         hide('addItemAfternoonTea_Recent_Select');
                                         show('addItemAfternoonTea_AddFood_Select');">Add Your Own Food</div>--%>
</div>
<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;margin-bottom: 10px;padding-bottom: 10px" id="addItemAfternoonTea_Search_Select">
    <div id="tabSearchByKeyword_AfternoonTea">
        <div style="font-weight: bold">
           <div style="font-weight: bold;color: #008CA7;">Keyword</div>
        </div>

        <div style="float:left;">
            <input type="text" id="searchByKeyword_AfternoonTea" style="height: 23px; width: 345px; border: 1px solid #999999; margin-top: 11px;padding-left: 35px"
                   onkeyup="searchItemsByAdvancedKeyword('AfternoonTea', 4);" />        
            <div style="position: absolute; top: 45px; left: 23px; cursor: pointer;"
                 onclick="searchItemsByAdvancedKeyword('AfternoonTea', 4);"><img src="/images/buttonSearchMag.gif" /></div>
        

        <div style="font-weight: bold;color: #008CA7;margin-top: 15px;width: 100%;">Macronutrients</div>
        <div style="font-weight: bold; padding-top: 12px;">
            Carbs:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchByCarbs_AfternoonTea" onkeyup="searchItemsByAdvanced('AfternoonTea', 4);" /> 
            Protein:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchByProtein_AfternoonTea" onkeyup="searchItemsByAdvanced('AfternoonTea', 4);"/> 
            Fat:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchByFat_AfternoonTea" onkeyup="searchItemsByAdvanced('AfternoonTea', 4);"/>
        </div>

        <div style="font-weight: bold;color: #008CA7;margin-top: 15px;">Category</div>
        <div style="font-weight: bold; padding-top: 12px;margin-bottom: 15px;">
            <asp:Literal ID="literalSearchByCategory_AfternoonTea" runat="server"></asp:Literal>
        </div>
        </div>
        <div class="rightSide" id="rightAfternoonTea"><!--button click takes to add new item panel-->
            <span style="display:inline-block;vertical-align:super;">Still can't find what you are looking for? Add new item</span>
            <input type="button" class="imgButtonAddItem" onclick="hide('addItemAfternoonTea_Search_Select');
                                                                   hide('addItemAfternoonTea_CopyPaste_Select');
                                                                   hide('addItemAfternoonTea_Recent_Select');
                                                                   show('addItemAfternoonTea_AddFood_Select');
                                                                   $('#AOFoodName_AfternoonTea').val($('#searchByKeyword_AfternoonTea').val());
                                                                   $('.search_results').hide();"/>
        </div>
        <div style="clear:both;"></div>
        <div id="searchByKeyword_AfternoonTea_Result" class="search_results" style="display: none"></div>
    </div>
    <div id="tabSearchByCategory_AfternoonTea" style="display: none">
        <span style="font-weight: bold">Search by Category</span>
        <a style="padding-left: 124px; font-weight: bold; color: #e27424" onclick="show('tabSearchByKeyword_AfternoonTea');hide('tabSearchByCategory_AfternoonTea');hide('tabSearchByMacronutrients_AfternoonTea');">By Keyword ></a>
        <a style="padding-left: 24px; font-weight: bold; color: #e27424" onclick="hide('tabSearchByKeyword_AfternoonTea');hide('tabSearchByCategory_AfternoonTea');show('tabSearchByMacronutrients_AfternoonTea');">By Macronutrients ></a>
        
        <div id="searchByCategory_AfternoonTea_Result" class="search_results" style="display: none"></div>
    </div>
    <div id="tabSearchByMacronutrients_AfternoonTea" style="display: none">
        <span style="font-weight: bold">Search by Macronutrients</span>
        <a style="padding-left: 124px; font-weight: bold; color: #e27424" onclick="show('tabSearchByKeyword_AfternoonTea');hide('tabSearchByCategory_AfternoonTea');hide('tabSearchByMacronutrients_AfternoonTea');">By Keyword ></a>
        <a style="padding-left: 24px; font-weight: bold; color: #e27424" onclick="hide('tabSearchByKeyword_AfternoonTea');show('tabSearchByCategory_AfternoonTea');hide('tabSearchByMacronutrients_AfternoonTea');">By Category ></a>
        
        <div id="searchByMacronutrients_AfternoonTea_Result" class="search_results" style="display: none"></div>
    </div>
    
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;display:none; margin-bottom: 10px;padding-bottom: 10px" id="addItemAfternoonTea_CopyPaste_Select">
    <div style="float: left; width: 200px"><span style="font-weight: bold">Select a date to copy from: </span><BDP:BasicDatePicker ID="bdpAfternoonTea" runat="server" DisplayType="Image" OnClientAfterSelectionChanged="getItemsCopyPaste"
        AutoPostBack="False" 
        ShowNoneButton="False" 
           ShowTodayButton="False" 
            ButtonImageFileName="calendar.gif" ButtonImageHeight="25" 
            ButtonImageWidth="26" RenderImages="File" ResourcePath="/images/" 
            DownYearSelectorImageFileName="" NextMonthImageFileName="calendar_forward.gif" 
            PrevMonthImageFileName="calendar_back.gif" RenderStyleSheet="None" TextBoxStyle-BackColor="White" TextBoxStyle-BorderColor="#999999" TextBoxStyle-BorderStyle="Solid" TextBoxStyle-BorderWidth="1px" TextBoxStyle-Height="23px" TextBoxStyle-Width="123px">
    </BDP:BasicDatePicker>
    </div>
    <div style="float: left; width: 610px; border-left: 1px solid #e5e5e5; min-height: 200px; padding-left: 30px">
    <div id="addItemAfternoonTea_CopyPaste_Results"></div>
    <p style="padding-top: 18px">Copy to which date: <% =CopyToDatesDropDown("copyToDateAfternoonTea")%></p>
    <div style="cursor: pointer; padding-top: 12px"  onclick="addCheckedItemsToDate('result_area_4', 'copyToDateAfternoonTea');"><img src="/images/buttonCopyPaste.gif"></div>
    </div>
    <div style="clear: both"></div>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;display:none; margin-bottom: 10px;padding-bottom: 10px" id="addItemAfternoonTea_Recent_Select">
    <select onchange="getRecentByMealtime($('#recentSearchAfternoonTea').val(), 'addItemAfternoonTea_Recent_Results', 4)" id="recentSearchAfternoonTea">
        <option value="0"></option>
        <option value="1">Breakfast</option>
        <option value="2">Morning Tea</option>
        <option value="3">Lunch</option>
        <option value="4">Afternoon Tea</option>
        <option value="5">Dinner</option>
        <option value="6">Supper</option>
        <option value="-1">All Meals</option>
    </select>
    <div id="addItemAfternoonTea_Recent_Results" class="search_results_recent"></div>
    </div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;display:none; margin-bottom: 10px;padding-bottom: 10px" id="addItemAfternoonTea_AddFood_Select" >
    <table>
        <tr style="font-weight: bold">
            <td>Food Name</td>
            <td style="padding-right: 10px">Brand Name</td>
            <td style="padding: 0px 25px">Amount</td>
            <td style="padding: 0px 21px">Serve</td>
            <td style="padding-left: 4px; padding-right: 20px;">Carbohydrate(g)</td> 
            <td>Protein(g)</td>
            <td style="padding-left: 38px ">Fat(g)</td>           
        </tr>
        <tr>
            <td><input type="text" id="AOFoodName_AfternoonTea" style="width: 180px" disabled/></td>
            <td style="padding-right: 10px"><input type="text" id="AOBrandName_AfternoonTea" placeholder="optional"  /></td>
            <td style="padding: 0px 25px"><input type="text" id="AOAmount_AfternoonTea" style="width: 48px"/></td>
            <td style="padding: 0px 21px"><input type="text" id="AOUnit_AfternoonTea" style="width: 50px" /></td>
            <td style="padding-left: 4px; padding-right: 20px; "><input type="text" id="AOCarbs_AfternoonTea" style="width: 50px;margin-left: 10px"/></td>
            <td><input type="text" id="AOPtn_AfternoonTea" style="width: 50px"/></td>
            <td style="padding-left: 25px "><input type="text" id="AOFat_AfternoonTea" style="width: 50px"/></td>
        </tr>
    </table>
    <i>Please note: Amount, Carbs, Protein and Fat entries are numerical only</i>
    <br/>
    <div style="float:left;cursor: pointer; display: inline" id="Div3" onclick="addOwnItem('4',$('#AOBrandName_AfternoonTea').val(),
                                                                    $('#AOFoodName_AfternoonTea').val(), $('#AOAmount_AfternoonTea').val(),
                                                                    $('#AOUnit_AfternoonTea').val(), $('#AOCarbs_AfternoonTea').val(),
                                                                    $('#AOPtn_AfternoonTea').val(),
                                                                    $('#AOFat_AfternoonTea').val())">
        <br/>
        <img src="/images/buttonSubmitOrange.gif" alt="Add Item" />
    </div>
    <div class="cancel" style="float:left;">
        <br />
        <img src="/images/buttonCancel.gif" alt="Cancel" />
    </div>
    <div style="clear:both;"></div>
</div>

</div>

<div style="border: 1px solid #d1d1d1; display: none; width: 883px; background-color: #ececec" id="addMealAfternoonTea_Select" class="one_of">
<div style="cursor: pointer; display: inline" id="addMealAfternoonTeaSelected" onclick="hide('addMealAfternoonTea_Select');show('buttonsAfternoonTea');"><img src="/images/buttonAddMealSelected.gif" /></div>
<div class="additem_options">
    <div class="additem_option additem_option_selected" onclick="additem_option_selected(this);show('addMealAfternoonTea_Search_Select');hide('addMealAfternoonTea_Recent_Select');">Search</div>
    <div class="additem_option" onclick="additem_option_selected(this);hide('addMealAfternoonTea_Search_Select');show('addMealAfternoonTea_Recent_Select');">Recent</div>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;margin-bottom: 10px;padding-bottom: 10px" id="addMealAfternoonTea_Search_Select">
        <div id="tabSearchMealByKeyword_AfternoonTea">
        <div style="font-weight: bold;color: #008CA7;">Keyword</div>
        <input type="text" id="searchMealByKeyword_AfternoonTea" style="height: 23px; width: 345px; border: 1px solid #999999; margin-top: 11px;padding-left: 35px"
               onkeyup="searchMealsByAdvancedKeyword('AfternoonTea', 4);" />
        <div style="position: absolute; top: 45px; left: 23px; cursor: pointer;"
             onclick="searchMealsByAdvancedKeyword('AfternoonTea', 4);"><img src="/images/buttonSearchMag.gif" /></div>
 
        <div class="clear"></div>
 
        <div style="font-weight: bold;color: #008CA7;margin-top: 15px;">Macronutrients</div>
 
        <div style="font-weight: bold; padding-top: 12px;">
            Carbs:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByCarbs_AfternoonTea" onkeyup="searchMealsByAdvanced('AfternoonTea', 4);" /> 
            Protein:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByProtein_AfternoonTea" onkeyup="searchMealsByAdvanced('AfternoonTea', 4);" /> 
            Fat:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByFat_AfternoonTea" onkeyup="searchMealsByAdvanced('AfternoonTea', 4);"/>
        </div>
 
        <div style="font-weight: bold;color: #008CA7;margin-top: 15px;">Meal Classification</div>
 
        <div data-textbox="searchMealByKeyword_AfternoonTea" class="mealTagAdvancedSearch">
            <div><input id="gf_AfternoonTea" type="checkbox" name="mealtag" value="gf" onclick="searchMealsByAdvanced('AfternoonTea', 4);">Gluten Free<br></div>
            <div><input id="vegan_AfternoonTea" type="checkbox" name="mealtag" value="vegan" onclick="searchMealsByAdvanced('AfternoonTea', 4);">Vegan<br></div>
            <div><input id="veg_AfternoonTea" type="checkbox" name="mealtag" value="vegetarian" onclick="searchMealsByAdvanced('AfternoonTea', 4);">Vegetarian<br></div>
            <div><input id="lf_AfternoonTea" type="checkbox" name="mealtag" value="lf" onclick="searchMealsByAdvanced('AfternoonTea', 4);">Lactose Free<br></div>
            <div><input id="sf_AfternoonTea" type="checkbox" name="mealtag" value="sf" onclick="searchMealsByAdvanced('AfternoonTea', 4);">Seafood Free<br></div>
            <div><input id="nf_AfternoonTea" type="checkbox" name="mealtag" value="nf" onclick="searchMealsByAdvanced('AfternoonTea', 4);">Nut Free<br></div>
        </div>
 
        <div id="searchMealByKeyword_AfternoonTea_Result" class="search_results" style="display: none"></div>
    </div>



    <div id="tabSearchMealByMacronutrients_AfternoonTea" style="display: none">
        <span style="font-weight: bold">Search by Macronutrients</span>
        <a style="padding-left: 124px; font-weight: bold; color: #e27424" onclick="show('tabSearchMealByKeyword_AfternoonTea');hide('tabSearchMealByMacronutrients_AfternoonTea');">By Keyword ></a>
        <div style="font-weight: bold; padding-top: 12px;">Carbs:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByCarbs_AfternoonTea" /> Protein:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByProtein_AfternoonTea" /> Fat:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByFat_AfternoonTea" />
        <div style="display: inline-block; cursor: pointer; position: relative; left: -20px; top: 4px;"  onclick="getMealsByMacronutrients($('#searchMealByCarbs_AfternoonTea').val(),$('#searchMealByProtein_AfternoonTea').val(),$('#searchMealByFat_AfternoonTea').val(),'searchMealByMacronutrients_AfternoonTea_Result',4);"><img src="/images/buttonSearchMag.gif" /></div>
        </div>
        <div id="searchMealByMacronutrients_AfternoonTea_Result" class="search_results" style="display: none"></div>
    </div>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;display:none; margin-bottom: 10px;padding-bottom: 10px" id="addMealAfternoonTea_Recent_Select">
    <select onchange="getRecentMealsByMealtime($('#recentSearchMealAfternoonTea').val(), 'addMealAfternoonTea_Recent_Results',4)" id="recentSearchMealAfternoonTea">
        <option value="0"></option>
        <option value="1">Breakfast</option>
        <option value="2">Morning Tea</option>
        <option value="3">Lunch</option>
        <option value="4">Afternoon Tea</option>
        <option value="5">Dinner</option>
        <option value="6">Supper</option>
        <option value="-1">All Meals</option>

    </select>
    <div id="addMealAfternoonTea_Recent_Results" class="search_results_recent"></div>
    </div>
</div>

<div class="result" id="resultAfternoonTea" style="display: none;"></div>
</td></tr>

<tr>
<td style="border-bottom: 1px solid #e5e5e5; padding-top: 15px; padding-bottom: 10px; font-size: 14px; font-weight: bold; padding-left: 11px;" colspan="8" >Dinner<span class="balloonright"><asp:Literal ID="LiteralDinnerMood" runat="server"></asp:Literal></span></td>
</tr>
<asp:Literal ID="literalDinnerRows" runat="server"></asp:Literal>
<tr class="buttons mealtime5"  id="buttonsDinner">
<td style="padding-top: 5px" colspan="2"><div style="position: relative">
<div style="cursor: pointer; display: inline" id="addItemDinner" onclick="hide_one_of();show('addItemDinner_Select');hide('buttonsDinner');"><img src="/images/buttonAddItem.gif" /></div>
<div style="cursor: pointer; display: inline" id="addMealDinner" onclick="hide_one_of();show('addMealDinner_Select');hide('buttonsDinner');"><img src="/images/buttonAddMeal.gif" /></div>
<div style="cursor: pointer; display: inline; margin-left: 13px" id="quickToolsDinner" onclick="var result = toggle_str('quickToolsDinner_Select');hide_one_of();switchto('quickToolsDinner_Select', result);"><img src="/images/buttonQuickTools.gif" /></div>

<div class="quicktools_options one_of" id="quickToolsDinner_Select">
    <div class="quicktools_option" onclick="hide_one_of();show('quickToolsDinner_SaveAsMeal_Select');">Save as meal</div>
    <div class="quicktools_option" onclick="hide_one_of();show('quickToolsDinner_CopyAndPaste_Select');">Copy &amp; paste</div>
</div>

<div class="one_of" style="z-index: 100; left: 114px; top: 29px; border: 1px solid #a0a0a0; display: none; position: absolute; width: 186px; height: 92px; padding-top: 6px; padding-left: 12px; background-color: #f1f1f1; font-weight: bold" id="quickToolsDinner_SaveAsMeal_Select">
    <p>Name this meal</p>
    <input type="text" id="saveAsMealName_Dinner" style="width: 172px; height: 23px; margin-top: 10px; border: 1px solid #999999" />
    <div style="float: left; cursor: pointer; padding-top: 5px;"  onclick="hide('quickToolsDinner_SaveAsMeal_Select');saveDiaryMealAsMeal(5,$('#saveAsMealName_Dinner').val(),'resultDinner');"><img src="/images/buttonSaveMeal.gif" /></div>
    <div style="float: left; cursor: pointer; padding-top: 10px; padding-left: 21px; color: #e27423"  onclick="hide('quickToolsDinner_SaveAsMeal_Select');">or Cancel</div>
</div>
<div class="one_of" style="z-index: 100; left: 114px; top: 29px; border: 1px solid #a0a0a0; display: none; position: absolute; width: 198px; height: 200px; background-color: #f1f1f1" id="quickToolsDinner_CopyAndPaste_Select">
    <div class="quicktools_copyto_header">Copy to which date?</div>
    <% =CopyToDates(5, "quickToolsDinner_CopyAndPaste_Select", "resultDinner")%>
</div>
<div class="one_of" style="z-index: 100; left: 114px; top: 29px; border: 1px solid #a0a0a0; display: none; position: absolute; width: 236px; height: 328px;  padding-top: 6px; padding-left: 12px; margin-top: -353px; background-color: #f1f1f1" id="quickToolsDinner_AddNewFood_Select">
    <p style="font-weight: bold">Food Item Request</p>
    <p style="padding-top: 10px">If you would like a food item to be added to the database please fill in the form below. Please note that only foods with a brand and product name can be added to the main food database.
</p>
   <input type="text" id="addNewFood_DinnerText1" style="width: 224px" placeholder="Brand Name (optional)"/>
        <input type="text" id="addNewFood_DinnerText2" style="width: 224px" placeholder="Product Name (Required)"/>
        <input type="text" id="addNewFood_DinnerText3" style="width: 224px"  placeholder="Serving Size (optional)"/>
        <input type="text" id="addNewFood_DinnerText4" style="width: 224px"  placeholder="Serving Unit (optional)" />
        <input type="text" id="addNewFood_DinnerText5" style="width: 224px"  placeholder="Carbohydrates per serving (optional)" />
        <input type="text" id="addNewFood_DinnerText6" style="width: 224px"  placeholder="Protein per serving (optional)"/>
        <input type="text" id="addNewFood_DinnerText7" style="width: 224px"  placeholder="Fat per serving (optional)"/> 
    <div style="float: left; cursor: pointer; padding-top: 5px;" onclick="hide('quickToolsDinner_AddNewFood_Select');foodItemRequest('Brand= ' + $('#addNewFood_DinnerText1').val()+ 
                                                                                                                    '. product = ' + $('#addNewFood_DinnerText2').val()+ 
                                                                                                                    '. ServSize= ' +  $('#addNewFood_DinnerText3').val() +
                                                                                                                    '. ServUnit = ' +$('#addNewFood_DinnerText4').val() +
                                                                                                                    '. Carb= ' + $('#addNewFood_DinnerText5').val()+
                                                                                                                    '. Ptn= ' + $('#addNewFood_DinnerText6').val() +
                                                                                                                    '. Fat= ' + $('#addNewFood_DinnerText7').val(),'resultDinner', 'false');"><img src="/images/buttonSendRequest.gif" /></div>
    <div style="float: left; cursor: pointer; padding-top: 10px; padding-left: 21px; color: #e27423"  onclick="hide('quickToolsDinner_AddNewFood_Select');">or Cancel</div>

</div>
</div>


</td>
<td>&nbsp;</td>
<td style="font-weight: bold; text-align: center">Subtotal</td>
<td style="font-weight: bold; text-align: center" id="carb-5"><asp:Literal ID="literalDinnerCarb" runat="server"></asp:Literal></td>
<td style="font-weight: bold; text-align: center" id="protein-5"><asp:Literal ID="literalDinnerProtein" runat="server"></asp:Literal></td>
<td style="font-weight: bold; text-align: center" id="fat-5"><asp:Literal ID="literalDinnerFat" runat="server"></asp:Literal></td>
<td>&nbsp;</td>
</tr>
<tr><td colspan="8">
<div style="border: 1px solid #d1d1d1; display: none; width: 883px; background-color: #ececec" id="addItemDinner_Select" class="one_of">
<div style="cursor: pointer; display: inline" id="addItemDinnerSelected" onclick="hide('addItemDinner_Select');show('buttonsDinner');"><img src="/images/buttonAddItemSelected.gif" /></div>
<div class="additem_options">
    <div class="additem_option additem_option_selected"
         onclick="additem_option_selected(this);
                  show('addItemDinner_Search_Select');
                  hide('addItemDinner_CopyPaste_Select');
                  hide('addItemDinner_Recent_Select');
                  hide('addItemDinner_AddFood_Select'); 
                  clearRight();">Search & Add</div>
    <div class="additem_option" onclick="additem_option_selected(this);
                                         hide('addItemDinner_Search_Select');
                                         show('addItemDinner_CopyPaste_Select');
                                         hide('addItemDinner_Recent_Select');
                                         hide('addItemDinner_AddFood_Select');">Copy &amp; Paste</div>
    <div class="additem_option" onclick="additem_option_selected(this);
                                         hide('addItemDinner_Search_Select');
                                         hide('addItemDinner_CopyPaste_Select');
                                         show('addItemDinner_Recent_Select');
                                         hide('addItemDinner_AddFood_Select');">Recent</div>
    <%--<div class="additem_option" onclick="additem_option_selected(this);
                                         hide('addItemDinner_Search_Select');
                                         hide('addItemDinner_CopyPaste_Select');
                                         hide('addItemDinner_Recent_Select');
                                         show('addItemDinner_AddFood_Select');">Add Your Own Food</div>--%>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;margin-bottom: 10px;padding-bottom: 10px" id="addItemDinner_Search_Select">
    <div id="tabSearchByKeyword_Dinner">
        <div style="font-weight: bold">
           <div style="font-weight: bold;color: #008CA7;">Keyword</div>
        </div>

        <div style="float:left;">
            <input type="text" id="searchByKeyword_Dinner" style="height: 23px; width: 345px; border: 1px solid #999999; margin-top: 11px;padding-left: 35px"
                   onkeyup="searchItemsByAdvancedKeyword('Dinner', 5);" />
        
            <div style="position: absolute; top: 45px; left: 23px; cursor: pointer;"  onclick="searchItemsByAdvancedKeyword('Dinner', 5);"><img src="/images/buttonSearchMag.gif" /></div>
            
            <div style="font-weight: bold;color: #008CA7;margin-top: 15px;">Macronutrients</div>
            <div style="font-weight: bold; padding-top: 12px;">
                <div style="font-weight: bold; padding-top: 12px;">
                    Carbs:<input onkeyup="searchItemsByAdvanced('Dinner', 5);" style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchByCarbs_Dinner" /> 
                    Protein:<input onkeyup="searchItemsByAdvanced('Dinner', 5);" style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchByProtein_Dinner" /> 
                    Fat:<input onkeyup="searchItemsByAdvanced('Dinner', 5);" style ="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchByFat_Dinner" />
                </div>
            </div>

            <div style="font-weight: bold;color: #008CA7;margin-top: 15px;">Category</div>
            <div style="font-weight: bold; padding-top: 12px;margin-bottom: 15px;">
                <asp:Literal ID="literalSearchByCategory_Dinner" runat="server"></asp:Literal>
            </div>
        </div>

        <div class="rightSide" id="rightDinner"><!--button click takes to add new item panel-->
            <span style="display:inline-block;vertical-align:super;">Still can't find what you are looking for? Add new item</span>
            <input type="button" class="imgButtonAddItem" onclick="hide('addItemDinner_Search_Select');
                                                                   hide('addItemDinner_CopyPaste_Select');
                                                                   hide('addItemDinner_Recent_Select');
                                                                   show('addItemDinner_AddFood_Select');
                                                                   $('#AOFoodName_Dinner').val($('#searchByKeyword_Dinner').val());
                                                                   $('.search_results').hide();"/>
        </div>
        <div style="clear:both;"></div>
        <div id="searchByKeyword_Dinner_Result" class="search_results" style="display: none"></div>
    </div>
    <div id="tabSearchByCategory_Dinner" style="display: none">
        <span style="font-weight: bold">Search by Category</span>
        <a style="padding-left: 124px; font-weight: bold; color: #e27424" onclick="show('tabSearchByKeyword_Dinner');hide('tabSearchByCategory_Dinner');hide('tabSearchByMacronutrients_Dinner');">By Keyword ></a>
        <a style="padding-left: 24px; font-weight: bold; color: #e27424" onclick="hide('tabSearchByKeyword_Dinner');hide('tabSearchByCategory_Dinner');show('tabSearchByMacronutrients_Dinner');">By Macronutrients ></a>
        
        <div id="searchByCategory_Dinner_Result" class="search_results" style="display: none"></div>
    </div>
    <div id="tabSearchByMacronutrients_Dinner" style="display: none">
        <span style="font-weight: bold">Search by Macronutrients</span>
        <a style="padding-left: 124px; font-weight: bold; color: #e27424" onclick="show('tabSearchByKeyword_Dinner');hide('tabSearchByCategory_Dinner');hide('tabSearchByMacronutrients_Dinner');">By Keyword ></a>
        <a style="padding-left: 24px; font-weight: bold; color: #e27424" onclick="hide('tabSearchByKeyword_Dinner');show('tabSearchByCategory_Dinner');hide('tabSearchByMacronutrients_Dinner');">By Category ></a>
        
        <div id="searchByMacronutrients_Dinner_Result" class="search_results" style="display: none"></div>
    </div>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;display:none; margin-bottom: 10px;padding-bottom: 10px" id="addItemDinner_CopyPaste_Select">
    <div style="float: left; width: 200px"><span style="font-weight: bold">Select a date to copy from: </span><BDP:BasicDatePicker ID="bdpDinner" runat="server" DisplayType="Image" OnClientAfterSelectionChanged="getItemsCopyPaste"
        AutoPostBack="False" 
        ShowNoneButton="False" 
           ShowTodayButton="False" 
            ButtonImageFileName="calendar.gif" ButtonImageHeight="25" 
            ButtonImageWidth="26" RenderImages="File" ResourcePath="/images/" 
            DownYearSelectorImageFileName="" NextMonthImageFileName="calendar_forward.gif" 
            PrevMonthImageFileName="calendar_back.gif" RenderStyleSheet="None" TextBoxStyle-BackColor="White" TextBoxStyle-BorderColor="#999999" TextBoxStyle-BorderStyle="Solid" TextBoxStyle-BorderWidth="1px" TextBoxStyle-Height="23px" TextBoxStyle-Width="123px">
    </BDP:BasicDatePicker>
    </div>
    <div style="float: left; width: 610px; border-left: 1px solid #e5e5e5; min-height: 200px; padding-left: 30px">
    <div id="addItemDinner_CopyPaste_Results"></div>
    <p style="padding-top: 18px">Copy to which date: <% =CopyToDatesDropDown("copyToDateDinner")%></p>
    <div style="cursor: pointer; padding-top: 12px"  onclick="addCheckedItemsToDate('result_area_5', 'copyToDateDinner');"><img src="/images/buttonCopyPaste.gif"></div>
    </div>
    <div style="clear: both"></div>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;display:none; margin-bottom: 10px;padding-bottom: 10px" id="addItemDinner_Recent_Select">
    <select onchange="getRecentByMealtime($('#recentSearchDinner').val(), 'addItemDinner_Recent_Results', 5)" id="recentSearchDinner">
        <option value="0"></option>
        <option value="1">Breakfast</option>
        <option value="2">Morning Tea</option>
        <option value="3">Lunch</option>
        <option value="4">Afternoon Tea</option>
        <option value="5">Dinner</option>
        <option value="6">Supper</option>
        <option value="-1">All Meals</option>
    </select>
    <div id="addItemDinner_Recent_Results" class="search_results_recent"></div>
    </div>
    
<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;display:none; margin-bottom: 10px;padding-bottom: 10px" id="addItemDinner_AddFood_Select">
    <table>
        <tr style="font-weight: bold">
            <td>Food Name</td>
            <td style="padding-right: 10px">Brand Name</td>
            <td style="padding: 0px 25px">Amount</td>
            <td style="padding: 0px 21px">Serve</td>
            <td style="padding-left: 4px; padding-right: 20px;">Carbohydrate(g)</td> 
            <td>Protein(g)</td>
            <td style="padding-left: 38px ">Fat(g)</td>           
        </tr>
        <tr>
            <td><input type="text" id="AOFoodName_Dinner" style="width: 180px" disabled/></td>
            <td style="padding-right: 10px"><input type="text" id="AOBrandName_Dinner" placeholder="optional"  /></td>
            <td style="padding: 0px 25px"><input type="text" id="AOAmount_Dinner" style="width: 48px"/></td>
            <td style="padding: 0px 21px"><input type="text" id="AOUnit_Dinner" style="width: 50px" /></td>
            <td style="padding-left: 4px; padding-right: 20px; "><input type="text" id="AOCarbs_Dinner" style="width: 50px;margin-left: 10px"/></td>
            <td><input type="text" id="AOPtn_Dinner" style="width: 50px"/></td>
            <td style="padding-left: 25px "><input type="text" id="AOFat_Dinner" style="width: 50px"/></td>
        </tr>
    </table>
    <i>Please note: Amount, Carbs, Protein and Fat entries are numerical only</i>
    <br/>
    <div style="float:left;cursor: pointer; display: inline" id="Div4" onclick="addOwnItem('5',$('#AOBrandName_Dinner').val(),
                                                                    $('#AOFoodName_Dinner').val(), $('#AOAmount_Dinner').val(),
                                                                    $('#AOUnit_Dinner').val(), $('#AOCarbs_Dinner').val(),
                                                                    $('#AOPtn_Dinner').val(),
                                                                    $('#AOFat_Dinner').val())">
        <br/>
        <img src="/images/buttonSubmitOrange.gif" alt="Add Item" />
    </div>
    <div class="cancel" style="float:left;">
        <br />
        <img src="/images/buttonCancel.gif" alt="Cancel" />
    </div>
    <div style="clear:both;"></div>
</div>
</div>

<div style="border: 1px solid #d1d1d1; display: none; width: 883px; background-color: #ececec" id="addMealDinner_Select" class="one_of">
<div style="cursor: pointer; display: inline" id="addMealDinnerSelected" onclick="hide('addMealDinner_Select');show('buttonsDinner');"><img src="/images/buttonAddMealSelected.gif" /></div>
<div class="additem_options">
    <div class="additem_option additem_option_selected" onclick="additem_option_selected(this);show('addMealDinner_Search_Select');hide('addMealDinner_Recent_Select');">Search</div>
    <div class="additem_option" onclick="additem_option_selected(this);hide('addMealDinner_Search_Select');show('addMealDinner_Recent_Select');">Recent</div>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;margin-bottom: 10px;padding-bottom: 10px" id="addMealDinner_Search_Select">
        <div id="tabSearchMealByKeyword_Dinner">
        <div style="font-weight: bold;color: #008CA7;">Keyword</div>
        <input type="text" id="searchMealByKeyword_Dinner" style="height: 23px; width: 345px; border: 1px solid #999999; margin-top: 11px;padding-left: 35px"
               onkeyup="searchMealsByAdvancedKeyword('Dinner', 5);" />
        <div style="position: absolute; top: 45px; left: 23px; cursor: pointer;"
             onclick="searchMealsByAdvancedKeyword('Dinner', 5);"><img src="/images/buttonSearchMag.gif" /></div>
 
        <div class="clear"></div>
 
        <div style="font-weight: bold;color: #008CA7;margin-top: 15px;">Macronutrients</div>
 
        <div style="font-weight: bold; padding-top: 12px;">
            Carbs:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByCarbs_Dinner" onkeyup="searchMealsByAdvanced('Dinner', 5);" /> 
            Protein:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByProtein_Dinner" onkeyup="searchMealsByAdvanced('Dinner', 5);" /> 
            Fat:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByFat_Dinner" onkeyup="searchMealsByAdvanced('Dinner', 5);"/>
        </div>
 
        <div style="font-weight: bold;color: #008CA7;margin-top: 15px;">Meal Classification</div>
 
        <div data-textbox="searchMealByKeyword_Dinner" class="mealTagAdvancedSearch">
            <div><input id="gf_Dinner" type="checkbox" name="mealtag" value="gf" onclick="searchMealsByAdvanced('Dinner', 5);">Gluten Free<br></div>
            <div><input id="vegan_Dinner" type="checkbox" name="mealtag" value="vegan" onclick="searchMealsByAdvanced('Dinner', 5);">Vegan<br></div>
            <div><input id="veg_Dinner" type="checkbox" name="mealtag" value="vegetarian" onclick="searchMealsByAdvanced('Dinner', 5);">Vegetarian<br></div>
            <div><input id="lf_Dinner" type="checkbox" name="mealtag" value="lf" onclick="searchMealsByAdvanced('Dinner', 5);">Lactose Free<br></div>
            <div><input id="sf_Dinner" type="checkbox" name="mealtag" value="sf" onclick="searchMealsByAdvanced('Dinner', 5);">Seafood Free<br></div>
            <div><input id="nf_Dinner" type="checkbox" name="mealtag" value="nf" onclick="searchMealsByAdvanced('Dinner', 5);">Nut Free<br></div>
        </div>
 
        <div id="searchMealByKeyword_Dinner_Result" class="search_results" style="display: none"></div>
    </div>


    <div id="tabSearchMealByMacronutrients_Dinner" style="display: none">
        <span style="font-weight: bold">Search by Macronutrients</span>
        <a style="padding-left: 124px; font-weight: bold; color: #e27424" onclick="show('tabSearchMealByKeyword_Dinner');hide('tabSearchMealByMacronutrients_Dinner');">By Keyword ></a>
        <div style="font-weight: bold; padding-top: 12px;">Carbs:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByCarbs_Dinner" /> Protein:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByProtein_Dinner" /> Fat:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByFat_Dinner" />
        <div style="display: inline-block; cursor: pointer; position: relative; left: -20px; top: 4px;"  onclick="getMealsByMacronutrients($('#searchMealByCarbs_Dinner').val(),$('#searchMealByProtein_Dinner').val(),$('#searchMealByFat_Dinner').val(),'searchMealByMacronutrients_Dinner_Result',5);"><img src="/images/buttonSearchMag.gif" /></div>
        </div>
        <div id="searchMealByMacronutrients_Dinner_Result" class="search_results" style="display: none"></div>
    </div>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;display:none; margin-bottom: 10px;padding-bottom: 10px" id="addMealDinner_Recent_Select">
    <select onchange="getRecentMealsByMealtime($('#recentSearchMealDinner').val(), 'addMealDinner_Recent_Results',5)" id="recentSearchMealDinner">
        <option value="0"></option>
        <option value="1">Breakfast</option>
        <option value="2">Morning Tea</option>
        <option value="3">Lunch</option>
        <option value="4">Afternoon Tea</option>
        <option value="5">Dinner</option>
        <option value="6">Supper</option>
        <option value="-1">All Meals</option>
    </select>
    <div id="addMealDinner_Recent_Results" class="search_results_recent"></div>
    </div>
</div>
<div class="result" id="resultDinner" style="display: none;"></div>
</td></tr>

<tr>
<td style="border-bottom: 1px solid #e5e5e5; padding-top: 15px; padding-bottom: 10px; font-size: 14px; font-weight: bold; padding-left: 11px;" colspan="8">Supper<span class="balloonright"><asp:Literal ID="LiteralSupperMood" runat="server"></asp:Literal></span></td>
</tr>
<asp:Literal ID="literalSupperRows" runat="server"></asp:Literal>
<tr class="buttons mealtime6"  id="buttonsSupper">
<td style="padding-top: 5px" colspan="2"><div style="position: relative">
<div style="cursor: pointer; display: inline" id="addItemSupper" onclick="hide_one_of();show('addItemSupper_Select');hide('buttonsSupper');"><img src="/images/buttonAddItem.gif" /></div>
<div style="cursor: pointer; display: inline" id="addMealSupper" onclick="hide_one_of();show('addMealSupper_Select');hide('buttonsSupper');"><img src="/images/buttonAddMeal.gif" /></div>
<div style="cursor: pointer; display: inline; margin-left: 13px" id="quickToolsSupper" onclick="var result = toggle_str('quickToolsSupper_Select');hide_one_of();switchto('quickToolsSupper_Select', result);"><img src="/images/buttonQuickTools.gif" /></div>

<div class="quicktools_options one_of" id="quickToolsSupper_Select">
    <div class="quicktools_option" onclick="hide_one_of();show('quickToolsSupper_SaveAsMeal_Select');">Save as meal</div>
    <div class="quicktools_option" onclick="hide_one_of();show('quickToolsSupper_CopyAndPaste_Select');">Copy &amp; paste</div>
</div>

<div class="one_of" style="z-index: 100; left: 114px; top: 29px; border: 1px solid #a0a0a0; display: none; position: absolute; width: 186px; height: 92px; padding-top: 6px; padding-left: 12px; background-color: #f1f1f1; font-weight: bold" id="quickToolsSupper_SaveAsMeal_Select">
    <p>Name this meal</p>
    <input type="text" id="saveAsMealName_Supper" style="width: 172px; height: 23px; margin-top: 10px; border: 1px solid #999999" />
    <div style="float: left; cursor: pointer; padding-top: 5px;"  onclick="hide('quickToolsSupper_SaveAsMeal_Select');saveDiaryMealAsMeal(6,$('#saveAsMealName_Supper').val(),'resultSupper');"><img src="/images/buttonSaveMeal.gif" /></div>
    <div style="float: left; cursor: pointer; padding-top: 10px; padding-left: 21px; color: #e27423"  onclick="hide('quickToolsSupper_SaveAsMeal_Select');">or Cancel</div>
</div>
<div class="one_of" style="z-index: 100; left: 114px; top: 29px; border: 1px solid #a0a0a0; display: none; position: absolute; width: 198px; height: 200px; background-color: #f1f1f1" id="quickToolsSupper_CopyAndPaste_Select">
    <div class="quicktools_copyto_header">Copy to which date?</div>
    <% =CopyToDates(6, "quickToolsSupper_CopyAndPaste_Select", "resultSupper")%>
</div>
<div class="one_of" style="z-index: 100; left: 114px; top: 29px; border: 1px solid #a0a0a0; display: none; position: absolute; width: 236px; height: 328px;  padding-top: 6px; padding-left: 12px; margin-top: -353px; background-color: #f1f1f1;" id="quickToolsSupper_AddNewFood_Select">
    <p style="font-weight: bold">Food Item Request</p>
    <p style="padding-top: 10px">If you would like a food item to be added to the database please fill in the form below. Please note that only foods with a brand and product name can be added to the main food database.
</p>
       <input type="text" id="addNewFood_SupperText1" style="width: 224px" placeholder="Brand Name (optional)"/>
        <input type="text" id="addNewFood_SupperText2" style="width: 224px" placeholder="Product Name (Required)"/>
        <input type="text" id="addNewFood_SupperText3" style="width: 224px"  placeholder="Serving Size (optional)"/>
        <input type="text" id="addNewFood_SupperText4" style="width: 224px"  placeholder="Serving Unit (optional)" />
        <input type="text" id="addNewFood_SupperText5" style="width: 224px"  placeholder="Carbohydrates per serving (optional)" />
        <input type="text" id="addNewFood_SupperText6" style="width: 224px"  placeholder="Protein per serving (optional)"/>
        <input type="text" id="addNewFood_SupperText7" style="width: 224px"  placeholder="Fat per serving (optional)"/> 
    <div style="float: left; cursor: pointer; padding-top: 5px;" onclick="hide('quickToolsSupper_AddNewFood_Select');foodItemRequest('Brand= ' + $('#addNewFood_SupperText1').val()+ 
                                                                                                                    '. product = ' + $('#addNewFood_SupperText2').val()+ 
                                                                                                                    '. ServSize= ' +  $('#addNewFood_SupperText3').val() +
                                                                                                                    '. ServUnit = ' +$('#addNewFood_SupperText4').val() +
                                                                                                                    '. Carb= ' + $('#addNewFood_SupperText5').val()+
                                                                                                                    '. Ptn= ' + $('#addNewFood_SupperText6').val() +
                                                                                                                    '. Fat= ' + $('#addNewFood_SupperText7').val(),'resultSupper', 'false');"><img src="/images/buttonSendRequest.gif" /></div>
    <div style="float: left; cursor: pointer; padding-top: 10px; padding-left: 21px; color: #e27423"  onclick="hide('quickToolsSupper_AddNewFood_Select');">or Cancel</div>

</div>
</div>


</td>
<td>&nbsp;</td>
<td style="font-weight: bold; text-align: center">Subtotal</td>
<td style="font-weight: bold; text-align: center" id="carb-6"><asp:Literal ID="literalSupperCarb" runat="server"></asp:Literal></td>
<td style="font-weight: bold; text-align: center" id="protein-6"><asp:Literal ID="literalSupperProtein" runat="server"></asp:Literal></td>
<td style="font-weight: bold; text-align: center" id="fat-6"><asp:Literal ID="literalSupperFat" runat="server"></asp:Literal></td>
<td>&nbsp;</td>
</tr>
<tr><td colspan="8">
<div style="border: 1px solid #d1d1d1; display: none; width: 883px; background-color: #ececec" id="addItemSupper_Select" class="one_of">
<div style="cursor: pointer; display: inline" id="addItemSupperSelected" onclick="hide('addItemSupper_Select');show('buttonsSupper');"><img src="/images/buttonAddItemSelected.gif" /></div>
<div class="additem_options">
    <div class="additem_option additem_option_selected" onclick="additem_option_selected(this);
                                                                 show('addItemSupper_Search_Select');
                                                                 hide('addItemSupper_CopyPaste_Select');
                                                                 hide('addItemSupper_Recent_Select');
                                                                 hide('addItemSupper_AddFood_Select');
                                                                  clearRight();">Search & Add</div>
    <div class="additem_option" onclick="additem_option_selected(this);
                                         hide('addItemSupper_Search_Select');
                                         show('addItemSupper_CopyPaste_Select');
                                         hide('addItemSupper_Recent_Select');
                                         hide('addItemSupper_AddFood_Select');">Copy &amp; Paste</div>
    <div class="additem_option" onclick="additem_option_selected(this);
                                         hide('addItemSupper_Search_Select');
                                         hide('addItemSupper_CopyPaste_Select');
                                         show('addItemSupper_Recent_Select');
                                         hide('addItemSupper_AddFood_Select');">Recent</div>
    <%--<div class="additem_option" onclick="additem_option_selected(this);
                                         hide('addItemSupper_Search_Select');
                                         hide('addItemSupper_CopyPaste_Select');
                                         hide('addItemSupper_Recent_Select');
                                         show('addItemSupper_AddFood_Select');">Add Your Own Food</div>--%>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;margin-bottom: 10px;padding-bottom: 10px" id="addItemSupper_Search_Select">
    <div id="tabSearchByKeyword_Supper">
        <div style="font-weight: bold">
           <div style="font-weight: bold;color: #008CA7;">Keyword</div>
        </div>

        <div style="float:left;">
            <input type="text" id="searchByKeyword_Supper" style="height: 23px; width: 345px; border: 1px solid #999999; margin-top: 11px;padding-left: 35px"
                   onkeyup="searchItemsByAdvancedKeyword('Supper', 6);"/>       
            <div style="position: absolute; top: 45px; left: 23px; cursor: pointer;"
                 onclick="searchItemsByAdvancedKeyword('Supper', 6);"><img src="/images/buttonSearchMag.gif" /></div>

            <div style="font-weight: bold;color: #008CA7;margin-top: 15px;">Macronutrients</div>
            <div style="font-weight: bold; padding-top: 12px;">
                <div style="font-weight: bold; padding-top: 12px;">
                    Carbs:<input onkeyup="searchItemsByAdvanced('Supper', 6);" style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchByCarbs_Supper" /> 
                    Protein:<input onkeyup="searchItemsByAdvanced('Supper', 6);" style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchByProtein_Supper" /> 
                    Fat:<input onkeyup="searchItemsByAdvanced('Supper', 6);" style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchByFat_Supper" />
                </div>
            </div>

            <div style="font-weight: bold;color: #008CA7;margin-top: 15px;">Category</div>
            <div style="font-weight: bold; padding-top: 12px;margin-bottom: 15px;">
                <asp:Literal ID="literalSearchByCategory_Supper" runat="server"></asp:Literal>
            </div>

        </div>
        <div class="rightSide" id="rightSupper"><!--button click takes to add new item panel-->
            <span style="display:inline-block;vertical-align:super;">Still can't find what you are looking for? Add new item</span>
            <input type="button" class="imgButtonAddItem" onclick="hide('addItemSupper_Search_Select');
                                                                   hide('addItemSupper_CopyPaste_Select');
                                                                   hide('addItemSupper_Recent_Select');
                                                                   show('addItemSupper_AddFood_Select');
                                                                   $('#AOFoodName_Supper').val($('#searchByKeyword_Supper').val());
                                                                   $('.search_results').hide();"/>
        </div>
        <div style="clear:both;"></div>
        <div id="searchByKeyword_Supper_Result" class="search_results" style="display: none"></div>
    </div>
    <div id="tabSearchByCategory_Supper" style="display: none">
        <span style="font-weight: bold">Search by Category</span>
        <a style="padding-left: 124px; font-weight: bold; color: #e27424" onclick="show('tabSearchByKeyword_Supper');hide('tabSearchByCategory_Supper');hide('tabSearchByMacronutrients_Supper');">By Keyword ></a>
        <a style="padding-left: 24px; font-weight: bold; color: #e27424" onclick="hide('tabSearchByKeyword_Supper');hide('tabSearchByCategory_Supper');show('tabSearchByMacronutrients_Supper');">By Macronutrients ></a>
        
        <div id="searchByCategory_Supper_Result" class="search_results" style="display: none"></div>
    </div>
    <div id="tabSearchByMacronutrients_Supper" style="display: none">
        <span style="font-weight: bold">Search by Macronutrients</span>
        <a style="padding-left: 124px; font-weight: bold; color: #e27424" onclick="show('tabSearchByKeyword_Supper');hide('tabSearchByCategory_Supper');hide('tabSearchByMacronutrients_Supper');">By Keyword ></a>
        <a style="padding-left: 24px; font-weight: bold; color: #e27424" onclick="hide('tabSearchByKeyword_Supper');show('tabSearchByCategory_Supper');hide('tabSearchByMacronutrients_Supper');">By Category ></a>
        
        <div id="searchByMacronutrients_Supper_Result" class="search_results" style="display: none"></div>
    </div>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;display:none; margin-bottom: 10px;padding-bottom: 10px" id="addItemSupper_CopyPaste_Select">
    <div style="float: left; width: 200px"><span style="font-weight: bold">Select a date to copy from: </span><BDP:BasicDatePicker ID="bdpSupper" runat="server" DisplayType="Image" OnClientAfterSelectionChanged="getItemsCopyPaste"
        AutoPostBack="False" 
        ShowNoneButton="False" 
           ShowTodayButton="False" 
            ButtonImageFileName="calendar.gif" ButtonImageHeight="25" 
            ButtonImageWidth="26" RenderImages="File" ResourcePath="/images/" 
            DownYearSelectorImageFileName="" NextMonthImageFileName="calendar_forward.gif" 
            PrevMonthImageFileName="calendar_back.gif" RenderStyleSheet="None" TextBoxStyle-BackColor="White" TextBoxStyle-BorderColor="#999999" TextBoxStyle-BorderStyle="Solid" TextBoxStyle-BorderWidth="1px" TextBoxStyle-Height="23px" TextBoxStyle-Width="123px">
    </BDP:BasicDatePicker>
    </div>
    <div style="float: left; width: 610px; border-left: 1px solid #e5e5e5; min-height: 200px; padding-left: 30px">
    <div id="addItemSupper_CopyPaste_Results"></div>
    <p style="padding-top: 18px">Copy to which date: <% =CopyToDatesDropDown("copyToDateSupper")%></p>
    <div style="cursor: pointer; padding-top: 12px"  onclick="addCheckedItemsToDate('result_area_6', 'copyToDateSupper');"><img src="/images/buttonCopyPaste.gif"></div>
    </div>
    <div style="clear: both"></div>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;display:none; margin-bottom: 10px;padding-bottom: 10px" id="addItemSupper_Recent_Select">
    <select onchange="getRecentByMealtime($('#recentSearchSupper').val(), 'addItemSupper_Recent_Results', 6)" id="recentSearchSupper">
        <option value="0"></option>
        <option value="1">Breakfast</option>
        <option value="2">Morning Tea</option>
        <option value="3">Lunch</option>
        <option value="4">Afternoon Tea</option>
        <option value="5">Dinner</option>
        <option value="6">Supper</option>
        <option value="-1">All Meals</option>

    </select>
    <div id="addItemSupper_Recent_Results" class="search_results_recent"></div>
    </div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;display:none; margin-bottom: 10px;padding-bottom: 10px" id="addItemSupper_AddFood_Select">
        <table>
        <tr style="font-weight: bold">
            <td>Food Name</td>
            <td style="padding-right: 10px">Brand Name</td>
            <td style="padding: 0px 25px">Amount</td>
            <td style="padding: 0px 21px">Serve</td>
            <td style="padding-left: 4px; padding-right: 20px;">Carbohydrate(g)</td> 
            <td>Protein(g)</td>
            <td style="padding-left: 38px ">Fat(g)</td>           
        </tr>
        <tr>
            <td><input type="text" id="AOFoodName_Supper" style="width: 180px" disabled/></td>
            <td style="padding-right: 10px"><input type="text" id="AOBrandName_Supper" placeholder="optional"  /></td>
            <td style="padding: 0px 25px"><input type="text" id="AOAmount_Supper" style="width: 48px"/></td>
            <td style="padding: 0px 21px"><input type="text" id="AOUnit_Supper" style="width: 50px" /></td>
            <td style="padding-left: 4px; padding-right: 20px; "><input type="text" id="AOCarbs_Supper" style="width: 50px;margin-left: 10px"/></td>
            <td><input type="text" id="AOPtn_Supper" style="width: 50px"/></td>
            <td style="padding-left: 25px "><input type="text" id="AOFat_Supper" style="width: 50px"/></td>
        </tr>
    </table>
    <i>Please note: Amount, Carbs, Protein and Fat entries are numerical only</i>
    <br/>
    <div style="float:left;cursor: pointer; display: inline" id="Div5" onclick="addOwnItem('6',$('#AOBrandName_Supper').val(),
                                                                    $('#AOFoodName_Supper').val(), $('#AOAmount_Supper').val(),
                                                                    $('#AOUnit_Supper').val(), $('#AOCarbs_Supper').val(),
                                                                    $('#AOPtn_Supper').val(),
                                                                    $('#AOFat_Supper').val())">
        <br/>
        <img src="/images/buttonSubmitOrange.gif" alt="Add Item" />
    </div>
    <div class="cancel" style="float:left;">
        <br />
        <img src="/images/buttonCancel.gif" alt="Cancel" />
    </div>
    <div style="clear:both;"></div>
</div>
</div>
<div style="border: 1px solid #d1d1d1; display: none; width: 883px; background-color: #ececec" id="addMealSupper_Select" class="one_of">
<div style="cursor: pointer; display: inline" id="addMealSupperSelected" onclick="hide('addMealSupper_Select');show('buttonsSupper');"><img src="/images/buttonAddMealSelected.gif" /></div>
<div class="additem_options">
    <div class="additem_option additem_option_selected" onclick="additem_option_selected(this);show('addMealSupper_Search_Select');hide('addMealSupper_Recent_Select');">Search</div>
    <div class="additem_option" onclick="additem_option_selected(this);hide('addMealSupper_Search_Select');show('addMealSupper_Recent_Select');">Recent</div>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;margin-bottom: 10px;padding-bottom: 10px" id="addMealSupper_Search_Select">
    <div id="tabSearchMealByKeyword_Supper">
        <div style="font-weight: bold;color: #008CA7;">Keyword</div>
        <input type="text" id="searchMealByKeyword_Supper" style="height: 23px; width: 345px; border: 1px solid #999999; margin-top: 11px;padding-left: 35px"
               onkeyup="searchMealsByAdvancedKeyword('Supper', 6);" />
        <div style="position: absolute; top: 45px; left: 23px; cursor: pointer;"
             onclick="searchMealsByAdvancedKeyword('Supper', 6);"><img src="/images/buttonSearchMag.gif" /></div>
 
        <div class="clear"></div>
 
        <div style="font-weight: bold;color: #008CA7;margin-top: 15px;">Macronutrients</div>
 
        <div style="font-weight: bold; padding-top: 12px;">
            Carbs:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByCarbs_Supper" onkeyup="searchMealsByAdvanced('Supper', 6);" /> 
            Protein:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByProtein_Supper" onkeyup="searchMealsByAdvanced('Supper', 6);" /> 
            Fat:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByFat_Supper" onkeyup="searchMealsByAdvanced('Supper', 6);"/>
        </div>
 
        <div style="font-weight: bold;color: #008CA7;margin-top: 15px;">Meal Classification</div>
 
        <div data-textbox="searchMealByKeyword_Supper" class="mealTagAdvancedSearch">
            <div><input id="gf_Supper" type="checkbox" name="mealtag" value="gf" onclick="searchMealsByAdvanced('Supper', 6);">Gluten Free<br></div>
            <div><input id="vegan_Supper" type="checkbox" name="mealtag" value="vegan" onclick="searchMealsByAdvanced('Supper', 6);">Vegan<br></div>
            <div><input id="veg_Supper" type="checkbox" name="mealtag" value="vegetarian" onclick="searchMealsByAdvanced('Supper', 6);">Vegetarian<br></div>
            <div><input id="lf_Supper" type="checkbox" name="mealtag" value="lf" onclick="searchMealsByAdvanced('Supper', 6);">Lactose Free<br></div>
            <div><input id="sf_Supper" type="checkbox" name="mealtag" value="sf" onclick="searchMealsByAdvanced('Supper', 6);">Seafood Free<br></div>
            <div><input id="nf_Supper" type="checkbox" name="mealtag" value="nf" onclick="searchMealsByAdvanced('Supper', 6);">Nut Free<br></div>
        </div>
 
        <div id="searchMealByKeyword_Supper_Result" class="search_results" style="display: none"></div>
    </div>


    <div id="tabSearchMealByMacronutrients_Supper" style="display: none">
        <span style="font-weight: bold">Search by Macronutrients</span>
        <a style="padding-left: 124px; font-weight: bold; color: #e27424" onclick="show('tabSearchMealByKeyword_Supper');hide('tabSearchMealByMacronutrients_Supper');">By Keyword ></a>
        <div style="font-weight: bold; padding-top: 12px;">Carbs:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByCarbs_Supper" /> Protein:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByProtein_Supper" /> Fat:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByFat_Supper" />
        <div style="display: inline-block; cursor: pointer; position: relative; left: -20px; top: 4px;"  onclick="getMealsByMacronutrients($('#searchMealByCarbs_Supper').val(),$('#searchMealByProtein_Supper').val(),$('#searchMealByFat_Supper').val(),'searchMealByMacronutrients_Supper_Result',6);"><img src="/images/buttonSearchMag.gif" /></div>
        </div>
        <div id="searchMealByMacronutrients_Supper_Result" class="search_results" style="display: none"></div>
    </div>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;display:none; margin-bottom: 10px;padding-bottom: 10px" id="addMealSupper_Recent_Select">
    <select onchange="getRecentMealsByMealtime($('#recentSearchMealSupper').val(), 'addMealSupper_Recent_Results',6)" id="recentSearchMealSupper">
        <option value="0"></option>
        <option value="1">Breakfast</option>
        <option value="2">Morning Tea</option>
        <option value="3">Lunch</option>
        <option value="4">Afternoon Tea</option>
        <option value="5">Dinner</option>
        <option value="6">Supper</option>
        <option value="-1">All Meals</option>

    </select>
    <div id="addMealSupper_Recent_Results" class="search_results_recent"></div>
    </div>
</div>
<div class="result" id="resultSupper" style="display: none;"></div>
</td></tr>


</table>
    <div class="clear">&nbsp;</div>
</div>
<% if (Request.QueryString["tab"] != "week")
   { %>
  <div id="eFoodDiaryTabWeek" style="padding-top: 55px; display: block; padding-left: 5px; padding-bottom: 5px;display: none">
  <% } else { %>
  <div id="eFoodDiaryTabWeek" style="padding-top: 55px; display: block; padding-left: 5px; padding-bottom: 5px;">
  <% } %>
    <div id="visionLogo" style="float:left;"></div>
    <table>
        <tr>
            <td>
                <p id="titleweekview" style="height: 15px;font-size: 18px; font-weight: bolder">Your Macronutrient Goals (g):</p><br/>
            </td>
            <td>
                <p id="totmacro" style="padding-bottom: 10px; font-size: 18px; font-weight: bolder"><asp:Literal id="literalMacros" runat="server"></asp:Literal></p>
            </td>
        </tr>
        <tr id="totmacroavetd" runat="server" ClientIDMode="Static">
            <td>
                <p style="padding-bottom: 10px; font-size: 15px;">Weekly Actual Average</p>
            </td>
            <td>
                <p id="totmacroave" style="padding-bottom: 10px; font-size: 15px;"><asp:Literal id="literalAveMacros" runat="server"></asp:Literal></p>
            </td>
        </tr>
        <tr id="accelDayForVVT" runat="server" Visible="False">
            <td>
                <p style="font-size: 15px;">Your accelerator day is </p>
            </td>
            <td>
                <div style="float: left;margin-top: 2px;">
                    <asp:DropDownList ID="AccelDropDownList1" runat="server" AutoPostBack="True" onselectedindexchanged="AccelDropDownList1_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div style="float: left">
                    <img src="/images/help.png" alt="What is an Accelerator Day" title="What is an Accelerator Day" style="cursor: hand;cursor: pointer" onclick="acceleratorDayInfo();" />
                </div>
            </td>
        </tr>
        <tr id="accelDayForVPT" runat="server" Visible="False">
            <td>
                <p style="font-size: 15px;">Your accelerator day is </p>
            </td>
            <td>
                <div id="accelBarContainer" style="float: left;margin-top: 2px;display: block;">
                     <asp:Literal ID="AccelDaysGridLiteral" runat="server"></asp:Literal>
                </div>
                <div style="float: left">
                    <img src="/images/help.png" alt="What is an Accelerator Day" title="What is an Accelerator Day" style="cursor: hand;cursor: pointer" onclick="acceleratorDayInfo();" />
                </div>
                
            </td>
        </tr>
    </table>
    
    <br/>
    <div id="weeklyFoodMotivQuote" runat="server"></div>
    
    <asp:Literal ID="FirstLoadNotifLiteral" runat="server"></asp:Literal>
    <br/>
    <br/>
    
    
<div id="captainIconWeek" runat="server" ClientIDMode="Static">
    <a title="this is the tip that captain will give it to ya" style="cursor: pointer; position: absolute; top: 230px; left: 40px;">
    <img src="/images/vpt_captainaccountabilityV2.jpg" alt="captain" /> </a>
</div>
<div class="foodDiaryControls" style="margin:-17px 0px 0px -22px;">
    <div id="imgWeekReset">
        <div class="fcimage">
            <img src="/images/icons/web/delete.png" style="cursor: pointer;" alt="Reset This Week"/>
        </div>
        <div class="fctext">
            Reset This Week
        </div>
    </div>
    <div onclick="popup_and_print()">
        <div class="fcimage">
            <img src="/images/icons/web/print.png" style="cursor: pointer" alt="Print" />
        </div>
        <div class="fctext">
            Print
        </div>
    </div>
    <div onclick="printShoppingListDateSelection();">
        <div class="fcimage">
            <img src="/images/icons/web/shoppingcart.png" style="cursor: pointer" alt="Shopping List" />
        </div>
        <div class="fctext">
            Shopping List
        </div>
    </div>
    <div style="width: 500px;height: 30px;padding-top:15px;">
    <div style="font-weight: bold; font-size: 14px;float: left; padding-top: 4px; padding-right: 10px">Week Beginning: </div>
    <div style="float: left;"></div>
    <div style="border: 1px solid #ffffff; padding: 1px;float: left; height: 21px; width: 260px" class="cal_controls">
        <asp:ImageButton runat="server" ID="buttonWeekNext"  
                onclick="buttonWeekNext_Click" ImageUrl="~/images/icons/web/nextarrow.png" style="float: right;height:25px;z-index: 9000;" />
        <asp:ImageButton runat="server" ID="buttonWeekPrev"
                onclick="buttonWeekPrev_Click" ImageUrl="~/images/icons/web/prevarrow.png" style="float: left;height:25px;z-index: 9000;" />
        <p style="display: inline-block; padding: 2px 14px;line-height:21px;text-align: center;"><asp:Literal runat="server" ID="literalWeek"></asp:Literal></p>
    </div>
    <div style="float: left;"></div>
    <div style="float: left; position: relative; top: -1px">
         <BDP:BasicDatePicker ID="bdpWeek" runat="server" DisplayType="Image" OnSelectionChanged="bdpWeek_SelectionChanged"
                AutoPostBack="True" 
                ShowNoneButton="False" 
                   ShowTodayButton="False" 
                    ButtonImageFileName="calendar.png" ButtonImageHeight="25" 
                    ButtonImageWidth="26" RenderImages="File" ResourcePath="/images/icons/web/" 
                    DownYearSelectorImageFileName="" NextMonthImageFileName="nextarrow.png" 
                    PrevMonthImageFileName="prevarrow.png" RenderStyleSheet="None" TextBoxStyle-BackColor="White" TextBoxStyle-BorderColor="#999999" TextBoxStyle-BorderStyle="Solid" TextBoxStyle-BorderWidth="1px" TextBoxStyle-Height="23px" TextBoxStyle-Width="123px">
        </BDP:BasicDatePicker>
    </div>
    </div>
</div>

<table width="884" border="0" cellpadding="0" cellspacing="0" style="padding-top:20px" id="foodDiaryWeek">
<!-- <tr>
<td width="44">&nbsp;</td>
<td width="120" class="foodDairyWeekHeader" data-day="1" id="startDateofTheWeek"><asp:Literal ID="literalDay1DateLink"></asp:Literal></td>
<td width="120" class="foodDairyWeekHeader" data-day="2"><asp:Literal ID="literalDay2DateLinks"></asp:Literal></td>
<td width="120" class="foodDairyWeekHeader" data-day="3"><asp:Literal ID="literalDay3DateLinks"></asp:Literal></td>
<td width="120" class="foodDairyWeekHeader" data-day="4"><asp:Literal ID="literalDay4DateLinks"></asp:Literal></td>
<td width="120" class="foodDairyWeekHeader" data-day="5"><asp:Literal ID="literalDay5DateLinks"></asp:Literal></td>
<td width="120" class="foodDairyWeekHeader" data-day="6"><asp:Literal ID="literalDay6DateLinks"></asp:Literal></td>
<td width="120" class="foodDairyWeekHeader" data-day="7"><asp:Literal ID="literalDay7DateLinks"></asp:Literal></td>
</tr> -->
<tr>
<td width="44">&nbsp;</td>
<asp:Literal runat="server" ID="literalDay1DateLink"></asp:Literal>
<asp:Literal runat="server" ID="literalDay2DateLink"></asp:Literal>
<asp:Literal runat="server" ID="literalDay3DateLink"></asp:Literal>
<asp:Literal runat="server" ID="literalDay4DateLink"></asp:Literal>
<asp:Literal runat="server" ID="literalDay5DateLink"></asp:Literal>
<asp:Literal runat="server" ID="literalDay6DateLink"></asp:Literal>
<asp:Literal runat="server" ID="literalDay7DateLink"></asp:Literal>
</tr>
<tr>
<td class="foodDairyWeekRowTitle" id="rowTitleMenu"><img src="/images/vertMenu.gif" /></td>
<asp:Literal runat="server" ID="literalDay1Menu"></asp:Literal>
<asp:Literal runat="server" ID="literalDay2Menu"></asp:Literal>
<asp:Literal runat="server" ID="literalDay3Menu"></asp:Literal>
<asp:Literal runat="server" ID="literalDay4Menu"></asp:Literal>
<asp:Literal runat="server" ID="literalDay5Menu"></asp:Literal>
<asp:Literal runat="server" ID="literalDay6Menu"></asp:Literal>
<asp:Literal runat="server" ID="literalDay7Menu"></asp:Literal>
</tr>
<tr> <!--new macro -->
<td class="foodDairyWeekRowTitle" id="Td1"><img src="/images/vertMacros.gif" /></td>
<asp:Literal runat="server" ID="literalDay1MacrosDiff"></asp:Literal>
<asp:Literal runat="server" ID="literalDay2MacrosDiff"></asp:Literal>
<asp:Literal runat="server" ID="literalDay3MacrosDiff"></asp:Literal>
<asp:Literal runat="server" ID="literalDay4MacrosDiff"></asp:Literal>
<asp:Literal runat="server" ID="literalDay5MacrosDiff"></asp:Literal>
<asp:Literal runat="server" ID="literalDay6MacrosDiff"></asp:Literal>
<asp:Literal runat="server" ID="literalDay7MacrosDiff"></asp:Literal>
</tr><!-- end new macro -->
<tr>
<td class="foodDairyWeekRowTitle" id="rowTitleBreakfast"><img src="/images/vertBreakfast.gif" /></td>
<asp:Literal runat="server" ID="literalDay1Breakfast"></asp:Literal>
<asp:Literal runat="server" ID="literalDay2Breakfast"></asp:Literal>
<asp:Literal runat="server" ID="literalDay3Breakfast"></asp:Literal>
<asp:Literal runat="server" ID="literalDay4Breakfast"></asp:Literal>
<asp:Literal runat="server" ID="literalDay5Breakfast"></asp:Literal>
<asp:Literal runat="server" ID="literalDay6Breakfast"></asp:Literal>
<asp:Literal runat="server" ID="literalDay7Breakfast"></asp:Literal>
</tr>
<tr>
<td class="foodDairyWeekRowTitle" id="rowTitleMorningTea"><img src="/images/vertMorningTea.gif" /></td>
<asp:Literal runat="server" ID="literalDay1MorningTea"></asp:Literal>
<asp:Literal runat="server" ID="literalDay2MorningTea"></asp:Literal>
<asp:Literal runat="server" ID="literalDay3MorningTea"></asp:Literal>
<asp:Literal runat="server" ID="literalDay4MorningTea"></asp:Literal>
<asp:Literal runat="server" ID="literalDay5MorningTea"></asp:Literal>
<asp:Literal runat="server" ID="literalDay6MorningTea"></asp:Literal>
<asp:Literal runat="server" ID="literalDay7MorningTea"></asp:Literal>
</tr>
<tr>
<td class="foodDairyWeekRowTitle" id="rowTitleLunch"><img src="/images/vertLunch.gif" /></td>
<asp:Literal runat="server" ID="literalDay1Lunch"></asp:Literal>
<asp:Literal runat="server" ID="literalDay2Lunch"></asp:Literal>
<asp:Literal runat="server" ID="literalDay3Lunch"></asp:Literal>
<asp:Literal runat="server" ID="literalDay4Lunch"></asp:Literal>
<asp:Literal runat="server" ID="literalDay5Lunch"></asp:Literal>
<asp:Literal runat="server" ID="literalDay6Lunch"></asp:Literal>
<asp:Literal runat="server" ID="literalDay7Lunch"></asp:Literal>
</tr>
<tr>
<td class="foodDairyWeekRowTitle" id="rowTitleAfternoonTea"><img src="/images/vertAfternoonTea.gif" /></td>
<asp:Literal runat="server" ID="literalDay1AfternoonTea"></asp:Literal>
<asp:Literal runat="server" ID="literalDay2AfternoonTea"></asp:Literal>
<asp:Literal runat="server" ID="literalDay3AfternoonTea"></asp:Literal>
<asp:Literal runat="server" ID="literalDay4AfternoonTea"></asp:Literal>
<asp:Literal runat="server" ID="literalDay5AfternoonTea"></asp:Literal>
<asp:Literal runat="server" ID="literalDay6AfternoonTea"></asp:Literal>
<asp:Literal runat="server" ID="literalDay7AfternoonTea"></asp:Literal>
</tr>
<tr>
<td class="foodDairyWeekRowTitle" id="rowTitleDinner"><img src="/images/vertDinner.gif" /></td>
<asp:Literal runat="server" ID="literalDay1Dinner"></asp:Literal>
<asp:Literal runat="server" ID="literalDay2Dinner"></asp:Literal>
<asp:Literal runat="server" ID="literalDay3Dinner"></asp:Literal>
<asp:Literal runat="server" ID="literalDay4Dinner"></asp:Literal>
<asp:Literal runat="server" ID="literalDay5Dinner"></asp:Literal>
<asp:Literal runat="server" ID="literalDay6Dinner"></asp:Literal>
<asp:Literal runat="server" ID="literalDay7Dinner"></asp:Literal>
</tr>
<tr>
<td class="foodDairyWeekRowTitle" id="rowTitleSupper"><img src="/images/vertSupper.gif" /></td>
<asp:Literal runat="server" ID="literalDay1Supper"></asp:Literal>
<asp:Literal runat="server" ID="literalDay2Supper"></asp:Literal>
<asp:Literal runat="server" ID="literalDay3Supper"></asp:Literal>
<asp:Literal runat="server" ID="literalDay4Supper"></asp:Literal>
<asp:Literal runat="server" ID="literalDay5Supper"></asp:Literal>
<asp:Literal runat="server" ID="literalDay6Supper"></asp:Literal>
<asp:Literal runat="server" ID="literalDay7Supper"></asp:Literal>
</tr>

<tr>
<td class="foodDairyWeekRowTitle" id="rowTitleMacros"><img src="/images/vertMacros.gif" /></td>
<asp:Literal runat="server" ID="literalDay1Macros"></asp:Literal>
<asp:Literal runat="server" ID="literalDay2Macros"></asp:Literal>
<asp:Literal runat="server" ID="literalDay3Macros"></asp:Literal>
<asp:Literal runat="server" ID="literalDay4Macros"></asp:Literal>
<asp:Literal runat="server" ID="literalDay5Macros"></asp:Literal>
<asp:Literal runat="server" ID="literalDay6Macros"></asp:Literal>
<asp:Literal runat="server" ID="literalDay7Macros"></asp:Literal>
</tr>

</table>
    <div class="clear">&nbsp;</div>
    </div>
  </div><!-- /eContent -->
  <div class="clear">&nbsp;</div>
</div><!-- /eFoodDiary -->

<asp:Literal ID="foodDiaryScript" runat="server"></asp:Literal>