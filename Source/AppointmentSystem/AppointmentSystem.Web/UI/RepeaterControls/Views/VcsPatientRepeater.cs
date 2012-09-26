using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.Design.WebControls;
using System.Web.UI.Design;
using System.Web.UI.WebControls;

namespace AppointmentSystem.Web.UI
{
    /// <summary>
    /// A designer class for a strongly typed repeater <c>VcsPatientRepeater</c>
    /// </summary>
	public class VcsPatientRepeaterDesigner : System.Web.UI.Design.ControlDesigner
	{
	    /// <summary>
        /// Initializes a new instance of the <see cref="T:VcsPatientRepeaterDesigner"/> class.
        /// </summary>
		public VcsPatientRepeaterDesigner()
		{
		}

        /// <summary>
        /// Initializes the control designer and loads the specified component.
        /// </summary>
        /// <param name="component">The control being designed.</param>
		public override void Initialize(IComponent component)
		{
			if (!(component is VcsPatientRepeater))
			{ 
				throw new ArgumentException("Component is not a VcsPatientRepeater."); 
			} 
			base.Initialize(component); 
			base.SetViewFlags(ViewFlags.TemplateEditing, true); 
		}


		/// <summary>
		/// Generate HTML for the designer
		/// </summary>
		/// <returns>a string of design time HTML</returns>
		public override string GetDesignTimeHtml()
		{

			// Get the instance this designer applies to
			//
			VcsPatientRepeater z = (VcsPatientRepeater)Component;
			z.DataBind();

			return base.GetDesignTimeHtml();
		}
	}

    /// <summary>
    /// A strongly typed repeater control for the <see cref="VcsPatientRepeater"/> Type.
    /// </summary>
	[Designer(typeof(VcsPatientRepeaterDesigner))]
	[ParseChildren(true)]
	[ToolboxData("<{0}:VcsPatientRepeater runat=\"server\"></{0}:VcsPatientRepeater>")]
	public class VcsPatientRepeater : CompositeDataBoundControl, System.Web.UI.INamingContainer
	{
	    /// <summary>
        /// Initializes a new instance of the <see cref="T:VcsPatientRepeater"/> class.
        /// </summary>
		public VcsPatientRepeater()
		{
		}

		/// <summary>
        /// Gets a <see cref="T:System.Web.UI.ControlCollection"></see> object that represents the child controls for a specified server control in the UI hierarchy.
        /// </summary>
        /// <value></value>
        /// <returns>The collection of child controls for the specified server control.</returns>
		public override ControlCollection Controls
		{
			get
			{
				this.EnsureChildControls();
				return base.Controls;
			}
		}

		private ITemplate m_headerTemplate;
		/// <summary>
        /// Gets or sets the header template.
        /// </summary>
        /// <value>The header template.</value>
		[Browsable(false)]
		[TemplateContainer(typeof(VcsPatientItem))]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public ITemplate HeaderTemplate
		{
			get { return m_headerTemplate; }
			set { m_headerTemplate = value; }
		}

		private ITemplate m_itemTemplate;
		/// <summary>
        /// Gets or sets the item template.
        /// </summary>
        /// <value>The item template.</value>
		[Browsable(false)]
		[TemplateContainer(typeof(VcsPatientItem))]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public ITemplate ItemTemplate
		{
			get { return m_itemTemplate; }
			set { m_itemTemplate = value; }
		}

		private ITemplate m_seperatorTemplate;
		/// <summary>
        /// Gets or sets the Seperator Template
        /// </summary>
        [Browsable(false)]
        [TemplateContainer(typeof(VcsPatientItem))]
        [PersistenceMode(PersistenceMode.InnerDefaultProperty)]
        public ITemplate SeperatorTemplate
        {
            get { return m_seperatorTemplate; }
            set { m_seperatorTemplate = value; }
        }

