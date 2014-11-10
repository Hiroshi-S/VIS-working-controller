using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VisionPersonalTrainingProject.usercontrols.clubvision
{
    public partial class ReadySetGoQuestionnaire : System.Web.UI.UserControl
    {
        private int _totalScore = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;
            if (Request.QueryString["isection"] != null && Request.QueryString["itype"] != null)
            {
                littext.Text = QuestionnaireTemplate(Convert.ToInt32(Request.QueryString["isection"]), Convert.ToInt32(Request.QueryString["itype"]));
            }
            else
            {
                //littext.Text = GenerateQuestionnaireTitleList();
                littext.Text = GenerateQuestionnaireTitleList_AndrewReorder();
            }
        }

        private void sendEmailToTrainer(int studioId, string ToEmail, string Subject, string ccEmail, string MessageBody)
        {
            VisionPersonalTrainingProject.VPTFacilities mailObj = new VisionPersonalTrainingProject.VPTFacilities();
            mailObj.MailTrainerClient(studioId, ToEmail, ccEmail, Subject, MessageBody, false, true);
            mailObj = null;
        }

        protected string QuestionnaireTemplate(int sectionid, int quesType)
        {
            ClubVisionDataContext cvdc = new ClubVisionDataContext();
            string text = "";
            string js = "submit_questionnarie();";
            if (Session["MemberType"] == "VVT")
                js = "submit_questionnarie_ext();";

            var qDetails = (from qss in cvdc.FormDetails
                            where qss.iFormID == 3
                            where qss.iSectionID == sectionid
                            select qss).SingleOrDefault();

            var formResult = (from fres in cvdc.FormResults
                              where fres.iCustomerID == (int)Session["MemberNo"]
                              where fres.iFormID == 3
                              where fres.iSectionID == sectionid
                              select fres);

            var formQs = (from fqs in cvdc.FormQuestions
                          where fqs.iFormID == 3
                          where fqs.iSectionID == sectionid
                          where fqs.bActive
                          select fqs);

            text += "<h1>" + qDetails.cTitle + "</h1>";

            var firstOrDefault = formResult.FirstOrDefault();
            if (firstOrDefault != null)
                text += "<h3>Date : " + firstOrDefault.dDateCaptured.ToString("dd MMMM yyyy") + "</h3>";
            else text += "<h3>Date : " + DateTime.Now.ToString("dd MMMM yyyy") + "</h3>";

            text += "<div id='blurb'>" + qDetails.cBlurb + "</div>";

            text += "<table id='qandatable'>";

            switch (quesType)
            {
                case 2:
                    {
                        var formQsChildren = (from fqsc in cvdc.FormQuestionChildrens
                                              where formQs.Select(x => x.iID).Contains((int)fqsc.ifqID)
                                              select fqsc);
                        text += GenerateQuestions(formQs, formResult, quesType, formQsChildren);
                    } break;

                default:
                    {
                        text += GenerateQuestions(formQs, formResult, quesType, null);

                    } break;
            }

            if (quesType == 1) text += "<tr style='font-weight: bold; text-align: right;'><td style='color:red;'>Total Score</td><td id='totalscore'>" + _totalScore + "</td></tr>";

            text += "</table>";

            text += qDetails.cInterpretation;

            text += "<br/>" +
                    "<div style=\"width:50%;text-align:center;display:block;margin:0 auto;\">" +

                    "<button Class=\"button-small vision_red rounded3\" text=\"Save and Exit\" onclick=\"" + js + " return false;\">Save and Exit</button/>" +
                //"<button class=\"thougtbot\" text=\"Save and Exit\" onclick=\"submit_questionnarie(); return false;\">Save and Exit</button/>" +

                    "</div>";
            //window.open('/club-vision/my-profile/ready-set-go/','_self');
            return text;
        }

        protected string GenerateQuestions(IQueryable<FormQuestion> formQs, IQueryable<FormResult> formResult,
            int quesType, IQueryable<FormQuestionChildren> formQsChildren = null)
        {
            string text = "";

            switch (quesType)
            {
                case 1:
                    {
                        foreach (var formQuestion in formQs)
                        {
                            string ddloption = "<select id='qSelect" + formQuestion.iQuestion + "' " +
                                                "onchange=\"changeQuesAnswer('" + formQuestion.iFormID + "', '" + formQuestion.iSectionID + "', '" + formQuestion.iQuestion + "', $(this).val()); return false;\">" +
                                                "<option value='0'></option>" +
                                                "<option value='1'>1</option>" +
                                                "<option value='2'>2</option>" +
                                                "<option value='3'>3</option>" +
                                                "<option value='4'>4</option></select>";

                            string value = formResult.Where(x => x.iQuestionID == formQuestion.iQuestion).Select(x => x.iResult).
                                SingleOrDefault() + "";

                            ddloption = ddloption.Replace("value='" + value + "'", "value='" + value + "' selected");

                            text += "<tr><td>" + formQuestion.cQuestion + "</td>" +
                                    "<td>" + ddloption + "</td></tr>";
                            if (value != "")
                            {
                                _totalScore += Convert.ToInt32(value);
                            }

                        }

                    } break;
                case 2:
                    {
                        foreach (var formQuestion in formQs)
                        {
                            FormQuestion question = formQuestion;
                            var formQsChilOp = formQsChildren.Where(x => x.ifqID == question.iID).OrderBy(x => x.iID).Select(x => x.cQuestion);

                            string ddloption = "<select id='qSelect" + question.iQuestion + "' " +
                                               "onchange=\"changeQuesAnswer('" + question.iFormID + "', '" + question.iSectionID + "', '" + question.iQuestion + "', $(this).val()); return false;\">" +
                                               "<option value='0'></option>" +
                                               "<option value='" + formQsChilOp.Skip(0).Take(1).SingleOrDefault().Substring(formQsChilOp.Skip(0).Take(1).SingleOrDefault().Length - 2, 1) + "'>" + formQsChilOp.Skip(0).Take(1).SingleOrDefault() + "</option>" +
                                               "<option value='" + formQsChilOp.Skip(1).Take(1).SingleOrDefault().Substring(formQsChilOp.Skip(1).Take(1).SingleOrDefault().Length - 2, 1) + "'>" + formQsChilOp.Skip(1).Take(1).SingleOrDefault() + "</option>" +
                                               "<option value='" + formQsChilOp.Skip(2).Take(1).SingleOrDefault().Substring(formQsChilOp.Skip(2).Take(1).SingleOrDefault().Length - 2, 1) + "'>" + formQsChilOp.Skip(2).Take(1).SingleOrDefault() + "</option>" +
                                               "<option value='" + formQsChilOp.Skip(3).Take(1).SingleOrDefault().Substring(formQsChilOp.Skip(3).Take(1).SingleOrDefault().Length - 2, 1) + "'>" + formQsChilOp.Skip(3).Take(1).SingleOrDefault() + "</option></select>";

                            string value = formResult.Where(x => x.iQuestionID == question.iQuestion).Select(x => x.iResult).
                                SingleOrDefault() + "";

                            ddloption = ddloption.Replace("value='" + value + "'", "value='" + value + "' selected");

                            text += "<tr><td>" + question.cQuestion + "</td>" +
                                    "<td>" + ddloption + "</td></tr>";

                            if (value != "")
                            {
                                _totalScore += Convert.ToInt32(value);
                            }
                        }

                    } break;
                case 3:
                    {
                        foreach (var formQuestion in formQs)
                        {
                            FormQuestion question = formQuestion;

                            string value = formResult.Where(x => x.iQuestionID == formQuestion.iQuestion).Select(x => x.cResult).
                                SingleOrDefault() + "";

                            string textbox = "<textarea onblur=\"changeQuesAnswer('" + question.iFormID + "', '" + question.iSectionID + "', '" + question.iQuestion + "', $(this).val()); return false;\"/" +
                                             ">" + value + "</textarea>";
                            text += "<tr><td>" + formQuestion.cQuestion + "</td>" +
                                    "<td>" + textbox + "</td></tr>";
                            _totalScore++;
                        }
                    } break;
            }



            return text;
        }

        protected string GenerateQuestionnaireTitleList()
        {
            string txt = "<h1>Ready Set Go Questionnaires</h1><br/><br/>";

            txt += "The \"Ready, Set, Go\" principle has been around for years to ensure that sports competitors are given the best opportunity to succeed in their chosen sport.  In order to take control of your long term health, you need to be \"Ready\" and \"Set\" before committing to \"Go\".<br/><br/>" +
                   "Take the below questionnaires as a tool to help determine your readiness to change and let go of the past and embrace the future.  If through doing these questionnaires you find that you aren’t quite ready to make changes in all areas yet, don’t beat yourself up-just understand that results may take longer to achieve.  But be encouraged by the fact that slow changes often lead to sustainable results.<br/><br/>" +
                   "These questionnaires are simply designed to help you identify any barriers that could prevent you achieving your goal as well as determine how ready you are to commit to it.<br/><br/>To learn more watch the Ready Set Go and Empowering Yourself to Take Control.<br/><br/>";

            ClubVisionDataContext cvdc = new ClubVisionDataContext();

            var titles = (from ttl in cvdc.FormDetails
                          where ttl.iFormID == 3
                          select ttl).Select(x => new { title = x.cTitle, section = x.iSectionID })
                          .OrderBy(x => x.section);

            var formres = (from result in cvdc.FormResults
                           where result.iFormID == 3
                           where result.iCustomerID == (int)Session["MemberNo"]
                           select result).Select(x => new { id = x.iID, section = x.iSectionID });

            var formqs = (from quests in cvdc.FormQuestions
                          where quests.iFormID == 3
                          where quests.bActive
                          select quests).Select(x => new { id = x.iID, section = x.iSectionID });

            foreach (var title in titles)
            {
                string stats = "<span style=\"font-weight:bold;color:red;\">Not yet completed</span>";

                if (formres.Count(x => x.section == title.section) == formqs.Count(x => x.section == title.section))
                {
                    stats = "<span style=\"font-weight:bold;color:green;\">Completed</span>";
                }

                txt += "<div class=\"redysetgoqtitles\">" +
                            "<h2>" + title.title + "</h2>" +
                            "<p>Status : " + stats + " | <a href=\"/club-vision/my-profile/ready-set-go/" + RenderQustionnaireLink(title.section) + "\">" +
                            "Take the questionnaire<img src=\"/images/gotoicon.jpg\"/></a>" +
                            "</p>" +
                            "</div>";
            }

            return txt;
        }

        protected string RenderQustionnaireLink(int section)
        {
            switch (section)
            {
                case 5:
                    return "?isection=" + section + "&itype=2";
                case 9:
                    return "?isection=" + section + "&itype=3";
                case 8:
                    return "?isection=" + section + "&itype=3";
                default:
                    return "?isection=" + section + "&itype=1";
            }
        }

        protected string GenerateQuestionnaireTitleList_AndrewReorder()
        {
            string txt = "<h1>Ready Set Go Questionnaires</h1><br/><br/>";

            txt += "The \"Ready, Set, Go\" principle has been around for years to ensure that sports competitors are given the best opportunity to succeed in their chosen sport.  In order to take control of your long term health, you need to be \"Ready\" and \"Set\" before committing to \"Go\".<br/><br/>" +
                   "Take the below questionnaires as a tool to help determine your readiness to change and let go of the past and embrace the future.  If through doing these questionnaires you find that you aren’t quite ready to make changes in all areas yet, don’t beat yourself up-just understand that results may take longer to achieve.  But be encouraged by the fact that slow changes often lead to sustainable results.<br/><br/>" +
                   "These questionnaires are simply designed to help you identify any barriers that could prevent you achieving your goal as well as determine how ready you are to commit to it.<br/><br/>";

            ClubVisionDataContext cvdc = new ClubVisionDataContext();
            int[] orders = {1,2,3,4,8,7,5,6,9};

            var titles = (from ttl in cvdc.FormDetails
                          where ttl.iFormID == 3
                          select ttl).Select(x => new { title = x.cTitle, section = x.iSectionID })
                          .OrderBy(x => x.section);

            var formres = (from result in cvdc.FormResults
                           where result.iFormID == 3
                           where result.iCustomerID == (int)Session["MemberNo"]
                           select result).Select(x => new { id = x.iID, section = x.iSectionID });

            var formqs = (from quests in cvdc.FormQuestions
                          where quests.iFormID == 3
                          where quests.bActive
                          select quests).Select(x => new { id = x.iID, section = x.iSectionID });

            int count = 1;

            foreach (var order in orders)
            {
                var title = titles.Skip(order-1).First();

                string stats = "<span style=\"font-weight:bold;color:red;\">Not yet completed</span>";

                if (formres.Count(x => x.section == title.section) == formqs.Count(x => x.section == title.section))
                {
                    stats = "<span style=\"font-weight:bold;color:green;\">Completed</span>";
                }

                txt += "<div class=\"redysetgoqtitles\">" +
                            "<h2>" + count + ". " + title.title + "</h2>" +
                            "<p>Status : " + stats + " | <a href=\"/club-vision/my-profile/ready-set-go/" + RenderQustionnaireLink(title.section) + "\">" +
                            "Take the questionnaire<img src=\"/images/gotoicon.jpg\"/></a>" +
                            "</p>" +
                            "</div>";
                count++;
            }

            return txt;
        }

        //protected void bt_Click(object sender, EventArgs e)
        //{
        //    string bodytext = "To (insert trainer name)"
        //                  + "Your Client (insert Client name) has completed the (insert q'naire name) questionnaire in Vision Virtual Training."
        //                  + "  Please log-in to their profile online and review as soon as possible."
        //                  + "It is recommended that you either call them to discuss their responses or ensure you cover this at during the next PT Session."

        //                    + "Thanks";

        //    sendEmailToTrainer(1, "hiroshi__777@hotmail.com", "test", "hiroshi.v27.6@gmail.com", "testing email konoyaro!");
        //}
    }
}