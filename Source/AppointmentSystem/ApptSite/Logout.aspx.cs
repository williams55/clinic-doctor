using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApptSite;
using System.Web.Security;

public partial class Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AccountSession.Session = null;
        //Response.Redirect(ResolveUrl("~/Login.aspx"));
        FormsAuthentication.RedirectToLoginPage();
    }
}
