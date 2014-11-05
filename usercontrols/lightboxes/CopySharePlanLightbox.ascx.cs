using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VisionPersonalTrainingProject.usercontrols.lightboxes
{
    public partial class CopySharePlan : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if(Request.QueryString["code"].Substring(0,2).Equals("ml"))
                {
                    mealtimerow.Visible = true;
                }
            }
        }
        
        protected override void Render(HtmlTextWriter writer)
        {
            string validatorOverrideScripts = "<script src=\"/scripts/validators.js\" type=\"text/javascript\"></script>";
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ValidatorOverrideScripts", validatorOverrideScripts, false);
            base.Render(writer);
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            int memberNo = GetLoginId(username.Text, password.Text);

            if (memberNo > 0)
            {
                VisionPersonalTrainingProject.ClubVisionDataContext cvdc =
                          new VisionPersonalTrainingProject.ClubVisionDataContext();
                
                if (!Request.QueryString["code"].Substring(0, 2).Equals("ml"))
                {

                    cvdc.CopyShareEntryMenuDPToDiaryEntry(memberNo, bdpDay.SelectedDate,
                                                            Request.QueryString["code"]);
                }
                else
                {
                    cvdc.CopyShareEntryMealToDiaryEntry(memberNo, bdpDay.SelectedDate,
                                                        Request.QueryString["code"],
                                                        Convert.ToInt32(DropDownList1.SelectedValue));
                }

                var sharedet = (from sd in cvdc.ShareDetails
                                where sd.cCode == Request.QueryString["code"]
                                select sd).SingleOrDefault();
                if (sharedet != null) sharedet.iCopied++;

                cvdc.SubmitChanges();
    

                string s = "<script type=\"text/javascript\">" +
                                 "saveShareAsFoodDiaryConfirmation('" + username.Text + "', '" + password.Text + "','" + bdpDay.SelectedDate + "');</script>";

                Page.ClientScript.RegisterStartupScript(GetType(), "test", s);
                

            }else
            {
                NotAMember();
            }
            
        }

        protected void ImageButton_copyToMealdiv_Click(object sender, ImageClickEventArgs e)
        {
            int memberNo = GetLoginId(usernameToMeal.Text, passwordToMeal.Text);

            if (memberNo > 0)
            {
                VisionPersonalTrainingProject.ClubVisionDataContext cvdc =
                          new VisionPersonalTrainingProject.ClubVisionDataContext();

                cvdc.CopyShareEntryToMeal(memberNo, Request.QueryString["code"], mealName.Text);
                
                var sharedet = (from sd in cvdc.ShareDetails
                                where sd.cCode == Request.QueryString["code"]
                                select sd).SingleOrDefault();

                if (sharedet != null) sharedet.iCopied++;
                
                cvdc.SubmitChanges();
                string s = "<script type=\"text/javascript\">" +
                                     "saveShareAsMealConfirmation('" + usernameToMeal.Text + "','" + passwordToMeal.Text + "');</script>";

                Page.ClientScript.RegisterStartupScript(GetType(), "test", s);
                
            }
            else
            {
                NotAMember();
            }

        }

        protected void ImageButton_copyToMenudiv_Click(object sender, ImageClickEventArgs e)
        {
            int memberNo = GetLoginId(usernameToMenu.Text, passwordToMenu.Text);

            if (memberNo > 0)
            {
                VisionPersonalTrainingProject.ClubVisionDataContext cvdc =
                         new VisionPersonalTrainingProject.ClubVisionDataContext();

                //this could be Daily Plan or Menu
                cvdc.CopyShareEntryToMenu(memberNo, menuName.Text, Request.QueryString["code"]);
                
                var sharedet = (from sd in cvdc.ShareDetails
                                where sd.cCode == Request.QueryString["code"]
                                select sd).SingleOrDefault();

                if (sharedet != null) sharedet.iCopied++;

                cvdc.SubmitChanges();

                string s = "<script type=\"text/javascript\">" +
                                     "saveShareAsMenuConfirmation('" + usernameToMenu.Text + "','" + passwordToMenu.Text + "');</script>";

                Page.ClientScript.RegisterStartupScript(GetType(), "test", s);
                
            }
            else
            {
                NotAMember();
            }

        }

        protected int GetLoginId(string username, string password)
        {
            try
            {
                VisionPersonalTrainingProject.VOSWebService.Service service = new VisionPersonalTrainingProject.VOSWebService.Service();
                service.AuthenticationHeaderValue = new VisionPersonalTrainingProject.VOSWebService.AuthenticationHeader();
                service.AuthenticationHeaderValue.UserName = "vosABC";
                service.AuthenticationHeaderValue.Password = "vosABCpass1";
                string login = username;
                string pwd = password;
                VisionPersonalTrainingProject.VOSWebService.MemberLogin member = service.GetMemberLogin(login, ref pwd);

                if (member.MemberNo != -1) // this should be member.Login
                {
                    return member.MemberNo;
                }

                //ExternalClubVisionProject.ClubVisionExternalDataContext cvedc = new ExternalClubVisionProject.ClubVisionExternalDataContext();
                ClubVisionDataContext cvdc = new ClubVisionDataContext();

                var customers = (from cu in cvdc.Customer_Externals
                                 where cu.cLoginName == username
                                 where cu.cPassword == password
                                 select cu).SingleOrDefault();

                if (customers != null && DateTime.Compare(customers.dDateTerminate, DateTime.Now) > 0)
                {
                    return customers.iID;
                }
                return -1;
            }
            catch (Exception ex)
            {

                return -1;
            }
        }

        protected void NotAMember()
        {
            const string s = "<script type=\"text/javascript\">" +
                                "saveShareNotMemberConfirmation();</script>";

            Page.ClientScript.RegisterStartupScript(GetType(), "test", s);
        }

        
    }
}