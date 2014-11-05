<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Menus.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.VVT.Menus" %>
<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls"
    TagPrefix="BDP" %>

<div id="eMenu" class="element">
    <div class="replace" id="menuTab" runat="server">
            <!-- <div id="tabVisionMenus1" style="cursor: pointer; position: absolute; top: 0px; left: 0px; width: 201px; height: 45px" onclick="document.location = '/club-vision/Redirecting?page=/club-vision/my-eating/menus/?tab=visionsmenus';">Vision's Daily Plans</div>
            <div id="tabMyMenus1" style="cursor: pointer; position: absolute; top: 0px; left: 262px; width: 201px; height: 45px" onclick="document.location = '/club-vision/Redirecting?page=/club-vision/my-eating/menus/?tab=mymenus';">My Daily Plans</div>
            <div id="tabVisionMeals1" style="cursor: pointer; position: absolute; top: 0px; left: 464px; width: 201px; height: 45px" onclick="document.location = '/club-vision/Redirecting?page=/club-vision/my-eating/menus/?tab=visionsmeals';">Vision's Meals</div>
            <div id="tabMyMeals1" style="cursor: pointer; position: absolute; top: 0px; left: 666px; width: 201px; height: 45px" onclick="document.location = '/club-vision/Redirecting?page=/club-vision/my-eating/menus/?tab=mymeals';">My Meals</div> -->
            
            <div id="tabVisionMenus" onclick="document.location = '/club-vision/Redirecting?page=/club-vision/my-eating/menus/?tab=visionsmenus';" runat="server" clientidmode="Static">Vision's Daily Plans</div>
            <div id="tabMyMenus" onclick="document.location = '/club-vision/Redirecting?page=/club-vision/my-eating/menus/?tab=mymenus';" runat="server" clientidmode="Static">My Daily Plans</div>
            <div id="tabVisionMeals" onclick="document.location = '/club-vision/Redirecting?page=/club-vision/my-eating/menus/?tab=visionsmeals';" runat="server" clientidmode="Static">Vision's Meals</div>
            <div id="tabMyMeals" onclick="document.location = '/club-vision/Redirecting?page=/club-vision/my-eating/menus/?tab=mymeals';" runat="server" clientidmode="Static">My Meals</div>
            <div style="border-left: none; border-top: none;border-right: none;cursor: default;width: 100px;height:46px;background: none;"></div>
        </div>

    <div class="eContent" id="tabMenus" runat="server">
        
    <div id="captainIcon" style="display: none;" >
        <a title="this is the tip that captain will give it to ya" style="cursor: pointer; position: absolute; top: 230px; left: 40px;">
        <img src="/images/vpt_captainaccountabilityV2.jpg" alt="captain" /> </a>
    </div> 

    <p id="pVisionDailyPlans" runat="server" visible="false">
        Vision Personal Training has provided daily plans for you to use as a guide to create your own plans. Substitute foods items to bring the macronutrient total as close as possible to your goals, and then save them as your own Daily Plan.
    </p>
    <p id="pMyDailyPlans" runat="server" visible="false">
        Quickly create and edit your daily and weekly plans that will help you achieve your macronutrient goals.
    </p>
    <p id="pVisionMeals" runat="server" visible="false">
        Vision has created a wide range of nutritious meal options that will assist you in creating your nutrition plans.  All meals viewed on Vision TV can be found here.
    </p>
    <p id="pMyMeals" runat="server" visible="false">
        This section allows you to view, edit and rename all your saved meals.
    </p>
    <h3>Your Macronutrient Goals (g):</h3>
    <p><asp:Literal id="literalMacros" runat="server"></asp:Literal></p>
    
        <div id="pVisionDailyPlansSearch" runat="server" visible="false"> 
       <div id="Div1" style="height: 30px; display: block;margin-top: -30px;">
            <% if (Request.QueryString["showall"] == "true")
           { %>
           <img src="/images/buttonSearchOrange.gif" style="float: right;margin-bottom: 20px;text-align: right; padding-right: 11px; position: relative;cursor: pointer;" alt="Search Vision Menu" onclick="slideToggle('divVisionDailyPlansSearch');"  />
            <% }
           else
           { %>
           <img src="/images/buttonSearchOrange.gif" style="float: right;margin-bottom: 20px;text-align: right; padding-right: 11px; position: relative;cursor: pointer;" alt="Search Vision Menu" onclick="slideToggle('divVisionDailyPlansSearchDB');"  />
            <% } %>
            <img src="/images/buttonShowAllMenus.gif" style="float: right;margin-bottom: 20px;text-align: right; padding-right: 11px; position: relative;cursor: pointer;" alt="Search Vision Menu" onclick="document.location = '/club-vision/Redirecting?page=/club-vision/my-eating/menus/?tab=visionsmenus&showall=true';" />
        </div>
        <div style="padding-top: 10px;border: 1px solid #d1d1d1; display: none; width: 857px;margin-left: 14px; margin-top: 20px; background-color: #ececec" id="divVisionDailyPlansSearch">
            
            <div class="additem_options" style="width: 820px;">
                <div class="additem_option additem_option_selected" onclick="additem_option_selected(this);hide('VisionDailyPlansSearchTextSearchByMacro');show('VisionDailyPlansSearchTextSearchByKeyword');">Search By Keyword</div>
                <div class="additem_option" onclick="additem_option_selected(this);show('VisionDailyPlansSearchTextSearchByMacro');hide('VisionDailyPlansSearchTextSearchByKeyword');">Search By Macro</div>
            </div>

            <div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 800px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;margin-bottom: 10px;padding-bottom: 10px" id="VisionDailyPlansSearchTextSearchByKeyword">
            
                <input id="VisionDailyPlansSearchTextSearch" type="text" style="padding-left: 30px; background: url(/images/buttonSearchField.gif) no-repeat scroll 2px 2px transparent; border: 1px solid #E5E5E5; height: 23px; width: 305px;" />
                <input type="button" value="Search" onclick="document.location.href = '/club-vision/Redirecting?page=/club-vision/my-eating/menus/?tab=visionsmenus&showall=true&carb=-1&keyword=' + $('#VisionDailyPlansSearchTextSearch').val() + '';"/>
                <!--<a onclick="$('#VisionDailyPlansSearchTextSearch').val('');filterMenuByKeyword('');" style="margin-left: -40px;cursor: pointer;">clear</a> -->
            
            </div>

            <div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 800px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;display:none; margin-bottom: 10px;padding-bottom: 10px" id="VisionDailyPlansSearchTextSearchByMacro">
                <!-- 
                 Carbs : <input id="pVisionDailyPlansSearchTextCarb" type="text" onkeyup="filterMenuByMacro($('#pVisionDailyPlansSearchTextCarb').val(),$('#pVisionDailyPlansSearchTextPtn').val(),$('#pVisionDailyPlansSearchTextFat').val());" style="border: 1px solid #E5E5E5; height: 23px; width: 40px;" /> &nbsp; &nbsp;
                 Protein : <input id="pVisionDailyPlansSearchTextPtn" type="text" onkeyup="filterMenuByMacro($('#pVisionDailyPlansSearchTextCarb').val(),$('#pVisionDailyPlansSearchTextPtn').val(),$('#pVisionDailyPlansSearchTextFat').val());" style="border: 1px solid #E5E5E5; height: 23px; width: 40px;" /> &nbsp; &nbsp;
                 Fat : <input id="pVisionDailyPlansSearchTextFat" type="text" onkeyup="filterMenuByMacro($('#pVisionDailyPlansSearchTextCarb').val(),$('#pVisionDailyPlansSearchTextPtn').val(),$('#pVisionDailyPlansSearchTextFat').val());" style="border: 1px solid #E5E5E5; height: 23px; width: 40px;" />&nbsp; &nbsp;
                 -->
                 Carbs : <input id="pVisionDailyPlansSearchTextCarb" type="text" style="border: 1px solid #E5E5E5; height: 23px; width: 40px;" /> &nbsp; &nbsp;
                 Protein : <input id="pVisionDailyPlansSearchTextPtn" type="text" style="border: 1px solid #E5E5E5; height: 23px; width: 40px;" /> &nbsp; &nbsp;
                 Fat : <input id="pVisionDailyPlansSearchTextFat" type="text" style="border: 1px solid #E5E5E5; height: 23px; width: 40px;" />&nbsp; &nbsp;
                 
                <input type="button" onclick="document.location.href = '/club-vision/Redirecting?page=/club-vision/my-eating/menus/?tab=visionsmenus&showall=true&carb=' + $('#pVisionDailyPlansSearchTextCarb').val() + '&protein=' + $('#pVisionDailyPlansSearchTextPtn').val() + '&fat=' + $('#pVisionDailyPlansSearchTextFat').val();" value="Search"/>
                
                 
                 <!-- <a onclick="$('#pVisionDailyPlansSearchTextCarb').val('');$('#pVisionDailyPlansSearchTextPtn').val('');$('#pVisionDailyPlansSearchTextFat').val('');filterMenuByMacro('','','');" style="cursor: pointer;">clear</a> -->

            </div>
        </div>
         <div style="padding-top: 10px;border: 1px solid #d1d1d1; display: none; width: 857px;margin-left: 14px; margin-top: 20px; background-color: #ececec" id="divVisionDailyPlansSearchDB">
            
            <div class="additem_options" style="width: 820px;">
                <div class="additem_option additem_option_selected" onclick="additem_option_selected(this);hide('Div6');show('Div5');">Search By Keyword</div>
                <div class="additem_option" onclick="additem_option_selected(this);show('Div6');hide('Div5');">Search By Macro</div>
            </div>

            <div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 800px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;margin-bottom: 10px;padding-bottom: 10px" id="Div5">
            
                <input id="VisionDailyPlansSearchTextSearchDB" type="text" style="padding-left: 30px; background: url(/images/buttonSearchField.gif) no-repeat scroll 2px 2px transparent; border: 1px solid #E5E5E5; height: 23px; width: 305px;" />
                <!--<a onclick="document.location = '/club-vision/Redirecting?page=/club-vision/my-eating/menus/?tab=visionsmenus&showall=false&carb=-1&keyword=' + $('#VisionDailyPlansSearchTextSearchDB').val() + '';" style="margin-left: -40px;cursor: pointer;">Search</a>-->
                <input type="button" value="Search" onclick="document.location.href = '/club-vision/Redirecting?page=/club-vision/my-eating/menus/?tab=visionsmenus&showall=false&carb=-1&keyword=' + $('#VisionDailyPlansSearchTextSearchDB').val() + '';"/>
               
            </div>

            <div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 800px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;display:none; margin-bottom: 10px;padding-bottom: 10px" id="Div6">
                 Carbs : <input id="carbdb" type="text"  style="border: 1px solid #E5E5E5; height: 23px; width: 40px;" /> &nbsp; &nbsp;
                 Protein : <input id="ptndb" type="text" style="border: 1px solid #E5E5E5; height: 23px; width: 40px;" /> &nbsp; &nbsp;
                 Fat : <input id="fatdb" type="text" style="border: 1px solid #E5E5E5; height: 23px; width: 40px;" />&nbsp; &nbsp;
                 
                 <!-- <a onclick="document.location = '/club-vision/Redirecting?page=/club-vision/my-eating/menus/?tab=visionsmenus&showall=false&carb=' + $('#carbdb').val() + '&protein=' + $('#ptndb').val() + '&fat=' + $('#fatdb').val();" style="cursor: pointer;">Search</a> -->
                 <input type="button" onclick="document.location.href = '/club-vision/Redirecting?page=/club-vision/Redirecting?page=/club-vision/my-eating/menus/?tab=visionsmenus&showall=false&carb=' + $('#carbdb').val() + '&protein=' + $('#ptndb').val() + '&fat=' + $('#fatdb').val();" value="Search"/>
                
            </div>
        </div>
    </div>
    <div id="pMyDailyPlansSearch" runat="server" visible="false">
        <div id="buttonMyDailyPlansSearch" style="height: 30px; display: block; margin-top: -30px;">
            <img src="/images/buttonSearchOrange.gif" style="float: right;margin-bottom: 20px;text-align: right; padding-right: 11px; position: relative;cursor: pointer;" alt="Search My Daily Plan" onclick="slideToggle('pMyDailyPlansSearchDiv');"  />
            <img src="/images/buttonMakeMyDailyPlan.gif" style="float: right; text-align: right; padding-right: 11px; position: relative;cursor: pointer;" alt="Make My Daily Plan" onclick="document.location='/club-vision/my-eating/menus/?tab=new'" />
            
        </div>
        <div style="padding-top: 10px;border: 1px solid #d1d1d1; display: none; width: 857px;margin-left: 14px; margin-top: 20px; background-color: #ececec" id="pMyDailyPlansSearchDiv">
            
            <div class="additem_options" style="width: 820px;">
                <div class="additem_option additem_option_selected" onclick="additem_option_selected(this);show('pMyMenusSearchSearchByKeyword');hide('pMyMenusSearchSearchByMacro');">Search By Keyword</div>
                <div class="additem_option" onclick="additem_option_selected(this);hide('pMyMenusSearchSearchByKeyword');show('pMyMenusSearchSearchByMacro');">Search By Macro</div>
            </div>

            <div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 800px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;margin-bottom: 10px;padding-bottom: 10px" id="pMyMenusSearchSearchByKeyword">
            
                <input id="pMyDailyPlansSearchText" type="text" style="padding-left: 30px; background: url(/images/buttonSearchField.gif) no-repeat scroll 2px 2px transparent; border: 1px solid #E5E5E5; height: 23px; width: 305px;" />
                <!-- <a onclick="$('#pMyDailyPlansSearchText').val('');filterMenuByKeyword('');"  style="margin-left: -40px;cursor: pointer">clear</a> -->
                <input type="button" value="Search" onclick="document.location.href = '/club-vision/Redirecting?page=/club-vision/my-eating/menus/?tab=mymenus&showall=false&carb=-1&keyword=' + $('#pMyDailyPlansSearchText').val() + '';"/>
               
            </div>

            <div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 800px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;display:none; margin-bottom: 10px;padding-bottom: 10px" id="pMyMenusSearchSearchByMacro">
                Carbs : <input id="pMyDailyPlansSearchTextCarb" type="text" style="border: 1px solid #E5E5E5; height: 23px; width: 40px;" /> &nbsp; &nbsp;
                Protein : <input id="pMyDailyPlansSearchTextPtn" type="text" style="border: 1px solid #E5E5E5; height: 23px; width: 40px;" /> &nbsp; &nbsp;
                Fat : <input id="pMyDailyPlansSearchTextFat" type="text" style="border: 1px solid #E5E5E5; height: 23px; width: 40px;" />&nbsp; &nbsp;
                
                <!-- <a onclick="$('#pMyDailyPlansSearchTextCarb').val('');$('#pMyDailyPlansSearchTextPtn').val('');$('#pMyDailyPlansSearchTextFat').val('');filterMenuByMacro('','','');" style="cursor: pointer;">clear</a> -->
                <input type="button" onclick="document.location.href = '/club-vision/Redirecting?page=/club-vision/Redirecting?page=/club-vision/my-eating/menus/?tab=mymenus&showall=false&carb=' + $('#pMyDailyPlansSearchTextCarb').val() + '&protein=' + $('#pMyDailyPlansSearchTextPtn').val() + '&fat=' + $('#pMyDailyPlansSearchTextFat').val();" value="Search"/>
                
                </div>
        </div>
    </div>
    <div id="pVisionMealsSearch" runat="server" visible="false">
        <div id="Div2" style="height: 30px; display: block;margin-top: -30px;">
            <img src="/images/buttonSearchOrange.gif" style="float: right;margin-bottom: 20px;text-align: right; padding-right: 11px; position: relative;cursor: pointer;" alt="Search Vision Meal" onclick="slideToggle('pVisionMealsSearchDiv');"  />
        </div>
        <div  id="pVisionMealsSearchDiv" style="padding-top: 10px;border: 1px solid #d1d1d1; display: none; width: 857px;margin-left: 14px; margin-top: 20px; background-color: #ececec">
            
            <div class="additem_options" style="width: 820px;display: none;">
                <div class="additem_option additem_option_selected" onclick="additem_option_selected(this);hide('pVisionMealsSearchSearchByMacro');show('pVisionMealsSearchSearchByKeyword');">Search By Keyword</div>
                <div class="additem_option" onclick="additem_option_selected(this);show('pVisionMealsSearchSearchByMacro');hide('pVisionMealsSearchSearchByKeyword');">Search By Macro</div>
            </div>

            <div style="border-left: 1px solid #cccccc;border-top: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 800px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;margin-bottom: 10px;padding-bottom: 10px" id="pVisionMealsSearchSearchByKeyword">
            
                <div style="font-weight: bold;color: #008CA7;margin-top: 5px;">Keyword</div>

                <input id="pVisionMealsSearchTextSearch" type="text" style="padding-left: 30px; background: url(/images/buttonSearchField.gif) no-repeat scroll 2px 2px transparent; border: 1px solid #E5E5E5; height: 23px; width: 305px;" />
                <br/>

                <div style="font-weight: bold;color: #008CA7;margin-top: 15px;">Macronutrients</div>

                Carbs : <input id="pVisionMealsSearchTextCarb" type="text" style="border: 1px solid #E5E5E5; height: 23px; width: 40px;" /> &nbsp; &nbsp;
                Protein : <input id="pVisionMealsSearchTextPtn" type="text" style="border: 1px solid #E5E5E5; height: 23px; width: 40px;" /> &nbsp; &nbsp;
                Fat : <input id="pVisionMealsSearchTextFat" type="text" style="border: 1px solid #E5E5E5; height: 23px; width: 40px;" />&nbsp; &nbsp;

                <div style="font-weight: bold;color: #008CA7;margin-top: 15px;">Meal Classification</div>
 
                <div data-textbox="searchMealByKeyword_Supper" class="mealTagAdvancedSearch">
                    <div><input id="gf_visionsMeal" type="checkbox" name="mealtag" value="gf">Gluten Free<br></div>
                    <div><input id="vegan_visionsMeal" type="checkbox" name="mealtag" value="vegan">Vegan<br></div>
                    <div><input id="veg_visionsMeal" type="checkbox" name="mealtag" value="vegetarian">Vegetarian<br></div>
                    <div><input id="lf_visionsMeal" type="checkbox" name="mealtag" value="lf">Lactose Free<br></div>
                    <div><input id="sf_visionsMeal" type="checkbox" name="mealtag" value="sf">Seafood Free<br></div>
                    <div><input id="nf_visionsMeal" type="checkbox" name="mealtag" value="nf">Nut Free<br></div>
                </div>
                <br/>
                <!-- <a onclick="$('#pVisionMealsSearchTextSearch').val('');filterMenuByKeyword('');" style="cursor: pointer;margin-left: -40px;">clear</a> 
                <button onclick="displayMealSetSearch($('#pVisionMealsSearchTextSearch').val()); return  false;">Search</button> 
                <button onclick="$('#pVisionMealsSearchTextSearch').val('');clearDisplayMealSetSearch(); return  false;">Clear</button> -->
                <button onclick="displayMealSetSearch($('#pVisionMealsSearchTextSearch').val(), $('#pVisionMealsSearchTextCarb').val(), $('#pVisionMealsSearchTextPtn').val(), $('#pVisionMealsSearchTextFat').val(), 
                                                      $('#gf_visionsMeal').is(':checked'),$('#vegan_visionsMeal').is(':checked'),$('#veg_visionsMeal').is(':checked'),
                                                      $('#lf_visionsMeal').is(':checked'),$('#sf_visionsMeal').is(':checked'),$('#nf_visionsMeal').is(':checked')); return  false;">Search</button> 
                <button onclick="$('#pVisionMealsSearchTextSearch').val('');clearDisplayMealSetSearch(); return  false;">Clear</button> 
            
            </div>

            <div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 800px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;display:none; margin-bottom: 10px;padding-bottom: 10px" id="pVisionMealsSearchSearchByMacro">
               <!-- Carbs : <input id="pVisionMealsSearchTextCarb" type="text" onkeyup="filterMealByMacro($('#pVisionMealsSearchTextCarb').val(),$('#pVisionMealsSearchTextPtn').val(),$('#pVisionMealsSearchTextFat').val());" style="border: 1px solid #E5E5E5; height: 23px; width: 40px;" /> &nbsp; &nbsp;
                Protein : <input id="pVisionMealsSearchTextPtn" type="text" onkeyup="filterMealByMacro($('#pVisionMealsSearchTextCarb').val(),$('#pVisionMealsSearchTextPtn').val(),$('#pVisionMealsSearchTextFat').val());" style="border: 1px solid #E5E5E5; height: 23px; width: 40px;" /> &nbsp; &nbsp;
                Fat : <input id="pVisionMealsSearchTextFat" type="text" onkeyup="filterMealByMacro($('#pVisionMealsSearchTextCarb').val(),$('#pVisionMealsSearchTextPtn').val(),$('#pVisionMealsSearchTextFat').val());" style="border: 1px solid #E5E5E5; height: 23px; width: 40px;" />&nbsp; &nbsp;
                
                <a onclick="$('#pVisionMealsSearchTextCarb').val('');$('#pVisionMealsSearchTextPtn').val('');$('#pVisionMealsSearchTextFat').val('');filterMealByMacro('','','');" style="cursor: pointer;">clear</a>
                -->
                
                

                <button onclick="displayMealSetSearchByMacro($('#pVisionMealsSearchTextCarb').val(),$('#pVisionMealsSearchTextPtn').val(),$('#pVisionMealsSearchTextFat').val()); return  false;">Search</button> 
                <button onclick="$('#pVisionMealsSearchTextCarb').val('');$('#pVisionMealsSearchTextPtn').val('');$('#pVisionMealsSearchTextFat').val('');clearDisplayMealSetSearch(); return  false;">Clear</button> 

    
            </div>
        </div>
    </div>
    <div id="pMyMealsSearch" runat="server" visible="false">
        <div id="Div3" style="height: 30px; display: block;margin-top: -30px;">
            <img src="/images/buttonSearchOrange.gif" style="float: right;margin-bottom: 20px;text-align: right; padding-right: 11px; position: relative;cursor: pointer;" alt="Search My Meal" onclick="slideToggle('pMyMealsSearchDiv');"  />
            <img src="/images/buttonMakeMyMeal.gif" style="float: right; text-align: right; padding-right: 11px; position: relative;cursor: pointer;" alt="Make My Meal"  onclick="document.location='/club-vision/my-eating/menus/?tab=new_meal'" />
            
        </div>
        <div style="padding-top: 10px;border: 1px solid #d1d1d1; display: none; width: 857px;margin-left: 14px; margin-top: 20px; background-color: #ececec" id="pMyMealsSearchDiv">
            
            <div class="additem_options" style="width: 820px;">
                <div class="additem_option additem_option_selected" onclick="additem_option_selected(this);show('pMyMealsSearchSearchByKeyword');hide('pMyMealsSearchSearchByMacro');">Search By Keyword</div>
                <div class="additem_option" onclick="additem_option_selected(this);hide('pMyMealsSearchSearchByKeyword');show('pMyMealsSearchSearchByMacro');">Search By Macro</div>
            </div>

            <div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 800px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;margin-bottom: 10px;padding-bottom: 10px" id="pMyMealsSearchSearchByKeyword">
            
                <input id="pMyMealsSearchText" type="text" style="padding-left: 30px; background: url(/images/buttonSearchField.gif) no-repeat scroll 2px 2px transparent; border: 1px solid #E5E5E5; height: 23px; width: 305px;" />
                
                <button onclick="displayMealSetSearch($('#pMyMealsSearchText').val()); return  false;">Search</button> 
                <button onclick="$('#pMyMealsSearchText').val('');clearDisplayMealSetSearch(); return  false;">Clear</button> 
            
            </div>

            <div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 800px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;display:none; margin-bottom: 10px;padding-bottom: 10px" id="pMyMealsSearchSearchByMacro">
                <!--
                Carbs : <input id="pMyMealsSearchTextCarb" type="text" onkeyup="filterMealByMacro($('#pMyMealsSearchTextCarb').val(),$('#pMyMealsSearchTextPtn').val(),$('#pMyMealsSearchTextFat').val());" style="border: 1px solid #E5E5E5; height: 23px; width: 40px;" /> &nbsp; &nbsp;
                Protein : <input id="pMyMealsSearchTextPtn" type="text" onkeyup="filterMealByMacro($('#pMyMealsSearchTextCarb').val(),$('#pMyMealsSearchTextPtn').val(),$('#pMyMealsSearchTextFat').val());" style="border: 1px solid #E5E5E5; height: 23px; width: 40px;" /> &nbsp; &nbsp;
                Fat : <input id="pMyMealsSearchTextFat" type="text" onkeyup="filterMealByMacro($('#pMyMealsSearchTextCarb').val(),$('#pMyMealsSearchTextPtn').val(),$('#pMyMealsSearchTextFat').val());" style="border: 1px solid #E5E5E5; height: 23px; width: 40px;" />&nbsp; &nbsp;
                
                <a onclick="$('#pMyMealsSearchTextCarb').val('');$('#pMyMealsSearchTextPtn').val('');$('#pMyMealsSearchTextFat').val('');filterMealByMacro('','','');" style="cursor: pointer;">clear</a> -->

                Carbs : <input id="pMyMealsSearchTextCarb" type="text" style="border: 1px solid #E5E5E5; height: 23px; width: 40px;" /> &nbsp; &nbsp;
                Protein : <input id="pMyMealsSearchTextPtn" type="text" style="border: 1px solid #E5E5E5; height: 23px; width: 40px;" /> &nbsp; &nbsp;
                Fat : <input id="pMyMealsSearchTextFat" type="text" style="border: 1px solid #E5E5E5; height: 23px; width: 40px;" />&nbsp; &nbsp;
                
                <button onclick="displayMealSetSearchByMacro($('#pMyMealsSearchTextCarb').val(),$('#pMyMealsSearchTextPtn').val(),$('#pMyMealsSearchTextFat').val()); return  false;">Search</button> 
                <button onclick="$('#pMyMealsSearchTextCarb').val('');$('#pMyMealsSearchTextPtn').val('');$('#pMyMealsSearchTextFat').val('');clearDisplayMealSetSearch(); return  false;">Clear</button> 

            </div>
        </div>
    </div>
    <div id="menus">
          <a id="scroller-prev"><img src="/images/buttonPrev.png" alt="Prev" /></a>
          <a id="scroller-next" data-skip="0" data-stop="NO"><img src="/images/buttonNext.png" alt="Next" /></a>
        <asp:Literal id="literalMenus" runat="server"></asp:Literal>
    </div>
    <div style="clear: both"></div>
    </div>

    <div class="eContent eOrange" id="tabEdit" runat="server"> 
