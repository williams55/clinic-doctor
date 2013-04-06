﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApptSite;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.Data;
using Common.Util;
using Appt.Common.Constants;
using AppointmentBusiness.Util;
using AppointmentSystem.Settings.BusinessLayer;
using AppointmentSystem.Entities;
using AppointmentSystem.Data;
using DevExpress.Web.ASPxUploadControl;
using Logger.Controller;

public partial class Admin_Patient_Default : System.Web.UI.Page
{
    string ScreenCode = "Patient";
    static string _message;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!RightAccess.CheckUserRight(AccountSession.Session, ScreenCode, OperationConstant.Read.Key, out _message))
            {
                WebCommon.ShowDialog(this, _message, WebCommon.GetHomepageUrl(this));
                gridPatient.Visible = false;
                return;
            }

            // Set page size
            gridPatient.SettingsPager.PageSize = ServiceFacade.SettingsHelper.PageSize;

            var gridViewCommandColumn = gridPatient.Columns["btnCommand"] as GridViewCommandColumn;
            if (gridViewCommandColumn == null) return;

            // Set hien thi/an cho nut edit cua tung row
            gridViewCommandColumn.EditButton.Visible = RightAccess.CheckUserRight(AccountSession.Session,
                                                                                  ScreenCode,
                                                                                  OperationConstant.Update.Key,
                                                                                  out _message);

            // Set hien thi/an cho nut new
            btnAdd.Visible = RightAccess.CheckUserRight(AccountSession.Session, ScreenCode,
                                                        OperationConstant.Create.Key, out _message);
          
            // Set hien thi/an cho nut delete
            btnGeneralDelete.Visible = RightAccess.CheckUserRight(AccountSession.Session,
                                                                                  ScreenCode,
                                                                                  OperationConstant.Delete.Key,
                                                                                  out _message);

            // Set hien thi/an cho nut delete cua tung row
            GridViewCommandColumnCustomButton btnDelete =
                gridViewCommandColumn.CustomButtons.Cast<GridViewCommandColumnCustomButton>().FirstOrDefault(
                    customButton => customButton.ID == "btnDelete");
            if (!btnGeneralDelete.Visible)
            {
                if (btnDelete != null) btnDelete.Visibility = GridViewCustomButtonVisibility.Invisible;
            }

            // Set hien thi/an cho cot button
            gridViewCommandColumn.Visible = gridViewCommandColumn.EditButton.Visible
                || gridViewCommandColumn.NewButton.Visible
                || btnGeneralDelete.Visible;
        }
        catch (Exception ex)
        {
            LoggerController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
        }
    }
  
    /// <summary>
    /// Them moi mot patient
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridPatient_RowInserting(object sender, ASPxDataInsertingEventArgs e)
    {
        try
        {
            if (!RightAccess.CheckUserRight(AccountSession.Session, ScreenCode, OperationConstant.Create.Key, out _message))
            {
                e.Cancel = true;

            }

            // Get grid
            var grid = sender as ASPxGridView;
            if (grid == null)
            {
                WebCommon.AlertGridView(sender, "Add form is error.");
                e.Cancel = true;
                return;
            }

            // Get control and validate
            var radMale = grid.FindEditFormTemplateControl("radMale") as ASPxRadioButton;
            var radFemale = grid.FindEditFormTemplateControl("radFemale") as ASPxRadioButton;

            if (radMale == null || radFemale == null)
            {
                WebCommon.AlertGridView(sender, "Add form is error.");
                e.Cancel = true;
                return;
            }

            // Validate empty field
            if (!WebCommon.ValidateEmpty("First Name", e.NewValues["FirstName"], out _message)
                || !WebCommon.ValidateEmpty("Last Name", e.NewValues["LastName"], out _message)
                || !WebCommon.ValidateEmpty("DOB", e.NewValues["DateOfBirth"], out _message)
                || !WebCommon.ValidateEmpty("Nationality", e.NewValues["Nationality"], out _message)
                || !WebCommon.ValidateEmpty("Mobile Phone", e.NewValues["MobilePhone"], out _message))
            {
                WebCommon.AlertGridView(sender, _message);
                e.Cancel = true;
                return;
            }

            // Validate field
            DateTime dtDOB;
            if (!DateTime.TryParse(e.NewValues["DateOfBirth"].ToString(), out dtDOB))
            {
                WebCommon.AlertGridView(sender, _message);
                e.Cancel = true;
                return;
            }

            var patient = new VcsPatient
                {
                    PatientCode = ServiceFacade.SettingsHelper.LocationCode,
                    FirstName = e.NewValues["FirstName"] == null ? null : e.NewValues["FirstName"].ToString(),
                    MiddleName = e.NewValues["MiddleName"] == null ? null : e.NewValues["MiddleName"].ToString(),
                    LastName = e.NewValues["LastName"] == null ? null : e.NewValues["LastName"].ToString(),
                    Sex = radMale.Checked
                              ? SexConstant.Male.Value
                              : radFemale.Checked ? SexConstant.Female.Value : string.Empty,
                    DateOfBirth = dtDOB,
                    Nationality = e.NewValues["Nationality"] == null ? null : e.NewValues["Nationality"].ToString(),
                    HomePhone = e.NewValues["HomePhone"] == null ? null : e.NewValues["HomePhone"].ToString(),
                    MobilePhone = e.NewValues["MobilePhone"] == null ? null : e.NewValues["MobilePhone"].ToString(),
                    CompanyCode = e.NewValues["CompanyCode"] == null ? null : e.NewValues["CompanyCode"].ToString(),
                    ApptRemark = e.NewValues["ApptRemark"] == null ? null : e.NewValues["ApptRemark"].ToString(),
                    UpdateUser = AccountSession.Session,
                };

            var patients = DataRepository.VcsPatientProvider.Insert(patient.PatientCode, patient.FirstName, patient.MiddleName
                , patient.LastName, patient.DateOfBirth, patient.Sex, patient.Nationality, patient.CompanyCode, patient.HomePhone
                , patient.MobilePhone, patient.UpdateUser, patient.ApptRemark);

            // Doan nay dung de fake cancel update
            var gridView = (ASPxGridView)sender;
            gridView.CancelEdit();
            WebCommon.AlertGridView(sender, "Patient is added successfully.");
        }
        catch (Exception ex)
        {
            LoggerController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            WebCommon.AlertGridView(sender, "Cannot add patient. Please contact Administrator");
        }
        e.Cancel = true;
    }
    
    /// <summary>
    /// Cap nhat thong tin mot patient
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridPatient_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
    {
        try
        {
            // Check update right
            if (!RightAccess.CheckUserRight(AccountSession.Session, ScreenCode, OperationConstant.Update.Key, out _message))
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
            if (!WebCommon.ValidateEmpty("First Name", e.NewValues["FirstName"], out _message)
                || !WebCommon.ValidateEmpty("Last Name", e.NewValues["LastName"], out _message)
                || !WebCommon.ValidateEmpty("DOB", e.NewValues["DateOfBirth"], out _message)
                || !WebCommon.ValidateEmpty("Nationality", e.NewValues["Nationality"], out _message)
                || !WebCommon.ValidateEmpty("Mobile Phone", e.NewValues["MobilePhone"], out _message))
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
            var patients = DataRepository.VcsPatientProvider.GetByPatientCode(e.OldValues["PatientCode"].ToString());
            if (patients == null || !patients.Any() || patients[0].IsDisabled)
            {
                WebCommon.AlertGridView(sender, "Cannot find patient.");
                e.Cancel = true;
                return;
            }

            var patient = patients[0];
            patient.FirstName = e.NewValues["FirstName"] == null ? null : e.NewValues["FirstName"].ToString();
            patient.MiddleName = e.NewValues["MiddleName"] == null ? null : e.NewValues["MiddleName"].ToString();
            patient.LastName = e.NewValues["LastName"] == null ? null : e.NewValues["LastName"].ToString();
            patient.Sex = radMale.Checked
                       ? SexConstant.Male.Value
                       : radFemale.Checked ? SexConstant.Female.Value : string.Empty;
            patient.DateOfBirth = dtDOB;
            patient.Nationality = e.NewValues["Nationality"] == null ? null : e.NewValues["Nationality"].ToString();
            patient.HomePhone = e.NewValues["HomePhone"] == null ? null : e.NewValues["HomePhone"].ToString();
            patient.MobilePhone = e.NewValues["MobilePhone"] == null ? null : e.NewValues["MobilePhone"].ToString();
            patient.CompanyCode = e.NewValues["CompanyCode"] == null ? null : e.NewValues["CompanyCode"].ToString();
            patient.ApptRemark = e.NewValues["ApptRemark"] == null ? null : e.NewValues["ApptRemark"].ToString();
            patient.UpdateUser = AccountSession.Session;
            DataRepository.VcsPatientProvider.Update(patient.PatientCode, patient.FirstName, patient.MiddleName, patient.LastName
                , patient.DateOfBirth, patient.Sex, patient.Nationality, patient.CompanyCode, patient.HomePhone, patient.MobilePhone
                , patient.UpdateUser, patient.ApptRemark, patient.IsDisabled);

            // Doan nay dung de fake cancel update
            var gridView = (ASPxGridView)sender;
            gridView.CancelEdit();
            WebCommon.AlertGridView(sender, "Patient is updated successfully.");
        }
        catch (Exception ex)
        {
            LoggerController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            WebCommon.AlertGridView(sender, "Cannot update patient. Please contact Administrator");
        }
        e.Cancel = true;
    }

    /// <summary>
    /// Xoa mot patient
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridPatient_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        try
        {
            if (e.ButtonID != "btnDelete") return;

            // Kiem tra xem user co quyen delete khong
            if (!RightAccess.CheckUserRight(AccountSession.Session, ScreenCode, OperationConstant.Delete.Key, out _message))
            {
                WebCommon.AlertGridView(sender, "You do not have permission delete");
                return;
            }
            string id = gridPatient.GetRowValues(e.VisibleIndex, "PatientCode").ToString();

            // Get patient by Patient Code
            var patients = DataRepository.VcsPatientProvider.GetByPatientCode(id);
            if (patients == null || !patients.Any())
            {
                WebCommon.AlertGridView(sender, "Cannot find patient, you cannot delete it.");
                return;
            }
            var patient = patients[0];
            if (patient.IsDisabled)
            {
                WebCommon.AlertGridView(sender, String.Format("Patient {0} is using, you cannot delete it.", patient.FirstName));
                return;
            }

            int count;
            DataRepository.AppointmentProvider.GetPaged(
               String.Format("IsDisabled = 'False' AND PatientCode = '{0}'", patient.PatientCode), string.Empty,
               0, ServiceFacade.SettingsHelper.GetPagedLength, out count);
            if (count > 0)
            {
                WebCommon.AlertGridView(sender, String.Format("Patient {0} is using, you cannot delete it.", patient.FirstName));
                return;
            }
            patient.IsDisabled = true;
            patient.UpdateUser = AccountSession.Session;
            DataRepository.VcsPatientProvider.Update(patient.PatientCode, patient.FirstName, patient.MiddleName, patient.LastName
                , patient.DateOfBirth, patient.Sex, patient.Nationality, patient.CompanyCode, patient.HomePhone, patient.MobilePhone
                , patient.UpdateUser, patient.ApptRemark, patient.IsDisabled);

            // Show message alert delete successfully
            WebCommon.AlertGridView(sender, "Patient is deleted successfully.");
        }
        catch (Exception ex)
        {
            LoggerController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
        }
    }
    
    /// <summary>
    /// Lay gia tri master de loc ra danh sach Appointment phu hop
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridAppointment_BeforePerformDataSelect(object sender, EventArgs e)
    {
        var parameter = AppointmentDatas.Parameters["whereClause"];
        var value = string.Empty;

        // Lay gia tri cua dong hien tai trong grid cha
        var grid = sender as ASPxGridView;
        if (grid != null)
        {
            value = grid.GetMasterRowKeyValue().ToString();
        }

        if (parameter == null)
        {

            AppointmentDatas.Parameters.Add("WhereClause",
                                            String.Format("IsDisabled = 'false' AND PatientCode = '{0}'", value));
        }
        else
        {
            parameter.DefaultValue = String.Format("IsDisabled = 'false' AND PatientCode = '{0}'", value);
        }
    }

    /// <summary>
    /// Xoa nhieu patient
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridPatient_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        TransactionManager tm = DataRepository.Provider.CreateTransaction();
        tm.BeginTransaction();
        try
        {
            if (e.Parameters == "Delete")
            {
                // Kiem tra xem user co quyen delete khong
                if (!RightAccess.CheckUserRight(AccountSession.Session, ScreenCode, OperationConstant.Delete.Key, out _message))
                {
                    WebCommon.AlertGridView(sender, "You do not have permission delete");
                    tm.Rollback();
                    return;
                }

                var grid = sender as ASPxGridView;
                if (grid == null)
                {
                    tm.Rollback();
                    return;
                }

                // Lay danh sach Id cua cac row duoc select
                var fieldNames = new[] { "PatientCode" };
                List<object> columnValues = grid.GetSelectedFieldValues(fieldNames);
                
                // Doi trang thai cua cac roster
                foreach (object patientCode in columnValues)
                {
                    // Kiem tra patient xem co ton tai khong
                    var patients = DataRepository.VcsPatientProvider.GetByPatientCode(patientCode.ToString());
                    if (patients == null || !patients.Any())
                    {
                        WebCommon.AlertGridView(sender, "Cannot find patient, you cannot delete it.");
                        tm.Rollback();
                        return;
                    }
                    var patient = patients[0];
                    if (patient.IsDisabled)
                    {
                        WebCommon.AlertGridView(sender, String.Format("Patient {0} is not existed, you cannot delete it.", patient.FirstName));
                        tm.Rollback();
                        return;
                    }

                    // Kiem tra xem patient da tung dat lich hen chua
                    int count;
                    DataRepository.AppointmentProvider.GetPaged(
                        String.Format("IsDisabled = 'False' AND PatientCode = '{0}'", patient.PatientCode), string.Empty,
                        0, ServiceFacade.SettingsHelper.GetPagedLength, out count);
                    if (count > 0)
                    {
                        WebCommon.AlertGridView(sender, String.Format("Patient {0} is using, you cannot delete it.", patient.FirstName));
                        tm.Rollback();
                        return;
                    }

                    // Tien anh cap nhat
                    patient.IsDisabled = true;
                    patient.UpdateUser = AccountSession.Session;
                    DataRepository.VcsPatientProvider.Update(patient.PatientCode, patient.FirstName, patient.MiddleName, patient.LastName
                        , patient.DateOfBirth, patient.Sex, patient.Nationality, patient.CompanyCode, patient.HomePhone, patient.MobilePhone
                        , patient.UpdateUser, patient.ApptRemark, patient.IsDisabled);
                }
                tm.Commit();
                WebCommon.AlertGridView(sender, String.Format("{0} deleted successfully.",
                                                      grid.Selection.Count > 1 ? "Patients are" : "Patient is"));

                // Set tam duration de lay duoc danh sach moi
                //int duration = RosterDataSource.CacheDuration;
                //RosterDataSource.CacheDuration = 0;
                grid.DataBind();
                grid.Selection.UnselectAll();
                //RosterDataSource.CacheDuration = duration;
            }
            else
            {
                // Thuc hien check/uncheck khi nhan check all
                bool needToSelectAll;
                bool.TryParse(e.Parameters, out needToSelectAll);

                var gridView = (ASPxGridView)sender;
                
                int startIndex = gridView.PageIndex * gridView.SettingsPager.PageSize;
                int endIndex = Math.Min(gridView.VisibleRowCount, startIndex + gridView.SettingsPager.PageSize);

                for (int i = startIndex; i < endIndex; i++)
                {
                    gridView.Selection.SetSelection(i, needToSelectAll);
                }
            }
        }
        catch (Exception ex)
        {
            tm.Rollback();
            LoggerController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            WebCommon.AlertGridView(sender, "Cannot delete Roster. Please contact Administrator.");
        }
    }

    /// <summary>
    /// An cac row co IsDisabled bang true
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridPatient_OnHtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        try
        {
            if (e.RowType == GridViewRowType.Data)
            {
                e.Row.Visible = !Boolean.Parse(e.GetValue("IsDisabled").ToString());
            }
        }
        catch (Exception ex)
        {
            LoggerController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
        }
    }

    /// <summary>
    /// Xu ly khi checkbox cua tung dong duoc khoi tao
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cbCheck_Init(object sender, EventArgs e)
    {
        var cb = (ASPxCheckBox)sender;
        var container = (GridViewDataItemTemplateContainer)cb.NamingContainer;
        cb.ClientInstanceName = string.Format("cbCheck{0}", container.VisibleIndex);
        cb.Checked = gridPatient.Selection.IsRowSelected(container.VisibleIndex);
        cb.ClientSideEvents.CheckedChanged = string.Format("function (s, e) {{ grid.SelectRowOnPage({0}, s.GetChecked()); }}", container.VisibleIndex);
    }
  
    /// <summary>
    /// Xu ly khi checkbox all duoc khoi tao
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cbCheckAll_Init(object sender, EventArgs e)
    {
        var cb = (ASPxCheckBox)sender;
        cb.ClientSideEvents.CheckedChanged = string.Format("function (s, e) {{ grid.PerformCallback(s.GetChecked().toString()); }}");
    }
}
