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
	/// Represents the DataRepository.AppointmentHistoryProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(AppointmentHistoryDataSourceDesigner))]
	public class AppointmentHistoryDataSource : ProviderDataSource<AppointmentHistory, AppointmentHistoryKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the AppointmentHistoryDataSource class.
		/// </summary>
		public AppointmentHistoryDataSource() : base(DataRepository.AppointmentHistoryProvider)
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the AppointmentHistoryDataSourceView used by the AppointmentHistoryDataSource.
		/// </summary>
		protected AppointmentHistoryDataSourceView AppointmentHistoryView
		{
			get { return ( View as AppointmentHistoryDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the AppointmentHistoryDataSource control invokes to retrieve data.
		/// </summary>
		public AppointmentHistorySelectMethod SelectMethod
		{
			get
			{
				AppointmentHistorySelectMethod selectMethod = AppointmentHistorySelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (AppointmentHistorySelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the AppointmentHistoryDataSourceView class that is to be
		/// used by the AppointmentHistoryDataSource.
		/// </summary>
		/// <returns>An instance of the AppointmentHistoryDataSourceView class.</returns>
		protected override BaseDataSourceView<AppointmentHistory, AppointmentHistoryKey> GetNewDataSourceView()
		{
			return new AppointmentHistoryDataSourceView(this, DefaultViewName);
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
	/// Supports the AppointmentHistoryDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class AppointmentHistoryDataSourceView : ProviderDataSourceView<AppointmentHistory, AppointmentHistoryKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the AppointmentHistoryDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the AppointmentHistoryDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public AppointmentHistoryDataSourceView(AppointmentHistoryDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal AppointmentHistoryDataSource AppointmentHistoryOwner
		{
			get { return Owner as AppointmentHistoryDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal AppointmentHistorySelectMethod SelectMethod
		{
			get { return AppointmentHistoryOwner.SelectMethod; }
			set { AppointmentHistoryOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal AppointmentHistoryProviderBase AppointmentHistoryProvider
		{
			get { return Provider as AppointmentHistoryProviderBase; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<AppointmentHistory> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<AppointmentHistory> results = null;
			AppointmentHistory item;
			count = 0;
			
			System.Guid _guid;

			switch ( SelectMethod )
			{
				case AppointmentHistorySelectMethod.Get:
					AppointmentHistoryKey entityKey  = new AppointmentHistoryKey();
					entityKey.Load(values);
					item = AppointmentHistoryProvider.Get(GetTransactionManager(), entityKey);
					results = new TList<AppointmentHistory>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case AppointmentHistorySelectMethod.GetAll:
                    results = AppointmentHistoryProvider.GetAll(GetTransactionManager(), StartIndex, PageSize, out count);
                    break;
				case AppointmentHistorySelectMethod.GetPaged:
					results = AppointmentHistoryProvider.GetPaged(GetTransactionManager(), WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case AppointmentHistorySelectMethod.Find:
					if ( FilterParameters != null )
						results = AppointmentHistoryProvider.Find(GetTransactionManager(), FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = AppointmentHistoryProvider.Find(GetTransactionManager(), WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case AppointmentHistorySelectMethod.GetByGuid:
					_guid = ( values["Guid"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["Guid"], typeof(System.Guid)) : Guid.Empty;
					item = AppointmentHistoryProvider.GetByGuid(GetTransactionManager(), _guid);
					results = new TList<AppointmentHistory>();
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
			if ( SelectMethod == AppointmentHistorySelectMethod.Get || SelectMethod == AppointmentHistorySelectMethod.GetByGuid )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Sets the primary key values of the specified Entity object.
		/// </summary>
		/// <param name="entity">The Entity object to update.</param>
		protected override void SetEntityKeyValues(AppointmentHistory entity)
		{
			base.SetEntityKeyValues(entity);
			
			// make sure primary key column(s) have been set
			if ( entity.Guid == Guid.Empty )
				entity.Guid = Guid.NewGuid();
		}
		
		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				AppointmentHistory entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// execute deep load method
					AppointmentHistoryProvider.DeepLoad(GetTransactionManager(), GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<AppointmentHistory> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// execute deep load method
			AppointmentHistoryProvider.DeepLoad(GetTransactionManager(), entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region AppointmentHistoryDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the AppointmentHistoryDataSource class.
	/// </summary>
	public class AppointmentHistoryDataSourceDesigner : ProviderDataSourceDesigner<AppointmentHistory, AppointmentHistoryKey>
	{
		/// <summary>
		/// Initializes a new instance of the AppointmentHistoryDataSourceDesigner class.
		/// </summary>
		public AppointmentHistoryDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public AppointmentHistorySelectMethod SelectMethod
		{
			get { return ((AppointmentHistoryDataSource) DataSource).SelectMethod; }
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
				actions.Add(new AppointmentHistoryDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region AppointmentHistoryDataSourceActionList

	/// <summary>
	/// Supports the AppointmentHistoryDataSourceDesigner class.
	/// </summary>
	internal class AppointmentHistoryDataSourceActionList : DesignerActionList
	{
		private AppointmentHistoryDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the AppointmentHistoryDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public AppointmentHistoryDataSourceActionList(AppointmentHistoryDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public AppointmentHistorySelectMethod SelectMethod
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

	#endregion AppointmentHistoryDataSourceActionList
	
	#endregion AppointmentHistoryDataSourceDesigner
	
	#region AppointmentHistorySelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the AppointmentHistoryDataSource.SelectMethod property.
	/// </summary>
	public enum AppointmentHistorySelectMethod
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
		/// Represents the GetByGuid method.
		/// </summary>
		GetByGuid
	}
	
	#endregion AppointmentHistorySelectMethod

	#region AppointmentHistoryFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="AppointmentHistory"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class AppointmentHistoryFilter : SqlFilter<AppointmentHistoryColumn>
	{
	}
	
	#endregion AppointmentHistoryFilter

	#region AppointmentHistoryExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="AppointmentHistory"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class AppointmentHistoryExpressionBuilder : SqlExpressionBuilder<AppointmentHistoryColumn>
	{
	}
	
	#endregion AppointmentHistoryExpressionBuilder	

	#region AppointmentHistoryProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;AppointmentHistoryChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="AppointmentHistory"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class AppointmentHistoryProperty : ChildEntityProperty<AppointmentHistoryChildEntityTypes>
	{
	}
	
	#endregion AppointmentHistoryProperty
}

