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
	/// Represents the DataRepository.FloorProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(FloorDataSourceDesigner))]
	public class FloorDataSource : ProviderDataSource<Floor, FloorKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the FloorDataSource class.
		/// </summary>
		public FloorDataSource() : base(DataRepository.FloorProvider)
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the FloorDataSourceView used by the FloorDataSource.
		/// </summary>
		protected FloorDataSourceView FloorView
		{
			get { return ( View as FloorDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the FloorDataSource control invokes to retrieve data.
		/// </summary>
		public FloorSelectMethod SelectMethod
		{
			get
			{
				FloorSelectMethod selectMethod = FloorSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (FloorSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the FloorDataSourceView class that is to be
		/// used by the FloorDataSource.
		/// </summary>
		/// <returns>An instance of the FloorDataSourceView class.</returns>
		protected override BaseDataSourceView<Floor, FloorKey> GetNewDataSourceView()
		{
			return new FloorDataSourceView(this, DefaultViewName);
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
	/// Supports the FloorDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class FloorDataSourceView : ProviderDataSourceView<Floor, FloorKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the FloorDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the FloorDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public FloorDataSourceView(FloorDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal FloorDataSource FloorOwner
		{
			get { return Owner as FloorDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal FloorSelectMethod SelectMethod
		{
			get { return FloorOwner.SelectMethod; }
			set { FloorOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal FloorProviderBase FloorProvider
		{
			get { return Provider as FloorProviderBase; }
		}

		#endregion Properties
		
		#region Methods
		
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<Floor> GetSelectData(out int count)
		{
			Hashtable values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<Floor> results = null;
			Floor item;
			count = 0;
			
			System.Int32 _id;

			switch ( SelectMethod )
			{
				case FloorSelectMethod.Get:
					FloorKey entityKey  = new FloorKey();
					entityKey.Load(values);
					item = FloorProvider.Get(GetTransactionManager(), entityKey);
					results = new TList<Floor>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case FloorSelectMethod.GetAll:
                    results = FloorProvider.GetAll(GetTransactionManager(), StartIndex, PageSize, out count);
                    break;
				case FloorSelectMethod.GetPaged:
					results = FloorProvider.GetPaged(GetTransactionManager(), WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case FloorSelectMethod.Find:
					if ( FilterParameters != null )
						results = FloorProvider.Find(GetTransactionManager(), FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = FloorProvider.Find(GetTransactionManager(), WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case FloorSelectMethod.GetById:
					_id = ( values["Id"] != null ) ? (System.Int32) EntityUtil.ChangeType(values["Id"], typeof(System.Int32)) : (int)0;
					item = FloorProvider.GetById(GetTransactionManager(), _id);
					results = new TList<Floor>();
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
			if ( SelectMethod == FloorSelectMethod.Get || SelectMethod == FloorSelectMethod.GetById )
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
				Floor entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// execute deep load method
					FloorProvider.DeepLoad(GetTransactionManager(), GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<Floor> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// execute deep load method
			FloorProvider.DeepLoad(GetTransactionManager(), entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region FloorDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the FloorDataSource class.
	/// </summary>
	public class FloorDataSourceDesigner : ProviderDataSourceDesigner<Floor, FloorKey>
	{
		/// <summary>
		/// Initializes a new instance of the FloorDataSourceDesigner class.
		/// </summary>
		public FloorDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public FloorSelectMethod SelectMethod
		{
			get { return ((FloorDataSource) DataSource).SelectMethod; }
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
				actions.Add(new FloorDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region FloorDataSourceActionList

	/// <summary>
	/// Supports the FloorDataSourceDesigner class.
	/// </summary>
	internal class FloorDataSourceActionList : DesignerActionList
	{
		private FloorDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the FloorDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public FloorDataSourceActionList(FloorDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public FloorSelectMethod SelectMethod
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

	#endregion FloorDataSourceActionList
	
	#endregion FloorDataSourceDesigner
	
	#region FloorSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the FloorDataSource.SelectMethod property.
	/// </summary>
	public enum FloorSelectMethod
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
	
	#endregion FloorSelectMethod

	#region FloorFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Floor"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class FloorFilter : SqlFilter<FloorColumn>
	{
	}
	
	#endregion FloorFilter

	#region FloorExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Floor"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class FloorExpressionBuilder : SqlExpressionBuilder<FloorColumn>
	{
	}
	
	#endregion FloorExpressionBuilder	

	#region FloorProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;FloorChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="Floor"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class FloorProperty : ChildEntityProperty<FloorChildEntityTypes>
	{
	}
	
	#endregion FloorProperty
}

