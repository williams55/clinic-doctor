
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

public partial class ServicesEdit : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{		
		FormUtil.RedirectAfterInsertUpdate(FormView1, "ServicesEdit.aspx?{0}", ServicesDataSource);
		FormUtil.RedirectAfterAddNew(FormView1, "ServicesEdit.aspx");
		FormUtil.RedirectAfterCancel(FormView1, "Services.aspx");
		FormUtil.SetDefaultMode(FormView1, "Id");
	}
	protected void GridViewDoctorService1_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("Id={0}", GridViewDoctorService1.SelectedDataKey.Values[0]);
		Response.Redirect("DoctorServiceEdit.aspx?" + urlParams, true);		
	}	
	protected void GridViewAppointment2_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("Id={0}", GridViewAppointment2.SelectedDataKey.Values[0]);
		Response.Redirect("AppointmentEdit.aspx?" + urlParams, true);		
	}	
	protected void GridViewRoom3_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("Id={0}", GridViewRoom3.SelectedDataKey.Values[0]);
		Response.Redirect("RoomEdit.aspx?" + urlParams, true);		
	}	
}


