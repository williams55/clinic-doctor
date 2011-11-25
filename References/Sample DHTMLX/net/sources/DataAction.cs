using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace dhtmlxConnectors
{
    /// <summary>
    /// Represents action to be done against particular table
    /// </summary>
    public class DataAction
    {
        private ActionType _ActionType;
        /// <summary>
        /// Gets type of this action. ActionType can only be set in object constructor
        /// </summary>
        public ActionType ActionType
        {
            [DebuggerStepThrough]
            get
            {
                return this._ActionType;
            }
        }

        /// <summary>
        /// Gets or Sets action name in case of user defined action
        /// </summary>
        public string CustomActionName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or Sets PrimaryKey field value
        /// </summary>
        public object PrimaryKeyValue
        {
            [DebuggerStepThrough]
            get;
            [DebuggerStepThrough]
            set;
        }

        private object _PostoperationalPrimaryKeyValue = null;
        
        /// <summary>
        /// Gets or Sets new PrimaryKey field value which was retrieved after action execution
        /// </summary>
        public object PostoperationalPrimaryKeyValue
        {
            [DebuggerStepThrough]
            get
            {
                return this._PostoperationalPrimaryKeyValue ?? this.PrimaryKeyValue;
            }
            [DebuggerStepThrough]
            set
            {
                if (value is DBNull)
                    this._PostoperationalPrimaryKeyValue = null;
                else
                    this._PostoperationalPrimaryKeyValue = value;
            }
        }

        private Dictionary<string, string> _CustomAttribs = new Dictionary<string, string>();
        
        /// <summary>
        /// Gets collection of custom attributes to be sent to client with response
        /// </summary>
        public Dictionary<string, string> CustomAttribs
        {
            [DebuggerStepThrough]
            get
            {
                return this._CustomAttribs;
            }
        }

        private Dictionary<Field, string> _Data = null;

        /// <summary>
        /// Gets reference to collection of field-value pairs to apply to database record during data action execution
        /// </summary>
        public Dictionary<Field, string> Data
        {
            [DebuggerStepThrough]
            get
            {
                return this._Data;
            }
        }

        private Dictionary<string, string> _UserData;
        /// <summary>
        /// Gets reference to UserData collection that may come with request
        /// </summary>
        public Dictionary<string, string> UserData
        {
            get
            {
                if (this._UserData == null)
                    this._UserData = new Dictionary<string, string>();
                return this._UserData;
            }
        }

        /// <summary>
        /// Gets or Sets table name
        /// </summary>
        public string TableName
        {
            [DebuggerStepThrough]
            get;
            [DebuggerStepThrough]
            set;
        }

        /// <summary>
        /// Gets or Sets primary key field
        /// </summary>
        public Field PrimaryKeyField
        {
            [DebuggerStepThrough]
            get;
            [DebuggerStepThrough]
            set;
        }

        /// <summary>
        /// Gets or Sets DataAction details
        /// </summary>
        public string Details
        {
            [DebuggerStepThrough]
            get;
            [DebuggerStepThrough]
            set;
        }

        private bool _Completed = false;
        /// <summary>
        /// Gets value indicating whether action was executed
        /// </summary>
        public bool Completed
        {
            [DebuggerStepThrough]
            get
            {
                return this._Completed;
            }
        }

        /// <summary>
        /// Creates new instance of DataAction
        /// </summary>
        /// <param name="ActionType">Type of data action</param>
        /// <param name="TableName">Table name</param>
        /// <param name="Data">Collection of column values</param>
        /// <param name="UserData">Collection of userdata values</param>
        /// <param name="PrimaryKeyField">PrimaryKey field</param>
        /// <param name="PrimaryKeyValue">PrimaryKey value</param>
        [DebuggerStepThrough]
        public DataAction(ActionType ActionType, string TableName, Dictionary<Field, string> Data, Dictionary<string, string> UserData, Field PrimaryKeyField, object PrimaryKeyValue)
            : this(ActionType, TableName, Data, UserData, PrimaryKeyField, PrimaryKeyValue, null)
        {
        }

        /// <summary>
        /// Creates new instance of DataAction
        /// </summary>
        /// <param name="CustomActionName">Name of action. ActionType will be automatically set to Custom</param>
        /// <param name="TableName">Table name</param>
        /// <param name="Data">Collection of column values</param>
        /// <param name="UserData">Collection of userdata values</param>
        /// <param name="PrimaryKeyField">PrimaryKey field</param>
        /// <param name="PrimaryKeyValue">PrimaryKey value</param>
        [DebuggerStepThrough]
        public DataAction(string CustomActionName, string TableName, Dictionary<Field, string> Data, Dictionary<string, string> UserData, Field PrimaryKeyField, object PrimaryKeyValue)
            : this(CustomActionName, TableName, Data, UserData, PrimaryKeyField, PrimaryKeyValue, null)
        {
        }

        /// <summary>
        /// Creates new instance of DataAction
        /// </summary>
        /// <param name="CustomActionName">Name of action. ActionType will be automatically set to Custom</param>
        /// <param name="TableName">Table name</param>
        /// <param name="Data">Collection of column values</param>
        /// <param name="UserData">Collection of userdata values</param>
        /// <param name="PrimaryKeyField">PrimaryKey field</param>
        /// <param name="PrimaryKeyValue">Old PrimaryKey value</param>
        /// <param name="PostoperationalPrimaryKeyValue">New PrimaryKey value</param>
        [DebuggerStepThrough]
        public DataAction(string CustomActionName, string TableName, Dictionary<Field, string> Data, Dictionary<string, string> UserData, Field PrimaryKeyField, object PrimaryKeyValue, object PostoperationalPrimaryKeyValue)
            : this(ActionType.Custom, TableName, Data, UserData, PrimaryKeyField, PrimaryKeyValue, PostoperationalPrimaryKeyValue)
        {
            this.CustomActionName = CustomActionName;
        }

        /// <summary>
        /// Creates new instance of DataAction
        /// </summary>
        /// <param name="ActionType">Type of data action</param>
        /// <param name="TableName">Table name</param>
        /// <param name="Data">Collection of column values</param>
        /// <param name="UserData">Collection of userdata values</param>
        /// <param name="PrimaryKeyField">PrimaryKey field</param>
        /// <param name="PrimaryKeyValue">Old PrimaryKey value</param>
        /// <param name="PostoperationalPrimaryKeyValue">New PrimaryKey value</param>
        public DataAction(ActionType ActionType, string TableName, Dictionary<Field, string> Data, Dictionary<string, string> UserData, Field PrimaryKeyField, object PrimaryKeyValue, object PostoperationalPrimaryKeyValue)
        {
            this.PrimaryKeyValue = PrimaryKeyValue;
            this.PostoperationalPrimaryKeyValue = PostoperationalPrimaryKeyValue;
            this._Data = Data;
            this.TableName = TableName;
            this.PrimaryKeyField = PrimaryKeyField;
            this.SetInitialActionType(ActionType);
            this._UserData = UserData;
        }

        /// <summary>
        /// Sets ActionType and modifies Completed state according to it
        /// </summary>
        /// <param name="ActionType">ActionType to set</param>
        private void SetInitialActionType(ActionType ActionType)
        {
            switch(ActionType)
            {
                case ActionType.Error:
                case ActionType.Invalid:
                    this._Completed = true;
                    break;
                default:
                    this._Completed = false;
                    break;
            }
            this._ActionType = ActionType;
        }

        /// <summary>
        /// Sets current action Completed status to true with applying new ActionType
        /// </summary>
        /// <param name="ActionType">New ActionType to apply</param>
        public void SetCompletedWithStatus(ActionType ActionType)
        {
            this._Completed = true;
            this._ActionType = ActionType;
        }

        /// <summary>
        /// Changes current action type. If new type is ActionType.Error or ActionType.Invalid current action will be marked as completed, no matter whether it was completed before or not.
        /// </summary>
        /// <param name="ActionType">New ActionType to apply</param>
        public void ChangeActionType(ActionType ActionType)
        {
            this.SetInitialActionType(ActionType);
        }

        /// <summary>
        /// Changes current action type to ActionType.Custom. Completed state will be turned to false
        /// </summary>
        /// <param name="CustomActionName">Custom action name</param>
        public void ChangeActionType(string CustomActionName)
        {
            this.SetInitialActionType(ActionType.Custom);
            this.CustomActionName = CustomActionName;
        }

        /// <summary>
        /// Marks current action as completed
        /// </summary>
        public void SetCompleted()
        {
            this._Completed = true;
        }

        /// <summary>
        /// Marks current action as failed
        /// </summary>
        public void SetFailed()
        {
            this.SetFailed(null);
        }

        /// <summary>
        /// Marks current action as failed
        /// </summary>
        /// <param name="Reason">Failure reason</param>
        public void SetFailed(string Reason)
        {
            this.Details = Reason;
            this.SetCompletedWithStatus(ActionType.Error);
        }

        /// <summary>
        /// Mark current action as Invalid
        /// </summary>
        public void SetInvalid()
        {
            this.SetInvalid(null);
        }

        /// <summary>
        /// Mark current action as Invalid
        /// </summary>
        /// <param name="Reason">Reason</param>
        public void SetInvalid(string Reason)
        {
            this.Details = Reason;
            this.SetCompletedWithStatus(ActionType.Invalid);
        }

        /// <summary>
        /// Render own content to XmlWriter
        /// </summary>
        /// <param name="writer">XmlWriter to render content to</param>
        public void Render(XmlWriter writer)
        {
            writer.WriteStartElement("action");
            writer.WriteAttributeString("type", this.ActionType == ActionType.Custom ? this.CustomActionName : this.ActionType.ToString().ToLower());
            writer.WriteAttributeString("sid", Convert.ToString(this.PrimaryKeyValue));
            writer.WriteAttributeString("tid", Convert.ToString(this.PostoperationalPrimaryKeyValue));
            foreach (KeyValuePair<string, string> attrib in this.CustomAttribs)
                writer.WriteAttributeString(attrib.Key, attrib.Value);
            if (!string.IsNullOrEmpty(this.Details))
                writer.WriteString(this.Details);
            writer.WriteEndElement();
        }

        /// <summary>
        /// Convert DataAction to its string representation
        /// </summary>
        /// <returns>String representation of DataAction</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("Action: type=").Append(this.ActionType == ActionType.Custom ? this.CustomActionName : this.ActionType.ToString().ToLower());
            sb.Append(",sid: ").Append(Convert.ToString(this.PrimaryKeyValue));
            sb.Append(",tid: ").Append(Convert.ToString(this.PostoperationalPrimaryKeyValue));
            sb.Append(", Details: ").Append(this.Details);
            return sb.ToString();
        }
    }

    /// <summary>
    /// Known action types
    /// </summary>
    public enum ActionType
    {
        /// <summary>
        /// Delete action
        /// </summary>
        Deleted,
        /// <summary>
        /// Insert action
        /// </summary>
        Inserted,
        /// <summary>
        /// Update action
        /// </summary>
        Updated,
        /// <summary>
        /// Action is invalid
        /// </summary>
        Invalid,
        /// <summary>
        /// Action caused an error
        /// </summary>
        Error,
        /// <summary>
        /// User-defined action
        /// </summary>
        Custom
    }
}
