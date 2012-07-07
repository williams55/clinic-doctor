
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

public partial class UnitsEdit : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{		
		FormUtil.RedirectAfterInsertUpdate(FormView1, "UnitsEdit.aspx?{0}", UnitsDataSource);
		FormUtil.RedirectAfterAddNew(FormView1, "UnitsEdit.aspx");
		FormUtil.RedirectAfterCancel(FormView1, "Units.aspx");
		FormUtil.SetDefaultMode(FormView1, "Id");
	}
	protected void GridViewAppointmentGroup1_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("Id={0}", GridViewAppointmentGroup1.SelectedDataKey.Values[0]);
		Response.Redirect("AppointmentGroupEdit.aspx?" + urlParams, true);		
	}	
}


