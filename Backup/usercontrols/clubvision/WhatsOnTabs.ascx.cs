using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using umbraco.NodeFactory;

namespace VisionPersonalTrainingProject.usercontrols.clubvision
{
    public partial class WhatsOnTabs : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int memberId = (int)Session["MemberNo"];

            ClubVisionDataContext cvdc = new ClubVisionDataContext();

            var customerStudio = (from cust in cvdc.Customers
                                  where cust.Id == memberId
                                  select cust.StudioId).SingleOrDefault();


            VOSWebService.Service service = new VOSWebService.Service();
            service.AuthenticationHeaderValue = new VOSWebService.AuthenticationHeader();
            service.AuthenticationHeaderValue.UserName = "vosABC";
            service.AuthenticationHeaderValue.Password = "vosABCpass1";

            if (customerStudio != null)
            {
                var groupExercise = service.GetWhatsOn((int)customerStudio, 1);
                var nutritionalSeminar = service.GetWhatsOn((int)customerStudio, 2);
                var shoppingTour = service.GetWhatsOn((int)customerStudio, 3);
                var events = service.GetWhatsOn((int)customerStudio, 4);

               
                GridViewGroupExercise.DataSource = groupExercise;
                GridViewSeminar.DataSource = nutritionalSeminar;
                GridViewShoppingTour.DataSource = shoppingTour;
                GridViewEvents.DataSource = events;
            }
            else
            {
                var events = service.GetWhatsOn(-1, 4);
                GridViewEvents.DataSource = events;
            }


            GridViewGroupExercise.DataBind();
            GridViewSeminar.DataBind();
            GridViewShoppingTour.DataBind();
            GridViewEvents.DataBind();

        }

        protected string RenderDays(Object iWeekDay)
        {
            string rtn = "DefaultValue";

            if (!Convert.IsDBNull(iWeekDay))
            {
                int intValue = Convert.ToInt32(iWeekDay);

                switch (intValue)
                {
                    case 1:
                        rtn = "Monday";
                        break;
                    case 2:
                        rtn = "Tuesday";
                        break;
                    case 3:
                        rtn = "Wednesday";
                        break;
                    case 4:
                        rtn = "Thursday";
                        break;
                    case 5:
                        rtn = "Friday";
                        break;
                    case 6:
                        rtn = "Saturday";
                        break;
                    case 7:
                        rtn = "Sunday";
                        break;
                }
            }
            return rtn;
        }

        protected string RenderExercise(Object exss)
        {
            //break down of exercise sentence
            string exwords = Convert.ToString(exss);
            string[] lines = Regex.Split(exwords, " ");

            var str = new string[4];

            str[0] = lines[lines.Length - 1]; //ampm    
            str[1] = lines[lines.Length - 2]; //time
            str[2] = lines[lines.Length - 3]; //day

            for (int j = 0; j < (lines.Length - 3); j++)
            {
                str[3] += lines[j] + " ";//exercise
            }

            return str[3];
        }

        protected string RenderWhatsOnDate(Object wod)
        {
            //break down of exercise sentence
            string exwords = Convert.ToString(wod);
            string[] lines = Regex.Split(exwords, " ");

            var str = new string[3];

            str[0] = lines[lines.Length - 1]; //ampm    
            str[1] = lines[lines.Length - 2]; //time
            str[2] = lines[lines.Length - 3]; //day

            DateTime dt = Convert.ToDateTime(str[2]);

            string dateee = dt.ToLongDateString();

            return dateee;

        }
    }
}