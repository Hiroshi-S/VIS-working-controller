<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Login.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.clubvision.Login" %>

<div class="leftCol regoform" style="position: relative;display: block;" id="insertCredentials" >
<h2>Login to Vision Virtual Training</h2>
<br />
<p class="error" runat="server" id="error">Authentication failed, check username and password.</p>
<p>Username:</p>
<input type="text" name="loginUser" class="sub" /><br />
<p>Password:</p>
<input type="password" name="loginPass" class="sub" /><br /><br />

    <input type="submit" class="button-large vision_red rounded3" value="Submit"/> 

<br/>
<br/>
<a href="javascript:void(0)" onclick="document.getElementById('forgottenPasswordDiv').style.display='block';document.getElementById('insertCredentials').style.display='none';">
    Can't access your account?
</a>
</div>

<div class="leftCol"  id="forgottenPasswordDiv" style="position: relative;display: none;">
    <h2>Having trouble signing in?</h2>
    <br/><br/>
    <input id="Radio1" type="radio" name="fpass" value="fpass" title="I don't know my password or my username" value="" onclick="$('.onediv').slideUp();$('#radiobutton1div').slideDown();"/>
    <%--<input id="Radio1" type="radio" name="fpass" title="I don't know my password or my username" value="fpass" onclick="handleClick(this)" />--%>
    I don't know my password or my username<br/><br/>
    <div id="radiobutton1div" style="display: none;margin: 20px;" class="onediv">
        Please enter your email address to retrieve your login details<br/>
        <input id="Text1fpass" type="text" style="width: 200px;" placeholder="email" />
        <input id="Button2" type="button" value="Send" onclick="forgottonPassword($('#Text1fpass').val(), 'radiobutton1div', null);" /> <br/><br/>
        
        <div id="multipleusers" style="display: none;">
            NOTICE : The email you entered is associated to multiple accounts <br/>
            Please also enter your Username to retrieve your login details<br/>
            <input id="Text1fuser" type="text" style="width: 200px;" placeholder="username" />
            <input id="Button1" type="button" value="Send" onclick="forgottonPassword($('#Text1fpass').val(), 'radiobutton1div', $('#Text1fuser').val());" /> <br/><br/>
        </div>
        
        <h3 id="success" style="display: none;">Your login details have been sent to you by email and you should receive these shortly.</h3>
        <p id="notfound" style="color: red;display: none;">Your email address is not recognised, please try again.</p>
        <p id="contactus" style="color: red; display:none;">
        Multiple accounts have been found. Please contact our office by emailing to 
        <a href="mailto:admin@visionvirtualtraining.com.au">admin@visionvirtualtraining.com.au</a> and we will be in contact with you shortly.</p>
    </div>

 
    <input id="Radio3" type="radio" name="fpass" value="other" onclick="$('.onediv').slideUp();$('#radiobutton3div').slideDown();" />
    <%--<input id="Radio3" type="radio" name="fpass" onclick="handleClick(this)" value="other" />--%>
        I'm having other problems signing in <br/><br/>
    <div id="radiobutton3div" style="display: none;margin: 20px;" class="onediv">
        Contact our office if you are experiencing any other login issue by email to admin@visionvirtualtraining.com.au and we will be in contact with you shortly.
    </div>
    <br/>
    <a href="javascript:void(0)" onclick="document.getElementById('insertCredentials').style.display='block';document.getElementById('forgottenPasswordDiv').style.display='none';">
    Back to login page
</a>
</div>
<script type="text/javascript">
    $(document).ready(function () {
        $("form").attr("action", "/login.aspx");
        $("form").attr("method", "post");
        $('.sub').keydown(function (event) {
            if (event.keyCode == 13) {
                this.form.submit();
                return false;
            }
        });
    });
</script>

