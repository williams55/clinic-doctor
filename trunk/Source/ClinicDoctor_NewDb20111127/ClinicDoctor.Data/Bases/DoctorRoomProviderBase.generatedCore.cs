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
	/// This class is the base class for any <see cref="DoctorRoomProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class DoctorRoomProviderBaseCore : EntityProviderBase<ClinicDoctor.Entities.DoctorRoom, ClinicDoctor.Entities.DoctorRoomKey>
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
		public override bool Delete(TransactionManager transactionManager, ClinicDoctor.Entities.DoctorRoomKey key)
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
		public override ClinicDoctor.Entities.DoctorRoom Get(TransactionManager transactionManager, ClinicDoctor.Entities.DoctorRoomKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_DoctorRoom_DoctorUserName index.
		/// </summary>
		/// <param name="_doctorUserName"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoom&gt;"/> class.</returns>
		public TList<DoctorRoom> GetByDoctorUserName(System.String _doctorUserName)
		{
			int count = -1;
			return GetByDoctorUserName(null,_doctorUserName, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoom_DoctorUserName index.
		/// </summary>
		/// <param name="_doctorUserName"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoom&gt;"/> class.</returns>
		public TList<DoctorRoom> GetByDoctorUserName(System.String _doctorUserName, int start, int pageLength)
		{
			int count = -1;
			return GetByDoctorUserName(null, _doctorUserName, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoom_DoctorUserName index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUserName"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoom&gt;"/> class.</returns>
		public TList<DoctorRoom> GetByDoctorUserName(TransactionManager transactionManager, System.String _doctorUserName)
		{
			int count = -1;
			return GetByDoctorUserName(transactionManager, _doctorUserName, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoom_DoctorUserName index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUserName"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoom&gt;"/> class.</returns>
		public TList<DoctorRoom> GetByDoctorUserName(TransactionManager transactionManager, System.String _doctorUserName, int start, int pageLength)
		{
			int count = -1;
			return GetByDoctorUserName(transactionManager, _doctorUserName, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoom_DoctorUserName index.
		/// </summary>
		/// <param name="_doctorUserName"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoom&gt;"/> class.</returns>
		public TList<DoctorRoom> GetByDoctorUserName(System.String _doctorUserName, int start, int pageLength, out int count)
		{
			return GetByDoctorUserName(null, _doctorUserName, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoom_DoctorUserName index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUserName"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoom&gt;"/> class.</returns>
		public abstract TList<DoctorRoom> GetByDoctorUserName(TransactionManager transactionManager, System.String _doctorUserName, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_DoctorRoom_DoctorUserName_IsDisabled index.
		/// </summary>
		/// <param name="_doctorUserName"></param>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoom&gt;"/> class.</returns>
		public TList<DoctorRoom> GetByDoctorUserNameIsDisabled(System.String _doctorUserName, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByDoctorUserNameIsDisabled(null,_doctorUserName, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoom_DoctorUserName_IsDisabled index.
		/// </summary>
		/// <param name="_doctorUserName"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoom&gt;"/> class.</returns>
		public TList<DoctorRoom> GetByDoctorUserNameIsDisabled(System.String _doctorUserName, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByDoctorUserNameIsDisabled(null, _doctorUserName, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoom_DoctorUserName_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUserName"></param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoom&gt;"/> class.</returns>
		public TList<DoctorRoom> GetByDoctorUserNameIsDisabled(TransactionManager transactionManager, System.String _doctorUserName, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByDoctorUserNameIsDisabled(transactionManager, _doctorUserName, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoom_DoctorUserName_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUserName"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoom&gt;"/> class.</returns>
		public TList<DoctorRoom> GetByDoctorUserNameIsDisabled(TransactionManager transactionManager, System.String _doctorUserName, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByDoctorUserNameIsDisabled(transactionManager, _doctorUserName, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoom_DoctorUserName_IsDisabled index.
		/// </summary>
		/// <param name="_doctorUserName"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoom&gt;"/> class.</returns>
		public TList<DoctorRoom> GetByDoctorUserNameIsDisabled(System.String _doctorUserName, System.Boolean _isDisabled, int start, int pageLength, out int count)
		{
			return GetByDoctorUserNameIsDisabled(null, _doctorUserName, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoom_DoctorUserName_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUserName"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoom&gt;"/> class.</returns>
		public abstract TList<DoctorRoom> GetByDoctorUserNameIsDisabled(TransactionManager transactionManager, System.String _doctorUserName, System.Boolean _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_DoctorRoom_DoctorUserName_RoomId index.
		/// </summary>
		/// <param name="_doctorUserName"></param>
		/// <param name="_roomId"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoom&gt;"/> class.</returns>
		public TList<DoctorRoom> GetByDoctorUserNameRoomId(System.String _doctorUserName, System.Int64 _roomId)
		{
			int count = -1;
			return GetByDoctorUserNameRoomId(null,_doctorUserName, _roomId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoom_DoctorUserName_RoomId index.
		/// </summary>
		/// <param name="_doctorUserName"></param>
		/// <param name="_roomId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoom&gt;"/> class.</returns>
		public TList<DoctorRoom> GetByDoctorUserNameRoomId(System.String _doctorUserName, System.Int64 _roomId, int start, int pageLength)
		{
			int count = -1;
			return GetByDoctorUserNameRoomId(null, _doctorUserName, _roomId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoom_DoctorUserName_RoomId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUserName"></param>
		/// <param name="_roomId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoom&gt;"/> class.</returns>
		public TList<DoctorRoom> GetByDoctorUserNameRoomId(TransactionManager transactionManager, System.String _doctorUserName, System.Int64 _roomId)
		{
			int count = -1;
			return GetByDoctorUserNameRoomId(transactionManager, _doctorUserName, _roomId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoom_DoctorUserName_RoomId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUserName"></param>
		/// <param name="_roomId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoom&gt;"/> class.</returns>
		public TList<DoctorRoom> GetByDoctorUserNameRoomId(TransactionManager transactionManager, System.String _doctorUserName, System.Int64 _roomId, int start, int pageLength)
		{
			int count = -1;
			return GetByDoctorUserNameRoomId(transactionManager, _doctorUserName, _roomId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoom_DoctorUserName_RoomId index.
		/// </summary>
		/// <param name="_doctorUserName"></param>
		/// <param name="_roomId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoom&gt;"/> class.</returns>
		public TList<DoctorRoom> GetByDoctorUserNameRoomId(System.String _doctorUserName, System.Int64 _roomId, int start, int pageLength, out int count)
		{
			return GetByDoctorUserNameRoomId(null, _doctorUserName, _roomId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoom_DoctorUserName_RoomId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUserName"></param>
		/// <param name="_roomId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoom&gt;"/> class.</returns>
		public abstract TList<DoctorRoom> GetByDoctorUserNameRoomId(TransactionManager transactionManager, System.String _doctorUserName, System.Int64 _roomId, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_DoctorRoom_DoctorUserName_RoomId_IsDisabled index.
		/// </summary>
		/// <param name="_doctorUserName"></param>
		/// <param name="_roomId"></param>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoom&gt;"/> class.</returns>
		public TList<DoctorRoom> GetByDoctorUserNameRoomIdIsDisabled(System.String _doctorUserName, System.Int64 _roomId, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByDoctorUserNameRoomIdIsDisabled(null,_doctorUserName, _roomId, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoom_DoctorUserName_RoomId_IsDisabled index.
		/// </summary>
		/// <param name="_doctorUserName"></param>
		/// <param name="_roomId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoom&gt;"/> class.</returns>
		public TList<DoctorRoom> GetByDoctorUserNameRoomIdIsDisabled(System.String _doctorUserName, System.Int64 _roomId, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByDoctorUserNameRoomIdIsDisabled(null, _doctorUserName, _roomId, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoom_DoctorUserName_RoomId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUserName"></param>
		/// <param name="_roomId"></param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoom&gt;"/> class.</returns>
		public TList<DoctorRoom> GetByDoctorUserNameRoomIdIsDisabled(TransactionManager transactionManager, System.String _doctorUserName, System.Int64 _roomId, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByDoctorUserNameRoomIdIsDisabled(transactionManager, _doctorUserName, _roomId, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoom_DoctorUserName_RoomId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUserName"></param>
		/// <param name="_roomId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoom&gt;"/> class.</returns>
		public TList<DoctorRoom> GetByDoctorUserNameRoomIdIsDisabled(TransactionManager transactionManager, System.String _doctorUserName, System.Int64 _roomId, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByDoctorUserNameRoomIdIsDisabled(transactionManager, _doctorUserName, _roomId, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoom_DoctorUserName_RoomId_IsDisabled index.
		/// </summary>
		/// <param name="_doctorUserName"></param>
		/// <param name="_roomId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoom&gt;"/> class.</returns>
		public TList<DoctorRoom> GetByDoctorUserNameRoomIdIsDisabled(System.String _doctorUserName, System.Int64 _roomId, System.Boolean _isDisabled, int start, int pageLength, out int count)
		{
			return GetByDoctorUserNameRoomIdIsDisabled(null, _doctorUserName, _roomId, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoom_DoctorUserName_RoomId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUserName"></param>
		/// <param name="_roomId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoom&gt;"/> class.</returns>
		public abstract TList<DoctorRoom> GetByDoctorUserNameRoomIdIsDisabled(TransactionManager transactionManager, System.String _doctorUserName, System.Int64 _roomId, System.Boolean _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_DoctorRoom_Id_IsDisabled index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.DoctorRoom"/> class.</returns>
		public ClinicDoctor.Entities.DoctorRoom GetByIdIsDisabled(System.Int64 _id, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByIdIsDisabled(null,_id, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoom_Id_IsDisabled index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.DoctorRoom"/> class.</returns>
		public ClinicDoctor.Entities.DoctorRoom GetByIdIsDisabled(System.Int64 _id, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIdIsDisabled(null, _id, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoom_Id_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.DoctorRoom"/> class.</returns>
		public ClinicDoctor.Entities.DoctorRoom GetByIdIsDisabled(TransactionManager transactionManager, System.Int64 _id, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByIdIsDisabled(transactionManager, _id, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoom_Id_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.DoctorRoom"/> class.</returns>
		public ClinicDoctor.Entities.DoctorRoom GetByIdIsDisabled(TransactionManager transactionManager, System.Int64 _id, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIdIsDisabled(transactionManager, _id, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoom_Id_IsDisabled index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.DoctorRoom"/> class.</returns>
		public ClinicDoctor.Entities.DoctorRoom GetByIdIsDisabled(System.Int64 _id, System.Boolean _isDisabled, int start, int pageLength, out int count)
		{
			return GetByIdIsDisabled(null, _id, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoom_Id_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.DoctorRoom"/> class.</returns>
		public abstract ClinicDoctor.Entities.DoctorRoom GetByIdIsDisabled(TransactionManager transactionManager, System.Int64 _id, System.Boolean _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_DoctorRoom_IsDisabled index.
		/// </summary>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoom&gt;"/> class.</returns>
		public TList<DoctorRoom> GetByIsDisabled(System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByIsDisabled(null,_isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoom_IsDisabled index.
		/// </summary>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoom&gt;"/> class.</returns>
		public TList<DoctorRoom> GetByIsDisabled(System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIsDisabled(null, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoom_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoom&gt;"/> class.</returns>
		public TList<DoctorRoom> GetByIsDisabled(TransactionManager transactionManager, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByIsDisabled(transactionManager, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoom_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoom&gt;"/> class.</returns>
		public TList<DoctorRoom> GetByIsDisabled(TransactionManager transactionManager, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIsDisabled(transactionManager, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoom_IsDisabled index.
		/// </summary>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoom&gt;"/> class.</returns>
		public TList<DoctorRoom> GetByIsDisabled(System.Boolean _isDisabled, int start, int pageLength, out int count)
		{
			return GetByIsDisabled(null, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoom_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoom&gt;"/> class.</returns>
		public abstract TList<DoctorRoom> GetByIsDisabled(TransactionManager transactionManager, System.Boolean _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_DoctorRoom_RoomId index.
		/// </summary>
		/// <param name="_roomId"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoom&gt;"/> class.</returns>
		public TList<DoctorRoom> GetByRoomId(System.Int64 _roomId)
		{
			int count = -1;
			return GetByRoomId(null,_roomId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoom_RoomId index.
		/// </summary>
		/// <param name="_roomId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoom&gt;"/> class.</returns>
		public TList<DoctorRoom> GetByRoomId(System.Int64 _roomId, int start, int pageLength)
		{
			int count = -1;
			return GetByRoomId(null, _roomId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoom_RoomId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_roomId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoom&gt;"/> class.</returns>
		public TList<DoctorRoom> GetByRoomId(TransactionManager transactionManager, System.Int64 _roomId)
		{
			int count = -1;
			return GetByRoomId(transactionManager, _roomId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoom_RoomId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_roomId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoom&gt;"/> class.</returns>
		public TList<DoctorRoom> GetByRoomId(TransactionManager transactionManager, System.Int64 _roomId, int start, int pageLength)
		{
			int count = -1;
			return GetByRoomId(transactionManager, _roomId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoom_RoomId index.
		/// </summary>
		/// <param name="_roomId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoom&gt;"/> class.</returns>
		public TList<DoctorRoom> GetByRoomId(System.Int64 _roomId, int start, int pageLength, out int count)
		{
			return GetByRoomId(null, _roomId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoom_RoomId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_roomId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoom&gt;"/> class.</returns>
		public abstract TList<DoctorRoom> GetByRoomId(TransactionManager transactionManager, System.Int64 _roomId, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_DoctorRoom_RoomId_IsDisabled index.
		/// </summary>
		/// <param name="_roomId"></param>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoom&gt;"/> class.</returns>
		public TList<DoctorRoom> GetByRoomIdIsDisabled(System.Int64 _roomId, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByRoomIdIsDisabled(null,_roomId, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoom_RoomId_IsDisabled index.
		/// </summary>
		/// <param name="_roomId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoom&gt;"/> class.</returns>
		public TList<DoctorRoom> GetByRoomIdIsDisabled(System.Int64 _roomId, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByRoomIdIsDisabled(null, _roomId, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoom_RoomId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_roomId"></param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoom&gt;"/> class.</returns>
		public TList<DoctorRoom> GetByRoomIdIsDisabled(TransactionManager transactionManager, System.Int64 _roomId, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByRoomIdIsDisabled(transactionManager, _roomId, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoom_RoomId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_roomId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoom&gt;"/> class.</returns>
		public TList<DoctorRoom> GetByRoomIdIsDisabled(TransactionManager transactionManager, System.Int64 _roomId, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByRoomIdIsDisabled(transactionManager, _roomId, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoom_RoomId_IsDisabled index.
		/// </summary>
		/// <param name="_roomId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoom&gt;"/> class.</returns>
		public TList<DoctorRoom> GetByRoomIdIsDisabled(System.Int64 _roomId, System.Boolean _isDisabled, int start, int pageLength, out int count)
		{
			return GetByRoomIdIsDisabled(null, _roomId, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_DoctorRoom_RoomId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_roomId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;DoctorRoom&gt;"/> class.</returns>
		public abstract TList<DoctorRoom> GetByRoomIdIsDisabled(TransactionManager transactionManager, System.Int64 _roomId, System.Boolean _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_DoctorRoom index.
		/// </summary>
		/// <param name="_id"></param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.DoctorRoom"/> class.</returns>
		public ClinicDoctor.Entities.DoctorRoom GetById(System.Int64 _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_DoctorRoom index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.DoctorRoom"/> class.</returns>
		public ClinicDoctor.Entities.DoctorRoom GetById(System.Int64 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_DoctorRoom index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.DoctorRoom"/> class.</returns>
		public ClinicDoctor.Entities.DoctorRoom GetById(TransactionManager transactionManager, System.Int64 _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_DoctorRoom index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.DoctorRoom"/> class.</returns>
		public ClinicDoctor.Entities.DoctorRoom GetById(TransactionManager transactionManager, System.Int64 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_DoctorRoom index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.DoctorRoom"/> class.</returns>
		public ClinicDoctor.Entities.DoctorRoom GetById(System.Int64 _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_DoctorRoom index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.DoctorRoom"/> class.</returns>
		public abstract ClinicDoctor.Entities.DoctorRoom GetById(TransactionManager transactionManager, System.Int64 _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;DoctorRoom&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;DoctorRoom&gt;"/></returns>
		public static TList<DoctorRoom> Fill(IDataReader reader, TList<DoctorRoom> rows, int start, int pageLength)
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
				
				ClinicDoctor.Entities.DoctorRoom c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("DoctorRoom")
					.Append("|").Append((System.Int64)reader[((int)DoctorRoomColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<DoctorRoom>(
					key.ToString(), // EntityTrackingKey
					"DoctorRoom",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new ClinicDoctor.Entities.DoctorRoom();
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
					c.Id = (System.Int64)reader[((int)DoctorRoomColumn.Id - 1)];
					c.DoctorUserName = (System.String)reader[((int)DoctorRoomColumn.DoctorUserName - 1)];
					c.DoctorShortName = (reader.IsDBNull(((int)DoctorRoomColumn.DoctorShortName - 1)))?null:(System.String)reader[((int)DoctorRoomColumn.DoctorShortName - 1)];
					c.RoomId = (System.Int64)reader[((int)DoctorRoomColumn.RoomId - 1)];
					c.RoomTitle = (reader.IsDBNull(((int)DoctorRoomColumn.RoomTitle - 1)))?null:(System.String)reader[((int)DoctorRoomColumn.RoomTitle - 1)];
					c.PriorityIndex = (System.Int32)reader[((int)DoctorRoomColumn.PriorityIndex - 1)];
					c.IsDisabled = (System.Boolean)reader[((int)DoctorRoomColumn.IsDisabled - 1)];
					c.CreateUser = (reader.IsDBNull(((int)DoctorRoomColumn.CreateUser - 1)))?null:(System.String)reader[((int)DoctorRoomColumn.CreateUser - 1)];
					c.CreateDate = (System.DateTime)reader[((int)DoctorRoomColumn.CreateDate - 1)];
					c.UpdateUser = (reader.IsDBNull(((int)DoctorRoomColumn.UpdateUser - 1)))?null:(System.String)reader[((int)DoctorRoomColumn.UpdateUser - 1)];
					c.UpdateDate = (System.DateTime)reader[((int)DoctorRoomColumn.UpdateDate - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="ClinicDoctor.Entities.DoctorRoom"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="ClinicDoctor.Entities.DoctorRoom"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, ClinicDoctor.Entities.DoctorRoom entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (System.Int64)reader[((int)DoctorRoomColumn.Id - 1)];
			entity.DoctorUserName = (System.String)reader[((int)DoctorRoomColumn.DoctorUserName - 1)];
			entity.DoctorShortName = (reader.IsDBNull(((int)DoctorRoomColumn.DoctorShortName - 1)))?null:(System.String)reader[((int)DoctorRoomColumn.DoctorShortName - 1)];
			entity.RoomId = (System.Int64)reader[((int)DoctorRoomColumn.RoomId - 1)];
			entity.RoomTitle = (reader.IsDBNull(((int)DoctorRoomColumn.RoomTitle - 1)))?null:(System.String)reader[((int)DoctorRoomColumn.RoomTitle - 1)];
			entity.PriorityIndex = (System.Int32)reader[((int)DoctorRoomColumn.PriorityIndex - 1)];
			entity.IsDisabled = (System.Boolean)reader[((int)DoctorRoomColumn.IsDisabled - 1)];
			entity.CreateUser = (reader.IsDBNull(((int)DoctorRoomColumn.CreateUser - 1)))?null:(System.String)reader[((int)DoctorRoomColumn.CreateUser - 1)];
			entity.CreateDate = (System.DateTime)reader[((int)DoctorRoomColumn.CreateDate - 1)];
			entity.UpdateUser = (reader.IsDBNull(((int)DoctorRoomColumn.UpdateUser - 1)))?null:(System.String)reader[((int)DoctorRoomColumn.UpdateUser - 1)];
			entity.UpdateDate = (System.DateTime)reader[((int)DoctorRoomColumn.UpdateDate - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="ClinicDoctor.Entities.DoctorRoom"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="ClinicDoctor.Entities.DoctorRoom"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, ClinicDoctor.Entities.DoctorRoom entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Id = (System.Int64)dataRow["Id"];
			entity.DoctorUserName = (System.String)dataRow["DoctorUserName"];
			entity.DoctorShortName = Convert.IsDBNull(dataRow["DoctorShortName"]) ? null : (System.String)dataRow["DoctorShortName"];
			entity.RoomId = (System.Int64)dataRow["RoomId"];
			entity.RoomTitle = Convert.IsDBNull(dataRow["RoomTitle"]) ? null : (System.String)dataRow["RoomTitle"];
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
		/// <param name="entity">The <see cref="ClinicDoctor.Entities.DoctorRoom"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">ClinicDoctor.Entities.DoctorRoom Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, ClinicDoctor.Entities.DoctorRoom entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region RoomIdSource	
			if (CanDeepLoad(entity, "Room|RoomIdSource", deepLoadType, innerList) 
				&& entity.RoomIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = entity.RoomId;
				Room tmpEntity = EntityManager.LocateEntity<Room>(EntityLocator.ConstructKeyFromPkItems(typeof(Room), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.RoomIdSource = tmpEntity;
				else
					entity.RoomIdSource = DataRepository.RoomProvider.GetById(transactionManager, entity.RoomId);		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'RoomIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.RoomIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.RoomProvider.DeepLoad(transactionManager, entity.RoomIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion RoomIdSource

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
		/// Deep Save the entire object graph of the ClinicDoctor.Entities.DoctorRoom object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">ClinicDoctor.Entities.DoctorRoom instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">ClinicDoctor.Entities.DoctorRoom Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, ClinicDoctor.Entities.DoctorRoom entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			
			#region RoomIdSource
			if (CanDeepSave(entity, "Room|RoomIdSource", deepSaveType, innerList) 
				&& entity.RoomIdSource != null)
			{
				DataRepository.RoomProvider.Save(transactionManager, entity.RoomIdSource);
				entity.RoomId = entity.RoomIdSource.Id;
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
	
	#region DoctorRoomChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>ClinicDoctor.Entities.DoctorRoom</c>
	///</summary>
	public enum DoctorRoomChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Room</c> at RoomIdSource
		///</summary>
		[ChildEntityType(typeof(Room))]
		Room,
			
		///<summary>
		/// Composite Property for <c>Staff</c> at DoctorUserNameSource
		///</summary>
		[ChildEntityType(typeof(Staff))]
		Staff,
		}
	
	#endregion DoctorRoomChildEntityTypes
	
	#region DoctorRoomFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;DoctorRoomColumn&gt;"/> class
	/// that is used exclusively with a <see cref="DoctorRoom"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class DoctorRoomFilterBuilder : SqlFilterBuilder<DoctorRoomColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DoctorRoomFilterBuilder class.
		/// </summary>
		public DoctorRoomFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the DoctorRoomFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public DoctorRoomFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the DoctorRoomFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public DoctorRoomFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion DoctorRoomFilterBuilder
	
	#region DoctorRoomParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;DoctorRoomColumn&gt;"/> class
	/// that is used exclusively with a <see cref="DoctorRoom"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class DoctorRoomParameterBuilder : ParameterizedSqlFilterBuilder<DoctorRoomColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DoctorRoomParameterBuilder class.
		/// </summary>
		public DoctorRoomParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the DoctorRoomParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public DoctorRoomParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the DoctorRoomParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public DoctorRoomParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion DoctorRoomParameterBuilder
	
	#region DoctorRoomSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;DoctorRoomColumn&gt;"/> class
	/// that is used exclusively with a <see cref="DoctorRoom"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class DoctorRoomSortBuilder : SqlSortBuilder<DoctorRoomColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DoctorRoomSqlSortBuilder class.
		/// </summary>
		public DoctorRoomSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion DoctorRoomSortBuilder
	
} // end namespace
