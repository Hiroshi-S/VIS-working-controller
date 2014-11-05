<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyExerciseTab.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.clubvision.MyEating" %>

<div id="eProfileTab" class="element" style="overflow: visible"><!-- 605 -->
        <div class="replace" id="profileTab" runat="server" style="background-image: url(/images/eHdrMyExercise-TrainingDiary.gif); ">
            
            <div id="tabTrainingDiary" style="cursor: pointer; position: absolute; top: 0px; left: 0px; width: 201px; height: 45px" onclick="document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_MyExerciseTab_2_profileTab').style.backgroundImage = 'url(/images/eHdrMyExercise-TrainingDiary.gif)';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_MyExerciseTab_2_eMyTrainingDiary').style.display = 'block';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_MyExerciseTab_2_eWeightsSess').style.display = 'none';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_MyExerciseTab_2_eWorkoutWeek').style.display = 'none';callbreadcrumb('tabTrainingDiary');"></div>
            <div id="tabWeightsSess" style="cursor: pointer; position: absolute; top: 0px; left: 202px; width: 201px; height: 45px" onclick="document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_MyExerciseTab_2_profileTab').style.backgroundImage = 'url(/images/eHdrMyExercise-WeightsSess.gif)';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_MyExerciseTab_2_eMyTrainingDiary').style.display = 'none';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_MyExerciseTab_2_eWeightsSess').style.display = 'block';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_MyExerciseTab_2_eWorkoutWeek').style.display = 'none';callbreadcrumb('tabWeightsSess');"></div>
            <div id="tabWorkoutWeek" style="cursor: pointer; position: absolute; top: 0px; left: 404px; width: 201px; height: 45px" onclick="document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_MyExerciseTab_2_profileTab').style.backgroundImage = 'url(/images/eHdrMyExercise-WorkoutWeek.gif)';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_MyExerciseTab_2_eMyTrainingDiary').style.display = 'none';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_MyExerciseTab_2_eWeightsSess').style.display = 'none';document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_MyExerciseTab_2_eWorkoutWeek').style.display = 'block';callbreadcrumb('tabWorkoutWeek');"></div>
            
        </div>

        <div class="clear"></div>

        <div class="eContent eOrange" id="eMyTrainingDiary" style="height: auto;" runat="server">
           Training Diary   
        </div>
        
        <div class="eContent eOrange" id="eWeightsSess" runat="server" style="display: none;height: auto;" runat="server">
            Weight Sess
         </div>  

        <div class="eContent eOrange" id="eWorkoutWeek" style="display: none;height: auto;"  runat="server">
            Workout Week
        </div>
</div><!-- /eProfileTab -->

<script>
    $(document).ready(function () {
        var tab = getUrlVars()["tab"];

        switch (tab) {
            case 'trainingdiary':
                document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_MyExerciseTab_2_profileTab').style.backgroundImage = 'url(/images/eHdrMyExercise-TrainingDiary.gif)';
                document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_MyExerciseTab_2_eMyTrainingDiary').style.display = 'block';
                document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_MyExerciseTab_2_eWeightsSess').style.display = 'none'; 
                document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_MyExerciseTab_2_eWorkoutWeek').style.display = 'none';
                callbreadcrumb('tabTrainingDiary');
                 break;
            case 'weightssess':
                document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_MyExerciseTab_2_profileTab').style.backgroundImage = 'url(/images/eHdrMyExercise-WeightsSess.gif)';
                document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_MyExerciseTab_2_eMyTrainingDiary').style.display = 'none';
                document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_MyExerciseTab_2_eWeightsSess').style.display = 'block'; 
                document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_MyExerciseTab_2_eWorkoutWeek').style.display = 'none'; 
                callbreadcrumb('tabWeightsSess');break;
            case 'workoutweek':
                document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_MyExerciseTab_2_profileTab').style.backgroundImage = 'url(/images/eHdrMyExercise-WorkoutWeek.gif)';
                document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_MyExerciseTab_2_eMyTrainingDiary').style.display = 'none';
                document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_MyExerciseTab_2_eWeightsSess').style.display = 'none'; 
                document.getElementById('ContentPlaceHolderDefault_ContentPlaceHolderMain_MyExerciseTab_2_eWorkoutWeek').style.display = 'block'; 
                callbreadcrumb('tabWorkoutWeek'); break;
            default: break;
        }
    });
    

</script>