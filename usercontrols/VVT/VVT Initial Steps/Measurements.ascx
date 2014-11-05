<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Measurements.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.VVT.VVT_Initial_Steps.Measurements" %>

<style type="text/css">
.unitLabels
{
    margin-left:7px;
    font-weight:bold;
}
.bp {
    width: 40px !important;
    font-size: 13px;
    text-align: right;
    padding-right: 2px;
}
</style>

<script type="text/javascript">
    $(document).ready(function () {
    });
    function val(valTarget) {
        if (!valTarget.value.match(/^\-?([1-9]\d*|0)(\.\d?[1-9])?$/) || valTarget.value == "") {
            valTarget.value = "0";
            valTarget.focus();
            document.getElementById('txtNutritionCalc').value = "0.0";
            alert("Please Enter in Digits");
            return false;
        }
    }
    //24/09 Hiroshi
    function setFatManual() {
        if ($('#' + $(fatRate).parent().parent().parent().parent().attr('id') + ' #txtFatRate').val() != "") {
            $('#' + $(fatRate).parent().parent().parent().parent().attr('id') + ' #txtNutritionCalc').val($('#' + $(fatRate).parent().parent().parent().parent().attr('id') + ' #txtFatRate').val());
        }
        else {
            $('#' + $(fatRate).parent().parent().parent().parent().attr('id') + ' #txtNutritionCalc').val(" ");
        }
    }
    function clearFatRate(focused) {
        if ($("#" + $(focused).parent().parent().parent().parent().attr('id') + " #radioNo").attr("checked") == "checked") {
            $('#' + $(focused).parent().parent().parent().parent().attr('id') + ' #txtNutritionCalc').val(" ");
        }
        else if ($("#" + $(focused).parent().parent().parent().parent().attr('id') + " #radioYes").attr("checked") == "checked") {
            $('#' + $(focused).parent().parent().parent().parent().attr('id') + ' #txtNutritionCalc').val(" ");
        }
    }
    function radioClick(fatRate) {
        if (fatRate.value == 'Y') {
            if ($('#' + $(fatRate).parent().parent().parent().parent().attr('id') + ' #txtFatRate').val() == "") {
                $('#' + $(fatRate).parent().parent().parent().parent().attr('id') + ' #txtNutritionCalc').val(" ");
            }
            $('#fatRate').fadeIn(500);
            $('#calcFat').hide();
            setFatManual();
        }
        if (fatRate.value == 'N') {
            if (($('#' + $(fatRate).parent().parent().parent().parent().attr('id') + ' #txtForeArm').val() == "") || ($('#' + $(fatRate).parent().parent().parent().parent().attr('id') + ' #txtWrist').val() == "")) {
                $('#' + $(fatRate).parent().parent().parent().parent().attr('id') + ' #txtNutritionCalc').val(" ");
            }
            $('#calcFat').fadeIn(500);
            $('#fatRate').hide();
            reCalcFat($(fatRate).parent().parent().parent().parent().attr('id'));
        }
    }
    function dropDownChanged(dropvalue) {
        popUpResettingPlan();
        $("#MeasurementContainer").find('input:text:enabled').val('');
        if (dropvalue.value == 'True') {
            $('#ContentPlaceHolderDefault_ContentPlaceHolderMain_ctl01_Measurements_3_WeightUnitLabel').text('kg');
            $('#ContentPlaceHolderDefault_ContentPlaceHolderMain_ctl01_Measurements_3_ChestUnitLabel').text('cm');
            $('#ContentPlaceHolderDefault_ContentPlaceHolderMain_ctl01_Measurements_3_WaistUnitLabel').text('cm');
            $('#ContentPlaceHolderDefault_ContentPlaceHolderMain_ctl01_Measurements_3_HipsUnitLabel').text('cm');
            $('#ContentPlaceHolderDefault_ContentPlaceHolderMain_ctl01_Measurements_3_ForearmUnitLabel').text('cm');
            $('#ContentPlaceHolderDefault_ContentPlaceHolderMain_ctl01_Measurements_3_WristUnitLabel').text('cm');

            $('#ContentPlaceHolderDefault_ContentPlaceHolderMain_NewGoalMeasurementsLightBox_3_ms_WeightUnitLabel').text('kg');
            $('#ContentPlaceHolderDefault_ContentPlaceHolderMain_NewGoalMeasurementsLightBox_3_ms_ChestUnitLabel').text('cm');
            $('#ContentPlaceHolderDefault_ContentPlaceHolderMain_NewGoalMeasurementsLightBox_3_ms_WaistUnitLabel').text('cm');
            $('#ContentPlaceHolderDefault_ContentPlaceHolderMain_NewGoalMeasurementsLightBox_3_ms_HipsUnitLabel').text('cm');
            $('#ContentPlaceHolderDefault_ContentPlaceHolderMain_NewGoalMeasurementsLightBox_3_ms_ForearmUnitLabel').text('cm');
            $('#ContentPlaceHolderDefault_ContentPlaceHolderMain_NewGoalMeasurementsLightBox_3_ms_WristUnitLabel').text('cm');
        }
        if (dropvalue.value == 'False') {
            $('#ContentPlaceHolderDefault_ContentPlaceHolderMain_ctl01_Measurements_3_WeightUnitLabel').text('pounds');
            $('#ContentPlaceHolderDefault_ContentPlaceHolderMain_ctl01_Measurements_3_ChestUnitLabel').text('inches');
            $('#ContentPlaceHolderDefault_ContentPlaceHolderMain_ctl01_Measurements_3_WaistUnitLabel').text('inches');
            $('#ContentPlaceHolderDefault_ContentPlaceHolderMain_ctl01_Measurements_3_HipsUnitLabel').text('inches');
            $('#ContentPlaceHolderDefault_ContentPlaceHolderMain_ctl01_Measurements_3_ForearmUnitLabel').text('inches');
            $('#ContentPlaceHolderDefault_ContentPlaceHolderMain_ctl01_Measurements_3_WristUnitLabel').text('inches');

            $('#ContentPlaceHolderDefault_ContentPlaceHolderMain_NewGoalMeasurementsLightBox_3_ms_WeightUnitLabel').text('pounds');
            $('#ContentPlaceHolderDefault_ContentPlaceHolderMain_NewGoalMeasurementsLightBox_3_ms_ChestUnitLabel').text('inches');
            $('#ContentPlaceHolderDefault_ContentPlaceHolderMain_NewGoalMeasurementsLightBox_3_ms_WaistUnitLabel').text('inches');
            $('#ContentPlaceHolderDefault_ContentPlaceHolderMain_NewGoalMeasurementsLightBox_3_ms_HipsUnitLabel').text('inches');
            $('#ContentPlaceHolderDefault_ContentPlaceHolderMain_NewGoalMeasurementsLightBox_3_ms_ForearmUnitLabel').text('inches');
            $('#ContentPlaceHolderDefault_ContentPlaceHolderMain_NewGoalMeasurementsLightBox_3_ms_WristUnitLabel').text('inches');
        }
        //reCalcFat($(dropvalue).parent().parent().parent().parent().attr('id'));
    }
