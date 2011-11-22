#region Using Directives
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Web.UI;
using System.Web.UI.Design;

using ClinicDoctor.Entities;
using ClinicDoctor.Data;
using ClinicDoctor.Data.Bases;
#endregion

namespace ClinicDoctor.Web.Data
{
	/// <summary>
	/// Represents the DataRepository.DoctorFuncProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(DoctorFuncDataSourceDesigner))]
	public class DoctorFuncDataSource : ProviderDataSource<DoctorFunc, DoctorFuncKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DoctorFuncDataSource class.
		/// </summary>
		public DoctorFuncDataSource() : base(DataRepository.DoctorFuncProvider)
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the DoctorFuncDataSourceView used by the DoctorFuncDataSource.
		/// </summary>
		protected DoctorFuncDataSourceView DoctorFuncView
		{
			get { return ( View as DoctorFuncDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DoctorFuncDataSource control invokes to retrieve data.
		/// </summary>
		public DoctorFuncSelectMethod SelectMethod
		{
			get
			{
				DoctorFuncSelectMethod selectMethod = DoctorFuncSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (DoctorFuncSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the DoctorFuncDataSourceView class that is to be
		/// used by the DoctorFuncDataSource.
		/// </summary>
		/// <returns>An instance of the DoctorFuncDataSourceView class.</returns>
		protected override BaseDataSourceView<DoctorFunc, DoctorFuncKey> GetNewDataSourceView()
		{
			return new DoctorFuncDataSourceView(this, DefaultViewName);
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
	/// Supports the DoctorFuncDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class DoctorFuncDataSourceView : ProviderDataSourceView<DoctorFunc, DoctorFuncKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DoctorFuncDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the DoctorFuncDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public DoctorFuncDataSourceView(DoctorFuncDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal DoctorFuncDataSource DoctorFuncOwner
		{
			get { return Owner as DoctorFuncDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal DoctorFuncSelectMethod SelectMethod
		{
			get { return DoctorFuncOwner.SelectMethod; }
			set { DoctorFuncOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal DoctorFuncProviderBase DoctorFuncProvider
		{
			get { return Provider as DoctorFuncProviderBase; }
		}

		#endregion Properties
		
		#region Methods
		
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<DoctorFunc> GetSelectData(out int count)
		{
			Hashtable values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<DoctorFunc> results = null;
			DoctorFunc item;
			count = 0;
			
			System.Int32 _id;
			System.Int32? _funcId_nullable;
			System.Int32? _doctorId_nullable;

			switch ( SelectMethod )
			{
				case DoctorFuncSelectMethod.Get:
					DoctorFuncKey entityKey  = new DoctorFuncKey();
					entityKey.Load(values);
					item = DoctorFuncProvider.Get(GetTransactionManager(), entityKey);
					results = new TList<DoctorFunc>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case DoctorFuncSelectMethod.GetAll:
                    results = DoctorFuncProvider.GetAll(GetTransactionManager(), StartIndex, PageSize, out count);
                    break;
				case DoctorFuncSelectMethod.GetPaged:
					results = DoctorFuncProvider.GetPaged(GetTransactionManager(), WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case DoctorFuncSelectMethod.Find:
					if ( FilterParameters != null )
						results = DoctorFuncProvider.Find(GetTransactionManager(), FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = DoctorFuncProvider.Find(GetTransactionManager(), WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case DoctorFuncSelectMethod.GetById:
					_id = ( values["Id"] != null ) ? (System.Int32) EntityUtil.ChangeType(values["Id"], typeof(System.Int32)) : (int)0;
					item = DoctorFuncProvider.GetById(GetTransactionManager(), _id);
					results = new TList<DoctorFunc>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				case DoctorFuncSelectMethod.GetByFuncId:
					_funcId_nullable = (System.Int32?) EntityUtil.ChangeType(values["FuncId"], typeof(System.Int32?));
					results = DoctorFuncProvider.GetByFuncId(GetTransactionManager(), _funcId_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case DoctorFuncSelectMethod.GetByDoctorId:
					_doctorId_nullable = (System.Int32?) EntityUtil.ChangeType(values["DoctorId"], typeof(System.Int32?));
					results = DoctorFuncProvider.GetByDoctorId(GetTransactionManager(), _doctorId_nullable, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == DoctorFuncSelectMethod.Get || SelectMethod == DoctorFuncSelectMethod.GetById )
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
				DoctorFunc entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// execute deep load method
					DoctorFuncProvider.DeepLoad(GetTransactionManager(), GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<DoctorFunc> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// execute deep load method
			DoctorFuncProvider.DeepLoad(GetTransactionManager(), entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region DoctorFuncDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the DoctorFuncDataSource class.
	/// </summary>
	public class DoctorFuncDataSourceDesigner : ProviderDataSourceDesigner<DoctorFunc, DoctorFuncKey>
	{
		/// <summary>
		/// Initializes a new instance of the DoctorFuncDataSourceDesigner class.
		/// </summary>
		public DoctorFuncDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public DoctorFuncSelectMethod SelectMethod
		{
			get { return ((DoctorFuncDataSource) DataSource).SelectMethod; }
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
				actions.Add(new DoctorFuncDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region DoctorFuncDataSourceActionList

	/// <summary>
	/// Supports the DoctorFuncDataSourceDesigner class.
	/// </summary>
	internal class DoctorFuncDataSourceActionList : DesignerActionList
	{
		private DoctorFuncDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the DoctorFuncDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public DoctorFuncDataSourceActionList(DoctorFuncDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public DoctorFuncSelectMethod SelectMethod
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

	#endregion DoctorFuncDataSourceActionList
	
	#endregion DoctorFuncDataSourceDesigner
	
	#region DoctorFuncSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the DoctorFuncDataSource.SelectMethod property.
	/// </summary>
	public enum DoctorFuncSelectMethod
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
		/// Represents the GetByFuncId method.
		/// </summary>
		GetByFuncId,
		/// <summary>
		/// Represents the GetByDoctorId method.
		/// </summary>
		GetByDoctorId
	}
	
	#endregion DoctorFuncSelectMethod

	#region DoctorFuncFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="DoctorFunc"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class DoctorFuncFilter : SqlFilter<DoctorFuncColumn>
	{
	}
	
	#endregion DoctorFuncFilter

	#region DoctorFuncExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="DoctorFunc"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class DoctorFuncExpressionBuilder : SqlExpressionBuilder<DoctorFuncColumn>
	{
	}
	
	#endregion DoctorFuncExpressionBuilder	

	#region DoctorFuncProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;DoctorFuncChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="DoctorFunc"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class DoctorFuncProperty : ChildEntityProperty<DoctorFuncChildEntityTypes>
	{
	}
	
	#endregion DoctorFuncProperty
}

