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
	/// This class is the base class for any <see cref="UsersProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class UsersProviderBaseCore : EntityProviderBase<AppointmentSystem.Entities.Users, AppointmentSystem.Entities.UsersKey>
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
		public override bool Delete(TransactionManager transactionManager, AppointmentSystem.Entities.UsersKey key)
		{
			return Delete(transactionManager, key.Username);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_username">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.String _username)
		{
			return Delete(null, _username);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_username">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.String _username);		
		
		#endregion Delete Methods
		
		#region Get By Foreign Key Functions
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Users_Services key.
		///		FK_Users_Services Description: 
		/// </summary>
		/// <param name="_servicesId">This user belongs what groups. It's seperated by semi-comma</param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Users objects.</returns>
		public TList<Users> GetByServicesId(System.Int32? _servicesId)
		{
			int count = -1;
			return GetByServicesId(_servicesId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Users_Services key.
		///		FK_Users_Services Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_servicesId">This user belongs what groups. It's seperated by semi-comma</param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Users objects.</returns>
		/// <remarks></remarks>
		public TList<Users> GetByServicesId(TransactionManager transactionManager, System.Int32? _servicesId)
		{
			int count = -1;
			return GetByServicesId(transactionManager, _servicesId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_Users_Services key.
		///		FK_Users_Services Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_servicesId">This user belongs what groups. It's seperated by semi-comma</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Users objects.</returns>
		public TList<Users> GetByServicesId(TransactionManager transactionManager, System.Int32? _servicesId, int start, int pageLength)
		{
			int count = -1;
			return GetByServicesId(transactionManager, _servicesId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Users_Services key.
		///		fkUsersServices Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_servicesId">This user belongs what groups. It's seperated by semi-comma</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Users objects.</returns>
		public TList<Users> GetByServicesId(System.Int32? _servicesId, int start, int pageLength)
		{
			int count =  -1;
			return GetByServicesId(null, _servicesId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Users_Services key.
		///		fkUsersServices Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_servicesId">This user belongs what groups. It's seperated by semi-comma</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Users objects.</returns>
		public TList<Users> GetByServicesId(System.Int32? _servicesId, int start, int pageLength,out int count)
		{
			return GetByServicesId(null, _servicesId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Users_Services key.
		///		FK_Users_Services Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_servicesId">This user belongs what groups. It's seperated by semi-comma</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Users objects.</returns>
		public abstract TList<Users> GetByServicesId(TransactionManager transactionManager, System.Int32? _servicesId, int start, int pageLength, out int count);
		
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Users_UserGroup key.
		///		FK_Users_UserGroup Description: 
		/// </summary>
		/// <param name="_userGroupId">This user belongs what groups. It's seperated by semi-comma</param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Users objects.</returns>
		public TList<Users> GetByUserGroupId(System.String _userGroupId)
		{
			int count = -1;
			return GetByUserGroupId(_userGroupId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Users_UserGroup key.
		///		FK_Users_UserGroup Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_userGroupId">This user belongs what groups. It's seperated by semi-comma</param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Users objects.</returns>
		/// <remarks></remarks>
		public TList<Users> GetByUserGroupId(TransactionManager transactionManager, System.String _userGroupId)
		{
			int count = -1;
			return GetByUserGroupId(transactionManager, _userGroupId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_Users_UserGroup key.
		///		FK_Users_UserGroup Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_userGroupId">This user belongs what groups. It's seperated by semi-comma</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Users objects.</returns>
		public TList<Users> GetByUserGroupId(TransactionManager transactionManager, System.String _userGroupId, int start, int pageLength)
		{
			int count = -1;
			return GetByUserGroupId(transactionManager, _userGroupId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Users_UserGroup key.
		///		fkUsersUserGroup Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_userGroupId">This user belongs what groups. It's seperated by semi-comma</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Users objects.</returns>
		public TList<Users> GetByUserGroupId(System.String _userGroupId, int start, int pageLength)
		{
			int count =  -1;
			return GetByUserGroupId(null, _userGroupId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Users_UserGroup key.
		///		fkUsersUserGroup Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_userGroupId">This user belongs what groups. It's seperated by semi-comma</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Users objects.</returns>
		public TList<Users> GetByUserGroupId(System.String _userGroupId, int start, int pageLength,out int count)
		{
			return GetByUserGroupId(null, _userGroupId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Users_UserGroup key.
		///		FK_Users_UserGroup Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_userGroupId">This user belongs what groups. It's seperated by semi-comma</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Users objects.</returns>
		public abstract TList<Users> GetByUserGroupId(TransactionManager transactionManager, System.String _userGroupId, int start, int pageLength, out int count);
		
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
		public override AppointmentSystem.Entities.Users Get(TransactionManager transactionManager, AppointmentSystem.Entities.UsersKey key, int start, int pageLength)
		{
			return GetByUsername(transactionManager, key.Username, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_User index.
		/// </summary>
		/// <param name="_username"></param>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.Users"/> class.</returns>
		public AppointmentSystem.Entities.Users GetByUsername(System.String _username)
		{
			int count = -1;
			return GetByUsername(null,_username, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_User index.
		/// </summary>
		/// <param name="_username"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.Users"/> class.</returns>
		public AppointmentSystem.Entities.Users GetByUsername(System.String _username, int start, int pageLength)
		{
			int count = -1;
			return GetByUsername(null, _username, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_User index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_username"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.Users"/> class.</returns>
		public AppointmentSystem.Entities.Users GetByUsername(TransactionManager transactionManager, System.String _username)
		{
			int count = -1;
			return GetByUsername(transactionManager, _username, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_User index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_username"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.Users"/> class.</returns>
		public AppointmentSystem.Entities.Users GetByUsername(TransactionManager transactionManager, System.String _username, int start, int pageLength)
		{
			int count = -1;
			return GetByUsername(transactionManager, _username, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_User index.
		/// </summary>
		/// <param name="_username"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.Users"/> class.</returns>
		public AppointmentSystem.Entities.Users GetByUsername(System.String _username, int start, int pageLength, out int count)
		{
			return GetByUsername(null, _username, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_User index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_username"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.Users"/> class.</returns>
		public abstract AppointmentSystem.Entities.Users GetByUsername(TransactionManager transactionManager, System.String _username, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Users&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Users&gt;"/></returns>
		public static TList<Users> Fill(IDataReader reader, TList<Users> rows, int start, int pageLength)
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
				
				AppointmentSystem.Entities.Users c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Users")
					.Append("|").Append((System.String)reader[((int)UsersColumn.Username - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Users>(
					key.ToString(), // EntityTrackingKey
					"Users",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new AppointmentSystem.Entities.Users();
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
					c.Username = (System.String)reader[((int)UsersColumn.Username - 1)];
					c.OriginalUsername = c.Username;
					c.Title = (reader.IsDBNull(((int)UsersColumn.Title - 1)))?null:(System.String)reader[((int)UsersColumn.Title - 1)];
					c.Firstname = (reader.IsDBNull(((int)UsersColumn.Firstname - 1)))?null:(System.String)reader[((int)UsersColumn.Firstname - 1)];
					c.Lastname = (reader.IsDBNull(((int)UsersColumn.Lastname - 1)))?null:(System.String)reader[((int)UsersColumn.Lastname - 1)];
					c.DisplayName = (System.String)reader[((int)UsersColumn.DisplayName - 1)];
					c.CellPhone = (reader.IsDBNull(((int)UsersColumn.CellPhone - 1)))?null:(System.String)reader[((int)UsersColumn.CellPhone - 1)];
					c.Email = (reader.IsDBNull(((int)UsersColumn.Email - 1)))?null:(System.String)reader[((int)UsersColumn.Email - 1)];
					c.Avatar = (reader.IsDBNull(((int)UsersColumn.Avatar - 1)))?null:(System.String)reader[((int)UsersColumn.Avatar - 1)];
					c.Note = (reader.IsDBNull(((int)UsersColumn.Note - 1)))?null:(System.String)reader[((int)UsersColumn.Note - 1)];
					c.UserGroupId = (System.String)reader[((int)UsersColumn.UserGroupId - 1)];
					c.ServicesId = (reader.IsDBNull(((int)UsersColumn.ServicesId - 1)))?null:(System.Int32?)reader[((int)UsersColumn.ServicesId - 1)];
					c.IsFemale = (System.Boolean)reader[((int)UsersColumn.IsFemale - 1)];
					c.IsDisabled = (System.Boolean)reader[((int)UsersColumn.IsDisabled - 1)];
					c.CreateUser = (reader.IsDBNull(((int)UsersColumn.CreateUser - 1)))?null:(System.String)reader[((int)UsersColumn.CreateUser - 1)];
					c.CreateDate = (System.DateTime)reader[((int)UsersColumn.CreateDate - 1)];
					c.UpdateUser = (reader.IsDBNull(((int)UsersColumn.UpdateUser - 1)))?null:(System.String)reader[((int)UsersColumn.UpdateUser - 1)];
					c.UpdateDate = (System.DateTime)reader[((int)UsersColumn.UpdateDate - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="AppointmentSystem.Entities.Users"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.Users"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, AppointmentSystem.Entities.Users entity)
		{
			if (!reader.Read()) return;
			
			entity.Username = (System.String)reader[((int)UsersColumn.Username - 1)];
			entity.OriginalUsername = (System.String)reader["Username"];
			entity.Title = (reader.IsDBNull(((int)UsersColumn.Title - 1)))?null:(System.String)reader[((int)UsersColumn.Title - 1)];
			entity.Firstname = (reader.IsDBNull(((int)UsersColumn.Firstname - 1)))?null:(System.String)reader[((int)UsersColumn.Firstname - 1)];
			entity.Lastname = (reader.IsDBNull(((int)UsersColumn.Lastname - 1)))?null:(System.String)reader[((int)UsersColumn.Lastname - 1)];
			entity.DisplayName = (System.String)reader[((int)UsersColumn.DisplayName - 1)];
			entity.CellPhone = (reader.IsDBNull(((int)UsersColumn.CellPhone - 1)))?null:(System.String)reader[((int)UsersColumn.CellPhone - 1)];
			entity.Email = (reader.IsDBNull(((int)UsersColumn.Email - 1)))?null:(System.String)reader[((int)UsersColumn.Email - 1)];
			entity.Avatar = (reader.IsDBNull(((int)UsersColumn.Avatar - 1)))?null:(System.String)reader[((int)UsersColumn.Avatar - 1)];
			entity.Note = (reader.IsDBNull(((int)UsersColumn.Note - 1)))?null:(System.String)reader[((int)UsersColumn.Note - 1)];
			entity.UserGroupId = (System.String)reader[((int)UsersColumn.UserGroupId - 1)];
			entity.ServicesId = (reader.IsDBNull(((int)UsersColumn.ServicesId - 1)))?null:(System.Int32?)reader[((int)UsersColumn.ServicesId - 1)];
			entity.IsFemale = (System.Boolean)reader[((int)UsersColumn.IsFemale - 1)];
			entity.IsDisabled = (System.Boolean)reader[((int)UsersColumn.IsDisabled - 1)];
			entity.CreateUser = (reader.IsDBNull(((int)UsersColumn.CreateUser - 1)))?null:(System.String)reader[((int)UsersColumn.CreateUser - 1)];
			entity.CreateDate = (System.DateTime)reader[((int)UsersColumn.CreateDate - 1)];
			entity.UpdateUser = (reader.IsDBNull(((int)UsersColumn.UpdateUser - 1)))?null:(System.String)reader[((int)UsersColumn.UpdateUser - 1)];
			entity.UpdateDate = (System.DateTime)reader[((int)UsersColumn.UpdateDate - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="AppointmentSystem.Entities.Users"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.Users"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, AppointmentSystem.Entities.Users entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Username = (System.String)dataRow["Username"];
			entity.OriginalUsername = (System.String)dataRow["Username"];
			entity.Title = Convert.IsDBNull(dataRow["Title"]) ? null : (System.String)dataRow["Title"];
			entity.Firstname = Convert.IsDBNull(dataRow["Firstname"]) ? null : (System.String)dataRow["Firstname"];
			entity.Lastname = Convert.IsDBNull(dataRow["Lastname"]) ? null : (System.String)dataRow["Lastname"];
			entity.DisplayName = (System.String)dataRow["DisplayName"];
			entity.CellPhone = Convert.IsDBNull(dataRow["CellPhone"]) ? null : (System.String)dataRow["CellPhone"];
			entity.Email = Convert.IsDBNull(dataRow["Email"]) ? null : (System.String)dataRow["Email"];
			entity.Avatar = Convert.IsDBNull(dataRow["Avatar"]) ? null : (System.String)dataRow["Avatar"];
			entity.Note = Convert.IsDBNull(dataRow["Note"]) ? null : (System.String)dataRow["Note"];
			entity.UserGroupId = (System.String)dataRow["UserGroupId"];
			entity.ServicesId = Convert.IsDBNull(dataRow["ServicesId"]) ? null : (System.Int32?)dataRow["ServicesId"];
			entity.IsFemale = (System.Boolean)dataRow["IsFemale"];
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
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.Users"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">AppointmentSystem.Entities.Users Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, AppointmentSystem.Entities.Users entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region ServicesIdSource	
			if (CanDeepLoad(entity, "Services|ServicesIdSource", deepLoadType, innerList) 
				&& entity.ServicesIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.ServicesId ?? (int)0);
				Services tmpEntity = EntityManager.LocateEntity<Services>(EntityLocator.ConstructKeyFromPkItems(typeof(Services), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.ServicesIdSource = tmpEntity;
				else
					entity.ServicesIdSource = DataRepository.ServicesProvider.GetById(transactionManager, (entity.ServicesId ?? (int)0));		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'ServicesIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.ServicesIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.ServicesProvider.DeepLoad(transactionManager, entity.ServicesIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion ServicesIdSource

			#region UserGroupIdSource	
			if (CanDeepLoad(entity, "UserGroup|UserGroupIdSource", deepLoadType, innerList) 
				&& entity.UserGroupIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = entity.UserGroupId;
				UserGroup tmpEntity = EntityManager.LocateEntity<UserGroup>(EntityLocator.ConstructKeyFromPkItems(typeof(UserGroup), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.UserGroupIdSource = tmpEntity;
				else
					entity.UserGroupIdSource = DataRepository.UserGroupProvider.GetById(transactionManager, entity.UserGroupId);		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'UserGroupIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.UserGroupIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.UserGroupProvider.DeepLoad(transactionManager, entity.UserGroupIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion UserGroupIdSource
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetByUsername methods when available
			
			#region UserRoleCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<UserRole>|UserRoleCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'UserRoleCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.UserRoleCollection = DataRepository.UserRoleProvider.GetByUsername(transactionManager, entity.Username);

				if (deep && entity.UserRoleCollection.Count > 0)
				{
					deepHandles.Add("UserRoleCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<UserRole>) DataRepository.UserRoleProvider.DeepLoad,
						new object[] { transactionManager, entity.UserRoleCollection, deep, deepLoadType, childTypes, innerList }
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

				entity.DoctorRoomCollection = DataRepository.DoctorRoomProvider.GetByUsername(transactionManager, entity.Username);

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

				entity.AppointmentCollection = DataRepository.AppointmentProvider.GetByUsername(transactionManager, entity.Username);

				if (deep && entity.AppointmentCollection.Count > 0)
				{
					deepHandles.Add("AppointmentCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<Appointment>) DataRepository.AppointmentProvider.DeepLoad,
						new object[] { transactionManager, entity.AppointmentCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			#region RosterCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<Roster>|RosterCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'RosterCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.RosterCollection = DataRepository.RosterProvider.GetByUsername(transactionManager, entity.Username);

				if (deep && entity.RosterCollection.Count > 0)
				{
					deepHandles.Add("RosterCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<Roster>) DataRepository.RosterProvider.DeepLoad,
						new object[] { transactionManager, entity.RosterCollection, deep, deepLoadType, childTypes, innerList }
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
		/// Deep Save the entire object graph of the AppointmentSystem.Entities.Users object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">AppointmentSystem.Entities.Users instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">AppointmentSystem.Entities.Users Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, AppointmentSystem.Entities.Users entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			
			#region ServicesIdSource
			if (CanDeepSave(entity, "Services|ServicesIdSource", deepSaveType, innerList) 
				&& entity.ServicesIdSource != null)
			{
				DataRepository.ServicesProvider.Save(transactionManager, entity.ServicesIdSource);
				entity.ServicesId = entity.ServicesIdSource.Id;
			}
			#endregion 
			
			#region UserGroupIdSource
			if (CanDeepSave(entity, "UserGroup|UserGroupIdSource", deepSaveType, innerList) 
				&& entity.UserGroupIdSource != null)
			{
				DataRepository.UserGroupProvider.Save(transactionManager, entity.UserGroupIdSource);
				entity.UserGroupId = entity.UserGroupIdSource.Id;
			}
			#endregion 
			#endregion Composite Parent Properties

			// Save Root Entity through Provider
			if (!entity.IsDeleted)
				this.Save(transactionManager, entity);
			
			//used to hold DeepSave method delegates and fire after all the local children have been saved.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
	
			#region List<UserRole>
				if (CanDeepSave(entity.UserRoleCollection, "List<UserRole>|UserRoleCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(UserRole child in entity.UserRoleCollection)
					{
						if(child.UsernameSource != null)
						{
							child.Username = child.UsernameSource.Username;
						}
						else
						{
							child.Username = entity.Username;
						}

					}

					if (entity.UserRoleCollection.Count > 0 || entity.UserRoleCollection.DeletedItems.Count > 0)
					{
						//DataRepository.UserRoleProvider.Save(transactionManager, entity.UserRoleCollection);
						
						deepHandles.Add("UserRoleCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< UserRole >) DataRepository.UserRoleProvider.DeepSave,
							new object[] { transactionManager, entity.UserRoleCollection, deepSaveType, childTypes, innerList }
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
						if(child.UsernameSource != null)
						{
							child.Username = child.UsernameSource.Username;
						}
						else
						{
							child.Username = entity.Username;
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
						if(child.UsernameSource != null)
						{
							child.Username = child.UsernameSource.Username;
						}
						else
						{
							child.Username = entity.Username;
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
				
	
			#region List<Roster>
				if (CanDeepSave(entity.RosterCollection, "List<Roster>|RosterCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(Roster child in entity.RosterCollection)
					{
						if(child.UsernameSource != null)
						{
							child.Username = child.UsernameSource.Username;
						}
						else
						{
							child.Username = entity.Username;
						}

					}

					if (entity.RosterCollection.Count > 0 || entity.RosterCollection.DeletedItems.Count > 0)
					{
						//DataRepository.RosterProvider.Save(transactionManager, entity.RosterCollection);
						
						deepHandles.Add("RosterCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< Roster >) DataRepository.RosterProvider.DeepSave,
							new object[] { transactionManager, entity.RosterCollection, deepSaveType, childTypes, innerList }
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
	
	#region UsersChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>AppointmentSystem.Entities.Users</c>
	///</summary>
	public enum UsersChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Services</c> at ServicesIdSource
		///</summary>
		[ChildEntityType(typeof(Services))]
		Services,
			
		///<summary>
		/// Composite Property for <c>UserGroup</c> at UserGroupIdSource
		///</summary>
		[ChildEntityType(typeof(UserGroup))]
		UserGroup,
	
		///<summary>
		/// Collection of <c>Users</c> as OneToMany for UserRoleCollection
		///</summary>
		[ChildEntityType(typeof(TList<UserRole>))]
		UserRoleCollection,

		///<summary>
		/// Collection of <c>Users</c> as OneToMany for DoctorRoomCollection
		///</summary>
		[ChildEntityType(typeof(TList<DoctorRoom>))]
		DoctorRoomCollection,

		///<summary>
		/// Collection of <c>Users</c> as OneToMany for AppointmentCollection
		///</summary>
		[ChildEntityType(typeof(TList<Appointment>))]
		AppointmentCollection,

		///<summary>
		/// Collection of <c>Users</c> as OneToMany for RosterCollection
		///</summary>
		[ChildEntityType(typeof(TList<Roster>))]
		RosterCollection,
	}
	
	#endregion UsersChildEntityTypes
	
	#region UsersFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;UsersColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Users"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class UsersFilterBuilder : SqlFilterBuilder<UsersColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the UsersFilterBuilder class.
		/// </summary>
		public UsersFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the UsersFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public UsersFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the UsersFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public UsersFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion UsersFilterBuilder
	
	#region UsersParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;UsersColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Users"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class UsersParameterBuilder : ParameterizedSqlFilterBuilder<UsersColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the UsersParameterBuilder class.
		/// </summary>
		public UsersParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the UsersParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public UsersParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the UsersParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public UsersParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion UsersParameterBuilder
	
	#region UsersSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;UsersColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Users"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class UsersSortBuilder : SqlSortBuilder<UsersColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the UsersSqlSortBuilder class.
		/// </summary>
		public UsersSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion UsersSortBuilder
	
} // end namespace
