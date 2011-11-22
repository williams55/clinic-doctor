#region Using directives

using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;

using ClinicDoctor.Entities;
using ClinicDoctor.Data;

#endregion

namespace ClinicDoctor.Data.Bases
{	
	///<summary>
	/// This class is the base class for any <see cref="StaffProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class StaffProviderBaseCore : EntityProviderBase<ClinicDoctor.Entities.Staff, ClinicDoctor.Entities.StaffKey>
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
		public override bool Delete(TransactionManager transactionManager, ClinicDoctor.Entities.StaffKey key)
		{
			return Delete(transactionManager, key.Id);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_id">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Int32 _id)
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
		public abstract bool Delete(TransactionManager transactionManager, System.Int32 _id);		
		
		#endregion Delete Methods
		
		#region Get By Foreign Key Functions
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Staff_Group key.
		///		FK_Staff_Group Description: 
		/// </summary>
		/// <param name="_groupId"></param>
		/// <returns>Returns a typed collection of ClinicDoctor.Entities.Staff objects.</returns>
		public TList<Staff> GetByGroupId(System.Int32 _groupId)
		{
			int count = -1;
			return GetByGroupId(_groupId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Staff_Group key.
		///		FK_Staff_Group Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_groupId"></param>
		/// <returns>Returns a typed collection of ClinicDoctor.Entities.Staff objects.</returns>
		/// <remarks></remarks>
		public TList<Staff> GetByGroupId(TransactionManager transactionManager, System.Int32 _groupId)
		{
			int count = -1;
			return GetByGroupId(transactionManager, _groupId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_Staff_Group key.
		///		FK_Staff_Group Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_groupId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of ClinicDoctor.Entities.Staff objects.</returns>
		public TList<Staff> GetByGroupId(TransactionManager transactionManager, System.Int32 _groupId, int start, int pageLength)
		{
			int count = -1;
			return GetByGroupId(transactionManager, _groupId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Staff_Group key.
		///		fkStaffGroup Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_groupId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of ClinicDoctor.Entities.Staff objects.</returns>
		public TList<Staff> GetByGroupId(System.Int32 _groupId, int start, int pageLength)
		{
			int count =  -1;
			return GetByGroupId(null, _groupId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Staff_Group key.
		///		fkStaffGroup Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_groupId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of ClinicDoctor.Entities.Staff objects.</returns>
		public TList<Staff> GetByGroupId(System.Int32 _groupId, int start, int pageLength,out int count)
		{
			return GetByGroupId(null, _groupId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Staff_Group key.
		///		FK_Staff_Group Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_groupId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of ClinicDoctor.Entities.Staff objects.</returns>
		public abstract TList<Staff> GetByGroupId(TransactionManager transactionManager, System.Int32 _groupId, int start, int pageLength, out int count);
		
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
		public override ClinicDoctor.Entities.Staff Get(TransactionManager transactionManager, ClinicDoctor.Entities.StaffKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Staff index.
		/// </summary>
		/// <param name="_id"></param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Staff"/> class.</returns>
		public ClinicDoctor.Entities.Staff GetById(System.Int32 _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Staff index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Staff"/> class.</returns>
		public ClinicDoctor.Entities.Staff GetById(System.Int32 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Staff index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Staff"/> class.</returns>
		public ClinicDoctor.Entities.Staff GetById(TransactionManager transactionManager, System.Int32 _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Staff index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Staff"/> class.</returns>
		public ClinicDoctor.Entities.Staff GetById(TransactionManager transactionManager, System.Int32 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Staff index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Staff"/> class.</returns>
		public ClinicDoctor.Entities.Staff GetById(System.Int32 _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Staff index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Staff"/> class.</returns>
		public abstract ClinicDoctor.Entities.Staff GetById(TransactionManager transactionManager, System.Int32 _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Staff&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Staff&gt;"/></returns>
		public static TList<Staff> Fill(IDataReader reader, TList<Staff> rows, int start, int pageLength)
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
				
				ClinicDoctor.Entities.Staff c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Staff")
					.Append("|").Append((System.Int32)reader[((int)StaffColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Staff>(
					key.ToString(), // EntityTrackingKey
					"Staff",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new ClinicDoctor.Entities.Staff();
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
					c.Id = (System.Int32)reader[((int)StaffColumn.Id - 1)];
					c.FirstName = (reader.IsDBNull(((int)StaffColumn.FirstName - 1)))?null:(System.String)reader[((int)StaffColumn.FirstName - 1)];
					c.LastName = (reader.IsDBNull(((int)StaffColumn.LastName - 1)))?null:(System.String)reader[((int)StaffColumn.LastName - 1)];
					c.ShortName = (reader.IsDBNull(((int)StaffColumn.ShortName - 1)))?null:(System.String)reader[((int)StaffColumn.ShortName - 1)];
					c.GroupId = (System.Int32)reader[((int)StaffColumn.GroupId - 1)];
					c.UserName = (reader.IsDBNull(((int)StaffColumn.UserName - 1)))?null:(System.String)reader[((int)StaffColumn.UserName - 1)];
					c.Address = (reader.IsDBNull(((int)StaffColumn.Address - 1)))?null:(System.String)reader[((int)StaffColumn.Address - 1)];
					c.HomePhone = (reader.IsDBNull(((int)StaffColumn.HomePhone - 1)))?null:(System.String)reader[((int)StaffColumn.HomePhone - 1)];
					c.WorkPhone = (reader.IsDBNull(((int)StaffColumn.WorkPhone - 1)))?null:(System.String)reader[((int)StaffColumn.WorkPhone - 1)];
					c.CellPhone = (reader.IsDBNull(((int)StaffColumn.CellPhone - 1)))?null:(System.String)reader[((int)StaffColumn.CellPhone - 1)];
					c.Birthdate = (reader.IsDBNull(((int)StaffColumn.Birthdate - 1)))?null:(System.DateTime?)reader[((int)StaffColumn.Birthdate - 1)];
					c.IsFemale = (reader.IsDBNull(((int)StaffColumn.IsFemale - 1)))?null:(System.Boolean?)reader[((int)StaffColumn.IsFemale - 1)];
					c.Title = (reader.IsDBNull(((int)StaffColumn.Title - 1)))?null:(System.String)reader[((int)StaffColumn.Title - 1)];
					c.Note = (reader.IsDBNull(((int)StaffColumn.Note - 1)))?null:(System.String)reader[((int)StaffColumn.Note - 1)];
					c.IsDisabled = (reader.IsDBNull(((int)StaffColumn.IsDisabled - 1)))?null:(System.Boolean?)reader[((int)StaffColumn.IsDisabled - 1)];
					c.CreateUser = (reader.IsDBNull(((int)StaffColumn.CreateUser - 1)))?null:(System.String)reader[((int)StaffColumn.CreateUser - 1)];
					c.CreateDate = (reader.IsDBNull(((int)StaffColumn.CreateDate - 1)))?null:(System.DateTime?)reader[((int)StaffColumn.CreateDate - 1)];
					c.UpdateUser = (reader.IsDBNull(((int)StaffColumn.UpdateUser - 1)))?null:(System.String)reader[((int)StaffColumn.UpdateUser - 1)];
					c.UpdateDate = (reader.IsDBNull(((int)StaffColumn.UpdateDate - 1)))?null:(System.DateTime?)reader[((int)StaffColumn.UpdateDate - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="ClinicDoctor.Entities.Staff"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="ClinicDoctor.Entities.Staff"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, ClinicDoctor.Entities.Staff entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (System.Int32)reader[((int)StaffColumn.Id - 1)];
			entity.FirstName = (reader.IsDBNull(((int)StaffColumn.FirstName - 1)))?null:(System.String)reader[((int)StaffColumn.FirstName - 1)];
			entity.LastName = (reader.IsDBNull(((int)StaffColumn.LastName - 1)))?null:(System.String)reader[((int)StaffColumn.LastName - 1)];
			entity.ShortName = (reader.IsDBNull(((int)StaffColumn.ShortName - 1)))?null:(System.String)reader[((int)StaffColumn.ShortName - 1)];
			entity.GroupId = (System.Int32)reader[((int)StaffColumn.GroupId - 1)];
			entity.UserName = (reader.IsDBNull(((int)StaffColumn.UserName - 1)))?null:(System.String)reader[((int)StaffColumn.UserName - 1)];
			entity.Address = (reader.IsDBNull(((int)StaffColumn.Address - 1)))?null:(System.String)reader[((int)StaffColumn.Address - 1)];
			entity.HomePhone = (reader.IsDBNull(((int)StaffColumn.HomePhone - 1)))?null:(System.String)reader[((int)StaffColumn.HomePhone - 1)];
			entity.WorkPhone = (reader.IsDBNull(((int)StaffColumn.WorkPhone - 1)))?null:(System.String)reader[((int)StaffColumn.WorkPhone - 1)];
			entity.CellPhone = (reader.IsDBNull(((int)StaffColumn.CellPhone - 1)))?null:(System.String)reader[((int)StaffColumn.CellPhone - 1)];
			entity.Birthdate = (reader.IsDBNull(((int)StaffColumn.Birthdate - 1)))?null:(System.DateTime?)reader[((int)StaffColumn.Birthdate - 1)];
			entity.IsFemale = (reader.IsDBNull(((int)StaffColumn.IsFemale - 1)))?null:(System.Boolean?)reader[((int)StaffColumn.IsFemale - 1)];
			entity.Title = (reader.IsDBNull(((int)StaffColumn.Title - 1)))?null:(System.String)reader[((int)StaffColumn.Title - 1)];
			entity.Note = (reader.IsDBNull(((int)StaffColumn.Note - 1)))?null:(System.String)reader[((int)StaffColumn.Note - 1)];
			entity.IsDisabled = (reader.IsDBNull(((int)StaffColumn.IsDisabled - 1)))?null:(System.Boolean?)reader[((int)StaffColumn.IsDisabled - 1)];
			entity.CreateUser = (reader.IsDBNull(((int)StaffColumn.CreateUser - 1)))?null:(System.String)reader[((int)StaffColumn.CreateUser - 1)];
			entity.CreateDate = (reader.IsDBNull(((int)StaffColumn.CreateDate - 1)))?null:(System.DateTime?)reader[((int)StaffColumn.CreateDate - 1)];
			entity.UpdateUser = (reader.IsDBNull(((int)StaffColumn.UpdateUser - 1)))?null:(System.String)reader[((int)StaffColumn.UpdateUser - 1)];
			entity.UpdateDate = (reader.IsDBNull(((int)StaffColumn.UpdateDate - 1)))?null:(System.DateTime?)reader[((int)StaffColumn.UpdateDate - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="ClinicDoctor.Entities.Staff"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="ClinicDoctor.Entities.Staff"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, ClinicDoctor.Entities.Staff entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Id = (System.Int32)dataRow["Id"];
			entity.FirstName = Convert.IsDBNull(dataRow["FirstName"]) ? null : (System.String)dataRow["FirstName"];
			entity.LastName = Convert.IsDBNull(dataRow["LastName"]) ? null : (System.String)dataRow["LastName"];
			entity.ShortName = Convert.IsDBNull(dataRow["ShortName"]) ? null : (System.String)dataRow["ShortName"];
			entity.GroupId = (System.Int32)dataRow["GroupId"];
			entity.UserName = Convert.IsDBNull(dataRow["UserName"]) ? null : (System.String)dataRow["UserName"];
			entity.Address = Convert.IsDBNull(dataRow["Address"]) ? null : (System.String)dataRow["Address"];
			entity.HomePhone = Convert.IsDBNull(dataRow["HomePhone"]) ? null : (System.String)dataRow["HomePhone"];
			entity.WorkPhone = Convert.IsDBNull(dataRow["WorkPhone"]) ? null : (System.String)dataRow["WorkPhone"];
			entity.CellPhone = Convert.IsDBNull(dataRow["CellPhone"]) ? null : (System.String)dataRow["CellPhone"];
			entity.Birthdate = Convert.IsDBNull(dataRow["Birthdate"]) ? null : (System.DateTime?)dataRow["Birthdate"];
			entity.IsFemale = Convert.IsDBNull(dataRow["IsFemale"]) ? null : (System.Boolean?)dataRow["IsFemale"];
			entity.Title = Convert.IsDBNull(dataRow["Title"]) ? null : (System.String)dataRow["Title"];
			entity.Note = Convert.IsDBNull(dataRow["Note"]) ? null : (System.String)dataRow["Note"];
			entity.IsDisabled = Convert.IsDBNull(dataRow["IsDisabled"]) ? null : (System.Boolean?)dataRow["IsDisabled"];
			entity.CreateUser = Convert.IsDBNull(dataRow["CreateUser"]) ? null : (System.String)dataRow["CreateUser"];
			entity.CreateDate = Convert.IsDBNull(dataRow["CreateDate"]) ? null : (System.DateTime?)dataRow["CreateDate"];
			entity.UpdateUser = Convert.IsDBNull(dataRow["UpdateUser"]) ? null : (System.String)dataRow["UpdateUser"];
			entity.UpdateDate = Convert.IsDBNull(dataRow["UpdateDate"]) ? null : (System.DateTime?)dataRow["UpdateDate"];
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
		/// <param name="entity">The <see cref="ClinicDoctor.Entities.Staff"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">ClinicDoctor.Entities.Staff Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, ClinicDoctor.Entities.Staff entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region GroupIdSource	
			if (CanDeepLoad(entity, "Group|GroupIdSource", deepLoadType, innerList) 
				&& entity.GroupIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = entity.GroupId;
				Group tmpEntity = EntityManager.LocateEntity<Group>(EntityLocator.ConstructKeyFromPkItems(typeof(Group), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.GroupIdSource = tmpEntity;
				else
					entity.GroupIdSource = DataRepository.GroupProvider.GetById(transactionManager, entity.GroupId);		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'GroupIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.GroupIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.GroupProvider.DeepLoad(transactionManager, entity.GroupIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion GroupIdSource
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetById methods when available
			
			#region NurseAppointmentCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<NurseAppointment>|NurseAppointmentCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'NurseAppointmentCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.NurseAppointmentCollection = DataRepository.NurseAppointmentProvider.GetByNurseId(transactionManager, entity.Id);

				if (deep && entity.NurseAppointmentCollection.Count > 0)
				{
					deepHandles.Add("NurseAppointmentCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<NurseAppointment>) DataRepository.NurseAppointmentProvider.DeepLoad,
						new object[] { transactionManager, entity.NurseAppointmentCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			#region DoctorRoomCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<DoctorRoom>|DoctorRoomCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'DoctorRoomCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.DoctorRoomCollection = DataRepository.DoctorRoomProvider.GetByDoctorId(transactionManager, entity.Id);

				if (deep && entity.DoctorRoomCollection.Count > 0)
				{
					deepHandles.Add("DoctorRoomCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<DoctorRoom>) DataRepository.DoctorRoomProvider.DeepLoad,
						new object[] { transactionManager, entity.DoctorRoomCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			#region DoctorRosterCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<DoctorRoster>|DoctorRosterCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'DoctorRosterCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.DoctorRosterCollection = DataRepository.DoctorRosterProvider.GetByDoctorId(transactionManager, entity.Id);

				if (deep && entity.DoctorRosterCollection.Count > 0)
				{
					deepHandles.Add("DoctorRosterCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<DoctorRoster>) DataRepository.DoctorRosterProvider.DeepLoad,
						new object[] { transactionManager, entity.DoctorRosterCollection, deep, deepLoadType, childTypes, innerList }
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

				entity.AppointmentCollection = DataRepository.AppointmentProvider.GetByDoctorId(transactionManager, entity.Id);

				if (deep && entity.AppointmentCollection.Count > 0)
				{
					deepHandles.Add("AppointmentCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<Appointment>) DataRepository.AppointmentProvider.DeepLoad,
						new object[] { transactionManager, entity.AppointmentCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			#region StaffRolesCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<StaffRoles>|StaffRolesCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'StaffRolesCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.StaffRolesCollection = DataRepository.StaffRolesProvider.GetByStaffId(transactionManager, entity.Id);

				if (deep && entity.StaffRolesCollection.Count > 0)
				{
					deepHandles.Add("StaffRolesCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<StaffRoles>) DataRepository.StaffRolesProvider.DeepLoad,
						new object[] { transactionManager, entity.StaffRolesCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			#region DoctorFuncCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<DoctorFunc>|DoctorFuncCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'DoctorFuncCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.DoctorFuncCollection = DataRepository.DoctorFuncProvider.GetByDoctorId(transactionManager, entity.Id);

				if (deep && entity.DoctorFuncCollection.Count > 0)
				{
					deepHandles.Add("DoctorFuncCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<DoctorFunc>) DataRepository.DoctorFuncProvider.DeepLoad,
						new object[] { transactionManager, entity.DoctorFuncCollection, deep, deepLoadType, childTypes, innerList }
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
		/// Deep Save the entire object graph of the ClinicDoctor.Entities.Staff object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">ClinicDoctor.Entities.Staff instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">ClinicDoctor.Entities.Staff Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, ClinicDoctor.Entities.Staff entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			
			#region GroupIdSource
			if (CanDeepSave(entity, "Group|GroupIdSource", deepSaveType, innerList) 
				&& entity.GroupIdSource != null)
			{
				DataRepository.GroupProvider.Save(transactionManager, entity.GroupIdSource);
				entity.GroupId = entity.GroupIdSource.Id;
			}
			#endregion 
			#endregion Composite Parent Properties

			// Save Root Entity through Provider
			if (!entity.IsDeleted)
				this.Save(transactionManager, entity);
			
			//used to hold DeepSave method delegates and fire after all the local children have been saved.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
	
			#region List<NurseAppointment>
				if (CanDeepSave(entity.NurseAppointmentCollection, "List<NurseAppointment>|NurseAppointmentCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(NurseAppointment child in entity.NurseAppointmentCollection)
					{
						if(child.NurseIdSource != null)
						{
							child.NurseId = child.NurseIdSource.Id;
						}
						else
						{
							child.NurseId = entity.Id;
						}

					}

					if (entity.NurseAppointmentCollection.Count > 0 || entity.NurseAppointmentCollection.DeletedItems.Count > 0)
					{
						//DataRepository.NurseAppointmentProvider.Save(transactionManager, entity.NurseAppointmentCollection);
						
						deepHandles.Add("NurseAppointmentCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< NurseAppointment >) DataRepository.NurseAppointmentProvider.DeepSave,
							new object[] { transactionManager, entity.NurseAppointmentCollection, deepSaveType, childTypes, innerList }
						));
					}
				} 
			#endregion 
				
	
			#region List<DoctorRoom>
				if (CanDeepSave(entity.DoctorRoomCollection, "List<DoctorRoom>|DoctorRoomCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(DoctorRoom child in entity.DoctorRoomCollection)
					{
						if(child.DoctorIdSource != null)
						{
							child.DoctorId = child.DoctorIdSource.Id;
						}
						else
						{
							child.DoctorId = entity.Id;
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
				
	
			#region List<DoctorRoster>
				if (CanDeepSave(entity.DoctorRosterCollection, "List<DoctorRoster>|DoctorRosterCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(DoctorRoster child in entity.DoctorRosterCollection)
					{
						if(child.DoctorIdSource != null)
						{
							child.DoctorId = child.DoctorIdSource.Id;
						}
						else
						{
							child.DoctorId = entity.Id;
						}

					}

					if (entity.DoctorRosterCollection.Count > 0 || entity.DoctorRosterCollection.DeletedItems.Count > 0)
					{
						//DataRepository.DoctorRosterProvider.Save(transactionManager, entity.DoctorRosterCollection);
						
						deepHandles.Add("DoctorRosterCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< DoctorRoster >) DataRepository.DoctorRosterProvider.DeepSave,
							new object[] { transactionManager, entity.DoctorRosterCollection, deepSaveType, childTypes, innerList }
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
						if(child.DoctorIdSource != null)
						{
							child.DoctorId = child.DoctorIdSource.Id;
						}
						else
						{
							child.DoctorId = entity.Id;
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
				
	
			#region List<StaffRoles>
				if (CanDeepSave(entity.StaffRolesCollection, "List<StaffRoles>|StaffRolesCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(StaffRoles child in entity.StaffRolesCollection)
					{
						if(child.StaffIdSource != null)
						{
							child.StaffId = child.StaffIdSource.Id;
						}
						else
						{
							child.StaffId = entity.Id;
						}

					}

					if (entity.StaffRolesCollection.Count > 0 || entity.StaffRolesCollection.DeletedItems.Count > 0)
					{
						//DataRepository.StaffRolesProvider.Save(transactionManager, entity.StaffRolesCollection);
						
						deepHandles.Add("StaffRolesCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< StaffRoles >) DataRepository.StaffRolesProvider.DeepSave,
							new object[] { transactionManager, entity.StaffRolesCollection, deepSaveType, childTypes, innerList }
						));
					}
				} 
			#endregion 
				
	
			#region List<DoctorFunc>
				if (CanDeepSave(entity.DoctorFuncCollection, "List<DoctorFunc>|DoctorFuncCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(DoctorFunc child in entity.DoctorFuncCollection)
					{
						if(child.DoctorIdSource != null)
						{
							child.DoctorId = child.DoctorIdSource.Id;
						}
						else
						{
							child.DoctorId = entity.Id;
						}

					}

					if (entity.DoctorFuncCollection.Count > 0 || entity.DoctorFuncCollection.DeletedItems.Count > 0)
					{
						//DataRepository.DoctorFuncProvider.Save(transactionManager, entity.DoctorFuncCollection);
						
						deepHandles.Add("DoctorFuncCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< DoctorFunc >) DataRepository.DoctorFuncProvider.DeepSave,
							new object[] { transactionManager, entity.DoctorFuncCollection, deepSaveType, childTypes, innerList }
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
	
	#region StaffChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>ClinicDoctor.Entities.Staff</c>
	///</summary>
	public enum StaffChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Group</c> at GroupIdSource
		///</summary>
		[ChildEntityType(typeof(Group))]
		Group,
	
		///<summary>
		/// Collection of <c>Staff</c> as OneToMany for NurseAppointmentCollection
		///</summary>
		[ChildEntityType(typeof(TList<NurseAppointment>))]
		NurseAppointmentCollection,

		///<summary>
		/// Collection of <c>Staff</c> as OneToMany for DoctorRoomCollection
		///</summary>
		[ChildEntityType(typeof(TList<DoctorRoom>))]
		DoctorRoomCollection,

		///<summary>
		/// Collection of <c>Staff</c> as OneToMany for DoctorRosterCollection
		///</summary>
		[ChildEntityType(typeof(TList<DoctorRoster>))]
		DoctorRosterCollection,

		///<summary>
		/// Collection of <c>Staff</c> as OneToMany for AppointmentCollection
		///</summary>
		[ChildEntityType(typeof(TList<Appointment>))]
		AppointmentCollection,

		///<summary>
		/// Collection of <c>Staff</c> as OneToMany for StaffRolesCollection
		///</summary>
		[ChildEntityType(typeof(TList<StaffRoles>))]
		StaffRolesCollection,

		///<summary>
		/// Collection of <c>Staff</c> as OneToMany for DoctorFuncCollection
		///</summary>
		[ChildEntityType(typeof(TList<DoctorFunc>))]
		DoctorFuncCollection,
	}
	
	#endregion StaffChildEntityTypes
	
	#region StaffFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;StaffColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Staff"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class StaffFilterBuilder : SqlFilterBuilder<StaffColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the StaffFilterBuilder class.
		/// </summary>
		public StaffFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the StaffFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public StaffFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the StaffFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public StaffFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion StaffFilterBuilder
	
	#region StaffParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;StaffColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Staff"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class StaffParameterBuilder : ParameterizedSqlFilterBuilder<StaffColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the StaffParameterBuilder class.
		/// </summary>
		public StaffParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the StaffParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public StaffParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the StaffParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public StaffParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion StaffParameterBuilder
	
	#region StaffSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;StaffColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Staff"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class StaffSortBuilder : SqlSortBuilder<StaffColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the StaffSqlSortBuilder class.
		/// </summary>
		public StaffSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion StaffSortBuilder
	
} // end namespace
