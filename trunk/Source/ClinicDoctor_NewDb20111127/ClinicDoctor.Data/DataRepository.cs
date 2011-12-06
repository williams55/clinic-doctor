#region Using directives

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Provider;
using System.Web.Configuration;
using System.Web;
using ClinicDoctor.Entities;
using ClinicDoctor.Data;
using ClinicDoctor.Data.Bases;

#endregion

namespace ClinicDoctor.Data
{
	/// <summary>
	/// This class represents the Data source repository and gives access to all the underlying providers.
	/// </summary>
	[CLSCompliant(true)]
	public sealed class DataRepository 
	{
		private static volatile NetTiersProvider _provider = null;
        private static volatile NetTiersProviderCollection _providers = null;
		private static volatile NetTiersServiceSection _section = null;
		private static volatile Configuration _config = null;
        
        private static object SyncRoot = new object();
				
		private DataRepository()
		{
		}
		
		#region Public LoadProvider
		/// <summary>
        /// Enables the DataRepository to programatically create and 
        /// pass in a <c>NetTiersProvider</c> during runtime.
        /// </summary>
        /// <param name="provider">An instatiated NetTiersProvider.</param>
        public static void LoadProvider(NetTiersProvider provider)
        {
			LoadProvider(provider, false);
        }
		
		/// <summary>
        /// Enables the DataRepository to programatically create and 
        /// pass in a <c>NetTiersProvider</c> during runtime.
        /// </summary>
        /// <param name="provider">An instatiated NetTiersProvider.</param>
        /// <param name="setAsDefault">ability to set any valid provider as the default provider for the DataRepository.</param>
		public static void LoadProvider(NetTiersProvider provider, bool setAsDefault)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");

            if (_providers == null)
			{
				lock(SyncRoot)
				{
            		if (_providers == null)
						_providers = new NetTiersProviderCollection();
				}
			}
			
            if (_providers[provider.Name] == null)
            {
                lock (_providers.SyncRoot)
                {
                    _providers.Add(provider);
                }
            }

            if (_provider == null || setAsDefault)
            {
                lock (SyncRoot)
                {
                    if(_provider == null || setAsDefault)
                         _provider = provider;
                }
            }
        }
		#endregion 
		
		///<summary>
		/// Configuration based provider loading, will load the providers on first call.
		///</summary>
		private static void LoadProviders()
        {
            // Avoid claiming lock if providers are already loaded
            if (_provider == null)
            {
                lock (SyncRoot)
                {
                    // Do this again to make sure _provider is still null
                    if (_provider == null)
                    {
                        // Load registered providers and point _provider to the default provider
                        _providers = new NetTiersProviderCollection();

                        ProvidersHelper.InstantiateProviders(NetTiersSection.Providers, _providers, typeof(NetTiersProvider));
						_provider = _providers[NetTiersSection.DefaultProvider];

                        if (_provider == null)
                        {
                            throw new ProviderException("Unable to load default NetTiersProvider");
                        }
                    }
                }
            }
        }

		/// <summary>
        /// Gets the provider.
        /// </summary>
        /// <value>The provider.</value>
        public static NetTiersProvider Provider
        {
            get { LoadProviders(); return _provider; }
        }

		/// <summary>
        /// Gets the provider collection.
        /// </summary>
        /// <value>The providers.</value>
        public static NetTiersProviderCollection Providers
        {
            get { LoadProviders(); return _providers; }
        }
		
		/// <summary>
		/// Creates a new <c cref="TransactionManager"/> instance from the current datasource.
		/// </summary>
		/// <returns></returns>
		public TransactionManager CreateTransaction()
		{
			return _provider.CreateTransaction();
		}

		#region Configuration

		/// <summary>
		/// Gets a reference to the configured NetTiersServiceSection object.
		/// </summary>
		public static NetTiersServiceSection NetTiersSection
		{
			get
			{
				// Try to get a reference to the default <netTiersService> section
				_section = WebConfigurationManager.GetSection("netTiersService") as NetTiersServiceSection;

				if ( _section == null )
				{
					// otherwise look for section based on the assembly name
					_section = WebConfigurationManager.GetSection("ClinicDoctor.Data") as NetTiersServiceSection;
				}

				#region Design-Time Support

				if ( _section == null )
				{
					// lastly, try to find the specific NetTiersServiceSection for this assembly
					foreach ( ConfigurationSection temp in Configuration.Sections )
					{
						if ( temp is NetTiersServiceSection )
						{
							_section = temp as NetTiersServiceSection;
							break;
						}
					}
				}

				#endregion Design-Time Support
				
				if ( _section == null )
				{
					throw new ProviderException("Unable to load NetTiersServiceSection");
				}

				return _section;
			}
		}

