
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

public partial class StatusEdit : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{		
		FormUtil.RedirectAfterInsertUpdate(FormView1, "StatusEdit.aspx?{0}", StatusDataSource);
		FormUtil.RedirectAfterAddNew(FormView1, "StatusEdit.aspx");
		FormUtil.RedirectAfterCancel(FormView1, "Status.aspx");
		FormUtil.SetDefaultMode(FormView1, "Id");
	}
	protected void GridViewAppointment1_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("Id={0}", GridViewAppointment1.SelectedDataKey.Values[0]);
		Response.Redirect("AppointmentEdit.aspx?" + urlParams, true);		
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