<input type="hidden" id="currentMenuId" runat="server" value="" />

<!-- Start modification for new header -->

<div class="mealMenuHeaderDiv" style="height: 144px;">
    <div id="Div11" runat="server" class="mealMenuHeaderLefttSide" style="height: 144px;">
        <img id="menuImage" alt="Meal Image" runat="server" src="/images/menus/menu_generic.jpg" width="144" height="144" />
    </div>
    <div id="Div13" class="mealMenuHeaderRightSide" style="height: 144px;">
        <div id="menuNameDiv" runat="server" class="mealOrMenuTitle">Meal Name: </div>
        <div id="Div15" class="mealOrMenuDetail" style="width: 660px;">
            <div id="Div17">
                <img src="/images/icons/web/tag.png" />
                Macronutrient Total (grams)
            </div>
            <div>
                <img src="/images/icons/web/tag.png" style="opacity: 0.0;" />
                Carbs:<span id="MenuTotSpanCho" runat="server" clientidmode="Static">15</span>
                Protein:<span id="MenuTotSpanPtn" runat="server" clientidmode="Static">43</span>
                Fat:<span id="MenuTotSpanFat" runat="server" clientidmode="Static">12</span>
            </div>
        </div>
    </div>
</div>
<div class="mealMenuOptionsDiv">
    <div class="mealMenuOptionsWrapper">
        <div id="menuAddPictureDiv" runat="server" style="border-left: none;" onclick="openMealMenuDialog('menuImageUploadDiv', this.id); return false;">
            <div><img src="/images/icons/web/addphoto.png" alt="picture"/></div>
            <div>Add Picture</div>
        </div>
        <div ID="menuShareDiv" runat="server" onclick="openMealMenuDialog('shareDailyPlan_Select', this.id); return false;">
            <div><img src="/images/icons/web/closedenvelope.png" alt="picture"/></div>
            <div>Share</div>
        </div>
        <div onclick="popup_and_printURL('/club-vision/my-eating/menus/?tab=view&menuId=' + $('#ContentPlaceHolderDefault_ContentPlaceHolderMain_Menus_2_currentMenuId').val()); return false;">
            <div><img src="/images/icons/web/print.png" alt="picture"/></div>
            <div>Print</div>
        </div>
        <asp:Literal runat="server" ID="literalMenuAddToDiary"></asp:Literal>
        <asp:Literal runat="server" ID="literalCopyToMyMenu"></asp:Literal>
    </div>
