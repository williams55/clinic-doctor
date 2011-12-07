using System;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web;
using ClinicDoctor.Data;
using ClinicDoctor.Entities;
using ClinicDoctor.Settings.BusinessLayer;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;

public partial class Admin_Doctor_WatchRosterIframe : System.Web.UI.Page
{

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string LoadRoster()
    {
        /* Return Structure
         * { result: true [success], false [fail],
         *   message: message content [will be showed when fail]
         *   data:  [{
         *              Id: id of roster,
         *              DoctorUserName: Doctor UserName,
         *              DoctorShortName: Doctor ShortName,
         *              RosterTypeId: Roster Type Id,
         *              RosterTypeTitle: Roster Type Title,
         *              start_date: start date,
         *              end_date: end date,
         *              note: note,
         *              text: roster type + note,
         *              color: color of event [get from status],
         *              isnew: true
         *          }, {}, {}]
         *  }
         */

        string result = string.Empty;
        TransactionManager tm = DataRepository.Provider.CreateTransaction();
        try
        {
            tm.BeginTransaction();

            string username = EntitiesUtilities.GetAuthName();
            Staff obj = DataRepository.StaffProvider.GetByUserNameIsDisabled(username, false);
            if (obj == null)
            {
                result = @"[{ 'result': 'false', 'message': 'Your account is not exist. Please contact Administrator or Manager.', 'data': '[]' }]";
                goto StepResult;
            }

            TList<DoctorRoster> lstObj = DataRepository.DoctorRosterProvider.GetByDoctorUserNameIsDisabled(tm, username, false);

            string color = string.Empty;
            foreach (DoctorRoster item in lstObj)
            {
                result += @"{id: '" + item.Id + @"',"
                    + @"start_date: '" + item.StartTime.ToString("dd/MM/yyyy HH:mm:ss") + @"',"
                    + @"end_date: '" + item.EndTime.ToString("dd/MM/yyyy HH:mm:ss") + @"',"
                    + @"section_id: '" + item.DoctorUserName + @"',"
                    + @"text: '" + item.RosterTypeTitle + @"<br />Doctor: " + item.DoctorUserName + @"<br />";

                if (!string.IsNullOrEmpty(item.Note))
                    result += item.Note;

                result += @"',DoctorUserName: '" + item.DoctorUserName + @"',"
                    + @"DoctorShortName: '" + item.DoctorShortName + @"',"
                    + @"RosterTypeId: '" + item.RosterTypeId + @"',"
                    + @"RosterTypeTitle: '" + item.RosterTypeTitle + @"',"
                    + @"RosterTypeId: '" + item.RosterTypeId + @"',"
                    + @"note: '" + item.Note + @"',";

                // If roster is passed, set color code and is completed
                if (item.StartTime <= DateTime.Now)
                {
                    item.ColorCode = ServiceFacade.SettingsHelper.CompleteColor;
                    item.IsComplete = true;
                    DataRepository.DoctorRosterProvider.Save(tm, item);

                    result += @"readonly: true,";
                }
                result += @"color: '" + item.ColorCode + @"',";
                result += @"isnew: 'false'},";
            }
            if (result.Length > 1)
            {
                result = @"[{ 'result': 'true', 'message': '', 'data': "
                    + "[" + result.Substring(0, result.Length - 1) + "]"
                    + @" }]";
            }
            else
            {
                result = @"[{ 'result': 'true', 'message': '' }]";
            }

            tm.Commit();
        }
        catch (Exception ex)
        {
            //Write log cho nay
            tm.Rollback();

            result = @"[{ 'result': 'false', 'message': '" + ex.Message + "', 'data': '[]' }]";
            goto StepResult;
        }

    StepResult:
        return result;
    }
}