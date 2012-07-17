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
	/// This class is the base class for any <see cref="AppointmentGroupProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class AppointmentGroupProviderBaseCore : EntityProviderBase<AppointmentSystem.Entities.AppointmentGroup, AppointmentSystem.Entities.AppointmentGroupKey>
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
		public override bool Delete(TransactionManager transactionManager, AppointmentSystem.Entities.AppointmentGroupKey key)
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
		/// 	Gets rows from the datasource based on the FK_AppointmentGroup_Unit key.
		///		FK_AppointmentGroup_Unit Description: 
		/// </summary>
		/// <param name="_unitId">Define current unit belongs to what tab.
		/// 		/// It's seperated by semi-comma [;]
		/// 		/// Ex: 1stFloor;2ndFloor</param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.AppointmentGroup objects.</returns>
		public TList<AppointmentGroup> GetByUnitId(System.Int32? _unitId)
		{
			int count = -1;
			return GetByUnitId(_unitId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_AppointmentGroup_Unit key.
		///		FK_AppointmentGroup_Unit Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_unitId">Define current unit belongs to what tab.
		/// 		/// It's seperated by semi-comma [;]
		/// 		/// Ex: 1stFloor;2ndFloor</param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.AppointmentGroup objects.</returns>
		/// <remarks></remarks>
		public TList<AppointmentGroup> GetByUnitId(TransactionManager transactionManager, System.Int32? _unitId)
		{
			int count = -1;
			return GetByUnitId(transactionManager, _unitId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_AppointmentGroup_Unit key.
		///		FK_AppointmentGroup_Unit Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_unitId">Define current unit belongs to what tab.
		/// 		/// It's seperated by semi-comma [;]
		/// 		/// Ex: 1stFloor;2ndFloor</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.AppointmentGroup objects.</returns>
		public TList<AppointmentGroup> GetByUnitId(TransactionManager transactionManager, System.Int32? _unitId, int start, int pageLength)
		{
			int count = -1;
			return GetByUnitId(transactionManager, _unitId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_AppointmentGroup_Unit key.
		///		fkAppointmentGroupUnit Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_unitId">Define current unit belongs to what tab.
		/// 		/// It's seperated by semi-comma [;]
		/// 		/// Ex: 1stFloor;2ndFloor</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.AppointmentGroup objects.</returns>
		public TList<AppointmentGroup> GetByUnitId(System.Int32? _unitId, int start, int pageLength)
		{
			int count =  -1;
			return GetByUnitId(null, _unitId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_AppointmentGroup_Unit key.
		///		fkAppointmentGroupUnit Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_unitId">Define current unit belongs to what tab.
		/// 		/// It's seperated by semi-comma [;]
		/// 		/// Ex: 1stFloor;2ndFloor</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.AppointmentGroup objects.</returns>
		public TList<AppointmentGroup> GetByUnitId(System.Int32? _unitId, int start, int pageLength,out int count)
		{
			return GetByUnitId(null, _unitId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_AppointmentGroup_Unit key.
		///		FK_AppointmentGroup_Unit Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_unitId">Define current unit belongs to what tab.
		/// 		/// It's seperated by semi-comma [;]
		/// 		/// Ex: 1stFloor;2ndFloor</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.AppointmentGroup objects.</returns>
		public abstract TList<AppointmentGroup> GetByUnitId(TransactionManager transactionManager, System.Int32? _unitId, int start, int pageLength, out int count);
		
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
		public override AppointmentSystem.Entities.AppointmentGroup Get(TransactionManager transactionManager, AppointmentSystem.Entities.AppointmentGroupKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_AppointmentGroup index.
		/// </summary>
		/// <param name="_id"></param>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.AppointmentGroup"/> class.</returns>
		public AppointmentSystem.Entities.AppointmentGroup GetById(System.Int32 _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_AppointmentGroup index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.AppointmentGroup"/> class.</returns>
		public AppointmentSystem.Entities.AppointmentGroup GetById(System.Int32 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_AppointmentGroup index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.AppointmentGroup"/> class.</returns>
		public AppointmentSystem.Entities.AppointmentGroup GetById(TransactionManager transactionManager, System.Int32 _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_AppointmentGroup index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.AppointmentGroup"/> class.</returns>
		public AppointmentSystem.Entities.AppointmentGroup GetById(TransactionManager transactionManager, System.Int32 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_AppointmentGroup index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.AppointmentGroup"/> class.</returns>
		public AppointmentSystem.Entities.AppointmentGroup GetById(System.Int32 _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_AppointmentGroup index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.AppointmentGroup"/> class.</returns>
		public abstract AppointmentSystem.Entities.AppointmentGroup GetById(TransactionManager transactionManager, System.Int32 _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;AppointmentGroup&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;AppointmentGroup&gt;"/></returns>
		public static TList<AppointmentGroup> Fill(IDataReader reader, TList<AppointmentGroup> rows, int start, int pageLength)
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
				
				AppointmentSystem.Entities.AppointmentGroup c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("AppointmentGroup")
					.Append("|").Append((System.Int32)reader[((int)AppointmentGroupColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<AppointmentGroup>(
					key.ToString(), // EntityTrackingKey
					"AppointmentGroup",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new AppointmentSystem.Entities.AppointmentGroup();
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
					c.Id = (System.Int32)reader[((int)AppointmentGroupColumn.Id - 1)];
					c.Title = (reader.IsDBNull(((int)AppointmentGroupColumn.Title - 1)))?null:(System.String)reader[((int)AppointmentGroupColumn.Title - 1)];
					c.Note = (reader.IsDBNull(((int)AppointmentGroupColumn.Note - 1)))?null:(System.String)reader[((int)AppointmentGroupColumn.Note - 1)];
					c.PriorityIndex = (System.Int32)reader[((int)AppointmentGroupColumn.PriorityIndex - 1)];
					c.IsDisabled = (System.Boolean)reader[((int)AppointmentGroupColumn.IsDisabled - 1)];
					c.CreateUser = (reader.IsDBNull(((int)AppointmentGroupColumn.CreateUser - 1)))?null:(System.String)reader[((int)AppointmentGroupColumn.CreateUser - 1)];
					c.CreateDate = (System.DateTime)reader[((int)AppointmentGroupColumn.CreateDate - 1)];
					c.UpdateUser = (reader.IsDBNull(((int)AppointmentGroupColumn.UpdateUser - 1)))?null:(System.String)reader[((int)AppointmentGroupColumn.UpdateUser - 1)];
					c.UpdateDate = (System.DateTime)reader[((int)AppointmentGroupColumn.UpdateDate - 1)];
					c.UnitId = (reader.IsDBNull(((int)AppointmentGroupColumn.UnitId - 1)))?null:(System.Int32?)reader[((int)AppointmentGroupColumn.UnitId - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="AppointmentSystem.Entities.AppointmentGroup"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.AppointmentGroup"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, AppointmentSystem.Entities.AppointmentGroup entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (System.Int32)reader[((int)AppointmentGroupColumn.Id - 1)];
			entity.Title = (reader.IsDBNull(((int)AppointmentGroupColumn.Title - 1)))?null:(System.String)reader[((int)AppointmentGroupColumn.Title - 1)];
			entity.Note = (reader.IsDBNull(((int)AppointmentGroupColumn.Note - 1)))?null:(System.String)reader[((int)AppointmentGroupColumn.Note - 1)];
			entity.PriorityIndex = (System.Int32)reader[((int)AppointmentGroupColumn.PriorityIndex - 1)];
			entity.IsDisabled = (System.Boolean)reader[((int)AppointmentGroupColumn.IsDisabled - 1)];
			entity.CreateUser = (reader.IsDBNull(((int)AppointmentGroupColumn.CreateUser - 1)))?null:(System.String)reader[((int)AppointmentGroupColumn.CreateUser - 1)];
			entity.CreateDate = (System.DateTime)reader[((int)AppointmentGroupColumn.CreateDate - 1)];
			entity.UpdateUser = (reader.IsDBNull(((int)AppointmentGroupColumn.UpdateUser - 1)))?null:(System.String)reader[((int)AppointmentGroupColumn.UpdateUser - 1)];
			entity.UpdateDate = (System.DateTime)reader[((int)AppointmentGroupColumn.UpdateDate - 1)];
			entity.UnitId = (reader.IsDBNull(((int)AppointmentGroupColumn.UnitId - 1)))?null:(System.Int32?)reader[((int)AppointmentGroupColumn.UnitId - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="AppointmentSystem.Entities.AppointmentGroup"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.AppointmentGroup"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, AppointmentSystem.Entities.AppointmentGroup entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Id = (System.Int32)dataRow["Id"];
			entity.Title = Convert.IsDBNull(dataRow["Title"]) ? null : (System.String)dataRow["Title"];
			entity.Note = Convert.IsDBNull(dataRow["Note"]) ? null : (System.String)dataRow["Note"];
			entity.PriorityIndex = (System.Int32)dataRow["PriorityIndex"];
			entity.IsDisabled = (System.Boolean)dataRow["IsDisabled"];
			entity.CreateUser = Convert.IsDBNull(dataRow["CreateUser"]) ? null : (System.String)dataRow["CreateUser"];
			entity.CreateDate = (System.DateTime)dataRow["CreateDate"];
			entity.UpdateUser = Convert.IsDBNull(dataRow["UpdateUser"]) ? null : (System.String)dataRow["UpdateUser"];
			entity.UpdateDate = (System.DateTime)dataRow["UpdateDate"];
			entity.UnitId = Convert.IsDBNull(dataRow["UnitId"]) ? null : (System.Int32?)dataRow["UnitId"];
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
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.AppointmentGroup"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">AppointmentSystem.Entities.AppointmentGroup Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, AppointmentSystem.Entities.AppointmentGroup entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region UnitIdSource	
			if (CanDeepLoad(entity, "Units|UnitIdSource", deepLoadType, innerList) 
				&& entity.UnitIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.UnitId ?? (int)0);
				Units tmpEntity = EntityManager.LocateEntity<Units>(EntityLocator.ConstructKeyFromPkItems(typeof(Units), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.UnitIdSource = tmpEntity;
				else
					entity.UnitIdSource = DataRepository.UnitsProvider.GetById(transactionManager, (entity.UnitId ?? (int)0));		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'UnitIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.UnitIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.UnitsProvider.DeepLoad(transactionManager, entity.UnitIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion UnitIdSource
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetById methods when available
			
			#region AppointmentCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<Appointment>|AppointmentCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'AppointmentCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.AppointmentCollection = DataRepository.AppointmentProvider.GetByAppointmentGroupId(transactionManager, entity.Id);

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
		/// Deep Save the entire object graph of the AppointmentSystem.Entities.AppointmentGroup object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">AppointmentSystem.Entities.AppointmentGroup instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">AppointmentSystem.Entities.AppointmentGroup Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, AppointmentSystem.Entities.AppointmentGroup entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			
			#region UnitIdSource
			if (CanDeepSave(entity, "Units|UnitIdSource", deepSaveType, innerList) 
				&& entity.UnitIdSource != null)
			{
				DataRepository.UnitsProvider.Save(transactionManager, entity.UnitIdSource);
				entity.UnitId = entity.UnitIdSource.Id;
			}
			#endregion 
			#endregion Composite Parent Properties

			// Save Root Entity through Provider
			if (!entity.IsDeleted)
				this.Save(transactionManager, entity);
			
			//used to hold DeepSave method delegates and fire after all the local children have been saved.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
	
			#region List<Appointment>
				if (CanDeepSave(entity.AppointmentCollection, "List<Appointment>|AppointmentCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(Appointment child in entity.AppointmentCollection)
					{
						if(child.AppointmentGroupIdSource != null)
						{
							child.AppointmentGroupId = child.AppointmentGroupIdSource.Id;
						}
						else
						{
							child.AppointmentGroupId = entity.Id;
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
	
	#region AppointmentGroupChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>AppointmentSystem.Entities.AppointmentGroup</c>
	///</summary>
	public enum AppointmentGroupChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Units</c> at UnitIdSource
		///</summary>
		[ChildEntityType(typeof(Units))]
		Units,
	
		///<summary>
		/// Collection of <c>AppointmentGroup</c> as OneToMany for AppointmentCollection
		///</summary>
		[ChildEntityType(typeof(TList<Appointment>))]
		AppointmentCollection,
	}
	
	#endregion AppointmentGroupChildEntityTypes
	
	#region AppointmentGroupFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;AppointmentGroupColumn&gt;"/> class
	/// that is used exclusively with a <see cref="AppointmentGroup"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class AppointmentGroupFilterBuilder : SqlFilterBuilder<AppointmentGroupColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the AppointmentGroupFilterBuilder class.
		/// </summary>
		public AppointmentGroupFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the AppointmentGroupFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public AppointmentGroupFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the AppointmentGroupFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public AppointmentGroupFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion AppointmentGroupFilterBuilder
	
	#region AppointmentGroupParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;AppointmentGroupColumn&gt;"/> class
	/// that is used exclusively with a <see cref="AppointmentGroup"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class AppointmentGroupParameterBuilder : ParameterizedSqlFilterBuilder<AppointmentGroupColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the AppointmentGroupParameterBuilder class.
		/// </summary>
		public AppointmentGroupParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the AppointmentGroupParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public AppointmentGroupParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the AppointmentGroupParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public AppointmentGroupParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion AppointmentGroupParameterBuilder
	
	#region AppointmentGroupSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;AppointmentGroupColumn&gt;"/> class
	/// that is used exclusively with a <see cref="AppointmentGroup"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class AppointmentGroupSortBuilder : SqlSortBuilder<AppointmentGroupColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the AppointmentGroupSqlSortBuilder class.
		/// </summary>
		public AppointmentGroupSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion AppointmentGroupSortBuilder
	
} // end namespace
