

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
        string ID = e.CommandArgument.ToString().Trim();
        if (e.CommandName == "CustomDelete")
        {

            TList<Appointment> listAppointmentDoctor = DataRepository.AppointmentProvider.GetByDoctorId((int.Parse(ID)));
            TList<Appointment> listAppointmentNurse = DataRepository.AppointmentProvider.GetByNurseId((int.Parse(ID)));
            TList<DoctorRoom> listDoctorRoom = DataRepository.DoctorRoomProvider.GetByDoctorId((int.Parse(ID)));
            TList<DoctorFunc> listDoctorFunc = DataRepository.DoctorFuncProvider.GetByDoctorId((int.Parse(ID)));
            TList<DoctorRoster> listDoctorRoster = DataRepository.DoctorRosterProvider.GetByDoctorId((int.Parse(ID)));
            if (listAppointmentDoctor.Count > 0 || listAppointmentNurse.Count > 0 || listDoctorRoom.Count > 0 || listDoctorFunc.Count > 0 || listDoctorRoster.Count>0)
            {
                Response.Write(@"<script language='javascript'>alert('Vui lòng xóa tất cả chi tiết.')</script>");

            }
            else
            {
                Staff objStaff = new Staff();
                objStaff.Id = int.Parse(ID);
                DataRepository.StaffProvider.Delete(objStaff);
            }
            GridView1.DataBind();
        }
    }
}


