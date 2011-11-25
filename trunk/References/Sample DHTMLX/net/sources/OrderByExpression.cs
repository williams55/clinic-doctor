using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dhtmlxConnectors
{
    /// <summary>
    /// Represents ORDER BY unparsed sql statement
    /// </summary>
    public class OrderByExpression : OrderByStatement
    {
        /// <summary>
        /// Gets or Sets order by sql expression
        /// </summary>
        public string Expression
        {
            get;
            set;
        }

        /// <summary>
        /// Creates new instance of OrderByExpression
        /// </summary>
        public OrderByExpression()
        {
        }

        /// <summary>
        /// Creates new instance of OrderByExpression
        /// </summary>
        /// <param name="Expression">SQL Expression to initialize order statement with</param>
        public OrderByExpression(string Expression)
        {
            this.Expression = Expression;
        }

        /// <summary>
        /// Converts statement into SQL-92 compatible string
        /// </summary>
        /// <returns>String that represents current instance of OrderByExpression</returns>
        public override string ToString()
        {
            return this.Expression;
        }
    }
}
