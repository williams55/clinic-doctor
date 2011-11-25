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
	/// Represents the DataRepository.RosterTypeProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(RosterTypeDataSourceDesigner))]
	public class RosterTypeDataSource : ProviderDataSource<RosterType, RosterTypeKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the RosterTypeDataSource class.
		/// </summary>
		public RosterTypeDataSource() : base(DataRepository.RosterTypeProvider)
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the RosterTypeDataSourceView used by the RosterTypeDataSource.
		/// </summary>
		protected RosterTypeDataSourceView RosterTypeView
		{
			get { return ( View as RosterTypeDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the RosterTypeDataSource control invokes to retrieve data.
		/// </summary>
		public RosterTypeSelectMethod SelectMethod
		{
			get
			{
				RosterTypeSelectMethod selectMethod = RosterTypeSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (RosterTypeSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the RosterTypeDataSourceView class that is to be
		/// used by the RosterTypeDataSource.
		/// </summary>
		/// <returns>An instance of the RosterTypeDataSourceView class.</returns>
		protected override BaseDataSourceView<RosterType, RosterTypeKey> GetNewDataSourceView()
		{
			return new RosterTypeDataSourceView(this, DefaultViewName);
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
	/// Supports the RosterTypeDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class RosterTypeDataSourceView : ProviderDataSourceView<RosterType, RosterTypeKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the RosterTypeDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the RosterTypeDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public RosterTypeDataSourceView(RosterTypeDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal RosterTypeDataSource RosterTypeOwner
		{
			get { return Owner as RosterTypeDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal RosterTypeSelectMethod SelectMethod
		{
			get { return RosterTypeOwner.SelectMethod; }
			set { RosterTypeOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal RosterTypeProviderBase RosterTypeProvider
		{
			get { return Provider as RosterTypeProviderBase; }
		}

		#endregion Properties
		
		#region Methods
		
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<RosterType> GetSelectData(out int count)
		{
			Hashtable values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<RosterType> results = null;
			RosterType item;
			count = 0;
			
			System.Int32 _id;
			System.Boolean? _isDisabled_nullable;
			System.Boolean? _isBooked_nullable;

			switch ( SelectMethod )
			{
				case RosterTypeSelectMethod.Get:
					RosterTypeKey entityKey  = new RosterTypeKey();
					entityKey.Load(values);
					item = RosterTypeProvider.Get(GetTransactionManager(), entityKey);
					results = new TList<RosterType>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case RosterTypeSelectMethod.GetAll:
                    results = RosterTypeProvider.GetAll(GetTransactionManager(), StartIndex, PageSize, out count);
                    break;
				case RosterTypeSelectMethod.GetPaged:
					results = RosterTypeProvider.GetPaged(GetTransactionManager(), WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case RosterTypeSelectMethod.Find:
					if ( FilterParameters != null )
						results = RosterTypeProvider.Find(GetTransactionManager(), FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = RosterTypeProvider.Find(GetTransactionManager(), WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case RosterTypeSelectMethod.GetById:
					_id = ( values["Id"] != null ) ? (System.Int32) EntityUtil.ChangeType(values["Id"], typeof(System.Int32)) : (int)0;
					item = RosterTypeProvider.GetById(GetTransactionManager(), _id);
					results = new TList<RosterType>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				case RosterTypeSelectMethod.GetByIdIsDisabled:
					_id = ( values["Id"] != null ) ? (System.Int32) EntityUtil.ChangeType(values["Id"], typeof(System.Int32)) : (int)0;
					_isDisabled_nullable = (System.Boolean?) EntityUtil.ChangeType(values["IsDisabled"], typeof(System.Boolean?));
					results = RosterTypeProvider.GetByIdIsDisabled(GetTransactionManager(), _id, _isDisabled_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case RosterTypeSelectMethod.GetByIsBooked:
					_isBooked_nullable = (System.Boolean?) EntityUtil.ChangeType(values["IsBooked"], typeof(System.Boolean?));
					results = RosterTypeProvider.GetByIsBooked(GetTransactionManager(), _isBooked_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case RosterTypeSelectMethod.GetByIsBookedIsDisabled:
					_isBooked_nullable = (System.Boolean?) EntityUtil.ChangeType(values["IsBooked"], typeof(System.Boolean?));
					_isDisabled_nullable = (System.Boolean?) EntityUtil.ChangeType(values["IsDisabled"], typeof(System.Boolean?));
					results = RosterTypeProvider.GetByIsBookedIsDisabled(GetTransactionManager(), _isBooked_nullable, _isDisabled_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case RosterTypeSelectMethod.GetByIsDisabled:
					_isDisabled_nullable = (System.Boolean?) EntityUtil.ChangeType(values["IsDisabled"], typeof(System.Boolean?));
					results = RosterTypeProvider.GetByIsDisabled(GetTransactionManager(), _isDisabled_nullable, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == RosterTypeSelectMethod.Get || SelectMethod == RosterTypeSelectMethod.GetById )
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
				RosterType entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// execute deep load method
					RosterTypeProvider.DeepLoad(GetTransactionManager(), GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<RosterType> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// execute deep load method
			RosterTypeProvider.DeepLoad(GetTransactionManager(), entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region RosterTypeDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the RosterTypeDataSource class.
	/// </summary>
	public class RosterTypeDataSourceDesigner : ProviderDataSourceDesigner<RosterType, RosterTypeKey>
	{
		/// <summary>
		/// Initializes a new instance of the RosterTypeDataSourceDesigner class.
		/// </summary>
		public RosterTypeDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public RosterTypeSelectMethod SelectMethod
		{
			get { return ((RosterTypeDataSource) DataSource).SelectMethod; }
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
				actions.Add(new RosterTypeDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region RosterTypeDataSourceActionList

	/// <summary>
	/// Supports the RosterTypeDataSourceDesigner class.
	/// </summary>
	internal class RosterTypeDataSourceActionList : DesignerActionList
	{
		private RosterTypeDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the RosterTypeDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public RosterTypeDataSourceActionList(RosterTypeDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public RosterTypeSelectMethod SelectMethod
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

	#endregion RosterTypeDataSourceActionList
	
	#endregion RosterTypeDataSourceDesigner
	
	#region RosterTypeSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the RosterTypeDataSource.SelectMethod property.
	/// </summary>
	public enum RosterTypeSelectMethod
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
		/// Represents the GetByIsBooked method.
		/// </summary>
		GetByIsBooked,
		/// <summary>
		/// Represents the GetByIsBookedIsDisabled method.
		/// </summary>
		GetByIsBookedIsDisabled,
		/// <summary>
		/// Represents the GetByIsDisabled method.
		/// </summary>
		GetByIsDisabled,
		/// <summary>
		/// Represents the GetById method.
		/// </summary>
		GetById
	}
	
	#endregion RosterTypeSelectMethod

	#region RosterTypeFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="RosterType"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class RosterTypeFilter : SqlFilter<RosterTypeColumn>
	{
	}
	
	#endregion RosterTypeFilter

	#region RosterTypeExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="RosterType"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class RosterTypeExpressionBuilder : SqlExpressionBuilder<RosterTypeColumn>
	{
	}
	
	#endregion RosterTypeExpressionBuilder	

	#region RosterTypeProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;RosterTypeChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="RosterType"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class RosterTypeProperty : ChildEntityProperty<RosterTypeChildEntityTypes>
	{
	}
	
	#endregion RosterTypeProperty
}

