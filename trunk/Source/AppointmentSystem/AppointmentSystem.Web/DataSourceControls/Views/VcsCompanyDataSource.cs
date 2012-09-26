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
	/// Represents the DataRepository.VcsCompanyProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[CLSCompliant(true)]
	[Designer(typeof(VcsCompanyDataSourceDesigner))]
	public class VcsCompanyDataSource : ReadOnlyDataSource<VcsCompany>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VcsCompanyDataSource class.
		/// </summary>
		public VcsCompanyDataSource() : base(DataRepository.VcsCompanyProvider)
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the VcsCompanyDataSourceView used by the VcsCompanyDataSource.
		/// </summary>
		protected VcsCompanyDataSourceView VcsCompanyView
		{
			get { return ( View as VcsCompanyDataSourceView ); }
		}
		
		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the VcsCompanyDataSourceView class that is to be
		/// used by the VcsCompanyDataSource.
		/// </summary>
		/// <returns>An instance of the VcsCompanyDataSourceView class.</returns>
		protected override BaseDataSourceView<VcsCompany, Object> GetNewDataSourceView()
		{
			return new VcsCompanyDataSourceView(this, DefaultViewName);
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
	/// Supports the VcsCompanyDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class VcsCompanyDataSourceView : ReadOnlyDataSourceView<VcsCompany>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VcsCompanyDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the VcsCompanyDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public VcsCompanyDataSourceView(VcsCompanyDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal VcsCompanyDataSource VcsCompanyOwner
		{
			get { return Owner as VcsCompanyDataSource; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal VcsCompanyProviderBase VcsCompanyProvider
		{
			get { return Provider as VcsCompanyProviderBase; }
		}

		#endregion Properties
		
		#region Methods
		
		#endregion Methods
	}

	#region VcsCompanyDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the VcsCompanyDataSource class.
	/// </summary>
	public class VcsCompanyDataSourceDesigner : ReadOnlyDataSourceDesigner<VcsCompany>
	{
	}

	#endregion VcsCompanyDataSourceDesigner

	#region VcsCompanyFilter

	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="VcsCompany"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VcsCompanyFilter : SqlFilter<VcsCompanyColumn>
	{
	}

	#endregion VcsCompanyFilter

	#region VcsCompanyExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="VcsCompany"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VcsCompanyExpressionBuilder : SqlExpressionBuilder<VcsCompanyColumn>
	{
	}
	
	#endregion VcsCompanyExpressionBuilder		
}

