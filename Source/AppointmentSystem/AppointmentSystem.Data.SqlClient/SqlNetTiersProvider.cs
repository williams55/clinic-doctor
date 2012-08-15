
#region Using directives

using System;
using System.Collections;
using System.Collections.Specialized;


using System.Web.Configuration;
using System.Data;
using System.Data.Common;
using System.Configuration.Provider;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

using AppointmentSystem.Entities;
using AppointmentSystem.Data;
using AppointmentSystem.Data.Bases;

#endregion

namespace AppointmentSystem.Data.SqlClient
{
	/// <summary>
	/// This class is the Sql implementation of the NetTiersProvider.
	/// </summary>
	public sealed class SqlNetTiersProvider : AppointmentSystem.Data.Bases.NetTiersProvider
	{
		private static object syncRoot = new Object();
		private string _applicationName;
        private string _connectionString;
        private bool _useStoredProcedure;
        string _providerInvariantName;
		
		/// <summary>
		/// Initializes a new instance of the <see cref="SqlNetTiersProvider"/> class.
		///</summary>
		public SqlNetTiersProvider()
		{	
		}		
		
		/// <summary>
        /// Initializes the provider.
        /// </summary>
        /// <param name="name">The friendly name of the provider.</param>
        /// <param name="config">A collection of the name/value pairs representing the provider-specific attributes specified in the configuration for this provider.</param>
        /// <exception cref="T:System.ArgumentNullException">The name of the provider is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">An attempt is made to call <see cref="M:System.Configuration.Provider.ProviderBase.Initialize(System.String,System.Collections.Specialized.NameValueCollection)"></see> on a provider after the provider has already been initialized.</exception>
        /// <exception cref="T:System.ArgumentException">The name of the provider has a length of zero.</exception>
		public override void Initialize(string name, NameValueCollection config)
        {
            // Verify that config isn't null
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            // Assign the provider a default name if it doesn't have one
            if (String.IsNullOrEmpty(name))
            {
                name = "SqlNetTiersProvider";
            }

            // Add a default "description" attribute to config if the
            // attribute doesn't exist or is empty
            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "NetTiers Sql provider");
            }

            // Call the base class's Initialize method
            base.Initialize(name, config);

            // Initialize _applicationName
            _applicationName = config["applicationName"];

            if (string.IsNullOrEmpty(_applicationName))
            {
                _applicationName = "/";
            }
            config.Remove("applicationName");


            #region "Initialize UseStoredProcedure"
            string storedProcedure  = config["useStoredProcedure"];
           	if (string.IsNullOrEmpty(storedProcedure))
            {
                throw new ProviderException("Empty or missing useStoredProcedure");
            }
            this._useStoredProcedure = Convert.ToBoolean(config["useStoredProcedure"]);
            config.Remove("useStoredProcedure");
            #endregion

			#region ConnectionString

			// Initialize _connectionString
			_connectionString = config["connectionString"];
			config.Remove("connectionString");

			string connect = config["connectionStringName"];
			config.Remove("connectionStringName");

			if ( String.IsNullOrEmpty(_connectionString) )
			{
				if ( String.IsNullOrEmpty(connect) )
				{
					throw new ProviderException("Empty or missing connectionStringName");
				}

				if ( DataRepository.ConnectionStrings[connect] == null )
				{
					throw new ProviderException("Missing connection string");
				}

				_connectionString = DataRepository.ConnectionStrings[connect].ConnectionString;
			}

            if ( String.IsNullOrEmpty(_connectionString) )
            {
                throw new ProviderException("Empty connection string");
			}

			#endregion
            
             #region "_providerInvariantName"

            // initialize _providerInvariantName
            this._providerInvariantName = config["providerInvariantName"];

            if (String.IsNullOrEmpty(_providerInvariantName))
            {
                throw new ProviderException("Empty or missing providerInvariantName");
            }
            config.Remove("providerInvariantName");

