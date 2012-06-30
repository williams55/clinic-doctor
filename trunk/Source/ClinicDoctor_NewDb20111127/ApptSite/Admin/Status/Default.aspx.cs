using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClinicDoctor.Data;
using ClinicDoctor.Entities;
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
    protected void grid_RowInserting(object sender, ASPxDataInsertingEventArgs e)
    {
        e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = WebCommon.GetAuthUsername();
        e.NewValues["CreateDate"] = e.NewValues["UpdateDate"] = DateTime.Now;
    }
    protected void grid_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        if (e.ButtonID != "btnDelete") return;
        long id;
        if (Int64.TryParse(grid.GetRowValues(e.VisibleIndex, "Id").ToString(), out id))
        {
            var obj = DataRepository.StatusProvider.GetById(id);
            obj.IsDisabled = true;
            obj.UpdateUser = WebCommon.GetAuthUsername();
            obj.UpdateDate = DateTime.Now;
            DataRepository.StatusProvider.Update(obj);
        }
    }
}
