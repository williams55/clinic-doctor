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
	/// Represents the DataRepository.RoomFuncProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(RoomFuncDataSourceDesigner))]
	public class RoomFuncDataSource : ProviderDataSource<RoomFunc, RoomFuncKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the RoomFuncDataSource class.
		/// </summary>
		public RoomFuncDataSource() : base(DataRepository.RoomFuncProvider)
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the RoomFuncDataSourceView used by the RoomFuncDataSource.
		/// </summary>
		protected RoomFuncDataSourceView RoomFuncView
		{
			get { return ( View as RoomFuncDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the RoomFuncDataSource control invokes to retrieve data.
		/// </summary>
		public RoomFuncSelectMethod SelectMethod
		{
			get
			{
				RoomFuncSelectMethod selectMethod = RoomFuncSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (RoomFuncSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the RoomFuncDataSourceView class that is to be
		/// used by the RoomFuncDataSource.
		/// </summary>
		/// <returns>An instance of the RoomFuncDataSourceView class.</returns>
		protected override BaseDataSourceView<RoomFunc, RoomFuncKey> GetNewDataSourceView()
		{
			return new RoomFuncDataSourceView(this, DefaultViewName);
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
	/// Supports the RoomFuncDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class RoomFuncDataSourceView : ProviderDataSourceView<RoomFunc, RoomFuncKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the RoomFuncDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the RoomFuncDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public RoomFuncDataSourceView(RoomFuncDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal RoomFuncDataSource RoomFuncOwner
		{
			get { return Owner as RoomFuncDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal RoomFuncSelectMethod SelectMethod
		{
			get { return RoomFuncOwner.SelectMethod; }
			set { RoomFuncOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal RoomFuncProviderBase RoomFuncProvider
		{
			get { return Provider as RoomFuncProviderBase; }
		}

		#endregion Properties
		
		#region Methods
		
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<RoomFunc> GetSelectData(out int count)
		{
			Hashtable values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<RoomFunc> results = null;
			RoomFunc item;
			count = 0;
			
			System.Int64 _funcId;
			System.Boolean _isDisabled;
			System.Int64 _id;
			System.Int64 _roomId;

			switch ( SelectMethod )
			{
				case RoomFuncSelectMethod.Get:
					RoomFuncKey entityKey  = new RoomFuncKey();
					entityKey.Load(values);
					item = RoomFuncProvider.Get(GetTransactionManager(), entityKey);
					results = new TList<RoomFunc>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case RoomFuncSelectMethod.GetAll:
                    results = RoomFuncProvider.GetAll(GetTransactionManager(), StartIndex, PageSize, out count);
                    break;
				case RoomFuncSelectMethod.GetPaged:
					results = RoomFuncProvider.GetPaged(GetTransactionManager(), WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case RoomFuncSelectMethod.Find:
					if ( FilterParameters != null )
						results = RoomFuncProvider.Find(GetTransactionManager(), FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = RoomFuncProvider.Find(GetTransactionManager(), WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case RoomFuncSelectMethod.GetById:
					_id = ( values["Id"] != null ) ? (System.Int64) EntityUtil.ChangeType(values["Id"], typeof(System.Int64)) : (long)0;
					item = RoomFuncProvider.GetById(GetTransactionManager(), _id);
					results = new TList<RoomFunc>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				case RoomFuncSelectMethod.GetByFuncId:
					_funcId = ( values["FuncId"] != null ) ? (System.Int64) EntityUtil.ChangeType(values["FuncId"], typeof(System.Int64)) : (long)0;
					results = RoomFuncProvider.GetByFuncId(GetTransactionManager(), _funcId, this.StartIndex, this.PageSize, out count);
					break;
				case RoomFuncSelectMethod.GetByFuncIdIsDisabled:
					_funcId = ( values["FuncId"] != null ) ? (System.Int64) EntityUtil.ChangeType(values["FuncId"], typeof(System.Int64)) : (long)0;
					_isDisabled = ( values["IsDisabled"] != null ) ? (System.Boolean) EntityUtil.ChangeType(values["IsDisabled"], typeof(System.Boolean)) : false;
					results = RoomFuncProvider.GetByFuncIdIsDisabled(GetTransactionManager(), _funcId, _isDisabled, this.StartIndex, this.PageSize, out count);
					break;
				case RoomFuncSelectMethod.GetByIdIsDisabled:
					_id = ( values["Id"] != null ) ? (System.Int64) EntityUtil.ChangeType(values["Id"], typeof(System.Int64)) : (long)0;
					_isDisabled = ( values["IsDisabled"] != null ) ? (System.Boolean) EntityUtil.ChangeType(values["IsDisabled"], typeof(System.Boolean)) : false;
					item = RoomFuncProvider.GetByIdIsDisabled(GetTransactionManager(), _id, _isDisabled);
					results = new TList<RoomFunc>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case RoomFuncSelectMethod.GetByIsDisabled:
					_isDisabled = ( values["IsDisabled"] != null ) ? (System.Boolean) EntityUtil.ChangeType(values["IsDisabled"], typeof(System.Boolean)) : false;
					results = RoomFuncProvider.GetByIsDisabled(GetTransactionManager(), _isDisabled, this.StartIndex, this.PageSize, out count);
					break;
				case RoomFuncSelectMethod.GetByRoomId:
					_roomId = ( values["RoomId"] != null ) ? (System.Int64) EntityUtil.ChangeType(values["RoomId"], typeof(System.Int64)) : (long)0;
					results = RoomFuncProvider.GetByRoomId(GetTransactionManager(), _roomId, this.StartIndex, this.PageSize, out count);
					break;
				case RoomFuncSelectMethod.GetByRoomIdFuncId:
					_roomId = ( values["RoomId"] != null ) ? (System.Int64) EntityUtil.ChangeType(values["RoomId"], typeof(System.Int64)) : (long)0;
					_funcId = ( values["FuncId"] != null ) ? (System.Int64) EntityUtil.ChangeType(values["FuncId"], typeof(System.Int64)) : (long)0;
					item = RoomFuncProvider.GetByRoomIdFuncId(GetTransactionManager(), _roomId, _funcId);
					results = new TList<RoomFunc>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case RoomFuncSelectMethod.GetByRoomIdFuncIdIsDisabled:
					_roomId = ( values["RoomId"] != null ) ? (System.Int64) EntityUtil.ChangeType(values["RoomId"], typeof(System.Int64)) : (long)0;
					_funcId = ( values["FuncId"] != null ) ? (System.Int64) EntityUtil.ChangeType(values["FuncId"], typeof(System.Int64)) : (long)0;
					_isDisabled = ( values["IsDisabled"] != null ) ? (System.Boolean) EntityUtil.ChangeType(values["IsDisabled"], typeof(System.Boolean)) : false;
					item = RoomFuncProvider.GetByRoomIdFuncIdIsDisabled(GetTransactionManager(), _roomId, _funcId, _isDisabled);
					results = new TList<RoomFunc>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case RoomFuncSelectMethod.GetByRoomIdIsDisabled:
					_roomId = ( values["RoomId"] != null ) ? (System.Int64) EntityUtil.ChangeType(values["RoomId"], typeof(System.Int64)) : (long)0;
					_isDisabled = ( values["IsDisabled"] != null ) ? (System.Boolean) EntityUtil.ChangeType(values["IsDisabled"], typeof(System.Boolean)) : false;
					results = RoomFuncProvider.GetByRoomIdIsDisabled(GetTransactionManager(), _roomId, _isDisabled, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == RoomFuncSelectMethod.Get || SelectMethod == RoomFuncSelectMethod.GetById )
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
				RoomFunc entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// execute deep load method
					RoomFuncProvider.DeepLoad(GetTransactionManager(), GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<RoomFunc> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// execute deep load method
			RoomFuncProvider.DeepLoad(GetTransactionManager(), entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region RoomFuncDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the RoomFuncDataSource class.
	/// </summary>
	public class RoomFuncDataSourceDesigner : ProviderDataSourceDesigner<RoomFunc, RoomFuncKey>
	{
		/// <summary>
		/// Initializes a new instance of the RoomFuncDataSourceDesigner class.
		/// </summary>
		public RoomFuncDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public RoomFuncSelectMethod SelectMethod
		{
			get { return ((RoomFuncDataSource) DataSource).SelectMethod; }
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
				actions.Add(new RoomFuncDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region RoomFuncDataSourceActionList

	/// <summary>
	/// Supports the RoomFuncDataSourceDesigner class.
	/// </summary>
	internal class RoomFuncDataSourceActionList : DesignerActionList
	{
		private RoomFuncDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the RoomFuncDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public RoomFuncDataSourceActionList(RoomFuncDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public RoomFuncSelectMethod SelectMethod
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

	#endregion RoomFuncDataSourceActionList
	
	#endregion RoomFuncDataSourceDesigner
	
	#region RoomFuncSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the RoomFuncDataSource.SelectMethod property.
	/// </summary>
	public enum RoomFuncSelectMethod
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
		/// Represents the GetByFuncId method.
		/// </summary>
		GetByFuncId,
		/// <summary>
		/// Represents the GetByFuncIdIsDisabled method.
		/// </summary>
		GetByFuncIdIsDisabled,
		/// <summary>
		/// Represents the GetByIdIsDisabled method.
		/// </summary>
		GetByIdIsDisabled,
		/// <summary>
		/// Represents the GetByIsDisabled method.
		/// </summary>
		GetByIsDisabled,
		/// <summary>
		/// Represents the GetByRoomId method.
		/// </summary>
		GetByRoomId,
		/// <summary>
		/// Represents the GetByRoomIdFuncId method.
		/// </summary>
		GetByRoomIdFuncId,
		/// <summary>
		/// Represents the GetByRoomIdFuncIdIsDisabled method.
		/// </summary>
		GetByRoomIdFuncIdIsDisabled,
		/// <summary>
		/// Represents the GetByRoomIdIsDisabled method.
		/// </summary>
		GetByRoomIdIsDisabled,
		/// <summary>
		/// Represents the GetById method.
		/// </summary>
		GetById
	}
	
	#endregion RoomFuncSelectMethod

	#region RoomFuncFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="RoomFunc"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class RoomFuncFilter : SqlFilter<RoomFuncColumn>
	{
	}
	
	#endregion RoomFuncFilter

	#region RoomFuncExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="RoomFunc"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class RoomFuncExpressionBuilder : SqlExpressionBuilder<RoomFuncColumn>
	{
	}
	
	#endregion RoomFuncExpressionBuilder	

	#region RoomFuncProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;RoomFuncChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="RoomFunc"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class RoomFuncProperty : ChildEntityProperty<RoomFuncChildEntityTypes>
	{
	}
	
	#endregion RoomFuncProperty
}

