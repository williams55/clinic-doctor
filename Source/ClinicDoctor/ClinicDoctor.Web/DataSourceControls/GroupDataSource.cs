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
	/// Represents the DataRepository.GroupProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(GroupDataSourceDesigner))]
	public class GroupDataSource : ProviderDataSource<Group, GroupKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the GroupDataSource class.
		/// </summary>
		public GroupDataSource() : base(DataRepository.GroupProvider)
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the GroupDataSourceView used by the GroupDataSource.
		/// </summary>
		protected GroupDataSourceView GroupView
		{
			get { return ( View as GroupDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the GroupDataSource control invokes to retrieve data.
		/// </summary>
		public GroupSelectMethod SelectMethod
		{
			get
			{
				GroupSelectMethod selectMethod = GroupSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (GroupSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the GroupDataSourceView class that is to be
		/// used by the GroupDataSource.
		/// </summary>
		/// <returns>An instance of the GroupDataSourceView class.</returns>
		protected override BaseDataSourceView<Group, GroupKey> GetNewDataSourceView()
		{
			return new GroupDataSourceView(this, DefaultViewName);
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
	/// Supports the GroupDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class GroupDataSourceView : ProviderDataSourceView<Group, GroupKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the GroupDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the GroupDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public GroupDataSourceView(GroupDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal GroupDataSource GroupOwner
		{
			get { return Owner as GroupDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal GroupSelectMethod SelectMethod
		{
			get { return GroupOwner.SelectMethod; }
			set { GroupOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal GroupProviderBase GroupProvider
		{
			get { return Provider as GroupProviderBase; }
		}

		#endregion Properties
		
		#region Methods
		
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<Group> GetSelectData(out int count)
		{
			Hashtable values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<Group> results = null;
			Group item;
			count = 0;
			
			System.Int32 _id;

			switch ( SelectMethod )
			{
				case GroupSelectMethod.Get:
					GroupKey entityKey  = new GroupKey();
					entityKey.Load(values);
					item = GroupProvider.Get(GetTransactionManager(), entityKey);
					results = new TList<Group>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case GroupSelectMethod.GetAll:
                    results = GroupProvider.GetAll(GetTransactionManager(), StartIndex, PageSize, out count);
                    break;
				case GroupSelectMethod.GetPaged:
					results = GroupProvider.GetPaged(GetTransactionManager(), WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case GroupSelectMethod.Find:
					if ( FilterParameters != null )
						results = GroupProvider.Find(GetTransactionManager(), FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = GroupProvider.Find(GetTransactionManager(), WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case GroupSelectMethod.GetById:
					_id = ( values["Id"] != null ) ? (System.Int32) EntityUtil.ChangeType(values["Id"], typeof(System.Int32)) : (int)0;
					item = GroupProvider.GetById(GetTransactionManager(), _id);
					results = new TList<Group>();
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
			if ( SelectMethod == GroupSelectMethod.Get || SelectMethod == GroupSelectMethod.GetById )
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
				Group entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// execute deep load method
					GroupProvider.DeepLoad(GetTransactionManager(), GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<Group> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// execute deep load method
			GroupProvider.DeepLoad(GetTransactionManager(), entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region GroupDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the GroupDataSource class.
	/// </summary>
	public class GroupDataSourceDesigner : ProviderDataSourceDesigner<Group, GroupKey>
	{
		/// <summary>
		/// Initializes a new instance of the GroupDataSourceDesigner class.
		/// </summary>
		public GroupDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public GroupSelectMethod SelectMethod
		{
			get { return ((GroupDataSource) DataSource).SelectMethod; }
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
				actions.Add(new GroupDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region GroupDataSourceActionList

	/// <summary>
	/// Supports the GroupDataSourceDesigner class.
	/// </summary>
	internal class GroupDataSourceActionList : DesignerActionList
	{
		private GroupDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the GroupDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public GroupDataSourceActionList(GroupDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public GroupSelectMethod SelectMethod
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

	#endregion GroupDataSourceActionList
	
	#endregion GroupDataSourceDesigner
	
	#region GroupSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the GroupDataSource.SelectMethod property.
	/// </summary>
	public enum GroupSelectMethod
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
	
	#endregion GroupSelectMethod

	#region GroupFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Group"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class GroupFilter : SqlFilter<GroupColumn>
	{
	}
	
	#endregion GroupFilter

	#region GroupExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Group"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class GroupExpressionBuilder : SqlExpressionBuilder<GroupColumn>
	{
	}
	
	#endregion GroupExpressionBuilder	

	#region GroupProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;GroupChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="Group"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class GroupProperty : ChildEntityProperty<GroupChildEntityTypes>
	{
	}
	
	#endregion GroupProperty
}

