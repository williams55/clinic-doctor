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
	/// Represents the DataRepository.StatusProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(StatusDataSourceDesigner))]
	public class StatusDataSource : ProviderDataSource<Status, StatusKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the StatusDataSource class.
		/// </summary>
		public StatusDataSource() : base(DataRepository.StatusProvider)
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the StatusDataSourceView used by the StatusDataSource.
		/// </summary>
		protected StatusDataSourceView StatusView
		{
			get { return ( View as StatusDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the StatusDataSource control invokes to retrieve data.
		/// </summary>
		public StatusSelectMethod SelectMethod
		{
			get
			{
				StatusSelectMethod selectMethod = StatusSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (StatusSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the StatusDataSourceView class that is to be
		/// used by the StatusDataSource.
		/// </summary>
		/// <returns>An instance of the StatusDataSourceView class.</returns>
		protected override BaseDataSourceView<Status, StatusKey> GetNewDataSourceView()
		{
			return new StatusDataSourceView(this, DefaultViewName);
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
	/// Supports the StatusDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class StatusDataSourceView : ProviderDataSourceView<Status, StatusKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the StatusDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the StatusDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public StatusDataSourceView(StatusDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal StatusDataSource StatusOwner
		{
			get { return Owner as StatusDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal StatusSelectMethod SelectMethod
		{
			get { return StatusOwner.SelectMethod; }
			set { StatusOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal StatusProviderBase StatusProvider
		{
			get { return Provider as StatusProviderBase; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<Status> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<Status> results = null;
			Status item;
			count = 0;
			
			System.String _id;

			switch ( SelectMethod )
			{
				case StatusSelectMethod.Get:
					StatusKey entityKey  = new StatusKey();
					entityKey.Load(values);
					item = StatusProvider.Get(GetTransactionManager(), entityKey);
					results = new TList<Status>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case StatusSelectMethod.GetAll:
                    results = StatusProvider.GetAll(GetTransactionManager(), StartIndex, PageSize, out count);
                    break;
				case StatusSelectMethod.GetPaged:
					results = StatusProvider.GetPaged(GetTransactionManager(), WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case StatusSelectMethod.Find:
					if ( FilterParameters != null )
						results = StatusProvider.Find(GetTransactionManager(), FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = StatusProvider.Find(GetTransactionManager(), WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case StatusSelectMethod.GetById:
					_id = ( values["Id"] != null ) ? (System.String) EntityUtil.ChangeType(values["Id"], typeof(System.String)) : string.Empty;
					item = StatusProvider.GetById(GetTransactionManager(), _id);
					results = new TList<Status>();
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
			if ( SelectMethod == StatusSelectMethod.Get || SelectMethod == StatusSelectMethod.GetById )
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
				Status entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// execute deep load method
					StatusProvider.DeepLoad(GetTransactionManager(), GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<Status> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// execute deep load method
			StatusProvider.DeepLoad(GetTransactionManager(), entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region StatusDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the StatusDataSource class.
	/// </summary>
	public class StatusDataSourceDesigner : ProviderDataSourceDesigner<Status, StatusKey>
	{
		/// <summary>
		/// Initializes a new instance of the StatusDataSourceDesigner class.
		/// </summary>
		public StatusDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public StatusSelectMethod SelectMethod
		{
			get { return ((StatusDataSource) DataSource).SelectMethod; }
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
				actions.Add(new StatusDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region StatusDataSourceActionList

	/// <summary>
	/// Supports the StatusDataSourceDesigner class.
	/// </summary>
	internal class StatusDataSourceActionList : DesignerActionList
	{
		private StatusDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the StatusDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public StatusDataSourceActionList(StatusDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public StatusSelectMethod SelectMethod
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

	#endregion StatusDataSourceActionList
	
	#endregion StatusDataSourceDesigner
	
	#region StatusSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the StatusDataSource.SelectMethod property.
	/// </summary>
	public enum StatusSelectMethod
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
		GetById
	}
	
	#endregion StatusSelectMethod

	#region StatusFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Status"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class StatusFilter : SqlFilter<StatusColumn>
	{
	}
	
	#endregion StatusFilter

	#region StatusExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Status"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class StatusExpressionBuilder : SqlExpressionBuilder<StatusColumn>
	{
	}
	
	#endregion StatusExpressionBuilder	

	#region StatusProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;StatusChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="Status"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class StatusProperty : ChildEntityProperty<StatusChildEntityTypes>
	{
	}
	
	#endregion StatusProperty
}

