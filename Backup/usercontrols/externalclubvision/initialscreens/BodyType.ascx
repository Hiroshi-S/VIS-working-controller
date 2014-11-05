<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BodyType.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.externalclubvision.initialscreens.BodyType" %>
<style type="text/css">
    .result
    {
        margin-left:20px;
    }
</style>
<script type="text/javascript" language="javascript">
    $(document).ready(function () {
        $('#<%=BodyTypescoreLabel.ClientID %>').text(scoreSum());
        $("#explannermessage").click(function () {
            $(".target").slideToggle('slow', function () {

            });
            return false;
        });
    });
    function scoreSum() {
        var sum = parseFloat($('#<%=BodyTypeDropDownList1.ClientID %>').val()) + parseFloat($('#<%=BodyTypeDropDownList2.ClientID %>').val())
                    + parseFloat($('#<%=BodyTypeDropDownList3.ClientID %>').val()) + parseFloat($('#<%=BodyTypeDropDownList4.ClientID %>').val())
                    + parseFloat($('#<%=BodyTypeDropDownList5.ClientID %>').val()) + parseFloat($('#<%=BodyTypeDropDownList6.ClientID %>').val())
                    + parseFloat($('#<%=BodyTypeDropDownList7.ClientID %>').val()) + parseFloat($('#<%=BodyTypeDropDownList8.ClientID %>').val())
                    + parseFloat($('#<%=BodyTypeDropDownList9.ClientID %>').val()) + parseFloat($('#<%=BodyTypeDropDownList10.ClientID %>').val());
        return sum;
    }
    function bodyTypeDropDownChanged(ddid) {
        if ($('#<%=BodyTypeDropDownList1.ClientID %>').val() != 0 && $('#<%=BodyTypeDropDownList2.ClientID %>').val() != 0
         && $('#<%=BodyTypeDropDownList3.ClientID %>').val() != 0 && $('#<%=BodyTypeDropDownList4.ClientID %>').val() != 0
         && $('#<%=BodyTypeDropDownList5.ClientID %>').val() != 0 && $('#<%=BodyTypeDropDownList6.ClientID %>').val() != 0
         && $('#<%=BodyTypeDropDownList7.ClientID %>').val() != 0 && $('#<%=BodyTypeDropDownList8.ClientID %>').val() != 0
         && $('#<%=BodyTypeDropDownList9.ClientID %>').val() != 0 && $('#<%=BodyTypeDropDownList10.ClientID %>').val() != 0) {
//            var sum = parseFloat($('#<%=BodyTypeDropDownList1.ClientID %>').val()) + parseFloat($('#<%=BodyTypeDropDownList2.ClientID %>').val())
//                    + parseFloat($('#<%=BodyTypeDropDownList3.ClientID %>').val()) + parseFloat($('#<%=BodyTypeDropDownList4.ClientID %>').val())
//                    + parseFloat($('#<%=BodyTypeDropDownList5.ClientID %>').val()) + parseFloat($('#<%=BodyTypeDropDownList6.ClientID %>').val())
//                    + parseFloat($('#<%=BodyTypeDropDownList7.ClientID %>').val()) + parseFloat($('#<%=BodyTypeDropDownList8.ClientID %>').val())
//                    + parseFloat($('#<%=BodyTypeDropDownList9.ClientID %>').val()) + parseFloat($('#<%=BodyTypeDropDownList10.ClientID %>').val());

            $('#<%=BodyTypescoreLabel.ClientID %>').text(scoreSum());
            $('#<%=BodyTypelblBodyType.ClientID %>').text(calcBodyType(scoreSum()));
            $('#<%=BodyTypeLabel25.ClientID %>').text("");
            $('#msg').empty();
            $('#msg2').empty();
            $('#message ul').empty();
            //alert("Your Total Score is " + sum + "\nYour Body Type is " + calcBodyType(sum));
        }
        else {
            showUnanswered();
            //$('#<%=BodyTypescoreLabel.ClientID %>').text("");
            $('#<%=BodyTypelblBodyType.ClientID %>').text("");
        }
    }
    function validateInputs() {
        if ($('#<%=BodyTypeDropDownList1.ClientID %>').val() == 0 || $('#<%=BodyTypeDropDownList2.ClientID %>').val() == 0
         || $('#<%=BodyTypeDropDownList3.ClientID %>').val() == 0 || $('#<%=BodyTypeDropDownList4.ClientID %>').val() == 0
         || $('#<%=BodyTypeDropDownList5.ClientID %>').val() == 0 || $('#<%=BodyTypeDropDownList6.ClientID %>').val() == 0
         || $('#<%=BodyTypeDropDownList7.ClientID %>').val() == 0 || $('#<%=BodyTypeDropDownList8.ClientID %>').val() == 0
         || $('#<%=BodyTypeDropDownList9.ClientID %>').val() == 0 || $('#<%=BodyTypeDropDownList10.ClientID %>').val() == 0) {
            alert("Please answer to all questions.");
            return false;
        }
        else
        { return true; }
    }
    function showUnanswered() {
        var str = "Please answer the following questions to complete calculation.";
        var strr = "";
        if ($('#<%=BodyTypeDropDownList1.ClientID %>').val() == 0) {
            strr+='<li><span>Question 1</span></li>';
        }
        if ($('#<%=BodyTypeDropDownList2.ClientID %>').val() == 0) {
            strr+='<li><span>Question 2</span></li>';
        }
        if ($('#<%=BodyTypeDropDownList3.ClientID %>').val() == 0) {
            strr += '<li><span>Question 3</span></li>';
        }
        if ($('#<%=BodyTypeDropDownList4.ClientID %>').val() == 0) {
            strr += '<li><span>Question 4</span></li>';
        }
        if ($('#<%=BodyTypeDropDownList5.ClientID %>').val() == 0) {
            strr += '<li><span>Question 5</span></li>';
        }
        if ($('#<%=BodyTypeDropDownList6.ClientID %>').val() == 0) {
            strr += '<li><span>Question 6</span></li>';
        }
        if ($('#<%=BodyTypeDropDownList7.ClientID %>').val() == 0) {
            strr += '<li><span>Question 7</span></li>';
        }
        if ($('#<%=BodyTypeDropDownList8.ClientID %>').val() == 0) {
            strr += '<li><span>Question 8</span></li>';
        }
        if ($('#<%=BodyTypeDropDownList9.ClientID %>').val() == 0) {
            strr += '<li><span>Question 9</span></li>';
        }
        if ($('#<%=BodyTypeDropDownList10.ClientID %>').val() == 0) {
            strr += '<li><span>Question 10</span></li>';
        }
        $('#msg').empty();
        $('#msg2').empty();
        $('#msg').append("Body Type is calculated after answering all the questions.");
        $('#msg2').append(str);
        $('#message ul').empty();
        $('#message ul').append(strr);
    }