</script>


<div id="MyMeasurementsDiv" runat="server" style="margin-top: 64px;">
<div id="measurediv" class="measureclass" style="margin-right: 10px !important;">
<div style="float: left;display: block;margin-bottom: 30px;" id="measHeaderButton" runat="server">
    <%--<span style="color: #E27423;"><h2><asp:Label ID="StepLabel" runat="server" Text="STEP 4"></asp:Label></h2></span>--%>
    <br/>
    <div style="display: block;float: left;width: 400px;height: 30px;">
        <h2>Measurement Date : <asp:Label ID="DateMeasuredLabel" runat="server" Text="Label"></asp:Label></h2>
    </div>
    <div class="clear"></div>
    <div id="measurementHeader1" runat="server" style="float: left;display: block">
       
        <asp:Button ID="PreviousButton" runat="server" Text="<< Previous" CssClass="button-small vision_red_reverse rounded3" OnClick="PreviousButton_Click" />
    </div>

 <!-- added on 20/08/2013 Hiroshi
      add corresponding button image file-->
    <div id="Div1" runat="server" style="float: left;display: block">
       
        <asp:Button ID="NextImageButton" runat="server" Text="Next >>" CssClass="button-small vision_red_reverse rounded3" onclick="NextImageButton_Click" />
    </div>

    <div id="measurementHeader2" runat="server" style="float: left;display: block;" >
      
        <asp:Button ID="CreateNewButton" runat="server" Text="Create New"
            CssClass="button-small vision_red rounded3" OnClientClick="popUpResettingPlan();return false;" />
       <!-- <button id="measCreateNew" onclick="testNewLightBox();return false;" class="thoughtbot">Create New</button>  -->
     </div>   
