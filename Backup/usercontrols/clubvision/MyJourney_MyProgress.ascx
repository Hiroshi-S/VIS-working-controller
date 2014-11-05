<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyJourney_MyProgress.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.clubvision.MyJourney_MyProgress" %>


<h2>TEST</h2>

<div id="chartProgress" style="height: 234px; width: 600px; left: 10px !important;">
</div>
<div class="profileCopy" style="display: none;">
    <span class="title">My Goal</span><br />
    <span id="goal" runat="server"></span><br /><br />
    <span class="title">My Progress</span><br />
    <span class="lost" style="color:#008CA7 !important;" id="change" runat="server"></span> <b><span id="change_text" runat="server">kg lost</span></b><br /><br />
    <span class="title">My Current Weight</span><br />
    <span id="weight" runat="server"></span><br /><br />                
</div>


<script type="text/javascript">

    function drawChartProgress()
    {
        var chartdata_weight = [<asp:Literal ID="chartProgressData" runat="server"></asp:Literal>];
        
        var chardata_weight_goal = [<asp:Literal ID="chartProgressDataGoal" runat="server"></asp:Literal>];

        var plot_weight = $.jqplot('chartProgress', [chartdata_weight , chardata_weight_goal], { 
            title: '', 
            gridPadding: { right: 35 }, 
            axes: { xaxis: { renderer: $.jqplot.DateAxisRenderer,
                    tickOptions: { formatString: '%b%y' },
                    tickInterval: '3 month'
                }
            }, 
            seriesDefaults: { 
                showMarker:true,
                pointLabels: { show:true } 
            }
        });   
    }

    $(document).ready(function () {
        drawChartProgress();
    });
</script>