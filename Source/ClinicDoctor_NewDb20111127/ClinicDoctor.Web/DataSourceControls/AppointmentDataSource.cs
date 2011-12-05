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
	/// Represents the DataRepository.AppointmentProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(AppointmentDataSourceDesigner))]
	public class AppointmentDataSource : ProviderDataSource<Appointment, AppointmentKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the AppointmentDataSource class.
		/// </summary>
		public AppointmentDataSource() : base(DataRepository.AppointmentProvider)
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the AppointmentDataSourceView used by the AppointmentDataSource.
		/// </summary>
		protected AppointmentDataSourceView AppointmentView
		{
			get { return ( View as AppointmentDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the AppointmentDataSource control invokes to retrieve data.
		/// </summary>
		public AppointmentSelectMethod SelectMethod
		{
			get
			{
				AppointmentSelectMethod selectMethod = AppointmentSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (AppointmentSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the AppointmentDataSourceView class that is to be
		/// used by the AppointmentDataSource.
		/// </summary>
		/// <returns>An instance of the AppointmentDataSourceView class.</returns>
		protected override BaseDataSourceView<Appointment, AppointmentKey> GetNewDataSourceView()
		{
			return new AppointmentDataSourceView(this, DefaultViewName);
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
	/// Supports the AppointmentDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class AppointmentDataSourceView : ProviderDataSourceView<Appointment, AppointmentKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the AppointmentDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the AppointmentDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public AppointmentDataSourceView(AppointmentDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal AppointmentDataSource AppointmentOwner
		{
			get { return Owner as AppointmentDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal AppointmentSelectMethod SelectMethod
		{
			get { return AppointmentOwner.SelectMethod; }
			set { AppointmentOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal AppointmentProviderBase AppointmentProvider
		{
			get { return Provider as AppointmentProviderBase; }
		}

		#endregion Properties
		
		#region Methods
		
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<Appointment> GetSelectData(out int count)
		{
			Hashtable values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<Appointment> results = null;
			Appointment item;
			count = 0;
			
			System.Int64 _contentId;
			System.Boolean _isDisabled;
			System.Int64 _customerId;
			System.Boolean _isComplete;
			System.String _doctorUsername_nullable;
			System.Int64? _statusId_nullable;
			System.String _id;
			System.Int64? _roomId_nullable;

			switch ( SelectMethod )
			{
				case AppointmentSelectMethod.Get:
					AppointmentKey entityKey  = new AppointmentKey();
					entityKey.Load(values);
					item = AppointmentProvider.Get(GetTransactionManager(), entityKey);
					results = new TList<Appointment>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case AppointmentSelectMethod.GetAll:
                    results = AppointmentProvider.GetAll(GetTransactionManager(), StartIndex, PageSize, out count);
                    break;
				case AppointmentSelectMethod.GetPaged:
					results = AppointmentProvider.GetPaged(GetTransactionManager(), WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case AppointmentSelectMethod.Find:
					if ( FilterParameters != null )
						results = AppointmentProvider.Find(GetTransactionManager(), FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = AppointmentProvider.Find(GetTransactionManager(), WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case AppointmentSelectMethod.GetById:
					_id = ( values["Id"] != null ) ? (System.String) EntityUtil.ChangeType(values["Id"], typeof(System.String)) : string.Empty;
					item = AppointmentProvider.GetById(GetTransactionManager(), _id);
					results = new TList<Appointment>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				case AppointmentSelectMethod.GetByContentId:
					_contentId = ( values["ContentId"] != null ) ? (System.Int64) EntityUtil.ChangeType(values["ContentId"], typeof(System.Int64)) : (long)0;
					results = AppointmentProvider.GetByContentId(GetTransactionManager(), _contentId, this.StartIndex, this.PageSize, out count);
					break;
				case AppointmentSelectMethod.GetByContentIdIsDisabled:
					_contentId = ( values["ContentId"] != null ) ? (System.Int64) EntityUtil.ChangeType(values["ContentId"], typeof(System.Int64)) : (long)0;
					_isDisabled = ( values["IsDisabled"] != null ) ? (System.Boolean) EntityUtil.ChangeType(values["IsDisabled"], typeof(System.Boolean)) : false;
					results = AppointmentProvider.GetByContentIdIsDisabled(GetTransactionManager(), _contentId, _isDisabled, this.StartIndex, this.PageSize, out count);
					break;
				case AppointmentSelectMethod.GetByCustomerId:
					_customerId = ( values["CustomerId"] != null ) ? (System.Int64) EntityUtil.ChangeType(values["CustomerId"], typeof(System.Int64)) : (long)0;
					results = AppointmentProvider.GetByCustomerId(GetTransactionManager(), _customerId, this.StartIndex, this.PageSize, out count);
					break;
				case AppointmentSelectMethod.GetByCustomerIdContentId:
					_customerId = ( values["CustomerId"] != null ) ? (System.Int64) EntityUtil.ChangeType(values["CustomerId"], typeof(System.Int64)) : (long)0;
					_contentId = ( values["ContentId"] != null ) ? (System.Int64) EntityUtil.ChangeType(values["ContentId"], typeof(System.Int64)) : (long)0;
					results = AppointmentProvider.GetByCustomerIdContentId(GetTransactionManager(), _customerId, _contentId, this.StartIndex, this.PageSize, out count);
					break;
				case AppointmentSelectMethod.GetByCustomerIdContentIdIsComplete:
					_customerId = ( values["CustomerId"] != null ) ? (System.Int64) EntityUtil.ChangeType(values["CustomerId"], typeof(System.Int64)) : (long)0;
					_contentId = ( values["ContentId"] != null ) ? (System.Int64) EntityUtil.ChangeType(values["ContentId"], typeof(System.Int64)) : (long)0;
					_isComplete = ( values["IsComplete"] != null ) ? (System.Boolean) EntityUtil.ChangeType(values["IsComplete"], typeof(System.Boolean)) : false;
					results = AppointmentProvider.GetByCustomerIdContentIdIsComplete(GetTransactionManager(), _customerId, _contentId, _isComplete, this.StartIndex, this.PageSize, out count);
					break;
				case AppointmentSelectMethod.GetByCustomerIdContentIdIsCompleteIsDisabled:
					_customerId = ( values["CustomerId"] != null ) ? (System.Int64) EntityUtil.ChangeType(values["CustomerId"], typeof(System.Int64)) : (long)0;
					_contentId = ( values["ContentId"] != null ) ? (System.Int64) EntityUtil.ChangeType(values["ContentId"], typeof(System.Int64)) : (long)0;
					_isComplete = ( values["IsComplete"] != null ) ? (System.Boolean) EntityUtil.ChangeType(values["IsComplete"], typeof(System.Boolean)) : false;
					_isDisabled = ( values["IsDisabled"] != null ) ? (System.Boolean) EntityUtil.ChangeType(values["IsDisabled"], typeof(System.Boolean)) : false;
					results = AppointmentProvider.GetByCustomerIdContentIdIsCompleteIsDisabled(GetTransactionManager(), _customerId, _contentId, _isComplete, _isDisabled, this.StartIndex, this.PageSize, out count);
					break;
				case AppointmentSelectMethod.GetByCustomerIdIsDisabled:
					_customerId = ( values["CustomerId"] != null ) ? (System.Int64) EntityUtil.ChangeType(values["CustomerId"], typeof(System.Int64)) : (long)0;
					_isDisabled = ( values["IsDisabled"] != null ) ? (System.Boolean) EntityUtil.ChangeType(values["IsDisabled"], typeof(System.Boolean)) : false;
					results = AppointmentProvider.GetByCustomerIdIsDisabled(GetTransactionManager(), _customerId, _isDisabled, this.StartIndex, this.PageSize, out count);
					break;
				case AppointmentSelectMethod.GetByDoctorUsername:
					_doctorUsername_nullable = (System.String) EntityUtil.ChangeType(values["DoctorUsername"], typeof(System.String));
					results = AppointmentProvider.GetByDoctorUsername(GetTransactionManager(), _doctorUsername_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case AppointmentSelectMethod.GetByDoctorUsernameIsCompleteIsDisabled:
					_doctorUsername_nullable = (System.String) EntityUtil.ChangeType(values["DoctorUsername"], typeof(System.String));
					_isComplete = ( values["IsComplete"] != null ) ? (System.Boolean) EntityUtil.ChangeType(values["IsComplete"], typeof(System.Boolean)) : false;
					_isDisabled = ( values["IsDisabled"] != null ) ? (System.Boolean) EntityUtil.ChangeType(values["IsDisabled"], typeof(System.Boolean)) : false;
					results = AppointmentProvider.GetByDoctorUsernameIsCompleteIsDisabled(GetTransactionManager(), _doctorUsername_nullable, _isComplete, _isDisabled, this.StartIndex, this.PageSize, out count);
					break;
				case AppointmentSelectMethod.GetByDoctorUsernameIsDisabled:
					_doctorUsername_nullable = (System.String) EntityUtil.ChangeType(values["DoctorUsername"], typeof(System.String));
					_isDisabled = ( values["IsDisabled"] != null ) ? (System.Boolean) EntityUtil.ChangeType(values["IsDisabled"], typeof(System.Boolean)) : false;
					results = AppointmentProvider.GetByDoctorUsernameIsDisabled(GetTransactionManager(), _doctorUsername_nullable, _isDisabled, this.StartIndex, this.PageSize, out count);
					break;
				case AppointmentSelectMethod.GetByDoctorUsernameStatusId:
					_doctorUsername_nullable = (System.String) EntityUtil.ChangeType(values["DoctorUsername"], typeof(System.String));
					_statusId_nullable = (System.Int64?) EntityUtil.ChangeType(values["StatusId"], typeof(System.Int64?));
					results = AppointmentProvider.GetByDoctorUsernameStatusId(GetTransactionManager(), _doctorUsername_nullable, _statusId_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case AppointmentSelectMethod.GetByDoctorUsernameStatusIdIsComplete:
					_doctorUsername_nullable = (System.String) EntityUtil.ChangeType(values["DoctorUsername"], typeof(System.String));
					_statusId_nullable = (System.Int64?) EntityUtil.ChangeType(values["StatusId"], typeof(System.Int64?));
					_isComplete = ( values["IsComplete"] != null ) ? (System.Boolean) EntityUtil.ChangeType(values["IsComplete"], typeof(System.Boolean)) : false;
					results = AppointmentProvider.GetByDoctorUsernameStatusIdIsComplete(GetTransactionManager(), _doctorUsername_nullable, _statusId_nullable, _isComplete, this.StartIndex, this.PageSize, out count);
					break;
				case AppointmentSelectMethod.GetByDoctorUsernameStatusIdIsCompleteIsDisabled:
					_doctorUsername_nullable = (System.String) EntityUtil.ChangeType(values["DoctorUsername"], typeof(System.String));
					_statusId_nullable = (System.Int64?) EntityUtil.ChangeType(values["StatusId"], typeof(System.Int64?));
					_isComplete = ( values["IsComplete"] != null ) ? (System.Boolean) EntityUtil.ChangeType(values["IsComplete"], typeof(System.Boolean)) : false;
					_isDisabled = ( values["IsDisabled"] != null ) ? (System.Boolean) EntityUtil.ChangeType(values["IsDisabled"], typeof(System.Boolean)) : false;
					results = AppointmentProvider.GetByDoctorUsernameStatusIdIsCompleteIsDisabled(GetTransactionManager(), _doctorUsername_nullable, _statusId_nullable, _isComplete, _isDisabled, this.StartIndex, this.PageSize, out count);
					break;
				case AppointmentSelectMethod.GetByIdIsDisabled:
					_id = ( values["Id"] != null ) ? (System.String) EntityUtil.ChangeType(values["Id"], typeof(System.String)) : string.Empty;
					_isDisabled = ( values["IsDisabled"] != null ) ? (System.Boolean) EntityUtil.ChangeType(values["IsDisabled"], typeof(System.Boolean)) : false;
					item = AppointmentProvider.GetByIdIsDisabled(GetTransactionManager(), _id, _isDisabled);
					results = new TList<Appointment>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case AppointmentSelectMethod.GetByIsDisabled:
					_isDisabled = ( values["IsDisabled"] != null ) ? (System.Boolean) EntityUtil.ChangeType(values["IsDisabled"], typeof(System.Boolean)) : false;
					results = AppointmentProvider.GetByIsDisabled(GetTransactionManager(), _isDisabled, this.StartIndex, this.PageSize, out count);
					break;
				case AppointmentSelectMethod.GetByRoomId:
					_roomId_nullable = (System.Int64?) EntityUtil.ChangeType(values["RoomId"], typeof(System.Int64?));
					results = AppointmentProvider.GetByRoomId(GetTransactionManager(), _roomId_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case AppointmentSelectMethod.GetByRoomIdIsDisabled:
					_roomId_nullable = (System.Int64?) EntityUtil.ChangeType(values["RoomId"], typeof(System.Int64?));
					_isDisabled = ( values["IsDisabled"] != null ) ? (System.Boolean) EntityUtil.ChangeType(values["IsDisabled"], typeof(System.Boolean)) : false;
					results = AppointmentProvider.GetByRoomIdIsDisabled(GetTransactionManager(), _roomId_nullable, _isDisabled, this.StartIndex, this.PageSize, out count);
					break;
				case AppointmentSelectMethod.GetByStatusId:
					_statusId_nullable = (System.Int64?) EntityUtil.ChangeType(values["StatusId"], typeof(System.Int64?));
					results = AppointmentProvider.GetByStatusId(GetTransactionManager(), _statusId_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case AppointmentSelectMethod.GetByStatusIdIsDisabled:
					_statusId_nullable = (System.Int64?) EntityUtil.ChangeType(values["StatusId"], typeof(System.Int64?));
					_isDisabled = ( values["IsDisabled"] != null ) ? (System.Boolean) EntityUtil.ChangeType(values["IsDisabled"], typeof(System.Boolean)) : false;
					results = AppointmentProvider.GetByStatusIdIsDisabled(GetTransactionManager(), _statusId_nullable, _isDisabled, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == AppointmentSelectMethod.Get || SelectMethod == AppointmentSelectMethod.GetById )
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
				Appointment entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// execute deep load method
					AppointmentProvider.DeepLoad(GetTransactionManager(), GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<Appointment> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// execute deep load method
			AppointmentProvider.DeepLoad(GetTransactionManager(), entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region AppointmentDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the AppointmentDataSource class.
	/// </summary>
	public class AppointmentDataSourceDesigner : ProviderDataSourceDesigner<Appointment, AppointmentKey>
	{
		/// <summary>
		/// Initializes a new instance of the AppointmentDataSourceDesigner class.
		/// </summary>
		public AppointmentDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public AppointmentSelectMethod SelectMethod
		{
			get { return ((AppointmentDataSource) DataSource).SelectMethod; }
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
				actions.Add(new AppointmentDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region AppointmentDataSourceActionList

	/// <summary>
	/// Supports the AppointmentDataSourceDesigner class.
	/// </summary>
	internal class AppointmentDataSourceActionList : DesignerActionList
	{
		private AppointmentDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the AppointmentDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public AppointmentDataSourceActionList(AppointmentDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public AppointmentSelectMethod SelectMethod
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

	#endregion AppointmentDataSourceActionList
	
	#endregion AppointmentDataSourceDesigner
	
	#region AppointmentSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the AppointmentDataSource.SelectMethod property.
	/// </summary>
	public enum AppointmentSelectMethod
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
		/// Represents the GetByContentId method.
		/// </summary>
		GetByContentId,
		/// <summary>
		/// Represents the GetByContentIdIsDisabled method.
		/// </summary>
		GetByContentIdIsDisabled,
		/// <summary>
		/// Represents the GetByCustomerId method.
		/// </summary>
		GetByCustomerId,
		/// <summary>
		/// Represents the GetByCustomerIdContentId method.
		/// </summary>
		GetByCustomerIdContentId,
		/// <summary>
		/// Represents the GetByCustomerIdContentIdIsComplete method.
		/// </summary>
		GetByCustomerIdContentIdIsComplete,
		/// <summary>
		/// Represents the GetByCustomerIdContentIdIsCompleteIsDisabled method.
		/// </summary>
		GetByCustomerIdContentIdIsCompleteIsDisabled,
		/// <summary>
		/// Represents the GetByCustomerIdIsDisabled method.
		/// </summary>
		GetByCustomerIdIsDisabled,
		/// <summary>
		/// Represents the GetByDoctorUsername method.
		/// </summary>
		GetByDoctorUsername,
		/// <summary>
		/// Represents the GetByDoctorUsernameIsCompleteIsDisabled method.
		/// </summary>
		GetByDoctorUsernameIsCompleteIsDisabled,
		/// <summary>
		/// Represents the GetByDoctorUsernameIsDisabled method.
		/// </summary>
		GetByDoctorUsernameIsDisabled,
		/// <summary>
		/// Represents the GetByDoctorUsernameStatusId method.
		/// </summary>
		GetByDoctorUsernameStatusId,
		/// <summary>
		/// Represents the GetByDoctorUsernameStatusIdIsComplete method.
		/// </summary>
		GetByDoctorUsernameStatusIdIsComplete,
		/// <summary>
		/// Represents the GetByDoctorUsernameStatusIdIsCompleteIsDisabled method.
		/// </summary>
		GetByDoctorUsernameStatusIdIsCompleteIsDisabled,
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
		/// Represents the GetByRoomIdIsDisabled method.
		/// </summary>
		GetByRoomIdIsDisabled,
		/// <summary>
		/// Represents the GetByStatusId method.
		/// </summary>
		GetByStatusId,
		/// <summary>
		/// Represents the GetByStatusIdIsDisabled method.
		/// </summary>
		GetByStatusIdIsDisabled,
		/// <summary>
		/// Represents the GetById method.
		/// </summary>
		GetById
	}
	
	#endregion AppointmentSelectMethod

	#region AppointmentFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Appointment"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class AppointmentFilter : SqlFilter<AppointmentColumn>
	{
	}
	
	#endregion AppointmentFilter

	#region AppointmentExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Appointment"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class AppointmentExpressionBuilder : SqlExpressionBuilder<AppointmentColumn>
	{
	}
	
	#endregion AppointmentExpressionBuilder	

	#region AppointmentProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;AppointmentChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="Appointment"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class AppointmentProperty : ChildEntityProperty<AppointmentChildEntityTypes>
	{
	}
	
	#endregion AppointmentProperty
}

