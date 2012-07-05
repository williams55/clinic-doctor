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
	/// Represents the DataRepository.AppointmentGroupProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(AppointmentGroupDataSourceDesigner))]
	public class AppointmentGroupDataSource : ProviderDataSource<AppointmentGroup, AppointmentGroupKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the AppointmentGroupDataSource class.
		/// </summary>
		public AppointmentGroupDataSource() : base(DataRepository.AppointmentGroupProvider)
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the AppointmentGroupDataSourceView used by the AppointmentGroupDataSource.
		/// </summary>
		protected AppointmentGroupDataSourceView AppointmentGroupView
		{
			get { return ( View as AppointmentGroupDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the AppointmentGroupDataSource control invokes to retrieve data.
		/// </summary>
		public AppointmentGroupSelectMethod SelectMethod
		{
			get
			{
				AppointmentGroupSelectMethod selectMethod = AppointmentGroupSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (AppointmentGroupSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the AppointmentGroupDataSourceView class that is to be
		/// used by the AppointmentGroupDataSource.
		/// </summary>
		/// <returns>An instance of the AppointmentGroupDataSourceView class.</returns>
		protected override BaseDataSourceView<AppointmentGroup, AppointmentGroupKey> GetNewDataSourceView()
		{
			return new AppointmentGroupDataSourceView(this, DefaultViewName);
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
	/// Supports the AppointmentGroupDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class AppointmentGroupDataSourceView : ProviderDataSourceView<AppointmentGroup, AppointmentGroupKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the AppointmentGroupDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the AppointmentGroupDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public AppointmentGroupDataSourceView(AppointmentGroupDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal AppointmentGroupDataSource AppointmentGroupOwner
		{
			get { return Owner as AppointmentGroupDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal AppointmentGroupSelectMethod SelectMethod
		{
			get { return AppointmentGroupOwner.SelectMethod; }
			set { AppointmentGroupOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal AppointmentGroupProviderBase AppointmentGroupProvider
		{
			get { return Provider as AppointmentGroupProviderBase; }
		}

		#endregion Properties
		
		#region Methods
		
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<AppointmentGroup> GetSelectData(out int count)
		{
			Hashtable values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<AppointmentGroup> results = null;
			AppointmentGroup item;
			count = 0;
			
			System.String _id;
			System.String _unitId_nullable;

			switch ( SelectMethod )
			{
				case AppointmentGroupSelectMethod.Get:
					AppointmentGroupKey entityKey  = new AppointmentGroupKey();
					entityKey.Load(values);
					item = AppointmentGroupProvider.Get(GetTransactionManager(), entityKey);
					results = new TList<AppointmentGroup>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case AppointmentGroupSelectMethod.GetAll:
                    results = AppointmentGroupProvider.GetAll(GetTransactionManager(), StartIndex, PageSize, out count);
                    break;
				case AppointmentGroupSelectMethod.GetPaged:
					results = AppointmentGroupProvider.GetPaged(GetTransactionManager(), WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case AppointmentGroupSelectMethod.Find:
					if ( FilterParameters != null )
						results = AppointmentGroupProvider.Find(GetTransactionManager(), FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = AppointmentGroupProvider.Find(GetTransactionManager(), WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case AppointmentGroupSelectMethod.GetById:
					_id = ( values["Id"] != null ) ? (System.String) EntityUtil.ChangeType(values["Id"], typeof(System.String)) : string.Empty;
					item = AppointmentGroupProvider.GetById(GetTransactionManager(), _id);
					results = new TList<AppointmentGroup>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				case AppointmentGroupSelectMethod.GetByUnitId:
					_unitId_nullable = (System.String) EntityUtil.ChangeType(values["UnitId"], typeof(System.String));
					results = AppointmentGroupProvider.GetByUnitId(GetTransactionManager(), _unitId_nullable, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == AppointmentGroupSelectMethod.Get || SelectMethod == AppointmentGroupSelectMethod.GetById )
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
				AppointmentGroup entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// execute deep load method
					AppointmentGroupProvider.DeepLoad(GetTransactionManager(), GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<AppointmentGroup> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// execute deep load method
			AppointmentGroupProvider.DeepLoad(GetTransactionManager(), entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region AppointmentGroupDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the AppointmentGroupDataSource class.
	/// </summary>
	public class AppointmentGroupDataSourceDesigner : ProviderDataSourceDesigner<AppointmentGroup, AppointmentGroupKey>
	{
		/// <summary>
		/// Initializes a new instance of the AppointmentGroupDataSourceDesigner class.
		/// </summary>
		public AppointmentGroupDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public AppointmentGroupSelectMethod SelectMethod
		{
			get { return ((AppointmentGroupDataSource) DataSource).SelectMethod; }
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
				actions.Add(new AppointmentGroupDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region AppointmentGroupDataSourceActionList

	/// <summary>
	/// Supports the AppointmentGroupDataSourceDesigner class.
	/// </summary>
	internal class AppointmentGroupDataSourceActionList : DesignerActionList
	{
		private AppointmentGroupDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the AppointmentGroupDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public AppointmentGroupDataSourceActionList(AppointmentGroupDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public AppointmentGroupSelectMethod SelectMethod
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

	#endregion AppointmentGroupDataSourceActionList
	
	#endregion AppointmentGroupDataSourceDesigner
	
	#region AppointmentGroupSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the AppointmentGroupDataSource.SelectMethod property.
	/// </summary>
	public enum AppointmentGroupSelectMethod
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
		/// Represents the GetByUnitId method.
		/// </summary>
		GetByUnitId
	}
	
	#endregion AppointmentGroupSelectMethod

	#region AppointmentGroupFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="AppointmentGroup"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class AppointmentGroupFilter : SqlFilter<AppointmentGroupColumn>
	{
	}
	
	#endregion AppointmentGroupFilter

	#region AppointmentGroupExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="AppointmentGroup"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class AppointmentGroupExpressionBuilder : SqlExpressionBuilder<AppointmentGroupColumn>
	{
	}
	
	#endregion AppointmentGroupExpressionBuilder	

	#region AppointmentGroupProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;AppointmentGroupChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="AppointmentGroup"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class AppointmentGroupProperty : ChildEntityProperty<AppointmentGroupChildEntityTypes>
	{
	}
	
	#endregion AppointmentGroupProperty
}