		#region Design-Time Support

		/// <summary>
		/// Gets a reference to the application configuration object.
		/// </summary>
		public static Configuration Configuration
		{
			get
			{
				if ( _config == null )
				{
					// load specific config file
					if ( HttpContext.Current != null )
					{
						_config = WebConfigurationManager.OpenWebConfiguration("~");
					}
					else
					{
						String configFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile.Replace(".config", "").Replace(".temp", "");

						// check for design mode
						if ( configFile.ToLower().Contains("devenv.exe") )
						{
							_config = GetDesignTimeConfig();
						}
						else
						{
							_config = ConfigurationManager.OpenExeConfiguration(configFile);
						}
					}
				}

				return _config;
			}
		}

		private static Configuration GetDesignTimeConfig()
		{
			ExeConfigurationFileMap configMap = null;
			Configuration config = null;
			String path = null;

			// Get an instance of the currently running Visual Studio IDE.
			EnvDTE80.DTE2 dte = (EnvDTE80.DTE2) System.Runtime.InteropServices.Marshal.GetActiveObject("VisualStudio.DTE.9.0");
			
			if ( dte != null )
			{
				dte.SuppressUI = true;

				EnvDTE.ProjectItem item = dte.Solution.FindProjectItem("web.config");
				if ( item != null )
				{
					if (!item.ContainingProject.FullName.ToLower().StartsWith("http:"))
               {
                  System.IO.FileInfo info = new System.IO.FileInfo(item.ContainingProject.FullName);
                  path = String.Format("{0}\\{1}", info.Directory.FullName, item.Name);
                  configMap = new ExeConfigurationFileMap();
                  configMap.ExeConfigFilename = path;
               }
               else
               {
                  configMap = new ExeConfigurationFileMap();
                  configMap.ExeConfigFilename = item.get_FileNames(0);
               }}

				/*
				Array projects = (Array) dte2.ActiveSolutionProjects;
				EnvDTE.Project project = (EnvDTE.Project) projects.GetValue(0);
				System.IO.FileInfo info;

				foreach ( EnvDTE.ProjectItem item in project.ProjectItems )
				{
					if ( String.Compare(item.Name, "web.config", true) == 0 )
					{
						info = new System.IO.FileInfo(project.FullName);
						path = String.Format("{0}\\{1}", info.Directory.FullName, item.Name);
						configMap = new ExeConfigurationFileMap();
						configMap.ExeConfigFilename = path;
						break;
					}
				}
				*/
			}

			config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
			return config;
		}

		#endregion Design-Time Support

		#endregion Configuration

		#region Connections

		/// <summary>
		/// Gets a reference to the ConnectionStringSettings collection.
		/// </summary>
		public static ConnectionStringSettingsCollection ConnectionStrings
		{
			get
			{
				// use default ConnectionStrings if _section has already been discovered
				if ( _config == null && _section != null )
				{
					return WebConfigurationManager.ConnectionStrings;
				}
				
				return Configuration.ConnectionStrings.ConnectionStrings;
			}
		}

		// dictionary of connection providers
		private static Dictionary<String, ConnectionProvider> _connections;

		/// <summary>
		/// Gets the dictionary of connection providers.
		/// </summary>
		public static Dictionary<String, ConnectionProvider> Connections
		{
			get
			{
				if ( _connections == null )
				{
					lock (SyncRoot)
                	{
						if (_connections == null)
						{
							_connections = new Dictionary<String, ConnectionProvider>();
		
							// add a connection provider for each configured connection string
							foreach ( ConnectionStringSettings conn in ConnectionStrings )
							{
								_connections.Add(conn.Name, new ConnectionProvider(conn.Name, conn.ConnectionString));
							}
						}
					}
				}

				return _connections;
			}
		}

