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
	/// This class is the base class for any <see cref="RosterProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class RosterProviderBaseCore : EntityProviderBase<ClinicDoctor.Entities.Roster, ClinicDoctor.Entities.RosterKey>
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
		public override bool Delete(TransactionManager transactionManager, ClinicDoctor.Entities.RosterKey key)
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
		/// 	Gets rows from the datasource based on the FK_Roster_RosterType key.
		///		FK_Roster_RosterType Description: 
		/// </summary>
		/// <param name="_rosterTypeId"></param>
		/// <returns>Returns a typed collection of ClinicDoctor.Entities.Roster objects.</returns>
		public TList<Roster> GetByRosterTypeId(System.Int32? _rosterTypeId)
		{
			int count = -1;
			return GetByRosterTypeId(_rosterTypeId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Roster_RosterType key.
		///		FK_Roster_RosterType Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_rosterTypeId"></param>
		/// <returns>Returns a typed collection of ClinicDoctor.Entities.Roster objects.</returns>
		/// <remarks></remarks>
		public TList<Roster> GetByRosterTypeId(TransactionManager transactionManager, System.Int32? _rosterTypeId)
		{
			int count = -1;
			return GetByRosterTypeId(transactionManager, _rosterTypeId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_Roster_RosterType key.
		///		FK_Roster_RosterType Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_rosterTypeId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of ClinicDoctor.Entities.Roster objects.</returns>
		public TList<Roster> GetByRosterTypeId(TransactionManager transactionManager, System.Int32? _rosterTypeId, int start, int pageLength)
		{
			int count = -1;
			return GetByRosterTypeId(transactionManager, _rosterTypeId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Roster_RosterType key.
		///		fkRosterRosterType Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_rosterTypeId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of ClinicDoctor.Entities.Roster objects.</returns>
		public TList<Roster> GetByRosterTypeId(System.Int32? _rosterTypeId, int start, int pageLength)
		{
			int count =  -1;
			return GetByRosterTypeId(null, _rosterTypeId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Roster_RosterType key.
		///		fkRosterRosterType Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_rosterTypeId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of ClinicDoctor.Entities.Roster objects.</returns>
		public TList<Roster> GetByRosterTypeId(System.Int32? _rosterTypeId, int start, int pageLength,out int count)
		{
			return GetByRosterTypeId(null, _rosterTypeId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Roster_RosterType key.
		///		FK_Roster_RosterType Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_rosterTypeId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of ClinicDoctor.Entities.Roster objects.</returns>
		public abstract TList<Roster> GetByRosterTypeId(TransactionManager transactionManager, System.Int32? _rosterTypeId, int start, int pageLength, out int count);
		
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
		public override ClinicDoctor.Entities.Roster Get(TransactionManager transactionManager, ClinicDoctor.Entities.RosterKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Roster index.
		/// </summary>
		/// <param name="_id"></param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Roster"/> class.</returns>
		public ClinicDoctor.Entities.Roster GetById(System.Int32 _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Roster index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Roster"/> class.</returns>
		public ClinicDoctor.Entities.Roster GetById(System.Int32 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Roster index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Roster"/> class.</returns>
		public ClinicDoctor.Entities.Roster GetById(TransactionManager transactionManager, System.Int32 _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Roster index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Roster"/> class.</returns>
		public ClinicDoctor.Entities.Roster GetById(TransactionManager transactionManager, System.Int32 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Roster index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Roster"/> class.</returns>
		public ClinicDoctor.Entities.Roster GetById(System.Int32 _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Roster index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Roster"/> class.</returns>
		public abstract ClinicDoctor.Entities.Roster GetById(TransactionManager transactionManager, System.Int32 _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Roster&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Roster&gt;"/></returns>
		public static TList<Roster> Fill(IDataReader reader, TList<Roster> rows, int start, int pageLength)
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
				
				ClinicDoctor.Entities.Roster c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Roster")
					.Append("|").Append((System.Int32)reader[((int)RosterColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Roster>(
					key.ToString(), // EntityTrackingKey
					"Roster",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new ClinicDoctor.Entities.Roster();
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
					c.Id = (System.Int32)reader[((int)RosterColumn.Id - 1)];
					c.RosterTypeId = (reader.IsDBNull(((int)RosterColumn.RosterTypeId - 1)))?null:(System.Int32?)reader[((int)RosterColumn.RosterTypeId - 1)];
					c.Note = (reader.IsDBNull(((int)RosterColumn.Note - 1)))?null:(System.String)reader[((int)RosterColumn.Note - 1)];
					c.IsDisabled = (reader.IsDBNull(((int)RosterColumn.IsDisabled - 1)))?null:(System.Boolean?)reader[((int)RosterColumn.IsDisabled - 1)];
					c.CreateUser = (reader.IsDBNull(((int)RosterColumn.CreateUser - 1)))?null:(System.String)reader[((int)RosterColumn.CreateUser - 1)];
					c.CreateDate = (reader.IsDBNull(((int)RosterColumn.CreateDate - 1)))?null:(System.DateTime?)reader[((int)RosterColumn.CreateDate - 1)];
					c.UpdateUser = (reader.IsDBNull(((int)RosterColumn.UpdateUser - 1)))?null:(System.String)reader[((int)RosterColumn.UpdateUser - 1)];
					c.UpdateDate = (reader.IsDBNull(((int)RosterColumn.UpdateDate - 1)))?null:(System.DateTime?)reader[((int)RosterColumn.UpdateDate - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="ClinicDoctor.Entities.Roster"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="ClinicDoctor.Entities.Roster"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, ClinicDoctor.Entities.Roster entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (System.Int32)reader[((int)RosterColumn.Id - 1)];
			entity.RosterTypeId = (reader.IsDBNull(((int)RosterColumn.RosterTypeId - 1)))?null:(System.Int32?)reader[((int)RosterColumn.RosterTypeId - 1)];
			entity.Note = (reader.IsDBNull(((int)RosterColumn.Note - 1)))?null:(System.String)reader[((int)RosterColumn.Note - 1)];
			entity.IsDisabled = (reader.IsDBNull(((int)RosterColumn.IsDisabled - 1)))?null:(System.Boolean?)reader[((int)RosterColumn.IsDisabled - 1)];
			entity.CreateUser = (reader.IsDBNull(((int)RosterColumn.CreateUser - 1)))?null:(System.String)reader[((int)RosterColumn.CreateUser - 1)];
			entity.CreateDate = (reader.IsDBNull(((int)RosterColumn.CreateDate - 1)))?null:(System.DateTime?)reader[((int)RosterColumn.CreateDate - 1)];
			entity.UpdateUser = (reader.IsDBNull(((int)RosterColumn.UpdateUser - 1)))?null:(System.String)reader[((int)RosterColumn.UpdateUser - 1)];
			entity.UpdateDate = (reader.IsDBNull(((int)RosterColumn.UpdateDate - 1)))?null:(System.DateTime?)reader[((int)RosterColumn.UpdateDate - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="ClinicDoctor.Entities.Roster"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="ClinicDoctor.Entities.Roster"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, ClinicDoctor.Entities.Roster entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Id = (System.Int32)dataRow["Id"];
			entity.RosterTypeId = Convert.IsDBNull(dataRow["RosterTypeId"]) ? null : (System.Int32?)dataRow["RosterTypeId"];
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
		/// <param name="entity">The <see cref="ClinicDoctor.Entities.Roster"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">ClinicDoctor.Entities.Roster Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, ClinicDoctor.Entities.Roster entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region RosterTypeIdSource	
			if (CanDeepLoad(entity, "RosterType|RosterTypeIdSource", deepLoadType, innerList) 
				&& entity.RosterTypeIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.RosterTypeId ?? (int)0);
				RosterType tmpEntity = EntityManager.LocateEntity<RosterType>(EntityLocator.ConstructKeyFromPkItems(typeof(RosterType), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.RosterTypeIdSource = tmpEntity;
				else
					entity.RosterTypeIdSource = DataRepository.RosterTypeProvider.GetById(transactionManager, (entity.RosterTypeId ?? (int)0));		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'RosterTypeIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.RosterTypeIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.RosterTypeProvider.DeepLoad(transactionManager, entity.RosterTypeIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion RosterTypeIdSource
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetById methods when available
			
			#region DoctorRosterCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<DoctorRoster>|DoctorRosterCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'DoctorRosterCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.DoctorRosterCollection = DataRepository.DoctorRosterProvider.GetByRosterId(transactionManager, entity.Id);

				if (deep && entity.DoctorRosterCollection.Count > 0)
				{
					deepHandles.Add("DoctorRosterCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<DoctorRoster>) DataRepository.DoctorRosterProvider.DeepLoad,
						new object[] { transactionManager, entity.DoctorRosterCollection, deep, deepLoadType, childTypes, innerList }
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
		/// Deep Save the entire object graph of the ClinicDoctor.Entities.Roster object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">ClinicDoctor.Entities.Roster instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">ClinicDoctor.Entities.Roster Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, ClinicDoctor.Entities.Roster entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			
			#region RosterTypeIdSource
			if (CanDeepSave(entity, "RosterType|RosterTypeIdSource", deepSaveType, innerList) 
				&& entity.RosterTypeIdSource != null)
			{
				DataRepository.RosterTypeProvider.Save(transactionManager, entity.RosterTypeIdSource);
				entity.RosterTypeId = entity.RosterTypeIdSource.Id;
			}
			#endregion 
			#endregion Composite Parent Properties

			// Save Root Entity through Provider
			if (!entity.IsDeleted)
				this.Save(transactionManager, entity);
			
			//used to hold DeepSave method delegates and fire after all the local children have been saved.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
	
			#region List<DoctorRoster>
				if (CanDeepSave(entity.DoctorRosterCollection, "List<DoctorRoster>|DoctorRosterCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(DoctorRoster child in entity.DoctorRosterCollection)
					{
						if(child.RosterIdSource != null)
						{
							child.RosterId = child.RosterIdSource.Id;
						}
						else
						{
							child.RosterId = entity.Id;
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
	
	#region RosterChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>ClinicDoctor.Entities.Roster</c>
	///</summary>
	public enum RosterChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>RosterType</c> at RosterTypeIdSource
		///</summary>
		[ChildEntityType(typeof(RosterType))]
		RosterType,
	
		///<summary>
		/// Collection of <c>Roster</c> as OneToMany for DoctorRosterCollection
		///</summary>
		[ChildEntityType(typeof(TList<DoctorRoster>))]
		DoctorRosterCollection,
	}
	
	#endregion RosterChildEntityTypes
	
	#region RosterFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;RosterColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Roster"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class RosterFilterBuilder : SqlFilterBuilder<RosterColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the RosterFilterBuilder class.
		/// </summary>
		public RosterFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the RosterFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public RosterFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the RosterFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public RosterFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion RosterFilterBuilder
	
	#region RosterParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;RosterColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Roster"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class RosterParameterBuilder : ParameterizedSqlFilterBuilder<RosterColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the RosterParameterBuilder class.
		/// </summary>
		public RosterParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the RosterParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public RosterParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the RosterParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public RosterParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion RosterParameterBuilder
	
	#region RosterSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;RosterColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Roster"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class RosterSortBuilder : SqlSortBuilder<RosterColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the RosterSqlSortBuilder class.
		/// </summary>
		public RosterSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion RosterSortBuilder
	
} // end namespace
