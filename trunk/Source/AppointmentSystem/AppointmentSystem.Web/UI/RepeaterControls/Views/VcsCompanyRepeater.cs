﻿using System;
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
    /// A designer class for a strongly typed repeater <c>VcsCompanyRepeater</c>
    /// </summary>
	public class VcsCompanyRepeaterDesigner : System.Web.UI.Design.ControlDesigner
	{
	    /// <summary>
        /// Initializes a new instance of the <see cref="T:VcsCompanyRepeaterDesigner"/> class.
        /// </summary>
		public VcsCompanyRepeaterDesigner()
		{
		}

        /// <summary>
        /// Initializes the control designer and loads the specified component.
        /// </summary>
        /// <param name="component">The control being designed.</param>
		public override void Initialize(IComponent component)
		{
			if (!(component is VcsCompanyRepeater))
			{ 
				throw new ArgumentException("Component is not a VcsCompanyRepeater."); 
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
			VcsCompanyRepeater z = (VcsCompanyRepeater)Component;
			z.DataBind();

			return base.GetDesignTimeHtml();
		}
	}

    /// <summary>
    /// A strongly typed repeater control for the <c cref="VcsCompanyRepeater"></c> Type.
    /// </summary>
	[Designer(typeof(VcsCompanyRepeaterDesigner))]
	[ParseChildren(true)]
	[ToolboxData("<{0}:VcsCompanyRepeater runat=\"server\"></{0}:VcsCompanyRepeater>")]
	public class VcsCompanyRepeater : CompositeDataBoundControl, System.Web.UI.INamingContainer
	{
	    /// <summary>
        /// Initializes a new instance of the <see cref="T:VcsCompanyRepeater"/> class.
        /// </summary>
		public VcsCompanyRepeater()
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
		[TemplateContainer(typeof(VcsCompanyItem))]
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
		[TemplateContainer(typeof(VcsCompanyItem))]
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
        [TemplateContainer(typeof(VcsCompanyItem))]
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
		[TemplateContainer(typeof(VcsCompanyItem))]
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
		[TemplateContainer(typeof(VcsCompanyItem))]
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
						AppointmentSystem.Entities.VcsCompany entity = o as AppointmentSystem.Entities.VcsCompany;
						VcsCompanyItem container = new VcsCompanyItem(entity);
	
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
	public class VcsCompanyItem : System.Web.UI.Control, System.Web.UI.INamingContainer
	{
		private AppointmentSystem.Entities.VcsCompany _entity;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:VcsCompanyItem"/> class.
        /// </summary>
		public VcsCompanyItem()
			: base()
		{ }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:VcsCompanyItem"/> class.
        /// </summary>
		public VcsCompanyItem(AppointmentSystem.Entities.VcsCompany entity)
			: base()
		{
			_entity = entity;
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
        /// Gets the CompanyName
        /// </summary>
        /// <value>The CompanyName.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String CompanyName
		{
			get { return _entity.CompanyName; }
		}
        /// <summary>
        /// Gets the Address
        /// </summary>
        /// <value>The Address.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Address
		{
			get { return _entity.Address; }
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
        /// Gets the CompanyTel
        /// </summary>
        /// <value>The CompanyTel.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String CompanyTel
		{
			get { return _entity.CompanyTel; }
		}
        /// <summary>
        /// Gets the CompanyTel2
        /// </summary>
        /// <value>The CompanyTel2.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String CompanyTel2
		{
			get { return _entity.CompanyTel2; }
		}
        /// <summary>
        /// Gets the TaxNumber
        /// </summary>
        /// <value>The TaxNumber.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String TaxNumber
		{
			get { return _entity.TaxNumber; }
		}
        /// <summary>
        /// Gets the AccountCode
        /// </summary>
        /// <value>The AccountCode.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String AccountCode
		{
			get { return _entity.AccountCode; }
		}
        /// <summary>
        /// Gets the Attn
        /// </summary>
        /// <value>The Attn.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Attn
		{
			get { return _entity.Attn; }
		}
        /// <summary>
        /// Gets the AttnEmail
        /// </summary>
        /// <value>The AttnEmail.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String AttnEmail
		{
			get { return _entity.AttnEmail; }
		}
        /// <summary>
        /// Gets the AttnPhone
        /// </summary>
        /// <value>The AttnPhone.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String AttnPhone
		{
			get { return _entity.AttnPhone; }
		}
        /// <summary>
        /// Gets the PaymentMode
        /// </summary>
        /// <value>The PaymentMode.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String PaymentMode
		{
			get { return _entity.PaymentMode; }
		}
        /// <summary>
        /// Gets the CreateUser
        /// </summary>
        /// <value>The CreateUser.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String CreateUser
		{
			get { return _entity.CreateUser; }
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
        /// Gets the TypeOfServices
        /// </summary>
        /// <value>The TypeOfServices.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String TypeOfServices
		{
			get { return _entity.TypeOfServices; }
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
