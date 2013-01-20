#region Using directives

using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;

using AppointmentSystem.Entities;
using AppointmentSystem.Data;

#endregion

namespace AppointmentSystem.Data.Bases
{	
	///<summary>
	/// This class is the base class for any <see cref="MessageConfigProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class MessageConfigProviderBaseCore : EntityProviderBase<AppointmentSystem.Entities.MessageConfig, AppointmentSystem.Entities.MessageConfigKey>
	{		
		#region Get from Many To Many Relationship Functions
		#endregion	
		
		#region Delete Methods

		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager">A <see cref="TransactionManager"/> object.</param>
		/// <param name="key">The unique identifier of the row to delete.</param>
		/// <returns>Returns true if operation suceeded.</returns>
		public override bool Delete(TransactionManager transactionManager, AppointmentSystem.Entities.MessageConfigKey key)
		{
			return Delete(transactionManager, key.MessageKey);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_messageKey">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.String _messageKey)
		{
			return Delete(null, _messageKey);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_messageKey">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.String _messageKey);		
		
		#endregion Delete Methods
		
		#region Get By Foreign Key Functions
		#endregion

		#region Get By Index Functions
		
		/// <summary>
		/// 	Gets a row from the DataSource based on its primary key.
		/// </summary>
		/// <param name="transactionManager">A <see cref="TransactionManager"/> object.</param>
		/// <param name="key">The unique identifier of the row to retrieve.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <returns>Returns an instance of the Entity class.</returns>
		public override AppointmentSystem.Entities.MessageConfig Get(TransactionManager transactionManager, AppointmentSystem.Entities.MessageConfigKey key, int start, int pageLength)
		{
			return GetByMessageKey(transactionManager, key.MessageKey, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_MessageConfig index.
		/// </summary>
		/// <param name="_messageKey"></param>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.MessageConfig"/> class.</returns>
		public AppointmentSystem.Entities.MessageConfig GetByMessageKey(System.String _messageKey)
		{
			int count = -1;
			return GetByMessageKey(null,_messageKey, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_MessageConfig index.
		/// </summary>
		/// <param name="_messageKey"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.MessageConfig"/> class.</returns>
		public AppointmentSystem.Entities.MessageConfig GetByMessageKey(System.String _messageKey, int start, int pageLength)
		{
			int count = -1;
			return GetByMessageKey(null, _messageKey, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_MessageConfig index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_messageKey"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.MessageConfig"/> class.</returns>
		public AppointmentSystem.Entities.MessageConfig GetByMessageKey(TransactionManager transactionManager, System.String _messageKey)
		{
			int count = -1;
			return GetByMessageKey(transactionManager, _messageKey, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_MessageConfig index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_messageKey"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.MessageConfig"/> class.</returns>
		public AppointmentSystem.Entities.MessageConfig GetByMessageKey(TransactionManager transactionManager, System.String _messageKey, int start, int pageLength)
		{
			int count = -1;
			return GetByMessageKey(transactionManager, _messageKey, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_MessageConfig index.
		/// </summary>
		/// <param name="_messageKey"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.MessageConfig"/> class.</returns>
		public AppointmentSystem.Entities.MessageConfig GetByMessageKey(System.String _messageKey, int start, int pageLength, out int count)
		{
			return GetByMessageKey(null, _messageKey, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_MessageConfig index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_messageKey"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.MessageConfig"/> class.</returns>
		public abstract AppointmentSystem.Entities.MessageConfig GetByMessageKey(TransactionManager transactionManager, System.String _messageKey, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;MessageConfig&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;MessageConfig&gt;"/></returns>
		public static TList<MessageConfig> Fill(IDataReader reader, TList<MessageConfig> rows, int start, int pageLength)
		{
			NetTiersProvider currentProvider = DataRepository.Provider;
            bool useEntityFactory = currentProvider.UseEntityFactory;
            bool enableEntityTracking = currentProvider.EnableEntityTracking;
            LoadPolicy currentLoadPolicy = currentProvider.CurrentLoadPolicy;
			Type entityCreationFactoryType = currentProvider.EntityCreationalFactoryType;
			
			// advance to the starting row
			for (int i = 0; i < start; i++)
			{
				if (!reader.Read())
				return rows; // not enough rows, just return
			}
			for (int i = 0; i < pageLength; i++)
			{
				if (!reader.Read())
					break; // we are done
					
				string key = null;
				
				AppointmentSystem.Entities.MessageConfig c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("MessageConfig")
					.Append("|").Append((System.String)reader[((int)MessageConfigColumn.MessageKey - 1)]).ToString();
					c = EntityManager.LocateOrCreate<MessageConfig>(
					key.ToString(), // EntityTrackingKey
					"MessageConfig",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new AppointmentSystem.Entities.MessageConfig();
				}
				
				if (!enableEntityTracking ||
					c.EntityState == EntityState.Added ||
					(enableEntityTracking &&
					
						(
							(currentLoadPolicy == LoadPolicy.PreserveChanges && c.EntityState == EntityState.Unchanged) ||
							(currentLoadPolicy == LoadPolicy.DiscardChanges && c.EntityState != EntityState.Unchanged)
						)
					))
				{
					c.SuppressEntityEvents = true;
					c.MessageKey = (System.String)reader[((int)MessageConfigColumn.MessageKey - 1)];
					c.OriginalMessageKey = c.MessageKey;
					c.MessageValue = (reader.IsDBNull(((int)MessageConfigColumn.MessageValue - 1)))?null:(System.String)reader[((int)MessageConfigColumn.MessageValue - 1)];
					c.Description = (reader.IsDBNull(((int)MessageConfigColumn.Description - 1)))?null:(System.String)reader[((int)MessageConfigColumn.Description - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="AppointmentSystem.Entities.MessageConfig"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.MessageConfig"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, AppointmentSystem.Entities.MessageConfig entity)
		{
			if (!reader.Read()) return;
			
			entity.MessageKey = (System.String)reader[((int)MessageConfigColumn.MessageKey - 1)];
			entity.OriginalMessageKey = (System.String)reader["MessageKey"];
			entity.MessageValue = (reader.IsDBNull(((int)MessageConfigColumn.MessageValue - 1)))?null:(System.String)reader[((int)MessageConfigColumn.MessageValue - 1)];
			entity.Description = (reader.IsDBNull(((int)MessageConfigColumn.Description - 1)))?null:(System.String)reader[((int)MessageConfigColumn.Description - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="AppointmentSystem.Entities.MessageConfig"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.MessageConfig"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, AppointmentSystem.Entities.MessageConfig entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.MessageKey = (System.String)dataRow["MessageKey"];
			entity.OriginalMessageKey = (System.String)dataRow["MessageKey"];
			entity.MessageValue = Convert.IsDBNull(dataRow["MessageValue"]) ? null : (System.String)dataRow["MessageValue"];
			entity.Description = Convert.IsDBNull(dataRow["Description"]) ? null : (System.String)dataRow["Description"];
			entity.AcceptChanges();
		}
		#endregion 
		
		#region DeepLoad Methods
		/// <summary>
		/// Deep Loads the <see cref="IEntity"/> object with criteria based of the child 
		/// property collections only N Levels Deep based on the <see cref="DeepLoadType"/>.
		/// </summary>
		/// <remarks>
		/// Use this method with caution as it is possible to DeepLoad with Recursion and traverse an entire object graph.
		/// </remarks>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.MessageConfig"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">AppointmentSystem.Entities.MessageConfig Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, AppointmentSystem.Entities.MessageConfig entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			
			//Fire all DeepLoad Items
			foreach(KeyValuePair<Delegate, object> pair in deepHandles.Values)
		    {
                pair.Key.DynamicInvoke((object[])pair.Value);
		    }
			deepHandles = null;
		}
		
		#endregion 
		
		#region DeepSave Methods

		/// <summary>
		/// Deep Save the entire object graph of the AppointmentSystem.Entities.MessageConfig object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">AppointmentSystem.Entities.MessageConfig instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">AppointmentSystem.Entities.MessageConfig Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, AppointmentSystem.Entities.MessageConfig entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			#endregion Composite Parent Properties

			// Save Root Entity through Provider
			if (!entity.IsDeleted)
				this.Save(transactionManager, entity);
			
			//used to hold DeepSave method delegates and fire after all the local children have been saved.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			//Fire all DeepSave Items
			foreach(KeyValuePair<Delegate, object> pair in deepHandles.Values)
		    {
                pair.Key.DynamicInvoke((object[])pair.Value);
		    }
			
			// Save Root Entity through Provider, if not already saved in delete mode
			if (entity.IsDeleted)
				this.Save(transactionManager, entity);
				

			deepHandles = null;
						
			return true;
		}
		#endregion
	} // end class
	
	#region MessageConfigChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>AppointmentSystem.Entities.MessageConfig</c>
	///</summary>
	public enum MessageConfigChildEntityTypes
	{
	}
	
	#endregion MessageConfigChildEntityTypes
	
	#region MessageConfigFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;MessageConfigColumn&gt;"/> class
	/// that is used exclusively with a <see cref="MessageConfig"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class MessageConfigFilterBuilder : SqlFilterBuilder<MessageConfigColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the MessageConfigFilterBuilder class.
		/// </summary>
		public MessageConfigFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the MessageConfigFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public MessageConfigFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the MessageConfigFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public MessageConfigFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion MessageConfigFilterBuilder
	
	#region MessageConfigParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;MessageConfigColumn&gt;"/> class
	/// that is used exclusively with a <see cref="MessageConfig"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class MessageConfigParameterBuilder : ParameterizedSqlFilterBuilder<MessageConfigColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the MessageConfigParameterBuilder class.
		/// </summary>
		public MessageConfigParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the MessageConfigParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public MessageConfigParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the MessageConfigParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public MessageConfigParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion MessageConfigParameterBuilder
	
	#region MessageConfigSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;MessageConfigColumn&gt;"/> class
	/// that is used exclusively with a <see cref="MessageConfig"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class MessageConfigSortBuilder : SqlSortBuilder<MessageConfigColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the MessageConfigSqlSortBuilder class.
		/// </summary>
		public MessageConfigSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion MessageConfigSortBuilder
	
} // end namespace
