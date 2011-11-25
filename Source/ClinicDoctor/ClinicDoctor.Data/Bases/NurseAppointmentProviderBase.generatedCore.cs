﻿#region Using directives

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
	/// This class is the base class for any <see cref="NurseAppointmentProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class NurseAppointmentProviderBaseCore : EntityProviderBase<ClinicDoctor.Entities.NurseAppointment, ClinicDoctor.Entities.NurseAppointmentKey>
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
		public override bool Delete(TransactionManager transactionManager, ClinicDoctor.Entities.NurseAppointmentKey key)
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
		public override ClinicDoctor.Entities.NurseAppointment Get(TransactionManager transactionManager, ClinicDoctor.Entities.NurseAppointmentKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_NurseAppointment_AppointmentId index.
		/// </summary>
		/// <param name="_appointmentId"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public TList<NurseAppointment> GetByAppointmentId(System.Int32? _appointmentId)
		{
			int count = -1;
			return GetByAppointmentId(null,_appointmentId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_NurseAppointment_AppointmentId index.
		/// </summary>
		/// <param name="_appointmentId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public TList<NurseAppointment> GetByAppointmentId(System.Int32? _appointmentId, int start, int pageLength)
		{
			int count = -1;
			return GetByAppointmentId(null, _appointmentId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_NurseAppointment_AppointmentId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_appointmentId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public TList<NurseAppointment> GetByAppointmentId(TransactionManager transactionManager, System.Int32? _appointmentId)
		{
			int count = -1;
			return GetByAppointmentId(transactionManager, _appointmentId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_NurseAppointment_AppointmentId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_appointmentId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public TList<NurseAppointment> GetByAppointmentId(TransactionManager transactionManager, System.Int32? _appointmentId, int start, int pageLength)
		{
			int count = -1;
			return GetByAppointmentId(transactionManager, _appointmentId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_NurseAppointment_AppointmentId index.
		/// </summary>
		/// <param name="_appointmentId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public TList<NurseAppointment> GetByAppointmentId(System.Int32? _appointmentId, int start, int pageLength, out int count)
		{
			return GetByAppointmentId(null, _appointmentId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_NurseAppointment_AppointmentId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_appointmentId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public abstract TList<NurseAppointment> GetByAppointmentId(TransactionManager transactionManager, System.Int32? _appointmentId, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_NurseAppointment_AppointmentId_IsDisabled index.
		/// </summary>
		/// <param name="_appointmentId"></param>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public TList<NurseAppointment> GetByAppointmentIdIsDisabled(System.Int32? _appointmentId, System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByAppointmentIdIsDisabled(null,_appointmentId, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_NurseAppointment_AppointmentId_IsDisabled index.
		/// </summary>
		/// <param name="_appointmentId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public TList<NurseAppointment> GetByAppointmentIdIsDisabled(System.Int32? _appointmentId, System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByAppointmentIdIsDisabled(null, _appointmentId, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_NurseAppointment_AppointmentId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_appointmentId"></param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public TList<NurseAppointment> GetByAppointmentIdIsDisabled(TransactionManager transactionManager, System.Int32? _appointmentId, System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByAppointmentIdIsDisabled(transactionManager, _appointmentId, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_NurseAppointment_AppointmentId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_appointmentId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public TList<NurseAppointment> GetByAppointmentIdIsDisabled(TransactionManager transactionManager, System.Int32? _appointmentId, System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByAppointmentIdIsDisabled(transactionManager, _appointmentId, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_NurseAppointment_AppointmentId_IsDisabled index.
		/// </summary>
		/// <param name="_appointmentId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public TList<NurseAppointment> GetByAppointmentIdIsDisabled(System.Int32? _appointmentId, System.Boolean? _isDisabled, int start, int pageLength, out int count)
		{
			return GetByAppointmentIdIsDisabled(null, _appointmentId, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_NurseAppointment_AppointmentId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_appointmentId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public abstract TList<NurseAppointment> GetByAppointmentIdIsDisabled(TransactionManager transactionManager, System.Int32? _appointmentId, System.Boolean? _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_NurseAppointment_AppointmentId_NurseId index.
		/// </summary>
		/// <param name="_appointmentId"></param>
		/// <param name="_nurseId"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public TList<NurseAppointment> GetByAppointmentIdNurseId(System.Int32? _appointmentId, System.Int32? _nurseId)
		{
			int count = -1;
			return GetByAppointmentIdNurseId(null,_appointmentId, _nurseId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_NurseAppointment_AppointmentId_NurseId index.
		/// </summary>
		/// <param name="_appointmentId"></param>
		/// <param name="_nurseId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public TList<NurseAppointment> GetByAppointmentIdNurseId(System.Int32? _appointmentId, System.Int32? _nurseId, int start, int pageLength)
		{
			int count = -1;
			return GetByAppointmentIdNurseId(null, _appointmentId, _nurseId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_NurseAppointment_AppointmentId_NurseId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_appointmentId"></param>
		/// <param name="_nurseId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public TList<NurseAppointment> GetByAppointmentIdNurseId(TransactionManager transactionManager, System.Int32? _appointmentId, System.Int32? _nurseId)
		{
			int count = -1;
			return GetByAppointmentIdNurseId(transactionManager, _appointmentId, _nurseId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_NurseAppointment_AppointmentId_NurseId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_appointmentId"></param>
		/// <param name="_nurseId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public TList<NurseAppointment> GetByAppointmentIdNurseId(TransactionManager transactionManager, System.Int32? _appointmentId, System.Int32? _nurseId, int start, int pageLength)
		{
			int count = -1;
			return GetByAppointmentIdNurseId(transactionManager, _appointmentId, _nurseId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_NurseAppointment_AppointmentId_NurseId index.
		/// </summary>
		/// <param name="_appointmentId"></param>
		/// <param name="_nurseId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public TList<NurseAppointment> GetByAppointmentIdNurseId(System.Int32? _appointmentId, System.Int32? _nurseId, int start, int pageLength, out int count)
		{
			return GetByAppointmentIdNurseId(null, _appointmentId, _nurseId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_NurseAppointment_AppointmentId_NurseId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_appointmentId"></param>
		/// <param name="_nurseId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public abstract TList<NurseAppointment> GetByAppointmentIdNurseId(TransactionManager transactionManager, System.Int32? _appointmentId, System.Int32? _nurseId, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_NurseAppointment_AppointmentId_NurseId_IsDisabled index.
		/// </summary>
		/// <param name="_appointmentId"></param>
		/// <param name="_nurseId"></param>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public TList<NurseAppointment> GetByAppointmentIdNurseIdIsDisabled(System.Int32? _appointmentId, System.Int32? _nurseId, System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByAppointmentIdNurseIdIsDisabled(null,_appointmentId, _nurseId, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_NurseAppointment_AppointmentId_NurseId_IsDisabled index.
		/// </summary>
		/// <param name="_appointmentId"></param>
		/// <param name="_nurseId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public TList<NurseAppointment> GetByAppointmentIdNurseIdIsDisabled(System.Int32? _appointmentId, System.Int32? _nurseId, System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByAppointmentIdNurseIdIsDisabled(null, _appointmentId, _nurseId, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_NurseAppointment_AppointmentId_NurseId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_appointmentId"></param>
		/// <param name="_nurseId"></param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public TList<NurseAppointment> GetByAppointmentIdNurseIdIsDisabled(TransactionManager transactionManager, System.Int32? _appointmentId, System.Int32? _nurseId, System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByAppointmentIdNurseIdIsDisabled(transactionManager, _appointmentId, _nurseId, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_NurseAppointment_AppointmentId_NurseId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_appointmentId"></param>
		/// <param name="_nurseId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public TList<NurseAppointment> GetByAppointmentIdNurseIdIsDisabled(TransactionManager transactionManager, System.Int32? _appointmentId, System.Int32? _nurseId, System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByAppointmentIdNurseIdIsDisabled(transactionManager, _appointmentId, _nurseId, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_NurseAppointment_AppointmentId_NurseId_IsDisabled index.
		/// </summary>
		/// <param name="_appointmentId"></param>
		/// <param name="_nurseId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public TList<NurseAppointment> GetByAppointmentIdNurseIdIsDisabled(System.Int32? _appointmentId, System.Int32? _nurseId, System.Boolean? _isDisabled, int start, int pageLength, out int count)
		{
			return GetByAppointmentIdNurseIdIsDisabled(null, _appointmentId, _nurseId, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_NurseAppointment_AppointmentId_NurseId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_appointmentId"></param>
		/// <param name="_nurseId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public abstract TList<NurseAppointment> GetByAppointmentIdNurseIdIsDisabled(TransactionManager transactionManager, System.Int32? _appointmentId, System.Int32? _nurseId, System.Boolean? _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_NurseAppointment_Id_IsDisabled index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public TList<NurseAppointment> GetByIdIsDisabled(System.Int32 _id, System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByIdIsDisabled(null,_id, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_NurseAppointment_Id_IsDisabled index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public TList<NurseAppointment> GetByIdIsDisabled(System.Int32 _id, System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIdIsDisabled(null, _id, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_NurseAppointment_Id_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public TList<NurseAppointment> GetByIdIsDisabled(TransactionManager transactionManager, System.Int32 _id, System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByIdIsDisabled(transactionManager, _id, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_NurseAppointment_Id_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public TList<NurseAppointment> GetByIdIsDisabled(TransactionManager transactionManager, System.Int32 _id, System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIdIsDisabled(transactionManager, _id, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_NurseAppointment_Id_IsDisabled index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public TList<NurseAppointment> GetByIdIsDisabled(System.Int32 _id, System.Boolean? _isDisabled, int start, int pageLength, out int count)
		{
			return GetByIdIsDisabled(null, _id, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_NurseAppointment_Id_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public abstract TList<NurseAppointment> GetByIdIsDisabled(TransactionManager transactionManager, System.Int32 _id, System.Boolean? _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_NurseAppointment_IsDisabled index.
		/// </summary>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public TList<NurseAppointment> GetByIsDisabled(System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByIsDisabled(null,_isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_NurseAppointment_IsDisabled index.
		/// </summary>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public TList<NurseAppointment> GetByIsDisabled(System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIsDisabled(null, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_NurseAppointment_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public TList<NurseAppointment> GetByIsDisabled(TransactionManager transactionManager, System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByIsDisabled(transactionManager, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_NurseAppointment_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public TList<NurseAppointment> GetByIsDisabled(TransactionManager transactionManager, System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIsDisabled(transactionManager, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_NurseAppointment_IsDisabled index.
		/// </summary>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public TList<NurseAppointment> GetByIsDisabled(System.Boolean? _isDisabled, int start, int pageLength, out int count)
		{
			return GetByIsDisabled(null, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_NurseAppointment_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public abstract TList<NurseAppointment> GetByIsDisabled(TransactionManager transactionManager, System.Boolean? _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_NurseAppointment_NurseId index.
		/// </summary>
		/// <param name="_nurseId"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public TList<NurseAppointment> GetByNurseId(System.Int32? _nurseId)
		{
			int count = -1;
			return GetByNurseId(null,_nurseId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_NurseAppointment_NurseId index.
		/// </summary>
		/// <param name="_nurseId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public TList<NurseAppointment> GetByNurseId(System.Int32? _nurseId, int start, int pageLength)
		{
			int count = -1;
			return GetByNurseId(null, _nurseId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_NurseAppointment_NurseId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_nurseId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public TList<NurseAppointment> GetByNurseId(TransactionManager transactionManager, System.Int32? _nurseId)
		{
			int count = -1;
			return GetByNurseId(transactionManager, _nurseId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_NurseAppointment_NurseId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_nurseId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public TList<NurseAppointment> GetByNurseId(TransactionManager transactionManager, System.Int32? _nurseId, int start, int pageLength)
		{
			int count = -1;
			return GetByNurseId(transactionManager, _nurseId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_NurseAppointment_NurseId index.
		/// </summary>
		/// <param name="_nurseId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public TList<NurseAppointment> GetByNurseId(System.Int32? _nurseId, int start, int pageLength, out int count)
		{
			return GetByNurseId(null, _nurseId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_NurseAppointment_NurseId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_nurseId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public abstract TList<NurseAppointment> GetByNurseId(TransactionManager transactionManager, System.Int32? _nurseId, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_NurseAppointment_NurseId_IsDisabled index.
		/// </summary>
		/// <param name="_nurseId"></param>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public TList<NurseAppointment> GetByNurseIdIsDisabled(System.Int32? _nurseId, System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByNurseIdIsDisabled(null,_nurseId, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_NurseAppointment_NurseId_IsDisabled index.
		/// </summary>
		/// <param name="_nurseId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public TList<NurseAppointment> GetByNurseIdIsDisabled(System.Int32? _nurseId, System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByNurseIdIsDisabled(null, _nurseId, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_NurseAppointment_NurseId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_nurseId"></param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public TList<NurseAppointment> GetByNurseIdIsDisabled(TransactionManager transactionManager, System.Int32? _nurseId, System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByNurseIdIsDisabled(transactionManager, _nurseId, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_NurseAppointment_NurseId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_nurseId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public TList<NurseAppointment> GetByNurseIdIsDisabled(TransactionManager transactionManager, System.Int32? _nurseId, System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByNurseIdIsDisabled(transactionManager, _nurseId, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_NurseAppointment_NurseId_IsDisabled index.
		/// </summary>
		/// <param name="_nurseId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public TList<NurseAppointment> GetByNurseIdIsDisabled(System.Int32? _nurseId, System.Boolean? _isDisabled, int start, int pageLength, out int count)
		{
			return GetByNurseIdIsDisabled(null, _nurseId, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_NurseAppointment_NurseId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_nurseId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;NurseAppointment&gt;"/> class.</returns>
		public abstract TList<NurseAppointment> GetByNurseIdIsDisabled(TransactionManager transactionManager, System.Int32? _nurseId, System.Boolean? _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_NurseAppointment index.
		/// </summary>
		/// <param name="_id"></param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.NurseAppointment"/> class.</returns>
		public ClinicDoctor.Entities.NurseAppointment GetById(System.Int32 _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_NurseAppointment index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.NurseAppointment"/> class.</returns>
		public ClinicDoctor.Entities.NurseAppointment GetById(System.Int32 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_NurseAppointment index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.NurseAppointment"/> class.</returns>
		public ClinicDoctor.Entities.NurseAppointment GetById(TransactionManager transactionManager, System.Int32 _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_NurseAppointment index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.NurseAppointment"/> class.</returns>
		public ClinicDoctor.Entities.NurseAppointment GetById(TransactionManager transactionManager, System.Int32 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_NurseAppointment index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.NurseAppointment"/> class.</returns>
		public ClinicDoctor.Entities.NurseAppointment GetById(System.Int32 _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_NurseAppointment index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.NurseAppointment"/> class.</returns>
		public abstract ClinicDoctor.Entities.NurseAppointment GetById(TransactionManager transactionManager, System.Int32 _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;NurseAppointment&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;NurseAppointment&gt;"/></returns>
		public static TList<NurseAppointment> Fill(IDataReader reader, TList<NurseAppointment> rows, int start, int pageLength)
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
				
				ClinicDoctor.Entities.NurseAppointment c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("NurseAppointment")
					.Append("|").Append((System.Int32)reader[((int)NurseAppointmentColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<NurseAppointment>(
					key.ToString(), // EntityTrackingKey
					"NurseAppointment",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new ClinicDoctor.Entities.NurseAppointment();
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
					c.Id = (System.Int32)reader[((int)NurseAppointmentColumn.Id - 1)];
					c.AppointmentId = (reader.IsDBNull(((int)NurseAppointmentColumn.AppointmentId - 1)))?null:(System.Int32?)reader[((int)NurseAppointmentColumn.AppointmentId - 1)];
					c.NurseId = (reader.IsDBNull(((int)NurseAppointmentColumn.NurseId - 1)))?null:(System.Int32?)reader[((int)NurseAppointmentColumn.NurseId - 1)];
					c.IsDisabled = (reader.IsDBNull(((int)NurseAppointmentColumn.IsDisabled - 1)))?null:(System.Boolean?)reader[((int)NurseAppointmentColumn.IsDisabled - 1)];
					c.CreateUser = (reader.IsDBNull(((int)NurseAppointmentColumn.CreateUser - 1)))?null:(System.String)reader[((int)NurseAppointmentColumn.CreateUser - 1)];
					c.CreateDate = (reader.IsDBNull(((int)NurseAppointmentColumn.CreateDate - 1)))?null:(System.DateTime?)reader[((int)NurseAppointmentColumn.CreateDate - 1)];
					c.UpdateUser = (reader.IsDBNull(((int)NurseAppointmentColumn.UpdateUser - 1)))?null:(System.String)reader[((int)NurseAppointmentColumn.UpdateUser - 1)];
					c.UpdateDate = (reader.IsDBNull(((int)NurseAppointmentColumn.UpdateDate - 1)))?null:(System.DateTime?)reader[((int)NurseAppointmentColumn.UpdateDate - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="ClinicDoctor.Entities.NurseAppointment"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="ClinicDoctor.Entities.NurseAppointment"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, ClinicDoctor.Entities.NurseAppointment entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (System.Int32)reader[((int)NurseAppointmentColumn.Id - 1)];
			entity.AppointmentId = (reader.IsDBNull(((int)NurseAppointmentColumn.AppointmentId - 1)))?null:(System.Int32?)reader[((int)NurseAppointmentColumn.AppointmentId - 1)];
			entity.NurseId = (reader.IsDBNull(((int)NurseAppointmentColumn.NurseId - 1)))?null:(System.Int32?)reader[((int)NurseAppointmentColumn.NurseId - 1)];
			entity.IsDisabled = (reader.IsDBNull(((int)NurseAppointmentColumn.IsDisabled - 1)))?null:(System.Boolean?)reader[((int)NurseAppointmentColumn.IsDisabled - 1)];
			entity.CreateUser = (reader.IsDBNull(((int)NurseAppointmentColumn.CreateUser - 1)))?null:(System.String)reader[((int)NurseAppointmentColumn.CreateUser - 1)];
			entity.CreateDate = (reader.IsDBNull(((int)NurseAppointmentColumn.CreateDate - 1)))?null:(System.DateTime?)reader[((int)NurseAppointmentColumn.CreateDate - 1)];
			entity.UpdateUser = (reader.IsDBNull(((int)NurseAppointmentColumn.UpdateUser - 1)))?null:(System.String)reader[((int)NurseAppointmentColumn.UpdateUser - 1)];
			entity.UpdateDate = (reader.IsDBNull(((int)NurseAppointmentColumn.UpdateDate - 1)))?null:(System.DateTime?)reader[((int)NurseAppointmentColumn.UpdateDate - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="ClinicDoctor.Entities.NurseAppointment"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="ClinicDoctor.Entities.NurseAppointment"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, ClinicDoctor.Entities.NurseAppointment entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Id = (System.Int32)dataRow["Id"];
			entity.AppointmentId = Convert.IsDBNull(dataRow["AppointmentId"]) ? null : (System.Int32?)dataRow["AppointmentId"];
			entity.NurseId = Convert.IsDBNull(dataRow["NurseId"]) ? null : (System.Int32?)dataRow["NurseId"];
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
		/// <param name="entity">The <see cref="ClinicDoctor.Entities.NurseAppointment"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">ClinicDoctor.Entities.NurseAppointment Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, ClinicDoctor.Entities.NurseAppointment entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region AppointmentIdSource	
			if (CanDeepLoad(entity, "Appointment|AppointmentIdSource", deepLoadType, innerList) 
				&& entity.AppointmentIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.AppointmentId ?? (int)0);
				Appointment tmpEntity = EntityManager.LocateEntity<Appointment>(EntityLocator.ConstructKeyFromPkItems(typeof(Appointment), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.AppointmentIdSource = tmpEntity;
				else
					entity.AppointmentIdSource = DataRepository.AppointmentProvider.GetById(transactionManager, (entity.AppointmentId ?? (int)0));		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'AppointmentIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.AppointmentIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.AppointmentProvider.DeepLoad(transactionManager, entity.AppointmentIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion AppointmentIdSource

			#region NurseIdSource	
			if (CanDeepLoad(entity, "Staff|NurseIdSource", deepLoadType, innerList) 
				&& entity.NurseIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.NurseId ?? (int)0);
				Staff tmpEntity = EntityManager.LocateEntity<Staff>(EntityLocator.ConstructKeyFromPkItems(typeof(Staff), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.NurseIdSource = tmpEntity;
				else
					entity.NurseIdSource = DataRepository.StaffProvider.GetById(transactionManager, (entity.NurseId ?? (int)0));		
				
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
		/// Deep Save the entire object graph of the ClinicDoctor.Entities.NurseAppointment object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">ClinicDoctor.Entities.NurseAppointment instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">ClinicDoctor.Entities.NurseAppointment Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, ClinicDoctor.Entities.NurseAppointment entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			
			#region AppointmentIdSource
			if (CanDeepSave(entity, "Appointment|AppointmentIdSource", deepSaveType, innerList) 
				&& entity.AppointmentIdSource != null)
			{
				DataRepository.AppointmentProvider.Save(transactionManager, entity.AppointmentIdSource);
				entity.AppointmentId = entity.AppointmentIdSource.Id;
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
	
	#region NurseAppointmentChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>ClinicDoctor.Entities.NurseAppointment</c>
	///</summary>
	public enum NurseAppointmentChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Appointment</c> at AppointmentIdSource
		///</summary>
		[ChildEntityType(typeof(Appointment))]
		Appointment,
			
		///<summary>
		/// Composite Property for <c>Staff</c> at NurseIdSource
		///</summary>
		[ChildEntityType(typeof(Staff))]
		Staff,
		}
	
	#endregion NurseAppointmentChildEntityTypes
	
	#region NurseAppointmentFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;NurseAppointmentColumn&gt;"/> class
	/// that is used exclusively with a <see cref="NurseAppointment"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class NurseAppointmentFilterBuilder : SqlFilterBuilder<NurseAppointmentColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the NurseAppointmentFilterBuilder class.
		/// </summary>
		public NurseAppointmentFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the NurseAppointmentFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public NurseAppointmentFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the NurseAppointmentFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public NurseAppointmentFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion NurseAppointmentFilterBuilder
	
	#region NurseAppointmentParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;NurseAppointmentColumn&gt;"/> class
	/// that is used exclusively with a <see cref="NurseAppointment"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class NurseAppointmentParameterBuilder : ParameterizedSqlFilterBuilder<NurseAppointmentColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the NurseAppointmentParameterBuilder class.
		/// </summary>
		public NurseAppointmentParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the NurseAppointmentParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public NurseAppointmentParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the NurseAppointmentParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public NurseAppointmentParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion NurseAppointmentParameterBuilder
	
	#region NurseAppointmentSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;NurseAppointmentColumn&gt;"/> class
	/// that is used exclusively with a <see cref="NurseAppointment"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class NurseAppointmentSortBuilder : SqlSortBuilder<NurseAppointmentColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the NurseAppointmentSqlSortBuilder class.
		/// </summary>
		public NurseAppointmentSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion NurseAppointmentSortBuilder
	
} // end namespace
