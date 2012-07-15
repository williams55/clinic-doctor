using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppointmentSystem.Data;
using AppointmentSystem.Entities;
using AppointmentSystem.Settings.BusinessLayer;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.Data;
using DevExpress.Web.ASPxEditors;

public partial class Admin_Role_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Gridroledetail_OnBeforePerformDataSelect(object sender, EventArgs e)
    {
        var param = RoledetailDataS.Parameters["WhereClause"];
        string ss = (sender as ASPxGridView).GetMasterRowKeyValue().ToString();
        if (param == null)
        {
            RoledetailDataS.Parameters.Add("WhereClause"
                , String.Format("IsDisabled = 'false' AND RoleId = {0}", (sender as ASPxGridView).GetMasterRowKeyValue()));
        }
        else
        {
            param.DefaultValue = String.Format("IsDisabled = 'false' AND UnitId = {0}",
                                  (sender as ASPxGridView).GetMasterRowKeyValue());
        }
    }
    protected void gridRole_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        if (e.ButtonID != "btnDelete") return;
        long id;
        if (Int64.TryParse(gridRole.GetRowValues(e.VisibleIndex, "Id").ToString(), out id))
        {

            var obj = (Role)DataRepository.RoleProvider.GetById(int.Parse(id.ToString()));
            obj.IsDisabled = true;
            obj.UpdateUser = WebCommon.GetAuthUsername();
            obj.UpdateDate = DateTime.Now;
            DataRepository.RoleProvider.Update(obj);
        }
    }
    protected void gridRole_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = WebCommon.GetAuthUsername();
        e.NewValues["CreateDate"] = e.NewValues["UpdateDate"] = DateTime.Now;
    }
    protected void gridRole_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = WebCommon.GetAuthUsername();
        e.NewValues["CreateDate"] = e.NewValues["UpdateDate"] = DateTime.Now;
    }
    protected void Gridroledetail_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = WebCommon.GetAuthUsername();
        e.NewValues["CreateDate"] = e.NewValues["UpdateDate"] = DateTime.Now;
    }
    protected void Gridroledetail_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = WebCommon.GetAuthUsername();
        e.NewValues["CreateDate"] = e.NewValues["UpdateDate"] = DateTime.Now;
    }
}