		/// <summary>
		/// Adds the specified connection string to the map of connection strings.
		/// </summary>
		/// <param name="connectionStringName">The connection string name.</param>
		/// <param name="connectionString">The provider specific connection information.</param>
		public static void AddConnection(String connectionStringName, String connectionString)
		{
			lock (SyncRoot)
            {
				Connections.Remove(connectionStringName);
				ConnectionProvider connection = new ConnectionProvider(connectionStringName, connectionString);
				Connections.Add(connectionStringName, connection);
			}
		}

		/// <summary>
		/// Provides ability to switch connection string at runtime.
		/// </summary>
		public sealed class ConnectionProvider
		{
			private NetTiersProvider _provider;
			private NetTiersProviderCollection _providers;
			private String _connectionStringName;
			private String _connectionString;


			/// <summary>
			/// Initializes a new instance of the ConnectionProvider class.
			/// </summary>
			/// <param name="connectionStringName">The connection string name.</param>
			/// <param name="connectionString">The provider specific connection information.</param>
			public ConnectionProvider(String connectionStringName, String connectionString)
			{
				_connectionString = connectionString;
				_connectionStringName = connectionStringName;
			}

			/// <summary>
			/// Gets the provider.
			/// </summary>
			public NetTiersProvider Provider
			{
				get { LoadProviders(); return _provider; }
			}

			/// <summary>
			/// Gets the provider collection.
			/// </summary>
			public NetTiersProviderCollection Providers
			{
				get { LoadProviders(); return _providers; }
			}

			/// <summary>
			/// Instantiates the configured providers based on the supplied connection string.
			/// </summary>
			private void LoadProviders()
			{
				DataRepository.LoadProviders();

				// Avoid claiming lock if providers are already loaded
				if ( _providers == null )
				{
					lock ( SyncRoot )
					{
						// Do this again to make sure _provider is still null
						if ( _providers == null )
						{
							// apply connection information to each provider
							for ( int i = 0; i < NetTiersSection.Providers.Count; i++ )
							{
								NetTiersSection.Providers[i].Parameters["connectionStringName"] = _connectionStringName;
								// remove previous connection string, if any
								NetTiersSection.Providers[i].Parameters.Remove("connectionString");

								if ( !String.IsNullOrEmpty(_connectionString) )
								{
									NetTiersSection.Providers[i].Parameters["connectionString"] = _connectionString;
								}
							}

							// Load registered providers and point _provider to the default provider
							_providers = new NetTiersProviderCollection();

							ProvidersHelper.InstantiateProviders(NetTiersSection.Providers, _providers, typeof(NetTiersProvider));
							_provider = _providers[NetTiersSection.DefaultProvider];
						}
					}
				}
			}
		}

		#endregion Connections

		#region Static properties
		
		#region RoomProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Room"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static RoomProviderBase RoomProvider
		{
			get 
			{
				LoadProviders();
				return _provider.RoomProvider;
			}
		}
		
		#endregion
		
		#region AppointmentProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Appointment"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static AppointmentProviderBase AppointmentProvider
		{
			get 
			{
				LoadProviders();
				return _provider.AppointmentProvider;
			}
		}
		
		#endregion
		
		#region RoomFuncProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="RoomFunc"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static RoomFuncProviderBase RoomFuncProvider
		{
			get 
			{
				LoadProviders();
				return _provider.RoomFuncProvider;
			}
		}
		
		#endregion
		
		#region RosterTypeProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="RosterType"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static RosterTypeProviderBase RosterTypeProvider
		{
			get 
			{
				LoadProviders();
				return _provider.RosterTypeProvider;
			}
		}
		
		#endregion
		
		#region StaffProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Staff"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static StaffProviderBase StaffProvider
		{
			get 
			{
				LoadProviders();
				return _provider.StaffProvider;
			}
		}
		
		#endregion
		
		#region FunctionalityProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Functionality"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static FunctionalityProviderBase FunctionalityProvider
		{
			get 
			{
				LoadProviders();
				return _provider.FunctionalityProvider;
			}
		}
		
		#endregion
		
		#region DoctorRosterProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="DoctorRoster"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static DoctorRosterProviderBase DoctorRosterProvider
		{
			get 
			{
				LoadProviders();
				return _provider.DoctorRosterProvider;
			}
		}
		
