
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace AppointmentSystem.Settings.Settings
{
    [Serializable]
    public class CustomBinarySerializable : CustomSerializable, ICustomBinarySerializable
    {
        #region ICustomBinarySerializable Members
        public void Deserialize(byte[] bytes)
        {
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                BinaryFormatter formatter = new BinaryFormatter();

                CustomBinarySerializable other = formatter.Deserialize(stream) as CustomBinarySerializable;

                this.Id = other.Id;
                this.Value1 = other.Value1;
                this.Value2 = other.Value2;
                this.Value3 = other.Value3;
                this.Value4 = other.Value4;
            }
        }
        public byte[] Serialize()
        {
            using (MemoryStream stream = new MemoryStream())            
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, this);

                return stream.ToArray();
            }
        }
        #endregion
    }
}
