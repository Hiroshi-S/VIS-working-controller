<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EatingPlanner.ascx.cs" Inherits="VisionPersonalTrainingProject.usercontrols.VVT.VVT_Initial_Steps.EatingPlanner" %>

<div class="eatingplan" style="margin-right: 10px !important;">
    <br/>
        <h3 class="introTitle">Eating Recommendation</h3><br/>
        <p>Based on your goals, body type, activity level and body composition. Your macronutrient requirements are: </p>
        <ul id="ulEatingPlan">
            <li>Carbohydrates : <asp:Label ID="lblCHO" runat="server" Text="Label"></asp:Label> grams</li>
            <li>Protein : <asp:Label ID="lblPTN" runat="server" Text="Label"></asp:Label> grams</li>
            <li>Fat : <asp:Label ID="lblFAT" runat="server" Text="Label"></asp:Label> grams</li>
        </ul>
                    
        <div class="button-small vision_red rounded3 gotofooddiaryDiv" onclick="window.open('/club-vision/my-eating/food-diary/','_self')" >
                Create your eating plan now
        </div>
        
        <div class="button-small vision_navy rounded3 gotofooddiaryDiv" onclick="window.open('/club-vision/Redirecting?page=/club-vision/my-eating/menus/?tab=visionsmenus','_self')" >
                Review sample daily plan
        </div>
                    
        <br/><br/>
        <div id="eatingExplanation">
        <h3  class="introTitle">Why you have been given nutrition macros and what do they mean?</h3>

        <p
        style="line-height: 1.5; padding: 10px 0 10px 0; text-align: justify;">
        Now that you have completed all initials steps in Getting Started
        with your health goals, you have been given your own personal
        macronutrient goals. So what is a macronutrient? It is a nutrient
        that the body requires for optimum health in relatively large
        amounts - eg Carbohydrates, Protein and Fats (plus water). They
        provide energy to the body as well as perform several other vital
        functions. The vitamins and minerals contained within these food
        groups are known as micronutrients.</p>

        <p
        style="line-height: 1.5; padding: 10px 0 10px 0; text-align: justify;">
        Please note that these are specific to you, your body type and your
        current goal. If you wish to change your goal at a later date these
        will be recalculated to ensure you are on track to either lose fat
        or gain muscle.</p>

        <p
        style="line-height: 1.5; padding: 10px 0 10px 0; text-align: justify;">
        When completing your daily eating plan you should aim to get as
        close as possible to the macro goal, plus or minus 10 is considered
        excellent. Should you exceed or not meet your goals one day, then
        do not beat yourself up, simply assess your food diary and see
        which areas could be improved upon to prevent a repeat
        occurrence.</p>

        <p
        style="line-height: 1.5; padding: 10px 0 0px 0; text-align: justify;">
        When attempting to meet your macro requirements it is recommended
        that you:</p>

        <ul style="line-height: 1.5;">
        <li>Meet the goals in 5-6 meals.</li>

        <li>Incorporate protein and carbohydrates into each meal..</li>

        <li>Reduce the amount of carbohydrate intake as the day progresses
        (if fat loss is the goal).</li>

        <li>Choose low GI foods in the early part of the day.</li>

        <li>Choose foods that are high in vitamins and minerals.</li>

        <li>Increase the amount of protein and 'good' fat as the day
        progresses.</li>

        <li>Eliminate 'sugars' and 'bad' fats where possible.</li>

        <li>Select foods you like and can realistically prepare.</li>
        </ul>

        <br />
            <br />
        <h3  class="introTitle">So let's find out more about each macronutrient:</h3>

        <br />
        <h3>Carbohydrate</h3>

        <p
        style="line-height: 1.5; padding: 10px 0 0px 0; text-align: justify;">
        Carbohydrates contain 16kJ of energy per gram.</p>

        <p
        style="line-height: 1.5; padding: 10px 0 0px 0; text-align: justify;">
        Many diets in the market place recommend eliminating carbohydrates
        if they wish to lose weight and as such they have gained negative
        press. This is simply not the case, starches and sugars are our
        major source of energy as well as essential fibre.</p>

        <p
        style="line-height: 1.5; padding: 10px 0 0px 0; text-align: justify;">
        Carbohydrates can be classified into two groups - simple and
        complex. Simple carbohydrates (or sugars) are very energy dense and
        absorbed quickly into the body. Naturally occurring sugars are
        found in honey, fruit and milk, whilst processed sugars come in the
        form of lollies, table sugar and molasses. To reduce your sugar
        intake, foods such as chocolate, biscuits, cakes, baked chips,
        pastries and sweets should be limited.</p>

        <p
        style="line-height: 1.5; padding: 10px 0 0px 0; text-align: justify;">
        Complex carbohydrates can be found in whole foods such as cereals,
        grains, breads, pasta, rice and starchy vegetables such as potato,
        pumpkin or squash. Not only are these slower digesting foods,
        meaning you feel fuller for longer when you eat them but you will
        also have a sustained energy release. They also provide essential
        fibre to maintain a healthy digestive system.</p>

        <p
        style="line-height: 1.5; padding: 10px 0 0px 0; text-align: justify;">
        It is recommended to choose wholegrain versions of white processed
        food for maximum health benefits, for example substitute white
        bread with wholegrain, normal pasta with wholemeal pasta, white
        rice with brown as well as remove processed cereals for a more
        nutritious muesli or oats option.</p>

        <p
        style="line-height: 1.5; padding: 10px 0 0px 0; text-align: justify;">
        Fibrous vegetables such as spinach, bok choy, salad greens,
        cucumber, tomato, celery, green beans, broccoli and cauliflower are
        full of nutrients whilst being low in energy. It is possible to eat
        a lot of these foods without over spilling on your carbohydrate
        requirements and you should look to fill your plate with these.</p>

        <p
        style="line-height: 1.5; padding: 10px 0 0px 0; text-align: justify;">
        Carbohydrate intake should be higher at the start of your day and
        tapered off towards the end. Complex carbohydrates are recommended
        to be eaten at breakfast to provide a sustained energy release to
        keep you going until your next meal. As your day ends your plate
        should be filled with fresh salad and/or vegetables.</p>

        <br />
        <h3>Protein</h3>

        <p
        style="line-height: 1.5; padding: 10px 0 0px 0; text-align: justify;">
        Protein contains 17kJ of energy per gram.</p>

        <p
        style="line-height: 1.5; padding: 10px 0 0px 0; text-align: justify;">
        Protein contains amino acids which are considered to be the
        building blocks of life as they are responsible for the repair,
        growth and regeneration of all cells within the body. Protein
        provides satiety and is recommended to be eaten at every meal to
        avoid over-consumption of carbohydrates.</p>

        <p
        style="line-height: 1.5; padding: 10px 0 0px 0; text-align: justify;">
        Protein in the past has been associated with being fattening due to
        high protein foods often containing high levels of saturated fat -
        think chicken with skin on, fatty cuts of meat, hamburgers and
        sausages.</p>

        <p
        style="line-height: 1.5; padding: 10px 0 0px 0; text-align: justify;">
        The best choices of lean protein to choose include chicken breast
        (no skin), turkey, fish, kangaroo, seafood, lean steak, healthy
        heart mince and eggs. Protein doesn't only come from meat, you can
        also find it in tofu, lentils, legumes and grains such as
        quinoa.</p>

        <br />
        <h3>Fat</h3>

        <p
        style="line-height: 1.5; padding: 10px 0 0px 0; text-align: justify;">
        Fat contains 37kJ of energy per gram.</p>

        <p
        style="line-height: 1.5; padding: 10px 0 0px 0; text-align: justify;">
        We need a portion of fat in our diet as it is essential for
        maintaining blood sugar levels, manufacturing hormones and
        transportation of vitamins A, D, E &amp; K. The trouble is many
        people tend to eat the wrong fats - that is saturated and
        hydrogenated that is found in fast food, ice cream, margarine,
        butter, cheese and chocolate.</p>

        <p
        style="line-height: 1.5; padding: 10px 0 0px 0; text-align: justify;">
        Good fats come from monounsaturated sources such as olive, canola
        and peanut oils as well as avocado, nuts and seeds. The key is to
        remember to keep the portion size small.</p>

        <p
        style="line-height: 1.5; padding: 10px 0 0px 0; text-align: justify;">
        Polyunsaturated fats provide the essential omega 3's and 6's. Omega
        3's are abundant in fish, particularly salmon, mackerel and
        sardines as well as walnuts and linseeds. These fats actually help
        prevent blood clotting. Omega 6's are found in foods that come from
        plant sources such as sunflower, corn and safflower oil as well as
        sesame and sunflower seeds.</p>

        <br />
        <h3>Water</h3>

        <p
        style="line-height: 1.5; padding: 10px 0 0px 0; text-align: justify;">
        Our body is comprised of predominantly water and is vital to health
        is as it plays a vital role in nearly every bodily function from
        digestion, nutrient absorption, circulation, toxin removal and
        temperature regulation. It is recommended to drink minimum of 2
        litres of water daily.</p>

        <p
        style="line-height: 1.5; padding: 10px 0 0px 0; text-align: justify;">
        For more information on these individual macronutrients please
        check out the associated videos on VisionTV or refer to the <a href="http://www.amazon.com/Ready-Steps-Better-Health-ebook/dp/B00BWHJI26/ref=sr_1_6?ie=UTF8&qid=1368500300&sr=8-6&keywords=ready+set+go" target="_blank">Ready,
        Set, Go</a> book.</p>
       </div>
       
       <div class="realButtonDiv" style="width: auto; height: 50px;display: none;" id="EatingPlannerButtonsDiv">
            <div style="float: left">
                <asp:imagebutton ID="EatingPlanButtonBack" ImageUrl="/images/buttonBack.gif" 
                    runat="server" onclick="EatingPlanButtonBack_Click"></asp:imagebutton>
            </div>
    
            <div style="float: right">
                
                <asp:imagebutton ID="EatingPlanButtonNext" ImageUrl="/images/buttonFinish.gif" 
                        runat="server" ClientIDMode="Static"
                    onclick="EatingPlanButtonNextClick"></asp:imagebutton>
            </div>
        </div>
        
        <div class="istepsDiv" style="margin-top: 40px;">
                <div class="istepsWrapper">
                    <div style="border-left: none;" onclick="window.open('/club-vision/account-setup/exercise-planner/', '_self');">
                        <div><img src="/images/icons/web/prevarrow.png" alt="picture"/></div>
                        <div>Prev</div>
                    </div>
                    <div id="nextDiv" onclick="$('#EatingPlanButtonNext').click();return false;">
                        <div><img src="/images/icons/web/nextarrow.png" alt="picture"/></div>
                        <div>Finish!</div>
                    </div>
                </div>
            </div>
                
   </div>
   
<script type="text/javascript">
       $("#flat7").addClass("active");
</script>
    