		#endregion
		
		#region ContentProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Content"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static ContentProviderBase ContentProvider
		{
			get 
			{
				LoadProviders();
				return _provider.ContentProvider;
			}
		}
		
		#endregion
		
		#region CustomerProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Customer"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static CustomerProviderBase CustomerProvider
		{
			get 
			{
				LoadProviders();
				return _provider.CustomerProvider;
			}
		}
		
		#endregion
		
		#region DoctorFuncProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="DoctorFunc"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static DoctorFuncProviderBase DoctorFuncProvider
		{
			get 
			{
				LoadProviders();
				return _provider.DoctorFuncProvider;
			}
		}
		
		#endregion
		
		#region DoctorRoomProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="DoctorRoom"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static DoctorRoomProviderBase DoctorRoomProvider
		{
			get 
			{
				LoadProviders();
				return _provider.DoctorRoomProvider;
			}
		}
		
		#endregion
		
		#region StatusProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Status"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static StatusProviderBase StatusProvider
		{
			get 
			{
				LoadProviders();
				return _provider.StatusProvider;
			}
		}
		
		#endregion
		
		
		#endregion
	}
	
	#region Query/Filters
		
	#region RoomFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Room"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class RoomFilters : RoomFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the RoomFilters class.
		/// </summary>
		public RoomFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the RoomFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public RoomFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the RoomFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public RoomFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion RoomFilters
	
	#region RoomQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="RoomParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Room"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class RoomQuery : RoomParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the RoomQuery class.
		/// </summary>
		public RoomQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the RoomQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public RoomQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the RoomQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public RoomQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion RoomQuery
		
	#region AppointmentFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Appointment"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class AppointmentFilters : AppointmentFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the AppointmentFilters class.
		/// </summary>
		public AppointmentFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the AppointmentFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public AppointmentFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the AppointmentFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public AppointmentFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion AppointmentFilters
	
	#region AppointmentQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="AppointmentParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Appointment"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class AppointmentQuery : AppointmentParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the AppointmentQuery class.
		/// </summary>
		public AppointmentQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the AppointmentQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public AppointmentQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the AppointmentQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public AppointmentQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion AppointmentQuery
		
	#region RoomFuncFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="RoomFunc"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class RoomFuncFilters : RoomFuncFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the RoomFuncFilters class.
		/// </summary>
		public RoomFuncFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the RoomFuncFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public RoomFuncFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the RoomFuncFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public RoomFuncFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion RoomFuncFilters
	
	#region RoomFuncQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="RoomFuncParameterBuilder"/> class
	/// that is used exclusively with a <see cref="RoomFunc"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class RoomFuncQuery : RoomFuncParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the RoomFuncQuery class.
		/// </summary>
		public RoomFuncQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the RoomFuncQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public RoomFuncQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the RoomFuncQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public RoomFuncQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion RoomFuncQuery
		
	#region RosterTypeFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="RosterType"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class RosterTypeFilters : RosterTypeFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the RosterTypeFilters class.
		/// </summary>
		public RosterTypeFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the RosterTypeFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public RosterTypeFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the RosterTypeFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public RosterTypeFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion RosterTypeFilters
	
	#region RosterTypeQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="RosterTypeParameterBuilder"/> class
	/// that is used exclusively with a <see cref="RosterType"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class RosterTypeQuery : RosterTypeParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the RosterTypeQuery class.
		/// </summary>
		public RosterTypeQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the RosterTypeQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public RosterTypeQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the RosterTypeQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public RosterTypeQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion RosterTypeQuery
		
	#region StaffFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Staff"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class StaffFilters : StaffFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the StaffFilters class.
		/// </summary>
		public StaffFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the StaffFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public StaffFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the StaffFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public StaffFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion StaffFilters
	
	#region StaffQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="StaffParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Staff"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class StaffQuery : StaffParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the StaffQuery class.
		/// </summary>
		public StaffQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the StaffQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public StaffQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the StaffQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public StaffQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion StaffQuery
		
	#region FunctionalityFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Functionality"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class FunctionalityFilters : FunctionalityFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the FunctionalityFilters class.
		/// </summary>
		public FunctionalityFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the FunctionalityFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public FunctionalityFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the FunctionalityFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public FunctionalityFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion FunctionalityFilters
	
	#region FunctionalityQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="FunctionalityParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Functionality"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class FunctionalityQuery : FunctionalityParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the FunctionalityQuery class.
		/// </summary>
		public FunctionalityQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the FunctionalityQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public FunctionalityQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the FunctionalityQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public FunctionalityQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion FunctionalityQuery
		
	#region DoctorRosterFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="DoctorRoster"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class DoctorRosterFilters : DoctorRosterFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DoctorRosterFilters class.
		/// </summary>
		public DoctorRosterFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the DoctorRosterFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public DoctorRosterFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the DoctorRosterFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public DoctorRosterFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion DoctorRosterFilters
	
	#region DoctorRosterQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="DoctorRosterParameterBuilder"/> class
	/// that is used exclusively with a <see cref="DoctorRoster"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class DoctorRosterQuery : DoctorRosterParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DoctorRosterQuery class.
		/// </summary>
		public DoctorRosterQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the DoctorRosterQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public DoctorRosterQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the DoctorRosterQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public DoctorRosterQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion DoctorRosterQuery
		
	#region ContentFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Content"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ContentFilters : ContentFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ContentFilters class.
		/// </summary>
		public ContentFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the ContentFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ContentFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ContentFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ContentFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ContentFilters
	
	#region ContentQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ContentParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Content"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ContentQuery : ContentParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ContentQuery class.
		/// </summary>
		public ContentQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the ContentQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ContentQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ContentQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ContentQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ContentQuery
		
	#region CustomerFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Customer"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class CustomerFilters : CustomerFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CustomerFilters class.
		/// </summary>
		public CustomerFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the CustomerFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public CustomerFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the CustomerFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public CustomerFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion CustomerFilters
	
	#region CustomerQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="CustomerParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Customer"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class CustomerQuery : CustomerParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CustomerQuery class.
		/// </summary>
		public CustomerQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the CustomerQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public CustomerQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the CustomerQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public CustomerQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion CustomerQuery
		
	#region DoctorFuncFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="DoctorFunc"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class DoctorFuncFilters : DoctorFuncFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DoctorFuncFilters class.
		/// </summary>
		public DoctorFuncFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the DoctorFuncFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public DoctorFuncFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the DoctorFuncFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public DoctorFuncFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion DoctorFuncFilters
	
	#region DoctorFuncQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="DoctorFuncParameterBuilder"/> class
	/// that is used exclusively with a <see cref="DoctorFunc"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class DoctorFuncQuery : DoctorFuncParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DoctorFuncQuery class.
		/// </summary>
		public DoctorFuncQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the DoctorFuncQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public DoctorFuncQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the DoctorFuncQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public DoctorFuncQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion DoctorFuncQuery
		
	#region DoctorRoomFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="DoctorRoom"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class DoctorRoomFilters : DoctorRoomFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DoctorRoomFilters class.
		/// </summary>
		public DoctorRoomFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the DoctorRoomFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public DoctorRoomFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the DoctorRoomFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public DoctorRoomFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion DoctorRoomFilters
	
	#region DoctorRoomQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="DoctorRoomParameterBuilder"/> class
	/// that is used exclusively with a <see cref="DoctorRoom"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class DoctorRoomQuery : DoctorRoomParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DoctorRoomQuery class.
		/// </summary>
		public DoctorRoomQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the DoctorRoomQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public DoctorRoomQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the DoctorRoomQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public DoctorRoomQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion DoctorRoomQuery
		
	#region StatusFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Status"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class StatusFilters : StatusFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the StatusFilters class.
		/// </summary>
		public StatusFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the StatusFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public StatusFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the StatusFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public StatusFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion StatusFilters
	
	#region StatusQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="StatusParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Status"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class StatusQuery : StatusParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the StatusQuery class.
		/// </summary>
		public StatusQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the StatusQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public StatusQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the StatusQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public StatusQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion StatusQuery
	#endregion

	
}
