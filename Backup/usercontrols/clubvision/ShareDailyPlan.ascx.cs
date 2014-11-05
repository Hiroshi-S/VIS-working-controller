using System;
using System.Globalization;
using System.Linq;

namespace VisionPersonalTrainingProject.usercontrols.clubvision
{
    public partial class ShareDailyPlan : System.Web.UI.UserControl
    {
        private System.DateTime when = System.DateTime.Today;

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


                    var cust = (from cs in cvdc.Customers
                                where cs.Id == shareDetail.iCustomerId
                                select cs).SingleOrDefault();

                    if (cust != null)
                    {
                        nameLabel.Text = cust.Firstname + " " + cust.LastName;
                    }
                    else
                    {
                        nameLabel.Text = "";
                    }
                    
                    UpdateViewSharePlan(code);

                    /*
                    int offset = when.DayOfWeek - DayOfWeek.Monday;

                    if ((int)when.DayOfWeek == 0)
                    {
                        offset += 7;
                    }

                    System.DateTime startDate = when.AddDays(-offset);
                    System.DateTime endDate = startDate.AddDays(6);
                    */

                    switch (sharetype)
                    {
                        case "dp":
                            detailtext = "Food Diary share on " + shareDetail.dWhen.Value.ToLongDateString();
                            break;
                        case "mn":
                            detailtext = (from rawmenu in cvdc.Menus
                                            where rawmenu.Id == shareDetail.iMenuId
                                            select rawmenu.Name).SingleOrDefault();
                            break;
                        case "ml":
                            detailtext = (from rawmeal in cvdc.Meals
                                          where rawmeal.Id == shareDetail.iMealId
                                          select rawmeal.Name).SingleOrDefault();
                            break;
                    }

                    detailLabel.Text = detailtext;

                    shareDetail.iViewed++;
                    cvdc.SubmitChanges();

