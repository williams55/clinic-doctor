
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicDoctor.Settings.Settings
{
    public interface ICustomBinarySerializable
    {
        void Deserialize(byte[] bytes);
        byte[] Serialize();
    }
}