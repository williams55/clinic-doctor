
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

public partial class CustomerEdit : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{		
		FormUtil.RedirectAfterInsertUpdate(FormView1, "CustomerEdit.aspx?{0}", CustomerDataSource);
		FormUtil.RedirectAfterAddNew(FormView1, "CustomerEdit.aspx");
		FormUtil.RedirectAfterCancel(FormView1, "Customer.aspx");
		FormUtil.SetDefaultMode(FormView1, "Id");
	}
	protected void GridViewAppointment1_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("Id={0}", GridViewAppointment1.SelectedDataKey.Values[0]);
		Response.Redirect("AppointmentEdit.aspx?" + urlParams, true);		
	}	
}


