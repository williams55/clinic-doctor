
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

public partial class UserGroupEdit : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{		
		FormUtil.RedirectAfterInsertUpdate(FormView1, "UserGroupEdit.aspx?{0}", UserGroupDataSource);
		FormUtil.RedirectAfterAddNew(FormView1, "UserGroupEdit.aspx");
		FormUtil.RedirectAfterCancel(FormView1, "UserGroup.aspx");
		FormUtil.SetDefaultMode(FormView1, "Id");
	}
	protected void GridViewGroupRole1_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("Id={0}", GridViewGroupRole1.SelectedDataKey.Values[0]);
		Response.Redirect("GroupRoleEdit.aspx?" + urlParams, true);		
	}	
	protected void GridViewUsers2_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("Id={0}", GridViewUsers2.SelectedDataKey.Values[0]);
		Response.Redirect("UsersEdit.aspx?" + urlParams, true);		
	}	
}


