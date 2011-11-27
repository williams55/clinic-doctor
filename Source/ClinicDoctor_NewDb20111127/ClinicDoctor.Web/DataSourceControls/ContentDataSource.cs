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
	/// Represents the DataRepository.ContentProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(ContentDataSourceDesigner))]
	public class ContentDataSource : ProviderDataSource<Content, ContentKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ContentDataSource class.
		/// </summary>
		public ContentDataSource() : base(DataRepository.ContentProvider)
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the ContentDataSourceView used by the ContentDataSource.
		/// </summary>
		protected ContentDataSourceView ContentView
		{
			get { return ( View as ContentDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the ContentDataSource control invokes to retrieve data.
		/// </summary>
		public ContentSelectMethod SelectMethod
		{
			get
			{
				ContentSelectMethod selectMethod = ContentSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (ContentSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the ContentDataSourceView class that is to be
		/// used by the ContentDataSource.
		/// </summary>
		/// <returns>An instance of the ContentDataSourceView class.</returns>
		protected override BaseDataSourceView<Content, ContentKey> GetNewDataSourceView()
		{
			return new ContentDataSourceView(this, DefaultViewName);
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
	/// Supports the ContentDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class ContentDataSourceView : ProviderDataSourceView<Content, ContentKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ContentDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the ContentDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public ContentDataSourceView(ContentDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal ContentDataSource ContentOwner
		{
			get { return Owner as ContentDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal ContentSelectMethod SelectMethod
		{
			get { return ContentOwner.SelectMethod; }
			set { ContentOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal ContentProviderBase ContentProvider
		{
			get { return Provider as ContentProviderBase; }
		}

		#endregion Properties
		
		#region Methods
		
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<Content> GetSelectData(out int count)
		{
			Hashtable values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<Content> results = null;
			Content item;
			count = 0;
			
			System.Int64 _funcId;
			System.Boolean _isDisabled;
			System.Int64 _id;

			switch ( SelectMethod )
			{
				case ContentSelectMethod.Get:
					ContentKey entityKey  = new ContentKey();
					entityKey.Load(values);
					item = ContentProvider.Get(GetTransactionManager(), entityKey);
					results = new TList<Content>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case ContentSelectMethod.GetAll:
                    results = ContentProvider.GetAll(GetTransactionManager(), StartIndex, PageSize, out count);
                    break;
				case ContentSelectMethod.GetPaged:
					results = ContentProvider.GetPaged(GetTransactionManager(), WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case ContentSelectMethod.Find:
					if ( FilterParameters != null )
						results = ContentProvider.Find(GetTransactionManager(), FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = ContentProvider.Find(GetTransactionManager(), WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case ContentSelectMethod.GetById:
					_id = ( values["Id"] != null ) ? (System.Int64) EntityUtil.ChangeType(values["Id"], typeof(System.Int64)) : (long)0;
					item = ContentProvider.GetById(GetTransactionManager(), _id);
					results = new TList<Content>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				case ContentSelectMethod.GetByFuncId:
					_funcId = ( values["FuncId"] != null ) ? (System.Int64) EntityUtil.ChangeType(values["FuncId"], typeof(System.Int64)) : (long)0;
					results = ContentProvider.GetByFuncId(GetTransactionManager(), _funcId, this.StartIndex, this.PageSize, out count);
					break;
				case ContentSelectMethod.GetByFuncIdIsDisabled:
					_funcId = ( values["FuncId"] != null ) ? (System.Int64) EntityUtil.ChangeType(values["FuncId"], typeof(System.Int64)) : (long)0;
					_isDisabled = ( values["IsDisabled"] != null ) ? (System.Boolean) EntityUtil.ChangeType(values["IsDisabled"], typeof(System.Boolean)) : false;
					results = ContentProvider.GetByFuncIdIsDisabled(GetTransactionManager(), _funcId, _isDisabled, this.StartIndex, this.PageSize, out count);
					break;
				case ContentSelectMethod.GetByIdIsDisabled:
					_id = ( values["Id"] != null ) ? (System.Int64) EntityUtil.ChangeType(values["Id"], typeof(System.Int64)) : (long)0;
					_isDisabled = ( values["IsDisabled"] != null ) ? (System.Boolean) EntityUtil.ChangeType(values["IsDisabled"], typeof(System.Boolean)) : false;
					item = ContentProvider.GetByIdIsDisabled(GetTransactionManager(), _id, _isDisabled);
					results = new TList<Content>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case ContentSelectMethod.GetByIsDisabled:
					_isDisabled = ( values["IsDisabled"] != null ) ? (System.Boolean) EntityUtil.ChangeType(values["IsDisabled"], typeof(System.Boolean)) : false;
					results = ContentProvider.GetByIsDisabled(GetTransactionManager(), _isDisabled, this.StartIndex, this.PageSize, out count);
					break;
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
			if ( SelectMethod == ContentSelectMethod.Get || SelectMethod == ContentSelectMethod.GetById )
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
				Content entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// execute deep load method
					ContentProvider.DeepLoad(GetTransactionManager(), GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<Content> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// execute deep load method
			ContentProvider.DeepLoad(GetTransactionManager(), entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region ContentDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the ContentDataSource class.
	/// </summary>
	public class ContentDataSourceDesigner : ProviderDataSourceDesigner<Content, ContentKey>
	{
		/// <summary>
		/// Initializes a new instance of the ContentDataSourceDesigner class.
		/// </summary>
		public ContentDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public ContentSelectMethod SelectMethod
		{
			get { return ((ContentDataSource) DataSource).SelectMethod; }
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
				actions.Add(new ContentDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region ContentDataSourceActionList

	/// <summary>
	/// Supports the ContentDataSourceDesigner class.
	/// </summary>
	internal class ContentDataSourceActionList : DesignerActionList
	{
		private ContentDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the ContentDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public ContentDataSourceActionList(ContentDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public ContentSelectMethod SelectMethod
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

	#endregion ContentDataSourceActionList
	
	#endregion ContentDataSourceDesigner
	
	#region ContentSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the ContentDataSource.SelectMethod property.
	/// </summary>
	public enum ContentSelectMethod
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
		/// Represents the GetByFuncId method.
		/// </summary>
		GetByFuncId,
		/// <summary>
		/// Represents the GetByFuncIdIsDisabled method.
		/// </summary>
		GetByFuncIdIsDisabled,
		/// <summary>
		/// Represents the GetByIdIsDisabled method.
		/// </summary>
		GetByIdIsDisabled,
		/// <summary>
		/// Represents the GetByIsDisabled method.
		/// </summary>
		GetByIsDisabled,
		/// <summary>
		/// Represents the GetById method.
		/// </summary>
		GetById
	}
	
	#endregion ContentSelectMethod

	#region ContentFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Content"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ContentFilter : SqlFilter<ContentColumn>
	{
	}
	
	#endregion ContentFilter

	#region ContentExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Content"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ContentExpressionBuilder : SqlExpressionBuilder<ContentColumn>
	{
	}
	
	#endregion ContentExpressionBuilder	

	#region ContentProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;ContentChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="Content"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ContentProperty : ChildEntityProperty<ContentChildEntityTypes>
	{
	}
	
	#endregion ContentProperty
}

