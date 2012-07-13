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
    /// A designer class for a strongly typed repeater <c>PatientRepeater</c>
    /// </summary>
	public class PatientRepeaterDesigner : System.Web.UI.Design.ControlDesigner
	{
	    /// <summary>
        /// Initializes a new instance of the <see cref="T:PatientRepeaterDesigner"/> class.
        /// </summary>
		public PatientRepeaterDesigner()
		{
		}

        /// <summary>
        /// Initializes the control designer and loads the specified component.
        /// </summary>
        /// <param name="component">The control being designed.</param>
		public override void Initialize(IComponent component)
		{
			if (!(component is PatientRepeater))
			{ 
				throw new ArgumentException("Component is not a PatientRepeater."); 
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
			PatientRepeater z = (PatientRepeater)Component;
			z.DataBind();

			return base.GetDesignTimeHtml();

			//return z.RenderAtDesignTime();

			//	ControlCollection c = z.Controls;
			//Totem z = (Totem) Component;
			//Totem z = (Totem) Component;
			//return ("<div style='border: 1px gray dotted; background-color: lightgray'><b>TagStat :</b> zae |  qsdds</div>");

		}
	}

    /// <summary>
    /// A strongly typed repeater control for the <c cref="PatientRepeater"></c> Type.
    /// </summary>
	[Designer(typeof(PatientRepeaterDesigner))]
	[ParseChildren(true)]
	[ToolboxData("<{0}:PatientRepeater runat=\"server\"></{0}:PatientRepeater>")]
	public class PatientRepeater : CompositeDataBoundControl, System.Web.UI.INamingContainer
	{
	    /// <summary>
        /// Initializes a new instance of the <see cref="T:PatientRepeater"/> class.
        /// </summary>
		public PatientRepeater()
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
		[TemplateContainer(typeof(PatientItem))]
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
		[TemplateContainer(typeof(PatientItem))]
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
        [TemplateContainer(typeof(PatientItem))]
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
		[TemplateContainer(typeof(PatientItem))]
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
		[TemplateContainer(typeof(PatientItem))]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public ITemplate FooterTemplate
		{
			get { return m_footerTemplate; }
			set { m_footerTemplate = value; }
		}

//      /// <summary>
//      /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
//      /// </summary>
//		protected override void CreateChildControls()
//      {
//         if (ChildControlsCreated)
//         {
//            return;
//         }

//         Controls.Clear();

//         //Instantiate the Header template (if exists)
//         if (m_headerTemplate != null)
//         {
//            Control headerItem = new Control();
//            m_headerTemplate.InstantiateIn(headerItem);
//            Controls.Add(headerItem);
//         }

//         //Instantiate the Footer template (if exists)
//         if (m_footerTemplate != null)
//         {
//            Control footerItem = new Control();
//            m_footerTemplate.InstantiateIn(footerItem);
//            Controls.Add(footerItem);
//         }
//
//         ChildControlsCreated = true;
//      }
	
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
						AppointmentSystem.Entities.Patient entity = o as AppointmentSystem.Entities.Patient;
						PatientItem container = new PatientItem(entity);
	
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
	public class PatientItem : System.Web.UI.Control, System.Web.UI.INamingContainer
	{
		private AppointmentSystem.Entities.Patient _entity;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PatientItem"/> class.
        /// </summary>
		public PatientItem()
			: base()
		{ }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PatientItem"/> class.
        /// </summary>
		public PatientItem(AppointmentSystem.Entities.Patient entity)
			: base()
		{
			_entity = entity;
		}
		
        /// <summary>
        /// Gets the Id
        /// </summary>
        /// <value>The Id.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Id
		{
			get { return _entity.Id; }
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
        /// Gets the LastName
        /// </summary>
        /// <value>The LastName.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String LastName
		{
			get { return _entity.LastName; }
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
        /// Gets the HomePhone
        /// </summary>
        /// <value>The HomePhone.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String HomePhone
		{
			get { return _entity.HomePhone; }
		}
        /// <summary>
        /// Gets the WorkPhone
        /// </summary>
        /// <value>The WorkPhone.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String WorkPhone
		{
			get { return _entity.WorkPhone; }
		}
        /// <summary>
        /// Gets the CellPhone
        /// </summary>
        /// <value>The CellPhone.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String CellPhone
		{
			get { return _entity.CellPhone; }
		}
        /// <summary>
        /// Gets the Avatar
        /// </summary>
        /// <value>The Avatar.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Avatar
		{
			get { return _entity.Avatar; }
		}
        /// <summary>
        /// Gets the Birthdate
        /// </summary>
        /// <value>The Birthdate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Birthdate
		{
			get { return _entity.Birthdate; }
		}
        /// <summary>
        /// Gets the IsFemale
        /// </summary>
        /// <value>The IsFemale.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Boolean IsFemale
		{
			get { return _entity.IsFemale; }
		}
        /// <summary>
        /// Gets the Title
        /// </summary>
        /// <value>The Title.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Title
		{
			get { return _entity.Title; }
		}
        /// <summary>
        /// Gets the Note
        /// </summary>
        /// <value>The Note.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Note
		{
			get { return _entity.Note; }
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
        /// Gets a <see cref="T:AppointmentSystem.Entities.Patient"></see> object
        /// </summary>
        /// <value></value>
        [System.ComponentModel.Bindable(true)]
        public AppointmentSystem.Entities.Patient Entity
        {
            get { return _entity; }
        }
	}
}
