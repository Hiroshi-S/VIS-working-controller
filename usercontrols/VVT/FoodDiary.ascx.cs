using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using VisionPersonalTrainingProject.usercontrols.clubvision;
using Calendar = System.Globalization.Calendar;

namespace VisionPersonalTrainingProject.usercontrols.VVT
{
    public partial class FoodDiary : System.Web.UI.UserControl
    {
        private System.DateTime _when = System.DateTime.Today;
        private bool _isFirstWeek = false;
        private bool _isTrainingWeek = false;
        private string _accDay = "N/A";
        private string _accDay2 = "N/A";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Request.QueryString["when"] != null)
                {
                    _when = System.DateTime.Parse(Request.QueryString["when"]);
                }

                if (_when < DateTime.Today)
                {
                    captainIcon.Style["display"] = "none";
                }

                bdpDay.SelectedDate = _when;

                literalDay.Text = _when.ToLongDateString();

                currentDate.Value = _when.ToString("MMM dd yyyy");

                bdpWeek.SelectedDate = _when;

                if (Request.QueryString["tab"] == "week")
                {   
                    System.DateTime startDate = _when;
                    System.DateTime endDate = startDate.AddDays(7);

                    if(Session["MemberType"].Equals("VVT"))
                    {
                        
                        int offset = _when.DayOfWeek - DayOfWeek.Monday;

                        if ((int)_when.DayOfWeek == 0)
                        {
                            offset += 7;
                        }
                    
                        startDate = _when.AddDays(-offset);
                        endDate = startDate.AddDays(6);
                    }

                    if (DateTime.Today.AddDays(1) > endDate)
                    {
                        captainIconWeek.Style["display"] = "none";
                    }

                    ClubVisionDataContext cvdc = new ClubVisionDataContext();

                    var dateStartVoss = cvdc.VOSWS_GetTrainingStartDate((int)Session["MemberNo"], _when).ToList();

                    foreach (VOSWS_GetTrainingStartDateResult gtsdr in dateStartVoss)
                    {
                        if (gtsdr.Column1 != null && (Request.QueryString["when"] == null)) 
                            startDate = (DateTime)gtsdr.Column1;
                    }
                    
                    //literalWeek.Text = startDate.ToShortDateString() + " - " + endDate.ToShortDateString();
                    literalWeek.Text = startDate.ToLongDateString();

                    if (Session["MemberType"].Equals("VPT") && 
                        (startDate == dateStartVoss.Take(1).SingleOrDefault().Column1))
                    {
                        FirstLoadNotifLiteral.Text = "<br/><h2 style=\"color:#999999;width:575px; height:35px;display:inline-table;\">Your week with your trainer starts on <span style=\"color:#002147;\">" + startDate.ToString("dddd") + "</span></h2>";
                        
                        _isTrainingWeek = true;

                        if(Session["FromTrainer"].Equals("YES"))
                        {
                            if (Request.QueryString["when"] == null)//pushback one week earlier
                            {
                                Response.Redirect("/club-vision/my-eating/food-diary/?when=" + startDate.AddDays(-7).ToString("dd/MM/yyyy") + "&tab=week");
                            }
                            FirstLoadNotifLiteral.Text +=
                                "<button onclick=\"updateWeeklyReportFD();return false;\" >Update Weekly Report</button>";
                        }
                    }

                    UpdateViewWeek(startDate);
                }
                else
                {
                    UpdateViewDay(_when);

                    literalSearchByCategory_Breakfast.Text = CategorySearch(1, "searchByCategory_Breakfast_Result");
                    literalSearchByCategory_MorningTea.Text = CategorySearch(2, "searchByCategory_MorningTea_Result");
                    literalSearchByCategory_Lunch.Text = CategorySearch(3, "searchByCategory_Lunch_Result");
                    literalSearchByCategory_AfternoonTea.Text = CategorySearch(4, "searchByCategory_AfternoonTea_Result");
                    literalSearchByCategory_Dinner.Text = CategorySearch(5, "searchByCategory_Dinner_Result");
                    literalSearchByCategory_Supper.Text = CategorySearch(6, "searchByCategory_Supper_Result");

                }

