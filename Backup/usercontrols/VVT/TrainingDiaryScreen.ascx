<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TrainingDiaryScreen.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.VVT.TrainingDiaryScreen" %>
<%@ Register TagPrefix="BDP" Namespace="BasicFrame.WebControls" Assembly="BasicFrame.WebControls.BasicDatePicker, Version=1.4.1.41500, Culture=neutral, PublicKeyToken=e1cce521aa9b4849" %>

<style>
    #ContentPlaceHolderDefault_help {
        display: none;
    }
</style>

<div id="captainIcon">
    <a title=" " style="cursor: pointer; position: absolute; top: 230px; left: 40px;display: block !important; ">
    <img id="captainImage" src="/images/vpt_captainaccountabilityV2.jpg" alt="captain" /> </a>
</div>
<div id="eWhatsOnMainWithNews" class="element" style="width:920px !important;">
       <!--   <div id="eWhatsOn" class="element eWhatsNewsletterOn605px" style="margin: 25px 0px 0px 0px;">605 -->
          <div class="replace" id="foodDiaryTab" style="height: 30px;width: 100%;">
            <div style="margin-left: 5px;border-bottom: none;background-image: -webkit-linear-gradient(top, rgb(237, 237, 237) 0px, rgb(247, 247, 247) 50%, rgb(255, 255, 255) 100%)">Training Diary</div>
            <div style="width: 706px; border:none; border-bottom: 1px solid #cccccc;cursor: default;height:46px;background: none;"></div>
        </div>

          <div class="eContent eRed" style="width : 918px !important;min-height:500px !important;">
            <div id="visionLibraryContainer" style="width:918px;margin-top:0px;background: url(/ images/recipeContainerBg920px.gif) 0 0 no-repeat;">

                <div class="pTrainingDiary" style="margin-right: 10px !important;padding:10px;">
                    <div class="dateTitleFoodDiary" style="height: 30px;margin-top: 10px;">
                        <div style="font-weight: bold; font-size: 14px;float: left; padding-top: 4px; padding-right: 10px;line-height: 30px;">Week beginning:</div>
                        <div style="border: 1px solid #ffffff; padding: 1px;float: left; height: 30px; width: 300px;">
                        <input type="hidden" id="currentDate" runat="server" value="" />
                        <asp:ImageButton runat="server" ID="buttonDayNext" Text=">" 
                                onclick="ButtonWeekNextClick" ImageUrl="/images/icons/Web/nextarrow.png" style="float: right;height: 30px;" />
                        <asp:ImageButton runat="server" ID="buttonDayPrev" Text="<" 
                                onclick="ButtonWeekPrevClick" ImageUrl="/images/icons/Web/prevarrow.png" style="float: left;;height: 30px;" />
                        <p style="display: inline-block; padding: 0px 14px;line-height: 30px;font-size: 16px;"><asp:Literal runat="server" ID="literalDay"></asp:Literal></p>
                        </div>
                        <div style="float: left; position: relative; top: -1px">  
                            <BDP:BasicDatePicker ID="bdpDay" runat="server" DisplayType="Image" 
                                AutoPostBack="True" 
                                ShowNoneButton="False" onselectionchanged="BdpDaySelectionChangeStartDay" 
                                ShowTodayButton="False" 
                                ButtonImageFileName="calendar.png" ButtonImageHeight="25" 
                                ButtonImageWidth="26" RenderImages="File" ResourcePath="/images/icons/web/" 
                                DownYearSelectorImageFileName="" NextMonthImageFileName="nextarrow.png" 
                                PrevMonthImageFileName="prevarrow.png" RenderStyleSheet="None"
                                ShowWeekNumbers="True" >
                            </BDP:BasicDatePicker>
                        </div>
                    </div>
                    
   

                    <br /><br />
                    <asp:Literal ID="FirstLoadNotifLiteral" runat="server"></asp:Literal>
                    <div id="TDresponsemessage" style="color: #6BB33A; font-weight: bold;height: 30px; display: block;"></div>
                    <input type="hidden" id="currentWeekNumber" runat="server" value="" ClientIDMode="Static" />
                    <input type="hidden" id="currentStartDay" runat="server" value="" ClientIDMode="Static" />
                    <table id="tableTrainingDiary">
                        <tr style="font-weight:bold;">
                            <td>Weekday</td>
                            <td></td>
                            <td>Exercise</td>
                            <td>When</td>
                            <td>Intensity</td>
                            <td>Duration</td>
                            <% if ((string) Session["MemberType"] == "VPT"){%>
                            <td>Type</td>
                            <% }%>
                            <td>Done?</td>
                            <td></td>
                            <td></td>
                            
                        </tr>
                        <asp:Literal ID="litTrainingDiary" runat="server"></asp:Literal>    
                    </table>
                    
                    <br/><br />
                    
                    <asp:Literal ID="LitSummaryPanel" runat="server"></asp:Literal>
                </div>

        </div>
    </div>
</div>

<script type="text/javascript">
    $(document).ready(function () {

        switch (getUrlVars()["tab"]) {
            case "edittrainingdiary":

                $("#captainIcon").css("display", "block");

                var buttons = '';

                for (var i = 1; i <= parseInt(getUrlVars()["count"]); i++) {
                    buttons += '<button class="thoughtbot" onclick="editTrainingDiaryFromExceededMacro(\''
                                    + getUrlVars()["b" + i + "click"] + '\', \'' + getUrlVars()["ext" + i] + '\', \''
                                    + getUrlVars()["kjs"] + '\');">'
                                    + getUrlVars()["b" + i + "txt"].replace(/%20/g, " ") + '</button><br/>';
                }


                $("#captainIcon a").showBalloon({
                    position: 'right',
                    contents: '<b>Get back on track!</b><br/><br/>'
                            + 'The easiest way to ensure you reach your health and fitness goals is to add extra time to exercise you have already planned to do.<br/><br/>'
                            + 'Captain Accountability suggest one of the following, please tick your choice to update your Training Diary<br/><br/>'
                            + '<br/>'
                            + buttons
                            + '<button class="thoughtbot" onclick="$(\'#captainIcon a\').hideBalloon();">Ignore</button>'
                            ,
                    tipSize: 24,
                    css: {
                        maxWidth: "37em",
                        border: "solid 5px #C60C30",
                        color: "#002147",
                        fontSize: "130%",
                        backgroundColor: "#efefef",
                        left: "460px",
                        padding: "10px",
                        opacity: "0.95"
                    }
                }).toggle(
                function () { $(this).hideBalloon(); },
                function () { $(this).showBalloon(); });

                $('body > div:last-child').css('left', '440px');
                break;

            default:
                callMotivationCaptainOnTrainingDiary(); break;
            //callMotivationCaptainOnTrainingDiary();  
        }
    });
</script>

