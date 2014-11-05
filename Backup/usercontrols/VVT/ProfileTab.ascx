<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProfileTab.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.VVT.ProfileTab" %>
<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls" TagPrefix="BDP" %>
<%--<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>--%>
<%@ Register TagPrefix="ddlb" Assembly="OptionDropDownList" Namespace="OptionDropDownList" %>
<%@ Register TagPrefix="uc" TagName="Chart" src="~/usercontrols/clubvision/ProgressGraph.ascx" %>

<style type="text/css">
    .bottomNum
    {
        margin-left:22px;
    }
    #tableTrainingDiaryProfil
    {
        margin:auto;
    }
</style>
<!-- -->

<div id="captainIcon" style="display: none;" >
    <a title="this is the tip that captain will give it to ya" style="cursor: pointer; position: absolute; top: 230px; left: 40px;">
    <img src="/images/vpt_captainaccountabilityV2.jpg" alt="captain" /> </a>
</div>

<div id="eProfileTab" class="element" style="overflow: visible;"><!-- 605 -->
    <div class="replace" id="profileTab" runat="server" style="background-image: none;">
        <!--              
            <div id="tabGoal" style="cursor: pointer; position: absolute; top: 0px; left: 404px; width: 201px; height: 45px"
                 onclick="document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_ProfileTab_2_profileTab').style.backgroundImage = 'url(/images/eHdrProfileTabGoal.gif)';
                          document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_ProfileTab_2_eProfileTabGoal').style.display = 'block';
                          document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_ProfileTab_2_eProfileTabTrain').style.display = 'none';
                          document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_ProfileTab_2_eProfileTabMenu').style.display = 'none';
                          document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_ProfileTab_2_eDailyMenusView').style.display = 'none';
                          document.getElementById('DivReadySetGoQ').style.display = 'block';
                          drawChartProgress();                          
                          document.getElementById('captainIcon').style.display = 'none';"></div>
            <div id="tabTrain" style="cursor: pointer; position: absolute; top: 0px; left: 202px; width: 201px; height: 45px"
                 onclick="document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_ProfileTab_2_profileTab').style.backgroundImage = 'url(/images/eHdrProfileTabTrain.gif)';
                          document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_ProfileTab_2_eProfileTabGoal').style.display = 'none';
                          document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_ProfileTab_2_eProfileTabTrain').style.display = 'block';
                          document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_ProfileTab_2_eProfileTabMenu').style.display = 'none';
                          document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_ProfileTab_2_eDailyMenusView').style.display = 'none';
                          document.getElementById('DivReadySetGoQ').style.display = 'none';
                          setEqualHeight($('#eProfileView .eContent'),$('#<%=eProfileTabTrain.ClientID %>'));
                          emptyGraph();
                          callMotivationCaptainOnTrainingDiary();"></div>
            <div id="tabMenu" style="cursor: pointer; position: absolute; top: 0px; left: 0px; width: 201px; height: 45px"
                 onclick="document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_ProfileTab_2_profileTab').style.backgroundImage = 'url(/images/eHdrProfileTabMenu.gif)';
                          document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_ProfileTab_2_eProfileTabGoal').style.display = 'none';
                          document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_ProfileTab_2_eProfileTabTrain').style.display = 'none';
                          document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_ProfileTab_2_eProfileTabMenu').style.display = 'block';
                          document.getElementById('DivReadySetGoQ').style.display = 'none';
                          emptyGraph();
                          drawChartMacros();
                          document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_ProfileTab_2_eDailyMenusView').style.display = 'block';
                          document.getElementById('captainIcon').style.display = 'none';"></div> -->
            <div id="tabMenu" onclick="myProfileTab_reset();myProfileTab_Menu(); return false;">Whats On The Menu</div>
            <div id="tabTrain" onclick="myProfileTab_reset();myProfileTab_TrainingDiary();return false;">Training Diary</div>             
            <div id="tabGoal" onclick="myProfileTab_reset();myProfileTab_Goal();return false;">Goal and Progress</div>
        </div>
    <div class="clear"></div>
    <div class="eContent" id="eProfileTabGoal" runat="server">
        <%--<uc:Chart ID="chart" runat="server" />--%>
        <div style="margin:10px 0px 10px 5px; overflow:hidden;">
            <div style="float:left;">
                Click on the chart to drag and open an area you wish to zoom in on.
                <br />
                To reset the zoom simply double click anywhere on the graph.
            </div>
            <div style="float:right;">
                <span id="weeklybtn" style="background-color:#A4A4A4;cursor:pointer; cursor:hand;" class="bt">Weekly</span>
                <span id="allbtn" style="background-color:#E27423;cursor:pointer; cursor:hand;" class="bt">Overall</span>
            </div>
        </div>
        <div id="chartProgress" class="unselectable" style="height: 234px; width: 600px;top: auto !important;"></div>
        <div class="profileCopy" style="display: none;">
                <span class="title">My Goal</span><br />
                <span id="goal" runat="server"></span><br /><br />
                <span class="title">My Progress</span><br />
                <span class="lost" style="color:#008CA7 !important;" id="change" runat="server"></span> <b><span id="change_text" runat="server">kg lost</span></b><br /><br />
                <span class="title">My Current Weight</span><br />
                <span id="weight" runat="server"></span><br /><br />                
            </div>
    </div> 
    <div class="eContent" id="eProfileTabTrain" style="display: none;" runat="server">
            <div style="font-weight: bold; font-size: 14px;float: left; padding-top: 4px; padding-right: 10px">Week beginning:</div>
            <div style="border: 1px solid #999999; padding: 1px;float: left; height: 21px; width: 300px;">
            <input type="hidden" id="currentDate" runat="server" value="" />
            <asp:ImageButton runat="server" ID="buttonDayNext" Text=">" 
                    onclick="ButtonWeekNextClick" ImageUrl="~/images/calendar_forward.gif" style="float: right" />
            <asp:ImageButton runat="server" ID="buttonDayPrev" Text="<" 
                    onclick="ButtonWeekPrevClick" ImageUrl="~/images/calendar_back.gif" style="float: left" />
            <p id="caldate" style="display: inline-block; padding: 2px 14px"><asp:Literal runat="server" ID="literalDay"></asp:Literal></p>
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
            <asp:Label ID="testLabel" runat="server" Text="Label" Visible="False"></asp:Label>
            <asp:Label ID="iIDLabel" runat="server" Text="Label" Visible="False"></asp:Label>
            <div class="clear"></div>
            
            <input type="hidden" id="currentWeekNumber" runat="server" value="" ClientIDMode="Static" />
            <input type="hidden" id="currentStartDay" runat="server" value="" ClientIDMode="Static" />
            
            <asp:Literal ID="FirstLoadNotifLiteral" runat="server"></asp:Literal>
            <div id="tableTrainingDiaryProfilediv" runat="server" clientidmode="Static">
                <br/>
                <asp:Literal ID="litTrainingDiary" runat="server"></asp:Literal>   
            </div>
 
            <br/>
            
            <div style="float: right;padding-right: 20px;">	 	
	                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/buttonEditTraining.gif" OnClick="ImageButton1Click" />	 	
            </div>
                    
            <asp:Literal ID="LitSummaryPanel" runat="server"></asp:Literal>
         </div>  
    <div class="eContent" id="eProfileTabMenu" style="display: none;margin-top: 47px;" runat="server">
            <div style="font-weight: bold; font-size: 14px;float: left; padding-top: 4px; padding-right: 10px">Whats on the menu for:</div>
