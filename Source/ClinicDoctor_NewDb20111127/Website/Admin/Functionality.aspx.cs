

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

public partial class Admin_Functionality : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormUtil.RedirectAfterUpdate(GridView1, "Functionality.aspx?page={0}");
        FormUtil.SetPageIndex(GridView1, "page");
        FormUtil.SetDefaultButton((Button)GridViewSearchPanel1.FindControl("cmdSearch"));
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string urlParams = string.Format("Id={0}", GridView1.SelectedDataKey.Values[0]);
        Response.Redirect("FunctionalityEdit.aspx?" + urlParams, true);
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string ID = e.CommandArgument.ToString().Trim();
        if (e.CommandName == "CustomDelete")
        {

            TList<ClinicDoctor.Entities.Content> listContent = DataRepository.ContentProvider.GetByFuncId((int.Parse(ID)));
            TList<DoctorFunc> listDoctorFunc = DataRepository.DoctorFuncProvider.GetByFuncId((int.Parse(ID)));
            TList<RoomFunc> listRoomFunc = DataRepository.RoomFuncProvider.GetByFuncId((int.Parse(ID)));
            if (listContent.Count > 0 || listDoctorFunc.Count >0 || listRoomFunc.Count>0)
            {
                Response.Write(@"<script language='javascript'>alert('Vui lòng xóa tất cả chi tiết.')</script>");

            }
            else
            {
                Functionality objFunc = new Functionality();
                objFunc.Id = int.Parse(ID);
                DataRepository.FunctionalityProvider.Delete(objFunc);
            }
            GridView1.DataBind();
        }
    }
}


