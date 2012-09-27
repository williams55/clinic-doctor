using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
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
    string ScreenCode = "Patient";
    static string _message;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Read.Key, out _message))
            {
                WebCommon.ShowDialog(this, _message, WebCommon.GetHomepageUrl(this));
                gridPatient.Visible = false;
                return;
            }

            // Set page size
            gridPatient.SettingsPager.PageSize = ServiceFacade.SettingsHelper.PageSize;

            var gridViewCommandColumn = gridPatient.Columns["btnCommand"] as GridViewCommandColumn;
            if (gridViewCommandColumn == null) return;
            gridViewCommandColumn.EditButton.Visible = RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(),
                                                                                  ScreenCode,
                                                                                  OperationConstant.Update.Key,
                                                                                  out _message);
            btnAdd.Visible = RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Create.Key, out _message);
            GridViewCommandColumnCustomButton btnDelete =
                gridViewCommandColumn.CustomButtons.Cast<GridViewCommandColumnCustomButton>().FirstOrDefault(
                    customButton => customButton.ID == "btnDelete");
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
    protected void gridPatient_RowInserting(object sender, ASPxDataInsertingEventArgs e)
    {
        try
        {
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Create.Key, out _message))
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
    protected void gridPatient_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
    {
        try
        {
            // Check update right
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Update.Key, out _message))
            {
                e.Cancel = true;
                return;
            }

            // Get grid
            var grid = sender as ASPxGridView;
            if (grid == null)
            {
                WebCommon.AlertGridView(sender, "Cannot find patient.");
                e.Cancel = true;
                return;
            }

            // Get control and validate
            var radMale = grid.FindEditFormTemplateControl("radMale") as ASPxRadioButton;
            var radFemale = grid.FindEditFormTemplateControl("radFemale") as ASPxRadioButton;

            if (radMale == null || radFemale == null)
            {
                WebCommon.AlertGridView(sender, "Edit form is error.");
                e.Cancel = true;
                return;
            }

            // Validate empty field
            if (!WebCommon.ValidateEmpty("Patient Code", e.NewValues["PatientCode"], out _message)
                || !WebCommon.ValidateEmpty("First Name", e.NewValues["FirstName"], out _message)
                || !WebCommon.ValidateEmpty("Last Name", e.NewValues["LastName"], out _message)
                || !WebCommon.ValidateEmpty("DOB", e.NewValues["DateOfBirth"], out _message))
            {
                WebCommon.AlertGridView(sender, _message);
                e.Cancel = true;
                return;
            }

            // Validate empty field
            DateTime dtDOB;
            if (!DateTime.TryParse(e.NewValues["DateOfBirth"].ToString(), out dtDOB))
            {
                WebCommon.AlertGridView(sender, _message);
                e.Cancel = true;
                return;
            }

            // Get patient by Patient Code
            var patients = DataRepository.VcsPatientProvider.GetByPatientCode(e.NewValues["PatientCode"].ToString());
            if (patients == null || !patients.Any() || patients[0].IsDisabled)
            {
                WebCommon.AlertGridView(sender, "Cannot find patient.");
                e.Cancel = true;
                return;
            }

            var patient = patients[0];
            patient.FirstName = e.NewValues["FirstName"].ToString();
            patient.MiddleName = e.NewValues["MiddleName"].ToString();
            patient.LastName = e.NewValues["LastName"].ToString();
            patient.Sex = radMale.Checked
                              ? SexConstant.Male.Value
                              : radFemale.Checked ? SexConstant.Female.Value : string.Empty;
            patient.MemberType = e.NewValues["MemberType"].ToString();
            patient.DateOfBirth = dtDOB;
            patient.Nationality = e.NewValues["Nationality"].ToString();

            patient.HomeStreet = e.NewValues["HomeStreet"].ToString();
            patient.HomeWard = e.NewValues["HomeWard"].ToString();
            patient.HomeDistrict = e.NewValues["HomeDistrict"].ToString();
            patient.HomeCity = e.NewValues["HomeCity"].ToString();
            patient.HomeCountry = e.NewValues["HomeCountry"].ToString();
            patient.HomePhone = e.NewValues["HomePhone"].ToString();
            patient.MobilePhone = e.NewValues["MobilePhone"].ToString();
            patient.BillingAddress = e.NewValues["BillingAddress"].ToString();

            patient.WorkStreet = e.NewValues["WorkStreet"].ToString();
            patient.WorkWard = e.NewValues["WorkWard"].ToString();
            patient.WorkDistrict = e.NewValues["WorkDistrict"].ToString();
            patient.WorkCity = e.NewValues["WorkCity"].ToString();
            patient.WorkCountry = e.NewValues["WorkCountry"].ToString();
            patient.CompanyPhone = e.NewValues["CompanyPhone"].ToString();
            patient.CompanyCode = e.NewValues["CompanyCode"].ToString();
            patient.Fax = e.NewValues["Fax"].ToString();

            patient.Remark = e.NewValues["Remark"].ToString();
            patient.UpdateUser = WebCommon.GetAuthUsername();
            patient.UpdateDate = DateTime.Now;
            DataRepository.VcsPatientProvider.Update(patient.PatientCode, patient.FirstName, patient.MiddleName, patient.LastName
                , patient.DateOfBirth, patient.Sex, patient.MemberType, patient.Nationality, patient.HomeStreet, patient.HomeWard
                , patient.HomeDistrict, patient.HomeCity, patient.HomeCountry, patient.WorkStreet, patient.WorkWard, patient.WorkDistrict
                , patient.WorkCity, patient.WorkCountry, patient.CompanyCode, patient.BillingAddress, patient.HomePhone, patient.MobilePhone
                , patient.CompanyPhone, patient.Fax, patient.EmailAddress, patient.CreateDate, patient.UpdateUser, patient.UpdateDate
                , patient.Remark);

            // Doan nay dung de fake cancel update
            var gridView = (ASPxGridView)sender;
            gridView.CancelEdit();
            WebCommon.AlertGridView(sender, "Patient is updated successfully.");
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            WebCommon.AlertGridView(sender, "Cannot update patient. Please contact Administrator");
        }
        e.Cancel = true;
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

            this.AppointmentDatas.Parameters.Add("WhereClause", String.Format("IsDisabled = 'false' AND PatientCode = '{0}'", (sender as ASPxGridView).GetMasterRowKeyValue()));
        }
        else
        {
            parameter.DefaultValue = String.Format("IsDisabled = 'false' AND PatientCode = '{0}'",
                                  (sender as ASPxGridView).GetMasterRowKeyValue());
        }
    }
    protected void gridPatient_BeforePerformDataSelect(object sender, EventArgs e)
    {

    }
}
