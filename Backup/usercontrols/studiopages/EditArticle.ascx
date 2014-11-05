<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditArticle.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.studiopages.EditArticle" %>


<div class="row firstdiv studiotitle">
    <div class="col-md-12 col-xs-12">
        <h1> <asp:Label ID="lblStudioTitle" runat="server" Text="Label"></asp:Label></h1>
        <div class="row glossary">
            <div class="col-md-12 col-sm-12 col-xs-12"><h4>Glossary</h4></div>
            <div class="col-md-4 col-sm-2 col-xs-5">
                <span style="color:green;"><i class="fa fa-check-square"></i> Published</span>
            </div>
            <div class="col-md-4 col-sm-4 col-xs-7">
                <span style="color:red;"><i class="fa fa-square"></i> Unpublished, subject for HQ's approval</span>
            </div>
            <div class="col-md-4 col-sm-4 col-xs-7">
                <span style="color:orange;"><i class="fa fa-circle-o"></i> Pending Changes, subject for HQ's approval</span>
            </div>
        </div>
    </div>
</div>

<asp:Literal ID="Literal1" runat="server"></asp:Literal>

<div class="row">
    <div class="col-md-4 articleLists"> <!--list of articles-->
       <h3>Articles</h3>    
        <asp:Literal ID="litPostLists" runat="server"></asp:Literal>
    </div>
    <div class="col-md-8"><!--editor-->
        <h3><asp:Label ID="lblNewPostOrEdit" runat="server" Text="Label"></asp:Label></h3>
        
          <div class="form-group">
            <label for="txtTitle">Title</label>
            <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" placeholder="Enter Title"></asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate="txtTitle" ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ForeColor="red" ValidationGroup="formArticle"></asp:RequiredFieldValidator>
          </div>
          
          <div class="form-group">
            <label for="txtBlurb">Blurb</label>
            <asp:TextBox ID="txtBlurb" runat="server" CssClass="form-control" placeholder="Enter Short Description"></asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate="txtBlurb" ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ForeColor="red" ValidationGroup="formArticle"></asp:RequiredFieldValidator>
          </div>
          
          <div class="form-group">
            <label for="FileUploadPicture">Picture in a landscape mode</label>
            <asp:Literal ID="litPicture" runat="server"></asp:Literal>
            <asp:FileUpload ID="FileUploadPicture" runat="server" />
            <asp:RequiredFieldValidator ControlToValidate="FileUploadPicture" ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ForeColor="red" ValidationGroup="formArticle"></asp:RequiredFieldValidator>
          </div>
          
          <div class="form-group">
            <label for="txtContent">Content</label>
            <asp:TextBox ID="txtContent" TextMode="MultiLine" runat="server"  CssClass="form-control" rows="20" ClientIDMode="Static"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Enabled="False" ControlToValidate="txtContent" runat="server" ErrorMessage="*" ForeColor="red" ValidationGroup="formArticle"></asp:RequiredFieldValidator>
          </div>
          
          <div class="form-group">
            <label for="txtTags">Tags</label>
            <asp:TextBox ID="txtTags" runat="server" CssClass="form-control" placeholder="Enter Tags"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Enabled="False" ControlToValidate="txtTags" runat="server" ErrorMessage="*" ForeColor="red" ValidationGroup="formArticle"></asp:RequiredFieldValidator>
          </div>
        
          <div class="form-group">
            <label for="ddlCategory">Category</label>
            <asp:DropDownList ID="ddlCategory" runat="server">
                <asp:ListItem runat="server" Enabled="False"></asp:ListItem>
                <asp:ListItem runat="server"  Value="Weight Loss">Weight Loss</asp:ListItem>
                <asp:ListItem runat="server"  Value="Mums and Bubs">Mums and Bubs</asp:ListItem>
                <asp:ListItem runat="server"  Value="Fitness for Men">Fitness for Men</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="ddlCategory" runat="server" ErrorMessage="*" ForeColor="red" ValidationGroup="formArticle"></asp:RequiredFieldValidator>
          </div>
          
          <asp:Button ID="Button1" runat="server" Text="Save Draft" 
              CssClass="btn btn-small btn-primary btn-visionnavy" onclick="SaveDraft" ValidationGroup="formArticle"/>
        
          <asp:Button ID="Button2" runat="server" Text="Send To Publish" 
              CssClass="btn btn-small btn-primary btn-visionred" onclick="PublishPost" ValidationGroup="formArticle"/>
          
        
    </div>

    
</div>
