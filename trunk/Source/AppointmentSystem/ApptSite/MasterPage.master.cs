﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppointmentSystem.Data;
using Common.Util;
using Log.Controller;

public partial class MasterPage : System.Web.UI.MasterPage
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            var user = DataRepository.UsersProvider.GetByUsername(EntitiesUtilities.GetAuthName());
            if (user == null || user.IsDisabled)
            {
                Response.Redirect("~/DeniedPage.aspx");
            }
        }
        catch (Exception ex)
        {
        }
    }
    #endregion
}