<div style="border: 1px solid #999999; padding: 1px;float: left; height: 21px; width: 300px">
<asp:ImageButton runat="server" ID="buttonDayNext2" Text=">" 
        onclick="ButtonDayNext2Click" ImageUrl="~/images/calendar_forward.gif" style="float: right" />
<asp:ImageButton runat="server" ID="buttonDayPrev2" Text="<" 
        onclick="ButtonDayPrev2Click" ImageUrl="~/images/calendar_back.gif" style="float: left" />
<p style="display: inline-block; padding: 2px 14px"><asp:Literal runat="server" ID="literalDay2"></asp:Literal></p>
</div>
<div style="float: left; position: relative; top: -1px">
<BDP:BasicDatePicker ID="bdpDay2" runat="server" DisplayType="Image" 
        AutoPostBack="True" 
        ShowNoneButton="False" onselectionchanged="BdpDay2SelectionChanged" 
           ShowTodayButton="False" 
            ButtonImageFileName="calendar.gif" ButtonImageHeight="25" 
            ButtonImageWidth="26" RenderImages="File" ResourcePath="/images/" 
            DownYearSelectorImageFileName="" NextMonthImageFileName="calendar_forward.gif" 
            PrevMonthImageFileName="calendar_back.gif" RenderStyleSheet="None">