</div>

<%--<br/><br /><br /><br/><br/><br/>--%>

<div id="myMeasurementsContentDiv" runat="server" style="display: block; margin-top: 0px;">
    
    <div style="height: 60px;width: 500px;display: block;float: left;cursor: hand; cursor: pointer;" onclick="measurementsVid();">
         <div style="float: left;display: block;margin-right: 20px;">
          <img src="/images/icons/web/mediaplay.png" alt="image"/>
        </div> 
        <div style="float: left;display: block; margin-top: -5px;">
          <div class="button-small vision_navy rounded3">>> Learn How to Measure Yourself <<</div> 
        </div> 
    </div>
   <br/>
    
<table style="width: 500px; margin-bottom: 30px;">
    <tr>
        <td>Preferred format</td>
        <td><asp:DropDownList ID="isMetricDdl" Width="200" runat="server" ClientIDMode="Static" onchange="dropDownChanged(this);" >
                <asp:ListItem runat="server" Value="True" Text="Metric (cm/kg)"></asp:ListItem>
                <asp:ListItem runat="server" Value="False" Text="Imperial (inches/pounds)"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
</table>
   <br/>
<asp:Label ID="txtMeasurementsWarning"  ForeColor="red" runat="server" Text=""></asp:Label>
<asp:ValidationSummary ID="msrValidationSummary" runat="server" ForeColor="red" HeaderText="* Please enter values in correct format" ValidationGroup="msr"/>

