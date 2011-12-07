﻿
#region Imports...
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClinicDoctor.Web.UI;
#endregion

public partial class FunctionalityEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormUtil.RedirectAfterInsertUpdate(FormView1, "FunctionalityEdit.aspx?{0}", FunctionalityDataSource);
        FormUtil.RedirectAfterAddNew(FormView1, "FunctionalityEdit.aspx");
        FormUtil.RedirectAfterCancel(FormView1, "Functionality.aspx");
        FormUtil.SetDefaultMode(FormView1, "Id");
    }
    protected void GridViewDoctorFunc1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string urlParams = string.Format("Id={0}", GridViewDoctorFunc1.SelectedDataKey.Values[0]);
        Response.Redirect("DoctorFuncEdit.aspx?" + urlParams, true);
    }
    protected void GridViewContent2_SelectedIndexChanged(object sender, EventArgs e)
    {
        string urlParams = string.Format("Id={0}", GridViewContent2.SelectedDataKey.Values[0]);
        Response.Redirect("ContentEdit.aspx?" + urlParams, true);
    }
    protected void GridViewRoomFunc3_SelectedIndexChanged(object sender, EventArgs e)
    {
        string urlParams = string.Format("Id={0}", GridViewRoomFunc3.SelectedDataKey.Values[0]);
        Response.Redirect("RoomFuncEdit.aspx?" + urlParams, true);
    }
    protected void FormView1_Load(object sender, EventArgs e)
    {
        string userName = Session["UserName"].ToString();
        if (FormView1.CurrentMode == FormViewMode.Insert)
        {
            HiddenField hdCreateDate = (HiddenField)FormView1.Row.FindControl("HDcreatedate");
            HiddenField hdUpdateDate = (HiddenField)FormView1.Row.FindControl("HDUpdateDate");
            HiddenField hdCreateUser = (HiddenField)FormView1.Row.FindControl("hdcreateUser");
            HiddenField hdUpdateUser = (HiddenField)FormView1.Row.FindControl("hdUpdateUser");
            hdCreateDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
            hdUpdateDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
            hdCreateUser.Value = userName;
            hdUpdateUser.Value = userName;

        }
        else
        {
            HiddenField hdUpdateDate = (HiddenField)FormView1.Row.FindControl("HDUpdateDate");
            hdUpdateDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
            HiddenField hdUpdateUser = (HiddenField)FormView1.Row.FindControl("hdUpdateUser");
            hdUpdateUser.Value = userName;

        }
    }
}

