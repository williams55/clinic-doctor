using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppointmentBusiness.BO;
using ApptSite;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && AccountSession.IsLogin)
        {
            Response.Redirect(ResolveUrl("~/"));
        }
    }

    protected void btnNative_OnClick(object sender, EventArgs e)
    {
        var usn = txtUsn.Text.Trim();
        if (BoFactory.UserBO.Authentication(usn, txtPsw.Text))
        {
            AccountSession.Session = usn;
            Response.Redirect(ResolveUrl(Request["ReturnUrl"] ?? "~/"), false);
        }
        else
        {
            messageError.Visible = true;
        }
    }
}
