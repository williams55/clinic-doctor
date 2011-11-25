using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dhtmlxConnectors
{
    /// <summary>
    /// Represents query column that is function, subquery
    /// </summary>
    public class ExpressionField: Field
    {

        /// <summary>
        /// Gets or Sets sql expression used for this field evaluation (e.g. "count(*)" or "(SELECT TOP 1 CityName FROM CITIES) AS RandomCity")
        /// </summary>
        public string Expression
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
                return this.Expression;
            else
                return string.Format("{0} as {1}", this.Expression, this.Alias);
        }

        /// <summary>
        /// Creates new instance of ExpressionField
        /// </summary>
        public ExpressionField()
        {
        }

        /// <summary>
        /// Creates new instance of ExpressionField
        /// </summary>
        /// <param name="Expression">sql expression to create this field for</param>
        public ExpressionField(string Expression)
            :this()
        {
            this.Expression = Expression;
        }

        /// <summary>
        /// Creates new instance of ExpressionField
        /// </summary>
        /// <param name="Expression">sql expression to create this field for</param>
        /// <param name="Alias">Alias to identify expression by</param>
        public ExpressionField(string Expression, string Alias)
            : this(Expression)
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
                return this.Alias ?? "";
            }
        }

        /// <summary>
        /// Gets field name to be used when referencing this field inside the query
        /// </summary>
        public override string InternalName
        {
            get {
                return this.Expression;
            }
        }

        /// <summary>
        /// Clones this field
        /// </summary>
        /// <returns>Copy of field being cloned</returns>
        public override Field Clone()
        {
            return new ExpressionField(this.Expression, this.Alias);
        }
    }
}
