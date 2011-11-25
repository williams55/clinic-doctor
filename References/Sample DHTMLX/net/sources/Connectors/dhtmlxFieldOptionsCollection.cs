using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dhtmlxConnectors
{
    /// <summary>
    /// Represents collection of Field-dhtmlxOptionsCollection pairs
    /// </summary>
    public class dhtmlxFieldOptionsCollection: Dictionary<Field, dhtmlxOptionsCollection>
    {
        /// <summary>
        /// Adds new Option into collection that belongs to the field provided.
        /// </summary>
        /// <param name="field">Field to add option to. If there's no collection for this field - one will be created.</param>
        /// <param name="OptionText">Text to create option from</param>
        public void Add(Field field, string OptionText)
        {
            this.Add(field, new dhtmlxOptionsItem(OptionText));
        }

        /// <summary>
        /// Adds new Option into collection that belongs to the field provided.
        /// </summary>
        /// <param name="field">Field to add option to. If there's no collection for this field - one will be created.</param>
        /// <param name="OptionValue">Value to create option from</param>
        /// <param name="OptionText">Text to create option from</param>
        public void Add(Field field, string OptionValue, string OptionText)
        {
            this.Add(field, new dhtmlxOptionsItem(OptionValue, OptionText));
        }

        /// <summary>
        /// Adds new Option into collection that belongs to the field provided.
        /// </summary>
        /// <param name="field">Field to add option to. If there's no collection for this field - one will be created.</param>
        /// <param name="option">Option to add into collection</param>
        public void Add(Field field, dhtmlxOptionsItem option)
        {
            if (!this.ContainsKey(field))
                this.Add(field, new dhtmlxOptionsCollection());
            this[field].Add(option);
        }

        /// <summary>
        /// Adds set of options into collection that belongs to the field provided.
        /// </summary>
        /// <param name="field">Field to add option to. If there's no collection for this field - one will be created.</param>
        /// <param name="Options">Option texts to create options from</param>
        public void Add(Field field, params string[] Options)
        {
            this.Add(field, (IEnumerable<string>)Options);
        }

        /// <summary>
        /// Adds set of options into collection that belongs to the field provided.
        /// </summary>
        /// <param name="field">Field to add option to. If there's no collection for this field - one will be created.</param>
        /// <param name="Options">List of option texts to create options from</param>
        public void Add(Field field, IEnumerable<string> Options)
        {
            foreach (string Option in Options)
                this.Add(field, new dhtmlxOptionsItem(Option));
        }

        /// <summary>
        /// Adds set of options into collection that belongs to the field provided.
        /// </summary>
        /// <param name="field">Field to add option to. If there's no collection for this field - one will be created.</param>
        /// <param name="Options">Collection of Value-Text to create options list from</param>
        public void Add(Field field, IEnumerable<KeyValuePair<string, string>> Options)
        {
            foreach (KeyValuePair<string, string> Option in Options)
                this.Add(field, new dhtmlxOptionsItem(Option.Key, Option.Value));
        }
    }
}
