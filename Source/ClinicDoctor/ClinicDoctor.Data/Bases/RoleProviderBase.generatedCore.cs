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
	/// This class is the base class for any <see cref="RoleProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class RoleProviderBaseCore : EntityProviderBase<ClinicDoctor.Entities.Role, ClinicDoctor.Entities.RoleKey>
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
		public override bool Delete(TransactionManager transactionManager, ClinicDoctor.Entities.RoleKey key)
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
		public override ClinicDoctor.Entities.Role Get(TransactionManager transactionManager, ClinicDoctor.Entities.RoleKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Role index.
		/// </summary>
		/// <param name="_id"></param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Role"/> class.</returns>
		public ClinicDoctor.Entities.Role GetById(System.Int32 _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Role index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Role"/> class.</returns>
		public ClinicDoctor.Entities.Role GetById(System.Int32 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Role index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Role"/> class.</returns>
		public ClinicDoctor.Entities.Role GetById(TransactionManager transactionManager, System.Int32 _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Role index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Role"/> class.</returns>
		public ClinicDoctor.Entities.Role GetById(TransactionManager transactionManager, System.Int32 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Role index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Role"/> class.</returns>
		public ClinicDoctor.Entities.Role GetById(System.Int32 _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Role index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Role"/> class.</returns>
		public abstract ClinicDoctor.Entities.Role GetById(TransactionManager transactionManager, System.Int32 _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Role&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Role&gt;"/></returns>
		public static TList<Role> Fill(IDataReader reader, TList<Role> rows, int start, int pageLength)
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
				
				ClinicDoctor.Entities.Role c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Role")
					.Append("|").Append((System.Int32)reader[((int)RoleColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Role>(
					key.ToString(), // EntityTrackingKey
					"Role",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new ClinicDoctor.Entities.Role();
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
					c.Id = (System.Int32)reader[((int)RoleColumn.Id - 1)];
					c.Title = (reader.IsDBNull(((int)RoleColumn.Title - 1)))?null:(System.String)reader[((int)RoleColumn.Title - 1)];
					c.Note = (reader.IsDBNull(((int)RoleColumn.Note - 1)))?null:(System.String)reader[((int)RoleColumn.Note - 1)];
					c.IsDisabled = (reader.IsDBNull(((int)RoleColumn.IsDisabled - 1)))?null:(System.Boolean?)reader[((int)RoleColumn.IsDisabled - 1)];
					c.CreateUser = (reader.IsDBNull(((int)RoleColumn.CreateUser - 1)))?null:(System.String)reader[((int)RoleColumn.CreateUser - 1)];
					c.CreateDate = (reader.IsDBNull(((int)RoleColumn.CreateDate - 1)))?null:(System.DateTime?)reader[((int)RoleColumn.CreateDate - 1)];
					c.UpdateUser = (reader.IsDBNull(((int)RoleColumn.UpdateUser - 1)))?null:(System.String)reader[((int)RoleColumn.UpdateUser - 1)];
					c.UpdateDate = (reader.IsDBNull(((int)RoleColumn.UpdateDate - 1)))?null:(System.DateTime?)reader[((int)RoleColumn.UpdateDate - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="ClinicDoctor.Entities.Role"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="ClinicDoctor.Entities.Role"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, ClinicDoctor.Entities.Role entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (System.Int32)reader[((int)RoleColumn.Id - 1)];
			entity.Title = (reader.IsDBNull(((int)RoleColumn.Title - 1)))?null:(System.String)reader[((int)RoleColumn.Title - 1)];
			entity.Note = (reader.IsDBNull(((int)RoleColumn.Note - 1)))?null:(System.String)reader[((int)RoleColumn.Note - 1)];
			entity.IsDisabled = (reader.IsDBNull(((int)RoleColumn.IsDisabled - 1)))?null:(System.Boolean?)reader[((int)RoleColumn.IsDisabled - 1)];
			entity.CreateUser = (reader.IsDBNull(((int)RoleColumn.CreateUser - 1)))?null:(System.String)reader[((int)RoleColumn.CreateUser - 1)];
			entity.CreateDate = (reader.IsDBNull(((int)RoleColumn.CreateDate - 1)))?null:(System.DateTime?)reader[((int)RoleColumn.CreateDate - 1)];
			entity.UpdateUser = (reader.IsDBNull(((int)RoleColumn.UpdateUser - 1)))?null:(System.String)reader[((int)RoleColumn.UpdateUser - 1)];
			entity.UpdateDate = (reader.IsDBNull(((int)RoleColumn.UpdateDate - 1)))?null:(System.DateTime?)reader[((int)RoleColumn.UpdateDate - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="ClinicDoctor.Entities.Role"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="ClinicDoctor.Entities.Role"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, ClinicDoctor.Entities.Role entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Id = (System.Int32)dataRow["Id"];
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
		/// <param name="entity">The <see cref="ClinicDoctor.Entities.Role"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">ClinicDoctor.Entities.Role Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, ClinicDoctor.Entities.Role entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetById methods when available
			
			#region StaffRolesCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<StaffRoles>|StaffRolesCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'StaffRolesCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.StaffRolesCollection = DataRepository.StaffRolesProvider.GetByRoleId(transactionManager, entity.Id);

				if (deep && entity.StaffRolesCollection.Count > 0)
				{
					deepHandles.Add("StaffRolesCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<StaffRoles>) DataRepository.StaffRolesProvider.DeepLoad,
						new object[] { transactionManager, entity.StaffRolesCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			#region GroupRolesCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<GroupRoles>|GroupRolesCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'GroupRolesCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.GroupRolesCollection = DataRepository.GroupRolesProvider.GetByRoleId(transactionManager, entity.Id);

				if (deep && entity.GroupRolesCollection.Count > 0)
				{
					deepHandles.Add("GroupRolesCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<GroupRoles>) DataRepository.GroupRolesProvider.DeepLoad,
						new object[] { transactionManager, entity.GroupRolesCollection, deep, deepLoadType, childTypes, innerList }
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
		/// Deep Save the entire object graph of the ClinicDoctor.Entities.Role object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">ClinicDoctor.Entities.Role instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">ClinicDoctor.Entities.Role Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, ClinicDoctor.Entities.Role entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
			#region List<StaffRoles>
				if (CanDeepSave(entity.StaffRolesCollection, "List<StaffRoles>|StaffRolesCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(StaffRoles child in entity.StaffRolesCollection)
					{
						if(child.RoleIdSource != null)
						{
							child.RoleId = child.RoleIdSource.Id;
						}
						else
						{
							child.RoleId = entity.Id;
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
				
	
			#region List<GroupRoles>
				if (CanDeepSave(entity.GroupRolesCollection, "List<GroupRoles>|GroupRolesCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(GroupRoles child in entity.GroupRolesCollection)
					{
						if(child.RoleIdSource != null)
						{
							child.RoleId = child.RoleIdSource.Id;
						}
						else
						{
							child.RoleId = entity.Id;
						}

					}

					if (entity.GroupRolesCollection.Count > 0 || entity.GroupRolesCollection.DeletedItems.Count > 0)
					{
						//DataRepository.GroupRolesProvider.Save(transactionManager, entity.GroupRolesCollection);
						
						deepHandles.Add("GroupRolesCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< GroupRoles >) DataRepository.GroupRolesProvider.DeepSave,
							new object[] { transactionManager, entity.GroupRolesCollection, deepSaveType, childTypes, innerList }
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
	
	#region RoleChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>ClinicDoctor.Entities.Role</c>
	///</summary>
	public enum RoleChildEntityTypes
	{

		///<summary>
		/// Collection of <c>Role</c> as OneToMany for StaffRolesCollection
		///</summary>
		[ChildEntityType(typeof(TList<StaffRoles>))]
		StaffRolesCollection,

		///<summary>
		/// Collection of <c>Role</c> as OneToMany for GroupRolesCollection
		///</summary>
		[ChildEntityType(typeof(TList<GroupRoles>))]
		GroupRolesCollection,
	}
	
	#endregion RoleChildEntityTypes
	
	#region RoleFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;RoleColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Role"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class RoleFilterBuilder : SqlFilterBuilder<RoleColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the RoleFilterBuilder class.
		/// </summary>
		public RoleFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the RoleFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public RoleFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the RoleFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public RoleFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion RoleFilterBuilder
	
	#region RoleParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;RoleColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Role"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class RoleParameterBuilder : ParameterizedSqlFilterBuilder<RoleColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the RoleParameterBuilder class.
		/// </summary>
		public RoleParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the RoleParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public RoleParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the RoleParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public RoleParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion RoleParameterBuilder
	
	#region RoleSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;RoleColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Role"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class RoleSortBuilder : SqlSortBuilder<RoleColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the RoleSqlSortBuilder class.
		/// </summary>
		public RoleSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion RoleSortBuilder
	
} // end namespace
