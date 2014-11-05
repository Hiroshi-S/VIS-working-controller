<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TrainingPlan.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.externalclubvision.initialscreens.TrainingPlan" %>
<%@ Register TagPrefix="ddlb" Assembly="OptionDropDownList" Namespace="OptionDropDownList" %>

<script type="text/javascript" language="javascript">

    $(document).ready(function () {

        $('#exWeekPlan>tbody>tr>td:nth-child(2)').addClass('exSelects');

        $('.exSelects select').css("width", 135);

        //beginner
        $('.exSelects select').find("option[value='90']:selected").each(function () {
            $(".exSelects select option[value='92']").attr("disabled", true);
            $(".exSelects select option[value='91']").attr("disabled", true);
            $(".exSelects select option[value='90']").attr("disabled", false);
            $("#ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_DropDownList8 option[value='2']").attr("disabled", true);
            $("#ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_DropDownList8 option[value='3']").attr("disabled", true);
            $(".selectedProg").html("You are on BEGINNER weight program. <a href=\"#\" onclick=\"clearSelectedPlan();\">Clear selected program</a>");
        });

        //intermediate 
        $('.exSelects select').find("option[value='91']:selected").each(function () {
            $(".exSelects select option[value='92']").attr("disabled", true);
            $(".exSelects select option[value='90']").attr("disabled", true);
            $(".exSelects select option[value='91']").attr("disabled", false);
            $("#ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_DropDownList8 option[value='1']").attr("disabled", true);
            $("#ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_DropDownList8 option[value='3']").attr("disabled", true);
            $(".selectedProg").html("You are on INTERMEDIATE weight program. <a href=\"#\" onclick=\"clearSelectedPlan();\">Clear selected program</a>");
        });

        //advance
        $('.exSelects select').find("option[value='92']:selected").each(function () {
            $(".exSelects select option[value='90']").attr("disabled", true);
            $(".exSelects select option[value='91']").attr("disabled", true);
            $(".exSelects select option[value='92']").attr("disabled", false);
            $("#ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_DropDownList8 option[value='2']").attr("disabled", true);
            $("#ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_DropDownList8 option[value='1']").attr("disabled", true);
            $(".selectedProg").html("You are on ADVANCED weight program. <a href=\"#\" onclick=\"clearSelectedPlan();\">Clear selected program</a>");
        });

        $('.exSelects select').change(function () {
            //beginner
            $(this).find("option[value='90']:selected").each(function () {
                $(".exSelects select option[value='92']").attr("disabled", true);
                $(".exSelects select option[value='91']").attr("disabled", true);
                $(".exSelects select option[value='90']").attr("disabled", false);
                $("#ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_DropDownList8 option[value='2']").attr("disabled", true);
                $("#ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_DropDownList8 option[value='3']").attr("disabled", true);
                $(".selectedProg").html("You are on BEGINNER weight program. <a href=\"#\" onclick=\"clearSelectedPlan();\">Clear selected program</a>");
                if ($("#FLBeginnerExs").data("exs") != "full") {
                    $("#ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_fatlossbeginner").slideToggle('slow', function () { });
                    $("#exercisePlannerDiv").slideToggle('slow', function () { });
                }
            });

            //intermediate
            $(this).find("option[value='91']:selected").each(function () {
                $(".exSelects select option[value='92']").attr("disabled", true);
                $(".exSelects select option[value='90']").attr("disabled", true);
                $(".exSelects select option[value='91']").attr("disabled", false);
                $("#ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_DropDownList8 option[value='1']").attr("disabled", true);
                $("#ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_DropDownList8 option[value='3']").attr("disabled", true);
                $(".selectedProg").html("You are on INTERMEDIATE weight program. <a href=\"#\" onclick=\"clearSelectedPlan();\">Clear selected program</a>");
                if ($("#FLIntermediateExs").data("exs") != "full") {
                    $("#ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_fatlossintermediate").slideToggle('slow', function () { });
                    $("#exercisePlannerDiv").slideToggle('slow', function () { });
                }
            });

            //advance
            $(this).find("option[value='92']:selected").each(function () {
                $(".exSelects select option[value='90']").attr("disabled", true);
                $(".exSelects select option[value='91']").attr("disabled", true);
                $(".exSelects select option[value='92']").attr("disabled", false);
                $("#ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_DropDownList8 option[value='2']").attr("disabled", true);
                $("#ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_DropDownList8 option[value='1']").attr("disabled", true);
                $(".selectedProg").html("You are on ADVANCED weight program. <a href=\"#\" onclick=\"clearSelectedPlan();\">Clear selected program</a>");
                if ($("#FLAdvanceExs").data("exs") != "full") {
                    $("#fatlossadvance").slideToggle('slow', function () {
                    });
                    $("#exercisePlannerDiv").slideToggle('slow', function () {
                    });
                }
            });
        }); //.change();

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
        */
        $(".reviewSuggestedPlans").click(function () {
            $('#ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_DropDownList8').val('0').change();
            $("#exercisePlannerDiv").slideToggle('slow', function () { });
            $("#reviewWorkOutDiv").slideToggle('slow', function () { });
            return false;
        });

        $("#ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_DropDownList8").change(function () {
            var val = $(this).val();

            switch (val) {
                case "1":
                    $("#ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_fatlossbeginner").slideToggle('slow', function () { });
                    $("#reviewWorkOutDiv").slideToggle('slow', function () { });
                    break;
                case "2":
                    $("#ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_fatlossintermediate").slideToggle('slow', function () { });
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

        $(".gotoReviewScreen").click(function () {
            $('#ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_DropDownList8').val('0').change();
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
    
    #ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_DropDownList6
    #ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_DropDownList31
    #ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_DropDownList36
    #ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_DropDownList11
    #ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_DropDownList41
    #ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_DropDownList16
    #ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_DropDownList56
    #ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_DropDownList1
    #ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_DropDownList66
    #ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_DropDownList21
    #ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_DropDownList76
    #ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_DropDownList26
    #ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_DropDownList86
    #ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_DropDownList96
    {
        width: 160px;
    }
    
    .trainingplan select{
    width: 60px ;
}
</style>

<div id="exercisePlannerDiv" class="trainingplan" style="margin-right: 10px !important;">
    <span style="color: #E27423;"><h2>STEP 6</h2></span><br/>
    <h3>Weekly Exercise Planner</h3>
    
    <div id="explannermessage" style="height: 60px;width: 360px;display: block;float: left;">
         <div style="float: left;display: block;margin-right: 20px;">
          <a ><img src="/images/ExtClubVision/running_icon.png" width="48px" height="48px"/> </a>
        </div> 
        <div style="float: left;display: block; margin-top: 17px;">
          <h3>Learn How to Create Exercise Planner</h3>
        </div> 
    </div>
    
    <div class="target" style="display: block; width: 583px; float: left; ">
        <br/>
        <div style="background-color: #EDEBEB; ">
            <p style="color: #e27423;font-weight: bold;">Firstly – Creating your Weights Program</p> <br/>
            <p>The foundation of any exercise program for weight loss is your Weights or Resistance Training Program. By clicking “Select your Weight Training Program” we will create a plan that suits your level. It is recommended that you spread your weights sessions over the week for recovery purposes. You may change the days that we suggest that you perform your Weights Sessions by simply clicking on the drop down boxes for each category and scroll through the list of items to make your selection.</p> <br/>
                
        </div><br/>
        <div class="reviewSuggestedPlans" style="height: 39px;width: 147px;display: block;float: left;cursor: hand; cursor: pointer;-moz-border-radius: 0px 0px 0px 5px;
                                                                                                                                                                                                                                                                                                                                                                                                                                 border-radius: 3px; background-color: #999999;color: white; margin-bottom: 30px;text-align: center;padding-top: 8px;">
                    Select your Weight Training Program
        </div>
        <br/>
    </div>
       

   <br/><br/><br/>
   
   <div class="target" style="display: block; width: 583px; float: left;">
       <br/>
           <div style="background-color: #EDEBEB; ">
              <p style="color: #e27423;font-weight: bold;">Next – Add Cardio</p> <br/>
               <p>To complete your exercise planner you need to add cardio exercises. Simply click on the drop down boxes on each and scroll through the list of cardio exercises that you wish to do to make your selection. On days that you do not exercise, simply  select “Rest Day”. It is advisable to plan your cardio sessions so that you have a day of rest following a weights session to allow for muscles to recover.</p> <br/>
                
            </div><br/>
   </div>
    <table id="exWeekPlan">
            <tr style="text-align: left;font-weight: bolder">
                <td>Weekday</td>
                <td>Exercise</td>
                <td>When</td>
                <td>Dur(mins)</td>
                <td>Intensity</td>
                <td></td>
            </tr>
            <tr id="monday" runat="server">
                <td>Monday</td>
                <td id="mo1">
                    <ddlb:OptionGroupSelect CssClass="tprow1" ID="DropDownList6" 
                    runat="server" width="154" onvaluechanged="DropDownList6_ValueChanged">
                    </ddlb:OptionGroupSelect>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList7" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList9" runat="server">
                    </asp:DropDownList>
                </td>
                
                <td>
                    <asp:DropDownList ID="DropDownList10" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <input id="Button23" type="button" value="Add" onclick="document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_monday2row').style.display = 'table-row';return false; " />

                </td>
            </tr>
            <tr id="monday2row" runat="server" style="display: none" >
                <td></td>
                <td id="mo2">
                    <ddlb:OptionGroupSelect ID="DropDownList36" CssClass="tprow1" width="154" 
                        runat="server" >
                    </ddlb:OptionGroupSelect>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList37" runat="server">
                    </asp:DropDownList>
                </td>
                
                <td>
                    <asp:DropDownList ID="DropDownList39" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList40" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <input id="Button24" type="button" value="Delete" onclick="document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_monday2row').style.display = 'none';   " />
                </td>
            </tr>
                
            <tr id="tuesday" runat="server">
                <td>Tuesday</td>
                <td id="tue1">
                    <ddlb:OptionGroupSelect ID="DropDownList11"  CssClass="tprow1" width="154" runat="server" >
                    </ddlb:OptionGroupSelect>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList12" runat="server">
                    </asp:DropDownList>
                </td>
                
                <td>
                    <asp:DropDownList ID="DropDownList14" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList15" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <input id="Button22" type="button" value="Add" onclick="document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_tuesday2row').style.display = 'table-row';" />
                </td>
            </tr>
            <tr id="tuesday2row" runat="server" style="display: none" >
                <td></td>
                <td id="tue2">
                    <ddlb:OptionGroupSelect ID="DropDownList41"  CssClass="tprow1" width="154" runat="server">
                    </ddlb:OptionGroupSelect>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList42" runat="server">
                    </asp:DropDownList>
                </td>
                
                <td>
                    <asp:DropDownList ID="DropDownList44" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList45" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <input id="Button1" type="button" value="Delete" onclick="document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_tuesday2row').style.display = 'none';" />
                </td>
            </tr>
                
            <tr id="wednesday" runat="server">
                <td>Wednesday</td>
                <td id="wed1">
                    <ddlb:OptionGroupSelect ID="DropDownList16" CssClass="tprow1" width="154" runat="server">
                    </ddlb:OptionGroupSelect>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList17" runat="server">
                    </asp:DropDownList>
                </td>
                
                <td>
                    <asp:DropDownList ID="DropDownList19" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList20" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <input id="Button27" type="button" value="Add" onclick="document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_wednesday2row').style.display = 'table-row';  " />
                </td>
            </tr>
            <tr id="wednesday2row" runat="server" style="display: none" >
                <td></td>
                <td id="wed2">
                    <ddlb:OptionGroupSelect ID="DropDownList56" CssClass="tprow1" width="154" runat="server">
                    </ddlb:OptionGroupSelect>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList57" runat="server">
                    </asp:DropDownList>
                </td>
               
                <td>
                    <asp:DropDownList ID="DropDownList59" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList60" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <input id="Button28" type="button" value="Delete" onclick="document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_wednesday2row').style.display = 'none';" />
                </td>
            </tr>
                
            <tr id="thursday" runat="server">
                <td>Thursday</td>
                <td id="thu1">
                    <ddlb:OptionGroupSelect ID="DropDownList1" CssClass="tprow1" width="154" runat="server">
                    </ddlb:OptionGroupSelect>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList2" runat="server">
                    </asp:DropDownList>
                </td>
                
                <td>
                    <asp:DropDownList ID="DropDownList4" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList5" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <input id="Button30" type="button" value="Add" onclick="document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_thursday2row').style.display = 'table-row';    " />
                </td>
            </tr>
            <tr id="thursday2row" runat="server" style="display: none" >
                <td></td>
                <td id="thu2">
                    <ddlb:OptionGroupSelect ID="DropDownList66" CssClass="tprow1" width="154" runat="server">
                    </ddlb:OptionGroupSelect>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList67" runat="server">
                    </asp:DropDownList>
                </td>
                
                <td>
                    <asp:DropDownList ID="DropDownList69" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList70" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <input id="Button31" type="button" value="Delete" onclick="document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_thursday2row').style.display = 'none';   " />
                </td>
            </tr>
                
            <tr id="friday" runat="server">
                <td>Friday</td>
                <td id="fri1">
                    <ddlb:OptionGroupSelect ID="DropDownList21" CssClass="tprow1" width="154" runat="server">
                    </ddlb:OptionGroupSelect>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList22" runat="server">
                    </asp:DropDownList>
                </td>
                
                <td>
                    <asp:DropDownList ID="DropDownList24" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList25" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <input id="Button33" type="button" value="Add" onclick="document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_friday2row').style.display = 'table-row';" />
                </td>
            </tr>
            <tr id="friday2row" runat="server" style="display: none" >
                <td></td>
                <td id="fri2">
                    <ddlb:OptionGroupSelect ID="DropDownList76" CssClass="tprow1" width="154" runat="server">
                    </ddlb:OptionGroupSelect>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList77" runat="server">
                    </asp:DropDownList>
                </td>
                
                <td>
                    <asp:DropDownList ID="DropDownList79" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList80" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <input id="Button34" type="button" value="Delete" onclick="document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_friday2row').style.display = 'none';" />
                </td>
            </tr>
                
            <tr id="saturday" runat="server">
                <td>Saturday</td>
                <td id="sat1">
                    <ddlb:OptionGroupSelect ID="DropDownList26" CssClass="tprow1" width="154" runat="server">
                    </ddlb:OptionGroupSelect>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList27" runat="server">
                    </asp:DropDownList>
                </td>
                
                <td>
                    <asp:DropDownList ID="DropDownList29" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList30" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <input id="Button36" type="button" value="Add" onclick="document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_saturday2row').style.display = 'table-row';" />
                </td>
            </tr>
            <tr id="saturday2row" runat="server" style="display: none" >
                <td></td>
                <td id="sat2">
                    <ddlb:OptionGroupSelect ID="DropDownList86" CssClass="tprow1" width="154" runat="server">
                    </ddlb:OptionGroupSelect>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList87" runat="server">
                    </asp:DropDownList>
                </td>
                
                <td>
                    <asp:DropDownList ID="DropDownList89" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList90" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <input id="Button37" type="button" value="Delete" onclick=" document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_saturday2row').style.display = 'none';" />
                </td>
            </tr>
                
            <tr id="sunday" runat="server">
                <td>Sunday</td>
                <td id="sun1">
                    <ddlb:OptionGroupSelect ID="DropDownList31" CssClass="tprow1" width="154" runat="server">
                    </ddlb:OptionGroupSelect>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList32" runat="server">
                    </asp:DropDownList>
                </td>
                
                <td>
                    <asp:DropDownList ID="DropDownList34" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList35" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <input id="Button39" type="button" value="Add" onclick="document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_sunday2row').style.display = 'table-row';      " />
                </td>
            </tr>
            <tr id="sunday2row" runat="server" style="display: none" >
                <td></td>
                <td id="sun2">
                    <ddlb:OptionGroupSelect ID="DropDownList96" CssClass="tprow1" width="154" runat="server">
                    </ddlb:OptionGroupSelect>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList97" runat="server">
                    </asp:DropDownList>
                </td>
                
                <td>
                    <asp:DropDownList ID="DropDownList99" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList100" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <input id="Button40" type="button" value="Delete" onclick="document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_EditScreensContentPlaceHolderMain_MyMeasurementsTabs_4_tp_sunday2row').style.display = 'none';" />
                </td>
            </tr>
                
        </table>
        
    <div class="selectedProg" style="font-weight: bold;padding-top: 20px;"></div>        
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate><br/>
            <div class="target" style="display: block; width: 583px; float: left;">
                   <br/>
                       <div style="background-color: #EDEBEB; ">
                          <p style="color: #e27423;font-weight: bold;">Finally – Check that your created program meets your weekly requirements</p> <br/>
                           <p>Please note: Only entries that have a selection for each section will be included in the calculation. Entries that are not completed in full will appear as a Rest Day in your final Training Diary so to make sure all exercises are included in your final Training Diary please complete in full before proceeding. Once you have completed your entries click “Calculate” to ensure that your program meets your weekly requirements.</p> <br/>
                
                        </div><br/>
               </div>
            <br/><br/>
            <asp:ImageButton ID="TrainingPlannerCalculate" 
                ImageUrl="/images/ExtClubVision/buttonCalculate.gif" runat="server"
                onclick="TrainerButtonCalculate_Click"/><br/>
                
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
            
            <br/><br/>
            <div id="WEPSummary" runat="server" class="WeeklySummary">
                <h3>Exercise Summary</h3>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            Actual total cardio
                        </td>
                        <td>
                            <asp:TextBox ID="ActualTotalCardioTextBox" runat="server" Width="40px" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td>
                            Total cardio required
                        </td>
                        <td>
                            <asp:TextBox ID="TotalCardioReqTextBox" runat="server" Width="40px" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td>
                            Total cardio achieved
                            </td>
                        <td>
                            <asp:TextBox ID="TotalCardioAchievedTextBox" runat="server" Width="40px" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Actual Hard Cardio
                        </td>
                        <td>
                            <asp:TextBox ID="ActualHardCardioTextBox" runat="server" Width="40px" ReadOnly="True" ></asp:TextBox>
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
                            <asp:TextBox ID="HardCardioAchievedTextBox" runat="server" Width="40px" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Actual L-M Cardio
                        </td>
                        <td>
                            <asp:TextBox ID="ActualLMCardioTextBox" runat="server" Width="40px" ReadOnly="True" ></asp:TextBox>
                        </td>
                        <td>
                            L-M Cardio Required
                        </td>
                        <td>
                            <asp:TextBox ID="LMCardioReqTextBox" runat="server" Width="40px" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td>
                            L-M Cardio Achieved
                        </td>
                        <td>
                            <asp:TextBox ID="LMCardioAchievedTextBox" runat="server" Width="40px" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Actual Weights
                        </td>
                        <td>
                            <asp:TextBox ID="ActualWeightsTextBox" runat="server" Width="40px" ReadOnly="True"></asp:TextBox>
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
                            <asp:TextBox ID="WeightsPtAchievedTextBox" runat="server" Width="40px" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            Is the above Training Plan realistic for you to complete? 
                        </td>
                        <td colspan="3">
                            <asp:DropDownList ID="DropDownList3" runat="server">
                                <asp:ListItem runat="server" Value="0" Text=""></asp:ListItem>
                                <asp:ListItem runat="server" Value="1" Text="YES"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <br/><br/>
            </div>
            
            <div id="messageboard" runat="server" style="display: none; background-color: #EDEBEB;">
                <h2 style="color: #CD0921;">Please Note</h2>
                
                <p id="p1" runat="server">You have not selected any exercises</p>
                <p id="p2" runat="server">
                   We want to ensure you give yourself every chance of achieving your weight loss goal within the next 9 weeks and the recommendations for the cardio and weights requirements are based on your goals set.  
                   Should you feel that you are not able to meet the activity levels recommended then we strongly recommend you readjust your goal.  
                   By making smaller, more manageable changes that integrate easily into your current lifestyle you can be confident of staying on track with your weight loss goals.
                
                </p>
            </div>
            <br/><br/>
            <div style="width: auto; height: 50px ">
                <div style="float: left">
                    <asp:imagebutton ID="MyGoalImagebuttonBack" ImageUrl="/images/buttonBack.gif" 
                        runat="server" OnClick="MyGoalImagebuttonBack_Click" ></asp:imagebutton>
                </div>
    
                <div style="float: right">
                    <asp:imagebutton ID="MyGoalImagebuttonNext" ImageUrl="/images/buttonSaveAndNext.gif" 
                        runat="server"
                        onclick="MyGoalImagebuttonNext_Click"></asp:imagebutton>
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
    <asp:DropDownList ID="DropDownList8" runat="server" CssClass="tprow1" width="154">
        <asp:ListItem runat="server" Text="" Value="0"></asp:ListItem>
        <asp:ListItem runat="server" Text="Weights Beginner" Value="1"></asp:ListItem>
        <asp:ListItem runat="server" Text="Weights Intermediate" Value="2"></asp:ListItem>
        <asp:ListItem runat="server" Text="Weights Advanced" Value="3"></asp:ListItem>
    </asp:DropDownList>
    <div class="selectedProg" style="font-weight: bold;padding-top: 20px;"></div>   
    <br/><br/>
    <a href="#" class="reviewSuggestedPlans">>> Close <<</a>

</div>

<div id="explanoptions">
<div id="fatlossbeginner" class="trainingplan" style="margin-right: 10px !important;display: none;" runat="server">
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
   
   <h3>Select Exercises</h3>
    <table id="FLBeginnerExs">
        <tr>
            <td>Area</td>
            <td>Exercise</td>
        </tr>
        <tr>
            <td>Legs</td>
            <td><div style="float: left; width: 50px; height: 60px;display: block;">
                    <img src="/images/Video-icon-orange.png" width="16" onclick="lungesVid();"/><br/>
                    <img src="/images/Video-icon-orange.png" width="16" onclick="squatsVid();"/><br/>
                    <img src="/images/Video-icon-orange.png" width="16" onclick="stepUpsVid();"/>
                </div>
                <div style="float: left; width: 150px; height: 60px;display: block;">
                    <asp:ListBox ID="ListBox1" runat="server" SelectionMode="Single"></asp:ListBox>  
                </div>
            </td>
            
        </tr>
        <tr>
            <td>Upper - Push</td>
            <td>
                <div style="float: left; width: 50px; height: 60px;display: block;">
                    <img src="/images/Video-icon-orange.png" width="16" onclick="pushUpsVids();"/><br/>
                    <img src="/images/Video-icon-orange.png" width="16" onclick="shoulderPressesVid();"/><br/>
                    <img src="/images/Video-icon-orange.png" width="16" onclick="tricepDipsVid();"/>
                </div>
                <div style="float: left; width: 150px; height: 60px;display: block;">
                    <asp:ListBox ID="ListBox2" runat="server" SelectionMode="Single"></asp:ListBox>  
                </div>
                
            </td>
           
        </tr>
        <tr>
            <td>Upper - Pull</td>
            <td>
                <div style="float: left; width: 50px; height: 60px;display: block;">
                    <img src="/images/Video-icon-orange.png" width="16" onclick="pullUpsVids();"/><br/>
                    <img src="/images/Video-icon-orange.png" width="16" onclick="resistanceRowVid();"/><br/>
                    <img src="/images/Video-icon-orange.png" width="16" onclick="uprightRowVid();"/>
                </div>
                <div style="float: left; width: 150px; height: 60px;display: block;">
                    <asp:ListBox ID="ListBox3" runat="server"></asp:ListBox>  
                </div>
            </td>
            
        </tr>
        <tr>
            <td>Core</td>
            <td>
                <div style="float: left; width: 50px; height: 60px;display: block;">
                    <img src="/images/Video-icon-orange.png" width="16" onclick="crunchesVid();"/><br/>
                    <img src="/images/Video-icon-orange.png" width="16" onclick="proneHoldsVid();"/>
                </div>
                <div style="float: left; width: 150px; height: 60px;display: block;">
                    <asp:ListBox ID="ListBox4" runat="server"></asp:ListBox>  
                </div>
            </td>
        </tr>
    </table>
    <br/><br/>
    
    <input id="Button2" type="button" value="Save" onclick="buttonFLBeginner();"/>
        
    <br/><br/>
    <a href="#" class="gotoReviewScreen">>> Close <<</a>
</div>

<div id="fatlossintermediate" class="trainingplan" style="margin-right: 10px !important;display: none;" runat="server">
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
    <table id="FLIntermediateExs">
        <tr>
            <td>Area</td>
            <td>Exercise</td>
        </tr>
        <tr>
            <td>Legs</td>
            <td>
                <div style="float: left; width: 50px; height: 60px;display: block;">
                    <img src="/images/Video-icon-orange.png" width="16" onclick="lungesVid();"/><br/>
                    <img src="/images/Video-icon-orange.png" width="16" onclick="squatsVid();"/><br/>
                    <img src="/images/Video-icon-orange.png" width="16" onclick="stepUpsVid();"/>
                </div>
                <div style="float: left; width: 150px; height: 60px;display: block;">
                    <asp:ListBox ID="exListBoxBeginner1" runat="server" SelectionMode="Multiple"></asp:ListBox>  
                </div>
            </td>
        </tr>
        <tr>
            <td>Upper - Push</td>
            <td>
                <div style="float: left; width: 50px; height: 60px;display: block;">
                    <img src="/images/Video-icon-orange.png" width="16" onclick="pushUpsVids();"/><br/>
                    <img src="/images/Video-icon-orange.png" width="16" onclick="shoulderPressesVid();"/><br/>
                    <img src="/images/Video-icon-orange.png" width="16" onclick="tricepDipsVid();"/>
                </div>
                <div style="float: left; width: 150px; height: 60px;display: block;">
                    <asp:ListBox ID="exListBoxBeginner2" runat="server" SelectionMode="Multiple"></asp:ListBox>  
                </div>
            </td>
        </tr>
        <tr>
            <td>Upper - Pull</td>
            <td>
                <div style="float: left; width: 50px; height: 60px;display: block;">
                    <img src="/images/Video-icon-orange.png" width="16" onclick="pullUpsVids();"/><br/>
                    <img src="/images/Video-icon-orange.png" width="16" onclick="resistanceRowVid();"/><br/>
                    <img src="/images/Video-icon-orange.png" width="16" onclick="uprightRowVid();"/>
                </div>
                <div style="float: left; width: 150px; height: 60px;display: block;">
                    <asp:ListBox ID="exListBoxBeginner3" runat="server" SelectionMode="Multiple"></asp:ListBox>  
                </div>
            </td>
        </tr>
        <tr>
            <td>Core</td>
            <td>
                <div style="float: left; width: 50px; height: 60px;display: block;">
                    <img src="/images/Video-icon-orange.png" width="16" onclick="crunchesVid();"/><br/>
                    <img src="/images/Video-icon-orange.png" width="16" onclick="proneHoldsVid();"/>
                </div>
                <div style="float: left; width: 150px; height: 60px;display: block;">
                    <asp:ListBox ID="exListBoxBeginner4" runat="server" SelectionMode="Single"></asp:ListBox>  
                </div>
            </td>
        </tr>
    </table>
    To select more than one exercise hold down the ctrl key
    <div class="message"></div>
    <br/><br/>
    <input id="Button3" type="button" value="Save" onclick="buttonFLIntermediate();"/>
    <br/><br/>
    <a href="#" class="gotoReviewScreen">>> Close <<</a>
</div>

<div id="fatlossadvance" class="trainingplan" style="margin-right: 10px !important;display: none;">
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
    <table id="FLAdvanceExs">
        <tr>
            <td>Area</td>
            <td>Exercise</td>
        </tr>
        <tr>
            <td>Legs</td>
            <td style="text-align: left">
                 <div style="float: left; width: 150px; height: 60px;display: block;">
                    <img src="/images/Video-icon-orange.png" width="16" onclick="lungesVid();"/>&nbsp;&nbsp;Lunges<br/>
                    <img src="/images/Video-icon-orange.png" width="16" onclick="squatsVid();"/>&nbsp;&nbsp;Squats<br/>
                    <img src="/images/Video-icon-orange.png" width="16" onclick="stepUpsVid();"/>&nbsp;&nbsp;Step Ups  
                </div>
            </td>
        </tr>
        <tr>
            <td>Upper - Push</td>
            <td style="text-align: left">
                 <div style="float: left; width: 150px; height: 60px;display: block;">
                    <img src="/images/Video-icon-orange.png" width="16" onclick="pushUpsVids();"/>&nbsp;&nbsp;Push Ups<br/>
                    <img src="/images/Video-icon-orange.png" width="16" onclick="shoulderPressesVid();"/>&nbsp;&nbsp;Shoulder Presses <br/>
                    <img src="/images/Video-icon-orange.png" width="16" onclick="tricepDipsVid();"/>&nbsp;&nbsp;Tricep Dips
                </div>
            </td>
        </tr>
        <tr>
            <td>Upper - Pull</td>
            <td style="text-align: left">
                <div style="float: left; width: 150px; height: 60px;display: block;">
                    <img src="/images/Video-icon-orange.png" width="16" onclick="pullUpsVids();"/>&nbsp;&nbsp;Pull Ups <br/>
                    <img src="/images/Video-icon-orange.png" width="16" onclick="resistanceRowVid();"/>&nbsp;&nbsp;Resistance Row <br/>
                    <img src="/images/Video-icon-orange.png" width="16" onclick="uprightRowVid();"/>&nbsp;&nbsp;Upright Rows
                </div>
            </td>
        </tr>
        <tr>
            <td>Core</td>
            <td style="text-align: left">
                <div style="float: left; width: 150px; height: 60px;display: block;">
                    <img src="/images/Video-icon-orange.png" width="16" onclick="crunchesVid();"/>&nbsp;&nbsp;Crunches <br/>
                    <img src="/images/Video-icon-orange.png" width="16" onclick="proneHoldsVid();"/>&nbsp;&nbsp;Prone Holds
                </div>
            </td>
        </tr>
    </table>
    <input id="Button4" type="button" value="Save" onclick="buttonFLAdvance();"/>
    <br/><br/>
    <a href="#" class="gotoReviewScreen">>> Close <<</a>
</div>

</div>