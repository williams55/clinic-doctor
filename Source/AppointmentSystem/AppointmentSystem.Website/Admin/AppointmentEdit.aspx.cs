
#region Imports...
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AppointmentSystem.Web.UI;
#endregion

public partial class AppointmentEdit : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{		
		FormUtil.RedirectAfterInsertUpdate(FormView1, "AppointmentEdit.aspx?{0}", AppointmentDataSource);
		FormUtil.RedirectAfterAddNew(FormView1, "AppointmentEdit.aspx");
		FormUtil.RedirectAfterCancel(FormView1, "Appointment.aspx");
		FormUtil.SetDefaultMode(FormView1, "Id");
	}
	protected void GridViewAppointmentHistory1_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("Guid={0}", GridViewAppointmentHistory1.SelectedDataKey.Values[0]);
		Response.Redirect("AppointmentHistoryEdit.aspx?" + urlParams, true);		
	}	
}


