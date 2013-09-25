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
	/// This class is the base class for any <see cref="SmsReceiverProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class SmsReceiverProviderBaseCore : EntityProviderBase<AppointmentSystem.Entities.SmsReceiver, AppointmentSystem.Entities.SmsReceiverKey>
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
		public override bool Delete(TransactionManager transactionManager, AppointmentSystem.Entities.SmsReceiverKey key)
		{
			return Delete(transactionManager, key.Id);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_id">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Int64 _id)
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
		public abstract bool Delete(TransactionManager transactionManager, System.Int64 _id);		
		
		#endregion Delete Methods
		
		#region Get By Foreign Key Functions
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_SmsReceiver_Sms key.
		///		FK_SmsReceiver_Sms Description: 
		/// </summary>
		/// <param name="_smsId"></param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.SmsReceiver objects.</returns>
		public TList<SmsReceiver> GetBySmsId(System.Int64 _smsId)
		{
			int count = -1;
			return GetBySmsId(_smsId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_SmsReceiver_Sms key.
		///		FK_SmsReceiver_Sms Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_smsId"></param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.SmsReceiver objects.</returns>
		/// <remarks></remarks>
		public TList<SmsReceiver> GetBySmsId(TransactionManager transactionManager, System.Int64 _smsId)
		{
			int count = -1;
			return GetBySmsId(transactionManager, _smsId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_SmsReceiver_Sms key.
		///		FK_SmsReceiver_Sms Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_smsId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.SmsReceiver objects.</returns>
		public TList<SmsReceiver> GetBySmsId(TransactionManager transactionManager, System.Int64 _smsId, int start, int pageLength)
		{
			int count = -1;
			return GetBySmsId(transactionManager, _smsId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_SmsReceiver_Sms key.
		///		fkSmsReceiverSms Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_smsId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.SmsReceiver objects.</returns>
		public TList<SmsReceiver> GetBySmsId(System.Int64 _smsId, int start, int pageLength)
		{
			int count =  -1;
			return GetBySmsId(null, _smsId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_SmsReceiver_Sms key.
		///		fkSmsReceiverSms Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_smsId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.SmsReceiver objects.</returns>
		public TList<SmsReceiver> GetBySmsId(System.Int64 _smsId, int start, int pageLength,out int count)
		{
			return GetBySmsId(null, _smsId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_SmsReceiver_Sms key.
		///		FK_SmsReceiver_Sms Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_smsId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.SmsReceiver objects.</returns>
		public abstract TList<SmsReceiver> GetBySmsId(TransactionManager transactionManager, System.Int64 _smsId, int start, int pageLength, out int count);
		
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
		public override AppointmentSystem.Entities.SmsReceiver Get(TransactionManager transactionManager, AppointmentSystem.Entities.SmsReceiverKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_SmsReceiver index.
		/// </summary>
		/// <param name="_id"></param>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.SmsReceiver"/> class.</returns>
		public AppointmentSystem.Entities.SmsReceiver GetById(System.Int64 _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_SmsReceiver index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.SmsReceiver"/> class.</returns>
		public AppointmentSystem.Entities.SmsReceiver GetById(System.Int64 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_SmsReceiver index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.SmsReceiver"/> class.</returns>
		public AppointmentSystem.Entities.SmsReceiver GetById(TransactionManager transactionManager, System.Int64 _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_SmsReceiver index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.SmsReceiver"/> class.</returns>
		public AppointmentSystem.Entities.SmsReceiver GetById(TransactionManager transactionManager, System.Int64 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_SmsReceiver index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.SmsReceiver"/> class.</returns>
		public AppointmentSystem.Entities.SmsReceiver GetById(System.Int64 _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_SmsReceiver index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.SmsReceiver"/> class.</returns>
		public abstract AppointmentSystem.Entities.SmsReceiver GetById(TransactionManager transactionManager, System.Int64 _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;SmsReceiver&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;SmsReceiver&gt;"/></returns>
		public static TList<SmsReceiver> Fill(IDataReader reader, TList<SmsReceiver> rows, int start, int pageLength)
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
				
				AppointmentSystem.Entities.SmsReceiver c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("SmsReceiver")
					.Append("|").Append((System.Int64)reader[((int)SmsReceiverColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<SmsReceiver>(
					key.ToString(), // EntityTrackingKey
					"SmsReceiver",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new AppointmentSystem.Entities.SmsReceiver();
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
					c.Id = (System.Int64)reader[((int)SmsReceiverColumn.Id - 1)];
					c.Mobile = (System.String)reader[((int)SmsReceiverColumn.Mobile - 1)];
					c.FirstName = (reader.IsDBNull(((int)SmsReceiverColumn.FirstName - 1)))?null:(System.String)reader[((int)SmsReceiverColumn.FirstName - 1)];
					c.LastName = (reader.IsDBNull(((int)SmsReceiverColumn.LastName - 1)))?null:(System.String)reader[((int)SmsReceiverColumn.LastName - 1)];
					c.UserType = (System.Byte)reader[((int)SmsReceiverColumn.UserType - 1)];
					c.SmsId = (System.Int64)reader[((int)SmsReceiverColumn.SmsId - 1)];
					c.IsSent = (System.Boolean)reader[((int)SmsReceiverColumn.IsSent - 1)];
					c.SendingTimes = (System.Int32)reader[((int)SmsReceiverColumn.SendingTimes - 1)];
					c.Note = (reader.IsDBNull(((int)SmsReceiverColumn.Note - 1)))?null:(System.String)reader[((int)SmsReceiverColumn.Note - 1)];
					c.IsDisabled = (System.Boolean)reader[((int)SmsReceiverColumn.IsDisabled - 1)];
					c.CreateUser = (reader.IsDBNull(((int)SmsReceiverColumn.CreateUser - 1)))?null:(System.String)reader[((int)SmsReceiverColumn.CreateUser - 1)];
					c.CreateDate = (System.DateTime)reader[((int)SmsReceiverColumn.CreateDate - 1)];
					c.UpdateUser = (reader.IsDBNull(((int)SmsReceiverColumn.UpdateUser - 1)))?null:(System.String)reader[((int)SmsReceiverColumn.UpdateUser - 1)];
					c.UpdateDate = (System.DateTime)reader[((int)SmsReceiverColumn.UpdateDate - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="AppointmentSystem.Entities.SmsReceiver"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.SmsReceiver"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, AppointmentSystem.Entities.SmsReceiver entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (System.Int64)reader[((int)SmsReceiverColumn.Id - 1)];
			entity.Mobile = (System.String)reader[((int)SmsReceiverColumn.Mobile - 1)];
			entity.FirstName = (reader.IsDBNull(((int)SmsReceiverColumn.FirstName - 1)))?null:(System.String)reader[((int)SmsReceiverColumn.FirstName - 1)];
			entity.LastName = (reader.IsDBNull(((int)SmsReceiverColumn.LastName - 1)))?null:(System.String)reader[((int)SmsReceiverColumn.LastName - 1)];
			entity.UserType = (System.Byte)reader[((int)SmsReceiverColumn.UserType - 1)];
			entity.SmsId = (System.Int64)reader[((int)SmsReceiverColumn.SmsId - 1)];
			entity.IsSent = (System.Boolean)reader[((int)SmsReceiverColumn.IsSent - 1)];
			entity.SendingTimes = (System.Int32)reader[((int)SmsReceiverColumn.SendingTimes - 1)];
			entity.Note = (reader.IsDBNull(((int)SmsReceiverColumn.Note - 1)))?null:(System.String)reader[((int)SmsReceiverColumn.Note - 1)];
			entity.IsDisabled = (System.Boolean)reader[((int)SmsReceiverColumn.IsDisabled - 1)];
			entity.CreateUser = (reader.IsDBNull(((int)SmsReceiverColumn.CreateUser - 1)))?null:(System.String)reader[((int)SmsReceiverColumn.CreateUser - 1)];
			entity.CreateDate = (System.DateTime)reader[((int)SmsReceiverColumn.CreateDate - 1)];
			entity.UpdateUser = (reader.IsDBNull(((int)SmsReceiverColumn.UpdateUser - 1)))?null:(System.String)reader[((int)SmsReceiverColumn.UpdateUser - 1)];
			entity.UpdateDate = (System.DateTime)reader[((int)SmsReceiverColumn.UpdateDate - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="AppointmentSystem.Entities.SmsReceiver"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.SmsReceiver"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, AppointmentSystem.Entities.SmsReceiver entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Id = (System.Int64)dataRow["Id"];
			entity.Mobile = (System.String)dataRow["Mobile"];
			entity.FirstName = Convert.IsDBNull(dataRow["FirstName"]) ? null : (System.String)dataRow["FirstName"];
			entity.LastName = Convert.IsDBNull(dataRow["LastName"]) ? null : (System.String)dataRow["LastName"];
			entity.UserType = (System.Byte)dataRow["UserType"];
			entity.SmsId = (System.Int64)dataRow["SmsId"];
			entity.IsSent = (System.Boolean)dataRow["IsSent"];
			entity.SendingTimes = (System.Int32)dataRow["SendingTimes"];
			entity.Note = Convert.IsDBNull(dataRow["Note"]) ? null : (System.String)dataRow["Note"];
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
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.SmsReceiver"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">AppointmentSystem.Entities.SmsReceiver Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, AppointmentSystem.Entities.SmsReceiver entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
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
		/// Deep Save the entire object graph of the AppointmentSystem.Entities.SmsReceiver object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">AppointmentSystem.Entities.SmsReceiver instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">AppointmentSystem.Entities.SmsReceiver Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, AppointmentSystem.Entities.SmsReceiver entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
	#region SmsReceiverChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>AppointmentSystem.Entities.SmsReceiver</c>
	///</summary>
	public enum SmsReceiverChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Sms</c> at SmsIdSource
		///</summary>
		[ChildEntityType(typeof(Sms))]
		Sms,
	}
	
	#endregion SmsReceiverChildEntityTypes
	
	#region SmsReceiverFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;SmsReceiverColumn&gt;"/> class
	/// that is used exclusively with a <see cref="SmsReceiver"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class SmsReceiverFilterBuilder : SqlFilterBuilder<SmsReceiverColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SmsReceiverFilterBuilder class.
		/// </summary>
		public SmsReceiverFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the SmsReceiverFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public SmsReceiverFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the SmsReceiverFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public SmsReceiverFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion SmsReceiverFilterBuilder
	
	#region SmsReceiverParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;SmsReceiverColumn&gt;"/> class
	/// that is used exclusively with a <see cref="SmsReceiver"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class SmsReceiverParameterBuilder : ParameterizedSqlFilterBuilder<SmsReceiverColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SmsReceiverParameterBuilder class.
		/// </summary>
		public SmsReceiverParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the SmsReceiverParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public SmsReceiverParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the SmsReceiverParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public SmsReceiverParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion SmsReceiverParameterBuilder
	
	#region SmsReceiverSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;SmsReceiverColumn&gt;"/> class
	/// that is used exclusively with a <see cref="SmsReceiver"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class SmsReceiverSortBuilder : SqlSortBuilder<SmsReceiverColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SmsReceiverSqlSortBuilder class.
		/// </summary>
		public SmsReceiverSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion SmsReceiverSortBuilder
	
} // end namespace
