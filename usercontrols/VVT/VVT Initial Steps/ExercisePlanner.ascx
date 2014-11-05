<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExercisePlanner.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.VVT.VVT_Initial_Steps.ExercisePlanner" %>
<%@ Register TagPrefix="ddlb" Assembly="OptionDropDownList" Namespace="OptionDropDownList" %>

<script type="text/javascript" language="javascript">

    $(document).ready(function () {

        $('#exWeekPlan>tbody>tr>td:nth-child(2)').addClass('exSelects');

        $('#explanoptions select').unwrap();

        $('#explanoptions select').css("opacity", 100).css("height", 59).css("width", 91);

        $('#ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_fatlossintermediate select').change(function () {
            var str = "";
            var count = 0;
            // $(this).find('option:selected').text();
            $(this).find('option:selected').each(function () {
                str += $(this).text() + " ";
                //alert(str);
                count++;
            });
            //$("div").text(str);
            if (count > 2) {
                alert('cannot select more than 2 exercises');
                this.selectedIndex = -1;
            }
        })
        .change();
        /*
        $("#explannermessage").click(function () {
        $(".target").slideToggle('slow', function () {

        });
        return false;
        });
        
        $(".reviewSuggestedPlans").click(function () {
        $('#DropDownListPrefProg').val('0').change();
        $("#exercisePlannerDiv").slideToggle('slow', function () { });
        $("#reviewWorkOutDiv").slideToggle('slow', function () { });
        return false;
        });

        $("#DropDownListPrefProg").change(function () {
        var val = $(this).val();

        switch (val) {
        case "1":
        $("#fatlossbeginner").slideToggle('slow', function () { });
        $("#reviewWorkOutDiv").slideToggle('slow', function () { });
        break;
        case "2":
        $("#fatlossintermediate").slideToggle('slow', function () { });
        $("#reviewWorkOutDiv").slideToggle('slow', function () { });
        break;
        case "3":
        $("#fatlossadvance").slideToggle('slow', function () { });
        $("#reviewWorkOutDiv").slideToggle('slow', function () { });
        break;
        default:
        break;
        }

        });
        */

        $(".gotoReviewScreen").click(function () {
            $('#DropDownListPrefProg').val('0').change();
            $(this).parent().slideToggle('slow', function () { });
            $("#reviewWorkOutDiv").slideToggle('slow', function () { });

            return false;
        });



    });
    
    
</script>
<style>
    #FLBeginnerExs select{
        width: 150px !important;
    }
    
    #FLIntermediateExs select{
        width: 150px !important;
    }
    
    #reviewWorkOutDiv div.cmf-skinned-select {
        width: 156px !important;
        height: 21px !important;
    }

    #reviewWorkOutDiv div.cmf-skinned-text {
        width: 126px !important;
        height: 21px !important;
    }
    
 
</style>

