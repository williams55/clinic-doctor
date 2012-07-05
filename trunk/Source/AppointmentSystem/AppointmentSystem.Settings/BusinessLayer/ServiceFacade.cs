
namespace AppointmentSystem.Settings.BusinessLayer
{
    public static class ServiceFacade
    {
        public static SettingsHelper SettingsHelper
        {
            get { return SettingsHelper.Current; }
        }
        public static SettingsService SettingsService
        {
            get { return new SettingsService(); }
        }
    }
}