</div>
<div class="menuChosenDiv">
    <div id="shareDailyPlan_Select" style="display: none;" runat="server" clientidmode="Static" class="one_of">
        <p>Share to?</p>
        <div class="quicktools_option" style="height:25px;text-align:left;vertical-align:middle;" id="menu_shareToFB" runat="server"><img src="/images/fb.small.icon.jpg"/> &nbsp;&nbsp;&nbsp;Facebook</div>
        <div class="quicktools_option" style="height:25px;text-align:left;vertical-align:middle;" id="menu_shareToTwitter" runat="server"><img src="/images/twitter-icon.png"/> &nbsp;&nbsp;&nbsp;Twitter</div>
        <div class="quicktools_option" style="height:25px;text-align:left;vertical-align:middle;" id="menu_shareToTrainerOrClient" runat="server"><img src="/images/trainer.small.icon.jpg"/>&nbsp;&nbsp;&nbsp;My Trainer</div>
        <div class="quicktools_option" style="height:25px;text-align:left;vertical-align:middle;" id="menu_shareToHQ" runat="server"><img src="/images/favicon002.png" height="21" width="21"/>&nbsp;&nbsp;&nbsp;Vision HQ</div>
        <div class="quicktools_option" style="height:25px;text-align:left;vertical-align:middle;" id="menu_shareToEmail" runat="server"><img src="/images/email_icon.png"/>&nbsp;&nbsp;&nbsp;Email</div>
    </div>
    <div id="menuImageUploadDiv" style="display: none;" class="uploaddiv" runat="server" clientidmode="Static">
        <asp:FileUpload ID="MenuImgFileUpload" runat="server" /> 
        <asp:Button ID="Button3" runat="server" Text="Upload Photo" OnClick="UploadMenuImage" ValidationGroup="photomenu" />
                <br/>    
        <i>Picture type must be .jpg and the size must be bigger than 250px width and height</i>
        <br/>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="photomenu" ErrorMessage="" ControlToValidate="MenuImgFileUpload"></asp:RequiredFieldValidator>
    </div>
    <asp:Literal ID="literalAddMenuToMyDiarySelect" runat="server"></asp:Literal>
</div>

<div style="clear:both"></div>

<div style="clear:both"></div>
<br />
<br />

<!-- End modification for new header -->

<div style="clear:both"></div>
<span style="display: none"><input type="checkbox" runat="server" id="isRecommended" style="float: left" /><div style="font-weight: bold; font-size: 14px;float: left; padding-top: 4px; padding-right: 10px">Is Recommended</div>
<input type="checkbox" runat="server" id="isFeatured" style="float: left" /><div style="font-weight: bold; font-size: 14px;float: left; padding-top: 4px; padding-right: 10px">Is Featured</div>
<div style="font-weight: bold; font-size: 14px;float: left; padding-top: 4px; padding-left: 20px; padding-right: 10px">Image: </div>
<input type="file" id="imageUrl" runat="server" style="width: 200px; height: 21px; border: 1px solid #999999; float: left" />
<div style="clear:both"></div></span>
<br />
<br />
<div class="result" id="menu_resultPage" style="display: none"></div>
<table width="885" border="0" cellpadding="0" cellspacing="0" id="menu-items">
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
<tr id="macro-goal">
<td style="padding-top: 15px;padding-left: 15px;" colspan="4">Daily Macronutrient Goals</td>
<td style="padding-top: 15px;padding-left: 15px;text-align: center;font-weight: bold" class="carb-goal"><asp:Literal ID="literalGoalCarb" runat="server"></asp:Literal></td>
<td style="padding-top: 15px;padding-left: 15px;text-align: center;font-weight: bold" class="protein-goal"><asp:Literal ID="literalGoalProtein" runat="server"></asp:Literal></td>
<td style="padding-top: 15px;padding-left: 15px;text-align: center;font-weight: bold" class="fat-goal"><asp:Literal ID="literalGoalFat" runat="server"></asp:Literal></td>
<td style="padding-top: 15px;padding-left: 15px;text-align: center;font-weight: bold">&nbsp;</td>
</tr>
<tr id="macro-total">
<td style="padding-top: 15px;padding-left: 15px;" colspan="4">Total Macronutrient (from Food Diary Entry)</td>
<td style="padding-top: 15px;padding-left: 15px;text-align: center;font-weight: bold" class="carb-total"><asp:Literal ID="literalTotalCarb" runat="server"></asp:Literal></td>
<td style="padding-top: 15px;padding-left: 15px;text-align: center;font-weight: bold" class="protein-total"><asp:Literal ID="literalTotalProtein" runat="server"></asp:Literal></td>
<td style="padding-top: 15px;padding-left: 15px;text-align: center;font-weight: bold" class="fat-total"><asp:Literal ID="literalTotalFat" runat="server"></asp:Literal></td>
<td style="padding-top: 15px;padding-left: 15px;text-align: center;font-weight: bold">&nbsp;</td>
</tr>
<tr id="macro-diff">
<td style="padding-top: 15px;padding-left: 15px;" colspan="4">Difference to Macronutrient</td>
<td style="padding-top: 15px;padding-left: 15px;text-align: center;font-weight: bold" class="carb-diff"><asp:Literal ID="literalDiffCarb" runat="server"></asp:Literal></td>
<td style="padding-top: 15px;padding-left: 15px;text-align: center;font-weight: bold" class="protein-diff"><asp:Literal ID="literalDiffProtein" runat="server"></asp:Literal></td>
<td style="padding-top: 15px;padding-left: 15px;text-align: center;font-weight: bold" class="fat-diff"><asp:Literal ID="literalDiffFat" runat="server"></asp:Literal></td>
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
<tr>
<td style="border-bottom: 1px solid #e5e5e5; padding-top: 15px; padding-bottom: 10px; font-size: 14px; font-weight: bold; padding-left: 11px;" colspan="8" >Breakfast</td>
</tr>
<asp:Literal ID="literalBreakfastRows" runat="server"></asp:Literal>
<tr class="buttons mealtime1" id="buttonsBreakfast">
<td style="padding-top: 5px" colspan="2"><div style="position: relative">
<div style="cursor: pointer; display: inline" id="addItemBreakfast"
     onclick="hide_one_of();show('addItemBreakfast_Select');hide('buttonsBreakfast');">
    <img src="/images/buttonAddItem.gif" /></div>
<div style="cursor: pointer; display: inline" id="addMealBreakfast" onclick="hide_one_of();show('addMealBreakfast_Select');hide('buttonsBreakfast');"><img src="/images/buttonAddMeal.gif" /></div>
<div style="cursor: pointer; display: inline" id="quickToolsBreakfast" onclick="var result = toggle_str('quickToolsBreakfast_Select');hide_one_of();switchto('quickToolsBreakfast_Select', result);"><img src="/images/buttonQuickTools.gif" /></div>

<div class="quicktools_options one_of" id="quickToolsBreakfast_Select">
    <div class="quicktools_option" onclick="hide_one_of();show('quickToolsBreakfast_SaveAsMeal_Select');">Save as meal</div>
</div>

<div class="one_of" style="z-index: 100; left: 114px; top: 29px; border: 1px solid #a0a0a0; display: none; position: absolute; width: 186px; height: 92px; padding-top: 6px; padding-left: 12px; background-color: #f1f1f1; font-weight: bold" id="quickToolsBreakfast_SaveAsMeal_Select">
    <p>Name this meal</p>
    <input type="text" id="saveAsMealName_Breakfast" style="width: 172px; height: 23px; margin-top: 10px; border: 1px solid #999999" />
    <div style="float: left; cursor: pointer; padding-top: 5px;"  onclick="hide('quickToolsBreakfast_SaveAsMeal_Select');saveMenuMealAsMeal(1,$('#saveAsMealName_Breakfast').val(),'resultBreakfast');"><img src="/images/buttonSaveMeal.gif" /></div>
    <div style="float: left; cursor: pointer; padding-top: 10px; padding-left: 21px; color: #e27423"  onclick="hide('quickToolsBreakfast_SaveAsMeal_Select');">or Cancel</div>
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
<div style="cursor: pointer; display: inline" id="addItemBreakfastSelected" onclick="hide('addItemBreakfast_Select');show('buttonsBreakfast');">
    <img src="/images/buttonAddItemSelected.gif" />
</div>
<div class="additem_options">
    <div class="additem_option additem_option_selected" onclick="additem_option_selected(this);
                                                                 show('addItemBreakfast_Search_Select');
                                                                 hide('addItemBreakfast_CopyPaste_Select');
                                                                 hide('addItemBreakfast_Recent_Select');
                                                                 hide('addItemBreakfast_AddFood_Select');
                                                                 clearRight();">Search & Add</div>
    <div class="additem_option" onclick="additem_option_selected(this);
                                         hide('addItemBreakfast_Search_Select');
                                         hide('addItemBreakfast_CopyPaste_Select');
                                         hide('addItemBreakfast_AddFood_Select');
                                         show('addItemBreakfast_Recent_Select');">Recent</div>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;margin-bottom: 10px;padding-bottom: 10px" id="addItemBreakfast_Search_Select">
    <div id="tabSearchByKeyword_Breakfast">
        <div style="font-weight: bold">
            <span>Search by Keyword</span>
            <a style="padding-left: 24px; font-weight: bold; color: #e27424"
               onclick="hide('tabSearchByKeyword_Breakfast');
                        show('tabSearchByCategory_Breakfast');
                        hide('tabSearchByMacronutrients_Breakfast');">By Category ></a>
            <a style="padding-left: 24px; font-weight: bold; color: #e27424"
               onclick="hide('tabSearchByKeyword_Breakfast');
                        hide('tabSearchByCategory_Breakfast');
                        show('tabSearchByMacronutrients_Breakfast');">By Macronutrients ></a>
        </div>
        <div style="float:left;">
            <input type="text" id="searchByKeyword_Breakfast" style="height: 23px; width: 345px; border: 1px solid #999999; margin-top: 11px;padding-left: 35px"
                   onkeyup="show('searchByKeyword_Breakfast_Result');
                            menu_getItemsByKeyword($('#searchByKeyword_Breakfast').val(),'searchByKeyword_Breakfast_Result',1, 'no');
                            showRight($(this),$('#rightBreakFast'));" />
        
            <div style="position: absolute; top: 45px; left: 23px; cursor: pointer;"
                 onclick="show('searchByKeyword_Breakfast_Result');
                          menu_getItemsByKeyword($('#searchByKeyword_Breakfast').val(),'searchByKeyword_Breakfast_Result',1, 'yes');">
                <img src="/images/buttonSearchMag.gif" />
            </div>
        </div>
        <!--Hiroshi-->
        <!--Added newly-->
        <div class="rightSide" id="rightBreakFast"><!--button click takes to add new item panel-->
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
    <div id="tabSearchByCategory_Breakfast" style="display: none">
        <span style="font-weight: bold">Search by Category</span>
        <a style="padding-left: 124px; font-weight: bold; color: #e27424" onclick="show('tabSearchByKeyword_Breakfast');hide('tabSearchByCategory_Breakfast');hide('tabSearchByMacronutrients_Breakfast');">By Keyword ></a>
        <a style="padding-left: 24px; font-weight: bold; color: #e27424" onclick="hide('tabSearchByKeyword_Breakfast');hide('tabSearchByCategory_Breakfast');show('tabSearchByMacronutrients_Breakfast');">By Macronutrients ></a>
        <asp:Literal ID="literalSearchByCategory_Breakfast" runat="server"></asp:Literal>
        <div id="searchByCategory_Breakfast_Result" class="search_results" style="display: none"></div>
    </div>
    <div id="tabSearchByMacronutrients_Breakfast" style="display: none">
        <span style="font-weight: bold">Search by Macronutrients</span>
        <a style="padding-left: 124px; font-weight: bold; color: #e27424" onclick="show('tabSearchByKeyword_Breakfast');hide('tabSearchByCategory_Breakfast');hide('tabSearchByMacronutrients_Breakfast');">By Keyword ></a>
        <a style="padding-left: 24px; font-weight: bold; color: #e27424" onclick="hide('tabSearchByKeyword_Breakfast');show('tabSearchByCategory_Breakfast');hide('tabSearchByMacronutrients_Breakfast');">By Category ></a>
        <div style="font-weight: bold; padding-top: 12px;">Carbs:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchByCarbs_Breakfast" /> Protein:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchByProtein_Breakfast" /> Fat:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchByFat_Breakfast" />
        <div style="display: inline-block; cursor: pointer; position: relative; left: -20px; top: 4px;"  onclick="menu_getItemsByMacronutrients($('#searchByCarbs_Breakfast').val(),$('#searchByProtein_Breakfast').val(),$('#searchByFat_Breakfast').val(),'searchByMacronutrients_Breakfast_Result',1);"><img src="/images/buttonSearchMag.gif" /></div>
        </div>
        <div id="searchByMacronutrients_Breakfast_Result" class="search_results" style="display: none"></div>
    </div>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;display:none; margin-bottom: 10px;padding-bottom: 10px" id="addItemBreakfast_Recent_Select">
    <select onchange="menu_getRecentByMealtime($('#recentSearchBreakfast').val(), 'addItemBreakfast_Recent_Results', 1)" id="recentSearchBreakfast">
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
    <div style="float:left;cursor: pointer; display: inline" id="addOwnFoodBreakfast"
         onclick="menu_addNewItem($('#brand').val(),
                                  $('#foodName').val(),
                                  $('#carbs').val(),
                                  $('#fat').val(),
                                  $('#ptn').val(),
                                  $('#amount').val(),
                                  $('#serve').val(),'1');">
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
    <div class="additem_option additem_option_selected"
         onclick="additem_option_selected(this);
                  show('addMealBreakfast_Search_Select');
                  hide('addMealBreakfast_Recent_Select');">Search</div>
    <div class="additem_option" onclick="additem_option_selected(this);
                                         hide('addMealBreakfast_Search_Select');
                                         show('addMealBreakfast_Recent_Select');">Recent</div>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;margin-bottom: 10px;padding-bottom: 10px" id="addMealBreakfast_Search_Select">
    <div id="tabSearchMealByKeyword_Breakfast">
        <div style="font-weight: bold">Search by Keyword</div>
        <input type="text" id="searchMealByKeyword_Breakfast" style="height: 23px; width: 345px; border: 1px solid #999999; margin-top: 11px;padding-left: 35px" onkeyup="show('searchMealByKeyword_Breakfast_Result');menu_getMealsByKeyword($('#searchMealByKeyword_Breakfast').val(),'searchMealByKeyword_Breakfast_Result',1, 'no');" />
        <a style="padding-left: 24px; font-weight: bold; color: #e27424" onclick="hide('tabSearchMealByKeyword_Breakfast');show('tabSearchMealByMacronutrients_Breakfast');">By Macronutrients ></a>
        <div style="position: absolute; top: 45px; left: 23px; cursor: pointer;"  onclick="show('searchMealByKeyword_Breakfast_Result');menu_getMealsByKeyword($('#searchMealByKeyword_Breakfast').val(),'searchMealByKeyword_Breakfast_Result',1, 'yes');"><img src="/images/buttonSearchMag.gif" /></div>
        <div id="searchMealByKeyword_Breakfast_Result" class="search_results" style="display: none"></div>
    </div>

    <div id="tabSearchMealByMacronutrients_Breakfast" style="display: none">
        <span style="font-weight: bold">Search by Macronutrients</span>
        <a style="padding-left: 124px; font-weight: bold; color: #e27424" onclick="show('tabSearchMealByKeyword_Breakfast');hide('tabSearchMealByMacronutrients_Breakfast');">By Keyword ></a>
        <div style="font-weight: bold; padding-top: 12px;">Carbs:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByCarbs_Breakfast" /> Protein:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByProtein_Breakfast" /> Fat:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByFat_Breakfast" />
        <div style="display: inline-block; cursor: pointer; position: relative; left: -20px; top: 4px;"  onclick="menu_getMealsByMacronutrients($('#searchMealByCarbs_Breakfast').val(),$('#searchMealByProtein_Breakfast').val(),$('#searchMealByFat_Breakfast').val(),'searchMealByMacronutrients_Breakfast_Result',1);"><img src="/images/buttonSearchMag.gif" /></div>
        </div>
        <div id="searchMealByMacronutrients_Breakfast_Result" class="search_results" style="display: none"></div>
    </div>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;display:none; margin-bottom: 10px;padding-bottom: 10px" id="addMealBreakfast_Recent_Select">
    <select onchange="menu_getRecentMealsByMealtime($('#recentSearchMealBreakfast').val(), 'addMealBreakfast_Recent_Results', 1)" id="recentSearchMealBreakfast">
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
<td style="border-bottom: 1px solid #e5e5e5; padding-top: 15px; padding-bottom: 10px; font-size: 14px; font-weight: bold; padding-left: 11px;" colspan="8" >Morning Tea</td>
</tr>
<asp:Literal ID="literalMorningTeaRows" runat="server"></asp:Literal>
<tr class="buttons mealtime2" id="buttonsMorningTea">
<td style="padding-top: 5px" colspan="2"><div style="position: relative">
<div style="cursor: pointer; display: inline" id="addItemMorningTea" onclick="hide_one_of();show('addItemMorningTea_Select');hide('buttonsMorningTea');"><img src="/images/buttonAddItem.gif" /></div>
<div style="cursor: pointer; display: inline" id="addMealMorningTea" onclick="hide_one_of();show('addMealMorningTea_Select');hide('buttonsMorningTea');"><img src="/images/buttonAddMeal.gif" /></div>
<div style="cursor: pointer; display: inline" id="quickToolsMorningTea" onclick="var result = toggle_str('quickToolsMorningTea_Select');hide_one_of();switchto('quickToolsMorningTea_Select', result);"><img src="/images/buttonQuickTools.gif" /></div>

