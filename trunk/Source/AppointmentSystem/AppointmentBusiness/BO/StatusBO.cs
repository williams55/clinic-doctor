using System;
using AppointmentSystem.Data;
using AppointmentSystem.Entities;
using Common.Util;
using Log.Controller;

namespace AppointmentBusiness.BO
{
    public class StatusBO : IStatusBO
    {
        public string Complete
        {
            get { return "Complete"; }
        }

        public string CompleteColor
        {
            get { return DataRepository.StatusProvider.GetById("Complete").ColorCode; }
        }

        public string New
        {
            get { return "New"; }
        }

        public string NewColor
        {
            get { return DataRepository.StatusProvider.GetById("New").ColorCode; }
        }

        public string Processing
        {
            get { return "Processing"; }
        }

        public string ProcessingColor
        {
            get { return DataRepository.StatusProvider.GetById("Processing").ColorCode; }
        }

        public string Cancel
        {
            get { return "Cancel"; }
        }

        public string CancelColor
        {
            get { return DataRepository.StatusProvider.GetById("Cancel").ColorCode; }
        }
    }
}
