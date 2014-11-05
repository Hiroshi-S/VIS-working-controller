<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LifeStyleScreen.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.externalclubvision.initialscreens.LifeStyleScreen" %>
<script type="text/javascript">
    function validateCheckBox() {
        if (!$('#<%=CheckBox1.ClientID %>').is(':checked')) {
            alert("Please confirm that all the information you have provided is correct");
            return false;
        }
        else {
            return true;
        }
    }
</script>
<div class="plifestylescreen" style="margin-right: 10px !important;">
    <span id="stepLabel" runat="server" style="color: #E27423;"><h2>STEP 2</h2></span><br/>
    <h3>Adult Pre-Exercise Screening Tool</h3>

    <p align="justify" style="width: auto">This screening tool does not provide advice on a particular matter, nor does it substitute for advice from an appropriate qualified medical professional. 
    No warranty of safety should result from its use. The screening system in no way guarantees against injury or death. No responsibility or liability whatsoever can be accepted by
    Exercise and Sport Science Australia, Fitness Australia or Sports Medicine Australia for any loss, damage or injury that may arise from any person acting on any statement or information 
    contained in this tool.

    <br />
    <br/>

    AIM : to identify those individuals with a known disease, or signs or symptomps of disease, who may be at higher risk of an adverse event during physical activity/exercise. 
    This test can be self administered and self evaluated, however it is best if discussed and completed with a health and fitness professional. 

    <br/>
    <br/>
    </p>

       
    <p><asp:Label ID="Label8" runat="server" Text="" ForeColor="red" ></asp:Label></p>
    <table class="bordered" style="width: auto;"   >
        <colgroup>
            <col style="background-color:lavender; color: #ffffff; width:30px; " />
            <col style="background-color:lavender; color: #ffffff;" />
            <col style="background-color:peachpuff; color: #ffffff; width:40px;"/>
            <col style="background-color:palegreen; width:40px;" />
            <thead>
                <tr>
                    <th colspan="2">
                        Questions
                    </th>
                    <th>
                        Yes
                    </th>
                    <th>
                        No
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>
                        1.
                    </td>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButton ID="RadioButton1" runat="server"
                            ForeColor="transparent" GroupName="A" 
                            Text="True" />
                    </td>
                    <td>
                        <asp:RadioButton ID="RadioButton2" runat="server"  ForeColor="transparent" 
                            GroupName="A" Text="False" 
                            />
                    </td>
                </tr>
                       
                <tr>
                    <td>
                        2.
                    </td>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButton ID="RadioButton3" runat="server"  ForeColor="transparent" 
                            GroupName="B" Text="True"  />
                    </td>
                    <td>
                        <asp:RadioButton ID="RadioButton4" runat="server"  ForeColor="transparent" 
                            GroupName="B" Text="False" />
                    </td>
                </tr>

                <tr>
                    <td>
                        3.
                    </td>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButton ID="RadioButton5" runat="server"  ForeColor="transparent" 
                            GroupName="C" Text="True" />
                    </td>
                    <td>
                        <asp:RadioButton ID="RadioButton6" runat="server" ForeColor="transparent"
                            GroupName="C" Text="False" />
                    </td>
                </tr>
                    
                <tr>
                    <td>
                        4.
                    </td>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButton ID="RadioButton7" runat="server" ForeColor="transparent"  
                            GroupName="D" Text="True"/>
                    </td>
                    <td>
                        <asp:RadioButton ID="RadioButton8" runat="server" ForeColor="transparent" 
                            GroupName="D" Text="False"  />
                    </td>
                </tr>

                <tr>
                    <td>
                        5.
                    </td>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButton ID="RadioButton9" runat="server" ForeColor="transparent" 
                            GroupName="E" Text="True"/>
                    </td>
                    <td>
                        <asp:RadioButton ID="RadioButton10" runat="server" ForeColor="transparent" 
                            GroupName="E" Text="False"  />
                    </td>
                </tr>
                        
                <tr>
                    <td>
                        6.
                    </td>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButton ID="RadioButton11" runat="server" ForeColor="transparent" 
                            GroupName="F" Text="True" />
                    </td>
                    <td>
                        <asp:RadioButton ID="RadioButton12" runat="server" ForeColor="transparent" 
                            GroupName="F" Text="False"  />
                    </td>
                </tr>
                        
                <tr>
                    <td>
                        7.
                    </td>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButton ID="RadioButton13" runat="server" ForeColor="transparent" 
                            GroupName="G" Text="True"  />
                    </td>
                    <td>
                        <asp:RadioButton ID="RadioButton14" runat="server" ForeColor="transparent" 
                            GroupName="G" Text="False"  />
                    </td>
                </tr>
                        
                <tr>
                    <td style="border-top: 20px solid #fff; background-color:peachpuff; border-right-color: peachpuff; padding: 0px;">
                        <div style=" border-color: transparent transparent transparent red;border-style: solid;border-width: 40px;height: 0px;width: 0px;">
                        </div>
                    </td>
                    <td style="border-top: 20px solid #fff;background-color:peachpuff; border-left-color: peachpuff; border-right-color: peachpuff;  ">
                        IF YOU ANSWERED &#39;YES&#39; to any of the 7 questions, please seek guidance from your 
                        GP or appropriate allied health professional prior to undertaking physical 
                        activity/exercise
                    </td>
                    <td style="border-top: 20px solid peachpuff; background-color: peachpuff; border-left-color: peachpuff;padding: 0px;">
                        <div style=" border-color: red  transparent transparent transparent;border-style: solid;border-width: 40px;height: 0px;width: 0px;">
                        </div>
                    </td>
                    <td style="border-top: 20px solid palegreen; background-color: palegreen; border-bottom-color: palegreen; padding: 0px; ">
                        <div style=" border-color: green  transparent transparent transparent;border-style: solid;border-width: 40px;height: 0px;width: 0px;">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="background-color:palegreen; border-right-color: palegreen; padding: 0px;border-top: 20px solid #fff;">
                        <div style=" border-color: transparent transparent transparent green;border-style: solid;border-width: 40px;height: 0px;width: 0px;">
                        </div>
                    </td>
                    <td style="background-color:palegreen; border-right-color: palegreen; border-left-color: palegreen; border-top: 20px solid #fff;">
                        IF YOU ANSWERED &#39;NO&#39; to all of the 7 questions, and you have no other concerns 
                        about your health, you may proceed to undertake light-moderate intensity 
                        physical activity/exercise
                    </td>
                    <td style="background-color: palegreen; border-right-color: palegreen; border-left-color: palegreen; border-top: 20px solid #fff;">
                    </td>
                    <td style="background-color: palegreen; border-left-color: palegreen; border-top-color: palegreen ">
                    </td>
                </tr>
            </tbody>
        </colgroup>
    </table>

    <p>
        &nbsp;</p>

    <p>
        <asp:CheckBox ID="CheckBox1" runat="server" Text="   I believe that to best of my knowledge, all of the information I have supplied 
        within this tool is correct" />
    </p>
    <p>
        &nbsp;</p>

        <div style="width: auto; height: 50px ">
            <div style="float: left">
                <asp:imagebutton ID="backButton" ImageUrl="/images/buttonBack.gif" 
                    runat="server" onclick="BackButtonClick" ></asp:imagebutton>
            </div>
            <div style="float: right">
                <asp:imagebutton ID="saveAndNextButton" ValidationGroup="ls1"
                        ImageUrl="/images/buttonSaveAndNext.gif" runat="server" 
                        onclick="SaveAndNextImageButtonClick" OnClientClick="return validateCheckBox();"></asp:imagebutton>
            </div>
        </div><asp:Label ID="Label9"  runat="server" Text="" Visible="False" ForeColor="red"></asp:Label>
        <asp:CustomValidator id="CustomValidator1" runat="server" Display="Dynamic" ValidationGroup="ls1" ErrorMessage="Please answer to all questions" ForeColor="red" ClientValidationFunction="CustomValidator1_ClientValidate"></asp:CustomValidator>

    <br />
    <br />
    <p align="justify" style="width: auto"><u>Note:</u> this tool has been designed by Fitness Australia, Exercise and Sports Science Australia (ESSA) 
    and Sports Medicine Australia (SMA) to offer a consistent standard among health professionals, as well as enhance
    safety and help create improved health outcomes for clients.</p>
</div>
