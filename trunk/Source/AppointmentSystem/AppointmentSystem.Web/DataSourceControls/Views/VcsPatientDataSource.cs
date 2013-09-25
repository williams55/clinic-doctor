#region Using Directives
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using AppointmentSystem.Entities;
using AppointmentSystem.Data;
using AppointmentSystem.Data.Bases;
#endregion

namespace AppointmentSystem.Web.Data
{
	/// <summary>
	/// Represents the DataRepository.VcsPatientProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[CLSCompliant(true)]
	[Designer(typeof(VcsPatientDataSourceDesigner))]
	public class VcsPatientDataSource : ReadOnlyDataSource<VcsPatient>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VcsPatientDataSource class.
		/// </summary>
		public VcsPatientDataSource() : base(DataRepository.VcsPatientProvider)
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the VcsPatientDataSourceView used by the VcsPatientDataSource.
		/// </summary>
		protected VcsPatientDataSourceView VcsPatientView
		{
			get { return ( View as VcsPatientDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the VcsPatientDataSource control invokes to retrieve data.
		/// </summary>
		public new VcsPatientSelectMethod SelectMethod
		{
			get
			{
				VcsPatientSelectMethod selectMethod = VcsPatientSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (VcsPatientSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}
		
		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the VcsPatientDataSourceView class that is to be
		/// used by the VcsPatientDataSource.
		/// </summary>
		/// <returns>An instance of the VcsPatientDataSourceView class.</returns>
		protected override BaseDataSourceView<VcsPatient, Object> GetNewDataSourceView()
		{
			return new VcsPatientDataSourceView(this, DefaultViewName);
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
	/// Supports the VcsPatientDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class VcsPatientDataSourceView : ReadOnlyDataSourceView<VcsPatient>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VcsPatientDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the VcsPatientDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public VcsPatientDataSourceView(VcsPatientDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal VcsPatientDataSource VcsPatientOwner
		{
			get { return Owner as VcsPatientDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal new VcsPatientSelectMethod SelectMethod
		{
			get { return VcsPatientOwner.SelectMethod; }
			set { VcsPatientOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal VcsPatientProviderBase VcsPatientProvider
		{
			get { return Provider as VcsPatientProviderBase; }
		}

		#endregion Properties
		
		#region Methods
		
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<VcsPatient> GetSelectData(out int count)
		{
			Hashtable values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
			
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			
			IList<VcsPatient> results = null;
			// VcsPatient item;
			count = 0;
			
			System.String sp2_PatientCode;
			System.String sp2_FirstName;
			System.String sp2_MiddleName;
			System.String sp2_LastName;
			System.DateTime? sp2_DateOfBirth;
			System.String sp2_Sex;
			System.String sp2_Nationality;
			System.String sp2_CompanyCode;
			System.String sp2_HomePhone;
			System.String sp2_MobilePhone;
			System.String sp2_CreateUser;
			System.String sp2_ApptRemark;
			System.String sp1_PatientCode;

			switch ( SelectMethod )
			{
				case VcsPatientSelectMethod.Get:
					results = VcsPatientProvider.Get(GetTransactionManager(), WhereClause, OrderBy, StartIndex, PageSize, out count);
                    break;
				case VcsPatientSelectMethod.GetPaged:
					results = VcsPatientProvider.GetPaged(GetTransactionManager(), WhereClause, OrderBy, StartIndex, PageSize, out count);
					break;
				case VcsPatientSelectMethod.GetAll:
					results = VcsPatientProvider.GetAll(GetTransactionManager(), StartIndex, PageSize, out count);
                    break;
				case VcsPatientSelectMethod.Find:
					results = VcsPatientProvider.Find(GetTransactionManager(), FilterParameters, OrderBy, StartIndex, PageSize, out count);
                    break;
				// Custom
				case VcsPatientSelectMethod.Insert:
					sp2_PatientCode = (System.String) EntityUtil.ChangeType(values["PatientCode"], typeof(System.String));
					sp2_FirstName = (System.String) EntityUtil.ChangeType(values["FirstName"], typeof(System.String));
					sp2_MiddleName = (System.String) EntityUtil.ChangeType(values["MiddleName"], typeof(System.String));
					sp2_LastName = (System.String) EntityUtil.ChangeType(values["LastName"], typeof(System.String));
					sp2_DateOfBirth = (System.DateTime?) EntityUtil.ChangeType(values["DateOfBirth"], typeof(System.DateTime?));
					sp2_Sex = (System.String) EntityUtil.ChangeType(values["Sex"], typeof(System.String));
					sp2_Nationality = (System.String) EntityUtil.ChangeType(values["Nationality"], typeof(System.String));
					sp2_CompanyCode = (System.String) EntityUtil.ChangeType(values["CompanyCode"], typeof(System.String));
					sp2_HomePhone = (System.String) EntityUtil.ChangeType(values["HomePhone"], typeof(System.String));
					sp2_MobilePhone = (System.String) EntityUtil.ChangeType(values["MobilePhone"], typeof(System.String));
					sp2_CreateUser = (System.String) EntityUtil.ChangeType(values["CreateUser"], typeof(System.String));
					sp2_ApptRemark = (System.String) EntityUtil.ChangeType(values["ApptRemark"], typeof(System.String));
					results = VcsPatientProvider.Insert(GetTransactionManager(), StartIndex, PageSize, sp2_PatientCode, sp2_FirstName, sp2_MiddleName, sp2_LastName, sp2_DateOfBirth, sp2_Sex, sp2_Nationality, sp2_CompanyCode, sp2_HomePhone, sp2_MobilePhone, sp2_CreateUser, sp2_ApptRemark);
					break;
				case VcsPatientSelectMethod.GetByPatientCode:
					sp1_PatientCode = (System.String) EntityUtil.ChangeType(values["PatientCode"], typeof(System.String));
					results = VcsPatientProvider.GetByPatientCode(GetTransactionManager(), StartIndex, PageSize, sp1_PatientCode);
					break;
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
		
		#endregion Methods
	}

	#region VcsPatientSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the VcsPatientDataSource.SelectMethod property.
	/// </summary>
	public enum VcsPatientSelectMethod
	{
		/// <summary>
		/// Represents the Get method.
		/// </summary>
		Get,
		/// <summary>
		/// Represents the GetPaged method.
		/// </summary>
		GetPaged,
		/// <summary>
		/// Represents the GetAll method.
		/// </summary>
		GetAll,
		/// <summary>
		/// Represents the Find method.
		/// </summary>
		Find,
		/// <summary>
		/// Represents the Insert method.
		/// </summary>
		Insert,
		/// <summary>
		/// Represents the GetByPatientCode method.
		/// </summary>
		GetByPatientCode
	}
	
	#endregion VcsPatientSelectMethod
	
	#region VcsPatientDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the VcsPatientDataSource class.
	/// </summary>
	public class VcsPatientDataSourceDesigner : ReadOnlyDataSourceDesigner<VcsPatient>
	{
		/// <summary>
		/// Initializes a new instance of the VcsPatientDataSourceDesigner class.
		/// </summary>
		public VcsPatientDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public new VcsPatientSelectMethod SelectMethod
		{
			get { return ((VcsPatientDataSource) DataSource).SelectMethod; }
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
				actions.Add(new VcsPatientDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region VcsPatientDataSourceActionList

	/// <summary>
	/// Supports the VcsPatientDataSourceDesigner class.
	/// </summary>
	internal class VcsPatientDataSourceActionList : DesignerActionList
	{
		private VcsPatientDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the VcsPatientDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public VcsPatientDataSourceActionList(VcsPatientDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public VcsPatientSelectMethod SelectMethod
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

	#endregion VcsPatientDataSourceActionList

	#endregion VcsPatientDataSourceDesigner

	#region VcsPatientFilter

	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="VcsPatient"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VcsPatientFilter : SqlFilter<VcsPatientColumn>
	{
	}

	#endregion VcsPatientFilter

	#region VcsPatientExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="VcsPatient"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VcsPatientExpressionBuilder : SqlExpressionBuilder<VcsPatientColumn>
	{
	}
	
	#endregion VcsPatientExpressionBuilder		
}

