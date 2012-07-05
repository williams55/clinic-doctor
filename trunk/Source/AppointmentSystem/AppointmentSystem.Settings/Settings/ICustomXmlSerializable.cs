
using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentSystem.Settings.Settings
{
    public interface ICustomXmlSerializable
    {
        void Deserialize(string str);
        string Serialize();
    }
}