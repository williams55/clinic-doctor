
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
using ClinicDoctor.Settings.BusinessLayer;
using ClinicDoctor.Entities;
using ClinicDoctor.Data;
#endregion

public partial class CustomerEdit : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{		
		FormUtil.RedirectAfterInsertUpdate(FormView1, "CustomerEdit.aspx?{0}", CustomerDataSource);
		FormUtil.RedirectAfterAddNew(FormView1, "CustomerEdit.aspx");
		FormUtil.RedirectAfterCancel(FormView1, "Customer.aspx");
		FormUtil.SetDefaultMode(FormView1, "Id");
	}
	protected void GridViewAppointment1_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("Id={0}", GridViewAppointment1.SelectedDataKey.Values[0]);
		Response.Redirect("AppointmentEdit.aspx?" + urlParams, true);		
	}

    protected void FormView1_Load(object sender, EventArgs e)
    {
        string userName = Session["UserName"].ToString();
        if (FormView1.CurrentMode == FormViewMode.Insert)
        {
            TextBox tbID = (TextBox)FormView1.Row.FindControl("dataId");
            HiddenField hdCreateDate = (HiddenField)FormView1.Row.FindControl("HDcreatedate");
            HiddenField hdUpdateDate = (HiddenField)FormView1.Row.FindControl("HDUpdateDate");
            HiddenField hdCreateUser = (HiddenField)FormView1.Row.FindControl("hdcreateUser");
            HiddenField hdUpdateUser = (HiddenField)FormView1.Row.FindControl("hdUpdateUser");
            ServiceFacade.SettingsHelper.CustomerPrefix = "";
            string Perfix = ServiceFacade.SettingsHelper.CustomerPrefix + DateTime.Now.ToString("yyMMdd");
            int Count = 0;
            TList<Customer> objCustomer = DataRepository.CustomerProvider.GetPaged("Id like '" + Perfix + "' + '%'", "Id desc", 0, 1, out Count);
            if (Count == 0)
                tbID.Text = Perfix + "-" + "001";
            else
            {
                tbID.Text = Perfix + "-" + String.Format("{0:000}", int.Parse(objCustomer[0].Id.Substring((objCustomer[0].Id.Length - 3))) + 1);
            }

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


