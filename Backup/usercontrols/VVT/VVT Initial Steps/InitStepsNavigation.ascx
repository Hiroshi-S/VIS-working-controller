<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InitStepsNavigation.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.VVT.VVT_Initial_Steps.InitStepsNavigation" %>

<asp:Literal ID="LiteralBreadCrumb" runat="server"></asp:Literal>

<script type="text/javascript">
    if ($("#completionInitStepsDiv").length > 0) {
        $("#completionInitStepsDiv").remove();
    }

</script>