<div class="quicktools_options one_of" id="quickToolsMorningTea_Select">
    <div class="quicktools_option" onclick="hide_one_of();show('quickToolsMorningTea_SaveAsMeal_Select');">Save as meal</div>
</div>

<div class="one_of" style="z-index: 100; left: 114px; top: 29px; border: 1px solid #a0a0a0; display: none; position: absolute; width: 186px; height: 92px; padding-top: 6px; padding-left: 12px; background-color: #f1f1f1; font-weight: bold" id="quickToolsMorningTea_SaveAsMeal_Select">
    <p>Name this meal</p>
    <input type="text" id="saveAsMealName_MorningTea" style="width: 172px; height: 23px; margin-top: 10px; border: 1px solid #999999" />
    <div style="float: left; cursor: pointer; padding-top: 5px;"  onclick="hide('quickToolsMorningTea_SaveAsMeal_Select');saveMenuMealAsMeal(2,$('#saveAsMealName_MorningTea').val(),'resultMorningTea');"><img src="/images/buttonSaveMeal.gif" /></div>
    <div style="float: left; cursor: pointer; padding-top: 10px; padding-left: 21px; color: #e27423"  onclick="hide('quickToolsMorningTea_SaveAsMeal_Select');">or Cancel</div>
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
    <div class="additem_option additem_option_selected" onclick="additem_option_selected(this);
                                                                 show('addItemMorningTea_Search_Select');
                                                                 hide('addItemMorningTea_CopyPaste_Select');
                                                                 hide('addItemMorningTea_Recent_Select');
                                                                 hide('addItemMorningTea_AddFood_Select');
                                                                 clearRight();">Search & Add</div>
    <div class="additem_option" onclick="additem_option_selected(this);
                                         hide('addItemMorningTea_Search_Select');
                                         hide('addItemMorningTea_CopyPaste_Select');
                                         hide('addItemMorningTea_AddFood_Select');
                                         show('addItemMorningTea_Recent_Select');">Recent</div>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;margin-bottom: 10px;padding-bottom: 10px" id="addItemMorningTea_Search_Select">
    <div id="tabSearchByKeyword_MorningTea">
        <div style="font-weight: bold">
            <span>Search by Keyword</span>
            <a style="padding-left: 24px; font-weight: bold; color: #e27424"
               onclick="hide('tabSearchByKeyword_MorningTea');
                        show('tabSearchByCategory_MorningTea');
                        hide('tabSearchByMacronutrients_MorningTea');">By Category ></a>
            <a style="padding-left: 24px; font-weight: bold; color: #e27424"
               onclick="hide('tabSearchByKeyword_MorningTea');
                        hide('tabSearchByCategory_MorningTea');
                        show('tabSearchByMacronutrients_MorningTea');">By Macronutrients ></a>           
        </div>
        <div style="float:left;">
            <input type="text" id="searchByKeyword_MorningTea" style="height: 23px; width: 345px; border: 1px solid #999999; margin-top: 11px;padding-left: 35px"
                   onkeyup="show('searchByKeyword_MorningTea_Result');
                            menu_getItemsByKeyword($('#searchByKeyword_MorningTea').val(),'searchByKeyword_MorningTea_Result',2, 'no');
                            showRight($(this),$('#rightMorningTea'));" />
            <div style="position: absolute; top: 45px; left: 23px; cursor: pointer;"
                 onclick="show('searchByKeyword_MorningTea_Result');
                          menu_getItemsByKeyword($('#searchByKeyword_MorningTea').val(),'searchByKeyword_MorningTea_Result',2, 'yes');">
                <img src="/images/buttonSearchMag.gif" />
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
    <div id="tabSearchByCategory_MorningTea" style="display: none">
        <span style="font-weight: bold">Search by Category</span>
        <a style="padding-left: 124px; font-weight: bold; color: #e27424" onclick="show('tabSearchByKeyword_MorningTea');hide('tabSearchByCategory_MorningTea');hide('tabSearchByMacronutrients_MorningTea');">By Keyword ></a>
        <a style="padding-left: 24px; font-weight: bold; color: #e27424" onclick="hide('tabSearchByKeyword_MorningTea');hide('tabSearchByCategory_MorningTea');show('tabSearchByMacronutrients_MorningTea');">By Macronutrients ></a>
        <asp:Literal ID="literalSearchByCategory_MorningTea" runat="server"></asp:Literal>
        <div id="searchByCategory_MorningTea_Result" class="search_results" style="display: none"></div>
    </div>
    <div id="tabSearchByMacronutrients_MorningTea" style="display: none">
        <span style="font-weight: bold">Search by Macronutrients</span>
        <a style="padding-left: 124px; font-weight: bold; color: #e27424" onclick="show('tabSearchByKeyword_MorningTea');hide('tabSearchByCategory_MorningTea');hide('tabSearchByMacronutrients_MorningTea');">By Keyword ></a>
        <a style="padding-left: 24px; font-weight: bold; color: #e27424" onclick="hide('tabSearchByKeyword_MorningTea');show('tabSearchByCategory_MorningTea');hide('tabSearchByMacronutrients_MorningTea');">By Category ></a>
        <div style="font-weight: bold; padding-top: 12px;">Carbs:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchByCarbs_MorningTea" /> Protein:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchByProtein_MorningTea" /> Fat:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchByFat_MorningTea" />
        <div style="display: inline-block; cursor: pointer; position: relative; left: -20px; top: 4px;"  onclick="menu_getItemsByMacronutrients($('#searchByCarbs_MorningTea').val(),$('#searchByProtein_MorningTea').val(),$('#searchByFat_MorningTea').val(),'searchByMacronutrients_MorningTea_Result',2);"><img src="/images/buttonSearchMag.gif" /></div>
        </div>
        <div id="searchByMacronutrients_MorningTea_Result" class="search_results" style="display: none"></div>
    </div>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;display:none; margin-bottom: 10px;padding-bottom: 10px" id="addItemMorningTea_Recent_Select">
    <select onchange="menu_getRecentByMealtime($('#recentSearchMorningTea').val(), 'addItemMorningTea_Recent_Results', 2)" id="recentSearchMorningTea">
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
    <br/><!--here-->
    <div style="float:left;cursor: pointer; display: inline" id="Div4"
         onclick="menu_addNewItem($('#AOBrandName_MorningTea').val(),
                                  $('#AOFoodName_MorningTea').val(),
                                  $('#AOCarbs_MorningTea').val(),
                                  $('#AOFat_MorningTea').val(),
                                  $('#AOPtn_MorningTea').val(),
                                  $('#AOAmount_MorningTea').val(),
                                  $('#AOUnit_MorningTea').val(),'2');">
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
        <div style="font-weight: bold">Search by Keyword</div>
        <input type="text" id="searchMealByKeyword_MorningTea" style="height: 23px; width: 345px; border: 1px solid #999999; margin-top: 11px;padding-left: 35px" onkeyup="show('searchMealByKeyword_MorningTea_Result');menu_getMealsByKeyword($('#searchMealByKeyword_MorningTea').val(),'searchMealByKeyword_MorningTea_Result',2, 'no');" />
        <a style="padding-left: 24px; font-weight: bold; color: #e27424" onclick="hide('tabSearchMealByKeyword_MorningTea');show('tabSearchMealByMacronutrients_MorningTea');">By Macronutrients ></a>
        <div style="position: absolute; top: 45px; left: 23px; cursor: pointer;"  onclick="show('searchMealByKeyword_MorningTea_Result');menu_getMealsByKeyword($('#searchMealByKeyword_MorningTea').val(),'searchMealByKeyword_MorningTea_Result',2, 'yes');"><img src="/images/buttonSearchMag.gif" /></div>
        <div id="searchMealByKeyword_MorningTea_Result" class="search_results" style="display: none"></div>
    </div>

    <div id="tabSearchMealByMacronutrients_MorningTea" style="display: none">
        <span style="font-weight: bold">Search by Macronutrients</span>
        <a style="padding-left: 124px; font-weight: bold; color: #e27424" onclick="show('tabSearchMealByKeyword_MorningTea');hide('tabSearchMealByMacronutrients_MorningTea');">By Keyword ></a>
        <div style="font-weight: bold; padding-top: 12px;">Carbs:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByCarbs_MorningTea" /> Protein:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByProtein_MorningTea" /> Fat:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByFat_MorningTea" />
        <div style="display: inline-block; cursor: pointer; position: relative; left: -20px; top: 4px;"  onclick="menu_getMealsByMacronutrients($('#searchMealByCarbs_MorningTea').val(),$('#searchMealByProtein_MorningTea').val(),$('#searchMealByFat_MorningTea').val(),'searchMealByMacronutrients_MorningTea_Result',2);"><img src="/images/buttonSearchMag.gif" /></div>
        </div>
        <div id="searchMealByMacronutrients_MorningTea_Result" class="search_results" style="display: none"></div>
    </div>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;display:none; margin-bottom: 10px;padding-bottom: 10px" id="addMealMorningTea_Recent_Select">
    <select onchange="menu_getRecentMealsByMealtime($('#recentSearchMealMorningTea').val(), 'addMealMorningTea_Recent_Results', 2)" id="recentSearchMealMorningTea">
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
<td style="border-bottom: 1px solid #e5e5e5; padding-top: 15px; padding-bottom: 10px; font-size: 14px; font-weight: bold; padding-left: 11px;" colspan="8" >Lunch</td>
</tr>
<asp:Literal ID="literalLunchRows" runat="server"></asp:Literal>
<tr class="buttons mealtime3" id="buttonsLunch">
<td style="padding-top: 5px" colspan="2"><div style="position: relative">
<div style="cursor: pointer; display: inline" id="addItemLunch" onclick="hide_one_of();show('addItemLunch_Select');hide('buttonsLunch');"><img src="/images/buttonAddItem.gif" /></div>
<div style="cursor: pointer; display: inline" id="addMealLunch" onclick="hide_one_of();show('addMealLunch_Select');hide('buttonsLunch');"><img src="/images/buttonAddMeal.gif" /></div>
<div style="cursor: pointer; display: inline" id="quickToolsLunch" onclick="var result = toggle_str('quickToolsLunch_Select');hide_one_of();switchto('quickToolsLunch_Select', result);"><img src="/images/buttonQuickTools.gif" /></div>

