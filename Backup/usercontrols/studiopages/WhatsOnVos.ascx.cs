using System;
using System.Linq;
using System.Text.RegularExpressions;
using umbraco.NodeFactory;

namespace VisionPersonalTrainingProject.usercontrols.studiopages
{
    public partial class WhatsOnVos : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Node currentNode = Node.GetCurrent();

            String myValue = currentNode.GetProperty("studioId").Value;

            Session["studioID"] = Convert.ToInt32(myValue);

            VOSWebService.Service service = new VOSWebService.Service();
            service.AuthenticationHeaderValue = new VOSWebService.AuthenticationHeader();
            service.AuthenticationHeaderValue.UserName = "vosABC";
            service.AuthenticationHeaderValue.Password = "vosABCpass1";

            var groupExercise = service.GetWhatsOn((int)Session["studioID"], 1);
            GridView1.DataSource = groupExercise;
            GridView1.DataBind();

            var eseminar = service.GetWhatsOn((int)Session["studioID"], 2);
            GridView2.DataSource = eseminar;
            GridView2.DataBind();

            var eshoppingtour = service.GetWhatsOn((int)Session["studioID"], 3);
            GridView3.DataSource = eshoppingtour;
            GridView3.DataBind();

            var events = service.GetWhatsOn((int)Session["studioID"], 4);
            GridView4.DataSource = events;
            GridView4.DataBind();

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