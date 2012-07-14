﻿using System;
using System.ComponentModel;

namespace AppointmentSystem.Entities
{
	/// <summary>
	///		The data structure representation of the 'Users' table via interface.
	/// </summary>
	/// <remarks>
	/// 	This struct is generated by a tool and should never be modified.
	/// </remarks>
	public interface IUsers 
	{
		/// <summary>			
		/// Id : 
		/// </summary>
		/// <remarks>Member of the primary key of the underlying table "Users"</remarks>
		System.String Id { get; set; }
				
		/// <summary>
		/// keep a copy of the original so it can be used for editable primary keys.
		/// </summary>
		System.String OriginalId { get; set; }
			
		
		
		/// <summary>
		/// Username : 
		/// </summary>
		System.String  Username  { get; set; }
		
		/// <summary>
		/// Title : Dr, Mr, Ms...
		/// </summary>
		System.String  Title  { get; set; }
		
		/// <summary>
		/// Firstname : 
		/// </summary>
		System.String  Firstname  { get; set; }
		
		/// <summary>
		/// Lastname : 
		/// </summary>
		System.String  Lastname  { get; set; }
		
		/// <summary>
		/// DisplayName : 
		/// </summary>
		System.String  DisplayName  { get; set; }
		
		/// <summary>
		/// CellPhone : 
		/// </summary>
		System.String  CellPhone  { get; set; }
		
		/// <summary>
		/// Email : 
		/// </summary>
		System.String  Email  { get; set; }
		
		/// <summary>
		/// Avatar : 
		/// </summary>
		System.String  Avatar  { get; set; }
		
		/// <summary>
		/// Note : 
		/// </summary>
		System.String  Note  { get; set; }
		
		/// <summary>
		/// UserGroupId : This user belongs what groups. It's seperated by semi-comma
		/// </summary>
		System.String  UserGroupId  { get; set; }
		
		/// <summary>
		/// IsFemale : 
		/// </summary>
		System.Boolean  IsFemale  { get; set; }
		
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
		///	which are related to this object through the relation _userRoleUserId
		/// </summary>	
		TList<UserRole> UserRoleCollection {  get;  set;}	


		/// <summary>
		///	Holds a collection of entity objects
		///	which are related to this object through the relation _doctorRoomDoctorId
		/// </summary>	
		TList<DoctorRoom> DoctorRoomCollection {  get;  set;}	


		/// <summary>
		///	Holds a collection of entity objects
		///	which are related to this object through the relation _appointmentDoctorId
		/// </summary>	
		TList<Appointment> AppointmentCollection {  get;  set;}	


		/// <summary>
		///	Holds a collection of entity objects
		///	which are related to this object through the relation _rosterDoctorId
		/// </summary>	
		TList<Roster> RosterCollection {  get;  set;}	


		/// <summary>
		///	Holds a collection of entity objects
		///	which are related to this object through the relation _doctorServiceDoctorId
		/// </summary>	
		TList<DoctorService> DoctorServiceCollection {  get;  set;}	

		#endregion Data Properties

	}
}


