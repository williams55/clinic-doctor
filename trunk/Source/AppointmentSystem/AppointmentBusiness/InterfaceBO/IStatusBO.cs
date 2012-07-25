using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppointmentSystem.Entities;

namespace AppointmentBusiness.BO
{
    public interface IStatusBO
    {
        string Complete { get; }
        string CompleteColor { get; }
        string New { get; }
        string NewColor { get; }
        string Processing { get; }
        string ProcessingColor { get; }
        string Cancel { get; }
        string CancelColor { get; }
    }
}