<div class="quicktools_options one_of" id="quickToolsLunch_Select">
    <div class="quicktools_option" onclick="hide_one_of();show('quickToolsLunch_SaveAsMeal_Select');">Save as meal</div>
</div>

<div class="one_of" style="z-index: 100; left: 114px; top: 29px; border: 1px solid #a0a0a0; display: none; position: absolute; width: 186px; height: 92px; padding-top: 6px; padding-left: 12px; background-color: #f1f1f1; font-weight: bold" id="quickToolsLunch_SaveAsMeal_Select">
    <p>Name this meal</p>
    <input type="text" id="saveAsMealName_Lunch" style="width: 172px; height: 23px; margin-top: 10px; border: 1px solid #999999" />
    <div style="float: left; cursor: pointer; padding-top: 5px;"  onclick="hide('quickToolsLunch_SaveAsMeal_Select');saveMenuMealAsMeal(3,$('#saveAsMealName_Lunch').val(),'resultLunch');"><img src="/images/buttonSaveMeal.gif" /></div>
    <div style="float: left; cursor: pointer; padding-top: 10px; padding-left: 21px; color: #e27423"  onclick="hide('quickToolsLunch_SaveAsMeal_Select');">or Cancel</div>
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
         onclick="additem_option_selected(this);
                  show('addItemLunch_Search_Select');
                  hide('addItemLunch_CopyPaste_Select');
                  hide('addItemLunch_Recent_Select');
                  hide('addItemLunch_AddFood_Select');
                  clearRight()">Search & Add</div>
    <div class="additem_option" onclick="additem_option_selected(this);
                                         hide('addItemLunch_Search_Select');
                                         hide('addItemLunch_CopyPaste_Select');
                                         hide('addItemLunch_AddFood_Select');
                                         show('addItemLunch_Recent_Select');">Recent</div>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;margin-bottom: 10px;padding-bottom: 10px" id="addItemLunch_Search_Select">
    <div id="tabSearchByKeyword_Lunch">
        <div style="font-weight: bold">
            <span>Search by Keyword</span>
            <a style="padding-left: 24px; font-weight: bold; color: #e27424"
               onclick="hide('tabSearchByKeyword_Lunch');
                        show('tabSearchByCategory_Lunch');
                        hide('tabSearchByMacronutrients_Lunch');">By Category ></a>
            <a style="padding-left: 24px; font-weight: bold; color: #e27424"
               onclick="hide('tabSearchByKeyword_Lunch');
                        hide('tabSearchByCategory_Lunch');
                        show('tabSearchByMacronutrients_Lunch');">By Macronutrients ></a>
        </div>
        <div style="float:left;">
            <input type="text" id="searchByKeyword_Lunch" style="height: 23px; width: 345px; border: 1px solid #999999; margin-top: 11px;padding-left: 35px"
                   onkeyup="show('searchByKeyword_Lunch_Result');
                            menu_getItemsByKeyword($('#searchByKeyword_Lunch').val(),'searchByKeyword_Lunch_Result',3, 'no');
                            showRight($(this),$('#rightLunch'));" />     
            <div style="position: absolute; top: 45px; left: 23px; cursor: pointer;"
                 onclick="show('searchByKeyword_Lunch_Result');
                          menu_getItemsByKeyword($('#searchByKeyword_Lunch').val(),'searchByKeyword_Lunch_Result',3, 'yes');">
                <img src="/images/buttonSearchMag.gif" />
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
        <asp:Literal ID="literalSearchByCategory_Lunch" runat="server"></asp:Literal>
        <div id="searchByCategory_Lunch_Result" class="search_results" style="display: none"></div>
    </div>
    <div id="tabSearchByMacronutrients_Lunch" style="display: none">
        <span style="font-weight: bold">Search by Macronutrients</span>
        <a style="padding-left: 124px; font-weight: bold; color: #e27424" onclick="show('tabSearchByKeyword_Lunch');hide('tabSearchByCategory_Lunch');hide('tabSearchByMacronutrients_Lunch');">By Keyword ></a>
        <a style="padding-left: 24px; font-weight: bold; color: #e27424" onclick="hide('tabSearchByKeyword_Lunch');show('tabSearchByCategory_Lunch');hide('tabSearchByMacronutrients_Lunch');">By Category ></a>
        <div style="font-weight: bold; padding-top: 12px;">Carbs:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchByCarbs_Lunch" /> Protein:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchByProtein_Lunch" /> Fat:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchByFat_Lunch" />
        <div style="display: inline-block; cursor: pointer; position: relative; left: -20px; top: 4px;"  onclick="menu_getItemsByMacronutrients($('#searchByCarbs_Lunch').val(),$('#searchByProtein_Lunch').val(),$('#searchByFat_Lunch').val(),'searchByMacronutrients_Lunch_Result',3);"><img src="/images/buttonSearchMag.gif" /></div>
        </div>
        <div id="searchByMacronutrients_Lunch_Result" class="search_results" style="display: none"></div>
    </div>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;display:none; margin-bottom: 10px;padding-bottom: 10px" id="addItemLunch_Recent_Select">
    <select onchange="menu_getRecentByMealtime($('#recentSearchLunch').val(), 'addItemLunch_Recent_Results', 3)" id="recentSearchLunch">
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
    <div style="float:left;cursor: pointer; display: inline" id="Div7"
         onclick="menu_addNewItem($('#AOBrandName_Lunch').val(),
                                  $('#AOFoodName_Lunch').val(),
                                  $('#AOCarbs_Lunch').val(),
                                  $('#AOFat_Lunch').val(),
                                  $('#AOPtn_Lunch').val(),
                                  $('#AOAmount_Lunch').val(),
                                  $('#AOUnit_Lunch').val(),'3');">
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
        <div style="font-weight: bold">Search by Keyword</div>
        <input type="text" id="searchMealByKeyword_Lunch" style="height: 23px; width: 345px; border: 1px solid #999999; margin-top: 11px;padding-left: 35px" onkeyup="show('searchMealByKeyword_Lunch_Result');menu_getMealsByKeyword($('#searchMealByKeyword_Lunch').val(),'searchMealByKeyword_Lunch_Result',3, 'no');" />
        <a style="padding-left: 24px; font-weight: bold; color: #e27424" onclick="hide('tabSearchMealByKeyword_Lunch');show('tabSearchMealByMacronutrients_Lunch');">By Macronutrients ></a>
        <div style="position: absolute; top: 45px; left: 23px; cursor: pointer;"  onclick="show('searchMealByKeyword_Lunch_Result');menu_getMealsByKeyword($('#searchMealByKeyword_Lunch').val(),'searchMealByKeyword_Lunch_Result',3, 'yes');"><img src="/images/buttonSearchMag.gif" /></div>
        <div id="searchMealByKeyword_Lunch_Result" class="search_results" style="display: none"></div>
    </div>

    <div id="tabSearchMealByMacronutrients_Lunch" style="display: none">
        <span style="font-weight: bold">Search by Macronutrients</span>
        <a style="padding-left: 124px; font-weight: bold; color: #e27424" onclick="show('tabSearchMealByKeyword_Lunch');hide('tabSearchMealByMacronutrients_Lunch');">By Keyword ></a>
        <div style="font-weight: bold; padding-top: 12px;">Carbs:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByCarbs_Lunch" /> Protein:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByProtein_Lunch" /> Fat:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByFat_Lunch" />
        <div style="display: inline-block; cursor: pointer; position: relative; left: -20px; top: 4px;"  onclick="menu_getMealsByMacronutrients($('#searchMealByCarbs_Lunch').val(),$('#searchMealByProtein_Lunch').val(),$('#searchMealByFat_Lunch').val(),'searchMealByMacronutrients_Lunch_Result',3);"><img src="/images/buttonSearchMag.gif" /></div>
        </div>
        <div id="searchMealByMacronutrients_Lunch_Result" class="search_results" style="display: none"></div>
    </div>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;display:none; margin-bottom: 10px;padding-bottom: 10px" id="addMealLunch_Recent_Select">
    <select onchange="menu_getRecentMealsByMealtime($('#recentSearchMealLunch').val(), 'addMealLunch_Recent_Results', 3)" id="recentSearchMealLunch">
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
<td style="border-bottom: 1px solid #e5e5e5; padding-top: 15px; padding-bottom: 10px; font-size: 14px; font-weight: bold; padding-left: 11px;" colspan="8" >Afternoon Tea</td>
</tr>
<asp:Literal ID="literalAfternoonTeaRows" runat="server"></asp:Literal>
<tr class="buttons mealtime4" id="buttonsAfternoonTea">
<td style="padding-top: 5px" colspan="2"><div style="position: relative">
<div style="cursor: pointer; display: inline" id="addItemAfternoonTea" onclick="hide_one_of();show('addItemAfternoonTea_Select');hide('buttonsAfternoonTea');"><img src="/images/buttonAddItem.gif" /></div>
<div style="cursor: pointer; display: inline" id="addMealAfternoonTea" onclick="hide_one_of();show('addMealAfternoonTea_Select');hide('buttonsAfternoonTea');"><img src="/images/buttonAddMeal.gif" /></div>
<div style="cursor: pointer; display: inline" id="quickToolsAfternoonTea" onclick="var result = toggle_str('quickToolsAfternoonTea_Select');hide_one_of();switchto('quickToolsAfternoonTea_Select', result);"><img src="/images/buttonQuickTools.gif" /></div>

<div class="quicktools_options one_of" id="quickToolsAfternoonTea_Select">
    <div class="quicktools_option" onclick="hide_one_of();show('quickToolsAfternoonTea_SaveAsMeal_Select');">Save as meal</div>
</div>

<div class="one_of" style="z-index: 100; left: 114px; top: 29px; border: 1px solid #a0a0a0; display: none; position: absolute; width: 186px; height: 92px; padding-top: 6px; padding-left: 12px; background-color: #f1f1f1; font-weight: bold" id="quickToolsAfternoonTea_SaveAsMeal_Select">
    <p>Name this meal</p>
    <input type="text" id="saveAsMealName_AfternoonTea" style="width: 172px; height: 23px; margin-top: 10px; border: 1px solid #999999" />
    <div style="float: left; cursor: pointer; padding-top: 5px;"  onclick="hide('quickToolsAfternoonTea_SaveAsMeal_Select');saveMenuMealAsMeal(4,$('#saveAsMealName_AfternoonTea').val(),'resultAfternoonTea');"><img src="/images/buttonSaveMeal.gif" /></div>
    <div style="float: left; cursor: pointer; padding-top: 10px; padding-left: 21px; color: #e27423"  onclick="hide('quickToolsAfternoonTea_SaveAsMeal_Select');">or Cancel</div>
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
                                         hide('addItemAfternoonTea_CopyPaste_Select');
                                         hide('addItemAfternoonTea_AddFood_Select');
                                         show('addItemAfternoonTea_Recent_Select');">Recent</div>