		private ITemplate m_altenateItemTemplate;
        /// <summary>
        /// Gets or sets the alternating item template.
        /// </summary>
        /// <value>The alternating item template.</value>
		[Browsable(false)]
		[TemplateContainer(typeof(VcsPatientItem))]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public ITemplate AlternatingItemTemplate
		{
			get { return m_altenateItemTemplate; }
			set { m_altenateItemTemplate = value; }
		}

		private ITemplate m_footerTemplate;
        /// <summary>
        /// Gets or sets the footer template.
        /// </summary>
        /// <value>The footer template.</value>
		[Browsable(false)]
		[TemplateContainer(typeof(VcsPatientItem))]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public ITemplate FooterTemplate
		{
			get { return m_footerTemplate; }
			set { m_footerTemplate = value; }
		}
		
		
		/// <summary>
        /// Overridden and Empty so that span tags are not written
        /// </summary>
        /// <param name="writer"></param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            
        }

        /// <summary>
        /// Overridden and Empty so that span tags are not written
        /// </summary>
        /// <param name="writer"></param>
        public override void RenderEndTag(HtmlTextWriter writer)
        {
                
        }		

//      /// <summary>
//      /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
//      /// </summary>
//		protected override void CreateChildControls()
//		{
//			if (ChildControlsCreated)
//			{
//				return;
//			}
//			Controls.Clear();
//
//			if (m_headerTemplate != null)
//			{
//				Control headerItem = new Control();
//				m_headerTemplate.InstantiateIn(headerItem);
//				Controls.Add(headerItem);
//			}
//
//			
//			if (m_footerTemplate != null)
//			{
//				Control footerItem = new Control();
//				m_footerTemplate.InstantiateIn(footerItem);
//				Controls.Add(footerItem);
//			}
//			ChildControlsCreated = true;
//		}
		
		/// <summary>
      /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
      /// </summary>
		protected override int CreateChildControls(System.Collections.IEnumerable dataSource, bool dataBinding)
      {
         int pos = 0;

         if (dataBinding)
         {
            //Instantiate the Header template (if exists)
            if (m_headerTemplate != null)
            {
                Control headerItem = new Control();
                m_headerTemplate.InstantiateIn(headerItem);
                Controls.Add(headerItem);
            }
			if (dataSource != null)
			{
				foreach (object o in dataSource)
				{
						AppointmentSystem.Entities.VcsPatient entity = o as AppointmentSystem.Entities.VcsPatient;
						VcsPatientItem container = new VcsPatientItem(entity);
	
						if (m_itemTemplate != null && (pos % 2) == 0)
						{
							m_itemTemplate.InstantiateIn(container);
							
							if (m_seperatorTemplate != null)
							{
								m_seperatorTemplate.InstantiateIn(container);
							}
						}
						else
						{
							if (m_altenateItemTemplate != null)
							{
								m_altenateItemTemplate.InstantiateIn(container);
								if (m_seperatorTemplate != null)
								{
									m_seperatorTemplate.InstantiateIn(container);
								}

							}
							else if (m_itemTemplate != null)
							{
								m_itemTemplate.InstantiateIn(container);
								if (m_seperatorTemplate != null)
								{
									m_seperatorTemplate.InstantiateIn(container);
								}

							}
							else
							{
								// no template !!!
							}
						}
						Controls.Add(container);
						
						container.DataBind();
						
						pos++;
				}
			}            
			//Instantiate the Footer template (if exists)
            if (m_footerTemplate != null)
            {
                Control footerItem = new Control();
                m_footerTemplate.InstantiateIn(footerItem);
                Controls.Add(footerItem);
            }				
		 }
			
			return pos;
		}
		
      /// <summary>
      /// Raises the <see cref="E:System.Web.UI.Control.PreRender"></see> event.
      /// </summary>
      /// <param name="e">An <see cref="T:System.EventArgs"></see> object that contains the event data.</param>
		protected override void OnPreRender(EventArgs e)
		{
			base.DataBind();
		}

