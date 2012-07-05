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
	/// Represents the DataRepository.RoleDetailProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(RoleDetailDataSourceDesigner))]
	public class RoleDetailDataSource : ProviderDataSource<RoleDetail, RoleDetailKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the RoleDetailDataSource class.
		/// </summary>
		public RoleDetailDataSource() : base(DataRepository.RoleDetailProvider)
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the RoleDetailDataSourceView used by the RoleDetailDataSource.
		/// </summary>
		protected RoleDetailDataSourceView RoleDetailView
		{
			get { return ( View as RoleDetailDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the RoleDetailDataSource control invokes to retrieve data.
		/// </summary>
		public RoleDetailSelectMethod SelectMethod
		{
			get
			{
				RoleDetailSelectMethod selectMethod = RoleDetailSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (RoleDetailSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the RoleDetailDataSourceView class that is to be
		/// used by the RoleDetailDataSource.
		/// </summary>
		/// <returns>An instance of the RoleDetailDataSourceView class.</returns>
		protected override BaseDataSourceView<RoleDetail, RoleDetailKey> GetNewDataSourceView()
		{
			return new RoleDetailDataSourceView(this, DefaultViewName);
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
	/// Supports the RoleDetailDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class RoleDetailDataSourceView : ProviderDataSourceView<RoleDetail, RoleDetailKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the RoleDetailDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the RoleDetailDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public RoleDetailDataSourceView(RoleDetailDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal RoleDetailDataSource RoleDetailOwner
		{
			get { return Owner as RoleDetailDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal RoleDetailSelectMethod SelectMethod
		{
			get { return RoleDetailOwner.SelectMethod; }
			set { RoleDetailOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal RoleDetailProviderBase RoleDetailProvider
		{
			get { return Provider as RoleDetailProviderBase; }
		}

		#endregion Properties
		
		#region Methods
		
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<RoleDetail> GetSelectData(out int count)
		{
			Hashtable values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<RoleDetail> results = null;
			RoleDetail item;
			count = 0;
			
			System.Int64 _id;
			System.Int32? _roleId_nullable;
			System.Int32? _screenId_nullable;

			switch ( SelectMethod )
			{
				case RoleDetailSelectMethod.Get:
					RoleDetailKey entityKey  = new RoleDetailKey();
					entityKey.Load(values);
					item = RoleDetailProvider.Get(GetTransactionManager(), entityKey);
					results = new TList<RoleDetail>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case RoleDetailSelectMethod.GetAll:
                    results = RoleDetailProvider.GetAll(GetTransactionManager(), StartIndex, PageSize, out count);
                    break;
				case RoleDetailSelectMethod.GetPaged:
					results = RoleDetailProvider.GetPaged(GetTransactionManager(), WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case RoleDetailSelectMethod.Find:
					if ( FilterParameters != null )
						results = RoleDetailProvider.Find(GetTransactionManager(), FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = RoleDetailProvider.Find(GetTransactionManager(), WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case RoleDetailSelectMethod.GetById:
					_id = ( values["Id"] != null ) ? (System.Int64) EntityUtil.ChangeType(values["Id"], typeof(System.Int64)) : (long)0;
					item = RoleDetailProvider.GetById(GetTransactionManager(), _id);
					results = new TList<RoleDetail>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				case RoleDetailSelectMethod.GetByRoleId:
					_roleId_nullable = (System.Int32?) EntityUtil.ChangeType(values["RoleId"], typeof(System.Int32?));
					results = RoleDetailProvider.GetByRoleId(GetTransactionManager(), _roleId_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case RoleDetailSelectMethod.GetByScreenId:
					_screenId_nullable = (System.Int32?) EntityUtil.ChangeType(values["ScreenId"], typeof(System.Int32?));
					results = RoleDetailProvider.GetByScreenId(GetTransactionManager(), _screenId_nullable, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == RoleDetailSelectMethod.Get || SelectMethod == RoleDetailSelectMethod.GetById )
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
				RoleDetail entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// execute deep load method
					RoleDetailProvider.DeepLoad(GetTransactionManager(), GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<RoleDetail> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// execute deep load method
			RoleDetailProvider.DeepLoad(GetTransactionManager(), entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region RoleDetailDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the RoleDetailDataSource class.
	/// </summary>
	public class RoleDetailDataSourceDesigner : ProviderDataSourceDesigner<RoleDetail, RoleDetailKey>
	{
		/// <summary>
		/// Initializes a new instance of the RoleDetailDataSourceDesigner class.
		/// </summary>
		public RoleDetailDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public RoleDetailSelectMethod SelectMethod
		{
			get { return ((RoleDetailDataSource) DataSource).SelectMethod; }
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
				actions.Add(new RoleDetailDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region RoleDetailDataSourceActionList

	/// <summary>
	/// Supports the RoleDetailDataSourceDesigner class.
	/// </summary>
	internal class RoleDetailDataSourceActionList : DesignerActionList
	{
		private RoleDetailDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the RoleDetailDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public RoleDetailDataSourceActionList(RoleDetailDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public RoleDetailSelectMethod SelectMethod
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

	#endregion RoleDetailDataSourceActionList
	
	#endregion RoleDetailDataSourceDesigner
	
	#region RoleDetailSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the RoleDetailDataSource.SelectMethod property.
	/// </summary>
	public enum RoleDetailSelectMethod
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
		/// Represents the GetByRoleId method.
		/// </summary>
		GetByRoleId,
		/// <summary>
		/// Represents the GetByScreenId method.
		/// </summary>
		GetByScreenId
	}
	
	#endregion RoleDetailSelectMethod

	#region RoleDetailFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="RoleDetail"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class RoleDetailFilter : SqlFilter<RoleDetailColumn>
	{
	}
	
	#endregion RoleDetailFilter

	#region RoleDetailExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="RoleDetail"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class RoleDetailExpressionBuilder : SqlExpressionBuilder<RoleDetailColumn>
	{
	}
	
	#endregion RoleDetailExpressionBuilder	

	#region RoleDetailProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;RoleDetailChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="RoleDetail"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class RoleDetailProperty : ChildEntityProperty<RoleDetailChildEntityTypes>
	{
	}
	
	#endregion RoleDetailProperty
}

