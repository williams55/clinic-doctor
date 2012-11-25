using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppointmentSystem.Data;
using ApptSite;

public partial class DeniedPage : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            var user = DataRepository.UsersProvider.GetByUsername(AccountSession.Session);
            if (user != null && !user.IsDisabled)
            {
                Response.Redirect("~/");
            }
        }
        catch (Exception ex)
        {
        }
    }
    #endregion
}
