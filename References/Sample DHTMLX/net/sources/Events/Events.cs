using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using System.Xml;
using System.Collections.Specialized;

namespace dhtmlxConnectors
{
    /// <summary>
    /// DataSelected event arguments class
    /// </summary>
    public class DataSelectedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or Sets reference to query result
        /// </summary>
        public DataTable Data
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or Sets total number of records
        /// </summary>
        public int TotalRowsCount
        {
            get;
            set;
        }

        /// <summary>
        /// Created new instance of DataSelectedEventArgs class
        /// </summary>
        /// <param name="Data">Query result</param>
        /// <param name="TotalRowsCount">Total rows count</param>
        public DataSelectedEventArgs(DataTable Data, int TotalRowsCount)
        {
            this.Data = Data;
            this.TotalRowsCount = TotalRowsCount;
        }
    }

    /// <summary>
    /// ItemPrerender events args
    /// </summary>
    /// <typeparam name="T">Type if item that is going to render</typeparam>
    public class ItemPrerenderEventArgs<T> : EventArgs
        where T: dhtmlxDataItem
    {
        private T _DataItem = null;
        
        /// <summary>
        /// Gets reference to item going to render
        /// </summary>
        public T DataItem
        {
            get
            {
                return this._DataItem;
            }
        }

        /// <summary>
        /// Creates new instance of ItemPrerenderEventArgs
        /// </summary>
        /// <param name="DataItem">Reference to item going to render</param>
        public ItemPrerenderEventArgs(T DataItem)
        {
            this._DataItem = DataItem;
        }
    }

    /// <summary>
    /// DataActionProcessing event args class
    /// </summary>
    public class DataActionProcessingEventArgs: EventArgs
    {
        private DataAction _DataAction = null;

        /// <summary>
        /// Gets reference to DataAction going to process
        /// </summary>
        public DataAction DataAction
        {
            get
            {
                return this._DataAction;
            }
        }

        /// <summary>
        /// Creates new instance of DataActionProcessingEventArgs
        /// </summary>
        /// <param name="Action">DataAction going to process</param>
        public DataActionProcessingEventArgs(DataAction Action)
        {
            this._DataAction = Action;
        }
    }

    /// <summary>
    /// Render event args
    /// </summary>
    public class RenderEventArgs : EventArgs
    {
        private XmlWriter _Writer;
        /// <summary>
        /// Gets reference to XmlWriter used for rendering
        /// </summary>
        public XmlWriter Writer
        {
            get
            {
                return this._Writer;
            }
        }

        /// <summary>
        /// Creates new instance of RenderEventArgs
        /// </summary>
        /// <param name="Writer">XmlWriter used for rendering</param>
        public RenderEventArgs(XmlWriter Writer)
        {
            this._Writer = Writer;
        }
    }

    /// <summary>
    /// RenderChildren event args
    /// </summary>
    /// <typeparam name="T">Type of dhtmlxDataItem for which children request is going to happen</typeparam>
    public class RequestChildrenEventArgs<T> : EventArgs
        where T : dhtmlxDataItem
    {
        private T _HierarchicalDataItem = null;
        /// <summary>
        /// Gets reference to dhtmlxDataItem for which children request is going to happen
        /// </summary>
        public T HierarchicalDataItem
        {
            get
            {
                return this._HierarchicalDataItem;
            }
        }

        /// <summary>
        /// Gets or Sets value indicating either HierarchicalDataItem has children or not
        /// </summary>
        public bool HasChildren
        {
            get;
            set;
        }

        /// <summary>
        /// Creates new instance of RequestChildrenEventArgs
        /// </summary>
        /// <param name="HierarchicalDataItem">dhtmlxDataItem for which children request is going to happen</param>
        public RequestChildrenEventArgs(T HierarchicalDataItem)
        {
            this._HierarchicalDataItem = HierarchicalDataItem;
        }

    }


}