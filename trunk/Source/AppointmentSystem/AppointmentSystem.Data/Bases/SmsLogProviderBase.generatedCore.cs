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
	/// This class is the base class for any <see cref="SmsLogProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class SmsLogProviderBaseCore : EntityProviderBase<AppointmentSystem.Entities.SmsLog, AppointmentSystem.Entities.SmsLogKey>
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
		public override bool Delete(TransactionManager transactionManager, AppointmentSystem.Entities.SmsLogKey key)
		{
			return Delete(transactionManager, key.Id);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_id">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Guid _id)
		{
			return Delete(null, _id);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Guid _id);		
		
		#endregion Delete Methods
		
		#region Get By Foreign Key Functions
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_SmsLog_Sms key.
		///		FK_SmsLog_Sms Description: 
		/// </summary>
		/// <param name="_smsId"></param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.SmsLog objects.</returns>
		public TList<SmsLog> GetBySmsId(System.Int64 _smsId)
		{
			int count = -1;
			return GetBySmsId(_smsId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_SmsLog_Sms key.
		///		FK_SmsLog_Sms Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_smsId"></param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.SmsLog objects.</returns>
		/// <remarks></remarks>
		public TList<SmsLog> GetBySmsId(TransactionManager transactionManager, System.Int64 _smsId)
		{
			int count = -1;
			return GetBySmsId(transactionManager, _smsId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_SmsLog_Sms key.
		///		FK_SmsLog_Sms Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_smsId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.SmsLog objects.</returns>
		public TList<SmsLog> GetBySmsId(TransactionManager transactionManager, System.Int64 _smsId, int start, int pageLength)
		{
			int count = -1;
			return GetBySmsId(transactionManager, _smsId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_SmsLog_Sms key.
		///		fkSmsLogSms Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_smsId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.SmsLog objects.</returns>
		public TList<SmsLog> GetBySmsId(System.Int64 _smsId, int start, int pageLength)
		{
			int count =  -1;
			return GetBySmsId(null, _smsId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_SmsLog_Sms key.
		///		fkSmsLogSms Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_smsId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.SmsLog objects.</returns>
		public TList<SmsLog> GetBySmsId(System.Int64 _smsId, int start, int pageLength,out int count)
		{
			return GetBySmsId(null, _smsId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_SmsLog_Sms key.
		///		FK_SmsLog_Sms Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_smsId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.SmsLog objects.</returns>
		public abstract TList<SmsLog> GetBySmsId(TransactionManager transactionManager, System.Int64 _smsId, int start, int pageLength, out int count);
		
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
		public override AppointmentSystem.Entities.SmsLog Get(TransactionManager transactionManager, AppointmentSystem.Entities.SmsLogKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_SmsLog index.
		/// </summary>
		/// <param name="_id"></param>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.SmsLog"/> class.</returns>
		public AppointmentSystem.Entities.SmsLog GetById(System.Guid _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_SmsLog index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.SmsLog"/> class.</returns>
		public AppointmentSystem.Entities.SmsLog GetById(System.Guid _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_SmsLog index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.SmsLog"/> class.</returns>
		public AppointmentSystem.Entities.SmsLog GetById(TransactionManager transactionManager, System.Guid _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_SmsLog index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.SmsLog"/> class.</returns>
		public AppointmentSystem.Entities.SmsLog GetById(TransactionManager transactionManager, System.Guid _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_SmsLog index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.SmsLog"/> class.</returns>
		public AppointmentSystem.Entities.SmsLog GetById(System.Guid _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_SmsLog index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.SmsLog"/> class.</returns>
		public abstract AppointmentSystem.Entities.SmsLog GetById(TransactionManager transactionManager, System.Guid _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;SmsLog&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;SmsLog&gt;"/></returns>
		public static TList<SmsLog> Fill(IDataReader reader, TList<SmsLog> rows, int start, int pageLength)
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
				
				AppointmentSystem.Entities.SmsLog c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("SmsLog")
					.Append("|").Append((System.Guid)reader[((int)SmsLogColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<SmsLog>(
					key.ToString(), // EntityTrackingKey
					"SmsLog",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new AppointmentSystem.Entities.SmsLog();
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
					c.Id = (System.Guid)reader[((int)SmsLogColumn.Id - 1)];
					c.OriginalId = c.Id;
					c.SmsId = (System.Int64)reader[((int)SmsLogColumn.SmsId - 1)];
					c.Message = (reader.IsDBNull(((int)SmsLogColumn.Message - 1)))?null:(System.String)reader[((int)SmsLogColumn.Message - 1)];
					c.Mobile = (System.String)reader[((int)SmsLogColumn.Mobile - 1)];
					c.Title = (reader.IsDBNull(((int)SmsLogColumn.Title - 1)))?null:(System.String)reader[((int)SmsLogColumn.Title - 1)];
					c.SendTime = (System.DateTime)reader[((int)SmsLogColumn.SendTime - 1)];
					c.RealSendTime = (reader.IsDBNull(((int)SmsLogColumn.RealSendTime - 1)))?null:(System.DateTime?)reader[((int)SmsLogColumn.RealSendTime - 1)];
					c.IsSent = (System.Boolean)reader[((int)SmsLogColumn.IsSent - 1)];
					c.IsDisabled = (System.Boolean)reader[((int)SmsLogColumn.IsDisabled - 1)];
					c.CreateUser = (reader.IsDBNull(((int)SmsLogColumn.CreateUser - 1)))?null:(System.String)reader[((int)SmsLogColumn.CreateUser - 1)];
					c.CreateDate = (System.DateTime)reader[((int)SmsLogColumn.CreateDate - 1)];
					c.UpdateUser = (reader.IsDBNull(((int)SmsLogColumn.UpdateUser - 1)))?null:(System.String)reader[((int)SmsLogColumn.UpdateUser - 1)];
					c.UpdateDate = (System.DateTime)reader[((int)SmsLogColumn.UpdateDate - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="AppointmentSystem.Entities.SmsLog"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.SmsLog"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, AppointmentSystem.Entities.SmsLog entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (System.Guid)reader[((int)SmsLogColumn.Id - 1)];
			entity.OriginalId = (System.Guid)reader["Id"];
			entity.SmsId = (System.Int64)reader[((int)SmsLogColumn.SmsId - 1)];
			entity.Message = (reader.IsDBNull(((int)SmsLogColumn.Message - 1)))?null:(System.String)reader[((int)SmsLogColumn.Message - 1)];
			entity.Mobile = (System.String)reader[((int)SmsLogColumn.Mobile - 1)];
			entity.Title = (reader.IsDBNull(((int)SmsLogColumn.Title - 1)))?null:(System.String)reader[((int)SmsLogColumn.Title - 1)];
			entity.SendTime = (System.DateTime)reader[((int)SmsLogColumn.SendTime - 1)];
			entity.RealSendTime = (reader.IsDBNull(((int)SmsLogColumn.RealSendTime - 1)))?null:(System.DateTime?)reader[((int)SmsLogColumn.RealSendTime - 1)];
			entity.IsSent = (System.Boolean)reader[((int)SmsLogColumn.IsSent - 1)];
			entity.IsDisabled = (System.Boolean)reader[((int)SmsLogColumn.IsDisabled - 1)];
			entity.CreateUser = (reader.IsDBNull(((int)SmsLogColumn.CreateUser - 1)))?null:(System.String)reader[((int)SmsLogColumn.CreateUser - 1)];
			entity.CreateDate = (System.DateTime)reader[((int)SmsLogColumn.CreateDate - 1)];
			entity.UpdateUser = (reader.IsDBNull(((int)SmsLogColumn.UpdateUser - 1)))?null:(System.String)reader[((int)SmsLogColumn.UpdateUser - 1)];
			entity.UpdateDate = (System.DateTime)reader[((int)SmsLogColumn.UpdateDate - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="AppointmentSystem.Entities.SmsLog"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.SmsLog"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, AppointmentSystem.Entities.SmsLog entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Id = (System.Guid)dataRow["Id"];
			entity.OriginalId = (System.Guid)dataRow["Id"];
			entity.SmsId = (System.Int64)dataRow["SmsId"];
			entity.Message = Convert.IsDBNull(dataRow["Message"]) ? null : (System.String)dataRow["Message"];
			entity.Mobile = (System.String)dataRow["Mobile"];
			entity.Title = Convert.IsDBNull(dataRow["Title"]) ? null : (System.String)dataRow["Title"];
			entity.SendTime = (System.DateTime)dataRow["SendTime"];
			entity.RealSendTime = Convert.IsDBNull(dataRow["RealSendTime"]) ? null : (System.DateTime?)dataRow["RealSendTime"];
			entity.IsSent = (System.Boolean)dataRow["IsSent"];
			entity.IsDisabled = (System.Boolean)dataRow["IsDisabled"];
			entity.CreateUser = Convert.IsDBNull(dataRow["CreateUser"]) ? null : (System.String)dataRow["CreateUser"];
			entity.CreateDate = (System.DateTime)dataRow["CreateDate"];
			entity.UpdateUser = Convert.IsDBNull(dataRow["UpdateUser"]) ? null : (System.String)dataRow["UpdateUser"];
			entity.UpdateDate = (System.DateTime)dataRow["UpdateDate"];
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
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.SmsLog"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">AppointmentSystem.Entities.SmsLog Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, AppointmentSystem.Entities.SmsLog entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region SmsIdSource	
			if (CanDeepLoad(entity, "Sms|SmsIdSource", deepLoadType, innerList) 
				&& entity.SmsIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = entity.SmsId;
				Sms tmpEntity = EntityManager.LocateEntity<Sms>(EntityLocator.ConstructKeyFromPkItems(typeof(Sms), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.SmsIdSource = tmpEntity;
				else
					entity.SmsIdSource = DataRepository.SmsProvider.GetById(transactionManager, entity.SmsId);		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'SmsIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.SmsIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.SmsProvider.DeepLoad(transactionManager, entity.SmsIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion SmsIdSource
			
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
		/// Deep Save the entire object graph of the AppointmentSystem.Entities.SmsLog object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">AppointmentSystem.Entities.SmsLog instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">AppointmentSystem.Entities.SmsLog Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, AppointmentSystem.Entities.SmsLog entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			
			#region SmsIdSource
			if (CanDeepSave(entity, "Sms|SmsIdSource", deepSaveType, innerList) 
				&& entity.SmsIdSource != null)
			{
				DataRepository.SmsProvider.Save(transactionManager, entity.SmsIdSource);
				entity.SmsId = entity.SmsIdSource.Id;
			}
			#endregion 
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
	
	#region SmsLogChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>AppointmentSystem.Entities.SmsLog</c>
	///</summary>
	public enum SmsLogChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Sms</c> at SmsIdSource
		///</summary>
		[ChildEntityType(typeof(Sms))]
		Sms,
	}
	
	#endregion SmsLogChildEntityTypes
	
	#region SmsLogFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;SmsLogColumn&gt;"/> class
	/// that is used exclusively with a <see cref="SmsLog"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class SmsLogFilterBuilder : SqlFilterBuilder<SmsLogColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SmsLogFilterBuilder class.
		/// </summary>
		public SmsLogFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the SmsLogFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public SmsLogFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the SmsLogFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public SmsLogFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion SmsLogFilterBuilder
	
	#region SmsLogParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;SmsLogColumn&gt;"/> class
	/// that is used exclusively with a <see cref="SmsLog"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class SmsLogParameterBuilder : ParameterizedSqlFilterBuilder<SmsLogColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SmsLogParameterBuilder class.
		/// </summary>
		public SmsLogParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the SmsLogParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public SmsLogParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the SmsLogParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public SmsLogParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion SmsLogParameterBuilder
	
	#region SmsLogSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;SmsLogColumn&gt;"/> class
	/// that is used exclusively with a <see cref="SmsLog"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class SmsLogSortBuilder : SqlSortBuilder<SmsLogColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SmsLogSqlSortBuilder class.
		/// </summary>
		public SmsLogSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion SmsLogSortBuilder
	
} // end namespace
