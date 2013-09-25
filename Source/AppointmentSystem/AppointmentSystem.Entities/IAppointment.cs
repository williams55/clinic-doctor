﻿using System;
using System.ComponentModel;

namespace AppointmentSystem.Entities
{
	/// <summary>
	///		The data structure representation of the 'Appointment' table via interface.
	/// </summary>
	/// <remarks>
	/// 	This struct is generated by a tool and should never be modified.
	/// </remarks>
	public interface IAppointment 
	{
		/// <summary>			
		/// Id : 
		/// </summary>
		/// <remarks>Member of the primary key of the underlying table "Appointment"</remarks>
		System.String Id { get; set; }
				
		/// <summary>
		/// keep a copy of the original so it can be used for editable primary keys.
		/// </summary>
		System.String OriginalId { get; set; }
			
		
		
		/// <summary>
		/// PatientCode : 
		/// </summary>
		System.String  PatientCode  { get; set; }
		
		/// <summary>
		/// Username : 
		/// </summary>
		System.String  Username  { get; set; }
		
		/// <summary>
		/// RoomId : 
		/// </summary>
		System.Int32?  RoomId  { get; set; }
		
		/// <summary>
		/// ServicesId : What do patient wanna be served
		/// </summary>
		System.Int32?  ServicesId  { get; set; }
		
		/// <summary>
		/// StatusId : 
		/// </summary>
		System.String  StatusId  { get; set; }
		
		/// <summary>
		/// AppointmentGroupId : 
		/// </summary>
		System.Int32?  AppointmentGroupId  { get; set; }
		
		/// <summary>
		/// Note : 
		/// </summary>
		System.String  Note  { get; set; }
		
		/// <summary>
		/// StartTime : 
		/// </summary>
		System.DateTime?  StartTime  { get; set; }
		
		/// <summary>
		/// EndTime : 
		/// </summary>
		System.DateTime?  EndTime  { get; set; }
		
		/// <summary>
		/// RosterId : 
		/// </summary>
		System.String  RosterId  { get; set; }
		
		/// <summary>
		/// IsComplete : 
		/// </summary>
		System.Boolean  IsComplete  { get; set; }
		
		/// <summary>
		/// IsDisabled : 
		/// </summary>
		System.Boolean  IsDisabled  { get; set; }
		
		/// <summary>
		/// CreateUser : 
		/// </summary>
		System.String  CreateUser  { get; set; }
		
		/// <summary>
		/// CreateDate : 
		/// </summary>
		System.DateTime  CreateDate  { get; set; }
		
		/// <summary>
		/// UpdateUser : 
		/// </summary>
		System.String  UpdateUser  { get; set; }
		
		/// <summary>
		/// UpdateDate : 
		/// </summary>
		System.DateTime  UpdateDate  { get; set; }
			
		/// <summary>
		/// Creates a new object that is a copy of the current instance.
		/// </summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		System.Object Clone();
		
		#region Data Properties


		/// <summary>
		///	Holds a collection of entity objects
		///	which are related to this object through the relation _appointmentHistoryAppointmentId
		/// </summary>	
		TList<AppointmentHistory> AppointmentHistoryCollection {  get;  set;}	


		/// <summary>
		///	Holds a collection of entity objects
		///	which are related to this object through the relation _smsAppointmentId
		/// </summary>	
		TList<Sms> SmsCollection {  get;  set;}	

		#endregion Data Properties

	}
}


