using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppointmentBusiness.Util;
using AppointmentSystem.Data;
using AppointmentSystem.Entities;
using AppointmentSystem.Settings.BusinessLayer;
using AppointmentSystem.Web.Data;
using Appt.Common.Constants;
using ApptSite;
using Common;
using Common.Util;
using DevExpress.Utils;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.Data;
using Log.Controller;
using Newtonsoft.Json;

public partial class Admin_Appointment_Grid : Page
{
    private const string ScreenCode = "Roster";
    static string _message;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Neu la postback thi bo qua
            if (IsPostBack)
            {
                return;
            }

            // Check reading right
            if (!RightAccess.CheckUserRight(AccountSession.Session, ScreenCode, OperationConstant.Read.Key, out _message))
            {
                WebCommon.ShowDialog(this, _message, WebCommon.GetHomepageUrl(this));
                gridAppointment.Visible = false;
                return;
            }

            // Set page size
            gridAppointment.SettingsPager.PageSize = ServiceFacade.SettingsHelper.PageSize;
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            WebCommon.ShowDialog(this, "System is error. Please contact Administrator.");
        }
    }

    protected void btnXlsExport_Click(object sender, EventArgs e)
    {
        gridAppointment.Columns["Room"].Visible = true;
        gridAppointment.Columns["Service"].Visible = true;
        gridExport.WriteXlsToResponse(String.Format("APPT_{0:yyyyMMdd_HHmmss}", DateTime.Now));
    }
}
