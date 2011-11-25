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
	/// This class is the base class for any <see cref="GroupRolesProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class GroupRolesProviderBaseCore : EntityProviderBase<ClinicDoctor.Entities.GroupRoles, ClinicDoctor.Entities.GroupRolesKey>
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
		public override bool Delete(TransactionManager transactionManager, ClinicDoctor.Entities.GroupRolesKey key)
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
		public override ClinicDoctor.Entities.GroupRoles Get(TransactionManager transactionManager, ClinicDoctor.Entities.GroupRolesKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_GroupRoles_GroupId index.
		/// </summary>
		/// <param name="_groupId"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public TList<GroupRoles> GetByGroupId(System.Int32? _groupId)
		{
			int count = -1;
			return GetByGroupId(null,_groupId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_GroupRoles_GroupId index.
		/// </summary>
		/// <param name="_groupId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public TList<GroupRoles> GetByGroupId(System.Int32? _groupId, int start, int pageLength)
		{
			int count = -1;
			return GetByGroupId(null, _groupId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_GroupRoles_GroupId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_groupId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public TList<GroupRoles> GetByGroupId(TransactionManager transactionManager, System.Int32? _groupId)
		{
			int count = -1;
			return GetByGroupId(transactionManager, _groupId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_GroupRoles_GroupId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_groupId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public TList<GroupRoles> GetByGroupId(TransactionManager transactionManager, System.Int32? _groupId, int start, int pageLength)
		{
			int count = -1;
			return GetByGroupId(transactionManager, _groupId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_GroupRoles_GroupId index.
		/// </summary>
		/// <param name="_groupId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public TList<GroupRoles> GetByGroupId(System.Int32? _groupId, int start, int pageLength, out int count)
		{
			return GetByGroupId(null, _groupId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_GroupRoles_GroupId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_groupId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public abstract TList<GroupRoles> GetByGroupId(TransactionManager transactionManager, System.Int32? _groupId, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_GroupRoles_GroupId_IsDisabled index.
		/// </summary>
		/// <param name="_groupId"></param>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public TList<GroupRoles> GetByGroupIdIsDisabled(System.Int32? _groupId, System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByGroupIdIsDisabled(null,_groupId, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_GroupRoles_GroupId_IsDisabled index.
		/// </summary>
		/// <param name="_groupId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public TList<GroupRoles> GetByGroupIdIsDisabled(System.Int32? _groupId, System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByGroupIdIsDisabled(null, _groupId, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_GroupRoles_GroupId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_groupId"></param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public TList<GroupRoles> GetByGroupIdIsDisabled(TransactionManager transactionManager, System.Int32? _groupId, System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByGroupIdIsDisabled(transactionManager, _groupId, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_GroupRoles_GroupId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_groupId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public TList<GroupRoles> GetByGroupIdIsDisabled(TransactionManager transactionManager, System.Int32? _groupId, System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByGroupIdIsDisabled(transactionManager, _groupId, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_GroupRoles_GroupId_IsDisabled index.
		/// </summary>
		/// <param name="_groupId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public TList<GroupRoles> GetByGroupIdIsDisabled(System.Int32? _groupId, System.Boolean? _isDisabled, int start, int pageLength, out int count)
		{
			return GetByGroupIdIsDisabled(null, _groupId, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_GroupRoles_GroupId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_groupId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public abstract TList<GroupRoles> GetByGroupIdIsDisabled(TransactionManager transactionManager, System.Int32? _groupId, System.Boolean? _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_GroupRoles_GroupId_RoleId index.
		/// </summary>
		/// <param name="_groupId"></param>
		/// <param name="_roleId"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public TList<GroupRoles> GetByGroupIdRoleId(System.Int32? _groupId, System.Int32? _roleId)
		{
			int count = -1;
			return GetByGroupIdRoleId(null,_groupId, _roleId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_GroupRoles_GroupId_RoleId index.
		/// </summary>
		/// <param name="_groupId"></param>
		/// <param name="_roleId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public TList<GroupRoles> GetByGroupIdRoleId(System.Int32? _groupId, System.Int32? _roleId, int start, int pageLength)
		{
			int count = -1;
			return GetByGroupIdRoleId(null, _groupId, _roleId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_GroupRoles_GroupId_RoleId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_groupId"></param>
		/// <param name="_roleId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public TList<GroupRoles> GetByGroupIdRoleId(TransactionManager transactionManager, System.Int32? _groupId, System.Int32? _roleId)
		{
			int count = -1;
			return GetByGroupIdRoleId(transactionManager, _groupId, _roleId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_GroupRoles_GroupId_RoleId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_groupId"></param>
		/// <param name="_roleId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public TList<GroupRoles> GetByGroupIdRoleId(TransactionManager transactionManager, System.Int32? _groupId, System.Int32? _roleId, int start, int pageLength)
		{
			int count = -1;
			return GetByGroupIdRoleId(transactionManager, _groupId, _roleId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_GroupRoles_GroupId_RoleId index.
		/// </summary>
		/// <param name="_groupId"></param>
		/// <param name="_roleId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public TList<GroupRoles> GetByGroupIdRoleId(System.Int32? _groupId, System.Int32? _roleId, int start, int pageLength, out int count)
		{
			return GetByGroupIdRoleId(null, _groupId, _roleId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_GroupRoles_GroupId_RoleId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_groupId"></param>
		/// <param name="_roleId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public abstract TList<GroupRoles> GetByGroupIdRoleId(TransactionManager transactionManager, System.Int32? _groupId, System.Int32? _roleId, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_GroupRoles_GroupId_RoleId_IsDisabled index.
		/// </summary>
		/// <param name="_groupId"></param>
		/// <param name="_roleId"></param>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public TList<GroupRoles> GetByGroupIdRoleIdIsDisabled(System.Int32? _groupId, System.Int32? _roleId, System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByGroupIdRoleIdIsDisabled(null,_groupId, _roleId, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_GroupRoles_GroupId_RoleId_IsDisabled index.
		/// </summary>
		/// <param name="_groupId"></param>
		/// <param name="_roleId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public TList<GroupRoles> GetByGroupIdRoleIdIsDisabled(System.Int32? _groupId, System.Int32? _roleId, System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByGroupIdRoleIdIsDisabled(null, _groupId, _roleId, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_GroupRoles_GroupId_RoleId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_groupId"></param>
		/// <param name="_roleId"></param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public TList<GroupRoles> GetByGroupIdRoleIdIsDisabled(TransactionManager transactionManager, System.Int32? _groupId, System.Int32? _roleId, System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByGroupIdRoleIdIsDisabled(transactionManager, _groupId, _roleId, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_GroupRoles_GroupId_RoleId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_groupId"></param>
		/// <param name="_roleId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public TList<GroupRoles> GetByGroupIdRoleIdIsDisabled(TransactionManager transactionManager, System.Int32? _groupId, System.Int32? _roleId, System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByGroupIdRoleIdIsDisabled(transactionManager, _groupId, _roleId, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_GroupRoles_GroupId_RoleId_IsDisabled index.
		/// </summary>
		/// <param name="_groupId"></param>
		/// <param name="_roleId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public TList<GroupRoles> GetByGroupIdRoleIdIsDisabled(System.Int32? _groupId, System.Int32? _roleId, System.Boolean? _isDisabled, int start, int pageLength, out int count)
		{
			return GetByGroupIdRoleIdIsDisabled(null, _groupId, _roleId, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_GroupRoles_GroupId_RoleId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_groupId"></param>
		/// <param name="_roleId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public abstract TList<GroupRoles> GetByGroupIdRoleIdIsDisabled(TransactionManager transactionManager, System.Int32? _groupId, System.Int32? _roleId, System.Boolean? _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_GroupRoles_Id_IsDisabled index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public TList<GroupRoles> GetByIdIsDisabled(System.Int32 _id, System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByIdIsDisabled(null,_id, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_GroupRoles_Id_IsDisabled index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public TList<GroupRoles> GetByIdIsDisabled(System.Int32 _id, System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIdIsDisabled(null, _id, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_GroupRoles_Id_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public TList<GroupRoles> GetByIdIsDisabled(TransactionManager transactionManager, System.Int32 _id, System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByIdIsDisabled(transactionManager, _id, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_GroupRoles_Id_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public TList<GroupRoles> GetByIdIsDisabled(TransactionManager transactionManager, System.Int32 _id, System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIdIsDisabled(transactionManager, _id, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_GroupRoles_Id_IsDisabled index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public TList<GroupRoles> GetByIdIsDisabled(System.Int32 _id, System.Boolean? _isDisabled, int start, int pageLength, out int count)
		{
			return GetByIdIsDisabled(null, _id, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_GroupRoles_Id_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public abstract TList<GroupRoles> GetByIdIsDisabled(TransactionManager transactionManager, System.Int32 _id, System.Boolean? _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_GroupRoles_IsDisabled index.
		/// </summary>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public TList<GroupRoles> GetByIsDisabled(System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByIsDisabled(null,_isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_GroupRoles_IsDisabled index.
		/// </summary>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public TList<GroupRoles> GetByIsDisabled(System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIsDisabled(null, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_GroupRoles_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public TList<GroupRoles> GetByIsDisabled(TransactionManager transactionManager, System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByIsDisabled(transactionManager, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_GroupRoles_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public TList<GroupRoles> GetByIsDisabled(TransactionManager transactionManager, System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIsDisabled(transactionManager, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_GroupRoles_IsDisabled index.
		/// </summary>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public TList<GroupRoles> GetByIsDisabled(System.Boolean? _isDisabled, int start, int pageLength, out int count)
		{
			return GetByIsDisabled(null, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_GroupRoles_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public abstract TList<GroupRoles> GetByIsDisabled(TransactionManager transactionManager, System.Boolean? _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_GroupRoles_RoleId index.
		/// </summary>
		/// <param name="_roleId"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public TList<GroupRoles> GetByRoleId(System.Int32? _roleId)
		{
			int count = -1;
			return GetByRoleId(null,_roleId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_GroupRoles_RoleId index.
		/// </summary>
		/// <param name="_roleId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public TList<GroupRoles> GetByRoleId(System.Int32? _roleId, int start, int pageLength)
		{
			int count = -1;
			return GetByRoleId(null, _roleId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_GroupRoles_RoleId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_roleId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public TList<GroupRoles> GetByRoleId(TransactionManager transactionManager, System.Int32? _roleId)
		{
			int count = -1;
			return GetByRoleId(transactionManager, _roleId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_GroupRoles_RoleId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_roleId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public TList<GroupRoles> GetByRoleId(TransactionManager transactionManager, System.Int32? _roleId, int start, int pageLength)
		{
			int count = -1;
			return GetByRoleId(transactionManager, _roleId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_GroupRoles_RoleId index.
		/// </summary>
		/// <param name="_roleId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public TList<GroupRoles> GetByRoleId(System.Int32? _roleId, int start, int pageLength, out int count)
		{
			return GetByRoleId(null, _roleId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_GroupRoles_RoleId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_roleId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public abstract TList<GroupRoles> GetByRoleId(TransactionManager transactionManager, System.Int32? _roleId, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_GroupRoles_RoleId_IsDisabled index.
		/// </summary>
		/// <param name="_roleId"></param>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public TList<GroupRoles> GetByRoleIdIsDisabled(System.Int32? _roleId, System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByRoleIdIsDisabled(null,_roleId, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_GroupRoles_RoleId_IsDisabled index.
		/// </summary>
		/// <param name="_roleId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public TList<GroupRoles> GetByRoleIdIsDisabled(System.Int32? _roleId, System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByRoleIdIsDisabled(null, _roleId, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_GroupRoles_RoleId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_roleId"></param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public TList<GroupRoles> GetByRoleIdIsDisabled(TransactionManager transactionManager, System.Int32? _roleId, System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByRoleIdIsDisabled(transactionManager, _roleId, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_GroupRoles_RoleId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_roleId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public TList<GroupRoles> GetByRoleIdIsDisabled(TransactionManager transactionManager, System.Int32? _roleId, System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByRoleIdIsDisabled(transactionManager, _roleId, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_GroupRoles_RoleId_IsDisabled index.
		/// </summary>
		/// <param name="_roleId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public TList<GroupRoles> GetByRoleIdIsDisabled(System.Int32? _roleId, System.Boolean? _isDisabled, int start, int pageLength, out int count)
		{
			return GetByRoleIdIsDisabled(null, _roleId, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_GroupRoles_RoleId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_roleId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;GroupRoles&gt;"/> class.</returns>
		public abstract TList<GroupRoles> GetByRoleIdIsDisabled(TransactionManager transactionManager, System.Int32? _roleId, System.Boolean? _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_GroupRoles index.
		/// </summary>
		/// <param name="_id"></param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.GroupRoles"/> class.</returns>
		public ClinicDoctor.Entities.GroupRoles GetById(System.Int32 _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_GroupRoles index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.GroupRoles"/> class.</returns>
		public ClinicDoctor.Entities.GroupRoles GetById(System.Int32 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_GroupRoles index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.GroupRoles"/> class.</returns>
		public ClinicDoctor.Entities.GroupRoles GetById(TransactionManager transactionManager, System.Int32 _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_GroupRoles index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.GroupRoles"/> class.</returns>
		public ClinicDoctor.Entities.GroupRoles GetById(TransactionManager transactionManager, System.Int32 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_GroupRoles index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.GroupRoles"/> class.</returns>
		public ClinicDoctor.Entities.GroupRoles GetById(System.Int32 _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_GroupRoles index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.GroupRoles"/> class.</returns>
		public abstract ClinicDoctor.Entities.GroupRoles GetById(TransactionManager transactionManager, System.Int32 _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;GroupRoles&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;GroupRoles&gt;"/></returns>
		public static TList<GroupRoles> Fill(IDataReader reader, TList<GroupRoles> rows, int start, int pageLength)
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
				
				ClinicDoctor.Entities.GroupRoles c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("GroupRoles")
					.Append("|").Append((System.Int32)reader[((int)GroupRolesColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<GroupRoles>(
					key.ToString(), // EntityTrackingKey
					"GroupRoles",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new ClinicDoctor.Entities.GroupRoles();
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
					c.Id = (System.Int32)reader[((int)GroupRolesColumn.Id - 1)];
					c.GroupId = (reader.IsDBNull(((int)GroupRolesColumn.GroupId - 1)))?null:(System.Int32?)reader[((int)GroupRolesColumn.GroupId - 1)];
					c.RoleId = (reader.IsDBNull(((int)GroupRolesColumn.RoleId - 1)))?null:(System.Int32?)reader[((int)GroupRolesColumn.RoleId - 1)];
					c.IsDisabled = (reader.IsDBNull(((int)GroupRolesColumn.IsDisabled - 1)))?null:(System.Boolean?)reader[((int)GroupRolesColumn.IsDisabled - 1)];
					c.CreateUser = (reader.IsDBNull(((int)GroupRolesColumn.CreateUser - 1)))?null:(System.String)reader[((int)GroupRolesColumn.CreateUser - 1)];
					c.CreateDate = (reader.IsDBNull(((int)GroupRolesColumn.CreateDate - 1)))?null:(System.DateTime?)reader[((int)GroupRolesColumn.CreateDate - 1)];
					c.UpdateUser = (reader.IsDBNull(((int)GroupRolesColumn.UpdateUser - 1)))?null:(System.String)reader[((int)GroupRolesColumn.UpdateUser - 1)];
					c.UpdateDate = (reader.IsDBNull(((int)GroupRolesColumn.UpdateDate - 1)))?null:(System.DateTime?)reader[((int)GroupRolesColumn.UpdateDate - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="ClinicDoctor.Entities.GroupRoles"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="ClinicDoctor.Entities.GroupRoles"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, ClinicDoctor.Entities.GroupRoles entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (System.Int32)reader[((int)GroupRolesColumn.Id - 1)];
			entity.GroupId = (reader.IsDBNull(((int)GroupRolesColumn.GroupId - 1)))?null:(System.Int32?)reader[((int)GroupRolesColumn.GroupId - 1)];
			entity.RoleId = (reader.IsDBNull(((int)GroupRolesColumn.RoleId - 1)))?null:(System.Int32?)reader[((int)GroupRolesColumn.RoleId - 1)];
			entity.IsDisabled = (reader.IsDBNull(((int)GroupRolesColumn.IsDisabled - 1)))?null:(System.Boolean?)reader[((int)GroupRolesColumn.IsDisabled - 1)];
			entity.CreateUser = (reader.IsDBNull(((int)GroupRolesColumn.CreateUser - 1)))?null:(System.String)reader[((int)GroupRolesColumn.CreateUser - 1)];
			entity.CreateDate = (reader.IsDBNull(((int)GroupRolesColumn.CreateDate - 1)))?null:(System.DateTime?)reader[((int)GroupRolesColumn.CreateDate - 1)];
			entity.UpdateUser = (reader.IsDBNull(((int)GroupRolesColumn.UpdateUser - 1)))?null:(System.String)reader[((int)GroupRolesColumn.UpdateUser - 1)];
			entity.UpdateDate = (reader.IsDBNull(((int)GroupRolesColumn.UpdateDate - 1)))?null:(System.DateTime?)reader[((int)GroupRolesColumn.UpdateDate - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="ClinicDoctor.Entities.GroupRoles"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="ClinicDoctor.Entities.GroupRoles"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, ClinicDoctor.Entities.GroupRoles entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Id = (System.Int32)dataRow["Id"];
			entity.GroupId = Convert.IsDBNull(dataRow["GroupId"]) ? null : (System.Int32?)dataRow["GroupId"];
			entity.RoleId = Convert.IsDBNull(dataRow["RoleId"]) ? null : (System.Int32?)dataRow["RoleId"];
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
		/// <param name="entity">The <see cref="ClinicDoctor.Entities.GroupRoles"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">ClinicDoctor.Entities.GroupRoles Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, ClinicDoctor.Entities.GroupRoles entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region GroupIdSource	
			if (CanDeepLoad(entity, "Group|GroupIdSource", deepLoadType, innerList) 
				&& entity.GroupIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.GroupId ?? (int)0);
				Group tmpEntity = EntityManager.LocateEntity<Group>(EntityLocator.ConstructKeyFromPkItems(typeof(Group), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.GroupIdSource = tmpEntity;
				else
					entity.GroupIdSource = DataRepository.GroupProvider.GetById(transactionManager, (entity.GroupId ?? (int)0));		
				
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
		/// Deep Save the entire object graph of the ClinicDoctor.Entities.GroupRoles object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">ClinicDoctor.Entities.GroupRoles instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">ClinicDoctor.Entities.GroupRoles Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, ClinicDoctor.Entities.GroupRoles entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
			
			#region RoleIdSource
			if (CanDeepSave(entity, "Role|RoleIdSource", deepSaveType, innerList) 
				&& entity.RoleIdSource != null)
			{
				DataRepository.RoleProvider.Save(transactionManager, entity.RoleIdSource);
				entity.RoleId = entity.RoleIdSource.Id;
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
	
	#region GroupRolesChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>ClinicDoctor.Entities.GroupRoles</c>
	///</summary>
	public enum GroupRolesChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Group</c> at GroupIdSource
		///</summary>
		[ChildEntityType(typeof(Group))]
		Group,
			
		///<summary>
		/// Composite Property for <c>Role</c> at RoleIdSource
		///</summary>
		[ChildEntityType(typeof(Role))]
		Role,
		}
	
	#endregion GroupRolesChildEntityTypes
	
	#region GroupRolesFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;GroupRolesColumn&gt;"/> class
	/// that is used exclusively with a <see cref="GroupRoles"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class GroupRolesFilterBuilder : SqlFilterBuilder<GroupRolesColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the GroupRolesFilterBuilder class.
		/// </summary>
		public GroupRolesFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the GroupRolesFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public GroupRolesFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the GroupRolesFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public GroupRolesFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion GroupRolesFilterBuilder
	
	#region GroupRolesParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;GroupRolesColumn&gt;"/> class
	/// that is used exclusively with a <see cref="GroupRoles"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class GroupRolesParameterBuilder : ParameterizedSqlFilterBuilder<GroupRolesColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the GroupRolesParameterBuilder class.
		/// </summary>
		public GroupRolesParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the GroupRolesParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public GroupRolesParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the GroupRolesParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public GroupRolesParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion GroupRolesParameterBuilder
	
	#region GroupRolesSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;GroupRolesColumn&gt;"/> class
	/// that is used exclusively with a <see cref="GroupRoles"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class GroupRolesSortBuilder : SqlSortBuilder<GroupRolesColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the GroupRolesSqlSortBuilder class.
		/// </summary>
		public GroupRolesSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion GroupRolesSortBuilder
	
} // end namespace
