﻿using System;
using System.ComponentModel;

namespace AppointmentSystem.Entities
{
	/// <summary>
	///		The data structure representation of the 'Room' table via interface.
	/// </summary>
	/// <remarks>
	/// 	This struct is generated by a tool and should never be modified.
	/// </remarks>
	public interface IRoom 
	{
		/// <summary>			
		/// Id : 
		/// </summary>
		/// <remarks>Member of the primary key of the underlying table "Room"</remarks>
		System.String Id { get; set; }
				
		/// <summary>
		/// keep a copy of the original so it can be used for editable primary keys.
		/// </summary>
		System.String OriginalId { get; set; }
			
		
		
		/// <summary>
		/// Title : 
		/// </summary>
		System.String  Title  { get; set; }
		
		/// <summary>
		/// Note : 
		/// </summary>
		System.String  Note  { get; set; }
		
		/// <summary>
		/// ServicesId : A room can have many procedures. They are seperated by semi-comma [;]
		/// 		/// For example: XRay;MRI
		/// </summary>
		System.String  ServicesId  { get; set; }
		
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
		///	which are related to this object through the relation _doctorRoomRoomId
		/// </summary>	
		TList<DoctorRoom> DoctorRoomCollection {  get;  set;}	


		/// <summary>
		///	Holds a collection of entity objects
		///	which are related to this object through the relation _appointmentRoomId
		/// </summary>	
		TList<Appointment> AppointmentCollection {  get;  set;}	

		#endregion Data Properties

	}
}


