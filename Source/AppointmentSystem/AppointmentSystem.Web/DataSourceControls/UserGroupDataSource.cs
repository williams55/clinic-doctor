﻿#region Using Directives
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
	/// Represents the DataRepository.UserGroupProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(UserGroupDataSourceDesigner))]
	public class UserGroupDataSource : ProviderDataSource<UserGroup, UserGroupKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the UserGroupDataSource class.
		/// </summary>
		public UserGroupDataSource() : base(DataRepository.UserGroupProvider)
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the UserGroupDataSourceView used by the UserGroupDataSource.
		/// </summary>
		protected UserGroupDataSourceView UserGroupView
		{
			get { return ( View as UserGroupDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the UserGroupDataSource control invokes to retrieve data.
		/// </summary>
		public UserGroupSelectMethod SelectMethod
		{
			get
			{
				UserGroupSelectMethod selectMethod = UserGroupSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (UserGroupSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the UserGroupDataSourceView class that is to be
		/// used by the UserGroupDataSource.
		/// </summary>
		/// <returns>An instance of the UserGroupDataSourceView class.</returns>
		protected override BaseDataSourceView<UserGroup, UserGroupKey> GetNewDataSourceView()
		{
			return new UserGroupDataSourceView(this, DefaultViewName);
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
	/// Supports the UserGroupDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class UserGroupDataSourceView : ProviderDataSourceView<UserGroup, UserGroupKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the UserGroupDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the UserGroupDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public UserGroupDataSourceView(UserGroupDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal UserGroupDataSource UserGroupOwner
		{
			get { return Owner as UserGroupDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal UserGroupSelectMethod SelectMethod
		{
			get { return UserGroupOwner.SelectMethod; }
			set { UserGroupOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal UserGroupProviderBase UserGroupProvider
		{
			get { return Provider as UserGroupProviderBase; }
		}

		#endregion Properties
		
		#region Methods
		
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<UserGroup> GetSelectData(out int count)
		{
			Hashtable values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<UserGroup> results = null;
			UserGroup item;
			count = 0;
			
			System.Int32 _id;

			switch ( SelectMethod )
			{
				case UserGroupSelectMethod.Get:
					UserGroupKey entityKey  = new UserGroupKey();
					entityKey.Load(values);
					item = UserGroupProvider.Get(GetTransactionManager(), entityKey);
					results = new TList<UserGroup>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case UserGroupSelectMethod.GetAll:
                    results = UserGroupProvider.GetAll(GetTransactionManager(), StartIndex, PageSize, out count);
                    break;
				case UserGroupSelectMethod.GetPaged:
					results = UserGroupProvider.GetPaged(GetTransactionManager(), WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case UserGroupSelectMethod.Find:
					if ( FilterParameters != null )
						results = UserGroupProvider.Find(GetTransactionManager(), FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = UserGroupProvider.Find(GetTransactionManager(), WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case UserGroupSelectMethod.GetById:
					_id = ( values["Id"] != null ) ? (System.Int32) EntityUtil.ChangeType(values["Id"], typeof(System.Int32)) : (int)0;
					item = UserGroupProvider.GetById(GetTransactionManager(), _id);
					results = new TList<UserGroup>();
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
			if ( SelectMethod == UserGroupSelectMethod.Get || SelectMethod == UserGroupSelectMethod.GetById )
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
				UserGroup entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// execute deep load method
					UserGroupProvider.DeepLoad(GetTransactionManager(), GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<UserGroup> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// execute deep load method
			UserGroupProvider.DeepLoad(GetTransactionManager(), entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region UserGroupDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the UserGroupDataSource class.
	/// </summary>
	public class UserGroupDataSourceDesigner : ProviderDataSourceDesigner<UserGroup, UserGroupKey>
	{
		/// <summary>
		/// Initializes a new instance of the UserGroupDataSourceDesigner class.
		/// </summary>
		public UserGroupDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public UserGroupSelectMethod SelectMethod
		{
			get { return ((UserGroupDataSource) DataSource).SelectMethod; }
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
				actions.Add(new UserGroupDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region UserGroupDataSourceActionList

	/// <summary>
	/// Supports the UserGroupDataSourceDesigner class.
	/// </summary>
	internal class UserGroupDataSourceActionList : DesignerActionList
	{
		private UserGroupDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the UserGroupDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public UserGroupDataSourceActionList(UserGroupDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public UserGroupSelectMethod SelectMethod
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

	#endregion UserGroupDataSourceActionList
	
	#endregion UserGroupDataSourceDesigner
	
	#region UserGroupSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the UserGroupDataSource.SelectMethod property.
	/// </summary>
	public enum UserGroupSelectMethod
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
	
	#endregion UserGroupSelectMethod

	#region UserGroupFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="UserGroup"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class UserGroupFilter : SqlFilter<UserGroupColumn>
	{
	}
	
	#endregion UserGroupFilter

	#region UserGroupExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="UserGroup"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class UserGroupExpressionBuilder : SqlExpressionBuilder<UserGroupColumn>
	{
	}
	
	#endregion UserGroupExpressionBuilder	

	#region UserGroupProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;UserGroupChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="UserGroup"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class UserGroupProperty : ChildEntityProperty<UserGroupChildEntityTypes>
	{
	}
	
	#endregion UserGroupProperty
}