<div id="MeasurementContainer">
    <div id="left">
        <div>
            <h3>Measurements</h3>
        </div>
        <div>
            <span>Body Weight</span>
            <asp:TextBox ID="txtBodyWeight" ClientIDMode="Static"  runat="server" onfocus="javascript:clearFatRate(this);" onblur="javascript:reCalcFat($(this).parent().parent().parent().parent().attr('id'));"></asp:TextBox><asp:Label ID="WeightUnitLabel" runat="server" cssClass="unitLabels" Width="20px" />
            <asp:RegularExpressionValidator ID="txtBodyWeightRegularExpressionValidator" runat="server" ErrorMessage="" Text="*" ForeColor="red" 
                                            ControlToValidate="txtBodyWeight"
                                            ValidationExpression="^[-+]?[0-9]*\.?[0-9]*([eE][-+]?[0-9]+)?$"
                                            ValidationGroup="msr"
                                            Display="Dynamic">
                                            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="txtBodyWeightRequiredFieldValidator" runat="server" ErrorMessage="" Text="*" ForeColor="red"
                                        ControlToValidate="txtBodyWeight"
                                        ValidationGroup="msr"
                                        Display="Dynamic">
                                        </asp:RequiredFieldValidator>
        </div>
        <div>
            <span>Chest</span>
            <asp:TextBox ID="txtChest" runat="server"></asp:TextBox><asp:Label ID="ChestUnitLabel" onfocus="javascript:clearFatRate(this);" runat="server" cssClass="unitLabels" Width="20px"/>
            <asp:RegularExpressionValidator ID="txtChestRegularExpressionValidator" runat="server" ErrorMessage="" Text="*" ForeColor="red"
                                            ControlToValidate="txtChest"
                                            ValidationExpression="^[-+]?[0-9]*\.?[0-9]*([eE][-+]?[0-9]+)?$"
                                            ValidationGroup="msr"
                                            Display="Dynamic">
                                            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="txtChestRequiredFieldValidator" runat="server" ErrorMessage="" Text="*" ForeColor="red"
                                        ControlToValidate="txtChest"
                                        ValidationGroup="msr"
                                        Display="Dynamic">
                                        </asp:RequiredFieldValidator>   
        </div>
        <div>
            <span>Waist</span>
            <asp:TextBox ID="txtWaist" ClientIDMode="Static" runat="server" onfocus="javascript:clearFatRate(this);" onblur="javascript:reCalcFat($(this).parent().parent().parent().parent().attr('id'));"></asp:TextBox><asp:Label ID="WaistUnitLabel" runat="server" cssClass="unitLabels" Width="20px"/>
            <asp:RegularExpressionValidator ID="txtWaistRegularExpressionValidator" runat="server" ErrorMessage="" Text="*" ForeColor="red"
                                            ControlToValidate="txtWaist"
                                            ValidationExpression="^[-+]?[0-9]*\.?[0-9]*([eE][-+]?[0-9]+)?$"
                                            ValidationGroup="msr"
                                            Display="Dynamic">
                                            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="txtWaistRequiredFieldValidator" runat="server" ErrorMessage="" Text="*" ForeColor="red"
                                        ControlToValidate="txtWaist"
                                        ValidationGroup="msr"
                                        Display="Dynamic">
                                        </asp:RequiredFieldValidator>
        </div>
        <div>     
            <span>Hips</span>
            <asp:TextBox ID="txtHips" ClientIDMode="Static"  runat="server" onfocus="javascript:clearFatRate(this);" onblur="javascript:reCalcFat($(this).parent().parent().parent().parent().attr('id'));"></asp:TextBox><asp:Label ID="HipsUnitLabel" runat="server" cssClass="unitLabels" Width="20px"/>
            <asp:RegularExpressionValidator ID="txtHipsRegularExpressionValidator" runat="server" ErrorMessage="" Text="*" ForeColor="red"
                                            ControlToValidate="txtHips"
                                            ValidationExpression="^[-+]?[0-9]*\.?[0-9]*([eE][-+]?[0-9]+)?$"
                                            ValidationGroup="msr"
                                            Display="Dynamic">
                                            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="txtHipsRequiredFieldValidator" runat="server" ErrorMessage="" Text="*" ForeColor="red"
                                        ControlToValidate="txtHips"
                                        ValidationGroup="msr"
                                        Display="Dynamic">
                                        </asp:RequiredFieldValidator>
        </div>
    </div>
    <div id="right">
        <div>
            <h3>Body Composition</h3>
        </div>
        <div>
            <span>Do you know your body fat % ?</span>
            <input id="radioYes" type="radio" name="fatRate" value="Y" onclick="radioClick(this)" runat="server" ClientIDMode="Static"/>Yes
            <input id="radioNo" type="radio" name="fatRate" value="N" onclick="radioClick(this)" runat="server" ClientIDMode="Static" />No
        </div>
        <div id="fatRate" ClientIDMode="Static" runat="server" style="display:none;">
            <span>Please enter the percentage</span>
            <asp:TextBox ID="txtFatRate" runat="server" ClientIDMode="Static" onfocus="javascript:clearFatRate(this);" onblur="javascript:setFatManual();" /><span class="unitLabels" style="width:20px;">%</span>
            <asp:RequiredFieldValidator ID="txtFatRateRequiredFieldValidator" runat="server" ErrorMessage="" Text="*" ForeColor="red"
                                            ControlToValidate="txtFatRate" Enabled="False"
                                            ValidationGroup="msr"
                                            Display="Dynamic">
                                            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="txtFatRateRegularExpressionValidator" runat="server" ErrorMessage="" Text="*" ForeColor="red"
                                                ControlToValidate="txtFatRate" Enabled="False"
                                                ValidationExpression="^[-+]?[0-9]*\.?[0-9]*([eE][-+]?[0-9]+)?$"
                                                ValidationGroup="msr"
                                                Display="Dynamic">
                                                </asp:RegularExpressionValidator>
        </div>
        <div id="calcFat" ClientIDMode="Static" runat="server" style="display:none;">
            <div>
                <span style="width:100%; line-height:22px;">Please enter the following to calculate your estimate fat %.</span>
            </div>
            <div>
                <span>Forearm</span>
                <asp:TextBox ID="txtForeArm" ClientIDMode="Static"  runat="server" onfocus="javascript:clearFatRate(this);" onblur="javascript:reCalcFat($(this).parent().parent().parent().parent().parent().attr('id'));"></asp:TextBox><asp:Label ID="ForearmUnitLabel" runat="server" cssClass="unitLabels" Width="20px"/>
                <asp:RegularExpressionValidator ID="txtForeArmRegularExpressionValidator" runat="server" ErrorMessage="" Text="*" ForeColor="red"
                                                ControlToValidate="txtForeArm" Enabled="False"
                                                ValidationExpression="^[-+]?[0-9]*\.?[0-9]*([eE][-+]?[0-9]+)?$"
                                                ValidationGroup="msr"
                                                Display="Dynamic">
                                                </asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="txtForeArmRequiredFieldValidator" runat="server" ErrorMessage="" Text="*" ForeColor="red"
                                            ControlToValidate="txtForeArm" Enabled="False"
                                            ValidationGroup="msr"
                                            Display="Dynamic">
                                            </asp:RequiredFieldValidator>
            </div>
            <div>
                <span>Wrist</span>
                <asp:TextBox ID="txtWrist" ClientIDMode="Static"  runat="server" onfocus="javascript:clearFatRate(this);" onblur="javascript:reCalcFat($(this).parent().parent().parent().parent().parent().attr('id'));"></asp:TextBox><asp:Label ID="WristUnitLabel" runat="server" cssClass="unitLabels" Width="20px"/>
                <asp:RegularExpressionValidator ID="txtWristRegularExpressionValidator" runat="server" ErrorMessage="" Text="*" ForeColor="red"
                                                ControlToValidate="txtWrist" Enabled="False"
                                                ValidationExpression="^[-+]?[0-9]*\.?[0-9]*([eE][-+]?[0-9]+)?$"
                                                ValidationGroup="msr"
                                                Display="Dynamic">
                                                </asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="txtWristRequiredFieldValidator" runat="server" ErrorMessage="" Text="*" ForeColor="red"
                                            ControlToValidate="txtWrist" Enabled="False"
                                            ValidationGroup="msr"
                                            Display="Dynamic">
                                            </asp:RequiredFieldValidator>
            </div>
        </div>
    </div>
    <div id="calcResult" style="clear:left;">
        Based on your measurements your estimated Body Fat % is 
        <asp:TextBox ID="txtNutritionCalc" ClientIDMode="Static"  CssClass="margin" placeholder="read only" Enabled="False" BorderColor="#ffffff" runat="server"></asp:TextBox> %
        <br />
        <span style="font-style:italic;margin-top:5px;">
            This figure is used to help calculate your macronutrient requirements.
        </span>
    </div>
