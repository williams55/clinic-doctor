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
	/// Represents the DataRepository.GroupRoleProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(GroupRoleDataSourceDesigner))]
	public class GroupRoleDataSource : ProviderDataSource<GroupRole, GroupRoleKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the GroupRoleDataSource class.
		/// </summary>
		public GroupRoleDataSource() : base(DataRepository.GroupRoleProvider)
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the GroupRoleDataSourceView used by the GroupRoleDataSource.
		/// </summary>
		protected GroupRoleDataSourceView GroupRoleView
		{
			get { return ( View as GroupRoleDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the GroupRoleDataSource control invokes to retrieve data.
		/// </summary>
		public GroupRoleSelectMethod SelectMethod
		{
			get
			{
				GroupRoleSelectMethod selectMethod = GroupRoleSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (GroupRoleSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the GroupRoleDataSourceView class that is to be
		/// used by the GroupRoleDataSource.
		/// </summary>
		/// <returns>An instance of the GroupRoleDataSourceView class.</returns>
		protected override BaseDataSourceView<GroupRole, GroupRoleKey> GetNewDataSourceView()
		{
			return new GroupRoleDataSourceView(this, DefaultViewName);
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
	/// Supports the GroupRoleDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class GroupRoleDataSourceView : ProviderDataSourceView<GroupRole, GroupRoleKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the GroupRoleDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the GroupRoleDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public GroupRoleDataSourceView(GroupRoleDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal GroupRoleDataSource GroupRoleOwner
		{
			get { return Owner as GroupRoleDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal GroupRoleSelectMethod SelectMethod
		{
			get { return GroupRoleOwner.SelectMethod; }
			set { GroupRoleOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal GroupRoleProviderBase GroupRoleProvider
		{
			get { return Provider as GroupRoleProviderBase; }
		}

		#endregion Properties
		
		#region Methods
		
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<GroupRole> GetSelectData(out int count)
		{
			Hashtable values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<GroupRole> results = null;
			GroupRole item;
			count = 0;
			
			System.Int64 _id;
			System.String _roleId_nullable;
			System.String _groupId;

			switch ( SelectMethod )
			{
				case GroupRoleSelectMethod.Get:
					GroupRoleKey entityKey  = new GroupRoleKey();
					entityKey.Load(values);
					item = GroupRoleProvider.Get(GetTransactionManager(), entityKey);
					results = new TList<GroupRole>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case GroupRoleSelectMethod.GetAll:
                    results = GroupRoleProvider.GetAll(GetTransactionManager(), StartIndex, PageSize, out count);
                    break;
				case GroupRoleSelectMethod.GetPaged:
					results = GroupRoleProvider.GetPaged(GetTransactionManager(), WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case GroupRoleSelectMethod.Find:
					if ( FilterParameters != null )
						results = GroupRoleProvider.Find(GetTransactionManager(), FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = GroupRoleProvider.Find(GetTransactionManager(), WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case GroupRoleSelectMethod.GetById:
					_id = ( values["Id"] != null ) ? (System.Int64) EntityUtil.ChangeType(values["Id"], typeof(System.Int64)) : (long)0;
					item = GroupRoleProvider.GetById(GetTransactionManager(), _id);
					results = new TList<GroupRole>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				case GroupRoleSelectMethod.GetByRoleId:
					_roleId_nullable = (System.String) EntityUtil.ChangeType(values["RoleId"], typeof(System.String));
					results = GroupRoleProvider.GetByRoleId(GetTransactionManager(), _roleId_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case GroupRoleSelectMethod.GetByGroupId:
					_groupId = ( values["GroupId"] != null ) ? (System.String) EntityUtil.ChangeType(values["GroupId"], typeof(System.String)) : string.Empty;
					results = GroupRoleProvider.GetByGroupId(GetTransactionManager(), _groupId, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == GroupRoleSelectMethod.Get || SelectMethod == GroupRoleSelectMethod.GetById )
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
				GroupRole entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// execute deep load method
					GroupRoleProvider.DeepLoad(GetTransactionManager(), GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<GroupRole> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// execute deep load method
			GroupRoleProvider.DeepLoad(GetTransactionManager(), entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region GroupRoleDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the GroupRoleDataSource class.
	/// </summary>
	public class GroupRoleDataSourceDesigner : ProviderDataSourceDesigner<GroupRole, GroupRoleKey>
	{
		/// <summary>
		/// Initializes a new instance of the GroupRoleDataSourceDesigner class.
		/// </summary>
		public GroupRoleDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public GroupRoleSelectMethod SelectMethod
		{
			get { return ((GroupRoleDataSource) DataSource).SelectMethod; }
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
				actions.Add(new GroupRoleDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region GroupRoleDataSourceActionList

	/// <summary>
	/// Supports the GroupRoleDataSourceDesigner class.
	/// </summary>
	internal class GroupRoleDataSourceActionList : DesignerActionList
	{
		private GroupRoleDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the GroupRoleDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public GroupRoleDataSourceActionList(GroupRoleDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public GroupRoleSelectMethod SelectMethod
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

	#endregion GroupRoleDataSourceActionList
	
	#endregion GroupRoleDataSourceDesigner
	
	#region GroupRoleSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the GroupRoleDataSource.SelectMethod property.
	/// </summary>
	public enum GroupRoleSelectMethod
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
		/// Represents the GetByGroupId method.
		/// </summary>
		GetByGroupId
	}
	
	#endregion GroupRoleSelectMethod

	#region GroupRoleFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="GroupRole"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class GroupRoleFilter : SqlFilter<GroupRoleColumn>
	{
	}
	
	#endregion GroupRoleFilter

	#region GroupRoleExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="GroupRole"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class GroupRoleExpressionBuilder : SqlExpressionBuilder<GroupRoleColumn>
	{
	}
	
	#endregion GroupRoleExpressionBuilder	

	#region GroupRoleProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;GroupRoleChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="GroupRole"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class GroupRoleProperty : ChildEntityProperty<GroupRoleChildEntityTypes>
	{
	}
	
	#endregion GroupRoleProperty
}

