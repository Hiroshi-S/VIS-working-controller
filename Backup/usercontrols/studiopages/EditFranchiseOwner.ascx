<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditFranchiseOwner.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.studiopages.EditFranchiseOwner" %>


<div class="row firstdiv studiotitle">
    <div class="col-md-12 col-xs-12">
        <h1> <asp:Label ID="lblStudioTitle" runat="server" Text="Label"></asp:Label></h1>
    </div>
</div>
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

<asp:Literal ID="Literal1" runat="server"></asp:Literal>

<div class="row">
    <div class="col-md-4 articleLists"> <!--list of articles-->
       <h3>Owner List</h3>    
       
        <asp:Literal ID="litFOLists" runat="server"></asp:Literal>
    </div>
    <div class="col-md-8"><!--editor-->
        <h3><asp:Label ID="lblNewPostOrEdit" runat="server" Text="Label"></asp:Label></h3>
        
          <div class="form-group">
            <label for="txtTitle">Name</label>
            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Enter Name"></asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate="txtName" ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ForeColor="red" ValidationGroup="formArticle"></asp:RequiredFieldValidator>
          </div>
          
          <div class="form-group">
            <label for="txtBlurb">Blurb</label>
            <asp:TextBox ID="txtBlurb" runat="server" Rows="10" TextMode="MultiLine" CssClass="form-control" placeholder="Enter Short Description"></asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate="txtBlurb" ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ForeColor="red" ValidationGroup="formArticle"></asp:RequiredFieldValidator>
          </div>
          
          <div class="form-group">
            <label for="FileUploadPicture">Picture</label>
            <asp:Literal ID="litPicture" runat="server"></asp:Literal>
            <asp:FileUpload ID="FileUploadPicture" runat="server" />
            <asp:RequiredFieldValidator ControlToValidate="FileUploadPicture" ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ForeColor="red" ValidationGroup="formArticle"></asp:RequiredFieldValidator>
          </div>
          
          <asp:Button ID="Button1" runat="server" Text="Save Draft" 
              CssClass="btn btn-small btn-primary btn-visionnavy" onclick="SaveDraft" ValidationGroup="formArticle"/>
        
          <asp:Button ID="Button2" runat="server" Text="Send To Publish" 
              CssClass="btn btn-small btn-primary btn-visionred" onclick="PublishPost" ValidationGroup="formArticle"/>
          
        
    </div>
    
</div>
