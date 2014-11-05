using System;
using System.Linq;

namespace VisionPersonalTrainingProject.usercontrols.clubvision
{
    public partial class WhatsOnView : System.Web.UI.UserControl
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
                var groupExercise = service.GetWhatsOn((int) customerStudio, 1);
                var nutritionalSeminar = service.GetWhatsOn((int) customerStudio, 2);
                var shoppingTour = service.GetWhatsOn((int) customerStudio, 3);
                var events = service.GetWhatsOn((int) customerStudio, 4);

                try
                {
                    for (var i = groupExercise.Rows.Count - 1; i > 1; i--)
                    {
                        groupExercise.Rows.RemoveAt(i);
                    }
                    for (var i = events.Rows.Count-1; i > 0 ; i--)
                    {
                        events.Rows.RemoveAt(i);
                    }
                    for (var i = nutritionalSeminar.Rows.Count - 1 ; i > 0 ; i--)
                    {
                        nutritionalSeminar.Rows.RemoveAt(i);
                    }
                    for (var i = shoppingTour.Rows.Count - 1; i > 0 ; i--)
                    {
                        shoppingTour.Rows.RemoveAt(i);
                    }
                   
                }
                catch (IndexOutOfRangeException exception){}
                
                GridViewGroupExercise.DataSource = groupExercise;
                GridViewSeminar.DataSource = nutritionalSeminar;
                GridViewShoppingTour.DataSource = shoppingTour;
                GridViewEvents.DataSource = events;
            }


            GridViewGroupExercise.DataBind();
            GridViewSeminar.DataBind();
            GridViewShoppingTour.DataBind();
            GridViewEvents.DataBind();
       }

        
    }
}