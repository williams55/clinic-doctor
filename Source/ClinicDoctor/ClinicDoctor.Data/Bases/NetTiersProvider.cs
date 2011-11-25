
#region Using directives

using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Configuration.Provider;

using ClinicDoctor.Entities;

#endregion

namespace ClinicDoctor.Data.Bases
{	
	///<summary>
	/// The base class to implements to create a .NetTiers provider.
	///</summary>
	public abstract class NetTiersProvider : NetTiersProviderBase
	{
		
		///<summary>
		/// Current RoomProviderBase instance.
		///</summary>
		public virtual RoomProviderBase RoomProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current AppointmentProviderBase instance.
		///</summary>
		public virtual AppointmentProviderBase AppointmentProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current RosterTypeProviderBase instance.
		///</summary>
		public virtual RosterTypeProviderBase RosterTypeProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current GroupProviderBase instance.
		///</summary>
		public virtual GroupProviderBase GroupProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current RoomFuncProviderBase instance.
		///</summary>
		public virtual RoomFuncProviderBase RoomFuncProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current RosterProviderBase instance.
		///</summary>
		public virtual RosterProviderBase RosterProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current StaffProviderBase instance.
		///</summary>
		public virtual StaffProviderBase StaffProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current RoleProviderBase instance.
		///</summary>
		public virtual RoleProviderBase RoleProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current StaffRolesProviderBase instance.
		///</summary>
		public virtual StaffRolesProviderBase StaffRolesProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current NurseAppointmentProviderBase instance.
		///</summary>
		public virtual NurseAppointmentProviderBase NurseAppointmentProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current ContentProviderBase instance.
		///</summary>
		public virtual ContentProviderBase ContentProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current StatusProviderBase instance.
		///</summary>
		public virtual StatusProviderBase StatusProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current CustomerProviderBase instance.
		///</summary>
		public virtual CustomerProviderBase CustomerProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current GroupRolesProviderBase instance.
		///</summary>
		public virtual GroupRolesProviderBase GroupRolesProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current DoctorFuncProviderBase instance.
		///</summary>
		public virtual DoctorFuncProviderBase DoctorFuncProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current DoctorRoomProviderBase instance.
		///</summary>
		public virtual DoctorRoomProviderBase DoctorRoomProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current DoctorRosterProviderBase instance.
		///</summary>
		public virtual DoctorRosterProviderBase DoctorRosterProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current FunctionalityProviderBase instance.
		///</summary>
		public virtual FunctionalityProviderBase FunctionalityProvider{get {throw new NotImplementedException();}}
		
		
	}
}
