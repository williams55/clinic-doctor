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
	/// Represents the DataRepository.UsersProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(UsersDataSourceDesigner))]
	public class UsersDataSource : ProviderDataSource<Users, UsersKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the UsersDataSource class.
		/// </summary>
		public UsersDataSource() : base(DataRepository.UsersProvider)
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the UsersDataSourceView used by the UsersDataSource.
		/// </summary>
		protected UsersDataSourceView UsersView
		{
			get { return ( View as UsersDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the UsersDataSource control invokes to retrieve data.
		/// </summary>
		public UsersSelectMethod SelectMethod
		{
			get
			{
				UsersSelectMethod selectMethod = UsersSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (UsersSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the UsersDataSourceView class that is to be
		/// used by the UsersDataSource.
		/// </summary>
		/// <returns>An instance of the UsersDataSourceView class.</returns>
		protected override BaseDataSourceView<Users, UsersKey> GetNewDataSourceView()
		{
			return new UsersDataSourceView(this, DefaultViewName);
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
	/// Supports the UsersDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class UsersDataSourceView : ProviderDataSourceView<Users, UsersKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the UsersDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the UsersDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public UsersDataSourceView(UsersDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal UsersDataSource UsersOwner
		{
			get { return Owner as UsersDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal UsersSelectMethod SelectMethod
		{
			get { return UsersOwner.SelectMethod; }
			set { UsersOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal UsersProviderBase UsersProvider
		{
			get { return Provider as UsersProviderBase; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<Users> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<Users> results = null;
			Users item;
			count = 0;
			
			System.String _username;
			System.Int32? _servicesId_nullable;
			System.String _userGroupId;

			switch ( SelectMethod )
			{
				case UsersSelectMethod.Get:
					UsersKey entityKey  = new UsersKey();
					entityKey.Load(values);
					item = UsersProvider.Get(GetTransactionManager(), entityKey);
					results = new TList<Users>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case UsersSelectMethod.GetAll:
                    results = UsersProvider.GetAll(GetTransactionManager(), StartIndex, PageSize, out count);
                    break;
				case UsersSelectMethod.GetPaged:
					results = UsersProvider.GetPaged(GetTransactionManager(), WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case UsersSelectMethod.Find:
					if ( FilterParameters != null )
						results = UsersProvider.Find(GetTransactionManager(), FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = UsersProvider.Find(GetTransactionManager(), WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case UsersSelectMethod.GetByUsername:
					_username = ( values["Username"] != null ) ? (System.String) EntityUtil.ChangeType(values["Username"], typeof(System.String)) : string.Empty;
					item = UsersProvider.GetByUsername(GetTransactionManager(), _username);
					results = new TList<Users>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				case UsersSelectMethod.GetByServicesId:
					_servicesId_nullable = (System.Int32?) EntityUtil.ChangeType(values["ServicesId"], typeof(System.Int32?));
					results = UsersProvider.GetByServicesId(GetTransactionManager(), _servicesId_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case UsersSelectMethod.GetByUserGroupId:
					_userGroupId = ( values["UserGroupId"] != null ) ? (System.String) EntityUtil.ChangeType(values["UserGroupId"], typeof(System.String)) : "0";
					results = UsersProvider.GetByUserGroupId(GetTransactionManager(), _userGroupId, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == UsersSelectMethod.Get || SelectMethod == UsersSelectMethod.GetByUsername )
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
				Users entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// execute deep load method
					UsersProvider.DeepLoad(GetTransactionManager(), GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<Users> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// execute deep load method
			UsersProvider.DeepLoad(GetTransactionManager(), entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region UsersDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the UsersDataSource class.
	/// </summary>
	public class UsersDataSourceDesigner : ProviderDataSourceDesigner<Users, UsersKey>
	{
		/// <summary>
		/// Initializes a new instance of the UsersDataSourceDesigner class.
		/// </summary>
		public UsersDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public UsersSelectMethod SelectMethod
		{
			get { return ((UsersDataSource) DataSource).SelectMethod; }
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
				actions.Add(new UsersDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region UsersDataSourceActionList

	/// <summary>
	/// Supports the UsersDataSourceDesigner class.
	/// </summary>
	internal class UsersDataSourceActionList : DesignerActionList
	{
		private UsersDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the UsersDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public UsersDataSourceActionList(UsersDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public UsersSelectMethod SelectMethod
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

	#endregion UsersDataSourceActionList
	
	#endregion UsersDataSourceDesigner
	
	#region UsersSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the UsersDataSource.SelectMethod property.
	/// </summary>
	public enum UsersSelectMethod
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
		/// Represents the GetByUsername method.
		/// </summary>
		GetByUsername,
		/// <summary>
		/// Represents the GetByServicesId method.
		/// </summary>
		GetByServicesId,
		/// <summary>
		/// Represents the GetByUserGroupId method.
		/// </summary>
		GetByUserGroupId
	}
	
	#endregion UsersSelectMethod

	#region UsersFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Users"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class UsersFilter : SqlFilter<UsersColumn>
	{
	}
	
	#endregion UsersFilter

	#region UsersExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Users"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class UsersExpressionBuilder : SqlExpressionBuilder<UsersColumn>
	{
	}
	
	#endregion UsersExpressionBuilder	

	#region UsersProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;UsersChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="Users"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class UsersProperty : ChildEntityProperty<UsersChildEntityTypes>
	{
	}
	
	#endregion UsersProperty
}

