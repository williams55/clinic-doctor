
#region Using directives

using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Configuration.Provider;

using AppointmentSystem.Entities;

#endregion

namespace AppointmentSystem.Data.Bases
{	
	///<summary>
	/// The base class to implements to create a .NetTiers provider.
	///</summary>
	public abstract class NetTiersProvider : NetTiersProviderBase
	{
		
		///<summary>
		/// Current RoleProviderBase instance.
		///</summary>
		public virtual RoleProviderBase RoleProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current RoomProviderBase instance.
		///</summary>
		public virtual RoomProviderBase RoomProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current StatusProviderBase instance.
		///</summary>
		public virtual StatusProviderBase StatusProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current UserGroupProviderBase instance.
		///</summary>
		public virtual UserGroupProviderBase UserGroupProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current AppointmentProviderBase instance.
		///</summary>
		public virtual AppointmentProviderBase AppointmentProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current RoleDetailProviderBase instance.
		///</summary>
		public virtual RoleDetailProviderBase RoleDetailProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current DoctorRoomProviderBase instance.
		///</summary>
		public virtual DoctorRoomProviderBase DoctorRoomProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current UserRoleProviderBase instance.
		///</summary>
		public virtual UserRoleProviderBase UserRoleProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current GroupRoleProviderBase instance.
		///</summary>
		public virtual GroupRoleProviderBase GroupRoleProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current AppointmentGroupProviderBase instance.
		///</summary>
		public virtual AppointmentGroupProviderBase AppointmentGroupProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current PatientProviderBase instance.
		///</summary>
		public virtual PatientProviderBase PatientProvider{get {throw new NotImplementedException();}}
		
		
	}
}
