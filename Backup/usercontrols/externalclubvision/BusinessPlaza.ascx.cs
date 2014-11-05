using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Web.Security;

namespace VisionPersonalTrainingProject.usercontrols.externalclubvision
{
    public partial class BusinessPlaza : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Browser.Browser.Equals("IE"))
                {
                    RequiredFieldValidator1.InitialValue = "First Name";
                    RequiredFieldValidator2.InitialValue = "Last Name";
                    RequiredFieldValidator4.InitialValue = "Email";
                    RequiredFieldValidator5.InitialValue = "Confirm Email";
                }
            }
        }

        protected void ImageButton1Click(object sender, ImageClickEventArgs e)
        {
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            VisionPersonalTrainingProject.VOSWebService.Service service = new VisionPersonalTrainingProject.VOSWebService.Service();
            service.AuthenticationHeaderValue = new VisionPersonalTrainingProject.VOSWebService.AuthenticationHeader();
            service.AuthenticationHeaderValue.UserName = "vosABC";
            service.AuthenticationHeaderValue.Password = "vosABCpass1";

            if (CheckBox3.Checked && CheckBox4.Checked && lblForEmail.Text.Equals(""))
            {
                string loginName = ti.ToLower(txtFirstName.Text + txtSurname.Text);
                bool isLoginNotInVOS = service.GetMemberLoginAvailability(loginName, 1).LoginAvailable;

                if (IsLoginNotInExt(loginName) && isLoginNotInVOS)
                {
                    string custid = SavingDatatoCustomerTable(loginName, ti.ToTitleCase(txtFirstName.Text), ti.ToTitleCase(txtSurname.Text),
                                              txtEmail.Text, txtClubName.Text, ti.ToUpper(txtVoucher.Text));

                    SendThemToPayPal(custid);
                }
                else
                {
                    regodiv.Style["display"] = "none";
                    usernamediv.Style["display"] = "block";
                }

            }
            else
            {
                LabelValidation.Visible = true;
            }

        }

        protected void ImageButton2Click(object sender, ImageClickEventArgs e)
        {
            if (IsLoginNotInExt(txtUserName.Text))
            {
                string custid = SavingDatatoCustomerTable(txtUserName.Text, txtFirstName.Text, txtSurname.Text,
                                            txtEmail.Text, txtClubName.Text, txtVoucher.Text);

                SendThemToPayPal(custid);
            }
            else
            {
                Label3.Text = "Sorry, this username is also not available, please choose again.";
            }
        }

        protected string SavingDatatoCustomerTable
        (string loginName, string fname, string lname, string email, string source, string voucherCode)
        {
            var cvedc = new ClubVisionDataContext();

            var customer = new Customer_External()
            {
                cLoginName = loginName,
                cPassword = "1234",
                cFirstName = char.ToUpper(fname[0]) + fname.Substring(1),
                cLastName = char.ToUpper(lname[0]) + lname.Substring(1),
                cEmail = email,
                cSource = source,
                cVoucherCode = "Business Plaza",
                dDateCaptured = DateTime.Now,
                bActive = false,
                bCompleteInitialState = false,
                dDateTerminate = DateTime.Now
            };

            //if (!string.IsNullOrEmpty(voucherCode))
            //{
            //    customer.cVoucherCode = ConvertVoucherCode(voucherCode);
            //}

            //if (DropDownList1.SelectedValue.Equals("11"))
            //{
            //    customer.cSource += " -- " + txtOther.Text;
            //}

            cvedc.Customer_Externals.InsertOnSubmit(customer);
            cvedc.SubmitChanges();

            return customer.iID.ToString(CultureInfo.InvariantCulture);
        }

        protected bool IsLoginNotInExt(string loginName)
        {
            try
            {
                //MembershipUser user = Membership.GetUser(loginName);
                //return user == null;
                ClubVisionDataContext cvdc = new ClubVisionDataContext();
                var cust = (from cs in cvdc.Customer_Externals
                            where cs.cLoginName == loginName
                            where cs.dDateTerminate > DateTime.Today
                            select cs).Count();

                if (cust == 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        protected void SendThemToPayPal(string custId)
        {
            //$1 for 4 weeks then $29 each month
            string redirect = "https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=LE6E8P3JLDU38&custom=" + custId;

            Response.Redirect(redirect);
        }

        protected void txtEmail_Changed(object sender, EventArgs e)
        {
            VisionPersonalTrainingProject.VOSWebService.Service service = new VisionPersonalTrainingProject.VOSWebService.Service();
            service.AuthenticationHeaderValue = new VisionPersonalTrainingProject.VOSWebService.AuthenticationHeader();
            service.AuthenticationHeaderValue.UserName = "vosABC";
            service.AuthenticationHeaderValue.Password = "vosABCpass1";
            bool isEmailNotInVOS = service.GetMemberLoginAvailability(txtEmail.Text, 2).LoginAvailable;


            var cvedc = new ClubVisionDataContext();

            var checkEmail = (from emails in cvedc.Customer_Externals
                              where emails.cEmail == txtEmail.Text
                              where emails.dDateTerminate > DateTime.Today || (emails.dDateCanceled != null && !emails.cPassword.Equals("1234"))
                              select emails);

            if (checkEmail.Any() || !isEmailNotInVOS)
            {
                lblForEmail.Text = "<br/>This email is already being used<br/>";
                lblForEmail.Visible = true;
            }
            else
            {
                lblForEmail.Text = "";
                lblForEmail.Visible = false;
            }

            cvedc.Dispose();
        }

        protected void test_Click(object sender, EventArgs e)
        {
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            VisionPersonalTrainingProject.VOSWebService.Service service = new VisionPersonalTrainingProject.VOSWebService.Service();
            service.AuthenticationHeaderValue = new VisionPersonalTrainingProject.VOSWebService.AuthenticationHeader();
            service.AuthenticationHeaderValue.UserName = "vosABC";
            service.AuthenticationHeaderValue.Password = "vosABCpass1";

            if (CheckBox3.Checked && CheckBox4.Checked && lblForEmail.Text.Equals(""))
            {
                string loginName = ti.ToLower(txtFirstName.Text + txtSurname.Text);
                bool isLoginNotInVOS = service.GetMemberLoginAvailability(loginName, 1).LoginAvailable;

                if (IsLoginNotInExt(loginName) && isLoginNotInVOS)
                {
                    string custid = SavingDatatoCustomerTable(loginName, ti.ToTitleCase(txtFirstName.Text), ti.ToTitleCase(txtSurname.Text),
                                              txtEmail.Text, txtClubName.Text, ti.ToUpper(txtVoucher.Text));

                    SendThemToPayPal(custid);
                }
                else
                {
                    regodiv.Style["display"] = "none";
                    usernamediv.Style["display"] = "block";
                }

            }
            else
            {
                LabelValidation.Visible = true;
            }
        }

        //protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (DropDownList1.SelectedValue.Equals("11"))
        //    {
        //        txtOther.Visible = true;
        //        RequiredFieldValidator6.Enabled = true;
        //    }
        //    else
        //    {
        //        txtOther.Visible = false;
        //        RequiredFieldValidator6.Enabled = false;
        //    }
        //}
    }
}