
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

public partial class RoleEdit : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{		
		FormUtil.RedirectAfterInsertUpdate(FormView1, "RoleEdit.aspx?{0}", RoleDataSource);
		FormUtil.RedirectAfterAddNew(FormView1, "RoleEdit.aspx");
		FormUtil.RedirectAfterCancel(FormView1, "Role.aspx");
		FormUtil.SetDefaultMode(FormView1, "Id");
	}
	protected void GridViewGroupRole1_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("Id={0}", GridViewGroupRole1.SelectedDataKey.Values[0]);
		Response.Redirect("GroupRoleEdit.aspx?" + urlParams, true);		
	}	
	protected void GridViewRoleDetail2_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("Id={0}", GridViewRoleDetail2.SelectedDataKey.Values[0]);
		Response.Redirect("RoleDetailEdit.aspx?" + urlParams, true);		
	}	
	protected void GridViewUserRole3_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("Id={0}", GridViewUserRole3.SelectedDataKey.Values[0]);
		Response.Redirect("UserRoleEdit.aspx?" + urlParams, true);		
	}	
}


