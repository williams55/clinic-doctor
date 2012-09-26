#region Using directives

using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using AppointmentSystem.Entities;
using AppointmentSystem.Data;

#endregion

namespace AppointmentSystem.Data.Bases
{	
	///<summary>
	/// This class is the base class for any <see cref="VcsMemberTypeProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract class VcsMemberTypeProviderBaseCore : EntityViewProviderBase<VcsMemberType>
	{
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions
		
		/*
		///<summary>
		/// Fill an VList&lt;VcsMemberType&gt; From a DataSet
		///</summary>
		/// <param name="dataSet">the DataSet</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Start row</param>
		/// <param name="pagelen">number of row.</param>
		///<returns><see chref="VList&lt;VcsMemberType&gt;"/></returns>
		protected static VList&lt;VcsMemberType&gt; Fill(DataSet dataSet, VList<VcsMemberType> rows, int start, int pagelen)
		{
			if (dataSet.Tables.Count == 1)
			{
				return Fill(dataSet.Tables[0], rows, start, pagelen);
			}
			else
			{
				return new VList<VcsMemberType>();
			}	
		}
		
		
		///<summary>
		/// Fill an VList&lt;VcsMemberType&gt; From a DataTable
		///</summary>
		/// <param name="dataTable">the DataTable that hold the data.</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Start row</param>
		/// <param name="pagelen">number of row.</param>
		///<returns><see chref="VList<VcsMemberType>"/></returns>
		protected static VList&lt;VcsMemberType&gt; Fill(DataTable dataTable, VList<VcsMemberType> rows, int start, int pagelen)
		{
			int recordnum = 0;
			
			System.Collections.IEnumerator dataRows =  dataTable.Rows.GetEnumerator();
			
			while (dataRows.MoveNext() && (pagelen != 0))
			{
				if(recordnum >= start)
				{
					DataRow row = (DataRow)dataRows.Current;
				
					VcsMemberType c = new VcsMemberType();
					c.MemberType = (Convert.IsDBNull(row["MemberType"]))?string.Empty:(System.String)row["MemberType"];
					c.Description = (Convert.IsDBNull(row["Description"]))?string.Empty:(System.String)row["Description"];
					c.IsActive = (Convert.IsDBNull(row["IsActive"]))?false:(System.Boolean)row["IsActive"];
					c.Remark = (Convert.IsDBNull(row["Remark"]))?string.Empty:(System.String)row["Remark"];
					c.AcceptChanges();
					rows.Add(c);
					pagelen -= 1;
				}
				recordnum += 1;
			}
			return rows;
		}
		*/	
						
		///<summary>
		/// Fill an <see cref="VList&lt;VcsMemberType&gt;"/> From a DataReader.
		///</summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Start row</param>
		/// <param name="pageLength">number of row.</param>
		///<returns>a <see cref="VList&lt;VcsMemberType&gt;"/></returns>
		protected VList<VcsMemberType> Fill(IDataReader reader, VList<VcsMemberType> rows, int start, int pageLength)
		{
			int recordnum = 0;
			while (reader.Read() && (pageLength != 0))
			{
				if(recordnum >= start)
				{
					VcsMemberType entity = null;
					if (DataRepository.Provider.UseEntityFactory)
					{
						entity = EntityManager.CreateViewEntity<VcsMemberType>("VcsMemberType",  DataRepository.Provider.EntityCreationalFactoryType); 
					}
					else
					{
						entity = new VcsMemberType();
					}
					
					entity.SuppressEntityEvents = true;

					entity.MemberType = (System.String)reader[((int)VcsMemberTypeColumn.MemberType)];
					//entity.MemberType = (Convert.IsDBNull(reader["MemberType"]))?string.Empty:(System.String)reader["MemberType"];
					entity.Description = (reader.IsDBNull(((int)VcsMemberTypeColumn.Description)))?null:(System.String)reader[((int)VcsMemberTypeColumn.Description)];
					//entity.Description = (Convert.IsDBNull(reader["Description"]))?string.Empty:(System.String)reader["Description"];
					entity.IsActive = (System.Boolean)reader[((int)VcsMemberTypeColumn.IsActive)];
					//entity.IsActive = (Convert.IsDBNull(reader["IsActive"]))?false:(System.Boolean)reader["IsActive"];
					entity.Remark = (reader.IsDBNull(((int)VcsMemberTypeColumn.Remark)))?null:(System.String)reader[((int)VcsMemberTypeColumn.Remark)];
					//entity.Remark = (Convert.IsDBNull(reader["Remark"]))?string.Empty:(System.String)reader["Remark"];
					entity.AcceptChanges();
					entity.SuppressEntityEvents = false;
					
					rows.Add(entity);
					pageLength -= 1;
				}
				recordnum += 1;
			}
			return rows;
		}
		
		
		/// <summary>
		/// Refreshes the <see cref="VcsMemberType"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="VcsMemberType"/> object to refresh.</param>
		protected void RefreshEntity(IDataReader reader, VcsMemberType entity)
		{
			reader.Read();
			entity.MemberType = (System.String)reader[((int)VcsMemberTypeColumn.MemberType)];
			//entity.MemberType = (Convert.IsDBNull(reader["MemberType"]))?string.Empty:(System.String)reader["MemberType"];
			entity.Description = (reader.IsDBNull(((int)VcsMemberTypeColumn.Description)))?null:(System.String)reader[((int)VcsMemberTypeColumn.Description)];
			//entity.Description = (Convert.IsDBNull(reader["Description"]))?string.Empty:(System.String)reader["Description"];
			entity.IsActive = (System.Boolean)reader[((int)VcsMemberTypeColumn.IsActive)];
			//entity.IsActive = (Convert.IsDBNull(reader["IsActive"]))?false:(System.Boolean)reader["IsActive"];
			entity.Remark = (reader.IsDBNull(((int)VcsMemberTypeColumn.Remark)))?null:(System.String)reader[((int)VcsMemberTypeColumn.Remark)];
			//entity.Remark = (Convert.IsDBNull(reader["Remark"]))?string.Empty:(System.String)reader["Remark"];
			reader.Close();
	
			entity.AcceptChanges();
		}
		
		/*
		/// <summary>
		/// Refreshes the <see cref="VcsMemberType"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="VcsMemberType"/> object.</param>
		protected static void RefreshEntity(DataSet dataSet, VcsMemberType entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.MemberType = (Convert.IsDBNull(dataRow["MemberType"]))?string.Empty:(System.String)dataRow["MemberType"];
			entity.Description = (Convert.IsDBNull(dataRow["Description"]))?string.Empty:(System.String)dataRow["Description"];
			entity.IsActive = (Convert.IsDBNull(dataRow["IsActive"]))?false:(System.Boolean)dataRow["IsActive"];
			entity.Remark = (Convert.IsDBNull(dataRow["Remark"]))?string.Empty:(System.String)dataRow["Remark"];
			entity.AcceptChanges();
		}
		*/
			
		#endregion Helper Functions
	}//end class

	#region VcsMemberTypeFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="VcsMemberType"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VcsMemberTypeFilterBuilder : SqlFilterBuilder<VcsMemberTypeColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VcsMemberTypeFilterBuilder class.
		/// </summary>
		public VcsMemberTypeFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the VcsMemberTypeFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public VcsMemberTypeFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the VcsMemberTypeFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public VcsMemberTypeFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion VcsMemberTypeFilterBuilder

	#region VcsMemberTypeParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="VcsMemberType"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VcsMemberTypeParameterBuilder : ParameterizedSqlFilterBuilder<VcsMemberTypeColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VcsMemberTypeParameterBuilder class.
		/// </summary>
		public VcsMemberTypeParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the VcsMemberTypeParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public VcsMemberTypeParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the VcsMemberTypeParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public VcsMemberTypeParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion VcsMemberTypeParameterBuilder
	
	#region VcsMemberTypeSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="VcsMemberType"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class VcsMemberTypeSortBuilder : SqlSortBuilder<VcsMemberTypeColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VcsMemberTypeSqlSortBuilder class.
		/// </summary>
		public VcsMemberTypeSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion VcsMemberTypeSortBuilder

} // end namespace
