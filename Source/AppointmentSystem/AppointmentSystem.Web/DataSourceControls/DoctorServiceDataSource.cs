#region Using Directives
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Web.UI;
using System.Web.UI.Design;

using AppointmentSystem.Entities;
using AppointmentSystem.Data;
using AppointmentSystem.Data.Bases;
#endregion

namespace AppointmentSystem.Web.Data
{
	/// <summary>
	/// Represents the DataRepository.DoctorServiceProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(DoctorServiceDataSourceDesigner))]
	public class DoctorServiceDataSource : ProviderDataSource<DoctorService, DoctorServiceKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DoctorServiceDataSource class.
		/// </summary>
		public DoctorServiceDataSource() : base(DataRepository.DoctorServiceProvider)
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the DoctorServiceDataSourceView used by the DoctorServiceDataSource.
		/// </summary>
		protected DoctorServiceDataSourceView DoctorServiceView
		{
			get { return ( View as DoctorServiceDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DoctorServiceDataSource control invokes to retrieve data.
		/// </summary>
		public DoctorServiceSelectMethod SelectMethod
		{
			get
			{
				DoctorServiceSelectMethod selectMethod = DoctorServiceSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (DoctorServiceSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the DoctorServiceDataSourceView class that is to be
		/// used by the DoctorServiceDataSource.
		/// </summary>
		/// <returns>An instance of the DoctorServiceDataSourceView class.</returns>
		protected override BaseDataSourceView<DoctorService, DoctorServiceKey> GetNewDataSourceView()
		{
			return new DoctorServiceDataSourceView(this, DefaultViewName);
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
	/// Supports the DoctorServiceDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class DoctorServiceDataSourceView : ProviderDataSourceView<DoctorService, DoctorServiceKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DoctorServiceDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the DoctorServiceDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public DoctorServiceDataSourceView(DoctorServiceDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal DoctorServiceDataSource DoctorServiceOwner
		{
			get { return Owner as DoctorServiceDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal DoctorServiceSelectMethod SelectMethod
		{
			get { return DoctorServiceOwner.SelectMethod; }
			set { DoctorServiceOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal DoctorServiceProviderBase DoctorServiceProvider
		{
			get { return Provider as DoctorServiceProviderBase; }
		}

		#endregion Properties
		
		#region Methods
		
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<DoctorService> GetSelectData(out int count)
		{
			Hashtable values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<DoctorService> results = null;
			DoctorService item;
			count = 0;
			
			System.Int64 _id;
			System.Int32 _serviceId;
			System.String _doctorId;

			switch ( SelectMethod )
			{
				case DoctorServiceSelectMethod.Get:
					DoctorServiceKey entityKey  = new DoctorServiceKey();
					entityKey.Load(values);
					item = DoctorServiceProvider.Get(GetTransactionManager(), entityKey);
					results = new TList<DoctorService>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case DoctorServiceSelectMethod.GetAll:
                    results = DoctorServiceProvider.GetAll(GetTransactionManager(), StartIndex, PageSize, out count);
                    break;
				case DoctorServiceSelectMethod.GetPaged:
					results = DoctorServiceProvider.GetPaged(GetTransactionManager(), WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case DoctorServiceSelectMethod.Find:
					if ( FilterParameters != null )
						results = DoctorServiceProvider.Find(GetTransactionManager(), FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = DoctorServiceProvider.Find(GetTransactionManager(), WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case DoctorServiceSelectMethod.GetById:
					_id = ( values["Id"] != null ) ? (System.Int64) EntityUtil.ChangeType(values["Id"], typeof(System.Int64)) : (long)0;
					item = DoctorServiceProvider.GetById(GetTransactionManager(), _id);
					results = new TList<DoctorService>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				case DoctorServiceSelectMethod.GetByServiceId:
					_serviceId = ( values["ServiceId"] != null ) ? (System.Int32) EntityUtil.ChangeType(values["ServiceId"], typeof(System.Int32)) : (int)0;
					results = DoctorServiceProvider.GetByServiceId(GetTransactionManager(), _serviceId, this.StartIndex, this.PageSize, out count);
					break;
				case DoctorServiceSelectMethod.GetByDoctorId:
					_doctorId = ( values["DoctorId"] != null ) ? (System.String) EntityUtil.ChangeType(values["DoctorId"], typeof(System.String)) : string.Empty;
					results = DoctorServiceProvider.GetByDoctorId(GetTransactionManager(), _doctorId, this.StartIndex, this.PageSize, out count);
					break;
				// M:M
				// Custom
				default:
					break;
			}

			if ( results != null && count < 1 )
			{
				count = results.Count;

				if ( !String.IsNullOrEmpty(CustomMethodRecordCountParamName) )
				{
					object objCustomCount = EntityUtil.ChangeType(customOutput[CustomMethodRecordCountParamName], typeof(Int32));
					
					if ( objCustomCount != null )
					{
						count = (int) objCustomCount;
					}
				}
			}
			
			return results;
		}
		
		/// <summary>
		/// Gets the values of any supplied parameters for internal caching.
		/// </summary>
		/// <param name="values">An IDictionary object of name/value pairs.</param>
		protected override void GetSelectParameters(IDictionary values)
		{
			if ( SelectMethod == DoctorServiceSelectMethod.Get || SelectMethod == DoctorServiceSelectMethod.GetById )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				DoctorService entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// execute deep load method
					DoctorServiceProvider.DeepLoad(GetTransactionManager(), GetCurrentEntity(), EnableRecursiveDeepLoad);
					// set loaded flag
					IsDeepLoaded = true;
				}
			}
		}

		/// <summary>
		/// Performs a DeepLoad operation on the specified entity collection.
		/// </summary>
		/// <param name="entityList"></param>
		/// <param name="properties"></param>
		internal override void DeepLoad(TList<DoctorService> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// execute deep load method
			DoctorServiceProvider.DeepLoad(GetTransactionManager(), entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region DoctorServiceDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the DoctorServiceDataSource class.
	/// </summary>
	public class DoctorServiceDataSourceDesigner : ProviderDataSourceDesigner<DoctorService, DoctorServiceKey>
	{
		/// <summary>
		/// Initializes a new instance of the DoctorServiceDataSourceDesigner class.
		/// </summary>
		public DoctorServiceDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public DoctorServiceSelectMethod SelectMethod
		{
			get { return ((DoctorServiceDataSource) DataSource).SelectMethod; }
			set { SetPropertyValue("SelectMethod", value); }
		}

		/// <summary>Gets the designer action list collection for this designer.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.Design.DesignerActionListCollection"/>
		/// associated with this designer.</returns>
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				DesignerActionListCollection actions = new DesignerActionListCollection();
				actions.Add(new DoctorServiceDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region DoctorServiceDataSourceActionList

	/// <summary>
	/// Supports the DoctorServiceDataSourceDesigner class.
	/// </summary>
	internal class DoctorServiceDataSourceActionList : DesignerActionList
	{
		private DoctorServiceDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the DoctorServiceDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public DoctorServiceDataSourceActionList(DoctorServiceDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public DoctorServiceSelectMethod SelectMethod
		{
			get { return _designer.SelectMethod; }
			set { _designer.SelectMethod = value; }
		}

		/// <summary>
		/// Returns the collection of <see cref="T:System.ComponentModel.Design.DesignerActionItem"/>
		/// objects contained in the list.
		/// </summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionItem"/>
		/// array that contains the items in this list.</returns>
		public override DesignerActionItemCollection GetSortedActionItems()
		{
			DesignerActionItemCollection items = new DesignerActionItemCollection();
			items.Add(new DesignerActionPropertyItem("SelectMethod", "Select Method", "Methods"));
			return items;
		}
	}

	#endregion DoctorServiceDataSourceActionList
	
	#endregion DoctorServiceDataSourceDesigner
	
	#region DoctorServiceSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the DoctorServiceDataSource.SelectMethod property.
	/// </summary>
	public enum DoctorServiceSelectMethod
	{
		/// <summary>
		/// Represents the Get method.
		/// </summary>
		Get,
		/// <summary>
		/// Represents the GetAll method.
		/// </summary>
		GetAll,
		/// <summary>
		/// Represents the GetPaged method.
		/// </summary>
		GetPaged,
		/// <summary>
		/// Represents the Find method.
		/// </summary>
		Find,
		/// <summary>
		/// Represents the GetById method.
		/// </summary>
		GetById,
		/// <summary>
		/// Represents the GetByServiceId method.
		/// </summary>
		GetByServiceId,
		/// <summary>
		/// Represents the GetByDoctorId method.
		/// </summary>
		GetByDoctorId
	}
	
	#endregion DoctorServiceSelectMethod

	#region DoctorServiceFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="DoctorService"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class DoctorServiceFilter : SqlFilter<DoctorServiceColumn>
	{
	}
	
	#endregion DoctorServiceFilter

	#region DoctorServiceExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="DoctorService"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class DoctorServiceExpressionBuilder : SqlExpressionBuilder<DoctorServiceColumn>
	{
	}
	
	#endregion DoctorServiceExpressionBuilder	

	#region DoctorServiceProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;DoctorServiceChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="DoctorService"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class DoctorServiceProperty : ChildEntityProperty<DoctorServiceChildEntityTypes>
	{
	}
	
	#endregion DoctorServiceProperty
}

