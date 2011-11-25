using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace dhtmlxConnectors
{
    /// <summary>
    /// Represents sql query field
    /// </summary>
    public class TableField: Field
    {
        /// <summary>
        /// Gets or Sets column name
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Converts current column into SQL-92 string
        /// </summary>
        /// <returns>String representation of column</returns>
        public override string ToString()
        {
            if (string.IsNullOrEmpty(this.Alias))
                return this.Name;
            else
                return string.Format("{0} as {1}", this.Name, this.Alias);
        }

        /// <summary>
        /// Creates new instance of TableField
        /// </summary>
        public TableField()
        {
        }

        /// <summary>
        /// Creates new instance of TableField
        /// </summary>
        /// <param name="Name">Field name</param>
        public TableField(string Name)
            :this()
        {
            this.Name = Name;
        }

        /// <summary>
        /// Creates new instance of TableField
        /// </summary>
        /// <param name="Name">Field name</param>
        /// <param name="Alias">Field alias</param>
        public TableField(string Name, string Alias)
            : this(Name)
        {
            this.Alias = Alias;
        }

        /// <summary>
        /// Gets field name to be used when referencing this field outside the query (e.g. accessing specified column in DataTable)
        /// </summary>
        public override string ExternalName
        {
            get 
            {
                string result = "";
                if (string.IsNullOrEmpty(this.Alias))
                    result = this.Name;
                else
                    result = this.Alias;
                result = result.Replace("[", "").Replace("]", "");
                return Regex.Replace(result, "^[^.]*\\.", "");
            }
        }

        /// <summary>
        /// Gets field name to be used when referencing this field inside the query
        /// </summary>
        public override string InternalName
        {
            get 
            {
                return this.Name;
            }
        }

        /// <summary>
        /// Converts string into TableField object
        /// </summary>
        /// <param name="FieldName">Field name to convert into TableField</param>
        /// <returns>new TableField object created from FieldName string</returns>
        public static explicit operator TableField(string FieldName)
        {
            return new TableField(FieldName);
        }

        /// <summary>
        /// Converts TableField object into string
        /// </summary>
        /// <param name="field">Field to convert</param>
        /// <returns>String representation of TableField object</returns>
        public static explicit operator string(TableField field)
        {
            return field.ExternalName;
        }


        /// <summary>
        /// Clones this field
        /// </summary>
        /// <returns>Copy of field being cloned</returns>
        public override Field Clone()
        {
            return new TableField(this.Name, this.Alias);
        }
    }
}
