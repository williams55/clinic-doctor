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
	/// This class is the base class for any <see cref="DoctorRosterProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class DoctorRosterProviderBaseCore : EntityProviderBase<ClinicDoctor.Entities.DoctorRoster, ClinicDoctor.Entities.DoctorRosterKey>
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
		public override bool Delete(TransactionManager transactionManager, ClinicDoctor.Entities.DoctorRosterKey key)
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
		/// 	Gets rows from the datasource based on the FK_DoctorRoster_Roster key.
		///		FK_DoctorRoster_Roster Description: 
		/// </summary>
		/// <param name="_rosterId"></param>
		/// <returns>Returns a typed collection of ClinicDoctor.Entities.DoctorRoster objects.</returns>
		public TList<DoctorRoster> GetByRosterId(System.Int32? _rosterId)
		{
			int count = -1;
			return GetByRosterId(_rosterId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_DoctorRoster_Roster key.
		///		FK_DoctorRoster_Roster Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_rosterId"></param>
		/// <returns>Returns a typed collection of ClinicDoctor.Entities.DoctorRoster objects.</returns>
		/// <remarks></remarks>
		public TList<DoctorRoster> GetByRosterId(TransactionManager transactionManager, System.Int32? _rosterId)
		{
			int count = -1;
			return GetByRosterId(transactionManager, _rosterId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_DoctorRoster_Roster key.
		///		FK_DoctorRoster_Roster Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_rosterId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of ClinicDoctor.Entities.DoctorRoster objects.</returns>
		public TList<DoctorRoster> GetByRosterId(TransactionManager transactionManager, System.Int32? _rosterId, int start, int pageLength)
		{
			int count = -1;
			return GetByRosterId(transactionManager, _rosterId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_DoctorRoster_Roster key.
		///		fkDoctorRosterRoster Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_rosterId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of ClinicDoctor.Entities.DoctorRoster objects.</returns>
		public TList<DoctorRoster> GetByRosterId(System.Int32? _rosterId, int start, int pageLength)
		{
			int count =  -1;
			return GetByRosterId(null, _rosterId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_DoctorRoster_Roster key.
		///		fkDoctorRosterRoster Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_rosterId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of ClinicDoctor.Entities.DoctorRoster objects.</returns>
		public TList<DoctorRoster> GetByRosterId(System.Int32? _rosterId, int start, int pageLength,out int count)
		{
			return GetByRosterId(null, _rosterId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_DoctorRoster_Roster key.
		///		FK_DoctorRoster_Roster Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_rosterId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of ClinicDoctor.Entities.DoctorRoster objects.</returns>
		public abstract TList<DoctorRoster> GetByRosterId(TransactionManager transactionManager, System.Int32? _rosterId, int start, int pageLength, out int count);
		
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_DoctorRoster_Staff key.
		///		FK_DoctorRoster_Staff Description: 
		/// </summary>
		/// <param name="_doctorId"></param>
		/// <returns>Returns a typed collection of ClinicDoctor.Entities.DoctorRoster objects.</returns>
		public TList<DoctorRoster> GetByDoctorId(System.Int32? _doctorId)
		{
			int count = -1;
			return GetByDoctorId(_doctorId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_DoctorRoster_Staff key.
		///		FK_DoctorRoster_Staff Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorId"></param>
		/// <returns>Returns a typed collection of ClinicDoctor.Entities.DoctorRoster objects.</returns>
		/// <remarks></remarks>
		public TList<DoctorRoster> GetByDoctorId(TransactionManager transactionManager, System.Int32? _doctorId)
		{
			int count = -1;
			return GetByDoctorId(transactionManager, _doctorId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_DoctorRoster_Staff key.
		///		FK_DoctorRoster_Staff Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of ClinicDoctor.Entities.DoctorRoster objects.</returns>
		public TList<DoctorRoster> GetByDoctorId(TransactionManager transactionManager, System.Int32? _doctorId, int start, int pageLength)
		{
			int count = -1;
			return GetByDoctorId(transactionManager, _doctorId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_DoctorRoster_Staff key.
		///		fkDoctorRosterStaff Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_doctorId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of ClinicDoctor.Entities.DoctorRoster objects.</returns>
		public TList<DoctorRoster> GetByDoctorId(System.Int32? _doctorId, int start, int pageLength)
		{
			int count =  -1;
			return GetByDoctorId(null, _doctorId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_DoctorRoster_Staff key.
		///		fkDoctorRosterStaff Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_doctorId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of ClinicDoctor.Entities.DoctorRoster objects.</returns>
		public TList<DoctorRoster> GetByDoctorId(System.Int32? _doctorId, int start, int pageLength,out int count)
		{
			return GetByDoctorId(null, _doctorId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_DoctorRoster_Staff key.
		///		FK_DoctorRoster_Staff Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of ClinicDoctor.Entities.DoctorRoster objects.</returns>
		public abstract TList<DoctorRoster> GetByDoctorId(TransactionManager transactionManager, System.Int32? _doctorId, int start, int pageLength, out int count);
		
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
		public override ClinicDoctor.Entities.DoctorRoster Get(TransactionManager transactionManager, ClinicDoctor.Entities.DoctorRosterKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_DoctorRoster index.
		/// </summary>
		/// <param name="_id"></param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.DoctorRoster"/> class.</returns>
		public ClinicDoctor.Entities.DoctorRoster GetById(System.Int32 _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_DoctorRoster index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.DoctorRoster"/> class.</returns>
		public ClinicDoctor.Entities.DoctorRoster GetById(System.Int32 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_DoctorRoster index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.DoctorRoster"/> class.</returns>
		public ClinicDoctor.Entities.DoctorRoster GetById(TransactionManager transactionManager, System.Int32 _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_DoctorRoster index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.DoctorRoster"/> class.</returns>
		public ClinicDoctor.Entities.DoctorRoster GetById(TransactionManager transactionManager, System.Int32 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_DoctorRoster index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.DoctorRoster"/> class.</returns>
		public ClinicDoctor.Entities.DoctorRoster GetById(System.Int32 _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_DoctorRoster index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.DoctorRoster"/> class.</returns>
		public abstract ClinicDoctor.Entities.DoctorRoster GetById(TransactionManager transactionManager, System.Int32 _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;DoctorRoster&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;DoctorRoster&gt;"/></returns>
		public static TList<DoctorRoster> Fill(IDataReader reader, TList<DoctorRoster> rows, int start, int pageLength)
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
				
				ClinicDoctor.Entities.DoctorRoster c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("DoctorRoster")
					.Append("|").Append((System.Int32)reader[((int)DoctorRosterColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<DoctorRoster>(
					key.ToString(), // EntityTrackingKey
					"DoctorRoster",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new ClinicDoctor.Entities.DoctorRoster();
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
					c.Id = (System.Int32)reader[((int)DoctorRosterColumn.Id - 1)];
					c.DoctorId = (reader.IsDBNull(((int)DoctorRosterColumn.DoctorId - 1)))?null:(System.Int32?)reader[((int)DoctorRosterColumn.DoctorId - 1)];
					c.RosterId = (reader.IsDBNull(((int)DoctorRosterColumn.RosterId - 1)))?null:(System.Int32?)reader[((int)DoctorRosterColumn.RosterId - 1)];
					c.StartTime = (reader.IsDBNull(((int)DoctorRosterColumn.StartTime - 1)))?null:(System.DateTime?)reader[((int)DoctorRosterColumn.StartTime - 1)];
					c.EndTime = (reader.IsDBNull(((int)DoctorRosterColumn.EndTime - 1)))?null:(System.DateTime?)reader[((int)DoctorRosterColumn.EndTime - 1)];
					c.Note = (reader.IsDBNull(((int)DoctorRosterColumn.Note - 1)))?null:(System.String)reader[((int)DoctorRosterColumn.Note - 1)];
					c.IsDisabled = (reader.IsDBNull(((int)DoctorRosterColumn.IsDisabled - 1)))?null:(System.Boolean?)reader[((int)DoctorRosterColumn.IsDisabled - 1)];
					c.CreateUser = (reader.IsDBNull(((int)DoctorRosterColumn.CreateUser - 1)))?null:(System.String)reader[((int)DoctorRosterColumn.CreateUser - 1)];
					c.CreateDate = (reader.IsDBNull(((int)DoctorRosterColumn.CreateDate - 1)))?null:(System.DateTime?)reader[((int)DoctorRosterColumn.CreateDate - 1)];
					c.UpdateUser = (reader.IsDBNull(((int)DoctorRosterColumn.UpdateUser - 1)))?null:(System.String)reader[((int)DoctorRosterColumn.UpdateUser - 1)];
					c.UpdateDate = (reader.IsDBNull(((int)DoctorRosterColumn.UpdateDate - 1)))?null:(System.DateTime?)reader[((int)DoctorRosterColumn.UpdateDate - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="ClinicDoctor.Entities.DoctorRoster"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="ClinicDoctor.Entities.DoctorRoster"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, ClinicDoctor.Entities.DoctorRoster entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (System.Int32)reader[((int)DoctorRosterColumn.Id - 1)];
			entity.DoctorId = (reader.IsDBNull(((int)DoctorRosterColumn.DoctorId - 1)))?null:(System.Int32?)reader[((int)DoctorRosterColumn.DoctorId - 1)];
			entity.RosterId = (reader.IsDBNull(((int)DoctorRosterColumn.RosterId - 1)))?null:(System.Int32?)reader[((int)DoctorRosterColumn.RosterId - 1)];
			entity.StartTime = (reader.IsDBNull(((int)DoctorRosterColumn.StartTime - 1)))?null:(System.DateTime?)reader[((int)DoctorRosterColumn.StartTime - 1)];
			entity.EndTime = (reader.IsDBNull(((int)DoctorRosterColumn.EndTime - 1)))?null:(System.DateTime?)reader[((int)DoctorRosterColumn.EndTime - 1)];
			entity.Note = (reader.IsDBNull(((int)DoctorRosterColumn.Note - 1)))?null:(System.String)reader[((int)DoctorRosterColumn.Note - 1)];
			entity.IsDisabled = (reader.IsDBNull(((int)DoctorRosterColumn.IsDisabled - 1)))?null:(System.Boolean?)reader[((int)DoctorRosterColumn.IsDisabled - 1)];
			entity.CreateUser = (reader.IsDBNull(((int)DoctorRosterColumn.CreateUser - 1)))?null:(System.String)reader[((int)DoctorRosterColumn.CreateUser - 1)];
			entity.CreateDate = (reader.IsDBNull(((int)DoctorRosterColumn.CreateDate - 1)))?null:(System.DateTime?)reader[((int)DoctorRosterColumn.CreateDate - 1)];
			entity.UpdateUser = (reader.IsDBNull(((int)DoctorRosterColumn.UpdateUser - 1)))?null:(System.String)reader[((int)DoctorRosterColumn.UpdateUser - 1)];
			entity.UpdateDate = (reader.IsDBNull(((int)DoctorRosterColumn.UpdateDate - 1)))?null:(System.DateTime?)reader[((int)DoctorRosterColumn.UpdateDate - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="ClinicDoctor.Entities.DoctorRoster"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="ClinicDoctor.Entities.DoctorRoster"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, ClinicDoctor.Entities.DoctorRoster entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Id = (System.Int32)dataRow["Id"];
			entity.DoctorId = Convert.IsDBNull(dataRow["DoctorId"]) ? null : (System.Int32?)dataRow["DoctorId"];
			entity.RosterId = Convert.IsDBNull(dataRow["RosterId"]) ? null : (System.Int32?)dataRow["RosterId"];
			entity.StartTime = Convert.IsDBNull(dataRow["StartTime"]) ? null : (System.DateTime?)dataRow["StartTime"];
			entity.EndTime = Convert.IsDBNull(dataRow["EndTime"]) ? null : (System.DateTime?)dataRow["EndTime"];
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
		/// <param name="entity">The <see cref="ClinicDoctor.Entities.DoctorRoster"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">ClinicDoctor.Entities.DoctorRoster Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, ClinicDoctor.Entities.DoctorRoster entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region RosterIdSource	
			if (CanDeepLoad(entity, "Roster|RosterIdSource", deepLoadType, innerList) 
				&& entity.RosterIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.RosterId ?? (int)0);
				Roster tmpEntity = EntityManager.LocateEntity<Roster>(EntityLocator.ConstructKeyFromPkItems(typeof(Roster), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.RosterIdSource = tmpEntity;
				else
					entity.RosterIdSource = DataRepository.RosterProvider.GetById(transactionManager, (entity.RosterId ?? (int)0));		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'RosterIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.RosterIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.RosterProvider.DeepLoad(transactionManager, entity.RosterIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion RosterIdSource

			#region DoctorIdSource	
			if (CanDeepLoad(entity, "Staff|DoctorIdSource", deepLoadType, innerList) 
				&& entity.DoctorIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.DoctorId ?? (int)0);
				Staff tmpEntity = EntityManager.LocateEntity<Staff>(EntityLocator.ConstructKeyFromPkItems(typeof(Staff), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.DoctorIdSource = tmpEntity;
				else
					entity.DoctorIdSource = DataRepository.StaffProvider.GetById(transactionManager, (entity.DoctorId ?? (int)0));		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'DoctorIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.DoctorIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.StaffProvider.DeepLoad(transactionManager, entity.DoctorIdSource, deep, deepLoadType, childTypes, innerList);
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
		/// Deep Save the entire object graph of the ClinicDoctor.Entities.DoctorRoster object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">ClinicDoctor.Entities.DoctorRoster instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">ClinicDoctor.Entities.DoctorRoster Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, ClinicDoctor.Entities.DoctorRoster entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			
			#region RosterIdSource
			if (CanDeepSave(entity, "Roster|RosterIdSource", deepSaveType, innerList) 
				&& entity.RosterIdSource != null)
			{
				DataRepository.RosterProvider.Save(transactionManager, entity.RosterIdSource);
				entity.RosterId = entity.RosterIdSource.Id;
			}
			#endregion 
			
			#region DoctorIdSource
			if (CanDeepSave(entity, "Staff|DoctorIdSource", deepSaveType, innerList) 
				&& entity.DoctorIdSource != null)
			{
				DataRepository.StaffProvider.Save(transactionManager, entity.DoctorIdSource);
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
	
	#region DoctorRosterChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>ClinicDoctor.Entities.DoctorRoster</c>
	///</summary>
	public enum DoctorRosterChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Roster</c> at RosterIdSource
		///</summary>
		[ChildEntityType(typeof(Roster))]
		Roster,
			
		///<summary>
		/// Composite Property for <c>Staff</c> at DoctorIdSource
		///</summary>
		[ChildEntityType(typeof(Staff))]
		Staff,
		}
	
	#endregion DoctorRosterChildEntityTypes
	
	#region DoctorRosterFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;DoctorRosterColumn&gt;"/> class
	/// that is used exclusively with a <see cref="DoctorRoster"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class DoctorRosterFilterBuilder : SqlFilterBuilder<DoctorRosterColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DoctorRosterFilterBuilder class.
		/// </summary>
		public DoctorRosterFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the DoctorRosterFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public DoctorRosterFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the DoctorRosterFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public DoctorRosterFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion DoctorRosterFilterBuilder
	
	#region DoctorRosterParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;DoctorRosterColumn&gt;"/> class
	/// that is used exclusively with a <see cref="DoctorRoster"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class DoctorRosterParameterBuilder : ParameterizedSqlFilterBuilder<DoctorRosterColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DoctorRosterParameterBuilder class.
		/// </summary>
		public DoctorRosterParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the DoctorRosterParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public DoctorRosterParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the DoctorRosterParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public DoctorRosterParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion DoctorRosterParameterBuilder
	
	#region DoctorRosterSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;DoctorRosterColumn&gt;"/> class
	/// that is used exclusively with a <see cref="DoctorRoster"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class DoctorRosterSortBuilder : SqlSortBuilder<DoctorRosterColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DoctorRosterSqlSortBuilder class.
		/// </summary>
		public DoctorRosterSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion DoctorRosterSortBuilder
	
} // end namespace
