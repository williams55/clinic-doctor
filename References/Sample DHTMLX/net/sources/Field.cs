using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dhtmlxConnectors
{
    /// <summary>
    /// Represents column to be returned by sql query
    /// </summary>
    public abstract class Field
    {
        /// <summary>
        /// Gets or Sets Alias to be used for this column
        /// </summary>
        public string Alias
        {
            get;
            set;
        }

        /// <summary>
        /// Gets field name to be used while accessing this field in a result row. 
        /// </summary>
        public abstract string ExternalName
        {
            get;
        }

        /// <summary>
        /// Gets field name to be used while accessing this field within the query
        /// </summary>
        public abstract string InternalName
        {
            get;
        }

        /// <summary>
        /// Checks if fields are equal
        /// </summary>
        /// <param name="One">First field to compare</param>
        /// <param name="Two">Second field to compare</param>
        /// <returns>True if fields are equal</returns>
        public static bool operator == (Field One, Field Two)
        {
            if (object.Equals(null, One) && object.Equals(Two, null))
                return true;
            if (object.Equals(null, One) || object.Equals(Two, null))
                return false;
            if (One.GetType() != Two.GetType())
                return false;
            return 
                One.InternalName == Two.InternalName 
                && One.ExternalName == Two.ExternalName 
                && One.Alias == Two.Alias;
        }

        /// <summary>
        /// Checks if fields are not equal
        /// </summary>
        /// <param name="One">First field to compare</param>
        /// <param name="Two">Second field to compare</param>
        /// <returns>True if fields are not equal</returns>
        public static bool operator !=(Field One, Field Two)
        {
            return !(One == Two);
        }

        /// <summary>
        /// Checks if this field equals to object provided
        /// </summary>
        /// <param name="obj">object to compare this field to</param>
        /// <returns>True if object provided is field and field is equal to this one</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
                return false;
            return this == (Field)obj;
        }

        /// <summary>
        /// Returns hash code for this field
        /// </summary>
        /// <returns>Hash code for this field</returns>
        public override int GetHashCode()
        {
            unchecked { return this.InternalName.GetHashCode() + this.ExternalName.GetHashCode(); }
        }

        /// <summary>
        /// Clones this field
        /// </summary>
        /// <returns>Copy of field being cloned</returns>
        public abstract Field Clone();
    }
}
