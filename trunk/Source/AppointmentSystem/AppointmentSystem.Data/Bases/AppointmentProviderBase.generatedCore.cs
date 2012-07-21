﻿#region Using directives

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
	/// This class is the base class for any <see cref="AppointmentProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class AppointmentProviderBaseCore : EntityProviderBase<AppointmentSystem.Entities.Appointment, AppointmentSystem.Entities.AppointmentKey>
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
		public override bool Delete(TransactionManager transactionManager, AppointmentSystem.Entities.AppointmentKey key)
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
		/// 	Gets rows from the datasource based on the FK_Appointment_AppointmentGroup key.
		///		FK_Appointment_AppointmentGroup Description: 
		/// </summary>
		/// <param name="_appointmentGroupId"></param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Appointment objects.</returns>
		public TList<Appointment> GetByAppointmentGroupId(System.Int32? _appointmentGroupId)
		{
			int count = -1;
			return GetByAppointmentGroupId(_appointmentGroupId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Appointment_AppointmentGroup key.
		///		FK_Appointment_AppointmentGroup Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_appointmentGroupId"></param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Appointment objects.</returns>
		/// <remarks></remarks>
		public TList<Appointment> GetByAppointmentGroupId(TransactionManager transactionManager, System.Int32? _appointmentGroupId)
		{
			int count = -1;
			return GetByAppointmentGroupId(transactionManager, _appointmentGroupId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_Appointment_AppointmentGroup key.
		///		FK_Appointment_AppointmentGroup Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_appointmentGroupId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Appointment objects.</returns>
		public TList<Appointment> GetByAppointmentGroupId(TransactionManager transactionManager, System.Int32? _appointmentGroupId, int start, int pageLength)
		{
			int count = -1;
			return GetByAppointmentGroupId(transactionManager, _appointmentGroupId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Appointment_AppointmentGroup key.
		///		fkAppointmentAppointmentGroup Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_appointmentGroupId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Appointment objects.</returns>
		public TList<Appointment> GetByAppointmentGroupId(System.Int32? _appointmentGroupId, int start, int pageLength)
		{
			int count =  -1;
			return GetByAppointmentGroupId(null, _appointmentGroupId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Appointment_AppointmentGroup key.
		///		fkAppointmentAppointmentGroup Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_appointmentGroupId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Appointment objects.</returns>
		public TList<Appointment> GetByAppointmentGroupId(System.Int32? _appointmentGroupId, int start, int pageLength,out int count)
		{
			return GetByAppointmentGroupId(null, _appointmentGroupId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Appointment_AppointmentGroup key.
		///		FK_Appointment_AppointmentGroup Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_appointmentGroupId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Appointment objects.</returns>
		public abstract TList<Appointment> GetByAppointmentGroupId(TransactionManager transactionManager, System.Int32? _appointmentGroupId, int start, int pageLength, out int count);
		
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Appointment_Patient key.
		///		FK_Appointment_Patient Description: 
		/// </summary>
		/// <param name="_patientId"></param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Appointment objects.</returns>
		public TList<Appointment> GetByPatientId(System.String _patientId)
		{
			int count = -1;
			return GetByPatientId(_patientId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Appointment_Patient key.
		///		FK_Appointment_Patient Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_patientId"></param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Appointment objects.</returns>
		/// <remarks></remarks>
		public TList<Appointment> GetByPatientId(TransactionManager transactionManager, System.String _patientId)
		{
			int count = -1;
			return GetByPatientId(transactionManager, _patientId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_Appointment_Patient key.
		///		FK_Appointment_Patient Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_patientId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Appointment objects.</returns>
		public TList<Appointment> GetByPatientId(TransactionManager transactionManager, System.String _patientId, int start, int pageLength)
		{
			int count = -1;
			return GetByPatientId(transactionManager, _patientId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Appointment_Patient key.
		///		fkAppointmentPatient Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_patientId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Appointment objects.</returns>
		public TList<Appointment> GetByPatientId(System.String _patientId, int start, int pageLength)
		{
			int count =  -1;
			return GetByPatientId(null, _patientId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Appointment_Patient key.
		///		fkAppointmentPatient Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_patientId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Appointment objects.</returns>
		public TList<Appointment> GetByPatientId(System.String _patientId, int start, int pageLength,out int count)
		{
			return GetByPatientId(null, _patientId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Appointment_Patient key.
		///		FK_Appointment_Patient Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_patientId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Appointment objects.</returns>
		public abstract TList<Appointment> GetByPatientId(TransactionManager transactionManager, System.String _patientId, int start, int pageLength, out int count);
		
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Appointment_Procedure key.
		///		FK_Appointment_Procedure Description: 
		/// </summary>
		/// <param name="_servicesId">What do patient wanna be served</param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Appointment objects.</returns>
		public TList<Appointment> GetByServicesId(System.Int32? _servicesId)
		{
			int count = -1;
			return GetByServicesId(_servicesId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Appointment_Procedure key.
		///		FK_Appointment_Procedure Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_servicesId">What do patient wanna be served</param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Appointment objects.</returns>
		/// <remarks></remarks>
		public TList<Appointment> GetByServicesId(TransactionManager transactionManager, System.Int32? _servicesId)
		{
			int count = -1;
			return GetByServicesId(transactionManager, _servicesId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_Appointment_Procedure key.
		///		FK_Appointment_Procedure Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_servicesId">What do patient wanna be served</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Appointment objects.</returns>
		public TList<Appointment> GetByServicesId(TransactionManager transactionManager, System.Int32? _servicesId, int start, int pageLength)
		{
			int count = -1;
			return GetByServicesId(transactionManager, _servicesId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Appointment_Procedure key.
		///		fkAppointmentProcedure Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_servicesId">What do patient wanna be served</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Appointment objects.</returns>
		public TList<Appointment> GetByServicesId(System.Int32? _servicesId, int start, int pageLength)
		{
			int count =  -1;
			return GetByServicesId(null, _servicesId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Appointment_Procedure key.
		///		fkAppointmentProcedure Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_servicesId">What do patient wanna be served</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Appointment objects.</returns>
		public TList<Appointment> GetByServicesId(System.Int32? _servicesId, int start, int pageLength,out int count)
		{
			return GetByServicesId(null, _servicesId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Appointment_Procedure key.
		///		FK_Appointment_Procedure Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_servicesId">What do patient wanna be served</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Appointment objects.</returns>
		public abstract TList<Appointment> GetByServicesId(TransactionManager transactionManager, System.Int32? _servicesId, int start, int pageLength, out int count);
		
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Appointment_Room key.
		///		FK_Appointment_Room Description: 
		/// </summary>
		/// <param name="_roomId"></param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Appointment objects.</returns>
		public TList<Appointment> GetByRoomId(System.Int32? _roomId)
		{
			int count = -1;
			return GetByRoomId(_roomId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Appointment_Room key.
		///		FK_Appointment_Room Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_roomId"></param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Appointment objects.</returns>
		/// <remarks></remarks>
		public TList<Appointment> GetByRoomId(TransactionManager transactionManager, System.Int32? _roomId)
		{
			int count = -1;
			return GetByRoomId(transactionManager, _roomId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_Appointment_Room key.
		///		FK_Appointment_Room Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_roomId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Appointment objects.</returns>
		public TList<Appointment> GetByRoomId(TransactionManager transactionManager, System.Int32? _roomId, int start, int pageLength)
		{
			int count = -1;
			return GetByRoomId(transactionManager, _roomId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Appointment_Room key.
		///		fkAppointmentRoom Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_roomId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Appointment objects.</returns>
		public TList<Appointment> GetByRoomId(System.Int32? _roomId, int start, int pageLength)
		{
			int count =  -1;
			return GetByRoomId(null, _roomId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Appointment_Room key.
		///		fkAppointmentRoom Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_roomId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Appointment objects.</returns>
		public TList<Appointment> GetByRoomId(System.Int32? _roomId, int start, int pageLength,out int count)
		{
			return GetByRoomId(null, _roomId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Appointment_Room key.
		///		FK_Appointment_Room Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_roomId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Appointment objects.</returns>
		public abstract TList<Appointment> GetByRoomId(TransactionManager transactionManager, System.Int32? _roomId, int start, int pageLength, out int count);
		
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Appointment_Status key.
		///		FK_Appointment_Status Description: 
		/// </summary>
		/// <param name="_statusId"></param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Appointment objects.</returns>
		public TList<Appointment> GetByStatusId(System.Int32? _statusId)
		{
			int count = -1;
			return GetByStatusId(_statusId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Appointment_Status key.
		///		FK_Appointment_Status Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_statusId"></param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Appointment objects.</returns>
		/// <remarks></remarks>
		public TList<Appointment> GetByStatusId(TransactionManager transactionManager, System.Int32? _statusId)
		{
			int count = -1;
			return GetByStatusId(transactionManager, _statusId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_Appointment_Status key.
		///		FK_Appointment_Status Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_statusId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Appointment objects.</returns>
		public TList<Appointment> GetByStatusId(TransactionManager transactionManager, System.Int32? _statusId, int start, int pageLength)
		{
			int count = -1;
			return GetByStatusId(transactionManager, _statusId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Appointment_Status key.
		///		fkAppointmentStatus Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_statusId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Appointment objects.</returns>
		public TList<Appointment> GetByStatusId(System.Int32? _statusId, int start, int pageLength)
		{
			int count =  -1;
			return GetByStatusId(null, _statusId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Appointment_Status key.
		///		fkAppointmentStatus Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_statusId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Appointment objects.</returns>
		public TList<Appointment> GetByStatusId(System.Int32? _statusId, int start, int pageLength,out int count)
		{
			return GetByStatusId(null, _statusId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Appointment_Status key.
		///		FK_Appointment_Status Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_statusId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Appointment objects.</returns>
		public abstract TList<Appointment> GetByStatusId(TransactionManager transactionManager, System.Int32? _statusId, int start, int pageLength, out int count);
		
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Appointment_Users key.
		///		FK_Appointment_Users Description: 
		/// </summary>
		/// <param name="_doctorId"></param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Appointment objects.</returns>
		public TList<Appointment> GetByDoctorId(System.String _doctorId)
		{
			int count = -1;
			return GetByDoctorId(_doctorId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Appointment_Users key.
		///		FK_Appointment_Users Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorId"></param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Appointment objects.</returns>
		/// <remarks></remarks>
		public TList<Appointment> GetByDoctorId(TransactionManager transactionManager, System.String _doctorId)
		{
			int count = -1;
			return GetByDoctorId(transactionManager, _doctorId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_Appointment_Users key.
		///		FK_Appointment_Users Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Appointment objects.</returns>
		public TList<Appointment> GetByDoctorId(TransactionManager transactionManager, System.String _doctorId, int start, int pageLength)
		{
			int count = -1;
			return GetByDoctorId(transactionManager, _doctorId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Appointment_Users key.
		///		fkAppointmentUsers Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_doctorId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Appointment objects.</returns>
		public TList<Appointment> GetByDoctorId(System.String _doctorId, int start, int pageLength)
		{
			int count =  -1;
			return GetByDoctorId(null, _doctorId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Appointment_Users key.
		///		fkAppointmentUsers Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_doctorId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Appointment objects.</returns>
		public TList<Appointment> GetByDoctorId(System.String _doctorId, int start, int pageLength,out int count)
		{
			return GetByDoctorId(null, _doctorId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Appointment_Users key.
		///		FK_Appointment_Users Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of AppointmentSystem.Entities.Appointment objects.</returns>
		public abstract TList<Appointment> GetByDoctorId(TransactionManager transactionManager, System.String _doctorId, int start, int pageLength, out int count);
		
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
		public override AppointmentSystem.Entities.Appointment Get(TransactionManager transactionManager, AppointmentSystem.Entities.AppointmentKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Appointment index.
		/// </summary>
		/// <param name="_id"></param>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.Appointment"/> class.</returns>
		public AppointmentSystem.Entities.Appointment GetById(System.String _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Appointment index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.Appointment"/> class.</returns>
		public AppointmentSystem.Entities.Appointment GetById(System.String _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Appointment index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.Appointment"/> class.</returns>
		public AppointmentSystem.Entities.Appointment GetById(TransactionManager transactionManager, System.String _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Appointment index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.Appointment"/> class.</returns>
		public AppointmentSystem.Entities.Appointment GetById(TransactionManager transactionManager, System.String _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Appointment index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.Appointment"/> class.</returns>
		public AppointmentSystem.Entities.Appointment GetById(System.String _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Appointment index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.Appointment"/> class.</returns>
		public abstract AppointmentSystem.Entities.Appointment GetById(TransactionManager transactionManager, System.String _id, int start, int pageLength, out int count);
						
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
				
				AppointmentSystem.Entities.Appointment c = null;
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
					c = new AppointmentSystem.Entities.Appointment();
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
					c.PatientId = (System.String)reader[((int)AppointmentColumn.PatientId - 1)];
					c.DoctorId = (System.String)reader[((int)AppointmentColumn.DoctorId - 1)];
					c.RoomId = (reader.IsDBNull(((int)AppointmentColumn.RoomId - 1)))?null:(System.Int32?)reader[((int)AppointmentColumn.RoomId - 1)];
					c.ServicesId = (reader.IsDBNull(((int)AppointmentColumn.ServicesId - 1)))?null:(System.Int32?)reader[((int)AppointmentColumn.ServicesId - 1)];
					c.StatusId = (reader.IsDBNull(((int)AppointmentColumn.StatusId - 1)))?null:(System.Int32?)reader[((int)AppointmentColumn.StatusId - 1)];
					c.AppointmentGroupId = (reader.IsDBNull(((int)AppointmentColumn.AppointmentGroupId - 1)))?null:(System.Int32?)reader[((int)AppointmentColumn.AppointmentGroupId - 1)];
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
		/// Refreshes the <see cref="AppointmentSystem.Entities.Appointment"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.Appointment"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, AppointmentSystem.Entities.Appointment entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (System.String)reader[((int)AppointmentColumn.Id - 1)];
			entity.OriginalId = (System.String)reader["Id"];
			entity.PatientId = (System.String)reader[((int)AppointmentColumn.PatientId - 1)];
			entity.DoctorId = (System.String)reader[((int)AppointmentColumn.DoctorId - 1)];
			entity.RoomId = (reader.IsDBNull(((int)AppointmentColumn.RoomId - 1)))?null:(System.Int32?)reader[((int)AppointmentColumn.RoomId - 1)];
			entity.ServicesId = (reader.IsDBNull(((int)AppointmentColumn.ServicesId - 1)))?null:(System.Int32?)reader[((int)AppointmentColumn.ServicesId - 1)];
			entity.StatusId = (reader.IsDBNull(((int)AppointmentColumn.StatusId - 1)))?null:(System.Int32?)reader[((int)AppointmentColumn.StatusId - 1)];
			entity.AppointmentGroupId = (reader.IsDBNull(((int)AppointmentColumn.AppointmentGroupId - 1)))?null:(System.Int32?)reader[((int)AppointmentColumn.AppointmentGroupId - 1)];
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
		/// Refreshes the <see cref="AppointmentSystem.Entities.Appointment"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.Appointment"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, AppointmentSystem.Entities.Appointment entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Id = (System.String)dataRow["Id"];
			entity.OriginalId = (System.String)dataRow["Id"];
			entity.PatientId = (System.String)dataRow["PatientId"];
			entity.DoctorId = (System.String)dataRow["DoctorId"];
			entity.RoomId = Convert.IsDBNull(dataRow["RoomId"]) ? null : (System.Int32?)dataRow["RoomId"];
			entity.ServicesId = Convert.IsDBNull(dataRow["ServicesId"]) ? null : (System.Int32?)dataRow["ServicesId"];
			entity.StatusId = Convert.IsDBNull(dataRow["StatusId"]) ? null : (System.Int32?)dataRow["StatusId"];
			entity.AppointmentGroupId = Convert.IsDBNull(dataRow["AppointmentGroupId"]) ? null : (System.Int32?)dataRow["AppointmentGroupId"];
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
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.Appointment"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">AppointmentSystem.Entities.Appointment Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, AppointmentSystem.Entities.Appointment entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region AppointmentGroupIdSource	
			if (CanDeepLoad(entity, "AppointmentGroup|AppointmentGroupIdSource", deepLoadType, innerList) 
				&& entity.AppointmentGroupIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.AppointmentGroupId ?? (int)0);
				AppointmentGroup tmpEntity = EntityManager.LocateEntity<AppointmentGroup>(EntityLocator.ConstructKeyFromPkItems(typeof(AppointmentGroup), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.AppointmentGroupIdSource = tmpEntity;
				else
					entity.AppointmentGroupIdSource = DataRepository.AppointmentGroupProvider.GetById(transactionManager, (entity.AppointmentGroupId ?? (int)0));		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'AppointmentGroupIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.AppointmentGroupIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.AppointmentGroupProvider.DeepLoad(transactionManager, entity.AppointmentGroupIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion AppointmentGroupIdSource

			#region PatientIdSource	
			if (CanDeepLoad(entity, "Patient|PatientIdSource", deepLoadType, innerList) 
				&& entity.PatientIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = entity.PatientId;
				Patient tmpEntity = EntityManager.LocateEntity<Patient>(EntityLocator.ConstructKeyFromPkItems(typeof(Patient), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.PatientIdSource = tmpEntity;
				else
					entity.PatientIdSource = DataRepository.PatientProvider.GetById(transactionManager, entity.PatientId);		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'PatientIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.PatientIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.PatientProvider.DeepLoad(transactionManager, entity.PatientIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion PatientIdSource

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

			#region RoomIdSource	
			if (CanDeepLoad(entity, "Room|RoomIdSource", deepLoadType, innerList) 
				&& entity.RoomIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.RoomId ?? (int)0);
				Room tmpEntity = EntityManager.LocateEntity<Room>(EntityLocator.ConstructKeyFromPkItems(typeof(Room), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.RoomIdSource = tmpEntity;
				else
					entity.RoomIdSource = DataRepository.RoomProvider.GetById(transactionManager, (entity.RoomId ?? (int)0));		
				
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

			#region StatusIdSource	
			if (CanDeepLoad(entity, "Status|StatusIdSource", deepLoadType, innerList) 
				&& entity.StatusIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.StatusId ?? (int)0);
				Status tmpEntity = EntityManager.LocateEntity<Status>(EntityLocator.ConstructKeyFromPkItems(typeof(Status), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.StatusIdSource = tmpEntity;
				else
					entity.StatusIdSource = DataRepository.StatusProvider.GetById(transactionManager, (entity.StatusId ?? (int)0));		
				
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

			#region DoctorIdSource	
			if (CanDeepLoad(entity, "Users|DoctorIdSource", deepLoadType, innerList) 
				&& entity.DoctorIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = entity.DoctorId;
				Users tmpEntity = EntityManager.LocateEntity<Users>(EntityLocator.ConstructKeyFromPkItems(typeof(Users), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.DoctorIdSource = tmpEntity;
				else
					entity.DoctorIdSource = DataRepository.UsersProvider.GetById(transactionManager, entity.DoctorId);		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'DoctorIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.DoctorIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.UsersProvider.DeepLoad(transactionManager, entity.DoctorIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion DoctorIdSource
			
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
		/// Deep Save the entire object graph of the AppointmentSystem.Entities.Appointment object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">AppointmentSystem.Entities.Appointment instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">AppointmentSystem.Entities.Appointment Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, AppointmentSystem.Entities.Appointment entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			
			#region AppointmentGroupIdSource
			if (CanDeepSave(entity, "AppointmentGroup|AppointmentGroupIdSource", deepSaveType, innerList) 
				&& entity.AppointmentGroupIdSource != null)
			{
				DataRepository.AppointmentGroupProvider.Save(transactionManager, entity.AppointmentGroupIdSource);
				entity.AppointmentGroupId = entity.AppointmentGroupIdSource.Id;
			}
			#endregion 
			
			#region PatientIdSource
			if (CanDeepSave(entity, "Patient|PatientIdSource", deepSaveType, innerList) 
				&& entity.PatientIdSource != null)
			{
				DataRepository.PatientProvider.Save(transactionManager, entity.PatientIdSource);
				entity.PatientId = entity.PatientIdSource.Id;
			}
			#endregion 
			
			#region ServicesIdSource
			if (CanDeepSave(entity, "Services|ServicesIdSource", deepSaveType, innerList) 
				&& entity.ServicesIdSource != null)
			{
				DataRepository.ServicesProvider.Save(transactionManager, entity.ServicesIdSource);
				entity.ServicesId = entity.ServicesIdSource.Id;
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
			
			#region StatusIdSource
			if (CanDeepSave(entity, "Status|StatusIdSource", deepSaveType, innerList) 
				&& entity.StatusIdSource != null)
			{
				DataRepository.StatusProvider.Save(transactionManager, entity.StatusIdSource);
				entity.StatusId = entity.StatusIdSource.Id;
			}
			#endregion 
			
			#region DoctorIdSource
			if (CanDeepSave(entity, "Users|DoctorIdSource", deepSaveType, innerList) 
				&& entity.DoctorIdSource != null)
			{
				DataRepository.UsersProvider.Save(transactionManager, entity.DoctorIdSource);
				entity.DoctorId = entity.DoctorIdSource.Id;
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
	/// for child properties in <c>AppointmentSystem.Entities.Appointment</c>
	///</summary>
	public enum AppointmentChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>AppointmentGroup</c> at AppointmentGroupIdSource
		///</summary>
		[ChildEntityType(typeof(AppointmentGroup))]
		AppointmentGroup,
			
		///<summary>
		/// Composite Property for <c>Patient</c> at PatientIdSource
		///</summary>
		[ChildEntityType(typeof(Patient))]
		Patient,
			
		///<summary>
		/// Composite Property for <c>Services</c> at ServicesIdSource
		///</summary>
		[ChildEntityType(typeof(Services))]
		Services,
			
		///<summary>
		/// Composite Property for <c>Room</c> at RoomIdSource
		///</summary>
		[ChildEntityType(typeof(Room))]
		Room,
			
		///<summary>
		/// Composite Property for <c>Status</c> at StatusIdSource
		///</summary>
		[ChildEntityType(typeof(Status))]
		Status,
			
		///<summary>
		/// Composite Property for <c>Users</c> at DoctorIdSource
		///</summary>
		[ChildEntityType(typeof(Users))]
		Users,
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