
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

public partial class StaffEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormUtil.RedirectAfterInsertUpdate(FormView1, "StaffEdit.aspx?{0}", StaffDataSource);
        FormUtil.RedirectAfterAddNew(FormView1, "StaffEdit.aspx");
        FormUtil.RedirectAfterCancel(FormView1, "Staff.aspx");
        FormUtil.SetDefaultMode(FormView1, "Id");
    }
    protected void GridViewDoctorRoom1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string urlParams = string.Format("Id={0}", GridViewDoctorRoom1.SelectedDataKey.Values[0]);
        Response.Redirect("DoctorRoomEdit.aspx?" + urlParams, true);
    }
    protected void GridViewAppointment2_SelectedIndexChanged(object sender, EventArgs e)
    {
        string urlParams = string.Format("Id={0}", GridViewAppointment2.SelectedDataKey.Values[0]);
        Response.Redirect("AppointmentEdit.aspx?" + urlParams, true);
    }
    protected void GridViewAppointment3_SelectedIndexChanged(object sender, EventArgs e)
    {
        string urlParams = string.Format("Id={0}", GridViewAppointment3.SelectedDataKey.Values[0]);
        Response.Redirect("AppointmentEdit.aspx?" + urlParams, true);
    }
    protected void GridViewDoctorRoster4_SelectedIndexChanged(object sender, EventArgs e)
    {
        string urlParams = string.Format("Id={0}", GridViewDoctorRoster4.SelectedDataKey.Values[0]);
        Response.Redirect("DoctorRosterEdit.aspx?" + urlParams, true);
    }
    protected void GridViewDoctorFunc5_SelectedIndexChanged(object sender, EventArgs e)
    {
        string urlParams = string.Format("Id={0}", GridViewDoctorFunc5.SelectedDataKey.Values[0]);
        Response.Redirect("DoctorFuncEdit.aspx?" + urlParams, true);
    }
    protected void FormView1_Load(object sender, EventArgs e)
    {
        CheckBoxList cbdataRoles = (CheckBoxList)FormView1.Row.FindControl("dataRoles");
        HiddenField hdhdRoles = (HiddenField)FormView1.Row.FindControl("hdRoles");
        string rolevalue = "";
        int counItem = cbdataRoles.Items.Count;

        if (FormView1.CurrentMode == FormViewMode.Insert)
        {
            HiddenField tbCreateDate = (HiddenField)FormView1.Row.FindControl("HDcreatedate");
            HiddenField tbUpdateDate = (HiddenField)FormView1.Row.FindControl("HDUpdateDate");

            tbCreateDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
            tbUpdateDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");

        }
        else
        {
            HiddenField tbUpdateDate = (HiddenField)FormView1.Row.FindControl("HDUpdateDate");
            tbUpdateDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
            //strikng[] userRole = role.GetRolesForUser(tbdataUserName.Text);
            //foreach (var item in userRole)
            //{
            //    foreach (ListItem listitem in cbdataRoles.Items)
            //    {
            //        if (listitem.Text == item.ToString())
            //            listitem.Selected = true;


            //    }
            //}
        }
    }
    protected void FormView1_ItemCommand(object sender, FormViewCommandEventArgs e)
    {
        string a = "";
    }
    protected void FormView1_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        //CheckBoxList cbdataRoles = (CheckBoxList)FormView1.Row.FindControl("dataRoles");
        //HiddenField hdhdRoles = (HiddenField)FormView1.Row.FindControl("hdRoles");
        //string rolevalue = "";
        //int counItem = cbdataRoles.Items.Count;
        //for (int i = 0; i < counItem; i++)
        //{
        //    string b = cbdataRoles.Items[i].ToString();
        //    if (cbdataRoles.Items[i].Selected == true)
        //    {
        //        rolevalue += cbdataRoles.Items[i].Text;
        //        if (i < counItem - 2)
        //            rolevalue += ";";

        //    }
        //}
        //hdhdRoles.Value = rolevalue;
    }
    protected void FormView1_ItemCreated(object sender, EventArgs e)
    {

    }
    protected void FormView1_DataBinding(object sender, EventArgs e)
    {
        
    }
    protected void FormView1_DataBound(object sender, EventArgs e)
    {
        CheckBoxList cbdataRoles = (CheckBoxList)FormView1.Row.FindControl("dataRoles");
        TextBox tbdataUserName = (TextBox)FormView1.Row.FindControl("dataUserName");
        CustomRoleProvider role = new CustomRoleProvider();
        string[] listRoles = role.GetAllRoles();
        //cbdataRoles.Items.Clear();
        foreach (var item in listRoles)
        {
            cbdataRoles.Items.Add(item.ToString());
        }
        if (FormView1.CurrentMode == FormViewMode.Edit)
        {
            listRoles = role.GetRolesForUser(tbdataUserName.Text);
            foreach (var item in listRoles)
            {
                (cbdataRoles.Items.FindByValue(item)).Selected=true;
            }
        }
    }
}


