using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace dhtmlxConnectors
{
    /// <summary>
    /// Class that represents fields collection
    /// </summary>
    public class dhtmlxFieldsCollection: Collection<Field>
    {
        /// <summary>
        /// Gets reference to a field by its name
        /// </summary>
        /// <param name="Name">Name or Alias to find field by</param>
        /// <returns>Reference to a field by name given, or null</returns>
        public Field this[string Name]
        {
            get
            {
                if (string.IsNullOrEmpty(Name))
                    throw new ArgumentException("Parameter cannot be null or empty", "Name");
                return this.FirstOrDefault(f => f.ExternalName == Name);
            }
        }

        /// <summary>
        /// Creates new instance of dhtmlxFieldsCollection class
        /// </summary>
        /// <param name="fields">Set of fields to initialize collection with</param>
        public dhtmlxFieldsCollection(IEnumerable<Field> fields)
        : this()
        {
            this.AddRange(fields);
        }

        /// <summary>
        /// Creates new instance of dhtmlxFieldsCollection class
        /// </summary>
        public dhtmlxFieldsCollection()
        : base()
        {
        }

        /// <summary>
        /// Adds set of fields into collection
        /// </summary>
        /// <param name="fields">Fields to add into collection</param>
        public void AddRange(IEnumerable<Field> fields)
        {
            foreach (var field in fields)
                this.Add(field);
        }

        /// <summary>
        /// Checks if specified field is in collection
        /// </summary>
        /// <param name="field">Field to check</param>
        /// <returns>True if specified field is in collection</returns>
        public new bool Contains(Field field)
        {
            return this.Count(f => f == field) > 0;
        }

        /// <summary>
        /// Checks if specified field is in collection
        /// </summary>
        /// <param name="FieldName">field name to check</param>
        /// <returns>True if specified field is in collection</returns>
        public bool Contains(string FieldName)
        {
            return this.Count(f => f.ExternalName == FieldName) > 0;
        }
    }
}