</div>
<br/><br/>

<table style="margin-bottom: 30px;" id="bptable">
    <colgroup style="width: 12.5%"></colgroup>
    <colgroup style="width: 37.5%"></colgroup>
    <colgroup style="width: 12.5%"></colgroup>
    <colgroup style="width: 37.5%"></colgroup>
    <tr><td colspan="4"><h3>Optional Blood Pressure</h3></td></tr>
    <tr>
        <td>Systolic</td>
        <td>
            <asp:TextBox ID="txtBPSystolic" runat="server" placeholder="optional"></asp:TextBox>
            <asp:Label ID="BPSystolicUnitLabel" runat="server" Text="mmHg" CssClass="unitLabels"></asp:Label>
        </td>
        <td>Diastolic</td>
        <td>
            <asp:TextBox ID="txtBPDiastolyc" runat="server" placeholder="optional"></asp:TextBox>
            <asp:Label ID="BPDiastolycUnitLabel" runat="server" Text="mmHg" CssClass="unitLabels"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="4"> <span style="font-style:italic;margin-top:5px;">Normal Blood Pressure is 90-120(mmHg)/60-80(mmHg).</span></td>
    </tr>
</table>
<table id="visualMotivation">
    <colgroup style="width: 12.5%"></colgroup>
    <colgroup style="width: 37.5%"></colgroup>
    <colgroup style="width: 12.5%"></colgroup>
    <colgroup style="width: 37.5%"></colgroup>
    <tr>
        <td colspan="4">
            <h3 style="width: 130px;float: left">Visual Motivation</h3>
            <img src="/images/icons/Web/questionbuble.png" width="20px" style="float: left; cursor: pointer;" onclick="visualMotivationAlert();return false;"/>
        </td>
    </tr>
    <tr>
        <td style="width: 50px;">
            Photo
        </td>
        <td>
            <asp:Literal ID="measurementsPhotoLiteral" runat="server"></asp:Literal>
            <asp:FileUpload ID="fileUpload" runat="server" />
        </td>
        <td>
            Desired Item Clothing <img src="/images/icons/Web/questionbuble.png" width="20px" style="display: inline; cursor: pointer;margin-left: 10px;" onclick="desiredClothingAlert();return false;"/>
        </td>
        <td>
            <asp:TextBox ID="txtDesiredItemClothing" TextMode="MultiLine" Width="250px" Rows="3" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr id="rowShowProgressPhoto" runat="server">
        <td colspan="4">
            <a href="#" class="button-small vision_navy rounded3" onclick="document.getElementById('measurementsPhotoFlickr').style.display = 'block';document.getElementById('measurediv').style.display = 'none';">Review your progress photos</a>
        </td>
    </tr>
