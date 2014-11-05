using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VisionPersonalTrainingProject.usercontrols.general
{
    public partial class MonthlyCampaign : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                DateTime daytoday = DateTime.Today;

                DateTime endOfMonth = new DateTime(daytoday.Year, daytoday.Month, 
                                                DateTime.DaysInMonth(daytoday.Year, daytoday.Month));

                string note = "endofmonth-" + endOfMonth.ToString("dd-MM-yyyy") + " Not the end of the month";

                if (daytoday.Day >= endOfMonth.Day)
                {
                    //here must be 2 days before the end of month 
                    //change the next month promo
                    daytoday = daytoday.AddDays(7);
                    note = "endofmonth-" + endOfMonth.ToString("dd-MM-yyyy") + " This is the end of the month";
                 }
                
                int year = daytoday.Year;
                string monthm = daytoday.ToString("MMM");

                string litpromo = "<p><img style=\"padding-left: 40px;\" data-note=\"" + note + "\" src=\"/images/monthly_campaign/" + year + "/" + monthm + "-" + year + ".jpg\" " +
                                   "alt=\"campaign promo\" width=\"900\" /></p>";
                
                Literal1.Text = litpromo;
            }
        }
    }
}