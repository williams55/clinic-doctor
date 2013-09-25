﻿using System;
using System.ComponentModel;

namespace AppointmentSystem.Entities
{
	/// <summary>
	///		The data structure representation of the 'Screen' table via interface.
	/// </summary>
	/// <remarks>
	/// 	This struct is generated by a tool and should never be modified.
	/// </remarks>
	public interface IScreen 
	{
		/// <summary>			
		/// ScreenCode : Link name of screen. 
		/// 		/// Ex: Status, Appointment...
		/// </summary>
		/// <remarks>Member of the primary key of the underlying table "Screen"</remarks>
		System.String ScreenCode { get; set; }
				
		/// <summary>
		/// keep a copy of the original so it can be used for editable primary keys.
		/// </summary>
		System.String OriginalScreenCode { get; set; }
			
		
		
		/// <summary>
		/// ScreenName : 
		/// </summary>
		System.String  ScreenName  { get; set; }
		
		/// <summary>
		/// PriorityIndex : 
		/// </summary>
		System.Int32  PriorityIndex  { get; set; }
		
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
		///	which are related to this object through the relation _roleDetailScreenCode
		/// </summary>	
		TList<RoleDetail> RoleDetailCollection {  get;  set;}	

		#endregion Data Properties

	}
}


