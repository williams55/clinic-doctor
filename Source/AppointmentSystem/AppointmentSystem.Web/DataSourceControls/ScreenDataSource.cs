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
	/// Represents the DataRepository.ScreenProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(ScreenDataSourceDesigner))]
	public class ScreenDataSource : ProviderDataSource<Screen, ScreenKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ScreenDataSource class.
		/// </summary>
		public ScreenDataSource() : base(DataRepository.ScreenProvider)
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the ScreenDataSourceView used by the ScreenDataSource.
		/// </summary>
		protected ScreenDataSourceView ScreenView
		{
			get { return ( View as ScreenDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the ScreenDataSource control invokes to retrieve data.
		/// </summary>
		public ScreenSelectMethod SelectMethod
		{
			get
			{
				ScreenSelectMethod selectMethod = ScreenSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (ScreenSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the ScreenDataSourceView class that is to be
		/// used by the ScreenDataSource.
		/// </summary>
		/// <returns>An instance of the ScreenDataSourceView class.</returns>
		protected override BaseDataSourceView<Screen, ScreenKey> GetNewDataSourceView()
		{
			return new ScreenDataSourceView(this, DefaultViewName);
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
	/// Supports the ScreenDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class ScreenDataSourceView : ProviderDataSourceView<Screen, ScreenKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ScreenDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the ScreenDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public ScreenDataSourceView(ScreenDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal ScreenDataSource ScreenOwner
		{
			get { return Owner as ScreenDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal ScreenSelectMethod SelectMethod
		{
			get { return ScreenOwner.SelectMethod; }
			set { ScreenOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal ScreenProviderBase ScreenProvider
		{
			get { return Provider as ScreenProviderBase; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<Screen> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<Screen> results = null;
			Screen item;
			count = 0;
			
			System.String _screenCode;

			switch ( SelectMethod )
			{
				case ScreenSelectMethod.Get:
					ScreenKey entityKey  = new ScreenKey();
					entityKey.Load(values);
					item = ScreenProvider.Get(GetTransactionManager(), entityKey);
					results = new TList<Screen>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case ScreenSelectMethod.GetAll:
                    results = ScreenProvider.GetAll(GetTransactionManager(), StartIndex, PageSize, out count);
                    break;
				case ScreenSelectMethod.GetPaged:
					results = ScreenProvider.GetPaged(GetTransactionManager(), WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case ScreenSelectMethod.Find:
					if ( FilterParameters != null )
						results = ScreenProvider.Find(GetTransactionManager(), FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = ScreenProvider.Find(GetTransactionManager(), WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case ScreenSelectMethod.GetByScreenCode:
					_screenCode = ( values["ScreenCode"] != null ) ? (System.String) EntityUtil.ChangeType(values["ScreenCode"], typeof(System.String)) : string.Empty;
					item = ScreenProvider.GetByScreenCode(GetTransactionManager(), _screenCode);
					results = new TList<Screen>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
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
			if ( SelectMethod == ScreenSelectMethod.Get || SelectMethod == ScreenSelectMethod.GetByScreenCode )
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
				Screen entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// execute deep load method
					ScreenProvider.DeepLoad(GetTransactionManager(), GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<Screen> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// execute deep load method
			ScreenProvider.DeepLoad(GetTransactionManager(), entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region ScreenDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the ScreenDataSource class.
	/// </summary>
	public class ScreenDataSourceDesigner : ProviderDataSourceDesigner<Screen, ScreenKey>
	{
		/// <summary>
		/// Initializes a new instance of the ScreenDataSourceDesigner class.
		/// </summary>
		public ScreenDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public ScreenSelectMethod SelectMethod
		{
			get { return ((ScreenDataSource) DataSource).SelectMethod; }
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
				actions.Add(new ScreenDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region ScreenDataSourceActionList

	/// <summary>
	/// Supports the ScreenDataSourceDesigner class.
	/// </summary>
	internal class ScreenDataSourceActionList : DesignerActionList
	{
		private ScreenDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the ScreenDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public ScreenDataSourceActionList(ScreenDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public ScreenSelectMethod SelectMethod
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

	#endregion ScreenDataSourceActionList
	
	#endregion ScreenDataSourceDesigner
	
	#region ScreenSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the ScreenDataSource.SelectMethod property.
	/// </summary>
	public enum ScreenSelectMethod
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
		/// Represents the GetByScreenCode method.
		/// </summary>
		GetByScreenCode
	}
	
	#endregion ScreenSelectMethod

	#region ScreenFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Screen"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ScreenFilter : SqlFilter<ScreenColumn>
	{
	}
	
	#endregion ScreenFilter

	#region ScreenExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Screen"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ScreenExpressionBuilder : SqlExpressionBuilder<ScreenColumn>
	{
	}
	
	#endregion ScreenExpressionBuilder	

	#region ScreenProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;ScreenChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="Screen"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ScreenProperty : ChildEntityProperty<ScreenChildEntityTypes>
	{
	}
	
	#endregion ScreenProperty
}

