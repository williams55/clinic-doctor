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
	/// This class is the base class for any <see cref="ContentProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class ContentProviderBaseCore : EntityProviderBase<ClinicDoctor.Entities.Content, ClinicDoctor.Entities.ContentKey>
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
		public override bool Delete(TransactionManager transactionManager, ClinicDoctor.Entities.ContentKey key)
		{
			return Delete(transactionManager, key.Id);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_id">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Int64 _id)
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
		public abstract bool Delete(TransactionManager transactionManager, System.Int64 _id);		
		
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
		public override ClinicDoctor.Entities.Content Get(TransactionManager transactionManager, ClinicDoctor.Entities.ContentKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_Content_FuncId index.
		/// </summary>
		/// <param name="_funcId"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Content&gt;"/> class.</returns>
		public TList<Content> GetByFuncId(System.Int64 _funcId)
		{
			int count = -1;
			return GetByFuncId(null,_funcId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Content_FuncId index.
		/// </summary>
		/// <param name="_funcId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Content&gt;"/> class.</returns>
		public TList<Content> GetByFuncId(System.Int64 _funcId, int start, int pageLength)
		{
			int count = -1;
			return GetByFuncId(null, _funcId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Content_FuncId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_funcId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Content&gt;"/> class.</returns>
		public TList<Content> GetByFuncId(TransactionManager transactionManager, System.Int64 _funcId)
		{
			int count = -1;
			return GetByFuncId(transactionManager, _funcId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Content_FuncId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_funcId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Content&gt;"/> class.</returns>
		public TList<Content> GetByFuncId(TransactionManager transactionManager, System.Int64 _funcId, int start, int pageLength)
		{
			int count = -1;
			return GetByFuncId(transactionManager, _funcId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Content_FuncId index.
		/// </summary>
		/// <param name="_funcId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Content&gt;"/> class.</returns>
		public TList<Content> GetByFuncId(System.Int64 _funcId, int start, int pageLength, out int count)
		{
			return GetByFuncId(null, _funcId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Content_FuncId index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_funcId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Content&gt;"/> class.</returns>
		public abstract TList<Content> GetByFuncId(TransactionManager transactionManager, System.Int64 _funcId, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_Content_FuncId_IsDisabled index.
		/// </summary>
		/// <param name="_funcId"></param>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Content&gt;"/> class.</returns>
		public TList<Content> GetByFuncIdIsDisabled(System.Int64 _funcId, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByFuncIdIsDisabled(null,_funcId, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Content_FuncId_IsDisabled index.
		/// </summary>
		/// <param name="_funcId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Content&gt;"/> class.</returns>
		public TList<Content> GetByFuncIdIsDisabled(System.Int64 _funcId, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByFuncIdIsDisabled(null, _funcId, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Content_FuncId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_funcId"></param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Content&gt;"/> class.</returns>
		public TList<Content> GetByFuncIdIsDisabled(TransactionManager transactionManager, System.Int64 _funcId, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByFuncIdIsDisabled(transactionManager, _funcId, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Content_FuncId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_funcId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Content&gt;"/> class.</returns>
		public TList<Content> GetByFuncIdIsDisabled(TransactionManager transactionManager, System.Int64 _funcId, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByFuncIdIsDisabled(transactionManager, _funcId, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Content_FuncId_IsDisabled index.
		/// </summary>
		/// <param name="_funcId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Content&gt;"/> class.</returns>
		public TList<Content> GetByFuncIdIsDisabled(System.Int64 _funcId, System.Boolean _isDisabled, int start, int pageLength, out int count)
		{
			return GetByFuncIdIsDisabled(null, _funcId, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Content_FuncId_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_funcId"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Content&gt;"/> class.</returns>
		public abstract TList<Content> GetByFuncIdIsDisabled(TransactionManager transactionManager, System.Int64 _funcId, System.Boolean _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_Content_Id_IsDisabled index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Content"/> class.</returns>
		public ClinicDoctor.Entities.Content GetByIdIsDisabled(System.Int64 _id, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByIdIsDisabled(null,_id, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Content_Id_IsDisabled index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Content"/> class.</returns>
		public ClinicDoctor.Entities.Content GetByIdIsDisabled(System.Int64 _id, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIdIsDisabled(null, _id, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Content_Id_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Content"/> class.</returns>
		public ClinicDoctor.Entities.Content GetByIdIsDisabled(TransactionManager transactionManager, System.Int64 _id, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByIdIsDisabled(transactionManager, _id, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Content_Id_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Content"/> class.</returns>
		public ClinicDoctor.Entities.Content GetByIdIsDisabled(TransactionManager transactionManager, System.Int64 _id, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIdIsDisabled(transactionManager, _id, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Content_Id_IsDisabled index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Content"/> class.</returns>
		public ClinicDoctor.Entities.Content GetByIdIsDisabled(System.Int64 _id, System.Boolean _isDisabled, int start, int pageLength, out int count)
		{
			return GetByIdIsDisabled(null, _id, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Content_Id_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Content"/> class.</returns>
		public abstract ClinicDoctor.Entities.Content GetByIdIsDisabled(TransactionManager transactionManager, System.Int64 _id, System.Boolean _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_Content_IsDisabled index.
		/// </summary>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Content&gt;"/> class.</returns>
		public TList<Content> GetByIsDisabled(System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByIsDisabled(null,_isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Content_IsDisabled index.
		/// </summary>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Content&gt;"/> class.</returns>
		public TList<Content> GetByIsDisabled(System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIsDisabled(null, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Content_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Content&gt;"/> class.</returns>
		public TList<Content> GetByIsDisabled(TransactionManager transactionManager, System.Boolean _isDisabled)
		{
			int count = -1;
			return GetByIsDisabled(transactionManager, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Content_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Content&gt;"/> class.</returns>
		public TList<Content> GetByIsDisabled(TransactionManager transactionManager, System.Boolean _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIsDisabled(transactionManager, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Content_IsDisabled index.
		/// </summary>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Content&gt;"/> class.</returns>
		public TList<Content> GetByIsDisabled(System.Boolean _isDisabled, int start, int pageLength, out int count)
		{
			return GetByIsDisabled(null, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Content_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Content&gt;"/> class.</returns>
		public abstract TList<Content> GetByIsDisabled(TransactionManager transactionManager, System.Boolean _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Content index.
		/// </summary>
		/// <param name="_id"></param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Content"/> class.</returns>
		public ClinicDoctor.Entities.Content GetById(System.Int64 _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Content index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Content"/> class.</returns>
		public ClinicDoctor.Entities.Content GetById(System.Int64 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Content index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Content"/> class.</returns>
		public ClinicDoctor.Entities.Content GetById(TransactionManager transactionManager, System.Int64 _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Content index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Content"/> class.</returns>
		public ClinicDoctor.Entities.Content GetById(TransactionManager transactionManager, System.Int64 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Content index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Content"/> class.</returns>
		public ClinicDoctor.Entities.Content GetById(System.Int64 _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Content index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Content"/> class.</returns>
		public abstract ClinicDoctor.Entities.Content GetById(TransactionManager transactionManager, System.Int64 _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Content&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Content&gt;"/></returns>
		public static TList<Content> Fill(IDataReader reader, TList<Content> rows, int start, int pageLength)
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
				
				ClinicDoctor.Entities.Content c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Content")
					.Append("|").Append((System.Int64)reader[((int)ContentColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Content>(
					key.ToString(), // EntityTrackingKey
					"Content",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new ClinicDoctor.Entities.Content();
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
					c.Id = (System.Int64)reader[((int)ContentColumn.Id - 1)];
					c.Title = (System.String)reader[((int)ContentColumn.Title - 1)];
					c.FuncId = (System.Int64)reader[((int)ContentColumn.FuncId - 1)];
					c.FuncTitle = (reader.IsDBNull(((int)ContentColumn.FuncTitle - 1)))?null:(System.String)reader[((int)ContentColumn.FuncTitle - 1)];
					c.Note = (reader.IsDBNull(((int)ContentColumn.Note - 1)))?null:(System.String)reader[((int)ContentColumn.Note - 1)];
					c.IsDisabled = (System.Boolean)reader[((int)ContentColumn.IsDisabled - 1)];
					c.CreateUser = (reader.IsDBNull(((int)ContentColumn.CreateUser - 1)))?null:(System.String)reader[((int)ContentColumn.CreateUser - 1)];
					c.CreateDate = (System.DateTime)reader[((int)ContentColumn.CreateDate - 1)];
					c.UpdateUser = (reader.IsDBNull(((int)ContentColumn.UpdateUser - 1)))?null:(System.String)reader[((int)ContentColumn.UpdateUser - 1)];
					c.UpdateDate = (System.DateTime)reader[((int)ContentColumn.UpdateDate - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="ClinicDoctor.Entities.Content"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="ClinicDoctor.Entities.Content"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, ClinicDoctor.Entities.Content entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (System.Int64)reader[((int)ContentColumn.Id - 1)];
			entity.Title = (System.String)reader[((int)ContentColumn.Title - 1)];
			entity.FuncId = (System.Int64)reader[((int)ContentColumn.FuncId - 1)];
			entity.FuncTitle = (reader.IsDBNull(((int)ContentColumn.FuncTitle - 1)))?null:(System.String)reader[((int)ContentColumn.FuncTitle - 1)];
			entity.Note = (reader.IsDBNull(((int)ContentColumn.Note - 1)))?null:(System.String)reader[((int)ContentColumn.Note - 1)];
			entity.IsDisabled = (System.Boolean)reader[((int)ContentColumn.IsDisabled - 1)];
			entity.CreateUser = (reader.IsDBNull(((int)ContentColumn.CreateUser - 1)))?null:(System.String)reader[((int)ContentColumn.CreateUser - 1)];
			entity.CreateDate = (System.DateTime)reader[((int)ContentColumn.CreateDate - 1)];
			entity.UpdateUser = (reader.IsDBNull(((int)ContentColumn.UpdateUser - 1)))?null:(System.String)reader[((int)ContentColumn.UpdateUser - 1)];
			entity.UpdateDate = (System.DateTime)reader[((int)ContentColumn.UpdateDate - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="ClinicDoctor.Entities.Content"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="ClinicDoctor.Entities.Content"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, ClinicDoctor.Entities.Content entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Id = (System.Int64)dataRow["Id"];
			entity.Title = (System.String)dataRow["Title"];
			entity.FuncId = (System.Int64)dataRow["FuncId"];
			entity.FuncTitle = Convert.IsDBNull(dataRow["FuncTitle"]) ? null : (System.String)dataRow["FuncTitle"];
			entity.Note = Convert.IsDBNull(dataRow["Note"]) ? null : (System.String)dataRow["Note"];
			entity.IsDisabled = (System.Boolean)dataRow["IsDisabled"];
			entity.CreateUser = Convert.IsDBNull(dataRow["CreateUser"]) ? null : (System.String)dataRow["CreateUser"];
			entity.CreateDate = (System.DateTime)dataRow["CreateDate"];
			entity.UpdateUser = Convert.IsDBNull(dataRow["UpdateUser"]) ? null : (System.String)dataRow["UpdateUser"];
			entity.UpdateDate = (System.DateTime)dataRow["UpdateDate"];
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
		/// <param name="entity">The <see cref="ClinicDoctor.Entities.Content"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">ClinicDoctor.Entities.Content Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, ClinicDoctor.Entities.Content entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region FuncIdSource	
			if (CanDeepLoad(entity, "Functionality|FuncIdSource", deepLoadType, innerList) 
				&& entity.FuncIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = entity.FuncId;
				Functionality tmpEntity = EntityManager.LocateEntity<Functionality>(EntityLocator.ConstructKeyFromPkItems(typeof(Functionality), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.FuncIdSource = tmpEntity;
				else
					entity.FuncIdSource = DataRepository.FunctionalityProvider.GetById(transactionManager, entity.FuncId);		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'FuncIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.FuncIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.FunctionalityProvider.DeepLoad(transactionManager, entity.FuncIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion FuncIdSource
			
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

				entity.AppointmentCollection = DataRepository.AppointmentProvider.GetByContentId(transactionManager, entity.Id);

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
		/// Deep Save the entire object graph of the ClinicDoctor.Entities.Content object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">ClinicDoctor.Entities.Content instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">ClinicDoctor.Entities.Content Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, ClinicDoctor.Entities.Content entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			
			#region FuncIdSource
			if (CanDeepSave(entity, "Functionality|FuncIdSource", deepSaveType, innerList) 
				&& entity.FuncIdSource != null)
			{
				DataRepository.FunctionalityProvider.Save(transactionManager, entity.FuncIdSource);
				entity.FuncId = entity.FuncIdSource.Id;
			}
			#endregion 
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
						if(child.ContentIdSource != null)
						{
							child.ContentId = child.ContentIdSource.Id;
						}
						else
						{
							child.ContentId = entity.Id;
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
	
	#region ContentChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>ClinicDoctor.Entities.Content</c>
	///</summary>
	public enum ContentChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Functionality</c> at FuncIdSource
		///</summary>
		[ChildEntityType(typeof(Functionality))]
		Functionality,
	
		///<summary>
		/// Collection of <c>Content</c> as OneToMany for AppointmentCollection
		///</summary>
		[ChildEntityType(typeof(TList<Appointment>))]
		AppointmentCollection,
	}
	
	#endregion ContentChildEntityTypes
	
	#region ContentFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;ContentColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Content"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ContentFilterBuilder : SqlFilterBuilder<ContentColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ContentFilterBuilder class.
		/// </summary>
		public ContentFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the ContentFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ContentFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ContentFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ContentFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ContentFilterBuilder
	
	#region ContentParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;ContentColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Content"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ContentParameterBuilder : ParameterizedSqlFilterBuilder<ContentColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ContentParameterBuilder class.
		/// </summary>
		public ContentParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the ContentParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ContentParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ContentParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ContentParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ContentParameterBuilder
	
	#region ContentSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;ContentColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Content"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class ContentSortBuilder : SqlSortBuilder<ContentColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ContentSqlSortBuilder class.
		/// </summary>
		public ContentSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion ContentSortBuilder
	
} // end namespace
