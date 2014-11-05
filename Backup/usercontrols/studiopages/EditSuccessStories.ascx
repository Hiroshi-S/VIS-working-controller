<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditSuccessStories.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.studiopages.EditSuccessStories" %>

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
       <h3>Success Story List</h3>   
       
        <asp:Literal ID="litFOLists" runat="server"></asp:Literal>
    </div>
    <div class="col-md-8"><!--editor-->
        <h3><asp:Label ID="lblNewPostOrEdit" runat="server" Text="Label"></asp:Label></h3>
        
          <div class="form-group">
            <label for="txtName">Name</label>
            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Enter Name"></asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate="txtName" ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ForeColor="red" ValidationGroup="formArticle"></asp:RequiredFieldValidator>
          </div>
          
          <div class="form-group">
            <label for="txtClientId">Client Id</label>
            <asp:TextBox ID="txtClientId" runat="server" CssClass="form-control" placeholder="VOS Client Id"></asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate="txtClientId" ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ForeColor="red" ValidationGroup="formArticle"></asp:RequiredFieldValidator>
          </div>

          <div class="form-group">
            <label for="txtResult">Result</label>
            <asp:TextBox ID="txtResult" runat="server" CssClass="form-control" placeholder="Enter Result"></asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate="txtResult" ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ForeColor="red" ValidationGroup="formArticle"></asp:RequiredFieldValidator>
          </div>
          
          <div class="form-group">
            <label for="txtStory">Story</label>
            <asp:TextBox ID="txtStory" ClientIDMode="Static" runat="server" Rows="10" TextMode="MultiLine" CssClass="form-control" placeholder="Enter Story"></asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate="txtStory" Enabled="False" ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ForeColor="red" ValidationGroup="formArticle"></asp:RequiredFieldValidator>
          </div>
          
          <div class="form-group">
            <label for="txtVideoUrl">Video Url</label>
            <asp:TextBox ID="txtVideoUrl" runat="server" CssClass="form-control" placeholder="Enter Video Url"></asp:TextBox>
            <asp:RequiredFieldValidator Enabled="False" ControlToValidate="txtVideoUrl" ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" ForeColor="red" ValidationGroup="formArticle"></asp:RequiredFieldValidator>
          </div>
          
          <div class="form-group">
            <label for="FileUploadBeforePhoto">Before Photo</label>
            <asp:Literal ID="litBeforePhoto" runat="server"></asp:Literal>
            <asp:FileUpload ID="FileUploadBeforePhoto" runat="server" />
            <asp:RequiredFieldValidator ControlToValidate="FileUploadBeforePhoto" ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ForeColor="red" ValidationGroup="formArticle"></asp:RequiredFieldValidator>
          </div>
          
          <div class="form-group">
            <label for="FileUploadAfterPhoto">After Photo</label>
            <asp:Literal ID="litAfterPhoto" runat="server"></asp:Literal>
            <asp:FileUpload ID="FileUploadAfterPhoto" runat="server" />
            <asp:RequiredFieldValidator ControlToValidate="FileUploadAfterPhoto" ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" ForeColor="red" ValidationGroup="formArticle"></asp:RequiredFieldValidator>
          </div>
          
          <div class="form-group">
            <label for="ModelReleaseFileUpload">Model Release Sign Form. <a href="/services/templates/VPT_Marketing_Model_Release_Form.doc">Click here to download the template.</a></label>
            <asp:Literal ID="LiteralModelRelease" runat="server"></asp:Literal>
            <asp:FileUpload ID="ModelReleaseFileUpload" runat="server" />
            <asp:RequiredFieldValidator ControlToValidate="ModelReleaseFileUpload" ID="RequiredFieldValidator8" runat="server" ErrorMessage="*" ForeColor="red" ValidationGroup="formArticle"></asp:RequiredFieldValidator>
          </div>
          
          <asp:Button ID="Button1" runat="server" Text="Save Draft" 
              CssClass="btn btn-small btn-primary btn-visionnavy"  ValidationGroup="formArticle" OnClick="SaveDraft"/>
        
          <asp:Button ID="Button2" runat="server" Text="Save and Send To Publish" 
              CssClass="btn btn-small btn-primary btn-visionred" ValidationGroup="formArticle" OnClick="PublishPost"/>
          
        
    </div>
    
</div>
