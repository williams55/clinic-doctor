﻿using System;
using System.ComponentModel;

namespace ClinicDoctor.Entities
{
	/// <summary>
	///		The data structure representation of the 'Staff' table via interface.
	/// </summary>
	/// <remarks>
	/// 	This struct is generated by a tool and should never be modified.
	/// </remarks>
	public interface IStaff 
	{
		/// <summary>			
		/// Id : 
		/// </summary>
		/// <remarks>Member of the primary key of the underlying table "Staff"</remarks>
		System.Int64 Id { get; set; }
				
		
		
		/// <summary>
		/// FirstName : 
		/// </summary>
		System.String  FirstName  { get; set; }
		
		/// <summary>
		/// LastName : 
		/// </summary>
		System.String  LastName  { get; set; }
		
		/// <summary>
		/// ShortName : 
		/// </summary>
		System.String  ShortName  { get; set; }
		
		/// <summary>
		/// GroupId : 
		/// </summary>
		System.Int64  GroupId  { get; set; }
		
		/// <summary>
		/// UserName : 
		/// </summary>
		System.String  UserName  { get; set; }
		
		/// <summary>
		/// Address : 
		/// </summary>
		System.String  Address  { get; set; }
		
		/// <summary>
		/// HomePhone : 
		/// </summary>
		System.String  HomePhone  { get; set; }
		
		/// <summary>
		/// WorkPhone : 
		/// </summary>
		System.String  WorkPhone  { get; set; }
		
		/// <summary>
		/// CellPhone : 
		/// </summary>
		System.String  CellPhone  { get; set; }
		
		/// <summary>
		/// Birthdate : 
		/// </summary>
		System.DateTime?  Birthdate  { get; set; }
		
		/// <summary>
		/// IsFemale : 
		/// </summary>
		System.Boolean  IsFemale  { get; set; }
		
		/// <summary>
		/// Title : 
		/// </summary>
		System.String  Title  { get; set; }
		
		/// <summary>
		/// Note : 
		/// </summary>
		System.String  Note  { get; set; }
		
		/// <summary>
		/// Roles : 
		/// </summary>
		System.String  Roles  { get; set; }
		
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
		///	which are related to this object through the relation _doctorRoomDoctorId
		/// </summary>	
		TList<DoctorRoom> DoctorRoomCollection {  get;  set;}	


		/// <summary>
		///	Holds a collection of entity objects
		///	which are related to this object through the relation _appointmentNurseIdGetByNurseId
		/// </summary>	
		TList<Appointment> AppointmentCollectionGetByNurseId {  get;  set;}	


		/// <summary>
		///	Holds a collection of entity objects
		///	which are related to this object through the relation _appointmentNurseIdGetByDoctorId
		/// </summary>	
		TList<Appointment> AppointmentCollectionGetByDoctorId {  get;  set;}	


		/// <summary>
		///	Holds a collection of entity objects
		///	which are related to this object through the relation _doctorRosterDoctorId
		/// </summary>	
		TList<DoctorRoster> DoctorRosterCollection {  get;  set;}	


		/// <summary>
		///	Holds a collection of entity objects
		///	which are related to this object through the relation _doctorFuncDoctorId
		/// </summary>	
		TList<DoctorFunc> DoctorFuncCollection {  get;  set;}	

		#endregion Data Properties

	}
}

