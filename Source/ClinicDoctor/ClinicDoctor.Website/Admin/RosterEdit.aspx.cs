
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

public partial class RosterEdit : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{		
		FormUtil.RedirectAfterInsertUpdate(FormView1, "RosterEdit.aspx?{0}", RosterDataSource);
		FormUtil.RedirectAfterAddNew(FormView1, "RosterEdit.aspx");
		FormUtil.RedirectAfterCancel(FormView1, "Roster.aspx");
		FormUtil.SetDefaultMode(FormView1, "Id");
	}
	protected void GridViewDoctorRoster1_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("Id={0}", GridViewDoctorRoster1.SelectedDataKey.Values[0]);
		Response.Redirect("DoctorRosterEdit.aspx?" + urlParams, true);		
	}	
}


