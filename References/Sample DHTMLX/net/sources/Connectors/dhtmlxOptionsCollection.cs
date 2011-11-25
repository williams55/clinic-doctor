using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace dhtmlxConnectors
{
    /// <summary>
    /// Represents collection of dhtmlxOptionsItem
    /// </summary>
    public class dhtmlxOptionsCollection: Collection<dhtmlxOptionsItem>
    {
        /// <summary>
        /// Adds new option into collection
        /// </summary>
        /// <param name="Value">New option value</param>
        /// <param name="Text">New option text</param>
        public void Add(string Value, string Text)
        {
            this.Add(new dhtmlxOptionsItem(Value, Text));
        }

        /// <summary>
        /// Adds new option into collection
        /// </summary>
        /// <param name="Text">New option text</param>
        public void Add(string Text)
        {
            this.Add(new dhtmlxOptionsItem(Text));
        }

        /// <summary>
        /// Adds set of items into collection
        /// </summary>
        /// <param name="Options">List of options to add</param>
        public void AddRange(IEnumerable<string> Options)
        {
            foreach (string Option in Options)
                this.Add(Option);
        }

        /// <summary>
        /// Adds set of items into collection
        /// </summary>
        /// <param name="Options">List of key-value pairs to create options from and add into current
        /// +collection</param>
        public void AddRange(IEnumerable<KeyValuePair<string, string>> Options)
        {
            foreach (KeyValuePair<string, string> Option in Options)
                this.Add(Option.Key, Option.Value);
        }
    }
}
