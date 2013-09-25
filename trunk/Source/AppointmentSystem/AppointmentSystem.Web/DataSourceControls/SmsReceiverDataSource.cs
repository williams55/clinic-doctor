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
	/// Represents the DataRepository.SmsReceiverProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(SmsReceiverDataSourceDesigner))]
	public class SmsReceiverDataSource : ProviderDataSource<SmsReceiver, SmsReceiverKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SmsReceiverDataSource class.
		/// </summary>
		public SmsReceiverDataSource() : base(DataRepository.SmsReceiverProvider)
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the SmsReceiverDataSourceView used by the SmsReceiverDataSource.
		/// </summary>
		protected SmsReceiverDataSourceView SmsReceiverView
		{
			get { return ( View as SmsReceiverDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the SmsReceiverDataSource control invokes to retrieve data.
		/// </summary>
		public SmsReceiverSelectMethod SelectMethod
		{
			get
			{
				SmsReceiverSelectMethod selectMethod = SmsReceiverSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (SmsReceiverSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the SmsReceiverDataSourceView class that is to be
		/// used by the SmsReceiverDataSource.
		/// </summary>
		/// <returns>An instance of the SmsReceiverDataSourceView class.</returns>
		protected override BaseDataSourceView<SmsReceiver, SmsReceiverKey> GetNewDataSourceView()
		{
			return new SmsReceiverDataSourceView(this, DefaultViewName);
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
	/// Supports the SmsReceiverDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class SmsReceiverDataSourceView : ProviderDataSourceView<SmsReceiver, SmsReceiverKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SmsReceiverDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the SmsReceiverDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public SmsReceiverDataSourceView(SmsReceiverDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal SmsReceiverDataSource SmsReceiverOwner
		{
			get { return Owner as SmsReceiverDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal SmsReceiverSelectMethod SelectMethod
		{
			get { return SmsReceiverOwner.SelectMethod; }
			set { SmsReceiverOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal SmsReceiverProviderBase SmsReceiverProvider
		{
			get { return Provider as SmsReceiverProviderBase; }
		}

		#endregion Properties
		
		#region Methods
		
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<SmsReceiver> GetSelectData(out int count)
		{
			Hashtable values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<SmsReceiver> results = null;
			SmsReceiver item;
			count = 0;
			
			System.Int64 _id;
			System.Int64 _smsId;

			switch ( SelectMethod )
			{
				case SmsReceiverSelectMethod.Get:
					SmsReceiverKey entityKey  = new SmsReceiverKey();
					entityKey.Load(values);
					item = SmsReceiverProvider.Get(GetTransactionManager(), entityKey);
					results = new TList<SmsReceiver>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case SmsReceiverSelectMethod.GetAll:
                    results = SmsReceiverProvider.GetAll(GetTransactionManager(), StartIndex, PageSize, out count);
                    break;
				case SmsReceiverSelectMethod.GetPaged:
					results = SmsReceiverProvider.GetPaged(GetTransactionManager(), WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case SmsReceiverSelectMethod.Find:
					if ( FilterParameters != null )
						results = SmsReceiverProvider.Find(GetTransactionManager(), FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = SmsReceiverProvider.Find(GetTransactionManager(), WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case SmsReceiverSelectMethod.GetById:
					_id = ( values["Id"] != null ) ? (System.Int64) EntityUtil.ChangeType(values["Id"], typeof(System.Int64)) : (long)0;
					item = SmsReceiverProvider.GetById(GetTransactionManager(), _id);
					results = new TList<SmsReceiver>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				case SmsReceiverSelectMethod.GetBySmsId:
					_smsId = ( values["SmsId"] != null ) ? (System.Int64) EntityUtil.ChangeType(values["SmsId"], typeof(System.Int64)) : (long)0;
					results = SmsReceiverProvider.GetBySmsId(GetTransactionManager(), _smsId, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == SmsReceiverSelectMethod.Get || SelectMethod == SmsReceiverSelectMethod.GetById )
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
				SmsReceiver entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// execute deep load method
					SmsReceiverProvider.DeepLoad(GetTransactionManager(), GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<SmsReceiver> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// execute deep load method
			SmsReceiverProvider.DeepLoad(GetTransactionManager(), entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region SmsReceiverDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the SmsReceiverDataSource class.
	/// </summary>
	public class SmsReceiverDataSourceDesigner : ProviderDataSourceDesigner<SmsReceiver, SmsReceiverKey>
	{
		/// <summary>
		/// Initializes a new instance of the SmsReceiverDataSourceDesigner class.
		/// </summary>
		public SmsReceiverDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public SmsReceiverSelectMethod SelectMethod
		{
			get { return ((SmsReceiverDataSource) DataSource).SelectMethod; }
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
				actions.Add(new SmsReceiverDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region SmsReceiverDataSourceActionList

	/// <summary>
	/// Supports the SmsReceiverDataSourceDesigner class.
	/// </summary>
	internal class SmsReceiverDataSourceActionList : DesignerActionList
	{
		private SmsReceiverDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the SmsReceiverDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public SmsReceiverDataSourceActionList(SmsReceiverDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public SmsReceiverSelectMethod SelectMethod
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

	#endregion SmsReceiverDataSourceActionList
	
	#endregion SmsReceiverDataSourceDesigner
	
	#region SmsReceiverSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the SmsReceiverDataSource.SelectMethod property.
	/// </summary>
	public enum SmsReceiverSelectMethod
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
		/// Represents the GetBySmsId method.
		/// </summary>
		GetBySmsId
	}
	
	#endregion SmsReceiverSelectMethod

	#region SmsReceiverFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="SmsReceiver"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class SmsReceiverFilter : SqlFilter<SmsReceiverColumn>
	{
	}
	
	#endregion SmsReceiverFilter

	#region SmsReceiverExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="SmsReceiver"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class SmsReceiverExpressionBuilder : SqlExpressionBuilder<SmsReceiverColumn>
	{
	}
	
	#endregion SmsReceiverExpressionBuilder	

	#region SmsReceiverProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;SmsReceiverChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="SmsReceiver"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class SmsReceiverProperty : ChildEntityProperty<SmsReceiverChildEntityTypes>
	{
	}
	
	#endregion SmsReceiverProperty
}

