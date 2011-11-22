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
	/// Represents the DataRepository.RosterProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(RosterDataSourceDesigner))]
	public class RosterDataSource : ProviderDataSource<Roster, RosterKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the RosterDataSource class.
		/// </summary>
		public RosterDataSource() : base(DataRepository.RosterProvider)
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the RosterDataSourceView used by the RosterDataSource.
		/// </summary>
		protected RosterDataSourceView RosterView
		{
			get { return ( View as RosterDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the RosterDataSource control invokes to retrieve data.
		/// </summary>
		public RosterSelectMethod SelectMethod
		{
			get
			{
				RosterSelectMethod selectMethod = RosterSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (RosterSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the RosterDataSourceView class that is to be
		/// used by the RosterDataSource.
		/// </summary>
		/// <returns>An instance of the RosterDataSourceView class.</returns>
		protected override BaseDataSourceView<Roster, RosterKey> GetNewDataSourceView()
		{
			return new RosterDataSourceView(this, DefaultViewName);
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
	/// Supports the RosterDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class RosterDataSourceView : ProviderDataSourceView<Roster, RosterKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the RosterDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the RosterDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public RosterDataSourceView(RosterDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal RosterDataSource RosterOwner
		{
			get { return Owner as RosterDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal RosterSelectMethod SelectMethod
		{
			get { return RosterOwner.SelectMethod; }
			set { RosterOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal RosterProviderBase RosterProvider
		{
			get { return Provider as RosterProviderBase; }
		}

		#endregion Properties
		
		#region Methods
		
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<Roster> GetSelectData(out int count)
		{
			Hashtable values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<Roster> results = null;
			Roster item;
			count = 0;
			
			System.Int32 _id;
			System.Int32? _rosterTypeId_nullable;

			switch ( SelectMethod )
			{
				case RosterSelectMethod.Get:
					RosterKey entityKey  = new RosterKey();
					entityKey.Load(values);
					item = RosterProvider.Get(GetTransactionManager(), entityKey);
					results = new TList<Roster>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case RosterSelectMethod.GetAll:
                    results = RosterProvider.GetAll(GetTransactionManager(), StartIndex, PageSize, out count);
                    break;
				case RosterSelectMethod.GetPaged:
					results = RosterProvider.GetPaged(GetTransactionManager(), WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case RosterSelectMethod.Find:
					if ( FilterParameters != null )
						results = RosterProvider.Find(GetTransactionManager(), FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = RosterProvider.Find(GetTransactionManager(), WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case RosterSelectMethod.GetById:
					_id = ( values["Id"] != null ) ? (System.Int32) EntityUtil.ChangeType(values["Id"], typeof(System.Int32)) : (int)0;
					item = RosterProvider.GetById(GetTransactionManager(), _id);
					results = new TList<Roster>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				case RosterSelectMethod.GetByRosterTypeId:
					_rosterTypeId_nullable = (System.Int32?) EntityUtil.ChangeType(values["RosterTypeId"], typeof(System.Int32?));
					results = RosterProvider.GetByRosterTypeId(GetTransactionManager(), _rosterTypeId_nullable, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == RosterSelectMethod.Get || SelectMethod == RosterSelectMethod.GetById )
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
				Roster entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// execute deep load method
					RosterProvider.DeepLoad(GetTransactionManager(), GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<Roster> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// execute deep load method
			RosterProvider.DeepLoad(GetTransactionManager(), entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region RosterDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the RosterDataSource class.
	/// </summary>
	public class RosterDataSourceDesigner : ProviderDataSourceDesigner<Roster, RosterKey>
	{
		/// <summary>
		/// Initializes a new instance of the RosterDataSourceDesigner class.
		/// </summary>
		public RosterDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public RosterSelectMethod SelectMethod
		{
			get { return ((RosterDataSource) DataSource).SelectMethod; }
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
				actions.Add(new RosterDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region RosterDataSourceActionList

	/// <summary>
	/// Supports the RosterDataSourceDesigner class.
	/// </summary>
	internal class RosterDataSourceActionList : DesignerActionList
	{
		private RosterDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the RosterDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public RosterDataSourceActionList(RosterDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public RosterSelectMethod SelectMethod
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

	#endregion RosterDataSourceActionList
	
	#endregion RosterDataSourceDesigner
	
	#region RosterSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the RosterDataSource.SelectMethod property.
	/// </summary>
	public enum RosterSelectMethod
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
		/// Represents the GetByRosterTypeId method.
		/// </summary>
		GetByRosterTypeId
	}
	
	#endregion RosterSelectMethod

	#region RosterFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Roster"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class RosterFilter : SqlFilter<RosterColumn>
	{
	}
	
	#endregion RosterFilter

	#region RosterExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Roster"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class RosterExpressionBuilder : SqlExpressionBuilder<RosterColumn>
	{
	}
	
	#endregion RosterExpressionBuilder	

	#region RosterProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;RosterChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="Roster"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class RosterProperty : ChildEntityProperty<RosterChildEntityTypes>
	{
	}
	
	#endregion RosterProperty
}

