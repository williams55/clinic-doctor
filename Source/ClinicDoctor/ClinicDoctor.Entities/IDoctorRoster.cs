﻿using System;
using System.ComponentModel;

namespace ClinicDoctor.Entities
{
	/// <summary>
	///		The data structure representation of the 'DoctorRoster' table via interface.
	/// </summary>
	/// <remarks>
	/// 	This struct is generated by a tool and should never be modified.
	/// </remarks>
	public interface IDoctorRoster 
	{
		/// <summary>			
		/// Id : 
		/// </summary>
		/// <remarks>Member of the primary key of the underlying table "DoctorRoster"</remarks>
		System.Int32 Id { get; set; }
				
		
		
		/// <summary>
		/// DoctorId : 
		/// </summary>
		System.Int32?  DoctorId  { get; set; }
		
		/// <summary>
		/// RosterId : 
		/// </summary>
		System.Int32?  RosterId  { get; set; }
		
		/// <summary>
		/// StartTime : 
		/// </summary>
		System.DateTime?  StartTime  { get; set; }
		
		/// <summary>
		/// EndTime : 
		/// </summary>
		System.DateTime?  EndTime  { get; set; }
		
		/// <summary>
		/// Note : 
		/// </summary>
		System.String  Note  { get; set; }
		
		/// <summary>
		/// IsDisabled : 
		/// </summary>
		System.Boolean?  IsDisabled  { get; set; }
		
		/// <summary>
		/// CreateUser : 
		/// </summary>
		System.String  CreateUser  { get; set; }
		
		/// <summary>
		/// CreateDate : 
		/// </summary>
		System.DateTime?  CreateDate  { get; set; }
		
		/// <summary>
		/// UpdateUser : 
		/// </summary>
		System.String  UpdateUser  { get; set; }
		
		/// <summary>
		/// UpdateDate : 
		/// </summary>
		System.DateTime?  UpdateDate  { get; set; }
			
		/// <summary>
		/// Creates a new object that is a copy of the current instance.
		/// </summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		System.Object Clone();
		
		#region Data Properties

		#endregion Data Properties

	}
}

