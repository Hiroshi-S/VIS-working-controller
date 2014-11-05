using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VisionPersonalTrainingProject.usercontrols.general
{
    public partial class DailyInspiration : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            setArticle();
        }
        protected void setArticle()
        {
            try
            {
                using (ClubVisionDataContext db = new ClubVisionDataContext())
                {
                    Random random = new Random();

                    var articleNum = (from a in db.DailyInspirations
                                      select a.dID).Count();

                    var article = (from ab in db.DailyInspirations
                                   where ab.dID == random.Next(1, articleNum)
                                   select ab.inspiration_text).FirstOrDefault();
                    if(article != null)
                    {
                        ArticleLiteral.Text = article;  
                    }
                }
            }
            catch (Exception e)
            { Response.Write(e.ToString()); }
        }
    }
}