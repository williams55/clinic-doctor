using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Collections.Specialized;
using System.Data;
using System.Xml;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace dhtmlxConnectors
{
    public partial class DataRequest
    {
        /// <summary>
        /// Enables or disables dynamic items loading for hierarchical dhtml controls. If set to true, only top items will be returned to the client.
        /// </summary>
        public bool EnableHierarchicalDynamicLoading
        {
            get;
            set;
        }


        private string _RootItemsRelationIDValue = "null";
        /// <summary>
        /// Gets or Sets foreign key value that is specific for root items in rows hierarchy
        /// </summary>
        public string RootItemRelationIDValue
        {
            get
            {
                return this._RootItemsRelationIDValue;
            }
            set
            {
                this._RootItemsRelationIDValue = value;
            }
        }

        private string _RelationIDValue = null;
        /// <summary>
        /// Gets or Sets current ForeignKey value for rows level being selected
        /// </summary>
        public string RelationIDValue
        {
            get
            {
                if (this._RelationIDValue == null)
                    return this._RootItemsRelationIDValue;
                else
                    return this._RelationIDValue;
            }
            set
            {
                this._RelationIDValue = value;
            }
        }

        /// <summary>
        /// Gets or Sets value indicating whether primary key columns can be inserted or updated. Default value is false - primary key fields are immutable
        /// </summary>
        public bool AllowPrimaryKeyUpdate
        {
            get;
            set;
        }

        /// <summary>
        /// Processes client request and performs operations requested by client components
        /// </summary>
        /// <param name="QueryString">QueryString collection of current request</param>
        /// <param name="Form">Form collection fof current request</param>
        public void ProcessRequest(NameValueCollection QueryString, NameValueCollection Form)
        {
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Processing client request. QueryString: " + QueryString.ToString() + ", Form: " + Form);
#endif
            #endregion
            this._RequestType = this.GetCurrentMode(QueryString);
            switch(this.RequestType)
            {
                //SELECTS
                case DataRequestType.Select:
                    UpdateSelectParams(QueryString);
                    break;
                //UPDATES, INSERTS, DELETES
                case DataRequestType.Edit:
                    this._DataActions.AddRange(ParseDataActions((QueryString["ids"] != null || QueryString["tr_id"] != null || QueryString["gr_id"] != null) ? QueryString : Form));
                    try
                    {
                        //start per-request transaction if necessary
                        if (this.TransactionMode == TransactionMode.PerRequest)
                        {
                            #region LOG ENTRY
#if !NO_LOG
                            Log.WriteLine(this, "Entering " + this.TransactionMode.ToString() + " transaction mode. ");
#endif
                            #endregion
                            this.Adapter.BeginTransaction();
                        }
                        //PROCESS DATA ACTIONS
                        foreach (DataAction action in this.DataActions)
                            for (int i = 0; i < this.DataActions.Count; i++)
                                this.ProcessDataAction(this.DataActions[i]);
                        //close pre-request transaction if necessary
                        if (this.TransactionMode == TransactionMode.PerRequest)
                        {
                            #region LOG ENTRY
#if !NO_LOG
			                Log.WriteLine(this, "Commiting " + this.TransactionMode.ToString() + " transaction.");
#endif
#endregion
                            this.Adapter.CommitTransaction();
                        }
                    }
                    catch (Exception ex1)
                    {
                        #region LOG ENTRY
#if !NO_LOG
                        Log.WriteLine(this, "Exception cought: " + ex1.Message);
#endif
                        #endregion
                        try
                        {
                            //if there was transaction - rollback it
                            if (this.TransactionMode == TransactionMode.PerRequest)
                            {
                                #region LOG ENTRY
#if !NO_LOG
                                Log.WriteLine(this, "Rolling back " + this.TransactionMode.ToString() + " transaction because of the following exception: " + ex1.Message);
#endif
                                #endregion
                                this.Adapter.RollbackTransaction();
                            }
                            //mark all data actions with failed and Completed state, so now their faith is user's concern
                            foreach (DataAction action in this.DataActions)
                                action.SetFailed(ex1.Message);
                        }
                            //Exception that probably occured while trying to rollback transaction
                        catch (Exception ex2)
                        {
                            #region LOG ENTRY
#if !NO_LOG
                            Log.WriteLine(this, "Exception cought: " + ex2.Message);
#endif
                            #endregion
                            foreach (DataAction action in this.DataActions)
                                action.SetFailed(ex1.Message + Environment.NewLine + "Transaction rollback failed with the following error: " + ex2.Message);
                        }
                    }
                    break;
                //SOMETHING UNEXPECTED
                default:
                    throw new NotImplementedException(this.RequestType.ToString() + " mode behavior has not been implemented yet!");
            }
        }

        /// <summary>
        /// Converts data actions from query string format to DataAction objects collection
        /// </summary>
        /// <param name="QueryString">QueryString collection that contains client data actions</param>
        /// <returns>Collection of DataAction objects</returns>
        protected List<DataAction> ParseDataActions(NameValueCollection QueryString)
        {
            List<DataAction> results = new List<DataAction>();
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Parsing data actions.");
#endif
            #endregion
            if (QueryString["ids"] != null)
            {
                string[] ids = QueryString["ids"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string id in ids)
                {
                    #region LOG ENTRY
#if !NO_LOG
                    Log.WriteLine(this, "Now parsing data action for record with id=" + id + ".");
#endif
                    #endregion
                    ActionType actionType = this.ParseActionType(QueryString[id + "_!nativeeditor_status"]);
                    if (actionType == ActionType.Custom)//custom action has another set of constructor parameters
                        results.Add(
                            new DataAction(
                                QueryString[id + "_!nativeeditor_status"],
                                this.TableName,
                                GetRowValues(QueryString, id),
                                GetUserDataValues(QueryString, id),
                                this.PrimaryKeyField,
                                id)
                            );
                    else
                        results.Add(//create data action of predefined type
                            new DataAction(
                                this.ParseActionType(QueryString[id + "_!nativeeditor_status"]),
                                this.TableName,
                                GetRowValues(QueryString, id),
                                GetUserDataValues(QueryString, id),
                                this.PrimaryKeyField,
                                id)
                            );
                    #region LOG ENTRY
#if !NO_LOG
                    Log.WriteLine(this, "Parsed new action: " + results.Last().ToString());
#endif
                    #endregion
                }
            }
            else //special for tree connector
            {
                #region LOG ENTRY
#if !NO_LOG
                Log.WriteLine(this, "Now parsing data action for record with id=" + QueryString["tr_id"] + ".");
#endif
                #endregion
                string ItemID = string.IsNullOrEmpty(QueryString["tr_id"]) ? QueryString["gr_id"] : QueryString["tr_id"];
                ActionType actionType = this.ParseActionType(QueryString["!nativeeditor_status"]);
                if (actionType == ActionType.Custom)//custom action has another set of constructor parameters
                    results.Add(
                        new DataAction(
                            QueryString["!nativeeditor_status"],
                            this.TableName,
                            GetRowValues(QueryString, ""),
                            GetUserDataValues(QueryString, ""),
                            this.PrimaryKeyField,
                            ItemID)
                        );
                else
                    results.Add(//create data action of predefined type
                        new DataAction(
                            this.ParseActionType(QueryString["!nativeeditor_status"]),
                            this.TableName,
                            GetRowValues(QueryString, ""),
                            GetUserDataValues(QueryString, ""),
                            this.PrimaryKeyField,
                            ItemID)
                        );
                #region LOG ENTRY
#if !NO_LOG
                Log.WriteLine(this, "Parsed new action: " + results.Last().ToString());
#endif
                #endregion
            }
            return results;
        }

        /// <summary>
        /// Executes DataAction object (if it's not marked as completed)
        /// </summary>
        /// <param name="Action">DataAction object to execute</param>
        protected void ProcessDataAction(DataAction Action)
        {
            //BEFORE EVENT
            if (this.BeforeDataActionProcessing != null)
            {
                #region LOG ENTRY
#if !NO_LOG
                Log.WriteLine(this, "Calling BeforeDataActionProcessing event(Action = [" + Action.ToString() + "]");
#endif
                #endregion
                this.BeforeDataActionProcessing(this, new DataActionProcessingEventArgs(Action));
            }
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Processing data action (Action = [" + Action.ToString() + "]");
#endif
            #endregion
            //PROCESSING
            if (!Action.Completed)
                try
                {
                    //Open transaction if necessary
                    if (this.TransactionMode == TransactionMode.PerRecord)
                    {
                        #region LOG ENTRY
#if !NO_LOG
                        Log.WriteLine(this, "Entering " + this.TransactionMode.ToString() + " transaction mode");
#endif
                        #endregion
                        this.Adapter.BeginTransaction();
                    }
                    switch (Action.ActionType)
                    {
                        case ActionType.Inserted:
                            this.DoInsertAction(Action);
                            break;
                        case ActionType.Updated:
                            this.DoUpdateAction(Action);
                            break;
                        case ActionType.Deleted:
                            this.DoDeleteAction(Action);
                            break;
                        // CUSTOM ACTION
                        default:
                            break;
                    }
                    //close transaction if necessary
                    if (this.TransactionMode == TransactionMode.PerRecord)
                    {
                        #region LOG ENTRY
#if !NO_LOG
                        Log.WriteLine(this, "Commiting " + this.TransactionMode.ToString() + " transaction");
#endif
                        #endregion
                        this.Adapter.CommitTransaction();
                    }
                    //now data action is finished
                    Action.SetCompleted();
                }
                catch (Exception ex1)
                {
                    #region LOG ENTRY
#if !NO_LOG
                    Log.WriteLine(this, "Exception cought: " + ex1.Message);
#endif
                    #endregion
                    try
                    {
                        //rollback transaction if necessary
                        if (this.TransactionMode == TransactionMode.PerRecord)
                        {
                            #region LOG ENTRY
#if !NO_LOG
                            Log.WriteLine(this, "Rolling back " + this.TransactionMode.ToString() + " transaction");
#endif
                            #endregion
                            this.Adapter.RollbackTransaction();
                        }
                        #region LOG ENTRY
#if !NO_LOG
                        Log.WriteLine(this, "ERROR: " + ex1.Message);
#endif
                        #endregion
                        Action.SetFailed(ex1.Message);
                    }
                    catch (Exception ex2)
                    {
                        #region LOG ENTRY
#if !NO_LOG
                        Log.WriteLine(this, "Exception cought: " + ex2.Message);
#endif
                        #endregion
                        Action.SetFailed(ex1.Message + Environment.NewLine + "Transaction rollback failed with the following error: " + ex2.Message);
                    }
                }
            //AFTER EVENT
            if (this.DataActionProcessed != null)
            {
                #region LOG ENTRY
#if !NO_LOG
                Log.WriteLine(this, "Calling DataActionProcessed event. Action=[" + Action.ToString() + "]");
#endif
                #endregion
                this.DataActionProcessed(this, new DataActionProcessingEventArgs(Action));
            }
        }

        /// <summary>
        /// Executes Insert DataAction
        /// </summary>
        /// <param name="Action">DataAction to execute</param>
        private void DoInsertAction(DataAction Action)
        {
            // BEFORE INSERT
            if (this.BeforeInsert != null)
            {
                #region LOG ENTRY
#if !NO_LOG
                Log.WriteLine(this, "Calling BeforeInsert event(Action = [" + Action.ToString() + "]");
#endif
                #endregion
                this.BeforeInsert(this, new DataActionProcessingEventArgs(Action));
            }
            this.AssertHasAccess(AccessRights.Insert);
            // INSERT ITSELF
            if (!Action.Completed)
            {
                #region LOG ENTRY
#if !NO_LOG
                Log.WriteLine(this, "Executing insert action.");
#endif
                #endregion
                if (this.CustomSQLs.ContainsKey(CustomSQLType.Insert))
                {
                    string sql = this.ParseSQLTemplate(this.CustomSQLs[CustomSQLType.Insert], Action.Data, Action.PrimaryKeyField, Action.PrimaryKeyValue);
                    Action.PostoperationalPrimaryKeyValue = this.Adapter.ExecuteScalar(sql);
                }
                else
                    Action.PostoperationalPrimaryKeyValue = this.Adapter.ExecuteInsertQuery(Action.TableName, Action.Data, Action.PrimaryKeyField, Action.PrimaryKeyValue);
                #region LOG ENTRY
#if !NO_LOG
                Log.WriteLine(this, "Action completed. PostoperationalPrimaryKeyValue is " + Convert.ToString(Action.PostoperationalPrimaryKeyValue ?? "null") + ".");
#endif
                #endregion
            }
            #region LOG ENTRY
#if !NO_LOG
            else
                Log.WriteLine(this, "Skipping insert action, because it's marked as completed.");
#endif
            #endregion

            // AFTER INSERT
            if (this.Inserted != null)
            {
                #region LOG ENTRY
#if !NO_LOG
                Log.WriteLine(this, "Calling Inserted event. Action =[" + Action.ToString() + "]");
#endif
                #endregion
                this.Inserted(this, new DataActionProcessingEventArgs(Action));
            }
        }

        /// <summary>
        /// Executes Update DataAction
        /// </summary>
        /// <param name="Action">DataAction to execute</param>
        private void DoUpdateAction(DataAction Action)
        {
            // BEFORE UPDATE
            if (this.BeforeUpdate != null)
            {
                #region LOG ENTRY
#if !NO_LOG
                Log.WriteLine(this, "Calling BeforeUpdate event(Action = [" + Action.ToString() + "]");
#endif
                #endregion
                this.BeforeUpdate(this, new DataActionProcessingEventArgs(Action));
            }
            this.AssertHasAccess(AccessRights.Update);
            // UPDATE ITSELF
            if (!Action.Completed)
            {
                #region LOG ENTRY
#if !NO_LOG
                Log.WriteLine(this, "Executing update action.");
#endif
                #endregion
                if (this.CustomSQLs.ContainsKey(CustomSQLType.Update))
                {
                    string sql = this.ParseSQLTemplate(this.CustomSQLs[CustomSQLType.Update], Action.Data, Action.PrimaryKeyField, Action.PrimaryKeyValue);
                    Action.PostoperationalPrimaryKeyValue = this.Adapter.ExecuteScalar(sql);
                }
                else
                    this.Adapter.ExecuteUpdateQuery(Action.TableName, Action.Data, Action.PrimaryKeyField, Action.PrimaryKeyValue);
            }
            #region LOG ENTRY
#if !NO_LOG
            else
                Log.WriteLine(this, "Skipping update action, because it's marked as completed.");
#endif
            #endregion

            // AFTER UPDATE
            if (this.Updated != null)
            {
                #region LOG ENTRY
#if !NO_LOG
                Log.WriteLine(this, "Calling Updated event. Action =[" + Action.ToString() + "]");
#endif
                #endregion
                this.Updated(this, new DataActionProcessingEventArgs(Action));
            }
        }

        /// <summary>
        /// Executes Delete DataAction
        /// </summary>
        /// <param name="Action">DataAction to execute</param>
        private void DoDeleteAction(DataAction Action)
        {
            // BEFORE DELETE
            if (this.BeforeDelete != null)
            {
                #region LOG ENTRY
#if !NO_LOG
                Log.WriteLine(this, "Calling BeforeDelete event(Action = [" + Action.ToString() + "]");
#endif
                #endregion
                this.BeforeDelete(this, new DataActionProcessingEventArgs(Action));
            }
            this.AssertHasAccess(AccessRights.Delete);
            // DELETE ITSELF
            if (!Action.Completed)
            {
                #region LOG ENTRY
#if !NO_LOG
                Log.WriteLine(this, "Executing delete action.");
#endif
                #endregion
                if (this.CustomSQLs.ContainsKey(CustomSQLType.Delete))
                {
                    string sql = this.ParseSQLTemplate(this.CustomSQLs[CustomSQLType.Delete], Action.Data, Action.PrimaryKeyField, Action.PrimaryKeyValue);
                    Action.PostoperationalPrimaryKeyValue = this.Adapter.ExecuteScalar(sql);
                }
                else
                    this.Adapter.ExecuteDeleteQuery(Action.TableName, Action.PrimaryKeyField, Action.PrimaryKeyValue);
            }
            #region LOG ENTRY
#if !NO_LOG
            else
                Log.WriteLine(this, "Skipping delete action, because it's marked as completed.");
#endif
            #endregion
            // DELETED
            if (this.Deleted != null)
            {
                #region LOG ENTRY
#if !NO_LOG
                Log.WriteLine(this, "Calling Deleted event. Action =[" + Action.ToString() + "]");
#endif
                #endregion
                this.Deleted(this, new DataActionProcessingEventArgs(Action));
            }
        }

        /// <summary>
        /// Converts custom SQL template into ready-to-execute sql statement
        /// </summary>
        /// <param name="SQL">SQL template</param>
        /// <param name="Data">Collection of field-value pairs to use for template population</param>
        /// <param name="PrimaryKeyField">Primary key field</param>
        /// <param name="PrimaryKeyValue">Primary key value</param>
        /// <returns>Ready-to-execute sql statement</returns>
        private string ParseSQLTemplate(string SQL, Dictionary<Field, string> Data, Field PrimaryKeyField, object PrimaryKeyValue)
        {
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Parsing custom sql: " + SQL);
#endif
            #endregion
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Current values are: [" + string.Join(",", Data.Select(item => item.Key.ExternalName + " = " + item.Value).ToArray()) + "]");
#endif
            #endregion
            foreach (Field field in Data.Keys)
                if (SQL.Contains("{" + field.ExternalName + "}"))
                    SQL = SQL.Replace("{" + field.ExternalName + "}", Tools.EscapeQueryValue(Data[field]));
            if (PrimaryKeyField != null && SQL.Contains("{" + PrimaryKeyField.ExternalName + "}"))
                SQL = SQL.Replace("{" + PrimaryKeyField.ExternalName + "}", Tools.EscapeQueryValue(PrimaryKeyValue));
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Parsed custom sql: " + SQL);
#endif
            #endregion
            return SQL;
        }

        /// <summary>
        /// Checks if current security settings allow DataRequest run particular database operation. If DataRequest has unsufficient access rights - exception will be thrown
        /// </summary>
        /// <param name="DesiredAccess">Access rights to check</param>
        private void AssertHasAccess(AccessRights DesiredAccess)
        {
            if ((uint)(this.AllowedAccess & DesiredAccess) == 0)
                throw new dhtmlxException("Current security settings deny '" + DesiredAccess.ToString() + "' operations over " + this.Adapter.GetType().Name + "!");
        }

        /// <summary>
        /// Converts string representation of ActionType into ActionType enum instance. If type is unknown - ActionType.Custom will be returned.
        /// </summary>
        /// <param name="Value">Value to convert</param>
        /// <returns>ActionType that represents value provided. If type is unknown - ActionType.Custom will be returned.</returns>
        private ActionType ParseActionType(string Value)
        {
            try
            {
                return (ActionType)Enum.Parse(typeof(ActionType), Value, true);
            }
            catch
            {
                return ActionType.Custom;
            }
        }

        /// <summary>
        /// Excracts column-value pairs came from client string into its object representation
        /// </summary>
        /// <param name="QueryString">QueryString to process</param>
        /// <param name="rowID">RowID to extract fields for (come from QueryString also)</param>
        /// <returns>Collection of Field-value pairs</returns>
        protected Dictionary<Field, string> GetRowValues(NameValueCollection QueryString, string rowID)
        {
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Extracting new column values for rowID=" + rowID); 
#endif
            #endregion
            Dictionary<Field, string> rowValues = new Dictionary<Field, string>();
            string prefix = (string.IsNullOrEmpty(rowID) ? "" : rowID + "_");
            IEnumerable<string> thisRowFields = QueryString.Keys.Cast<string>().Where(k => k.StartsWith(prefix) && k != (prefix + "gr_id") && k != (prefix + "!nativeeditor_status")).Select(k => k.Substring(prefix.Length));
            foreach (string encodedRowField in thisRowFields)
            {
                Field rowField = this.Connector.DecodeField(encodedRowField);
                if (rowField == null || (this.PrimaryKeyField == rowField && !this.AllowPrimaryKeyUpdate))
                    continue;
                else
                    rowValues.Add(rowField, QueryString[prefix + encodedRowField]);
            }
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Extracted values are: [" + string.Join(",", rowValues.Select(item => item.Key.ExternalName + " = " + item.Value).ToArray()) + "]");
#endif
            #endregion
            return rowValues;
        }

        /// <summary>
        /// Extracts key-value userdata items from query string
        /// </summary>
        /// <param name="QueryString">QueryString to process</param>
        /// <param name="rowID">RowID to extract userdata for (come from QueryString also)</param>
        /// <returns>Collection of key-value pairs</returns>
        protected Dictionary<string, string> GetUserDataValues(NameValueCollection QueryString, string rowID)
        {
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Extracting UserData for rowID=" + rowID);
#endif
            #endregion
            Dictionary<string, string> userData = new Dictionary<string, string>();
            string prefix = rowID + "_";
            IEnumerable<string> thisRowFields = QueryString.Keys.Cast<string>().Where(k => k.StartsWith(prefix) && k != (prefix + "gr_id") && k != (prefix + "!nativeeditor_status")).Select(k => k.Substring(prefix.Length));
            foreach (string encodedRowField in thisRowFields)
                if (this.Connector.DecodeField(encodedRowField) == null)
                    userData.Add(encodedRowField, QueryString[prefix + encodedRowField]);

            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Extracted UserData is: [" + string.Join(",", userData.Select(item => item.Key + " = " + item.Value).ToArray()) + "]");
#endif
            #endregion
            return userData;
        }

        /// <summary>
        /// Gets type of current request
        /// </summary>
        /// <param name="QueryString">QueryString collection for current request</param>
        /// <returns>DataRequestType value that represents current request type (Select or Edit)</returns>
        protected DataRequestType GetCurrentMode(NameValueCollection QueryString)
        {
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Resolving type of request."); 
#endif
            #endregion 
            if (QueryString["!nativeeditor_status"] != null)
                return DataRequestType.Edit;
            if (string.IsNullOrEmpty(QueryString["editing"]) || !string.IsNullOrEmpty(QueryString["connector"]))
                return DataRequestType.Select;
            else
                return DataRequestType.Edit;
        }

        /// <summary>
        /// Updates DataRequest settigns with new ones token from client request QueryString collection
        /// </summary>
        /// <param name="QueryString">QueryString to take new parameters from</param>
        protected virtual void UpdateSelectParams(NameValueCollection QueryString)
        {
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Processing client select."); 
#endif
            #endregion
            //posStart, count
            if (!string.IsNullOrEmpty(QueryString["posStart"]))
            {
                #region LOG ENTRY
#if !NO_LOG
                Log.WriteLine(this, "Parsing start index ('posStart') value."); 
#endif
                #endregion
                this.StartIndex = Convert.ToInt32(QueryString["posStart"]);
            }
            if (!string.IsNullOrEmpty(QueryString["count"]))
            {
                #region LOG ENTRY
#if !NO_LOG
                Log.WriteLine(this, "Parsing count ('count') value"); 
#endif
                #endregion
                this.Count = Convert.ToInt32(QueryString["count"]);
            }
            foreach (string Key in QueryString.Keys)
            {
                //id
                if (Key == "id")
                {
                    #region LOG ENTRY
#if !NO_LOG
                    Log.WriteLine(this, "Foreign key value: " + Key); 
#endif
                    #endregion
                    this.RelationIDValue = QueryString[Key];
                }
                //dhx_filter
                if (Key.StartsWith("dhx_filter") && !string.IsNullOrEmpty(QueryString[Key]))
                    this.Rules.Add(this.ParseQueryStringRuleItem(Key, QueryString[Key]));
                //dhx_sort
                if (Key.StartsWith("dhx_sort") && !string.IsNullOrEmpty(QueryString[Key]))
                    this.OrderBy.Add(this.ParseQueryStringOrderByItem(Key, QueryString[Key]));

            }
            if (this.EnableHierarchicalDynamicLoading && this.ParentRecordIDField != null)
            {
                this.Rules.Add(new FieldRule(this.ParentRecordIDField, Operator.Equals, this.RelationIDValue));
                #region LOG ENTRY
#if !NO_LOG
                Log.WriteLine(this, "Dynamic loading is enabled as well as foreign key field is set -> adding additional select rule: " + this.Rules[this.Rules.Count - 1].ToString()); 
#endif
                #endregion
            }
        }

        /// <summary>
        /// Converts rule stored in QueryString into its object representation
        /// </summary>
        /// <param name="ParamName">Name of QueryString parameter that represents FieldRule</param>
        /// <param name="ParamValue">Value of QueryString parameter that represents FieldRule</param>
        /// <returns>FieldRule object</returns>
        protected FieldRule ParseQueryStringRuleItem(string ParamName, string ParamValue)
        {
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Parsing rule(" + ParamName + " -> " + ParamValue + ")."); 
#endif
            #endregion 
            int FieldIndex = Int32.Parse(Regex.Replace(ParamName, "[^0-9]*", ""));
            FieldRule newRule = new FieldRule(this.RequestedFields[FieldIndex], Operator.Like, "%" + ParamValue + "%");
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Parsed rule is: " + newRule.ToString()); 
#endif
            #endregion 
            return newRule;
        }

        /// <summary>
        /// Converts OrderByField stored in QueryString into its object representation
        /// </summary>
        /// <param name="ParamName">Name of QueryString parameter that represents OrderByField</param>
        /// <param name="ParamValue">Value of QueryString parameter that represents OrderByField</param>
        /// <returns>OrderByField object</returns>
        protected OrderByField ParseQueryStringOrderByItem(string ParamName, string ParamValue)
        {
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Parsing order by statement(" + ParamName + " -> " + ParamValue + ")."); 
#endif
            #endregion
            int FieldIndex = Int32.Parse(Regex.Replace(ParamName, "[^0-9]*", ""));
            OrderByField order = new OrderByField(this.RequestedFields[FieldIndex], ParamValue);
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Parsed statement is: " + order.ToString());  
#endif
            #endregion
            return order;
        }

        //before/after insert/update/delete/select
        //before/after processing
        
        /// <summary>
        /// Event to be fired before any DataAction is executed
        /// </summary>
        public event EventHandler<DataActionProcessingEventArgs> BeforeDataActionProcessing;
        
        /// <summary>
        /// Event to be fired after any DataAction was executed
        /// </summary>
        public event EventHandler<DataActionProcessingEventArgs> DataActionProcessed;

        /// <summary>
        /// Event to be fired before any insert DataAction is executed
        /// </summary>
        public event EventHandler<DataActionProcessingEventArgs> BeforeInsert;
        
        /// <summary>
        /// Event to be fired after insert DataAction was executed
        /// </summary>
        public event EventHandler<DataActionProcessingEventArgs> Inserted;

        /// <summary>
        /// Event to be fired before any update DataAction is executed
        /// </summary>
        public event EventHandler<DataActionProcessingEventArgs> BeforeUpdate;

        /// <summary>
        /// Event to be fired after Update DataAction was executed
        /// </summary>
        public event EventHandler<DataActionProcessingEventArgs> Updated;

        /// <summary>
        /// Event to be fired before any delete DataAction is executed
        /// </summary>
        public event EventHandler<DataActionProcessingEventArgs> BeforeDelete;

        /// <summary>
        /// Event to be fired after delete DataAction was executed
        /// </summary>
        public event EventHandler<DataActionProcessingEventArgs> Deleted;

        /// <summary>
        /// Event to be fired before data select
        /// </summary>
        public event EventHandler BeforeSelect;

        /// <summary>
        /// Event to be fired after select results were retrieved
        /// </summary>
        public event EventHandler<DataSelectedEventArgs> Selected;
        
        /// <summary>
        /// Outputs DataAction of Select results into XmlWriter provided
        /// </summary>
        /// <param name="xWriter">Writer where to put rendered results</param>
        public void Render(XmlWriter xWriter)
        {
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Enterering Render method."); 
#endif
            #endregion 
            switch (this.RequestType)
            {
                case DataRequestType.Select:
                    //perform data select
                    if (this.BeforeSelect != null)
                    {
                        #region LOG ENTRY
#if !NO_LOG
                        Log.WriteLine(this, "Calling BeforeSelect event");  
#endif
                        #endregion
                        this.BeforeSelect(this, EventArgs.Empty);
                    }
                    this.AssertHasAccess(AccessRights.Select);
                    #region LOG ENTRY
#if !NO_LOG
                    Log.WriteLine(this, "Selecting data.");  
#endif
                    #endregion
                    DataTable dataToRender = this.Adapter.ExecuteSelectQuery(this.TableName, this.RequestedFields.Optimize().ToList(), this.Rules, this.OrderBy, this.StartIndex, this.Count);
                    #region LOG ENTRY
#if !NO_LOG
                    Log.WriteLine(this, "Selecting items count.");  
#endif
                    #endregion
                    int totalRowsCount = this.Adapter.ExecuteGetCountQuery(this.TableName, this.Rules);
                    //render data itself
                    if (this.Selected != null)
                    {
                        #region LOG ENTRY
#if !NO_LOG
                        Log.WriteLine(this, "Calling Selected event"); 
#endif
                        #endregion 
                        this.Selected(this, new DataSelectedEventArgs(dataToRender, totalRowsCount));
                    }
                    this.Connector.RenderData(xWriter, dataToRender, totalRowsCount);
                    break;
                default:
                    //case DataRequestType.Delete:
                    this.Connector.RenderActions(xWriter, this.DataActions);
                    break;
            }
        }
    }

}