</div>
<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;margin-bottom: 10px;padding-bottom: 10px" id="addItemAfternoonTea_Search_Select">
    <div id="tabSearchByKeyword_AfternoonTea">
        <div style="font-weight: bold">
            <span>Search by Keyword</span>
            <a style="padding-left: 24px; font-weight: bold; color: #e27424"
               onclick="hide('tabSearchByKeyword_AfternoonTea');
                        show('tabSearchByCategory_AfternoonTea');
                        hide('tabSearchByMacronutrients_AfternoonTea');">By Category ></a>
            <a style="padding-left: 24px; font-weight: bold; color: #e27424"
               onclick="hide('tabSearchByKeyword_AfternoonTea');
                        hide('tabSearchByCategory_AfternoonTea');
                        show('tabSearchByMacronutrients_AfternoonTea');">By Macronutrients ></a>
        </div>
        <div style="float:left;">
            <input type="text" id="searchByKeyword_AfternoonTea" style="height: 23px; width: 345px; border: 1px solid #999999; margin-top: 11px;padding-left: 35px"
                   onkeyup="show('searchByKeyword_AfternoonTea_Result');
                            menu_getItemsByKeyword($('#searchByKeyword_AfternoonTea').val(),'searchByKeyword_AfternoonTea_Result',4, 'no');
                            showRight($(this),$('#rightAfternoonTea'));" />
            <div style="position: absolute; top: 45px; left: 23px; cursor: pointer;"
                 onclick="show('searchByKeyword_AfternoonTea_Result');
                          menu_getItemsByKeyword($('#searchByKeyword_AfternoonTea').val(),'searchByKeyword_AfternoonTea_Result',4, 'yes');">
                <img src="/images/buttonSearchMag.gif" />
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
        <asp:Literal ID="literalSearchByCategory_AfternoonTea" runat="server"></asp:Literal>
        <div id="searchByCategory_AfternoonTea_Result" class="search_results" style="display: none"></div>
    </div>
    <div id="tabSearchByMacronutrients_AfternoonTea" style="display: none">
        <span style="font-weight: bold">Search by Macronutrients</span>
        <a style="padding-left: 124px; font-weight: bold; color: #e27424" onclick="show('tabSearchByKeyword_AfternoonTea');hide('tabSearchByCategory_AfternoonTea');hide('tabSearchByMacronutrients_AfternoonTea');">By Keyword ></a>
        <a style="padding-left: 24px; font-weight: bold; color: #e27424" onclick="hide('tabSearchByKeyword_AfternoonTea');show('tabSearchByCategory_AfternoonTea');hide('tabSearchByMacronutrients_AfternoonTea');">By Category ></a>
        <div style="font-weight: bold; padding-top: 12px;">Carbs:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchByCarbs_AfternoonTea" /> Protein:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchByProtein_AfternoonTea" /> Fat:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchByFat_AfternoonTea" />
        <div style="display: inline-block; cursor: pointer; position: relative; left: -20px; top: 4px;"  onclick="menu_getItemsByMacronutrients($('#searchByCarbs_AfternoonTea').val(),$('#searchByProtein_AfternoonTea').val(),$('#searchByFat_AfternoonTea').val(),'searchByMacronutrients_AfternoonTea_Result',4);"><img src="/images/buttonSearchMag.gif" /></div>
        </div>
        <div id="searchByMacronutrients_AfternoonTea_Result" class="search_results" style="display: none"></div>
    </div>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;display:none; margin-bottom: 10px;padding-bottom: 10px" id="addItemAfternoonTea_Recent_Select">
    <select onchange="menu_getRecentByMealtime($('#recentSearchAfternoonTea').val(), 'addItemAfternoonTea_Recent_Results', 4)" id="recentSearchAfternoonTea">
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
    <div style="float:left;cursor: pointer; display: inline" id="Div8"
         onclick="menu_addNewItem($('#AOBrandName_AfternoonTea').val(),
                                  $('#AOFoodName_AfternoonTea').val(),
                                  $('#AOCarbs_AfternoonTea').val(),
                                  $('#AOFat_AfternoonTea').val(),
                                  $('#AOPtn_AfternoonTea').val(),
                                  $('#AOAmount_AfternoonTea').val(),
                                  $('#AOUnit_AfternoonTea').val(),'4');">
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
        <div style="font-weight: bold">Search by Keyword</div>
        <input type="text" id="searchMealByKeyword_AfternoonTea" style="height: 23px; width: 345px; border: 1px solid #999999; margin-top: 11px;padding-left: 35px" onkeyup="show('searchMealByKeyword_AfternoonTea_Result');menu_getMealsByKeyword($('#searchMealByKeyword_AfternoonTea').val(),'searchMealByKeyword_AfternoonTea_Result',4, 'no');" />
        <a style="padding-left: 24px; font-weight: bold; color: #e27424" onclick="hide('tabSearchMealByKeyword_AfternoonTea');show('tabSearchMealByMacronutrients_AfternoonTea');">By Macronutrients ></a>
        <div style="position: absolute; top: 45px; left: 23px; cursor: pointer;"  onclick="show('searchMealByKeyword_AfternoonTea_Result');menu_getMealsByKeyword($('#searchMealByKeyword_AfternoonTea').val(),'searchMealByKeyword_AfternoonTea_Result',4, 'yes');"><img src="/images/buttonSearchMag.gif" /></div>
        <div id="searchMealByKeyword_AfternoonTea_Result" class="search_results" style="display: none"></div>
    </div>

    <div id="tabSearchMealByMacronutrients_AfternoonTea" style="display: none">
        <span style="font-weight: bold">Search by Macronutrients</span>
        <a style="padding-left: 124px; font-weight: bold; color: #e27424" onclick="show('tabSearchMealByKeyword_AfternoonTea');hide('tabSearchMealByMacronutrients_AfternoonTea');">By Keyword ></a>
        <div style="font-weight: bold; padding-top: 12px;">Carbs:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByCarbs_AfternoonTea" /> Protein:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByProtein_AfternoonTea" /> Fat:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByFat_AfternoonTea" />
        <div style="display: inline-block; cursor: pointer; position: relative; left: -20px; top: 4px;"  onclick="menu_getMealsByMacronutrients($('#searchMealByCarbs_AfternoonTea').val(),$('#searchMealByProtein_AfternoonTea').val(),$('#searchMealByFat_AfternoonTea').val(),'searchMealByMacronutrients_AfternoonTea_Result',4);"><img src="/images/buttonSearchMag.gif" /></div>
        </div>
        <div id="searchMealByMacronutrients_AfternoonTea_Result" class="search_results" style="display: none"></div>
    </div>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;display:none; margin-bottom: 10px;padding-bottom: 10px" id="addMealAfternoonTea_Recent_Select">
    <select onchange="menu_getRecentMealsByMealtime($('#recentSearchMealAfternoonTea').val(), 'addMealAfternoonTea_Recent_Results', 4)" id="recentSearchMealAfternoonTea">
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
<td style="border-bottom: 1px solid #e5e5e5; padding-top: 15px; padding-bottom: 10px; font-size: 14px; font-weight: bold; padding-left: 11px;" colspan="8" >Dinner</td>
</tr>
<asp:Literal ID="literalDinnerRows" runat="server"></asp:Literal>
<tr class="buttons mealtime5" id="buttonsDinner">
<td style="padding-top: 5px" colspan="2"><div style="position: relative">
<div style="cursor: pointer; display: inline" id="addItemDinner" onclick="hide_one_of();show('addItemDinner_Select');hide('buttonsDinner');"><img src="/images/buttonAddItem.gif" /></div>
<div style="cursor: pointer; display: inline" id="addMealDinner" onclick="hide_one_of();show('addMealDinner_Select');hide('buttonsDinner');"><img src="/images/buttonAddMeal.gif" /></div>
<div style="cursor: pointer; display: inline" id="quickToolsDinner" onclick="var result = toggle_str('quickToolsDinner_Select');hide_one_of();switchto('quickToolsDinner_Select', result);"><img src="/images/buttonQuickTools.gif" /></div>

<div class="quicktools_options one_of" id="quickToolsDinner_Select">
    <div class="quicktools_option" onclick="hide_one_of();show('quickToolsDinner_SaveAsMeal_Select');">Save as meal</div>
</div>

<div class="one_of" style="z-index: 100; left: 114px; top: 29px; border: 1px solid #a0a0a0; display: none; position: absolute; width: 186px; height: 92px; padding-top: 6px; padding-left: 12px; background-color: #f1f1f1; font-weight: bold" id="quickToolsDinner_SaveAsMeal_Select">
    <p>Name this meal</p>
    <input type="text" id="saveAsMealName_Dinner" style="width: 172px; height: 23px; margin-top: 10px; border: 1px solid #999999" />
    <div style="float: left; cursor: pointer; padding-top: 5px;"  onclick="hide('quickToolsDinner_SaveAsMeal_Select');saveMenuMealAsMeal(5,$('#saveAsMealName_Dinner').val(),'resultDinner');"><img src="/images/buttonSaveMeal.gif" /></div>
    <div style="float: left; cursor: pointer; padding-top: 10px; padding-left: 21px; color: #e27423"  onclick="hide('quickToolsDinner_SaveAsMeal_Select');">or Cancel</div>
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
                                         hide('addItemDinner_CopyPaste_Select');
                                         hide('addItemDinner_AddFood_Select');
                                         show('addItemDinner_Recent_Select');">Recent</div>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;margin-bottom: 10px;padding-bottom: 10px" id="addItemDinner_Search_Select">
    <div id="tabSearchByKeyword_Dinner">
        <div style="font-weight: bold">
            <span>Search by Keyword</span>
            <a style="padding-left: 24px; font-weight: bold; color: #e27424"
               onclick="hide('tabSearchByKeyword_Dinner');
                        show('tabSearchByCategory_Dinner');
                        hide('tabSearchByMacronutrients_Dinner');">By Category ></a>
            <a style="padding-left: 24px; font-weight: bold; color: #e27424"
               onclick="hide('tabSearchByKeyword_Dinner');
                        hide('tabSearchByCategory_Dinner');
                        show('tabSearchByMacronutrients_Dinner');">By Macronutrients ></a>
        </div>
        <div style="float:left;">
            <input type="text" id="searchByKeyword_Dinner" style="height: 23px; width: 345px; border: 1px solid #999999; margin-top: 11px;padding-left: 35px"
                   onkeyup="show('searchByKeyword_Dinner_Result');
                            menu_getItemsByKeyword($('#searchByKeyword_Dinner').val(),'searchByKeyword_Dinner_Result',5, 'no');
                            showRight($(this),$('#rightDinner'));" />       
            <div style="position: absolute; top: 45px; left: 23px; cursor: pointer;"
                 onclick="show('searchByKeyword_Dinner_Result');
                          menu_getItemsByKeyword($('#searchByKeyword_Dinner').val(),'searchByKeyword_Dinner_Result',5, 'yes');">
                <img src="/images/buttonSearchMag.gif" />
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
        <asp:Literal ID="literalSearchByCategory_Dinner" runat="server"></asp:Literal>
        <div id="searchByCategory_Dinner_Result" class="search_results" style="display: none"></div>
    </div>
    <div id="tabSearchByMacronutrients_Dinner" style="display: none">
        <span style="font-weight: bold">Search by Macronutrients</span>
        <a style="padding-left: 124px; font-weight: bold; color: #e27424" onclick="show('tabSearchByKeyword_Dinner');hide('tabSearchByCategory_Dinner');hide('tabSearchByMacronutrients_Dinner');">By Keyword ></a>
        <a style="padding-left: 24px; font-weight: bold; color: #e27424" onclick="hide('tabSearchByKeyword_Dinner');show('tabSearchByCategory_Dinner');hide('tabSearchByMacronutrients_Dinner');">By Category ></a>
        <div style="font-weight: bold; padding-top: 12px;">Carbs:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchByCarbs_Dinner" /> Protein:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchByProtein_Dinner" /> Fat:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchByFat_Dinner" />
        <div style="display: inline-block; cursor: pointer; position: relative; left: -20px; top: 4px;"  onclick="menu_getItemsByMacronutrients($('#searchByCarbs_Dinner').val(),$('#searchByProtein_Dinner').val(),$('#searchByFat_Dinner').val(),'searchByMacronutrients_Dinner_Result',5);"><img src="/images/buttonSearchMag.gif" /></div>
        </div>
        <div id="searchByMacronutrients_Dinner_Result" class="search_results" style="display: none"></div>
    </div>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;display:none; margin-bottom: 10px;padding-bottom: 10px" id="addItemDinner_Recent_Select">
    <select onchange="menu_getRecentByMealtime($('#recentSearchDinner').val(), 'addItemDinner_Recent_Results', 5)" id="recentSearchDinner">
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
    <div style="float:left;cursor: pointer; display: inline" id="Div9"
         onclick="menu_addNewItem($('#AOBrandName_Dinner').val(),
                                  $('#AOFoodName_Dinner').val(),
                                  $('#AOCarbs_Dinner').val(),
                                  $('#AOFat_Dinner').val(),
                                  $('#AOPtn_Dinner').val(),
                                  $('#AOAmount_Dinner').val(),
                                  $('#AOUnit_Dinner').val(),'5');">
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
        <div style="font-weight: bold">Search by Keyword</div>
        <input type="text" id="searchMealByKeyword_Dinner" style="height: 23px; width: 345px; border: 1px solid #999999; margin-top: 11px;padding-left: 35px" onkeyup="show('searchMealByKeyword_Dinner_Result');menu_getMealsByKeyword($('#searchMealByKeyword_Dinner').val(),'searchMealByKeyword_Dinner_Result',5, 'no');" />
        <a style="padding-left: 24px; font-weight: bold; color: #e27424" onclick="hide('tabSearchMealByKeyword_Dinner');show('tabSearchMealByMacronutrients_Dinner');">By Macronutrients ></a>
        <div style="position: absolute; top: 45px; left: 23px; cursor: pointer;"  onclick="show('searchMealByKeyword_Dinner_Result');menu_getMealsByKeyword($('#searchMealByKeyword_Dinner').val(),'searchMealByKeyword_Dinner_Result',5, 'yes');"><img src="/images/buttonSearchMag.gif" /></div>
        <div id="searchMealByKeyword_Dinner_Result" class="search_results" style="display: none"></div>
    </div>

    <div id="tabSearchMealByMacronutrients_Dinner" style="display: none">
        <span style="font-weight: bold">Search by Macronutrients</span>
        <a style="padding-left: 124px; font-weight: bold; color: #e27424" onclick="show('tabSearchMealByKeyword_Dinner');hide('tabSearchMealByMacronutrients_Dinner');">By Keyword ></a>
        <div style="font-weight: bold; padding-top: 12px;">Carbs:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByCarbs_Dinner" /> Protein:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByProtein_Dinner" /> Fat:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByFat_Dinner" />
        <div style="display: inline-block; cursor: pointer; position: relative; left: -20px; top: 4px;"  onclick="menu_getMealsByMacronutrients($('#searchMealByCarbs_Dinner').val(),$('#searchMealByProtein_Dinner').val(),$('#searchMealByFat_Dinner').val(),'searchMealByMacronutrients_Dinner_Result',5);"><img src="/images/buttonSearchMag.gif" /></div>
        </div>
        <div id="searchMealByMacronutrients_Dinner_Result" class="search_results" style="display: none"></div>
    </div>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;display:none; margin-bottom: 10px;padding-bottom: 10px" id="addMealDinner_Recent_Select">
    <select onchange="menu_getRecentMealsByMealtime($('#recentSearchMealDinner').val(), 'addMealDinner_Recent_Results', 5)" id="recentSearchMealDinner">
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
<td style="border-bottom: 1px solid #e5e5e5; padding-top: 15px; padding-bottom: 10px; font-size: 14px; font-weight: bold; padding-left: 11px;" colspan="8" >Supper</td>
</tr>
<asp:Literal ID="literalSupperRows" runat="server"></asp:Literal>
<tr class="buttons mealtime6" id="buttonsSupper">
<td style="padding-top: 5px" colspan="2"><div style="position: relative">
<div style="cursor: pointer; display: inline" id="addItemSupper" onclick="hide_one_of();show('addItemSupper_Select');hide('buttonsSupper');"><img src="/images/buttonAddItem.gif" /></div>
<div style="cursor: pointer; display: inline" id="addMealSupper" onclick="hide_one_of();show('addMealSupper_Select');hide('buttonsSupper');"><img src="/images/buttonAddMeal.gif" /></div>
<div style="cursor: pointer; display: inline" id="quickToolsSupper" onclick="var result = toggle_str('quickToolsSupper_Select');hide_one_of();switchto('quickToolsSupper_Select', result);"><img src="/images/buttonQuickTools.gif" /></div>

<div class="quicktools_options one_of" id="quickToolsSupper_Select">
    <div class="quicktools_option" onclick="hide_one_of();show('quickToolsSupper_SaveAsMeal_Select');">Save as meal</div>
</div>

