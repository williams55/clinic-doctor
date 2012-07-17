using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using AppointmentSystem.Data;
public partial class Admin_Role_GroupRole : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void  gridGroupRole_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = WebCommon.GetAuthUsername();
        e.NewValues["CreateDate"] = e.NewValues["UpdateDate"] = DateTime.Now;
    }
    protected void gridGroupRole_OnRowUpdating(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = WebCommon.GetAuthUsername();
        e.NewValues["CreateDate"] = e.NewValues["UpdateDate"] = DateTime.Now;
    }
    protected void Gridrole_OnRowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        int id = int.Parse((sender as ASPxGridView).GetMasterRowKeyValue().ToString());
        
        e.NewValues["GroupId"] = id;
        e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = WebCommon.GetAuthUsername();
        e.NewValues["CreateDate"] = e.NewValues["UpdateDate"] = DateTime.Now;
    }
    protected void Gridrolegroup_OnBeforePerformDataSelect(object sender, EventArgs e)
    {
        var param = GroupRoleDatas.Parameters["WhereClause"];
        string ss = (sender as ASPxGridView).GetMasterRowKeyValue().ToString();
        if (param == null)
        {
            GroupRoleDatas.Parameters.Add("WhereClause"
                , String.Format("IsDisabled = 'false' AND GroupId = {0}", (sender as ASPxGridView).GetMasterRowKeyValue()));
        }
        else
        {
            param.DefaultValue = String.Format("IsDisabled = 'false' AND GroupId = {0}",
                                  (sender as ASPxGridView).GetMasterRowKeyValue());
        }

    }

}
