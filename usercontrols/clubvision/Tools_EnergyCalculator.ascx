<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Tools_EnergyCalculator.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.clubvision.Tools_EnergyCalculator" %>
<%@ Register TagPrefix="BDP" Namespace="BasicFrame.WebControls" Assembly="BasicFrame.WebControls.BasicDatePicker, Version=1.4.1.41500, Culture=neutral, PublicKeyToken=e1cce521aa9b4849" %>
<style>
    #ContentPlaceHolderDefault_help {
        display: none;
    }
</style>


<div class="pTrainingDiary" id="weightSessDiv" style="margin-right: 10px !important;padding:0px;height: 300px;" runat="server" ClientIDMode="Static">
    <!--<div style="font-weight: bold; font-size: 14px;float: left; padding-top: 4px; padding-right: 10px;width: 100%; display: inline-block;height: 40px;">
       
    </div>    --> 
    <h2 id="evseTitle">Eating vs Exercise Calculator</h2><br/>
    
    <div id="explanationdiv" style="display: none;font-size: 14px;float: left; padding-top: 4px; padding-right: 10px;width: 100%; height: 40px;" >
        Check how much you need to exercise to burn off the calories with your weight of 
        <span id="weight"><asp:Label ID="weightLabel" runat="server" Text="Label"></asp:Label></span> kg!<br/>

        <asp:Label ID="closestWeightLabel" runat="server" Text="Label" ClientIDMode="Static"></asp:Label>
        <asp:Label ID="exerciseOptionLabel" runat="server" Text="Label" ClientIDMode="Static"></asp:Label>
        <asp:Label ID="walkingKjLabel" runat="server" Text="Label"  ClientIDMode="Static"></asp:Label>
    </div> 

    
    <div id="Div1" clientidmode="Static" style="display: none;">
        <a style="cursor: pointer; position: absolute; top: 230px; left: 40px;">
            <img id="captainImage" src="/images/vpt_captainaccountabilityV2.jpg" alt="captain" /> 
        </a>
    </div>
    
    <div id="energycalcsteps">
        <div id="energycalcstepsleft" >
            <span id="evseSpanTitle1">1</span> Select your food
        </div>
         <div id="energycalcstepsright">
            <span>2</span> Review the results down here!
        </div>
    </div>
    

    <div style="float: left; padding-top: 4px; padding-right: 21px;width: 48%; display: inline-block;height:350px;margin-left: -21px;">

        <div class="additem_options" style="width: 100%">
            <div class="additem_option additem_option_selected" onclick="additem_option_selected(this);
                                                                        show('addItemMorningTea_Search_Select');
                                                                        hide('addItemMorningTea_CopyPaste_Select');
                                                                        hide('addItemMorningTea_Recent_Select');
                                                                        hide('addItemMorningTea_AddFood_Select');
                                                                        hide('addItemAlcoholicDrinks');
                                                                        clearRight();">Search by Keyword</div>

            <div class="additem_option" onclick="additem_option_selected(this);
                                                 hide('addItemMorningTea_Search_Select');
                                                 show('addItemMorningTea_CopyPaste_Select');
                                                 hide('addItemMorningTea_Recent_Select');
                                                 hide('addItemMorningTea_AddFood_Select');
                                                 hide('addItemAlcoholicDrinks');">Search by Category</div>
                                                 
            <div class="additem_option" id="alcoholtab" onclick="additem_option_selected(this);
                                                 hide('addItemMorningTea_Search_Select');
                                                 hide('addItemMorningTea_CopyPaste_Select');
                                                 hide('addItemMorningTea_Recent_Select');
                                                 hide('addItemMorningTea_AddFood_Select');
                                                 show('addItemAlcoholicDrinks');
                                                 $('#eWhatsOnMainWithNews').height('auto');
                                                 $('.pTrainingDiary').height('730px');">Alcoholic Drinks</div>
    
        </div>

        <div id="addItemMorningTea_Search_Select" style="border: 1px solid rgb(204, 204, 204); margin-left: 12px; padding-left: 20px; padding-top: 16px; position: relative; margin-bottom: 10px; padding-bottom: 10px; display: block;" >
            <div id="tabSearchByKeyword_MorningTea" style="display: block;">
                <div style="float:left;">
                    <input type="text" id="searchByKeyword_MorningTea" style="height: 23px; width: 345px; border: 1px solid #999999; margin-top: 11px;padding-left: 35px" onkeyup="show('searchByKeyword_MorningTea_Result');
                                    getItemsByKeywordForTools($('#searchByKeyword_MorningTea').val(),'searchByKeyword_MorningTea_Result',2, 'no');
                                    showRight($(this),$('#rightMorningTea'));">
                    <div style="position: absolute; top: 30px; left: 23px; cursor: pointer;" onclick="show('searchByKeyword_MorningTea_Result');
                                  getItemsByKeywordForTools($('#searchByKeyword_MorningTea').val(),'searchByKeyword_MorningTea_Result',2, 'yes');"><img src="/images/red-search-icon.png"></div>
                </div>
                <div style="clear:both;"></div>
                <div id="searchByKeyword_MorningTea_Result" class="search_results" style="display: none; width: 95%;"></div>
            </div>
        </div>

        <div style="border: 1px solid rgb(204, 204, 204); width: 95%; background-color: rgb(255, 255, 255); margin-left: 12px; padding-left: 20px; padding-top: 16px; position: relative; margin-bottom: 10px; padding-bottom: 10px; display: none;" id="addItemMorningTea_CopyPaste_Select">
            <div id="tabSearchByCategory_MorningTea">
                <asp:Literal ID="literalSearchByCategory_MorningTea" runat="server"></asp:Literal>
                <div id="searchByCategory_MorningTea_Result" class="search_results" style="display: none; width: 95%; max-height: 412px;"></div>
            </div>
        </div>
        
        <div id="addItemAlcoholicDrinks" style="border: 1px solid rgb(204, 204, 204); margin-left: 12px; padding-left: 20px; padding-top: 16px; 
            position: relative; margin-bottom: 10px; padding-bottom: 10px; display: none;height: 560px;">
            <asp:Literal ID="LiteralAlcohol" runat="server" Text="You should see a message"></asp:Literal>
        </div>
    </div>
   
    <div id="captainDiv">
        <div id="captainIcon" style="width: 100%; display: none;">
            <a title="this is the tip that captain will give it to ya" style="cursor: pointer;">
            <img src="/images/vpt_captainaccountabilityV2.jpg" alt="captain" /> </a>
        </div>
    </div>
 
    <br/><br />
</div>