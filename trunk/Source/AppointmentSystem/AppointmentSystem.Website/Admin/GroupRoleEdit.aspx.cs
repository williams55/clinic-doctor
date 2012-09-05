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
using AppointmentSystem.Web.UI;
#endregion

public partial class GroupRoleEdit : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{		
		FormUtil.RedirectAfterInsertUpdate(FormView1, "GroupRoleEdit.aspx?{0}", GroupRoleDataSource);
		FormUtil.RedirectAfterAddNew(FormView1, "GroupRoleEdit.aspx");
		FormUtil.RedirectAfterCancel(FormView1, "GroupRole.aspx");
		FormUtil.SetDefaultMode(FormView1, "Id");
	}
}