                if (Request.QueryString["print"] == "true")
                {
                    foodDiaryScript.Text = "<script> $(document).ready(function () { PrintFoodDiaryWeek() } );</script>";
                }

            }
        }

        protected void buttonDayPrev_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["when"] != null)
            {
                _when = System.DateTime.Parse(Request.QueryString["when"]);
            }

            _when = _when.AddDays(-1);
            Response.Redirect("/club-vision/my-eating/food-diary/?when=" + _when.ToString("dd/MM/yyyy"));
        }

        protected void buttonDayNext_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["when"] != null)
            {
                _when = System.DateTime.Parse(Request.QueryString["when"]);
            }

            _when = _when.AddDays(1);
            Response.Redirect("/club-vision/my-eating/food-diary/?when=" + _when.ToString("dd/MM/yyyy"));
        }

        protected void bdpDay_SelectionChanged(object sender, EventArgs e)
        {
            Response.Redirect("/club-vision/my-eating/food-diary/?when=" + bdpDay.SelectedDate.ToString("dd/MM/yyyy"));
        }

        public string CategorySearch(int mealTimeId, string resultElement)
        {
            ClubVisionDataContext cvdc = new ClubVisionDataContext();

            var categories = (from category in cvdc.Categories
                              orderby category.Name
                              select category);
            /*
            string returnValue = "<div style=\"margin-top: 10px; height: 124px; overflow: auto\">";

            foreach (Category category in categories)
            {
                returnValue += "<a style=\"font-weight: bold; display: inline-block;width: 190px; padding-bottom: 10px\" onclick=\"show('" + resultElement + "');getItemsByCategory(" + category.Id + ",'" + resultElement + "', " + mealTimeId + ");\">" + category.Name + "</a>";
            }

            returnValue += "</div>";
            */

            string returnValue = "<select id=\"searchByCategory_" + GetMealTimeName(mealTimeId) + "\" onchange=\"searchItemsByAdvanced('" + GetMealTimeName(mealTimeId) + "', " + mealTimeId + ");\">"
                                + "<option value=\"-1\" selected></option>"
                                + "<option value=\"0\" style=\"font-style:italic;\">*Your Own Food Items</option>";

            foreach (Category category in categories)
            {
                returnValue += "<option value=\"" + category.Id + "\">" + category.Name + "</option>";
            }

            returnValue += "</select>";

            return returnValue;
        }

        private string ServeUnit(DiaryEntry diaryEntry, int mealTimeId)
        {
            string returnValue = diaryEntry.Item.ServeUnit;

            ClubVisionDataContext cvdc = new ClubVisionDataContext();
            var items = (from item in cvdc.Items
                         where item.Name == diaryEntry.Item.Name
                         where diaryEntry.FromItemsAdd == null
                         where item.ItemCategories.First().CategoryId == diaryEntry.Item.ItemCategories.First().CategoryId
                         select item);

            if (items.Count() > 1)
            {
                returnValue = "<select class=\"serve_unit\" onchange=\"updateServe($(this)," + mealTimeId + ");\">";
                foreach (Item item in items)
                {
                    returnValue += "<option ";
                    if (item.Id == diaryEntry.Item.Id)
                    {
                        returnValue += "selected ";
                    }
                    returnValue += "data-id=\"" + item.Id + "\" data-serve-amount=\"" + item.ServeAmount + "\"  data-serve-unit=\"" + item.ServeUnit + "\" data-carbohydrate=\"" + item.Carbohydrate + "\" data-protein=\"" + item.Protein + "\" data-fat=\"" + item.Fat + "\">" + item.ServeUnit + "</option>";
                }
                returnValue += "</select>";
            }

            return returnValue;
        }

        private string ServeAddedUnit(DiaryEntry diaryEntry, int mealTimeId)
        {
            string returnValue = diaryEntry.ItemAdd.ServeUnit;

            ClubVisionDataContext cvdc = new ClubVisionDataContext();
            var items = (from item in cvdc.ItemAdds
                         where item.Name == diaryEntry.ItemAdd.Name
                         where diaryEntry.FromItemsAdd == null
                         where item.CustomerId == (int)Session["MemberNo"]
                         //where item.ItemCategories.First().CategoryId == diaryEntry.Item.ItemCategories.First().CategoryId
                         select item);

            if (items.Count() > 1)
            {
                returnValue = "<select class=\"serve_unit\" onchange=\"updateServe($(this)," + mealTimeId + ");\">";
                foreach (ItemAdd item in items)
                {
                    returnValue += "<option ";
                    if (item.Id == diaryEntry.ItemAdd.Id)
                    {
                        returnValue += "selected ";
                    }
                    returnValue += "data-id=\"" + item.Id + "\" data-serve-amount=\"" + item.ServeAmount + "\"  data-serve-unit=\"" + item.ServeUnit + "\" data-carbohydrate=\"" + item.Carbohydrate + "\" data-protein=\"" + item.Protein + "\" data-fat=\"" + item.Fat + "\">" + item.ServeUnit + "</option>";
                }
                returnValue += "</select>";
            }

            return returnValue;
        }

        private void UpdateViewDay(System.DateTime when)
        {
            int hl = -1;
            if (Request.QueryString["hl"] != null)
            {
                hl = int.Parse(Request.QueryString["hl"]);
            }

            decimal carbohydrate = 0;
            decimal protein = 0;
            decimal fat = 0;

            decimal carbohydrate_section = 0;
            decimal protein_section = 0;
            decimal fat_section = 0;

            ClubVisionDataContext cvdc = new ClubVisionDataContext();

            if (when <= DateTime.Today)
            {
                List<Literal> litMood = new List<Literal> { LiteralBreakfastMood, LiteralMorningTeaMood, LiteralLunchMood, LiteralAfternoonTeaMood, LiteralDinnerMood, LiteralSupperMood };

                var MoodsOfTheDay = (from moo in cvdc.CustomerMoods
                                     where moo.CustomerId == (int)Session["MemberNo"]
                                     where moo.When == when
                                     select moo).OrderBy(x => x.MealTimeId);

                for (var i = 0; i <= 5; i++)
                {
                    CustomerMood currentMood = MoodsOfTheDay.FirstOrDefault(x => x.MealTimeId == i + 1);

                    if (currentMood != null)
                    {
                        litMood[i].Text = "<img id=\"mood-" + when.ToString("ddd") + "-" + (i + 1) + "\" title=\"Your mood -- " + currentMood.Mood.MoodString + "\" " +
                                          "src=\"/images/icons/moods_small/" + currentMood.Mood.fileName + "\" height=\"20\" onclick=\"foodDiaryLoadMoodPallete('" + when.ToShortDateString() + "', '" + (i + 1) + "', '" + when.ToString("ddd") + "');return false;\"/>";
                    }
                    else
                    {
                        litMood[i].Text = "<img id=\"mood-" + when.ToString("ddd") + "-" + (i + 1) + "\" title=\"Click here to insert your mood\" " +
                                          "src=\"/images/face-question.gif\" height=\"20\" onclick=\"foodDiaryLoadMoodPallete('" + when.ToShortDateString() + "', '" + (i + 1) + "', '" + when.ToString("ddd") + "');return false;\"/>";
                    }
                }
            }

            List<Literal> literalRows = new List<Literal>() { literalBreakfastRows, literalMorningTeaRows, literalLunchRows, literalAfternoonTeaRows, literalDinnerRows, literalSupperRows };
            List<Literal> literalCarbs = new List<Literal>() { literalBreakfastCarb, literalMorningTeaCarb, literalLunchCarb, literalAfternoonTeaCarb, literalDinnerCarb, literalSupperCarb };
            List<Literal> literalPtns = new List<Literal>() { literalBreakfastProtein, literalMorningTeaProtein, literalLunchProtein, literalAfternoonTeaProtein, literalDinnerProtein, literalSupperProtein };
            List<Literal> literalFats = new List<Literal>() { literalBreakfastFat, literalMorningTeaFat, literalLunchFat, literalAfternoonTeaFat, literalDinnerFat, literalSupperFat };
            bool isFirst = true;

            for (int i = 1; i <= 6; i++)
            {
                /************************************************************************************************
                 *                begin meal section simplified by Dewi Candraningsih                           *
                 ************************************************************************************************/
                int i1 = i;
                int liti = i - 1;

                var diaryEntries = (from de in cvdc.DiaryEntries // Stored Procedure [dbo].[FoodDiaryEntries]((int)Session["MemberNo"],when,1)
                                    where de.MealTimeId == i1 // Can Delete if use Stored Procedure
                                    where de.CustomerId == (int)Session["MemberNo"] // Can Delete if use Stored Procedure
                                    where de.When == when // Can Delete if use Stored Procedur
                                    orderby de.Created // Can Delete if use Stored Procedure
                                    select de);
                carbohydrate_section = 0;
                protein_section = 0;
                fat_section = 0;

                //just added
                int MealNumber = 0;

                foreach (DiaryEntry diaryEntry in diaryEntries)
                {
                    if (diaryEntry.FromMealId != null && MealNumber != diaryEntry.FromMealId)
                    {
                        var themeal = (from mlname in cvdc.Meals
                                       where mlname.Id == diaryEntry.FromMealId
                                       select mlname).SingleOrDefault();
                        if (themeal != null)
                        {
                            literalRows[liti].Text += "<tr class=\"FoodDiaryMealTitle\" name=\"" + diaryEntry.MealTimeId + "-" + diaryEntry.FromMealId + "\"><td colspan=\"8\" style=\"font-family: Arial; font-style:bold; font-size: 12px; text-align: left; padding: " + (isFirst ? "0px 12px" : "0 12px 6px 12px") + ";\">";
                            literalRows[liti].Text += "<div style=\"float:left;margin-top: 6px; margin-right:10px;\"><a href=\"/club-vision/my-eating/menus/?tab=" + (themeal.CustomerId == (int)Session["MemberNo"] ? "edit" : "view") + "_meal&mealId=" + themeal.Id + "\">" + themeal.Name + "</a></div>";
                            if (themeal.VideoId != null)
                            {
                                var themealbrightcove = (from tmbc in cvdc.MealFromBrightCoves
                                                         where tmbc.VideoId == themeal.VideoId
                                                         select tmbc).SingleOrDefault();
                                if (themealbrightcove != null)
                                    literalRows[liti].Text += "<a href=\"/media" + themealbrightcove.DownloadPDF + "\" target=\"_blank\"><img src=\"/images/iconDownload.png\" alt=\"Download Recipe\" title=\"Download Recipe\"/></a>";
                                literalRows[liti].Text += "<a href=\"/club-vision/education/vision-tv?vid=" + themeal.VideoId + "\"><img src=\"/images/iconPlay.png\" alt=\"Play Video\" title=\"Play Video\"/></a>";
                            }
                            if (themeal.VideoId == null && themeal.PDFAddress != null)
                            {
                                literalRows[liti].Text += "<a href=\"/club-vision/recipe/" + themeal.PDFAddress + "\" target=\"_blank\"><img src=\"/images/iconDownload.png\" alt=\"Download Recipe\" title=\"Download Recipe\"/></a>";
                            }
                            literalRows[liti].Text += "</td></tr>";
                            MealNumber = themeal.Id;
                        }
                    }
                    if (diaryEntry.FromItemsAdd == true)
                    {
                        if (hl == diaryEntry.Id)
                        {
                            literalRows[liti].Text += "<tr data-id=\"" + diaryEntry.Id + "\" style=\"background-color: #f8dcc8\"><td colspan=\"2\" style=\"font-family: Arial; font-style:italic; font-size: 12px; text-align: left; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (MealNumber == diaryEntry.FromMealId ? "<span style=\"color:#E27423;\">&nbsp;&nbsp;&raquo;&nbsp;&nbsp;</span>" : "") + "*" + diaryEntry.ItemAdd.Name + "</td>";
                        }
                        else
                        {
                            literalRows[liti].Text += "<tr data-id=\"" + diaryEntry.Id + "\"><td colspan=\"2\" style=\"font-family: Arial; font-style:italic; font-size: 12px; text-align: left; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (MealNumber == diaryEntry.FromMealId ? "<span style=\"color:#E27423;\">&nbsp;&nbsp;&raquo;&nbsp;&nbsp;</span>" : "") + "*" + diaryEntry.ItemAdd.Name + "</td>";
                        }
                        literalRows[liti].Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\"><input class=\"diary_amount\" type=\"text\" id=\"editAmount" + diaryEntry.Id + "\" value=\"" + diaryEntry.Amount.ToString("0.00") + "\" onblur=\"editAmount(" + diaryEntry.Id + ", " + i1 + ", $(this));\" data-serve-amount=\"" + diaryEntry.ItemAdd.ServeAmount + "\" data-carbohydrate=\"" + diaryEntry.ItemAdd.Carbohydrate + "\" data-protein=\"" + diaryEntry.ItemAdd.Protein + "\" data-fat=\"" + diaryEntry.ItemAdd.Fat + "\" ></td>";
                        literalRows[liti].Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: left; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + ServeAddedUnit(diaryEntry, i1) + "</td>";
                        literalRows[liti].Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.ItemAdd.Carbohydrate * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount)).ToString("0.00") + "</td>";
                        literalRows[liti].Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.ItemAdd.Protein * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount)).ToString("0.00") + "</td>";
                        literalRows[liti].Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.ItemAdd.Fat * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount)).ToString("0.00") + "</td>";
                        literalRows[liti].Text += "<td style=\"text-align: center; padding: " + (isFirst ? "6px 0" : "0 0 6px 0") + ";\">" + "<a onclick=\"deleteRow(" + diaryEntry.Id + "," + i1 + ");$(this).parent().parent().remove();\" style=\"cursor: pointer;position: relative;left: -12px;\"><img src=\"/images/delete" + (hl == diaryEntry.Id ? "_hl" : "") + ".gif\" border=\"0\"></a>" + "</td></tr>";

                        carbohydrate += (diaryEntry.ItemAdd.Carbohydrate * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount));
                        protein += (diaryEntry.ItemAdd.Protein * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount));
                        fat += (diaryEntry.ItemAdd.Fat * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount));

                        carbohydrate_section += (diaryEntry.ItemAdd.Carbohydrate * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount));
                        protein_section += (diaryEntry.ItemAdd.Protein * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount));
                        fat_section += (diaryEntry.ItemAdd.Fat * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount));

                        if (i == 1)
                        {
                            if (isFirst)
                            {
                                isFirst = false;
                            }
                        }
                    }
                    else
                    {
                        if (hl == diaryEntry.Id)
                        {
                            literalRows[liti].Text += "<tr data-id=\"" + diaryEntry.Id + "\" style=\"background-color: #f8dcc8\"><td colspan=\"2\" style=\"font-family: Arial; font-size: 12px; text-align: left; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (MealNumber == diaryEntry.FromMealId ? "<span style=\"color:#E27423;\">&nbsp;&nbsp;&raquo;&nbsp;&nbsp;</span>" : "") + diaryEntry.Item.Name + "</td>";
                        }
                        else
                        {
                            literalRows[liti].Text += "<tr data-id=\"" + diaryEntry.Id + "\"><td colspan=\"2\" style=\"font-family: Arial; font-size: 12px; text-align: left; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (MealNumber == diaryEntry.FromMealId ? "<span style=\"color:#E27423;\">&nbsp;&nbsp;&raquo;&nbsp;&nbsp;</span>" : "") + diaryEntry.Item.Name + "</td>";
                        }
                        literalRows[liti].Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\"><input class=\"diary_amount\" type=\"text\" id=\"editAmount" + diaryEntry.Id + "\" value=\"" + diaryEntry.Amount.ToString("0.00") + "\" onblur=\"editAmount(" + diaryEntry.Id + ", " + i1 + ", $(this));\" data-serve-amount=\"" + diaryEntry.Item.ServeAmount + "\" data-carbohydrate=\"" + diaryEntry.Item.Carbohydrate + "\" data-protein=\"" + diaryEntry.Item.Protein + "\" data-fat=\"" + diaryEntry.Item.Fat + "\" ></td>";
                        literalRows[liti].Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: left; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + ServeUnit(diaryEntry, i1) + "</td>";
                        literalRows[liti].Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.Item.Carbohydrate * (diaryEntry.Amount / diaryEntry.Item.ServeAmount)).ToString("0.00") + "</td>";
                        literalRows[liti].Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.Item.Protein * (diaryEntry.Amount / diaryEntry.Item.ServeAmount)).ToString("0.00") + "</td>";
                        literalRows[liti].Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.Item.Fat * (diaryEntry.Amount / diaryEntry.Item.ServeAmount)).ToString("0.00") + "</td>";
                        literalRows[liti].Text += "<td style=\"text-align: center; padding: " + (isFirst ? "6px 0" : "0 0 6px 0") + ";\">" + "<a onclick=\"deleteRow(" + diaryEntry.Id + "," + i1 + ");$(this).parent().parent().remove();\" style=\"cursor: pointer;position: relative;left: -12px;\"><img src=\"/images/delete" + (hl == diaryEntry.Id ? "_hl" : "") + ".gif\" border=\"0\"></a>" + "</td></tr>";

                        carbohydrate += (diaryEntry.Item.Carbohydrate * (diaryEntry.Amount / diaryEntry.Item.ServeAmount));
                        protein += (diaryEntry.Item.Protein * (diaryEntry.Amount / diaryEntry.Item.ServeAmount));
                        fat += (diaryEntry.Item.Fat * (diaryEntry.Amount / diaryEntry.Item.ServeAmount));

                        carbohydrate_section += (diaryEntry.Item.Carbohydrate * (diaryEntry.Amount / diaryEntry.Item.ServeAmount)); // All ".Item" will be deleted if use Stored Procedure
                        protein_section += (diaryEntry.Item.Protein * (diaryEntry.Amount / diaryEntry.Item.ServeAmount));
                        fat_section += (diaryEntry.Item.Fat * (diaryEntry.Amount / diaryEntry.Item.ServeAmount));

                        if (isFirst)
                        {
                            isFirst = false;
                        }
                    }
                }

                literalCarbs[liti].Text = carbohydrate_section.ToString("0.00");
                literalPtns[liti].Text = protein_section.ToString("0.00");
                literalFats[liti].Text = fat_section.ToString("0.00");


            }


            literalTotalCarb.Text = carbohydrate.ToString("0.00");
            literalTotalProtein.Text = protein.ToString("0.00");
            literalTotalFat.Text = fat.ToString("0.00");

            decimal goal_carbohydrate = 0;
            decimal goal_protein = 0;
            decimal goal_fat = 0;

            switch ((string)Session["MemberType"])
            {
                case "VPT":
                    {
                        var customers = (from cu in cvdc.Customers
                                         where cu.Id == (int)Session["MemberNo"]
                                         select cu);

                        foreach (Customer customer in customers)
                        {
                            if (GetAccelDay(when) != when.DayOfWeek.ToString())
                            {
                                goal_carbohydrate = customer.Carb;
                                goal_protein = customer.Protein;
                                goal_fat = customer.Fat;

                            }
                            else
                            {
                                goal_carbohydrate = 30;
                                goal_protein = customer.Protein + 30;
                                goal_fat = customer.Fat + 30;
                                dailyGoalText.InnerText = "Accelerator Day Goals";

                                //dailyGoalText.Style["Color"] = "#008CA7";
                                //dailyGoalText.Style["font-weight"] = "bold";

                                const string s = "<script type=\"text/javascript\">" +
                                                "$(\"#macro-accel\").css(\"display\", \"\");" +
                                                "$(\"#macro-goal\").css(\"display\", \"none\");" +
                                                "$(\"#macro-diff\").css(\"display\", \"none\");</script>";

                                Page.ClientScript.RegisterStartupScript(GetType(), "test", s);
                            }

                        }
                    } break;
                case "VVT":
                    {
                        var custexternal = (from cex in cvdc.Customer_Externals
                                            where cex.iID == (int)Session["MemberNo"]
                                            select cex).SingleOrDefault();

                        var firstOrDefault = custexternal.Goals.OrderByDescending(x => x.dDateCreated).FirstOrDefault();
                        if (firstOrDefault != null)
                        {
                            goal_carbohydrate = Convert.ToDecimal(firstOrDefault.CHO);
                            goal_protein = Convert.ToDecimal(firstOrDefault.PTN);
                            goal_fat = Convert.ToDecimal(firstOrDefault.FAT);
                        }
                    } break;

            }
            
            if (when > DateTime.Now)
            {
                foodDiaryDayCheck.Visible = false;
            }
            else
            {
                var foodDiaryChecks = (from fdc in cvdc.DailyFoodDiarySummaries
                                       where fdc.CustomerId == (int)Session["MemberNo"]
                                       where fdc.dWhen == when
                                       select fdc);

                foreach (DailyFoodDiarySummary dailyFoodDiarySummary in foodDiaryChecks)
                {
                    if (dailyFoodDiarySummary.bComplete)
                    {
                        foodDiaryDayCheckImg.Src = "/images/icons/web/checked.jpg";
                        foodDiaryDayCheckImg.Attributes["data-value"] = "True";
                    }
                }
            }

            literalGoalCarb.Text = goal_carbohydrate.ToString("0.00");
            literalGoalProtein.Text = goal_protein.ToString("0.00");
            literalGoalFat.Text = goal_fat.ToString("0.00");

            decimal carb_diff = carbohydrate - goal_carbohydrate;
            decimal ptn_diff = protein - goal_protein;
            decimal fat_diff = fat - goal_fat;

            literalDiffCarb.Text = carb_diff.ToString("0.00");
            literalDiffProtein.Text = ptn_diff.ToString("0.00");
            literalDiffFat.Text = fat_diff.ToString("0.00");

            cvdc.Dispose();

        }

        protected string CopyToDates(int mealTimeId, string hideElement, string resultElement)
        {
            string returnValue = "";

            for (int offset = 0; offset <= 6; offset++)
            {
                // ToDo: Tomorrow, Today, Yesterday
                returnValue += "<div class=\"quicktools_option\"><a onclick=\"CopyDiaryMealToDate(" + mealTimeId + ", '" + System.DateTime.Today.AddDays(offset).ToString("dd/MM/yyyy") + "', '" + resultElement + "');hide('" + hideElement + "');\">" + System.DateTime.Today.AddDays(offset).ToLongDateString() + "</a></div>";
            }


            return returnValue;
        }

        protected string CopyToDatesDropDown(string elementId)
        {
            string returnValue = "<select id=\"" + elementId + "\">";

            for (int offset = 0; offset <= 6; offset++)
            {
                returnValue += "<option value=\"" + System.DateTime.Today.AddDays(offset).ToString("yyyy-MM-dd");
                returnValue += "\" ";
                if (offset == 0)
                {
                    returnValue += "selected ";
                }
                returnValue += ">" + System.DateTime.Today.AddDays(offset).ToLongDateString() + "</option>";
            }

            returnValue += "</select> ";

            return returnValue;
        }

        protected void UpdateViewWeek(System.DateTime startDate)
        {
            decimal goalCHO = 0, goalPTN = 0, goalFAT = 0;
            string custWeight = "";

            Dictionary<DateTime, string> accelDays = new Dictionary<DateTime, string>();

            ClubVisionDataContext cvdc = new ClubVisionDataContext();

            switch ((string)Session["MemberType"])
            {
                case "VPT":
                    {
                        accelDayForVPT.Visible = true;
                        var _customers = (from customers in cvdc.Customers
                                          where customers.Id == (int)Session["MemberNo"]
                                          select customers);

                        foreach (Customer customer in _customers)
                        {
                            goalCHO = customer.Carb;
                            goalPTN = customer.Protein;
                            goalFAT = customer.Fat;
                            decimal goalKJs = (goalCHO * 16) + (goalPTN * 17) + (goalFAT * 37);
                            literalMacros.Text = "Carbohydrate: <span id=\"wtot-cho\">" + customer.Carb.ToString("0.0") + "</span> Protein: <span id=\"wtot-ptn\">" + customer.Protein.ToString("0.0") + "</span> Fat: <span id=\"wtot-fat\">" + customer.Fat.ToString("0.0") + "</span>" +
                                                 "<span id=\"goalKJs\" style=\"color: #FFFFFF;\">" + goalKJs.ToString("0.0") + "</span>";
                            custWeight = customer.CurrentWt;
                            int kg_pos = custWeight.IndexOf("kg");
                            custWeight = custWeight.Remove(kg_pos); //todo : this is not quite right cause it gets latest weight regardless the date
                        }
                        AccelDaysGridLiteral.Text = GetAccelDaysVPT(startDate);
                    } break;
                case "VVT":
                    {
                        accelDayForVVT.Visible = true;
                        var customers_Ext = (from customers in cvdc.Customer_Externals
                                             where customers.iID == (int)Session["MemberNo"]
                                             select customers).SingleOrDefault();

                        if (customers_Ext != null)
                        {
                            var firstOrDefault = customers_Ext.Goals.OrderByDescending(x => x.dDateCreated).FirstOrDefault();
                            if (firstOrDefault != null)
                            {
                                goalCHO = (decimal)firstOrDefault.CHO;
                                goalPTN = (decimal)firstOrDefault.PTN;
                                goalFAT = (decimal)firstOrDefault.FAT;
                                decimal goalKJs = (goalCHO * 16) + (goalPTN * 17) + (goalFAT * 37);
                                literalMacros.Text = "Carbohydrate: <span id=\"wtot-cho\">" + goalCHO.ToString("0.0") + "</span> Protein: <span id=\"wtot-ptn\">" + goalPTN.ToString("0.0") + "</span> Fat: <span id=\"wtot-fat\">" + goalFAT.ToString("0.0") + "</span>" +
                                     "<span id=\"goalKJs\" style=\"color: #FFFFFF;\">" + goalKJs.ToString("0.0") + "</span>";
                                custWeight =
                                    customers_Ext.Measurements.OrderByDescending(x => x.dMeasured).FirstOrDefault().fBodyWeight.ToString();
                            }
                        }

                        accelDays.Add(DateTime.Parse("2000-01-01"), "N/A");
                        for (int i = 0; i < 7; i++)
                        {
                            accelDays.Add(startDate.AddDays(i), startDate.AddDays(i).ToString("dddd"));
                        }

                        AccelDropDownList1.DataSource = accelDays;
                        AccelDropDownList1.DataValueField = "Key";
                        AccelDropDownList1.DataTextField = "Value";
                        AccelDropDownList1.DataBind();

                        _accDay = GetAccelDay(startDate);

                        AccelDropDownList1.Items.FindByText(_accDay).Selected = true;
                    } break;
            }

            literalAveMacros.Text = GetWeeklyMacro(startDate, _accDay, _accDay2, goalCHO, goalPTN, goalFAT, custWeight);
            List<Literal> literalDateLinks = new List<Literal>() { literalDay1DateLink, literalDay2DateLink, literalDay3DateLink, literalDay4DateLink, literalDay5DateLink, literalDay6DateLink, literalDay7DateLink };
            List<Literal> literalMenus = new List<Literal>() { literalDay1Menu, literalDay2Menu, literalDay3Menu, literalDay4Menu, literalDay5Menu, literalDay6Menu, literalDay7Menu };
            List<Literal> literalMacrosDiffs = new List<Literal>() { literalDay1MacrosDiff, literalDay2MacrosDiff, literalDay3MacrosDiff, literalDay4MacrosDiff, literalDay5MacrosDiff, literalDay6MacrosDiff, literalDay7MacrosDiff };
            List<Literal> literalBreakfasts = new List<Literal>() { literalDay1Breakfast, literalDay2Breakfast, literalDay3Breakfast, literalDay4Breakfast, literalDay5Breakfast, literalDay6Breakfast, literalDay7Breakfast };
            List<Literal> literalMorningTeas = new List<Literal>() { literalDay1MorningTea, literalDay2MorningTea, literalDay3MorningTea, literalDay4MorningTea, literalDay5MorningTea, literalDay6MorningTea, literalDay7MorningTea };
            List<Literal> literalLunchs = new List<Literal>() { literalDay1Lunch, literalDay2Lunch, literalDay3Lunch, literalDay4Lunch, literalDay5Lunch, literalDay6Lunch, literalDay7Lunch };
            List<Literal> literalAfternoonTeas = new List<Literal>() { literalDay1AfternoonTea, literalDay2AfternoonTea, literalDay3AfternoonTea, literalDay4AfternoonTea, literalDay5AfternoonTea, literalDay6AfternoonTea, literalDay7AfternoonTea };
            List<Literal> literalDinners = new List<Literal>() { literalDay1Dinner, literalDay2Dinner, literalDay3Dinner, literalDay4Dinner, literalDay5Dinner, literalDay6Dinner, literalDay7Dinner };
            List<Literal> literalSuppers = new List<Literal>() { literalDay1Supper, literalDay2Supper, literalDay3Supper, literalDay4Supper, literalDay5Supper, literalDay6Supper, literalDay7Supper };
            List<Literal> literalMacross = new List<Literal>() { literalDay1Macros, literalDay2Macros, literalDay3Macros, literalDay4Macros, literalDay5Macros, literalDay6Macros, literalDay7Macros };

            System.DateTime currentDateDT = new DateTime();

            for (int i = 1; i <= 7; i++)
            {
                if (i == 1)
                {
                    currentDateDT = startDate;
                    _isFirstWeek = true;
                    currentDate.Value = currentDateDT.ToString("MMM dd yyyy");
                }
                else
                {
                    _isFirstWeek = false;
                    currentDateDT = currentDateDT.AddDays(1);
                }
                int liti = i - 1;
                
                /*
                int dayOfWeek = (int)currentDate.DayOfWeek;
                if (dayOfWeek == 0) dayOfWeek = 7;
                */
                int dayOfWeek = i;

                literalDateLinks[liti].Text = GetLiteralDateLink(currentDateDT, _accDay, _accDay2, dayOfWeek);
                literalMenus[liti].Text = GetMenu(currentDateDT, dayOfWeek);
                literalMacrosDiffs[liti].Text = GetMacrosDiff(currentDateDT, goalCHO, goalPTN, goalFAT, _accDay, _accDay2, dayOfWeek);
                literalBreakfasts[liti].Text = GetMealTimeList(currentDateDT, 1, dayOfWeek);
                literalMorningTeas[liti].Text = GetMealTimeList(currentDateDT, 2, dayOfWeek);
                literalLunchs[liti].Text = GetMealTimeList(currentDateDT, 3, dayOfWeek);
                literalAfternoonTeas[liti].Text = GetMealTimeList(currentDateDT, 4, dayOfWeek);
                literalDinners[liti].Text = GetMealTimeList(currentDateDT, 5, dayOfWeek);
                literalSuppers[liti].Text = GetMealTimeList(currentDateDT, 6, dayOfWeek);
                literalMacross[liti].Text = GetMacrosDiff(currentDateDT, goalCHO, goalPTN, goalFAT, _accDay, _accDay2, dayOfWeek);//GetMacros(currentDate);
                //currentDate = _when.ToString("MMM dd yyyy");
            }

            cvdc.Dispose();
        }

        protected void AccelDropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime thisday = Convert.ToDateTime(AccelDropDownList1.Items.FindByText("Monday").Value);
            ClubVisionDataContext cvdc = new ClubVisionDataContext();
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;

            int year = thisday.Year;
            int week = cal.GetWeekOfYear(thisday, dfi.CalendarWeekRule,
                                            dfi.FirstDayOfWeek);

            //todo : this will be removed and changed to budi's stored procedure to return closest date but double check with data in vision's db
            var accelDay = (from accelDays in cvdc.WeeklyAccelDaySummaries
                            where accelDays.CustomerId == (int)Session["MemberNo"]
                            where accelDays.iYear == year
                            where accelDays.iWeekNumber == week
                            select accelDays).SingleOrDefault();

            if (accelDay != null) //means data in result set
            {
                accelDay.dAcceleratorDay = AccelDropDownList1.SelectedItem.Text != "N/A" ?
                    Convert.ToDateTime(AccelDropDownList1.SelectedValue) : Convert.ToDateTime(AccelDropDownList1.Items.FindByText("N/A").Value);
            }
            else
            {
                WeeklyAccelDaySummary wads = new WeeklyAccelDaySummary();
                wads.CustomerId = (int)Session["MemberNo"];
                wads.iWeekNumber = week;
                wads.iYear = year;
                wads.dAcceleratorDay = AccelDropDownList1.SelectedItem.Text != "N/A" ?
                    Convert.ToDateTime(AccelDropDownList1.SelectedValue) : Convert.ToDateTime(AccelDropDownList1.Items.FindByText("N/A").Value);
                //wads.dAcceleratorDay = thisday;
                cvdc.WeeklyAccelDaySummaries.InsertOnSubmit(wads);
            }
            cvdc.SubmitChanges();
            cvdc.Dispose();
            Response.Redirect(Request.RawUrl);

        }

        protected void ChangeStartDayDList_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private string GetAccelDay(System.DateTime startDate)
        {
            ClubVisionDataContext cvdc = new ClubVisionDataContext();

            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;

            int year = startDate.Year;
            int week = cal.GetWeekOfYear(startDate, dfi.CalendarWeekRule,
                                         dfi.FirstDayOfWeek);

            var accelDay = (from accelDays in cvdc.WeeklyAccelDaySummaries
                            where accelDays.CustomerId == (int)Session["MemberNo"]
                            where accelDays.iYear == year
                            where accelDays.iWeekNumber == week
                            select accelDays.dAcceleratorDay).FirstOrDefault();

            if (accelDay.Date.Equals(DateTime.Parse("2000-01-01")))
            {
                return "N/A";
            }

            if (accelDay.Year == 1) //means no data in result set
            {
                switch ((string)Session["MemberType"])
                {
                    case "VPT":
                        {
                            return (from custs in cvdc.Customers
                                    where custs.Id == (int)Session["MemberNo"]
                                    select custs.AcceleratorDay).SingleOrDefault();
                        } break;
                    case "VVT":
                        {
                            return "N/A";
                        } break;
                }
            }
            cvdc.Dispose();
            return accelDay.DayOfWeek.ToString();
        }

        private string GetAccelDaysVPT(System.DateTime startDate)
        {
            string returnVal = null;

            returnVal = _isTrainingWeek ? "<select id=\"accDaysOption\" onchange=\"saveChangesAccelDay();return false;\" >" :
                                            "<select id=\"accDaysOption\" onchange=\"saveFailAccelDay();return false;\" >";

            ClubVisionDataContext cvdc = new ClubVisionDataContext();

            var accelDayInDB = (from acidb in cvdc.WeeklyAccelDaySummaries
                                where acidb.CustomerId == (int) Session["MemberNo"]
                                where acidb.dAcceleratorDay >= startDate
                                where acidb.dAcceleratorDay < startDate.AddDays(7)
                                select acidb.dAcceleratorDay);
            int count = 0;

            for (int i = 0; i < 7; i++)
            {
                string selected = "";
                if (i == 0)
                {
                    returnVal += "<option value=\"N/A\">N/A</option>";
                }

                if(accelDayInDB.Contains(startDate.AddDays(i)))
                {
                    selected = "selected";
                    
                    if(count > 0)
                    {
                        _accDay2 = startDate.AddDays(i).ToString("dd-MM-yyyy");
                    }
                    else
                    {
                        _accDay = startDate.AddDays(i).ToString("dd-MM-yyyy");
                    }
                    count++;
                }

                int weeknum = TrainingDiaryScreen.GetWeekNumber(startDate.AddDays(0));

                returnVal += "<option " + selected + " data-week=\"" + (weeknum).ToString().Substring(4, 2) + "\" " +
                             "data-year=\"" + (weeknum).ToString().Substring(0, 4) + "\" " +
                             "value=\"" + startDate.AddDays(i).ToString("dd/MM/yyyy") + "\">" + startDate.AddDays(i).ToString("dddd dd/MM") + "</option>";

            }
            returnVal += "</select>";
            
            return returnVal;
        }
        
        public string GetWeeklyMacro(System.DateTime startDate, string accDay, string accDay2, decimal gcho, decimal gptn, decimal gfat, string custWeight)
        {
            ClubVisionDataContext cvdc = new ClubVisionDataContext();

            int weeknum = TrainingDiaryScreen.GetWeekNumber(startDate);

            decimal carbs = 0;
            decimal protein = 0;
            decimal fat = 0;
            string returnValue = "";

            var foodDiaryChecks = (from fdc in cvdc.DailyFoodDiarySummaries
                                   where fdc.CustomerId == (int)Session["MemberNo"]
                                   where fdc.dWhen >= startDate
                                   where fdc.dWhen < startDate.AddDays(7)
                                   where fdc.bComplete
                                   select fdc.dWhen.DayOfWeek).ToList();

            if (!accDay.Equals("N/A") && Session["MemberType"].Equals("VVT")) //remove accelerator day from calculation
            {
                foodDiaryChecks.Remove((DayOfWeek)Enum.Parse(typeof(DayOfWeek), accDay));
            }

            if (!accDay.Equals("N/A") && Session["MemberType"].Equals("VPT")) //remove accelerator day from calculation
            {
                accDay = Convert.ToDateTime(accDay).DayOfWeek.ToString();
                foodDiaryChecks.Remove((DayOfWeek)Enum.Parse(typeof(DayOfWeek), accDay));
            }

            if (!accDay2.Equals("N/A") && Session["MemberType"].Equals("VPT")) //remove accelerator day from calculation
            {
                accDay2 = Convert.ToDateTime(accDay2).DayOfWeek.ToString();
                foodDiaryChecks.Remove((DayOfWeek)Enum.Parse(typeof(DayOfWeek), accDay2));
            }

            var diaryEntries = (from de in cvdc.DiaryEntries
                                where de.CustomerId == (int)Session["MemberNo"]
                                where de.When >= startDate
                                where de.When < startDate.AddDays(7)
                                orderby de.Created
                                select de);

            foreach (DiaryEntry diaryEntry in diaryEntries)
            {
                if (foodDiaryChecks.All(c => c != diaryEntry.When.DayOfWeek)) continue;
                switch (diaryEntry.FromItemsAdd)
                {
                    case null:
                        carbs += diaryEntry.Item.Carbohydrate * (diaryEntry.Amount / diaryEntry.Item.ServeAmount);
                        protein += diaryEntry.Item.Protein * (diaryEntry.Amount / diaryEntry.Item.ServeAmount);
                        fat += diaryEntry.Item.Fat * (diaryEntry.Amount / diaryEntry.Item.ServeAmount);
                        break;
                    case true:
                        carbs += diaryEntry.ItemAdd.Carbohydrate * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount);
                        protein += diaryEntry.ItemAdd.Protein * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount);
                        fat += diaryEntry.ItemAdd.Fat * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount);
                        break;
                }
            }

            int daysCompleted = foodDiaryChecks.Count;

            if (daysCompleted > 0)
            {
                carbs = carbs / daysCompleted;
                protein = protein / daysCompleted;
                fat = fat / daysCompleted;

                if ((carbs <= gcho + 10 && carbs >= gcho - 10) &&
                    (protein <= gptn + 10 && protein >= gptn - 10) &&
                    (fat <= gfat + 10 && fat >= gfat - 10))
                {
                    var quote = (from quotes in cvdc.MotivationalQuotes
                                 where quotes.iMsgId == 1
                                 select quotes.cMotivationalMsg).ToList();
                    Random rand = new Random();

                    if (quote.Any()) weeklyFoodMotivQuote.InnerText = "\"" + quote.ElementAt(rand.Next(quote.Count())) + "\"";
                }
            }

            decimal actualKJs = (carbs * 16) + (protein * 17) + (fat * 37);

            returnValue = "Carbohydrate: <span id=\"wave-cho\">" + carbs.ToString("0.0") + "</span> Protein: <span id=\"wave-ptn\">" + protein.ToString("0.0") + "</span> Fat: <span id=\"wave-fat\">" + fat.ToString("0.0") + "</span>";
            returnValue += "<span id=\"weeknum\" style=\"color: #FFFFFF;\">" + weeknum + "</span>";
            returnValue += "<span id=\"custWeight\" style=\"color: #FFFFFF;\">" + custWeight + "</span>";
            returnValue += "<span id=\"actualKJs\" style=\"color: #FFFFFF;\">" + actualKJs.ToString("0.0") + "</span>";
            //totmacroavetd.Visible = true;

            if (carbs > 0 || protein > 0 || fat > 0)
            {
                totmacroavetd.Style["display"] = "table-row";
                returnValue += "<span id=\"showtotmacroavetd\" style=\"color: #FFFFFF;\">table-row</span>";
            }
            else
            {
                totmacroavetd.Style["display"] = "none";
                returnValue += "<span id=\"showtotmacroavetd\" style=\"color: #FFFFFF;\">none</span>";
            }

            cvdc.Dispose();
            return returnValue;
        }

        private string GetMealName(int? id)
        {
            string returnValue = "";

            if (id != null)
            {
                ClubVisionDataContext cvdc = new ClubVisionDataContext();
                var meals = (from m in cvdc.Meals
                             where m.Id == id
                             select m);

                foreach (Meal meal in meals)
                {
                    returnValue = meal.Name;
                }
            }
            return returnValue;
        }

        private string GetMenu(System.DateTime when, int dayOfWeek)
        {
            string returnValue = "";
            int? menuId = null;

            ClubVisionDataContext cvdc = new ClubVisionDataContext();

            var diaryEntries = (from de in cvdc.DiaryEntries
                                where de.CustomerId == (int)Session["MemberNo"]
                                where de.When == when
                                orderby de.Created
                                select de);

            foreach (DiaryEntry diaryEntry in diaryEntries)
            {
                if (diaryEntry.FromMenuId != null)
                {
                    menuId = diaryEntry.FromMenuId;
                }
            }

            if (menuId != null)
            {
                var menus = (from m in cvdc.Menus
                             where m.Id == (int)menuId
                             select m);

                foreach (Menu menu in menus)
                {
                    if (menu.IsRecommended)
                    {
                        returnValue = "<a class=\"menu-drag ui-draggable\" data-type=\"menu\" data-menuid=\"" + menu.Id + "\" href=\"/club-vision/my-eating/menus/?tab=view&menuId=" + menu.Id + "\">" + menu.Name + "</a>";
                    }
                    else
                    {
                        returnValue = "<a class=\"menu-drag ui-draggable\" data-type=\"menu\" data-menuid=\"" + menu.Id + "\" href=\"/club-vision/my-eating/menus/?tab=edit&menuId=" + menu.Id + "\">" + menu.Name + "</a>";
                    }
                }
            }

            cvdc.Dispose();

            if (returnValue == "" && when >= System.DateTime.Today)
            {
                returnValue = "<a href=\"/club-vision/my-eating/menus/?when=" + when.ToString("dd/MM/yyyy") + "\"><img src=\"/images/buttonAddMenu.gif\" border=\"0\"></a>";
            }
            
            return "<td class=\"foodDairyWeekMenu\" data-day=\"" + dayOfWeek + "\">" + returnValue + "</td>";
        }

        private string GetLiteralDateLink(System.DateTime when, string accDay, string accDay2, int dayOfWeek)
        {
            ClubVisionDataContext cvdc = new ClubVisionDataContext();

            var foodDiaryChecks = (from fdc in cvdc.DailyFoodDiarySummaries
                                   where fdc.CustomerId == (int)Session["MemberNo"]
                                   where fdc.dWhen == when
                                   where fdc.bComplete
                                   select fdc.iID);
            bool isChecked = false;
            string checkedImages = "";

            foreach (int ids in foodDiaryChecks)
            {
                isChecked = true;
            }

            string initDay = _isFirstWeek ? "id=\"startDateofTheWeek\"" : "";

            string isFinish = isChecked ? "False" : "True";

            string hideyesno = "";
            if (when > DateTime.Now)
            {
                hideyesno = "style=\"display:none;\"";
            }

            if (Session["MemberType"].Equals("VVT") && (when.DayOfWeek + "" == accDay))
            {
                checkedImages = isChecked ? "<img class=\"chkimg\" src=\"/images/checkbox_refine_checked_over_blue.gif\" style=\"padding-left:5px;\"/>" : "<img class=\"chkimg\" src=\"/images/checkbox_refine_over_blue.gif\" style=\"padding-left:5px;\"/>";
                return "<td width=\"120\" class=\"foodDairyWeekHeader\" data-day=\"" + dayOfWeek + "\" " + initDay + "><div id=\"sample1\" class=\"sample-outer accDayTitle\"><a id=\"diary-" + when.ToString("dd-MM-yyyy") + "\" style=\"color:#008CA7;\" title=\"Accelerator Day\" data-type=\"diaryday\" data-when=\"" + when.ToString("dd-MM-yyyy") + "\" class=\"diaryday-drag ui-draggable\" href=\"/club-vision/my-eating/food-diary/?when=" + when.ToString("dd-MM-yyyy") + "\">" + when.ToString("ddd dd/MM") + "</a>" +
                       "<a href=\"#\" id=\"img-" + when.ToString("dd-MM-yyyy") + "\" " + hideyesno + " onclick=\"FoodDiaryDidYouYesOrNoWeek('" + when.ToString("dd-MM-yyyy") + "', '" + isFinish + "', 'accday');return false;\">" + checkedImages + "</a></div></td>";
            }

            if (Session["MemberType"].Equals("VPT") && (when.ToString("dd-MM-yyyy") == accDay || when.ToString("dd-MM-yyyy") == accDay2))
            {
                checkedImages = isChecked ? "<img class=\"chkimg\" src=\"/images/checkbox_refine_checked_over_blue.gif\" style=\"padding-left:5px;\"/>" : "<img class=\"chkimg\" src=\"/images/checkbox_refine_over_blue.gif\" style=\"padding-left:5px;\"/>";
                return "<td width=\"120\" class=\"foodDairyWeekHeader\" data-day=\"" + dayOfWeek + "\" " + initDay + "><div id=\"sample1\" class=\"sample-outer accDayTitle\"><a id=\"diary-" + when.ToString("dd-MM-yyyy") + "\" style=\"color:#008CA7;\" title=\"Accelerator Day\" data-type=\"diaryday\" data-when=\"" + when.ToString("dd-MM-yyyy") + "\" class=\"diaryday-drag ui-draggable\" href=\"/club-vision/my-eating/food-diary/?when=" + when.ToString("dd-MM-yyyy") + "\">" + when.ToString("ddd dd/MM") + "</a>" +
                       "<a href=\"#\" id=\"img-" + when.ToString("dd-MM-yyyy") + "\" " + hideyesno + " onclick=\"FoodDiaryDidYouYesOrNoWeek('" + when.ToString("dd-MM-yyyy") + "', '" + isFinish + "', 'accday');return false;\">" + checkedImages + "</a></div></td>";
            }

            checkedImages = isChecked ? "<img class=\"chkimg\" src=\"/images/checkbox_refine_checked_over.gif\" style=\"padding-left:5px;\"/>" : "<img class=\"chkimg\" src=\"/images/checkbox_refine_over.gif\" style=\"padding-left:5px;\"/>";
            return "<td width=\"120\" class=\"foodDairyWeekHeader\" data-day=\"" + dayOfWeek + "\" " + initDay + "><a id=\"diary-" + when.ToString("dd-MM-yyyy") + "\" data-type=\"diaryday\" data-when=\"" + when.ToString("dd-MM-yyyy") + "\" class=\"diaryday-drag ui-draggable\" href=\"/club-vision/my-eating/food-diary/?when=" + when.ToString("dd-MM-yyyy") + "\">" + when.ToString("ddd dd/MM") + "</a>" +
                   "<a href=\"#\" id=\"img-" + when.ToString("dd-MM-yyyy") + "\" " + hideyesno + " onclick=\"FoodDiaryDidYouYesOrNoWeek('" + when.ToString("dd-MM-yyyy") + "', '" + isFinish + "', 'normalday');return false;\">" + checkedImages + "</a></td>";
        }

        private string GetMacrosDiff(System.DateTime when, decimal gcarb, decimal gptn, decimal gfat, string accDay, string accDay2, int dayOfWeek)
        {
            try
            {
                ClubVisionDataContext cvdc = new ClubVisionDataContext();
                bool isAccelDay = false;

                decimal carbs = 0;
                decimal protein = 0;
                decimal fat = 0;

                var diaryEntries = (from de in cvdc.DiaryEntries
                                    where de.CustomerId == (int)Session["MemberNo"]
                                    where de.When == when
                                    where de.FromItemsAdd == null
                                    orderby de.Created
                                    select de);

                foreach (DiaryEntry diaryEntry in diaryEntries)
                {
                    carbs += diaryEntry.Item.Carbohydrate * (diaryEntry.Amount / diaryEntry.Item.ServeAmount);
                    protein += diaryEntry.Item.Protein * (diaryEntry.Amount / diaryEntry.Item.ServeAmount);
                    fat += diaryEntry.Item.Fat * (diaryEntry.Amount / diaryEntry.Item.ServeAmount);
                }

                var diaryEntries2 = (from de in cvdc.DiaryEntries
                                     where de.CustomerId == (int)Session["MemberNo"]
                                     where de.When == when
                                     where de.FromItemsAdd == true
                                     orderby de.Created
                                     select de);

                foreach (DiaryEntry diaryEntry in diaryEntries2)
                {
                    carbs += diaryEntry.ItemAdd.Carbohydrate * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount);
                    protein += diaryEntry.ItemAdd.Protein * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount);
                    fat += diaryEntry.ItemAdd.Fat * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount);
                }

                string returnValue = "";

                if (Session["MemberType"].Equals("VVT") && when.DayOfWeek + "" == accDay)
                {
                    gcarb = 30;
                    gptn = gptn + 30;
                    gfat = gfat + 30;
                    isAccelDay = true;
                }

                if (Session["MemberType"].Equals("VPT") && (when.ToString("dd-MM-yyyy") == accDay || when.ToString("dd-MM-yyyy") == accDay2))
                {
                    gcarb = 30;
                    gptn = gptn + 30;
                    gfat = gfat + 30;
                    isAccelDay = true;
                }

                decimal carbsdiff = carbs - gcarb;
                decimal proteindiff = protein - gptn;
                decimal fatdiff = fat - gfat;

                if (carbs > 0 || protein > 0 || fat > 0)
                {
                    returnValue = "CHO: " + carbs.ToString("0.0") + " (" + GetMacroDiffColor(carbsdiff) + ")" +
                                  "<br>PTN: " + protein.ToString("0.0") + " (" + GetMacroDiffColor(proteindiff) + ")" +
                                  "<br>FAT: " + fat.ToString("0.0") + " (" + GetMacroDiffColor(fatdiff) + ")";
                    if (isAccelDay)
                    {
                        returnValue = "CHO: " + carbs.ToString("0.0") +
                                      "<br>PTN: " + protein.ToString("0.0") +
                                      "<br>FAT: " + fat.ToString("0.0");
                    }
                }

                return  "<td class=\"foodDairyWeekHeader day" + dayOfWeek + "macro\">" + returnValue  + "</td>";
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
                return "";

            }

        }

        private string GetMacroDiffColor(decimal macro)
        {
            string macroString = macro.ToString("0.0");

            if (macro > 0)
            {
                macroString = "+" + macroString;
            }

            if (macro >= -10 && macro <= 10)
            {
                return "<span style='color:green'>" + macroString + "</span>";
            }
            /*
            const string sss = "<script type=\"text/javascript\">" +
                             "$('#captainIcon a').showBalloon();</script>";

            Page.ClientScript.RegisterStartupScript(GetType(), "test", sss);
             */

            return "<span style='color:red'>" + macroString + "</span>";
        }

        private string GetMealTimeList(System.DateTime when, int mealTimeId, int dayOfWeek)
        {
            string returnValue = "";

            ClubVisionDataContext cvdc = new ClubVisionDataContext();

            var diaryEntries = (from de in cvdc.DiaryEntries
                                where de.MealTimeId == mealTimeId
                                where de.CustomerId == (int)Session["MemberNo"]
                                where de.When == when
                                orderby de.FromMealId
                                orderby de.Created
                                select de);

            int? currentMealId = null;
     

            foreach (DiaryEntry diaryEntry in diaryEntries)
            {
                //var dotw = (int)when.DayOfWeek;
                //if (dotw == 0) dotw = 7;
                var dotw = dayOfWeek;

                if (currentMealId != diaryEntry.FromMealId)
                {
                    if (currentMealId != null)
                    {
                        returnValue += "</div>";
                    }
                    if (diaryEntry.FromMealId != null)
                    {
                        returnValue += "<div class=\"meal-drag\" data-id=\"" + diaryEntry.FromMealId + "\" data-day=\"" + dotw + "\" data-mealtimeid=\"" + mealTimeId + "\" data-type=\"meal\">" + GetMealName(diaryEntry.FromMealId);
                    }
                    currentMealId = diaryEntry.FromMealId;
                }
                if (diaryEntry.FromItemsAdd == true)
                {
                    returnValue += "<p style=\"font-style:italic !important;\" class=\"item-drag\" data-type=\"item\" data-id=\"" + diaryEntry.Id + "\" data-day=\"" + dotw + "\" data-mealtimeid=\"" + mealTimeId + "\">*" + diaryEntry.ItemAdd.Name + "</p>";
                }
                else
                {
                    returnValue += "<p class=\"item-drag\" data-type=\"item\" data-id=\"" + diaryEntry.Id + "\" data-day=\"" + dotw + "\" data-mealtimeid=\"" + mealTimeId + "\">" + diaryEntry.Item.Name + "</p>";
                }
            }

            if (currentMealId != null)
            {
                returnValue += "</div>";
            }
            cvdc.Dispose();


            return "<td class=\"foodDairyWeekCell\" data-day=\"" + dayOfWeek + "\" data-mealtimeid=\"" + mealTimeId + "\">" + returnValue + "</td>";
        }

        protected void buttonWeekPrev_Click(object sender, EventArgs e)
        {
            DateTime date = Convert.ToDateTime(literalWeek.Text);
            date = date.AddDays(-7);
            Response.Redirect("/club-vision/my-eating/food-diary/?tab=week&when=" + date.ToString("dd/MM/yyyy"));
        }

        protected void buttonWeekNext_Click(object sender, EventArgs e)
        {

            DateTime date = Convert.ToDateTime(literalWeek.Text);
            date = date.AddDays(7);
            Response.Redirect("/club-vision/my-eating/food-diary/?tab=week&when=" + date.ToString("dd/MM/yyyy"));
        }

        protected void bdpWeek_SelectionChanged(object sender, EventArgs e)
        {
            Response.Redirect("/club-vision/my-eating/food-diary/?tab=week&when=" + bdpWeek.SelectedDate.ToString("dd/MM/yyyy"));
        }

        protected void bdpWeek_SelectionChangedWeekStart(object sender, EventArgs e)
        {

            System.DateTime startDate = bdpWeek.SelectedDate;
            System.DateTime endDate = startDate.AddDays(6);

            if (DateTime.Today.AddDays(1) > endDate)
            {
                captainIconWeek.Style["display"] = "none";
            }
            
            //literalWeek.Text = startDate.ToShortDateString() + " - " + endDate.ToShortDateString();
            literalWeek.Text = startDate.ToLongDateString();

            //FirstLoadNotifLiteral.Text = "";
            
            UpdateViewWeek(startDate);
        }
        
        protected void CaptainSuggestionWeekPlan(System.DateTime startDate)
        {
            ClubVisionDataContext cvdc = new ClubVisionDataContext();


        }

        protected void GenerateSharePlan()
        {
            foodDiaryScript.Text = "<script> $(document).ready(function () { ShareFoodDiaryDay() } );</script>";

        }

        public string GetMealTimeName(int id)
        {
            switch (id)
            {
                case 1:
                    return "Breakfast";
                case 2:
                    return "MorningTea";
                case 3:
                    return "Lunch";
                case 4:
                    return "AfternoonTea";
                case 5:
                    return "Dinner";
                case 6:
                    return "Supper";
                default:
                    return "";
            }
        }
    }
}