<div id="exercisePlannerDiv" class="trainingplan" style="margin-right: 10px !important;">
    <span style="display: none;" id="currentWeekNum" runat="server" ClientIDMode="Static"></span>
    
    <div class="target" style="display: block;float: left; ">
        <br/>
        <div>
            <p class="introTitle">Firstly – Creating your Weights Program</p> <br/>
            <p>The foundation of any exercise program for weight loss is your Weights or Resistance Training Program. By clicking “Select your Weight Training Program” we will create a plan that suits your level. It is recommended that you spread your weights sessions over the week for recovery purposes. You may change the days that we suggest that you perform your Weights Sessions by simply clicking on the drop down boxes for each category and scroll through the list of items to make your selection.</p> <br/>
                
        </div><br/>

        <div class="selectTrainingWrapper" style="margin: 0 auto;">
            <div style="float: left;width: 55px;display: block;"><img src="/images/icons/web/point-right.png" width="50px;"/></div>
            <div class="reviewSuggestedPlans button-small vision_red rounded3" onclick="$('#exercisePlannerDiv').slideToggle('slow', function () { });$('#reviewWorkOutDiv').slideToggle('slow', function () { });return false;" id="divSelectProgram" runat="server" clientidmode="Static" >
                    <h3>Select your Weight Training Program</h3>
            </div>
            <div id="divSelectedProgram" runat="server" clientidmode="Static" style="display: none;">
                <table>
                    <tr>
                        <td colspan="2" style="height: 50px;">
                            <span id="chosenProgram" runat="server"></span>
                        </td>
                    </tr>
                    <tr>
                        <td onclick="" id="reviewProgram" runat="server" class="button-small vision_red rounded3" style="margin-right: 15px;">
                            Review Your Program
                        </td>
                        <td onclick="clearSelectedProgramAlert();" class="button-small vision_red rounded3">
                            Clear Selection
                        </td>
                    </tr>
                </table>
            </div>
            <div style="float: left;width: 55px;display: none;"><img src="/images/icons/web/point-left.png" width="50px;"/></div>
        </div>

        <br/>
    </div>
       

   <br/><br/><br/>
   
   <div class="target" style="display: block; float: left;">
       <br/>
           <div>
              <p class="introTitle">Next – Add Cardio</p> <br/>
               <p>To complete your exercise planner you need to add cardio exercises. Simply click on the drop down boxes on each and scroll through the list of cardio exercises that you wish to do to make your selection. On days that you do not exercise, simply  select “Rest Day”. It is advisable to plan low to moderate intensity cardio sessions on the day/s following a weights session to allow for muscles to recover.</p> <br/>
                
            </div><br/>
   </div>
           
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            
            <table id="exWeekPlan" style="margin: 0px auto;">
                <tr style="font-weight: bold;">
                    <td>Weekday</td>
                    <td></td>
                    <td>Exercise</td>
                    <td>When</td>
                    <td>Intensity</td>
                    <td>Duration (mins)</td>
                    <td></td>
                </tr>
                <asp:Literal ID="LitExPlanner" runat="server"></asp:Literal>
            </table>
        
            <div class="selectedProg" style="font-weight: bold;padding-top: 20px;"></div> 
            
            <br/>
            <div class="target" style="display: block; float: left;">
                   <br/>
                       <div>
                          <p class="introTitle">Finally – Check that your created program meets your weekly requirements</p> <br/>
                           <p>Note that only entries completed in full will be included and appear in your final Training Diary. Your goal is to ensure that green 'YES' appears at the end of each row in the Exercise Summary below.</p> <br/>
                
                        </div><br/>
               </div>
            <br/><br/>
            <br/>
                
            <asp:Label ID="Label1" runat="server" Text="" Visible="False"></asp:Label>
            <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
            
            <br/><br/>
            <div id="WEPSummary" runat="server" class="WeeklySummary">
                <h3 style="margin-bottom: 10px;margin-top: 10px;">Exercise Summary</h3>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            Actual total cardio
                        </td>
                        <td>
                            <asp:TextBox ID="ActualTotalCardioTextBox" runat="server" Width="40px" ReadOnly="True" ClientIDMode="Static"></asp:TextBox>
                        </td>
                        <td>
                            Total cardio required
                        </td>
                        <td>
                            <asp:TextBox ID="TotalCardioReqTextBox" runat="server" Width="40px" ReadOnly="True" ></asp:TextBox>
                        </td>
                        <td>
                            Total cardio achieved
                            </td>
                        <td>
                            <asp:TextBox ID="TotalCardioAchievedTextBox" runat="server" Width="40px" ReadOnly="True" ClientIDMode="Static"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Actual Hard Cardio
                        </td>
                        <td>
                            <asp:TextBox ID="ActualHardCardioTextBox" runat="server" Width="40px" ReadOnly="True" ClientIDMode="Static"></asp:TextBox>
                        </td>
                        <td>
                            Hard Cardio Required
                        </td>
                        <td>
                            <asp:TextBox ID="HardCardioReqTextBox" runat="server" Width="40px" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td>
                            Hard Cardio Achieved
                        </td>
                        <td>
                            <asp:TextBox ID="HardCardioAchievedTextBox" runat="server" Width="40px" ReadOnly="True" ClientIDMode="Static"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Actual L-M Cardio
                        </td>
                        <td>
                            <asp:TextBox ID="ActualLMCardioTextBox" runat="server" Width="40px" ReadOnly="True" ClientIDMode="Static"></asp:TextBox>
                        </td>
                        <td>
                            L-M Cardio Required
                        </td>
                        <td>
                            <asp:TextBox ID="LMCardioReqTextBox" runat="server" Width="40px" ReadOnly="True"  ClientIDMode="Static"></asp:TextBox>
                        </td>
                        <td>
                            L-M Cardio Achieved
                        </td>
                        <td>
                            <asp:TextBox ID="LMCardioAchievedTextBox" runat="server" Width="40px" ReadOnly="True" ClientIDMode="Static"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Actual Weights
                        </td>
                        <td>
                            <asp:TextBox ID="ActualWeightsTextBox" runat="server" Width="40px" ReadOnly="True" ClientIDMode="Static"></asp:TextBox>
                        </td>
                        <td>
                            Weights Required
                        </td>
                        <td>
                            <asp:TextBox ID="WeightsReqTextBox" runat="server" Width="40px" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td>
                            Weights Achieved
                        </td>
                        <td>
                            <asp:TextBox ID="WeightsPtAchievedTextBox" runat="server" Width="40px" ReadOnly="True" ClientIDMode="Static"></asp:TextBox>
                        </td>
                    </tr>
                    
                </table>
                <br/><br/>
            </div>
            
            <div id="messageboard" runat="server" style="display: none;">
                <h2 style="color: #CD0921;">Please Note</h2>
                
                <p id="p1" runat="server">You have not selected any exercises</p>
                <p id="p2" runat="server">
                   We want to ensure you give yourself every chance of achieving your weight loss goal within the next 9 weeks and the recommendations for the cardio and weights requirements are based on your goals set.  
                   Should you feel that you are not able to meet the activity levels recommended then we strongly recommend you readjust your goal.  
                   By making smaller, more manageable changes that integrate easily into your current lifestyle you can be confident of staying on track with your weight loss goals.
                
                </p>
            </div>
            <br/><br/>
            <div class="realButtonDiv" style="width: auto; height: 50px;display: none;">
                <div style="float: left">
                    <asp:imagebutton ID="MyGoalImagebuttonBack" ImageUrl="/images/buttonBack.gif" 
                        runat="server" OnClick="MyGoalImagebuttonBackClick" ></asp:imagebutton>
                </div>
    
                <div style="float: right">
                    <asp:imagebutton ID="MyGoalImagebuttonNext" ImageUrl="/images/buttonSaveAndNext.gif" 
                        runat="server"
                        onclick="MyGoalImagebuttonNextClick"></asp:imagebutton>
                </div>
            </div>
            
            <div class="istepsDiv">
                <div class="istepsWrapper">
                    <div style="border-left: none;" onclick="window.open('/club-vision/account-setup/my-goals/', '_self');">
                        <div><img src="/images/icons/web/prevarrow.png" alt="picture"/></div>
                        <div>Prev</div>
                    </div>
                    <div onclick="skipInitialSteps(5);return false;">
                        <div><img src="/images/icons/web/skip.png" alt="picture"/></div>
                        <div>Skip</div>
                    </div>
                    <div id="nextDiv" onclick="$('#ContentPlaceHolderDefault_ContentPlaceHolderMain_ctl01_ExercisePlanner_3_MyGoalImagebuttonNext').click();return false;">
                        <div><img src="/images/icons/web/nextarrow.png" alt="picture"/></div>
                        <div>Next</div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
      
    </asp:UpdatePanel>
    
    
