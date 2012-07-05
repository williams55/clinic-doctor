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
	/// Represents the DataRepository.PatientProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(PatientDataSourceDesigner))]
	public class PatientDataSource : ProviderDataSource<Patient, PatientKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PatientDataSource class.
		/// </summary>
		public PatientDataSource() : base(DataRepository.PatientProvider)
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the PatientDataSourceView used by the PatientDataSource.
		/// </summary>
		protected PatientDataSourceView PatientView
		{
			get { return ( View as PatientDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the PatientDataSource control invokes to retrieve data.
		/// </summary>
		public PatientSelectMethod SelectMethod
		{
			get
			{
				PatientSelectMethod selectMethod = PatientSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (PatientSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the PatientDataSourceView class that is to be
		/// used by the PatientDataSource.
		/// </summary>
		/// <returns>An instance of the PatientDataSourceView class.</returns>
		protected override BaseDataSourceView<Patient, PatientKey> GetNewDataSourceView()
		{
			return new PatientDataSourceView(this, DefaultViewName);
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
	/// Supports the PatientDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class PatientDataSourceView : ProviderDataSourceView<Patient, PatientKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PatientDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the PatientDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public PatientDataSourceView(PatientDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal PatientDataSource PatientOwner
		{
			get { return Owner as PatientDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal PatientSelectMethod SelectMethod
		{
			get { return PatientOwner.SelectMethod; }
			set { PatientOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal PatientProviderBase PatientProvider
		{
			get { return Provider as PatientProviderBase; }
		}

		#endregion Properties
		
		#region Methods
		
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<Patient> GetSelectData(out int count)
		{
			Hashtable values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<Patient> results = null;
			Patient item;
			count = 0;
			
			System.String _id;

			switch ( SelectMethod )
			{
				case PatientSelectMethod.Get:
					PatientKey entityKey  = new PatientKey();
					entityKey.Load(values);
					item = PatientProvider.Get(GetTransactionManager(), entityKey);
					results = new TList<Patient>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case PatientSelectMethod.GetAll:
                    results = PatientProvider.GetAll(GetTransactionManager(), StartIndex, PageSize, out count);
                    break;
				case PatientSelectMethod.GetPaged:
					results = PatientProvider.GetPaged(GetTransactionManager(), WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case PatientSelectMethod.Find:
					if ( FilterParameters != null )
						results = PatientProvider.Find(GetTransactionManager(), FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = PatientProvider.Find(GetTransactionManager(), WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case PatientSelectMethod.GetById:
					_id = ( values["Id"] != null ) ? (System.String) EntityUtil.ChangeType(values["Id"], typeof(System.String)) : string.Empty;
					item = PatientProvider.GetById(GetTransactionManager(), _id);
					results = new TList<Patient>();
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
			if ( SelectMethod == PatientSelectMethod.Get || SelectMethod == PatientSelectMethod.GetById )
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
				Patient entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// execute deep load method
					PatientProvider.DeepLoad(GetTransactionManager(), GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<Patient> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// execute deep load method
			PatientProvider.DeepLoad(GetTransactionManager(), entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region PatientDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the PatientDataSource class.
	/// </summary>
	public class PatientDataSourceDesigner : ProviderDataSourceDesigner<Patient, PatientKey>
	{
		/// <summary>
		/// Initializes a new instance of the PatientDataSourceDesigner class.
		/// </summary>
		public PatientDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public PatientSelectMethod SelectMethod
		{
			get { return ((PatientDataSource) DataSource).SelectMethod; }
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
				actions.Add(new PatientDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region PatientDataSourceActionList

	/// <summary>
	/// Supports the PatientDataSourceDesigner class.
	/// </summary>
	internal class PatientDataSourceActionList : DesignerActionList
	{
		private PatientDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the PatientDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public PatientDataSourceActionList(PatientDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public PatientSelectMethod SelectMethod
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

	#endregion PatientDataSourceActionList
	
	#endregion PatientDataSourceDesigner
	
	#region PatientSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the PatientDataSource.SelectMethod property.
	/// </summary>
	public enum PatientSelectMethod
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
	
	#endregion PatientSelectMethod

	#region PatientFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Patient"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PatientFilter : SqlFilter<PatientColumn>
	{
	}
	
	#endregion PatientFilter

	#region PatientExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Patient"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PatientExpressionBuilder : SqlExpressionBuilder<PatientColumn>
	{
	}
	
	#endregion PatientExpressionBuilder	

	#region PatientProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;PatientChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="Patient"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PatientProperty : ChildEntityProperty<PatientChildEntityTypes>
	{
	}
	
	#endregion PatientProperty
}

