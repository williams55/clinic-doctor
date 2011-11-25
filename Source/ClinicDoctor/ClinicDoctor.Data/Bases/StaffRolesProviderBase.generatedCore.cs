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
	/// This class is the base class for any <see cref="StaffRolesProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class StaffRolesProviderBaseCore : EntityProviderBase<ClinicDoctor.Entities.StaffRoles, ClinicDoctor.Entities.StaffRolesKey>
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
		public override bool Delete(TransactionManager transactionManager, ClinicDoctor.Entities.StaffRolesKey key)
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
		public override ClinicDoctor.Entities.StaffRoles Get(TransactionManager transactionManager, ClinicDoctor.Entities.StaffRolesKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_StaffRoles_Id_IsDisabled index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public TList<StaffRoles> GetByIdIsDisabled(System.Int32 _id, System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByIdIsDisabled(null,_id, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_StaffRoles_Id_IsDisabled index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public TList<StaffRoles> GetByIdIsDisabled(System.Int32 _id, System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIdIsDisabled(null, _id, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_StaffRoles_Id_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public TList<StaffRoles> GetByIdIsDisabled(TransactionManager transactionManager, System.Int32 _id, System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByIdIsDisabled(transactionManager, _id, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_StaffRoles_Id_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public TList<StaffRoles> GetByIdIsDisabled(TransactionManager transactionManager, System.Int32 _id, System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIdIsDisabled(transactionManager, _id, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_StaffRoles_Id_IsDisabled index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public TList<StaffRoles> GetByIdIsDisabled(System.Int32 _id, System.Boolean? _isDisabled, int start, int pageLength, out int count)
		{
			return GetByIdIsDisabled(null, _id, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_StaffRoles_Id_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public abstract TList<StaffRoles> GetByIdIsDisabled(TransactionManager transactionManager, System.Int32 _id, System.Boolean? _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_StaffRoles_IsDisabled index.
		/// </summary>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public TList<StaffRoles> GetByIsDisabled(System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByIsDisabled(null,_isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_StaffRoles_IsDisabled index.
		/// </summary>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public TList<StaffRoles> GetByIsDisabled(System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIsDisabled(null, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_StaffRoles_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public TList<StaffRoles> GetByIsDisabled(TransactionManager transactionManager, System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByIsDisabled(transactionManager, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_StaffRoles_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public TList<StaffRoles> GetByIsDisabled(TransactionManager transactionManager, System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIsDisabled(transactionManager, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_StaffRoles_IsDisabled index.
		/// </summary>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public TList<StaffRoles> GetByIsDisabled(System.Boolean? _isDisabled, int start, int pageLength, out int count)
		{
			return GetByIsDisabled(null, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_StaffRoles_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public abstract TList<StaffRoles> GetByIsDisabled(TransactionManager transactionManager, System.Boolean? _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_StaffRoles_RoleId index.
		/// </summary>
		/// <param name="_roleId"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public TList<StaffRoles> GetByRoleId(System.Int32? _roleId)
		{
			int count = -1;
			return GetByRoleId(null,_roleId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_StaffRoles_RoleId index.
		/// </summary>
		/// <param name="_roleId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public TList<StaffRoles> GetByRoleId(System.Int32? _roleId, int start, int pageLength)
		{
			int count = -1;
			return GetByRoleId(null, _roleId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_StaffRoles_RoleId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_roleId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public TList<StaffRoles> GetByRoleId(TransactionManager transactionManager, System.Int32? _roleId)
		{
			int count = -1;
			return GetByRoleId(transactionManager, _roleId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_StaffRoles_RoleId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_roleId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public TList<StaffRoles> GetByRoleId(TransactionManager transactionManager, System.Int32? _roleId, int start, int pageLength)
		{
			int count = -1;
			return GetByRoleId(transactionManager, _roleId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_StaffRoles_RoleId index.
		/// </summary>
		/// <param name="_roleId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public TList<StaffRoles> GetByRoleId(System.Int32? _roleId, int start, int pageLength, out int count)
		{
			return GetByRoleId(null, _roleId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_StaffRoles_RoleId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_roleId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public abstract TList<StaffRoles> GetByRoleId(TransactionManager transactionManager, System.Int32? _roleId, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_StaffRoles_RoleId_IsDisabled index.
		/// </summary>
		/// <param name="_roleId"></param>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public TList<StaffRoles> GetByRoleIdIsDisabled(System.Int32? _roleId, System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByRoleIdIsDisabled(null,_roleId, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_StaffRoles_RoleId_IsDisabled index.
		/// </summary>
		/// <param name="_roleId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public TList<StaffRoles> GetByRoleIdIsDisabled(System.Int32? _roleId, System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByRoleIdIsDisabled(null, _roleId, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_StaffRoles_RoleId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_roleId"></param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public TList<StaffRoles> GetByRoleIdIsDisabled(TransactionManager transactionManager, System.Int32? _roleId, System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByRoleIdIsDisabled(transactionManager, _roleId, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_StaffRoles_RoleId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_roleId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public TList<StaffRoles> GetByRoleIdIsDisabled(TransactionManager transactionManager, System.Int32? _roleId, System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByRoleIdIsDisabled(transactionManager, _roleId, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_StaffRoles_RoleId_IsDisabled index.
		/// </summary>
		/// <param name="_roleId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public TList<StaffRoles> GetByRoleIdIsDisabled(System.Int32? _roleId, System.Boolean? _isDisabled, int start, int pageLength, out int count)
		{
			return GetByRoleIdIsDisabled(null, _roleId, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_StaffRoles_RoleId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_roleId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public abstract TList<StaffRoles> GetByRoleIdIsDisabled(TransactionManager transactionManager, System.Int32? _roleId, System.Boolean? _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_StaffRoles_StaffId index.
		/// </summary>
		/// <param name="_staffId"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public TList<StaffRoles> GetByStaffId(System.Int32? _staffId)
		{
			int count = -1;
			return GetByStaffId(null,_staffId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_StaffRoles_StaffId index.
		/// </summary>
		/// <param name="_staffId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public TList<StaffRoles> GetByStaffId(System.Int32? _staffId, int start, int pageLength)
		{
			int count = -1;
			return GetByStaffId(null, _staffId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_StaffRoles_StaffId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_staffId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public TList<StaffRoles> GetByStaffId(TransactionManager transactionManager, System.Int32? _staffId)
		{
			int count = -1;
			return GetByStaffId(transactionManager, _staffId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_StaffRoles_StaffId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_staffId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public TList<StaffRoles> GetByStaffId(TransactionManager transactionManager, System.Int32? _staffId, int start, int pageLength)
		{
			int count = -1;
			return GetByStaffId(transactionManager, _staffId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_StaffRoles_StaffId index.
		/// </summary>
		/// <param name="_staffId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public TList<StaffRoles> GetByStaffId(System.Int32? _staffId, int start, int pageLength, out int count)
		{
			return GetByStaffId(null, _staffId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_StaffRoles_StaffId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_staffId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public abstract TList<StaffRoles> GetByStaffId(TransactionManager transactionManager, System.Int32? _staffId, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_StaffRoles_StaffId_IsDisabled index.
		/// </summary>
		/// <param name="_staffId"></param>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public TList<StaffRoles> GetByStaffIdIsDisabled(System.Int32? _staffId, System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByStaffIdIsDisabled(null,_staffId, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_StaffRoles_StaffId_IsDisabled index.
		/// </summary>
		/// <param name="_staffId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public TList<StaffRoles> GetByStaffIdIsDisabled(System.Int32? _staffId, System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByStaffIdIsDisabled(null, _staffId, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_StaffRoles_StaffId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_staffId"></param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public TList<StaffRoles> GetByStaffIdIsDisabled(TransactionManager transactionManager, System.Int32? _staffId, System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByStaffIdIsDisabled(transactionManager, _staffId, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_StaffRoles_StaffId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_staffId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public TList<StaffRoles> GetByStaffIdIsDisabled(TransactionManager transactionManager, System.Int32? _staffId, System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByStaffIdIsDisabled(transactionManager, _staffId, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_StaffRoles_StaffId_IsDisabled index.
		/// </summary>
		/// <param name="_staffId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public TList<StaffRoles> GetByStaffIdIsDisabled(System.Int32? _staffId, System.Boolean? _isDisabled, int start, int pageLength, out int count)
		{
			return GetByStaffIdIsDisabled(null, _staffId, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_StaffRoles_StaffId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_staffId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public abstract TList<StaffRoles> GetByStaffIdIsDisabled(TransactionManager transactionManager, System.Int32? _staffId, System.Boolean? _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_StaffRoles_StaffId_RoleId index.
		/// </summary>
		/// <param name="_staffId"></param>
		/// <param name="_roleId"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public TList<StaffRoles> GetByStaffIdRoleId(System.Int32? _staffId, System.Int32? _roleId)
		{
			int count = -1;
			return GetByStaffIdRoleId(null,_staffId, _roleId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_StaffRoles_StaffId_RoleId index.
		/// </summary>
		/// <param name="_staffId"></param>
		/// <param name="_roleId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public TList<StaffRoles> GetByStaffIdRoleId(System.Int32? _staffId, System.Int32? _roleId, int start, int pageLength)
		{
			int count = -1;
			return GetByStaffIdRoleId(null, _staffId, _roleId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_StaffRoles_StaffId_RoleId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_staffId"></param>
		/// <param name="_roleId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public TList<StaffRoles> GetByStaffIdRoleId(TransactionManager transactionManager, System.Int32? _staffId, System.Int32? _roleId)
		{
			int count = -1;
			return GetByStaffIdRoleId(transactionManager, _staffId, _roleId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_StaffRoles_StaffId_RoleId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_staffId"></param>
		/// <param name="_roleId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public TList<StaffRoles> GetByStaffIdRoleId(TransactionManager transactionManager, System.Int32? _staffId, System.Int32? _roleId, int start, int pageLength)
		{
			int count = -1;
			return GetByStaffIdRoleId(transactionManager, _staffId, _roleId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_StaffRoles_StaffId_RoleId index.
		/// </summary>
		/// <param name="_staffId"></param>
		/// <param name="_roleId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public TList<StaffRoles> GetByStaffIdRoleId(System.Int32? _staffId, System.Int32? _roleId, int start, int pageLength, out int count)
		{
			return GetByStaffIdRoleId(null, _staffId, _roleId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_StaffRoles_StaffId_RoleId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_staffId"></param>
		/// <param name="_roleId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public abstract TList<StaffRoles> GetByStaffIdRoleId(TransactionManager transactionManager, System.Int32? _staffId, System.Int32? _roleId, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_StaffRoles_StaffId_RoleId_IsDisabled index.
		/// </summary>
		/// <param name="_staffId"></param>
		/// <param name="_roleId"></param>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public TList<StaffRoles> GetByStaffIdRoleIdIsDisabled(System.Int32? _staffId, System.Int32? _roleId, System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByStaffIdRoleIdIsDisabled(null,_staffId, _roleId, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_StaffRoles_StaffId_RoleId_IsDisabled index.
		/// </summary>
		/// <param name="_staffId"></param>
		/// <param name="_roleId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public TList<StaffRoles> GetByStaffIdRoleIdIsDisabled(System.Int32? _staffId, System.Int32? _roleId, System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByStaffIdRoleIdIsDisabled(null, _staffId, _roleId, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_StaffRoles_StaffId_RoleId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_staffId"></param>
		/// <param name="_roleId"></param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public TList<StaffRoles> GetByStaffIdRoleIdIsDisabled(TransactionManager transactionManager, System.Int32? _staffId, System.Int32? _roleId, System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByStaffIdRoleIdIsDisabled(transactionManager, _staffId, _roleId, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_StaffRoles_StaffId_RoleId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_staffId"></param>
		/// <param name="_roleId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public TList<StaffRoles> GetByStaffIdRoleIdIsDisabled(TransactionManager transactionManager, System.Int32? _staffId, System.Int32? _roleId, System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByStaffIdRoleIdIsDisabled(transactionManager, _staffId, _roleId, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_StaffRoles_StaffId_RoleId_IsDisabled index.
		/// </summary>
		/// <param name="_staffId"></param>
		/// <param name="_roleId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public TList<StaffRoles> GetByStaffIdRoleIdIsDisabled(System.Int32? _staffId, System.Int32? _roleId, System.Boolean? _isDisabled, int start, int pageLength, out int count)
		{
			return GetByStaffIdRoleIdIsDisabled(null, _staffId, _roleId, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_StaffRoles_StaffId_RoleId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_staffId"></param>
		/// <param name="_roleId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;StaffRoles&gt;"/> class.</returns>
		public abstract TList<StaffRoles> GetByStaffIdRoleIdIsDisabled(TransactionManager transactionManager, System.Int32? _staffId, System.Int32? _roleId, System.Boolean? _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_StaffRoles index.
		/// </summary>
		/// <param name="_id"></param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.StaffRoles"/> class.</returns>
		public ClinicDoctor.Entities.StaffRoles GetById(System.Int32 _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_StaffRoles index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.StaffRoles"/> class.</returns>
		public ClinicDoctor.Entities.StaffRoles GetById(System.Int32 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_StaffRoles index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.StaffRoles"/> class.</returns>
		public ClinicDoctor.Entities.StaffRoles GetById(TransactionManager transactionManager, System.Int32 _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_StaffRoles index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.StaffRoles"/> class.</returns>
		public ClinicDoctor.Entities.StaffRoles GetById(TransactionManager transactionManager, System.Int32 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_StaffRoles index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.StaffRoles"/> class.</returns>
		public ClinicDoctor.Entities.StaffRoles GetById(System.Int32 _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_StaffRoles index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.StaffRoles"/> class.</returns>
		public abstract ClinicDoctor.Entities.StaffRoles GetById(TransactionManager transactionManager, System.Int32 _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;StaffRoles&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;StaffRoles&gt;"/></returns>
		public static TList<StaffRoles> Fill(IDataReader reader, TList<StaffRoles> rows, int start, int pageLength)
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
				
				ClinicDoctor.Entities.StaffRoles c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("StaffRoles")
					.Append("|").Append((System.Int32)reader[((int)StaffRolesColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<StaffRoles>(
					key.ToString(), // EntityTrackingKey
					"StaffRoles",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new ClinicDoctor.Entities.StaffRoles();
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
					c.Id = (System.Int32)reader[((int)StaffRolesColumn.Id - 1)];
					c.StaffId = (reader.IsDBNull(((int)StaffRolesColumn.StaffId - 1)))?null:(System.Int32?)reader[((int)StaffRolesColumn.StaffId - 1)];
					c.RoleId = (reader.IsDBNull(((int)StaffRolesColumn.RoleId - 1)))?null:(System.Int32?)reader[((int)StaffRolesColumn.RoleId - 1)];
					c.IsDisabled = (reader.IsDBNull(((int)StaffRolesColumn.IsDisabled - 1)))?null:(System.Boolean?)reader[((int)StaffRolesColumn.IsDisabled - 1)];
					c.CreateUser = (reader.IsDBNull(((int)StaffRolesColumn.CreateUser - 1)))?null:(System.String)reader[((int)StaffRolesColumn.CreateUser - 1)];
					c.CreateDate = (reader.IsDBNull(((int)StaffRolesColumn.CreateDate - 1)))?null:(System.DateTime?)reader[((int)StaffRolesColumn.CreateDate - 1)];
					c.UpdateUser = (reader.IsDBNull(((int)StaffRolesColumn.UpdateUser - 1)))?null:(System.String)reader[((int)StaffRolesColumn.UpdateUser - 1)];
					c.UpdateDate = (reader.IsDBNull(((int)StaffRolesColumn.UpdateDate - 1)))?null:(System.DateTime?)reader[((int)StaffRolesColumn.UpdateDate - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="ClinicDoctor.Entities.StaffRoles"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="ClinicDoctor.Entities.StaffRoles"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, ClinicDoctor.Entities.StaffRoles entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (System.Int32)reader[((int)StaffRolesColumn.Id - 1)];
			entity.StaffId = (reader.IsDBNull(((int)StaffRolesColumn.StaffId - 1)))?null:(System.Int32?)reader[((int)StaffRolesColumn.StaffId - 1)];
			entity.RoleId = (reader.IsDBNull(((int)StaffRolesColumn.RoleId - 1)))?null:(System.Int32?)reader[((int)StaffRolesColumn.RoleId - 1)];
			entity.IsDisabled = (reader.IsDBNull(((int)StaffRolesColumn.IsDisabled - 1)))?null:(System.Boolean?)reader[((int)StaffRolesColumn.IsDisabled - 1)];
			entity.CreateUser = (reader.IsDBNull(((int)StaffRolesColumn.CreateUser - 1)))?null:(System.String)reader[((int)StaffRolesColumn.CreateUser - 1)];
			entity.CreateDate = (reader.IsDBNull(((int)StaffRolesColumn.CreateDate - 1)))?null:(System.DateTime?)reader[((int)StaffRolesColumn.CreateDate - 1)];
			entity.UpdateUser = (reader.IsDBNull(((int)StaffRolesColumn.UpdateUser - 1)))?null:(System.String)reader[((int)StaffRolesColumn.UpdateUser - 1)];
			entity.UpdateDate = (reader.IsDBNull(((int)StaffRolesColumn.UpdateDate - 1)))?null:(System.DateTime?)reader[((int)StaffRolesColumn.UpdateDate - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="ClinicDoctor.Entities.StaffRoles"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="ClinicDoctor.Entities.StaffRoles"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, ClinicDoctor.Entities.StaffRoles entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Id = (System.Int32)dataRow["Id"];
			entity.StaffId = Convert.IsDBNull(dataRow["StaffId"]) ? null : (System.Int32?)dataRow["StaffId"];
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
		/// <param name="entity">The <see cref="ClinicDoctor.Entities.StaffRoles"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">ClinicDoctor.Entities.StaffRoles Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, ClinicDoctor.Entities.StaffRoles entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
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

			#region StaffIdSource	
			if (CanDeepLoad(entity, "Staff|StaffIdSource", deepLoadType, innerList) 
				&& entity.StaffIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.StaffId ?? (int)0);
				Staff tmpEntity = EntityManager.LocateEntity<Staff>(EntityLocator.ConstructKeyFromPkItems(typeof(Staff), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.StaffIdSource = tmpEntity;
				else
					entity.StaffIdSource = DataRepository.StaffProvider.GetById(transactionManager, (entity.StaffId ?? (int)0));		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'StaffIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.StaffIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.StaffProvider.DeepLoad(transactionManager, entity.StaffIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion StaffIdSource
			
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
		/// Deep Save the entire object graph of the ClinicDoctor.Entities.StaffRoles object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">ClinicDoctor.Entities.StaffRoles instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">ClinicDoctor.Entities.StaffRoles Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, ClinicDoctor.Entities.StaffRoles entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
			
			#region StaffIdSource
			if (CanDeepSave(entity, "Staff|StaffIdSource", deepSaveType, innerList) 
				&& entity.StaffIdSource != null)
			{
				DataRepository.StaffProvider.Save(transactionManager, entity.StaffIdSource);
				entity.StaffId = entity.StaffIdSource.Id;
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
	
	#region StaffRolesChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>ClinicDoctor.Entities.StaffRoles</c>
	///</summary>
	public enum StaffRolesChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Role</c> at RoleIdSource
		///</summary>
		[ChildEntityType(typeof(Role))]
		Role,
			
		///<summary>
		/// Composite Property for <c>Staff</c> at StaffIdSource
		///</summary>
		[ChildEntityType(typeof(Staff))]
		Staff,
		}
	
	#endregion StaffRolesChildEntityTypes
	
	#region StaffRolesFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;StaffRolesColumn&gt;"/> class
	/// that is used exclusively with a <see cref="StaffRoles"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class StaffRolesFilterBuilder : SqlFilterBuilder<StaffRolesColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the StaffRolesFilterBuilder class.
		/// </summary>
		public StaffRolesFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the StaffRolesFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public StaffRolesFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the StaffRolesFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public StaffRolesFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion StaffRolesFilterBuilder
	
	#region StaffRolesParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;StaffRolesColumn&gt;"/> class
	/// that is used exclusively with a <see cref="StaffRoles"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class StaffRolesParameterBuilder : ParameterizedSqlFilterBuilder<StaffRolesColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the StaffRolesParameterBuilder class.
		/// </summary>
		public StaffRolesParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the StaffRolesParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public StaffRolesParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the StaffRolesParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public StaffRolesParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion StaffRolesParameterBuilder
	
	#region StaffRolesSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;StaffRolesColumn&gt;"/> class
	/// that is used exclusively with a <see cref="StaffRoles"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class StaffRolesSortBuilder : SqlSortBuilder<StaffRolesColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the StaffRolesSqlSortBuilder class.
		/// </summary>
		public StaffRolesSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion StaffRolesSortBuilder
	
} // end namespace
