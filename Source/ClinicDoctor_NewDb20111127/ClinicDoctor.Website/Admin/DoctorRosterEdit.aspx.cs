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

public partial class DoctorRosterEdit : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{		
		FormUtil.RedirectAfterInsertUpdate(FormView1, "DoctorRosterEdit.aspx?{0}", DoctorRosterDataSource);
		FormUtil.RedirectAfterAddNew(FormView1, "DoctorRosterEdit.aspx");
		FormUtil.RedirectAfterCancel(FormView1, "DoctorRoster.aspx");
		FormUtil.SetDefaultMode(FormView1, "Id");
	}
}