</BDP:BasicDatePicker>
</div>
<br /><br />
<br /><br />
<div id="foodDiary" runat="server"></div>
<div id="chartMacros" style="height: 184px; width: 303px;"></div>

<script type="text/javascript">
    var chartdata_weight = [<asp:Literal id="chartProgressData" runat="server"></asp:Literal>];
    var chardata_weight_goal = [<asp:Literal ID="chartProgressDataGoal" runat="server"></asp:Literal>];
    var interval = getInterval();
    var plot_weight;
    $(document).ready(function () {
        if('<%=Session["MemberType"]%>'=='VVT'){
            $('#weeklybtn').text("Recent");
            // $('#<%=eProfileTabGoal.ClientID %>').height(445);
            $('#chartProgress').height(190);
            $('#chartUsage').css("margin-top", 30);
            $('#chartUsage').css("margin-bottom", 20);
        }
        else if ('<%=Session["MemberType"]%>'=='VPT'){
            $('#weeklybtn').text("Weekly");
            $('#tableTrainingDiaryProfilediv').height(270);
        }
        if('<%=Session["FromTrainer"]%>'=='YES') {
            $('#tableTrainingDiaryProfilediv').height(250);
        }
        drawChartMacros();
        drawChartProgress();
        setDateFormat();
    });
    function Zoom(){
        drawChartProgressParam(chartdata_weight.filter(isBigEnough),chardata_weight_goal,'1 month');
    }
    function ZoomExt(){
        if(chartdata_weight.length>4){
            drawChartProgressParam(chartdata_weight.slice(chartdata_weight.length-5,chartdata_weight.length),chardata_weight_goal,'1 month');
        }
        else{
            drawCharProgresstParam(chartdata_weight,chardata_weight_goal,interval);
            return false;
        }
    }
    function isBigEnough(element) {
        var fd = element[0].split(' ')[0].split(/[^0-9]/);
        var lg = ('<%=Session["lastGoalSession"]%>').split(' ')[0].split(/[^0-9]/);
        var fromDate = new Date(fd[0], fd[2] - 1, fd[1]);
        var lastGoalSession = new Date(lg[0],lg[2]-1,lg[1]);
        return Date.parse(fromDate) >= Date.parse(lastGoalSession);
    }
    function getInterval(){
        try{
            var years = getDaysDiffInDates2(chartdata_weight[0][0],chartdata_weight[chartdata_weight.length-1][0])/365;
            return (parseInt(years) + 2).toString() + ' month';
        }
        catch(err){return '3 month';
        }
    }
    function getDaysDiffInDates2(date1,date2) {
        var dateTime1Split = date1.split(' ');
        var dateTime2Split = date2.split(' ');
        var a = dateTime1Split[0].split(/[^0-9]/);
        var a2 = dateTime2Split[0].split(/[^0-9]/);
        var d = new Date(a[0], a[2] - 1, a[1]);
        var d2 = new Date(a2[0], a2[2] - 1, a2[1]);
        var diff = Math.floor((Date.parse(d2) - Date.parse(d)) / 86400000);
           
        return diff;
    }

    function drawChartProgress(){
        drawChartProgressParam(chartdata_weight,chardata_weight_goal,interval);
    }
    function drawChartProgressParam(data1,data2,tickInterval){
        plot_weight = $.jqplot('chartProgress', [data1 , data2], {
            title: '',
            gridPadding: { right: 35 },
            axes: {
                xaxis: {
                    renderer: $.jqplot.DateAxisRenderer,
                    tickOptions: { formatString: '%d %b %y' },
                    tickInterval: tickInterval
                },
                yaxis: { 
                    tickOptions:{formatString:"%#.1f"}
                }
            },
            series: [{ 
                showMarker:true,
                pointLabels: { show:false },
                markerOptions: {
                    size: 5
                }
            },{ 
                showMarker:true,
                pointLabels: { show:true },
                markerOptions: {
                    size: 10
                }
            }],
            highlighter: {
                show: true,
                sizeAdjust: 7.5,
                tooltipAxes: 'both',
                tooltipLocation: 'ne',
                useAxesFormatters: true,
                formatString : '<span style="font-size:14px;">Date: %s<br>Weight: %#.1f kg</span>'
            },     
            cursor:{ 
                show: true,
                zoom:true, 
                showTooltip:false
            }
        });
        $('#chartProgress').dblclick(function() {
            setDateFormat();
        });
        setDateFormat();
    }
    function drawChartMacros()
    {
        var s1 = [<asp:Literal runat="server" id="chartMacrosData_Actual"></asp:Literal>];
        var s2 = [<asp:Literal runat="server" id="chartMacrosData_Target"></asp:Literal>];
        var ticks = ['Carbs', 'Protein', 'Fat'];
        var plot1 = $.jqplot(
            'chartMacros', 
            [s1, s2], 
            {
                seriesDefaults:{
                    renderer:$.jqplot.BarRenderer,
                    rendererOptions: {fillToZero: true}
                },
                series:[
                    {label:'Actual'},
                    {label:'Target'}
                ],
                legend: {
                    show: true,
                    placement: 'outsideGrid'
                },
                axes: {
                    xaxis: {
                        renderer: $.jqplot.CategoryAxisRenderer,
                        ticks: ticks
                    },
                    yaxis: {
                        min: 0,
                        pad: 1.05             
                    //tickOptions: {formatString: '$%d'}
                    }
                }
            });
    }
    $('#weeklybtn').click(function() {    
        if(plot_weight){
            plot_weight.destroy();
            if('<%=Session["MemberType"]%>'=='VPT'){
                Zoom();
            }
            else if('<%=Session["MemberType"]%>'=='VVT'){
                ZoomExt();
            }
        }
        $(this).css('background-color','#E27423');
        $('#allbtn').css('background-color','#A4A4A4');
    });
    
    function ShowOrHideMacro(){
        if('<%=Session["MemberType"]%>'=='VPT')
            document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_ProfileTab_2_eDailyMenusView').style.display = 'block';
        else if('<%=Session["MemberType"]%>'=='VVT')
            document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_ProfileTab_2_eDailyMenusView').style.display = 'none';
    }
    function setHeightExt(element1,element2){
        if('<%=Session["MemberType"]%>'=='VVT'){
            setEqualHeight(element1, element2);
        }
    }
    $('#allbtn').click(function() {
        if(plot_weight){
            plot_weight.destroy();
            drawChartProgressParam(chartdata_weight,chardata_weight_goal,interval);
        }
        $(this).css('background-color','#E27423');
        $('#weeklybtn').css('background-color','#A4A4A4');  
    });

    function setDateFormat(){
        $('.jqplot-xaxis-tick').each(function(i, obj) {
            var testtext = $(this).text();
            if(testtext.length>7){
                $(this).text(testtext.substr(testtext.indexOf(" ") + 1));
            }
        });
    }
    function emptyGraph(){
        plot_weight.destroy();
        $('#chartProgress').empty();
        $('#allbtn').css('background-color','#E27423');
        $('#weeklybtn').css('background-color','#A4A4A4'); 
    }
