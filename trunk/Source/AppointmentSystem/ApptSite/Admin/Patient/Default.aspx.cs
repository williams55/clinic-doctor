using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.Data;
using Log.Controller;
using Common.Util;
using Appt.Common.Constants;
using AppointmentBusiness.Util;
using AppointmentSystem.Settings.BusinessLayer;
using AppointmentSystem.Entities;
using AppointmentSystem.Data;
using DevExpress.Web.ASPxUploadControl;
public partial class Admin_Patient_Default : System.Web.UI.Page
{
    string ScreenCode="Patient";
    static string _message;
    protected void Page_Load(object sender, EventArgs e)
    {
        try{
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Read.Key, out _message))
            {
                WebCommon.ShowDialog(this, _message, WebCommon.GetHomepageUrl(this));
                this.gridPatient.Visible = false;
                return;
            }
            var gridViewCommandColumn = gridPatient.Columns["btnCommand"] as GridViewCommandColumn;
            if (gridViewCommandColumn == null) return;
            gridViewCommandColumn.EditButton.Visible = RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(),
                                                                                  ScreenCode,
                                                                                  OperationConstant.Update.Key,
                                                                                  out _message);
            gridViewCommandColumn.NewButton.Visible = RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(),ScreenCode, OperationConstant.Create.Key,
                                                                                  out _message);
            GridViewCommandColumnCustomButton btnDelete = null;
            foreach (GridViewCommandColumnCustomButton customButton in gridViewCommandColumn.CustomButtons)
            {
                if (customButton.ID == "btnDelete")
                {
                    btnDelete = customButton;
                    break;
                }
            }
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(),
                                                                                  ScreenCode,
                                                                                  OperationConstant.Delete.Key,
                                                                                  out _message))
            {
                btnDelete.Visibility = GridViewCustomButtonVisibility.Invisible;
            }

            gridViewCommandColumn.Visible = gridViewCommandColumn.EditButton.Visible
                || gridViewCommandColumn.NewButton.Visible
                || (btnDelete.Visibility != GridViewCustomButtonVisibility.Invisible);
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
        }
    }
    protected void gridPatient_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        try
        {
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Create.Key,out _message))
            {
                e.Cancel = true;
                 
            }
            if (e.NewValues["LastName"].ToString().Trim().Length < 1)
            {
                ((ASPxGridView)sender).JSProperties[GeneralConstants.ApptMessage] = "Field Last name can not be empty";
                    e.Cancel = true;
                     
                
            }
            if (e.NewValues["FirstName"].ToString().Trim().Length < 1)
            {
                ((ASPxGridView)sender).JSProperties[GeneralConstants.ApptMessage] = "Field First name can not be empty";
                e.Cancel = true;
                return;

            }
            e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = WebCommon.GetAuthUsername();
            e.NewValues["CreateDate"] = e.NewValues["UpdateDate"] = DateTime.Now;
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
        }
    }
    protected void gridPatient_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        try
        {
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Create.Key,out _message))
            {
                e.Cancel = true;
                return;
            }
            if (e.NewValues["LastName"].ToString().Trim().Length < 1)
            {
                ((ASPxGridView)sender).JSProperties[GeneralConstants.ApptMessage] = "Field Last name can not be empty";
                e.Cancel = true;
            }
            if (e.NewValues["FirstName"].ToString().Trim().Length < 1)
            {
                ((ASPxGridView)sender).JSProperties[GeneralConstants.ApptMessage] = "Field First name can not be empty";
                e.Cancel = true;
                return;
            }
            e.NewValues["UpdateUser"] = WebCommon.GetAuthUsername();
            e.NewValues["UpdateDate"] = DateTime.Now;
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
        }
    }
    protected void gridPatient_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        string perxe = "PTC";
        int count = 0;
        TList<Patient> objuser = DataRepository.PatientProvider.GetPaged("PatientCode like '" + perxe + "' + '%'", "PatientCode desc", 0, 1, out count);
        if (count == 0)
        {
            e.NewValues["PatientCode"] = perxe + "0001";
        }
        else
        {
            e.NewValues["PatientCode"] = perxe + string.Format("{0:0000}", int.Parse(objuser[0].PatientCode.Substring(objuser[0].PatientCode.Length - 3)) + 1);
        }
    }
    protected void gridPatient_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        try
        {
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Delete.Key, out _message))
            {
                ((ASPxGridView)sender).JSProperties[GeneralConstants.ApptMessage]
                        = String.Format("You do not have permission delete", "Delete Patient");
                return;
            }
            if (e.ButtonID != "btnDelete") return;
            string id = gridPatient.GetRowValues(e.VisibleIndex, "PatientCode").ToString();
            var obj = DataRepository.PatientProvider.GetByPatientCode(id);
            if (obj == null || obj.IsDisabled) return;
            DataRepository.PatientProvider.DeepLoad(obj);
            if (obj.AppointmentCollection.Exists(x => !x.IsDisabled))
            {
                ((ASPxGridView)sender).JSProperties[GeneralConstants.ApptMessage] = String.Format("Dr. {0} is using, you cannot delete it.", obj.FirstName);
                return;
            }
            obj.IsDisabled = true;
            obj.UpdateUser = WebCommon.GetAuthUsername();
            obj.UpdateDate = DateTime.Now;
            DataRepository.PatientProvider.Update(obj);

        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
        }
    }
    protected void GridAppointment_BeforePerformDataSelect(object sender, EventArgs e)
    {
        var parameter = AppointmentDatas.Parameters["whereClause"];
        if (parameter == null)
        {

            this.AppointmentDatas.Parameters.Add("WhereClause", String.Format("IsDisabled = 'false' AND PatientId = '{0}'", (sender as ASPxGridView).GetMasterRowKeyValue()));
        }
        else
        {
            parameter.DefaultValue = String.Format("IsDisabled = 'false' AND PatientId = '{0}'",
                                  (sender as ASPxGridView).GetMasterRowKeyValue());
        }
    }
    protected void gridPatient_BeforePerformDataSelect(object sender, EventArgs e)
    {

    }
}