</div>

<div id="reviewWorkOutDiv" class="trainingplan" style="margin-right: 10px !important;display: none;">
   <p>We can help you get started with a suggested exercise program that is designed to give you a full body workout that you can do 
    either outside or in the comfort of your own home.
    </p>
    <br/>
    <p>
        To view just click on your preferred program from the dropdown box  
    </p>
    <select id="DropDownListPrefProg" class="tprow1" style="width: 154px;" onchange="DropDownListPrefProgFunc($(this).val())" >
        <option value="0"></option>
        <option value="1">Weights Beginner</option>
        <option value="2">Weights Intermediate</option>
        <option value="3">Weights Advanced</option>
    </select>
    <div class="selectedProg" style="font-weight: bold;padding-top: 20px;"></div>   
    <br/><br/>
    <a href="#" style="margin-left: 1px;"
class="button-small grey_dark_reverse rounded3" onclick="$('#DropDownListPrefProg').val('0').change();$('#exercisePlannerDiv').slideToggle('slow', function () { });$('#reviewWorkOutDiv').slideToggle('slow', function () { });return false;">Close</a>

</div>

<div id="explanoptions">
    
<div id="fatlossbeginner" class="trainingplan" style="margin-right: 10px !important;display: none;" runat="server" ClientIDMode="Static">
    <p><h3>Weights - Beginner</h3></p>
    <p>This program is suitable for those who are brand new to exercise or those who have not exercised for a period of time.
    For more information in each exercise click the relevant video icon
    </p><br/>
    <p>To create your program:
    <ul>
    <li>Select one exercise from each of the body parts listed</li>
    <li>Aim to complete 15 repetitions of each exercise</li>
    <li>Aim to do 1 to 2 sets of each exercise</li>
    <li>Rest between each exercise for up to 60 seconds</li>
    <li>Do the required number of sets for each exercise before moving onto the next exercise</li>
    </ul>
    </p><br/>
    <p style="font-style: italic;">
        In this program you should be aiming for an intensity level of 2.  Please refer to the intensity guide below for how this should feel.
