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
	/// This class is the base class for any <see cref="SmsProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class SmsProviderBaseCore : EntityProviderBase<AppointmentSystem.Entities.Sms, AppointmentSystem.Entities.SmsKey>
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
		public override bool Delete(TransactionManager transactionManager, AppointmentSystem.Entities.SmsKey key)
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
		/// 	Gets rows from the datasource based on the FK_Sms_Appointment key.
		///		FK_Sms_Appointment Description: 
		/// </summary>
		/// <param name="_appointmentId">Id của appointment nếu đó là loại của appointment</param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Sms objects.</returns>
		public TList<Sms> GetByAppointmentId(System.String _appointmentId)
		{
			int count = -1;
			return GetByAppointmentId(_appointmentId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Sms_Appointment key.
		///		FK_Sms_Appointment Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_appointmentId">Id của appointment nếu đó là loại của appointment</param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Sms objects.</returns>
		/// <remarks></remarks>
		public TList<Sms> GetByAppointmentId(TransactionManager transactionManager, System.String _appointmentId)
		{
			int count = -1;
			return GetByAppointmentId(transactionManager, _appointmentId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_Sms_Appointment key.
		///		FK_Sms_Appointment Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_appointmentId">Id của appointment nếu đó là loại của appointment</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Sms objects.</returns>
		public TList<Sms> GetByAppointmentId(TransactionManager transactionManager, System.String _appointmentId, int start, int pageLength)
		{
			int count = -1;
			return GetByAppointmentId(transactionManager, _appointmentId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Sms_Appointment key.
		///		fkSmsAppointment Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_appointmentId">Id của appointment nếu đó là loại của appointment</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Sms objects.</returns>
		public TList<Sms> GetByAppointmentId(System.String _appointmentId, int start, int pageLength)
		{
			int count =  -1;
			return GetByAppointmentId(null, _appointmentId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Sms_Appointment key.
		///		fkSmsAppointment Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_appointmentId">Id của appointment nếu đó là loại của appointment</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Sms objects.</returns>
		public TList<Sms> GetByAppointmentId(System.String _appointmentId, int start, int pageLength,out int count)
		{
			return GetByAppointmentId(null, _appointmentId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Sms_Appointment key.
		///		FK_Sms_Appointment Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_appointmentId">Id của appointment nếu đó là loại của appointment</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Sms objects.</returns>
		public abstract TList<Sms> GetByAppointmentId(TransactionManager transactionManager, System.String _appointmentId, int start, int pageLength, out int count);
		
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
		public override AppointmentSystem.Entities.Sms Get(TransactionManager transactionManager, AppointmentSystem.Entities.SmsKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Sms index.
		/// </summary>
		/// <param name="_id"></param>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.Sms"/> class.</returns>
		public AppointmentSystem.Entities.Sms GetById(System.Int64 _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Sms index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.Sms"/> class.</returns>
		public AppointmentSystem.Entities.Sms GetById(System.Int64 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Sms index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.Sms"/> class.</returns>
		public AppointmentSystem.Entities.Sms GetById(TransactionManager transactionManager, System.Int64 _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Sms index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.Sms"/> class.</returns>
		public AppointmentSystem.Entities.Sms GetById(TransactionManager transactionManager, System.Int64 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Sms index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.Sms"/> class.</returns>
		public AppointmentSystem.Entities.Sms GetById(System.Int64 _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Sms index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.Sms"/> class.</returns>
		public abstract AppointmentSystem.Entities.Sms GetById(TransactionManager transactionManager, System.Int64 _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Sms&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Sms&gt;"/></returns>
		public static TList<Sms> Fill(IDataReader reader, TList<Sms> rows, int start, int pageLength)
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
				
				AppointmentSystem.Entities.Sms c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Sms")
					.Append("|").Append((System.Int64)reader[((int)SmsColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Sms>(
					key.ToString(), // EntityTrackingKey
					"Sms",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new AppointmentSystem.Entities.Sms();
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
					c.Id = (System.Int64)reader[((int)SmsColumn.Id - 1)];
					c.Message = (reader.IsDBNull(((int)SmsColumn.Message - 1)))?null:(System.String)reader[((int)SmsColumn.Message - 1)];
					c.SmsType = (System.Byte)reader[((int)SmsColumn.SmsType - 1)];
					c.SendTime = (System.DateTime)reader[((int)SmsColumn.SendTime - 1)];
					c.IsSendNow = (System.Boolean)reader[((int)SmsColumn.IsSendNow - 1)];
					c.IsSent = (System.Boolean)reader[((int)SmsColumn.IsSent - 1)];
					c.SendingTimes = (System.Int32)reader[((int)SmsColumn.SendingTimes - 1)];
					c.AppointmentId = (reader.IsDBNull(((int)SmsColumn.AppointmentId - 1)))?null:(System.String)reader[((int)SmsColumn.AppointmentId - 1)];
					c.Note = (reader.IsDBNull(((int)SmsColumn.Note - 1)))?null:(System.String)reader[((int)SmsColumn.Note - 1)];
					c.IsDisabled = (System.Boolean)reader[((int)SmsColumn.IsDisabled - 1)];
					c.CreateUser = (reader.IsDBNull(((int)SmsColumn.CreateUser - 1)))?null:(System.String)reader[((int)SmsColumn.CreateUser - 1)];
					c.CreateDate = (System.DateTime)reader[((int)SmsColumn.CreateDate - 1)];
					c.UpdateUser = (reader.IsDBNull(((int)SmsColumn.UpdateUser - 1)))?null:(System.String)reader[((int)SmsColumn.UpdateUser - 1)];
					c.UpdateDate = (System.DateTime)reader[((int)SmsColumn.UpdateDate - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="AppointmentSystem.Entities.Sms"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.Sms"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, AppointmentSystem.Entities.Sms entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (System.Int64)reader[((int)SmsColumn.Id - 1)];
			entity.Message = (reader.IsDBNull(((int)SmsColumn.Message - 1)))?null:(System.String)reader[((int)SmsColumn.Message - 1)];
			entity.SmsType = (System.Byte)reader[((int)SmsColumn.SmsType - 1)];
			entity.SendTime = (System.DateTime)reader[((int)SmsColumn.SendTime - 1)];
			entity.IsSendNow = (System.Boolean)reader[((int)SmsColumn.IsSendNow - 1)];
			entity.IsSent = (System.Boolean)reader[((int)SmsColumn.IsSent - 1)];
			entity.SendingTimes = (System.Int32)reader[((int)SmsColumn.SendingTimes - 1)];
			entity.AppointmentId = (reader.IsDBNull(((int)SmsColumn.AppointmentId - 1)))?null:(System.String)reader[((int)SmsColumn.AppointmentId - 1)];
			entity.Note = (reader.IsDBNull(((int)SmsColumn.Note - 1)))?null:(System.String)reader[((int)SmsColumn.Note - 1)];
			entity.IsDisabled = (System.Boolean)reader[((int)SmsColumn.IsDisabled - 1)];
			entity.CreateUser = (reader.IsDBNull(((int)SmsColumn.CreateUser - 1)))?null:(System.String)reader[((int)SmsColumn.CreateUser - 1)];
			entity.CreateDate = (System.DateTime)reader[((int)SmsColumn.CreateDate - 1)];
			entity.UpdateUser = (reader.IsDBNull(((int)SmsColumn.UpdateUser - 1)))?null:(System.String)reader[((int)SmsColumn.UpdateUser - 1)];
			entity.UpdateDate = (System.DateTime)reader[((int)SmsColumn.UpdateDate - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="AppointmentSystem.Entities.Sms"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.Sms"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, AppointmentSystem.Entities.Sms entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Id = (System.Int64)dataRow["Id"];
			entity.Message = Convert.IsDBNull(dataRow["Message"]) ? null : (System.String)dataRow["Message"];
			entity.SmsType = (System.Byte)dataRow["SmsType"];
			entity.SendTime = (System.DateTime)dataRow["SendTime"];
			entity.IsSendNow = (System.Boolean)dataRow["IsSendNow"];
			entity.IsSent = (System.Boolean)dataRow["IsSent"];
			entity.SendingTimes = (System.Int32)dataRow["SendingTimes"];
			entity.AppointmentId = Convert.IsDBNull(dataRow["AppointmentId"]) ? null : (System.String)dataRow["AppointmentId"];
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
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.Sms"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">AppointmentSystem.Entities.Sms Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, AppointmentSystem.Entities.Sms entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region AppointmentIdSource	
			if (CanDeepLoad(entity, "Appointment|AppointmentIdSource", deepLoadType, innerList) 
				&& entity.AppointmentIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.AppointmentId ?? string.Empty);
				Appointment tmpEntity = EntityManager.LocateEntity<Appointment>(EntityLocator.ConstructKeyFromPkItems(typeof(Appointment), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.AppointmentIdSource = tmpEntity;
				else
					entity.AppointmentIdSource = DataRepository.AppointmentProvider.GetById(transactionManager, (entity.AppointmentId ?? string.Empty));		
				
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
			// Deep load child collections  - Call GetById methods when available
			
			#region SmsLogCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<SmsLog>|SmsLogCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'SmsLogCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.SmsLogCollection = DataRepository.SmsLogProvider.GetBySmsId(transactionManager, entity.Id);

				if (deep && entity.SmsLogCollection.Count > 0)
				{
					deepHandles.Add("SmsLogCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<SmsLog>) DataRepository.SmsLogProvider.DeepLoad,
						new object[] { transactionManager, entity.SmsLogCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			#region SmsReceiverCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<SmsReceiver>|SmsReceiverCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'SmsReceiverCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.SmsReceiverCollection = DataRepository.SmsReceiverProvider.GetBySmsId(transactionManager, entity.Id);

				if (deep && entity.SmsReceiverCollection.Count > 0)
				{
					deepHandles.Add("SmsReceiverCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<SmsReceiver>) DataRepository.SmsReceiverProvider.DeepLoad,
						new object[] { transactionManager, entity.SmsReceiverCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
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
		/// Deep Save the entire object graph of the AppointmentSystem.Entities.Sms object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">AppointmentSystem.Entities.Sms instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">AppointmentSystem.Entities.Sms Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, AppointmentSystem.Entities.Sms entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
			#region List<SmsLog>
				if (CanDeepSave(entity.SmsLogCollection, "List<SmsLog>|SmsLogCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(SmsLog child in entity.SmsLogCollection)
					{
						if(child.SmsIdSource != null)
						{
							child.SmsId = child.SmsIdSource.Id;
						}
						else
						{
							child.SmsId = entity.Id;
						}

					}

					if (entity.SmsLogCollection.Count > 0 || entity.SmsLogCollection.DeletedItems.Count > 0)
					{
						//DataRepository.SmsLogProvider.Save(transactionManager, entity.SmsLogCollection);
						
						deepHandles.Add("SmsLogCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< SmsLog >) DataRepository.SmsLogProvider.DeepSave,
							new object[] { transactionManager, entity.SmsLogCollection, deepSaveType, childTypes, innerList }
						));
					}
				} 
			#endregion 
				
	
			#region List<SmsReceiver>
				if (CanDeepSave(entity.SmsReceiverCollection, "List<SmsReceiver>|SmsReceiverCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(SmsReceiver child in entity.SmsReceiverCollection)
					{
						if(child.SmsIdSource != null)
						{
							child.SmsId = child.SmsIdSource.Id;
						}
						else
						{
							child.SmsId = entity.Id;
						}

					}

					if (entity.SmsReceiverCollection.Count > 0 || entity.SmsReceiverCollection.DeletedItems.Count > 0)
					{
						//DataRepository.SmsReceiverProvider.Save(transactionManager, entity.SmsReceiverCollection);
						
						deepHandles.Add("SmsReceiverCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< SmsReceiver >) DataRepository.SmsReceiverProvider.DeepSave,
							new object[] { transactionManager, entity.SmsReceiverCollection, deepSaveType, childTypes, innerList }
						));
					}
				} 
			#endregion 
				
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
	
	#region SmsChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>AppointmentSystem.Entities.Sms</c>
	///</summary>
	public enum SmsChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Appointment</c> at AppointmentIdSource
		///</summary>
		[ChildEntityType(typeof(Appointment))]
		Appointment,
		///<summary>
		/// Collection of <c>Sms</c> as OneToMany for SmsLogCollection
		///</summary>
		[ChildEntityType(typeof(TList<SmsLog>))]
		SmsLogCollection,
		///<summary>
		/// Collection of <c>Sms</c> as OneToMany for SmsReceiverCollection
		///</summary>
		[ChildEntityType(typeof(TList<SmsReceiver>))]
		SmsReceiverCollection,
	}
	
	#endregion SmsChildEntityTypes
	
	#region SmsFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;SmsColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Sms"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class SmsFilterBuilder : SqlFilterBuilder<SmsColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SmsFilterBuilder class.
		/// </summary>
		public SmsFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the SmsFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public SmsFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the SmsFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public SmsFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion SmsFilterBuilder
	
	#region SmsParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;SmsColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Sms"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class SmsParameterBuilder : ParameterizedSqlFilterBuilder<SmsColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SmsParameterBuilder class.
		/// </summary>
		public SmsParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the SmsParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public SmsParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the SmsParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public SmsParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion SmsParameterBuilder
	
	#region SmsSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;SmsColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Sms"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class SmsSortBuilder : SqlSortBuilder<SmsColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SmsSqlSortBuilder class.
		/// </summary>
		public SmsSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion SmsSortBuilder
	
} // end namespace
