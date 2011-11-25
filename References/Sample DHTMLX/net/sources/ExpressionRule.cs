using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dhtmlxConnectors
{
    /// <summary>
    /// Represents WHERE statement as SQL expression (e.g. "dbo.getDay(RegisterDate) IN ['Sat', 'Sun']")
    /// </summary>
    public class ExpressionRule: Rule
    {
        /// <summary>
        /// Gets or Sets rule sql expression
        /// </summary>
        public string Expression
        {
            get;
            set;
        }

        /// <summary>
        /// Creates new instance of ExpressionRule
        /// </summary>
        public ExpressionRule()
        {
        }

        /// <summary>
        /// Creates new instance of ExpressionRule
        /// </summary>
        /// <param name="Expression">SQL Expression to initialize rule with</param>
        public ExpressionRule(string Expression)
        {
            this.Expression = Expression;
        }

        /// <summary>
        /// Converts rule into SQL-92 compatible string
        /// </summary>
        /// <returns>String that represents current instance of ExpressionRule</returns>
        public override string ToString()
        {
            return this.Expression;
        }
    }
}
