

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
#endregion

public partial class Admin_Staff : System.Web.UI.Page
{	
    protected void Page_Load(object sender, EventArgs e)
	{
		FormUtil.RedirectAfterUpdate(GridView1, "Staff.aspx?page={0}");
		FormUtil.SetPageIndex(GridView1, "page");
		FormUtil.SetDefaultButton((Button)GridViewSearchPanel1.FindControl("cmdSearch"));
    }

	protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("Id={0}", GridView1.SelectedDataKey.Values[0]);
		Response.Redirect("StaffEdit.aspx?" + urlParams, true);
	}

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //string ID = e.CommandArgument.ToString().Trim();
        //if (e.CommandName == "CustomDelete")
        //{

        //    TList<Appointment> listAppointment = DataRepository.AppointmentProvider.GetByStatusId((int.Parse(ID)));
        //    TList<Room> listRoom = DataRepository.RoomProvider.GetByStatusId((int.Parse(ID)));
        //    if (listAppointment.Count > 0 || listRoom.Count > 0)
        //    {
        //        Response.Write(@"<script language='javascript'>alert('Vui lòng xóa tất cả chi tiết.')</script>");

        //    }
        //    else
        //    {
        //        Status objStatus = new Status();
        //        objStatus.Id = int.Parse(ID);
        //        DataRepository.StatusProvider.Delete(objStatus);
        //    }
        //    GridView1.DataBind();
        //}
    }
}


