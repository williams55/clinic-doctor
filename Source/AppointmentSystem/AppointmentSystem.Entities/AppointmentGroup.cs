#region Using directives

using System;

#endregion

namespace AppointmentSystem.Entities
{	
	///<summary>
	/// This table group follow floor or something like that. For example, 1st Floor is a group and there are some staffs [Doctors, ProcedureGroup]	
	///</summary>
	/// <remarks>
	/// This file is generated once and will never be overwritten.
	/// </remarks>	
	[Serializable]
	[CLSCompliant(true)]
	public partial class AppointmentGroup : AppointmentGroupBase
	{		
		#region Constructors

		///<summary>
		/// Creates a new <see cref="AppointmentGroup"/> instance.
		///</summary>
		public AppointmentGroup():base(){}	
		
		#endregion
	}
}
