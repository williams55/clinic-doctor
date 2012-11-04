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
	/// Represents the DataRepository.VcsCountryProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[CLSCompliant(true)]
	[Designer(typeof(VcsCountryDataSourceDesigner))]
	public class VcsCountryDataSource : ReadOnlyDataSource<VcsCountry>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VcsCountryDataSource class.
		/// </summary>
		public VcsCountryDataSource() : base(DataRepository.VcsCountryProvider)
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the VcsCountryDataSourceView used by the VcsCountryDataSource.
		/// </summary>
		protected VcsCountryDataSourceView VcsCountryView
		{
			get { return ( View as VcsCountryDataSourceView ); }
		}
		
		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the VcsCountryDataSourceView class that is to be
		/// used by the VcsCountryDataSource.
		/// </summary>
		/// <returns>An instance of the VcsCountryDataSourceView class.</returns>
		protected override BaseDataSourceView<VcsCountry, Object> GetNewDataSourceView()
		{
			return new VcsCountryDataSourceView(this, DefaultViewName);
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
	/// Supports the VcsCountryDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class VcsCountryDataSourceView : ReadOnlyDataSourceView<VcsCountry>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VcsCountryDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the VcsCountryDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public VcsCountryDataSourceView(VcsCountryDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal VcsCountryDataSource VcsCountryOwner
		{
			get { return Owner as VcsCountryDataSource; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal VcsCountryProviderBase VcsCountryProvider
		{
			get { return Provider as VcsCountryProviderBase; }
		}

		#endregion Properties
		
		#region Methods
		
		#endregion Methods
	}

	#region VcsCountryDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the VcsCountryDataSource class.
	/// </summary>
	public class VcsCountryDataSourceDesigner : ReadOnlyDataSourceDesigner<VcsCountry>
	{
	}

	#endregion VcsCountryDataSourceDesigner

	#region VcsCountryFilter

	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="VcsCountry"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VcsCountryFilter : SqlFilter<VcsCountryColumn>
	{
	}

	#endregion VcsCountryFilter

	#region VcsCountryExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="VcsCountry"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VcsCountryExpressionBuilder : SqlExpressionBuilder<VcsCountryColumn>
	{
	}
	
	#endregion VcsCountryExpressionBuilder		
}

