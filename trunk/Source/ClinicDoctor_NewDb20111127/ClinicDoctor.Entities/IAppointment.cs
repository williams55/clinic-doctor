﻿using System;
using System.ComponentModel;

namespace ClinicDoctor.Entities
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
		/// CustomerId : 
		/// </summary>
		System.Int64  CustomerId  { get; set; }
		
		/// <summary>
		/// ContentId : 
		/// </summary>
		System.Int64  ContentId  { get; set; }
		
		/// <summary>
		/// DoctorId : 
		/// </summary>
		System.Int64?  DoctorId  { get; set; }
		
		/// <summary>
		/// RoomId : 
		/// </summary>
		System.Int64?  RoomId  { get; set; }
		
		/// <summary>
		/// NurseId : 
		/// </summary>
		System.Int64?  NurseId  { get; set; }
		
		/// <summary>
		/// StatusId : 
		/// </summary>
		System.Int64?  StatusId  { get; set; }
		
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

		#endregion Data Properties

	}
}