		#region Design time
        /// <summary>
        /// Renders at design time.
        /// </summary>
        /// <returns>a  string of the Designed HTML</returns>
		internal string RenderAtDesignTime()
		{			
			return "Designer currently not implemented"; 
		}

		#endregion
	}

    /// <summary>
    /// A wrapper type for the entity
    /// </summary>
	[System.ComponentModel.ToolboxItem(false)]
	public class VcsPatientItem : System.Web.UI.Control, System.Web.UI.INamingContainer
	{
		private AppointmentSystem.Entities.VcsPatient _entity;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:VcsPatientItem"/> class.
        /// </summary>
		public VcsPatientItem()
			: base()
		{ }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:VcsPatientItem"/> class.
        /// </summary>
		public VcsPatientItem(AppointmentSystem.Entities.VcsPatient entity)
			: base()
		{
			_entity = entity;
		}
		
        /// <summary>
        /// Gets the PatientCode
        /// </summary>
        /// <value>The PatientCode.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String PatientCode
		{
			get { return _entity.PatientCode; }
		}
        /// <summary>
        /// Gets the FirstName
        /// </summary>
        /// <value>The FirstName.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String FirstName
		{
			get { return _entity.FirstName; }
		}
        /// <summary>
        /// Gets the MiddleName
        /// </summary>
        /// <value>The MiddleName.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String MiddleName
		{
			get { return _entity.MiddleName; }
		}
        /// <summary>
        /// Gets the LastName
        /// </summary>
        /// <value>The LastName.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String LastName
		{
			get { return _entity.LastName; }
		}
        /// <summary>
        /// Gets the DateOfBirth
        /// </summary>
        /// <value>The DateOfBirth.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime DateOfBirth
		{
			get { return _entity.DateOfBirth; }
		}
        /// <summary>
        /// Gets the Sex
        /// </summary>
        /// <value>The Sex.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Sex
		{
			get { return _entity.Sex; }
		}
        /// <summary>
        /// Gets the MemberType
        /// </summary>
        /// <value>The MemberType.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String MemberType
		{
			get { return _entity.MemberType; }
		}
        /// <summary>
        /// Gets the Nationality
        /// </summary>
        /// <value>The Nationality.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Nationality
		{
			get { return _entity.Nationality; }
		}
        /// <summary>
        /// Gets the HomeStreet
        /// </summary>
        /// <value>The HomeStreet.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String HomeStreet
		{
			get { return _entity.HomeStreet; }
		}
        /// <summary>
        /// Gets the HomeWard
        /// </summary>
        /// <value>The HomeWard.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String HomeWard
		{
			get { return _entity.HomeWard; }
		}
        /// <summary>
        /// Gets the HomeDistrict
        /// </summary>
        /// <value>The HomeDistrict.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String HomeDistrict
		{
			get { return _entity.HomeDistrict; }
		}
        /// <summary>
        /// Gets the HomeCity
        /// </summary>
        /// <value>The HomeCity.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String HomeCity
		{
			get { return _entity.HomeCity; }
		}
        /// <summary>
        /// Gets the HomeCountry
        /// </summary>
        /// <value>The HomeCountry.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String HomeCountry
		{
			get { return _entity.HomeCountry; }
		}
        /// <summary>
        /// Gets the WorkStreet
        /// </summary>
        /// <value>The WorkStreet.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String WorkStreet
		{
			get { return _entity.WorkStreet; }
		}
        /// <summary>
        /// Gets the WorkWard
        /// </summary>
        /// <value>The WorkWard.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String WorkWard
		{
			get { return _entity.WorkWard; }
		}
        /// <summary>
        /// Gets the WorkDistrict
        /// </summary>
        /// <value>The WorkDistrict.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String WorkDistrict
		{
			get { return _entity.WorkDistrict; }
		}
        /// <summary>
        /// Gets the WorkCity
        /// </summary>
        /// <value>The WorkCity.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String WorkCity
		{
			get { return _entity.WorkCity; }
		}
        /// <summary>
        /// Gets the WorkCountry
        /// </summary>
        /// <value>The WorkCountry.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String WorkCountry
		{
			get { return _entity.WorkCountry; }
		}
        /// <summary>
        /// Gets the CompanyCode
        /// </summary>
        /// <value>The CompanyCode.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String CompanyCode
		{
			get { return _entity.CompanyCode; }
		}
        /// <summary>
        /// Gets the BillingAddress
        /// </summary>
        /// <value>The BillingAddress.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String BillingAddress
		{
			get { return _entity.BillingAddress; }
		}
        /// <summary>
        /// Gets the HomePhone
        /// </summary>
        /// <value>The HomePhone.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String HomePhone
		{
			get { return _entity.HomePhone; }
		}
        /// <summary>
        /// Gets the MobilePhone
        /// </summary>
        /// <value>The MobilePhone.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String MobilePhone
		{
			get { return _entity.MobilePhone; }
		}
        /// <summary>
        /// Gets the CompanyPhone
        /// </summary>
        /// <value>The CompanyPhone.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String CompanyPhone
		{
			get { return _entity.CompanyPhone; }
		}
        /// <summary>
        /// Gets the Fax
        /// </summary>
        /// <value>The Fax.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Fax
		{
			get { return _entity.Fax; }
		}
        /// <summary>
        /// Gets the EmailAddress
        /// </summary>
        /// <value>The EmailAddress.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String EmailAddress
		{
			get { return _entity.EmailAddress; }
		}
        /// <summary>
        /// Gets the CreateDate
        /// </summary>
        /// <value>The CreateDate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime CreateDate
		{
			get { return _entity.CreateDate; }
		}
        /// <summary>
        /// Gets the ValidCorporate
        /// </summary>
        /// <value>The ValidCorporate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Boolean ValidCorporate
		{
			get { return _entity.ValidCorporate; }
		}
        /// <summary>
        /// Gets the DefaultPaymentMode
        /// </summary>
        /// <value>The DefaultPaymentMode.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String DefaultPaymentMode
		{
			get { return _entity.DefaultPaymentMode; }
		}
        /// <summary>
        /// Gets the InsuranceCardNumber
        /// </summary>
        /// <value>The InsuranceCardNumber.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String InsuranceCardNumber
		{
			get { return _entity.InsuranceCardNumber; }
		}
        /// <summary>
        /// Gets the InsuranceCardExpDate
        /// </summary>
        /// <value>The InsuranceCardExpDate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? InsuranceCardExpDate
		{
			get { return _entity.InsuranceCardExpDate; }
		}
        /// <summary>
        /// Gets the MembershipSosNumber
        /// </summary>
        /// <value>The MembershipSosNumber.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String MembershipSosNumber
		{
			get { return _entity.MembershipSosNumber; }
		}
        /// <summary>
        /// Gets the MembershipSosExpDate
        /// </summary>
        /// <value>The MembershipSosExpDate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? MembershipSosExpDate
		{
			get { return _entity.MembershipSosExpDate; }
		}
        /// <summary>
        /// Gets the IsDisabled
        /// </summary>
        /// <value>The IsDisabled.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Boolean IsDisabled
		{
			get { return _entity.IsDisabled; }
		}
        /// <summary>
        /// Gets the UpdateUser
        /// </summary>
        /// <value>The UpdateUser.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String UpdateUser
		{
			get { return _entity.UpdateUser; }
		}
        /// <summary>
        /// Gets the UpdateDate
        /// </summary>
        /// <value>The UpdateDate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime UpdateDate
		{
			get { return _entity.UpdateDate; }
		}
        /// <summary>
        /// Gets the Remark
        /// </summary>
        /// <value>The Remark.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Remark
		{
			get { return _entity.Remark; }
		}

	}
}
