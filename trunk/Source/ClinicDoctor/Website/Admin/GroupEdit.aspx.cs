
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

public partial class GroupEdit : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{		
		FormUtil.RedirectAfterInsertUpdate(FormView1, "GroupEdit.aspx?{0}", GroupDataSource);
		FormUtil.RedirectAfterAddNew(FormView1, "GroupEdit.aspx");
		FormUtil.RedirectAfterCancel(FormView1, "Group.aspx");
		FormUtil.SetDefaultMode(FormView1, "Id");
	}
	protected void GridViewStaff1_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("Id={0}", GridViewStaff1.SelectedDataKey.Values[0]);
		Response.Redirect("StaffEdit.aspx?" + urlParams, true);		
	}	
	protected void GridViewGroupRoles2_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("Id={0}", GridViewGroupRoles2.SelectedDataKey.Values[0]);
		Response.Redirect("GroupRolesEdit.aspx?" + urlParams, true);		
	}
    protected void FormView1_Load(object sender, EventArgs e)
    {
        if (FormView1.CurrentMode == FormViewMode.Insert)
        {
            TextBox tbCreateDate = (TextBox)FormView1.Row.FindControl("dataCreateDate");
            tbCreateDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            tbCreateDate.Enabled = false;
            
            TextBox tbUpdateDate = (TextBox)FormView1.Row.FindControl("dataUpdateDate");
            tbUpdateDate.Enabled = false;
        }
        else
        {
            TextBox tbCreateDate = (TextBox)FormView1.Row.FindControl("dataCreateDate");
            tbCreateDate.Enabled = false;

            TextBox tbUpdateDate = (TextBox)FormView1.Row.FindControl("dataUpdateDate");
            tbUpdateDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            tbUpdateDate.Enabled = false;
        }
    }
}