If you find you can do more than the recommended 15 repetitions, simply do more until you reach the correct intensity level.

    </p>
    <br/><br/>
    
    <table>
        <tr>
            <td>Level</td>
            <td>Intensity</td>
            <td>Description</td>
        </tr>
        <tr>
            <td>1</td>
            <td>Light</td>
            <td>You are able to complete each exercise with little effort</td>
        </tr>
        <tr>
            <td>2</td>
            <td>Moderate</td>
            <td>You can complete each exercise comfortably</td>
        </tr>
        <tr>
            <td>3</td>
            <td>Hard</td>
            <td>Each working weight set is completed once you cannot do anymore by yourself</td>
        </tr>
    </table>
    <br/><br/>
   
    <h3 class="introTitle">Select Exercise</h3>
    <table id="FLBeginnerExs">
        <tr>
            <td>Area</td>
            <td>Exercise</td>
        </tr>
        <tr>
            <td>Legs</td>
            <td><div style="float: left; width: 50px; height: 60px;display: block;">
                    <img src="/images/Video-icon-orange.png" width="16" title="Click here to see the video" onclick="lungesVid();"/><br/>
                    <img src="/images/Video-icon-orange.png" width="16" title="Click here to see the video" onclick="squatsVid();"/><br/>
                    <img src="/images/Video-icon-orange.png" width="16" title="Click here to see the video" onclick="stepUpsVid();"/>
                </div>
                <div style="float: left; width: 150px; height: 60px !important;display: block;">
                    <asp:ListBox ID="exListBoxLegBeginner" runat="server" SelectionMode="Single" BodyPart="1"></asp:ListBox>  
                </div>
            </td>
            
        </tr>
        <tr>
            <td>Upper - Push</td>
            <td>
                <div style="float: left; width: 50px; height: 60px;display: block;">
                    <img src="/images/Video-icon-orange.png" width="16" title="Click here to see the video" onclick="pushUpsVids();"/><br/>
                    <img src="/images/Video-icon-orange.png" width="16" title="Click here to see the video" onclick="shoulderPressesVid();"/><br/>
                    <img src="/images/Video-icon-orange.png" width="16" title="Click here to see the video" onclick="tricepDipsVid();"/>
                </div>
                <div style="float: left; width: 150px; height: 60px !important;display: block;">
                    <asp:ListBox ID="exListBoxUpPushBeginner" runat="server" SelectionMode="Single" BodyPart="2"></asp:ListBox>  
                </div>
                
            </td>
           
        </tr>
        <tr>
            <td>Upper - Pull</td>
            <td>
                <div style="float: left; width: 50px; height: 60px;display: block;">
                    <img src="/images/Video-icon-orange.png" width="16" title="Click here to see the video" onclick="pullUpsVids();"/><br/>
                    <img src="/images/Video-icon-orange.png" width="16" title="Click here to see the video" onclick="resistanceRowVid();"/><br/>
                    <img src="/images/Video-icon-orange.png" width="16" title="Click here to see the video" onclick="uprightRowVid();"/>
                </div>
                <div style="float: left; width: 150px; height: 60px !important;display: block;">
                    <asp:ListBox ID="exListBoxUpPullBeginner" runat="server" SelectionMode="Single" BodyPart="3"></asp:ListBox>  
                </div>
            </td>
            
        </tr>
        <tr>
            <td>Core</td>
            <td>
                <div style="float: left; width: 50px; height: 60px;display: block;">
                    <img src="/images/Video-icon-orange.png" width="16" title="Click here to see the video" onclick="crunchesVid();"/><br/>
                    <img src="/images/Video-icon-orange.png" width="16" title="Click here to see the video" onclick="proneHoldsVid();"/>
                </div>
                <div style="float: left; width: 150px; height: 60px !important;display: block;">
                    <asp:ListBox ID="exListBoxCoreBeginner" runat="server" SelectionMode="Single" BodyPart="4"></asp:ListBox>  
                </div>
            </td>
        </tr>
    </table>
    <br/><br/>
    
    <h3 class="introTitle">Select Days</h3>
    <i>*To select more than one day hold down the ctrl key</i>
    <table>
        <tr>
            <td style="width: 73px;">Days</td>
            <td style="width: 200px;">
                <div>
                    <asp:ListBox ID="daysBeginner" runat="server" ClientIDMode="Static" SelectionMode="Multiple" style="width: 100px !important; height: 120px !important;"></asp:ListBox>
                </div>
            </td>
        </tr>
    </table>

    <br/><br/>
    <button class="button-small grey_dark_reverse rounded3" id="cancelBeginner" onclick="goBackToReviewScreen($(this).attr('id'));return false;">Cancel and Exit</button>&nbsp;&nbsp;&nbsp;
    <button class="button-small vision_red rounded3" onclick="buttonFLBeginner();return false;">Save</button>&nbsp;&nbsp;&nbsp;
    <!-- <button class="thoughtbot" id="Button1" onclick="clearSelectedProgramAlert();goBackToReviewScreen($(this).attr('id'));return false;">Clear This Program</button>&nbsp;&nbsp;&nbsp;  -->
