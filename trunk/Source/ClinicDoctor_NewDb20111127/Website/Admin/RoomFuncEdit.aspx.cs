
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

public partial class RoomFuncEdit : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{		
		FormUtil.RedirectAfterInsertUpdate(FormView1, "RoomFuncEdit.aspx?{0}", RoomFuncDataSource);
		FormUtil.RedirectAfterAddNew(FormView1, "RoomFuncEdit.aspx");
		FormUtil.RedirectAfterCancel(FormView1, "RoomFunc.aspx");
		FormUtil.SetDefaultMode(FormView1, "Id");
	}
    protected void FormView1_Load(object sender, EventArgs e)
    {
        if (FormView1.CurrentMode == FormViewMode.Insert)
        {
            HiddenField tbCreateDate = (HiddenField)FormView1.Row.FindControl("HDcreatedate");
            HiddenField tbUpdateDate = (HiddenField)FormView1.Row.FindControl("HDUpdateDate");
            tbCreateDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
            tbUpdateDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");

        }
        else
        {
            HiddenField tbUpdateDate = (HiddenField)FormView1.Row.FindControl("HDUpdateDate");
            tbUpdateDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
        }
    }
}


