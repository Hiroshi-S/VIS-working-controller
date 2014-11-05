<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyGoals.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.externalclubvision.initialscreens.MyGoals" %>
<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="VisionPersonalTrainingProject.usercontrols.externalclubvision.initialscreens" %>

<div id="MyGoalsDiv" runat="server">
    <div class="mygoal"  style="margin-right: 10px !important;">
        <span style="color: #E27423;"><h2><asp:Label ID="StepLabel" runat="server" Text="STEP 5" /></h2></span>
        <div style="float: left;display: block;margin-bottom: 30px;" id="goalHeaderButton" runat="server">
            <%--<span style="color: #E27423;"><h2><asp:Label ID="StepLabel" runat="server" Text="STEP 5" /></h2></span>--%>
            <br/>
            <div style="float: left;display: block" >
                <div style="display: block;float: left;width: 583px;height: 30px;">
                    <div style="float: left;">
                        <h3>Weight Loss Goal : <asp:Label ID="GoalDateLabel" runat="server" Text="Label"></asp:Label></h3>
                    </div>
                </div>
                <div id="myGoalHeader1" runat="server" style="float: left;display: block">
                    <asp:Button ID="PreviousGoalButton" runat="server" Text="<< Previous" CssClass="NextPrevButton" onclick="PreviousGoalButton_Click" />
                </div>
                <div id="myGoalHeaderNext" runat="server" style="float:left;display:block;">
                    <asp:Button ID="NextGoalButton" runat="server" Text="Next >>" CssClass="NextPrevButton" onclick="NextGoalButton_Click" />
                </div>
                <div id="myGoalHeader2" runat="server" style="float: left;display: block;">
                    <asp:Button ID="CreateNewGoalButton" runat="server" Text="Create New" CssClass="NextPrevButton" onclick="CreateNewGoalButton_Click"  Visible="False"/>
                    <asp:Button ID="CreateNewButton" runat="server" Text="Create New"
                CssClass="NextPrevButton" OnClientClick="popUpResettingPlan();return false;" />
                </div>  
            </div>
        </div>
        <span style="color: #ffffff;"><asp:Label ID="bodytypeLabel" runat="server" Text="Label"></asp:Label></span>    
        <br/>
        <div id="myGoalsContentDiv" runat="server" style="display: block; margin-top: 0px;">
        <p>We recommend you set short term weight loss goals that are more achievable as this will help you stay on track with your long term targets.</p>
        <br/>
        <asp:Label ID="txtMyGoalsWarning" runat="server" ForeColor="red" Text=""></asp:Label>  
        <asp:ValidationSummary ID="myGoalValidationSummary" ForeColor="red" runat="server" ValidationGroup="mygoal" HeaderText="* Please enter values in correct format" />
        <%--<table style="width: 350px;">
            <colgroup>
                <col style="width:335px;" />
                <col style="width:150px;" />
               
                
            </colgroup>
        </table>--%>
        <asp:UpdatePanel ID="myGoalUpdatePanel" runat="server">
            <ContentTemplate>
                <table style="width: 583px;">
                    <colgroup>
                        <col style="width:200px;" />
                        <col style="width:150px;" />
                        <col style="width:50px;" />
                        <tr>
                            <td>Preferred format</td>
                            <td>
                                <asp:Label ID="txtIsMetric" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Current Body Weight
                            </td>
                            <td>
                                <asp:TextBox ID="txtCurrentBW" Enabled="False" runat="server" Width="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trImperial" runat="server" style="display: none;">
                            <td>
                                Desired Weight Loss in 9 Weeks
                                <img src="/images/help.png" alt="Why 9 Weeks programs are recommended" title="Why 9 Weeks programs are recommended"
                                     style="cursor: pointer; vertical-align: middle; height: 29px;margin-top:-7px;" onclick="whyNineWeeksInfo();" />
                            </td>
                            <td>
                                <asp:DropDownList ID="weightGoalDropDownListImperial" runat="server" Width="54" Height="22"
                                    AutoPostBack="True" OnSelectedIndexChanged="WeightGoalDropDownListSelectedIndexChanged">
                                    <asp:ListItem runat="server" Value="0">0</asp:ListItem>
                                    <asp:ListItem runat="server" Value="0.5">1.1</asp:ListItem>
                                    <asp:ListItem runat="server" Value="1">2.2</asp:ListItem>
                                    <asp:ListItem runat="server" Value="1.5">3.3</asp:ListItem>
                                    <asp:ListItem runat="server" Value="2">4.4</asp:ListItem>
                                    <asp:ListItem runat="server" Value="2.5">5.5</asp:ListItem>
                                    <asp:ListItem runat="server" Value="3">6.6</asp:ListItem>
                                    <asp:ListItem runat="server" Value="3.5">7.7</asp:ListItem>
                                    <asp:ListItem runat="server" Value="4">8.8</asp:ListItem>
                                    <asp:ListItem runat="server" Value="4.5">9.9</asp:ListItem>
                                    <asp:ListItem runat="server" Value="5">11</asp:ListItem>
                                    <asp:ListItem runat="server" Value="5.5">12.1</asp:ListItem>
                                    <asp:ListItem runat="server" Value="6">13.2</asp:ListItem>
                                    <asp:ListItem runat="server" Value="6.5">14.3</asp:ListItem>
                                    <asp:ListItem runat="server" Value="7">15.4</asp:ListItem>
                                    <asp:ListItem runat="server" Value="7.5">16.5</asp:ListItem>
                                    <asp:ListItem runat="server" Value="8">17.6</asp:ListItem>
                                    <asp:ListItem runat="server" Value="8.5">18.7</asp:ListItem>
                                    <asp:ListItem runat="server" Value="9">19.8</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="imperialddlRequiredFieldValidator" Enabled="False"
                                    runat="server" ControlToValidate="weightGoalDropDownListImperial" ErrorMessage="" 
                                    ForeColor="red" InitialValue="0" Text="*" ValidationGroup="mygoal"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="trMetric" runat="server" style="display: none;">
                            <td>
                                Desired Weight Loss in 9 Weeks
                                <img src="/images/help.png" alt="Why 9 Weeks programs are recommended" title="Why 9 Weeks programs are recommended"
                                     style="cursor: pointer; vertical-align: middle; height: 29px;margin-top:-7px;" onclick="whyNineWeeksInfo();" />
                            </td>
                            <td>
                                <asp:DropDownList ID="weightGoalDropDownListMetric" runat="server" 
                                    AutoPostBack="True" Width="54" Height="22"
                                    OnSelectedIndexChanged="WeightGoalDropDownListSelectedIndexChanged">
                                    <asp:ListItem runat="server" Value="0"></asp:ListItem>
                                    <asp:ListItem runat="server" Value="0.5">0.5</asp:ListItem>
                                    <asp:ListItem runat="server" Value="1">1</asp:ListItem>
                                    <asp:ListItem runat="server" Value="1.5">1.5</asp:ListItem>
                                    <asp:ListItem runat="server" Value="2">2</asp:ListItem>
                                    <asp:ListItem runat="server" Value="2.5">2.5</asp:ListItem>
                                    <asp:ListItem runat="server" Value="3">3</asp:ListItem>
                                    <asp:ListItem runat="server" Value="3.5">3.5</asp:ListItem>
                                    <asp:ListItem runat="server" Value="4">4</asp:ListItem>
                                    <asp:ListItem runat="server" Value="4.5">4.5</asp:ListItem>
                                    <asp:ListItem runat="server" Value="5">5</asp:ListItem>
                                    <asp:ListItem runat="server" Value="5.5">5.5</asp:ListItem>
                                    <asp:ListItem runat="server" Value="6">6</asp:ListItem>
                                    <asp:ListItem runat="server" Value="6.5">6.5</asp:ListItem>
                                    <asp:ListItem runat="server" Value="7">7</asp:ListItem>
                                    <asp:ListItem runat="server" Value="7.5">7.5</asp:ListItem>
                                    <asp:ListItem runat="server" Value="8">8</asp:ListItem>
                                    <asp:ListItem runat="server" Value="8.5">8.5</asp:ListItem>
                                    <asp:ListItem runat="server" Value="9">9</asp:ListItem>
                                    <asp:ListItem runat="server" Value="9.5">9.5</asp:ListItem>
                                    <asp:ListItem runat="server" Value="10">10</asp:ListItem>
                                    <asp:ListItem runat="server" Value="10.5">10.5</asp:ListItem>
                                    <asp:ListItem runat="server" Value="11">11</asp:ListItem>
                                    <asp:ListItem runat="server" Value="11.5">11.5</asp:ListItem>
                                    <asp:ListItem runat="server" Value="12">12</asp:ListItem>
                                    <asp:ListItem runat="server" Value="12.5">12.5</asp:ListItem>
                                    <asp:ListItem runat="server" Value="13">13</asp:ListItem>
                                    <asp:ListItem runat="server" Value="13.5">13.5</asp:ListItem>
                                    <asp:ListItem runat="server" Value="14">14</asp:ListItem>
                                    <asp:ListItem runat="server" Value="14.5">14.5</asp:ListItem>
                                    <asp:ListItem runat="server" Value="15">15</asp:ListItem>
                                    <asp:ListItem runat="server" Value="15.5">15.5</asp:ListItem>
                                    <asp:ListItem runat="server" Value="16">16</asp:ListItem>
                                    <asp:ListItem runat="server" Value="16.5">16.5</asp:ListItem>
                                    <asp:ListItem runat="server" Value="17">17</asp:ListItem>
                                    <asp:ListItem runat="server" Value="17.5">17.5</asp:ListItem>
                                    <asp:ListItem runat="server" Value="18">18</asp:ListItem>
                                    <asp:ListItem runat="server" Value="18.5">18.5</asp:ListItem>
                                    <asp:ListItem runat="server" Value="19">19</asp:ListItem>
                                    <asp:ListItem runat="server" Value="19.5">19.5</asp:ListItem>
                                    <asp:ListItem runat="server" Value="20">20</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="metricddlRequiredFieldValidator"  Enabled="False"
                                    runat="server" ControlToValidate="weightGoalDropDownListMetric" ErrorMessage="" 
                                    ForeColor="red" InitialValue="0" Text="*" ValidationGroup="mygoal"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                
                        <tr>
                            <td>Body Weight Goal</td>
                            <td>
                                <asp:TextBox ID="txtBWGoal" runat="server" Enabled="False" Width="50" ></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtBWGoal" ErrorMessage="" 
                                    ForeColor="red" Text="*" ValidationGroup="mygoal"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <br/>
                                 <h3>Activity Level</h3>
                            </td>
                        </tr>
                        <%--<tr>
                            <td colspan="3">
                                <p>Your activity level is used to determine the required exercise to achieve your desired 9 week weight loss goal</p>
                            </td>
                        </tr>--%>

                        <tr>
                            <td>
                                Your Daily Activities are mainly</td>
                            <td>
                                <asp:DropDownList ID="activityLvlDropDownList" runat="server" Width="250"
                                    AutoPostBack="True" 
                                    onselectedindexchanged="ActivityLvlDropDownListSelectedIndexChanged">
                                    <asp:ListItem runat="server" Value="0" Text=""></asp:ListItem>
                                    <asp:ListItem runat="server" Value="2" Text="Fairly Active">Predominantly Sitting (eg. Office Job)</asp:ListItem>
                                    <asp:ListItem runat="server" Value="3" Text="Moderately Active">Moderate (eg.Sales Rep, Teaching)</asp:ListItem>
                                    <asp:ListItem runat="server" Value="4" Text="Very Active">Intensive (eg. Labouring)</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="activityLvlDropDownListRequiredFieldValidator" 
                                    runat="server" ControlToValidate="activityLvlDropDownList" ErrorMessage="" 
                                    ForeColor="red" InitialValue="0" Text="*" ValidationGroup="mygoal"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="trActLvl" runat="server" Visible="False">
                            <td>
                                Your activities level is
                            </td>
                            <td>
                                <asp:Label ID="lblActivityLevel" runat="server" Text=""></asp:Label>
                            </td>
                            <td></td>
                        </tr>
                    </colgroup>
                </table>
       
                <div id="RecommendationDiv" runat="server" style="display: none;">
                    <br /> <br/>
                    <h3>Exercise Recommendations</h3>
                    <div>
                       <asp:Literal ID="myGoalLiteral" runat="server"></asp:Literal>
                    </div>
           
                    <br/>
                    <p>
                        The recommendations given outline what is required in terms of minutes for both weight training exercises and cardio exercises for you to achieve your goal. 
                    </p>
                    <br/>
                    <p>
                        <h3>Cardio</h3>
                        Cardio exercise is given two classifications – Low to Moderate or Hard. <br/>
                        Low to Moderate  where you can breathe comfortably and carry out a continued conversation during activity <br/>
                        Hard where you cannot hold a continued conversation due to heavy breathing, you will feel hotter and most likely be sweating and puffing. <br/>
                        Your total cardio percentage is broken down into 75% Low to Moderate (L-M) and 25% Hard. <br/>
                        <br />
                        <span onclick="lmcardio();" style="font-weight:bold;color: #E27423;cursor: hand; cursor:pointer">Click here to see a video about L-M/Hard cardio</span>
                        <p>
                        </p>
                        <br/>
                        <br />
                        <p>
                            <h3>
                                Weights</h3>
                            Weights session include working out with barbells, dumbbells, resistance 
                            equipment or body weight, either at home or in the gym.
                            <p>
                            </p>
                            <br/>
                            <br/>
                            <div>
                                <table style="border: 1px #e27423 solid; display: none;  ">
                                    <colgroup>
                                        <col style="width:100px;" />
                                        <col style="width:100px;" />
                                        <col style="width:100px;" />
                                        <tr style="background-color: #e27423; color: white; ">
                                            <td colspan="3" style="text-align: center;">
                                                <h3 style="height: 32px; margin-top: 15px;">
                                                    Your Macronutrients</h3>
                                            </td>
                                        </tr>
                                        <tr style="font-weight: bold;">
                                            <td>
                                                Carbohydrates</td>
                                            <td>
                                                Protein</td>
                                            <td>
                                                Fat</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCHO" runat="server" Text="Label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblPTN" runat="server" Text="Label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblFAT" runat="server" Text="Label"></asp:Label>
                                            </td>
                                        </tr>
                                    </colgroup>
                                </table>
                            </div>
                            <p>
                            </p>
                            <p>
                            </p>
                        </p>
                    </p>
                 </div> <!-- end of RecommendationDiv -->

            </ContentTemplate>
   
        </asp:UpdatePanel>
  


        <br/><br/>    
        <div style="width: auto; height: 50px ">
            <div style="float: left">
                <asp:imagebutton ID="MyGoalImagebuttonBack" ImageUrl="/images/buttonBack.gif" 
                    runat="server" onclick="MyGoalImagebuttonBack_Click"></asp:imagebutton>
            </div>
    
            <div style="float: right">
                <asp:imagebutton ID="MyGoalImagebuttonNext" ImageUrl="/images/buttonSaveAndNext.gif" 
                    runat="server" 
                    onclick="MyGoalImagebuttonNextClick"></asp:imagebutton>
                <asp:imagebutton ID="MyGoalImagebuttonNextUC" ImageUrl="/images/buttonSaveAndNext.gif" 
                    runat="server" onclick="NewInsertToDB" Visible="False"></asp:imagebutton>
            </div>
        </div>
        </div>
    </div>
</div>