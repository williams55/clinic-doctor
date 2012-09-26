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
	/// Represents the DataRepository.VcsMemberTypeProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[CLSCompliant(true)]
	[Designer(typeof(VcsMemberTypeDataSourceDesigner))]
	public class VcsMemberTypeDataSource : ReadOnlyDataSource<VcsMemberType>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VcsMemberTypeDataSource class.
		/// </summary>
		public VcsMemberTypeDataSource() : base(DataRepository.VcsMemberTypeProvider)
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the VcsMemberTypeDataSourceView used by the VcsMemberTypeDataSource.
		/// </summary>
		protected VcsMemberTypeDataSourceView VcsMemberTypeView
		{
			get { return ( View as VcsMemberTypeDataSourceView ); }
		}
		
		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the VcsMemberTypeDataSourceView class that is to be
		/// used by the VcsMemberTypeDataSource.
		/// </summary>
		/// <returns>An instance of the VcsMemberTypeDataSourceView class.</returns>
		protected override BaseDataSourceView<VcsMemberType, Object> GetNewDataSourceView()
		{
			return new VcsMemberTypeDataSourceView(this, DefaultViewName);
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
	/// Supports the VcsMemberTypeDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class VcsMemberTypeDataSourceView : ReadOnlyDataSourceView<VcsMemberType>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VcsMemberTypeDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the VcsMemberTypeDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public VcsMemberTypeDataSourceView(VcsMemberTypeDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal VcsMemberTypeDataSource VcsMemberTypeOwner
		{
			get { return Owner as VcsMemberTypeDataSource; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal VcsMemberTypeProviderBase VcsMemberTypeProvider
		{
			get { return Provider as VcsMemberTypeProviderBase; }
		}

		#endregion Properties
		
		#region Methods
		
		#endregion Methods
	}

	#region VcsMemberTypeDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the VcsMemberTypeDataSource class.
	/// </summary>
	public class VcsMemberTypeDataSourceDesigner : ReadOnlyDataSourceDesigner<VcsMemberType>
	{
	}

	#endregion VcsMemberTypeDataSourceDesigner

	#region VcsMemberTypeFilter

	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="VcsMemberType"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VcsMemberTypeFilter : SqlFilter<VcsMemberTypeColumn>
	{
	}

	#endregion VcsMemberTypeFilter

	#region VcsMemberTypeExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="VcsMemberType"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VcsMemberTypeExpressionBuilder : SqlExpressionBuilder<VcsMemberTypeColumn>
	{
	}
	
	#endregion VcsMemberTypeExpressionBuilder		
}

