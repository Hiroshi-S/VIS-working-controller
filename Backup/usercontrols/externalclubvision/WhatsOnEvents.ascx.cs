using System;
using System.Text.RegularExpressions;

namespace VisionPersonalTrainingProject.usercontrols.externalclubvision
{
    public partial class WhatsOnEvents : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ClubVisionDataContext cvdc = new ClubVisionDataContext();
            
            VOSWebService.Service service = new VOSWebService.Service();
            service.AuthenticationHeaderValue = new VOSWebService.AuthenticationHeader();
            service.AuthenticationHeaderValue.UserName = "vosABC";
            service.AuthenticationHeaderValue.Password = "vosABCpass1";

            var events = service.GetWhatsOn(-1, 4);
            GridViewEvents.DataSource = events;

            GridViewEvents.DataBind();
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