                    if (Request.QueryString["print"] == "true")
                    {
                        foodDiaryScript.Text += "<script> $(document).ready(function () { PrinttoShare() } );</script>";
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


            /************************************************************************************************
             *                                    begin breakfast section                                   *
             ************************************************************************************************/
            var diaryEntries = (from de in cvdc.ShareEntries // Stored Procedure [dbo].[FoodDiaryEntries]((int)Session["MemberNo"],when,1)
                                where de.MealTimeId == 1 // Can Delete if use Stored Procedure
                                where de.cCode == ccode // Can Delete if use Stored Procedure
                                orderby de.Created // Can Delete if use Stored Procedure
                                select de);

            carbohydrate_section = 0;
            protein_section = 0;
            fat_section = 0;

            bool isFirst = true;

            foreach (ShareEntry diaryEntry in diaryEntries)
            {
                if (diaryEntry.FromItemsAdd == true)
                {
                    if (hl == diaryEntry.Id)
                    {
                        literalBreakfastRows.Text += "<tr data-id=\"" + diaryEntry.Id + "\" style=\"background-color: #f8dcc8\"><td colspan=\"2\" style=\"font-family: Arial; font-size: 12px; text-align: left; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + diaryEntry.ItemAdd.Name + "</td>";
                    }
                    else
                    {
                        literalBreakfastRows.Text += "<tr data-id=\"" + diaryEntry.Id + "\"><td colspan=\"2\" style=\"font-family: Arial; font-style:italic; font-size: 12px; text-align: left; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">*" + diaryEntry.ItemAdd.Name + "</td>";
                    }
                    literalBreakfastRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + diaryEntry.Amount.ToString("0.00") + "</td>";
                    literalBreakfastRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: left; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + ServeAddedUnit(diaryEntry, 1) + "</td>";
                    literalBreakfastRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.ItemAdd.Carbohydrate * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount)).ToString("0.00") + "</td>";
                    literalBreakfastRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.ItemAdd.Protein * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount)).ToString("0.00") + "</td>";
                    literalBreakfastRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.ItemAdd.Fat * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount)).ToString("0.00") + "</td>";
                    literalBreakfastRows.Text += "</tr>";

                    carbohydrate += (diaryEntry.ItemAdd.Carbohydrate * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount));
                    protein += (diaryEntry.ItemAdd.Protein * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount));
                    fat += (diaryEntry.ItemAdd.Fat * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount));

                    carbohydrate_section += (diaryEntry.ItemAdd.Carbohydrate * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount));
                    protein_section += (diaryEntry.ItemAdd.Protein * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount));
                    fat_section += (diaryEntry.ItemAdd.Fat * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount));

                    if (isFirst)
                    {
                        isFirst = false;
                    }
                }
                else
                {
                    if (hl == diaryEntry.Id)
                    {
                        literalBreakfastRows.Text += "<tr data-id=\"" + diaryEntry.Id + "\" style=\"background-color: #f8dcc8\"><td colspan=\"2\" style=\"font-family: Arial; font-size: 12px; text-align: left; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + diaryEntry.Item.Name + "</td>";
                    }
                    else
                    {
                        literalBreakfastRows.Text += "<tr data-id=\"" + diaryEntry.Id + "\"><td colspan=\"2\" style=\"font-family: Arial; font-size: 12px; text-align: left; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + diaryEntry.Item.Name + "</td>";
                    }
                    literalBreakfastRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + diaryEntry.Amount.ToString("0.00") +  "</td>";
                    literalBreakfastRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: left; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + ServeUnit(diaryEntry, 1) + "</td>";
                    literalBreakfastRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.Item.Carbohydrate * (diaryEntry.Amount / diaryEntry.Item.ServeAmount)).ToString("0.00") + "</td>";
                    literalBreakfastRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.Item.Protein * (diaryEntry.Amount / diaryEntry.Item.ServeAmount)).ToString("0.00") + "</td>";
                    literalBreakfastRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.Item.Fat * (diaryEntry.Amount / diaryEntry.Item.ServeAmount)).ToString("0.00") + "</td>";
                    literalBreakfastRows.Text += "</tr>";
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

            literalBreakfastCarb.Text = carbohydrate_section.ToString("0.00");
            literalBreakfastProtein.Text = protein_section.ToString("0.00");
            literalBreakfastFat.Text = fat_section.ToString("0.00");


            /************************************************************************************************
             *                                    begin morning tea section                                 *
             ************************************************************************************************/
            diaryEntries = (from de in cvdc.ShareEntries
                            where de.MealTimeId == 2
                            where de.cCode == ccode
                            orderby de.Created
                            select de);

            carbohydrate_section = 0;
            protein_section = 0;
            fat_section = 0;

            foreach (ShareEntry diaryEntry in diaryEntries)
            {
                if (diaryEntry.FromItemsAdd == true)
                {
                    if (hl == diaryEntry.Id)
                    {
                        literalMorningTeaRows.Text += "<tr data-id=\"" + diaryEntry.Id + "\" style=\"background-color: #f8dcc8\"><td colspan=\"2\" style=\"font-family: Arial; font-size: 12px; text-align: left; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + diaryEntry.ItemAdd.Name + "</td>";
                    }
                    else
                    {
                        literalMorningTeaRows.Text += "<tr data-id=\"" + diaryEntry.Id + "\"><td colspan=\"2\" style=\"font-family: Arial;font-style:italic; font-size: 12px; text-align: left; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">*" + diaryEntry.ItemAdd.Name + "</td>";
                    }
                    literalMorningTeaRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">"+ diaryEntry.Amount.ToString("0.00") + "</td>";
                    literalMorningTeaRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: left; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + ServeAddedUnit(diaryEntry, 2) + "</td>";
                    literalMorningTeaRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.ItemAdd.Carbohydrate * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount)).ToString("0.00") + "</td>";
                    literalMorningTeaRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.ItemAdd.Protein * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount)).ToString("0.00") + "</td>";
                    literalMorningTeaRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.ItemAdd.Fat * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount)).ToString("0.00") + "</td>";
                    literalMorningTeaRows.Text += "</tr>";

                    carbohydrate += (diaryEntry.ItemAdd.Carbohydrate * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount));
                    protein += (diaryEntry.ItemAdd.Protein * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount));
                    fat += (diaryEntry.ItemAdd.Fat * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount));

                    carbohydrate_section += (diaryEntry.ItemAdd.Carbohydrate * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount));
                    protein_section += (diaryEntry.ItemAdd.Protein * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount));
                    fat_section += (diaryEntry.ItemAdd.Fat * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount));
                }
                else
                {
                    if (hl == diaryEntry.Id)
                    {
                        literalMorningTeaRows.Text += "<tr data-id=\"" + diaryEntry.Id + "\" style=\"background-color: #f8dcc8\"><td colspan=\"2\" style=\"font-family: Arial; font-size: 12px; text-align: left; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + diaryEntry.Item.Name + "</td>";
                    }
                    else
                    {
                        literalMorningTeaRows.Text += "<tr data-id=\"" + diaryEntry.Id + "\"><td colspan=\"2\" style=\"font-family: Arial; font-size: 12px; text-align: left; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + diaryEntry.Item.Name + "</td>";
                    }
                    literalMorningTeaRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + diaryEntry.Amount.ToString("0.00") + "</td>";
                    literalMorningTeaRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: left; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + ServeUnit(diaryEntry, 2) + "</td>";
                    literalMorningTeaRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.Item.Carbohydrate * (diaryEntry.Amount / diaryEntry.Item.ServeAmount)).ToString("0.00") + "</td>";
                    literalMorningTeaRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.Item.Protein * (diaryEntry.Amount / diaryEntry.Item.ServeAmount)).ToString("0.00") + "</td>";
                    literalMorningTeaRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.Item.Fat * (diaryEntry.Amount / diaryEntry.Item.ServeAmount)).ToString("0.00") + "</td>";
                    literalMorningTeaRows.Text += "</tr>";

                    carbohydrate += (diaryEntry.Item.Carbohydrate * (diaryEntry.Amount / diaryEntry.Item.ServeAmount));
                    protein += (diaryEntry.Item.Protein * (diaryEntry.Amount / diaryEntry.Item.ServeAmount));
                    fat += (diaryEntry.Item.Fat * (diaryEntry.Amount / diaryEntry.Item.ServeAmount));

                    carbohydrate_section += (diaryEntry.Item.Carbohydrate * (diaryEntry.Amount / diaryEntry.Item.ServeAmount));
                    protein_section += (diaryEntry.Item.Protein * (diaryEntry.Amount / diaryEntry.Item.ServeAmount));
                    fat_section += (diaryEntry.Item.Fat * (diaryEntry.Amount / diaryEntry.Item.ServeAmount));
                }
            }

            literalMorningTeaCarb.Text = carbohydrate_section.ToString("0.00");
            literalMorningTeaProtein.Text = protein_section.ToString("0.00");
            literalMorningTeaFat.Text = fat_section.ToString("0.00");


            /************************************************************************************************
             *                                    begin lunch section                                       *
             ************************************************************************************************/
            diaryEntries = (from de in cvdc.ShareEntries
                            where de.MealTimeId == 3
                            where de.cCode == ccode
                            orderby de.Created
                            select de);

            carbohydrate_section = 0;
            protein_section = 0;
            fat_section = 0;

            foreach (ShareEntry diaryEntry in diaryEntries)
            {
                if (diaryEntry.FromItemsAdd == true)
                {
                    if (hl == diaryEntry.Id)
                    {
                        literalLunchRows.Text += "<tr data-id=\"" + diaryEntry.Id + "\" style=\"background-color: #f8dcc8\"><td colspan=\"2\" style=\"font-family: Arial; font-size: 12px; text-align: left; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + diaryEntry.ItemAdd.Name + "</td>";
                    }
                    else
                    {
                        literalLunchRows.Text += "<tr data-id=\"" + diaryEntry.Id + "\"><td colspan=\"2\" style=\"font-family: Arial; font-style:italic; font-size: 12px; text-align: left; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">*" + diaryEntry.ItemAdd.Name + "</td>";
                    }
                    literalLunchRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + diaryEntry.Amount.ToString("0.00") + "</td>";
                    literalLunchRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: left; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + ServeAddedUnit(diaryEntry, 3) + "</td>";
                    literalLunchRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.ItemAdd.Carbohydrate * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount)).ToString("0.00") + "</td>";
                    literalLunchRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.ItemAdd.Protein * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount)).ToString("0.00") + "</td>";
                    literalLunchRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.ItemAdd.Fat * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount)).ToString("0.00") + "</td>";
                    literalLunchRows.Text += "</tr>";

                    carbohydrate += (diaryEntry.ItemAdd.Carbohydrate * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount));
                    protein += (diaryEntry.ItemAdd.Protein * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount));
                    fat += (diaryEntry.ItemAdd.Fat * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount));

                    carbohydrate_section += (diaryEntry.ItemAdd.Carbohydrate * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount));
                    protein_section += (diaryEntry.ItemAdd.Protein * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount));
                    fat_section += (diaryEntry.ItemAdd.Fat * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount));
                }
                else
                {
                    if (hl == diaryEntry.Id)
                    {
                        literalLunchRows.Text += "<tr data-id=\"" + diaryEntry.Id + "\" style=\"background-color: #f8dcc8\"><td colspan=\"2\" style=\"font-family: Arial; font-size: 12px; text-align: left; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + diaryEntry.Item.Name + "</td>";
                    }
                    else
                    {
                        literalLunchRows.Text += "<tr data-id=\"" + diaryEntry.Id + "\"><td colspan=\"2\" style=\"font-family: Arial; font-size: 12px; text-align: left; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + diaryEntry.Item.Name + "</td>";
                    }
                    literalLunchRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + diaryEntry.Amount.ToString("0.00") +  "</td>";
                    literalLunchRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: left; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + ServeUnit(diaryEntry, 3) + "</td>";
                    literalLunchRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.Item.Carbohydrate * (diaryEntry.Amount / diaryEntry.Item.ServeAmount)).ToString("0.00") + "</td>";
                    literalLunchRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.Item.Protein * (diaryEntry.Amount / diaryEntry.Item.ServeAmount)).ToString("0.00") + "</td>";
                    literalLunchRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.Item.Fat * (diaryEntry.Amount / diaryEntry.Item.ServeAmount)).ToString("0.00") + "</td>";
                    literalLunchRows.Text += "</tr>";

                    carbohydrate += (diaryEntry.Item.Carbohydrate * (diaryEntry.Amount / diaryEntry.Item.ServeAmount));
                    protein += (diaryEntry.Item.Protein * (diaryEntry.Amount / diaryEntry.Item.ServeAmount));
                    fat += (diaryEntry.Item.Fat * (diaryEntry.Amount / diaryEntry.Item.ServeAmount));

                    carbohydrate_section += (diaryEntry.Item.Carbohydrate * (diaryEntry.Amount / diaryEntry.Item.ServeAmount));
                    protein_section += (diaryEntry.Item.Protein * (diaryEntry.Amount / diaryEntry.Item.ServeAmount));
                    fat_section += (diaryEntry.Item.Fat * (diaryEntry.Amount / diaryEntry.Item.ServeAmount));
                }

            }

            literalLunchCarb.Text = carbohydrate_section.ToString("0.00");
            literalLunchProtein.Text = protein_section.ToString("0.00");
            literalLunchFat.Text = fat_section.ToString("0.00");


            /************************************************************************************************
             *                              begin afternoon tea section                                     *
             ************************************************************************************************/
            diaryEntries = (from de in cvdc.ShareEntries
                            where de.MealTimeId == 4
                            where de.cCode == ccode
                            orderby de.Created
                            select de);

            carbohydrate_section = 0;
            protein_section = 0;
            fat_section = 0;

            foreach (ShareEntry diaryEntry in diaryEntries)
            {
                if (diaryEntry.FromItemsAdd == true)
                {
                    if (hl == diaryEntry.Id)
                    {
                        literalAfternoonTeaRows.Text += "<tr data-id=\"" + diaryEntry.Id + "\" style=\"background-color: #f8dcc8\"><td colspan=\"2\" style=\"font-family: Arial; font-size: 12px; text-align: left; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + diaryEntry.ItemAdd.Name + "</td>";
                    }
                    else
                    {
                        literalAfternoonTeaRows.Text += "<tr data-id=\"" + diaryEntry.Id + "\"><td colspan=\"2\" style=\"font-family: Arial; font-style:italic; font-size: 12px; text-align: left; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">*" + diaryEntry.ItemAdd.Name + "</td>";
                    }
                    literalAfternoonTeaRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + diaryEntry.Amount.ToString("0.00") + "</td>";
                    literalAfternoonTeaRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: left; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + ServeAddedUnit(diaryEntry, 4) + "</td>";
                    literalAfternoonTeaRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.ItemAdd.Carbohydrate * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount)).ToString("0.00") + "</td>";
                    literalAfternoonTeaRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.ItemAdd.Protein * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount)).ToString("0.00") + "</td>";
                    literalAfternoonTeaRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.ItemAdd.Fat * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount)).ToString("0.00") + "</td>";
                    literalAfternoonTeaRows.Text += "</tr>";

                    carbohydrate += (diaryEntry.ItemAdd.Carbohydrate * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount));
                    protein += (diaryEntry.ItemAdd.Protein * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount));
                    fat += (diaryEntry.ItemAdd.Fat * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount));

                    carbohydrate_section += (diaryEntry.ItemAdd.Carbohydrate * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount));
                    protein_section += (diaryEntry.ItemAdd.Protein * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount));
                    fat_section += (diaryEntry.ItemAdd.Fat * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount));
                }
                else
                {
                    if (hl == diaryEntry.Id)
                    {
                        literalAfternoonTeaRows.Text += "<tr data-id=\"" + diaryEntry.Id + "\" style=\"background-color: #f8dcc8\"><td colspan=\"2\" style=\"font-family: Arial; font-size: 12px; text-align: left; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + diaryEntry.Item.Name + "</td>";
                    }
                    else
                    {
                        literalAfternoonTeaRows.Text += "<tr data-id=\"" + diaryEntry.Id + "\"><td colspan=\"2\" style=\"font-family: Arial;  font-size: 12px; text-align: left; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + diaryEntry.Item.Name + "</td>";
                    }
                    literalAfternoonTeaRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + diaryEntry.Amount.ToString("0.00") + "</td>";
                    literalAfternoonTeaRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: left; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + ServeUnit(diaryEntry, 4) + "</td>";
                    literalAfternoonTeaRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.Item.Carbohydrate * (diaryEntry.Amount / diaryEntry.Item.ServeAmount)).ToString("0.00") + "</td>";
                    literalAfternoonTeaRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.Item.Protein * (diaryEntry.Amount / diaryEntry.Item.ServeAmount)).ToString("0.00") + "</td>";
                    literalAfternoonTeaRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.Item.Fat * (diaryEntry.Amount / diaryEntry.Item.ServeAmount)).ToString("0.00") + "</td>";
                    literalAfternoonTeaRows.Text += "</tr>";

                    carbohydrate += (diaryEntry.Item.Carbohydrate * (diaryEntry.Amount / diaryEntry.Item.ServeAmount));
                    protein += (diaryEntry.Item.Protein * (diaryEntry.Amount / diaryEntry.Item.ServeAmount));
                    fat += (diaryEntry.Item.Fat * (diaryEntry.Amount / diaryEntry.Item.ServeAmount));

                    carbohydrate_section += (diaryEntry.Item.Carbohydrate * (diaryEntry.Amount / diaryEntry.Item.ServeAmount));
                    protein_section += (diaryEntry.Item.Protein * (diaryEntry.Amount / diaryEntry.Item.ServeAmount));
                    fat_section += (diaryEntry.Item.Fat * (diaryEntry.Amount / diaryEntry.Item.ServeAmount));
                }
            }

            literalAfternoonTeaCarb.Text = carbohydrate_section.ToString("0.00");
            literalAfternoonTeaProtein.Text = protein_section.ToString("0.00");
            literalAfternoonTeaFat.Text = fat_section.ToString("0.00");


            /************************************************************************************************
             *                              begin dinner section                                            *
             ************************************************************************************************/
            diaryEntries = (from de in cvdc.ShareEntries
                            where de.MealTimeId == 5
                            where de.cCode == ccode
                            orderby de.Created
                            select de);

            carbohydrate_section = 0;
            protein_section = 0;
            fat_section = 0;
            
            foreach (ShareEntry diaryEntry in diaryEntries)
            {
                if (diaryEntry.FromItemsAdd == true)
                {
                    if (hl == diaryEntry.Id)
                    {
                        literalDinnerRows.Text += "<tr data-id=\"" + diaryEntry.Id + "\" style=\"background-color: #f8dcc8\"><td colspan=\"2\" style=\"font-family: Arial; font-size: 12px; text-align: left; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + diaryEntry.ItemAdd.Name + "</td>";
                    }
                    else
                    {
                        literalDinnerRows.Text += "<tr data-id=\"" + diaryEntry.Id + "\"><td colspan=\"2\" style=\"font-family: Arial; font-style:italic; font-size: 12px; text-align: left; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">*" + diaryEntry.ItemAdd.Name + "</td>";
                    }
                    literalDinnerRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + diaryEntry.Amount.ToString("0.00") + "</td>";
                    literalDinnerRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: left; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + ServeAddedUnit(diaryEntry, 5) + "</td>";
                    literalDinnerRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.ItemAdd.Carbohydrate * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount)).ToString("0.00") + "</td>";
                    literalDinnerRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.ItemAdd.Protein * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount)).ToString("0.00") + "</td>";
                    literalDinnerRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.ItemAdd.Fat * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount)).ToString("0.00") + "</td>";
                    literalDinnerRows.Text += "</tr>";

                    carbohydrate += (diaryEntry.ItemAdd.Carbohydrate * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount));
                    protein += (diaryEntry.ItemAdd.Protein * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount));
                    fat += (diaryEntry.ItemAdd.Fat * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount));

                    carbohydrate_section += (diaryEntry.ItemAdd.Carbohydrate * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount));
                    protein_section += (diaryEntry.ItemAdd.Protein * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount));
                    fat_section += (diaryEntry.ItemAdd.Fat * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount));
                }
                else
                {
                    if (hl == diaryEntry.Id)
                    {
                        literalDinnerRows.Text += "<tr data-id=\"" + diaryEntry.Id + "\" style=\"background-color: #f8dcc8\"><td colspan=\"2\" style=\"font-family: Arial; font-size: 12px; text-align: left; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + diaryEntry.Item.Name + "</td>";
                    }
                    else
                    {
                        literalDinnerRows.Text += "<tr data-id=\"" + diaryEntry.Id + "\"><td colspan=\"2\" style=\"font-family: Arial; font-size: 12px; text-align: left; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + diaryEntry.Item.Name + "</td>";
                    }
                    literalDinnerRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + diaryEntry.Amount.ToString("0.00") +"</td>";
                    literalDinnerRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: left; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + ServeUnit(diaryEntry, 5) + "</td>";
                    literalDinnerRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.Item.Carbohydrate * (diaryEntry.Amount / diaryEntry.Item.ServeAmount)).ToString("0.00") + "</td>";
                    literalDinnerRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.Item.Protein * (diaryEntry.Amount / diaryEntry.Item.ServeAmount)).ToString("0.00") + "</td>";
                    literalDinnerRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.Item.Fat * (diaryEntry.Amount / diaryEntry.Item.ServeAmount)).ToString("0.00") + "</td>";
                    literalDinnerRows.Text += "</tr>";

                    carbohydrate += (diaryEntry.Item.Carbohydrate * (diaryEntry.Amount / diaryEntry.Item.ServeAmount));
                    protein += (diaryEntry.Item.Protein * (diaryEntry.Amount / diaryEntry.Item.ServeAmount));
                    fat += (diaryEntry.Item.Fat * (diaryEntry.Amount / diaryEntry.Item.ServeAmount));

                    carbohydrate_section += (diaryEntry.Item.Carbohydrate * (diaryEntry.Amount / diaryEntry.Item.ServeAmount));
                    protein_section += (diaryEntry.Item.Protein * (diaryEntry.Amount / diaryEntry.Item.ServeAmount));
                    fat_section += (diaryEntry.Item.Fat * (diaryEntry.Amount / diaryEntry.Item.ServeAmount));
                }

            }

            literalDinnerCarb.Text = carbohydrate_section.ToString("0.00");
            literalDinnerProtein.Text = protein_section.ToString("0.00");
            literalDinnerFat.Text = fat_section.ToString("0.00");

            
            /************************************************************************************************
             *                              begin supper section                                            *
             ************************************************************************************************/
            diaryEntries = (from de in cvdc.ShareEntries
                            where de.MealTimeId == 6
                            where de.cCode == ccode
                            orderby de.Created
                            select de);

            carbohydrate_section = 0;
            protein_section = 0;
            fat_section = 0;
            
            foreach (ShareEntry diaryEntry in diaryEntries)
            {
                if (diaryEntry.FromItemsAdd == true)
                {
                    if (hl == diaryEntry.Id)
                    {
                        literalSupperRows.Text += "<tr data-id=\"" + diaryEntry.Id + "\" style=\"background-color: #f8dcc8\"><td colspan=\"2\" style=\"font-family: Arial; font-size: 12px; text-align: left; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + diaryEntry.ItemAdd.Name + "</td>";
                    }
                    else
                    {
                        literalSupperRows.Text += "<tr data-id=\"" + diaryEntry.Id + "\"><td colspan=\"2\" style=\"font-family: Arial; font-style:italic; font-size: 12px; text-align: left; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">*" + diaryEntry.ItemAdd.Name + "</td>";
                    }
                    literalSupperRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + diaryEntry.Amount.ToString("0.00") + "</td>";
                    literalSupperRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: left; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + ServeUnit(diaryEntry, 6) + "</td>";
                    literalSupperRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.ItemAdd.Carbohydrate * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount)).ToString("0.00") + "</td>";
                    literalSupperRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.ItemAdd.Protein * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount)).ToString("0.00") + "</td>";
                    literalSupperRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.ItemAdd.Fat * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount)).ToString("0.00") + "</td>";
                    literalSupperRows.Text += "</tr>";

                    carbohydrate += (diaryEntry.ItemAdd.Carbohydrate * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount));
                    protein += (diaryEntry.ItemAdd.Protein * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount));
                    fat += (diaryEntry.ItemAdd.Fat * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount));

                    carbohydrate_section += (diaryEntry.ItemAdd.Carbohydrate * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount));
                    protein_section += (diaryEntry.ItemAdd.Protein * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount));
                    fat_section += (diaryEntry.ItemAdd.Fat * (diaryEntry.Amount / diaryEntry.ItemAdd.ServeAmount));
                }
                else
                {
                    if (hl == diaryEntry.Id)
                    {
                        literalSupperRows.Text += "<tr data-id=\"" + diaryEntry.Id + "\" style=\"background-color: #f8dcc8\"><td colspan=\"2\" style=\"font-family: Arial; font-size: 12px; text-align: left; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + diaryEntry.Item.Name + "</td>";
                    }
                    else
                    {
                        literalSupperRows.Text += "<tr data-id=\"" + diaryEntry.Id + "\"><td colspan=\"2\" style=\"font-family: Arial; font-size: 12px; text-align: left; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + diaryEntry.Item.Name + "</td>";
                    }
                    literalSupperRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + diaryEntry.Amount.ToString("0.00") + "</td>";
                    literalSupperRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: left; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + ServeUnit(diaryEntry, 6) + "</td>";
                    literalSupperRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.Item.Carbohydrate * (diaryEntry.Amount / diaryEntry.Item.ServeAmount)).ToString("0.00") + "</td>";
                    literalSupperRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.Item.Protein * (diaryEntry.Amount / diaryEntry.Item.ServeAmount)).ToString("0.00") + "</td>";
                    literalSupperRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: " + (isFirst ? "6px 12px" : "0 12px 6px 12px") + ";\">" + (diaryEntry.Item.Fat * (diaryEntry.Amount / diaryEntry.Item.ServeAmount)).ToString("0.00") + "</td>";
                    literalSupperRows.Text += "</tr>";

                    carbohydrate += (diaryEntry.Item.Carbohydrate * (diaryEntry.Amount / diaryEntry.Item.ServeAmount));
                    protein += (diaryEntry.Item.Protein * (diaryEntry.Amount / diaryEntry.Item.ServeAmount));
                    fat += (diaryEntry.Item.Fat * (diaryEntry.Amount / diaryEntry.Item.ServeAmount));

                    carbohydrate_section += (diaryEntry.Item.Carbohydrate * (diaryEntry.Amount / diaryEntry.Item.ServeAmount));
                    protein_section += (diaryEntry.Item.Protein * (diaryEntry.Amount / diaryEntry.Item.ServeAmount));
                    fat_section += (diaryEntry.Item.Fat * (diaryEntry.Amount / diaryEntry.Item.ServeAmount));
                }

            }
            
            literalSupperCarb.Text = carbohydrate_section.ToString("0.00");
            literalSupperProtein.Text = protein_section.ToString("0.00");
            literalSupperFat.Text = fat_section.ToString("0.00");


            literalTotalCarb.Text = carbohydrate.ToString("0.00");
            literalTotalProtein.Text = protein.ToString("0.00");
            literalTotalFat.Text = fat.ToString("0.00");


            cvdc.Dispose();

        }
        
        protected void GenerateSharePlan()
        {
            foodDiaryScript.Text = "<script> $(document).ready(function () { ShareFoodDiaryDay() } );</script>";

        }
    }
}