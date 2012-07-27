#region Using directives

using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;

using AppointmentSystem.Entities;
using AppointmentSystem.Data;

#endregion

namespace AppointmentSystem.Data.Bases
{	
	///<summary>
	/// This class is the base class for any <see cref="PatientProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class PatientProviderBaseCore : EntityProviderBase<AppointmentSystem.Entities.Patient, AppointmentSystem.Entities.PatientKey>
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
		public override bool Delete(TransactionManager transactionManager, AppointmentSystem.Entities.PatientKey key)
		{
			return Delete(transactionManager, key.PatientCode);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_patientCode">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.String _patientCode)
		{
			return Delete(null, _patientCode);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_patientCode">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.String _patientCode);		
		
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
		public override AppointmentSystem.Entities.Patient Get(TransactionManager transactionManager, AppointmentSystem.Entities.PatientKey key, int start, int pageLength)
		{
			return GetByPatientCode(transactionManager, key.PatientCode, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Patient index.
		/// </summary>
		/// <param name="_patientCode"></param>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.Patient"/> class.</returns>
		public AppointmentSystem.Entities.Patient GetByPatientCode(System.String _patientCode)
		{
			int count = -1;
			return GetByPatientCode(null,_patientCode, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Patient index.
		/// </summary>
		/// <param name="_patientCode"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.Patient"/> class.</returns>
		public AppointmentSystem.Entities.Patient GetByPatientCode(System.String _patientCode, int start, int pageLength)
		{
			int count = -1;
			return GetByPatientCode(null, _patientCode, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Patient index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_patientCode"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.Patient"/> class.</returns>
		public AppointmentSystem.Entities.Patient GetByPatientCode(TransactionManager transactionManager, System.String _patientCode)
		{
			int count = -1;
			return GetByPatientCode(transactionManager, _patientCode, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Patient index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_patientCode"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.Patient"/> class.</returns>
		public AppointmentSystem.Entities.Patient GetByPatientCode(TransactionManager transactionManager, System.String _patientCode, int start, int pageLength)
		{
			int count = -1;
			return GetByPatientCode(transactionManager, _patientCode, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Patient index.
		/// </summary>
		/// <param name="_patientCode"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.Patient"/> class.</returns>
		public AppointmentSystem.Entities.Patient GetByPatientCode(System.String _patientCode, int start, int pageLength, out int count)
		{
			return GetByPatientCode(null, _patientCode, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Patient index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_patientCode"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.Patient"/> class.</returns>
		public abstract AppointmentSystem.Entities.Patient GetByPatientCode(TransactionManager transactionManager, System.String _patientCode, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Patient&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Patient&gt;"/></returns>
		public static TList<Patient> Fill(IDataReader reader, TList<Patient> rows, int start, int pageLength)
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
				
				AppointmentSystem.Entities.Patient c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Patient")
					.Append("|").Append((System.String)reader[((int)PatientColumn.PatientCode - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Patient>(
					key.ToString(), // EntityTrackingKey
					"Patient",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new AppointmentSystem.Entities.Patient();
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
					c.PatientCode = (System.String)reader[((int)PatientColumn.PatientCode - 1)];
					c.OriginalPatientCode = c.PatientCode;
					c.FirstName = (System.String)reader[((int)PatientColumn.FirstName - 1)];
					c.LastName = (System.String)reader[((int)PatientColumn.LastName - 1)];
					c.MemberType = (reader.IsDBNull(((int)PatientColumn.MemberType - 1)))?null:(System.String)reader[((int)PatientColumn.MemberType - 1)];
					c.HomePhone = (reader.IsDBNull(((int)PatientColumn.HomePhone - 1)))?null:(System.String)reader[((int)PatientColumn.HomePhone - 1)];
					c.WorkPhone = (reader.IsDBNull(((int)PatientColumn.WorkPhone - 1)))?null:(System.String)reader[((int)PatientColumn.WorkPhone - 1)];
					c.CellPhone = (reader.IsDBNull(((int)PatientColumn.CellPhone - 1)))?null:(System.String)reader[((int)PatientColumn.CellPhone - 1)];
					c.Avatar = (reader.IsDBNull(((int)PatientColumn.Avatar - 1)))?null:(System.String)reader[((int)PatientColumn.Avatar - 1)];
					c.CompanyCode = (reader.IsDBNull(((int)PatientColumn.CompanyCode - 1)))?null:(System.String)reader[((int)PatientColumn.CompanyCode - 1)];
					c.Birthdate = (reader.IsDBNull(((int)PatientColumn.Birthdate - 1)))?null:(System.DateTime?)reader[((int)PatientColumn.Birthdate - 1)];
					c.IsFemale = (System.Boolean)reader[((int)PatientColumn.IsFemale - 1)];
					c.Title = (reader.IsDBNull(((int)PatientColumn.Title - 1)))?null:(System.String)reader[((int)PatientColumn.Title - 1)];
					c.Note = (reader.IsDBNull(((int)PatientColumn.Note - 1)))?null:(System.String)reader[((int)PatientColumn.Note - 1)];
					c.IsDisabled = (System.Boolean)reader[((int)PatientColumn.IsDisabled - 1)];
					c.CreateUser = (reader.IsDBNull(((int)PatientColumn.CreateUser - 1)))?null:(System.String)reader[((int)PatientColumn.CreateUser - 1)];
					c.CreateDate = (System.DateTime)reader[((int)PatientColumn.CreateDate - 1)];
					c.UpdateUser = (reader.IsDBNull(((int)PatientColumn.UpdateUser - 1)))?null:(System.String)reader[((int)PatientColumn.UpdateUser - 1)];
					c.UpdateDate = (System.DateTime)reader[((int)PatientColumn.UpdateDate - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="AppointmentSystem.Entities.Patient"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.Patient"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, AppointmentSystem.Entities.Patient entity)
		{
			if (!reader.Read()) return;
			
			entity.PatientCode = (System.String)reader[((int)PatientColumn.PatientCode - 1)];
			entity.OriginalPatientCode = (System.String)reader["PatientCode"];
			entity.FirstName = (System.String)reader[((int)PatientColumn.FirstName - 1)];
			entity.LastName = (System.String)reader[((int)PatientColumn.LastName - 1)];
			entity.MemberType = (reader.IsDBNull(((int)PatientColumn.MemberType - 1)))?null:(System.String)reader[((int)PatientColumn.MemberType - 1)];
			entity.HomePhone = (reader.IsDBNull(((int)PatientColumn.HomePhone - 1)))?null:(System.String)reader[((int)PatientColumn.HomePhone - 1)];
			entity.WorkPhone = (reader.IsDBNull(((int)PatientColumn.WorkPhone - 1)))?null:(System.String)reader[((int)PatientColumn.WorkPhone - 1)];
			entity.CellPhone = (reader.IsDBNull(((int)PatientColumn.CellPhone - 1)))?null:(System.String)reader[((int)PatientColumn.CellPhone - 1)];
			entity.Avatar = (reader.IsDBNull(((int)PatientColumn.Avatar - 1)))?null:(System.String)reader[((int)PatientColumn.Avatar - 1)];
			entity.CompanyCode = (reader.IsDBNull(((int)PatientColumn.CompanyCode - 1)))?null:(System.String)reader[((int)PatientColumn.CompanyCode - 1)];
			entity.Birthdate = (reader.IsDBNull(((int)PatientColumn.Birthdate - 1)))?null:(System.DateTime?)reader[((int)PatientColumn.Birthdate - 1)];
			entity.IsFemale = (System.Boolean)reader[((int)PatientColumn.IsFemale - 1)];
			entity.Title = (reader.IsDBNull(((int)PatientColumn.Title - 1)))?null:(System.String)reader[((int)PatientColumn.Title - 1)];
			entity.Note = (reader.IsDBNull(((int)PatientColumn.Note - 1)))?null:(System.String)reader[((int)PatientColumn.Note - 1)];
			entity.IsDisabled = (System.Boolean)reader[((int)PatientColumn.IsDisabled - 1)];
			entity.CreateUser = (reader.IsDBNull(((int)PatientColumn.CreateUser - 1)))?null:(System.String)reader[((int)PatientColumn.CreateUser - 1)];
			entity.CreateDate = (System.DateTime)reader[((int)PatientColumn.CreateDate - 1)];
			entity.UpdateUser = (reader.IsDBNull(((int)PatientColumn.UpdateUser - 1)))?null:(System.String)reader[((int)PatientColumn.UpdateUser - 1)];
			entity.UpdateDate = (System.DateTime)reader[((int)PatientColumn.UpdateDate - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="AppointmentSystem.Entities.Patient"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.Patient"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, AppointmentSystem.Entities.Patient entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.PatientCode = (System.String)dataRow["PatientCode"];
			entity.OriginalPatientCode = (System.String)dataRow["PatientCode"];
			entity.FirstName = (System.String)dataRow["FirstName"];
			entity.LastName = (System.String)dataRow["LastName"];
			entity.MemberType = Convert.IsDBNull(dataRow["MemberType"]) ? null : (System.String)dataRow["MemberType"];
			entity.HomePhone = Convert.IsDBNull(dataRow["HomePhone"]) ? null : (System.String)dataRow["HomePhone"];
			entity.WorkPhone = Convert.IsDBNull(dataRow["WorkPhone"]) ? null : (System.String)dataRow["WorkPhone"];
			entity.CellPhone = Convert.IsDBNull(dataRow["CellPhone"]) ? null : (System.String)dataRow["CellPhone"];
			entity.Avatar = Convert.IsDBNull(dataRow["Avatar"]) ? null : (System.String)dataRow["Avatar"];
			entity.CompanyCode = Convert.IsDBNull(dataRow["CompanyCode"]) ? null : (System.String)dataRow["CompanyCode"];
			entity.Birthdate = Convert.IsDBNull(dataRow["Birthdate"]) ? null : (System.DateTime?)dataRow["Birthdate"];
			entity.IsFemale = (System.Boolean)dataRow["IsFemale"];
			entity.Title = Convert.IsDBNull(dataRow["Title"]) ? null : (System.String)dataRow["Title"];
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
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.Patient"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">AppointmentSystem.Entities.Patient Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, AppointmentSystem.Entities.Patient entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetByPatientCode methods when available
			
			#region AppointmentCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<Appointment>|AppointmentCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'AppointmentCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.AppointmentCollection = DataRepository.AppointmentProvider.GetByPatientCode(transactionManager, entity.PatientCode);

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
		/// Deep Save the entire object graph of the AppointmentSystem.Entities.Patient object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">AppointmentSystem.Entities.Patient instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">AppointmentSystem.Entities.Patient Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, AppointmentSystem.Entities.Patient entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
						if(child.PatientCodeSource != null)
						{
							child.PatientCode = child.PatientCodeSource.PatientCode;
						}
						else
						{
							child.PatientCode = entity.PatientCode;
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
	
	#region PatientChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>AppointmentSystem.Entities.Patient</c>
	///</summary>
	public enum PatientChildEntityTypes
	{

		///<summary>
		/// Collection of <c>Patient</c> as OneToMany for AppointmentCollection
		///</summary>
		[ChildEntityType(typeof(TList<Appointment>))]
		AppointmentCollection,
	}
	
	#endregion PatientChildEntityTypes
	
	#region PatientFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;PatientColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Patient"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PatientFilterBuilder : SqlFilterBuilder<PatientColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PatientFilterBuilder class.
		/// </summary>
		public PatientFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PatientFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PatientFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PatientFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PatientFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PatientFilterBuilder
	
	#region PatientParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;PatientColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Patient"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PatientParameterBuilder : ParameterizedSqlFilterBuilder<PatientColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PatientParameterBuilder class.
		/// </summary>
		public PatientParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PatientParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PatientParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PatientParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PatientParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PatientParameterBuilder
	
	#region PatientSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;PatientColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Patient"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class PatientSortBuilder : SqlSortBuilder<PatientColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PatientSqlSortBuilder class.
		/// </summary>
		public PatientSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion PatientSortBuilder
	
} // end namespace
