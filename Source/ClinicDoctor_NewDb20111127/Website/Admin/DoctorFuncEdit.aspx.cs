
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

public partial class DoctorFuncEdit : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{		
		FormUtil.RedirectAfterInsertUpdate(FormView1, "DoctorFuncEdit.aspx?{0}", DoctorFuncDataSource);
		FormUtil.RedirectAfterAddNew(FormView1, "DoctorFuncEdit.aspx");
		FormUtil.RedirectAfterCancel(FormView1, "DoctorFunc.aspx");
		FormUtil.SetDefaultMode(FormView1, "Id");
	}
}


