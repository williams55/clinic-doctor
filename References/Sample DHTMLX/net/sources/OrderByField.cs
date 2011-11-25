using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace dhtmlxConnectors
{
    /// <summary>
    /// Represents single ORDER BY element (e.g. ORDER BY "CustomerName" or "CreatedDate DESC")
    /// </summary>
    public class OrderByField : OrderByStatement
    {
        /// <summary>
        /// Gets or Sets field to order by
        /// </summary>
        public Field Field
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or Sets order direction
        /// </summary>
        public SortDirection Direction
        {
            get;
            set;
        }

        /// <summary>
        /// Creates new instance of OrderByField
        /// </summary>
        /// <param name="OrderField">Field to order by</param>
        /// <param name="SortDirection">Sort direction (Asc or Desc)</param>
        public OrderByField(Field OrderField, string SortDirection)
            : this(OrderField, OrderByField.ParseDirection(SortDirection))
        {
        }

        /// <summary>
        /// Creates new instance of OrderByField
        /// </summary>
        /// <param name="OrderField">Field to order by</param>
        /// <param name="Direction">Sort direction</param>
        public OrderByField(Field OrderField, SortDirection Direction)
        {
            if (OrderField == null)
                throw new ArgumentNullException("OrderField");
            this.Field = OrderField;
            this.Direction = Direction;
        }

        /// <summary>
        /// Converts current OrderByField to SQL-92 string
        /// </summary>
        /// <returns>SQL-92 representation of current statement</returns>
        public override string ToString()
        {
            return this.Field.InternalName + " " + (this.Direction == SortDirection.Ascending? "ASC" : "DESC" );
        }

        /// <summary>
        /// Converts specified value to valid SortDirection enum representation
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static SortDirection ParseDirection(string Value)
        {
            if (string.IsNullOrEmpty(Value) || Value.ToLower().StartsWith("asc"))
                return SortDirection.Ascending;
            else
                return SortDirection.Descending;
        }
    }
}
