
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

public partial class DoctorRoomEdit : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{		
		FormUtil.RedirectAfterInsertUpdate(FormView1, "DoctorRoomEdit.aspx?{0}", DoctorRoomDataSource);
		FormUtil.RedirectAfterAddNew(FormView1, "DoctorRoomEdit.aspx");
		FormUtil.RedirectAfterCancel(FormView1, "DoctorRoom.aspx");
		FormUtil.SetDefaultMode(FormView1, "Id");
	}
}


