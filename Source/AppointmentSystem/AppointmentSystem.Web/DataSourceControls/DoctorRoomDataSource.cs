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
	/// Represents the DataRepository.DoctorRoomProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(DoctorRoomDataSourceDesigner))]
	public class DoctorRoomDataSource : ProviderDataSource<DoctorRoom, DoctorRoomKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DoctorRoomDataSource class.
		/// </summary>
		public DoctorRoomDataSource() : base(DataRepository.DoctorRoomProvider)
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the DoctorRoomDataSourceView used by the DoctorRoomDataSource.
		/// </summary>
		protected DoctorRoomDataSourceView DoctorRoomView
		{
			get { return ( View as DoctorRoomDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DoctorRoomDataSource control invokes to retrieve data.
		/// </summary>
		public DoctorRoomSelectMethod SelectMethod
		{
			get
			{
				DoctorRoomSelectMethod selectMethod = DoctorRoomSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (DoctorRoomSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the DoctorRoomDataSourceView class that is to be
		/// used by the DoctorRoomDataSource.
		/// </summary>
		/// <returns>An instance of the DoctorRoomDataSourceView class.</returns>
		protected override BaseDataSourceView<DoctorRoom, DoctorRoomKey> GetNewDataSourceView()
		{
			return new DoctorRoomDataSourceView(this, DefaultViewName);
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
	/// Supports the DoctorRoomDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class DoctorRoomDataSourceView : ProviderDataSourceView<DoctorRoom, DoctorRoomKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DoctorRoomDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the DoctorRoomDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public DoctorRoomDataSourceView(DoctorRoomDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal DoctorRoomDataSource DoctorRoomOwner
		{
			get { return Owner as DoctorRoomDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal DoctorRoomSelectMethod SelectMethod
		{
			get { return DoctorRoomOwner.SelectMethod; }
			set { DoctorRoomOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal DoctorRoomProviderBase DoctorRoomProvider
		{
			get { return Provider as DoctorRoomProviderBase; }
		}

		#endregion Properties
		
		#region Methods
		
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<DoctorRoom> GetSelectData(out int count)
		{
			Hashtable values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<DoctorRoom> results = null;
			DoctorRoom item;
			count = 0;
			
			System.Int64 _id;
			System.String _roomId_nullable;
			System.String _doctorId_nullable;

			switch ( SelectMethod )
			{
				case DoctorRoomSelectMethod.Get:
					DoctorRoomKey entityKey  = new DoctorRoomKey();
					entityKey.Load(values);
					item = DoctorRoomProvider.Get(GetTransactionManager(), entityKey);
					results = new TList<DoctorRoom>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case DoctorRoomSelectMethod.GetAll:
                    results = DoctorRoomProvider.GetAll(GetTransactionManager(), StartIndex, PageSize, out count);
                    break;
				case DoctorRoomSelectMethod.GetPaged:
					results = DoctorRoomProvider.GetPaged(GetTransactionManager(), WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case DoctorRoomSelectMethod.Find:
					if ( FilterParameters != null )
						results = DoctorRoomProvider.Find(GetTransactionManager(), FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = DoctorRoomProvider.Find(GetTransactionManager(), WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case DoctorRoomSelectMethod.GetById:
					_id = ( values["Id"] != null ) ? (System.Int64) EntityUtil.ChangeType(values["Id"], typeof(System.Int64)) : (long)0;
					item = DoctorRoomProvider.GetById(GetTransactionManager(), _id);
					results = new TList<DoctorRoom>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				case DoctorRoomSelectMethod.GetByRoomId:
					_roomId_nullable = (System.String) EntityUtil.ChangeType(values["RoomId"], typeof(System.String));
					results = DoctorRoomProvider.GetByRoomId(GetTransactionManager(), _roomId_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case DoctorRoomSelectMethod.GetByDoctorId:
					_doctorId_nullable = (System.String) EntityUtil.ChangeType(values["DoctorId"], typeof(System.String));
					results = DoctorRoomProvider.GetByDoctorId(GetTransactionManager(), _doctorId_nullable, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == DoctorRoomSelectMethod.Get || SelectMethod == DoctorRoomSelectMethod.GetById )
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
				DoctorRoom entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// execute deep load method
					DoctorRoomProvider.DeepLoad(GetTransactionManager(), GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<DoctorRoom> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// execute deep load method
			DoctorRoomProvider.DeepLoad(GetTransactionManager(), entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region DoctorRoomDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the DoctorRoomDataSource class.
	/// </summary>
	public class DoctorRoomDataSourceDesigner : ProviderDataSourceDesigner<DoctorRoom, DoctorRoomKey>
	{
		/// <summary>
		/// Initializes a new instance of the DoctorRoomDataSourceDesigner class.
		/// </summary>
		public DoctorRoomDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public DoctorRoomSelectMethod SelectMethod
		{
			get { return ((DoctorRoomDataSource) DataSource).SelectMethod; }
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
				actions.Add(new DoctorRoomDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region DoctorRoomDataSourceActionList

	/// <summary>
	/// Supports the DoctorRoomDataSourceDesigner class.
	/// </summary>
	internal class DoctorRoomDataSourceActionList : DesignerActionList
	{
		private DoctorRoomDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the DoctorRoomDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public DoctorRoomDataSourceActionList(DoctorRoomDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public DoctorRoomSelectMethod SelectMethod
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

	#endregion DoctorRoomDataSourceActionList
	
	#endregion DoctorRoomDataSourceDesigner
	
	#region DoctorRoomSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the DoctorRoomDataSource.SelectMethod property.
	/// </summary>
	public enum DoctorRoomSelectMethod
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
		/// Represents the GetByRoomId method.
		/// </summary>
		GetByRoomId,
		/// <summary>
		/// Represents the GetByDoctorId method.
		/// </summary>
		GetByDoctorId
	}
	
	#endregion DoctorRoomSelectMethod

	#region DoctorRoomFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="DoctorRoom"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class DoctorRoomFilter : SqlFilter<DoctorRoomColumn>
	{
	}
	
	#endregion DoctorRoomFilter

	#region DoctorRoomExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="DoctorRoom"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class DoctorRoomExpressionBuilder : SqlExpressionBuilder<DoctorRoomColumn>
	{
	}
	
	#endregion DoctorRoomExpressionBuilder	

	#region DoctorRoomProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;DoctorRoomChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="DoctorRoom"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class DoctorRoomProperty : ChildEntityProperty<DoctorRoomChildEntityTypes>
	{
	}
	
	#endregion DoctorRoomProperty
}

