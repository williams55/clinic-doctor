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
    /// A designer class for a strongly typed repeater <c>RosterRepeater</c>
    /// </summary>
	public class RosterRepeaterDesigner : System.Web.UI.Design.ControlDesigner
	{
	    /// <summary>
        /// Initializes a new instance of the <see cref="T:RosterRepeaterDesigner"/> class.
        /// </summary>
		public RosterRepeaterDesigner()
		{
		}

        /// <summary>
        /// Initializes the control designer and loads the specified component.
        /// </summary>
        /// <param name="component">The control being designed.</param>
		public override void Initialize(IComponent component)
		{
			if (!(component is RosterRepeater))
			{ 
				throw new ArgumentException("Component is not a RosterRepeater."); 
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
			RosterRepeater z = (RosterRepeater)Component;
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
    /// A strongly typed repeater control for the <c cref="RosterRepeater"></c> Type.
    /// </summary>
	[Designer(typeof(RosterRepeaterDesigner))]
	[ParseChildren(true)]
	[ToolboxData("<{0}:RosterRepeater runat=\"server\"></{0}:RosterRepeater>")]
	public class RosterRepeater : CompositeDataBoundControl, System.Web.UI.INamingContainer
	{
	    /// <summary>
        /// Initializes a new instance of the <see cref="T:RosterRepeater"/> class.
        /// </summary>
		public RosterRepeater()
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
		[TemplateContainer(typeof(RosterItem))]
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
		[TemplateContainer(typeof(RosterItem))]
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
        [TemplateContainer(typeof(RosterItem))]
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
		[TemplateContainer(typeof(RosterItem))]
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
		[TemplateContainer(typeof(RosterItem))]
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
						AppointmentSystem.Entities.Roster entity = o as AppointmentSystem.Entities.Roster;
						RosterItem container = new RosterItem(entity);
	
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
	public class RosterItem : System.Web.UI.Control, System.Web.UI.INamingContainer
	{
		private AppointmentSystem.Entities.Roster _entity;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:RosterItem"/> class.
        /// </summary>
		public RosterItem()
			: base()
		{ }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:RosterItem"/> class.
        /// </summary>
		public RosterItem(AppointmentSystem.Entities.Roster entity)
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
        /// Gets the Username
        /// </summary>
        /// <value>The Username.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Username
		{
			get { return _entity.Username; }
		}
        /// <summary>
        /// Gets the RoomId
        /// </summary>
        /// <value>The RoomId.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? RoomId
		{
			get { return _entity.RoomId; }
		}
        /// <summary>
        /// Gets the RosterTypeId
        /// </summary>
        /// <value>The RosterTypeId.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32 RosterTypeId
		{
			get { return _entity.RosterTypeId; }
		}
        /// <summary>
        /// Gets the StartTime
        /// </summary>
        /// <value>The StartTime.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime StartTime
		{
			get { return _entity.StartTime; }
		}
        /// <summary>
        /// Gets the EndTime
        /// </summary>
        /// <value>The EndTime.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime EndTime
		{
			get { return _entity.EndTime; }
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
        /// Gets the RepeatId
        /// </summary>
        /// <value>The RepeatId.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Guid? RepeatId
		{
			get { return _entity.RepeatId; }
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
        /// Gets a <see cref="T:AppointmentSystem.Entities.Roster"></see> object
        /// </summary>
        /// <value></value>
        [System.ComponentModel.Bindable(true)]
        public AppointmentSystem.Entities.Roster Entity
        {
            get { return _entity; }
        }
	}
}
