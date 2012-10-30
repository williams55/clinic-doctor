using System;
using System.Linq;
using AppointmentSystem.Data;
using AppointmentSystem.Entities;
using AppointmentSystem.Settings.BusinessLayer;
using Common.Extension;
using Common.Util;
using Log.Controller;

namespace AppointmentBusiness.BO
{
    public class RosterBO : IRosterBO
    {
        public bool Insert(Roster roster, ref string message)
        {
            try
            {
                if (!ValidateRoster(roster, false, ref message)) return false;
                DataRepository.RosterProvider.Insert(roster);
                return true;
            }
            catch (Exception ex)
            {
                LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            }
            return false;
        }

        public bool InsertRepeat(Roster roster, string weekday, TList<Roster> lstRoster, ref string message)
        {
            try
            {
                // Chia thoi gian ra
                var startTime = new DateTime(roster.StartTime.Year, roster.StartTime.Month, roster.StartTime.Day
                    , roster.StartTime.Hour, roster.StartTime.Minute, 0);
                var endTime = new DateTime(roster.StartTime.Year, roster.StartTime.Month, roster.StartTime.Day
                    , roster.EndTime.Hour, roster.EndTime.Minute, 0);
                var startDate = new DateTime(roster.StartTime.Year, roster.StartTime.Month, roster.StartTime.Day, 0, 0, 0);
                var endDate = new DateTime(roster.EndTime.Year, roster.EndTime.Month, roster.EndTime.Day, 0, 0, 0);

                // Kiem tra ngay bat dau, ngay ket thuc
                if (startTime >= endTime)
                {
                    message = "End time must be greater than from time.";
                    return false;
                }

                // Kiem tra ngay bat dau, ngay ket thuc
                if (startDate >= endDate)
                {
                    message = "End date must be greater than start date.";
                    return false;
                }

                // Variable for error message if there is conflict roster
                string errorMessage = string.Empty;
                var repeatRosterId = Guid.NewGuid();

                // Declare list of roster in case repeat roster
                if (lstRoster == null)
                    lstRoster = new TList<Roster>();

                startDate = startDate.AddDays(-1);
                while (startDate < endDate)
                {
                    startDate = startDate.AddDays(1);

                    if (weekday.Contains(startDate.DateIndexOfWeek().ToString()))
                    {
                        var newRoster = new Roster
                            {
                                Id = roster.Id,
                                RepeatId = repeatRosterId,
                                Username = roster.Username,
                                RosterTypeId = roster.RosterTypeId,
                                StartTime = new DateTime(startDate.Year, startDate.Month, startDate.Day, startTime.Hour, startTime.Minute, 0),
                                EndTime = new DateTime(startDate.Year, startDate.Month, startDate.Day, endTime.Hour, endTime.Minute, 0),
                                Note = roster.Note,
                                CreateUser = roster.CreateUser,
                                UpdateUser = roster.UpdateUser
                            };

                        if (!ValidateRoster(newRoster, false, ref message))
                        {
                            errorMessage += message;
                            continue;
                        }

                        lstRoster.Add(newRoster);

                        // Gan lai Id cho roster
                        roster.Id = newRoster.Id;
                    }
                }

                if (errorMessage.Length > 0)
                {
                    message = String.Format("There are some rosters conflicted: {0}", errorMessage.Substring(0, errorMessage.Length - 1));
                    return false;
                }

                // Insert new rosters
                DataRepository.RosterProvider.Insert(lstRoster);

                return true;
            }
            catch (Exception ex)
            {
                LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            }
            return false;
        }

        public bool Update(ref Roster roster, ref string message)
        {
            try
            {
                // Get old roster by id
                Roster oldRoster = DataRepository.RosterProvider.GetById(roster.Id);

                // Validate if roster's not existed
                // return message, nothing more
                if (oldRoster == null || oldRoster.IsDisabled)
                {
                    message = "There is no roster to update.";
                    return false;
                }

                // Gan gia tri moi
                oldRoster.Username = roster.Username;
                oldRoster.RosterTypeId = roster.RosterTypeId;
                oldRoster.StartTime = roster.StartTime;
                oldRoster.EndTime = roster.EndTime;
                oldRoster.Note = roster.Note;
                oldRoster.UpdateUser = roster.UpdateUser;

                // Kiem tra xem co appointment nao da duoc dung cho roster khong
                // Neu co thi khong thay doi duoc
                DataRepository.RosterProvider.DeepLoad(oldRoster);
                if (oldRoster.AppointmentCollection
                    .Any(appointment => (appointment.StartTime < oldRoster.StartTime || appointment.EndTime > oldRoster.EndTime) 
                        && !appointment.IsDisabled))
                {
                    message = String.Format("Cannot change roster {0} because there are some appointments.", oldRoster.Id);
                    return false;
                }

                // Goi ham kiem tra roster
                if (!ValidateRoster(oldRoster, true, ref message)) return false;
                DataRepository.RosterProvider.Update(oldRoster);

                // Gan lai doi tuong roster
                roster = oldRoster;
                return true;
            }
            catch (Exception ex)
            {
                LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            }
            return false;
        }

        #region Private methods
        /// <summary>
        /// Kiem tra roster
        /// </summary>
        /// <param name="roster"></param>
        /// <param name="isUpdate"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private bool ValidateRoster(Roster roster, bool isUpdate, ref string message)
        {
            try
            {
                // Kiem tra doctor co ton tai khong
                var objUser = DataRepository.UsersProvider.GetByUsername(roster.Username);
                if (objUser == null || objUser.IsDisabled)
                {
                    message = "User is not exist.";
                    return false;
                }

                // Kiem tra roster type co ton tai khong
                RosterType objRosterType = DataRepository.RosterTypeProvider.GetById(roster.RosterTypeId);
                if (objRosterType == null || objRosterType.IsDisabled)
                {
                    message = "Roster Type is not exist.";
                    return false;
                }
                
                // Kiem tra ngay bat dau, ngay ket thuc
                if (roster.StartTime >= roster.EndTime)
                {
                    message = "End time must be greater than from time.";
                    return false;
                }

                // If roster is created in a passed or current day
                if (DateTime.Now >= roster.StartTime)
                {
                    message = "You can not change roster to passed or current date.";
                    return false;
                }

                // Check existed rosters
                // Neu isUpdate = true => dang update
                int count;
                string query =
                    string.Format(
                        "Username = '{0}' AND Id <> '{3}' AND IsDisabled = 'False' AND StartTime < '{1}' AND EndTime > '{2}'"
                        , roster.Username, roster.EndTime, roster.StartTime,
                        isUpdate ? roster.Id : string.Empty);
                DataRepository.RosterProvider.GetPaged(query, "Id desc", 0, ServiceFacade.SettingsHelper.GetPagedLength, out count);
                // If there is no roster -> insert new roster
                if (count > 0)
                {
                    message = String.Format("There is roster conflicted: From {0} {1} to {2} {3}"
                        , roster.StartTime.DayOfWeek, roster.StartTime.ToString("dd MMM yyyy HH:mm")
                        , roster.EndTime.DayOfWeek, roster.EndTime.ToString("dd MMM yyyy HH:mm"));
                    return false;
                }

                // Tao Id moi bang cach truyen Id cu vao
                // Neu la update thi khong can RosterId moi
                roster.Id = isUpdate ? roster.Id : BoFactory.IdBO.RosterId(roster.Id);

                return true;
            }
            catch (Exception ex)
            {
                LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            }
            return false;
        }
        #endregion
    }
}
