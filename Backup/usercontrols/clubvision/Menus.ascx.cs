using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Configuration;
using System.Web.UI.WebControls;
using BrightcoveAPI;
using Image = System.Drawing.Image;

/*
 * [1] Dewi 22/01/2014 > Add the ability for user to add their own photo and recipe to their meal banks
 */
namespace VisionPersonalTrainingProject.usercontrols.clubvision
{
    public partial class Menus : System.Web.UI.UserControl
    {
        private BCVideo BrightCoveRequest(string id)
        {
            string Token = ConfigurationManager.AppSettings["brightcoveToken"].ToString();
            string reqUrl = "http://api.brightcove.com/services/library?command=find_video_by_id&video_id=" + id + "&token=" + Token;
            WebRequest webRequest = WebRequest.Create(reqUrl) as HttpWebRequest;
            HttpWebResponse response = webRequest.GetResponse() as HttpWebResponse;
            DataContractJsonSerializer JsonReader = new DataContractJsonSerializer(typeof(BCVideo));
            return (BCVideo)JsonReader.ReadObject(response.GetResponseStream());

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                ClubVisionDataContext cvdc = new ClubVisionDataContext();
                decimal c_carb = 0, c_ptn = 0, c_fat = 0;

                switch ((string)Session["MemberType"])
                {
                    case "VPT":
                        {
                            var _customers = (from customers in cvdc.Customers
                                              where customers.Id == (int)Session["MemberNo"]
                                              select customers).SingleOrDefault();
                            c_carb = _customers.Carb;
                            c_ptn = _customers.Protein;
                            c_fat = _customers.Fat;
                        } break;
                    case "VVT":
                        {
                            var _customers = (from customers in cvdc.Customer_Externals
                                              where customers.iID == (int)Session["MemberNo"]
                                              select customers).SingleOrDefault();
                            if (_customers != null)
                            {
                                var custMacros =
                                    _customers.Goals.OrderByDescending(x => x.dDateCreated).FirstOrDefault();
                                if (custMacros != null)
                                {
                                    c_carb = (decimal)custMacros.CHO;
                                    c_ptn = (decimal)custMacros.PTN;
                                    c_fat = (decimal)custMacros.FAT;
                                }
                            }
                        } break;
                }

                literalMacros.Text = "Carbohydrate: <span id=\"wtot-cho\">" + c_carb + "</span> Protein: <span id=\"wtot-ptn\">" + c_ptn +
                                         "</span> Fat: <span id=\"wtot-fat\">" + c_fat + "</span>";

                if (Request.QueryString["tab"] == null || Request.QueryString["tab"] == "mymenus" || Request.QueryString["tab"] == "visionsmenus")
                {
                    if (Request.QueryString["tab"] == null || Request.QueryString["tab"] == "mymenus")
                    {
                        menuTab.Style["background-image"] = "url(/images/eHdrMenuTabMyDailyPlans.gif)";
                        pMyDailyPlans.Visible = true;
                        pMyDailyPlansSearch.Visible = true;
                    }
                    else if (Request.QueryString["tab"] == "visionsmenus")
                    {
                        menuTab.Style["background-image"] = "url(/images/eHdrMenuTabVisionsDailyPlans.gif)";
                        pVisionDailyPlans.Visible = true;
                        pVisionDailyPlansSearch.Visible = true;
                    }

                    tabMenus.Visible = true;
                    tabEdit.Visible = false;
                    tabEdit_Meal.Visible = false;

                    List<GetMenusByMacro_MenuPageResult> menus = new List<GetMenusByMacro_MenuPageResult>();

                    decimal m_carb = Request.QueryString["carb"] == null
                                                 ? c_carb
                                                 : Convert.ToDecimal(Request.QueryString["carb"]);

                    decimal m_protein = Request.QueryString["protein"] == null
                                         ? c_ptn
                                         : Convert.ToDecimal(Request.QueryString["protein"]);

                    decimal m_fat = Request.QueryString["fat"] == null
                                         ? c_fat
                                         : Convert.ToDecimal(Request.QueryString["fat"]);

                    string m_keyword = Request.QueryString["keyword"] ?? "";

                    if (Request.QueryString["tab"] == "visionsmenus")
                    {
                        if (Request.QueryString["showall"] == "true" && Request.QueryString["carb"] == null)
                        {
                            menus = cvdc.GetMenusByMacro_MenuPage(-1, -1, -1, -1, 0, "").Take(4).ToList();
                        }
                        else
                        {   
                            int range = Request.QueryString["keyword"] == null ? 10 : 100;

                            int limit = Request.QueryString["keyword"] == null ? 7 : 0;

                            menus = cvdc.GetMenusByMacro_MenuPage(m_carb, m_protein, m_fat, -1, range, m_keyword).Take(4).ToList();
                            /*
                            do
                            {
                                menus = cvdc.GetMenusByMacro_MenuPage(m_carb, m_protein, m_fat,-1, range, m_keyword).Take(8).ToList();
                                range = range + 10;
                            } while (menus.Count() < limit); //its not valid anymore then
                            */
                            literalMacros.Text += "<span id=\"rangenumber\" style=\"color: #FFFFFF;\">" + (range) + "</span>";
                        }

                    }else //tab my menus
                    {
                        
                        if (Request.QueryString["fat"] != null && Request.QueryString["keyword"] != null)
                        {
                            menus = cvdc.GetMenusByMacro_MenuPage(m_carb, m_protein, m_fat, (int)Session["MemberNo"], 0, m_keyword).Take(4).ToList();
                        }
                        else if(Request.QueryString["fat"] == null && Request.QueryString["keyword"] != null)
                        {
                            menus = cvdc.GetMenusByMacro_MenuPage(-1, -1, -1, (int)Session["MemberNo"], 0, m_keyword).Take(4).ToList();
                        }
                        else
                        {
                            menus = cvdc.GetMenusByMacro_MenuPage(-1, -1, -1, (int)Session["MemberNo"], 0, "").Take(4).ToList();
                        }
                    }

                    int count = 0;
                    int result;

                    int left = 0;

                    foreach (GetMenusByMacro_MenuPageResult menu in menus)
                    {
                        count++;

                        decimal carbohydrate = 0;
                        decimal protein = 0;
                        decimal fat = 0;

                        carbohydrate = Convert.ToDecimal(menu.CarbohydrateSum);
                        protein = Convert.ToDecimal(menu.ProteinSum);
                        fat = Convert.ToDecimal(menu.FatSum);

                        //if (Request.QueryString["tab"] == "visionsmenus")
                        //{
                        //    if ((string)Session["Admin"] != "Yes" && (_customers.First().Carb <= carbohydrate - 10 || _customers.First().Carb >= carbohydrate + 10 || _customers.First().Protein <= protein - 10 || _customers.First().Protein >= protein + 10 || _customers.First().Fat <= fat - 10 || _customers.First().Fat >= fat + 10))
                        //    {
                        //        count--;
                        //        continue;
                        //    }
                        //}

                        if (count == 1)
                        {
                            literalMenus.Text += "<div id=\"menu_frame\"><div id=\"menu_scroller\">";
                        }

                        Math.DivRem(count, 2, out result);
                        if (result == 1)
                        {
                            literalMenus.Text += "<div class=\"menu\" style=\"position: absolute;top: 0px; left: " + left + "px;\">";
                        }
                        else
                        {
                            literalMenus.Text += "<div class=\"menu\" style=\"position: absolute;top: 296px; left: " + left + "px;\">";
                        }
                        literalMenus.Text += "<p class=\"title\">" + menu.Name + "</p>";
                        literalMenus.Text += "<a class=\"viewdetails\" href=\"?tab=" + (Request.QueryString["tab"] == "visionsmenus" ? ((string)Session["Admin"] == "Yes" ? "edit" : "view") : "edit") + "&menuId=" + menu.Id + "\">View full plan ></a>";

                        if (menu.ImageUrl != null)
                        {
                            literalMenus.Text += "<a href=\"?tab=" + (Request.QueryString["tab"] == "visionsmenus" ? ((string)Session["Admin"] == "Yes" ? "edit" : "view") : "edit") + "&menuId=" + menu.Id + "\"><img class=\"menu_image\" src=\"/images/menus/" + menu.ImageUrl + "\" style=\"position:relative; top: 15px;left:1px\"/></a>";
                        }
                        literalMenus.Text += "<ul>";
                        
                        literalMenus.Text += menu.MenuItems;
                        literalMenus.Text += menu.TripleDot;
                        
                        literalMenus.Text += "</ul>";
                        literalMenus.Text += "<p class=\"macro\">";
                        literalMenus.Text += "Carb(g): <span class=\"macro-carb\">" + carbohydrate.ToString("0.00") + "</span><br />";
                        literalMenus.Text += "Protein(g): <span class=\"macro-ptn\">" + protein.ToString("0.00") + "</span><br />";
                        literalMenus.Text += "Fat(g): <span class=\"macro-fat\">" + fat.ToString("0.00") + "</span>";
                        literalMenus.Text += "</p>";
                        literalMenus.Text += "<div class=\"controls\">";
                        literalMenus.Text += "<img src=\"/images/buttonAddToDiary.gif\" style=\"cursor: pointer\" onclick=\"var result = toggle_str('menu_" + menu.Id + "');hide_one_of();switchto('menu_" + menu.Id + "', result);\" />";
                        if(Session["MemberType"].Equals("VPT"))
                        {
                            literalMenus.Text += "<img src=\"/images/iconShare1.png\" alt=\"Share Menu\" title=\"Share Menu\" style=\"cursor: pointer\" onclick=\"var result = toggle_str('share_menu_" + menu.Id + "');hide_one_of();switchto('share_menu_" + menu.Id + "', result);\" />";
                        }
                        else
                        {
                            literalMenus.Text += "<img src=\"/images/facebook-icon-round.png\" alt=\"Share Menu\" title=\"Share Menu\" style=\"cursor: pointer\" onclick=\"ShareMenuOrMeal(2, " + menu.Id + ", -1, 'facebook');\" />";
                        }
                        
                        //literalMenus.Text += "<a onclick=\"popup_and_printURL('?tab=edit&menuId=" + menu.Id + "');\"><img src=\"/images/iconPrint.png\" alt=\"Print\" /></a>";
                        if (Request.QueryString["tab"] == null || Request.QueryString["tab"] == "mymenus")
                        {
                            literalMenus.Text += "<a href=\"?tab=edit&menuId=" + menu.Id + "\"><img src=\"/images/iconEdit.png\" alt=\"Edit\" title=\"Edit\"/></a>";
                            literalMenus.Text += "<img src=\"/images/iconCancel.png\" alt=\"Delete\" title=\"Delete\" style=\"cursor: pointer\" onclick=\"delete_menu(" + menu.Id + ");\" />";
                            
                        }
                        literalMenus.Text += "<div id=\"menu_" + menu.Id + "\" class=\"one_of\" style=\"z-index: 100; left: 11px; top: -93px; border: 1px solid #a0a0a0; display: none; position: relative; width: 198px; height: 60px; background-color: #f1f1f1\" >";
                        literalMenus.Text += "<div class=\"quicktools_copyto_header\">Copy to which date?</div>";
                        literalMenus.Text += CopyToDates(menu.Id);
                        literalMenus.Text += "</div>";

                        literalMenus.Text += "<div id=\"share_menu_" + menu.Id + "\" class=\"one_of\" style=\"z-index: 100; left: 94px; top: -124px; border: 1px solid #a0a0a0; display: none; position: relative; width: 120px; height: 90px; background-color: #f1f1f1\" >";
                        literalMenus.Text += "<div class=\"quicktools_copyto_header\">Share to?</div>"; //ShareMenuOrMeal(shareType, menuid, mealid, sharevia)
                        literalMenus.Text += "<div class=\"quicktools_option\" style=\"height:25px;text-align:left;vertical-align:middle;\" onclick=\"ShareMenuOrMeal(2, "+ menu.Id +", -1, 'facebook')\"><img src=\"/images/fb.small.icon.jpg\" /> &nbsp;&nbsp;&nbsp;Facebook</div>";
                        if (Session["Trainer"].Equals("No"))
                        {
                            literalMenus.Text += "<div class=\"quicktools_option\" style=\"height:25px;text-align:left;vertical-align:middle;\" onclick=\"ShareMenuOrMeal(2, " + menu.Id + ", -1, 'trainer')\"><img src=\"/images/trainer.small.icon.jpg\" />&nbsp;&nbsp;&nbsp;My Trainer</div>";
                        }
                        else
                        {
                            literalMenus.Text += "<div class=\"quicktools_option\" style=\"height:25px;text-align:left;vertical-align:middle;\" onclick=\"ShareMenuOrMeal(2, " + menu.Id + ", -1, 'clients')\"><img src=\"/images/trainer.small.icon.jpg\" />&nbsp;&nbsp;&nbsp;My Clients</div>";
                        }
                        

                        literalMenus.Text += "</div>";

                        literalMenus.Text += "</div>";
                        literalMenus.Text += "</div>";
                        Math.DivRem(count, 2, out result);
                        if (result == 0)
                        {
                            left = left + 440;
                        }
                    }

                    Math.DivRem(count, 2, out result);
                    literalMenus.Text += "</div></div>";

                    int output = 0;

                    Math.DivRem(left, 880, out output);
                    if (output == 0)
                    {
                        left = left + 880;
                    }

                    literalMenus.Text += "<script  type=\"text/javascript\">var scroller_width = " + left + ";</script>";
                }


                if (Request.QueryString["tab"] == "visionsmeals" || Request.QueryString["tab"] == "mymeals")
                {
                    if (Request.QueryString["tab"] == "mymeals")
                    {
                        menuTab.Style["background-image"] = "url(/images/eHdrMenuTabMyMeals.gif)";
                        pMyMeals.Visible = true;
                        pMyMealsSearch.Visible = true;
                    }
                    if (Request.QueryString["tab"] == "visionsmeals")
                    {
                        menuTab.Style["background-image"] = "url(/images/eHdrMenuTabVisionsMeals.gif)";
                        pVisionMeals.Visible = true;
                        pVisionMealsSearch.Visible = true;
                    }
                    tabMenus.Visible = true;
                    tabEdit.Visible = false;
                    tabEdit_Meal.Visible = false;
                    
                    var _meals = (Request.QueryString["tab"] == "mymeals" ? cvdc.GetMealsForCustomer((int)Session["MemberNo"]).Take(8) : cvdc.GetMealsForCustomer(-1).Take(8));

                    int count = 0;
                    int result;

                    int left = 0;

                    foreach (GetMealsForCustomerResult meal in _meals)
                    {
                        count++;

                        decimal carbohydrate = 0;
                        decimal protein = 0;
                        decimal fat = 0;

                        if (count == 1)
                        {
                            literalMenus.Text += "<div id=\"menu_frame\"><div id=\"menu_scroller\">";
                        }
                        Math.DivRem(count, 2, out result);
                        if (result == 1)
                        {
                            literalMenus.Text += "<div class=\"menu\" style=\"position: absolute;top: 0px; left: " + left + "px;\">";
                        }
                        else
                        {
                            literalMenus.Text += "<div class=\"menu\" style=\"position: absolute;top: 296px; left: " + left + "px;\">";
                        }
                        literalMenus.Text += "<p class=\"title\">" + meal.Name + "</p>";
                        literalMenus.Text += "<a class=\"viewdetails\" href=\"?tab=" + (Request.QueryString["tab"] == "visionsmeals" ? ((string)Session["Admin"] == "Yes" ? "edit" : "view") : "edit") + "_meal&mealId=" + meal.Id + "\">View full meal ></a>";
                        
                        if (meal.VideoId != null)
                        {
                            literalMenus.Text += "<a href=\"?tab=" + (Request.QueryString["tab"] == "visionsmeals" ? ((string)Session["Admin"] == "Yes" ? "edit" : "view") : "edit") + "_meal&mealId=" + meal.Id + "\"><div style=\"position:relative; top: 15px;left:1px;height: 144px; width: 144px;overflow: hidden\"><img class=\"menu_image thumb\" src=\"/images/meals/" + meal.ImageUrl + "\" style=\"position:absolute; height: 144px; width: 192px;left: -24px;\"/></div></a>";
                        }
                        else
                        {
                            if (meal.ImageUrl != null)
                            {
                                literalMenus.Text += "<a href=\"?tab=" + (Request.QueryString["tab"] == "visionsmeals" ? ((string)Session["Admin"] == "Yes" ? "edit" : "view") : "edit") + "_meal&mealId=" + meal.Id + "\"><div style=\"position:relative; top: 15px;left:1px;height: 144px; width: 144px;overflow: hidden\"><img class=\"menu_image\" src=\"/images/meals/" + meal.ImageUrl + "\" style=\"position:absolute;height: 144px; width: 144px;\"/></div></a>";
                            }
                            else
                            {
                                literalMenus.Text += "<a href=\"?tab=" + (Request.QueryString["tab"] == "visionsmeals" ? ((string)Session["Admin"] == "Yes" ? "edit" : "view") : "edit") + "_meal&mealId=" + meal.Id + "\"><div style=\"position:relative; top: 15px;left:1px;height: 144px; width: 144px;overflow: hidden\"><img class=\"menu_image\" src=\"/images/menus/meal_generic.jpg\" style=\"position:absolute;height: 144px; width: 144px;\"/></div></a>";
                            }
                        }
                         
                        literalMenus.Text += "<ul>";
                        int item_count = (int) meal.ItemCount;

                        literalMenus.Text += meal.MealItems;
                        literalMenus.Text += meal.TripleDot;
                        literalMenus.Text += "</ul>";

                        carbohydrate = Convert.ToInt32(meal.CarbohydrateSum);
                        protein = Convert.ToInt32(meal.ProteinSum);
                        fat = Convert.ToInt32(meal.FatSum);

                        literalMenus.Text += "<p class=\"macro\">";
                        literalMenus.Text += "Carb(g): <span class=\"macro-carb\">" + carbohydrate.ToString("0.00") + "</span><br />";
                        literalMenus.Text += "Protein(g): <span class=\"macro-ptn\">" + protein.ToString("0.00") + "</span><br />";
                        literalMenus.Text += "Fat(g): <span class=\"macro-fat\">" + fat.ToString("0.00") + "</span>";
                        literalMenus.Text += "</p>";
                        literalMenus.Text += "<div class=\"controls\">";
                        literalMenus.Text += "<img src=\"/images/buttonAddToDiary.gif\" style=\"cursor: pointer\" onclick=\"var result = toggle_str('meal_" + meal.Id + "');hide_one_of();switchto('meal_" + meal.Id + "', result);\" />";
                        literalMenus.Text += "<img src=\"/images/iconShare1.png\" alt=\"Share Menu\" title=\"Share Menu\" style=\"cursor: pointer\" onclick=\"var result = toggle_str('share_meal_" + meal.Id + "');hide_one_of();switchto('share_meal_" + meal.Id + "', result);\" />";
                        //the one below previously used for external
                        //literalMenus.Text += "<img src=\"/images/facebook-icon-round.png\" alt=\"Share Menu\" title=\"Share Menu\" style=\"cursor: pointer\" onclick=\"ShareMenuOrMeal(3,-1," + meal.Id + ",'facebook');\" />";
                        
                        //literalMenus.Text += "<img src=\"/images/iconPrint.png\" alt=\"Print\" />";
                        if (Request.QueryString["tab"] == "mymeals")
                        {
                            literalMenus.Text += "<a href=\"?tab=edit_meal&mealId=" + meal.Id + "\"><img src=\"/images/iconEdit.png\" alt=\"Edit\" title=\"Edit\" /></a>";
                            literalMenus.Text += "<img src=\"/images/iconCancel.png\" alt=\"Delete\" title=\"Delete\" style=\"cursor: pointer\" onclick=\"delete_meal(" + meal.Id + ");\" />";
                           
                        }
                        if (meal.VideoId != null)
                        {
                            literalMenus.Text += "<a href=\"/club-vision/education/vision-tv?vid=" + meal.VideoId + "\"><img src=\"/images/iconPlay.png\" alt=\"Play\" title=\"Play\" /></a>";
                            if(meal.PDFAddress != null)
                            {
                                literalMenus.Text += "<a href=\"/media" + meal.PDFAddress + "\" target=\"_blank\"><img src=\"/images/iconDownload.png\" alt=\"Download\" title=\"Download\" /></a>";    
                            }
                        }
                        if (meal.PDFAddress != null && meal.VideoId == null)
                        {
                            literalMenus.Text += "<a href=\"/club-vision/recipe/" + meal.PDFAddress + "\" target=\"_blank\"><img src=\"/images/iconDownload.png\" alt=\"Download\" title=\"Download\" /></a>";
                        }
                        literalMenus.Text += "<div id=\"meal_" + meal.Id + "\" class=\"one_of\" style=\"z-index: 100; left: 11px; top: -185px; border: 1px solid #a0a0a0; display: none; position: relative; width: 198px; height: 150px; background-color: #f1f1f1\" >";
                        literalMenus.Text += "<div class=\"quicktools_copyto_header\">Copy to which meal?</div>";
                        literalMenus.Text += "<div class=\"quicktools_option\" style=\"height:30px;padding-left:40px;\"><select  id=\"meal_" + meal.Id + "_time\"><option value=\"1\">Breakfast</option><option value=\"2\">Morning Tea</option><option value=\"3\">Lunch</option><option value=\"4\">Afternoon Tea</option><option value=\"5\">Dinner</option><option value=\"6\">Supper</option></select></div>";
                        literalMenus.Text += "<div class=\"quicktools_copyto_header\">Copy to which date?</div>";
                        literalMenus.Text += CopyToDates_Meal(meal.Id);
                        literalMenus.Text += "</div>";

                        int sharemealheight = Request.QueryString["tab"] == "mymeals" ? 154 : 121;

                        if (Session["MemberType"].Equals("VVT"))
                        {
                            sharemealheight = sharemealheight - 33;
                        }

                        literalMenus.Text += "<div id=\"share_meal_" + meal.Id + "\" class=\"one_of\" style=\"z-index: 100; left: 94px; top: -124px; border: 1px solid #a0a0a0; display: none; position: relative; width: 137px; height: " + sharemealheight + "px; background-color: #f1f1f1\" >";
                        literalMenus.Text += "<div class=\"quicktools_copyto_header\">Share to?</div>"; //ShareMenuOrMeal(shareType, menuid, mealid, sharevia)
                        literalMenus.Text += "<div class=\"quicktools_option\" style=\"height:25px;text-align:left;vertical-align:middle;\" onclick=\"hide_one_of();ShareMenuOrMeal(3,-1," + meal.Id + ",'facebook'); return  false;\"><img src=\"/images/fb.small.icon.jpg\" /> &nbsp;&nbsp;&nbsp;Facebook</div>";
                        
                        if (Session["Trainer"].Equals("No"))
                        {
                            if(Session["MemberType"].Equals("VPT"))
                                literalMenus.Text += "<div class=\"quicktools_option\" style=\"height:25px;text-align:left;vertical-align:middle;\" onclick=\"hide_one_of();ShareMenuOrMeal(3,-1," + meal.Id + ",'trainer'); return  false;\"><img src=\"/images/trainer.small.icon.jpg\" />&nbsp;&nbsp;&nbsp;My Trainer</div>";
                        }
                        else
                        {
                            literalMenus.Text += "<div class=\"quicktools_option\" style=\"height:25px;text-align:left;vertical-align:middle;\" onclick=\"hide_one_of();ShareMenuOrMeal(3,-1," + meal.Id + ",'clients'); return  false;\"><img src=\"/images/trainer.small.icon.jpg\" />&nbsp;&nbsp;&nbsp;My Clients</div>";
                        }

                        if (Request.QueryString["tab"] == "mymeals")
                        {
                            literalMenus.Text += "<div class=\"quicktools_option\" style=\"height:25px;text-align:left;vertical-align:middle;\" onclick=\"hide_one_of();ShareMenuOrMeal(3,-1," + meal.Id + ",'VVTHQ'); return  false;\"><img src=\"/images/favicon002.png\" Height=\"21\" Width=\"21\"/>&nbsp;&nbsp;&nbsp;Vision Meals</div>"; 
                        }
                        literalMenus.Text += "<div class=\"quicktools_option\" style=\"height:25px;text-align:left;vertical-align:middle;\" onclick=\"hide_one_of();enterEmailToShare('ShareMenuOrMeal(3,-1," + meal.Id + ",email)'); return false;\"><img src=\"/images/email_icon.png\" />&nbsp;&nbsp;&nbsp;Email</div>";
                        literalMenus.Text += "</div>";

                        literalMenus.Text += "</div>";
                        literalMenus.Text += "</div>";
                        Math.DivRem(count, 2, out result);
                        if (result == 0)
                        {
                            left = left + 440;
                        }
                    }
                    Math.DivRem(count, 2, out result);
                    literalMenus.Text += "</div></div>";

                    int output = 0;

                    Math.DivRem(left, 880, out output);
                    if (output == 0)
                    {
                        left = left + 880;
                    }

                    literalMenus.Text += "<script type=\"text/javascript\">var scroller_width = " + left + ";var ori_dataskip; var ori_scroller_width; var ori_menuscrollerleft; var ori_menuwindows;</script>";

                }

                if (Request.QueryString["tab"] == "edit") // edit menu
                {
                    menuTab.Style["background-image"] = "url(/images/eHdrMenuTabMyDailyPlans.gif)";
                    pMyDailyPlans.Visible = true;
                    pMyDailyPlansSearch.Visible = true;

                    tabMenus.Visible = false;
                    tabEdit.Visible = true;
                    tabEdit_Meal.Visible = false;

                    currentMenuId.Value = Request.QueryString["menuId"];
                    UpdateEditMenu(int.Parse(currentMenuId.Value));

                    literalSearchByCategory_Breakfast.Text = CategorySearch(1, "searchByCategory_Breakfast_Result");
                    literalSearchByCategory_MorningTea.Text = CategorySearch(2, "searchByCategory_MorningTea_Result");
                    literalSearchByCategory_Lunch.Text = CategorySearch(3, "searchByCategory_Lunch_Result");
                    literalSearchByCategory_AfternoonTea.Text = CategorySearch(4, "searchByCategory_AfternoonTea_Result");
                    literalSearchByCategory_Dinner.Text = CategorySearch(5, "searchByCategory_Dinner_Result");
                    literalSearchByCategory_Supper.Text = CategorySearch(6, "searchByCategory_Supper_Result");

                }
                if (Request.QueryString["tab"] == "view")
                {
                    menuTab.Style["background-image"] = "url(/images/eHdrMenuTabVisionsDailyPlans.gif)";
                    pMyDailyPlans.Visible = true;

                    tabMenus.Visible = false;
                    tabEdit.Visible = true;
                    tabEdit_Meal.Visible = false;

                    currentMenuId.Value = Request.QueryString["menuId"];
                    UpdateViewMenu(Convert.ToInt32(currentMenuId.Value));

                }
                if (Request.QueryString["tab"] == "edit_meal")
                {
                    menuTab.Style["background-image"] = "url(/images/eHdrMenuTabMyMeals.gif)";
                    pMyMeals.Visible = true;

                    tabMenus.Visible = false;
                    tabEdit.Visible = false;
                    tabEdit_Meal.Visible = true;

                    currentMealId.Value = Request.QueryString["mealId"];
                    UpdateEditMeal(int.Parse(currentMealId.Value));

                    literalSearchByCategory_Meal.Text = CategorySearch_Meal("searchByCategory_Meal_Result");

                }
                if (Request.QueryString["tab"] == "view_meal")
                {
                    menuTab.Style["background-image"] = "url(/images/eHdrMenuTabVisionsMeals.gif)";
                    pMyDailyPlans.Visible = true;

                    tabMenus.Visible = false;
                    tabEdit.Visible = false;
                    tabEdit_Meal.Visible = true;

                    currentMealId.Value = Request.QueryString["mealId"];
                    UpdateViewMeal(int.Parse(currentMealId.Value));
                }
                if (Request.QueryString["tab"] == "new")
                {
                    menuTab.Style["background-image"] = "url(/images/eHdrMenuTabMyDailyPlans.gif)";
                    pMyDailyPlans.Visible = true;

                    tabMenus.Visible = false;
                    tabEdit.Visible = true;
                    tabEdit_Meal.Visible = false;

                    //ClubVisionDataContext cvdc = new ClubVisionDataContext();

                    Menu _menu = new Menu();
                    _menu.Name = "New Menu";
                    _menu.CustomerId = (int)Session["MemberNo"];
                    _menu.Created = System.DateTime.Now;
                    _menu.Updated = _menu.Created;
                    _menu.ImageUrl = "menu_generic.jpg";
                    _menu.IsRecommended = false;
                    cvdc.Menus.InsertOnSubmit(_menu);
                    cvdc.SubmitChanges();


                    currentMenuId.Value = _menu.Id.ToString();
                    UpdateEditMenu(int.Parse(currentMenuId.Value));

                    literalSearchByCategory_Breakfast.Text = CategorySearch(1, "searchByCategory_Breakfast_Result");
                    literalSearchByCategory_MorningTea.Text = CategorySearch(2, "searchByCategory_MorningTea_Result");
                    literalSearchByCategory_Lunch.Text = CategorySearch(3, "searchByCategory_Lunch_Result");
                    literalSearchByCategory_AfternoonTea.Text = CategorySearch(4, "searchByCategory_AfternoonTea_Result");
                    literalSearchByCategory_Dinner.Text = CategorySearch(5, "searchByCategory_Dinner_Result");
                    literalSearchByCategory_Supper.Text = CategorySearch(6, "searchByCategory_Supper_Result");
                }

                if (Request.QueryString["tab"] == "new_meal")
                {
                    menuTab.Style["background-image"] = "url(/images/eHdrMenuTabMyMeals.gif)";
                    pMyMeals.Visible = true;

                    tabMenus.Visible = false;
                    tabEdit.Visible = false;
                    tabEdit_Meal.Visible = true;

                    //ClubVisionDataContext cvdc = new ClubVisionDataContext();

                    Meal _meal = new Meal();
                    _meal.Name = "New Meal";
                    _meal.CustomerId = (int)Session["MemberNo"];
                    _meal.Created = System.DateTime.Now;
                    _meal.Updated = _meal.Created;
                    _meal.ImageUrl = "meal_generic.jpg";
                    _meal.IsRecommended = false;
                    cvdc.Meals.InsertOnSubmit(_meal);
                    cvdc.SubmitChanges();

                    currentMealId.Value = _meal.Id.ToString();
                    UpdateEditMeal(int.Parse(currentMealId.Value));

                    literalSearchByCategory_Meal.Text = CategorySearch_Meal("searchByCategory_Meal_Result");
                }

                if (Request.QueryString["tab"] == "copy")
                {
                    //ClubVisionDataContext cvdc = new ClubVisionDataContext();

                    var _menus = (from menus in cvdc.Menus
                                  where menus.Id == int.Parse(Request.QueryString["menuId"])
                                  select menus);



                    Menu _menu = new Menu();
                    _menu.Name = _menus.First().Name;
                    _menu.CustomerId = (int)Session["MemberNo"];
                    _menu.Created = _menus.First().Created;
                    _menu.Updated = System.DateTime.Now;
                    _menu.ImageUrl = _menus.First().ImageUrl;
                    _menu.IsRecommended = false;
                    cvdc.Menus.InsertOnSubmit(_menu);
                    cvdc.SubmitChanges();

                    var _menuItems = (from menuItems in cvdc.MenuItems
                                      where menuItems.MenuId == int.Parse(Request.QueryString["menuId"])
                                      select menuItems);

                    foreach (MenuItem menuItem in _menuItems)
                    {
                        MenuItem _menuItem = new MenuItem();
                        _menuItem.ItemId = menuItem.ItemId;
                        _menuItem.MenuId = _menu.Id;
                        _menuItem.Amount = menuItem.Amount;
                        _menuItem.MealTimeId = menuItem.MealTimeId;
                        _menuItem.FromItemsAdd = menuItem.FromItemsAdd;
                        _menuItem.FromMealId = menuItem.FromMealId;
                        cvdc.MenuItems.InsertOnSubmit(_menuItem);
                    }
                    cvdc.SubmitChanges();

                    Response.Redirect("/club-vision/my-eating/menus/?tab=edit&menuId=" + _menu.Id, false);
                }

                if (Request.QueryString["tab"] == "copy_meal")
                {
                    //ClubVisionDataContext cvdc = new ClubVisionDataContext();

                    var _meals = (from meals in cvdc.Meals
                                  where meals.Id == int.Parse(Request.QueryString["mealId"])
                                  select meals);



                    Meal _meal = new Meal();
                    _meal.Name = _meals.First().Name;
                    _meal.CustomerId = (int)Session["MemberNo"];
                    _meal.Created = _meals.First().Created;
                    _meal.Updated = System.DateTime.Now;
                    _meal.ImageUrl = _meals.First().ImageUrl;
                    _meal.VideoId = _meals.First().VideoId;
                    _meal.IsRecommended = false;
                    _meal.PDFAddress = _meals.First().PDFAddress;
                    cvdc.Meals.InsertOnSubmit(_meal);
                    cvdc.SubmitChanges();

                    var _mealItems = (from mealItems in cvdc.MealItems
                                      where mealItems.MealId == int.Parse(Request.QueryString["mealId"])
                                      select mealItems);

                    foreach (MealItem mealItem in _mealItems)
                    {
                        MealItem _mealItem = new MealItem();
                        _mealItem.ItemId = mealItem.ItemId;
                        _mealItem.MealId = _meal.Id;
                        _mealItem.Amount = mealItem.Amount;
                        _mealItem.FromItemsAdd = mealItem.FromItemsAdd;
                        cvdc.MealItems.InsertOnSubmit(_mealItem);
                    }
                    cvdc.SubmitChanges();

                    Response.Redirect("/club-vision/my-eating/menus/?tab=edit_meal&mealId=" + _meal.Id, false);

                }
            }
        }

