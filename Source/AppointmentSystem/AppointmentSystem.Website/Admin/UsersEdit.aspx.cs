
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

public partial class UsersEdit : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{		
		FormUtil.RedirectAfterInsertUpdate(FormView1, "UsersEdit.aspx?{0}", UsersDataSource);
		FormUtil.RedirectAfterAddNew(FormView1, "UsersEdit.aspx");
		FormUtil.RedirectAfterCancel(FormView1, "Users.aspx");
		FormUtil.SetDefaultMode(FormView1, "Username");
	}
	protected void GridViewUserRole1_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("Id={0}", GridViewUserRole1.SelectedDataKey.Values[0]);
		Response.Redirect("UserRoleEdit.aspx?" + urlParams, true);		
	}	
	protected void GridViewDoctorRoom2_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("Id={0}", GridViewDoctorRoom2.SelectedDataKey.Values[0]);
		Response.Redirect("DoctorRoomEdit.aspx?" + urlParams, true);		
	}	
	protected void GridViewAppointment3_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("Id={0}", GridViewAppointment3.SelectedDataKey.Values[0]);
		Response.Redirect("AppointmentEdit.aspx?" + urlParams, true);		
	}	
	protected void GridViewRoster4_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("Id={0}", GridViewRoster4.SelectedDataKey.Values[0]);
		Response.Redirect("RosterEdit.aspx?" + urlParams, true);		
	}	
}


