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
	/// Represents the DataRepository.NurseAppointmentProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(NurseAppointmentDataSourceDesigner))]
	public class NurseAppointmentDataSource : ProviderDataSource<NurseAppointment, NurseAppointmentKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the NurseAppointmentDataSource class.
		/// </summary>
		public NurseAppointmentDataSource() : base(DataRepository.NurseAppointmentProvider)
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the NurseAppointmentDataSourceView used by the NurseAppointmentDataSource.
		/// </summary>
		protected NurseAppointmentDataSourceView NurseAppointmentView
		{
			get { return ( View as NurseAppointmentDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the NurseAppointmentDataSource control invokes to retrieve data.
		/// </summary>
		public NurseAppointmentSelectMethod SelectMethod
		{
			get
			{
				NurseAppointmentSelectMethod selectMethod = NurseAppointmentSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (NurseAppointmentSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the NurseAppointmentDataSourceView class that is to be
		/// used by the NurseAppointmentDataSource.
		/// </summary>
		/// <returns>An instance of the NurseAppointmentDataSourceView class.</returns>
		protected override BaseDataSourceView<NurseAppointment, NurseAppointmentKey> GetNewDataSourceView()
		{
			return new NurseAppointmentDataSourceView(this, DefaultViewName);
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
	/// Supports the NurseAppointmentDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class NurseAppointmentDataSourceView : ProviderDataSourceView<NurseAppointment, NurseAppointmentKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the NurseAppointmentDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the NurseAppointmentDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public NurseAppointmentDataSourceView(NurseAppointmentDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal NurseAppointmentDataSource NurseAppointmentOwner
		{
			get { return Owner as NurseAppointmentDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal NurseAppointmentSelectMethod SelectMethod
		{
			get { return NurseAppointmentOwner.SelectMethod; }
			set { NurseAppointmentOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal NurseAppointmentProviderBase NurseAppointmentProvider
		{
			get { return Provider as NurseAppointmentProviderBase; }
		}

		#endregion Properties
		
		#region Methods
		
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<NurseAppointment> GetSelectData(out int count)
		{
			Hashtable values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<NurseAppointment> results = null;
			NurseAppointment item;
			count = 0;
			
			System.Int32? _appointmentId_nullable;
			System.Boolean? _isDisabled_nullable;
			System.Int32? _nurseId_nullable;
			System.Int32 _id;

			switch ( SelectMethod )
			{
				case NurseAppointmentSelectMethod.Get:
					NurseAppointmentKey entityKey  = new NurseAppointmentKey();
					entityKey.Load(values);
					item = NurseAppointmentProvider.Get(GetTransactionManager(), entityKey);
					results = new TList<NurseAppointment>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case NurseAppointmentSelectMethod.GetAll:
                    results = NurseAppointmentProvider.GetAll(GetTransactionManager(), StartIndex, PageSize, out count);
                    break;
				case NurseAppointmentSelectMethod.GetPaged:
					results = NurseAppointmentProvider.GetPaged(GetTransactionManager(), WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case NurseAppointmentSelectMethod.Find:
					if ( FilterParameters != null )
						results = NurseAppointmentProvider.Find(GetTransactionManager(), FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = NurseAppointmentProvider.Find(GetTransactionManager(), WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case NurseAppointmentSelectMethod.GetById:
					_id = ( values["Id"] != null ) ? (System.Int32) EntityUtil.ChangeType(values["Id"], typeof(System.Int32)) : (int)0;
					item = NurseAppointmentProvider.GetById(GetTransactionManager(), _id);
					results = new TList<NurseAppointment>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				case NurseAppointmentSelectMethod.GetByAppointmentId:
					_appointmentId_nullable = (System.Int32?) EntityUtil.ChangeType(values["AppointmentId"], typeof(System.Int32?));
					results = NurseAppointmentProvider.GetByAppointmentId(GetTransactionManager(), _appointmentId_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case NurseAppointmentSelectMethod.GetByAppointmentIdIsDisabled:
					_appointmentId_nullable = (System.Int32?) EntityUtil.ChangeType(values["AppointmentId"], typeof(System.Int32?));
					_isDisabled_nullable = (System.Boolean?) EntityUtil.ChangeType(values["IsDisabled"], typeof(System.Boolean?));
					results = NurseAppointmentProvider.GetByAppointmentIdIsDisabled(GetTransactionManager(), _appointmentId_nullable, _isDisabled_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case NurseAppointmentSelectMethod.GetByAppointmentIdNurseId:
					_appointmentId_nullable = (System.Int32?) EntityUtil.ChangeType(values["AppointmentId"], typeof(System.Int32?));
					_nurseId_nullable = (System.Int32?) EntityUtil.ChangeType(values["NurseId"], typeof(System.Int32?));
					results = NurseAppointmentProvider.GetByAppointmentIdNurseId(GetTransactionManager(), _appointmentId_nullable, _nurseId_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case NurseAppointmentSelectMethod.GetByAppointmentIdNurseIdIsDisabled:
					_appointmentId_nullable = (System.Int32?) EntityUtil.ChangeType(values["AppointmentId"], typeof(System.Int32?));
					_nurseId_nullable = (System.Int32?) EntityUtil.ChangeType(values["NurseId"], typeof(System.Int32?));
					_isDisabled_nullable = (System.Boolean?) EntityUtil.ChangeType(values["IsDisabled"], typeof(System.Boolean?));
					results = NurseAppointmentProvider.GetByAppointmentIdNurseIdIsDisabled(GetTransactionManager(), _appointmentId_nullable, _nurseId_nullable, _isDisabled_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case NurseAppointmentSelectMethod.GetByIdIsDisabled:
					_id = ( values["Id"] != null ) ? (System.Int32) EntityUtil.ChangeType(values["Id"], typeof(System.Int32)) : (int)0;
					_isDisabled_nullable = (System.Boolean?) EntityUtil.ChangeType(values["IsDisabled"], typeof(System.Boolean?));
					results = NurseAppointmentProvider.GetByIdIsDisabled(GetTransactionManager(), _id, _isDisabled_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case NurseAppointmentSelectMethod.GetByIsDisabled:
					_isDisabled_nullable = (System.Boolean?) EntityUtil.ChangeType(values["IsDisabled"], typeof(System.Boolean?));
					results = NurseAppointmentProvider.GetByIsDisabled(GetTransactionManager(), _isDisabled_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case NurseAppointmentSelectMethod.GetByNurseId:
					_nurseId_nullable = (System.Int32?) EntityUtil.ChangeType(values["NurseId"], typeof(System.Int32?));
					results = NurseAppointmentProvider.GetByNurseId(GetTransactionManager(), _nurseId_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case NurseAppointmentSelectMethod.GetByNurseIdIsDisabled:
					_nurseId_nullable = (System.Int32?) EntityUtil.ChangeType(values["NurseId"], typeof(System.Int32?));
					_isDisabled_nullable = (System.Boolean?) EntityUtil.ChangeType(values["IsDisabled"], typeof(System.Boolean?));
					results = NurseAppointmentProvider.GetByNurseIdIsDisabled(GetTransactionManager(), _nurseId_nullable, _isDisabled_nullable, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == NurseAppointmentSelectMethod.Get || SelectMethod == NurseAppointmentSelectMethod.GetById )
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
				NurseAppointment entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// execute deep load method
					NurseAppointmentProvider.DeepLoad(GetTransactionManager(), GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<NurseAppointment> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// execute deep load method
			NurseAppointmentProvider.DeepLoad(GetTransactionManager(), entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region NurseAppointmentDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the NurseAppointmentDataSource class.
	/// </summary>
	public class NurseAppointmentDataSourceDesigner : ProviderDataSourceDesigner<NurseAppointment, NurseAppointmentKey>
	{
		/// <summary>
		/// Initializes a new instance of the NurseAppointmentDataSourceDesigner class.
		/// </summary>
		public NurseAppointmentDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public NurseAppointmentSelectMethod SelectMethod
		{
			get { return ((NurseAppointmentDataSource) DataSource).SelectMethod; }
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
				actions.Add(new NurseAppointmentDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region NurseAppointmentDataSourceActionList

	/// <summary>
	/// Supports the NurseAppointmentDataSourceDesigner class.
	/// </summary>
	internal class NurseAppointmentDataSourceActionList : DesignerActionList
	{
		private NurseAppointmentDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the NurseAppointmentDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public NurseAppointmentDataSourceActionList(NurseAppointmentDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public NurseAppointmentSelectMethod SelectMethod
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

	#endregion NurseAppointmentDataSourceActionList
	
	#endregion NurseAppointmentDataSourceDesigner
	
	#region NurseAppointmentSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the NurseAppointmentDataSource.SelectMethod property.
	/// </summary>
	public enum NurseAppointmentSelectMethod
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
		/// Represents the GetByAppointmentId method.
		/// </summary>
		GetByAppointmentId,
		/// <summary>
		/// Represents the GetByAppointmentIdIsDisabled method.
		/// </summary>
		GetByAppointmentIdIsDisabled,
		/// <summary>
		/// Represents the GetByAppointmentIdNurseId method.
		/// </summary>
		GetByAppointmentIdNurseId,
		/// <summary>
		/// Represents the GetByAppointmentIdNurseIdIsDisabled method.
		/// </summary>
		GetByAppointmentIdNurseIdIsDisabled,
		/// <summary>
		/// Represents the GetByIdIsDisabled method.
		/// </summary>
		GetByIdIsDisabled,
		/// <summary>
		/// Represents the GetByIsDisabled method.
		/// </summary>
		GetByIsDisabled,
		/// <summary>
		/// Represents the GetByNurseId method.
		/// </summary>
		GetByNurseId,
		/// <summary>
		/// Represents the GetByNurseIdIsDisabled method.
		/// </summary>
		GetByNurseIdIsDisabled,
		/// <summary>
		/// Represents the GetById method.
		/// </summary>
		GetById
	}
	
	#endregion NurseAppointmentSelectMethod

	#region NurseAppointmentFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="NurseAppointment"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class NurseAppointmentFilter : SqlFilter<NurseAppointmentColumn>
	{
	}
	
	#endregion NurseAppointmentFilter

	#region NurseAppointmentExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="NurseAppointment"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class NurseAppointmentExpressionBuilder : SqlExpressionBuilder<NurseAppointmentColumn>
	{
	}
	
	#endregion NurseAppointmentExpressionBuilder	

	#region NurseAppointmentProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;NurseAppointmentChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="NurseAppointment"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class NurseAppointmentProperty : ChildEntityProperty<NurseAppointmentChildEntityTypes>
	{
	}
	
	#endregion NurseAppointmentProperty
}

