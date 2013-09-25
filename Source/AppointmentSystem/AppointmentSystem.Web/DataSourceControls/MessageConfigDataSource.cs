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
	/// Represents the DataRepository.MessageConfigProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(MessageConfigDataSourceDesigner))]
	public class MessageConfigDataSource : ProviderDataSource<MessageConfig, MessageConfigKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the MessageConfigDataSource class.
		/// </summary>
		public MessageConfigDataSource() : base(DataRepository.MessageConfigProvider)
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the MessageConfigDataSourceView used by the MessageConfigDataSource.
		/// </summary>
		protected MessageConfigDataSourceView MessageConfigView
		{
			get { return ( View as MessageConfigDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the MessageConfigDataSource control invokes to retrieve data.
		/// </summary>
		public MessageConfigSelectMethod SelectMethod
		{
			get
			{
				MessageConfigSelectMethod selectMethod = MessageConfigSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (MessageConfigSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the MessageConfigDataSourceView class that is to be
		/// used by the MessageConfigDataSource.
		/// </summary>
		/// <returns>An instance of the MessageConfigDataSourceView class.</returns>
		protected override BaseDataSourceView<MessageConfig, MessageConfigKey> GetNewDataSourceView()
		{
			return new MessageConfigDataSourceView(this, DefaultViewName);
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
	/// Supports the MessageConfigDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class MessageConfigDataSourceView : ProviderDataSourceView<MessageConfig, MessageConfigKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the MessageConfigDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the MessageConfigDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public MessageConfigDataSourceView(MessageConfigDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal MessageConfigDataSource MessageConfigOwner
		{
			get { return Owner as MessageConfigDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal MessageConfigSelectMethod SelectMethod
		{
			get { return MessageConfigOwner.SelectMethod; }
			set { MessageConfigOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal MessageConfigProviderBase MessageConfigProvider
		{
			get { return Provider as MessageConfigProviderBase; }
		}

		#endregion Properties
		
		#region Methods
		
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<MessageConfig> GetSelectData(out int count)
		{
			Hashtable values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<MessageConfig> results = null;
			MessageConfig item;
			count = 0;
			
			System.String _messageKey;

			switch ( SelectMethod )
			{
				case MessageConfigSelectMethod.Get:
					MessageConfigKey entityKey  = new MessageConfigKey();
					entityKey.Load(values);
					item = MessageConfigProvider.Get(GetTransactionManager(), entityKey);
					results = new TList<MessageConfig>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case MessageConfigSelectMethod.GetAll:
                    results = MessageConfigProvider.GetAll(GetTransactionManager(), StartIndex, PageSize, out count);
                    break;
				case MessageConfigSelectMethod.GetPaged:
					results = MessageConfigProvider.GetPaged(GetTransactionManager(), WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case MessageConfigSelectMethod.Find:
					if ( FilterParameters != null )
						results = MessageConfigProvider.Find(GetTransactionManager(), FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = MessageConfigProvider.Find(GetTransactionManager(), WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case MessageConfigSelectMethod.GetByMessageKey:
					_messageKey = ( values["MessageKey"] != null ) ? (System.String) EntityUtil.ChangeType(values["MessageKey"], typeof(System.String)) : string.Empty;
					item = MessageConfigProvider.GetByMessageKey(GetTransactionManager(), _messageKey);
					results = new TList<MessageConfig>();
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
			if ( SelectMethod == MessageConfigSelectMethod.Get || SelectMethod == MessageConfigSelectMethod.GetByMessageKey )
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
				MessageConfig entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// execute deep load method
					MessageConfigProvider.DeepLoad(GetTransactionManager(), GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<MessageConfig> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// execute deep load method
			MessageConfigProvider.DeepLoad(GetTransactionManager(), entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region MessageConfigDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the MessageConfigDataSource class.
	/// </summary>
	public class MessageConfigDataSourceDesigner : ProviderDataSourceDesigner<MessageConfig, MessageConfigKey>
	{
		/// <summary>
		/// Initializes a new instance of the MessageConfigDataSourceDesigner class.
		/// </summary>
		public MessageConfigDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public MessageConfigSelectMethod SelectMethod
		{
			get { return ((MessageConfigDataSource) DataSource).SelectMethod; }
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
				actions.Add(new MessageConfigDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region MessageConfigDataSourceActionList

	/// <summary>
	/// Supports the MessageConfigDataSourceDesigner class.
	/// </summary>
	internal class MessageConfigDataSourceActionList : DesignerActionList
	{
		private MessageConfigDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the MessageConfigDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public MessageConfigDataSourceActionList(MessageConfigDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public MessageConfigSelectMethod SelectMethod
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

	#endregion MessageConfigDataSourceActionList
	
	#endregion MessageConfigDataSourceDesigner
	
	#region MessageConfigSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the MessageConfigDataSource.SelectMethod property.
	/// </summary>
	public enum MessageConfigSelectMethod
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
		/// Represents the GetByMessageKey method.
		/// </summary>
		GetByMessageKey
	}
	
	#endregion MessageConfigSelectMethod

	#region MessageConfigFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="MessageConfig"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class MessageConfigFilter : SqlFilter<MessageConfigColumn>
	{
	}
	
	#endregion MessageConfigFilter

	#region MessageConfigExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="MessageConfig"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class MessageConfigExpressionBuilder : SqlExpressionBuilder<MessageConfigColumn>
	{
	}
	
	#endregion MessageConfigExpressionBuilder	

	#region MessageConfigProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;MessageConfigChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="MessageConfig"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class MessageConfigProperty : ChildEntityProperty<MessageConfigChildEntityTypes>
	{
	}
	
	#endregion MessageConfigProperty
}

