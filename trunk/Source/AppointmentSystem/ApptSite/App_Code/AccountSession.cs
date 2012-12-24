using System;
using System.Web;

namespace ApptSite
{
    /// <summary>
    /// Summary description for AccountSession
    /// </summary>
    public class AccountSession
    {
        #region Private Variables
        private const string SessionName = "User";
        #endregion

        /// <summary>
        /// Lay thong tin hoac gan gia tri cho session cua user
        /// </summary>
        public static string Session
        {
            get
            {
                return HttpContext.Current.Session[SessionName] == null
                           ? null
                           : HttpContext.Current.Session[SessionName].ToString();
            }
            set
            {
                HttpContext.Current.Session[SessionName] = value;
            }
        }

        /// <summary>
        /// Lay thong tin hoac gan gia tri cho session cua user
        /// </summary>
        public static bool IsLogin
        {
            get
            {
                return HttpContext.Current.Session[SessionName] != null;
            }
        }

        /// <summary>
        /// Lay thong tin hoac gan gia tri cho session cua user
        /// </summary>
        public static string CurrentRosterSelection
        {
            get
            {
                return HttpContext.Current.Session["RosterSelector"] == null
                           ? null
                           : HttpContext.Current.Session["RosterSelector"].ToString();
            }
            set
            {
                HttpContext.Current.Session["RosterSelector"] = value;
            }
        }
    }
}