        private string CategorySearch(int mealTimeId, string resultElement)
        {
            ClubVisionDataContext cvdc = new ClubVisionDataContext();

            var categories = (from category in cvdc.Categories
                              orderby category.Name
                              select category);

            string returnValue = "<div style=\"margin-top: 10px; height: 124px; overflow: auto\">";

            foreach (Category category in categories)
            {
                returnValue += "<a style=\"font-weight: bold; display: inline-block;width: 190px; padding-bottom: 10px\" onclick=\"show('" + resultElement + "');menu_getItemsByCategory(" + category.Id + ",'" + resultElement + "', " + mealTimeId + ");\">" + category.Name + "</a>";
            }

            returnValue += "</div>";

            return returnValue;
        }

        private string CategorySearch_Meal(string resultElement)
        {
            ClubVisionDataContext cvdc = new ClubVisionDataContext();

            var categories = (from category in cvdc.Categories
                              orderby category.Name
                              select category);

            string returnValue = "<div style=\"margin-top: 10px; height: 124px; overflow: auto\">";

            foreach (Category category in categories)
            {
                returnValue += "<a style=\"font-weight: bold; display: inline-block;width: 190px; padding-bottom: 10px\" onclick=\"show('" + resultElement + "');meal_getItemsByCategory(" + category.Id + ",'" + resultElement + "');\">" + category.Name + "</a>";
            }

            returnValue += "</div>";

            return returnValue;
        }

