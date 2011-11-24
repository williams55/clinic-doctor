﻿#region Using directives

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
	/// This class is the base class for any <see cref="DoctorFuncProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class DoctorFuncProviderBaseCore : EntityProviderBase<ClinicDoctor.Entities.DoctorFunc, ClinicDoctor.Entities.DoctorFuncKey>
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
		public override bool Delete(TransactionManager transactionManager, ClinicDoctor.Entities.DoctorFuncKey key)
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
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_DoctorFunc_Functionality key.
		///		FK_DoctorFunc_Functionality Description: 
		/// </summary>
		/// <param name="_funcId"></param>
		/// <returns>Returns a typed collection of ClinicDoctor.Entities.DoctorFunc objects.</returns>
		public TList<DoctorFunc> GetByFuncId(System.Int32? _funcId)
		{
			int count = -1;
			return GetByFuncId(_funcId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_DoctorFunc_Functionality key.
		///		FK_DoctorFunc_Functionality Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_funcId"></param>
		/// <returns>Returns a typed collection of ClinicDoctor.Entities.DoctorFunc objects.</returns>
		/// <remarks></remarks>
		public TList<DoctorFunc> GetByFuncId(TransactionManager transactionManager, System.Int32? _funcId)
		{
			int count = -1;
			return GetByFuncId(transactionManager, _funcId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_DoctorFunc_Functionality key.
		///		FK_DoctorFunc_Functionality Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_funcId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of ClinicDoctor.Entities.DoctorFunc objects.</returns>
		public TList<DoctorFunc> GetByFuncId(TransactionManager transactionManager, System.Int32? _funcId, int start, int pageLength)
		{
			int count = -1;
			return GetByFuncId(transactionManager, _funcId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_DoctorFunc_Functionality key.
		///		fkDoctorFuncFunctionality Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_funcId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of ClinicDoctor.Entities.DoctorFunc objects.</returns>
		public TList<DoctorFunc> GetByFuncId(System.Int32? _funcId, int start, int pageLength)
		{
			int count =  -1;
			return GetByFuncId(null, _funcId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_DoctorFunc_Functionality key.
		///		fkDoctorFuncFunctionality Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_funcId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of ClinicDoctor.Entities.DoctorFunc objects.</returns>
		public TList<DoctorFunc> GetByFuncId(System.Int32? _funcId, int start, int pageLength,out int count)
		{
			return GetByFuncId(null, _funcId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_DoctorFunc_Functionality key.
		///		FK_DoctorFunc_Functionality Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_funcId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of ClinicDoctor.Entities.DoctorFunc objects.</returns>
		public abstract TList<DoctorFunc> GetByFuncId(TransactionManager transactionManager, System.Int32? _funcId, int start, int pageLength, out int count);
		
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_DoctorFunc_Staff key.
		///		FK_DoctorFunc_Staff Description: 
		/// </summary>
		/// <param name="_doctorId"></param>
		/// <returns>Returns a typed collection of ClinicDoctor.Entities.DoctorFunc objects.</returns>
		public TList<DoctorFunc> GetByDoctorId(System.Int32? _doctorId)
		{
			int count = -1;
			return GetByDoctorId(_doctorId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_DoctorFunc_Staff key.
		///		FK_DoctorFunc_Staff Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorId"></param>
		/// <returns>Returns a typed collection of ClinicDoctor.Entities.DoctorFunc objects.</returns>
		/// <remarks></remarks>
		public TList<DoctorFunc> GetByDoctorId(TransactionManager transactionManager, System.Int32? _doctorId)
		{
			int count = -1;
			return GetByDoctorId(transactionManager, _doctorId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_DoctorFunc_Staff key.
		///		FK_DoctorFunc_Staff Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of ClinicDoctor.Entities.DoctorFunc objects.</returns>
		public TList<DoctorFunc> GetByDoctorId(TransactionManager transactionManager, System.Int32? _doctorId, int start, int pageLength)
		{
			int count = -1;
			return GetByDoctorId(transactionManager, _doctorId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_DoctorFunc_Staff key.
		///		fkDoctorFuncStaff Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_doctorId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of ClinicDoctor.Entities.DoctorFunc objects.</returns>
		public TList<DoctorFunc> GetByDoctorId(System.Int32? _doctorId, int start, int pageLength)
		{
			int count =  -1;
			return GetByDoctorId(null, _doctorId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_DoctorFunc_Staff key.
		///		fkDoctorFuncStaff Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_doctorId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of ClinicDoctor.Entities.DoctorFunc objects.</returns>
		public TList<DoctorFunc> GetByDoctorId(System.Int32? _doctorId, int start, int pageLength,out int count)
		{
			return GetByDoctorId(null, _doctorId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_DoctorFunc_Staff key.
		///		FK_DoctorFunc_Staff Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doctorId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of ClinicDoctor.Entities.DoctorFunc objects.</returns>
		public abstract TList<DoctorFunc> GetByDoctorId(TransactionManager transactionManager, System.Int32? _doctorId, int start, int pageLength, out int count);
		
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
		public override ClinicDoctor.Entities.DoctorFunc Get(TransactionManager transactionManager, ClinicDoctor.Entities.DoctorFuncKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_DoctorFunc index.
		/// </summary>
		/// <param name="_id"></param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.DoctorFunc"/> class.</returns>
		public ClinicDoctor.Entities.DoctorFunc GetById(System.Int32 _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_DoctorFunc index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.DoctorFunc"/> class.</returns>
		public ClinicDoctor.Entities.DoctorFunc GetById(System.Int32 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_DoctorFunc index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.DoctorFunc"/> class.</returns>
		public ClinicDoctor.Entities.DoctorFunc GetById(TransactionManager transactionManager, System.Int32 _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_DoctorFunc index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.DoctorFunc"/> class.</returns>
		public ClinicDoctor.Entities.DoctorFunc GetById(TransactionManager transactionManager, System.Int32 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_DoctorFunc index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.DoctorFunc"/> class.</returns>
		public ClinicDoctor.Entities.DoctorFunc GetById(System.Int32 _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_DoctorFunc index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.DoctorFunc"/> class.</returns>
		public abstract ClinicDoctor.Entities.DoctorFunc GetById(TransactionManager transactionManager, System.Int32 _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;DoctorFunc&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;DoctorFunc&gt;"/></returns>
		public static TList<DoctorFunc> Fill(IDataReader reader, TList<DoctorFunc> rows, int start, int pageLength)
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
				
				ClinicDoctor.Entities.DoctorFunc c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("DoctorFunc")
					.Append("|").Append((System.Int32)reader[((int)DoctorFuncColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<DoctorFunc>(
					key.ToString(), // EntityTrackingKey
					"DoctorFunc",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new ClinicDoctor.Entities.DoctorFunc();
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
					c.Id = (System.Int32)reader[((int)DoctorFuncColumn.Id - 1)];
					c.DoctorId = (reader.IsDBNull(((int)DoctorFuncColumn.DoctorId - 1)))?null:(System.Int32?)reader[((int)DoctorFuncColumn.DoctorId - 1)];
					c.FuncId = (reader.IsDBNull(((int)DoctorFuncColumn.FuncId - 1)))?null:(System.Int32?)reader[((int)DoctorFuncColumn.FuncId - 1)];
					c.IsDisabled = (reader.IsDBNull(((int)DoctorFuncColumn.IsDisabled - 1)))?null:(System.Boolean?)reader[((int)DoctorFuncColumn.IsDisabled - 1)];
					c.CreateUser = (reader.IsDBNull(((int)DoctorFuncColumn.CreateUser - 1)))?null:(System.String)reader[((int)DoctorFuncColumn.CreateUser - 1)];
					c.CreateDate = (reader.IsDBNull(((int)DoctorFuncColumn.CreateDate - 1)))?null:(System.DateTime?)reader[((int)DoctorFuncColumn.CreateDate - 1)];
					c.UpdateUser = (reader.IsDBNull(((int)DoctorFuncColumn.UpdateUser - 1)))?null:(System.String)reader[((int)DoctorFuncColumn.UpdateUser - 1)];
					c.UpdateDate = (reader.IsDBNull(((int)DoctorFuncColumn.UpdateDate - 1)))?null:(System.DateTime?)reader[((int)DoctorFuncColumn.UpdateDate - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="ClinicDoctor.Entities.DoctorFunc"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="ClinicDoctor.Entities.DoctorFunc"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, ClinicDoctor.Entities.DoctorFunc entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (System.Int32)reader[((int)DoctorFuncColumn.Id - 1)];
			entity.DoctorId = (reader.IsDBNull(((int)DoctorFuncColumn.DoctorId - 1)))?null:(System.Int32?)reader[((int)DoctorFuncColumn.DoctorId - 1)];
			entity.FuncId = (reader.IsDBNull(((int)DoctorFuncColumn.FuncId - 1)))?null:(System.Int32?)reader[((int)DoctorFuncColumn.FuncId - 1)];
			entity.IsDisabled = (reader.IsDBNull(((int)DoctorFuncColumn.IsDisabled - 1)))?null:(System.Boolean?)reader[((int)DoctorFuncColumn.IsDisabled - 1)];
			entity.CreateUser = (reader.IsDBNull(((int)DoctorFuncColumn.CreateUser - 1)))?null:(System.String)reader[((int)DoctorFuncColumn.CreateUser - 1)];
			entity.CreateDate = (reader.IsDBNull(((int)DoctorFuncColumn.CreateDate - 1)))?null:(System.DateTime?)reader[((int)DoctorFuncColumn.CreateDate - 1)];
			entity.UpdateUser = (reader.IsDBNull(((int)DoctorFuncColumn.UpdateUser - 1)))?null:(System.String)reader[((int)DoctorFuncColumn.UpdateUser - 1)];
			entity.UpdateDate = (reader.IsDBNull(((int)DoctorFuncColumn.UpdateDate - 1)))?null:(System.DateTime?)reader[((int)DoctorFuncColumn.UpdateDate - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="ClinicDoctor.Entities.DoctorFunc"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="ClinicDoctor.Entities.DoctorFunc"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, ClinicDoctor.Entities.DoctorFunc entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Id = (System.Int32)dataRow["Id"];
			entity.DoctorId = Convert.IsDBNull(dataRow["DoctorId"]) ? null : (System.Int32?)dataRow["DoctorId"];
			entity.FuncId = Convert.IsDBNull(dataRow["FuncId"]) ? null : (System.Int32?)dataRow["FuncId"];
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
		/// <param name="entity">The <see cref="ClinicDoctor.Entities.DoctorFunc"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">ClinicDoctor.Entities.DoctorFunc Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, ClinicDoctor.Entities.DoctorFunc entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region FuncIdSource	
			if (CanDeepLoad(entity, "Functionality|FuncIdSource", deepLoadType, innerList) 
				&& entity.FuncIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.FuncId ?? (int)0);
				Functionality tmpEntity = EntityManager.LocateEntity<Functionality>(EntityLocator.ConstructKeyFromPkItems(typeof(Functionality), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.FuncIdSource = tmpEntity;
				else
					entity.FuncIdSource = DataRepository.FunctionalityProvider.GetById(transactionManager, (entity.FuncId ?? (int)0));		
				
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

			#region DoctorIdSource	
			if (CanDeepLoad(entity, "Staff|DoctorIdSource", deepLoadType, innerList) 
				&& entity.DoctorIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.DoctorId ?? (int)0);
				Staff tmpEntity = EntityManager.LocateEntity<Staff>(EntityLocator.ConstructKeyFromPkItems(typeof(Staff), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.DoctorIdSource = tmpEntity;
				else
					entity.DoctorIdSource = DataRepository.StaffProvider.GetById(transactionManager, (entity.DoctorId ?? (int)0));		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'DoctorIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.DoctorIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.StaffProvider.DeepLoad(transactionManager, entity.DoctorIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion DoctorIdSource
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			
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
		/// Deep Save the entire object graph of the ClinicDoctor.Entities.DoctorFunc object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">ClinicDoctor.Entities.DoctorFunc instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">ClinicDoctor.Entities.DoctorFunc Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, ClinicDoctor.Entities.DoctorFunc entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
			
			#region DoctorIdSource
			if (CanDeepSave(entity, "Staff|DoctorIdSource", deepSaveType, innerList) 
				&& entity.DoctorIdSource != null)
			{
				DataRepository.StaffProvider.Save(transactionManager, entity.DoctorIdSource);
				entity.DoctorId = entity.DoctorIdSource.Id;
			}
			#endregion 
			#endregion Composite Parent Properties

			// Save Root Entity through Provider
			if (!entity.IsDeleted)
				this.Save(transactionManager, entity);
			
			//used to hold DeepSave method delegates and fire after all the local children have been saved.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
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
	
	#region DoctorFuncChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>ClinicDoctor.Entities.DoctorFunc</c>
	///</summary>
	public enum DoctorFuncChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Functionality</c> at FuncIdSource
		///</summary>
		[ChildEntityType(typeof(Functionality))]
		Functionality,
			
		///<summary>
		/// Composite Property for <c>Staff</c> at DoctorIdSource
		///</summary>
		[ChildEntityType(typeof(Staff))]
		Staff,
		}
	
	#endregion DoctorFuncChildEntityTypes
	
	#region DoctorFuncFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;DoctorFuncColumn&gt;"/> class
	/// that is used exclusively with a <see cref="DoctorFunc"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class DoctorFuncFilterBuilder : SqlFilterBuilder<DoctorFuncColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DoctorFuncFilterBuilder class.
		/// </summary>
		public DoctorFuncFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the DoctorFuncFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public DoctorFuncFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the DoctorFuncFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public DoctorFuncFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion DoctorFuncFilterBuilder
	
	#region DoctorFuncParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;DoctorFuncColumn&gt;"/> class
	/// that is used exclusively with a <see cref="DoctorFunc"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class DoctorFuncParameterBuilder : ParameterizedSqlFilterBuilder<DoctorFuncColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DoctorFuncParameterBuilder class.
		/// </summary>
		public DoctorFuncParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the DoctorFuncParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public DoctorFuncParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the DoctorFuncParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public DoctorFuncParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion DoctorFuncParameterBuilder
	
	#region DoctorFuncSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;DoctorFuncColumn&gt;"/> class
	/// that is used exclusively with a <see cref="DoctorFunc"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class DoctorFuncSortBuilder : SqlSortBuilder<DoctorFuncColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DoctorFuncSqlSortBuilder class.
		/// </summary>
		public DoctorFuncSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion DoctorFuncSortBuilder
	
} // end namespace