</script>
<div class="pbodytype" style="margin-right: 10px !important;">
    <span id="stepLabel" runat="server" style="color: #E27423;"><h2>STEP 3</h2></span><br/>
               
    <p align="justify" style="width: auto">
        Assessing your body type allows you to understand how your metabolism works and a more accurate nutritional program can be prescribed. </p>
    <p>
        Answer each question closest to the one that best describes you. If you fall between two categories, simply give a half score, eg 2.5.
    </p>

    <p  align="justify" style="width: auto">
        <asp:Label ID="BodyTypeLabel25" runat="server" Text="Label"
            ForeColor="Red" Visible="False"></asp:Label>
    </p>
    <table style="width: auto;" cellpadding="10px">
        <col style="width: 40px" />
        <col style="width: 400px" />
        <col style="" />
        <col style="width: 400px;" />
            <tr>
                <td>
                    1
                </td>
                <td>
                    <asp:Label ID="BodyTypeLabel1" runat="server" Text="Label"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="BodyTypeDropDownList1" runat="server" onchange="bodyTypeDropDownChanged(this);">
                    </asp:DropDownList>
                    <%--<asp:RequiredFieldValidator ForeColor="red" ID="BodyTypeRequiredFieldValidator1" runat="server" ControlToValidate="BodyTypeDropDownList1" InitialValue="0" ValidationGroup="bt" ErrorMessage="*"></asp:RequiredFieldValidator>        
                --%></td>
                <td>
                    <asp:Label ID="BodyTypeLabel11" runat="server" Text="" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    2
                </td>
                <td>
                    <asp:Label ID="BodyTypeLabel2" runat="server" Text="Label"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="BodyTypeDropDownList2" runat="server" onchange="bodyTypeDropDownChanged(this);">
                    </asp:DropDownList>
                    <%--<asp:RequiredFieldValidator ForeColor="red" ID="BodyTypeRequiredFieldValidator2" runat="server" ControlToValidate="BodyTypeDropDownList2" InitialValue="0" ValidationGroup="bt" ErrorMessage="*"></asp:RequiredFieldValidator>
                --%></td>
                <td>
                    <asp:Label ID="BodyTypeLabel12" runat="server" Text="" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    3
                </td>
                <td>
                    <asp:Label ID="BodyTypeLabel3" runat="server" Text="Label"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="BodyTypeDropDownList3" runat="server" onchange="bodyTypeDropDownChanged(this);">
                    </asp:DropDownList>
                    <%--<asp:RequiredFieldValidator ForeColor="red" ID="BodyTypeRequiredFieldValidator3" runat="server" ControlToValidate="BodyTypeDropDownList3" InitialValue="0" ValidationGroup="bt" ErrorMessage="*"></asp:RequiredFieldValidator>
                --%></td>
                <td>
                    <asp:Label ID="BodyTypeLabel13" runat="server" Text="" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    4
                </td>
                <td>
                    <asp:Label ID="BodyTypeLabel4" runat="server" Text="Label"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="BodyTypeDropDownList4" runat="server" onchange="bodyTypeDropDownChanged(this);">
                    </asp:DropDownList>
                    <%--<asp:RequiredFieldValidator ForeColor="red" ID="BodyTypeRequiredFieldValidator4" runat="server" ControlToValidate="BodyTypeDropDownList4" InitialValue="0" ValidationGroup="bt" ErrorMessage="*"></asp:RequiredFieldValidator>
               --%> </td>
                <td>
                    <asp:Label ID="BodyTypeLabel14" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    5
                </td>
                <td>
                    <asp:Label ID="BodyTypeLabel5" runat="server" Text="Label"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="BodyTypeDropDownList5" runat="server" onchange="bodyTypeDropDownChanged(this);">
                    </asp:DropDownList>
                    <%--<asp:RequiredFieldValidator ForeColor="red" ID="BodyTypeRequiredFieldValidator5" runat="server" ControlToValidate="BodyTypeDropDownList5" InitialValue="0" ValidationGroup="bt" ErrorMessage="*"></asp:RequiredFieldValidator>
                --%></td>
                <td>
                    <asp:Label ID="BodyTypeLabel15" runat="server" Text="" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    6
                </td>
                <td>
                    <asp:Label ID="BodyTypeLabel6" runat="server" Text="Label"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="BodyTypeDropDownList6" runat="server" onchange="bodyTypeDropDownChanged(this);">
                    </asp:DropDownList>
                    <%--<asp:RequiredFieldValidator ForeColor="red" ID="BodyTypeRequiredFieldValidator6" runat="server" ControlToValidate="BodyTypeDropDownList6" InitialValue="0" ValidationGroup="bt" ErrorMessage="*"></asp:RequiredFieldValidator>
                --%></td>
                <td>
                    <asp:Label ID="BodyTypeLabel16" runat="server" Text="" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    7
                </td>
                <td>
                    <asp:Label ID="BodyTypeLabel7" runat="server" Text="Label" onchange="bodyTypeDropDownChanged(this);"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="BodyTypeDropDownList7" runat="server" onchange="bodyTypeDropDownChanged(this);">
                    </asp:DropDownList>
                    <%--<asp:RequiredFieldValidator ForeColor="red" ID="BodyTypeRequiredFieldValidator7" runat="server" ControlToValidate="BodyTypeDropDownList7" InitialValue="0" ValidationGroup="bt" ErrorMessage="*"></asp:RequiredFieldValidator>
                --%></td>
                <td>
                    <asp:Label ID="BodyTypeLabel17" runat="server" Text="" ></asp:Label> 
                </td>
            </tr>
            <tr>
                <td>
                    8
                </td>
                <td>
                    <asp:Label ID="BodyTypeLabel8" runat="server" Text="Label"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="BodyTypeDropDownList8" runat="server" onchange="bodyTypeDropDownChanged(this);">
                    </asp:DropDownList>
                    <%--<asp:RequiredFieldValidator ForeColor="red" ID="BodyTypeRequiredFieldValidator8" runat="server" ControlToValidate="BodyTypeDropDownList8" InitialValue="0" ValidationGroup="bt" ErrorMessage="*"></asp:RequiredFieldValidator>
                --%></td>
                <td>
                    <asp:Label ID="BodyTypeLabel18" runat="server" Text="" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    9
                </td>
                <td>
                    <asp:Label ID="BodyTypeLabel9" runat="server" Text="Label"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="BodyTypeDropDownList9" runat="server" onchange="bodyTypeDropDownChanged(this);">
                    </asp:DropDownList>
                    <%--<asp:RequiredFieldValidator ForeColor="red" ID="BodyTypeRequiredFieldValidator9" runat="server" ControlToValidate="BodyTypeDropDownList9" InitialValue="0" ValidationGroup="bt" ErrorMessage="*"></asp:RequiredFieldValidator>
                --%></td>
                <td>
                    <asp:Label ID="BodyTypeLabel19" runat="server" Text="" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    10
                </td>
                <td>
                    <asp:Label ID="BodyTypeLabel10" runat="server" Text="Label"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="BodyTypeDropDownList10" runat="server" onchange="bodyTypeDropDownChanged(this);">
                    </asp:DropDownList>
                    <%--<asp:RequiredFieldValidator ForeColor="red" ID="BodyTypeRequiredFieldValidator10" runat="server" ControlToValidate="BodyTypeDropDownList10" InitialValue="0" ValidationGroup="bt" ErrorMessage="*"></asp:RequiredFieldValidator>
                --%></td>
                <td>
                    <asp:Label ID="BodyTypeLabel20" runat="server" Text="" ></asp:Label>
                </td>
            </tr>    
    </table>
    <div id="message" style="color:Red; font-weight:bold;margin-left:20px;">
        <span id="msg" style="display:block;"></span>
        <span id="msg2" style="display:block;"></span>
        <ul>
        </ul>
    </div>
    <br />
    <div id="bodytypesummary" style="height: auto;">
        <div style="width:160px; font-weight: bold; font-size: 14px; float:left;">
            Total Score :<asp:Label ID="BodyTypescoreLabel" runat="server" Text="Label" CssClass="result" ForeColor="red"></asp:Label>
        </div>
        <div style="float:left; font-weight: bold; font-size: 14px; float:left;">
            Your Body Type :<asp:Label ID="BodyTypelblBodyType" runat="server" Text="Label" CssClass="result" ForeColor="red"></asp:Label>
        </div><div style="clear:both;"></div>
        <%--<p style="font-weight: bold; font-size: 14px">
            Total Score :  &nbsp; &nbsp;
            <asp:Label ID="BodyTypescoreLabel" runat="server" Text="Label" ForeColor="red"></asp:Label> &nbsp; &nbsp; &nbsp; &nbsp;
            Your Body Type :
            <asp:Label ID="BodyTypelblBodyType" runat="server" Text="Label" ForeColor="red"></asp:Label>
        </p>--%>
        <p>
            <br/>
            <h3><a href="#" id="explannermessage">>> Learn more about your body type <<</a></h3>
        </p>     
    </div>
     
    <div class="target" style="display: none">
           <br/>
           <div style="background-color: #EDEBEB; ">
              <asp:Literal ID="Literalboodytype" runat="server"></asp:Literal>
              
              <br/>
              <div id="ectosection">
                  <br/>
                  <h3>Typical Traits of an Ectomorph</h3>
                  <p>
                      <i>Ectomorphs tend to be naturally skinny people and find it hard to put on weight. Generally, they don’t need to do as much cardiovascular training but do need more weight training to achieve their results. They can also get away with eating more food without putting on weight</i>
                      <ul>
                          <li>Classic “hardgainer”</li>
                          <li>Flat chest</li>
                          <li>Small delicate frame and bone structure</li>
                          <li>Small shoulders</li>
                          <li>Thin</li>
                          <li>Lean muscle mass</li>
                          <li>Finds it hard to gain weight</li>
                          <li>Fast metabolism</li>
                          <li>Can lose fat very easily</li>
                      </ul>
                  </p>
              </div>
              <div id="endosection">
                  <br/>
                  <h3>Typical traits of a Mesomorph</h3>
                  <p>
                      <i>Mesomorphs are naturally muscular people and tend to see changes to their body shapes quite quickly in terms of both gaining and losing weight</i>
                      <ul>
                          <li>Athletic</li>
                          <li>Generally hard body</li>
                          <li>Well defined muscles</li>
                          <li>Rectangular shaped body</li>
                          <li>Strong</li>
                          <li>ains muscle easily</li>
                          <li>Gains fat more easily than ectomorphs</li>
                          <li>Usually a combination of weight training and cardio works best for mesomorphs</li>
                      </ul>

                  </p>
              </div>
              <div id="mesosection">
                  <br/>
                  <h3>Typical traits of an Endomorph</h3>
                  <p>
                      <i>Tend to put on weight easily. They need to do more cardiovascular training, less weight training, and cannot afford to eat as much food to achieve their results.</i>
                      <ul>
                          <li>Soft and round body</li>
                          <li>Gains muscle and fat very easily</li>
                          <li>Is generally short</li>
                          <li>"Stocky" build</li>
                          <li>Round physique</li>
                          <li>Finds it hard to lose fat</li>
                          <li>Slow metabolism</li>
                          <li>Muscles not so well defined</li>
                      </ul>
                  </p>
              </div>
            </div><br/>
       </div>
    <div style="width: auto; height: 50px ">
        <div style="float: left">
            <asp:imagebutton ID="Imagebutton4" ImageUrl="/images/buttonBack.gif" 
                runat="server" onclick="Imagebutton4_Click" ValidationGroup="bt"></asp:imagebutton>
        </div>
        <div style="float: right">
             <%--<asp:ImageButton ID="bodyTypeCalculateButton" 
                ImageUrl="/images/ExtClubVision/buttonCalculate.gif" runat="server" 
                onclick="bodyTypeCalculateButton_Click" ValidationGroup="bt" />
                &nbsp; &nbsp; &nbsp; &nbsp;--%>
            <asp:imagebutton ID="ImagebuttonNext" ImageUrl="/images/buttonSaveAndNext.gif" 
                runat="server" ValidationGroup="bt"  onclick="ImagebuttonNextClick" OnClientClick="return validateInputs();"></asp:imagebutton>
        </div>
    </div>   
</div>