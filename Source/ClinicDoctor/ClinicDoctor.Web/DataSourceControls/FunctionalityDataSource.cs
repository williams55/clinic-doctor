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
	/// Represents the DataRepository.FunctionalityProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(FunctionalityDataSourceDesigner))]
	public class FunctionalityDataSource : ProviderDataSource<Functionality, FunctionalityKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the FunctionalityDataSource class.
		/// </summary>
		public FunctionalityDataSource() : base(DataRepository.FunctionalityProvider)
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the FunctionalityDataSourceView used by the FunctionalityDataSource.
		/// </summary>
		protected FunctionalityDataSourceView FunctionalityView
		{
			get { return ( View as FunctionalityDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the FunctionalityDataSource control invokes to retrieve data.
		/// </summary>
		public FunctionalitySelectMethod SelectMethod
		{
			get
			{
				FunctionalitySelectMethod selectMethod = FunctionalitySelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (FunctionalitySelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the FunctionalityDataSourceView class that is to be
		/// used by the FunctionalityDataSource.
		/// </summary>
		/// <returns>An instance of the FunctionalityDataSourceView class.</returns>
		protected override BaseDataSourceView<Functionality, FunctionalityKey> GetNewDataSourceView()
		{
			return new FunctionalityDataSourceView(this, DefaultViewName);
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
	/// Supports the FunctionalityDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class FunctionalityDataSourceView : ProviderDataSourceView<Functionality, FunctionalityKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the FunctionalityDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the FunctionalityDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public FunctionalityDataSourceView(FunctionalityDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal FunctionalityDataSource FunctionalityOwner
		{
			get { return Owner as FunctionalityDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal FunctionalitySelectMethod SelectMethod
		{
			get { return FunctionalityOwner.SelectMethod; }
			set { FunctionalityOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal FunctionalityProviderBase FunctionalityProvider
		{
			get { return Provider as FunctionalityProviderBase; }
		}

		#endregion Properties
		
		#region Methods
		
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<Functionality> GetSelectData(out int count)
		{
			Hashtable values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<Functionality> results = null;
			Functionality item;
			count = 0;
			
			System.Int32 _id;
			System.Boolean? _isDisabled_nullable;

			switch ( SelectMethod )
			{
				case FunctionalitySelectMethod.Get:
					FunctionalityKey entityKey  = new FunctionalityKey();
					entityKey.Load(values);
					item = FunctionalityProvider.Get(GetTransactionManager(), entityKey);
					results = new TList<Functionality>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case FunctionalitySelectMethod.GetAll:
                    results = FunctionalityProvider.GetAll(GetTransactionManager(), StartIndex, PageSize, out count);
                    break;
				case FunctionalitySelectMethod.GetPaged:
					results = FunctionalityProvider.GetPaged(GetTransactionManager(), WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case FunctionalitySelectMethod.Find:
					if ( FilterParameters != null )
						results = FunctionalityProvider.Find(GetTransactionManager(), FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = FunctionalityProvider.Find(GetTransactionManager(), WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case FunctionalitySelectMethod.GetById:
					_id = ( values["Id"] != null ) ? (System.Int32) EntityUtil.ChangeType(values["Id"], typeof(System.Int32)) : (int)0;
					item = FunctionalityProvider.GetById(GetTransactionManager(), _id);
					results = new TList<Functionality>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				case FunctionalitySelectMethod.GetByIdIsDisabled:
					_id = ( values["Id"] != null ) ? (System.Int32) EntityUtil.ChangeType(values["Id"], typeof(System.Int32)) : (int)0;
					_isDisabled_nullable = (System.Boolean?) EntityUtil.ChangeType(values["IsDisabled"], typeof(System.Boolean?));
					results = FunctionalityProvider.GetByIdIsDisabled(GetTransactionManager(), _id, _isDisabled_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case FunctionalitySelectMethod.GetByIsDisabled:
					_isDisabled_nullable = (System.Boolean?) EntityUtil.ChangeType(values["IsDisabled"], typeof(System.Boolean?));
					results = FunctionalityProvider.GetByIsDisabled(GetTransactionManager(), _isDisabled_nullable, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == FunctionalitySelectMethod.Get || SelectMethod == FunctionalitySelectMethod.GetById )
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
				Functionality entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// execute deep load method
					FunctionalityProvider.DeepLoad(GetTransactionManager(), GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<Functionality> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// execute deep load method
			FunctionalityProvider.DeepLoad(GetTransactionManager(), entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region FunctionalityDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the FunctionalityDataSource class.
	/// </summary>
	public class FunctionalityDataSourceDesigner : ProviderDataSourceDesigner<Functionality, FunctionalityKey>
	{
		/// <summary>
		/// Initializes a new instance of the FunctionalityDataSourceDesigner class.
		/// </summary>
		public FunctionalityDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public FunctionalitySelectMethod SelectMethod
		{
			get { return ((FunctionalityDataSource) DataSource).SelectMethod; }
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
				actions.Add(new FunctionalityDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region FunctionalityDataSourceActionList

	/// <summary>
	/// Supports the FunctionalityDataSourceDesigner class.
	/// </summary>
	internal class FunctionalityDataSourceActionList : DesignerActionList
	{
		private FunctionalityDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the FunctionalityDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public FunctionalityDataSourceActionList(FunctionalityDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public FunctionalitySelectMethod SelectMethod
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

	#endregion FunctionalityDataSourceActionList
	
	#endregion FunctionalityDataSourceDesigner
	
	#region FunctionalitySelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the FunctionalityDataSource.SelectMethod property.
	/// </summary>
	public enum FunctionalitySelectMethod
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
	
	#endregion FunctionalitySelectMethod

	#region FunctionalityFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Functionality"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class FunctionalityFilter : SqlFilter<FunctionalityColumn>
	{
	}
	
	#endregion FunctionalityFilter

	#region FunctionalityExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Functionality"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class FunctionalityExpressionBuilder : SqlExpressionBuilder<FunctionalityColumn>
	{
	}
	
	#endregion FunctionalityExpressionBuilder	

	#region FunctionalityProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;FunctionalityChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="Functionality"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class FunctionalityProperty : ChildEntityProperty<FunctionalityChildEntityTypes>
	{
	}
	
	#endregion FunctionalityProperty
}

