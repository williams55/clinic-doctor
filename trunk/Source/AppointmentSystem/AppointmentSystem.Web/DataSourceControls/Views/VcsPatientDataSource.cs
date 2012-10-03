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
	    /// <param name="values"></param>
		/// <param name="count">The total number of rows in the DataSource.</param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<VcsPatient> GetSelectData(IDictionary values, out int count)
		{	
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			
			IList<VcsPatient> results = null;
			// VcsPatient item;
			count = 0;
			
			System.String sp3_PatientCode;
			System.String sp1_PatientCode;
			System.String sp1_FirstName;
			System.String sp1_MiddleName;
			System.String sp1_LastName;
			System.DateTime? sp1_DateOfBirth;
			System.String sp1_Sex;
			System.String sp1_MemberType;
			System.String sp1_Nationality;
			System.String sp1_HomeStreet;
			System.String sp1_HomeWard;
			System.String sp1_HomeDistrict;
			System.String sp1_HomeCity;
			System.String sp1_HomeCountry;
			System.String sp1_WorkStreet;
			System.String sp1_WorkWard;
			System.String sp1_WorkDistrict;
			System.String sp1_WorkCity;
			System.String sp1_WorkCountry;
			System.String sp1_CompanyCode;
			System.String sp1_BillingAddress;
			System.String sp1_HomePhone;
			System.String sp1_MobilePhone;
			System.String sp1_CompanyPhone;
			System.String sp1_Fax;
			System.String sp1_EmailAddress;
			System.DateTime? sp1_CreateDate;
			System.String sp1_UpdateUser;
			System.DateTime? sp1_UpdateDate;
			System.String sp1_Remark;

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
				case VcsPatientSelectMethod.GetByPatientCode:
					sp3_PatientCode = (System.String) EntityUtil.ChangeType(values["PatientCode"], typeof(System.String));
					results = VcsPatientProvider.GetByPatientCode(GetTransactionManager(), StartIndex, PageSize, sp3_PatientCode);
					break;
				case VcsPatientSelectMethod.Insert:
					sp1_PatientCode = (System.String) EntityUtil.ChangeType(values["PatientCode"], typeof(System.String));
					sp1_FirstName = (System.String) EntityUtil.ChangeType(values["FirstName"], typeof(System.String));
					sp1_MiddleName = (System.String) EntityUtil.ChangeType(values["MiddleName"], typeof(System.String));
					sp1_LastName = (System.String) EntityUtil.ChangeType(values["LastName"], typeof(System.String));
					sp1_DateOfBirth = (System.DateTime?) EntityUtil.ChangeType(values["DateOfBirth"], typeof(System.DateTime?));
					sp1_Sex = (System.String) EntityUtil.ChangeType(values["Sex"], typeof(System.String));
					sp1_MemberType = (System.String) EntityUtil.ChangeType(values["MemberType"], typeof(System.String));
					sp1_Nationality = (System.String) EntityUtil.ChangeType(values["Nationality"], typeof(System.String));
					sp1_HomeStreet = (System.String) EntityUtil.ChangeType(values["HomeStreet"], typeof(System.String));
					sp1_HomeWard = (System.String) EntityUtil.ChangeType(values["HomeWard"], typeof(System.String));
					sp1_HomeDistrict = (System.String) EntityUtil.ChangeType(values["HomeDistrict"], typeof(System.String));
					sp1_HomeCity = (System.String) EntityUtil.ChangeType(values["HomeCity"], typeof(System.String));
					sp1_HomeCountry = (System.String) EntityUtil.ChangeType(values["HomeCountry"], typeof(System.String));
					sp1_WorkStreet = (System.String) EntityUtil.ChangeType(values["WorkStreet"], typeof(System.String));
					sp1_WorkWard = (System.String) EntityUtil.ChangeType(values["WorkWard"], typeof(System.String));
					sp1_WorkDistrict = (System.String) EntityUtil.ChangeType(values["WorkDistrict"], typeof(System.String));
					sp1_WorkCity = (System.String) EntityUtil.ChangeType(values["WorkCity"], typeof(System.String));
					sp1_WorkCountry = (System.String) EntityUtil.ChangeType(values["WorkCountry"], typeof(System.String));
					sp1_CompanyCode = (System.String) EntityUtil.ChangeType(values["CompanyCode"], typeof(System.String));
					sp1_BillingAddress = (System.String) EntityUtil.ChangeType(values["BillingAddress"], typeof(System.String));
					sp1_HomePhone = (System.String) EntityUtil.ChangeType(values["HomePhone"], typeof(System.String));
					sp1_MobilePhone = (System.String) EntityUtil.ChangeType(values["MobilePhone"], typeof(System.String));
					sp1_CompanyPhone = (System.String) EntityUtil.ChangeType(values["CompanyPhone"], typeof(System.String));
					sp1_Fax = (System.String) EntityUtil.ChangeType(values["Fax"], typeof(System.String));
					sp1_EmailAddress = (System.String) EntityUtil.ChangeType(values["EmailAddress"], typeof(System.String));
					sp1_CreateDate = (System.DateTime?) EntityUtil.ChangeType(values["CreateDate"], typeof(System.DateTime?));
					sp1_UpdateUser = (System.String) EntityUtil.ChangeType(values["UpdateUser"], typeof(System.String));
					sp1_UpdateDate = (System.DateTime?) EntityUtil.ChangeType(values["UpdateDate"], typeof(System.DateTime?));
					sp1_Remark = (System.String) EntityUtil.ChangeType(values["Remark"], typeof(System.String));
					results = VcsPatientProvider.Insert(GetTransactionManager(), StartIndex, PageSize, sp1_PatientCode, sp1_FirstName, sp1_MiddleName, sp1_LastName, sp1_DateOfBirth, sp1_Sex, sp1_MemberType, sp1_Nationality, sp1_HomeStreet, sp1_HomeWard, sp1_HomeDistrict, sp1_HomeCity, sp1_HomeCountry, sp1_WorkStreet, sp1_WorkWard, sp1_WorkDistrict, sp1_WorkCity, sp1_WorkCountry, sp1_CompanyCode, sp1_BillingAddress, sp1_HomePhone, sp1_MobilePhone, sp1_CompanyPhone, sp1_Fax, sp1_EmailAddress, sp1_CreateDate, sp1_UpdateUser, sp1_UpdateDate, sp1_Remark);
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
		/// Represents the GetByPatientCode method.
		/// </summary>
		GetByPatientCode,
		/// <summary>
		/// Represents the Insert method.
		/// </summary>
		Insert
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

