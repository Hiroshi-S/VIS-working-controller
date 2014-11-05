<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WeightSessions.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.VVT.WeightSessions" %>
<style>
    #ContentPlaceHolderDefault_help {
        display: none;
    }
</style>
<!-- <div id="eWhatsOnMainWithNews" class="element" style="width:920px !important;"> -->
       <!-- <div id="visionLibraryContainer" style="width:918px;margin-top:0px;background: url(/images/recipeContainerBg920px.gif) 0 0 no-repeat;"> -->

                <div class="pTrainingDiary" id="weightSessDiv" style="margin-right: 10px !important;padding:10px;" runat="server" ClientIDMode="Static">
                    <div style="font-weight: bold; font-size: 14px;float: left; padding-top: 4px; padding-right: 10px">Date Exercise:</div>
                            <div style="border: 1px solid #999999; padding: 1px;float: left; height: 21px; width: 300px;">
                            <input type="hidden" id="currentDate" runat="server" value="" />
                            <asp:ImageButton runat="server" ID="buttonDayNext" Text=">" Visible="False"
                                     ImageUrl="~/images/calendar_forward.gif" style="float: right" />
                            <asp:ImageButton runat="server" ID="buttonDayPrev" Text="<"  Visible="False"
                                     ImageUrl="~/images/calendar_back.gif" style="float: left" />
                            <p style="display: inline-block; padding: 2px 14px"><asp:Literal runat="server" ID="literalDay"></asp:Literal></p>
                            </div>
                            <div style="float: left; position: relative; top: -1px">  
                                
                            </div>
                    <br /><br />
                    <div id="TDresponsemessage" style="color: #6BB33A; font-weight: bold;height: 30px; display: block;"></div>
                    <asp:Label ID="weekLabel" runat="server" Text="" Visible="True" ForeColor="#FFFFFF"></asp:Label>
                    
                    
                    <asp:Literal ID="litProgramSummary" runat="server"></asp:Literal>
                   <br/>

                    <!-- as table -->
                        <asp:Literal ID="litWeightsSess" runat="server"></asp:Literal>    
                    <!-- as table -->
                    
                    <br/><br />
                    <div id="WeightsSessParagraph">
                        <p>
                            In order for you to achieve results, you must progress. The most effective way to do this is either by increasing the repetitions performed or by increasing the resistance. A repetition (or rep) is one complete motion of a single exercise.  When you perform a number of consecutive repetitions this is known as a set.  For example, if you performed one push up this is a rep and if you did 10 push ups this would be called a set of push ups.
                        </p>
                        <br/>
                        <p>
                            To ensure that you progress, if you were able to perform 10 push ups on your knees, your goal should be to aim for 11 or 12 or even more if possible during your next session. Once you can do that, the goal would then to see how many you can do on your toes before dropping to your knees. Always remember 'If nothing changes, nothing changes'.
                        </p>
                        <br/>
                        <p>
                            To check your session history, select the 'Session History' tab above.
                        </p>
                    </div>
                    
                </div>
                
                <div id="wsNotAVailable" clientidmode="Static" runat="server" Visible="False">
                    <div style="margin: 0 auto;width: ">
                        <h2>You don't have current or upcoming weights session this week</h2>
                    </div>
                </div>

       <!--   </div>
</div>-->


<script type="text/javascript">
    $(document).ready(function () {
        $("div").children('input').bind('keypress', function (e) {
            return (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57) && e.which != 46) ? false : true;
        });
    });
</script>