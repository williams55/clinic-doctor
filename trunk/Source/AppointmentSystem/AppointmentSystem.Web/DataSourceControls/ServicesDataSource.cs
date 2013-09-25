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
	/// Represents the DataRepository.ServicesProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(ServicesDataSourceDesigner))]
	public class ServicesDataSource : ProviderDataSource<Services, ServicesKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ServicesDataSource class.
		/// </summary>
		public ServicesDataSource() : base(DataRepository.ServicesProvider)
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the ServicesDataSourceView used by the ServicesDataSource.
		/// </summary>
		protected ServicesDataSourceView ServicesView
		{
			get { return ( View as ServicesDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the ServicesDataSource control invokes to retrieve data.
		/// </summary>
		public ServicesSelectMethod SelectMethod
		{
			get
			{
				ServicesSelectMethod selectMethod = ServicesSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (ServicesSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the ServicesDataSourceView class that is to be
		/// used by the ServicesDataSource.
		/// </summary>
		/// <returns>An instance of the ServicesDataSourceView class.</returns>
		protected override BaseDataSourceView<Services, ServicesKey> GetNewDataSourceView()
		{
			return new ServicesDataSourceView(this, DefaultViewName);
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
	/// Supports the ServicesDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class ServicesDataSourceView : ProviderDataSourceView<Services, ServicesKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ServicesDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the ServicesDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public ServicesDataSourceView(ServicesDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal ServicesDataSource ServicesOwner
		{
			get { return Owner as ServicesDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal ServicesSelectMethod SelectMethod
		{
			get { return ServicesOwner.SelectMethod; }
			set { ServicesOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal ServicesProviderBase ServicesProvider
		{
			get { return Provider as ServicesProviderBase; }
		}

		#endregion Properties
		
		#region Methods
		
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<Services> GetSelectData(out int count)
		{
			Hashtable values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<Services> results = null;
			Services item;
			count = 0;
			
			System.Int32 _id;

			switch ( SelectMethod )
			{
				case ServicesSelectMethod.Get:
					ServicesKey entityKey  = new ServicesKey();
					entityKey.Load(values);
					item = ServicesProvider.Get(GetTransactionManager(), entityKey);
					results = new TList<Services>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case ServicesSelectMethod.GetAll:
                    results = ServicesProvider.GetAll(GetTransactionManager(), StartIndex, PageSize, out count);
                    break;
				case ServicesSelectMethod.GetPaged:
					results = ServicesProvider.GetPaged(GetTransactionManager(), WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case ServicesSelectMethod.Find:
					if ( FilterParameters != null )
						results = ServicesProvider.Find(GetTransactionManager(), FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = ServicesProvider.Find(GetTransactionManager(), WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case ServicesSelectMethod.GetById:
					_id = ( values["Id"] != null ) ? (System.Int32) EntityUtil.ChangeType(values["Id"], typeof(System.Int32)) : (int)0;
					item = ServicesProvider.GetById(GetTransactionManager(), _id);
					results = new TList<Services>();
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
			if ( SelectMethod == ServicesSelectMethod.Get || SelectMethod == ServicesSelectMethod.GetById )
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
				Services entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// execute deep load method
					ServicesProvider.DeepLoad(GetTransactionManager(), GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<Services> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// execute deep load method
			ServicesProvider.DeepLoad(GetTransactionManager(), entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region ServicesDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the ServicesDataSource class.
	/// </summary>
	public class ServicesDataSourceDesigner : ProviderDataSourceDesigner<Services, ServicesKey>
	{
		/// <summary>
		/// Initializes a new instance of the ServicesDataSourceDesigner class.
		/// </summary>
		public ServicesDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public ServicesSelectMethod SelectMethod
		{
			get { return ((ServicesDataSource) DataSource).SelectMethod; }
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
				actions.Add(new ServicesDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region ServicesDataSourceActionList

	/// <summary>
	/// Supports the ServicesDataSourceDesigner class.
	/// </summary>
	internal class ServicesDataSourceActionList : DesignerActionList
	{
		private ServicesDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the ServicesDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public ServicesDataSourceActionList(ServicesDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public ServicesSelectMethod SelectMethod
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

	#endregion ServicesDataSourceActionList
	
	#endregion ServicesDataSourceDesigner
	
	#region ServicesSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the ServicesDataSource.SelectMethod property.
	/// </summary>
	public enum ServicesSelectMethod
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
	
	#endregion ServicesSelectMethod

	#region ServicesFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Services"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ServicesFilter : SqlFilter<ServicesColumn>
	{
	}
	
	#endregion ServicesFilter

	#region ServicesExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Services"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ServicesExpressionBuilder : SqlExpressionBuilder<ServicesColumn>
	{
	}
	
	#endregion ServicesExpressionBuilder	

	#region ServicesProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;ServicesChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="Services"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ServicesProperty : ChildEntityProperty<ServicesChildEntityTypes>
	{
	}
	
	#endregion ServicesProperty
}

