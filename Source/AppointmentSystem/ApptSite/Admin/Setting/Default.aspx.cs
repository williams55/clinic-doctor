using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppointmentSystem.Settings.BusinessLayer;
using DevExpress.Web.ASPxClasses.Internal;
using DevExpress.Web.ASPxEditors;

public partial class Admin_Setting_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;
        MinuteStepTextBox.Text = ServiceFacade.SettingsHelper.MinuteStep.ToString();
        AppointmentTextBox.Text = ServiceFacade.SettingsHelper.AppointmentPrefix;
        RosterTextBox.Text = ServiceFacade.SettingsHelper.RosterPrefix;
        PatientTextBox.Text = ServiceFacade.SettingsHelper.PatientPrefix;
        UserTextBox.Text = ServiceFacade.SettingsHelper.UserPrefix;
    }

    protected void MinuteStepTextBox_Validation(object sender, ValidationEventArgs e)
    {
        if (CommonUtils.IsNullValue(e.Value) || ((string)e.Value == ""))
            return;
        string strAge = ((string)e.Value).TrimStart('0');
        if (strAge.Length == 0)
            return;
        UInt32 age;
        if (!UInt32.TryParse(strAge, out age) || age < 5 || age > 60)
            e.IsValid = false;
    }

    protected void SubmitAppointment_Click(object sender, EventArgs e)
    {
        int age;
        if (Int32.TryParse(MinuteStepTextBox.Text, out age))
            ServiceFacade.SettingsHelper.MinuteStep = age;
    }

    protected void SubmitPrefix_Click(object sender, EventArgs e)
    {
        ServiceFacade.SettingsHelper.AppointmentPrefix = AppointmentTextBox.Text;
        ServiceFacade.SettingsHelper.RosterPrefix = RosterTextBox.Text;
        ServiceFacade.SettingsHelper.PatientPrefix = PatientTextBox.Text;
        ServiceFacade.SettingsHelper.UserPrefix = UserTextBox.Text;
    }
}
