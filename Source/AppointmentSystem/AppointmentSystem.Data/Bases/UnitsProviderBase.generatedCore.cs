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
	/// This class is the base class for any <see cref="UnitsProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class UnitsProviderBaseCore : EntityProviderBase<AppointmentSystem.Entities.Units, AppointmentSystem.Entities.UnitsKey>
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
		public override bool Delete(TransactionManager transactionManager, AppointmentSystem.Entities.UnitsKey key)
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
		public override AppointmentSystem.Entities.Units Get(TransactionManager transactionManager, AppointmentSystem.Entities.UnitsKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Unit index.
		/// </summary>
		/// <param name="_id"></param>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.Units"/> class.</returns>
		public AppointmentSystem.Entities.Units GetById(System.Int32 _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Unit index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.Units"/> class.</returns>
		public AppointmentSystem.Entities.Units GetById(System.Int32 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Unit index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.Units"/> class.</returns>
		public AppointmentSystem.Entities.Units GetById(TransactionManager transactionManager, System.Int32 _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Unit index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.Units"/> class.</returns>
		public AppointmentSystem.Entities.Units GetById(TransactionManager transactionManager, System.Int32 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Unit index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.Units"/> class.</returns>
		public AppointmentSystem.Entities.Units GetById(System.Int32 _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Unit index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.Units"/> class.</returns>
		public abstract AppointmentSystem.Entities.Units GetById(TransactionManager transactionManager, System.Int32 _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Units&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Units&gt;"/></returns>
		public static TList<Units> Fill(IDataReader reader, TList<Units> rows, int start, int pageLength)
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
				
				AppointmentSystem.Entities.Units c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Units")
					.Append("|").Append((System.Int32)reader[((int)UnitsColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Units>(
					key.ToString(), // EntityTrackingKey
					"Units",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new AppointmentSystem.Entities.Units();
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
					c.Id = (System.Int32)reader[((int)UnitsColumn.Id - 1)];
					c.Title = (reader.IsDBNull(((int)UnitsColumn.Title - 1)))?null:(System.String)reader[((int)UnitsColumn.Title - 1)];
					c.Note = (reader.IsDBNull(((int)UnitsColumn.Note - 1)))?null:(System.String)reader[((int)UnitsColumn.Note - 1)];
					c.PriorityIndex = (System.Int32)reader[((int)UnitsColumn.PriorityIndex - 1)];
					c.IsDisabled = (System.Boolean)reader[((int)UnitsColumn.IsDisabled - 1)];
					c.CreateUser = (reader.IsDBNull(((int)UnitsColumn.CreateUser - 1)))?null:(System.String)reader[((int)UnitsColumn.CreateUser - 1)];
					c.CreateDate = (System.DateTime)reader[((int)UnitsColumn.CreateDate - 1)];
					c.UpdateUser = (reader.IsDBNull(((int)UnitsColumn.UpdateUser - 1)))?null:(System.String)reader[((int)UnitsColumn.UpdateUser - 1)];
					c.UpdateDate = (System.DateTime)reader[((int)UnitsColumn.UpdateDate - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="AppointmentSystem.Entities.Units"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.Units"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, AppointmentSystem.Entities.Units entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (System.Int32)reader[((int)UnitsColumn.Id - 1)];
			entity.Title = (reader.IsDBNull(((int)UnitsColumn.Title - 1)))?null:(System.String)reader[((int)UnitsColumn.Title - 1)];
			entity.Note = (reader.IsDBNull(((int)UnitsColumn.Note - 1)))?null:(System.String)reader[((int)UnitsColumn.Note - 1)];
			entity.PriorityIndex = (System.Int32)reader[((int)UnitsColumn.PriorityIndex - 1)];
			entity.IsDisabled = (System.Boolean)reader[((int)UnitsColumn.IsDisabled - 1)];
			entity.CreateUser = (reader.IsDBNull(((int)UnitsColumn.CreateUser - 1)))?null:(System.String)reader[((int)UnitsColumn.CreateUser - 1)];
			entity.CreateDate = (System.DateTime)reader[((int)UnitsColumn.CreateDate - 1)];
			entity.UpdateUser = (reader.IsDBNull(((int)UnitsColumn.UpdateUser - 1)))?null:(System.String)reader[((int)UnitsColumn.UpdateUser - 1)];
			entity.UpdateDate = (System.DateTime)reader[((int)UnitsColumn.UpdateDate - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="AppointmentSystem.Entities.Units"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.Units"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, AppointmentSystem.Entities.Units entity)
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
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.Units"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">AppointmentSystem.Entities.Units Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, AppointmentSystem.Entities.Units entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetById methods when available
			
			#region AppointmentGroupCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<AppointmentGroup>|AppointmentGroupCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'AppointmentGroupCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.AppointmentGroupCollection = DataRepository.AppointmentGroupProvider.GetByUnitId(transactionManager, entity.Id);

				if (deep && entity.AppointmentGroupCollection.Count > 0)
				{
					deepHandles.Add("AppointmentGroupCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<AppointmentGroup>) DataRepository.AppointmentGroupProvider.DeepLoad,
						new object[] { transactionManager, entity.AppointmentGroupCollection, deep, deepLoadType, childTypes, innerList }
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
		/// Deep Save the entire object graph of the AppointmentSystem.Entities.Units object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">AppointmentSystem.Entities.Units instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">AppointmentSystem.Entities.Units Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, AppointmentSystem.Entities.Units entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
			#region List<AppointmentGroup>
				if (CanDeepSave(entity.AppointmentGroupCollection, "List<AppointmentGroup>|AppointmentGroupCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(AppointmentGroup child in entity.AppointmentGroupCollection)
					{
						if(child.UnitIdSource != null)
						{
							child.UnitId = child.UnitIdSource.Id;
						}
						else
						{
							child.UnitId = entity.Id;
						}

					}

					if (entity.AppointmentGroupCollection.Count > 0 || entity.AppointmentGroupCollection.DeletedItems.Count > 0)
					{
						//DataRepository.AppointmentGroupProvider.Save(transactionManager, entity.AppointmentGroupCollection);
						
						deepHandles.Add("AppointmentGroupCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< AppointmentGroup >) DataRepository.AppointmentGroupProvider.DeepSave,
							new object[] { transactionManager, entity.AppointmentGroupCollection, deepSaveType, childTypes, innerList }
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
	
	#region UnitsChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>AppointmentSystem.Entities.Units</c>
	///</summary>
	public enum UnitsChildEntityTypes
	{

		///<summary>
		/// Collection of <c>Units</c> as OneToMany for AppointmentGroupCollection
		///</summary>
		[ChildEntityType(typeof(TList<AppointmentGroup>))]
		AppointmentGroupCollection,
	}
	
	#endregion UnitsChildEntityTypes
	
	#region UnitsFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;UnitsColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Units"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class UnitsFilterBuilder : SqlFilterBuilder<UnitsColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the UnitsFilterBuilder class.
		/// </summary>
		public UnitsFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the UnitsFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public UnitsFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the UnitsFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public UnitsFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion UnitsFilterBuilder
	
	#region UnitsParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;UnitsColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Units"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class UnitsParameterBuilder : ParameterizedSqlFilterBuilder<UnitsColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the UnitsParameterBuilder class.
		/// </summary>
		public UnitsParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the UnitsParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public UnitsParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the UnitsParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public UnitsParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion UnitsParameterBuilder
	
	#region UnitsSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;UnitsColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Units"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class UnitsSortBuilder : SqlSortBuilder<UnitsColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the UnitsSqlSortBuilder class.
		/// </summary>
		public UnitsSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion UnitsSortBuilder
	
} // end namespace