</div>

<div id="fatlossintermediate" class="trainingplan" style="margin-right: 10px !important;display: none;" runat="server" ClientIDMode="Static">
    <p><h3>Weights - Intermediate</h3></p>
    <p>This program is suitable for those who have a little exercise experience and is currently fairly active.
        For more information in each exercise click the relevant video icon.</p><br/>
    <p>To create your program:
    <ul>
    <li>Select two exercises from each of the body parts listed</li>
    <li>Select one core exercise</li>
    <li>Aim to complete 12 repetitions of each exercise</li>
    <li>Aim to do 2 to 3 sets of each exercise</li>
    <li>Rest between each exercise for up to 45 seconds</li>
    <li>Do the required number of sets for each exercise before moving onto the next exercise</li>
    </ul>
    </p><br/>
    <p style="font-style: italic;">
       In this program you should be aiming for an intensity level of 3.  Please refer to the intensity guide below for how this should feel.
If you find you can do more than the recommended 12 repetitions, simply do more until you reach the correct intensity level.

    </p>
    <br/><br/>
    
    <table>
        <tr>
            <td>Level</td>
            <td>Intensity</td>
            <td>Description</td>
        </tr>
        <tr>
            <td>1</td>
            <td>Light</td>
            <td>You are able to complete each exercise with little effort</td>
        </tr>
        <tr>
            <td>2</td>
            <td>Moderate</td>
            <td>You can complete each exercise comfortably</td>
        </tr>
        <tr>
            <td>3</td>
            <td>Hard</td>
            <td>Each working weight set is completed once you cannot do anymore by yourself</td>
        </tr>
    </table>
    <br/><br/>
    
    <h3 class="introTitle">Select Exercises</h3>
    <i>*To select more than one exercise hold down the ctrl key</i>
    <table id="FLIntermediateExs">
        <tr>
            <td>Area</td>
            <td>
                 Exercise 
            </td>
        </tr>
        <tr>
            <td>
                Legs <br/>
                <i>(2 exercises)</i>
            </td>
            <td>
                <div style="float: left; width: 50px; height: 60px;display: block;">
                    <img src="/images/Video-icon-orange.png" width="16" title="Click here to see the video" onclick="lungesVid();"/><br/>
                    <img src="/images/Video-icon-orange.png" width="16" title="Click here to see the video" onclick="squatsVid();"/><br/>
                    <img src="/images/Video-icon-orange.png" width="16" title="Click here to see the video" onclick="stepUpsVid();"/>
                </div>
                <div style="float: left; width: 150px; height: 60px;display: block;">
                    <asp:ListBox ID="exListBoxLegInterm" runat="server" SelectionMode="Multiple" bodypart="1"></asp:ListBox>  
                </div>
            </td>
        </tr>
        <tr>
            <td>
                Upper - Push<br/>
                <i>(2 exercises)</i>
            </td>
            <td>
                <div style="float: left; width: 50px; height: 60px;display: block;">
                    <img src="/images/Video-icon-orange.png" width="16" title="Click here to see the video" onclick="pushUpsVids();"/><br/>
                    <img src="/images/Video-icon-orange.png" width="16" title="Click here to see the video" onclick="shoulderPressesVid();"/><br/>
                    <img src="/images/Video-icon-orange.png" width="16" title="Click here to see the video" onclick="tricepDipsVid();"/>
                </div>
                <div style="float: left; width: 150px; height: 60px;display: block;">
                    <asp:ListBox ID="exListBoxUpPushInterm" runat="server" SelectionMode="Multiple" bodypart="2"></asp:ListBox>  
                </div>
            </td>
        </tr>
        <tr>
            <td>
                Upper - Pull <br/>
                <i>(2 exercises)</i>
            </td>
            <td>
                <div style="float: left; width: 50px; height: 60px;display: block;">
                    <img src="/images/Video-icon-orange.png" width="16" title="Click here to see the video" onclick="pullUpsVids();"/><br/>
                    <img src="/images/Video-icon-orange.png" width="16" title="Click here to see the video" onclick="resistanceRowVid();"/><br/>
                    <img src="/images/Video-icon-orange.png" width="16" title="Click here to see the video" onclick="uprightRowVid();"/>
                </div>
                <div style="float: left; width: 150px; height: 60px;display: block;">
                    <asp:ListBox ID="exListBoxUpPullInterm" runat="server" SelectionMode="Multiple" bodypart="3"></asp:ListBox>  
                </div>
            </td>
        </tr>
        <tr>
            <td>
                Core <br/>
                <i>(1 exercise)</i>
            </td>
            <td>
                <div style="float: left; width: 50px; height: 60px;display: block;">
                    <img src="/images/Video-icon-orange.png" width="16" title="Click here to see the video" onclick="crunchesVid();"/><br/>
                    <img src="/images/Video-icon-orange.png" width="16" title="Click here to see the video" onclick="proneHoldsVid();"/>
                </div>
                <div style="float: left; width: 150px; height: 60px;display: block;">
                    <asp:ListBox ID="exListBoxCoreInterm" runat="server" SelectionMode="Single" bodypart="4"></asp:ListBox>  
                </div>
            </td>
        </tr>
    </table>
    
    <div class="message"></div>
    <br/><br/>
    <h3 class="introTitle">Select Days</h3>
    <i>*To select more than one day hold down the ctrl key</i>
    <table>
        <tr>
            <td style="width: 73px;">Days</td>
            <td style="width: 200px;">
                <div>
                    <asp:ListBox ID="daysIntermediate" runat="server" ClientIDMode="Static" SelectionMode="Multiple" style="width: 100px !important; height: 120px !important;"></asp:ListBox>
                </div>
            </td>
        </tr>
    </table>
    
    

    <br/><br/>
    <button class="button-small grey_dark_reverse rounded3" id="Button1" onclick="goBackToReviewScreen($(this).attr('id'));return false;">Cancel and Exit</button>&nbsp;&nbsp;&nbsp;
    <button class="button-small vision_red rounded3" onclick="buttonFLIntermediate();return false;">Save</button>&nbsp;&nbsp;&nbsp;
    