</table>

    <asp:HiddenField ID="HiddenField1" runat="server" ClientIDMode="Static" />

<br/><br/>
<div style="width: auto; height: 50px;display: none;" class="realButtonDiv">
    <div style="float: left">
        <asp:imagebutton ID="MeasureImagebuttonBack" ImageUrl="/images/buttonBack.gif" 
            runat="server" onclick="MeasureImagebuttonBack_Click"></asp:imagebutton>
    </div>
    
    <div style="float: right">
        <asp:imagebutton ID="MeasureImagebuttonNext" ImageUrl="/images/buttonSaveAndNext.gif" ClientIDMode="Static"
            runat="server" onclick="MeasureImagebuttonNext_Click" ></asp:imagebutton>
        <asp:imagebutton ID="MeasureImagebuttonNextUC" ImageUrl="/images/buttonSaveAndNext.gif" OnClientClick="testDewi234();"
            Visible="False" ClientIDMode="Static"></asp:imagebutton>
    </div>
</div>
<div class="istepsDiv">
    <div class="istepsWrapper">
        <div style="border-left: none;" onclick="window.open('/club-vision/account-setup/body-type/', '_self');">
            <div><img src="/images/icons/web/prevarrow.png" alt="picture"/></div>
            <div>Prev</div>
        </div>
        <div onclick="skipInitialSteps(4);return false;">
            <div><img src="/images/icons/web/skip.png" alt="picture"/></div>
            <div>Skip</div>
        </div>
        <div id="nextDiv" onclick="$('#MeasureImagebuttonNext').click();return false;">
            <div><img src="/images/icons/web/nextarrow.png" alt="picture"/></div>
            <div>Next</div>
        </div>
    </div>
</div>
</div>
</div>

<div id="measurementsPhotoFlickr" class="measureclass" style="display: none; margin-right: 10px !important;overflow: hidden;">
  <h3 style="margin-left: 10px;"><asp:Literal ID="LiteralFlickrHeading" runat="server"></asp:Literal></h3>

  <div id="mainphotoflickr" style="height: auto; overflow: hidden; ">
      <asp:Literal ID="literalmainphotoimg" runat="server"></asp:Literal>
  </div>

    <div id="fotoflickrcarousel" class="fotoflickr">
        <a id="mycarousel-prev" href="#"><img src="/images/buttonPrev.png" alt="Prev" /></a>
        <a id="mycarousel-next" href="#"><img src="/images/buttonNext.png" alt="Next" /></a>
            <ul id="menus" style="width: 952px">
                <asp:Literal ID="literalPhotoFlickr" runat="server"></asp:Literal>
            </ul>    
        <div class="clear">&nbsp;</div>
    </div>
    <br/><br/><br/>
    <div style="margin-left: 10px">
     <a href="#" class="button-small grey_dark_reverse rounded3"  onclick="document.getElementById('measurementsPhotoFlickr').style.display = 'none';document.getElementById('measurediv').style.display = 'block';">Close Photo Review</a>
    </div>
    
   
        <asp:TextBox ID="txtWaistHip" placeholder="read only" runat="server" Enabled="False" BorderColor="#ffffff" Visible="false" ></asp:TextBox> 
        
        
</div>
</div>

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

    $("#flat4").addClass("active");
</script>


