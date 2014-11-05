<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Tools_EmotionTracker.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.clubvision.Tools_EmotionTracker" %>
<%@ Register TagPrefix="BDP" Namespace="BasicFrame.WebControls" Assembly="BasicFrame.WebControls.BasicDatePicker, Version=1.4.1.41500, Culture=neutral, PublicKeyToken=e1cce521aa9b4849" %>

<style>
    #ContentPlaceHolderDefault_help {
        display: none;
    }
</style>

<div class="pTrainingDiary" id="weightSessDiv" style="margin-right: 10px !important;padding:10px;" runat="server" ClientIDMode="Static">
    
    <div style="font-weight: bold; font-size: 14px;float: left; padding-top: 4px; padding-right: 10px">Date Range:</div>
            <div style="border: 1px solid #999999; padding: 1px;float: left; height: 21px; width: 300px;">
                <input type="hidden" id="currentDate" runat="server" value="" />
                <asp:ImageButton runat="server" ID="buttonDayNext" Text=">" 
                        onclick="ButtonWeekNextClick" ImageUrl="~/images/calendar_forward.gif" style="float: right" />
                <asp:ImageButton runat="server" ID="buttonDayPrev" Text="<" 
                        onclick="ButtonWeekPrevClick" ImageUrl="~/images/calendar_back.gif" style="float: left" />
                <p style="display: inline-block; padding: 2px 14px"><asp:Literal runat="server" ID="literalWeek"></asp:Literal></p>
            </div>
            <div style="float: left; position: relative; top: -1px">  
                <BDP:BasicDatePicker ID="bdpDay" runat="server" DisplayType="Image" 
                    AutoPostBack="True" 
                    ShowNoneButton="False" onselectionchanged="BdpDaySelectionChanged" 
                    ShowTodayButton="False" 
                    ButtonImageFileName="calendar.gif" ButtonImageHeight="25" 
                    ButtonImageWidth="26" RenderImages="File" ResourcePath="/images/" 
                    DownYearSelectorImageFileName="" NextMonthImageFileName="calendar_forward.gif" 
                    PrevMonthImageFileName="calendar_back.gif" RenderStyleSheet="None"
                    ShowWeekNumbers="True" >
                </BDP:BasicDatePicker>
            </div>

            <div style="float: right; position: relative; top: 7px">  
                <a href="#" onclick="$('#emoExplanation').slideToggle();return false;" style="color: #002147;">Learn why tracking your emotions is important?</a>
            </div>
            
    <div id="emoExplanation" style="width: 100%; float: left;display: none;height: 120px; padding-top: 20px; padding-bottom: 10px;">
        <p style="line-height: 150%;font-size: 14px;">How you feel each day can have an impact on what and when you eat.  For example some people find stress makes them eat whilst other miss meals when busy.  Similarly people eat when they are happy, angry, tired or bored.  It is important to recognise when eating to satisfy hunger or to satisfy an emotion.  Eating in response to moods can set up an unwanted habit that is difficult to break.</p><br/>
        <p style="line-height: 150%;font-size: 14px;">By tracking how you feel every day you can identify your behaviour patterns during the week, the weekend and over a month.  Reviewing these will assist in establishing healthier eating habits, be in control of your emotions and empower yourself to success.</p>
    </div>
    <br />

    <div id="TDresponsemessage" style="color: #6BB33A; font-weight: bold;height: 30px; display: block;"></div>
    <asp:Label ID="weekLabel" runat="server" Text="" Visible="True" ForeColor="#FFFFFF"></asp:Label>
    
    <div style="width: 100%; float: right; padding-top: 20px;">
        <h4 style="font-weight: normal;font-size: 16px; color: #C60C30;">Simply click on the meal time to record how you feel at that meal.</h4>
        <br />    
        <!-- as table -->
        <asp:Literal ID="litEmotionTrackerSummary" runat="server"></asp:Literal>
        <br/>
        <!-- as table -->
    </div>    

    <br/><br />

    <div id="WeightsSessParagraph">
       
    </div>
                    
</div>