</script>
</div>
</div><!-- /eProfileTab -->

<div style="display:none;" id="eDailyMenusView" class="element" runat="server"><!-- 605 -->
  <!-- <h3 class="replace">Daily Menus</h3> -->
  <div class="tabHeading">
       <div style="width: 265px;text-align: left;padding-left: 10px;cursor: default;" class="divHeadingActive">My Daily Plans</div>
       <div style="width: 328px;" class="lastDivHeading"></div>    
  </div>
  <div class="clear"></div>
  <div class="eContent eClean" style="margin-top: 2px;"> 
  <a id="mycarousel-prev" href="#"><img src="/images/buttonPrev.png" alt="Prev" /></a>
    <a id="mycarousel-next" href="#"><img src="/images/buttonNext.png" alt="Next" /></a>
        <ul id="menus">
        <asp:Literal id="literalMenus" runat="server"></asp:Literal>
    </ul>    
    <div class="clear">&nbsp;</div>
  </div><!-- /eContent -->
  <div class="clear">&nbsp;</div>
</div><!-- /eDailyMenus -->

<div style="display:block;" id="DivReadySetGoQ" class="element"><!-- 605 -->
  <!-- <h3 class="replace">Ready Set Go Questionnaire</h3> -->
  <div class="tabHeading">
       <div style="width: 265px;text-align: left;padding-left: 10px;cursor: default;" class="divHeadingActive">Ready Set Go Questionnaire</div>
       <div style="width: 328px;" class="lastDivHeading"></div>    
  </div>
  <div class="clear"></div>
  <div class="eContent eClean"> 
        <a href="/club-vision/my-profile/ready-set-go/">Click here</a> to assess your levels of readiness by completing the questionnaires outlined in the "Ready, Set, Go - 3 Steps to Better Health" book written by Andrew Simmons, Founder of Vision.  These questionnaires will help you identify areas that may influence your results.
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
