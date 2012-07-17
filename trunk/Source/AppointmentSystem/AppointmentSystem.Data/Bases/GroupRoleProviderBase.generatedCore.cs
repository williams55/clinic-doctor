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
	/// This class is the base class for any <see cref="GroupRoleProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class GroupRoleProviderBaseCore : EntityProviderBase<AppointmentSystem.Entities.GroupRole, AppointmentSystem.Entities.GroupRoleKey>
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
		public override bool Delete(TransactionManager transactionManager, AppointmentSystem.Entities.GroupRoleKey key)
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
		/// 	Gets rows from the datasource based on the FK_GroupRole_Role key.
		///		FK_GroupRole_Role Description: 
		/// </summary>
		/// <param name="_roleId"></param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.GroupRole objects.</returns>
		public TList<GroupRole> GetByRoleId(System.Int32? _roleId)
		{
			int count = -1;
			return GetByRoleId(_roleId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_GroupRole_Role key.
		///		FK_GroupRole_Role Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_roleId"></param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.GroupRole objects.</returns>
		/// <remarks></remarks>
		public TList<GroupRole> GetByRoleId(TransactionManager transactionManager, System.Int32? _roleId)
		{
			int count = -1;
			return GetByRoleId(transactionManager, _roleId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_GroupRole_Role key.
		///		FK_GroupRole_Role Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_roleId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.GroupRole objects.</returns>
		public TList<GroupRole> GetByRoleId(TransactionManager transactionManager, System.Int32? _roleId, int start, int pageLength)
		{
			int count = -1;
			return GetByRoleId(transactionManager, _roleId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_GroupRole_Role key.
		///		fkGroupRoleRole Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_roleId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.GroupRole objects.</returns>
		public TList<GroupRole> GetByRoleId(System.Int32? _roleId, int start, int pageLength)
		{
			int count =  -1;
			return GetByRoleId(null, _roleId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_GroupRole_Role key.
		///		fkGroupRoleRole Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_roleId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.GroupRole objects.</returns>
		public TList<GroupRole> GetByRoleId(System.Int32? _roleId, int start, int pageLength,out int count)
		{
			return GetByRoleId(null, _roleId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_GroupRole_Role key.
		///		FK_GroupRole_Role Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_roleId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.GroupRole objects.</returns>
		public abstract TList<GroupRole> GetByRoleId(TransactionManager transactionManager, System.Int32? _roleId, int start, int pageLength, out int count);
		
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_GroupRole_UserGroup key.
		///		FK_GroupRole_UserGroup Description: 
		/// </summary>
		/// <param name="_groupId"></param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.GroupRole objects.</returns>
		public TList<GroupRole> GetByGroupId(System.String _groupId)
		{
			int count = -1;
			return GetByGroupId(_groupId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_GroupRole_UserGroup key.
		///		FK_GroupRole_UserGroup Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_groupId"></param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.GroupRole objects.</returns>
		/// <remarks></remarks>
		public TList<GroupRole> GetByGroupId(TransactionManager transactionManager, System.String _groupId)
		{
			int count = -1;
			return GetByGroupId(transactionManager, _groupId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_GroupRole_UserGroup key.
		///		FK_GroupRole_UserGroup Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_groupId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.GroupRole objects.</returns>
		public TList<GroupRole> GetByGroupId(TransactionManager transactionManager, System.String _groupId, int start, int pageLength)
		{
			int count = -1;
			return GetByGroupId(transactionManager, _groupId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_GroupRole_UserGroup key.
		///		fkGroupRoleUserGroup Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_groupId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.GroupRole objects.</returns>
		public TList<GroupRole> GetByGroupId(System.String _groupId, int start, int pageLength)
		{
			int count =  -1;
			return GetByGroupId(null, _groupId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_GroupRole_UserGroup key.
		///		fkGroupRoleUserGroup Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_groupId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.GroupRole objects.</returns>
		public TList<GroupRole> GetByGroupId(System.String _groupId, int start, int pageLength,out int count)
		{
			return GetByGroupId(null, _groupId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_GroupRole_UserGroup key.
		///		FK_GroupRole_UserGroup Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_groupId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.GroupRole objects.</returns>
		public abstract TList<GroupRole> GetByGroupId(TransactionManager transactionManager, System.String _groupId, int start, int pageLength, out int count);
		
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
		public override AppointmentSystem.Entities.GroupRole Get(TransactionManager transactionManager, AppointmentSystem.Entities.GroupRoleKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_GroupRole index.
		/// </summary>
		/// <param name="_id"></param>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.GroupRole"/> class.</returns>
		public AppointmentSystem.Entities.GroupRole GetById(System.Int64 _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_GroupRole index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.GroupRole"/> class.</returns>
		public AppointmentSystem.Entities.GroupRole GetById(System.Int64 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_GroupRole index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.GroupRole"/> class.</returns>
		public AppointmentSystem.Entities.GroupRole GetById(TransactionManager transactionManager, System.Int64 _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_GroupRole index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.GroupRole"/> class.</returns>
		public AppointmentSystem.Entities.GroupRole GetById(TransactionManager transactionManager, System.Int64 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_GroupRole index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.GroupRole"/> class.</returns>
		public AppointmentSystem.Entities.GroupRole GetById(System.Int64 _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_GroupRole index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.GroupRole"/> class.</returns>
		public abstract AppointmentSystem.Entities.GroupRole GetById(TransactionManager transactionManager, System.Int64 _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;GroupRole&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;GroupRole&gt;"/></returns>
		public static TList<GroupRole> Fill(IDataReader reader, TList<GroupRole> rows, int start, int pageLength)
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
				
				AppointmentSystem.Entities.GroupRole c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("GroupRole")
					.Append("|").Append((System.Int64)reader[((int)GroupRoleColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<GroupRole>(
					key.ToString(), // EntityTrackingKey
					"GroupRole",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new AppointmentSystem.Entities.GroupRole();
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
					c.Id = (System.Int64)reader[((int)GroupRoleColumn.Id - 1)];
					c.GroupId = (System.String)reader[((int)GroupRoleColumn.GroupId - 1)];
					c.RoleId = (reader.IsDBNull(((int)GroupRoleColumn.RoleId - 1)))?null:(System.Int32?)reader[((int)GroupRoleColumn.RoleId - 1)];
					c.IsDisabled = (System.Boolean)reader[((int)GroupRoleColumn.IsDisabled - 1)];
					c.CreateUser = (reader.IsDBNull(((int)GroupRoleColumn.CreateUser - 1)))?null:(System.String)reader[((int)GroupRoleColumn.CreateUser - 1)];
					c.CreateDate = (System.DateTime)reader[((int)GroupRoleColumn.CreateDate - 1)];
					c.UpdateUser = (reader.IsDBNull(((int)GroupRoleColumn.UpdateUser - 1)))?null:(System.String)reader[((int)GroupRoleColumn.UpdateUser - 1)];
					c.UpdateDate = (System.DateTime)reader[((int)GroupRoleColumn.UpdateDate - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="AppointmentSystem.Entities.GroupRole"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.GroupRole"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, AppointmentSystem.Entities.GroupRole entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (System.Int64)reader[((int)GroupRoleColumn.Id - 1)];
			entity.GroupId = (System.String)reader[((int)GroupRoleColumn.GroupId - 1)];
			entity.RoleId = (reader.IsDBNull(((int)GroupRoleColumn.RoleId - 1)))?null:(System.Int32?)reader[((int)GroupRoleColumn.RoleId - 1)];
			entity.IsDisabled = (System.Boolean)reader[((int)GroupRoleColumn.IsDisabled - 1)];
			entity.CreateUser = (reader.IsDBNull(((int)GroupRoleColumn.CreateUser - 1)))?null:(System.String)reader[((int)GroupRoleColumn.CreateUser - 1)];
			entity.CreateDate = (System.DateTime)reader[((int)GroupRoleColumn.CreateDate - 1)];
			entity.UpdateUser = (reader.IsDBNull(((int)GroupRoleColumn.UpdateUser - 1)))?null:(System.String)reader[((int)GroupRoleColumn.UpdateUser - 1)];
			entity.UpdateDate = (System.DateTime)reader[((int)GroupRoleColumn.UpdateDate - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="AppointmentSystem.Entities.GroupRole"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.GroupRole"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, AppointmentSystem.Entities.GroupRole entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Id = (System.Int64)dataRow["Id"];
			entity.GroupId = (System.String)dataRow["GroupId"];
			entity.RoleId = Convert.IsDBNull(dataRow["RoleId"]) ? null : (System.Int32?)dataRow["RoleId"];
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
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.GroupRole"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">AppointmentSystem.Entities.GroupRole Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, AppointmentSystem.Entities.GroupRole entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region RoleIdSource	
			if (CanDeepLoad(entity, "Role|RoleIdSource", deepLoadType, innerList) 
				&& entity.RoleIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.RoleId ?? (int)0);
				Role tmpEntity = EntityManager.LocateEntity<Role>(EntityLocator.ConstructKeyFromPkItems(typeof(Role), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.RoleIdSource = tmpEntity;
				else
					entity.RoleIdSource = DataRepository.RoleProvider.GetById(transactionManager, (entity.RoleId ?? (int)0));		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'RoleIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.RoleIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.RoleProvider.DeepLoad(transactionManager, entity.RoleIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion RoleIdSource

			#region GroupIdSource	
			if (CanDeepLoad(entity, "UserGroup|GroupIdSource", deepLoadType, innerList) 
				&& entity.GroupIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = entity.GroupId;
				UserGroup tmpEntity = EntityManager.LocateEntity<UserGroup>(EntityLocator.ConstructKeyFromPkItems(typeof(UserGroup), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.GroupIdSource = tmpEntity;
				else
					entity.GroupIdSource = DataRepository.UserGroupProvider.GetById(transactionManager, entity.GroupId);		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'GroupIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.GroupIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.UserGroupProvider.DeepLoad(transactionManager, entity.GroupIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion GroupIdSource
			
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
		/// Deep Save the entire object graph of the AppointmentSystem.Entities.GroupRole object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">AppointmentSystem.Entities.GroupRole instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">AppointmentSystem.Entities.GroupRole Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, AppointmentSystem.Entities.GroupRole entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			
			#region RoleIdSource
			if (CanDeepSave(entity, "Role|RoleIdSource", deepSaveType, innerList) 
				&& entity.RoleIdSource != null)
			{
				DataRepository.RoleProvider.Save(transactionManager, entity.RoleIdSource);
				entity.RoleId = entity.RoleIdSource.Id;
			}
			#endregion 
			
			#region GroupIdSource
			if (CanDeepSave(entity, "UserGroup|GroupIdSource", deepSaveType, innerList) 
				&& entity.GroupIdSource != null)
			{
				DataRepository.UserGroupProvider.Save(transactionManager, entity.GroupIdSource);
				entity.GroupId = entity.GroupIdSource.Id;
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
	
	#region GroupRoleChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>AppointmentSystem.Entities.GroupRole</c>
	///</summary>
	public enum GroupRoleChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Role</c> at RoleIdSource
		///</summary>
		[ChildEntityType(typeof(Role))]
		Role,
			
		///<summary>
		/// Composite Property for <c>UserGroup</c> at GroupIdSource
		///</summary>
		[ChildEntityType(typeof(UserGroup))]
		UserGroup,
		}
	
	#endregion GroupRoleChildEntityTypes
	
	#region GroupRoleFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;GroupRoleColumn&gt;"/> class
	/// that is used exclusively with a <see cref="GroupRole"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class GroupRoleFilterBuilder : SqlFilterBuilder<GroupRoleColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the GroupRoleFilterBuilder class.
		/// </summary>
		public GroupRoleFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the GroupRoleFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public GroupRoleFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the GroupRoleFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public GroupRoleFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion GroupRoleFilterBuilder
	
	#region GroupRoleParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;GroupRoleColumn&gt;"/> class
	/// that is used exclusively with a <see cref="GroupRole"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class GroupRoleParameterBuilder : ParameterizedSqlFilterBuilder<GroupRoleColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the GroupRoleParameterBuilder class.
		/// </summary>
		public GroupRoleParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the GroupRoleParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public GroupRoleParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the GroupRoleParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public GroupRoleParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion GroupRoleParameterBuilder
	
	#region GroupRoleSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;GroupRoleColumn&gt;"/> class
	/// that is used exclusively with a <see cref="GroupRole"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class GroupRoleSortBuilder : SqlSortBuilder<GroupRoleColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the GroupRoleSqlSortBuilder class.
		/// </summary>
		public GroupRoleSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion GroupRoleSortBuilder
	
} // end namespace
