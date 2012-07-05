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
	/// Represents the DataRepository.RoomProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(RoomDataSourceDesigner))]
	public class RoomDataSource : ProviderDataSource<Room, RoomKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the RoomDataSource class.
		/// </summary>
		public RoomDataSource() : base(DataRepository.RoomProvider)
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the RoomDataSourceView used by the RoomDataSource.
		/// </summary>
		protected RoomDataSourceView RoomView
		{
			get { return ( View as RoomDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the RoomDataSource control invokes to retrieve data.
		/// </summary>
		public RoomSelectMethod SelectMethod
		{
			get
			{
				RoomSelectMethod selectMethod = RoomSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (RoomSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the RoomDataSourceView class that is to be
		/// used by the RoomDataSource.
		/// </summary>
		/// <returns>An instance of the RoomDataSourceView class.</returns>
		protected override BaseDataSourceView<Room, RoomKey> GetNewDataSourceView()
		{
			return new RoomDataSourceView(this, DefaultViewName);
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
	/// Supports the RoomDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class RoomDataSourceView : ProviderDataSourceView<Room, RoomKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the RoomDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the RoomDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public RoomDataSourceView(RoomDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal RoomDataSource RoomOwner
		{
			get { return Owner as RoomDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal RoomSelectMethod SelectMethod
		{
			get { return RoomOwner.SelectMethod; }
			set { RoomOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal RoomProviderBase RoomProvider
		{
			get { return Provider as RoomProviderBase; }
		}

		#endregion Properties
		
		#region Methods
		
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<Room> GetSelectData(out int count)
		{
			Hashtable values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<Room> results = null;
			Room item;
			count = 0;
			
			System.Int32 _id;
			System.Int32? _servicesId_nullable;

			switch ( SelectMethod )
			{
				case RoomSelectMethod.Get:
					RoomKey entityKey  = new RoomKey();
					entityKey.Load(values);
					item = RoomProvider.Get(GetTransactionManager(), entityKey);
					results = new TList<Room>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case RoomSelectMethod.GetAll:
                    results = RoomProvider.GetAll(GetTransactionManager(), StartIndex, PageSize, out count);
                    break;
				case RoomSelectMethod.GetPaged:
					results = RoomProvider.GetPaged(GetTransactionManager(), WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case RoomSelectMethod.Find:
					if ( FilterParameters != null )
						results = RoomProvider.Find(GetTransactionManager(), FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = RoomProvider.Find(GetTransactionManager(), WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case RoomSelectMethod.GetById:
					_id = ( values["Id"] != null ) ? (System.Int32) EntityUtil.ChangeType(values["Id"], typeof(System.Int32)) : (int)0;
					item = RoomProvider.GetById(GetTransactionManager(), _id);
					results = new TList<Room>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				case RoomSelectMethod.GetByServicesId:
					_servicesId_nullable = (System.Int32?) EntityUtil.ChangeType(values["ServicesId"], typeof(System.Int32?));
					results = RoomProvider.GetByServicesId(GetTransactionManager(), _servicesId_nullable, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == RoomSelectMethod.Get || SelectMethod == RoomSelectMethod.GetById )
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
				Room entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// execute deep load method
					RoomProvider.DeepLoad(GetTransactionManager(), GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<Room> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// execute deep load method
			RoomProvider.DeepLoad(GetTransactionManager(), entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region RoomDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the RoomDataSource class.
	/// </summary>
	public class RoomDataSourceDesigner : ProviderDataSourceDesigner<Room, RoomKey>
	{
		/// <summary>
		/// Initializes a new instance of the RoomDataSourceDesigner class.
		/// </summary>
		public RoomDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public RoomSelectMethod SelectMethod
		{
			get { return ((RoomDataSource) DataSource).SelectMethod; }
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
				actions.Add(new RoomDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region RoomDataSourceActionList

	/// <summary>
	/// Supports the RoomDataSourceDesigner class.
	/// </summary>
	internal class RoomDataSourceActionList : DesignerActionList
	{
		private RoomDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the RoomDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public RoomDataSourceActionList(RoomDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public RoomSelectMethod SelectMethod
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

	#endregion RoomDataSourceActionList
	
	#endregion RoomDataSourceDesigner
	
	#region RoomSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the RoomDataSource.SelectMethod property.
	/// </summary>
	public enum RoomSelectMethod
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
		/// Represents the GetByServicesId method.
		/// </summary>
		GetByServicesId
	}
	
	#endregion RoomSelectMethod

	#region RoomFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Room"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class RoomFilter : SqlFilter<RoomColumn>
	{
	}
	
	#endregion RoomFilter

	#region RoomExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Room"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class RoomExpressionBuilder : SqlExpressionBuilder<RoomColumn>
	{
	}
	
	#endregion RoomExpressionBuilder	

	#region RoomProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;RoomChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="Room"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class RoomProperty : ChildEntityProperty<RoomChildEntityTypes>
	{
	}
	
	#endregion RoomProperty
}

