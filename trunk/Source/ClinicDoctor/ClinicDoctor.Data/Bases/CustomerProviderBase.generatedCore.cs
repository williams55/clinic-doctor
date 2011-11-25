#region Using directives

using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;

using ClinicDoctor.Entities;
using ClinicDoctor.Data;

#endregion

namespace ClinicDoctor.Data.Bases
{	
	///<summary>
	/// This class is the base class for any <see cref="CustomerProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class CustomerProviderBaseCore : EntityProviderBase<ClinicDoctor.Entities.Customer, ClinicDoctor.Entities.CustomerKey>
	{		
		#region Get from Many To Many Relationship Functions
		#endregion	
		
		#region Delete Methods

		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager">A <see cref="TransactionManager"/> object.</param>
		/// <param name="key">The unique identifier of the row to delete.</param>
		/// <returns>Returns true if operation suceeded.</returns>
		public override bool Delete(TransactionManager transactionManager, ClinicDoctor.Entities.CustomerKey key)
		{
			return Delete(transactionManager, key.Id);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_id">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Int32 _id)
		{
			return Delete(null, _id);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Int32 _id);		
		
		#endregion Delete Methods
		
		#region Get By Foreign Key Functions
		#endregion

		#region Get By Index Functions
		
		/// <summary>
		/// 	Gets a row from the DataSource based on its primary key.
		/// </summary>
		/// <param name="transactionManager">A <see cref="TransactionManager"/> object.</param>
		/// <param name="key">The unique identifier of the row to retrieve.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <returns>Returns an instance of the Entity class.</returns>
		public override ClinicDoctor.Entities.Customer Get(TransactionManager transactionManager, ClinicDoctor.Entities.CustomerKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_Customer_Id_IsDisabled index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Customer&gt;"/> class.</returns>
		public TList<Customer> GetByIdIsDisabled(System.Int32 _id, System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByIdIsDisabled(null,_id, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Customer_Id_IsDisabled index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Customer&gt;"/> class.</returns>
		public TList<Customer> GetByIdIsDisabled(System.Int32 _id, System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIdIsDisabled(null, _id, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Customer_Id_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Customer&gt;"/> class.</returns>
		public TList<Customer> GetByIdIsDisabled(TransactionManager transactionManager, System.Int32 _id, System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByIdIsDisabled(transactionManager, _id, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Customer_Id_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Customer&gt;"/> class.</returns>
		public TList<Customer> GetByIdIsDisabled(TransactionManager transactionManager, System.Int32 _id, System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIdIsDisabled(transactionManager, _id, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Customer_Id_IsDisabled index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Customer&gt;"/> class.</returns>
		public TList<Customer> GetByIdIsDisabled(System.Int32 _id, System.Boolean? _isDisabled, int start, int pageLength, out int count)
		{
			return GetByIdIsDisabled(null, _id, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Customer_Id_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Customer&gt;"/> class.</returns>
		public abstract TList<Customer> GetByIdIsDisabled(TransactionManager transactionManager, System.Int32 _id, System.Boolean? _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_Customer_IsDisabled index.
		/// </summary>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Customer&gt;"/> class.</returns>
		public TList<Customer> GetByIsDisabled(System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByIsDisabled(null,_isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Customer_IsDisabled index.
		/// </summary>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Customer&gt;"/> class.</returns>
		public TList<Customer> GetByIsDisabled(System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIsDisabled(null, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Customer_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Customer&gt;"/> class.</returns>
		public TList<Customer> GetByIsDisabled(TransactionManager transactionManager, System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByIsDisabled(transactionManager, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Customer_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Customer&gt;"/> class.</returns>
		public TList<Customer> GetByIsDisabled(TransactionManager transactionManager, System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIsDisabled(transactionManager, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Customer_IsDisabled index.
		/// </summary>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Customer&gt;"/> class.</returns>
		public TList<Customer> GetByIsDisabled(System.Boolean? _isDisabled, int start, int pageLength, out int count)
		{
			return GetByIsDisabled(null, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Customer_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Customer&gt;"/> class.</returns>
		public abstract TList<Customer> GetByIsDisabled(TransactionManager transactionManager, System.Boolean? _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_Customer_IsFemale index.
		/// </summary>
		/// <param name="_isFemale"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Customer&gt;"/> class.</returns>
		public TList<Customer> GetByIsFemale(System.Boolean? _isFemale)
		{
			int count = -1;
			return GetByIsFemale(null,_isFemale, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Customer_IsFemale index.
		/// </summary>
		/// <param name="_isFemale"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Customer&gt;"/> class.</returns>
		public TList<Customer> GetByIsFemale(System.Boolean? _isFemale, int start, int pageLength)
		{
			int count = -1;
			return GetByIsFemale(null, _isFemale, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Customer_IsFemale index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_isFemale"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Customer&gt;"/> class.</returns>
		public TList<Customer> GetByIsFemale(TransactionManager transactionManager, System.Boolean? _isFemale)
		{
			int count = -1;
			return GetByIsFemale(transactionManager, _isFemale, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Customer_IsFemale index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_isFemale"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Customer&gt;"/> class.</returns>
		public TList<Customer> GetByIsFemale(TransactionManager transactionManager, System.Boolean? _isFemale, int start, int pageLength)
		{
			int count = -1;
			return GetByIsFemale(transactionManager, _isFemale, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Customer_IsFemale index.
		/// </summary>
		/// <param name="_isFemale"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Customer&gt;"/> class.</returns>
		public TList<Customer> GetByIsFemale(System.Boolean? _isFemale, int start, int pageLength, out int count)
		{
			return GetByIsFemale(null, _isFemale, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Customer_IsFemale index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_isFemale"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Customer&gt;"/> class.</returns>
		public abstract TList<Customer> GetByIsFemale(TransactionManager transactionManager, System.Boolean? _isFemale, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_Customer_IsFemale_IsDisabled index.
		/// </summary>
		/// <param name="_isFemale"></param>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Customer&gt;"/> class.</returns>
		public TList<Customer> GetByIsFemaleIsDisabled(System.Boolean? _isFemale, System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByIsFemaleIsDisabled(null,_isFemale, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Customer_IsFemale_IsDisabled index.
		/// </summary>
		/// <param name="_isFemale"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Customer&gt;"/> class.</returns>
		public TList<Customer> GetByIsFemaleIsDisabled(System.Boolean? _isFemale, System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIsFemaleIsDisabled(null, _isFemale, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Customer_IsFemale_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_isFemale"></param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Customer&gt;"/> class.</returns>
		public TList<Customer> GetByIsFemaleIsDisabled(TransactionManager transactionManager, System.Boolean? _isFemale, System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByIsFemaleIsDisabled(transactionManager, _isFemale, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Customer_IsFemale_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_isFemale"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Customer&gt;"/> class.</returns>
		public TList<Customer> GetByIsFemaleIsDisabled(TransactionManager transactionManager, System.Boolean? _isFemale, System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIsFemaleIsDisabled(transactionManager, _isFemale, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Customer_IsFemale_IsDisabled index.
		/// </summary>
		/// <param name="_isFemale"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Customer&gt;"/> class.</returns>
		public TList<Customer> GetByIsFemaleIsDisabled(System.Boolean? _isFemale, System.Boolean? _isDisabled, int start, int pageLength, out int count)
		{
			return GetByIsFemaleIsDisabled(null, _isFemale, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Customer_IsFemale_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_isFemale"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Customer&gt;"/> class.</returns>
		public abstract TList<Customer> GetByIsFemaleIsDisabled(TransactionManager transactionManager, System.Boolean? _isFemale, System.Boolean? _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Customer index.
		/// </summary>
		/// <param name="_id"></param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Customer"/> class.</returns>
		public ClinicDoctor.Entities.Customer GetById(System.Int32 _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Customer index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Customer"/> class.</returns>
		public ClinicDoctor.Entities.Customer GetById(System.Int32 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Customer index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Customer"/> class.</returns>
		public ClinicDoctor.Entities.Customer GetById(TransactionManager transactionManager, System.Int32 _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Customer index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Customer"/> class.</returns>
		public ClinicDoctor.Entities.Customer GetById(TransactionManager transactionManager, System.Int32 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Customer index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Customer"/> class.</returns>
		public ClinicDoctor.Entities.Customer GetById(System.Int32 _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Customer index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Customer"/> class.</returns>
		public abstract ClinicDoctor.Entities.Customer GetById(TransactionManager transactionManager, System.Int32 _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Customer&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Customer&gt;"/></returns>
		public static TList<Customer> Fill(IDataReader reader, TList<Customer> rows, int start, int pageLength)
		{
			NetTiersProvider currentProvider = DataRepository.Provider;
            bool useEntityFactory = currentProvider.UseEntityFactory;
            bool enableEntityTracking = currentProvider.EnableEntityTracking;
            LoadPolicy currentLoadPolicy = currentProvider.CurrentLoadPolicy;
			Type entityCreationFactoryType = currentProvider.EntityCreationalFactoryType;
			
			// advance to the starting row
			for (int i = 0; i < start; i++)
			{
				if (!reader.Read())
				return rows; // not enough rows, just return
			}
			for (int i = 0; i < pageLength; i++)
			{
				if (!reader.Read())
					break; // we are done
					
				string key = null;
				
				ClinicDoctor.Entities.Customer c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Customer")
					.Append("|").Append((System.Int32)reader[((int)CustomerColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Customer>(
					key.ToString(), // EntityTrackingKey
					"Customer",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new ClinicDoctor.Entities.Customer();
				}
				
				if (!enableEntityTracking ||
					c.EntityState == EntityState.Added ||
					(enableEntityTracking &&
					
						(
							(currentLoadPolicy == LoadPolicy.PreserveChanges && c.EntityState == EntityState.Unchanged) ||
							(currentLoadPolicy == LoadPolicy.DiscardChanges && c.EntityState != EntityState.Unchanged)
						)
					))
				{
					c.SuppressEntityEvents = true;
					c.Id = (System.Int32)reader[((int)CustomerColumn.Id - 1)];
					c.FirstName = (reader.IsDBNull(((int)CustomerColumn.FirstName - 1)))?null:(System.String)reader[((int)CustomerColumn.FirstName - 1)];
					c.LastName = (reader.IsDBNull(((int)CustomerColumn.LastName - 1)))?null:(System.String)reader[((int)CustomerColumn.LastName - 1)];
					c.Address = (reader.IsDBNull(((int)CustomerColumn.Address - 1)))?null:(System.String)reader[((int)CustomerColumn.Address - 1)];
					c.HomePhone = (reader.IsDBNull(((int)CustomerColumn.HomePhone - 1)))?null:(System.String)reader[((int)CustomerColumn.HomePhone - 1)];
					c.WorkPhone = (reader.IsDBNull(((int)CustomerColumn.WorkPhone - 1)))?null:(System.String)reader[((int)CustomerColumn.WorkPhone - 1)];
					c.CellPhone = (reader.IsDBNull(((int)CustomerColumn.CellPhone - 1)))?null:(System.String)reader[((int)CustomerColumn.CellPhone - 1)];
					c.Birthdate = (reader.IsDBNull(((int)CustomerColumn.Birthdate - 1)))?null:(System.DateTime?)reader[((int)CustomerColumn.Birthdate - 1)];
					c.IsFemale = (reader.IsDBNull(((int)CustomerColumn.IsFemale - 1)))?null:(System.Boolean?)reader[((int)CustomerColumn.IsFemale - 1)];
					c.Title = (reader.IsDBNull(((int)CustomerColumn.Title - 1)))?null:(System.String)reader[((int)CustomerColumn.Title - 1)];
					c.Note = (reader.IsDBNull(((int)CustomerColumn.Note - 1)))?null:(System.String)reader[((int)CustomerColumn.Note - 1)];
					c.IsDisabled = (reader.IsDBNull(((int)CustomerColumn.IsDisabled - 1)))?null:(System.Boolean?)reader[((int)CustomerColumn.IsDisabled - 1)];
					c.CreateUser = (reader.IsDBNull(((int)CustomerColumn.CreateUser - 1)))?null:(System.String)reader[((int)CustomerColumn.CreateUser - 1)];
					c.CreateDate = (reader.IsDBNull(((int)CustomerColumn.CreateDate - 1)))?null:(System.DateTime?)reader[((int)CustomerColumn.CreateDate - 1)];
					c.UpdateUser = (reader.IsDBNull(((int)CustomerColumn.UpdateUser - 1)))?null:(System.String)reader[((int)CustomerColumn.UpdateUser - 1)];
					c.UpdateDate = (reader.IsDBNull(((int)CustomerColumn.UpdateDate - 1)))?null:(System.DateTime?)reader[((int)CustomerColumn.UpdateDate - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="ClinicDoctor.Entities.Customer"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="ClinicDoctor.Entities.Customer"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, ClinicDoctor.Entities.Customer entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (System.Int32)reader[((int)CustomerColumn.Id - 1)];
			entity.FirstName = (reader.IsDBNull(((int)CustomerColumn.FirstName - 1)))?null:(System.String)reader[((int)CustomerColumn.FirstName - 1)];
			entity.LastName = (reader.IsDBNull(((int)CustomerColumn.LastName - 1)))?null:(System.String)reader[((int)CustomerColumn.LastName - 1)];
			entity.Address = (reader.IsDBNull(((int)CustomerColumn.Address - 1)))?null:(System.String)reader[((int)CustomerColumn.Address - 1)];
			entity.HomePhone = (reader.IsDBNull(((int)CustomerColumn.HomePhone - 1)))?null:(System.String)reader[((int)CustomerColumn.HomePhone - 1)];
			entity.WorkPhone = (reader.IsDBNull(((int)CustomerColumn.WorkPhone - 1)))?null:(System.String)reader[((int)CustomerColumn.WorkPhone - 1)];
			entity.CellPhone = (reader.IsDBNull(((int)CustomerColumn.CellPhone - 1)))?null:(System.String)reader[((int)CustomerColumn.CellPhone - 1)];
			entity.Birthdate = (reader.IsDBNull(((int)CustomerColumn.Birthdate - 1)))?null:(System.DateTime?)reader[((int)CustomerColumn.Birthdate - 1)];
			entity.IsFemale = (reader.IsDBNull(((int)CustomerColumn.IsFemale - 1)))?null:(System.Boolean?)reader[((int)CustomerColumn.IsFemale - 1)];
			entity.Title = (reader.IsDBNull(((int)CustomerColumn.Title - 1)))?null:(System.String)reader[((int)CustomerColumn.Title - 1)];
			entity.Note = (reader.IsDBNull(((int)CustomerColumn.Note - 1)))?null:(System.String)reader[((int)CustomerColumn.Note - 1)];
			entity.IsDisabled = (reader.IsDBNull(((int)CustomerColumn.IsDisabled - 1)))?null:(System.Boolean?)reader[((int)CustomerColumn.IsDisabled - 1)];
			entity.CreateUser = (reader.IsDBNull(((int)CustomerColumn.CreateUser - 1)))?null:(System.String)reader[((int)CustomerColumn.CreateUser - 1)];
			entity.CreateDate = (reader.IsDBNull(((int)CustomerColumn.CreateDate - 1)))?null:(System.DateTime?)reader[((int)CustomerColumn.CreateDate - 1)];
			entity.UpdateUser = (reader.IsDBNull(((int)CustomerColumn.UpdateUser - 1)))?null:(System.String)reader[((int)CustomerColumn.UpdateUser - 1)];
			entity.UpdateDate = (reader.IsDBNull(((int)CustomerColumn.UpdateDate - 1)))?null:(System.DateTime?)reader[((int)CustomerColumn.UpdateDate - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="ClinicDoctor.Entities.Customer"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="ClinicDoctor.Entities.Customer"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, ClinicDoctor.Entities.Customer entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Id = (System.Int32)dataRow["Id"];
			entity.FirstName = Convert.IsDBNull(dataRow["FirstName"]) ? null : (System.String)dataRow["FirstName"];
			entity.LastName = Convert.IsDBNull(dataRow["LastName"]) ? null : (System.String)dataRow["LastName"];
			entity.Address = Convert.IsDBNull(dataRow["Address"]) ? null : (System.String)dataRow["Address"];
			entity.HomePhone = Convert.IsDBNull(dataRow["HomePhone"]) ? null : (System.String)dataRow["HomePhone"];
			entity.WorkPhone = Convert.IsDBNull(dataRow["WorkPhone"]) ? null : (System.String)dataRow["WorkPhone"];
			entity.CellPhone = Convert.IsDBNull(dataRow["CellPhone"]) ? null : (System.String)dataRow["CellPhone"];
			entity.Birthdate = Convert.IsDBNull(dataRow["Birthdate"]) ? null : (System.DateTime?)dataRow["Birthdate"];
			entity.IsFemale = Convert.IsDBNull(dataRow["IsFemale"]) ? null : (System.Boolean?)dataRow["IsFemale"];
			entity.Title = Convert.IsDBNull(dataRow["Title"]) ? null : (System.String)dataRow["Title"];
			entity.Note = Convert.IsDBNull(dataRow["Note"]) ? null : (System.String)dataRow["Note"];
			entity.IsDisabled = Convert.IsDBNull(dataRow["IsDisabled"]) ? null : (System.Boolean?)dataRow["IsDisabled"];
			entity.CreateUser = Convert.IsDBNull(dataRow["CreateUser"]) ? null : (System.String)dataRow["CreateUser"];
			entity.CreateDate = Convert.IsDBNull(dataRow["CreateDate"]) ? null : (System.DateTime?)dataRow["CreateDate"];
			entity.UpdateUser = Convert.IsDBNull(dataRow["UpdateUser"]) ? null : (System.String)dataRow["UpdateUser"];
			entity.UpdateDate = Convert.IsDBNull(dataRow["UpdateDate"]) ? null : (System.DateTime?)dataRow["UpdateDate"];
			entity.AcceptChanges();
		}
		#endregion 
		
		#region DeepLoad Methods
		/// <summary>
		/// Deep Loads the <see cref="IEntity"/> object with criteria based of the child 
		/// property collections only N Levels Deep based on the <see cref="DeepLoadType"/>.
		/// </summary>
		/// <remarks>
		/// Use this method with caution as it is possible to DeepLoad with Recursion and traverse an entire object graph.
		/// </remarks>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="entity">The <see cref="ClinicDoctor.Entities.Customer"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">ClinicDoctor.Entities.Customer Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, ClinicDoctor.Entities.Customer entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetById methods when available
			
			#region AppointmentCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<Appointment>|AppointmentCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'AppointmentCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.AppointmentCollection = DataRepository.AppointmentProvider.GetByCustomerId(transactionManager, entity.Id);

				if (deep && entity.AppointmentCollection.Count > 0)
				{
					deepHandles.Add("AppointmentCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<Appointment>) DataRepository.AppointmentProvider.DeepLoad,
						new object[] { transactionManager, entity.AppointmentCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			//Fire all DeepLoad Items
			foreach(KeyValuePair<Delegate, object> pair in deepHandles.Values)
		    {
                pair.Key.DynamicInvoke((object[])pair.Value);
		    }
			deepHandles = null;
		}
		
		#endregion 
		
		#region DeepSave Methods

		/// <summary>
		/// Deep Save the entire object graph of the ClinicDoctor.Entities.Customer object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">ClinicDoctor.Entities.Customer instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">ClinicDoctor.Entities.Customer Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, ClinicDoctor.Entities.Customer entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			#endregion Composite Parent Properties

			// Save Root Entity through Provider
			if (!entity.IsDeleted)
				this.Save(transactionManager, entity);
			
			//used to hold DeepSave method delegates and fire after all the local children have been saved.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
	
			#region List<Appointment>
				if (CanDeepSave(entity.AppointmentCollection, "List<Appointment>|AppointmentCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(Appointment child in entity.AppointmentCollection)
					{
						if(child.CustomerIdSource != null)
						{
							child.CustomerId = child.CustomerIdSource.Id;
						}
						else
						{
							child.CustomerId = entity.Id;
						}

					}

					if (entity.AppointmentCollection.Count > 0 || entity.AppointmentCollection.DeletedItems.Count > 0)
					{
						//DataRepository.AppointmentProvider.Save(transactionManager, entity.AppointmentCollection);
						
						deepHandles.Add("AppointmentCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< Appointment >) DataRepository.AppointmentProvider.DeepSave,
							new object[] { transactionManager, entity.AppointmentCollection, deepSaveType, childTypes, innerList }
						));
					}
				} 
			#endregion 
				
			//Fire all DeepSave Items
			foreach(KeyValuePair<Delegate, object> pair in deepHandles.Values)
		    {
                pair.Key.DynamicInvoke((object[])pair.Value);
		    }
			
			// Save Root Entity through Provider, if not already saved in delete mode
			if (entity.IsDeleted)
				this.Save(transactionManager, entity);
				

			deepHandles = null;
						
			return true;
		}
		#endregion
	} // end class
	
	#region CustomerChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>ClinicDoctor.Entities.Customer</c>
	///</summary>
	public enum CustomerChildEntityTypes
	{

		///<summary>
		/// Collection of <c>Customer</c> as OneToMany for AppointmentCollection
		///</summary>
		[ChildEntityType(typeof(TList<Appointment>))]
		AppointmentCollection,
	}
	
	#endregion CustomerChildEntityTypes
	
	#region CustomerFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;CustomerColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Customer"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class CustomerFilterBuilder : SqlFilterBuilder<CustomerColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CustomerFilterBuilder class.
		/// </summary>
		public CustomerFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the CustomerFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public CustomerFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the CustomerFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public CustomerFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion CustomerFilterBuilder
	
	#region CustomerParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;CustomerColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Customer"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class CustomerParameterBuilder : ParameterizedSqlFilterBuilder<CustomerColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CustomerParameterBuilder class.
		/// </summary>
		public CustomerParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the CustomerParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public CustomerParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the CustomerParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public CustomerParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion CustomerParameterBuilder
	
	#region CustomerSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;CustomerColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Customer"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class CustomerSortBuilder : SqlSortBuilder<CustomerColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CustomerSqlSortBuilder class.
		/// </summary>
		public CustomerSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion CustomerSortBuilder
	
} // end namespace
