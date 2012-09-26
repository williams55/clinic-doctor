#region Using Directives
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using AppointmentSystem.Entities;
using AppointmentSystem.Data;
using AppointmentSystem.Data.Bases;
#endregion

namespace AppointmentSystem.Web.Data
{
	/// <summary>
	/// Represents the DataRepository.VcsPatientProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[CLSCompliant(true)]
	[Designer(typeof(VcsPatientDataSourceDesigner))]
	public class VcsPatientDataSource : ReadOnlyDataSource<VcsPatient>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VcsPatientDataSource class.
		/// </summary>
		public VcsPatientDataSource() : base(DataRepository.VcsPatientProvider)
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the VcsPatientDataSourceView used by the VcsPatientDataSource.
		/// </summary>
		protected VcsPatientDataSourceView VcsPatientView
		{
			get { return ( View as VcsPatientDataSourceView ); }
		}
		
		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the VcsPatientDataSourceView class that is to be
		/// used by the VcsPatientDataSource.
		/// </summary>
		/// <returns>An instance of the VcsPatientDataSourceView class.</returns>
		protected override BaseDataSourceView<VcsPatient, Object> GetNewDataSourceView()
		{
			return new VcsPatientDataSourceView(this, DefaultViewName);
		}
		
		/// <summary>
        /// Creates a cache hashing key based on the startIndex, pageSize and the SelectMethod being used.
        /// </summary>
        /// <param name="startIndex">The current start row index.</param>
        /// <param name="pageSize">The current page size.</param>
        /// <returns>A string that can be used as a key for caching purposes.</returns>
		protected override string CacheHashKey(int startIndex, int pageSize)
        {
			return String.Format("{0}:{1}:{2}", SelectMethod, startIndex, pageSize);
        }
		
		#endregion Methods
	}
	
	/// <summary>
	/// Supports the VcsPatientDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class VcsPatientDataSourceView : ReadOnlyDataSourceView<VcsPatient>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VcsPatientDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the VcsPatientDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public VcsPatientDataSourceView(VcsPatientDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal VcsPatientDataSource VcsPatientOwner
		{
			get { return Owner as VcsPatientDataSource; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal VcsPatientProviderBase VcsPatientProvider
		{
			get { return Provider as VcsPatientProviderBase; }
		}

		#endregion Properties
		
		#region Methods
		
		#endregion Methods
	}

	#region VcsPatientDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the VcsPatientDataSource class.
	/// </summary>
	public class VcsPatientDataSourceDesigner : ReadOnlyDataSourceDesigner<VcsPatient>
	{
	}

	#endregion VcsPatientDataSourceDesigner

	#region VcsPatientFilter

	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="VcsPatient"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VcsPatientFilter : SqlFilter<VcsPatientColumn>
	{
	}

	#endregion VcsPatientFilter

	#region VcsPatientExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="VcsPatient"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VcsPatientExpressionBuilder : SqlExpressionBuilder<VcsPatientColumn>
	{
	}
	
	#endregion VcsPatientExpressionBuilder		
}