<div class="one_of" style="z-index: 100; left: 114px; top: 29px; border: 1px solid #a0a0a0; display: none; position: absolute; width: 186px; height: 92px; padding-top: 6px; padding-left: 12px; background-color: #f1f1f1; font-weight: bold" id="quickToolsSupper_SaveAsMeal_Select">
    <p>Name this meal</p>
    <input type="text" id="saveAsMealName_Supper" style="width: 172px; height: 23px; margin-top: 10px; border: 1px solid #999999" />
    <div style="float: left; cursor: pointer; padding-top: 5px;"  onclick="hide('quickToolsSupper_SaveAsMeal_Select');saveMenuMealAsMeal(6,$('#saveAsMealName_Supper').val(),'resultSupper');"><img src="/images/buttonSaveMeal.gif" /></div>
    <div style="float: left; cursor: pointer; padding-top: 10px; padding-left: 21px; color: #e27423"  onclick="hide('quickToolsSupper_SaveAsMeal_Select');">or Cancel</div>
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
    <div class="additem_option additem_option_selected"
         onclick="additem_option_selected(this);
                  show('addItemSupper_Search_Select');
                  hide('addItemSupper_CopyPaste_Select');
                  hide('addItemSupper_Recent_Select');
                  hide('addItemSupper_AddFood_Select');
                  clearRight();">Search & Add</div>
    <div class="additem_option" onclick="additem_option_selected(this);
                                         hide('addItemSupper_Search_Select');
                                         hide('addItemSupper_CopyPaste_Select');
                                         hide('addItemSupper_AddFood_Select');
                                         show('addItemSupper_Recent_Select');">Recent</div>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;margin-bottom: 10px;padding-bottom: 10px" id="addItemSupper_Search_Select">
    <div id="tabSearchByKeyword_Supper">
        <div style="font-weight: bold">
            <span>Search by Keyword</span>
            <a style="padding-left: 24px; font-weight: bold; color: #e27424"
               onclick="hide('tabSearchByKeyword_Supper');
                        show('tabSearchByCategory_Supper');
                        hide('tabSearchByMacronutrients_Supper');">By Category ></a>
            <a style="padding-left: 24px; font-weight: bold; color: #e27424"
               onclick="hide('tabSearchByKeyword_Supper');
                        hide('tabSearchByCategory_Supper');
                        show('tabSearchByMacronutrients_Supper');">By Macronutrients ></a>
        </div>
        <div style="float:left;">
            <input type="text" id="searchByKeyword_Supper" style="height: 23px; width: 345px; border: 1px solid #999999; margin-top: 11px;padding-left: 35px"
                   onkeyup="show('searchByKeyword_Supper_Result');
                            menu_getItemsByKeyword($('#searchByKeyword_Supper').val(),'searchByKeyword_Supper_Result',6, 'no');
                            showRight($(this),$('#rightSupper'));" />    
            <div style="position: absolute; top: 45px; left: 23px; cursor: pointer;"
                 onclick="show('searchByKeyword_Supper_Result');
                          menu_getItemsByKeyword($('#searchByKeyword_Supper').val(),'searchByKeyword_Supper_Result',6, 'yes');">
                <img src="/images/buttonSearchMag.gif" />
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
        <asp:Literal ID="literalSearchByCategory_Supper" runat="server"></asp:Literal>
        <div id="searchByCategory_Supper_Result" class="search_results" style="display: none"></div>
    </div>
    <div id="tabSearchByMacronutrients_Supper" style="display: none">
        <span style="font-weight: bold">Search by Macronutrients</span>
        <a style="padding-left: 124px; font-weight: bold; color: #e27424" onclick="show('tabSearchByKeyword_Supper');hide('tabSearchByCategory_Supper');hide('tabSearchByMacronutrients_Supper');">By Keyword ></a>
        <a style="padding-left: 24px; font-weight: bold; color: #e27424" onclick="hide('tabSearchByKeyword_Supper');show('tabSearchByCategory_Supper');hide('tabSearchByMacronutrients_Supper');">By Category ></a>
        <div style="font-weight: bold; padding-top: 12px;">Carbs:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchByCarbs_Supper" /> Protein:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchByProtein_Supper" /> Fat:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchByFat_Supper" />
        <div style="display: inline-block; cursor: pointer; position: relative; left: -20px; top: 4px;"  onclick="menu_getItemsByMacronutrients($('#searchByCarbs_Supper').val(),$('#searchByProtein_Supper').val(),$('#searchByFat_Supper').val(),'searchByMacronutrients_Supper_Result',6);"><img src="/images/buttonSearchMag.gif" /></div>
        </div>
        <div id="searchByMacronutrients_Supper_Result" class="search_results" style="display: none"></div>
    </div>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;display:none; margin-bottom: 10px;padding-bottom: 10px" id="addItemSupper_Recent_Select">
    <select onchange="menu_getRecentByMealtime($('#recentSearchSupper').val(), 'addItemSupper_Recent_Results', 6)" id="recentSearchSupper">
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
    <div style="float:left;cursor: pointer; display: inline" id="Div10"
         onclick="menu_addNewItem($('#AOBrandName_Supper').val(),
                                  $('#AOFoodName_Supper').val(),
                                  $('#AOCarbs_Supper').val(),
                                  $('#AOFat_Supper').val(),
                                  $('#AOPtn_Supper').val(),
                                  $('#AOAmount_Supper').val(),
                                  $('#AOUnit_Supper').val(),'6');">
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
    <div class="additem_option additem_option_selected"
         onclick="additem_option_selected(this);
                  show('addMealSupper_Search_Select');
                  hide('addMealSupper_Recent_Select');">Search</div>
    <div class="additem_option" onclick="additem_option_selected(this);
                                         hide('addMealSupper_Search_Select');
                                         show('addMealSupper_Recent_Select');">Recent</div>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;margin-bottom: 10px;padding-bottom: 10px" id="addMealSupper_Search_Select">
    <div id="tabSearchMealByKeyword_Supper">
        <div style="font-weight: bold">Search by Keyword</div>
        <input type="text" id="searchMealByKeyword_Supper" style="height: 23px; width: 345px; border: 1px solid #999999; margin-top: 11px;padding-left: 35px" onkeyup="show('searchMealByKeyword_Supper_Result');menu_getMealsByKeyword($('#searchMealByKeyword_Supper').val(),'searchMealByKeyword_Supper_Result',6, 'no');" />
        <a style="padding-left: 24px; font-weight: bold; color: #e27424" onclick="hide('tabSearchMealByKeyword_Supper');show('tabSearchMealByMacronutrients_Supper');">By Macronutrients ></a>
        <div style="position: absolute; top: 45px; left: 23px; cursor: pointer;"  onclick="show('searchMealByKeyword_Supper_Result');menu_getMealsByKeyword($('#searchMealByKeyword_Supper').val(),'searchMealByKeyword_Supper_Result',6, 'yes');"><img src="/images/buttonSearchMag.gif" /></div>
        <div id="searchMealByKeyword_Supper_Result" class="search_results" style="display: none"></div>
    </div>

    <div id="tabSearchMealByMacronutrients_Supper" style="display: none">
        <span style="font-weight: bold">Search by Macronutrients</span>
        <a style="padding-left: 124px; font-weight: bold; color: #e27424" onclick="show('tabSearchMealByKeyword_Supper');hide('tabSearchMealByMacronutrients_Supper');">By Keyword ></a>
        <div style="font-weight: bold; padding-top: 12px;">Carbs:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByCarbs_Supper" /> Protein:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByProtein_Supper" /> Fat:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchMealByFat_Supper" />
        <div style="display: inline-block; cursor: pointer; position: relative; left: -20px; top: 4px;"  onclick="menu_getMealsByMacronutrients($('#searchMealByCarbs_Supper').val(),$('#searchMealByProtein_Supper').val(),$('#searchMealByFat_Supper').val(),'searchMealByMacronutrients_Supper_Result',6);"><img src="/images/buttonSearchMag.gif" /></div>
        </div>
        <div id="searchMealByMacronutrients_Supper_Result" class="search_results" style="display: none"></div>
    </div>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;display:none; margin-bottom: 10px;padding-bottom: 10px" id="addMealSupper_Recent_Select">
    <select onchange="menu_getRecentMealsByMealtime($('#recentSearchMealSupper').val(), 'addMealSupper_Recent_Results', 6)" id="recentSearchMealSupper">
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

    <div class="eContent eOrange" id="tabEdit_Meal" runat="server">
<span id="mealIntroduction" runat="server" clientidmode="Static" Visible="False"></span>
<input type="hidden" id="currentMealId" runat="server" value="" />


<div class="mealMenuHeaderDiv">
    <div id="mealImgDiv" runat="server" class="mealMenuHeaderLefttSide">
        <img id="mealImg" alt="Meal Image" runat="server" src="/images/meals/meal_generic.jpg" width="200" height="200" />
    </div>
    <div id="mealTitleDiv" class="mealMenuHeaderRightSide">
        <div id="mealNameDiv" runat="server" class="mealOrMenuTitle">Meal Name: </div>
        <div id="serveAndMacrodiv" class="mealOrMenuDetail">
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
        <div id="descriptiondiv" class="mealOrMenuDetail">
            <div style="padding-left: 20px;">
                <img src="/images/icons/web/description.png" alt="description" /> Description 
                <img src="/images/icons/web/edit-red.png" id="iconEditDesc" runat="server" Visible="False"  ClientIDMode="Static" onclick="editMealDescription(); return false;" class="editicons" alt="edit description" title="Edit Meal Description"/>
                <div id="mealDescription" runat="server" clientidmode="Static">This is a lovely traditional lemon drizzle cake which tastes wonderfully tangy-sweet. Perfect for a work morning tea.
                    This is a lovely traditional lemon drizzle cake which tastes wonderfully tangy-sweet. Perfect for a work morning tea.
                </div>
            </div>
        </div>
    </div>
</div>
<div class="mealMenuOptionsDiv">
    <div class="mealMenuOptionsWrapper">
        <div id="lnkAddPicture" runat="server" style="border-left: none;" onclick="openMealMenuDialog('mealImageUploadDiv', this.id); return false;">
            <div><img src="/images/icons/web/addphoto.png" alt="picture"/></div>
            <div>Add Picture</div>
        </div>
        <div ID="lnkDL" target="_blank" runat="server">
            <div><img src="/images/icons/web/download.png" alt="picture"/></div>
            <div>Download Recipe</div>
        </div>
        <div ID="lnkUL" runat="server" onclick="openMealMenuDialog('mealRecipeUploadDiv', this.id); return false;">
            <div><img src="/images/icons/web/upload.png" alt="picture"/></div>
            <div>Upload Recipe</div>
        </div>
        <div ID="lnkEditPortion" runat="server" Visible="False" onclick="openMealMenuDialog('editPortionDiv', this.id); return false;">
            <div><img src="/images/icons/web/calculator.png" alt="picture"/></div>
            <div>Change Quantity</div>
        </div>
        <div ID="lnkTV" runat="server">
            <div><img src="/images/icons/web/mediaplay.png" alt="picture"/></div>
            <div>Play Video</div>
        </div>
        <div ID="lnkShare" runat="server" onclick="openMealMenuDialog('shareMealPlan_Select', this.id); return false;">
            <div><img src="/images/icons/web/closedenvelope.png" alt="picture"/></div>
            <div>Share</div>
        </div>
        <asp:Literal runat="server" ID="mealAddToDiary"></asp:Literal>
        <asp:Literal runat="server" ID="saveMealToMyAcc"></asp:Literal>
    </div>
</div>
<div class="menuChosenDiv">
    
    <div id="shareMealPlan_Select" style="display: none;" runat="server" clientidmode="Static" class="one_of">
        <p>Share to?</p>
        <div class="quicktools_option" style="height:25px;text-align:left;vertical-align:middle;" id="shareToFB" runat="server"><img src="/images/fb.small.icon.jpg"/> &nbsp;&nbsp;&nbsp;Facebook</div>
        <div class="quicktools_option" style="height:25px;text-align:left;vertical-align:middle;" id="shareToTwitter" runat="server"><img src="/images/twitter-icon.png"/> &nbsp;&nbsp;&nbsp;Twitter</div>
        <div class="quicktools_option" style="height:25px;text-align:left;vertical-align:middle;" id="shareToTrainerOrClient" runat="server"><img src="/images/trainer.small.icon.jpg"/>&nbsp;&nbsp;&nbsp;My Trainer</div>
        <div class="quicktools_option" style="height:25px;text-align:left;vertical-align:middle;" id="shareToHQ" runat="server"><img src="/images/favicon002.png" height="21" width="21"/>&nbsp;&nbsp;&nbsp;Vision HQ</div>
        <div class="quicktools_option" style="height:25px;text-align:left;vertical-align:middle;" id="shareToEmail" runat="server"><img src="/images/email_icon.png"/>&nbsp;&nbsp;&nbsp;Email</div>
    </div>

    <div style="top: 321px; left: 687px; border: 1px solid #a0a0a0; display: none; position: absolute; width: 186px; height: 64px; padding-top: 6px; padding-left: 12px; background-color: #f1f1f1; font-weight: bold;text-align: left; z-index: 100;" id="mealRecipeOptions" class="one_of" clientidmode="Static">
            <div class="quicktools_option" style="height:25px;text-align:left;vertical-align:middle;" id="downRecipe" runat="server">Download Recipe</div>
            <div class="quicktools_option" style="height:25px;text-align:left;vertical-align:middle;" id="upRecipe" runat="server">Upload Recipe</div>
    </div>

    <div id="editPortionDiv" style="display: none;" class="one_of" clientidmode="Static"> 
            Divide all items in this meal by <input type="number" id="serveByNum" style="width: 50px;"/> 
            <input type="submit" value="Go" onclick="hide_one_of();MealDivideByNumberServing($('#serveByNum').val(), $('#ContentPlaceHolderDefault_ContentPlaceHolderMain_Menus_2_currentMealId').val()); return  false;"/>
    </div>

    <div id="singlePortionDiv" style="display: none;" class="uploaddiv" runat="server" clientidmode="Static">
        <a onclick="$('#singlePortionDiv').slideToggle();" style="cursor: pointer;position: absolute;right: 183px;"><img src="/images/delete.gif" border="0"></a>
        To ensure accuracy when adding meals to your food diary and for your convenience you are able to use the Edit Portion button to divide the full list of ingredients into the amount of portions the recipe makes.
        When you add the meal to your food diary, only the single portion macronutrient amount will be added.
       </div>

    <div id="mealImageUploadDiv" style="display: none;" class="uploaddiv">
        <asp:FileUpload ID="mealImageFileUpload" runat="server" /> 
        <asp:Button ID="Button1" runat="server" Text="Upload Photo" OnClick="UploadMealImage" ValidationGroup="photo" />
                <br/>    
        <i>Picture type must be .jpg and the size must be bigger than 250px width and height</i>
        <br/>
        <asp:RequiredFieldValidator ID="mealImageRequiredFieldValidator" runat="server" ValidationGroup="photo" ErrorMessage="" ControlToValidate="mealImageFileUpload"></asp:RequiredFieldValidator>
    </div>

    <div id="mealRecipeUploadDiv" style="display: none;" class="uploaddiv" clientidmode="Static">
        <asp:FileUpload ID="mealRecipeFileUpload" runat="server" /> 
        <asp:Button ID="Button2" runat="server" Text="Upload Recipe" OnClick="UploadMealRecipe" ValidationGroup="recipe" />
        <br/>
        <i>Accepts only .pdf, .doc, .docx, .txt and size no bigger than 500 KB</i>
        <br/>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="recipe" ErrorMessage="" ControlToValidate="mealRecipeFileUpload"></asp:RequiredFieldValidator>
    </div>
    
    <asp:Literal ID="literalMealAddToDiaryDiv" runat="server"></asp:Literal>

