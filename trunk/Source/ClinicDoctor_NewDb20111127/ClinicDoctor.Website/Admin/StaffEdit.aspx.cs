﻿
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
using ClinicDoctor.Web.UI;
#endregion

public partial class StaffEdit : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{		
		FormUtil.RedirectAfterInsertUpdate(FormView1, "StaffEdit.aspx?{0}", StaffDataSource);
		FormUtil.RedirectAfterAddNew(FormView1, "StaffEdit.aspx");
		FormUtil.RedirectAfterCancel(FormView1, "Staff.aspx");
		FormUtil.SetDefaultMode(FormView1, "Id");
	}
	protected void GridViewDoctorRoom1_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("Id={0}", GridViewDoctorRoom1.SelectedDataKey.Values[0]);
		Response.Redirect("DoctorRoomEdit.aspx?" + urlParams, true);		
	}	
	protected void GridViewAppointment2_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("Id={0}", GridViewAppointment2.SelectedDataKey.Values[0]);
		Response.Redirect("AppointmentEdit.aspx?" + urlParams, true);		
	}	
	protected void GridViewAppointment3_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("Id={0}", GridViewAppointment3.SelectedDataKey.Values[0]);
		Response.Redirect("AppointmentEdit.aspx?" + urlParams, true);		
	}	
	protected void GridViewDoctorRoster4_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("Id={0}", GridViewDoctorRoster4.SelectedDataKey.Values[0]);
		Response.Redirect("DoctorRosterEdit.aspx?" + urlParams, true);		
	}	
	protected void GridViewDoctorFunc5_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("Id={0}", GridViewDoctorFunc5.SelectedDataKey.Values[0]);
		Response.Redirect("DoctorFuncEdit.aspx?" + urlParams, true);		
	}	
}


