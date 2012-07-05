
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

public partial class RoleDetailEdit : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{		
		FormUtil.RedirectAfterInsertUpdate(FormView1, "RoleDetailEdit.aspx?{0}", RoleDetailDataSource);
		FormUtil.RedirectAfterAddNew(FormView1, "RoleDetailEdit.aspx");
		FormUtil.RedirectAfterCancel(FormView1, "RoleDetail.aspx");
		FormUtil.SetDefaultMode(FormView1, "Id");
	}
}


