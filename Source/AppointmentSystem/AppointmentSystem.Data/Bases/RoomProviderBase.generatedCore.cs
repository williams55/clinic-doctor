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
	/// This class is the base class for any <see cref="RoomProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class RoomProviderBaseCore : EntityProviderBase<AppointmentSystem.Entities.Room, AppointmentSystem.Entities.RoomKey>
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
		public override bool Delete(TransactionManager transactionManager, AppointmentSystem.Entities.RoomKey key)
		{
			return Delete(transactionManager, key.Id);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_id">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.String _id)
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
		public abstract bool Delete(TransactionManager transactionManager, System.String _id);		
		
		#endregion Delete Methods
		
		#region Get By Foreign Key Functions
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Room_Procedure key.
		///		FK_Room_Procedure Description: 
		/// </summary>
		/// <param name="_servicesId">A room can have many procedures. They are seperated by semi-comma [;]
		/// 		/// For example: XRay;MRI</param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Room objects.</returns>
		public TList<Room> GetByServicesId(System.String _servicesId)
		{
			int count = -1;
			return GetByServicesId(_servicesId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Room_Procedure key.
		///		FK_Room_Procedure Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_servicesId">A room can have many procedures. They are seperated by semi-comma [;]
		/// 		/// For example: XRay;MRI</param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Room objects.</returns>
		/// <remarks></remarks>
		public TList<Room> GetByServicesId(TransactionManager transactionManager, System.String _servicesId)
		{
			int count = -1;
			return GetByServicesId(transactionManager, _servicesId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_Room_Procedure key.
		///		FK_Room_Procedure Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_servicesId">A room can have many procedures. They are seperated by semi-comma [;]
		/// 		/// For example: XRay;MRI</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Room objects.</returns>
		public TList<Room> GetByServicesId(TransactionManager transactionManager, System.String _servicesId, int start, int pageLength)
		{
			int count = -1;
			return GetByServicesId(transactionManager, _servicesId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Room_Procedure key.
		///		fkRoomProcedure Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_servicesId">A room can have many procedures. They are seperated by semi-comma [;]
		/// 		/// For example: XRay;MRI</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Room objects.</returns>
		public TList<Room> GetByServicesId(System.String _servicesId, int start, int pageLength)
		{
			int count =  -1;
			return GetByServicesId(null, _servicesId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Room_Procedure key.
		///		fkRoomProcedure Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_servicesId">A room can have many procedures. They are seperated by semi-comma [;]
		/// 		/// For example: XRay;MRI</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Room objects.</returns>
		public TList<Room> GetByServicesId(System.String _servicesId, int start, int pageLength,out int count)
		{
			return GetByServicesId(null, _servicesId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Room_Procedure key.
		///		FK_Room_Procedure Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_servicesId">A room can have many procedures. They are seperated by semi-comma [;]
		/// 		/// For example: XRay;MRI</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Room objects.</returns>
		public abstract TList<Room> GetByServicesId(TransactionManager transactionManager, System.String _servicesId, int start, int pageLength, out int count);
		
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
		public override AppointmentSystem.Entities.Room Get(TransactionManager transactionManager, AppointmentSystem.Entities.RoomKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Room index.
		/// </summary>
		/// <param name="_id"></param>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.Room"/> class.</returns>
		public AppointmentSystem.Entities.Room GetById(System.String _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Room index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.Room"/> class.</returns>
		public AppointmentSystem.Entities.Room GetById(System.String _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Room index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.Room"/> class.</returns>
		public AppointmentSystem.Entities.Room GetById(TransactionManager transactionManager, System.String _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Room index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.Room"/> class.</returns>
		public AppointmentSystem.Entities.Room GetById(TransactionManager transactionManager, System.String _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Room index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.Room"/> class.</returns>
		public AppointmentSystem.Entities.Room GetById(System.String _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Room index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.Room"/> class.</returns>
		public abstract AppointmentSystem.Entities.Room GetById(TransactionManager transactionManager, System.String _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Room&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Room&gt;"/></returns>
		public static TList<Room> Fill(IDataReader reader, TList<Room> rows, int start, int pageLength)
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
				
				AppointmentSystem.Entities.Room c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Room")
					.Append("|").Append((System.String)reader[((int)RoomColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Room>(
					key.ToString(), // EntityTrackingKey
					"Room",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new AppointmentSystem.Entities.Room();
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
					c.Id = (System.String)reader[((int)RoomColumn.Id - 1)];
					c.OriginalId = c.Id;
					c.Title = (reader.IsDBNull(((int)RoomColumn.Title - 1)))?null:(System.String)reader[((int)RoomColumn.Title - 1)];
					c.Note = (reader.IsDBNull(((int)RoomColumn.Note - 1)))?null:(System.String)reader[((int)RoomColumn.Note - 1)];
					c.ServicesId = (reader.IsDBNull(((int)RoomColumn.ServicesId - 1)))?null:(System.String)reader[((int)RoomColumn.ServicesId - 1)];
					c.IsDisabled = (System.Boolean)reader[((int)RoomColumn.IsDisabled - 1)];
					c.CreateUser = (reader.IsDBNull(((int)RoomColumn.CreateUser - 1)))?null:(System.String)reader[((int)RoomColumn.CreateUser - 1)];
					c.CreateDate = (System.DateTime)reader[((int)RoomColumn.CreateDate - 1)];
					c.UpdateUser = (reader.IsDBNull(((int)RoomColumn.UpdateUser - 1)))?null:(System.String)reader[((int)RoomColumn.UpdateUser - 1)];
					c.UpdateDate = (System.DateTime)reader[((int)RoomColumn.UpdateDate - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="AppointmentSystem.Entities.Room"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.Room"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, AppointmentSystem.Entities.Room entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (System.String)reader[((int)RoomColumn.Id - 1)];
			entity.OriginalId = (System.String)reader["Id"];
			entity.Title = (reader.IsDBNull(((int)RoomColumn.Title - 1)))?null:(System.String)reader[((int)RoomColumn.Title - 1)];
			entity.Note = (reader.IsDBNull(((int)RoomColumn.Note - 1)))?null:(System.String)reader[((int)RoomColumn.Note - 1)];
			entity.ServicesId = (reader.IsDBNull(((int)RoomColumn.ServicesId - 1)))?null:(System.String)reader[((int)RoomColumn.ServicesId - 1)];
			entity.IsDisabled = (System.Boolean)reader[((int)RoomColumn.IsDisabled - 1)];
			entity.CreateUser = (reader.IsDBNull(((int)RoomColumn.CreateUser - 1)))?null:(System.String)reader[((int)RoomColumn.CreateUser - 1)];
			entity.CreateDate = (System.DateTime)reader[((int)RoomColumn.CreateDate - 1)];
			entity.UpdateUser = (reader.IsDBNull(((int)RoomColumn.UpdateUser - 1)))?null:(System.String)reader[((int)RoomColumn.UpdateUser - 1)];
			entity.UpdateDate = (System.DateTime)reader[((int)RoomColumn.UpdateDate - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="AppointmentSystem.Entities.Room"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.Room"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, AppointmentSystem.Entities.Room entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Id = (System.String)dataRow["Id"];
			entity.OriginalId = (System.String)dataRow["Id"];
			entity.Title = Convert.IsDBNull(dataRow["Title"]) ? null : (System.String)dataRow["Title"];
			entity.Note = Convert.IsDBNull(dataRow["Note"]) ? null : (System.String)dataRow["Note"];
			entity.ServicesId = Convert.IsDBNull(dataRow["ServicesId"]) ? null : (System.String)dataRow["ServicesId"];
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
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.Room"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">AppointmentSystem.Entities.Room Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, AppointmentSystem.Entities.Room entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetById methods when available
			
			#region DoctorRoomCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<DoctorRoom>|DoctorRoomCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'DoctorRoomCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.DoctorRoomCollection = DataRepository.DoctorRoomProvider.GetByRoomId(transactionManager, entity.Id);

				if (deep && entity.DoctorRoomCollection.Count > 0)
				{
					deepHandles.Add("DoctorRoomCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<DoctorRoom>) DataRepository.DoctorRoomProvider.DeepLoad,
						new object[] { transactionManager, entity.DoctorRoomCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			#region AppointmentCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<Appointment>|AppointmentCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'AppointmentCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.AppointmentCollection = DataRepository.AppointmentProvider.GetByRoomId(transactionManager, entity.Id);

				if (deep && entity.AppointmentCollection.Count > 0)
				{
					deepHandles.Add("AppointmentCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<Appointment>) DataRepository.AppointmentProvider.DeepLoad,
						new object[] { transactionManager, entity.AppointmentCollection, deep, deepLoadType, childTypes, innerList }
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
		/// Deep Save the entire object graph of the AppointmentSystem.Entities.Room object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">AppointmentSystem.Entities.Room instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">AppointmentSystem.Entities.Room Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, AppointmentSystem.Entities.Room entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
			#region List<DoctorRoom>
				if (CanDeepSave(entity.DoctorRoomCollection, "List<DoctorRoom>|DoctorRoomCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(DoctorRoom child in entity.DoctorRoomCollection)
					{
						if(child.RoomIdSource != null)
						{
							child.RoomId = child.RoomIdSource.Id;
						}
						else
						{
							child.RoomId = entity.Id;
						}

					}

					if (entity.DoctorRoomCollection.Count > 0 || entity.DoctorRoomCollection.DeletedItems.Count > 0)
					{
						//DataRepository.DoctorRoomProvider.Save(transactionManager, entity.DoctorRoomCollection);
						
						deepHandles.Add("DoctorRoomCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< DoctorRoom >) DataRepository.DoctorRoomProvider.DeepSave,
							new object[] { transactionManager, entity.DoctorRoomCollection, deepSaveType, childTypes, innerList }
						));
					}
				} 
			#endregion 
				
	
			#region List<Appointment>
				if (CanDeepSave(entity.AppointmentCollection, "List<Appointment>|AppointmentCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(Appointment child in entity.AppointmentCollection)
					{
						if(child.RoomIdSource != null)
						{
							child.RoomId = child.RoomIdSource.Id;
						}
						else
						{
							child.RoomId = entity.Id;
						}

					}

					if (entity.AppointmentCollection.Count > 0 || entity.AppointmentCollection.DeletedItems.Count > 0)
					{
						//DataRepository.AppointmentProvider.Save(transactionManager, entity.AppointmentCollection);
						
						deepHandles.Add("AppointmentCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< Appointment >) DataRepository.AppointmentProvider.DeepSave,
							new object[] { transactionManager, entity.AppointmentCollection, deepSaveType, childTypes, innerList }
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
	
	#region RoomChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>AppointmentSystem.Entities.Room</c>
	///</summary>
	public enum RoomChildEntityTypes
	{

		///<summary>
		/// Collection of <c>Room</c> as OneToMany for DoctorRoomCollection
		///</summary>
		[ChildEntityType(typeof(TList<DoctorRoom>))]
		DoctorRoomCollection,

		///<summary>
		/// Collection of <c>Room</c> as OneToMany for AppointmentCollection
		///</summary>
		[ChildEntityType(typeof(TList<Appointment>))]
		AppointmentCollection,
	}
	
	#endregion RoomChildEntityTypes
	
	#region RoomFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;RoomColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Room"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class RoomFilterBuilder : SqlFilterBuilder<RoomColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the RoomFilterBuilder class.
		/// </summary>
		public RoomFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the RoomFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public RoomFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the RoomFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public RoomFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion RoomFilterBuilder
	
	#region RoomParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;RoomColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Room"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class RoomParameterBuilder : ParameterizedSqlFilterBuilder<RoomColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the RoomParameterBuilder class.
		/// </summary>
		public RoomParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the RoomParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public RoomParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the RoomParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public RoomParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion RoomParameterBuilder
	
	#region RoomSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;RoomColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Room"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class RoomSortBuilder : SqlSortBuilder<RoomColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the RoomSqlSortBuilder class.
		/// </summary>
		public RoomSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion RoomSortBuilder
	
} // end namespace
