using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppointmentSystem.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.Data;

public partial class Admin_Status_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void grid_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["UpdateUser"] = WebCommon.GetAuthUsername();
        e.NewValues["UpdateDate"] = DateTime.Now;
        e.NewValues["IsDisabled"] = true;
    }
}
