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
	/// This class is the base class for any <see cref="DoctorServiceProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class DoctorServiceProviderBaseCore : EntityProviderBase<AppointmentSystem.Entities.DoctorService, AppointmentSystem.Entities.DoctorServiceKey>
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
		public override bool Delete(TransactionManager transactionManager, AppointmentSystem.Entities.DoctorServiceKey key)
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
		/// 	Gets rows from the datasource based on the FK_DoctorService_Services key.
		///		FK_DoctorService_Services Description: 
		/// </summary>
		/// <param name="_serviceId"></param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.DoctorService objects.</returns>
		public TList<DoctorService> GetByServiceId(System.Int32 _serviceId)
		{
			int count = -1;
			return GetByServiceId(_serviceId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_DoctorService_Services key.
		///		FK_DoctorService_Services Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_serviceId"></param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.DoctorService objects.</returns>
		/// <remarks></remarks>
		public TList<DoctorService> GetByServiceId(TransactionManager transactionManager, System.Int32 _serviceId)
		{
			int count = -1;
			return GetByServiceId(transactionManager, _serviceId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_DoctorService_Services key.
		///		FK_DoctorService_Services Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_serviceId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.DoctorService objects.</returns>
		public TList<DoctorService> GetByServiceId(TransactionManager transactionManager, System.Int32 _serviceId, int start, int pageLength)
		{
			int count = -1;
			return GetByServiceId(transactionManager, _serviceId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_DoctorService_Services key.
		///		fkDoctorServiceServices Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_serviceId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.DoctorService objects.</returns>
		public TList<DoctorService> GetByServiceId(System.Int32 _serviceId, int start, int pageLength)
		{
			int count =  -1;
			return GetByServiceId(null, _serviceId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_DoctorService_Services key.
		///		fkDoctorServiceServices Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_serviceId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.DoctorService objects.</returns>
		public TList<DoctorService> GetByServiceId(System.Int32 _serviceId, int start, int pageLength,out int count)
		{
			return GetByServiceId(null, _serviceId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_DoctorService_Services key.
		///		FK_DoctorService_Services Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_serviceId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.DoctorService objects.</returns>
		public abstract TList<DoctorService> GetByServiceId(TransactionManager transactionManager, System.Int32 _serviceId, int start, int pageLength, out int count);
		
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_DoctorService_Users key.
		///		FK_DoctorService_Users Description: 
		/// </summary>
		/// <param name="_doctorId"></param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.DoctorService objects.</returns>
		public TList<DoctorService> GetByDoctorId(System.String _doctorId)
		{
			int count = -1;
			return GetByDoctorId(_doctorId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_DoctorService_Users key.
		///		FK_DoctorService_Users Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorId"></param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.DoctorService objects.</returns>
		/// <remarks></remarks>
		public TList<DoctorService> GetByDoctorId(TransactionManager transactionManager, System.String _doctorId)
		{
			int count = -1;
			return GetByDoctorId(transactionManager, _doctorId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_DoctorService_Users key.
		///		FK_DoctorService_Users Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.DoctorService objects.</returns>
		public TList<DoctorService> GetByDoctorId(TransactionManager transactionManager, System.String _doctorId, int start, int pageLength)
		{
			int count = -1;
			return GetByDoctorId(transactionManager, _doctorId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_DoctorService_Users key.
		///		fkDoctorServiceUsers Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_doctorId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.DoctorService objects.</returns>
		public TList<DoctorService> GetByDoctorId(System.String _doctorId, int start, int pageLength)
		{
			int count =  -1;
			return GetByDoctorId(null, _doctorId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_DoctorService_Users key.
		///		fkDoctorServiceUsers Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_doctorId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.DoctorService objects.</returns>
		public TList<DoctorService> GetByDoctorId(System.String _doctorId, int start, int pageLength,out int count)
		{
			return GetByDoctorId(null, _doctorId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_DoctorService_Users key.
		///		FK_DoctorService_Users Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.DoctorService objects.</returns>
		public abstract TList<DoctorService> GetByDoctorId(TransactionManager transactionManager, System.String _doctorId, int start, int pageLength, out int count);
		
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
		public override AppointmentSystem.Entities.DoctorService Get(TransactionManager transactionManager, AppointmentSystem.Entities.DoctorServiceKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_DoctorService index.
		/// </summary>
		/// <param name="_id"></param>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.DoctorService"/> class.</returns>
		public AppointmentSystem.Entities.DoctorService GetById(System.Int64 _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_DoctorService index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.DoctorService"/> class.</returns>
		public AppointmentSystem.Entities.DoctorService GetById(System.Int64 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_DoctorService index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.DoctorService"/> class.</returns>
		public AppointmentSystem.Entities.DoctorService GetById(TransactionManager transactionManager, System.Int64 _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_DoctorService index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.DoctorService"/> class.</returns>
		public AppointmentSystem.Entities.DoctorService GetById(TransactionManager transactionManager, System.Int64 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_DoctorService index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.DoctorService"/> class.</returns>
		public AppointmentSystem.Entities.DoctorService GetById(System.Int64 _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_DoctorService index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.DoctorService"/> class.</returns>
		public abstract AppointmentSystem.Entities.DoctorService GetById(TransactionManager transactionManager, System.Int64 _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;DoctorService&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;DoctorService&gt;"/></returns>
		public static TList<DoctorService> Fill(IDataReader reader, TList<DoctorService> rows, int start, int pageLength)
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
				
				AppointmentSystem.Entities.DoctorService c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("DoctorService")
					.Append("|").Append((System.Int64)reader[((int)DoctorServiceColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<DoctorService>(
					key.ToString(), // EntityTrackingKey
					"DoctorService",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new AppointmentSystem.Entities.DoctorService();
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
					c.Id = (System.Int64)reader[((int)DoctorServiceColumn.Id - 1)];
					c.DoctorId = (System.String)reader[((int)DoctorServiceColumn.DoctorId - 1)];
					c.ServiceId = (System.Int32)reader[((int)DoctorServiceColumn.ServiceId - 1)];
					c.IsDisabled = (System.Boolean)reader[((int)DoctorServiceColumn.IsDisabled - 1)];
					c.CreateUser = (reader.IsDBNull(((int)DoctorServiceColumn.CreateUser - 1)))?null:(System.String)reader[((int)DoctorServiceColumn.CreateUser - 1)];
					c.CreateDate = (System.DateTime)reader[((int)DoctorServiceColumn.CreateDate - 1)];
					c.UpdateUser = (reader.IsDBNull(((int)DoctorServiceColumn.UpdateUser - 1)))?null:(System.String)reader[((int)DoctorServiceColumn.UpdateUser - 1)];
					c.UpdateDate = (System.DateTime)reader[((int)DoctorServiceColumn.UpdateDate - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="AppointmentSystem.Entities.DoctorService"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.DoctorService"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, AppointmentSystem.Entities.DoctorService entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (System.Int64)reader[((int)DoctorServiceColumn.Id - 1)];
			entity.DoctorId = (System.String)reader[((int)DoctorServiceColumn.DoctorId - 1)];
			entity.ServiceId = (System.Int32)reader[((int)DoctorServiceColumn.ServiceId - 1)];
			entity.IsDisabled = (System.Boolean)reader[((int)DoctorServiceColumn.IsDisabled - 1)];
			entity.CreateUser = (reader.IsDBNull(((int)DoctorServiceColumn.CreateUser - 1)))?null:(System.String)reader[((int)DoctorServiceColumn.CreateUser - 1)];
			entity.CreateDate = (System.DateTime)reader[((int)DoctorServiceColumn.CreateDate - 1)];
			entity.UpdateUser = (reader.IsDBNull(((int)DoctorServiceColumn.UpdateUser - 1)))?null:(System.String)reader[((int)DoctorServiceColumn.UpdateUser - 1)];
			entity.UpdateDate = (System.DateTime)reader[((int)DoctorServiceColumn.UpdateDate - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="AppointmentSystem.Entities.DoctorService"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.DoctorService"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, AppointmentSystem.Entities.DoctorService entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Id = (System.Int64)dataRow["Id"];
			entity.DoctorId = (System.String)dataRow["DoctorId"];
			entity.ServiceId = (System.Int32)dataRow["ServiceId"];
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
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.DoctorService"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">AppointmentSystem.Entities.DoctorService Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, AppointmentSystem.Entities.DoctorService entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region ServiceIdSource	
			if (CanDeepLoad(entity, "Services|ServiceIdSource", deepLoadType, innerList) 
				&& entity.ServiceIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = entity.ServiceId;
				Services tmpEntity = EntityManager.LocateEntity<Services>(EntityLocator.ConstructKeyFromPkItems(typeof(Services), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.ServiceIdSource = tmpEntity;
				else
					entity.ServiceIdSource = DataRepository.ServicesProvider.GetById(transactionManager, entity.ServiceId);		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'ServiceIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.ServiceIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.ServicesProvider.DeepLoad(transactionManager, entity.ServiceIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion ServiceIdSource

			#region DoctorIdSource	
			if (CanDeepLoad(entity, "Users|DoctorIdSource", deepLoadType, innerList) 
				&& entity.DoctorIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = entity.DoctorId;
				Users tmpEntity = EntityManager.LocateEntity<Users>(EntityLocator.ConstructKeyFromPkItems(typeof(Users), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.DoctorIdSource = tmpEntity;
				else
					entity.DoctorIdSource = DataRepository.UsersProvider.GetById(transactionManager, entity.DoctorId);		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'DoctorIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.DoctorIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.UsersProvider.DeepLoad(transactionManager, entity.DoctorIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion DoctorIdSource
			
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
		/// Deep Save the entire object graph of the AppointmentSystem.Entities.DoctorService object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">AppointmentSystem.Entities.DoctorService instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">AppointmentSystem.Entities.DoctorService Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, AppointmentSystem.Entities.DoctorService entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			
			#region ServiceIdSource
			if (CanDeepSave(entity, "Services|ServiceIdSource", deepSaveType, innerList) 
				&& entity.ServiceIdSource != null)
			{
				DataRepository.ServicesProvider.Save(transactionManager, entity.ServiceIdSource);
				entity.ServiceId = entity.ServiceIdSource.Id;
			}
			#endregion 
			
			#region DoctorIdSource
			if (CanDeepSave(entity, "Users|DoctorIdSource", deepSaveType, innerList) 
				&& entity.DoctorIdSource != null)
			{
				DataRepository.UsersProvider.Save(transactionManager, entity.DoctorIdSource);
				entity.DoctorId = entity.DoctorIdSource.Id;
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
	
	#region DoctorServiceChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>AppointmentSystem.Entities.DoctorService</c>
	///</summary>
	public enum DoctorServiceChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Services</c> at ServiceIdSource
		///</summary>
		[ChildEntityType(typeof(Services))]
		Services,
			
		///<summary>
		/// Composite Property for <c>Users</c> at DoctorIdSource
		///</summary>
		[ChildEntityType(typeof(Users))]
		Users,
		}
	
	#endregion DoctorServiceChildEntityTypes
	
	#region DoctorServiceFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;DoctorServiceColumn&gt;"/> class
	/// that is used exclusively with a <see cref="DoctorService"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class DoctorServiceFilterBuilder : SqlFilterBuilder<DoctorServiceColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DoctorServiceFilterBuilder class.
		/// </summary>
		public DoctorServiceFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the DoctorServiceFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public DoctorServiceFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the DoctorServiceFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public DoctorServiceFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion DoctorServiceFilterBuilder
	
	#region DoctorServiceParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;DoctorServiceColumn&gt;"/> class
	/// that is used exclusively with a <see cref="DoctorService"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class DoctorServiceParameterBuilder : ParameterizedSqlFilterBuilder<DoctorServiceColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DoctorServiceParameterBuilder class.
		/// </summary>
		public DoctorServiceParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the DoctorServiceParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public DoctorServiceParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the DoctorServiceParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public DoctorServiceParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion DoctorServiceParameterBuilder
	
	#region DoctorServiceSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;DoctorServiceColumn&gt;"/> class
	/// that is used exclusively with a <see cref="DoctorService"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class DoctorServiceSortBuilder : SqlSortBuilder<DoctorServiceColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DoctorServiceSqlSortBuilder class.
		/// </summary>
		public DoctorServiceSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion DoctorServiceSortBuilder
	
} // end namespace