</div>

<div style="clear:both"></div>

<div style="clear:both"></div>
<br />
<br />
<asp:Panel runat="server" ID="panel_admin">
<div style="font-weight: bold; font-size: 14px;float: left; padding-top: 4px; padding-right: 10px">Video Id: </div>
<input type="text" id="videoId" runat="server" style="width: 200px; height: 21px; border: 1px solid #999999; float: left" onblur="meal_editVideoId(this.value);" />
<input type="checkbox" runat="server" id="checkboxIsRecommendedMeal" style="float: left" onchange="meal_editIsRecommended($(this));" /><div style="font-weight: bold; font-size: 14px;float: left; padding-top: 4px; padding-right: 10px">Publish to Vision Meal?</div>
<div style="clear:both"></div>
Note: Image from Video is used if Video Id is set.
<br /><br />
</asp:Panel>
<div class="result" id="resultPage_Meal" style="display: none"></div>
<table width="885" border="0" cellpadding="0" cellspacing="0">
<tr>
<td style="border-top: 1px solid #ffffff; padding-top: 15px; padding-bottom: 15px; font-size: 12px; font-weight: bold; text-align: center;" width="185">&nbsp;</td>
<td style="border-top: 1px solid #ffffff; padding-top: 15px; padding-bottom: 15px; font-size: 12px; font-weight: bold; text-align: center;" width="184">&nbsp;</td>
<td style="border-top: 1px solid #ffffff; padding-top: 15px; padding-bottom: 15px; font-size: 12px; font-weight: bold; text-align: center;" width="100">Amount</td>
<td style="border-top: 1px solid #ffffff; padding-top: 15px; padding-bottom: 15px; font-size: 12px; font-weight: bold; text-align: center;" width="100">Serve</td>
<td style="border-top: 1px solid #ffffff; padding-top: 15px; padding-bottom: 15px; font-size: 12px; font-weight: bold; text-align: center;" width="100">Carbohydrate(g)</td>
<td style="border-top: 1px solid #ffffff; padding-top: 15px; padding-bottom: 15px; font-size: 12px; font-weight: bold; text-align: center;" width="100">Protein(g)</td>
<td style="border-top: 1px solid #ffffff; padding-top: 15px; padding-bottom: 15px; font-size: 12px; font-weight: bold; text-align: center;" width="100">Fat(g)</td>
<td style="border-top: 1px solid #ffffff; padding-top: 15px; padding-bottom: 15px; font-size: 12px; font-weight: bold; text-align: center;" width="16">&nbsp;</td>
</tr>
<asp:Literal ID="literalMealRows" runat="server"></asp:Literal>
<tr class="buttons" id="buttonsMeal">
<td style="padding-top: 5px" colspan="2">
<div style="position: relative">
    <div style="cursor: pointer; display: inline" id="addItemMeal" onclick="hide_one_of();show('addItemMeal_Select');hide('buttonsMeal');">
        <img src="/images/buttonAddItem.gif" />
    </div>
</div>
</td>
<td>&nbsp;</td>
<td>&nbsp;</td>
<td>&nbsp;</td>
<td>&nbsp;</td>
<td>&nbsp;</td>
<td>&nbsp;</td>
</tr>
<tr><td colspan="8">
<div style="border: 1px solid #d1d1d1; display: none; width: 883px; background-color: #ececec" id="addItemMeal_Select" class="one_of">
<div style="cursor: pointer; display: inline" id="addItemMealSelected" onclick="hide('addItemMeal_Select');show('buttonsMeal');">
    <img src="/images/buttonAddItemSelected.gif" />
</div>
<div class="additem_options">
    <div class="additem_option additem_option_selected"
         onclick="additem_option_selected(this);
                  show('addItemMeal_Search_Select');
                  hide('addItemMeal_CopyPaste_Select');
                  hide('addItemMeal_Recent_Select');
                  hide('addItemMeal_AddFood_Select');">Search & Add</div>
    <div class="additem_option" onclick="additem_option_selected(this);
                                         hide('addItemMeal_Search_Select');
                                         hide('addItemMeal_CopyPaste_Select');
                                         show('addItemMeal_Recent_Select');
                                         hide('addItemMeal_AddFood_Select');">Recent</div>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;margin-bottom: 10px;padding-bottom: 10px" id="addItemMeal_Search_Select">
    <div id="tabSearchByKeyword_Meal">
        <div style="font-weight: bold">
            <span>Search by Keyword</span>
            <a style="padding-left: 24px; font-weight: bold; color: #e27424"
               onclick="hide('tabSearchByKeyword_Meal');
                        show('tabSearchByCategory_Meal');
                        hide('tabSearchByMacronutrients_Meal');
                        ">By Category ></a>
            <a style="padding-left: 24px; font-weight: bold; color: #e27424"
               onclick="hide('tabSearchByKeyword_Meal');
                        hide('tabSearchByCategory_Meal');
                        show('tabSearchByMacronutrients_Meal');">By Macronutrients ></a>
        </div>
        <div style="float:left;">
            <input type="text" id="searchByKeyword_Meal" style="height: 23px; width: 345px; border: 1px solid #999999; margin-top: 11px;padding-left: 35px"
                   onkeyup="show('searchByKeyword_Meal_Result');
                            meal_getItemsByKeyword($('#searchByKeyword_Meal').val(),'searchByKeyword_Meal_Result', 'no');
                            showRight($(this),$('#rightMeal'));" /> 
            <div style="position: absolute; top: 45px; left: 23px; cursor: pointer;"
                 onclick="show('searchByKeyword_Meal_Result');
                          meal_getItemsByKeyword($('#searchByKeyword_Meal').val(),'searchByKeyword_Meal_Result', 'yes');">
                <img src="/images/buttonSearchMag.gif" />
            </div>
        </div>
        <div class="rightSide" id="rightMeal"><!--button click takes to add new item panel-->
            <span style="display:inline-block;vertical-align:super;">Still can't find what you are looking for? Add new items</span>
            <input type="button" class="imgButtonAddItem" onclick="hide('addItemMeal_Search_Select');
                                                                   hide('addItemMeal_CopyPaste_Select');
                                                                   hide('addItemMeal_Recent_Select');
                                                                   show('addItemMeal_AddFood_Select');
                                                                   $('#AOFoodName_Meal').val($('#searchByKeyword_Meal').val());
                                                                   $('.search_results').hide();"/>
        </div>
        <div style="clear:both;"></div>
        <div id="searchByKeyword_Meal_Result" class="search_results" style="display: none"></div>
    </div>
    <div id="tabSearchByCategory_Meal" style="display: none">
        <span style="font-weight: bold">Search by Category</span>
        <a style="padding-left: 124px; font-weight: bold; color: #e27424" onclick="show('tabSearchByKeyword_Meal');hide('tabSearchByCategory_Meal');hide('tabSearchByMacronutrients_Meal');">By Keyword ></a>
        <a style="padding-left: 24px; font-weight: bold; color: #e27424" onclick="hide('tabSearchByKeyword_Meal');hide('tabSearchByCategory_Meal');show('tabSearchByMacronutrients_Meal');">By Macronutrients ></a>
        <asp:Literal ID="literalSearchByCategory_Meal" runat="server"></asp:Literal>
        <div id="searchByCategory_Meal_Result" class="search_results" style="display: none"></div>
    </div>
    <div id="tabSearchByMacronutrients_Meal" style="display: none">
        <span style="font-weight: bold">Search by Macronutrients</span>
        <a style="padding-left: 124px; font-weight: bold; color: #e27424" onclick="show('tabSearchByKeyword_Meal');hide('tabSearchByCategory_Meal');hide('tabSearchByMacronutrients_Meal');">By Keyword ></a>
        <a style="padding-left: 24px; font-weight: bold; color: #e27424" onclick="hide('tabSearchByKeyword_Meal');show('tabSearchByCategory_Meal');hide('tabSearchByMacronutrients_Meal');">By Category ></a>
        <div style="font-weight: bold; padding-top: 12px;">Carbs:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchByCarbs_Meal" /> Protein:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchByProtein_Meal" /> Fat:<input style="margin: 0 24px 0 6px; width: 48px; height: 23px; border: 1px solid #a0a0a0" type="text" id="searchByFat_Meal" />
        <div style="display: inline-block; cursor: pointer; position: relative; left: -20px; top: 4px;"  onclick="meal_getItemsByMacronutrients($('#searchByCarbs_Meal').val(),$('#searchByProtein_Meal').val(),$('#searchByFat_Meal').val(),'searchByMacronutrients_Meal_Result');"><img src="/images/buttonSearchMag.gif" /></div>
        </div>
        <div id="searchByMacronutrients_Meal_Result" class="search_results" style="display: none"></div>
    </div>
</div>

<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;display:none; margin-bottom: 10px;padding-bottom: 10px" id="addItemMeal_Recent_Select">
    <select onchange="meal_getRecentByMealtime($('#recentSearchMeal').val(), 'addItemMeal_Recent_Results')" id="recentSearchMeal">
        <option value="0"></option>
        <option value="1">Breakfast</option>
        <option value="2">Morning Tea</option>
        <option value="3">Lunch</option>
        <option value="4">Afternoon Tea</option>
        <option value="5">Dinner</option>
        <option value="6">Supper</option>
        <option value="-1">All Meals</option>
    </select>
    <div id="addItemMeal_Recent_Results" class="search_results_recent"></div>
    </div>

<!--------------------------------------------->
<div style="border-left: 1px solid #cccccc;border-right: 1px solid #cccccc;border-bottom: 1px solid #cccccc; width: 841px; background-color: #ffffff; margin-left: 12px;padding-left: 20px; padding-top: 16px; position: relative;display:none; margin-bottom: 10px;padding-bottom: 10px" id="addItemMeal_AddFood_Select">
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
            <td><input type="text" id="AOFoodName_Meal" style="width: 180px" disabled/></td>
            <td style="padding-right: 10px"><input type="text" id="AOBrandName_Meal" placeholder="optional"/></td>
            <td style="padding: 0px 25px"><input type="text" id="AOAmount_Meal" style="width: 48px"/></td>
            <td style="padding: 0px 21px"><input type="text" id="AOUnit_Meal" style="width: 50px" /></td>
            <td style="padding-left: 4px; padding-right: 20px; "><input type="text" id="AOCarbs_Meal" style="width: 50px;margin-left: 10px"/></td>
            <td><input type="text" id="AOPtn_Meal" style="width: 50px"/></td>
            <td style="padding-left: 25px "><input type="text" id="AOFat_Meal" style="width: 50px"/></td>
        </tr>
    </table>
    <i>Please note: Amount, Carbs, Protein and Fat entries are numerical only</i>
    <br/>
    <div style="float:left;cursor: pointer; display: inline" id="Div12"
         onclick="meal_addNewItem($('#AOBrandName_Meal').val(),
                                  $('#AOFoodName_Meal').val(),
                                  $('#AOCarbs_Meal').val(),
                                  $('#AOPtn_Meal').val(),
                                  $('#AOFat_Meal').val(),
                                  $('#AOAmount_Meal').val(),
                                  $('#AOUnit_Meal').val())">
        <br/>
        <img src="/images/buttonSubmitOrange.gif" alt="Add Item" />
    </div>
    <div class="cancel" style="float:left;">
        <br />
        <img src="/images/buttonCancel.gif" alt="Cancel" />
    </div>
    <div style="clear:both;"></div>
</div>
<!----------------------------------------------->


</div>
<div class="result" id="resultMeal" style="display: none;"></div>
</td></tr>

<tr>
<td style="border-bottom: 1px solid #e5e5e5; border-top: 1px solid #e5e5e5; padding-top: 15px; padding-bottom: 10px; font-size: 14px; font-weight: bold; padding-left: 11px;" colspan="8">Macronutrients Total</td>
</tr>
<tr>
<td style="padding-top: 15px;padding-left: 15px;" colspan="4">Per Recipe</td>
<td style="padding-top: 15px;padding-left: 15px;text-align: center;font-weight: bold" id="carb-total"><asp:Literal ID="literalMealCarb" runat="server"></asp:Literal></td>
<td style="padding-top: 15px;padding-left: 15px;text-align: center;font-weight: bold" id="protein-total"><asp:Literal ID="literalMealProtein" runat="server"></asp:Literal></td>
<td style="padding-top: 15px;padding-left: 15px;text-align: center;font-weight: bold" id="fat-total"><asp:Literal ID="literalMealFat" runat="server"></asp:Literal></td>
<td style="padding-top: 15px;padding-left: 15px;text-align: center;font-weight: bold">&nbsp;</td>
</tr>

</table>

<div style="float: left;display: block;width: 515px;padding-left: 15px;font-size: 14px; color: #e27424;padding-top: 10px;" id="singlePortionTitle" runat="server" Visible="False">
    
</div>
    <div class="clear">&nbsp;</div>    
</div>
</div>

<script type="text/javascript">
    window.onclick = function() {
        if ($(".search_results").is(':visible')) {
            $(".search_results").slideUp('fast');
        }
    };
    $(document).ready(function () {
        <asp:Literal runat="server" id="menuScript"></asp:Literal>
        
        menuPageLoadingScripts();
    });

    $(document).ajaxStop(function () {
        alert('lalala')
        // debugger;
        $(".datepicker").datepicker(
            {
                dateFormat: "mm/dd/yy",
                constrainInput: true
            });
    });
</script>

<asp:Literal ID="menusloadingscript" runat="server"></asp:Literal>

