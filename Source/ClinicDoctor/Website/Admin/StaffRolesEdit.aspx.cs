
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

public partial class StaffRolesEdit : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{		
		FormUtil.RedirectAfterInsertUpdate(FormView1, "StaffRolesEdit.aspx?{0}", StaffRolesDataSource);
		FormUtil.RedirectAfterAddNew(FormView1, "StaffRolesEdit.aspx");
		FormUtil.RedirectAfterCancel(FormView1, "StaffRoles.aspx");
		FormUtil.SetDefaultMode(FormView1, "Id");
	}
}


