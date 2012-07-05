
using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentSystem.Settings.Settings
{
    [Serializable]
    public class CustomSerializable
    {
        #region members
        private Guid id;
        private int value1;
        private DateTime value2;
        private string value3;
        private decimal value4;
        #endregion members

        #region properties
        public Guid Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        public int Value1
        {
            get { return this.value1; }
            set { this.value1 = value; }
        }
        public DateTime Value2
        {
            get { return this.value2; }
            set { this.value2 = value; }
        }
        public string Value3
        {
            get { return this.value3; }
            set { this.value3 = value; }
        }
        public decimal Value4
        {
            get { return this.value4; }
            set { this.value4 = value; }
        }
        #endregion properties

        #region ctors
        public CustomSerializable()
        {
            // there must be an parameterless constructor
        }
        #endregion ctors
    }
}