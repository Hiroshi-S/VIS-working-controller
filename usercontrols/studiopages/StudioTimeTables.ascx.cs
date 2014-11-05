using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using umbraco.NodeFactory;

namespace VisionPersonalTrainingProject.usercontrols.studiopages
{
    public partial class StudioTimeTables : System.Web.UI.UserControl
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
          /*  GridView1.DataSource = groupExercise;
            GridView1.DataBind();
           * 
           * */ var events = service.GetWhatsOn((int)Session["studioID"], 4);
            GridView4.DataSource = events;
            GridView4.DataBind();

            var eseminar = service.GetWhatsOn((int)Session["studioID"], 2);
            GridView2.DataSource = eseminar;
            GridView2.DataBind();
 
            var eshoppingtour = service.GetWhatsOn((int)Session["studioID"], 3);
            GridView3.DataSource = eshoppingtour;
            GridView3.DataBind();

            IEnumerable<DataRow> dtRowGroupExercise = groupExercise.AsEnumerable();

            string[,] exerciseTable = new string[6,10];

            int day = 0;
            int count = 0;
            int initialiseDay = 0;
            string dtgeStr = "<table class=\"timetableStud table\">" +
                            "<tr><td>Mon</td><td>Tue</td><td>Wed</td><td>Thurs</td><td>Fri</td><td>Sat</td></tr>";


            foreach (var dtge in dtRowGroupExercise)
            {
                if (dtge["WeekDay"].ToString() != day + "")
                {
                    day = Convert.ToInt32(dtge["WeekDay"]);
                    count = 0;
                }

                exerciseTable[day - 1, count] = "<td><img src=\"/images/vgt_group_classes/VPT " + dtge["WhatsOn"] + ".jpg\" alt=\"group class\"/><br/>" +
                                                dtge["Time"] + " " + dtge["WhenAMPM"] + "<br/>" + dtge["WhatsOn"] + "<br/>" + dtge["Duration"] + "</td>";//+ "<br/>60 Minutes</td>";
                count++;
            }

            for (int j = 0; j < exerciseTable.GetLength(1); j++) 
            {
                string rowdtgeStr = "<tr>";
                for (int i = 0; i < exerciseTable.GetLength(0); i++)
                {
                    string s = exerciseTable[i, j];
                    rowdtgeStr += s;
                    if(string.IsNullOrEmpty(s))
                    {
                        rowdtgeStr += "<td></td>";
                    }
                }
                rowdtgeStr += "</tr>";

                if(rowdtgeStr.Contains("M"))
                {
                    dtgeStr += rowdtgeStr;
                }
            }

            //dtgeStr += "</tr></table>";
            dtgeStr += "</table>";

            Literal1.Text = dtgeStr;

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