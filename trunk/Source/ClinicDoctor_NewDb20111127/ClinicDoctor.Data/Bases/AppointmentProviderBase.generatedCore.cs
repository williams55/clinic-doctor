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
	/// This class is the base class for any <see cref="AppointmentProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class AppointmentProviderBaseCore : EntityProviderBase<ClinicDoctor.Entities.Appointment, ClinicDoctor.Entities.AppointmentKey>
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
		public override bool Delete(TransactionManager transactionManager, ClinicDoctor.Entities.AppointmentKey key)
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
		public override ClinicDoctor.Entities.Appointment Get(TransactionManager transactionManager, ClinicDoctor.Entities.AppointmentKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_Appointment_ContentId index.
		/// </summary>
		/// <param name="_contentId"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByContentId(System.Int64 _contentId)
		{
			int count = -1;
			return GetByContentId(null,_contentId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_ContentId index.
		/// </summary>
		/// <param name="_contentId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByContentId(System.Int64 _contentId, int start, int pageLength)
		{
			int count = -1;
			return GetByContentId(null, _contentId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_ContentId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_contentId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByContentId(TransactionManager transactionManager, System.Int64 _contentId)
		{
			int count = -1;
			return GetByContentId(transactionManager, _contentId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_ContentId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_contentId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByContentId(TransactionManager transactionManager, System.Int64 _contentId, int start, int pageLength)
		{
			int count = -1;
			return GetByContentId(transactionManager, _contentId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_ContentId index.
		/// </summary>
		/// <param name="_contentId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByContentId(System.Int64 _contentId, int start, int pageLength, out int count)
		{
			return GetByContentId(null, _contentId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_ContentId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_contentId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public abstract TList<Appointment> GetByContentId(TransactionManager transactionManager, System.Int64 _contentId, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_Appointment_ContentId_IsDisabled index.
		/// </summary>
		/// <param name="_contentId"></param>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByContentIdIsDisabled(System.Int64 _contentId, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByContentIdIsDisabled(null,_contentId, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_ContentId_IsDisabled index.
		/// </summary>
		/// <param name="_contentId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByContentIdIsDisabled(System.Int64 _contentId, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByContentIdIsDisabled(null, _contentId, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_ContentId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_contentId"></param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByContentIdIsDisabled(TransactionManager transactionManager, System.Int64 _contentId, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByContentIdIsDisabled(transactionManager, _contentId, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_ContentId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_contentId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByContentIdIsDisabled(TransactionManager transactionManager, System.Int64 _contentId, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByContentIdIsDisabled(transactionManager, _contentId, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_ContentId_IsDisabled index.
		/// </summary>
		/// <param name="_contentId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByContentIdIsDisabled(System.Int64 _contentId, System.Boolean _isDisabled, int start, int pageLength, out int count)
		{
			return GetByContentIdIsDisabled(null, _contentId, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_ContentId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_contentId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public abstract TList<Appointment> GetByContentIdIsDisabled(TransactionManager transactionManager, System.Int64 _contentId, System.Boolean _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_Appointment_CustomerId index.
		/// </summary>
		/// <param name="_customerId"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByCustomerId(System.Int64 _customerId)
		{
			int count = -1;
			return GetByCustomerId(null,_customerId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId index.
		/// </summary>
		/// <param name="_customerId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByCustomerId(System.Int64 _customerId, int start, int pageLength)
		{
			int count = -1;
			return GetByCustomerId(null, _customerId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_customerId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByCustomerId(TransactionManager transactionManager, System.Int64 _customerId)
		{
			int count = -1;
			return GetByCustomerId(transactionManager, _customerId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_customerId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByCustomerId(TransactionManager transactionManager, System.Int64 _customerId, int start, int pageLength)
		{
			int count = -1;
			return GetByCustomerId(transactionManager, _customerId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId index.
		/// </summary>
		/// <param name="_customerId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByCustomerId(System.Int64 _customerId, int start, int pageLength, out int count)
		{
			return GetByCustomerId(null, _customerId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_customerId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public abstract TList<Appointment> GetByCustomerId(TransactionManager transactionManager, System.Int64 _customerId, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_Appointment_CustomerId_ContentId index.
		/// </summary>
		/// <param name="_customerId"></param>
		/// <param name="_contentId"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByCustomerIdContentId(System.Int64 _customerId, System.Int64 _contentId)
		{
			int count = -1;
			return GetByCustomerIdContentId(null,_customerId, _contentId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId_ContentId index.
		/// </summary>
		/// <param name="_customerId"></param>
		/// <param name="_contentId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByCustomerIdContentId(System.Int64 _customerId, System.Int64 _contentId, int start, int pageLength)
		{
			int count = -1;
			return GetByCustomerIdContentId(null, _customerId, _contentId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId_ContentId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_customerId"></param>
		/// <param name="_contentId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByCustomerIdContentId(TransactionManager transactionManager, System.Int64 _customerId, System.Int64 _contentId)
		{
			int count = -1;
			return GetByCustomerIdContentId(transactionManager, _customerId, _contentId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId_ContentId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_customerId"></param>
		/// <param name="_contentId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByCustomerIdContentId(TransactionManager transactionManager, System.Int64 _customerId, System.Int64 _contentId, int start, int pageLength)
		{
			int count = -1;
			return GetByCustomerIdContentId(transactionManager, _customerId, _contentId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId_ContentId index.
		/// </summary>
		/// <param name="_customerId"></param>
		/// <param name="_contentId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByCustomerIdContentId(System.Int64 _customerId, System.Int64 _contentId, int start, int pageLength, out int count)
		{
			return GetByCustomerIdContentId(null, _customerId, _contentId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId_ContentId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_customerId"></param>
		/// <param name="_contentId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public abstract TList<Appointment> GetByCustomerIdContentId(TransactionManager transactionManager, System.Int64 _customerId, System.Int64 _contentId, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_Appointment_CustomerId_ContentId_DoctorId_RoomId_NurseId_StatusId index.
		/// </summary>
		/// <param name="_customerId"></param>
		/// <param name="_contentId"></param>
		/// <param name="_doctorId"></param>
		/// <param name="_roomId"></param>
		/// <param name="_nurseId"></param>
		/// <param name="_statusId"></param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Appointment"/> class.</returns>
		public ClinicDoctor.Entities.Appointment GetByCustomerIdContentIdDoctorIdRoomIdNurseIdStatusId(System.Int64 _customerId, System.Int64 _contentId, System.Int64? _doctorId, System.Int64? _roomId, System.Int64? _nurseId, System.Int64? _statusId)
		{
			int count = -1;
			return GetByCustomerIdContentIdDoctorIdRoomIdNurseIdStatusId(null,_customerId, _contentId, _doctorId, _roomId, _nurseId, _statusId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId_ContentId_DoctorId_RoomId_NurseId_StatusId index.
		/// </summary>
		/// <param name="_customerId"></param>
		/// <param name="_contentId"></param>
		/// <param name="_doctorId"></param>
		/// <param name="_roomId"></param>
		/// <param name="_nurseId"></param>
		/// <param name="_statusId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Appointment"/> class.</returns>
		public ClinicDoctor.Entities.Appointment GetByCustomerIdContentIdDoctorIdRoomIdNurseIdStatusId(System.Int64 _customerId, System.Int64 _contentId, System.Int64? _doctorId, System.Int64? _roomId, System.Int64? _nurseId, System.Int64? _statusId, int start, int pageLength)
		{
			int count = -1;
			return GetByCustomerIdContentIdDoctorIdRoomIdNurseIdStatusId(null, _customerId, _contentId, _doctorId, _roomId, _nurseId, _statusId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId_ContentId_DoctorId_RoomId_NurseId_StatusId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_customerId"></param>
		/// <param name="_contentId"></param>
		/// <param name="_doctorId"></param>
		/// <param name="_roomId"></param>
		/// <param name="_nurseId"></param>
		/// <param name="_statusId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Appointment"/> class.</returns>
		public ClinicDoctor.Entities.Appointment GetByCustomerIdContentIdDoctorIdRoomIdNurseIdStatusId(TransactionManager transactionManager, System.Int64 _customerId, System.Int64 _contentId, System.Int64? _doctorId, System.Int64? _roomId, System.Int64? _nurseId, System.Int64? _statusId)
		{
			int count = -1;
			return GetByCustomerIdContentIdDoctorIdRoomIdNurseIdStatusId(transactionManager, _customerId, _contentId, _doctorId, _roomId, _nurseId, _statusId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId_ContentId_DoctorId_RoomId_NurseId_StatusId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_customerId"></param>
		/// <param name="_contentId"></param>
		/// <param name="_doctorId"></param>
		/// <param name="_roomId"></param>
		/// <param name="_nurseId"></param>
		/// <param name="_statusId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Appointment"/> class.</returns>
		public ClinicDoctor.Entities.Appointment GetByCustomerIdContentIdDoctorIdRoomIdNurseIdStatusId(TransactionManager transactionManager, System.Int64 _customerId, System.Int64 _contentId, System.Int64? _doctorId, System.Int64? _roomId, System.Int64? _nurseId, System.Int64? _statusId, int start, int pageLength)
		{
			int count = -1;
			return GetByCustomerIdContentIdDoctorIdRoomIdNurseIdStatusId(transactionManager, _customerId, _contentId, _doctorId, _roomId, _nurseId, _statusId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId_ContentId_DoctorId_RoomId_NurseId_StatusId index.
		/// </summary>
		/// <param name="_customerId"></param>
		/// <param name="_contentId"></param>
		/// <param name="_doctorId"></param>
		/// <param name="_roomId"></param>
		/// <param name="_nurseId"></param>
		/// <param name="_statusId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Appointment"/> class.</returns>
		public ClinicDoctor.Entities.Appointment GetByCustomerIdContentIdDoctorIdRoomIdNurseIdStatusId(System.Int64 _customerId, System.Int64 _contentId, System.Int64? _doctorId, System.Int64? _roomId, System.Int64? _nurseId, System.Int64? _statusId, int start, int pageLength, out int count)
		{
			return GetByCustomerIdContentIdDoctorIdRoomIdNurseIdStatusId(null, _customerId, _contentId, _doctorId, _roomId, _nurseId, _statusId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId_ContentId_DoctorId_RoomId_NurseId_StatusId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_customerId"></param>
		/// <param name="_contentId"></param>
		/// <param name="_doctorId"></param>
		/// <param name="_roomId"></param>
		/// <param name="_nurseId"></param>
		/// <param name="_statusId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Appointment"/> class.</returns>
		public abstract ClinicDoctor.Entities.Appointment GetByCustomerIdContentIdDoctorIdRoomIdNurseIdStatusId(TransactionManager transactionManager, System.Int64 _customerId, System.Int64 _contentId, System.Int64? _doctorId, System.Int64? _roomId, System.Int64? _nurseId, System.Int64? _statusId, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_Appointment_CustomerId_ContentId_DoctorId_RoomId_NurseId_StatusId_IsComplete index.
		/// </summary>
		/// <param name="_customerId"></param>
		/// <param name="_contentId"></param>
		/// <param name="_doctorId"></param>
		/// <param name="_roomId"></param>
		/// <param name="_nurseId"></param>
		/// <param name="_statusId"></param>
		/// <param name="_isComplete"></param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Appointment"/> class.</returns>
		public ClinicDoctor.Entities.Appointment GetByCustomerIdContentIdDoctorIdRoomIdNurseIdStatusIdIsComplete(System.Int64 _customerId, System.Int64 _contentId, System.Int64? _doctorId, System.Int64? _roomId, System.Int64? _nurseId, System.Int64? _statusId, System.Boolean _isComplete)
		{
			int count = -1;
			return GetByCustomerIdContentIdDoctorIdRoomIdNurseIdStatusIdIsComplete(null,_customerId, _contentId, _doctorId, _roomId, _nurseId, _statusId, _isComplete, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId_ContentId_DoctorId_RoomId_NurseId_StatusId_IsComplete index.
		/// </summary>
		/// <param name="_customerId"></param>
		/// <param name="_contentId"></param>
		/// <param name="_doctorId"></param>
		/// <param name="_roomId"></param>
		/// <param name="_nurseId"></param>
		/// <param name="_statusId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Appointment"/> class.</returns>
		public ClinicDoctor.Entities.Appointment GetByCustomerIdContentIdDoctorIdRoomIdNurseIdStatusIdIsComplete(System.Int64 _customerId, System.Int64 _contentId, System.Int64? _doctorId, System.Int64? _roomId, System.Int64? _nurseId, System.Int64? _statusId, System.Boolean _isComplete, int start, int pageLength)
		{
			int count = -1;
			return GetByCustomerIdContentIdDoctorIdRoomIdNurseIdStatusIdIsComplete(null, _customerId, _contentId, _doctorId, _roomId, _nurseId, _statusId, _isComplete, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId_ContentId_DoctorId_RoomId_NurseId_StatusId_IsComplete index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_customerId"></param>
		/// <param name="_contentId"></param>
		/// <param name="_doctorId"></param>
		/// <param name="_roomId"></param>
		/// <param name="_nurseId"></param>
		/// <param name="_statusId"></param>
		/// <param name="_isComplete"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Appointment"/> class.</returns>
		public ClinicDoctor.Entities.Appointment GetByCustomerIdContentIdDoctorIdRoomIdNurseIdStatusIdIsComplete(TransactionManager transactionManager, System.Int64 _customerId, System.Int64 _contentId, System.Int64? _doctorId, System.Int64? _roomId, System.Int64? _nurseId, System.Int64? _statusId, System.Boolean _isComplete)
		{
			int count = -1;
			return GetByCustomerIdContentIdDoctorIdRoomIdNurseIdStatusIdIsComplete(transactionManager, _customerId, _contentId, _doctorId, _roomId, _nurseId, _statusId, _isComplete, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId_ContentId_DoctorId_RoomId_NurseId_StatusId_IsComplete index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_customerId"></param>
		/// <param name="_contentId"></param>
		/// <param name="_doctorId"></param>
		/// <param name="_roomId"></param>
		/// <param name="_nurseId"></param>
		/// <param name="_statusId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Appointment"/> class.</returns>
		public ClinicDoctor.Entities.Appointment GetByCustomerIdContentIdDoctorIdRoomIdNurseIdStatusIdIsComplete(TransactionManager transactionManager, System.Int64 _customerId, System.Int64 _contentId, System.Int64? _doctorId, System.Int64? _roomId, System.Int64? _nurseId, System.Int64? _statusId, System.Boolean _isComplete, int start, int pageLength)
		{
			int count = -1;
			return GetByCustomerIdContentIdDoctorIdRoomIdNurseIdStatusIdIsComplete(transactionManager, _customerId, _contentId, _doctorId, _roomId, _nurseId, _statusId, _isComplete, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId_ContentId_DoctorId_RoomId_NurseId_StatusId_IsComplete index.
		/// </summary>
		/// <param name="_customerId"></param>
		/// <param name="_contentId"></param>
		/// <param name="_doctorId"></param>
		/// <param name="_roomId"></param>
		/// <param name="_nurseId"></param>
		/// <param name="_statusId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Appointment"/> class.</returns>
		public ClinicDoctor.Entities.Appointment GetByCustomerIdContentIdDoctorIdRoomIdNurseIdStatusIdIsComplete(System.Int64 _customerId, System.Int64 _contentId, System.Int64? _doctorId, System.Int64? _roomId, System.Int64? _nurseId, System.Int64? _statusId, System.Boolean _isComplete, int start, int pageLength, out int count)
		{
			return GetByCustomerIdContentIdDoctorIdRoomIdNurseIdStatusIdIsComplete(null, _customerId, _contentId, _doctorId, _roomId, _nurseId, _statusId, _isComplete, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId_ContentId_DoctorId_RoomId_NurseId_StatusId_IsComplete index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_customerId"></param>
		/// <param name="_contentId"></param>
		/// <param name="_doctorId"></param>
		/// <param name="_roomId"></param>
		/// <param name="_nurseId"></param>
		/// <param name="_statusId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Appointment"/> class.</returns>
		public abstract ClinicDoctor.Entities.Appointment GetByCustomerIdContentIdDoctorIdRoomIdNurseIdStatusIdIsComplete(TransactionManager transactionManager, System.Int64 _customerId, System.Int64 _contentId, System.Int64? _doctorId, System.Int64? _roomId, System.Int64? _nurseId, System.Int64? _statusId, System.Boolean _isComplete, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_Appointment_CustomerId_ContentId_DoctorId_RoomId_NurseId_StatusId_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="_customerId"></param>
		/// <param name="_contentId"></param>
		/// <param name="_doctorId"></param>
		/// <param name="_roomId"></param>
		/// <param name="_nurseId"></param>
		/// <param name="_statusId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Appointment"/> class.</returns>
		public ClinicDoctor.Entities.Appointment GetByCustomerIdContentIdDoctorIdRoomIdNurseIdStatusIdIsCompleteIsDisabled(System.Int64 _customerId, System.Int64 _contentId, System.Int64? _doctorId, System.Int64? _roomId, System.Int64? _nurseId, System.Int64? _statusId, System.Boolean _isComplete, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByCustomerIdContentIdDoctorIdRoomIdNurseIdStatusIdIsCompleteIsDisabled(null,_customerId, _contentId, _doctorId, _roomId, _nurseId, _statusId, _isComplete, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId_ContentId_DoctorId_RoomId_NurseId_StatusId_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="_customerId"></param>
		/// <param name="_contentId"></param>
		/// <param name="_doctorId"></param>
		/// <param name="_roomId"></param>
		/// <param name="_nurseId"></param>
		/// <param name="_statusId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Appointment"/> class.</returns>
		public ClinicDoctor.Entities.Appointment GetByCustomerIdContentIdDoctorIdRoomIdNurseIdStatusIdIsCompleteIsDisabled(System.Int64 _customerId, System.Int64 _contentId, System.Int64? _doctorId, System.Int64? _roomId, System.Int64? _nurseId, System.Int64? _statusId, System.Boolean _isComplete, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByCustomerIdContentIdDoctorIdRoomIdNurseIdStatusIdIsCompleteIsDisabled(null, _customerId, _contentId, _doctorId, _roomId, _nurseId, _statusId, _isComplete, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId_ContentId_DoctorId_RoomId_NurseId_StatusId_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_customerId"></param>
		/// <param name="_contentId"></param>
		/// <param name="_doctorId"></param>
		/// <param name="_roomId"></param>
		/// <param name="_nurseId"></param>
		/// <param name="_statusId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Appointment"/> class.</returns>
		public ClinicDoctor.Entities.Appointment GetByCustomerIdContentIdDoctorIdRoomIdNurseIdStatusIdIsCompleteIsDisabled(TransactionManager transactionManager, System.Int64 _customerId, System.Int64 _contentId, System.Int64? _doctorId, System.Int64? _roomId, System.Int64? _nurseId, System.Int64? _statusId, System.Boolean _isComplete, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByCustomerIdContentIdDoctorIdRoomIdNurseIdStatusIdIsCompleteIsDisabled(transactionManager, _customerId, _contentId, _doctorId, _roomId, _nurseId, _statusId, _isComplete, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId_ContentId_DoctorId_RoomId_NurseId_StatusId_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_customerId"></param>
		/// <param name="_contentId"></param>
		/// <param name="_doctorId"></param>
		/// <param name="_roomId"></param>
		/// <param name="_nurseId"></param>
		/// <param name="_statusId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Appointment"/> class.</returns>
		public ClinicDoctor.Entities.Appointment GetByCustomerIdContentIdDoctorIdRoomIdNurseIdStatusIdIsCompleteIsDisabled(TransactionManager transactionManager, System.Int64 _customerId, System.Int64 _contentId, System.Int64? _doctorId, System.Int64? _roomId, System.Int64? _nurseId, System.Int64? _statusId, System.Boolean _isComplete, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByCustomerIdContentIdDoctorIdRoomIdNurseIdStatusIdIsCompleteIsDisabled(transactionManager, _customerId, _contentId, _doctorId, _roomId, _nurseId, _statusId, _isComplete, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId_ContentId_DoctorId_RoomId_NurseId_StatusId_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="_customerId"></param>
		/// <param name="_contentId"></param>
		/// <param name="_doctorId"></param>
		/// <param name="_roomId"></param>
		/// <param name="_nurseId"></param>
		/// <param name="_statusId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Appointment"/> class.</returns>
		public ClinicDoctor.Entities.Appointment GetByCustomerIdContentIdDoctorIdRoomIdNurseIdStatusIdIsCompleteIsDisabled(System.Int64 _customerId, System.Int64 _contentId, System.Int64? _doctorId, System.Int64? _roomId, System.Int64? _nurseId, System.Int64? _statusId, System.Boolean _isComplete, System.Boolean _isDisabled, int start, int pageLength, out int count)
		{
			return GetByCustomerIdContentIdDoctorIdRoomIdNurseIdStatusIdIsCompleteIsDisabled(null, _customerId, _contentId, _doctorId, _roomId, _nurseId, _statusId, _isComplete, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId_ContentId_DoctorId_RoomId_NurseId_StatusId_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_customerId"></param>
		/// <param name="_contentId"></param>
		/// <param name="_doctorId"></param>
		/// <param name="_roomId"></param>
		/// <param name="_nurseId"></param>
		/// <param name="_statusId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Appointment"/> class.</returns>
		public abstract ClinicDoctor.Entities.Appointment GetByCustomerIdContentIdDoctorIdRoomIdNurseIdStatusIdIsCompleteIsDisabled(TransactionManager transactionManager, System.Int64 _customerId, System.Int64 _contentId, System.Int64? _doctorId, System.Int64? _roomId, System.Int64? _nurseId, System.Int64? _statusId, System.Boolean _isComplete, System.Boolean _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_Appointment_CustomerId_ContentId_DoctorId_RoomId_NurseId_StatusId_IsDisabled index.
		/// </summary>
		/// <param name="_customerId"></param>
		/// <param name="_contentId"></param>
		/// <param name="_doctorId"></param>
		/// <param name="_roomId"></param>
		/// <param name="_nurseId"></param>
		/// <param name="_statusId"></param>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByCustomerIdContentIdDoctorIdRoomIdNurseIdStatusIdIsDisabled(System.Int64 _customerId, System.Int64 _contentId, System.Int64? _doctorId, System.Int64? _roomId, System.Int64? _nurseId, System.Int64? _statusId, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByCustomerIdContentIdDoctorIdRoomIdNurseIdStatusIdIsDisabled(null,_customerId, _contentId, _doctorId, _roomId, _nurseId, _statusId, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId_ContentId_DoctorId_RoomId_NurseId_StatusId_IsDisabled index.
		/// </summary>
		/// <param name="_customerId"></param>
		/// <param name="_contentId"></param>
		/// <param name="_doctorId"></param>
		/// <param name="_roomId"></param>
		/// <param name="_nurseId"></param>
		/// <param name="_statusId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByCustomerIdContentIdDoctorIdRoomIdNurseIdStatusIdIsDisabled(System.Int64 _customerId, System.Int64 _contentId, System.Int64? _doctorId, System.Int64? _roomId, System.Int64? _nurseId, System.Int64? _statusId, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByCustomerIdContentIdDoctorIdRoomIdNurseIdStatusIdIsDisabled(null, _customerId, _contentId, _doctorId, _roomId, _nurseId, _statusId, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId_ContentId_DoctorId_RoomId_NurseId_StatusId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_customerId"></param>
		/// <param name="_contentId"></param>
		/// <param name="_doctorId"></param>
		/// <param name="_roomId"></param>
		/// <param name="_nurseId"></param>
		/// <param name="_statusId"></param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByCustomerIdContentIdDoctorIdRoomIdNurseIdStatusIdIsDisabled(TransactionManager transactionManager, System.Int64 _customerId, System.Int64 _contentId, System.Int64? _doctorId, System.Int64? _roomId, System.Int64? _nurseId, System.Int64? _statusId, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByCustomerIdContentIdDoctorIdRoomIdNurseIdStatusIdIsDisabled(transactionManager, _customerId, _contentId, _doctorId, _roomId, _nurseId, _statusId, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId_ContentId_DoctorId_RoomId_NurseId_StatusId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_customerId"></param>
		/// <param name="_contentId"></param>
		/// <param name="_doctorId"></param>
		/// <param name="_roomId"></param>
		/// <param name="_nurseId"></param>
		/// <param name="_statusId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByCustomerIdContentIdDoctorIdRoomIdNurseIdStatusIdIsDisabled(TransactionManager transactionManager, System.Int64 _customerId, System.Int64 _contentId, System.Int64? _doctorId, System.Int64? _roomId, System.Int64? _nurseId, System.Int64? _statusId, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByCustomerIdContentIdDoctorIdRoomIdNurseIdStatusIdIsDisabled(transactionManager, _customerId, _contentId, _doctorId, _roomId, _nurseId, _statusId, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId_ContentId_DoctorId_RoomId_NurseId_StatusId_IsDisabled index.
		/// </summary>
		/// <param name="_customerId"></param>
		/// <param name="_contentId"></param>
		/// <param name="_doctorId"></param>
		/// <param name="_roomId"></param>
		/// <param name="_nurseId"></param>
		/// <param name="_statusId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByCustomerIdContentIdDoctorIdRoomIdNurseIdStatusIdIsDisabled(System.Int64 _customerId, System.Int64 _contentId, System.Int64? _doctorId, System.Int64? _roomId, System.Int64? _nurseId, System.Int64? _statusId, System.Boolean _isDisabled, int start, int pageLength, out int count)
		{
			return GetByCustomerIdContentIdDoctorIdRoomIdNurseIdStatusIdIsDisabled(null, _customerId, _contentId, _doctorId, _roomId, _nurseId, _statusId, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId_ContentId_DoctorId_RoomId_NurseId_StatusId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_customerId"></param>
		/// <param name="_contentId"></param>
		/// <param name="_doctorId"></param>
		/// <param name="_roomId"></param>
		/// <param name="_nurseId"></param>
		/// <param name="_statusId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public abstract TList<Appointment> GetByCustomerIdContentIdDoctorIdRoomIdNurseIdStatusIdIsDisabled(TransactionManager transactionManager, System.Int64 _customerId, System.Int64 _contentId, System.Int64? _doctorId, System.Int64? _roomId, System.Int64? _nurseId, System.Int64? _statusId, System.Boolean _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_Appointment_CustomerId_ContentId_IsComplete index.
		/// </summary>
		/// <param name="_customerId"></param>
		/// <param name="_contentId"></param>
		/// <param name="_isComplete"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByCustomerIdContentIdIsComplete(System.Int64 _customerId, System.Int64 _contentId, System.Boolean _isComplete)
		{
			int count = -1;
			return GetByCustomerIdContentIdIsComplete(null,_customerId, _contentId, _isComplete, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId_ContentId_IsComplete index.
		/// </summary>
		/// <param name="_customerId"></param>
		/// <param name="_contentId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByCustomerIdContentIdIsComplete(System.Int64 _customerId, System.Int64 _contentId, System.Boolean _isComplete, int start, int pageLength)
		{
			int count = -1;
			return GetByCustomerIdContentIdIsComplete(null, _customerId, _contentId, _isComplete, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId_ContentId_IsComplete index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_customerId"></param>
		/// <param name="_contentId"></param>
		/// <param name="_isComplete"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByCustomerIdContentIdIsComplete(TransactionManager transactionManager, System.Int64 _customerId, System.Int64 _contentId, System.Boolean _isComplete)
		{
			int count = -1;
			return GetByCustomerIdContentIdIsComplete(transactionManager, _customerId, _contentId, _isComplete, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId_ContentId_IsComplete index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_customerId"></param>
		/// <param name="_contentId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByCustomerIdContentIdIsComplete(TransactionManager transactionManager, System.Int64 _customerId, System.Int64 _contentId, System.Boolean _isComplete, int start, int pageLength)
		{
			int count = -1;
			return GetByCustomerIdContentIdIsComplete(transactionManager, _customerId, _contentId, _isComplete, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId_ContentId_IsComplete index.
		/// </summary>
		/// <param name="_customerId"></param>
		/// <param name="_contentId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByCustomerIdContentIdIsComplete(System.Int64 _customerId, System.Int64 _contentId, System.Boolean _isComplete, int start, int pageLength, out int count)
		{
			return GetByCustomerIdContentIdIsComplete(null, _customerId, _contentId, _isComplete, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId_ContentId_IsComplete index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_customerId"></param>
		/// <param name="_contentId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public abstract TList<Appointment> GetByCustomerIdContentIdIsComplete(TransactionManager transactionManager, System.Int64 _customerId, System.Int64 _contentId, System.Boolean _isComplete, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_Appointment_CustomerId_ContentId_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="_customerId"></param>
		/// <param name="_contentId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByCustomerIdContentIdIsCompleteIsDisabled(System.Int64 _customerId, System.Int64 _contentId, System.Boolean _isComplete, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByCustomerIdContentIdIsCompleteIsDisabled(null,_customerId, _contentId, _isComplete, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId_ContentId_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="_customerId"></param>
		/// <param name="_contentId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByCustomerIdContentIdIsCompleteIsDisabled(System.Int64 _customerId, System.Int64 _contentId, System.Boolean _isComplete, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByCustomerIdContentIdIsCompleteIsDisabled(null, _customerId, _contentId, _isComplete, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId_ContentId_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_customerId"></param>
		/// <param name="_contentId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByCustomerIdContentIdIsCompleteIsDisabled(TransactionManager transactionManager, System.Int64 _customerId, System.Int64 _contentId, System.Boolean _isComplete, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByCustomerIdContentIdIsCompleteIsDisabled(transactionManager, _customerId, _contentId, _isComplete, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId_ContentId_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_customerId"></param>
		/// <param name="_contentId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByCustomerIdContentIdIsCompleteIsDisabled(TransactionManager transactionManager, System.Int64 _customerId, System.Int64 _contentId, System.Boolean _isComplete, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByCustomerIdContentIdIsCompleteIsDisabled(transactionManager, _customerId, _contentId, _isComplete, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId_ContentId_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="_customerId"></param>
		/// <param name="_contentId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByCustomerIdContentIdIsCompleteIsDisabled(System.Int64 _customerId, System.Int64 _contentId, System.Boolean _isComplete, System.Boolean _isDisabled, int start, int pageLength, out int count)
		{
			return GetByCustomerIdContentIdIsCompleteIsDisabled(null, _customerId, _contentId, _isComplete, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId_ContentId_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_customerId"></param>
		/// <param name="_contentId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public abstract TList<Appointment> GetByCustomerIdContentIdIsCompleteIsDisabled(TransactionManager transactionManager, System.Int64 _customerId, System.Int64 _contentId, System.Boolean _isComplete, System.Boolean _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_Appointment_CustomerId_IsDisabled index.
		/// </summary>
		/// <param name="_customerId"></param>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByCustomerIdIsDisabled(System.Int64 _customerId, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByCustomerIdIsDisabled(null,_customerId, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId_IsDisabled index.
		/// </summary>
		/// <param name="_customerId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByCustomerIdIsDisabled(System.Int64 _customerId, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByCustomerIdIsDisabled(null, _customerId, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_customerId"></param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByCustomerIdIsDisabled(TransactionManager transactionManager, System.Int64 _customerId, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByCustomerIdIsDisabled(transactionManager, _customerId, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_customerId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByCustomerIdIsDisabled(TransactionManager transactionManager, System.Int64 _customerId, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByCustomerIdIsDisabled(transactionManager, _customerId, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId_IsDisabled index.
		/// </summary>
		/// <param name="_customerId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByCustomerIdIsDisabled(System.Int64 _customerId, System.Boolean _isDisabled, int start, int pageLength, out int count)
		{
			return GetByCustomerIdIsDisabled(null, _customerId, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_CustomerId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_customerId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public abstract TList<Appointment> GetByCustomerIdIsDisabled(TransactionManager transactionManager, System.Int64 _customerId, System.Boolean _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_Appointment_DoctorId index.
		/// </summary>
		/// <param name="_doctorId"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByDoctorId(System.Int64? _doctorId)
		{
			int count = -1;
			return GetByDoctorId(null,_doctorId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_DoctorId index.
		/// </summary>
		/// <param name="_doctorId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByDoctorId(System.Int64? _doctorId, int start, int pageLength)
		{
			int count = -1;
			return GetByDoctorId(null, _doctorId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_DoctorId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByDoctorId(TransactionManager transactionManager, System.Int64? _doctorId)
		{
			int count = -1;
			return GetByDoctorId(transactionManager, _doctorId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_DoctorId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByDoctorId(TransactionManager transactionManager, System.Int64? _doctorId, int start, int pageLength)
		{
			int count = -1;
			return GetByDoctorId(transactionManager, _doctorId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_DoctorId index.
		/// </summary>
		/// <param name="_doctorId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByDoctorId(System.Int64? _doctorId, int start, int pageLength, out int count)
		{
			return GetByDoctorId(null, _doctorId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_DoctorId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public abstract TList<Appointment> GetByDoctorId(TransactionManager transactionManager, System.Int64? _doctorId, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_Appointment_DoctorId_IsDisabled index.
		/// </summary>
		/// <param name="_doctorId"></param>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByDoctorIdIsDisabled(System.Int64? _doctorId, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByDoctorIdIsDisabled(null,_doctorId, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_DoctorId_IsDisabled index.
		/// </summary>
		/// <param name="_doctorId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByDoctorIdIsDisabled(System.Int64? _doctorId, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByDoctorIdIsDisabled(null, _doctorId, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_DoctorId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorId"></param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByDoctorIdIsDisabled(TransactionManager transactionManager, System.Int64? _doctorId, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByDoctorIdIsDisabled(transactionManager, _doctorId, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_DoctorId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByDoctorIdIsDisabled(TransactionManager transactionManager, System.Int64? _doctorId, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByDoctorIdIsDisabled(transactionManager, _doctorId, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_DoctorId_IsDisabled index.
		/// </summary>
		/// <param name="_doctorId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByDoctorIdIsDisabled(System.Int64? _doctorId, System.Boolean _isDisabled, int start, int pageLength, out int count)
		{
			return GetByDoctorIdIsDisabled(null, _doctorId, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_DoctorId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public abstract TList<Appointment> GetByDoctorIdIsDisabled(TransactionManager transactionManager, System.Int64? _doctorId, System.Boolean _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_Appointment_Id_IsDisabled index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Appointment"/> class.</returns>
		public ClinicDoctor.Entities.Appointment GetByIdIsDisabled(System.String _id, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByIdIsDisabled(null,_id, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_Id_IsDisabled index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Appointment"/> class.</returns>
		public ClinicDoctor.Entities.Appointment GetByIdIsDisabled(System.String _id, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIdIsDisabled(null, _id, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_Id_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Appointment"/> class.</returns>
		public ClinicDoctor.Entities.Appointment GetByIdIsDisabled(TransactionManager transactionManager, System.String _id, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByIdIsDisabled(transactionManager, _id, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_Id_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Appointment"/> class.</returns>
		public ClinicDoctor.Entities.Appointment GetByIdIsDisabled(TransactionManager transactionManager, System.String _id, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIdIsDisabled(transactionManager, _id, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_Id_IsDisabled index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Appointment"/> class.</returns>
		public ClinicDoctor.Entities.Appointment GetByIdIsDisabled(System.String _id, System.Boolean _isDisabled, int start, int pageLength, out int count)
		{
			return GetByIdIsDisabled(null, _id, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_Id_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Appointment"/> class.</returns>
		public abstract ClinicDoctor.Entities.Appointment GetByIdIsDisabled(TransactionManager transactionManager, System.String _id, System.Boolean _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_Appointment_IsDisabled index.
		/// </summary>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByIsDisabled(System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByIsDisabled(null,_isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_IsDisabled index.
		/// </summary>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByIsDisabled(System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIsDisabled(null, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByIsDisabled(TransactionManager transactionManager, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByIsDisabled(transactionManager, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByIsDisabled(TransactionManager transactionManager, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIsDisabled(transactionManager, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_IsDisabled index.
		/// </summary>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByIsDisabled(System.Boolean _isDisabled, int start, int pageLength, out int count)
		{
			return GetByIsDisabled(null, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public abstract TList<Appointment> GetByIsDisabled(TransactionManager transactionManager, System.Boolean _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_Appointment_NurseId index.
		/// </summary>
		/// <param name="_nurseId"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByNurseId(System.Int64? _nurseId)
		{
			int count = -1;
			return GetByNurseId(null,_nurseId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_NurseId index.
		/// </summary>
		/// <param name="_nurseId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByNurseId(System.Int64? _nurseId, int start, int pageLength)
		{
			int count = -1;
			return GetByNurseId(null, _nurseId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_NurseId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_nurseId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByNurseId(TransactionManager transactionManager, System.Int64? _nurseId)
		{
			int count = -1;
			return GetByNurseId(transactionManager, _nurseId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_NurseId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_nurseId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByNurseId(TransactionManager transactionManager, System.Int64? _nurseId, int start, int pageLength)
		{
			int count = -1;
			return GetByNurseId(transactionManager, _nurseId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_NurseId index.
		/// </summary>
		/// <param name="_nurseId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByNurseId(System.Int64? _nurseId, int start, int pageLength, out int count)
		{
			return GetByNurseId(null, _nurseId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_NurseId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_nurseId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public abstract TList<Appointment> GetByNurseId(TransactionManager transactionManager, System.Int64? _nurseId, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_Appointment_NurseId_IsDisabled index.
		/// </summary>
		/// <param name="_nurseId"></param>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByNurseIdIsDisabled(System.Int64? _nurseId, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByNurseIdIsDisabled(null,_nurseId, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_NurseId_IsDisabled index.
		/// </summary>
		/// <param name="_nurseId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByNurseIdIsDisabled(System.Int64? _nurseId, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByNurseIdIsDisabled(null, _nurseId, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_NurseId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_nurseId"></param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByNurseIdIsDisabled(TransactionManager transactionManager, System.Int64? _nurseId, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByNurseIdIsDisabled(transactionManager, _nurseId, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_NurseId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_nurseId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByNurseIdIsDisabled(TransactionManager transactionManager, System.Int64? _nurseId, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByNurseIdIsDisabled(transactionManager, _nurseId, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_NurseId_IsDisabled index.
		/// </summary>
		/// <param name="_nurseId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByNurseIdIsDisabled(System.Int64? _nurseId, System.Boolean _isDisabled, int start, int pageLength, out int count)
		{
			return GetByNurseIdIsDisabled(null, _nurseId, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_NurseId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_nurseId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public abstract TList<Appointment> GetByNurseIdIsDisabled(TransactionManager transactionManager, System.Int64? _nurseId, System.Boolean _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_Appointment_RoomId index.
		/// </summary>
		/// <param name="_roomId"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByRoomId(System.Int64? _roomId)
		{
			int count = -1;
			return GetByRoomId(null,_roomId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_RoomId index.
		/// </summary>
		/// <param name="_roomId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByRoomId(System.Int64? _roomId, int start, int pageLength)
		{
			int count = -1;
			return GetByRoomId(null, _roomId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_RoomId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_roomId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByRoomId(TransactionManager transactionManager, System.Int64? _roomId)
		{
			int count = -1;
			return GetByRoomId(transactionManager, _roomId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_RoomId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_roomId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByRoomId(TransactionManager transactionManager, System.Int64? _roomId, int start, int pageLength)
		{
			int count = -1;
			return GetByRoomId(transactionManager, _roomId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_RoomId index.
		/// </summary>
		/// <param name="_roomId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByRoomId(System.Int64? _roomId, int start, int pageLength, out int count)
		{
			return GetByRoomId(null, _roomId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_RoomId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_roomId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public abstract TList<Appointment> GetByRoomId(TransactionManager transactionManager, System.Int64? _roomId, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_Appointment_RoomId_IsDisabled index.
		/// </summary>
		/// <param name="_roomId"></param>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByRoomIdIsDisabled(System.Int64? _roomId, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByRoomIdIsDisabled(null,_roomId, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_RoomId_IsDisabled index.
		/// </summary>
		/// <param name="_roomId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByRoomIdIsDisabled(System.Int64? _roomId, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByRoomIdIsDisabled(null, _roomId, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_RoomId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_roomId"></param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByRoomIdIsDisabled(TransactionManager transactionManager, System.Int64? _roomId, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByRoomIdIsDisabled(transactionManager, _roomId, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_RoomId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_roomId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByRoomIdIsDisabled(TransactionManager transactionManager, System.Int64? _roomId, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByRoomIdIsDisabled(transactionManager, _roomId, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_RoomId_IsDisabled index.
		/// </summary>
		/// <param name="_roomId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByRoomIdIsDisabled(System.Int64? _roomId, System.Boolean _isDisabled, int start, int pageLength, out int count)
		{
			return GetByRoomIdIsDisabled(null, _roomId, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_RoomId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_roomId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public abstract TList<Appointment> GetByRoomIdIsDisabled(TransactionManager transactionManager, System.Int64? _roomId, System.Boolean _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_Appointment_StatusId index.
		/// </summary>
		/// <param name="_statusId"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByStatusId(System.Int64? _statusId)
		{
			int count = -1;
			return GetByStatusId(null,_statusId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_StatusId index.
		/// </summary>
		/// <param name="_statusId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByStatusId(System.Int64? _statusId, int start, int pageLength)
		{
			int count = -1;
			return GetByStatusId(null, _statusId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_StatusId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_statusId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByStatusId(TransactionManager transactionManager, System.Int64? _statusId)
		{
			int count = -1;
			return GetByStatusId(transactionManager, _statusId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_StatusId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_statusId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByStatusId(TransactionManager transactionManager, System.Int64? _statusId, int start, int pageLength)
		{
			int count = -1;
			return GetByStatusId(transactionManager, _statusId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_StatusId index.
		/// </summary>
		/// <param name="_statusId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByStatusId(System.Int64? _statusId, int start, int pageLength, out int count)
		{
			return GetByStatusId(null, _statusId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_StatusId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_statusId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public abstract TList<Appointment> GetByStatusId(TransactionManager transactionManager, System.Int64? _statusId, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_Appointment_StatusId_IsDisabled index.
		/// </summary>
		/// <param name="_statusId"></param>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByStatusIdIsDisabled(System.Int64? _statusId, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByStatusIdIsDisabled(null,_statusId, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_StatusId_IsDisabled index.
		/// </summary>
		/// <param name="_statusId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByStatusIdIsDisabled(System.Int64? _statusId, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByStatusIdIsDisabled(null, _statusId, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_StatusId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_statusId"></param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByStatusIdIsDisabled(TransactionManager transactionManager, System.Int64? _statusId, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByStatusIdIsDisabled(transactionManager, _statusId, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_StatusId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_statusId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByStatusIdIsDisabled(TransactionManager transactionManager, System.Int64? _statusId, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByStatusIdIsDisabled(transactionManager, _statusId, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_StatusId_IsDisabled index.
		/// </summary>
		/// <param name="_statusId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByStatusIdIsDisabled(System.Int64? _statusId, System.Boolean _isDisabled, int start, int pageLength, out int count)
		{
			return GetByStatusIdIsDisabled(null, _statusId, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_StatusId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_statusId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public abstract TList<Appointment> GetByStatusIdIsDisabled(TransactionManager transactionManager, System.Int64? _statusId, System.Boolean _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Appointment_1 index.
		/// </summary>
		/// <param name="_id"></param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Appointment"/> class.</returns>
		public ClinicDoctor.Entities.Appointment GetById(System.String _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Appointment_1 index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Appointment"/> class.</returns>
		public ClinicDoctor.Entities.Appointment GetById(System.String _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Appointment_1 index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Appointment"/> class.</returns>
		public ClinicDoctor.Entities.Appointment GetById(TransactionManager transactionManager, System.String _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Appointment_1 index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Appointment"/> class.</returns>
		public ClinicDoctor.Entities.Appointment GetById(TransactionManager transactionManager, System.String _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Appointment_1 index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Appointment"/> class.</returns>
		public ClinicDoctor.Entities.Appointment GetById(System.String _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Appointment_1 index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Appointment"/> class.</returns>
		public abstract ClinicDoctor.Entities.Appointment GetById(TransactionManager transactionManager, System.String _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Appointment&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Appointment&gt;"/></returns>
		public static TList<Appointment> Fill(IDataReader reader, TList<Appointment> rows, int start, int pageLength)
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
				
				ClinicDoctor.Entities.Appointment c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Appointment")
					.Append("|").Append((System.String)reader[((int)AppointmentColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Appointment>(
					key.ToString(), // EntityTrackingKey
					"Appointment",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new ClinicDoctor.Entities.Appointment();
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
					c.Id = (System.String)reader[((int)AppointmentColumn.Id - 1)];
					c.OriginalId = c.Id;
					c.CustomerId = (System.Int64)reader[((int)AppointmentColumn.CustomerId - 1)];
					c.ContentId = (System.Int64)reader[((int)AppointmentColumn.ContentId - 1)];
					c.DoctorId = (reader.IsDBNull(((int)AppointmentColumn.DoctorId - 1)))?null:(System.Int64?)reader[((int)AppointmentColumn.DoctorId - 1)];
					c.RoomId = (reader.IsDBNull(((int)AppointmentColumn.RoomId - 1)))?null:(System.Int64?)reader[((int)AppointmentColumn.RoomId - 1)];
					c.NurseId = (reader.IsDBNull(((int)AppointmentColumn.NurseId - 1)))?null:(System.Int64?)reader[((int)AppointmentColumn.NurseId - 1)];
					c.StatusId = (reader.IsDBNull(((int)AppointmentColumn.StatusId - 1)))?null:(System.Int64?)reader[((int)AppointmentColumn.StatusId - 1)];
					c.Note = (reader.IsDBNull(((int)AppointmentColumn.Note - 1)))?null:(System.String)reader[((int)AppointmentColumn.Note - 1)];
					c.StartTime = (reader.IsDBNull(((int)AppointmentColumn.StartTime - 1)))?null:(System.DateTime?)reader[((int)AppointmentColumn.StartTime - 1)];
					c.EndTime = (reader.IsDBNull(((int)AppointmentColumn.EndTime - 1)))?null:(System.DateTime?)reader[((int)AppointmentColumn.EndTime - 1)];
					c.IsComplete = (System.Boolean)reader[((int)AppointmentColumn.IsComplete - 1)];
					c.IsDisabled = (System.Boolean)reader[((int)AppointmentColumn.IsDisabled - 1)];
					c.CreateUser = (reader.IsDBNull(((int)AppointmentColumn.CreateUser - 1)))?null:(System.String)reader[((int)AppointmentColumn.CreateUser - 1)];
					c.CreateDate = (System.DateTime)reader[((int)AppointmentColumn.CreateDate - 1)];
					c.UpdateUser = (reader.IsDBNull(((int)AppointmentColumn.UpdateUser - 1)))?null:(System.String)reader[((int)AppointmentColumn.UpdateUser - 1)];
					c.UpdateDate = (System.DateTime)reader[((int)AppointmentColumn.UpdateDate - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="ClinicDoctor.Entities.Appointment"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="ClinicDoctor.Entities.Appointment"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, ClinicDoctor.Entities.Appointment entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (System.String)reader[((int)AppointmentColumn.Id - 1)];
			entity.OriginalId = (System.String)reader["Id"];
			entity.CustomerId = (System.Int64)reader[((int)AppointmentColumn.CustomerId - 1)];
			entity.ContentId = (System.Int64)reader[((int)AppointmentColumn.ContentId - 1)];
			entity.DoctorId = (reader.IsDBNull(((int)AppointmentColumn.DoctorId - 1)))?null:(System.Int64?)reader[((int)AppointmentColumn.DoctorId - 1)];
			entity.RoomId = (reader.IsDBNull(((int)AppointmentColumn.RoomId - 1)))?null:(System.Int64?)reader[((int)AppointmentColumn.RoomId - 1)];
			entity.NurseId = (reader.IsDBNull(((int)AppointmentColumn.NurseId - 1)))?null:(System.Int64?)reader[((int)AppointmentColumn.NurseId - 1)];
			entity.StatusId = (reader.IsDBNull(((int)AppointmentColumn.StatusId - 1)))?null:(System.Int64?)reader[((int)AppointmentColumn.StatusId - 1)];
			entity.Note = (reader.IsDBNull(((int)AppointmentColumn.Note - 1)))?null:(System.String)reader[((int)AppointmentColumn.Note - 1)];
			entity.StartTime = (reader.IsDBNull(((int)AppointmentColumn.StartTime - 1)))?null:(System.DateTime?)reader[((int)AppointmentColumn.StartTime - 1)];
			entity.EndTime = (reader.IsDBNull(((int)AppointmentColumn.EndTime - 1)))?null:(System.DateTime?)reader[((int)AppointmentColumn.EndTime - 1)];
			entity.IsComplete = (System.Boolean)reader[((int)AppointmentColumn.IsComplete - 1)];
			entity.IsDisabled = (System.Boolean)reader[((int)AppointmentColumn.IsDisabled - 1)];
			entity.CreateUser = (reader.IsDBNull(((int)AppointmentColumn.CreateUser - 1)))?null:(System.String)reader[((int)AppointmentColumn.CreateUser - 1)];
			entity.CreateDate = (System.DateTime)reader[((int)AppointmentColumn.CreateDate - 1)];
			entity.UpdateUser = (reader.IsDBNull(((int)AppointmentColumn.UpdateUser - 1)))?null:(System.String)reader[((int)AppointmentColumn.UpdateUser - 1)];
			entity.UpdateDate = (System.DateTime)reader[((int)AppointmentColumn.UpdateDate - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="ClinicDoctor.Entities.Appointment"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="ClinicDoctor.Entities.Appointment"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, ClinicDoctor.Entities.Appointment entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Id = (System.String)dataRow["Id"];
			entity.OriginalId = (System.String)dataRow["Id"];
			entity.CustomerId = (System.Int64)dataRow["CustomerId"];
			entity.ContentId = (System.Int64)dataRow["ContentId"];
			entity.DoctorId = Convert.IsDBNull(dataRow["DoctorId"]) ? null : (System.Int64?)dataRow["DoctorId"];
			entity.RoomId = Convert.IsDBNull(dataRow["RoomId"]) ? null : (System.Int64?)dataRow["RoomId"];
			entity.NurseId = Convert.IsDBNull(dataRow["NurseId"]) ? null : (System.Int64?)dataRow["NurseId"];
			entity.StatusId = Convert.IsDBNull(dataRow["StatusId"]) ? null : (System.Int64?)dataRow["StatusId"];
			entity.Note = Convert.IsDBNull(dataRow["Note"]) ? null : (System.String)dataRow["Note"];
			entity.StartTime = Convert.IsDBNull(dataRow["StartTime"]) ? null : (System.DateTime?)dataRow["StartTime"];
			entity.EndTime = Convert.IsDBNull(dataRow["EndTime"]) ? null : (System.DateTime?)dataRow["EndTime"];
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
		/// <param name="entity">The <see cref="ClinicDoctor.Entities.Appointment"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">ClinicDoctor.Entities.Appointment Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, ClinicDoctor.Entities.Appointment entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region ContentIdSource	
			if (CanDeepLoad(entity, "Content|ContentIdSource", deepLoadType, innerList) 
				&& entity.ContentIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = entity.ContentId;
				Content tmpEntity = EntityManager.LocateEntity<Content>(EntityLocator.ConstructKeyFromPkItems(typeof(Content), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.ContentIdSource = tmpEntity;
				else
					entity.ContentIdSource = DataRepository.ContentProvider.GetById(transactionManager, entity.ContentId);		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'ContentIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.ContentIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.ContentProvider.DeepLoad(transactionManager, entity.ContentIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion ContentIdSource

			#region CustomerIdSource	
			if (CanDeepLoad(entity, "Customer|CustomerIdSource", deepLoadType, innerList) 
				&& entity.CustomerIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = entity.CustomerId;
				Customer tmpEntity = EntityManager.LocateEntity<Customer>(EntityLocator.ConstructKeyFromPkItems(typeof(Customer), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.CustomerIdSource = tmpEntity;
				else
					entity.CustomerIdSource = DataRepository.CustomerProvider.GetById(transactionManager, entity.CustomerId);		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'CustomerIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.CustomerIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.CustomerProvider.DeepLoad(transactionManager, entity.CustomerIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion CustomerIdSource

			#region RoomIdSource	
			if (CanDeepLoad(entity, "Room|RoomIdSource", deepLoadType, innerList) 
				&& entity.RoomIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.RoomId ?? (long)0);
				Room tmpEntity = EntityManager.LocateEntity<Room>(EntityLocator.ConstructKeyFromPkItems(typeof(Room), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.RoomIdSource = tmpEntity;
				else
					entity.RoomIdSource = DataRepository.RoomProvider.GetById(transactionManager, (entity.RoomId ?? (long)0));		
				
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

			#region DoctorIdSource	
			if (CanDeepLoad(entity, "Staff|DoctorIdSource", deepLoadType, innerList) 
				&& entity.DoctorIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.DoctorId ?? (long)0);
				Staff tmpEntity = EntityManager.LocateEntity<Staff>(EntityLocator.ConstructKeyFromPkItems(typeof(Staff), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.DoctorIdSource = tmpEntity;
				else
					entity.DoctorIdSource = DataRepository.StaffProvider.GetById(transactionManager, (entity.DoctorId ?? (long)0));		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'DoctorIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.DoctorIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.StaffProvider.DeepLoad(transactionManager, entity.DoctorIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion DoctorIdSource

			#region NurseIdSource	
			if (CanDeepLoad(entity, "Staff|NurseIdSource", deepLoadType, innerList) 
				&& entity.NurseIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.NurseId ?? (long)0);
				Staff tmpEntity = EntityManager.LocateEntity<Staff>(EntityLocator.ConstructKeyFromPkItems(typeof(Staff), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.NurseIdSource = tmpEntity;
				else
					entity.NurseIdSource = DataRepository.StaffProvider.GetById(transactionManager, (entity.NurseId ?? (long)0));		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'NurseIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.NurseIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.StaffProvider.DeepLoad(transactionManager, entity.NurseIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion NurseIdSource

			#region StatusIdSource	
			if (CanDeepLoad(entity, "Status|StatusIdSource", deepLoadType, innerList) 
				&& entity.StatusIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.StatusId ?? (long)0);
				Status tmpEntity = EntityManager.LocateEntity<Status>(EntityLocator.ConstructKeyFromPkItems(typeof(Status), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.StatusIdSource = tmpEntity;
				else
					entity.StatusIdSource = DataRepository.StatusProvider.GetById(transactionManager, (entity.StatusId ?? (long)0));		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'StatusIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.StatusIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.StatusProvider.DeepLoad(transactionManager, entity.StatusIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion StatusIdSource
			
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
		/// Deep Save the entire object graph of the ClinicDoctor.Entities.Appointment object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">ClinicDoctor.Entities.Appointment instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">ClinicDoctor.Entities.Appointment Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, ClinicDoctor.Entities.Appointment entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			
			#region ContentIdSource
			if (CanDeepSave(entity, "Content|ContentIdSource", deepSaveType, innerList) 
				&& entity.ContentIdSource != null)
			{
				DataRepository.ContentProvider.Save(transactionManager, entity.ContentIdSource);
				entity.ContentId = entity.ContentIdSource.Id;
			}
			#endregion 
			
			#region CustomerIdSource
			if (CanDeepSave(entity, "Customer|CustomerIdSource", deepSaveType, innerList) 
				&& entity.CustomerIdSource != null)
			{
				DataRepository.CustomerProvider.Save(transactionManager, entity.CustomerIdSource);
				entity.CustomerId = entity.CustomerIdSource.Id;
			}
			#endregion 
			
			#region RoomIdSource
			if (CanDeepSave(entity, "Room|RoomIdSource", deepSaveType, innerList) 
				&& entity.RoomIdSource != null)
			{
				DataRepository.RoomProvider.Save(transactionManager, entity.RoomIdSource);
				entity.RoomId = entity.RoomIdSource.Id;
			}
			#endregion 
			
			#region DoctorIdSource
			if (CanDeepSave(entity, "Staff|DoctorIdSource", deepSaveType, innerList) 
				&& entity.DoctorIdSource != null)
			{
				DataRepository.StaffProvider.Save(transactionManager, entity.DoctorIdSource);
				entity.DoctorId = entity.DoctorIdSource.Id;
			}
			#endregion 
			
			#region NurseIdSource
			if (CanDeepSave(entity, "Staff|NurseIdSource", deepSaveType, innerList) 
				&& entity.NurseIdSource != null)
			{
				DataRepository.StaffProvider.Save(transactionManager, entity.NurseIdSource);
				entity.NurseId = entity.NurseIdSource.Id;
			}
			#endregion 
			
			#region StatusIdSource
			if (CanDeepSave(entity, "Status|StatusIdSource", deepSaveType, innerList) 
				&& entity.StatusIdSource != null)
			{
				DataRepository.StatusProvider.Save(transactionManager, entity.StatusIdSource);
				entity.StatusId = entity.StatusIdSource.Id;
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
	
	#region AppointmentChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>ClinicDoctor.Entities.Appointment</c>
	///</summary>
	public enum AppointmentChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Content</c> at ContentIdSource
		///</summary>
		[ChildEntityType(typeof(Content))]
		Content,
			
		///<summary>
		/// Composite Property for <c>Customer</c> at CustomerIdSource
		///</summary>
		[ChildEntityType(typeof(Customer))]
		Customer,
			
		///<summary>
		/// Composite Property for <c>Room</c> at RoomIdSource
		///</summary>
		[ChildEntityType(typeof(Room))]
		Room,
			
		///<summary>
		/// Composite Property for <c>Staff</c> at DoctorIdSource
		///</summary>
		[ChildEntityType(typeof(Staff))]
		Staff,
			
		///<summary>
		/// Composite Property for <c>Status</c> at StatusIdSource
		///</summary>
		[ChildEntityType(typeof(Status))]
		Status,
		}
	
	#endregion AppointmentChildEntityTypes
	
	#region AppointmentFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;AppointmentColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Appointment"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class AppointmentFilterBuilder : SqlFilterBuilder<AppointmentColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the AppointmentFilterBuilder class.
		/// </summary>
		public AppointmentFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the AppointmentFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public AppointmentFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the AppointmentFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public AppointmentFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion AppointmentFilterBuilder
	
	#region AppointmentParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;AppointmentColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Appointment"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class AppointmentParameterBuilder : ParameterizedSqlFilterBuilder<AppointmentColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the AppointmentParameterBuilder class.
		/// </summary>
		public AppointmentParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the AppointmentParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public AppointmentParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the AppointmentParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public AppointmentParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion AppointmentParameterBuilder
	
	#region AppointmentSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;AppointmentColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Appointment"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class AppointmentSortBuilder : SqlSortBuilder<AppointmentColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the AppointmentSqlSortBuilder class.
		/// </summary>
		public AppointmentSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion AppointmentSortBuilder
	
} // end namespace
