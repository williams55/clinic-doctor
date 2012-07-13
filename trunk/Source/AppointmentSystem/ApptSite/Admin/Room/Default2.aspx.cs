﻿using System;
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
public partial class Admin_Room_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void gridRoom_RowInserting(object sender,ASPxDataInsertingEventArgs e)
    {
        e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = WebCommon.GetAuthUsername();
        e.NewValues["CreateDate"] = e.NewValues["UpdateDate"] = DateTime.Now;
       
    }
    protected void gridRoom_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
    { 
        e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = WebCommon.GetAuthUsername();
        e.NewValues["CreateDate"] = e.NewValues["UpdateDate"] = DateTime.Now;
    }
    protected void gridRoom_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        if (e.ButtonID != "btnDelete") return;
        Int64 id;
        if (Int64.TryParse(gridRoom.GetRowValues(e.VisibleIndex, "Id").ToString(), out id))
        {
           
            var obj = DataRepository.RoomProvider.GetById(int.Parse(id.ToString()));
            obj.IsDisabled = true;
            obj.UpdateUser = WebCommon.GetAuthUsername();
            obj.UpdateDate = DateTime.Now;
            DataRepository.RoomProvider.Update(obj);
        }
    }
    
}
