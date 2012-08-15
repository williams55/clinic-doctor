#region Using directives

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Provider;
using System.Web.Configuration;
using System.Web;
using AppointmentSystem.Entities;
using AppointmentSystem.Data;
using AppointmentSystem.Data.Bases;

#endregion

namespace AppointmentSystem.Data
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
					_section = WebConfigurationManager.GetSection("AppointmentSystem.Data") as NetTiersServiceSection;
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
		
		#region ServicesProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Services"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static ServicesProviderBase ServicesProvider
		{
			get 
			{
				LoadProviders();
				return _provider.ServicesProvider;
			}
		}
		
		#endregion
		
		#region UsersProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Users"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static UsersProviderBase UsersProvider
		{
			get 
			{
				LoadProviders();
				return _provider.UsersProvider;
			}
		}
		
		#endregion
		
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
		
		#region ScreenProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Screen"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static ScreenProviderBase ScreenProvider
		{
			get 
			{
				LoadProviders();
				return _provider.ScreenProvider;
			}
		}
		
		#endregion
		
		#region UnitsProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Units"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static UnitsProviderBase UnitsProvider
		{
			get 
			{
				LoadProviders();
				return _provider.UnitsProvider;
			}
		}
		
		#endregion
		
		#region UserGroupProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="UserGroup"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static UserGroupProviderBase UserGroupProvider
		{
			get 
			{
				LoadProviders();
				return _provider.UserGroupProvider;
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
		
		#region UserRoleProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="UserRole"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static UserRoleProviderBase UserRoleProvider
		{
			get 
			{
				LoadProviders();
				return _provider.UserRoleProvider;
			}
		}
		
		#endregion
		
		#region RosterProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Roster"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static RosterProviderBase RosterProvider
		{
			get 
			{
				LoadProviders();
				return _provider.RosterProvider;
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
		
		#region GroupRoleProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="GroupRole"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static GroupRoleProviderBase GroupRoleProvider
		{
			get 
			{
				LoadProviders();
				return _provider.GroupRoleProvider;
			}
		}
		
		#endregion
		
		#region PatientProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Patient"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static PatientProviderBase PatientProvider
		{
			get 
			{
				LoadProviders();
				return _provider.PatientProvider;
			}
		}
		
		#endregion
		
		#region RoleProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Role"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static RoleProviderBase RoleProvider
		{
			get 
			{
				LoadProviders();
				return _provider.RoleProvider;
			}
		}
		
		#endregion
		
		#region RoleDetailProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="RoleDetail"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static RoleDetailProviderBase RoleDetailProvider
		{
			get 
			{
				LoadProviders();
				return _provider.RoleDetailProvider;
			}
		}
		
		#endregion
		
		#region AppointmentGroupProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="AppointmentGroup"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static AppointmentGroupProviderBase AppointmentGroupProvider
		{
			get 
			{
				LoadProviders();
				return _provider.AppointmentGroupProvider;
			}
		}
		
		#endregion
		
		
		#endregion
	}
	
	#region Query/Filters
		
	#region ServicesFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Services"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ServicesFilters : ServicesFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ServicesFilters class.
		/// </summary>
		public ServicesFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the ServicesFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ServicesFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ServicesFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ServicesFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ServicesFilters
	
	#region ServicesQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ServicesParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Services"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ServicesQuery : ServicesParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ServicesQuery class.
		/// </summary>
		public ServicesQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the ServicesQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ServicesQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ServicesQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ServicesQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ServicesQuery
		
	#region UsersFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Users"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class UsersFilters : UsersFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the UsersFilters class.
		/// </summary>
		public UsersFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the UsersFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public UsersFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the UsersFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public UsersFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion UsersFilters
	
	#region UsersQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="UsersParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Users"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class UsersQuery : UsersParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the UsersQuery class.
		/// </summary>
		public UsersQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the UsersQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public UsersQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the UsersQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public UsersQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion UsersQuery
		
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
		
	#region ScreenFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Screen"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ScreenFilters : ScreenFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ScreenFilters class.
		/// </summary>
		public ScreenFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the ScreenFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ScreenFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ScreenFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ScreenFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ScreenFilters
	
	#region ScreenQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ScreenParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Screen"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ScreenQuery : ScreenParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ScreenQuery class.
		/// </summary>
		public ScreenQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the ScreenQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ScreenQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ScreenQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ScreenQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ScreenQuery
		
	#region UnitsFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Units"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class UnitsFilters : UnitsFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the UnitsFilters class.
		/// </summary>
		public UnitsFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the UnitsFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public UnitsFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the UnitsFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public UnitsFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion UnitsFilters
	
	#region UnitsQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="UnitsParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Units"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class UnitsQuery : UnitsParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the UnitsQuery class.
		/// </summary>
		public UnitsQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the UnitsQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public UnitsQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the UnitsQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public UnitsQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion UnitsQuery
		
	#region UserGroupFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="UserGroup"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class UserGroupFilters : UserGroupFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the UserGroupFilters class.
		/// </summary>
		public UserGroupFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the UserGroupFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public UserGroupFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the UserGroupFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public UserGroupFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion UserGroupFilters
	
	#region UserGroupQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="UserGroupParameterBuilder"/> class
	/// that is used exclusively with a <see cref="UserGroup"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class UserGroupQuery : UserGroupParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the UserGroupQuery class.
		/// </summary>
		public UserGroupQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the UserGroupQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public UserGroupQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the UserGroupQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public UserGroupQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion UserGroupQuery
		
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
		
	#region UserRoleFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="UserRole"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class UserRoleFilters : UserRoleFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the UserRoleFilters class.
		/// </summary>
		public UserRoleFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the UserRoleFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public UserRoleFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the UserRoleFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public UserRoleFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion UserRoleFilters
	
	#region UserRoleQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="UserRoleParameterBuilder"/> class
	/// that is used exclusively with a <see cref="UserRole"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class UserRoleQuery : UserRoleParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the UserRoleQuery class.
		/// </summary>
		public UserRoleQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the UserRoleQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public UserRoleQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the UserRoleQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public UserRoleQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion UserRoleQuery
		
	#region RosterFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Roster"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class RosterFilters : RosterFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the RosterFilters class.
		/// </summary>
		public RosterFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the RosterFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public RosterFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the RosterFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public RosterFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion RosterFilters
	
	#region RosterQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="RosterParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Roster"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class RosterQuery : RosterParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the RosterQuery class.
		/// </summary>
		public RosterQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the RosterQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public RosterQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the RosterQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public RosterQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion RosterQuery
		
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
		
	#region GroupRoleFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="GroupRole"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class GroupRoleFilters : GroupRoleFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the GroupRoleFilters class.
		/// </summary>
		public GroupRoleFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the GroupRoleFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public GroupRoleFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the GroupRoleFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public GroupRoleFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion GroupRoleFilters
	
	#region GroupRoleQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="GroupRoleParameterBuilder"/> class
	/// that is used exclusively with a <see cref="GroupRole"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class GroupRoleQuery : GroupRoleParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the GroupRoleQuery class.
		/// </summary>
		public GroupRoleQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the GroupRoleQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public GroupRoleQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the GroupRoleQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public GroupRoleQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion GroupRoleQuery
		
	#region PatientFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Patient"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PatientFilters : PatientFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PatientFilters class.
		/// </summary>
		public PatientFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the PatientFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PatientFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PatientFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PatientFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PatientFilters
	
	#region PatientQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="PatientParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Patient"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PatientQuery : PatientParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PatientQuery class.
		/// </summary>
		public PatientQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the PatientQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PatientQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PatientQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PatientQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PatientQuery
		
	#region RoleFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Role"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class RoleFilters : RoleFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the RoleFilters class.
		/// </summary>
		public RoleFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the RoleFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public RoleFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the RoleFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public RoleFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion RoleFilters
	
	#region RoleQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="RoleParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Role"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class RoleQuery : RoleParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the RoleQuery class.
		/// </summary>
		public RoleQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the RoleQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public RoleQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the RoleQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public RoleQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion RoleQuery
		
	#region RoleDetailFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="RoleDetail"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class RoleDetailFilters : RoleDetailFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the RoleDetailFilters class.
		/// </summary>
		public RoleDetailFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the RoleDetailFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public RoleDetailFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the RoleDetailFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public RoleDetailFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion RoleDetailFilters
	
	#region RoleDetailQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="RoleDetailParameterBuilder"/> class
	/// that is used exclusively with a <see cref="RoleDetail"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class RoleDetailQuery : RoleDetailParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the RoleDetailQuery class.
		/// </summary>
		public RoleDetailQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the RoleDetailQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public RoleDetailQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the RoleDetailQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public RoleDetailQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion RoleDetailQuery
		
	#region AppointmentGroupFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="AppointmentGroup"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class AppointmentGroupFilters : AppointmentGroupFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the AppointmentGroupFilters class.
		/// </summary>
		public AppointmentGroupFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the AppointmentGroupFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public AppointmentGroupFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the AppointmentGroupFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public AppointmentGroupFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion AppointmentGroupFilters
	
	#region AppointmentGroupQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="AppointmentGroupParameterBuilder"/> class
	/// that is used exclusively with a <see cref="AppointmentGroup"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class AppointmentGroupQuery : AppointmentGroupParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the AppointmentGroupQuery class.
		/// </summary>
		public AppointmentGroupQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the AppointmentGroupQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public AppointmentGroupQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the AppointmentGroupQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public AppointmentGroupQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion AppointmentGroupQuery
	#endregion

	
}
