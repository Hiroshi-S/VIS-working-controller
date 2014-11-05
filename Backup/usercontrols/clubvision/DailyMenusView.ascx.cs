using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VisionPersonalTrainingProject.usercontrols.clubvision
{
    public partial class DailyMenusView : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                ClubVisionDataContext cvdc = new ClubVisionDataContext();

                var _menus = (from menus in cvdc.Menus
                              where menus.CustomerId == (int)Session["MemberNo"]
                              orderby menus.Created descending
                              select menus);

                foreach (Menu menu in _menus)
                {
                    literalMenus.Text += "<li class=\"menu\">";
                    literalMenus.Text += "<a href=\"/club-vision/my-eating/menus/?tab=view&menuId=" + menu.Id + "\">";
                    if (menu.ImageUrl != null)
                    {
                        literalMenus.Text += "<img class=\"menu_image\" src=\"/images/menus/" + menu.ImageUrl.Replace("meal_generic", "menu_generic") + "\" style=\"height: 64px;left: -4px;position: relative;top: 8px;width: 64px;\" />";
                        // TODO: fix: http://selbie.wordpress.com/2011/01/23/scale-crop-and-center-an-image-with-correct-aspect-ratio-in-html-and-javascript/
                    }
                    literalMenus.Text += "<p class=\"title\">" + menu.Name + "</p></a>";
                    literalMenus.Text += "</li>";
                }
            }
        }
    }
}