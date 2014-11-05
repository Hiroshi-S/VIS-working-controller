<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CopySharePlanLightbox.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.lightboxes.CopySharePlan" %>
<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls"
    TagPrefix="BDP" %>
    
<div class="contactOverlay" id="cErrorPopup" >				
  <div class="contactBox">
  	<div class="cTop"></div>
  	<div class="cContent">
    	<a href="#" target="_blank" class="replace cCross" title="Close" onclick="$('#cErrorPopup').fadeOut();return false;">Close</a>
    	<h2 id="title">Copy This Plan to My Account</h2>
        <h4 id="h4text"></h4>

            <div class="row" id="copyFromMenudiv" style="display:none;">
              	<table>
              	    <tr>
                        <td>Username</td>
                        <td><asp:TextBox ID="username" runat="server" ValidationGroup="username"></asp:TextBox></td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ErrorMessage="*" ControlToValidate="username" 
                                ValidationGroup="mcta"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>Password</td>
                        <td><asp:TextBox ID="password" runat="server" TextMode="Password"></asp:TextBox></td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ErrorMessage="*" ControlToValidate="password" 
                                ValidationGroup="mcta"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr runat="server" visible="false" id="mealtimerow">
                        <td>
                            Meal Time to Copy
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList1" runat="server">
                                <asp:ListItem Text="" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Breakfast" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Morning Tea" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Lunch" Value="3"></asp:ListItem>
                                <asp:ListItem Text="Afternoon Tea" Value="4"></asp:ListItem>
                                <asp:ListItem Text="Dinner" Value="5"></asp:ListItem>
                                <asp:ListItem Text="Supper" Value="6"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                             ErrorMessage="*" ControlToValidate="DropDownList1" InitialValue="0"
                                ValidationGroup="mcta"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
              	        <td>Date to Copy </td>
                        <td>
                            <div style="float:left;width:200px;">
                                <BDP:BasicDatePicker ID="bdpDay" runat="server" 
                                    ShowNoneButton="False" 
                                    ShowTodayButton="False" 
                                    ButtonImageFileName="calendar.gif" ButtonImageHeight="25" 
                                    ButtonImageWidth="26" RenderImages="File" ResourcePath="/images/" 
                                    DownYearSelectorImageFileName="" NextMonthImageFileName="calendar_forward.gif" 
                                    PrevMonthImageFileName="calendar_back.gif" RenderStyleSheet="None" 
                                    TextBoxStyle-BackColor="White" TextBoxStyle-BorderColor="#999999" 
                                    TextBoxStyle-BorderStyle="Solid" TextBoxStyle-BorderWidth="1px" 
                                    TextBoxStyle-Height="23px" TextBoxStyle-Width="161px" 
                                    DateFormat="dd/MM/yyyy" 
                                    ShowCalendarOnTextBoxFocus="True" 
                                    >
                            </BDP:BasicDatePicker>
                                
                            </div>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                ErrorMessage="*" ControlToValidate="bdpDay" 
                                ValidationGroup="mcta"></asp:RequiredFieldValidator></td>
                            
              	    </tr>
                    <tr>
                        <td>
                            <asp:ImageButton ID="ImageButton1" runat="server" ValidationGroup="mcta"
                                ImageUrl="~/images/buttonSubmitOrange.gif" Height="28" Width="108" 
                                style="border: 0px;"
                                onclick="ImageButton1_Click"/>
                              
                        </td>
                    </tr>
              	</table>
              	    
      	    <br/>
            <br/>
             
        </div><!-- /copyFromMenudiv -->
        
            <div class="row" id="copyToMealdiv" style="display:none;">
            <table>
              	    <tr>
                        <td>Username</td>
                        <td><asp:TextBox ID="usernameToMeal" runat="server" ValidationGroup="username"></asp:TextBox></td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                ErrorMessage="*" ControlToValidate="usernameToMeal" 
                                ValidationGroup="mcta3"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>Password</td>
                        <td><asp:TextBox ID="passwordToMeal" runat="server" TextMode="Password"></asp:TextBox></td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                ErrorMessage="*" ControlToValidate="passwordToMeal" 
                                ValidationGroup="mcta3"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>
                            Meal Name
                        </td>
                        <td>
                            <asp:TextBox ID="mealName" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                             ErrorMessage="*" ControlToValidate="mealName"
                                ValidationGroup="mcta3"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ImageButton ID="ImageButton2" runat="server" ValidationGroup="mcta3"
                                ImageUrl="~/images/buttonSubmitOrange.gif" Height="28" Width="108" 
                                style="border: 0px;"
                                onclick="ImageButton_copyToMealdiv_Click"/>
                              
                        </td>
                    </tr>
              	</table>
              	    
      	    <br/>
            <br/>
            </div><!-- /copyToMealdiv -->
            
            <div class="row" id="copyToMenudiv" style="display:none;">
            <table>
              	    <tr>
                        <td>Username</td>
                        <td><asp:TextBox ID="usernameToMenu" runat="server" ValidationGroup="username"></asp:TextBox></td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                                ErrorMessage="*" ControlToValidate="usernameToMenu" 
                                ValidationGroup="mcta4"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>Password</td>
                        <td><asp:TextBox ID="passwordToMenu" runat="server" TextMode="Password"></asp:TextBox></td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                                ErrorMessage="*" ControlToValidate="passwordToMenu" 
                                ValidationGroup="mcta4"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>
                            Daily Plan Name
                        </td>
                        <td>
                            <asp:TextBox ID="menuName" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                             ErrorMessage="*" ControlToValidate="menuName"
                                ValidationGroup="mcta4"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ImageButton ID="ImageButton3" runat="server" ValidationGroup="mcta4"
                                ImageUrl="~/images/buttonSubmitOrange.gif" Height="28" Width="108" 
                                style="border: 0px;"
                                onclick="ImageButton_copyToMenudiv_Click"/>
                              
                        </td>
                    </tr>
              	</table>
              	    
      	    <br/>
            <br/>
            </div><!-- /copyToMenudiv -->
            
  		<div class="clear"></div>
  	</div><!-- /cContent -->
  	<div class="cBase"></div>
  </div><!-- /contactBox  -->
</div><!-- /contactOverlay cEnquire -->