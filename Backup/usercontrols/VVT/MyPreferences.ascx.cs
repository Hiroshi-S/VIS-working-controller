using System;
using System.Linq;

namespace VisionPersonalTrainingProject.usercontrols.VVT
{
    public partial class MyPreferences : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                LitMyPref.Text = GenerateHTML();
            }
        }

        protected string GenerateHTML()
        {
            ClubVisionDataContext cvdc = new ClubVisionDataContext();
            bool isVPTOnly = (string) Session["MemberType"] == "VPT";
            var custId = (int) Session["MemberNo"];

            string txt =
                "<div id=\"myPreferencesdiv\" style=\"margin: 0 auto; width: 930px; height: auto;display: block;\">" +
                "<h1>My Preferences</h1><br/>" +
                "<p>We want to support you in every way possible to ensure you successfully achieve your health and fitness goals.  Please review the list of additional support available to you and indicate which options would suit you best.  These reminders will be sent to you via push notifications on your phone so you will need to ensure that you have activated these for the Vision Virtual Training app.</p><br/><br/>";

            /*Nutrition preferences*/
            string nutpref = "";
            var nutritionPref = (from prf in cvdc.Preferences
                                 where prf.Type == 1 && prf.isActive
                                 select prf);

            if(!isVPTOnly)
            {
                nutritionPref = nutritionPref.Where(x => x.VPTonly == false);
            }

            foreach (Preference preference in nutritionPref)
            {
                var singleOrDefault = preference.CustomerPreferences.SingleOrDefault(x => x.CustomerId == custId);
                string onoff = "Off";
                if (singleOrDefault != null)
                {
                    onoff = singleOrDefault.isActive == false ? "On" : "Off";
                }

                nutpref += "<tr><td>" + preference.PreferenceStr + "</td>" +
                            "<td><div class=\"Switch " + onoff + "\" data-type=\"" + preference.Type + "\"" +
                            "data-id=\"" + preference.Id + "\" data-isactive=\"" + onoff + "\"><div class=\"Toggle\"></div><span class=\"On\">YES</span><span class=\"Off\">NO</span></div></td></tr>";
            }

            if(nutpref.Length > 0)
            {
                nutpref = "<h2>Nutrition Preferences</h2><table>" + nutpref + "</table><br/>";
            }

            /*Reminders*/
            string reminder = "";
            var reminders = (from prf in cvdc.Preferences
                                 where prf.Type == 2 && prf.isActive
                                 select prf);

            if (!isVPTOnly)
            {
                reminders = reminders.Where(x => x.VPTonly == false);
            }

            foreach (Preference preference in reminders)
            {
                var singleOrDefault = preference.CustomerPreferences.SingleOrDefault(x => x.CustomerId == custId);
                string onoff = "Off";
                if (singleOrDefault != null)
                {
                    onoff = singleOrDefault.isActive == false ? "On" : "Off";
                }

                reminder += "<tr><td>" + preference.PreferenceStr + "</td>" +
                            "<td><div class=\"Switch " + onoff + "\" data-type=\"" + preference.Type + "\"" +
                            "data-id=\"" + preference.Id + "\" data-isactive=\"" + onoff + "\"><div class=\"Toggle\"></div><span class=\"On\">YES</span><span class=\"Off\">NO</span></div></td></tr>";

            }

            if (reminder.Length > 0)
            {
                reminder = "<h2>I would like reminders</h2><table>" + reminder + "</table><br/>";
            }

            /*Notifications*/
            string notification = "";
            var notifs = (from prf in cvdc.Preferences
                             where prf.Type == 3 && prf.isActive
                             select prf);

            if (!isVPTOnly)
            {
                notifs = notifs.Where(x => x.VPTonly == false);
            }

            foreach (Preference preference in notifs)
            {
                var singleOrDefault = preference.CustomerPreferences.SingleOrDefault(x => x.CustomerId == custId);
                string onoff = "Off";
                if (singleOrDefault != null)
                {
                    onoff = singleOrDefault.isActive == false ? "On" : "Off";
                }

                notification += "<tr><td>" + preference.PreferenceStr + "</td>" +
                                "<td><div class=\"Switch " + onoff + "\" data-type=\"" + preference.Type + "\"" +
                                "data-id=\"" + preference.Id +"\" data-isactive=\"" + onoff + "\"><div class=\"Toggle\"></div><span class=\"On\">YES</span><span class=\"Off\">NO</span></div></td></tr>";

            }

            if (notification.Length > 0)
            {
                notification = "<h2>I would like to be notified when</h2><table>" + notification + "</table><br/>";
            }

            txt = txt + nutpref + reminder + notification + "</div>";

            return txt;
        }
    }
}