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

public partial class MasterPage : System.Web.UI.MasterPage
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!AccountSession.IsLogin)
                Response.Redirect(ResolveUrl("~/Login.aspx") + "?ReturnUrl=" + ResolveUrl(Request.Url.AbsoluteUri));

            var user = DataRepository.UsersProvider.GetByUsername(AccountSession.Session);
            if (user == null || user.IsDisabled)
            {
                Response.Redirect(ResolveUrl("~/DeniedPage.aspx"));
            }
        }
        catch (Exception ex)
        {
        }
    }
    #endregion
}
