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
		public override ClinicDoctor.Entities.DoctorRoster Get(TransactionManager transactionManager, ClinicDoctor.Entities.DoctorRosterKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_DoctorRoster_DoctorUserName index.
		/// </summary>
		/// <param name="_doctorUserName"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByDoctorUserName(System.String _doctorUserName)
		{
			int count = -1;
			return GetByDoctorUserName(null,_doctorUserName, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_DoctorUserName index.
		/// </summary>
		/// <param name="_doctorUserName"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByDoctorUserName(System.String _doctorUserName, int start, int pageLength)
		{
			int count = -1;
			return GetByDoctorUserName(null, _doctorUserName, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_DoctorUserName index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUserName"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByDoctorUserName(TransactionManager transactionManager, System.String _doctorUserName)
		{
			int count = -1;
			return GetByDoctorUserName(transactionManager, _doctorUserName, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_DoctorUserName index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUserName"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByDoctorUserName(TransactionManager transactionManager, System.String _doctorUserName, int start, int pageLength)
		{
			int count = -1;
			return GetByDoctorUserName(transactionManager, _doctorUserName, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_DoctorUserName index.
		/// </summary>
		/// <param name="_doctorUserName"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByDoctorUserName(System.String _doctorUserName, int start, int pageLength, out int count)
		{
			return GetByDoctorUserName(null, _doctorUserName, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_DoctorUserName index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUserName"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public abstract TList<DoctorRoster> GetByDoctorUserName(TransactionManager transactionManager, System.String _doctorUserName, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_DoctorRoster_DoctorUserName_IsComplete index.
		/// </summary>
		/// <param name="_doctorUserName"></param>
		/// <param name="_isComplete"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByDoctorUserNameIsComplete(System.String _doctorUserName, System.Boolean _isComplete)
		{
			int count = -1;
			return GetByDoctorUserNameIsComplete(null,_doctorUserName, _isComplete, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_DoctorUserName_IsComplete index.
		/// </summary>
		/// <param name="_doctorUserName"></param>
		/// <param name="_isComplete"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByDoctorUserNameIsComplete(System.String _doctorUserName, System.Boolean _isComplete, int start, int pageLength)
		{
			int count = -1;
			return GetByDoctorUserNameIsComplete(null, _doctorUserName, _isComplete, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_DoctorUserName_IsComplete index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUserName"></param>
		/// <param name="_isComplete"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByDoctorUserNameIsComplete(TransactionManager transactionManager, System.String _doctorUserName, System.Boolean _isComplete)
		{
			int count = -1;
			return GetByDoctorUserNameIsComplete(transactionManager, _doctorUserName, _isComplete, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_DoctorUserName_IsComplete index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUserName"></param>
		/// <param name="_isComplete"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByDoctorUserNameIsComplete(TransactionManager transactionManager, System.String _doctorUserName, System.Boolean _isComplete, int start, int pageLength)
		{
			int count = -1;
			return GetByDoctorUserNameIsComplete(transactionManager, _doctorUserName, _isComplete, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_DoctorUserName_IsComplete index.
		/// </summary>
		/// <param name="_doctorUserName"></param>
		/// <param name="_isComplete"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByDoctorUserNameIsComplete(System.String _doctorUserName, System.Boolean _isComplete, int start, int pageLength, out int count)
		{
			return GetByDoctorUserNameIsComplete(null, _doctorUserName, _isComplete, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_DoctorUserName_IsComplete index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUserName"></param>
		/// <param name="_isComplete"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public abstract TList<DoctorRoster> GetByDoctorUserNameIsComplete(TransactionManager transactionManager, System.String _doctorUserName, System.Boolean _isComplete, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_DoctorRoster_DoctorUserName_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="_doctorUserName"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByDoctorUserNameIsCompleteIsDisabled(System.String _doctorUserName, System.Boolean _isComplete, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByDoctorUserNameIsCompleteIsDisabled(null,_doctorUserName, _isComplete, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_DoctorUserName_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="_doctorUserName"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByDoctorUserNameIsCompleteIsDisabled(System.String _doctorUserName, System.Boolean _isComplete, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByDoctorUserNameIsCompleteIsDisabled(null, _doctorUserName, _isComplete, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_DoctorUserName_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUserName"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByDoctorUserNameIsCompleteIsDisabled(TransactionManager transactionManager, System.String _doctorUserName, System.Boolean _isComplete, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByDoctorUserNameIsCompleteIsDisabled(transactionManager, _doctorUserName, _isComplete, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_DoctorUserName_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUserName"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByDoctorUserNameIsCompleteIsDisabled(TransactionManager transactionManager, System.String _doctorUserName, System.Boolean _isComplete, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByDoctorUserNameIsCompleteIsDisabled(transactionManager, _doctorUserName, _isComplete, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_DoctorUserName_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="_doctorUserName"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByDoctorUserNameIsCompleteIsDisabled(System.String _doctorUserName, System.Boolean _isComplete, System.Boolean _isDisabled, int start, int pageLength, out int count)
		{
			return GetByDoctorUserNameIsCompleteIsDisabled(null, _doctorUserName, _isComplete, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_DoctorUserName_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUserName"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public abstract TList<DoctorRoster> GetByDoctorUserNameIsCompleteIsDisabled(TransactionManager transactionManager, System.String _doctorUserName, System.Boolean _isComplete, System.Boolean _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_DoctorRoster_DoctorUserName_IsDisabled index.
		/// </summary>
		/// <param name="_doctorUserName"></param>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByDoctorUserNameIsDisabled(System.String _doctorUserName, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByDoctorUserNameIsDisabled(null,_doctorUserName, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_DoctorUserName_IsDisabled index.
		/// </summary>
		/// <param name="_doctorUserName"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByDoctorUserNameIsDisabled(System.String _doctorUserName, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByDoctorUserNameIsDisabled(null, _doctorUserName, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_DoctorUserName_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUserName"></param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByDoctorUserNameIsDisabled(TransactionManager transactionManager, System.String _doctorUserName, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByDoctorUserNameIsDisabled(transactionManager, _doctorUserName, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_DoctorUserName_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUserName"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByDoctorUserNameIsDisabled(TransactionManager transactionManager, System.String _doctorUserName, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByDoctorUserNameIsDisabled(transactionManager, _doctorUserName, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_DoctorUserName_IsDisabled index.
		/// </summary>
		/// <param name="_doctorUserName"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByDoctorUserNameIsDisabled(System.String _doctorUserName, System.Boolean _isDisabled, int start, int pageLength, out int count)
		{
			return GetByDoctorUserNameIsDisabled(null, _doctorUserName, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_DoctorUserName_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUserName"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public abstract TList<DoctorRoster> GetByDoctorUserNameIsDisabled(TransactionManager transactionManager, System.String _doctorUserName, System.Boolean _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_DoctorRoster_DoctorUserName_RosterTypeId index.
		/// </summary>
		/// <param name="_doctorUserName"></param>
		/// <param name="_rosterTypeId"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByDoctorUserNameRosterTypeId(System.String _doctorUserName, System.Int64 _rosterTypeId)
		{
			int count = -1;
			return GetByDoctorUserNameRosterTypeId(null,_doctorUserName, _rosterTypeId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_DoctorUserName_RosterTypeId index.
		/// </summary>
		/// <param name="_doctorUserName"></param>
		/// <param name="_rosterTypeId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByDoctorUserNameRosterTypeId(System.String _doctorUserName, System.Int64 _rosterTypeId, int start, int pageLength)
		{
			int count = -1;
			return GetByDoctorUserNameRosterTypeId(null, _doctorUserName, _rosterTypeId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_DoctorUserName_RosterTypeId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUserName"></param>
		/// <param name="_rosterTypeId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByDoctorUserNameRosterTypeId(TransactionManager transactionManager, System.String _doctorUserName, System.Int64 _rosterTypeId)
		{
			int count = -1;
			return GetByDoctorUserNameRosterTypeId(transactionManager, _doctorUserName, _rosterTypeId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_DoctorUserName_RosterTypeId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUserName"></param>
		/// <param name="_rosterTypeId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByDoctorUserNameRosterTypeId(TransactionManager transactionManager, System.String _doctorUserName, System.Int64 _rosterTypeId, int start, int pageLength)
		{
			int count = -1;
			return GetByDoctorUserNameRosterTypeId(transactionManager, _doctorUserName, _rosterTypeId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_DoctorUserName_RosterTypeId index.
		/// </summary>
		/// <param name="_doctorUserName"></param>
		/// <param name="_rosterTypeId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByDoctorUserNameRosterTypeId(System.String _doctorUserName, System.Int64 _rosterTypeId, int start, int pageLength, out int count)
		{
			return GetByDoctorUserNameRosterTypeId(null, _doctorUserName, _rosterTypeId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_DoctorUserName_RosterTypeId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUserName"></param>
		/// <param name="_rosterTypeId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public abstract TList<DoctorRoster> GetByDoctorUserNameRosterTypeId(TransactionManager transactionManager, System.String _doctorUserName, System.Int64 _rosterTypeId, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_DoctorRoster_DoctorUserName_RosterTypeId_IsComplete index.
		/// </summary>
		/// <param name="_doctorUserName"></param>
		/// <param name="_rosterTypeId"></param>
		/// <param name="_isComplete"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByDoctorUserNameRosterTypeIdIsComplete(System.String _doctorUserName, System.Int64 _rosterTypeId, System.Boolean _isComplete)
		{
			int count = -1;
			return GetByDoctorUserNameRosterTypeIdIsComplete(null,_doctorUserName, _rosterTypeId, _isComplete, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_DoctorUserName_RosterTypeId_IsComplete index.
		/// </summary>
		/// <param name="_doctorUserName"></param>
		/// <param name="_rosterTypeId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByDoctorUserNameRosterTypeIdIsComplete(System.String _doctorUserName, System.Int64 _rosterTypeId, System.Boolean _isComplete, int start, int pageLength)
		{
			int count = -1;
			return GetByDoctorUserNameRosterTypeIdIsComplete(null, _doctorUserName, _rosterTypeId, _isComplete, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_DoctorUserName_RosterTypeId_IsComplete index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUserName"></param>
		/// <param name="_rosterTypeId"></param>
		/// <param name="_isComplete"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByDoctorUserNameRosterTypeIdIsComplete(TransactionManager transactionManager, System.String _doctorUserName, System.Int64 _rosterTypeId, System.Boolean _isComplete)
		{
			int count = -1;
			return GetByDoctorUserNameRosterTypeIdIsComplete(transactionManager, _doctorUserName, _rosterTypeId, _isComplete, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_DoctorUserName_RosterTypeId_IsComplete index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUserName"></param>
		/// <param name="_rosterTypeId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByDoctorUserNameRosterTypeIdIsComplete(TransactionManager transactionManager, System.String _doctorUserName, System.Int64 _rosterTypeId, System.Boolean _isComplete, int start, int pageLength)
		{
			int count = -1;
			return GetByDoctorUserNameRosterTypeIdIsComplete(transactionManager, _doctorUserName, _rosterTypeId, _isComplete, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_DoctorUserName_RosterTypeId_IsComplete index.
		/// </summary>
		/// <param name="_doctorUserName"></param>
		/// <param name="_rosterTypeId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByDoctorUserNameRosterTypeIdIsComplete(System.String _doctorUserName, System.Int64 _rosterTypeId, System.Boolean _isComplete, int start, int pageLength, out int count)
		{
			return GetByDoctorUserNameRosterTypeIdIsComplete(null, _doctorUserName, _rosterTypeId, _isComplete, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_DoctorUserName_RosterTypeId_IsComplete index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUserName"></param>
		/// <param name="_rosterTypeId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public abstract TList<DoctorRoster> GetByDoctorUserNameRosterTypeIdIsComplete(TransactionManager transactionManager, System.String _doctorUserName, System.Int64 _rosterTypeId, System.Boolean _isComplete, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_DoctorRoster_DoctorUserName_RosterTypeId_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="_doctorUserName"></param>
		/// <param name="_rosterTypeId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByDoctorUserNameRosterTypeIdIsCompleteIsDisabled(System.String _doctorUserName, System.Int64 _rosterTypeId, System.Boolean _isComplete, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByDoctorUserNameRosterTypeIdIsCompleteIsDisabled(null,_doctorUserName, _rosterTypeId, _isComplete, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_DoctorUserName_RosterTypeId_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="_doctorUserName"></param>
		/// <param name="_rosterTypeId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByDoctorUserNameRosterTypeIdIsCompleteIsDisabled(System.String _doctorUserName, System.Int64 _rosterTypeId, System.Boolean _isComplete, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByDoctorUserNameRosterTypeIdIsCompleteIsDisabled(null, _doctorUserName, _rosterTypeId, _isComplete, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_DoctorUserName_RosterTypeId_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUserName"></param>
		/// <param name="_rosterTypeId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByDoctorUserNameRosterTypeIdIsCompleteIsDisabled(TransactionManager transactionManager, System.String _doctorUserName, System.Int64 _rosterTypeId, System.Boolean _isComplete, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByDoctorUserNameRosterTypeIdIsCompleteIsDisabled(transactionManager, _doctorUserName, _rosterTypeId, _isComplete, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_DoctorUserName_RosterTypeId_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUserName"></param>
		/// <param name="_rosterTypeId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByDoctorUserNameRosterTypeIdIsCompleteIsDisabled(TransactionManager transactionManager, System.String _doctorUserName, System.Int64 _rosterTypeId, System.Boolean _isComplete, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByDoctorUserNameRosterTypeIdIsCompleteIsDisabled(transactionManager, _doctorUserName, _rosterTypeId, _isComplete, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_DoctorUserName_RosterTypeId_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="_doctorUserName"></param>
		/// <param name="_rosterTypeId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByDoctorUserNameRosterTypeIdIsCompleteIsDisabled(System.String _doctorUserName, System.Int64 _rosterTypeId, System.Boolean _isComplete, System.Boolean _isDisabled, int start, int pageLength, out int count)
		{
			return GetByDoctorUserNameRosterTypeIdIsCompleteIsDisabled(null, _doctorUserName, _rosterTypeId, _isComplete, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_DoctorUserName_RosterTypeId_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUserName"></param>
		/// <param name="_rosterTypeId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public abstract TList<DoctorRoster> GetByDoctorUserNameRosterTypeIdIsCompleteIsDisabled(TransactionManager transactionManager, System.String _doctorUserName, System.Int64 _rosterTypeId, System.Boolean _isComplete, System.Boolean _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_DoctorRoster_DoctorUserName_RosterTypeId_IsDisabled index.
		/// </summary>
		/// <param name="_doctorUserName"></param>
		/// <param name="_rosterTypeId"></param>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByDoctorUserNameRosterTypeIdIsDisabled(System.String _doctorUserName, System.Int64 _rosterTypeId, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByDoctorUserNameRosterTypeIdIsDisabled(null,_doctorUserName, _rosterTypeId, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_DoctorUserName_RosterTypeId_IsDisabled index.
		/// </summary>
		/// <param name="_doctorUserName"></param>
		/// <param name="_rosterTypeId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByDoctorUserNameRosterTypeIdIsDisabled(System.String _doctorUserName, System.Int64 _rosterTypeId, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByDoctorUserNameRosterTypeIdIsDisabled(null, _doctorUserName, _rosterTypeId, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_DoctorUserName_RosterTypeId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUserName"></param>
		/// <param name="_rosterTypeId"></param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByDoctorUserNameRosterTypeIdIsDisabled(TransactionManager transactionManager, System.String _doctorUserName, System.Int64 _rosterTypeId, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByDoctorUserNameRosterTypeIdIsDisabled(transactionManager, _doctorUserName, _rosterTypeId, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_DoctorUserName_RosterTypeId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUserName"></param>
		/// <param name="_rosterTypeId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByDoctorUserNameRosterTypeIdIsDisabled(TransactionManager transactionManager, System.String _doctorUserName, System.Int64 _rosterTypeId, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByDoctorUserNameRosterTypeIdIsDisabled(transactionManager, _doctorUserName, _rosterTypeId, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_DoctorUserName_RosterTypeId_IsDisabled index.
		/// </summary>
		/// <param name="_doctorUserName"></param>
		/// <param name="_rosterTypeId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByDoctorUserNameRosterTypeIdIsDisabled(System.String _doctorUserName, System.Int64 _rosterTypeId, System.Boolean _isDisabled, int start, int pageLength, out int count)
		{
			return GetByDoctorUserNameRosterTypeIdIsDisabled(null, _doctorUserName, _rosterTypeId, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_DoctorUserName_RosterTypeId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUserName"></param>
		/// <param name="_rosterTypeId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public abstract TList<DoctorRoster> GetByDoctorUserNameRosterTypeIdIsDisabled(TransactionManager transactionManager, System.String _doctorUserName, System.Int64 _rosterTypeId, System.Boolean _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_DoctorRoster_Id_IsComplete index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="_isComplete"></param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.DoctorRoster"/> class.</returns>
		public ClinicDoctor.Entities.DoctorRoster GetByIdIsComplete(System.String _id, System.Boolean _isComplete)
		{
			int count = -1;
			return GetByIdIsComplete(null,_id, _isComplete, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_Id_IsComplete index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="_isComplete"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.DoctorRoster"/> class.</returns>
		public ClinicDoctor.Entities.DoctorRoster GetByIdIsComplete(System.String _id, System.Boolean _isComplete, int start, int pageLength)
		{
			int count = -1;
			return GetByIdIsComplete(null, _id, _isComplete, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_Id_IsComplete index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="_isComplete"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.DoctorRoster"/> class.</returns>
		public ClinicDoctor.Entities.DoctorRoster GetByIdIsComplete(TransactionManager transactionManager, System.String _id, System.Boolean _isComplete)
		{
			int count = -1;
			return GetByIdIsComplete(transactionManager, _id, _isComplete, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_Id_IsComplete index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="_isComplete"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.DoctorRoster"/> class.</returns>
		public ClinicDoctor.Entities.DoctorRoster GetByIdIsComplete(TransactionManager transactionManager, System.String _id, System.Boolean _isComplete, int start, int pageLength)
		{
			int count = -1;
			return GetByIdIsComplete(transactionManager, _id, _isComplete, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_Id_IsComplete index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="_isComplete"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.DoctorRoster"/> class.</returns>
		public ClinicDoctor.Entities.DoctorRoster GetByIdIsComplete(System.String _id, System.Boolean _isComplete, int start, int pageLength, out int count)
		{
			return GetByIdIsComplete(null, _id, _isComplete, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_Id_IsComplete index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="_isComplete"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.DoctorRoster"/> class.</returns>
		public abstract ClinicDoctor.Entities.DoctorRoster GetByIdIsComplete(TransactionManager transactionManager, System.String _id, System.Boolean _isComplete, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_DoctorRoster_Id_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.DoctorRoster"/> class.</returns>
		public ClinicDoctor.Entities.DoctorRoster GetByIdIsCompleteIsDisabled(System.String _id, System.Boolean _isComplete, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByIdIsCompleteIsDisabled(null,_id, _isComplete, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_Id_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.DoctorRoster"/> class.</returns>
		public ClinicDoctor.Entities.DoctorRoster GetByIdIsCompleteIsDisabled(System.String _id, System.Boolean _isComplete, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIdIsCompleteIsDisabled(null, _id, _isComplete, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_Id_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.DoctorRoster"/> class.</returns>
		public ClinicDoctor.Entities.DoctorRoster GetByIdIsCompleteIsDisabled(TransactionManager transactionManager, System.String _id, System.Boolean _isComplete, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByIdIsCompleteIsDisabled(transactionManager, _id, _isComplete, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_Id_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.DoctorRoster"/> class.</returns>
		public ClinicDoctor.Entities.DoctorRoster GetByIdIsCompleteIsDisabled(TransactionManager transactionManager, System.String _id, System.Boolean _isComplete, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIdIsCompleteIsDisabled(transactionManager, _id, _isComplete, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_Id_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.DoctorRoster"/> class.</returns>
		public ClinicDoctor.Entities.DoctorRoster GetByIdIsCompleteIsDisabled(System.String _id, System.Boolean _isComplete, System.Boolean _isDisabled, int start, int pageLength, out int count)
		{
			return GetByIdIsCompleteIsDisabled(null, _id, _isComplete, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_Id_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.DoctorRoster"/> class.</returns>
		public abstract ClinicDoctor.Entities.DoctorRoster GetByIdIsCompleteIsDisabled(TransactionManager transactionManager, System.String _id, System.Boolean _isComplete, System.Boolean _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_DoctorRoster_Id_IsDisabled index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.DoctorRoster"/> class.</returns>
		public ClinicDoctor.Entities.DoctorRoster GetByIdIsDisabled(System.String _id, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByIdIsDisabled(null,_id, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_Id_IsDisabled index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.DoctorRoster"/> class.</returns>
		public ClinicDoctor.Entities.DoctorRoster GetByIdIsDisabled(System.String _id, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIdIsDisabled(null, _id, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_Id_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.DoctorRoster"/> class.</returns>
		public ClinicDoctor.Entities.DoctorRoster GetByIdIsDisabled(TransactionManager transactionManager, System.String _id, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByIdIsDisabled(transactionManager, _id, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_Id_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.DoctorRoster"/> class.</returns>
		public ClinicDoctor.Entities.DoctorRoster GetByIdIsDisabled(TransactionManager transactionManager, System.String _id, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIdIsDisabled(transactionManager, _id, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_Id_IsDisabled index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.DoctorRoster"/> class.</returns>
		public ClinicDoctor.Entities.DoctorRoster GetByIdIsDisabled(System.String _id, System.Boolean _isDisabled, int start, int pageLength, out int count)
		{
			return GetByIdIsDisabled(null, _id, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_Id_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.DoctorRoster"/> class.</returns>
		public abstract ClinicDoctor.Entities.DoctorRoster GetByIdIsDisabled(TransactionManager transactionManager, System.String _id, System.Boolean _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_DoctorRoster_IsDisabled index.
		/// </summary>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByIsDisabled(System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByIsDisabled(null,_isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_IsDisabled index.
		/// </summary>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByIsDisabled(System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIsDisabled(null, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByIsDisabled(TransactionManager transactionManager, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByIsDisabled(transactionManager, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByIsDisabled(TransactionManager transactionManager, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIsDisabled(transactionManager, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_IsDisabled index.
		/// </summary>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByIsDisabled(System.Boolean _isDisabled, int start, int pageLength, out int count)
		{
			return GetByIsDisabled(null, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public abstract TList<DoctorRoster> GetByIsDisabled(TransactionManager transactionManager, System.Boolean _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_DoctorRoster_RosterTypeId index.
		/// </summary>
		/// <param name="_rosterTypeId"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByRosterTypeId(System.Int64 _rosterTypeId)
		{
			int count = -1;
			return GetByRosterTypeId(null,_rosterTypeId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_RosterTypeId index.
		/// </summary>
		/// <param name="_rosterTypeId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByRosterTypeId(System.Int64 _rosterTypeId, int start, int pageLength)
		{
			int count = -1;
			return GetByRosterTypeId(null, _rosterTypeId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_RosterTypeId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_rosterTypeId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByRosterTypeId(TransactionManager transactionManager, System.Int64 _rosterTypeId)
		{
			int count = -1;
			return GetByRosterTypeId(transactionManager, _rosterTypeId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_RosterTypeId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_rosterTypeId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByRosterTypeId(TransactionManager transactionManager, System.Int64 _rosterTypeId, int start, int pageLength)
		{
			int count = -1;
			return GetByRosterTypeId(transactionManager, _rosterTypeId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_RosterTypeId index.
		/// </summary>
		/// <param name="_rosterTypeId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByRosterTypeId(System.Int64 _rosterTypeId, int start, int pageLength, out int count)
		{
			return GetByRosterTypeId(null, _rosterTypeId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_RosterTypeId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_rosterTypeId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public abstract TList<DoctorRoster> GetByRosterTypeId(TransactionManager transactionManager, System.Int64 _rosterTypeId, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_DoctorRoster_RosterTypeId_IsComplete index.
		/// </summary>
		/// <param name="_rosterTypeId"></param>
		/// <param name="_isComplete"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByRosterTypeIdIsComplete(System.Int64 _rosterTypeId, System.Boolean _isComplete)
		{
			int count = -1;
			return GetByRosterTypeIdIsComplete(null,_rosterTypeId, _isComplete, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_RosterTypeId_IsComplete index.
		/// </summary>
		/// <param name="_rosterTypeId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByRosterTypeIdIsComplete(System.Int64 _rosterTypeId, System.Boolean _isComplete, int start, int pageLength)
		{
			int count = -1;
			return GetByRosterTypeIdIsComplete(null, _rosterTypeId, _isComplete, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_RosterTypeId_IsComplete index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_rosterTypeId"></param>
		/// <param name="_isComplete"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByRosterTypeIdIsComplete(TransactionManager transactionManager, System.Int64 _rosterTypeId, System.Boolean _isComplete)
		{
			int count = -1;
			return GetByRosterTypeIdIsComplete(transactionManager, _rosterTypeId, _isComplete, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_RosterTypeId_IsComplete index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_rosterTypeId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByRosterTypeIdIsComplete(TransactionManager transactionManager, System.Int64 _rosterTypeId, System.Boolean _isComplete, int start, int pageLength)
		{
			int count = -1;
			return GetByRosterTypeIdIsComplete(transactionManager, _rosterTypeId, _isComplete, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_RosterTypeId_IsComplete index.
		/// </summary>
		/// <param name="_rosterTypeId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByRosterTypeIdIsComplete(System.Int64 _rosterTypeId, System.Boolean _isComplete, int start, int pageLength, out int count)
		{
			return GetByRosterTypeIdIsComplete(null, _rosterTypeId, _isComplete, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_RosterTypeId_IsComplete index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_rosterTypeId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public abstract TList<DoctorRoster> GetByRosterTypeIdIsComplete(TransactionManager transactionManager, System.Int64 _rosterTypeId, System.Boolean _isComplete, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_DoctorRoster_RosterTypeId_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="_rosterTypeId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByRosterTypeIdIsCompleteIsDisabled(System.Int64 _rosterTypeId, System.Boolean _isComplete, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByRosterTypeIdIsCompleteIsDisabled(null,_rosterTypeId, _isComplete, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_RosterTypeId_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="_rosterTypeId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByRosterTypeIdIsCompleteIsDisabled(System.Int64 _rosterTypeId, System.Boolean _isComplete, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByRosterTypeIdIsCompleteIsDisabled(null, _rosterTypeId, _isComplete, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_RosterTypeId_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_rosterTypeId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByRosterTypeIdIsCompleteIsDisabled(TransactionManager transactionManager, System.Int64 _rosterTypeId, System.Boolean _isComplete, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByRosterTypeIdIsCompleteIsDisabled(transactionManager, _rosterTypeId, _isComplete, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_RosterTypeId_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_rosterTypeId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByRosterTypeIdIsCompleteIsDisabled(TransactionManager transactionManager, System.Int64 _rosterTypeId, System.Boolean _isComplete, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByRosterTypeIdIsCompleteIsDisabled(transactionManager, _rosterTypeId, _isComplete, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_RosterTypeId_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="_rosterTypeId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public TList<DoctorRoster> GetByRosterTypeIdIsCompleteIsDisabled(System.Int64 _rosterTypeId, System.Boolean _isComplete, System.Boolean _isDisabled, int start, int pageLength, out int count)
		{
			return GetByRosterTypeIdIsCompleteIsDisabled(null, _rosterTypeId, _isComplete, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoster_RosterTypeId_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_rosterTypeId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoster&gt;"/> class.</returns>
		public abstract TList<DoctorRoster> GetByRosterTypeIdIsCompleteIsDisabled(TransactionManager transactionManager, System.Int64 _rosterTypeId, System.Boolean _isComplete, System.Boolean _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_DoctorRoster index.
		/// </summary>
		/// <param name="_id"></param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.DoctorRoster"/> class.</returns>
		public ClinicDoctor.Entities.DoctorRoster GetById(System.String _id)
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
		public ClinicDoctor.Entities.DoctorRoster GetById(System.String _id, int start, int pageLength)
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
		public ClinicDoctor.Entities.DoctorRoster GetById(TransactionManager transactionManager, System.String _id)
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
		public ClinicDoctor.Entities.DoctorRoster GetById(TransactionManager transactionManager, System.String _id, int start, int pageLength)
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
		public ClinicDoctor.Entities.DoctorRoster GetById(System.String _id, int start, int pageLength, out int count)
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
		public abstract ClinicDoctor.Entities.DoctorRoster GetById(TransactionManager transactionManager, System.String _id, int start, int pageLength, out int count);
						
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
					.Append("|").Append((System.String)reader[((int)DoctorRosterColumn.Id - 1)]).ToString();
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
					c.Id = (System.String)reader[((int)DoctorRosterColumn.Id - 1)];
					c.OriginalId = c.Id;
					c.DoctorUserName = (System.String)reader[((int)DoctorRosterColumn.DoctorUserName - 1)];
					c.DoctorShortName = (reader.IsDBNull(((int)DoctorRosterColumn.DoctorShortName - 1)))?null:(System.String)reader[((int)DoctorRosterColumn.DoctorShortName - 1)];
					c.DoctorEmail = (reader.IsDBNull(((int)DoctorRosterColumn.DoctorEmail - 1)))?null:(System.String)reader[((int)DoctorRosterColumn.DoctorEmail - 1)];
					c.RosterTypeId = (System.Int64)reader[((int)DoctorRosterColumn.RosterTypeId - 1)];
					c.RosterTypeTitle = (reader.IsDBNull(((int)DoctorRosterColumn.RosterTypeTitle - 1)))?null:(System.String)reader[((int)DoctorRosterColumn.RosterTypeTitle - 1)];
					c.ColorCode = (reader.IsDBNull(((int)DoctorRosterColumn.ColorCode - 1)))?null:(System.String)reader[((int)DoctorRosterColumn.ColorCode - 1)];
					c.IsBooked = (reader.IsDBNull(((int)DoctorRosterColumn.IsBooked - 1)))?null:(System.Boolean?)reader[((int)DoctorRosterColumn.IsBooked - 1)];
					c.StartTime = (System.DateTime)reader[((int)DoctorRosterColumn.StartTime - 1)];
					c.EndTime = (System.DateTime)reader[((int)DoctorRosterColumn.EndTime - 1)];
					c.Note = (reader.IsDBNull(((int)DoctorRosterColumn.Note - 1)))?null:(System.String)reader[((int)DoctorRosterColumn.Note - 1)];
					c.IsComplete = (System.Boolean)reader[((int)DoctorRosterColumn.IsComplete - 1)];
					c.IsDisabled = (System.Boolean)reader[((int)DoctorRosterColumn.IsDisabled - 1)];
					c.CreateUser = (reader.IsDBNull(((int)DoctorRosterColumn.CreateUser - 1)))?null:(System.String)reader[((int)DoctorRosterColumn.CreateUser - 1)];
					c.CreateDate = (System.DateTime)reader[((int)DoctorRosterColumn.CreateDate - 1)];
					c.UpdateUser = (reader.IsDBNull(((int)DoctorRosterColumn.UpdateUser - 1)))?null:(System.String)reader[((int)DoctorRosterColumn.UpdateUser - 1)];
					c.UpdateDate = (System.DateTime)reader[((int)DoctorRosterColumn.UpdateDate - 1)];
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
			
			entity.Id = (System.String)reader[((int)DoctorRosterColumn.Id - 1)];
			entity.OriginalId = (System.String)reader["Id"];
			entity.DoctorUserName = (System.String)reader[((int)DoctorRosterColumn.DoctorUserName - 1)];
			entity.DoctorShortName = (reader.IsDBNull(((int)DoctorRosterColumn.DoctorShortName - 1)))?null:(System.String)reader[((int)DoctorRosterColumn.DoctorShortName - 1)];
			entity.DoctorEmail = (reader.IsDBNull(((int)DoctorRosterColumn.DoctorEmail - 1)))?null:(System.String)reader[((int)DoctorRosterColumn.DoctorEmail - 1)];
			entity.RosterTypeId = (System.Int64)reader[((int)DoctorRosterColumn.RosterTypeId - 1)];
			entity.RosterTypeTitle = (reader.IsDBNull(((int)DoctorRosterColumn.RosterTypeTitle - 1)))?null:(System.String)reader[((int)DoctorRosterColumn.RosterTypeTitle - 1)];
			entity.ColorCode = (reader.IsDBNull(((int)DoctorRosterColumn.ColorCode - 1)))?null:(System.String)reader[((int)DoctorRosterColumn.ColorCode - 1)];
			entity.IsBooked = (reader.IsDBNull(((int)DoctorRosterColumn.IsBooked - 1)))?null:(System.Boolean?)reader[((int)DoctorRosterColumn.IsBooked - 1)];
			entity.StartTime = (System.DateTime)reader[((int)DoctorRosterColumn.StartTime - 1)];
			entity.EndTime = (System.DateTime)reader[((int)DoctorRosterColumn.EndTime - 1)];
			entity.Note = (reader.IsDBNull(((int)DoctorRosterColumn.Note - 1)))?null:(System.String)reader[((int)DoctorRosterColumn.Note - 1)];
			entity.IsComplete = (System.Boolean)reader[((int)DoctorRosterColumn.IsComplete - 1)];
			entity.IsDisabled = (System.Boolean)reader[((int)DoctorRosterColumn.IsDisabled - 1)];
			entity.CreateUser = (reader.IsDBNull(((int)DoctorRosterColumn.CreateUser - 1)))?null:(System.String)reader[((int)DoctorRosterColumn.CreateUser - 1)];
			entity.CreateDate = (System.DateTime)reader[((int)DoctorRosterColumn.CreateDate - 1)];
			entity.UpdateUser = (reader.IsDBNull(((int)DoctorRosterColumn.UpdateUser - 1)))?null:(System.String)reader[((int)DoctorRosterColumn.UpdateUser - 1)];
			entity.UpdateDate = (System.DateTime)reader[((int)DoctorRosterColumn.UpdateDate - 1)];
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
			
			entity.Id = (System.String)dataRow["Id"];
			entity.OriginalId = (System.String)dataRow["Id"];
			entity.DoctorUserName = (System.String)dataRow["DoctorUserName"];
			entity.DoctorShortName = Convert.IsDBNull(dataRow["DoctorShortName"]) ? null : (System.String)dataRow["DoctorShortName"];
			entity.DoctorEmail = Convert.IsDBNull(dataRow["DoctorEmail"]) ? null : (System.String)dataRow["DoctorEmail"];
			entity.RosterTypeId = (System.Int64)dataRow["RosterTypeId"];
			entity.RosterTypeTitle = Convert.IsDBNull(dataRow["RosterTypeTitle"]) ? null : (System.String)dataRow["RosterTypeTitle"];
			entity.ColorCode = Convert.IsDBNull(dataRow["ColorCode"]) ? null : (System.String)dataRow["ColorCode"];
			entity.IsBooked = Convert.IsDBNull(dataRow["IsBooked"]) ? null : (System.Boolean?)dataRow["IsBooked"];
			entity.StartTime = (System.DateTime)dataRow["StartTime"];
			entity.EndTime = (System.DateTime)dataRow["EndTime"];
			entity.Note = Convert.IsDBNull(dataRow["Note"]) ? null : (System.String)dataRow["Note"];
			entity.IsComplete = (System.Boolean)dataRow["IsComplete"];
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

			#region RosterTypeIdSource	
			if (CanDeepLoad(entity, "RosterType|RosterTypeIdSource", deepLoadType, innerList) 
				&& entity.RosterTypeIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = entity.RosterTypeId;
				RosterType tmpEntity = EntityManager.LocateEntity<RosterType>(EntityLocator.ConstructKeyFromPkItems(typeof(RosterType), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.RosterTypeIdSource = tmpEntity;
				else
					entity.RosterTypeIdSource = DataRepository.RosterTypeProvider.GetById(transactionManager, entity.RosterTypeId);		
				
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

			#region DoctorUserNameSource	
			if (CanDeepLoad(entity, "Staff|DoctorUserNameSource", deepLoadType, innerList) 
				&& entity.DoctorUserNameSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = entity.DoctorUserName;
				Staff tmpEntity = EntityManager.LocateEntity<Staff>(EntityLocator.ConstructKeyFromPkItems(typeof(Staff), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.DoctorUserNameSource = tmpEntity;
				else
					entity.DoctorUserNameSource = DataRepository.StaffProvider.GetByUserName(transactionManager, entity.DoctorUserName);		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'DoctorUserNameSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.DoctorUserNameSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.StaffProvider.DeepLoad(transactionManager, entity.DoctorUserNameSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion DoctorUserNameSource
			
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
			
			#region RosterTypeIdSource
			if (CanDeepSave(entity, "RosterType|RosterTypeIdSource", deepSaveType, innerList) 
				&& entity.RosterTypeIdSource != null)
			{
				DataRepository.RosterTypeProvider.Save(transactionManager, entity.RosterTypeIdSource);
				entity.RosterTypeId = entity.RosterTypeIdSource.Id;
			}
			#endregion 
			
			#region DoctorUserNameSource
			if (CanDeepSave(entity, "Staff|DoctorUserNameSource", deepSaveType, innerList) 
				&& entity.DoctorUserNameSource != null)
			{
				DataRepository.StaffProvider.Save(transactionManager, entity.DoctorUserNameSource);
				entity.DoctorUserName = entity.DoctorUserNameSource.UserName;
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
		/// Composite Property for <c>RosterType</c> at RosterTypeIdSource
		///</summary>
		[ChildEntityType(typeof(RosterType))]
		RosterType,
			
		///<summary>
		/// Composite Property for <c>Staff</c> at DoctorUserNameSource
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
