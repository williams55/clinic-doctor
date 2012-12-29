using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppointmentSystem.Data;
using ApptSite;
using Common.Util;
using Log.Controller;
using System.Web.Security;

public partial class MasterPage : System.Web.UI.MasterPage
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!AccountSession.IsLogin)
                //Response.Redirect(ResolveUrl("~/Login.aspx") + "?ReturnUrl=" + ResolveUrl(Request.Url.AbsoluteUri));
                FormsAuthentication.RedirectToLoginPage();
            else
            {
                var user = DataRepository.UsersProvider.GetByUsername(AccountSession.Session);
                //if (user == null || user.IsDisabled)
                //{
                //    Response.Redirect(ResolveUrl("~/DeniedPage.aspx"));
                //}
                lbl_Username.Text = user.DisplayName;
            }
        }
        catch (Exception ex)
        {
        }
    }
    #endregion
    protected void btn_Logout_Click(object sender, EventArgs e)
    {
        AccountSession.Session = null;
        FormsAuthentication.RedirectToLoginPage();
    }
}
