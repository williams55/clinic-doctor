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
	/// This class is the base class for any <see cref="ScreenProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class ScreenProviderBaseCore : EntityProviderBase<AppointmentSystem.Entities.Screen, AppointmentSystem.Entities.ScreenKey>
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
		public override bool Delete(TransactionManager transactionManager, AppointmentSystem.Entities.ScreenKey key)
		{
			return Delete(transactionManager, key.ScreenCode);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_screenCode">Link name of screen. 
		/// 		/// Ex: Status, Appointment.... Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.String _screenCode)
		{
			return Delete(null, _screenCode);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_screenCode">Link name of screen. 
		/// 		/// Ex: Status, Appointment.... Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.String _screenCode);		
		
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
		public override AppointmentSystem.Entities.Screen Get(TransactionManager transactionManager, AppointmentSystem.Entities.ScreenKey key, int start, int pageLength)
		{
			return GetByScreenCode(transactionManager, key.ScreenCode, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Screen index.
		/// </summary>
		/// <param name="_screenCode">Link name of screen. 
		/// 		/// Ex: Status, Appointment...</param>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.Screen"/> class.</returns>
		public AppointmentSystem.Entities.Screen GetByScreenCode(System.String _screenCode)
		{
			int count = -1;
			return GetByScreenCode(null,_screenCode, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Screen index.
		/// </summary>
		/// <param name="_screenCode">Link name of screen. 
		/// 		/// Ex: Status, Appointment...</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.Screen"/> class.</returns>
		public AppointmentSystem.Entities.Screen GetByScreenCode(System.String _screenCode, int start, int pageLength)
		{
			int count = -1;
			return GetByScreenCode(null, _screenCode, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Screen index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_screenCode">Link name of screen. 
		/// 		/// Ex: Status, Appointment...</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.Screen"/> class.</returns>
		public AppointmentSystem.Entities.Screen GetByScreenCode(TransactionManager transactionManager, System.String _screenCode)
		{
			int count = -1;
			return GetByScreenCode(transactionManager, _screenCode, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Screen index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_screenCode">Link name of screen. 
		/// 		/// Ex: Status, Appointment...</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.Screen"/> class.</returns>
		public AppointmentSystem.Entities.Screen GetByScreenCode(TransactionManager transactionManager, System.String _screenCode, int start, int pageLength)
		{
			int count = -1;
			return GetByScreenCode(transactionManager, _screenCode, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Screen index.
		/// </summary>
		/// <param name="_screenCode">Link name of screen. 
		/// 		/// Ex: Status, Appointment...</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.Screen"/> class.</returns>
		public AppointmentSystem.Entities.Screen GetByScreenCode(System.String _screenCode, int start, int pageLength, out int count)
		{
			return GetByScreenCode(null, _screenCode, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Screen index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_screenCode">Link name of screen. 
		/// 		/// Ex: Status, Appointment...</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="AppointmentSystem.Entities.Screen"/> class.</returns>
		public abstract AppointmentSystem.Entities.Screen GetByScreenCode(TransactionManager transactionManager, System.String _screenCode, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Screen&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Screen&gt;"/></returns>
		public static TList<Screen> Fill(IDataReader reader, TList<Screen> rows, int start, int pageLength)
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
				
				AppointmentSystem.Entities.Screen c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Screen")
					.Append("|").Append((System.String)reader[((int)ScreenColumn.ScreenCode - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Screen>(
					key.ToString(), // EntityTrackingKey
					"Screen",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new AppointmentSystem.Entities.Screen();
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
					c.ScreenCode = (System.String)reader[((int)ScreenColumn.ScreenCode - 1)];
					c.OriginalScreenCode = c.ScreenCode;
					c.ScreenName = (reader.IsDBNull(((int)ScreenColumn.ScreenName - 1)))?null:(System.String)reader[((int)ScreenColumn.ScreenName - 1)];
					c.PriorityIndex = (System.Int32)reader[((int)ScreenColumn.PriorityIndex - 1)];
					c.IsDisabled = (System.Boolean)reader[((int)ScreenColumn.IsDisabled - 1)];
					c.CreateUser = (reader.IsDBNull(((int)ScreenColumn.CreateUser - 1)))?null:(System.String)reader[((int)ScreenColumn.CreateUser - 1)];
					c.CreateDate = (System.DateTime)reader[((int)ScreenColumn.CreateDate - 1)];
					c.UpdateUser = (reader.IsDBNull(((int)ScreenColumn.UpdateUser - 1)))?null:(System.String)reader[((int)ScreenColumn.UpdateUser - 1)];
					c.UpdateDate = (System.DateTime)reader[((int)ScreenColumn.UpdateDate - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="AppointmentSystem.Entities.Screen"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.Screen"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, AppointmentSystem.Entities.Screen entity)
		{
			if (!reader.Read()) return;
			
			entity.ScreenCode = (System.String)reader[((int)ScreenColumn.ScreenCode - 1)];
			entity.OriginalScreenCode = (System.String)reader["ScreenCode"];
			entity.ScreenName = (reader.IsDBNull(((int)ScreenColumn.ScreenName - 1)))?null:(System.String)reader[((int)ScreenColumn.ScreenName - 1)];
			entity.PriorityIndex = (System.Int32)reader[((int)ScreenColumn.PriorityIndex - 1)];
			entity.IsDisabled = (System.Boolean)reader[((int)ScreenColumn.IsDisabled - 1)];
			entity.CreateUser = (reader.IsDBNull(((int)ScreenColumn.CreateUser - 1)))?null:(System.String)reader[((int)ScreenColumn.CreateUser - 1)];
			entity.CreateDate = (System.DateTime)reader[((int)ScreenColumn.CreateDate - 1)];
			entity.UpdateUser = (reader.IsDBNull(((int)ScreenColumn.UpdateUser - 1)))?null:(System.String)reader[((int)ScreenColumn.UpdateUser - 1)];
			entity.UpdateDate = (System.DateTime)reader[((int)ScreenColumn.UpdateDate - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="AppointmentSystem.Entities.Screen"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.Screen"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, AppointmentSystem.Entities.Screen entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.ScreenCode = (System.String)dataRow["ScreenCode"];
			entity.OriginalScreenCode = (System.String)dataRow["ScreenCode"];
			entity.ScreenName = Convert.IsDBNull(dataRow["ScreenName"]) ? null : (System.String)dataRow["ScreenName"];
			entity.PriorityIndex = (System.Int32)dataRow["PriorityIndex"];
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
		/// <param name="entity">The <see cref="AppointmentSystem.Entities.Screen"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">AppointmentSystem.Entities.Screen Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, AppointmentSystem.Entities.Screen entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetByScreenCode methods when available
			
			#region RoleDetailCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<RoleDetail>|RoleDetailCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'RoleDetailCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.RoleDetailCollection = DataRepository.RoleDetailProvider.GetByScreenCode(transactionManager, entity.ScreenCode);

				if (deep && entity.RoleDetailCollection.Count > 0)
				{
					deepHandles.Add("RoleDetailCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<RoleDetail>) DataRepository.RoleDetailProvider.DeepLoad,
						new object[] { transactionManager, entity.RoleDetailCollection, deep, deepLoadType, childTypes, innerList }
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
		/// Deep Save the entire object graph of the AppointmentSystem.Entities.Screen object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">AppointmentSystem.Entities.Screen instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">AppointmentSystem.Entities.Screen Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, AppointmentSystem.Entities.Screen entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
			#region List<RoleDetail>
				if (CanDeepSave(entity.RoleDetailCollection, "List<RoleDetail>|RoleDetailCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(RoleDetail child in entity.RoleDetailCollection)
					{
						if(child.ScreenCodeSource != null)
						{
							child.ScreenCode = child.ScreenCodeSource.ScreenCode;
						}
						else
						{
							child.ScreenCode = entity.ScreenCode;
						}

					}

					if (entity.RoleDetailCollection.Count > 0 || entity.RoleDetailCollection.DeletedItems.Count > 0)
					{
						//DataRepository.RoleDetailProvider.Save(transactionManager, entity.RoleDetailCollection);
						
						deepHandles.Add("RoleDetailCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< RoleDetail >) DataRepository.RoleDetailProvider.DeepSave,
							new object[] { transactionManager, entity.RoleDetailCollection, deepSaveType, childTypes, innerList }
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
	
	#region ScreenChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>AppointmentSystem.Entities.Screen</c>
	///</summary>
	public enum ScreenChildEntityTypes
	{

		///<summary>
		/// Collection of <c>Screen</c> as OneToMany for RoleDetailCollection
		///</summary>
		[ChildEntityType(typeof(TList<RoleDetail>))]
		RoleDetailCollection,
	}
	
	#endregion ScreenChildEntityTypes
	
	#region ScreenFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;ScreenColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Screen"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ScreenFilterBuilder : SqlFilterBuilder<ScreenColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ScreenFilterBuilder class.
		/// </summary>
		public ScreenFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the ScreenFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ScreenFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ScreenFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ScreenFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ScreenFilterBuilder
	
	#region ScreenParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;ScreenColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Screen"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ScreenParameterBuilder : ParameterizedSqlFilterBuilder<ScreenColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ScreenParameterBuilder class.
		/// </summary>
		public ScreenParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the ScreenParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ScreenParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ScreenParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ScreenParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ScreenParameterBuilder
	
	#region ScreenSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;ScreenColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Screen"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class ScreenSortBuilder : SqlSortBuilder<ScreenColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ScreenSqlSortBuilder class.
		/// </summary>
		public ScreenSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion ScreenSortBuilder
	
} // end namespace
