﻿

#region Using directives
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClinicDoctor.Web.UI;
using ClinicDoctor.Entities;
using ClinicDoctor.Data;
#endregion

public partial class Admin_DoctorRoom : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormUtil.RedirectAfterUpdate(GridView1, "DoctorRoom.aspx?page={0}");
        FormUtil.SetPageIndex(GridView1, "page");
        FormUtil.SetDefaultButton((Button)GridViewSearchPanel1.FindControl("cmdSearch"));
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string urlParams = string.Format("Id={0}", GridView1.SelectedDataKey.Values[0]);
        Response.Redirect("DoctorRoomEdit.aspx?" + urlParams, true);
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string ID = e.CommandArgument.ToString().Trim();
        if (e.CommandName == "CustomDelete")
        {
            DoctorRoom objDoctorRoom = new DoctorRoom();
            objDoctorRoom.Id = int.Parse(ID);
            DataRepository.DoctorRoomProvider.Delete(objDoctorRoom);
        }
        GridView1.DataBind();
    }

}