</div>

<div id="fatlossadvance" class="trainingplan" style="margin-right: 10px !important;display: none;" >
    <p><h3>Weights - Advanced</h3></p>
    <p>This program is suitable for those who are experienced at exercise and are currently working out.  You may also have equipment at home which can be used to add intensity to your program.
        For more information in each exercise click the relevant video icon.</p><br/>
    <p>To create your program:
    <ul>
        <li>Aim to complete 12 repetitions of each exercise</li>
        <li>Aim to do 2 to 3 sets of each exercise</li>
        <li>Rest between each exercise for up to 30 seconds</li>
        <li>Do the required number of sets for each exercise before moving onto the next exercise</li>
    </ul>
    </p><br/>
    <p style="font-style: italic;">
       In this program you should be aiming for an intensity level of 3.  Please refer to the intensity guide below for how this should feel.
If you find you can do more than the recommended 12 repetitions, simply do more until you reach the correct intensity level.

    </p>
    <br/><br/>
    
    <table>
        <tr>
            <td>Level</td>
            <td>Intensity</td>
            <td>Description</td>
        </tr>
        <tr>
            <td>1</td>
            <td>Light</td>
            <td>You are able to complete each exercise with little effort</td>
        </tr>
        <tr>
            <td>2</td>
            <td>Moderate</td>
            <td>You can complete each exercise comfortably</td>
        </tr>
        <tr>
            <td>3</td>
            <td>Hard</td>
            <td>Each working weight set is completed once you cannot do anymore by yourself</td>
        </tr>
    </table>
    <br/><br/>
    <h3 class="introTitle">Given Exercises</h3>
    <table id="FLAdvanceExs">
        <tr>
            <td>Area</td>
            <td>Exercise</td>
        </tr>
        <tr>
            <td>Legs</td>
            <td style="text-align: left">
                 <div style="float: left; width: 150px; height: 60px;display: block;">
                    <img src="/images/Video-icon-orange.png" width="16" onclick="lungesVid();" title="Click here to see the video"/>&nbsp;&nbsp;Lunges<br/>
                    <img src="/images/Video-icon-orange.png" width="16" onclick="squatsVid();" title="Click here to see the video"/>&nbsp;&nbsp;Squats<br/>
                    <img src="/images/Video-icon-orange.png" width="16" onclick="stepUpsVid();" title="Click here to see the video"/>&nbsp;&nbsp;Step Ups  
                </div>
            </td>
        </tr>
        <tr>
            <td>Upper - Push</td>
            <td style="text-align: left">
                 <div style="float: left; width: 150px; height: 60px;display: block;">
                    <img src="/images/Video-icon-orange.png" width="16" onclick="pushUpsVids();" title="Click here to see the video"/>&nbsp;&nbsp;Push Ups<br/>
                    <img src="/images/Video-icon-orange.png" width="16" onclick="shoulderPressesVid();" title="Click here to see the video"/>&nbsp;&nbsp;Shoulder Presses <br/>
                    <img src="/images/Video-icon-orange.png" width="16" onclick="tricepDipsVid();" title="Click here to see the video"/>&nbsp;&nbsp;Tricep Dips
                </div>
            </td>
        </tr>
        <tr>
            <td>Upper - Pull</td>
            <td style="text-align: left">
                <div style="float: left; width: 150px; height: 60px;display: block;">
                    <img src="/images/Video-icon-orange.png" width="16" onclick="pullUpsVids();" title="Click here to see the video"/>&nbsp;&nbsp;Pull Ups <br/>
                    <img src="/images/Video-icon-orange.png" width="16" onclick="resistanceRowVid();" title="Click here to see the video"/>&nbsp;&nbsp;Resistance Row <br/>
                    <img src="/images/Video-icon-orange.png" width="16" onclick="uprightRowVid();" title="Click here to see the video"/>&nbsp;&nbsp;Upright Rows
                </div>
            </td>
        </tr>
        <tr>
            <td>Core</td>
            <td style="text-align: left">
                <div style="float: left; width: 150px; height: 60px;display: block;">
                    <img src="/images/Video-icon-orange.png" width="16" onclick="crunchesVid();" title="Click here to see the video"/>&nbsp;&nbsp;Crunches <br/>
                    <img src="/images/Video-icon-orange.png" width="16" onclick="proneHoldsVid();" title="Click here to see the video"/>&nbsp;&nbsp;Prone Holds
                </div>
            </td>
        </tr>
    </table>
    <br/><br/>
    
    <h3 class="introTitle">Select Days</h3>
    <i>*To select more than one day hold down the ctrl key</i>
    <table>
        <tr>
            <td style="width: 73px;">Days</td>
            <td style="width: 150px;">
                <div>
                    <asp:ListBox ID="daysAdvanced" runat="server" ClientIDMode="Static" SelectionMode="Multiple" style="width: 100px !important; height: 120px !important;"></asp:ListBox>
                </div>
            </td>
        </tr>
    </table>

    <br/><br/>
    <button class="button-small grey_dark_reverse rounded3" id="cancelAdvance" onclick="goBackToReviewScreen($(this).attr('id'));return false;">Cancel and Exit</button>&nbsp;&nbsp;&nbsp;
    <button class="button-small vision_red rounded3" onclick="buttonFLAdvance();return false;">Save</button>
    
    
</div>

</div>

<script type="text/javascript">
    $("#flat6").addClass("active");
    $(document).ready(function () {
        if ($("#divSelectedProgram").css("display") == "block") {
            
            $("#divSelectedProgram").prev().prev().css("display", "none");
        } 
    });
</script>