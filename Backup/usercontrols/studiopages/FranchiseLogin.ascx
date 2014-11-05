<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FranchiseLogin.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.studiopages.FranchiseLogin" %>

 <div class="row firstdiv studiotitle">
     <h1>Franchise Login</h1>
   
     <div class="form-group">
        <label for="tbUsername">Username</label>
        <asp:TextBox ID="tbUsername" CssClass="form-control" runat="server" Text="Email Address" onclick="this.value='';" onfocus="this.select()"></asp:TextBox>
     </div>

     <div class="form-group">
        <label for="tbPassword">Password</label>
        <asp:TextBox ID="tbPassword" CssClass="form-control" runat="server" TextMode="Password" Text="Password" onclick="this.value='';" onfocus="this.select()"></asp:TextBox>
     </div>
     
    <div class="form-group">
         <asp:LinkButton ID="btnLogin" CssClass="replace buttonLogin btn btn-visionred btn-lg" runat="server" 
            onclick="btnLogin_Click">Log In</asp:LinkButton>
    </div>


<asp:Label ID="lblErrorMessage" runat="server" Text=""></asp:Label>
         
</div>