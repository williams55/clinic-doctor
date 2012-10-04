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
	/// This class is the base class for any <see cref="AppointmentHistoryProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class AppointmentHistoryProviderBaseCore : EntityProviderBase<AppointmentSystem.Entities.AppointmentHistory, AppointmentSystem.Entities.AppointmentHistoryKey>
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
		public override bool Delete(TransactionManager transactionManager, AppointmentSystem.Entities.AppointmentHistoryKey key)
		{
			return Delete(transactionManager, key.Guid);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_guid">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Guid _guid)
		{
			return Delete(null, _guid);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_guid">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Guid _guid);		
		
		#endregion Delete Methods
		
		#region Get By Foreign Key Functions
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_AppointmentHistory_Appointment key.
		///		FK_AppointmentHistory_Appointment Description: 
		/// </summary>
		/// <param name="_appointmentId"></param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.AppointmentHistory objects.</returns>
		public TList<AppointmentHistory> GetByAppointmentId(System.String _appointmentId)
		{
			int count = -1;
			return GetByAppointmentId(_appointmentId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_AppointmentHistory_Appointment key.
		///		FK_AppointmentHistory_Appointment Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_appointmentId"></param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.AppointmentHistory objects.</returns>
		/// <remarks></remarks>
		public TList<AppointmentHistory> GetByAppointmentId(TransactionManager transactionManager, System.String _appointmentId)
		{
			int count = -1;
			return GetByAppointmentId(transactionManager, _appointmentId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_AppointmentHistory_Appointment key.
		///		FK_AppointmentHistory_Appointment Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_appointmentId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.AppointmentHistory objects.</returns>
		public TList<AppointmentHistory> GetByAppointmentId(TransactionManager transactionManager, System.String _appointmentId, int start, int pageLength)
		{
			int count = -1;
			return GetByAppointmentId(transactionManager, _appointmentId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_AppointmentHistory_Appointment key.
		///		fkAppointmentHistoryAppointment Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_appointmentId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.AppointmentHistory objects.</returns>
		public TList<AppointmentHistory> GetByAppointmentId(System.String _appointmentId, int start, int pageLength)
		{
			int count =  -1;
			return GetByAppointmentId(null, _appointmentId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_AppointmentHistory_Appointment key.
		///		fkAppointmentHistoryAppointment Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_appointmentId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.AppointmentHistory objects.</returns>
		public TList<AppointmentHistory> GetByAppointmentId(System.String _appointmentId, int start, int pageLength,out int count)
		{
			return GetByAppointmentId(null, _appointmentId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_AppointmentHistory_Appointment key.
		///		FK_AppointmentHistory_Appointment Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_appointmentId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.AppointmentHistory objects.</returns>
		public abstract TList<AppointmentHistory> GetByAppointmentId(TransactionManager transactionManager, System.String _appointmentId, int start, int pageLength, out int count);
		
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
		public override AppointmentSystem.Entities.AppointmentHistory Get(TransactionManager transactionManager, AppointmentSystem.Entities.AppointmentHistoryKey key, int start, int pageLength)
		{
			return GetByGuid(transactionManager, key.Guid, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_AppointmentHistory index.
		/// </summary>
		/// <param name="_guid"></param>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.AppointmentHistory"/> class.</returns>
		public AppointmentSystem.Entities.AppointmentHistory GetByGuid(System.Guid _guid)
		{
			int count = -1;
			return GetByGuid(null,_guid, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_AppointmentHistory index.
		/// </summary>
		/// <param name="_guid"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.AppointmentHistory"/> class.</returns>
		public AppointmentSystem.Entities.AppointmentHistory GetByGuid(System.Guid _guid, int start, int pageLength)
		{
			int count = -1;
			return GetByGuid(null, _guid, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_AppointmentHistory index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_guid"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.AppointmentHistory"/> class.</returns>
		public AppointmentSystem.Entities.AppointmentHistory GetByGuid(TransactionManager transactionManager, System.Guid _guid)
		{
			int count = -1;
			return GetByGuid(transactionManager, _guid, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_AppointmentHistory index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_guid"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.AppointmentHistory"/> class.</returns>
		public AppointmentSystem.Entities.AppointmentHistory GetByGuid(TransactionManager transactionManager, System.Guid _guid, int start, int pageLength)
		{
			int count = -1;
			return GetByGuid(transactionManager, _guid, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_AppointmentHistory index.
		/// </summary>
		/// <param name="_guid"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.AppointmentHistory"/> class.</returns>
		public AppointmentSystem.Entities.AppointmentHistory GetByGuid(System.Guid _guid, int start, int pageLength, out int count)
		{
			return GetByGuid(null, _guid, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_AppointmentHistory index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_guid"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.AppointmentHistory"/> class.</returns>
		public abstract AppointmentSystem.Entities.AppointmentHistory GetByGuid(TransactionManager transactionManager, System.Guid _guid, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;AppointmentHistory&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;AppointmentHistory&gt;"/></returns>
		public static TList<AppointmentHistory> Fill(IDataReader reader, TList<AppointmentHistory> rows, int start, int pageLength)
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
				
				AppointmentSystem.Entities.AppointmentHistory c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("AppointmentHistory")
					.Append("|").Append((System.Guid)reader[((int)AppointmentHistoryColumn.Guid - 1)]).ToString();
					c = EntityManager.LocateOrCreate<AppointmentHistory>(
					key.ToString(), // EntityTrackingKey
					"AppointmentHistory",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new AppointmentSystem.Entities.AppointmentHistory();
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
					c.Guid = (System.Guid)reader[((int)AppointmentHistoryColumn.Guid - 1)];
					c.OriginalGuid = c.Guid;
					c.AppointmentId = (System.String)reader[((int)AppointmentHistoryColumn.AppointmentId - 1)];
					c.Note = (reader.IsDBNull(((int)AppointmentHistoryColumn.Note - 1)))?null:(System.String)reader[((int)AppointmentHistoryColumn.Note - 1)];
					c.CreateUser = (reader.IsDBNull(((int)AppointmentHistoryColumn.CreateUser - 1)))?null:(System.String)reader[((int)AppointmentHistoryColumn.CreateUser - 1)];
					c.CreateDate = (System.DateTime)reader[((int)AppointmentHistoryColumn.CreateDate - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="AppointmentSystem.Entities.AppointmentHistory"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.AppointmentHistory"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, AppointmentSystem.Entities.AppointmentHistory entity)
		{
			if (!reader.Read()) return;
			
			entity.Guid = (System.Guid)reader[((int)AppointmentHistoryColumn.Guid - 1)];
			entity.OriginalGuid = (System.Guid)reader["Guid"];
			entity.AppointmentId = (System.String)reader[((int)AppointmentHistoryColumn.AppointmentId - 1)];
			entity.Note = (reader.IsDBNull(((int)AppointmentHistoryColumn.Note - 1)))?null:(System.String)reader[((int)AppointmentHistoryColumn.Note - 1)];
			entity.CreateUser = (reader.IsDBNull(((int)AppointmentHistoryColumn.CreateUser - 1)))?null:(System.String)reader[((int)AppointmentHistoryColumn.CreateUser - 1)];
			entity.CreateDate = (System.DateTime)reader[((int)AppointmentHistoryColumn.CreateDate - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="AppointmentSystem.Entities.AppointmentHistory"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.AppointmentHistory"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, AppointmentSystem.Entities.AppointmentHistory entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Guid = (System.Guid)dataRow["Guid"];
			entity.OriginalGuid = (System.Guid)dataRow["Guid"];
			entity.AppointmentId = (System.String)dataRow["AppointmentId"];
			entity.Note = Convert.IsDBNull(dataRow["Note"]) ? null : (System.String)dataRow["Note"];
			entity.CreateUser = Convert.IsDBNull(dataRow["CreateUser"]) ? null : (System.String)dataRow["CreateUser"];
			entity.CreateDate = (System.DateTime)dataRow["CreateDate"];
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
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.AppointmentHistory"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">AppointmentSystem.Entities.AppointmentHistory Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, AppointmentSystem.Entities.AppointmentHistory entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region AppointmentIdSource	
			if (CanDeepLoad(entity, "Appointment|AppointmentIdSource", deepLoadType, innerList) 
				&& entity.AppointmentIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = entity.AppointmentId;
				Appointment tmpEntity = EntityManager.LocateEntity<Appointment>(EntityLocator.ConstructKeyFromPkItems(typeof(Appointment), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.AppointmentIdSource = tmpEntity;
				else
					entity.AppointmentIdSource = DataRepository.AppointmentProvider.GetById(transactionManager, entity.AppointmentId);		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'AppointmentIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.AppointmentIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.AppointmentProvider.DeepLoad(transactionManager, entity.AppointmentIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion AppointmentIdSource
			
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
		/// Deep Save the entire object graph of the AppointmentSystem.Entities.AppointmentHistory object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">AppointmentSystem.Entities.AppointmentHistory instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">AppointmentSystem.Entities.AppointmentHistory Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, AppointmentSystem.Entities.AppointmentHistory entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			
			#region AppointmentIdSource
			if (CanDeepSave(entity, "Appointment|AppointmentIdSource", deepSaveType, innerList) 
				&& entity.AppointmentIdSource != null)
			{
				DataRepository.AppointmentProvider.Save(transactionManager, entity.AppointmentIdSource);
				entity.AppointmentId = entity.AppointmentIdSource.Id;
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
	
	#region AppointmentHistoryChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>AppointmentSystem.Entities.AppointmentHistory</c>
	///</summary>
	public enum AppointmentHistoryChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Appointment</c> at AppointmentIdSource
		///</summary>
		[ChildEntityType(typeof(Appointment))]
		Appointment,
		}
	
	#endregion AppointmentHistoryChildEntityTypes
	
	#region AppointmentHistoryFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;AppointmentHistoryColumn&gt;"/> class
	/// that is used exclusively with a <see cref="AppointmentHistory"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class AppointmentHistoryFilterBuilder : SqlFilterBuilder<AppointmentHistoryColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the AppointmentHistoryFilterBuilder class.
		/// </summary>
		public AppointmentHistoryFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the AppointmentHistoryFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public AppointmentHistoryFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the AppointmentHistoryFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public AppointmentHistoryFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion AppointmentHistoryFilterBuilder
	
	#region AppointmentHistoryParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;AppointmentHistoryColumn&gt;"/> class
	/// that is used exclusively with a <see cref="AppointmentHistory"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class AppointmentHistoryParameterBuilder : ParameterizedSqlFilterBuilder<AppointmentHistoryColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the AppointmentHistoryParameterBuilder class.
		/// </summary>
		public AppointmentHistoryParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the AppointmentHistoryParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public AppointmentHistoryParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the AppointmentHistoryParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public AppointmentHistoryParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion AppointmentHistoryParameterBuilder
	
	#region AppointmentHistorySortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;AppointmentHistoryColumn&gt;"/> class
	/// that is used exclusively with a <see cref="AppointmentHistory"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class AppointmentHistorySortBuilder : SqlSortBuilder<AppointmentHistoryColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the AppointmentHistorySqlSortBuilder class.
		/// </summary>
		public AppointmentHistorySortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion AppointmentHistorySortBuilder
	
} // end namespace