            #endregion

        }
		
		/// <summary>
		/// Creates a new <c cref="TransactionManager"/> instance from the current datasource.
		/// </summary>
		/// <returns></returns>
		public override TransactionManager CreateTransaction()
		{
			return new TransactionManager(this._connectionString);
		}
		
		/// <summary>
		/// Gets a value indicating whether to use stored procedure or not.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this repository use stored procedures; otherwise, <c>false</c>.
		/// </value>
		public bool UseStoredProcedure
		{
			get {return this._useStoredProcedure;}
			set {this._useStoredProcedure = value;}
		}
		
		 /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>The connection string.</value>
		public string ConnectionString
		{
			get {return this._connectionString;}
			set {this._connectionString = value;}
		}
		
		/// <summary>
	    /// Gets or sets the invariant provider name listed in the DbProviderFactories machine.config section.
	    /// </summary>
	    /// <value>The name of the provider invariant.</value>
	    public string ProviderInvariantName
	    {
	        get { return this._providerInvariantName; }
	        set { this._providerInvariantName = value; }
	    }		
		
		///<summary>
		/// Indicates if the current <c cref="NetTiersProvider"/> implementation supports Transacton.
		///</summary>
		public override bool IsTransactionSupported
		{
			get
			{
				return true;
			}
		}

		
		#region "ServicesProvider"
			
		private SqlServicesProvider innerSqlServicesProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Services"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override ServicesProviderBase ServicesProvider
		{
			get
			{
				if (innerSqlServicesProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlServicesProvider == null)
						{
							this.innerSqlServicesProvider = new SqlServicesProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlServicesProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <c cref="SqlServicesProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlServicesProvider SqlServicesProvider
		{
			get {return ServicesProvider as SqlServicesProvider;}
		}
		
		#endregion
		
		
		#region "UsersProvider"
			
		private SqlUsersProvider innerSqlUsersProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Users"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override UsersProviderBase UsersProvider
		{
			get
			{
				if (innerSqlUsersProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlUsersProvider == null)
						{
							this.innerSqlUsersProvider = new SqlUsersProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlUsersProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <c cref="SqlUsersProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlUsersProvider SqlUsersProvider
		{
			get {return UsersProvider as SqlUsersProvider;}
		}
		
		#endregion
		
		
		#region "RoomProvider"
			
		private SqlRoomProvider innerSqlRoomProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Room"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override RoomProviderBase RoomProvider
		{
			get
			{
				if (innerSqlRoomProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlRoomProvider == null)
						{
							this.innerSqlRoomProvider = new SqlRoomProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlRoomProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <c cref="SqlRoomProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlRoomProvider SqlRoomProvider
		{
			get {return RoomProvider as SqlRoomProvider;}
		}
		
		#endregion
		
		
		#region "StatusProvider"
			
		private SqlStatusProvider innerSqlStatusProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Status"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override StatusProviderBase StatusProvider
		{
			get
			{
				if (innerSqlStatusProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlStatusProvider == null)
						{
							this.innerSqlStatusProvider = new SqlStatusProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlStatusProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <c cref="SqlStatusProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlStatusProvider SqlStatusProvider
		{
			get {return StatusProvider as SqlStatusProvider;}
		}
		
		#endregion
		
		
		#region "ScreenProvider"
			
		private SqlScreenProvider innerSqlScreenProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Screen"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override ScreenProviderBase ScreenProvider
		{
			get
			{
				if (innerSqlScreenProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlScreenProvider == null)
						{
							this.innerSqlScreenProvider = new SqlScreenProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlScreenProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <c cref="SqlScreenProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlScreenProvider SqlScreenProvider
		{
			get {return ScreenProvider as SqlScreenProvider;}
		}
		
		#endregion
		
		
		#region "UnitsProvider"
			
		private SqlUnitsProvider innerSqlUnitsProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Units"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override UnitsProviderBase UnitsProvider
		{
			get
			{
				if (innerSqlUnitsProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlUnitsProvider == null)
						{
							this.innerSqlUnitsProvider = new SqlUnitsProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlUnitsProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <c cref="SqlUnitsProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlUnitsProvider SqlUnitsProvider
		{
			get {return UnitsProvider as SqlUnitsProvider;}
		}
		
		#endregion
		
		
		#region "UserGroupProvider"
			
		private SqlUserGroupProvider innerSqlUserGroupProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="UserGroup"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override UserGroupProviderBase UserGroupProvider
		{
			get
			{
				if (innerSqlUserGroupProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlUserGroupProvider == null)
						{
							this.innerSqlUserGroupProvider = new SqlUserGroupProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlUserGroupProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <c cref="SqlUserGroupProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlUserGroupProvider SqlUserGroupProvider
		{
			get {return UserGroupProvider as SqlUserGroupProvider;}
		}
		
		#endregion
		
		
		#region "AppointmentProvider"
			
		private SqlAppointmentProvider innerSqlAppointmentProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Appointment"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override AppointmentProviderBase AppointmentProvider
		{
			get
			{
				if (innerSqlAppointmentProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlAppointmentProvider == null)
						{
							this.innerSqlAppointmentProvider = new SqlAppointmentProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlAppointmentProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <c cref="SqlAppointmentProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlAppointmentProvider SqlAppointmentProvider
		{
			get {return AppointmentProvider as SqlAppointmentProvider;}
		}
		
		#endregion
		
		
		#region "RosterTypeProvider"
			
		private SqlRosterTypeProvider innerSqlRosterTypeProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="RosterType"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override RosterTypeProviderBase RosterTypeProvider
		{
			get
			{
				if (innerSqlRosterTypeProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlRosterTypeProvider == null)
						{
							this.innerSqlRosterTypeProvider = new SqlRosterTypeProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlRosterTypeProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <c cref="SqlRosterTypeProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlRosterTypeProvider SqlRosterTypeProvider
		{
			get {return RosterTypeProvider as SqlRosterTypeProvider;}
		}
		
		#endregion
		
		
		#region "UserRoleProvider"
			
		private SqlUserRoleProvider innerSqlUserRoleProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="UserRole"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override UserRoleProviderBase UserRoleProvider
		{
			get
			{
				if (innerSqlUserRoleProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlUserRoleProvider == null)
						{
							this.innerSqlUserRoleProvider = new SqlUserRoleProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlUserRoleProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <c cref="SqlUserRoleProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlUserRoleProvider SqlUserRoleProvider
		{
			get {return UserRoleProvider as SqlUserRoleProvider;}
		}
		
		#endregion
		
		
		#region "RosterProvider"
			
		private SqlRosterProvider innerSqlRosterProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Roster"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override RosterProviderBase RosterProvider
		{
			get
			{
				if (innerSqlRosterProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlRosterProvider == null)
						{
							this.innerSqlRosterProvider = new SqlRosterProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlRosterProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <c cref="SqlRosterProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlRosterProvider SqlRosterProvider
		{
			get {return RosterProvider as SqlRosterProvider;}
		}
		
		#endregion
		
		
		#region "DoctorRoomProvider"
			
		private SqlDoctorRoomProvider innerSqlDoctorRoomProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="DoctorRoom"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override DoctorRoomProviderBase DoctorRoomProvider
		{
			get
			{
				if (innerSqlDoctorRoomProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlDoctorRoomProvider == null)
						{
							this.innerSqlDoctorRoomProvider = new SqlDoctorRoomProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlDoctorRoomProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <c cref="SqlDoctorRoomProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlDoctorRoomProvider SqlDoctorRoomProvider
		{
			get {return DoctorRoomProvider as SqlDoctorRoomProvider;}
		}
		
		#endregion
		
		
		#region "GroupRoleProvider"
			
		private SqlGroupRoleProvider innerSqlGroupRoleProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="GroupRole"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override GroupRoleProviderBase GroupRoleProvider
		{
			get
			{
				if (innerSqlGroupRoleProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlGroupRoleProvider == null)
						{
							this.innerSqlGroupRoleProvider = new SqlGroupRoleProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlGroupRoleProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <c cref="SqlGroupRoleProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlGroupRoleProvider SqlGroupRoleProvider
		{
			get {return GroupRoleProvider as SqlGroupRoleProvider;}
		}
		
		#endregion
		
		
		#region "PatientProvider"
			
		private SqlPatientProvider innerSqlPatientProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Patient"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PatientProviderBase PatientProvider
		{
			get
			{
				if (innerSqlPatientProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlPatientProvider == null)
						{
							this.innerSqlPatientProvider = new SqlPatientProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlPatientProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <c cref="SqlPatientProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlPatientProvider SqlPatientProvider
		{
			get {return PatientProvider as SqlPatientProvider;}
		}
		
		#endregion
		
		
		#region "RoleProvider"
			
		private SqlRoleProvider innerSqlRoleProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Role"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override RoleProviderBase RoleProvider
		{
			get
			{
				if (innerSqlRoleProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlRoleProvider == null)
						{
							this.innerSqlRoleProvider = new SqlRoleProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlRoleProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <c cref="SqlRoleProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlRoleProvider SqlRoleProvider
		{
			get {return RoleProvider as SqlRoleProvider;}
		}
		
		#endregion
		
		
		#region "RoleDetailProvider"
			
		private SqlRoleDetailProvider innerSqlRoleDetailProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="RoleDetail"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override RoleDetailProviderBase RoleDetailProvider
		{
			get
			{
				if (innerSqlRoleDetailProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlRoleDetailProvider == null)
						{
							this.innerSqlRoleDetailProvider = new SqlRoleDetailProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlRoleDetailProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <c cref="SqlRoleDetailProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlRoleDetailProvider SqlRoleDetailProvider
		{
			get {return RoleDetailProvider as SqlRoleDetailProvider;}
		}
		
		#endregion
		
		
		#region "AppointmentGroupProvider"
			
		private SqlAppointmentGroupProvider innerSqlAppointmentGroupProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="AppointmentGroup"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override AppointmentGroupProviderBase AppointmentGroupProvider
		{
			get
			{
				if (innerSqlAppointmentGroupProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlAppointmentGroupProvider == null)
						{
							this.innerSqlAppointmentGroupProvider = new SqlAppointmentGroupProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlAppointmentGroupProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <c cref="SqlAppointmentGroupProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlAppointmentGroupProvider SqlAppointmentGroupProvider
		{
			get {return AppointmentGroupProvider as SqlAppointmentGroupProvider;}
		}
		
		#endregion
		
		
		
		#region "General data access methods"

		#region "ExecuteNonQuery"
		/// <summary>
		/// Executes the non query.
		/// </summary>
		/// <param name="storedProcedureName">Name of the stored procedure.</param>
		/// <param name="parameterValues">The parameter values.</param>
		/// <returns></returns>
		public override int ExecuteNonQuery(string storedProcedureName, params object[] parameterValues)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);
			return database.ExecuteNonQuery(storedProcedureName, parameterValues);	
		}

		/// <summary>
		/// Executes the non query.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="storedProcedureName">Name of the stored procedure.</param>
		/// <param name="parameterValues">The parameter values.</param>
		/// <returns></returns>
		public override int ExecuteNonQuery(TransactionManager transactionManager, string storedProcedureName, params object[] parameterValues)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);
			return database.ExecuteNonQuery(transactionManager.TransactionObject, storedProcedureName, parameterValues);	
		}

		/// <summary>
		/// Executes the non query.
		/// </summary>
		/// <param name="commandWrapper">The command wrapper.</param>
		public override void ExecuteNonQuery(DbCommand commandWrapper)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);
			database.ExecuteNonQuery(commandWrapper);	
			
		}

		/// <summary>
		/// Executes the non query.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="commandWrapper">The command wrapper.</param>
		public override void ExecuteNonQuery(TransactionManager transactionManager, DbCommand commandWrapper)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);
			database.ExecuteNonQuery(commandWrapper, transactionManager.TransactionObject);	
		}


		/// <summary>
		/// Executes the non query.
		/// </summary>
		/// <param name="commandType">Type of the command.</param>
		/// <param name="commandText">The command text.</param>
		/// <returns></returns>
		public override int ExecuteNonQuery(CommandType commandType, string commandText)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);
			return database.ExecuteNonQuery(commandType, commandText);	
		}
		/// <summary>
		/// Executes the non query.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="commandType">Type of the command.</param>
		/// <param name="commandText">The command text.</param>
		/// <returns></returns>
		public override int ExecuteNonQuery(TransactionManager transactionManager, CommandType commandType, string commandText)
		{
			Database database = transactionManager.Database;			
			return database.ExecuteNonQuery(transactionManager.TransactionObject , commandType, commandText);				
		}
		#endregion

		#region "ExecuteDataReader"
		/// <summary>
		/// Executes the reader.
		/// </summary>
		/// <param name="storedProcedureName">Name of the stored procedure.</param>
		/// <param name="parameterValues">The parameter values.</param>
		/// <returns></returns>
		public override IDataReader ExecuteReader(string storedProcedureName, params object[] parameterValues)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);			
			return database.ExecuteReader(storedProcedureName, parameterValues);	
		}

		/// <summary>
		/// Executes the reader.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="storedProcedureName">Name of the stored procedure.</param>
		/// <param name="parameterValues">The parameter values.</param>
		/// <returns></returns>
		public override IDataReader ExecuteReader(TransactionManager transactionManager, string storedProcedureName, params object[] parameterValues)
		{
			Database database = transactionManager.Database;
			return database.ExecuteReader(transactionManager.TransactionObject, storedProcedureName, parameterValues);	
		}

		/// <summary>
		/// Executes the reader.
		/// </summary>
		/// <param name="commandWrapper">The command wrapper.</param>
		/// <returns></returns>
		public override IDataReader ExecuteReader(DbCommand commandWrapper)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);			
			return database.ExecuteReader(commandWrapper);	
		}

		/// <summary>
		/// Executes the reader.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="commandWrapper">The command wrapper.</param>
		/// <returns></returns>
		public override IDataReader ExecuteReader(TransactionManager transactionManager, DbCommand commandWrapper)
		{
			Database database = transactionManager.Database;
			return database.ExecuteReader(commandWrapper, transactionManager.TransactionObject);	
		}


		/// <summary>
		/// Executes the reader.
		/// </summary>
		/// <param name="commandType">Type of the command.</param>
		/// <param name="commandText">The command text.</param>
		/// <returns></returns>
		public override IDataReader ExecuteReader(CommandType commandType, string commandText)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);
			return database.ExecuteReader(commandType, commandText);	
		}
		/// <summary>
		/// Executes the reader.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="commandType">Type of the command.</param>
		/// <param name="commandText">The command text.</param>
		/// <returns></returns>
		public override IDataReader ExecuteReader(TransactionManager transactionManager, CommandType commandType, string commandText)
		{
			Database database = transactionManager.Database;			
			return database.ExecuteReader(transactionManager.TransactionObject , commandType, commandText);				
		}
		#endregion

		#region "ExecuteDataSet"
		/// <summary>
		/// Executes the data set.
		/// </summary>
		/// <param name="storedProcedureName">Name of the stored procedure.</param>
		/// <param name="parameterValues">The parameter values.</param>
		/// <returns></returns>
		public override DataSet ExecuteDataSet(string storedProcedureName, params object[] parameterValues)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);			
			return database.ExecuteDataSet(storedProcedureName, parameterValues);	
		}

		/// <summary>
		/// Executes the data set.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="storedProcedureName">Name of the stored procedure.</param>
		/// <param name="parameterValues">The parameter values.</param>
		/// <returns></returns>
		public override DataSet ExecuteDataSet(TransactionManager transactionManager, string storedProcedureName, params object[] parameterValues)
		{
			Database database = transactionManager.Database;
			return database.ExecuteDataSet(transactionManager.TransactionObject, storedProcedureName, parameterValues);	
		}

		/// <summary>
		/// Executes the data set.
		/// </summary>
		/// <param name="commandWrapper">The command wrapper.</param>
		/// <returns></returns>
		public override DataSet ExecuteDataSet(DbCommand commandWrapper)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);			
			return database.ExecuteDataSet(commandWrapper);	
		}

		/// <summary>
		/// Executes the data set.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="commandWrapper">The command wrapper.</param>
		/// <returns></returns>
		public override DataSet ExecuteDataSet(TransactionManager transactionManager, DbCommand commandWrapper)
		{
			Database database = transactionManager.Database;
			return database.ExecuteDataSet(commandWrapper, transactionManager.TransactionObject);	
		}


		/// <summary>
		/// Executes the data set.
		/// </summary>
		/// <param name="commandType">Type of the command.</param>
		/// <param name="commandText">The command text.</param>
		/// <returns></returns>
		public override DataSet ExecuteDataSet(CommandType commandType, string commandText)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);
			return database.ExecuteDataSet(commandType, commandText);	
		}
		/// <summary>
		/// Executes the data set.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="commandType">Type of the command.</param>
		/// <param name="commandText">The command text.</param>
		/// <returns></returns>
		public override DataSet ExecuteDataSet(TransactionManager transactionManager, CommandType commandType, string commandText)
		{
			Database database = transactionManager.Database;			
			return database.ExecuteDataSet(transactionManager.TransactionObject , commandType, commandText);				
		}
		#endregion

		#region "ExecuteScalar"
		/// <summary>
		/// Executes the scalar.
		/// </summary>
		/// <param name="storedProcedureName">Name of the stored procedure.</param>
		/// <param name="parameterValues">The parameter values.</param>
		/// <returns></returns>
		public override object ExecuteScalar(string storedProcedureName, params object[] parameterValues)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);			
			return database.ExecuteScalar(storedProcedureName, parameterValues);	
		}

		/// <summary>
		/// Executes the scalar.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="storedProcedureName">Name of the stored procedure.</param>
		/// <param name="parameterValues">The parameter values.</param>
		/// <returns></returns>
		public override object ExecuteScalar(TransactionManager transactionManager, string storedProcedureName, params object[] parameterValues)
		{
			Database database = transactionManager.Database;
			return database.ExecuteScalar(transactionManager.TransactionObject, storedProcedureName, parameterValues);	
		}

		/// <summary>
		/// Executes the scalar.
		/// </summary>
		/// <param name="commandWrapper">The command wrapper.</param>
		/// <returns></returns>
		public override object ExecuteScalar(DbCommand commandWrapper)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);			
			return database.ExecuteScalar(commandWrapper);	
		}

		/// <summary>
		/// Executes the scalar.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="commandWrapper">The command wrapper.</param>
		/// <returns></returns>
		public override object ExecuteScalar(TransactionManager transactionManager, DbCommand commandWrapper)
		{
			Database database = transactionManager.Database;
			return database.ExecuteScalar(commandWrapper, transactionManager.TransactionObject);	
		}

		/// <summary>
		/// Executes the scalar.
		/// </summary>
		/// <param name="commandType">Type of the command.</param>
		/// <param name="commandText">The command text.</param>
		/// <returns></returns>
		public override object ExecuteScalar(CommandType commandType, string commandText)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);
			return database.ExecuteScalar(commandType, commandText);	
		}
		/// <summary>
		/// Executes the scalar.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="commandType">Type of the command.</param>
		/// <param name="commandText">The command text.</param>
		/// <returns></returns>
		public override object ExecuteScalar(TransactionManager transactionManager, CommandType commandType, string commandText)
		{
			Database database = transactionManager.Database;			
			return database.ExecuteScalar(transactionManager.TransactionObject , commandType, commandText);				
		}
		#endregion

		#endregion


	}
}
