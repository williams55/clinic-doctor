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
	/// Represents the DataRepository.DoctorRosterProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(DoctorRosterDataSourceDesigner))]
	public class DoctorRosterDataSource : ProviderDataSource<DoctorRoster, DoctorRosterKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DoctorRosterDataSource class.
		/// </summary>
		public DoctorRosterDataSource() : base(DataRepository.DoctorRosterProvider)
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the DoctorRosterDataSourceView used by the DoctorRosterDataSource.
		/// </summary>
		protected DoctorRosterDataSourceView DoctorRosterView
		{
			get { return ( View as DoctorRosterDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DoctorRosterDataSource control invokes to retrieve data.
		/// </summary>
		public DoctorRosterSelectMethod SelectMethod
		{
			get
			{
				DoctorRosterSelectMethod selectMethod = DoctorRosterSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (DoctorRosterSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the DoctorRosterDataSourceView class that is to be
		/// used by the DoctorRosterDataSource.
		/// </summary>
		/// <returns>An instance of the DoctorRosterDataSourceView class.</returns>
		protected override BaseDataSourceView<DoctorRoster, DoctorRosterKey> GetNewDataSourceView()
		{
			return new DoctorRosterDataSourceView(this, DefaultViewName);
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
	/// Supports the DoctorRosterDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class DoctorRosterDataSourceView : ProviderDataSourceView<DoctorRoster, DoctorRosterKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DoctorRosterDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the DoctorRosterDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public DoctorRosterDataSourceView(DoctorRosterDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal DoctorRosterDataSource DoctorRosterOwner
		{
			get { return Owner as DoctorRosterDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal DoctorRosterSelectMethod SelectMethod
		{
			get { return DoctorRosterOwner.SelectMethod; }
			set { DoctorRosterOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal DoctorRosterProviderBase DoctorRosterProvider
		{
			get { return Provider as DoctorRosterProviderBase; }
		}

		#endregion Properties
		
		#region Methods
		
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<DoctorRoster> GetSelectData(out int count)
		{
			Hashtable values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<DoctorRoster> results = null;
			DoctorRoster item;
			count = 0;
			
			System.Int64 _doctorId;
			System.Boolean _isComplete;
			System.Boolean _isDisabled;
			System.Int64 _rosterTypeId;
			System.String _id;

			switch ( SelectMethod )
			{
				case DoctorRosterSelectMethod.Get:
					DoctorRosterKey entityKey  = new DoctorRosterKey();
					entityKey.Load(values);
					item = DoctorRosterProvider.Get(GetTransactionManager(), entityKey);
					results = new TList<DoctorRoster>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case DoctorRosterSelectMethod.GetAll:
                    results = DoctorRosterProvider.GetAll(GetTransactionManager(), StartIndex, PageSize, out count);
                    break;
				case DoctorRosterSelectMethod.GetPaged:
					results = DoctorRosterProvider.GetPaged(GetTransactionManager(), WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case DoctorRosterSelectMethod.Find:
					if ( FilterParameters != null )
						results = DoctorRosterProvider.Find(GetTransactionManager(), FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = DoctorRosterProvider.Find(GetTransactionManager(), WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case DoctorRosterSelectMethod.GetById:
					_id = ( values["Id"] != null ) ? (System.String) EntityUtil.ChangeType(values["Id"], typeof(System.String)) : string.Empty;
					item = DoctorRosterProvider.GetById(GetTransactionManager(), _id);
					results = new TList<DoctorRoster>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				case DoctorRosterSelectMethod.GetByDoctorId:
					_doctorId = ( values["DoctorId"] != null ) ? (System.Int64) EntityUtil.ChangeType(values["DoctorId"], typeof(System.Int64)) : (long)0;
					results = DoctorRosterProvider.GetByDoctorId(GetTransactionManager(), _doctorId, this.StartIndex, this.PageSize, out count);
					break;
				case DoctorRosterSelectMethod.GetByDoctorIdIsComplete:
					_doctorId = ( values["DoctorId"] != null ) ? (System.Int64) EntityUtil.ChangeType(values["DoctorId"], typeof(System.Int64)) : (long)0;
					_isComplete = ( values["IsComplete"] != null ) ? (System.Boolean) EntityUtil.ChangeType(values["IsComplete"], typeof(System.Boolean)) : false;
					results = DoctorRosterProvider.GetByDoctorIdIsComplete(GetTransactionManager(), _doctorId, _isComplete, this.StartIndex, this.PageSize, out count);
					break;
				case DoctorRosterSelectMethod.GetByDoctorIdIsCompleteIsDisabled:
					_doctorId = ( values["DoctorId"] != null ) ? (System.Int64) EntityUtil.ChangeType(values["DoctorId"], typeof(System.Int64)) : (long)0;
					_isComplete = ( values["IsComplete"] != null ) ? (System.Boolean) EntityUtil.ChangeType(values["IsComplete"], typeof(System.Boolean)) : false;
					_isDisabled = ( values["IsDisabled"] != null ) ? (System.Boolean) EntityUtil.ChangeType(values["IsDisabled"], typeof(System.Boolean)) : false;
					results = DoctorRosterProvider.GetByDoctorIdIsCompleteIsDisabled(GetTransactionManager(), _doctorId, _isComplete, _isDisabled, this.StartIndex, this.PageSize, out count);
					break;
				case DoctorRosterSelectMethod.GetByDoctorIdRosterTypeId:
					_doctorId = ( values["DoctorId"] != null ) ? (System.Int64) EntityUtil.ChangeType(values["DoctorId"], typeof(System.Int64)) : (long)0;
					_rosterTypeId = ( values["RosterTypeId"] != null ) ? (System.Int64) EntityUtil.ChangeType(values["RosterTypeId"], typeof(System.Int64)) : (long)0;
					results = DoctorRosterProvider.GetByDoctorIdRosterTypeId(GetTransactionManager(), _doctorId, _rosterTypeId, this.StartIndex, this.PageSize, out count);
					break;
				case DoctorRosterSelectMethod.GetByDoctorIdRosterTypeIdIsComplete:
					_doctorId = ( values["DoctorId"] != null ) ? (System.Int64) EntityUtil.ChangeType(values["DoctorId"], typeof(System.Int64)) : (long)0;
					_rosterTypeId = ( values["RosterTypeId"] != null ) ? (System.Int64) EntityUtil.ChangeType(values["RosterTypeId"], typeof(System.Int64)) : (long)0;
					_isComplete = ( values["IsComplete"] != null ) ? (System.Boolean) EntityUtil.ChangeType(values["IsComplete"], typeof(System.Boolean)) : false;
					results = DoctorRosterProvider.GetByDoctorIdRosterTypeIdIsComplete(GetTransactionManager(), _doctorId, _rosterTypeId, _isComplete, this.StartIndex, this.PageSize, out count);
					break;
				case DoctorRosterSelectMethod.GetByDoctorIdRosterTypeIdIsCompleteIsDisabled:
					_doctorId = ( values["DoctorId"] != null ) ? (System.Int64) EntityUtil.ChangeType(values["DoctorId"], typeof(System.Int64)) : (long)0;
					_rosterTypeId = ( values["RosterTypeId"] != null ) ? (System.Int64) EntityUtil.ChangeType(values["RosterTypeId"], typeof(System.Int64)) : (long)0;
					_isComplete = ( values["IsComplete"] != null ) ? (System.Boolean) EntityUtil.ChangeType(values["IsComplete"], typeof(System.Boolean)) : false;
					_isDisabled = ( values["IsDisabled"] != null ) ? (System.Boolean) EntityUtil.ChangeType(values["IsDisabled"], typeof(System.Boolean)) : false;
					results = DoctorRosterProvider.GetByDoctorIdRosterTypeIdIsCompleteIsDisabled(GetTransactionManager(), _doctorId, _rosterTypeId, _isComplete, _isDisabled, this.StartIndex, this.PageSize, out count);
					break;
				case DoctorRosterSelectMethod.GetByIdIsComplete:
					_id = ( values["Id"] != null ) ? (System.String) EntityUtil.ChangeType(values["Id"], typeof(System.String)) : string.Empty;
					_isComplete = ( values["IsComplete"] != null ) ? (System.Boolean) EntityUtil.ChangeType(values["IsComplete"], typeof(System.Boolean)) : false;
					item = DoctorRosterProvider.GetByIdIsComplete(GetTransactionManager(), _id, _isComplete);
					results = new TList<DoctorRoster>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case DoctorRosterSelectMethod.GetByIdIsCompleteIsDisabled:
					_id = ( values["Id"] != null ) ? (System.String) EntityUtil.ChangeType(values["Id"], typeof(System.String)) : string.Empty;
					_isComplete = ( values["IsComplete"] != null ) ? (System.Boolean) EntityUtil.ChangeType(values["IsComplete"], typeof(System.Boolean)) : false;
					_isDisabled = ( values["IsDisabled"] != null ) ? (System.Boolean) EntityUtil.ChangeType(values["IsDisabled"], typeof(System.Boolean)) : false;
					item = DoctorRosterProvider.GetByIdIsCompleteIsDisabled(GetTransactionManager(), _id, _isComplete, _isDisabled);
					results = new TList<DoctorRoster>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case DoctorRosterSelectMethod.GetByIdIsDisabled:
					_id = ( values["Id"] != null ) ? (System.String) EntityUtil.ChangeType(values["Id"], typeof(System.String)) : string.Empty;
					_isDisabled = ( values["IsDisabled"] != null ) ? (System.Boolean) EntityUtil.ChangeType(values["IsDisabled"], typeof(System.Boolean)) : false;
					item = DoctorRosterProvider.GetByIdIsDisabled(GetTransactionManager(), _id, _isDisabled);
					results = new TList<DoctorRoster>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case DoctorRosterSelectMethod.GetByIsDisabled:
					_isDisabled = ( values["IsDisabled"] != null ) ? (System.Boolean) EntityUtil.ChangeType(values["IsDisabled"], typeof(System.Boolean)) : false;
					results = DoctorRosterProvider.GetByIsDisabled(GetTransactionManager(), _isDisabled, this.StartIndex, this.PageSize, out count);
					break;
				case DoctorRosterSelectMethod.GetByRosterTypeId:
					_rosterTypeId = ( values["RosterTypeId"] != null ) ? (System.Int64) EntityUtil.ChangeType(values["RosterTypeId"], typeof(System.Int64)) : (long)0;
					results = DoctorRosterProvider.GetByRosterTypeId(GetTransactionManager(), _rosterTypeId, this.StartIndex, this.PageSize, out count);
					break;
				case DoctorRosterSelectMethod.GetByRosterTypeIdIsComplete:
					_rosterTypeId = ( values["RosterTypeId"] != null ) ? (System.Int64) EntityUtil.ChangeType(values["RosterTypeId"], typeof(System.Int64)) : (long)0;
					_isComplete = ( values["IsComplete"] != null ) ? (System.Boolean) EntityUtil.ChangeType(values["IsComplete"], typeof(System.Boolean)) : false;
					results = DoctorRosterProvider.GetByRosterTypeIdIsComplete(GetTransactionManager(), _rosterTypeId, _isComplete, this.StartIndex, this.PageSize, out count);
					break;
				case DoctorRosterSelectMethod.GetByRosterTypeIdIsCompleteIsDisabled:
					_rosterTypeId = ( values["RosterTypeId"] != null ) ? (System.Int64) EntityUtil.ChangeType(values["RosterTypeId"], typeof(System.Int64)) : (long)0;
					_isComplete = ( values["IsComplete"] != null ) ? (System.Boolean) EntityUtil.ChangeType(values["IsComplete"], typeof(System.Boolean)) : false;
					_isDisabled = ( values["IsDisabled"] != null ) ? (System.Boolean) EntityUtil.ChangeType(values["IsDisabled"], typeof(System.Boolean)) : false;
					results = DoctorRosterProvider.GetByRosterTypeIdIsCompleteIsDisabled(GetTransactionManager(), _rosterTypeId, _isComplete, _isDisabled, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == DoctorRosterSelectMethod.Get || SelectMethod == DoctorRosterSelectMethod.GetById )
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
				DoctorRoster entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// execute deep load method
					DoctorRosterProvider.DeepLoad(GetTransactionManager(), GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<DoctorRoster> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// execute deep load method
			DoctorRosterProvider.DeepLoad(GetTransactionManager(), entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region DoctorRosterDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the DoctorRosterDataSource class.
	/// </summary>
	public class DoctorRosterDataSourceDesigner : ProviderDataSourceDesigner<DoctorRoster, DoctorRosterKey>
	{
		/// <summary>
		/// Initializes a new instance of the DoctorRosterDataSourceDesigner class.
		/// </summary>
		public DoctorRosterDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public DoctorRosterSelectMethod SelectMethod
		{
			get { return ((DoctorRosterDataSource) DataSource).SelectMethod; }
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
				actions.Add(new DoctorRosterDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region DoctorRosterDataSourceActionList

	/// <summary>
	/// Supports the DoctorRosterDataSourceDesigner class.
	/// </summary>
	internal class DoctorRosterDataSourceActionList : DesignerActionList
	{
		private DoctorRosterDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the DoctorRosterDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public DoctorRosterDataSourceActionList(DoctorRosterDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public DoctorRosterSelectMethod SelectMethod
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

	#endregion DoctorRosterDataSourceActionList
	
	#endregion DoctorRosterDataSourceDesigner
	
	#region DoctorRosterSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the DoctorRosterDataSource.SelectMethod property.
	/// </summary>
	public enum DoctorRosterSelectMethod
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
		/// Represents the GetByDoctorId method.
		/// </summary>
		GetByDoctorId,
		/// <summary>
		/// Represents the GetByDoctorIdIsComplete method.
		/// </summary>
		GetByDoctorIdIsComplete,
		/// <summary>
		/// Represents the GetByDoctorIdIsCompleteIsDisabled method.
		/// </summary>
		GetByDoctorIdIsCompleteIsDisabled,
		/// <summary>
		/// Represents the GetByDoctorIdRosterTypeId method.
		/// </summary>
		GetByDoctorIdRosterTypeId,
		/// <summary>
		/// Represents the GetByDoctorIdRosterTypeIdIsComplete method.
		/// </summary>
		GetByDoctorIdRosterTypeIdIsComplete,
		/// <summary>
		/// Represents the GetByDoctorIdRosterTypeIdIsCompleteIsDisabled method.
		/// </summary>
		GetByDoctorIdRosterTypeIdIsCompleteIsDisabled,
		/// <summary>
		/// Represents the GetByIdIsComplete method.
		/// </summary>
		GetByIdIsComplete,
		/// <summary>
		/// Represents the GetByIdIsCompleteIsDisabled method.
		/// </summary>
		GetByIdIsCompleteIsDisabled,
		/// <summary>
		/// Represents the GetByIdIsDisabled method.
		/// </summary>
		GetByIdIsDisabled,
		/// <summary>
		/// Represents the GetByIsDisabled method.
		/// </summary>
		GetByIsDisabled,
		/// <summary>
		/// Represents the GetByRosterTypeId method.
		/// </summary>
		GetByRosterTypeId,
		/// <summary>
		/// Represents the GetByRosterTypeIdIsComplete method.
		/// </summary>
		GetByRosterTypeIdIsComplete,
		/// <summary>
		/// Represents the GetByRosterTypeIdIsCompleteIsDisabled method.
		/// </summary>
		GetByRosterTypeIdIsCompleteIsDisabled,
		/// <summary>
		/// Represents the GetById method.
		/// </summary>
		GetById
	}
	
	#endregion DoctorRosterSelectMethod

	#region DoctorRosterFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="DoctorRoster"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class DoctorRosterFilter : SqlFilter<DoctorRosterColumn>
	{
	}
	
	#endregion DoctorRosterFilter

	#region DoctorRosterExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="DoctorRoster"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class DoctorRosterExpressionBuilder : SqlExpressionBuilder<DoctorRosterColumn>
	{
	}
	
	#endregion DoctorRosterExpressionBuilder	

	#region DoctorRosterProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;DoctorRosterChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="DoctorRoster"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class DoctorRosterProperty : ChildEntityProperty<DoctorRosterChildEntityTypes>
	{
	}
	
	#endregion DoctorRosterProperty
}

