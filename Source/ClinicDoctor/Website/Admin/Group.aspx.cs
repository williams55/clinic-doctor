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

public partial class Admin_Group : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormUtil.RedirectAfterUpdate(GridView1, "Group.aspx?page={0}");
        FormUtil.SetPageIndex(GridView1, "page");
        FormUtil.SetDefaultButton((Button)GridViewSearchPanel1.FindControl("cmdSearch"));
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string urlParams = string.Format("Id={0}", GridView1.SelectedDataKey.Values[0]);
        Response.Redirect("GroupEdit.aspx?" + urlParams, true);
    }
}
