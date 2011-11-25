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
	/// Represents the DataRepository.StaffRolesProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(StaffRolesDataSourceDesigner))]
	public class StaffRolesDataSource : ProviderDataSource<StaffRoles, StaffRolesKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the StaffRolesDataSource class.
		/// </summary>
		public StaffRolesDataSource() : base(DataRepository.StaffRolesProvider)
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the StaffRolesDataSourceView used by the StaffRolesDataSource.
		/// </summary>
		protected StaffRolesDataSourceView StaffRolesView
		{
			get { return ( View as StaffRolesDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the StaffRolesDataSource control invokes to retrieve data.
		/// </summary>
		public StaffRolesSelectMethod SelectMethod
		{
			get
			{
				StaffRolesSelectMethod selectMethod = StaffRolesSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (StaffRolesSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the StaffRolesDataSourceView class that is to be
		/// used by the StaffRolesDataSource.
		/// </summary>
		/// <returns>An instance of the StaffRolesDataSourceView class.</returns>
		protected override BaseDataSourceView<StaffRoles, StaffRolesKey> GetNewDataSourceView()
		{
			return new StaffRolesDataSourceView(this, DefaultViewName);
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
	/// Supports the StaffRolesDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class StaffRolesDataSourceView : ProviderDataSourceView<StaffRoles, StaffRolesKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the StaffRolesDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the StaffRolesDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public StaffRolesDataSourceView(StaffRolesDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal StaffRolesDataSource StaffRolesOwner
		{
			get { return Owner as StaffRolesDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal StaffRolesSelectMethod SelectMethod
		{
			get { return StaffRolesOwner.SelectMethod; }
			set { StaffRolesOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal StaffRolesProviderBase StaffRolesProvider
		{
			get { return Provider as StaffRolesProviderBase; }
		}

		#endregion Properties
		
		#region Methods
		
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<StaffRoles> GetSelectData(out int count)
		{
			Hashtable values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<StaffRoles> results = null;
			StaffRoles item;
			count = 0;
			
			System.Int32 _id;
			System.Boolean? _isDisabled_nullable;
			System.Int32? _roleId_nullable;
			System.Int32? _staffId_nullable;

			switch ( SelectMethod )
			{
				case StaffRolesSelectMethod.Get:
					StaffRolesKey entityKey  = new StaffRolesKey();
					entityKey.Load(values);
					item = StaffRolesProvider.Get(GetTransactionManager(), entityKey);
					results = new TList<StaffRoles>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case StaffRolesSelectMethod.GetAll:
                    results = StaffRolesProvider.GetAll(GetTransactionManager(), StartIndex, PageSize, out count);
                    break;
				case StaffRolesSelectMethod.GetPaged:
					results = StaffRolesProvider.GetPaged(GetTransactionManager(), WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case StaffRolesSelectMethod.Find:
					if ( FilterParameters != null )
						results = StaffRolesProvider.Find(GetTransactionManager(), FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = StaffRolesProvider.Find(GetTransactionManager(), WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case StaffRolesSelectMethod.GetById:
					_id = ( values["Id"] != null ) ? (System.Int32) EntityUtil.ChangeType(values["Id"], typeof(System.Int32)) : (int)0;
					item = StaffRolesProvider.GetById(GetTransactionManager(), _id);
					results = new TList<StaffRoles>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				case StaffRolesSelectMethod.GetByIdIsDisabled:
					_id = ( values["Id"] != null ) ? (System.Int32) EntityUtil.ChangeType(values["Id"], typeof(System.Int32)) : (int)0;
					_isDisabled_nullable = (System.Boolean?) EntityUtil.ChangeType(values["IsDisabled"], typeof(System.Boolean?));
					results = StaffRolesProvider.GetByIdIsDisabled(GetTransactionManager(), _id, _isDisabled_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case StaffRolesSelectMethod.GetByIsDisabled:
					_isDisabled_nullable = (System.Boolean?) EntityUtil.ChangeType(values["IsDisabled"], typeof(System.Boolean?));
					results = StaffRolesProvider.GetByIsDisabled(GetTransactionManager(), _isDisabled_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case StaffRolesSelectMethod.GetByRoleId:
					_roleId_nullable = (System.Int32?) EntityUtil.ChangeType(values["RoleId"], typeof(System.Int32?));
					results = StaffRolesProvider.GetByRoleId(GetTransactionManager(), _roleId_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case StaffRolesSelectMethod.GetByRoleIdIsDisabled:
					_roleId_nullable = (System.Int32?) EntityUtil.ChangeType(values["RoleId"], typeof(System.Int32?));
					_isDisabled_nullable = (System.Boolean?) EntityUtil.ChangeType(values["IsDisabled"], typeof(System.Boolean?));
					results = StaffRolesProvider.GetByRoleIdIsDisabled(GetTransactionManager(), _roleId_nullable, _isDisabled_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case StaffRolesSelectMethod.GetByStaffId:
					_staffId_nullable = (System.Int32?) EntityUtil.ChangeType(values["StaffId"], typeof(System.Int32?));
					results = StaffRolesProvider.GetByStaffId(GetTransactionManager(), _staffId_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case StaffRolesSelectMethod.GetByStaffIdIsDisabled:
					_staffId_nullable = (System.Int32?) EntityUtil.ChangeType(values["StaffId"], typeof(System.Int32?));
					_isDisabled_nullable = (System.Boolean?) EntityUtil.ChangeType(values["IsDisabled"], typeof(System.Boolean?));
					results = StaffRolesProvider.GetByStaffIdIsDisabled(GetTransactionManager(), _staffId_nullable, _isDisabled_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case StaffRolesSelectMethod.GetByStaffIdRoleId:
					_staffId_nullable = (System.Int32?) EntityUtil.ChangeType(values["StaffId"], typeof(System.Int32?));
					_roleId_nullable = (System.Int32?) EntityUtil.ChangeType(values["RoleId"], typeof(System.Int32?));
					results = StaffRolesProvider.GetByStaffIdRoleId(GetTransactionManager(), _staffId_nullable, _roleId_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case StaffRolesSelectMethod.GetByStaffIdRoleIdIsDisabled:
					_staffId_nullable = (System.Int32?) EntityUtil.ChangeType(values["StaffId"], typeof(System.Int32?));
					_roleId_nullable = (System.Int32?) EntityUtil.ChangeType(values["RoleId"], typeof(System.Int32?));
					_isDisabled_nullable = (System.Boolean?) EntityUtil.ChangeType(values["IsDisabled"], typeof(System.Boolean?));
					results = StaffRolesProvider.GetByStaffIdRoleIdIsDisabled(GetTransactionManager(), _staffId_nullable, _roleId_nullable, _isDisabled_nullable, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == StaffRolesSelectMethod.Get || SelectMethod == StaffRolesSelectMethod.GetById )
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
				StaffRoles entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// execute deep load method
					StaffRolesProvider.DeepLoad(GetTransactionManager(), GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<StaffRoles> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// execute deep load method
			StaffRolesProvider.DeepLoad(GetTransactionManager(), entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region StaffRolesDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the StaffRolesDataSource class.
	/// </summary>
	public class StaffRolesDataSourceDesigner : ProviderDataSourceDesigner<StaffRoles, StaffRolesKey>
	{
		/// <summary>
		/// Initializes a new instance of the StaffRolesDataSourceDesigner class.
		/// </summary>
		public StaffRolesDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public StaffRolesSelectMethod SelectMethod
		{
			get { return ((StaffRolesDataSource) DataSource).SelectMethod; }
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
				actions.Add(new StaffRolesDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region StaffRolesDataSourceActionList

	/// <summary>
	/// Supports the StaffRolesDataSourceDesigner class.
	/// </summary>
	internal class StaffRolesDataSourceActionList : DesignerActionList
	{
		private StaffRolesDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the StaffRolesDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public StaffRolesDataSourceActionList(StaffRolesDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public StaffRolesSelectMethod SelectMethod
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

	#endregion StaffRolesDataSourceActionList
	
	#endregion StaffRolesDataSourceDesigner
	
	#region StaffRolesSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the StaffRolesDataSource.SelectMethod property.
	/// </summary>
	public enum StaffRolesSelectMethod
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
		/// Represents the GetByRoleId method.
		/// </summary>
		GetByRoleId,
		/// <summary>
		/// Represents the GetByRoleIdIsDisabled method.
		/// </summary>
		GetByRoleIdIsDisabled,
		/// <summary>
		/// Represents the GetByStaffId method.
		/// </summary>
		GetByStaffId,
		/// <summary>
		/// Represents the GetByStaffIdIsDisabled method.
		/// </summary>
		GetByStaffIdIsDisabled,
		/// <summary>
		/// Represents the GetByStaffIdRoleId method.
		/// </summary>
		GetByStaffIdRoleId,
		/// <summary>
		/// Represents the GetByStaffIdRoleIdIsDisabled method.
		/// </summary>
		GetByStaffIdRoleIdIsDisabled,
		/// <summary>
		/// Represents the GetById method.
		/// </summary>
		GetById
	}
	
	#endregion StaffRolesSelectMethod

	#region StaffRolesFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="StaffRoles"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class StaffRolesFilter : SqlFilter<StaffRolesColumn>
	{
	}
	
	#endregion StaffRolesFilter

	#region StaffRolesExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="StaffRoles"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class StaffRolesExpressionBuilder : SqlExpressionBuilder<StaffRolesColumn>
	{
	}
	
	#endregion StaffRolesExpressionBuilder	

	#region StaffRolesProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;StaffRolesChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="StaffRoles"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class StaffRolesProperty : ChildEntityProperty<StaffRolesChildEntityTypes>
	{
	}
	
	#endregion StaffRolesProperty
}

