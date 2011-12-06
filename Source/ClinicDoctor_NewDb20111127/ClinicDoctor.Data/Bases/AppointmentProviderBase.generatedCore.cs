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
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Appointment_Staff1 key.
		///		FK_Appointment_Staff1 Description: 
		/// </summary>
		/// <param name="_nurseUsername"></param>
		/// <returns>Returns a typed collection of ClinicDoctor.Entities.Appointment objects.</returns>
		public TList<Appointment> GetByNurseUsername(System.String _nurseUsername)
		{
			int count = -1;
			return GetByNurseUsername(_nurseUsername, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Appointment_Staff1 key.
		///		FK_Appointment_Staff1 Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_nurseUsername"></param>
		/// <returns>Returns a typed collection of ClinicDoctor.Entities.Appointment objects.</returns>
		/// <remarks></remarks>
		public TList<Appointment> GetByNurseUsername(TransactionManager transactionManager, System.String _nurseUsername)
		{
			int count = -1;
			return GetByNurseUsername(transactionManager, _nurseUsername, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_Appointment_Staff1 key.
		///		FK_Appointment_Staff1 Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_nurseUsername"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of ClinicDoctor.Entities.Appointment objects.</returns>
		public TList<Appointment> GetByNurseUsername(TransactionManager transactionManager, System.String _nurseUsername, int start, int pageLength)
		{
			int count = -1;
			return GetByNurseUsername(transactionManager, _nurseUsername, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Appointment_Staff1 key.
		///		fkAppointmentStaff1 Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_nurseUsername"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of ClinicDoctor.Entities.Appointment objects.</returns>
		public TList<Appointment> GetByNurseUsername(System.String _nurseUsername, int start, int pageLength)
		{
			int count =  -1;
			return GetByNurseUsername(null, _nurseUsername, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Appointment_Staff1 key.
		///		fkAppointmentStaff1 Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_nurseUsername"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of ClinicDoctor.Entities.Appointment objects.</returns>
		public TList<Appointment> GetByNurseUsername(System.String _nurseUsername, int start, int pageLength,out int count)
		{
			return GetByNurseUsername(null, _nurseUsername, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Appointment_Staff1 key.
		///		FK_Appointment_Staff1 Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_nurseUsername"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of ClinicDoctor.Entities.Appointment objects.</returns>
		public abstract TList<Appointment> GetByNurseUsername(TransactionManager transactionManager, System.String _nurseUsername, int start, int pageLength, out int count);
		
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
		/// 	Gets rows from the datasource based on the primary key IX_Appointment_DoctorUsername index.
		/// </summary>
		/// <param name="_doctorUsername"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByDoctorUsername(System.String _doctorUsername)
		{
			int count = -1;
			return GetByDoctorUsername(null,_doctorUsername, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_DoctorUsername index.
		/// </summary>
		/// <param name="_doctorUsername"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByDoctorUsername(System.String _doctorUsername, int start, int pageLength)
		{
			int count = -1;
			return GetByDoctorUsername(null, _doctorUsername, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_DoctorUsername index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUsername"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByDoctorUsername(TransactionManager transactionManager, System.String _doctorUsername)
		{
			int count = -1;
			return GetByDoctorUsername(transactionManager, _doctorUsername, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_DoctorUsername index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUsername"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByDoctorUsername(TransactionManager transactionManager, System.String _doctorUsername, int start, int pageLength)
		{
			int count = -1;
			return GetByDoctorUsername(transactionManager, _doctorUsername, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_DoctorUsername index.
		/// </summary>
		/// <param name="_doctorUsername"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByDoctorUsername(System.String _doctorUsername, int start, int pageLength, out int count)
		{
			return GetByDoctorUsername(null, _doctorUsername, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_DoctorUsername index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUsername"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public abstract TList<Appointment> GetByDoctorUsername(TransactionManager transactionManager, System.String _doctorUsername, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_Appointment_DoctorUsername_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="_doctorUsername"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByDoctorUsernameIsCompleteIsDisabled(System.String _doctorUsername, System.Boolean _isComplete, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByDoctorUsernameIsCompleteIsDisabled(null,_doctorUsername, _isComplete, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_DoctorUsername_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="_doctorUsername"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByDoctorUsernameIsCompleteIsDisabled(System.String _doctorUsername, System.Boolean _isComplete, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByDoctorUsernameIsCompleteIsDisabled(null, _doctorUsername, _isComplete, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_DoctorUsername_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUsername"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByDoctorUsernameIsCompleteIsDisabled(TransactionManager transactionManager, System.String _doctorUsername, System.Boolean _isComplete, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByDoctorUsernameIsCompleteIsDisabled(transactionManager, _doctorUsername, _isComplete, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_DoctorUsername_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUsername"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByDoctorUsernameIsCompleteIsDisabled(TransactionManager transactionManager, System.String _doctorUsername, System.Boolean _isComplete, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByDoctorUsernameIsCompleteIsDisabled(transactionManager, _doctorUsername, _isComplete, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_DoctorUsername_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="_doctorUsername"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByDoctorUsernameIsCompleteIsDisabled(System.String _doctorUsername, System.Boolean _isComplete, System.Boolean _isDisabled, int start, int pageLength, out int count)
		{
			return GetByDoctorUsernameIsCompleteIsDisabled(null, _doctorUsername, _isComplete, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_DoctorUsername_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUsername"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public abstract TList<Appointment> GetByDoctorUsernameIsCompleteIsDisabled(TransactionManager transactionManager, System.String _doctorUsername, System.Boolean _isComplete, System.Boolean _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_Appointment_DoctorUsername_IsDisabled index.
		/// </summary>
		/// <param name="_doctorUsername"></param>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByDoctorUsernameIsDisabled(System.String _doctorUsername, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByDoctorUsernameIsDisabled(null,_doctorUsername, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_DoctorUsername_IsDisabled index.
		/// </summary>
		/// <param name="_doctorUsername"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByDoctorUsernameIsDisabled(System.String _doctorUsername, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByDoctorUsernameIsDisabled(null, _doctorUsername, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_DoctorUsername_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUsername"></param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByDoctorUsernameIsDisabled(TransactionManager transactionManager, System.String _doctorUsername, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByDoctorUsernameIsDisabled(transactionManager, _doctorUsername, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_DoctorUsername_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUsername"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByDoctorUsernameIsDisabled(TransactionManager transactionManager, System.String _doctorUsername, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByDoctorUsernameIsDisabled(transactionManager, _doctorUsername, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_DoctorUsername_IsDisabled index.
		/// </summary>
		/// <param name="_doctorUsername"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByDoctorUsernameIsDisabled(System.String _doctorUsername, System.Boolean _isDisabled, int start, int pageLength, out int count)
		{
			return GetByDoctorUsernameIsDisabled(null, _doctorUsername, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_DoctorUsername_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUsername"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public abstract TList<Appointment> GetByDoctorUsernameIsDisabled(TransactionManager transactionManager, System.String _doctorUsername, System.Boolean _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_Appointment_DoctorUsername_StatusId index.
		/// </summary>
		/// <param name="_doctorUsername"></param>
		/// <param name="_statusId"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByDoctorUsernameStatusId(System.String _doctorUsername, System.Int64? _statusId)
		{
			int count = -1;
			return GetByDoctorUsernameStatusId(null,_doctorUsername, _statusId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_DoctorUsername_StatusId index.
		/// </summary>
		/// <param name="_doctorUsername"></param>
		/// <param name="_statusId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByDoctorUsernameStatusId(System.String _doctorUsername, System.Int64? _statusId, int start, int pageLength)
		{
			int count = -1;
			return GetByDoctorUsernameStatusId(null, _doctorUsername, _statusId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_DoctorUsername_StatusId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUsername"></param>
		/// <param name="_statusId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByDoctorUsernameStatusId(TransactionManager transactionManager, System.String _doctorUsername, System.Int64? _statusId)
		{
			int count = -1;
			return GetByDoctorUsernameStatusId(transactionManager, _doctorUsername, _statusId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_DoctorUsername_StatusId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUsername"></param>
		/// <param name="_statusId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByDoctorUsernameStatusId(TransactionManager transactionManager, System.String _doctorUsername, System.Int64? _statusId, int start, int pageLength)
		{
			int count = -1;
			return GetByDoctorUsernameStatusId(transactionManager, _doctorUsername, _statusId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_DoctorUsername_StatusId index.
		/// </summary>
		/// <param name="_doctorUsername"></param>
		/// <param name="_statusId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByDoctorUsernameStatusId(System.String _doctorUsername, System.Int64? _statusId, int start, int pageLength, out int count)
		{
			return GetByDoctorUsernameStatusId(null, _doctorUsername, _statusId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_DoctorUsername_StatusId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUsername"></param>
		/// <param name="_statusId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public abstract TList<Appointment> GetByDoctorUsernameStatusId(TransactionManager transactionManager, System.String _doctorUsername, System.Int64? _statusId, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_Appointment_DoctorUsername_StatusId_IsComplete index.
		/// </summary>
		/// <param name="_doctorUsername"></param>
		/// <param name="_statusId"></param>
		/// <param name="_isComplete"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByDoctorUsernameStatusIdIsComplete(System.String _doctorUsername, System.Int64? _statusId, System.Boolean _isComplete)
		{
			int count = -1;
			return GetByDoctorUsernameStatusIdIsComplete(null,_doctorUsername, _statusId, _isComplete, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_DoctorUsername_StatusId_IsComplete index.
		/// </summary>
		/// <param name="_doctorUsername"></param>
		/// <param name="_statusId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByDoctorUsernameStatusIdIsComplete(System.String _doctorUsername, System.Int64? _statusId, System.Boolean _isComplete, int start, int pageLength)
		{
			int count = -1;
			return GetByDoctorUsernameStatusIdIsComplete(null, _doctorUsername, _statusId, _isComplete, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_DoctorUsername_StatusId_IsComplete index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUsername"></param>
		/// <param name="_statusId"></param>
		/// <param name="_isComplete"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByDoctorUsernameStatusIdIsComplete(TransactionManager transactionManager, System.String _doctorUsername, System.Int64? _statusId, System.Boolean _isComplete)
		{
			int count = -1;
			return GetByDoctorUsernameStatusIdIsComplete(transactionManager, _doctorUsername, _statusId, _isComplete, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_DoctorUsername_StatusId_IsComplete index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUsername"></param>
		/// <param name="_statusId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByDoctorUsernameStatusIdIsComplete(TransactionManager transactionManager, System.String _doctorUsername, System.Int64? _statusId, System.Boolean _isComplete, int start, int pageLength)
		{
			int count = -1;
			return GetByDoctorUsernameStatusIdIsComplete(transactionManager, _doctorUsername, _statusId, _isComplete, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_DoctorUsername_StatusId_IsComplete index.
		/// </summary>
		/// <param name="_doctorUsername"></param>
		/// <param name="_statusId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByDoctorUsernameStatusIdIsComplete(System.String _doctorUsername, System.Int64? _statusId, System.Boolean _isComplete, int start, int pageLength, out int count)
		{
			return GetByDoctorUsernameStatusIdIsComplete(null, _doctorUsername, _statusId, _isComplete, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_DoctorUsername_StatusId_IsComplete index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUsername"></param>
		/// <param name="_statusId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public abstract TList<Appointment> GetByDoctorUsernameStatusIdIsComplete(TransactionManager transactionManager, System.String _doctorUsername, System.Int64? _statusId, System.Boolean _isComplete, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_Appointment_DoctorUsername_StatusId_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="_doctorUsername"></param>
		/// <param name="_statusId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByDoctorUsernameStatusIdIsCompleteIsDisabled(System.String _doctorUsername, System.Int64? _statusId, System.Boolean _isComplete, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByDoctorUsernameStatusIdIsCompleteIsDisabled(null,_doctorUsername, _statusId, _isComplete, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_DoctorUsername_StatusId_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="_doctorUsername"></param>
		/// <param name="_statusId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByDoctorUsernameStatusIdIsCompleteIsDisabled(System.String _doctorUsername, System.Int64? _statusId, System.Boolean _isComplete, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByDoctorUsernameStatusIdIsCompleteIsDisabled(null, _doctorUsername, _statusId, _isComplete, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_DoctorUsername_StatusId_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUsername"></param>
		/// <param name="_statusId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByDoctorUsernameStatusIdIsCompleteIsDisabled(TransactionManager transactionManager, System.String _doctorUsername, System.Int64? _statusId, System.Boolean _isComplete, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByDoctorUsernameStatusIdIsCompleteIsDisabled(transactionManager, _doctorUsername, _statusId, _isComplete, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_DoctorUsername_StatusId_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUsername"></param>
		/// <param name="_statusId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByDoctorUsernameStatusIdIsCompleteIsDisabled(TransactionManager transactionManager, System.String _doctorUsername, System.Int64? _statusId, System.Boolean _isComplete, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByDoctorUsernameStatusIdIsCompleteIsDisabled(transactionManager, _doctorUsername, _statusId, _isComplete, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_DoctorUsername_StatusId_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="_doctorUsername"></param>
		/// <param name="_statusId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public TList<Appointment> GetByDoctorUsernameStatusIdIsCompleteIsDisabled(System.String _doctorUsername, System.Int64? _statusId, System.Boolean _isComplete, System.Boolean _isDisabled, int start, int pageLength, out int count)
		{
			return GetByDoctorUsernameStatusIdIsCompleteIsDisabled(null, _doctorUsername, _statusId, _isComplete, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Appointment_DoctorUsername_StatusId_IsComplete_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorUsername"></param>
		/// <param name="_statusId"></param>
		/// <param name="_isComplete"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Appointment&gt;"/> class.</returns>
		public abstract TList<Appointment> GetByDoctorUsernameStatusIdIsCompleteIsDisabled(TransactionManager transactionManager, System.String _doctorUsername, System.Int64? _statusId, System.Boolean _isComplete, System.Boolean _isDisabled, int start, int pageLength, out int count);
						
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
					c.CustomerName = (reader.IsDBNull(((int)AppointmentColumn.CustomerName - 1)))?null:(System.String)reader[((int)AppointmentColumn.CustomerName - 1)];
					c.ContentId = (System.Int64)reader[((int)AppointmentColumn.ContentId - 1)];
					c.ContentTitle = (reader.IsDBNull(((int)AppointmentColumn.ContentTitle - 1)))?null:(System.String)reader[((int)AppointmentColumn.ContentTitle - 1)];
					c.DoctorUsername = (reader.IsDBNull(((int)AppointmentColumn.DoctorUsername - 1)))?null:(System.String)reader[((int)AppointmentColumn.DoctorUsername - 1)];
					c.DoctorShortName = (reader.IsDBNull(((int)AppointmentColumn.DoctorShortName - 1)))?null:(System.String)reader[((int)AppointmentColumn.DoctorShortName - 1)];
					c.RoomId = (reader.IsDBNull(((int)AppointmentColumn.RoomId - 1)))?null:(System.Int64?)reader[((int)AppointmentColumn.RoomId - 1)];
					c.RoomTitle = (reader.IsDBNull(((int)AppointmentColumn.RoomTitle - 1)))?null:(System.String)reader[((int)AppointmentColumn.RoomTitle - 1)];
					c.NurseUsername = (reader.IsDBNull(((int)AppointmentColumn.NurseUsername - 1)))?null:(System.String)reader[((int)AppointmentColumn.NurseUsername - 1)];
					c.NurseShortName = (reader.IsDBNull(((int)AppointmentColumn.NurseShortName - 1)))?null:(System.String)reader[((int)AppointmentColumn.NurseShortName - 1)];
					c.StatusId = (reader.IsDBNull(((int)AppointmentColumn.StatusId - 1)))?null:(System.Int64?)reader[((int)AppointmentColumn.StatusId - 1)];
					c.StatusTitle = (reader.IsDBNull(((int)AppointmentColumn.StatusTitle - 1)))?null:(System.String)reader[((int)AppointmentColumn.StatusTitle - 1)];
					c.Note = (reader.IsDBNull(((int)AppointmentColumn.Note - 1)))?null:(System.String)reader[((int)AppointmentColumn.Note - 1)];
					c.StartTime = (reader.IsDBNull(((int)AppointmentColumn.StartTime - 1)))?null:(System.DateTime?)reader[((int)AppointmentColumn.StartTime - 1)];
					c.EndTime = (reader.IsDBNull(((int)AppointmentColumn.EndTime - 1)))?null:(System.DateTime?)reader[((int)AppointmentColumn.EndTime - 1)];
					c.ColorCode = (System.String)reader[((int)AppointmentColumn.ColorCode - 1)];
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
			entity.CustomerName = (reader.IsDBNull(((int)AppointmentColumn.CustomerName - 1)))?null:(System.String)reader[((int)AppointmentColumn.CustomerName - 1)];
			entity.ContentId = (System.Int64)reader[((int)AppointmentColumn.ContentId - 1)];
			entity.ContentTitle = (reader.IsDBNull(((int)AppointmentColumn.ContentTitle - 1)))?null:(System.String)reader[((int)AppointmentColumn.ContentTitle - 1)];
			entity.DoctorUsername = (reader.IsDBNull(((int)AppointmentColumn.DoctorUsername - 1)))?null:(System.String)reader[((int)AppointmentColumn.DoctorUsername - 1)];
			entity.DoctorShortName = (reader.IsDBNull(((int)AppointmentColumn.DoctorShortName - 1)))?null:(System.String)reader[((int)AppointmentColumn.DoctorShortName - 1)];
			entity.RoomId = (reader.IsDBNull(((int)AppointmentColumn.RoomId - 1)))?null:(System.Int64?)reader[((int)AppointmentColumn.RoomId - 1)];
			entity.RoomTitle = (reader.IsDBNull(((int)AppointmentColumn.RoomTitle - 1)))?null:(System.String)reader[((int)AppointmentColumn.RoomTitle - 1)];
			entity.NurseUsername = (reader.IsDBNull(((int)AppointmentColumn.NurseUsername - 1)))?null:(System.String)reader[((int)AppointmentColumn.NurseUsername - 1)];
			entity.NurseShortName = (reader.IsDBNull(((int)AppointmentColumn.NurseShortName - 1)))?null:(System.String)reader[((int)AppointmentColumn.NurseShortName - 1)];
			entity.StatusId = (reader.IsDBNull(((int)AppointmentColumn.StatusId - 1)))?null:(System.Int64?)reader[((int)AppointmentColumn.StatusId - 1)];
			entity.StatusTitle = (reader.IsDBNull(((int)AppointmentColumn.StatusTitle - 1)))?null:(System.String)reader[((int)AppointmentColumn.StatusTitle - 1)];
			entity.Note = (reader.IsDBNull(((int)AppointmentColumn.Note - 1)))?null:(System.String)reader[((int)AppointmentColumn.Note - 1)];
			entity.StartTime = (reader.IsDBNull(((int)AppointmentColumn.StartTime - 1)))?null:(System.DateTime?)reader[((int)AppointmentColumn.StartTime - 1)];
			entity.EndTime = (reader.IsDBNull(((int)AppointmentColumn.EndTime - 1)))?null:(System.DateTime?)reader[((int)AppointmentColumn.EndTime - 1)];
			entity.ColorCode = (System.String)reader[((int)AppointmentColumn.ColorCode - 1)];
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
			entity.CustomerName = Convert.IsDBNull(dataRow["CustomerName"]) ? null : (System.String)dataRow["CustomerName"];
			entity.ContentId = (System.Int64)dataRow["ContentId"];
			entity.ContentTitle = Convert.IsDBNull(dataRow["ContentTitle"]) ? null : (System.String)dataRow["ContentTitle"];
			entity.DoctorUsername = Convert.IsDBNull(dataRow["DoctorUsername"]) ? null : (System.String)dataRow["DoctorUsername"];
			entity.DoctorShortName = Convert.IsDBNull(dataRow["DoctorShortName"]) ? null : (System.String)dataRow["DoctorShortName"];
			entity.RoomId = Convert.IsDBNull(dataRow["RoomId"]) ? null : (System.Int64?)dataRow["RoomId"];
			entity.RoomTitle = Convert.IsDBNull(dataRow["RoomTitle"]) ? null : (System.String)dataRow["RoomTitle"];
			entity.NurseUsername = Convert.IsDBNull(dataRow["NurseUsername"]) ? null : (System.String)dataRow["NurseUsername"];
			entity.NurseShortName = Convert.IsDBNull(dataRow["NurseShortName"]) ? null : (System.String)dataRow["NurseShortName"];
			entity.StatusId = Convert.IsDBNull(dataRow["StatusId"]) ? null : (System.Int64?)dataRow["StatusId"];
			entity.StatusTitle = Convert.IsDBNull(dataRow["StatusTitle"]) ? null : (System.String)dataRow["StatusTitle"];
			entity.Note = Convert.IsDBNull(dataRow["Note"]) ? null : (System.String)dataRow["Note"];
			entity.StartTime = Convert.IsDBNull(dataRow["StartTime"]) ? null : (System.DateTime?)dataRow["StartTime"];
			entity.EndTime = Convert.IsDBNull(dataRow["EndTime"]) ? null : (System.DateTime?)dataRow["EndTime"];
			entity.ColorCode = (System.String)dataRow["ColorCode"];
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

			#region DoctorUsernameSource	
			if (CanDeepLoad(entity, "Staff|DoctorUsernameSource", deepLoadType, innerList) 
				&& entity.DoctorUsernameSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.DoctorUsername ?? string.Empty);
				Staff tmpEntity = EntityManager.LocateEntity<Staff>(EntityLocator.ConstructKeyFromPkItems(typeof(Staff), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.DoctorUsernameSource = tmpEntity;
				else
					entity.DoctorUsernameSource = DataRepository.StaffProvider.GetByUserName(transactionManager, (entity.DoctorUsername ?? string.Empty));		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'DoctorUsernameSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.DoctorUsernameSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.StaffProvider.DeepLoad(transactionManager, entity.DoctorUsernameSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion DoctorUsernameSource

			#region NurseUsernameSource	
			if (CanDeepLoad(entity, "Staff|NurseUsernameSource", deepLoadType, innerList) 
				&& entity.NurseUsernameSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.NurseUsername ?? string.Empty);
				Staff tmpEntity = EntityManager.LocateEntity<Staff>(EntityLocator.ConstructKeyFromPkItems(typeof(Staff), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.NurseUsernameSource = tmpEntity;
				else
					entity.NurseUsernameSource = DataRepository.StaffProvider.GetByUserName(transactionManager, (entity.NurseUsername ?? string.Empty));		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'NurseUsernameSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.NurseUsernameSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.StaffProvider.DeepLoad(transactionManager, entity.NurseUsernameSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion NurseUsernameSource

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
			
			#region DoctorUsernameSource
			if (CanDeepSave(entity, "Staff|DoctorUsernameSource", deepSaveType, innerList) 
				&& entity.DoctorUsernameSource != null)
			{
				DataRepository.StaffProvider.Save(transactionManager, entity.DoctorUsernameSource);
				entity.DoctorUsername = entity.DoctorUsernameSource.UserName;
			}
			#endregion 
			
			#region NurseUsernameSource
			if (CanDeepSave(entity, "Staff|NurseUsernameSource", deepSaveType, innerList) 
				&& entity.NurseUsernameSource != null)
			{
				DataRepository.StaffProvider.Save(transactionManager, entity.NurseUsernameSource);
				entity.NurseUsername = entity.NurseUsernameSource.UserName;
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
		/// Composite Property for <c>Staff</c> at DoctorUsernameSource
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
