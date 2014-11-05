<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProgressGraph.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.clubvision.ProgressGraph" %>

<div style="margin:10px 0px 10px 5px; overflow:hidden;width: 571px;">
    <div style="float:left;">
        Click on the chart to drag and open an area you wish to zoom in on.
        <br />
        To reset the zoom simply double click anywhere on the graph.
    </div>
    <div style="float:right;">
        <span id="weeklybtn" style="background-color:#A4A4A4;cursor:hand;cursor:pointer;" class="bt"></span>
        <span id="allbtn" style="background-color:#E27423;cursor:hand;cursor:pointer;" class="bt">Overall</span>
    </div>
</div>
<div id="chart1" class="unselectable" style="height: 234px; width: 600px;"></div>

<script type="text/javascript">
    var chartdata_weight = [<asp:Literal id="chartProgressData" runat="server"></asp:Literal>];
    var chardata_weight_goal = [<asp:Literal ID="chartProgressDataGoal" runat="server"></asp:Literal>];
    var interval = getInterval();
    var plot_weight;
    $(document).ready(function () {
        if('<%=Session["MemberType"]%>'=='VVT')
            $('#weeklybtn').text("Recent");
        else if ('<%=Session["MemberType"]%>'=='VPT')
            $('#weeklybtn').text("Weekly");
        drawChart();
        setDateFormat();
    });
    function drawChart(){
        drawChartParam(chartdata_weight,chardata_weight_goal,interval);
    }
    function Zoom(){
        drawChartParam(chartdata_weight.filter(isBigEnough),chardata_weight_goal,'1 month');
    }
    function ZoomExt(){
        if(chartdata_weight.length>4){
            drawChartParam(chartdata_weight.slice(chartdata_weight.length-5,chartdata_weight.length),chardata_weight_goal,'1 month');
        }
        else{
            drawChartParam(chartdata_weight,chardata_weight_goal,interval);
            return false;
        }
    }
    function isBigEnough(element) {
        var fd = element[0].split(' ')[0].split(/[^0-9]/);
        var lg = ('<%=Session["lastGoalSession"]%>').split(' ')[0].split(/[^0-9]/);
        var fromDate = new Date(fd[0], fd[1] - 1, fd[2]);
        var lastGoalSession = new Date(lg[0],lg[1]-1,lg[2]);
        return Date.parse(fromDate) >= Date.parse(lastGoalSession);
    }
    function getInterval(){
        var years = getDaysDiffInDates(chartdata_weight[0][0],chardata_weight_goal[0][0])/365;
        return (parseInt(years) + 2).toString() + ' month';
    }
    function drawChartParam(data1,data2,tickInterval){
        plot_weight = $.jqplot('chart1', [data1,data2], { 
            title: '', 
            gridPadding: { right: 35 }, 
            axes: { 
                xaxis: {
                    renderer: $.jqplot.DateAxisRenderer,
                    tickOptions: { formatString: '%d %b %y' },
                    tickInterval: tickInterval
                },
                yaxis: {
                    tickOptions:{formatString: "%#.1f"}
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
                bringSeriesToFront:true,
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
        $('#chart1').dblclick(function() {
            setDateFormat();
        });    
        setDateFormat();
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
    $('#allbtn').click(function() {
        if(plot_weight){
            plot_weight.destroy();
            drawChartParam(chartdata_weight,chardata_weight_goal,interval);
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
        $('#chart1').empty();
         $('#allbtn').css('background-color','#E27423');
         $('#weeklybtn').css('background-color','#A4A4A4'); 
    }
</script>