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
	/// This class is the base class for any <see cref="RosterTypeProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class RosterTypeProviderBaseCore : EntityProviderBase<ClinicDoctor.Entities.RosterType, ClinicDoctor.Entities.RosterTypeKey>
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
		public override bool Delete(TransactionManager transactionManager, ClinicDoctor.Entities.RosterTypeKey key)
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
		public override ClinicDoctor.Entities.RosterType Get(TransactionManager transactionManager, ClinicDoctor.Entities.RosterTypeKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_RosterType_Id_IsDisabled index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;RosterType&gt;"/> class.</returns>
		public TList<RosterType> GetByIdIsDisabled(System.Int32 _id, System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByIdIsDisabled(null,_id, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_RosterType_Id_IsDisabled index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;RosterType&gt;"/> class.</returns>
		public TList<RosterType> GetByIdIsDisabled(System.Int32 _id, System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIdIsDisabled(null, _id, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_RosterType_Id_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;RosterType&gt;"/> class.</returns>
		public TList<RosterType> GetByIdIsDisabled(TransactionManager transactionManager, System.Int32 _id, System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByIdIsDisabled(transactionManager, _id, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_RosterType_Id_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;RosterType&gt;"/> class.</returns>
		public TList<RosterType> GetByIdIsDisabled(TransactionManager transactionManager, System.Int32 _id, System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIdIsDisabled(transactionManager, _id, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_RosterType_Id_IsDisabled index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;RosterType&gt;"/> class.</returns>
		public TList<RosterType> GetByIdIsDisabled(System.Int32 _id, System.Boolean? _isDisabled, int start, int pageLength, out int count)
		{
			return GetByIdIsDisabled(null, _id, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_RosterType_Id_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;RosterType&gt;"/> class.</returns>
		public abstract TList<RosterType> GetByIdIsDisabled(TransactionManager transactionManager, System.Int32 _id, System.Boolean? _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_RosterType_IsBooked index.
		/// </summary>
		/// <param name="_isBooked"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;RosterType&gt;"/> class.</returns>
		public TList<RosterType> GetByIsBooked(System.Boolean? _isBooked)
		{
			int count = -1;
			return GetByIsBooked(null,_isBooked, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_RosterType_IsBooked index.
		/// </summary>
		/// <param name="_isBooked"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;RosterType&gt;"/> class.</returns>
		public TList<RosterType> GetByIsBooked(System.Boolean? _isBooked, int start, int pageLength)
		{
			int count = -1;
			return GetByIsBooked(null, _isBooked, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_RosterType_IsBooked index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_isBooked"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;RosterType&gt;"/> class.</returns>
		public TList<RosterType> GetByIsBooked(TransactionManager transactionManager, System.Boolean? _isBooked)
		{
			int count = -1;
			return GetByIsBooked(transactionManager, _isBooked, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_RosterType_IsBooked index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_isBooked"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;RosterType&gt;"/> class.</returns>
		public TList<RosterType> GetByIsBooked(TransactionManager transactionManager, System.Boolean? _isBooked, int start, int pageLength)
		{
			int count = -1;
			return GetByIsBooked(transactionManager, _isBooked, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_RosterType_IsBooked index.
		/// </summary>
		/// <param name="_isBooked"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;RosterType&gt;"/> class.</returns>
		public TList<RosterType> GetByIsBooked(System.Boolean? _isBooked, int start, int pageLength, out int count)
		{
			return GetByIsBooked(null, _isBooked, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_RosterType_IsBooked index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_isBooked"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;RosterType&gt;"/> class.</returns>
		public abstract TList<RosterType> GetByIsBooked(TransactionManager transactionManager, System.Boolean? _isBooked, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_RosterType_IsBooked_IsDisabled index.
		/// </summary>
		/// <param name="_isBooked"></param>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;RosterType&gt;"/> class.</returns>
		public TList<RosterType> GetByIsBookedIsDisabled(System.Boolean? _isBooked, System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByIsBookedIsDisabled(null,_isBooked, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_RosterType_IsBooked_IsDisabled index.
		/// </summary>
		/// <param name="_isBooked"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;RosterType&gt;"/> class.</returns>
		public TList<RosterType> GetByIsBookedIsDisabled(System.Boolean? _isBooked, System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIsBookedIsDisabled(null, _isBooked, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_RosterType_IsBooked_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_isBooked"></param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;RosterType&gt;"/> class.</returns>
		public TList<RosterType> GetByIsBookedIsDisabled(TransactionManager transactionManager, System.Boolean? _isBooked, System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByIsBookedIsDisabled(transactionManager, _isBooked, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_RosterType_IsBooked_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_isBooked"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;RosterType&gt;"/> class.</returns>
		public TList<RosterType> GetByIsBookedIsDisabled(TransactionManager transactionManager, System.Boolean? _isBooked, System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIsBookedIsDisabled(transactionManager, _isBooked, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_RosterType_IsBooked_IsDisabled index.
		/// </summary>
		/// <param name="_isBooked"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;RosterType&gt;"/> class.</returns>
		public TList<RosterType> GetByIsBookedIsDisabled(System.Boolean? _isBooked, System.Boolean? _isDisabled, int start, int pageLength, out int count)
		{
			return GetByIsBookedIsDisabled(null, _isBooked, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_RosterType_IsBooked_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_isBooked"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;RosterType&gt;"/> class.</returns>
		public abstract TList<RosterType> GetByIsBookedIsDisabled(TransactionManager transactionManager, System.Boolean? _isBooked, System.Boolean? _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_RosterType_IsDisabled index.
		/// </summary>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;RosterType&gt;"/> class.</returns>
		public TList<RosterType> GetByIsDisabled(System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByIsDisabled(null,_isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_RosterType_IsDisabled index.
		/// </summary>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;RosterType&gt;"/> class.</returns>
		public TList<RosterType> GetByIsDisabled(System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIsDisabled(null, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_RosterType_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;RosterType&gt;"/> class.</returns>
		public TList<RosterType> GetByIsDisabled(TransactionManager transactionManager, System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByIsDisabled(transactionManager, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_RosterType_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;RosterType&gt;"/> class.</returns>
		public TList<RosterType> GetByIsDisabled(TransactionManager transactionManager, System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIsDisabled(transactionManager, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_RosterType_IsDisabled index.
		/// </summary>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;RosterType&gt;"/> class.</returns>
		public TList<RosterType> GetByIsDisabled(System.Boolean? _isDisabled, int start, int pageLength, out int count)
		{
			return GetByIsDisabled(null, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_RosterType_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;RosterType&gt;"/> class.</returns>
		public abstract TList<RosterType> GetByIsDisabled(TransactionManager transactionManager, System.Boolean? _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_RosterType index.
		/// </summary>
		/// <param name="_id"></param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.RosterType"/> class.</returns>
		public ClinicDoctor.Entities.RosterType GetById(System.Int32 _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_RosterType index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.RosterType"/> class.</returns>
		public ClinicDoctor.Entities.RosterType GetById(System.Int32 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_RosterType index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.RosterType"/> class.</returns>
		public ClinicDoctor.Entities.RosterType GetById(TransactionManager transactionManager, System.Int32 _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_RosterType index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.RosterType"/> class.</returns>
		public ClinicDoctor.Entities.RosterType GetById(TransactionManager transactionManager, System.Int32 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_RosterType index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.RosterType"/> class.</returns>
		public ClinicDoctor.Entities.RosterType GetById(System.Int32 _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_RosterType index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.RosterType"/> class.</returns>
		public abstract ClinicDoctor.Entities.RosterType GetById(TransactionManager transactionManager, System.Int32 _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;RosterType&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;RosterType&gt;"/></returns>
		public static TList<RosterType> Fill(IDataReader reader, TList<RosterType> rows, int start, int pageLength)
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
				
				ClinicDoctor.Entities.RosterType c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("RosterType")
					.Append("|").Append((System.Int32)reader[((int)RosterTypeColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<RosterType>(
					key.ToString(), // EntityTrackingKey
					"RosterType",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new ClinicDoctor.Entities.RosterType();
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
					c.Id = (System.Int32)reader[((int)RosterTypeColumn.Id - 1)];
					c.Title = (reader.IsDBNull(((int)RosterTypeColumn.Title - 1)))?null:(System.String)reader[((int)RosterTypeColumn.Title - 1)];
					c.IsBooked = (reader.IsDBNull(((int)RosterTypeColumn.IsBooked - 1)))?null:(System.Boolean?)reader[((int)RosterTypeColumn.IsBooked - 1)];
					c.Note = (reader.IsDBNull(((int)RosterTypeColumn.Note - 1)))?null:(System.String)reader[((int)RosterTypeColumn.Note - 1)];
					c.IsDisabled = (reader.IsDBNull(((int)RosterTypeColumn.IsDisabled - 1)))?null:(System.Boolean?)reader[((int)RosterTypeColumn.IsDisabled - 1)];
					c.CreateUser = (reader.IsDBNull(((int)RosterTypeColumn.CreateUser - 1)))?null:(System.String)reader[((int)RosterTypeColumn.CreateUser - 1)];
					c.CreateDate = (reader.IsDBNull(((int)RosterTypeColumn.CreateDate - 1)))?null:(System.DateTime?)reader[((int)RosterTypeColumn.CreateDate - 1)];
					c.UpdateUser = (reader.IsDBNull(((int)RosterTypeColumn.UpdateUser - 1)))?null:(System.String)reader[((int)RosterTypeColumn.UpdateUser - 1)];
					c.UpdateDate = (reader.IsDBNull(((int)RosterTypeColumn.UpdateDate - 1)))?null:(System.DateTime?)reader[((int)RosterTypeColumn.UpdateDate - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="ClinicDoctor.Entities.RosterType"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="ClinicDoctor.Entities.RosterType"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, ClinicDoctor.Entities.RosterType entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (System.Int32)reader[((int)RosterTypeColumn.Id - 1)];
			entity.Title = (reader.IsDBNull(((int)RosterTypeColumn.Title - 1)))?null:(System.String)reader[((int)RosterTypeColumn.Title - 1)];
			entity.IsBooked = (reader.IsDBNull(((int)RosterTypeColumn.IsBooked - 1)))?null:(System.Boolean?)reader[((int)RosterTypeColumn.IsBooked - 1)];
			entity.Note = (reader.IsDBNull(((int)RosterTypeColumn.Note - 1)))?null:(System.String)reader[((int)RosterTypeColumn.Note - 1)];
			entity.IsDisabled = (reader.IsDBNull(((int)RosterTypeColumn.IsDisabled - 1)))?null:(System.Boolean?)reader[((int)RosterTypeColumn.IsDisabled - 1)];
			entity.CreateUser = (reader.IsDBNull(((int)RosterTypeColumn.CreateUser - 1)))?null:(System.String)reader[((int)RosterTypeColumn.CreateUser - 1)];
			entity.CreateDate = (reader.IsDBNull(((int)RosterTypeColumn.CreateDate - 1)))?null:(System.DateTime?)reader[((int)RosterTypeColumn.CreateDate - 1)];
			entity.UpdateUser = (reader.IsDBNull(((int)RosterTypeColumn.UpdateUser - 1)))?null:(System.String)reader[((int)RosterTypeColumn.UpdateUser - 1)];
			entity.UpdateDate = (reader.IsDBNull(((int)RosterTypeColumn.UpdateDate - 1)))?null:(System.DateTime?)reader[((int)RosterTypeColumn.UpdateDate - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="ClinicDoctor.Entities.RosterType"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="ClinicDoctor.Entities.RosterType"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, ClinicDoctor.Entities.RosterType entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Id = (System.Int32)dataRow["Id"];
			entity.Title = Convert.IsDBNull(dataRow["Title"]) ? null : (System.String)dataRow["Title"];
			entity.IsBooked = Convert.IsDBNull(dataRow["IsBooked"]) ? null : (System.Boolean?)dataRow["IsBooked"];
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
		/// <param name="entity">The <see cref="ClinicDoctor.Entities.RosterType"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">ClinicDoctor.Entities.RosterType Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, ClinicDoctor.Entities.RosterType entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetById methods when available
			
			#region RosterCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<Roster>|RosterCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'RosterCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.RosterCollection = DataRepository.RosterProvider.GetByRosterTypeId(transactionManager, entity.Id);

				if (deep && entity.RosterCollection.Count > 0)
				{
					deepHandles.Add("RosterCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<Roster>) DataRepository.RosterProvider.DeepLoad,
						new object[] { transactionManager, entity.RosterCollection, deep, deepLoadType, childTypes, innerList }
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
		/// Deep Save the entire object graph of the ClinicDoctor.Entities.RosterType object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">ClinicDoctor.Entities.RosterType instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">ClinicDoctor.Entities.RosterType Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, ClinicDoctor.Entities.RosterType entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
			#region List<Roster>
				if (CanDeepSave(entity.RosterCollection, "List<Roster>|RosterCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(Roster child in entity.RosterCollection)
					{
						if(child.RosterTypeIdSource != null)
						{
							child.RosterTypeId = child.RosterTypeIdSource.Id;
						}
						else
						{
							child.RosterTypeId = entity.Id;
						}

					}

					if (entity.RosterCollection.Count > 0 || entity.RosterCollection.DeletedItems.Count > 0)
					{
						//DataRepository.RosterProvider.Save(transactionManager, entity.RosterCollection);
						
						deepHandles.Add("RosterCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< Roster >) DataRepository.RosterProvider.DeepSave,
							new object[] { transactionManager, entity.RosterCollection, deepSaveType, childTypes, innerList }
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
	
	#region RosterTypeChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>ClinicDoctor.Entities.RosterType</c>
	///</summary>
	public enum RosterTypeChildEntityTypes
	{

		///<summary>
		/// Collection of <c>RosterType</c> as OneToMany for RosterCollection
		///</summary>
		[ChildEntityType(typeof(TList<Roster>))]
		RosterCollection,
	}
	
	#endregion RosterTypeChildEntityTypes
	
	#region RosterTypeFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;RosterTypeColumn&gt;"/> class
	/// that is used exclusively with a <see cref="RosterType"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class RosterTypeFilterBuilder : SqlFilterBuilder<RosterTypeColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the RosterTypeFilterBuilder class.
		/// </summary>
		public RosterTypeFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the RosterTypeFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public RosterTypeFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the RosterTypeFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public RosterTypeFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion RosterTypeFilterBuilder
	
	#region RosterTypeParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;RosterTypeColumn&gt;"/> class
	/// that is used exclusively with a <see cref="RosterType"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class RosterTypeParameterBuilder : ParameterizedSqlFilterBuilder<RosterTypeColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the RosterTypeParameterBuilder class.
		/// </summary>
		public RosterTypeParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the RosterTypeParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public RosterTypeParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the RosterTypeParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public RosterTypeParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion RosterTypeParameterBuilder
	
	#region RosterTypeSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;RosterTypeColumn&gt;"/> class
	/// that is used exclusively with a <see cref="RosterType"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class RosterTypeSortBuilder : SqlSortBuilder<RosterTypeColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the RosterTypeSqlSortBuilder class.
		/// </summary>
		public RosterTypeSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion RosterTypeSortBuilder
	
} // end namespace
