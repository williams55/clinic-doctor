using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.Data;

public partial class Admin_AppointmentGroup_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void gridRoom_RowInserting(object sender, ASPxDataInsertingEventArgs e)
    {
        e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = WebCommon.GetAuthUsername();
        e.NewValues["CreateDate"] = e.NewValues["UpdateDate"] = DateTime.Now;
    }
    protected void gridRoom_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        //if (e.ButtonID != "btnDelete") return;
        //long id;
        //if (Int64.TryParse( gridRoom.GetRowValues(e.VisibleIndex, "Id").ToString(), out id))
        //{
        //   // var abc = DataRepository.RoomProvider.GetById(id);
        //    var obj =(Room)DataRepository.RoomProvider.GetById(id);
        //    obj.IsDisabled = true;
        //    obj.UpdateUser = WebCommon.GetAuthUsername();
        //    obj.UpdateDate = DateTime.Now;
        //    DataRepository.RoomProvider.Update(obj);
        //}
    }
    protected void detailGrid_DataSelect(object sender, EventArgs e)
    {
        var param = AppointmentGroupDataSource.Parameters["WhereClause"];
        if (param==null)
        {
            AppointmentGroupDataSource.Parameters.Add("WhereClause"
                , String.Format("IsDisabled = 'false' AND UnitId = {0}", (sender as ASPxGridView).GetMasterRowKeyValue()));
        }
        else
        {
            param.DefaultValue = String.Format("IsDisabled = 'false' AND UnitId = {0}",
                                  (sender as ASPxGridView).GetMasterRowKeyValue());
        }
    }
    protected void detailGrid_RowInserting(object sender, ASPxDataInsertingEventArgs e)
    {
        e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = WebCommon.GetAuthUsername();
        e.NewValues["CreateDate"] = e.NewValues["UpdateDate"] = DateTime.Now;
        e.NewValues["UnitId"] = (sender as ASPxGridView).GetMasterRowKeyValue();
    }
    protected void chkSingleExpanded_CheckedChanged(object sender, EventArgs e)
    {
        string abc = "â";
        abc += "fassfa";
        //grid.SettingsDetail.AllowOnlyOneMasterRowExpanded = chkSingleExpanded.Checked;
        //if (grid.SettingsDetail.AllowOnlyOneMasterRowExpanded)
        //{
        //    grid.DetailRows.CollapseAllRows();
        //}
    }
}
