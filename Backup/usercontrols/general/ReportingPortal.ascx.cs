using System;
using System.Globalization;
using System.Linq;

namespace VisionPersonalTrainingProject.usercontrols.general
{
    public partial class ReportingPortal : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ReportingTab.Style["background"] =
                            "url(\"/images/eHdrReportTab1.gif\") no-repeat scroll 0 0 transparent"; 
        }

        protected void EnterButton_Click(object sender, EventArgs e)
        {
            if(txtPassword.Text.Equals("dewican888"))
            {
                gateMenu.Visible = true;
                gateDiv.Visible = false;
                extVVTStatDiv.Visible = true;
                ShareStatDiv.Visible = true;
                extVVTStatDiv.Style["display"] = "block";
                ShareStatDiv.Style["display"] = "none";
            }
            else
            {
                litDivGate.Text = "<p><span style='color:red;'>Incorrect password</span>. Please email <a href='mailto:dewi@hq.visionpersonaltraining.com'>Dewi Candraningsih</a> for further assistance</p>";
            }
        }

        protected void GetSummaryStatClick(object sender, EventArgs e)
        {
            try
            {
                ClubVisionDataContext cvdc = new ClubVisionDataContext();
                
                var list = cvdc.Statistic_extVVTCustomer(Convert.ToDateTime(fromDate1.Value), Convert.ToDateTime(toDate1.Value));
     
                var listcodes = cvdc.Statistic_extVVTCustCodes(Convert.ToDateTime(fromDate1.Value), Convert.ToDateTime(toDate1.Value));
                string lscodestr = listcodes.Aggregate("", (current, statCodes) => current + (statCodes.cVoucherCode + " = " + statCodes.totCount + "</br>"));

                var listsources = cvdc.Statistic_extVVTCustSources(Convert.ToDateTime(fromDate1.Value), Convert.ToDateTime(toDate1.Value));
                string lssrcstr = listsources.Aggregate("", (current, statCodes) => current + (statCodes.cSource + " = " + statCodes.totCount + "</br>"));

                var listamtpay = cvdc.Statistic_extVVTClientAmountPay(Convert.ToDateTime(fromDate1.Value), Convert.ToDateTime(toDate1.Value));
                string lsamtpay = listamtpay.Aggregate("", (current, statCodes) => current + ("$" + statCodes.LastPaymentAmt + " = " + statCodes.Count + "</br>"));

                foreach (Statistic_extVVTCustomerResult stat in list)
                {
                    extVVTStatLit.Text = "<br/><br/><h3>VVT External " + stat.title + "</h3><br/>" +
                                         "<table class='statSumTable'>" +
                                         "<tr><td>Total Enquiries</td><td>" + stat.totalInquiry + "</td></tr>" +
                                         "<tr><td>New Clients</td><td>Total = " + stat.newClients + "<br/>------------<br/>" + lscodestr + "</td></tr>" +
                                         "<tr><td>Cancelled Clients</td><td>" + stat.cancelledClients + "</td></tr>" +
                                         "<tr><td>Source for Enquiry</td><td>" + lssrcstr + "</td></tr>" +
                                         "</table><br/>";

                    extVVTStatLit.Text += "<h3>VVT External Current Statistics</h3><br/>" +
                                            "<table class='statSumTable'>" +
                                            "<tr><td>Total Active Clients</td><td>" + stat.totalClients + "</td></tr>" +
                                            "<tr><td>Current Paying Amount</td><td>" + lsamtpay + "</td></tr>" +
                                            "<tr><td>Current Length of Stay</td><td>" + stat.lengthStay + " Days</td></tr></table><br/>";

                    extVVTStatLit.Text += "<h3>Referral, Ebook and Newsletter Statistics for " + Convert.ToDateTime(fromDate1.Value).ToLongDateString() + " to " + Convert.ToDateTime(toDate1.Value).ToLongDateString() + "</h3><br/>" +
                                            "<table class='statSumTable'>" +
                                            "<tr><td>Referrals to VPT</td><td>" + stat.reftoVPT + "</td></tr>" +
                                            "<tr><td>Referrals to VVT</td><td>" + stat.reftoVVT + "</td></tr>" +
                                            "<tr><td>eBook Downloader</td><td>" + stat.ebookDownloader + "</td></tr>" +
                                            "<tr><td>Newsletter Downloader</td><td>" + stat.newsletter + "</td></tr></table>";
                  
                }
            }
            catch (Exception exception)
            {
                extVVTStatLit.Text = "<p>Error generating reports.. please call IT team for assistance</p><br/>";
                extVVTStatLit.Text += "<p>" + exception + "</p>";
            }

            ShareStatDiv.Style["display"] = "none";
            extVVTStatDiv.Style["display"] = "block";
            ReportingTab.Style["background"] =
                       "url(\"/images/eHdrReportTab1.gif\") no-repeat scroll 0 0 transparent"; 
        }

        protected void GetListClick(object sender, EventArgs e)
        {
            try
            {
                ClubVisionDataContext cvdc = new ClubVisionDataContext();
                var notjoinlist = cvdc.Statistic_extVVTCustList_InquiryOnly(Convert.ToDateTime(fromDate1.Value), Convert.ToDateTime(toDate1.Value));

                extVVTStatLit.Text = "<br/><br/><h3>Not Join List</h3><br/><table class='listtable'><tr><td>Name</td><td>Email</td></tr>";

                foreach (var custext in notjoinlist)
                {
                    extVVTStatLit.Text += "<tr><td>" + custext.name + "</td>" +
                                          "<td>" + custext.email + "</td>" +
                                          "</tr>";
                }

                extVVTStatLit.Text += "</table>";


                var cancellist = cvdc.Statistic_extVVTCustList_Cancel(Convert.ToDateTime(fromDate1.Value), Convert.ToDateTime(toDate1.Value));

                extVVTStatLit.Text += "<br/><br/><h3>Cancelled List</h3><br/><table class='listtable'><tr><td>Name</td><td>Email</td><td>Date Cancelled</td></tr>";

                foreach (var custext in cancellist)
                {
                    extVVTStatLit.Text += "<tr><td>" + custext.name + "</td>" +
                                          "<td>" + custext.email + "</td>" +
                                          "<td>" + custext.datecanceled + "</td></tr>";
                }

                extVVTStatLit.Text += "</table>";



            }
            catch (Exception exception)
            {
                extVVTStatLit.Text = "<p>Error generating reports.. please call IT team for assistance</p>";
            }

            ShareStatDiv.Style["display"] = "none";
            extVVTStatDiv.Style["display"] = "block";
            ReportingTab.Style["background"] =
                       "url(\"/images/eHdrReportTab1.gif\") no-repeat scroll 0 0 transparent"; 
        }

        protected void GetSummaryShareClick(object sender, EventArgs e)
        {
            try
            {
                ClubVisionDataContext cvdc = new ClubVisionDataContext();

                var Sharelist = cvdc.Statistic_ShareStat(Convert.ToDateTime(fromDate2.Value), Convert.ToDateTime(toDate2.Value));

                var topstudios = cvdc.Statistic_ShareStat_TopStudios(Convert.ToDateTime(fromDate2.Value), Convert.ToDateTime(toDate2.Value));
                string topstudiostr = topstudios.Aggregate("", (current, statCodes) => current + (statCodes.studioname + " = " + statCodes.count + "</br>"));

                foreach (Statistic_ShareStatResult stat in Sharelist)
                {
                    ShareStatLit.Text = "<br/><br/><h3>" + stat.title + "</h3><br/>" +
                                         "<table class='statSumTable'><tr><td>Total Share</td><td>" + stat.totalShares + "</td></tr>" +
                                         "<tr><td>By Client</td><td>" + stat.byClient + "</td></tr>" +
                                         "<tr><td>By Trainer</td><td>" + stat.byTrainer + "</td></tr>" +
                                         "<tr><td>To Facebook</td><td>" + stat.toFacebook + "</td></tr>" +
                                         "<tr><td>Top 5 Studios</td><td>" + topstudiostr + "</td></tr></table>";
                }

                var statisticDiaryDaysCaptainResult = cvdc.Statistic_DiaryDaysCaptain(Convert.ToDateTime(fromDate2.Value), Convert.ToDateTime(toDate2.Value)).SingleOrDefault();
                if (
                    statisticDiaryDaysCaptainResult != null)
                    ShareStatLit.Text += "<br/><h3>Food Diary days being populated by captain for that period above = " +
                                         statisticDiaryDaysCaptainResult.Column1.Value.ToString(CultureInfo.InvariantCulture) + "</h3>";
            }
            catch (Exception exception)
            {
                ShareStatLit.Text = "<p>Error generating reports.. please call IT team for assistance</p>";
            }

            ShareStatDiv.Style["display"] = "block";
            extVVTStatDiv.Style["display"] = "none";
            ReportingTab.Style["background"] =
                       "url(\"/images/eHdrReportTab2.gif\") no-repeat scroll 0 0 transparent"; 
        }
    }
}