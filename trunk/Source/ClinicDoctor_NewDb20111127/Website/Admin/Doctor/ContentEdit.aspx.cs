
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
using ClinicDoctor.Data;
using Telerik.Web.UI;
using ClinicDoctor.Entities;
using Telerik.Web.UI;
#endregion

public partial class ContentEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //FormUtil.RedirectAfterAddNew(FormView1, "ContentEdit.aspx");
        //FormUtil.RedirectAfterCancel(FormView1, "Content.aspx");
        //FormUtil.SetDefaultMode(FormView1, "Id");
    }

    //protected void FormView1_Load(object sender, EventArgs e)
    //{
    //    string userName = Session["UserName"].ToString();
    //    if (FormView1.CurrentMode == FormViewMode.Insert)
    //    {
    //        HiddenField hdCreateDate = (HiddenField)FormView1.Row.FindControl("HDcreatedate");
    //        HiddenField hdUpdateDate = (HiddenField)FormView1.Row.FindControl("HDUpdateDate");
    //        HiddenField hdCreateUser = (HiddenField)FormView1.Row.FindControl("hdcreateUser");
    //        HiddenField hdUpdateUser = (HiddenField)FormView1.Row.FindControl("hdUpdateUser");
    //        hdCreateDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
    //        hdUpdateDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
    //        hdCreateUser.Value = userName;
    //        hdUpdateUser.Value = userName;

    //    }
    //    else
    //    {
    //        HiddenField hdUpdateDate = (HiddenField)FormView1.Row.FindControl("HDUpdateDate");
    //        hdUpdateDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
    //        HiddenField hdUpdateUser = (HiddenField)FormView1.Row.FindControl("hdUpdateUser");
    //        hdUpdateUser.Value = userName;

    //    }
    //}
 
    protected void RaddataFuncId_ItemsRequested(object o, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
    {
        TList<ClinicDoctor.Entities.Functionality> listResult = DataRepository.FunctionalityProvider.GetAll();
        DataSet ds = new DataSet();
        ds = listResult.ToDataSet(true);
        DataTable dataTable = ds.Tables[0];

        foreach (DataRow dataRow in dataTable.Rows)
        {
            RadComboBoxItem item = new RadComboBoxItem();

            item.Text = (string)dataRow["FuncTitle"];
            item.Value = dataRow["Id"].ToString();

            string Id = dataRow["ID"].ToString();
          

            item.Attributes.Add("Id",Id);
          
            RaddataFuncId.Items.Add(item);

            item.DataBind();
        }
    }
}


