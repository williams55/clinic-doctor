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
	/// This class is the base class for any <see cref="FunctionalityProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class FunctionalityProviderBaseCore : EntityProviderBase<ClinicDoctor.Entities.Functionality, ClinicDoctor.Entities.FunctionalityKey>
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
		public override bool Delete(TransactionManager transactionManager, ClinicDoctor.Entities.FunctionalityKey key)
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
		public override ClinicDoctor.Entities.Functionality Get(TransactionManager transactionManager, ClinicDoctor.Entities.FunctionalityKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_Functionality_Id_IsDisabled index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Functionality&gt;"/> class.</returns>
		public TList<Functionality> GetByIdIsDisabled(System.Int32 _id, System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByIdIsDisabled(null,_id, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Functionality_Id_IsDisabled index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Functionality&gt;"/> class.</returns>
		public TList<Functionality> GetByIdIsDisabled(System.Int32 _id, System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIdIsDisabled(null, _id, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Functionality_Id_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Functionality&gt;"/> class.</returns>
		public TList<Functionality> GetByIdIsDisabled(TransactionManager transactionManager, System.Int32 _id, System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByIdIsDisabled(transactionManager, _id, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Functionality_Id_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Functionality&gt;"/> class.</returns>
		public TList<Functionality> GetByIdIsDisabled(TransactionManager transactionManager, System.Int32 _id, System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIdIsDisabled(transactionManager, _id, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Functionality_Id_IsDisabled index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Functionality&gt;"/> class.</returns>
		public TList<Functionality> GetByIdIsDisabled(System.Int32 _id, System.Boolean? _isDisabled, int start, int pageLength, out int count)
		{
			return GetByIdIsDisabled(null, _id, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Functionality_Id_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Functionality&gt;"/> class.</returns>
		public abstract TList<Functionality> GetByIdIsDisabled(TransactionManager transactionManager, System.Int32 _id, System.Boolean? _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_Functionality_IsDisabled index.
		/// </summary>
		/// <param name="_isDisabled"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Functionality&gt;"/> class.</returns>
		public TList<Functionality> GetByIsDisabled(System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByIsDisabled(null,_isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Functionality_IsDisabled index.
		/// </summary>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Functionality&gt;"/> class.</returns>
		public TList<Functionality> GetByIsDisabled(System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIsDisabled(null, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Functionality_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_isDisabled"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Functionality&gt;"/> class.</returns>
		public TList<Functionality> GetByIsDisabled(TransactionManager transactionManager, System.Boolean? _isDisabled)
		{
			int count = -1;
			return GetByIsDisabled(transactionManager, _isDisabled, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Functionality_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Functionality&gt;"/> class.</returns>
		public TList<Functionality> GetByIsDisabled(TransactionManager transactionManager, System.Boolean? _isDisabled, int start, int pageLength)
		{
			int count = -1;
			return GetByIsDisabled(transactionManager, _isDisabled, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Functionality_IsDisabled index.
		/// </summary>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Functionality&gt;"/> class.</returns>
		public TList<Functionality> GetByIsDisabled(System.Boolean? _isDisabled, int start, int pageLength, out int count)
		{
			return GetByIsDisabled(null, _isDisabled, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_Functionality_IsDisabled index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_isDisabled"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Functionality&gt;"/> class.</returns>
		public abstract TList<Functionality> GetByIsDisabled(TransactionManager transactionManager, System.Boolean? _isDisabled, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Functionality index.
		/// </summary>
		/// <param name="_id"></param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Functionality"/> class.</returns>
		public ClinicDoctor.Entities.Functionality GetById(System.Int32 _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Functionality index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Functionality"/> class.</returns>
		public ClinicDoctor.Entities.Functionality GetById(System.Int32 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Functionality index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Functionality"/> class.</returns>
		public ClinicDoctor.Entities.Functionality GetById(TransactionManager transactionManager, System.Int32 _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Functionality index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Functionality"/> class.</returns>
		public ClinicDoctor.Entities.Functionality GetById(TransactionManager transactionManager, System.Int32 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Functionality index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Functionality"/> class.</returns>
		public ClinicDoctor.Entities.Functionality GetById(System.Int32 _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Functionality index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="ClinicDoctor.Entities.Functionality"/> class.</returns>
		public abstract ClinicDoctor.Entities.Functionality GetById(TransactionManager transactionManager, System.Int32 _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Functionality&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Functionality&gt;"/></returns>
		public static TList<Functionality> Fill(IDataReader reader, TList<Functionality> rows, int start, int pageLength)
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
				
				ClinicDoctor.Entities.Functionality c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Functionality")
					.Append("|").Append((System.Int32)reader[((int)FunctionalityColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Functionality>(
					key.ToString(), // EntityTrackingKey
					"Functionality",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new ClinicDoctor.Entities.Functionality();
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
					c.Id = (System.Int32)reader[((int)FunctionalityColumn.Id - 1)];
					c.Title = (reader.IsDBNull(((int)FunctionalityColumn.Title - 1)))?null:(System.String)reader[((int)FunctionalityColumn.Title - 1)];
					c.Note = (reader.IsDBNull(((int)FunctionalityColumn.Note - 1)))?null:(System.String)reader[((int)FunctionalityColumn.Note - 1)];
					c.IsDisabled = (reader.IsDBNull(((int)FunctionalityColumn.IsDisabled - 1)))?null:(System.Boolean?)reader[((int)FunctionalityColumn.IsDisabled - 1)];
					c.CreateUser = (reader.IsDBNull(((int)FunctionalityColumn.CreateUser - 1)))?null:(System.String)reader[((int)FunctionalityColumn.CreateUser - 1)];
					c.CreateDate = (reader.IsDBNull(((int)FunctionalityColumn.CreateDate - 1)))?null:(System.DateTime?)reader[((int)FunctionalityColumn.CreateDate - 1)];
					c.UpdateUser = (reader.IsDBNull(((int)FunctionalityColumn.UpdateUser - 1)))?null:(System.String)reader[((int)FunctionalityColumn.UpdateUser - 1)];
					c.UpdateDate = (reader.IsDBNull(((int)FunctionalityColumn.UpdateDate - 1)))?null:(System.DateTime?)reader[((int)FunctionalityColumn.UpdateDate - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="ClinicDoctor.Entities.Functionality"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="ClinicDoctor.Entities.Functionality"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, ClinicDoctor.Entities.Functionality entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (System.Int32)reader[((int)FunctionalityColumn.Id - 1)];
			entity.Title = (reader.IsDBNull(((int)FunctionalityColumn.Title - 1)))?null:(System.String)reader[((int)FunctionalityColumn.Title - 1)];
			entity.Note = (reader.IsDBNull(((int)FunctionalityColumn.Note - 1)))?null:(System.String)reader[((int)FunctionalityColumn.Note - 1)];
			entity.IsDisabled = (reader.IsDBNull(((int)FunctionalityColumn.IsDisabled - 1)))?null:(System.Boolean?)reader[((int)FunctionalityColumn.IsDisabled - 1)];
			entity.CreateUser = (reader.IsDBNull(((int)FunctionalityColumn.CreateUser - 1)))?null:(System.String)reader[((int)FunctionalityColumn.CreateUser - 1)];
			entity.CreateDate = (reader.IsDBNull(((int)FunctionalityColumn.CreateDate - 1)))?null:(System.DateTime?)reader[((int)FunctionalityColumn.CreateDate - 1)];
			entity.UpdateUser = (reader.IsDBNull(((int)FunctionalityColumn.UpdateUser - 1)))?null:(System.String)reader[((int)FunctionalityColumn.UpdateUser - 1)];
			entity.UpdateDate = (reader.IsDBNull(((int)FunctionalityColumn.UpdateDate - 1)))?null:(System.DateTime?)reader[((int)FunctionalityColumn.UpdateDate - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="ClinicDoctor.Entities.Functionality"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="ClinicDoctor.Entities.Functionality"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, ClinicDoctor.Entities.Functionality entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Id = (System.Int32)dataRow["Id"];
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
		/// <param name="entity">The <see cref="ClinicDoctor.Entities.Functionality"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">ClinicDoctor.Entities.Functionality Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, ClinicDoctor.Entities.Functionality entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetById methods when available
			
			#region DoctorFuncCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<DoctorFunc>|DoctorFuncCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'DoctorFuncCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.DoctorFuncCollection = DataRepository.DoctorFuncProvider.GetByFuncId(transactionManager, entity.Id);

				if (deep && entity.DoctorFuncCollection.Count > 0)
				{
					deepHandles.Add("DoctorFuncCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<DoctorFunc>) DataRepository.DoctorFuncProvider.DeepLoad,
						new object[] { transactionManager, entity.DoctorFuncCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			#region ContentCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<Content>|ContentCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'ContentCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.ContentCollection = DataRepository.ContentProvider.GetByFuncId(transactionManager, entity.Id);

				if (deep && entity.ContentCollection.Count > 0)
				{
					deepHandles.Add("ContentCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<Content>) DataRepository.ContentProvider.DeepLoad,
						new object[] { transactionManager, entity.ContentCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			#region RoomFuncCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<RoomFunc>|RoomFuncCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'RoomFuncCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.RoomFuncCollection = DataRepository.RoomFuncProvider.GetByFuncId(transactionManager, entity.Id);

				if (deep && entity.RoomFuncCollection.Count > 0)
				{
					deepHandles.Add("RoomFuncCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<RoomFunc>) DataRepository.RoomFuncProvider.DeepLoad,
						new object[] { transactionManager, entity.RoomFuncCollection, deep, deepLoadType, childTypes, innerList }
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
		/// Deep Save the entire object graph of the ClinicDoctor.Entities.Functionality object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">ClinicDoctor.Entities.Functionality instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">ClinicDoctor.Entities.Functionality Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, ClinicDoctor.Entities.Functionality entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
			#region List<DoctorFunc>
				if (CanDeepSave(entity.DoctorFuncCollection, "List<DoctorFunc>|DoctorFuncCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(DoctorFunc child in entity.DoctorFuncCollection)
					{
						if(child.FuncIdSource != null)
						{
							child.FuncId = child.FuncIdSource.Id;
						}
						else
						{
							child.FuncId = entity.Id;
						}

					}

					if (entity.DoctorFuncCollection.Count > 0 || entity.DoctorFuncCollection.DeletedItems.Count > 0)
					{
						//DataRepository.DoctorFuncProvider.Save(transactionManager, entity.DoctorFuncCollection);
						
						deepHandles.Add("DoctorFuncCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< DoctorFunc >) DataRepository.DoctorFuncProvider.DeepSave,
							new object[] { transactionManager, entity.DoctorFuncCollection, deepSaveType, childTypes, innerList }
						));
					}
				} 
			#endregion 
				
	
			#region List<Content>
				if (CanDeepSave(entity.ContentCollection, "List<Content>|ContentCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(Content child in entity.ContentCollection)
					{
						if(child.FuncIdSource != null)
						{
							child.FuncId = child.FuncIdSource.Id;
						}
						else
						{
							child.FuncId = entity.Id;
						}

					}

					if (entity.ContentCollection.Count > 0 || entity.ContentCollection.DeletedItems.Count > 0)
					{
						//DataRepository.ContentProvider.Save(transactionManager, entity.ContentCollection);
						
						deepHandles.Add("ContentCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< Content >) DataRepository.ContentProvider.DeepSave,
							new object[] { transactionManager, entity.ContentCollection, deepSaveType, childTypes, innerList }
						));
					}
				} 
			#endregion 
				
	
			#region List<RoomFunc>
				if (CanDeepSave(entity.RoomFuncCollection, "List<RoomFunc>|RoomFuncCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(RoomFunc child in entity.RoomFuncCollection)
					{
						if(child.FuncIdSource != null)
						{
							child.FuncId = child.FuncIdSource.Id;
						}
						else
						{
							child.FuncId = entity.Id;
						}

					}

					if (entity.RoomFuncCollection.Count > 0 || entity.RoomFuncCollection.DeletedItems.Count > 0)
					{
						//DataRepository.RoomFuncProvider.Save(transactionManager, entity.RoomFuncCollection);
						
						deepHandles.Add("RoomFuncCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< RoomFunc >) DataRepository.RoomFuncProvider.DeepSave,
							new object[] { transactionManager, entity.RoomFuncCollection, deepSaveType, childTypes, innerList }
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
	
	#region FunctionalityChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>ClinicDoctor.Entities.Functionality</c>
	///</summary>
	public enum FunctionalityChildEntityTypes
	{

		///<summary>
		/// Collection of <c>Functionality</c> as OneToMany for DoctorFuncCollection
		///</summary>
		[ChildEntityType(typeof(TList<DoctorFunc>))]
		DoctorFuncCollection,

		///<summary>
		/// Collection of <c>Functionality</c> as OneToMany for ContentCollection
		///</summary>
		[ChildEntityType(typeof(TList<Content>))]
		ContentCollection,

		///<summary>
		/// Collection of <c>Functionality</c> as OneToMany for RoomFuncCollection
		///</summary>
		[ChildEntityType(typeof(TList<RoomFunc>))]
		RoomFuncCollection,
	}
	
	#endregion FunctionalityChildEntityTypes
	
	#region FunctionalityFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;FunctionalityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Functionality"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class FunctionalityFilterBuilder : SqlFilterBuilder<FunctionalityColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the FunctionalityFilterBuilder class.
		/// </summary>
		public FunctionalityFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the FunctionalityFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public FunctionalityFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the FunctionalityFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public FunctionalityFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion FunctionalityFilterBuilder
	
	#region FunctionalityParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;FunctionalityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Functionality"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class FunctionalityParameterBuilder : ParameterizedSqlFilterBuilder<FunctionalityColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the FunctionalityParameterBuilder class.
		/// </summary>
		public FunctionalityParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the FunctionalityParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public FunctionalityParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the FunctionalityParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public FunctionalityParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion FunctionalityParameterBuilder
	
	#region FunctionalitySortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;FunctionalityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Functionality"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class FunctionalitySortBuilder : SqlSortBuilder<FunctionalityColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the FunctionalitySqlSortBuilder class.
		/// </summary>
		public FunctionalitySortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion FunctionalitySortBuilder
	
} // end namespace
