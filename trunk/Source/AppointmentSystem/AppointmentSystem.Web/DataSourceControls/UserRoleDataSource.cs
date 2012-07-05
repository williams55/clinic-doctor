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
	/// Represents the DataRepository.UserRoleProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(UserRoleDataSourceDesigner))]
	public class UserRoleDataSource : ProviderDataSource<UserRole, UserRoleKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the UserRoleDataSource class.
		/// </summary>
		public UserRoleDataSource() : base(DataRepository.UserRoleProvider)
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the UserRoleDataSourceView used by the UserRoleDataSource.
		/// </summary>
		protected UserRoleDataSourceView UserRoleView
		{
			get { return ( View as UserRoleDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the UserRoleDataSource control invokes to retrieve data.
		/// </summary>
		public UserRoleSelectMethod SelectMethod
		{
			get
			{
				UserRoleSelectMethod selectMethod = UserRoleSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (UserRoleSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the UserRoleDataSourceView class that is to be
		/// used by the UserRoleDataSource.
		/// </summary>
		/// <returns>An instance of the UserRoleDataSourceView class.</returns>
		protected override BaseDataSourceView<UserRole, UserRoleKey> GetNewDataSourceView()
		{
			return new UserRoleDataSourceView(this, DefaultViewName);
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
	/// Supports the UserRoleDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class UserRoleDataSourceView : ProviderDataSourceView<UserRole, UserRoleKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the UserRoleDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the UserRoleDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public UserRoleDataSourceView(UserRoleDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal UserRoleDataSource UserRoleOwner
		{
			get { return Owner as UserRoleDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal UserRoleSelectMethod SelectMethod
		{
			get { return UserRoleOwner.SelectMethod; }
			set { UserRoleOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal UserRoleProviderBase UserRoleProvider
		{
			get { return Provider as UserRoleProviderBase; }
		}

		#endregion Properties
		
		#region Methods
		
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<UserRole> GetSelectData(out int count)
		{
			Hashtable values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<UserRole> results = null;
			UserRole item;
			count = 0;
			
			System.Int64 _id;
			System.Int32? _roleId_nullable;
			System.String _userId;

			switch ( SelectMethod )
			{
				case UserRoleSelectMethod.Get:
					UserRoleKey entityKey  = new UserRoleKey();
					entityKey.Load(values);
					item = UserRoleProvider.Get(GetTransactionManager(), entityKey);
					results = new TList<UserRole>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case UserRoleSelectMethod.GetAll:
                    results = UserRoleProvider.GetAll(GetTransactionManager(), StartIndex, PageSize, out count);
                    break;
				case UserRoleSelectMethod.GetPaged:
					results = UserRoleProvider.GetPaged(GetTransactionManager(), WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case UserRoleSelectMethod.Find:
					if ( FilterParameters != null )
						results = UserRoleProvider.Find(GetTransactionManager(), FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = UserRoleProvider.Find(GetTransactionManager(), WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case UserRoleSelectMethod.GetById:
					_id = ( values["Id"] != null ) ? (System.Int64) EntityUtil.ChangeType(values["Id"], typeof(System.Int64)) : (long)0;
					item = UserRoleProvider.GetById(GetTransactionManager(), _id);
					results = new TList<UserRole>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				case UserRoleSelectMethod.GetByRoleId:
					_roleId_nullable = (System.Int32?) EntityUtil.ChangeType(values["RoleId"], typeof(System.Int32?));
					results = UserRoleProvider.GetByRoleId(GetTransactionManager(), _roleId_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case UserRoleSelectMethod.GetByUserId:
					_userId = ( values["UserId"] != null ) ? (System.String) EntityUtil.ChangeType(values["UserId"], typeof(System.String)) : string.Empty;
					results = UserRoleProvider.GetByUserId(GetTransactionManager(), _userId, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == UserRoleSelectMethod.Get || SelectMethod == UserRoleSelectMethod.GetById )
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
				UserRole entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// execute deep load method
					UserRoleProvider.DeepLoad(GetTransactionManager(), GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<UserRole> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// execute deep load method
			UserRoleProvider.DeepLoad(GetTransactionManager(), entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region UserRoleDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the UserRoleDataSource class.
	/// </summary>
	public class UserRoleDataSourceDesigner : ProviderDataSourceDesigner<UserRole, UserRoleKey>
	{
		/// <summary>
		/// Initializes a new instance of the UserRoleDataSourceDesigner class.
		/// </summary>
		public UserRoleDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public UserRoleSelectMethod SelectMethod
		{
			get { return ((UserRoleDataSource) DataSource).SelectMethod; }
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
				actions.Add(new UserRoleDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region UserRoleDataSourceActionList

	/// <summary>
	/// Supports the UserRoleDataSourceDesigner class.
	/// </summary>
	internal class UserRoleDataSourceActionList : DesignerActionList
	{
		private UserRoleDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the UserRoleDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public UserRoleDataSourceActionList(UserRoleDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public UserRoleSelectMethod SelectMethod
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

	#endregion UserRoleDataSourceActionList
	
	#endregion UserRoleDataSourceDesigner
	
	#region UserRoleSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the UserRoleDataSource.SelectMethod property.
	/// </summary>
	public enum UserRoleSelectMethod
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
		/// Represents the GetByUserId method.
		/// </summary>
		GetByUserId
	}
	
	#endregion UserRoleSelectMethod

	#region UserRoleFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="UserRole"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class UserRoleFilter : SqlFilter<UserRoleColumn>
	{
	}
	
	#endregion UserRoleFilter

	#region UserRoleExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="UserRole"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class UserRoleExpressionBuilder : SqlExpressionBuilder<UserRoleColumn>
	{
	}
	
	#endregion UserRoleExpressionBuilder	

	#region UserRoleProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;UserRoleChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="UserRole"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class UserRoleProperty : ChildEntityProperty<UserRoleChildEntityTypes>
	{
	}
	
	#endregion UserRoleProperty
}

