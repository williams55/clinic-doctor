
using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentSystem.Settings.Settings
{
    public interface ICustomBinarySerializable
    {
        void Deserialize(byte[] bytes);
        byte[] Serialize();
    }
}