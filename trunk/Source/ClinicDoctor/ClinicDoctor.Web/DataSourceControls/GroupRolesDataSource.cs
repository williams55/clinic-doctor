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
	/// Represents the DataRepository.GroupRolesProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(GroupRolesDataSourceDesigner))]
	public class GroupRolesDataSource : ProviderDataSource<GroupRoles, GroupRolesKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the GroupRolesDataSource class.
		/// </summary>
		public GroupRolesDataSource() : base(DataRepository.GroupRolesProvider)
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the GroupRolesDataSourceView used by the GroupRolesDataSource.
		/// </summary>
		protected GroupRolesDataSourceView GroupRolesView
		{
			get { return ( View as GroupRolesDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the GroupRolesDataSource control invokes to retrieve data.
		/// </summary>
		public GroupRolesSelectMethod SelectMethod
		{
			get
			{
				GroupRolesSelectMethod selectMethod = GroupRolesSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (GroupRolesSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the GroupRolesDataSourceView class that is to be
		/// used by the GroupRolesDataSource.
		/// </summary>
		/// <returns>An instance of the GroupRolesDataSourceView class.</returns>
		protected override BaseDataSourceView<GroupRoles, GroupRolesKey> GetNewDataSourceView()
		{
			return new GroupRolesDataSourceView(this, DefaultViewName);
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
	/// Supports the GroupRolesDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class GroupRolesDataSourceView : ProviderDataSourceView<GroupRoles, GroupRolesKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the GroupRolesDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the GroupRolesDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public GroupRolesDataSourceView(GroupRolesDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal GroupRolesDataSource GroupRolesOwner
		{
			get { return Owner as GroupRolesDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal GroupRolesSelectMethod SelectMethod
		{
			get { return GroupRolesOwner.SelectMethod; }
			set { GroupRolesOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal GroupRolesProviderBase GroupRolesProvider
		{
			get { return Provider as GroupRolesProviderBase; }
		}

		#endregion Properties
		
		#region Methods
		
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<GroupRoles> GetSelectData(out int count)
		{
			Hashtable values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<GroupRoles> results = null;
			GroupRoles item;
			count = 0;
			
			System.Int32 _id;
			System.Int32? _groupId_nullable;
			System.Int32? _roleId_nullable;

			switch ( SelectMethod )
			{
				case GroupRolesSelectMethod.Get:
					GroupRolesKey entityKey  = new GroupRolesKey();
					entityKey.Load(values);
					item = GroupRolesProvider.Get(GetTransactionManager(), entityKey);
					results = new TList<GroupRoles>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case GroupRolesSelectMethod.GetAll:
                    results = GroupRolesProvider.GetAll(GetTransactionManager(), StartIndex, PageSize, out count);
                    break;
				case GroupRolesSelectMethod.GetPaged:
					results = GroupRolesProvider.GetPaged(GetTransactionManager(), WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case GroupRolesSelectMethod.Find:
					if ( FilterParameters != null )
						results = GroupRolesProvider.Find(GetTransactionManager(), FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = GroupRolesProvider.Find(GetTransactionManager(), WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case GroupRolesSelectMethod.GetById:
					_id = ( values["Id"] != null ) ? (System.Int32) EntityUtil.ChangeType(values["Id"], typeof(System.Int32)) : (int)0;
					item = GroupRolesProvider.GetById(GetTransactionManager(), _id);
					results = new TList<GroupRoles>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				case GroupRolesSelectMethod.GetByGroupId:
					_groupId_nullable = (System.Int32?) EntityUtil.ChangeType(values["GroupId"], typeof(System.Int32?));
					results = GroupRolesProvider.GetByGroupId(GetTransactionManager(), _groupId_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case GroupRolesSelectMethod.GetByRoleId:
					_roleId_nullable = (System.Int32?) EntityUtil.ChangeType(values["RoleId"], typeof(System.Int32?));
					results = GroupRolesProvider.GetByRoleId(GetTransactionManager(), _roleId_nullable, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == GroupRolesSelectMethod.Get || SelectMethod == GroupRolesSelectMethod.GetById )
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
				GroupRoles entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// execute deep load method
					GroupRolesProvider.DeepLoad(GetTransactionManager(), GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<GroupRoles> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// execute deep load method
			GroupRolesProvider.DeepLoad(GetTransactionManager(), entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region GroupRolesDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the GroupRolesDataSource class.
	/// </summary>
	public class GroupRolesDataSourceDesigner : ProviderDataSourceDesigner<GroupRoles, GroupRolesKey>
	{
		/// <summary>
		/// Initializes a new instance of the GroupRolesDataSourceDesigner class.
		/// </summary>
		public GroupRolesDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public GroupRolesSelectMethod SelectMethod
		{
			get { return ((GroupRolesDataSource) DataSource).SelectMethod; }
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
				actions.Add(new GroupRolesDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region GroupRolesDataSourceActionList

	/// <summary>
	/// Supports the GroupRolesDataSourceDesigner class.
	/// </summary>
	internal class GroupRolesDataSourceActionList : DesignerActionList
	{
		private GroupRolesDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the GroupRolesDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public GroupRolesDataSourceActionList(GroupRolesDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public GroupRolesSelectMethod SelectMethod
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

	#endregion GroupRolesDataSourceActionList
	
	#endregion GroupRolesDataSourceDesigner
	
	#region GroupRolesSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the GroupRolesDataSource.SelectMethod property.
	/// </summary>
	public enum GroupRolesSelectMethod
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
		/// Represents the GetByGroupId method.
		/// </summary>
		GetByGroupId,
		/// <summary>
		/// Represents the GetByRoleId method.
		/// </summary>
		GetByRoleId
	}
	
	#endregion GroupRolesSelectMethod

	#region GroupRolesFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="GroupRoles"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class GroupRolesFilter : SqlFilter<GroupRolesColumn>
	{
	}
	
	#endregion GroupRolesFilter

	#region GroupRolesExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="GroupRoles"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class GroupRolesExpressionBuilder : SqlExpressionBuilder<GroupRolesColumn>
	{
	}
	
	#endregion GroupRolesExpressionBuilder	

	#region GroupRolesProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;GroupRolesChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="GroupRoles"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class GroupRolesProperty : ChildEntityProperty<GroupRolesChildEntityTypes>
	{
	}
	
	#endregion GroupRolesProperty
}

