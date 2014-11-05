using System;
using System.Globalization;
using System.Linq;

namespace VisionPersonalTrainingProject.usercontrols.clubvision
{
    public partial class Tools_EnergyCalculator : System.Web.UI.UserControl
    {
        private decimal _walkingKjs = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                ClubVisionDataContext cvdc = new ClubVisionDataContext();
                string weight = "";
                double closestWeight = 0.00;
                double closestWeightAlco = 0.00;

                switch ((string)Session["MemberType"])
                {
                    case "VPT":
                        {
                            weight = (from custs in cvdc.CustomerWeights
                                      where custs.CustomerId == (int) Session["MemberNo"]
                                      select custs).OrderByDescending(x => x.WeightDate).Select(x => x.Weight).
                                FirstOrDefault().ToString(CultureInfo.InvariantCulture);
                        } break;
                    case "VVT":
                        {
                            weight = (from custe in cvdc.CustomerWeights
                                      where custe.CustomerId == (int) Session["MemberNo"]
                                      select custe).OrderByDescending(x => x.WeightDate).
                                      Take(1).Select(x => x.Weight).SingleOrDefault().ToString(CultureInfo.InvariantCulture);
                        } break;
                }

                closestWeight = GetClosestWeight(Convert.ToDouble(weight));

                weightLabel.Text = weight;
                closestWeightLabel.Text = closestWeight.ToString(CultureInfo.InvariantCulture);
                exerciseOptionLabel.Text = RenderExerciseMacroList(closestWeight);
                LiteralAlcohol.Text = RenderYummyAlcoholPictures(closestWeight);

                closestWeightLabel.Style["display"] = "none";
                exerciseOptionLabel.Style["display"] = "none";
                walkingKjLabel.Style["display"] = "none";
                literalSearchByCategory_MorningTea.Text = CategorySearchForTools("searchByCategory_MorningTea_Result");
            }
        }

        public string CategorySearchForTools(string resultElement)
        {
            ClubVisionDataContext cvdc = new ClubVisionDataContext();

            var categories = (from category in cvdc.Categories
                              orderby category.Name
                              select category);

            string returnValue = "<div style=\"margin-top: 10px; height: 124px; overflow: auto\">";

            foreach (Category category in categories)
            {
                if(category.Id != 5)
                {
                    returnValue += "<a style=\"font-weight: bold; display: inline-block;width: 190px; padding-bottom: 10px\" onclick=\"show('" + resultElement + "');getItemsByCategoryTools(" + category.Id + ",'" + resultElement + "');\">" + category.Name + "</a>";
                }
            }

            returnValue += "</div>";

            return returnValue;
        }

        protected string RenderYummyPictures()
        {
            ClubVisionDataContext cvdc = new ClubVisionDataContext();

            var picCollate = (from pcc in cvdc.EnergyCalculatorFoods
                              select pcc);

            string yummpic = "";

            foreach (var pcl in picCollate)
            {
                yummpic += "<div class=\"imgCell150px187pxRed\">" +
                                "<div class=\"imgBox94px94px\">" +
                                    "<img src=\"/images/energy_calculator/"+pcl.ImageUrl+"\" alt border=\"0\"/>"+
                                "</div>" +
                                "<h5>" +
                                    "<a href=\"#\" onclick=\"energyCalculateItem('" + pcl.iID + "');return false;\" data-cho=\"" + pcl.Carbohydrate + "\" data-ptn=\"" + pcl.Protein + "\" data-fat=\"" + pcl.Fat + "\">" + pcl.Name + "</a>"+
                                "</h5>"+
                           "</div>";

            }
            return yummpic;
        }

        protected string RenderYummyAlcoholPictures(double weights)
        {
            try
            {
                ClubVisionDataContext cvdc = new ClubVisionDataContext();

                var picCollate = (from pcc in cvdc.ItemAlcohols
                                  // where pcc.fKGs == weights
                                  select pcc);

                string yummpic = "";

                foreach (var pcl in picCollate)
                {
                    yummpic += "<div class=\"imgCell150px187pxRed\">" +
                                    "<div>" +
                                        "<img src=\"/images/energy_calculator/" + pcl.imageUrl + "\" alt border=\"0\" height=\"70\"/>" +
                                    "</div>" +
                                    "<h5>" +
                                        "<a href=\"#\" onclick=\"energyCalculateItemVer2($(this));return false;\" data-kjs=\"" + pcl.KJs + "\" " +
                                            "data-kjsperone=\"" + (pcl.KJs / pcl.ServeAmount) + "\" data-serveamount=\"" + pcl.ServeAmount + "\" " +
                                            "data-serveunit=\"" + pcl.ServeUnit + "\" data-exercisemins=\"" + getExerciseMinutes(weights, pcl.KJs) + "\">" + pcl.Name + "</a>" +
                                    "</h5>" +
                               "</div>";
                }

                if (yummpic.Equals(""))
                {
                    return "Sorry an error has occured. Please report to admin@visionvirtualtraining.com.au. Thank you";
                }
                return yummpic;
            }
            catch(Exception ex)
            {
                return ex.ToString();
            }
           
        }

        protected string RenderExerciseMacroList(double closestweight)
        {
            ClubVisionDataContext cvdc = new ClubVisionDataContext();

            var exlists = (from xlis in cvdc.MacroCalculators
                           where xlis.fKGs == closestweight
                           select xlis).OrderBy(x => x.cExercise);

            string exlist = "";

            foreach (var exitem in exlists)
            {
                exlist += "<option value=\"" + exitem.fKJs + "\">" + exitem.cExercise +"</option>";
                if(exitem.iExerciseID == 78)
                {
                    walkingKjLabel.Text = exitem.fKJs.ToString("0.00");
                    _walkingKjs = (decimal) exitem.fKJs;
                }
            }

            return exlist;
        }

        protected double GetClosestWeight(double weight)
        {
            try
            {
                ClubVisionDataContext cvdc = new ClubVisionDataContext();
                var exerciselist = (from exlist in cvdc.MacroCalculators
                                    where exlist.fKGs >= weight
                                    select exlist.fKGs).Distinct();

                double minDistance = exerciselist.Min(n => Math.Abs(weight - n));
                return exerciselist.First(n => Math.Abs(weight - n) == minDistance);
            }
            catch(Exception ex){
                //return max weight
                return 135;
            }
            
        }

        protected double GetClosestWeightAlcohol(double weight)
        {
            ClubVisionDataContext cvdc = new ClubVisionDataContext(); 

            if(weight >= 60 && weight <= 100)
            {
                var exerciselist = (from exlist in cvdc.MacroCalculatorAlcohols
                                    where exlist.fKGs >= weight
                                    select exlist.fKGs).Distinct();

                double minDistance = exerciselist.Min(n => Math.Abs(weight - n));
                return exerciselist.First(n => Math.Abs(weight - n) == minDistance);
            }
            else
            {
                return weight < 60 ? 60 : 100;
            }
        }

        protected int getExerciseMinutes(double closestWeight, decimal totKjs)
        {

            var exMacro = _walkingKjs;

            decimal exKJsMinute = totKjs > 0 ? totKjs / exMacro * 60 : 0;

            return Convert.ToInt32(exKJsMinute);
        }

    }
}