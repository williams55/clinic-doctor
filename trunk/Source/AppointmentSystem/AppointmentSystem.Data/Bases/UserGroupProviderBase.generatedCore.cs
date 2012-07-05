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
	/// This class is the base class for any <see cref="UserGroupProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class UserGroupProviderBaseCore : EntityProviderBase<AppointmentSystem.Entities.UserGroup, AppointmentSystem.Entities.UserGroupKey>
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
		public override bool Delete(TransactionManager transactionManager, AppointmentSystem.Entities.UserGroupKey key)
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
		public override AppointmentSystem.Entities.UserGroup Get(TransactionManager transactionManager, AppointmentSystem.Entities.UserGroupKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_UserGroup index.
		/// </summary>
		/// <param name="_id"></param>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.UserGroup"/> class.</returns>
		public AppointmentSystem.Entities.UserGroup GetById(System.String _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_UserGroup index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.UserGroup"/> class.</returns>
		public AppointmentSystem.Entities.UserGroup GetById(System.String _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_UserGroup index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.UserGroup"/> class.</returns>
		public AppointmentSystem.Entities.UserGroup GetById(TransactionManager transactionManager, System.String _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_UserGroup index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.UserGroup"/> class.</returns>
		public AppointmentSystem.Entities.UserGroup GetById(TransactionManager transactionManager, System.String _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_UserGroup index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.UserGroup"/> class.</returns>
		public AppointmentSystem.Entities.UserGroup GetById(System.String _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_UserGroup index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.UserGroup"/> class.</returns>
		public abstract AppointmentSystem.Entities.UserGroup GetById(TransactionManager transactionManager, System.String _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;UserGroup&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;UserGroup&gt;"/></returns>
		public static TList<UserGroup> Fill(IDataReader reader, TList<UserGroup> rows, int start, int pageLength)
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
				
				AppointmentSystem.Entities.UserGroup c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("UserGroup")
					.Append("|").Append((System.String)reader[((int)UserGroupColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<UserGroup>(
					key.ToString(), // EntityTrackingKey
					"UserGroup",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new AppointmentSystem.Entities.UserGroup();
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
					c.Id = (System.String)reader[((int)UserGroupColumn.Id - 1)];
					c.OriginalId = c.Id;
					c.Title = (reader.IsDBNull(((int)UserGroupColumn.Title - 1)))?null:(System.String)reader[((int)UserGroupColumn.Title - 1)];
					c.Note = (reader.IsDBNull(((int)UserGroupColumn.Note - 1)))?null:(System.String)reader[((int)UserGroupColumn.Note - 1)];
					c.Roles = (reader.IsDBNull(((int)UserGroupColumn.Roles - 1)))?null:(System.String)reader[((int)UserGroupColumn.Roles - 1)];
					c.IsLocked = (System.Boolean)reader[((int)UserGroupColumn.IsLocked - 1)];
					c.IsDisabled = (System.Boolean)reader[((int)UserGroupColumn.IsDisabled - 1)];
					c.CreateUser = (reader.IsDBNull(((int)UserGroupColumn.CreateUser - 1)))?null:(System.String)reader[((int)UserGroupColumn.CreateUser - 1)];
					c.CreateDate = (System.DateTime)reader[((int)UserGroupColumn.CreateDate - 1)];
					c.UpdateUser = (reader.IsDBNull(((int)UserGroupColumn.UpdateUser - 1)))?null:(System.String)reader[((int)UserGroupColumn.UpdateUser - 1)];
					c.UpdateDate = (System.DateTime)reader[((int)UserGroupColumn.UpdateDate - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="AppointmentSystem.Entities.UserGroup"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.UserGroup"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, AppointmentSystem.Entities.UserGroup entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (System.String)reader[((int)UserGroupColumn.Id - 1)];
			entity.OriginalId = (System.String)reader["Id"];
			entity.Title = (reader.IsDBNull(((int)UserGroupColumn.Title - 1)))?null:(System.String)reader[((int)UserGroupColumn.Title - 1)];
			entity.Note = (reader.IsDBNull(((int)UserGroupColumn.Note - 1)))?null:(System.String)reader[((int)UserGroupColumn.Note - 1)];
			entity.Roles = (reader.IsDBNull(((int)UserGroupColumn.Roles - 1)))?null:(System.String)reader[((int)UserGroupColumn.Roles - 1)];
			entity.IsLocked = (System.Boolean)reader[((int)UserGroupColumn.IsLocked - 1)];
			entity.IsDisabled = (System.Boolean)reader[((int)UserGroupColumn.IsDisabled - 1)];
			entity.CreateUser = (reader.IsDBNull(((int)UserGroupColumn.CreateUser - 1)))?null:(System.String)reader[((int)UserGroupColumn.CreateUser - 1)];
			entity.CreateDate = (System.DateTime)reader[((int)UserGroupColumn.CreateDate - 1)];
			entity.UpdateUser = (reader.IsDBNull(((int)UserGroupColumn.UpdateUser - 1)))?null:(System.String)reader[((int)UserGroupColumn.UpdateUser - 1)];
			entity.UpdateDate = (System.DateTime)reader[((int)UserGroupColumn.UpdateDate - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="AppointmentSystem.Entities.UserGroup"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.UserGroup"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, AppointmentSystem.Entities.UserGroup entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Id = (System.String)dataRow["Id"];
			entity.OriginalId = (System.String)dataRow["Id"];
			entity.Title = Convert.IsDBNull(dataRow["Title"]) ? null : (System.String)dataRow["Title"];
			entity.Note = Convert.IsDBNull(dataRow["Note"]) ? null : (System.String)dataRow["Note"];
			entity.Roles = Convert.IsDBNull(dataRow["Roles"]) ? null : (System.String)dataRow["Roles"];
			entity.IsLocked = (System.Boolean)dataRow["IsLocked"];
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
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.UserGroup"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">AppointmentSystem.Entities.UserGroup Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, AppointmentSystem.Entities.UserGroup entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetById methods when available
			
			#region GroupRoleCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<GroupRole>|GroupRoleCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'GroupRoleCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.GroupRoleCollection = DataRepository.GroupRoleProvider.GetByGroupId(transactionManager, entity.Id);

				if (deep && entity.GroupRoleCollection.Count > 0)
				{
					deepHandles.Add("GroupRoleCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<GroupRole>) DataRepository.GroupRoleProvider.DeepLoad,
						new object[] { transactionManager, entity.GroupRoleCollection, deep, deepLoadType, childTypes, innerList }
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
		/// Deep Save the entire object graph of the AppointmentSystem.Entities.UserGroup object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">AppointmentSystem.Entities.UserGroup instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">AppointmentSystem.Entities.UserGroup Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, AppointmentSystem.Entities.UserGroup entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
			#region List<GroupRole>
				if (CanDeepSave(entity.GroupRoleCollection, "List<GroupRole>|GroupRoleCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(GroupRole child in entity.GroupRoleCollection)
					{
						if(child.GroupIdSource != null)
						{
							child.GroupId = child.GroupIdSource.Id;
						}
						else
						{
							child.GroupId = entity.Id;
						}

					}

					if (entity.GroupRoleCollection.Count > 0 || entity.GroupRoleCollection.DeletedItems.Count > 0)
					{
						//DataRepository.GroupRoleProvider.Save(transactionManager, entity.GroupRoleCollection);
						
						deepHandles.Add("GroupRoleCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< GroupRole >) DataRepository.GroupRoleProvider.DeepSave,
							new object[] { transactionManager, entity.GroupRoleCollection, deepSaveType, childTypes, innerList }
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
	
	#region UserGroupChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>AppointmentSystem.Entities.UserGroup</c>
	///</summary>
	public enum UserGroupChildEntityTypes
	{

		///<summary>
		/// Collection of <c>UserGroup</c> as OneToMany for GroupRoleCollection
		///</summary>
		[ChildEntityType(typeof(TList<GroupRole>))]
		GroupRoleCollection,
	}
	
	#endregion UserGroupChildEntityTypes
	
	#region UserGroupFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;UserGroupColumn&gt;"/> class
	/// that is used exclusively with a <see cref="UserGroup"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class UserGroupFilterBuilder : SqlFilterBuilder<UserGroupColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the UserGroupFilterBuilder class.
		/// </summary>
		public UserGroupFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the UserGroupFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public UserGroupFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the UserGroupFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public UserGroupFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion UserGroupFilterBuilder
	
	#region UserGroupParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;UserGroupColumn&gt;"/> class
	/// that is used exclusively with a <see cref="UserGroup"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class UserGroupParameterBuilder : ParameterizedSqlFilterBuilder<UserGroupColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the UserGroupParameterBuilder class.
		/// </summary>
		public UserGroupParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the UserGroupParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public UserGroupParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the UserGroupParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public UserGroupParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion UserGroupParameterBuilder
	
	#region UserGroupSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;UserGroupColumn&gt;"/> class
	/// that is used exclusively with a <see cref="UserGroup"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class UserGroupSortBuilder : SqlSortBuilder<UserGroupColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the UserGroupSqlSortBuilder class.
		/// </summary>
		public UserGroupSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion UserGroupSortBuilder
	
} // end namespace
