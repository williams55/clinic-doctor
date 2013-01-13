using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppointmentBusiness.BO;
using ApptSite;
using DevExpress.Web.ASPxClasses.Internal;
using DevExpress.Web.ASPxEditors;

public partial class Admin_Setting_ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void SubmitAppointment_Click(object sender, EventArgs e)
    {
        string message;
        if (string.IsNullOrEmpty(OldPasswordTextBox.Text)
            || string.IsNullOrEmpty(NewPasswordTextBox.Text)
            || string.IsNullOrEmpty(ConfirmPasswordTextBox.Text)
            || NewPasswordTextBox.Text != ConfirmPasswordTextBox.Text)
        {
            message = "Invalid input";
        }
        else
        {
            if (BoFactory.UserBO.ChangePassword(AccountSession.Session, OldPasswordTextBox.Text, NewPasswordTextBox.Text))
            {
                message = "Your password has been changed";
                OldPasswordTextBox.Text = NewPasswordTextBox.Text = ConfirmPasswordTextBox.Text = string.Empty;
            }
            else
            {
                message = "Your old password is wrong";
            }
        }

        WebCommon.ShowDialog(this, message);
    }
}
