using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;

namespace VisionPersonalTrainingProject.usercontrols.VVT
{
    public partial class ShareDailyPlan : System.Web.UI.UserControl
    {
        private System.DateTime when = System.DateTime.Today;
        private int _serve = 1;
        private bool _isFound = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {   
                    string code = Request.QueryString["code"];
                    string sharetype = code.Substring(0,2);
                    string detailtext = "";

                    ClubVisionDataContext cvdc = new ClubVisionDataContext();

                    if (sharetype.Equals("ml"))
                    {
                        foodDiaryScript.Text = "<script> $(document).ready(function () { hideRowsforShareMealDisplay(); } );</script>";
                    }
                    var shareDetail = (from sdls in cvdc.ShareDetails
                                        where sdls.cCode == code
                                        select sdls).SingleOrDefault();


                    when = (DateTime)shareDetail.dWhen;
                    string name = "";

                    if(shareDetail.iCustomerId < 2000000 || shareDetail.iCustomerId >= 1000000000)
                    {
                        var cust = (from cs in cvdc.Customers
                                where cs.Id == shareDetail.iCustomerId
                                select cs).SingleOrDefault();

                        if (cust != null)
                        {
                            name = cust.Firstname + " " + cust.LastName;
                        }
                    }
                    else
                    {
                        var cust = (from cs in cvdc.Customer_Externals
                                    where cs.iID == shareDetail.iCustomerId
                                    select cs).SingleOrDefault();

                        if (cust != null)
                        {
                            name = cust.cFirstName + " " + cust.cLastName;
                        }
                    }

                    
                    switch (sharetype)
                    {
                        case "dp": //Food Diary
                            {
                                var foodDiaryDetail = (from fdd in cvdc.DiaryEntries
                                                       where fdd.CustomerId == shareDetail.iCustomerId
                                                       where fdd.When == shareDetail.dWhen
                                                       select fdd);
                                if (foodDiaryDetail.Any())
                                {
                                    detailtext = "Food Diary share on " + shareDetail.dWhen.Value.ToLongDateString();
                                    servespan.InnerText = "1";
                                    mealDescription.InnerHtml = "<p style=\"font-style: italic;\">No description available</p>";
                                    _isFound = true;
                                    cvdc.CopyDiaryEntryToShareEntry(shareDetail.iCustomerId, shareDetail.dWhen, code);                
                                }
                            }
                            break;
                        case "mn": //Daily Plan
                            {
                                var menuDetail = (from rawmenu in cvdc.Menus
                                                    where rawmenu.Id == shareDetail.iMenuId
                                                    select rawmenu).SingleOrDefault();
                                if(menuDetail != null)
                                {
                                    detailtext = menuDetail.Name;
                                    mealImg.Src = "/images/menus/" + menuDetail.ImageUrl;
                                    servespan.InnerText = "1";
                                    mealDescription.InnerHtml = "<p style=\"font-style: italic;\">No description available</p>";
                                    _isFound = true;
                                    cvdc.CopyMenuToShareEntry(shareDetail.iCustomerId, Convert.ToInt32(menuDetail.Id), DateTime.Now, code);
                                }                            
                            }
                            break;
                        case "ml": //Meal
                            {
                                var mealDetail= (from rawmeal in cvdc.Meals
                                                  where rawmeal.Id == shareDetail.iMealId
                                                  select rawmeal).SingleOrDefault();
                                if(mealDetail != null)
                                {
                                    detailtext = mealDetail.Name;
                                    mealImg.Src = "/images/meals/" + mealDetail.ImageUrl;
                                    servespan.InnerText = mealDetail.iPortion.ToString();
                                    _serve = Convert.ToInt32(mealDetail.iPortion);
                                    mealDescription.InnerHtml = "<p>" + mealDetail.cDescription + "</p>";
                                    if (mealDetail.PDFAddress != null)
                                    {
                                        lnkDL.Attributes["onclick"] = "window.open('/club-vision/recipe/" + mealDetail.PDFAddress + "','_blank');";
                                        lnkDL.Visible = true;
                                    }
                                    _isFound = true;
                                    cvdc.CopyMealToShareEntry(shareDetail.iCustomerId, Convert.ToInt32(mealDetail.Id), DateTime.Now, 1, code);
                                }
                            }
                            
                            break;
                    }

                    if(_isFound == false)
                    {
                        eFoodDiary.Visible = false;
                        divNotFound.Visible = true;
                        divNotFound.InnerHtml = "<h1 style=\"width:800px;margin: 0 auto;\">Ooops, this plan was originally shared by <span style=\"color:#C60C30;\">" + name + "</span> and for some reason is no longer available.</h1>";
                    }
                    else
                    {
                        divNotFound.Visible = false;
                        UpdateViewSharePlan(code);

                        mealNameDiv.InnerHtml = detailtext + "<br/><span style=\"font-size:13px;\">By : " + name + "</span>";

                        shareDetail.iViewed++;
                        cvdc.SubmitChanges();

                        if (Request.QueryString["print"] == "true")
                        {
                            foodDiaryScript.Text += "<script> $(document).ready(function () { PrinttoShare() } );</script>";
                        }
                    }

                    
                }
            }
            catch(Exception ex)
            {
                Response.Redirect("~/error-page");
                //Response.Write(ex.ToString());
            }
        }

        private string ServeUnit(ShareEntry diaryEntry, int mealTimeId)
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
                foreach (Item item in items)
                {
                    if (item.Id == diaryEntry.Item.Id)
                    {
                        returnValue = item.ServeUnit;
                    }
                }
            }

            return returnValue;
        }

        private string ServeAddedUnit(ShareEntry diaryEntry, int mealTimeId)
        {
            string returnValue = diaryEntry.ItemAdd.ServeUnit;

            ClubVisionDataContext cvdc = new ClubVisionDataContext();
            var items = (from item in cvdc.ItemAdds
                         where item.Name == diaryEntry.ItemAdd.Name
                         //where item.CustomerId == (int)Session["MemberNo"]
                         where diaryEntry.FromItemsAdd == null
                         select item);

            if (items.Count() > 1)
            {
               
                foreach (ItemAdd item in items)
                {
                    if (item.Id == diaryEntry.ItemAdd.Id)
                    {
                        returnValue = item.ServeUnit;
                    }
                }
            }

            return returnValue;
        }

        private void UpdateViewSharePlan(string ccode)
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

            //todo: The code below is very rubbish if you have time please tidy it up like you did to Food Diary or Menu section. Thanks. Dewi

            List<Literal> literalRows = new List<Literal>() { literalBreakfastRows, literalMorningTeaRows, literalLunchRows, literalAfternoonTeaRows, literalDinnerRows, literalSupperRows };
            List<Literal> literalCarbs = new List<Literal>() { literalBreakfastCarb, literalMorningTeaCarb, literalLunchCarb, literalAfternoonTeaCarb, literalDinnerCarb, literalSupperCarb };
            List<Literal> literalPtns = new List<Literal>() { literalBreakfastProtein, literalMorningTeaProtein, literalLunchProtein, literalAfternoonTeaProtein, literalDinnerProtein, literalSupperProtein };
            List<Literal> literalFats = new List<Literal>() { literalBreakfastFat, literalMorningTeaFat, literalLunchFat, literalAfternoonTeaFat, literalDinnerFat, literalSupperFat };
            bool isFirst = true;

            for (int i = 1; i <= 6; i++)
            {
                int i1 = i;
                int liti = i - 1;

                var diaryEntries = (from de in cvdc.ShareEntries // Stored Procedure [dbo].[FoodDiaryEntries]((int)Session["MemberNo"],when,1)
                                    where de.MealTimeId == i1 // Can Delete if use Stored Procedure
                                    where de.cCode == ccode // Can Delete if use Stored Procedure
                                    orderby de.Created // Can Delete if use Stored Procedure
                                    select de);

                carbohydrate_section = 0;
                protein_section = 0;
                fat_section = 0;

                //just added
                int MealNumber = 0;

                foreach (ShareEntry diaryEntry in diaryEntries)
                {
                    if (diaryEntry.FromMealId != null && MealNumber != diaryEntry.FromMealId)
                    {
                        var themeal = (from mlname in cvdc.Meals
                                       where mlname.Id == diaryEntry.FromMealId
                                       select mlname).SingleOrDefault();
                        if (themeal != null)
                        {
                            literalRows[liti].Text += "<tr class=\"FoodDiaryMealTitle\" name=\"" + diaryEntry.MealTimeId + "-" + diaryEntry.FromMealId + "\"><td colspan=\"8\" style=\"font-family: Arial; font-style:bold; font-size: 12px; text-align: left; padding: " + (isFirst ? "0px 12px" : "0 12px 6px 12px") + ";\">";
                            literalRows[liti].Text += "<div style=\"float:left;margin-top: 6px; margin-right:10px;\"><a href=\"#\" title=\"Individual meal can be viewed from your VVT account\">" + themeal.Name + "</a></div>";
                            if (themeal.VideoId != null)
                            {
                                var themealbrightcove = (from tmbc in cvdc.MealFromBrightCoves
                                                         where tmbc.VideoId == themeal.VideoId
                                                         select tmbc).SingleOrDefault();
                                if (themealbrightcove != null)
                                    literalRows[liti].Text += "<a href=\"#\" title=\"Individual recipe can be downloaded from your VVT account\"><img src=\"/images/iconDownload.png\" alt=\"Download Recipe\" title=\"Individual recipe can be downloaded from your VVT account\"/></a>";
                                literalRows[liti].Text += "<a href=\"#\" title=\"Video can be viewed from your VVT account\"><img src=\"/images/iconPlay.png\" alt=\"Play Video\" title=\"Play Video\"/></a>";
                            }
                            if (themeal.VideoId == null && themeal.PDFAddress != null)
                            {
                                literalRows[liti].Text += "<a href=\"#\" title=\"Individual recipe can be downloaded from your VVT account\"><img src=\"/images/iconDownload.png\" alt=\"Download Recipe\" title=\"Download Recipe\"/></a>";
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
                        literalRows[liti].Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + diaryEntry.Amount.ToString("0.00") + "</td>";
                        literalRows[liti].Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + ServeAddedUnit(diaryEntry, i1) + "</td>";
                        literalRows[liti].Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.ItemAdd.Carbohydrate * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount)).ToString("0.00") + "</td>";
                        literalRows[liti].Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.ItemAdd.Protein * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount)).ToString("0.00") + "</td>";
                        literalRows[liti].Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.ItemAdd.Fat * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount)).ToString("0.00") + "</td>";
                        literalRows[liti].Text += "<td style=\"text-align: center; padding: " + (isFirst ? "6px 0" : "0 0 6px 0") + ";\"></td></tr>";

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
                        literalRows[liti].Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + diaryEntry.Amount.ToString("0.00") + "</td>";
                        literalRows[liti].Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + ServeUnit(diaryEntry, i1) + "</td>";
                        literalRows[liti].Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.Item.Carbohydrate * (diaryEntry.Amount / diaryEntry.Item.ServeAmount)).ToString("0.00") + "</td>";
                        literalRows[liti].Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.Item.Protein * (diaryEntry.Amount / diaryEntry.Item.ServeAmount)).ToString("0.00") + "</td>";
                        literalRows[liti].Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.Item.Fat * (diaryEntry.Amount / diaryEntry.Item.ServeAmount)).ToString("0.00") + "</td>";
                        literalRows[liti].Text += "<td style=\"text-align: center; padding: " + (isFirst ? "6px 0" : "0 0 6px 0") + ";\"></td></tr>";

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

            SpanCHOPortion.InnerText = (carbohydrate / _serve).ToString("0.00");
            SpanPTNPortion.InnerText = (protein / _serve).ToString("0.00");
            SpanFATPortion.InnerText = (fat / _serve).ToString("0.00");


            cvdc.Dispose();

        }
        
        protected void GenerateSharePlan()
        {
            foodDiaryScript.Text = "<script> $(document).ready(function () { ShareFoodDiaryDay() } );</script>";
        }
        
    }
}