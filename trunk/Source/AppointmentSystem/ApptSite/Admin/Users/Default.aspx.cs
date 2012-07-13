using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Diagnostics;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AppointmentSystem.Data;
using AppointmentSystem.Entities;
using AppointmentSystem.Settings.BusinessLayer;

public partial class Admin_Users_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void gridUser_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        string perxe = "user" + DateTime.Now.ToString("ddMMyy");
        int count = 0;
        TList<Users> objuser = DataRepository.UsersProvider.GetPaged("Id like '" + perxe + "' + '%'", "Id desc", 0, 1, out count);
        if (count == 0)
        {
            e.NewValues["Id"] = perxe + "001";
        }
        else { 
            e.NewValues["Id"]=perxe+string.Format("{0:000}",int.Parse(objuser[0].Id.Substring(objuser[0].Id.Length-3))+1);
        }
    }
}
