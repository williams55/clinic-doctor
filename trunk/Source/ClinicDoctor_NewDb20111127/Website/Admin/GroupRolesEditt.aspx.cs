using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ClinicDoctor.Web.UI;

public partial class Admin_GroupRolesEditt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormUtil.RedirectAfterInsertUpdate(FormView1, "GroupRolesEdit.aspx?{0}", GroupRolesDataSource);
        FormUtil.RedirectAfterAddNew(FormView1, "GroupRolesEdit.aspx");
        FormUtil.RedirectAfterCancel(FormView1, "GroupRoles.aspx");
        FormUtil.SetDefaultMode(FormView1, "Id");
    }
}
