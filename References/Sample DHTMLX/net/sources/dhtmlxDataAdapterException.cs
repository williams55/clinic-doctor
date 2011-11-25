using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dhtmlxConnectors
{
    /// <summary>
    /// Exceptions class for dhtmlx components
    /// </summary>
    public class dhtmlxException: ApplicationException
    {
        /// <summary>
        /// Creates new instance of dhtmlxException
        /// </summary>
        public dhtmlxException()
            : base()
        {
        }

        /// <summary>
        /// Creates new instance of dhtmlxException
        /// </summary>
        /// <param name="Message">Exception message</param>
        public dhtmlxException(string Message)
            : base(Message)
        {
        }
    }
}