        private string GetMenuName(int menuId)
        {
            string returnValue = "";

            ClubVisionDataContext cvdc = new ClubVisionDataContext();

            var _menus = (from menus in cvdc.Menus
                          where menus.Id == menuId
                          select menus);

            foreach (Menu menu in _menus)
            {
                returnValue = menu.Name;

                //prevent unauthorized access
                if (Request.QueryString["tab"].Equals("edit") && menu.CustomerId != (int)Session["MemberNo"] && (string)Session["Admin"] == "No")
                {
                    Response.Redirect("/club-vision/my-eating/menus/?tab=view&menuId=" + menu.Id);
                }
            }
            return returnValue;
        }

        private void UpdateViewMenu(int menuId)
        {
            if ((string)Session["Admin"] != "Yes")
            {
                saveMenuImage.Src = "/images/buttonCopyToMyMenus.png";
                saveMenu.HRef = "/club-vision/my-eating/menus/?tab=copy&menuId=" + menuId;
                saveMenuImage.Alt = "Copy to My Menus";
            }

            menuName.Value = GetMenuName(menuId);
            menuName.Disabled = true;

            menuName.Attributes["onblur"] = "";

            decimal carbohydrate = 0;
            decimal protein = 0;
            decimal fat = 0;

            decimal carbohydrate_section = 0;
            decimal protein_section = 0;
            decimal fat_section = 0;
            int MealNumber = 0;

            ClubVisionDataContext cvdc = new ClubVisionDataContext();
            List<string> litname = new List<string>() {"Breakfast", "MorningTea", "Lunch", "AfternoonTea", "Dinner", "Supper"};
            bool isFirst = true;

            for(int i = 1; i <= 6; i++)
            {
                int i1 = i;
                int j = i - 1;

                var menuItems = (from mi in cvdc.MenuItems
                                 where mi.MealTimeId == i1
                                 where mi.MenuId == menuId
                                 select mi);

                carbohydrate_section = 0;
                protein_section = 0;
                fat_section = 0;
                MealNumber = 0;

                foreach (MenuItem menuItem in menuItems)
                {
                    if (menuItem.FromMealId != null && MealNumber != menuItem.FromMealId)
                    {
                        var themeal = (from mlname in cvdc.Meals
                                       where mlname.Id == menuItem.FromMealId
                                       select mlname).SingleOrDefault();
                        if (themeal != null)
                        {
                            MyLiteral("literal" + litname[j] + "Rows").Text += "<tr class=\"FoodDiaryMealTitle\" name=\"" + menuItem.MealTimeId + "-" + menuItem.FromMealId + "\"><td colspan=\"8\" style=\"font-family: Arial; font-style:bold; font-size: 12px; text-align: left; padding: " + (isFirst ? "0px 12px" : "0 12px 6px 12px") + ";\">";
                            MyLiteral("literal" + litname[j] + "Rows").Text += "<div style=\"float:left;margin-top: 6px; margin-right:10px;\"><a href=\"/club-vision/my-eating/menus/?tab=" + (themeal.CustomerId == (int)Session["MemberNo"] ? "edit" : "view") + "_meal&mealId=" + themeal.Id + "\">" + themeal.Name + "</a></div>";
                            if (themeal.VideoId != null)
                            {
                                var themealbrightcove = (from tmbc in cvdc.MealFromBrightCoves
                                                         where tmbc.VideoId == themeal.VideoId
                                                         select tmbc).SingleOrDefault();
                                if (themealbrightcove != null)
                                    MyLiteral("literal" + litname[j] + "Rows").Text += "<a href=\"/media" + themealbrightcove.DownloadPDF + "\" target=\"_blank\"><img src=\"/images/iconDownload.png\" alt=\"Download Recipe\" title=\"Download Recipe\"/></a>";
                                MyLiteral("literal" + litname[j] + "Rows").Text += "<a href=\"/club-vision/education/vision-tv?vid=" + themeal.VideoId + "\"><img src=\"/images/iconPlay.png\" alt=\"Play Video\" title=\"Play Video\"/></a>";
                            }
                            if (themeal.VideoId == null && themeal.PDFAddress != null)
                            {
                                MyLiteral("literal" + litname[j] + "Rows").Text += "<a href=\"/club-vision/recipe/" + themeal.PDFAddress + "\" target=\"_blank\"><img src=\"/images/iconDownload.png\" alt=\"Download Recipe\" title=\"Download Recipe\"/></a>";
                            }
                            MyLiteral("literal" + litname[j] + "Rows").Text += "</td></tr>";
                            MealNumber = themeal.Id;
                        }
                    }

                    if (menuItem.FromItemsAdd == null)
                    {
                        MyLiteral("literal" + litname[j] + "Rows").Text += "<tr data-id=\"" + menuItem.Id + "\" ><td colspan=\"2\" style=\"font-family: Arial; font-size: 12px; text-align: left; padding: 0 12px 6px 12px;\">" + (MealNumber == menuItem.FromMealId ? "<span style=\"color:#E27423;\">&nbsp;&nbsp;&raquo;&nbsp;&nbsp;</span>" : "") + menuItem.Item.Name + "</td>";
                        MyLiteral("literal" + litname[j] + "Rows").Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: 0 12px 6px 12px;\">" + menuItem.Amount.ToString("0.00") + "</td>";
                        MyLiteral("literal" + litname[j] + "Rows").Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: left; padding: 0 12px 6px 12px;\">" + menuItem.Item.ServeUnit + "</td>";
                        MyLiteral("literal" + litname[j] + "Rows").Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: 0 12px 6px 12px;\">" + (menuItem.Item.Carbohydrate * (menuItem.Amount / menuItem.Item.ServeAmount)).ToString("0.00") + "</td>";
                        MyLiteral("literal" + litname[j] + "Rows").Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: 0 12px 6px 12px;\">" + (menuItem.Item.Protein * (menuItem.Amount / menuItem.Item.ServeAmount)).ToString("0.00") + "</td>";
                        MyLiteral("literal" + litname[j] + "Rows").Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: 0 12px 6px 12px;\">" + (menuItem.Item.Fat * (menuItem.Amount / menuItem.Item.ServeAmount)).ToString("0.00") + "</td>";
                        MyLiteral("literal" + litname[j] + "Rows").Text += "<td style=\"text-align: center; padding: 0 0 6px 0;\"></td></tr>";

                        carbohydrate += (menuItem.Item.Carbohydrate * (menuItem.Amount / menuItem.Item.ServeAmount));
                        protein += (menuItem.Item.Protein * (menuItem.Amount / menuItem.Item.ServeAmount));
                        fat += (menuItem.Item.Fat * (menuItem.Amount / menuItem.Item.ServeAmount));

                        carbohydrate_section += (menuItem.Item.Carbohydrate * (menuItem.Amount / menuItem.Item.ServeAmount));
                        protein_section += (menuItem.Item.Protein * (menuItem.Amount / menuItem.Item.ServeAmount));
                        fat_section += (menuItem.Item.Fat * (menuItem.Amount / menuItem.Item.ServeAmount));
                        if (i != 1) continue;
                        if (isFirst)
                        {
                            isFirst = false;
                        }
                    }
                    else
                    {
                        MyLiteral("literal" + litname[j] + "Rows").Text += "<tr data-id=\"" + menuItem.Id + "\" ><td colspan=\"2\" style=\"font-family: Arial; font-size: 12px; font-style:italic; text-align: left; padding: 0 12px 6px 12px;\">" + (MealNumber == menuItem.FromMealId ? "<span style=\"color:#E27423;\">&nbsp;&nbsp;&raquo;&nbsp;&nbsp;</span>" : "") + "*" + menuItem.ItemAdd.Name + "</td>";
                        MyLiteral("literal" + litname[j] + "Rows").Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: 0 12px 6px 12px;\">" + menuItem.Amount.ToString("0.00") + "</td>";
                        MyLiteral("literal" + litname[j] + "Rows").Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: left; padding: 0 12px 6px 12px;\">" + menuItem.ItemAdd.ServeUnit + "</td>";
                        MyLiteral("literal" + litname[j] + "Rows").Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: 0 12px 6px 12px;\">" + (menuItem.ItemAdd.Carbohydrate * (menuItem.Amount / menuItem.ItemAdd.ServeAmount)).ToString("0.00") + "</td>";
                        MyLiteral("literal" + litname[j] + "Rows").Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: 0 12px 6px 12px;\">" + (menuItem.ItemAdd.Protein * (menuItem.Amount / menuItem.ItemAdd.ServeAmount)).ToString("0.00") + "</td>";
                        MyLiteral("literal" + litname[j] + "Rows").Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: 0 12px 6px 12px;\">" + (menuItem.ItemAdd.Fat * (menuItem.Amount / menuItem.ItemAdd.ServeAmount)).ToString("0.00") + "</td>";
                        MyLiteral("literal" + litname[j] + "Rows").Text += "<td style=\"text-align: center; padding: 0 0 6px 0;\"></td></tr>";

                        carbohydrate += (menuItem.ItemAdd.Carbohydrate * (menuItem.Amount / menuItem.ItemAdd.ServeAmount));
                        protein += (menuItem.ItemAdd.Protein * (menuItem.Amount / menuItem.ItemAdd.ServeAmount));
                        fat += (menuItem.ItemAdd.Fat * (menuItem.Amount / menuItem.ItemAdd.ServeAmount));

                        carbohydrate_section += (menuItem.ItemAdd.Carbohydrate * (menuItem.Amount / menuItem.ItemAdd.ServeAmount));
                        protein_section += (menuItem.ItemAdd.Protein * (menuItem.Amount / menuItem.ItemAdd.ServeAmount));
                        fat_section += (menuItem.ItemAdd.Fat * (menuItem.Amount / menuItem.ItemAdd.ServeAmount));
                    }
                    if (i != 1) continue;
                    if (isFirst)
                    {
                        isFirst = false;
                    }
                }

                MyLiteral("literal" + litname[j] + "Carb").Text = carbohydrate_section.ToString("0.00");
                MyLiteral("literal" + litname[j] + "Protein").Text = protein_section.ToString("0.00");
                MyLiteral("literal" + litname[j] + "Fat").Text = fat_section.ToString("0.00");

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
                            goal_carbohydrate = customer.Carb;
                            goal_protein = customer.Protein;
                            goal_fat = customer.Fat;
                        }
                    } break;
                case "VVT":
                    {
                        var _customers = (from customers in cvdc.Customer_Externals
                                          where customers.iID == (int)Session["MemberNo"]
                                          select customers).SingleOrDefault();
                        if (_customers != null)
                        {
                            var custMacros =
                                _customers.Goals.OrderByDescending(x => x.dDateCreated).FirstOrDefault();
                            if (custMacros != null)
                            {
                                goal_carbohydrate = (decimal)custMacros.CHO;
                                goal_protein = (decimal)custMacros.PTN;
                                goal_fat = (decimal)custMacros.FAT;
                            }
                        }
                    } break;
            }

            literalGoalCarb.Text = goal_carbohydrate.ToString("0.00");
            literalGoalProtein.Text = goal_protein.ToString("0.00");
            literalGoalFat.Text = goal_fat.ToString("0.00");

            literalDiffCarb.Text = (carbohydrate - goal_carbohydrate).ToString("0.00");
            literalDiffProtein.Text = (protein - goal_protein).ToString("0.00");
            literalDiffFat.Text = (fat - goal_fat).ToString("0.00");

            cvdc.Dispose();

            menuScript.Text = "$('.buttons').hide();";
        }

        private void UpdateEditMenu(int menuId)
        {
            menuName.Value = GetMenuName(menuId);
            menuName.Attributes["onblur"] = "menu_editName(this.value.replace('&','%26'));";
            
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
            int MealNumber = 0;


            ClubVisionDataContext cvdc = new ClubVisionDataContext();

            List<string> litname = new List<string>() { "Breakfast", "MorningTea", "Lunch", "AfternoonTea", "Dinner", "Supper" };
            bool isFirst = true;

            for(int i = 1; i <= 6 ; i++)
            {
                int i1 = i;
                int j = i - 1;

                var menuItems = (from mi in cvdc.MenuItems
                                 where mi.MealTimeId == i1
                                 where mi.MenuId == menuId
                                 select mi);

                carbohydrate_section = 0;
                protein_section = 0;
                fat_section = 0;
                MealNumber = 0;

                foreach (MenuItem menuItem in menuItems)
                {
                    if (menuItem.FromMealId != null && MealNumber != menuItem.FromMealId)
                    {
                        var themeal = (from mlname in cvdc.Meals
                                       where mlname.Id == menuItem.FromMealId
                                       select mlname).SingleOrDefault();
                        if (themeal != null)
                        {
                            MyLiteral("literal" + litname[j] + "Rows").Text += "<tr class=\"FoodDiaryMealTitle\" name=\"" + menuItem.MealTimeId + "-" + menuItem.FromMealId + "\"><td colspan=\"8\" style=\"font-family: Arial; font-style:bold; font-size: 12px; text-align: left; padding: " + (isFirst ? "0px 12px" : "0 12px 6px 12px") + ";\">";
                            MyLiteral("literal" + litname[j] + "Rows").Text += "<div style=\"float:left;margin-top: 6px; margin-right:10px;\"><a href=\"/club-vision/my-eating/menus/?tab=" + (themeal.CustomerId == (int)Session["MemberNo"] ? "edit" : "view") + "_meal&mealId=" + themeal.Id + "\">" + themeal.Name + "</a></div>";
                            if (themeal.VideoId != null)
                            {
                                var themealbrightcove = (from tmbc in cvdc.MealFromBrightCoves
                                                         where tmbc.VideoId == themeal.VideoId
                                                         select tmbc).SingleOrDefault();
                                if (themealbrightcove != null)
                                    MyLiteral("literal" + litname[j] + "Rows").Text += "<a href=\"/media" + themealbrightcove.DownloadPDF + "\" target=\"_blank\"><img src=\"/images/iconDownload.png\" alt=\"Download Recipe\" title=\"Download Recipe\"/></a>";
                                MyLiteral("literal" + litname[j] + "Rows").Text += "<a href=\"/club-vision/education/vision-tv?vid=" + themeal.VideoId + "\"><img src=\"/images/iconPlay.png\" alt=\"Play Video\" title=\"Play Video\"/></a>";
                            }
                            if (themeal.VideoId == null && themeal.PDFAddress != null)
                            {
                                MyLiteral("literal" + litname[j] + "Rows").Text += "<a href=\"/club-vision/recipe/" + themeal.PDFAddress + "\" target=\"_blank\"><img src=\"/images/iconDownload.png\" alt=\"Download Recipe\" title=\"Download Recipe\"/></a>";
                            }
                            MyLiteral("literal" + litname[j] + "Rows").Text += "</td></tr>";
                            MealNumber = themeal.Id;
                        }
                    }
                    if (menuItem.FromItemsAdd == null)
                    {
                        if (hl == menuItem.Id)
                        {
                            MyLiteral("literal" + litname[j] + "Rows").Text += "<tr data-id=\"" + menuItem.Id + "\"  style=\"background-color: #f8dcc8\"><td colspan=\"2\" style=\"font-family: Arial; font-size: 12px; text-align: left; padding: 0 12px 6px 12px;\">" + (MealNumber == menuItem.FromMealId ? "<span style=\"color:#E27423;\">&nbsp;&nbsp;&raquo;&nbsp;&nbsp;</span>" : "") + menuItem.Item.Name + "</td>";
                        }
                        else
                        {
                            MyLiteral("literal" + litname[j] + "Rows").Text += "<tr data-id=\"" + menuItem.Id + "\" ><td colspan=\"2\" style=\"font-family: Arial; font-size: 12px; text-align: left; padding: 0 12px 6px 12px;\">" + (MealNumber == menuItem.FromMealId ? "<span style=\"color:#E27423;\">&nbsp;&nbsp;&raquo;&nbsp;&nbsp;</span>" : "") + menuItem.Item.Name + "</td>";
                        }
                        MyLiteral("literal" + litname[j] + "Rows").Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: 0 12px 6px 12px;\"><input class=\"diary_amount\" type=\"text\" id=\"menu_editAmount" + menuItem.Id + "\" value=\"" + menuItem.Amount.ToString("0.00") + "\" onblur=\"menu_editAmount(" + menuItem.Id + ", " + i1 + ", $(this));\" data-serve-amount=\"" + menuItem.Item.ServeAmount + "\" data-carbohydrate=\"" + menuItem.Item.Carbohydrate + "\" data-protein=\"" + menuItem.Item.Protein + "\" data-fat=\"" + menuItem.Item.Fat + "\" ></td>";
                        MyLiteral("literal" + litname[j] + "Rows").Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: left; padding: 0 12px 6px 12px;\">" + ServeUnit(menuItem, i1) + "</td>";
                        MyLiteral("literal" + litname[j] + "Rows").Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: 0 12px 6px 12px;\">" + (menuItem.Item.Carbohydrate * (menuItem.Amount / menuItem.Item.ServeAmount)).ToString("0.00") + "</td>";
                        MyLiteral("literal" + litname[j] + "Rows").Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: 0 12px 6px 12px;\">" + (menuItem.Item.Protein * (menuItem.Amount / menuItem.Item.ServeAmount)).ToString("0.00") + "</td>";
                        MyLiteral("literal" + litname[j] + "Rows").Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: 0 12px 6px 12px;\">" + (menuItem.Item.Fat * (menuItem.Amount / menuItem.Item.ServeAmount)).ToString("0.00") + "</td>";
                        MyLiteral("literal" + litname[j] + "Rows").Text += "<td style=\"text-align: center; padding: 0 0 6px 0;\">" + "<a onclick=\"menu_deleteRow(" + menuItem.Id + "," + i1 + ");$(this).parent().parent().remove();\" style=\"cursor: pointer;position: relative;left: -12px;\"><img src=\"/images/delete" + (hl == menuItem.Id ? "_hl" : "") + ".gif\" border=\"0\"></a>" + "</td></tr>";

                        carbohydrate += (menuItem.Item.Carbohydrate * (menuItem.Amount / menuItem.Item.ServeAmount));
                        protein += (menuItem.Item.Protein * (menuItem.Amount / menuItem.Item.ServeAmount));
                        fat += (menuItem.Item.Fat * (menuItem.Amount / menuItem.Item.ServeAmount));

                        carbohydrate_section += (menuItem.Item.Carbohydrate * (menuItem.Amount / menuItem.Item.ServeAmount));
                        protein_section += (menuItem.Item.Protein * (menuItem.Amount / menuItem.Item.ServeAmount));
                        fat_section += (menuItem.Item.Fat * (menuItem.Amount / menuItem.Item.ServeAmount));
                        if (i != 1) continue;
                        if (isFirst)
                        {
                            isFirst = false;
                        }
                    }
                    else
                    {
                        if (hl == menuItem.Id)
                        {
                            MyLiteral("literal" + litname[j] + "Rows").Text += "<tr data-id=\"" + menuItem.Id + "\"  style=\"background-color: #f8dcc8\"><td colspan=\"2\" style=\"font-family: Arial; font-size: 12px; text-align: left; font-style : italic; padding: 0 12px 6px 12px;\">" + (MealNumber == menuItem.FromMealId ? "<span style=\"color:#E27423;\">&nbsp;&nbsp;&raquo;&nbsp;&nbsp;</span>" : "") + "*" + menuItem.ItemAdd.Name + "</td>";
                        }
                        else
                        {
                            MyLiteral("literal" + litname[j] + "Rows").Text += "<tr data-id=\"" + menuItem.Id + "\" ><td colspan=\"2\" style=\"font-family: Arial; font-size: 12px; text-align: left; padding: 0 12px 6px 12px;font-style : italic;\">" + (MealNumber == menuItem.FromMealId ? "<span style=\"color:#E27423;\">&nbsp;&nbsp;&raquo;&nbsp;&nbsp;</span>" : "") + "*" + menuItem.ItemAdd.Name + "</td>";
                        }
                        MyLiteral("literal" + litname[j] + "Rows").Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: 0 12px 6px 12px;\"><input class=\"diary_amount\" type=\"text\" id=\"menu_editAmount" + menuItem.Id + "\" value=\"" + menuItem.Amount.ToString("0.00") + "\" onblur=\"menu_editAmount(" + menuItem.Id + ", " + i1 + ", $(this));\" data-serve-amount=\"" + menuItem.ItemAdd.ServeAmount + "\" data-carbohydrate=\"" + menuItem.ItemAdd.Carbohydrate + "\" data-protein=\"" + menuItem.ItemAdd.Protein + "\" data-fat=\"" + menuItem.ItemAdd.Fat + "\" ></td>";
                        MyLiteral("literal" + litname[j] + "Rows").Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: left; padding: 0 12px 6px 12px;\">" + ServeUnit(menuItem, i1) + "</td>";
                        MyLiteral("literal" + litname[j] + "Rows").Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: 0 12px 6px 12px;\">" + (menuItem.ItemAdd.Carbohydrate * (menuItem.Amount / menuItem.ItemAdd.ServeAmount)).ToString("0.00") + "</td>";
                        MyLiteral("literal" + litname[j] + "Rows").Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: 0 12px 6px 12px;\">" + (menuItem.ItemAdd.Protein * (menuItem.Amount / menuItem.ItemAdd.ServeAmount)).ToString("0.00") + "</td>";
                        MyLiteral("literal" + litname[j] + "Rows").Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: 0 12px 6px 12px;\">" + (menuItem.ItemAdd.Fat * (menuItem.Amount / menuItem.ItemAdd.ServeAmount)).ToString("0.00") + "</td>";
                        MyLiteral("literal" + litname[j] + "Rows").Text += "<td style=\"text-align: center; padding: 0 0 6px 0;\">" + "<a onclick=\"menu_deleteRow(" + menuItem.Id + "," + i1 + ");$(this).parent().parent().remove();\" style=\"cursor: pointer;position: relative;left: -12px;\"><img src=\"/images/delete" + (hl == menuItem.Id ? "_hl" : "") + ".gif\" border=\"0\"></a>" + "</td></tr>";

                        carbohydrate += (menuItem.ItemAdd.Carbohydrate * (menuItem.Amount / menuItem.ItemAdd.ServeAmount));
                        protein += (menuItem.ItemAdd.Protein * (menuItem.Amount / menuItem.ItemAdd.ServeAmount));
                        fat += (menuItem.ItemAdd.Fat * (menuItem.Amount / menuItem.ItemAdd.ServeAmount));

                        carbohydrate_section += (menuItem.ItemAdd.Carbohydrate * (menuItem.Amount / menuItem.ItemAdd.ServeAmount));
                        protein_section += (menuItem.ItemAdd.Protein * (menuItem.Amount / menuItem.ItemAdd.ServeAmount));
                        fat_section += (menuItem.ItemAdd.Fat * (menuItem.Amount / menuItem.ItemAdd.ServeAmount));
                    }

                    if (i != 1) continue;
                    if (isFirst)
                    {
                        isFirst = false;
                    }
                }

                MyLiteral("literal" + litname[j] + "Carb").Text = carbohydrate_section.ToString("0.00");
                MyLiteral("literal" + litname[j] + "Protein").Text = protein_section.ToString("0.00");
                MyLiteral("literal" + litname[j] + "Fat").Text = fat_section.ToString("0.00");
            
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
                            goal_carbohydrate = customer.Carb;
                            goal_protein = customer.Protein;
                            goal_fat = customer.Fat;
                        }
                    } break;
                case "VVT":
                    {
                        var _customers = (from customers in cvdc.Customer_Externals
                                          where customers.iID == (int)Session["MemberNo"]
                                          select customers).SingleOrDefault();
                        if (_customers != null)
                        {
                            var custMacros =
                                _customers.Goals.OrderByDescending(x => x.dDateCreated).FirstOrDefault();
                            if (custMacros != null)
                            {
                                goal_carbohydrate = (decimal)custMacros.CHO;
                                goal_protein = (decimal)custMacros.PTN;
                                goal_fat = (decimal)custMacros.FAT;
                            }
                        }
                    } break;
            }

            literalGoalCarb.Text = goal_carbohydrate.ToString("0.00");
            literalGoalProtein.Text = goal_protein.ToString("0.00");
            literalGoalFat.Text = goal_fat.ToString("0.00");

            literalDiffCarb.Text = (carbohydrate - goal_carbohydrate).ToString("0.00");
            literalDiffProtein.Text = (protein - goal_protein).ToString("0.00");
            literalDiffFat.Text = (fat - goal_fat).ToString("0.00");

            cvdc.Dispose();

        }

        private void UpdateViewMeal(int mealId)
        {
            ClubVisionDataContext cvdc = new ClubVisionDataContext();

            var _meals = (from meals in cvdc.Meals
                          where meals.Id == mealId
                          select meals);
            /*
            mealName.Value = _meals.First().Name;
            mealName.Disabled = true;
            mealName.Style["display"] = "none";
            */

            mealNameDiv.Style["font-family"] = "cursive";
            mealNameDiv.InnerHtml = _meals.First().Name;
            mealImg.Src = "/images/meals/" + _meals.First().ImageUrl;
            ShareDivOnclickDefiner(mealId);
            shareToHQ.Visible = false;
            shareDailyPlan_Select.Style["height"] = "112px";

            if((string)Session["MemberType"] == "VVT")
            {
                shareDailyPlan_Select.Style["height"] = "80px";
                shareToTrainerOrClient.Visible = false;
            }

            if ((string)Session["Admin"] == "Yes")
            {
                panel_admin.Visible = true;
            }
            else
            {
                panel_admin.Visible = false;

                saveMealImage.Src = "/images/buttonCopyToMyMeals.png";
                saveMeal.HRef = "/club-vision/my-eating/menus/?tab=copy_meal&mealId=" + mealId;
                saveMealImage.Alt = "Copy to My Meals";
            }


            lnkTV.Visible = false;
            lnkDL.Visible = false;

            if (_meals.First().VideoId == null)
            {
                if (_meals.First().PDFAddress != null)
                {
                    lnkDL.HRef = "/club-vision/recipe/" + _meals.First().PDFAddress;
                    lnkDL.Visible = true;
                }
            }
            else
            {
                videoId.Value = _meals.First().VideoId.ToString();
                lnkTV.HRef = "/club-vision/education/vision-tv?vid=" + _meals.First().VideoId;
                //lnkTV.InnerText = " TV ";

                BCVideo video = BrightCoveRequest(_meals.First().VideoId.ToString());

                if(video != null)
                {
                    if (video.LinkURL != "")
                    {
                        lnkDL.HRef = video.LinkURL;
                        lnkDL.Visible = true;
                    }
                    lnkTV.Visible = true;

                }
            }

            checkboxIsRecommendedMeal.Checked = _meals.First().IsRecommended;

            decimal carbohydrate = 0;
            decimal protein = 0;
            decimal fat = 0;


            var mealItems = (from mi in cvdc.MealItems
                             where mi.MealId == mealId
                             select mi);

            bool isFirst = true;

            foreach (MealItem mealItem in mealItems)
            {
                if(mealItem.FromItemsAdd == null)
                {
                    literalMealRows.Text += "<tr data-id=\"" + mealItem.Id + "\"><td colspan=\"2\" style=\"font-family: Arial; font-size: 12px; text-align: left; padding: 0 12px 6px 12px;\">" + mealItem.Item.Name + "</td>";
                    literalMealRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: 0 12px 6px 12px;\">" + mealItem.Amount.ToString("0.00") + "</td>";
                    literalMealRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: 0 12px 6px 12px;\">" + mealItem.Item.ServeUnit + "</td>";
                    literalMealRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: 0 12px 6px 12px;\">" + (mealItem.Item.Carbohydrate * (mealItem.Amount / mealItem.Item.ServeAmount)).ToString("0.00") + "</td>";
                    literalMealRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: 0 12px 6px 12px;\">" + (mealItem.Item.Protein * (mealItem.Amount / mealItem.Item.ServeAmount)).ToString("0.00") + "</td>";
                    literalMealRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: 0 12px 6px 12px;\">" + (mealItem.Item.Fat * (mealItem.Amount / mealItem.Item.ServeAmount)).ToString("0.00") + "</td>";
                    literalMealRows.Text += "<td style=\"text-align: center; padding: 0 0 6px 0;\"></td></tr>";
                    carbohydrate += (mealItem.Item.Carbohydrate * (mealItem.Amount / mealItem.Item.ServeAmount));
                    protein += (mealItem.Item.Protein * (mealItem.Amount / mealItem.Item.ServeAmount));
                    fat += (mealItem.Item.Fat * (mealItem.Amount / mealItem.Item.ServeAmount));
                }
                else
                {
                    literalMealRows.Text += "<tr data-id=\"" + mealItem.Id + "\"><td colspan=\"2\" style=\"font-family: Arial; font-size: 12px; text-align: left; font-style:italic; padding: 0 12px 6px 12px;\">*" + mealItem.ItemAdd.Name + "</td>";
                    literalMealRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: 0 12px 6px 12px;\">" + mealItem.Amount.ToString("0.00") + "</td>";
                    literalMealRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: 0 12px 6px 12px;\">" + mealItem.ItemAdd.ServeUnit + "</td>";
                    literalMealRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: 0 12px 6px 12px;\">" + (mealItem.ItemAdd.Carbohydrate * (mealItem.Amount / mealItem.ItemAdd.ServeAmount)).ToString("0.00") + "</td>";
                    literalMealRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: 0 12px 6px 12px;\">" + (mealItem.ItemAdd.Protein * (mealItem.Amount / mealItem.ItemAdd.ServeAmount)).ToString("0.00") + "</td>";
                    literalMealRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: 0 12px 6px 12px;\">" + (mealItem.ItemAdd.Fat * (mealItem.Amount / mealItem.ItemAdd.ServeAmount)).ToString("0.00") + "</td>";
                    literalMealRows.Text += "<td style=\"text-align: center; padding: 0 0 6px 0;\"></td></tr>";
                    carbohydrate += (mealItem.ItemAdd.Carbohydrate * (mealItem.Amount / mealItem.ItemAdd.ServeAmount));
                    protein += (mealItem.ItemAdd.Protein * (mealItem.Amount / mealItem.ItemAdd.ServeAmount));
                    fat += (mealItem.ItemAdd.Fat * (mealItem.Amount / mealItem.ItemAdd.ServeAmount));
                }
                

                if (isFirst)
                {
                    isFirst = false;
                }
            }


            literalMealCarb.Text = carbohydrate.ToString("0.00");
            literalMealProtein.Text = protein.ToString("0.00");
            literalMealFat.Text = fat.ToString("0.00");


            mealAddToDiary.Text += "<img src=\"/images/buttonAddToDiary.gif\" style=\"cursor: pointer; float: right\" onclick=\"var result = toggle_str('meal_" + _meals.First().Id + "');hide_one_of_nobuttons();switchto('meal_" + _meals.First().Id + "', result);\" />";
            mealAddToDiary.Text += "<div id=\"meal_" + _meals.First().Id + "\" class=\"one_of\" style=\"z-index: 100; top:320px; left: 870px; border: 1px solid #a0a0a0; display: none; position: absolute; width: 198px; height: 250px; background-color: #f1f1f1\" >";
            mealAddToDiary.Text += "<div class=\"quicktools_copyto_header\">Copy to which meal?</div>";
            mealAddToDiary.Text += "<div class=\"quicktools_option\"><select  id=\"meal_" + _meals.First().Id + "_time\"><option value=\"1\">Breakfast</option><option value=\"2\">Morning Tea</option><option value=\"3\">Lunch</option><option value=\"4\">Afternoon Tea</option><option value=\"5\">Dinner</option><option value=\"6\">Supper</option></select></div>";
            mealAddToDiary.Text += "<div class=\"quicktools_copyto_header\">Copy to which date?</div>";
            mealAddToDiary.Text += CopyToDates_Meal(_meals.First().Id);
            mealAddToDiary.Text += "</div>";

            cvdc.Dispose();

            menuScript.Text = "$('.buttons').hide();$('buttons').hide();";
        }

        private void UpdateEditMeal(int mealId)
        {
            ClubVisionDataContext cvdc = new ClubVisionDataContext();

            var _meals = (from meals in cvdc.Meals
                          where meals.Id == mealId
                          select meals);

            if(_meals.First().CustomerId != Convert.ToInt32(Session["MemberNo"]) && (string)Session["Admin"] == "No")
            {
                Response.Redirect("/club-vision/my-eating/menus/?tab=view_meal&mealId=" + mealId);
            }
            else
            {
                downloadRecipeImage.Src = "/images/buttonMealRecipe.gif";
                //mealName.Value = _meals.First().Name;

                mealNameDiv.Style["font-family"] = "cursive";
                mealNameDiv.InnerHtml = _meals.First().Name + "<img src=\"/images/iconEdit.png\" onclick=\"editMealName('" + _meals.First().Name + "'); return false;\" style=\"cursor:pointer;margin-left:10px;\" alt=\"edit name\" title=\"Edit Meal Name\"/>";

                mealImgDiv.Attributes["onmouseover"] = "$('#editMealImgDiv').css('display','block');";
                mealImgDiv.Attributes["onmouseout"] = "$('#editMealImgDiv').css('display','none');";
                editMealImgDiv.Attributes["onclick"] = "editMealPicture(); return false;";
                lnkEditPortion.Visible = true;
                shareDailyPlan_Select.Style["left"] = "742px";

                if ((string)Session["MemberType"] == "VVT")
                {
                    shareDailyPlan_Select.Style["height"] = "112px";
                    shareToTrainerOrClient.Visible = false;
                }

                ShareDivOnclickDefiner(mealId);

                if (_meals.First().ImageUrl.Equals("meal_generic.jpg"))
                {
                    mealImg.Src = "/images/meals/meal_generic.jpg";
                    editMealImgDiv.Style["display"] = "block";
                }
                else
                {
                    mealImg.Src = "/images/meals/" + _meals.First().ImageUrl;
                }


                panel_admin.Visible = (string)Session["Admin"] == "Yes";

                saveMeal.Visible = false;
                lnkTV.Visible = false;
                //lnkDL.Visible = false;
                lnkDL.Attributes["onclick"] =
                    "var result = toggle_str('mealRecipeOptions');hide_one_of();switchto('mealRecipeOptions', result);";
                upRecipe.Attributes["onclick"] = "hide_one_of();$('.uploaddiv').css('display', 'none');$('#mealRecipeUploadDiv').slideDown();";
                
                if(_meals.First().PDFAddress != null)
                {
                    downRecipe.Attributes["onclick"] = "hide_one_of();window.open('/club-vision/recipe/" + _meals.First().PDFAddress + "','_blank')";
                }
                else
                {
                    downRecipe.Attributes["onclick"] = "hide_one_of();mealNoRecipeFound();";
                }

                if (_meals.First().VideoId != null)
                {
                    videoId.Value = _meals.First().VideoId.ToString();
                    lnkTV.HRef = "/club-vision/education/vision-tv?vid=" + _meals.First().VideoId;
                    //lnkTV.InnerText = " TV ";

                    BCVideo video = BrightCoveRequest(_meals.First().VideoId.ToString());

                    if (video != null)
                    {
                        if (video.LinkURL != "")
                        {
                            lnkDL.HRef = video.LinkURL;
                            lnkDL.Visible = true;
                        }
                        lnkTV.Visible = true;

                    }
                    
                }

                checkboxIsRecommendedMeal.Checked = _meals.First().IsRecommended;

                int hl = -1;
                if (Request.QueryString["hl"] != null)
                {
                    hl = int.Parse(Request.QueryString["hl"]);
                }

                decimal carbohydrate = 0;
                decimal protein = 0;
                decimal fat = 0;

                var mealItems = (from mi in cvdc.MealItems
                                 where mi.MealId == mealId
                                 select mi);

                bool isFirst = true;

                foreach (MealItem mealItem in mealItems)
                {
                    if (mealItem.FromItemsAdd == null)
                    {
                        if (hl == mealItem.Id)
                        {
                            literalMealRows.Text += "<tr data-id=\"" + mealItem.Id + "\" style=\"background-color: #f8dcc8\"><td colspan=\"2\" style=\"font-family: Arial; font-size: 12px; text-align: left; padding: 0 12px 6px 12px;\">" + mealItem.Item.Name + "</td>";
                        }
                        else
                        {
                            literalMealRows.Text += "<tr data-id=\"" + mealItem.Id + "\"><td colspan=\"2\" style=\"font-family: Arial; font-size: 12px; text-align: left; padding: 0 12px 6px 12px;\">" + mealItem.Item.Name + "</td>";
                        }
                        literalMealRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: 0 12px 6px 12px;\"><input class=\"diary_amount\" type=\"text\" id=\"editAmount" + mealItem.Id + "\" value=\"" + mealItem.Amount.ToString("0.00") + "\" onblur=\"meal_editAmount(" + mealItem.Id + ", $(this));\" data-serve-amount=\"" + mealItem.Item.ServeAmount + "\" data-carbohydrate=\"" + mealItem.Item.Carbohydrate + "\" data-protein=\"" + mealItem.Item.Protein + "\" data-fat=\"" + mealItem.Item.Fat + "\"></td>";
                        literalMealRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: 0 12px 6px 12px;\">" + ServeUnit(mealItem) + "</td>";
                        literalMealRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: 0 12px 6px 12px;\">" + (mealItem.Item.Carbohydrate * (mealItem.Amount / mealItem.Item.ServeAmount)).ToString("0.00") + "</td>";
                        literalMealRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: 0 12px 6px 12px;\">" + (mealItem.Item.Protein * (mealItem.Amount / mealItem.Item.ServeAmount)).ToString("0.00") + "</td>";
                        literalMealRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: 0 12px 6px 12px;\">" + (mealItem.Item.Fat * (mealItem.Amount / mealItem.Item.ServeAmount)).ToString("0.00") + "</td>";
                        literalMealRows.Text += "<td style=\"text-align: center; padding: 0 0 6px 0;\">" + "<a onclick=\"meal_deleteRow(" + mealItem.Id + ");$(this).parent().parent().remove();\" style=\"cursor: pointer;position: relative;left: -12px;\"><img src=\"/images/delete" + (hl == mealItem.Id ? "_hl" : "") + ".gif\" border=\"0\"></a>" + "</td></tr>";
                        carbohydrate += (mealItem.Item.Carbohydrate * (mealItem.Amount / mealItem.Item.ServeAmount));
                        protein += (mealItem.Item.Protein * (mealItem.Amount / mealItem.Item.ServeAmount));
                        fat += (mealItem.Item.Fat * (mealItem.Amount / mealItem.Item.ServeAmount));
                    }
                    else
                    {
                        if (hl == mealItem.Id)
                        {
                            literalMealRows.Text += "<tr data-id=\"" + mealItem.Id + "\" style=\"background-color: #f8dcc8\"><td colspan=\"2\" style=\"font-family: Arial; font-size: 12px; text-align: left; padding: 0 12px 6px 12px;\">" + mealItem.ItemAdd.Name + "</td>";
                        }
                        else
                        {
                            literalMealRows.Text += "<tr data-id=\"" + mealItem.Id + "\"><td colspan=\"2\" style=\"font-family: Arial; font-size: 12px; text-align: left; font-style: italic; padding: 0 12px 6px 12px;\">*" + mealItem.ItemAdd.Name + "</td>";
                        }
                        literalMealRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: 0 12px 6px 12px;\"><input class=\"diary_amount\" type=\"text\" id=\"editAmount" + mealItem.Id + "\" value=\"" + mealItem.Amount.ToString("0.00") + "\" onblur=\"meal_editAmount(" + mealItem.Id + ", $(this));\" data-serve-amount=\"" + mealItem.ItemAdd.ServeAmount + "\" data-carbohydrate=\"" + mealItem.ItemAdd.Carbohydrate + "\" data-protein=\"" + mealItem.ItemAdd.Protein + "\" data-fat=\"" + mealItem.ItemAdd.Fat + "\"></td>";
                        literalMealRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: 0 12px 6px 12px;\">" + ServeUnit(mealItem) + "</td>";
                        literalMealRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: 0 12px 6px 12px;\">" + (mealItem.ItemAdd.Carbohydrate * (mealItem.Amount / mealItem.ItemAdd.ServeAmount)).ToString("0.00") + "</td>";
                        literalMealRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: 0 12px 6px 12px;\">" + (mealItem.ItemAdd.Protein * (mealItem.Amount / mealItem.ItemAdd.ServeAmount)).ToString("0.00") + "</td>";
                        literalMealRows.Text += "<td style=\"font-weight: bold; font-family: Arial; font-size: 12px; text-align: center; padding: 0 12px 6px 12px;\">" + (mealItem.ItemAdd.Fat * (mealItem.Amount / mealItem.ItemAdd.ServeAmount)).ToString("0.00") + "</td>";
                        literalMealRows.Text += "<td style=\"text-align: center; padding: 0 0 6px 0;\">" + "<a onclick=\"meal_deleteRow(" + mealItem.Id + ");$(this).parent().parent().remove();\" style=\"cursor: pointer;position: relative;left: -12px;\"><img src=\"/images/delete" + (hl == mealItem.Id ? "_hl" : "") + ".gif\" border=\"0\"></a>" + "</td></tr>";
                        carbohydrate += (mealItem.ItemAdd.Carbohydrate * (mealItem.Amount / mealItem.ItemAdd.ServeAmount));
                        protein += (mealItem.ItemAdd.Protein * (mealItem.Amount / mealItem.ItemAdd.ServeAmount));
                        fat += (mealItem.ItemAdd.Fat * (mealItem.Amount / mealItem.ItemAdd.ServeAmount));
                    }


                    if (isFirst)
                    {
                        isFirst = false;
                    }
                }


                literalMealCarb.Text = carbohydrate.ToString("0.00");
                literalMealProtein.Text = protein.ToString("0.00");
                literalMealFat.Text = fat.ToString("0.00");

                mealAddToDiary.Text += "<img src=\"/images/buttonAddToDiary.gif\" style=\"cursor: pointer; float: right\" onclick=\"var result = toggle_str('meal_" + _meals.First().Id + "');hide_one_of();switchto('meal_" + _meals.First().Id + "', result);\" />";
                mealAddToDiary.Text += "<div id=\"meal_" + _meals.First().Id + "\" class=\"one_of\" style=\"z-index: 100; top:320px; left: 870px; border: 1px solid #a0a0a0; display: none; position: absolute; width: 198px; height: 250px; background-color: #f1f1f1\" >";
                mealAddToDiary.Text += "<div class=\"quicktools_copyto_header\">Copy to which meal?</div>";
                mealAddToDiary.Text += "<div class=\"quicktools_option\"><select  id=\"meal_" + _meals.First().Id + "_time\"><option value=\"1\">Breakfast</option><option value=\"2\">Morning Tea</option><option value=\"3\">Lunch</option><option value=\"4\">Afternoon Tea</option><option value=\"5\">Dinner</option><option value=\"6\">Supper</option></select></div>";
                mealAddToDiary.Text += "<div class=\"quicktools_copyto_header\">Copy to which date?</div>";
                mealAddToDiary.Text += CopyToDates_Meal(_meals.First().Id);
                mealAddToDiary.Text += "</div>";

                cvdc.Dispose();
            }
            if (Request.QueryString["tab"].Equals("new_meal"))
            {
                singlePortionTitle.Visible = true;
                lnkEditPortion.Visible = false;
                mealNameDiv.InnerHtml =
                    "<input type=\"text\" id=\"mealName\" style=\"width: 200px; margin-top: 10px; height: 21px; border: 1px solid #999999; float: left;padding-left:5px;\" onblur=\"meal_editName(this.value.replace('&','%26'));\" placeholder=\"Meal Name\" value=\"initval123\"/>" +
                    "<img src=\"/images/savetick.jpg\" width=\"20\" style=\"cursor:pointer;margin-left:10px;\" onclick=\"editMealNameBackLabel($('#mealName').val().replace('&','%26'));\" alt=\"Save Image\" title=\"Save Meal Name\"/>";

                string s = "<script type=\"text/javascript\">" +
                             "$( document ).ready(function() {$('#mealName').focus();" +
                               "});" +
                             "</script>";

                Page.ClientScript.RegisterStartupScript(GetType(), "test", s);

            }
        }

        private void ShareDivOnclickDefiner(int mealid)
        {
            shareToFB.Attributes["onclick"] = "hide_one_of();ShareMenuOrMeal(3,-1," + mealid + ",'facebook'); return  false;";
            shareToHQ.Attributes["onclick"] = "hide_one_of();ShareMenuOrMeal(3,-1," + mealid + ",'VVTHQ'); return  false;";
            shareToEmail.Attributes["onclick"] = "hide_one_of();enterEmailToShare(\"ShareMenuOrMeal(3,-1," + mealid + ",'email')\"); return  false;";

            if(Session["Trainer"].Equals("No"))
            {
                //from client to trainer
                shareToTrainerOrClient.Attributes["onclick"] = "hide_one_of();ShareMenuOrMeal(3,-1," + mealid + ",'trainer'); return  false;";
            }
            else
            {
                //from trainer to client
                shareToTrainerOrClient.Attributes["onclick"] = "hide_one_of();ShareMenuOrMeal(3,-1," + mealid + ",'clients'); return  false;";
                shareToTrainerOrClient.InnerHtml = "<img src='/images/trainer.small.icon.jpg'/>&nbsp;&nbsp;&nbsp;My Clients";
            }

        }

        private string ServeUnit(MealItem mealItem)
        {
            string returnValue = "";
            ClubVisionDataContext cvdc = new ClubVisionDataContext();

            if (mealItem.FromItemsAdd == true)
            {
                returnValue = mealItem.ItemAdd.ServeUnit;
                var items = (from item in cvdc.ItemAdds
                             where item.Name == mealItem.Item.Name
                             where item.CustomerId == (int)Session["MemberNo"]
                             //where item.ItemCategories.First().CategoryId == mealItem.Item.ItemCategories.First().CategoryId
                             select item);

                if (items.Count() > 1)
                {
                    returnValue = "<select class=\"serve_unit\" onchange=\"meal_updateServe($(this));\">";
                    foreach (ItemAdd item in items)
                    {
                        returnValue += "<option ";
                        if (item.Id == mealItem.Item.Id)
                        {
                            returnValue += "selected ";
                        }
                        returnValue += "data-id=\"" + item.Id + "\" data-serve-amount=\"" + item.ServeAmount + "\"  data-serve-unit=\"" + item.ServeUnit + "\" data-carbohydrate=\"" + item.Carbohydrate + "\" data-protein=\"" + item.Protein + "\" data-fat=\"" + item.Fat + "\">" + item.ServeUnit + "</option>";
                    }
                    returnValue += "</select>";
                }
            }
            else
            {
                returnValue = mealItem.Item.ServeUnit;
                var items = (from item in cvdc.Items
                             where item.Name == mealItem.Item.Name
                             where item.ItemCategories.First().CategoryId == mealItem.Item.ItemCategories.First().CategoryId
                             select item);

                if (items.Count() > 1)
                {
                    returnValue = "<select class=\"serve_unit\" onchange=\"meal_updateServe($(this));\">";
                    foreach (Item item in items)
                    {
                        returnValue += "<option ";
                        if (item.Id == mealItem.Item.Id)
                        {
                            returnValue += "selected ";
                        }
                        returnValue += "data-id=\"" + item.Id + "\" data-serve-amount=\"" + item.ServeAmount + "\"  data-serve-unit=\"" + item.ServeUnit + "\" data-carbohydrate=\"" + item.Carbohydrate + "\" data-protein=\"" + item.Protein + "\" data-fat=\"" + item.Fat + "\">" + item.ServeUnit + "</option>";
                    }
                    returnValue += "</select>";
                }
            }
            

            return returnValue;
        }

        private string ServeUnit(MenuItem menuItem, int mealTimeId)
        {
            string returnValue = "";

            ClubVisionDataContext cvdc = new ClubVisionDataContext();

            if (menuItem.FromItemsAdd == true)
            {
                returnValue = menuItem.ItemAdd.ServeUnit;

                var items = (from item in cvdc.ItemAdds
                             where item.Name == menuItem.Item.Name
                             where item.CustomerId == (int)Session["MemberNo"]
                             //where item.ItemCategories.First().CategoryId == mealItem.Item.ItemCategories.First().CategoryId
                             select item);


                if (items.Count() > 1)
                {
                    returnValue = "<select class=\"serve_unit\" onchange=\"updateServe($(this)," + mealTimeId + ");\">";
                    foreach (ItemAdd item in items)
                    {
                        returnValue += "<option ";
                        if (item.Id == menuItem.ItemAdd.Id)
                        {
                            returnValue += "selected ";
                        }
                        returnValue += "data-id=\"" + item.Id + "\" data-serve-amount=\"" + item.ServeAmount + "\"  data-serve-unit=\"" + item.ServeUnit + "\" data-carbohydrate=\"" + item.Carbohydrate + "\" data-protein=\"" + item.Protein + "\" data-fat=\"" + item.Fat + "\">" + item.ServeUnit + "</option>";
                    }
                    returnValue += "</select>";
                }

            }
            else
            {
                returnValue = menuItem.Item.ServeUnit;

                var items = (from item in cvdc.Items
                         where item.Name == menuItem.Item.Name
                         where item.ItemCategories.First().CategoryId == menuItem.Item.ItemCategories.First().CategoryId
                         select item);

                if (items.Count() > 1)
                {
                    returnValue = "<select class=\"serve_unit\" onchange=\"menu_updateServe($(this)," + mealTimeId + ");\">";
                    foreach (Item item in items)
                    {
                        returnValue += "<option ";
                        if (item.Id == menuItem.Item.Id)
                        {
                            returnValue += "selected ";
                        }
                        returnValue += "data-id=\"" + item.Id + "\" data-serve-amount=\"" + item.ServeAmount + "\"  data-serve-unit=\"" + item.ServeUnit + "\" data-carbohydrate=\"" + item.Carbohydrate + "\" data-protein=\"" + item.Protein + "\" data-fat=\"" + item.Fat + "\">" + item.ServeUnit + "</option>";
                    }
                    returnValue += "</select>";
                }
            }
            
            return returnValue;
        }

        protected string CopyToDates(int menuId)
        {
            string returnValue = "";

            returnValue = "<div>" +
                          "<input type=\"text\" placeholder=\"dd/mm/yyyy\" class=\"datepicker\" onchange=\"CopyMenuToDate(" + menuId + ", $(this).val() )\" />" +
                          "</div>";
            
            //for (int offset = 0; offset <= 6; offset++)
            //{
                // ToDo: Tomorrow, Today, Yesterday
               // returnValue += "<div class=\"quicktools_option\"><a onclick=\"CopyMenuToDate(" + menuId + ", '" + System.DateTime.Today.AddDays(offset).ToString("dd/MM/yyyy") + "');\">" + System.DateTime.Today.AddDays(offset).ToLongDateString() + "</a></div>";
            //}

            return returnValue;
        }

        protected string CopyToDates_Meal(int mealId)
        {
            string returnValue = "";

            returnValue = "<div>" +
                         "<input type=\"text\" placeholder=\"dd/mm/yyyy\" class=\"datepicker\" onchange=\"copyMealToDate($('#meal_" + mealId + "_time').val()," + mealId + ", $(this).val() )\" />" +
                         "</div>";

           // for (int offset = 0; offset <= 6; offset++)
            //{
            //    // ToDo: Tomorrow, Today, Yesterday
            //    returnValue += "<div class=\"quicktools_option\"><a onclick=\"copyMealToDate($('#meal_" + mealId + "_time').val()," + mealId + ", '" + System.DateTime.Today.AddDays(offset).ToString("dd/MM/yyyy") + "');\">" + System.DateTime.Today.AddDays(offset).ToLongDateString() + "</a></div>";
           // }

            return returnValue;
        }

        protected Literal MyLiteral(string name)
        {
            var myLiteralNew = this.FindControl(name) as Literal;
            return myLiteralNew;
        }

        protected void UploadMealImage(object sender, EventArgs e)
        {
            try
            {
                if (mealImageFileUpload.HasFile)
                {
                    string rawurl = Request.RawUrl.Replace("&msg=photofailed", "");

                    if((mealImageFileUpload.PostedFile.ContentType.Equals("image/jpeg") ||
                        mealImageFileUpload.PostedFile.ContentType.Equals("image/jpg") ||
                        mealImageFileUpload.PostedFile.ContentType.Equals("image/png") ||
                        mealImageFileUpload.PostedFile.ContentType.Equals("image/gif")) &&
                        mealImageFileUpload.PostedFile.ContentLength < 5243000)
                    {
                        string fileName = currentMealId.Value + Path.GetExtension(mealImageFileUpload.FileName);

                        ClubVisionDataContext cvdc = new ClubVisionDataContext();

                        var meal = (from ml in cvdc.Meals
                                    where ml.Id == Convert.ToInt32(currentMealId.Value)
                                    select ml).SingleOrDefault();
                       
                        if (meal != null && (SaveImgAndResize(fileName) 
                            && CheckUploadEligibility((int) meal.CustomerId, (int)Session["MemberNo"])))
                        {
                            meal.ImageUrl = "customer_meals/" + Session["MemberNo"] + "/" + currentMealId.Value + Path.GetExtension(mealImageFileUpload.FileName);

                            cvdc.SubmitChanges();

                            Response.Redirect(rawurl);
                        }
                        else
                        {
                            Response.Redirect(rawurl + "&msg=photofailed");
                        }
                    }
                    else
                    {
                        Response.Redirect(rawurl + "&msg=photofailed");
                    }
                    
                }
            }
            catch
            {
                throw;
            }
        }

        protected void UploadMealRecipe(object sender, EventArgs e)
        {
            try
            {
                if (mealRecipeFileUpload.HasFile)
                {
                    string rawurl = Request.RawUrl.Replace("&msg=photofailed", "");

                    if ((mealRecipeFileUpload.PostedFile.ContentType.Equals("application/pdf") ||
                        mealRecipeFileUpload.PostedFile.ContentType.Equals("application/msword") ||
                        mealRecipeFileUpload.PostedFile.ContentType.Equals("application/vnd.openxmlformats-officedocument.wordprocessingml.document") ||
                        mealRecipeFileUpload.PostedFile.ContentType.Equals("text/richtext") ||
                        mealRecipeFileUpload.PostedFile.ContentType.Equals("text/plain")) &&
                        mealRecipeFileUpload.PostedFile.ContentLength < 102400)
                    {
                        string fileName = currentMealId.Value + Path.GetExtension(mealRecipeFileUpload.FileName);

                        ClubVisionDataContext cvdc = new ClubVisionDataContext();

                        var meal = (from ml in cvdc.Meals
                                    where ml.Id == Convert.ToInt32(currentMealId.Value)
                                    select ml).SingleOrDefault();

                        if (meal != null && (SaveRecipe(fileName)
                            && CheckUploadEligibility((int)meal.CustomerId, (int)Session["MemberNo"])))
                        {
                            

                            meal.PDFAddress = "customer_recipes/" + Session["MemberNo"] + "/" + currentMealId.Value + Path.GetExtension(mealRecipeFileUpload.FileName);

                            cvdc.SubmitChanges();

                            Response.Redirect(rawurl);
                        }
                        else
                        {
                            Response.Redirect(rawurl + "&msg=recipefailed");
                        }
                    }
                    else
                    {
                        Response.Redirect(rawurl + "&msg=recipefailed");
                    }

                }
            }
            catch
            {
                throw;
            }
        }

        protected bool SaveRecipe(string filename)
        {
            try
            {
                // Find the fileUpload control
                string path = "/club-vision/recipe/customer_recipes/" + Session["MemberNo"] + "/";

                // Check if the directory we want the image uploaded to actually exists or not
                if (!Directory.Exists(Server.MapPath(path)))
                {
                    Directory.CreateDirectory(Server.MapPath(path));
                }

                // Specify the upload directory
               // string directory = Server.MapPath(path);

                mealRecipeFileUpload.SaveAs(Server.MapPath(path + filename));

                mealRecipeFileUpload.Dispose();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        protected bool SaveImgAndResize(string filename)
        {
            try
            {
                // Find the fileUpload control
                //string filename = mealImageFileUpload.FileName;

                // Find the fileUpload control
                string path = "/images/meals/customer_meals/" + Session["MemberNo"] + "/";

                // Check if the directory we want the image uploaded to actually exists or not
                if (!Directory.Exists(Server.MapPath(path)))
                {
                    Directory.CreateDirectory(Server.MapPath(path));
                }

                // Specify the upload directory
                string directory = Server.MapPath(path);

                // Create a bitmap of the content of the fileUpload control in memory
                Bitmap originalBMP = new Bitmap(mealImageFileUpload.FileContent);

                Image saveCroppedImage = SaveCroppedImage(originalBMP, 250, 250);
                //Image resizeImageDistort = ResizeImageDistort(originalBMP, 250, true);
                
                // Save the new graphic file to the server
                saveCroppedImage.Save(directory + filename);
                //resizeImageDistort.Save(directory + "resizeImageDistort" + filename);

                // Deallocate them once finish
                originalBMP.Dispose();
                saveCroppedImage.Dispose();
                saveCroppedImage.Dispose();
                
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
            
        }

        protected bool CheckUploadEligibility(int mealcustid, int custid)
        {
            return true;
        }

        //Image resize and stretch
        protected static Image ResizeImageDistort(Bitmap originalBMP, int size, bool preserveAspectRatio = true)
        {
            // Calculate the new image dimensions
            int origWidth = originalBMP.Width;
            int origHeight = originalBMP.Height;
            int sngRatio = origWidth / origHeight;
            int newWidth = size;
            int newHeight = newWidth / sngRatio;

            // Create a new bitmap which will hold the previous resized bitmap
            Bitmap newBMP = new Bitmap(originalBMP, newWidth, newHeight);

            // Create a graphic based on the new bitmap
            Graphics oGraphics = Graphics.FromImage(newBMP);

            // Set the properties for the new graphic file
            oGraphics.SmoothingMode = SmoothingMode.AntiAlias; oGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // Draw the new graphic based on the resized bitmap
            oGraphics.DrawImage(originalBMP, 0, 0, newWidth, newHeight);

            return newBMP;
        }

        //Image resize maintain ratio with background
        protected static Image FixedSize(Image imgPhoto, int Width, int Height)
        {
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)Width / (float)sourceWidth);
            nPercentH = ((float)Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = System.Convert.ToInt16((Width -
                              (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = System.Convert.ToInt16((Height -
                              (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(Width, Height,
                              PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                             imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.Red);
            grPhoto.InterpolationMode =
                    InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }
        
        //Hard resize attempts to resize as close as it can to the desired size and then crops the excess
        protected static Image HardResizeImageLowQuality(int Width, int Height, Image Image)
        {
            int width = Image.Width;
            int height = Image.Height;
            Image resized = null;
            if (Width > Height)
            {
                resized = ResizeImage(Width, Width, Image);
            }
            else
            {
                resized = ResizeImage(Height, Height, Image);
            }
            Image output = CropImage(resized, Height, Width);
            //return the original resized image
            return output;
        }

        //Overload for crop that default starts top left of the image.
        public static Image CropImage(System.Drawing.Image Image, int Height, int Width)
        {
            return CropImage(Image, Height, Width, 0, 0);
        }

        //The crop image sub
        public static Image CropImage(Image Image, int Height, int Width, int StartAtX, int StartAtY)
        {
            Image outimage;
            MemoryStream mm = null;
            try
            {
                //check the image height against our desired image height
                if (Image.Height < Height)
                {
                    Height = Image.Height;
                }

                if (Image.Width < Width)
                {
                    Width = Image.Width;
                }

                //create a bitmap window for cropping
                Bitmap bmPhoto = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
                bmPhoto.SetResolution(72, 72);

                //create a new graphics object from our image and set properties
                Graphics grPhoto = Graphics.FromImage(bmPhoto);
                grPhoto.SmoothingMode = SmoothingMode.AntiAlias;
                grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
                grPhoto.PixelOffsetMode = PixelOffsetMode.HighQuality;

                //now do the crop
                grPhoto.DrawImage(Image, new Rectangle(0, 0, Width, Height), StartAtX, StartAtY, Width, Height, GraphicsUnit.Pixel);

                // Save out to memory and get an image from it to send back out the method.
                mm = new MemoryStream();
                bmPhoto.Save(mm, System.Drawing.Imaging.ImageFormat.Jpeg);
                Image.Dispose();
                bmPhoto.Dispose();
                grPhoto.Dispose();
                outimage = Image.FromStream(mm);

                return outimage;
            }
            catch (Exception ex)
            {
                throw new Exception("Error cropping image, the error was: " + ex.Message);
            }
        }

        //Image resizing
        protected static Image ResizeImage(int maxWidth, int maxHeight, Image image)
        {
            int width = image.Width;
            int height = image.Height;
            if (width > maxWidth || height > maxHeight)
            {
                //The flips are in here to prevent any embedded image thumbnails -- usually from cameras
                //from displaying as the thumbnail image later, in other words, we want a clean
                //resize, not a grainy one.
                image.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipX);
                image.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipX);

                float ratio = 0;
                if (width > height)
                {
                    ratio = (float)width / (float)height;
                    width = maxWidth;
                    height = Convert.ToInt32(Math.Round((float)width / ratio));
                }
                else
                {
                    ratio = (float)height / (float)width;
                    height = maxHeight;
                    width = Convert.ToInt32(Math.Round((float)height / ratio));
                }

                //return the resized image
                return image.GetThumbnailImage(width, height, null, IntPtr.Zero);
            }
            //return the original resized image
            return image;
        }
        
        //Resize image and crop from center
        public Image SaveCroppedImage(Image image, int maxWidth, int maxHeight)
        {
            ImageCodecInfo jpgInfo = ImageCodecInfo.GetImageEncoders()
                                     .Where(codecInfo =>
                                     codecInfo.MimeType == "image/jpeg").First();
            Image finalImage = image;
            System.Drawing.Bitmap bitmap = null;
           
            int left = 0;
            int top = 0;
            int srcWidth = maxWidth;
            int srcHeight = maxHeight;
            bitmap = new System.Drawing.Bitmap(maxWidth, maxHeight);
            double croppedHeightToWidth = (double)maxHeight / maxWidth;
            double croppedWidthToHeight = (double)maxWidth / maxHeight;

            if (image.Width > image.Height)
            {
                srcWidth = (int)(Math.Round(image.Height * croppedWidthToHeight));
                if (srcWidth < image.Width)
                {
                    srcHeight = image.Height;
                    left = (image.Width - srcWidth) / 2;
                }
                else
                {
                    srcHeight = (int)Math.Round(image.Height * ((double)image.Width / srcWidth));
                    srcWidth = image.Width;
                    top = (image.Height - srcHeight) / 2;
                }
            }
            else
            {
                srcHeight = (int)(Math.Round(image.Width * croppedHeightToWidth));
                if (srcHeight < image.Height)
                {
                    srcWidth = image.Width;
                    top = (image.Height - srcHeight) / 2;
                }
                else
                {
                    srcWidth = (int)Math.Round(image.Width * ((double)image.Height / srcHeight));
                    srcHeight = image.Height;
                    left = (image.Width - srcWidth) / 2;
                }
            }
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(image, new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                new Rectangle(left, top, srcWidth, srcHeight), GraphicsUnit.Pixel);
            }
            return finalImage = bitmap;
            
            /*
            try
            {
                using (EncoderParameters encParams = new EncoderParameters(1))
                {
                    encParams.Param[0] = new EncoderParameter(Encoder.Quality, (long)100);
                    //quality should be in the range 
                    //[0..100] .. 100 for max, 0 for min (0 best compression)
                    finalImage.Save(filePath, jpgInfo, encParams);
                    return true;
                }
            }
            catch { }
            if (bitmap != null)
            {
                bitmap.Dispose();
            }
            return false;
             * */
        }  

    }
}