using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;


namespace VisionPersonalTrainingProject.usercontrols.clubvision
{
    public partial class ForumsView : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(GetConnectionString("yafnet"));

            System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand("SELECT TOP 3 yaf_Message.TopicID, [Topic], [Message] FROM yaf_Message INNER JOIN yaf_Topic ON yaf_Message.TopicID = yaf_Topic.TopicId WHERE yaf_Message.IsDeleted = 0 AND yaf_Topic.IsDeleted = 0 ORDER BY yaf_Message.Posted DESC", conn);

            conn.Open();
            System.Data.SqlClient.SqlDataReader reader = command.ExecuteReader();

            literalForumView.Text = "";

            while (reader.Read())
            {
                literalForumView.Text += "<h5><a href=\"/yaf/forum.aspx?g=posts&t=" + reader.GetInt32(0).ToString() + "\">" + reader.GetString(1) + "</a></h5>";

                string regex = "(\\[.*\\])";

                string displayString = Regex.Replace(reader.GetString(2), regex, "");

                try
                {
                    displayString = displayString.Remove(90);
                    displayString = displayString.Remove(displayString.LastIndexOf(" "));
                    displayString = displayString + " ...";
                }
                catch
                {
                }

                literalForumView.Text += "<p>" + displayString + "</p>";
                literalForumView.Text += "<div class=\"eForumsViewDivider\">&nbsp;</div>";
            }

            literalForumView.Text += "<h5 style=\"float:right; padding-right: 14px\"><a href=\"/yaf/forum.aspx?g=forum\">View more &gt;</a></h5>";
            conn.Close();
        }

        private string GetConnectionString(string _connectionStringsName)
        {
            System.Configuration.ConnectionStringSettingsCollection config = System.Configuration.ConfigurationManager.ConnectionStrings;
            for (int i = 0; i < config.Count; i++)
            {
                if (config[i].Name.Equals(_connectionStringsName, StringComparison.OrdinalIgnoreCase))
                    return config[i].ToString();
            }
            return String.Empty;
        }
    }
}