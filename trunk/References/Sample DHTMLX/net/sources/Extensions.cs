using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dhtmlxConnectors
{
    /// <summary>
    /// Extension methods storage
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Returns fields collection without duplicated or redundant items
        /// </summary>
        /// <param name="source">Collection to optimize</param>
        /// <returns>Fields collection without duplicated or redundant items</returns>
        public static IEnumerable<Field> Optimize(this IEnumerable<Field> source)
        {
            if (source.FirstOrDefault(field => field is ExpressionField && (field as ExpressionField).Expression.Trim() == "*") != null)
                return source.Where(field => field is ExpressionField).Distinct();
            else
                return source.Distinct();
        }

        /// <summary>
        /// Converts fields collection into dhtmlxFieldsCollection object
        /// </summary>
        /// <param name="source">Collection to convert</param>
        /// <returns>dhtmlxFieldsCollection object</returns>
        public static dhtmlxFieldsCollection ToFieldsCollection(this IEnumerable<Field> source)
        {
            return new dhtmlxFieldsCollection(source);
        }

        /// <summary>
        /// Adds Field-value pair into dictionary
        /// </summary>
        /// <param name="source">Dictionary to add element to</param>
        /// <param name="Name">Name to create field from</param>
        /// <param name="Value">Value to add into dictionary</param>
        public static void Add(this Dictionary<Field, string> source, string Name, string Value)
        {
            source.Add(new TableField(Name), Value);
        }
    }
}
