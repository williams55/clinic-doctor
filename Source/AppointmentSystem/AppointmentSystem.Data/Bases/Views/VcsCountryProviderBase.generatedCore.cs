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
	/// This class is the base class for any <see cref="VcsCountryProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract class VcsCountryProviderBaseCore : EntityViewProviderBase<VcsCountry>
	{
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions
		
		/*
		///<summary>
		/// Fill an VList&lt;VcsCountry&gt; From a DataSet
		///</summary>
		/// <param name="dataSet">the DataSet</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Start row</param>
		/// <param name="pagelen">number of row.</param>
		///<returns><see chref="VList&lt;VcsCountry&gt;"/></returns>
		protected static VList&lt;VcsCountry&gt; Fill(DataSet dataSet, VList<VcsCountry> rows, int start, int pagelen)
		{
			if (dataSet.Tables.Count == 1)
			{
				return Fill(dataSet.Tables[0], rows, start, pagelen);
			}
			else
			{
				return new VList<VcsCountry>();
			}	
		}
		
		
		///<summary>
		/// Fill an VList&lt;VcsCountry&gt; From a DataTable
		///</summary>
		/// <param name="dataTable">the DataTable that hold the data.</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Start row</param>
		/// <param name="pagelen">number of row.</param>
		///<returns><see chref="VList<VcsCountry>"/></returns>
		protected static VList&lt;VcsCountry&gt; Fill(DataTable dataTable, VList<VcsCountry> rows, int start, int pagelen)
		{
			int recordnum = 0;
			
			System.Collections.IEnumerator dataRows =  dataTable.Rows.GetEnumerator();
			
			while (dataRows.MoveNext() && (pagelen != 0))
			{
				if(recordnum >= start)
				{
					DataRow row = (DataRow)dataRows.Current;
				
					VcsCountry c = new VcsCountry();
					c.CountryName = (Convert.IsDBNull(row["CountryName"]))?string.Empty:(System.String)row["CountryName"];
					c.CitizenName = (Convert.IsDBNull(row["CitizenName"]))?string.Empty:(System.String)row["CitizenName"];
					c.NationalCode = (Convert.IsDBNull(row["NationalCode"]))?string.Empty:(System.String)row["NationalCode"];
					c.CreateUser = (Convert.IsDBNull(row["CreateUser"]))?string.Empty:(System.String)row["CreateUser"];
					c.CreateDate = (Convert.IsDBNull(row["CreateDate"]))?DateTime.MinValue:(System.DateTime)row["CreateDate"];
					c.IsDisabled = (Convert.IsDBNull(row["IsDisabled"]))?false:(System.Boolean)row["IsDisabled"];
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
		/// Fill an <see cref="VList&lt;VcsCountry&gt;"/> From a DataReader.
		///</summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Start row</param>
		/// <param name="pageLength">number of row.</param>
		///<returns>a <see cref="VList&lt;VcsCountry&gt;"/></returns>
		protected VList<VcsCountry> Fill(IDataReader reader, VList<VcsCountry> rows, int start, int pageLength)
		{
			int recordnum = 0;
			while (reader.Read() && (pageLength != 0))
			{
				if(recordnum >= start)
				{
					VcsCountry entity = null;
					if (DataRepository.Provider.UseEntityFactory)
					{
						entity = EntityManager.CreateViewEntity<VcsCountry>("VcsCountry",  DataRepository.Provider.EntityCreationalFactoryType); 
					}
					else
					{
						entity = new VcsCountry();
					}
					
					entity.SuppressEntityEvents = true;

					entity.CountryName = (System.String)reader[((int)VcsCountryColumn.CountryName)];
					//entity.CountryName = (Convert.IsDBNull(reader["CountryName"]))?string.Empty:(System.String)reader["CountryName"];
					entity.CitizenName = (reader.IsDBNull(((int)VcsCountryColumn.CitizenName)))?null:(System.String)reader[((int)VcsCountryColumn.CitizenName)];
					//entity.CitizenName = (Convert.IsDBNull(reader["CitizenName"]))?string.Empty:(System.String)reader["CitizenName"];
					entity.NationalCode = (reader.IsDBNull(((int)VcsCountryColumn.NationalCode)))?null:(System.String)reader[((int)VcsCountryColumn.NationalCode)];
					//entity.NationalCode = (Convert.IsDBNull(reader["NationalCode"]))?string.Empty:(System.String)reader["NationalCode"];
					entity.CreateUser = (reader.IsDBNull(((int)VcsCountryColumn.CreateUser)))?null:(System.String)reader[((int)VcsCountryColumn.CreateUser)];
					//entity.CreateUser = (Convert.IsDBNull(reader["CreateUser"]))?string.Empty:(System.String)reader["CreateUser"];
					entity.CreateDate = (System.DateTime)reader[((int)VcsCountryColumn.CreateDate)];
					//entity.CreateDate = (Convert.IsDBNull(reader["CreateDate"]))?DateTime.MinValue:(System.DateTime)reader["CreateDate"];
					entity.IsDisabled = (System.Boolean)reader[((int)VcsCountryColumn.IsDisabled)];
					//entity.IsDisabled = (Convert.IsDBNull(reader["IsDisabled"]))?false:(System.Boolean)reader["IsDisabled"];
					entity.Remark = (reader.IsDBNull(((int)VcsCountryColumn.Remark)))?null:(System.String)reader[((int)VcsCountryColumn.Remark)];
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
		/// Refreshes the <see cref="VcsCountry"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="VcsCountry"/> object to refresh.</param>
		protected void RefreshEntity(IDataReader reader, VcsCountry entity)
		{
			reader.Read();
			entity.CountryName = (System.String)reader[((int)VcsCountryColumn.CountryName)];
			//entity.CountryName = (Convert.IsDBNull(reader["CountryName"]))?string.Empty:(System.String)reader["CountryName"];
			entity.CitizenName = (reader.IsDBNull(((int)VcsCountryColumn.CitizenName)))?null:(System.String)reader[((int)VcsCountryColumn.CitizenName)];
			//entity.CitizenName = (Convert.IsDBNull(reader["CitizenName"]))?string.Empty:(System.String)reader["CitizenName"];
			entity.NationalCode = (reader.IsDBNull(((int)VcsCountryColumn.NationalCode)))?null:(System.String)reader[((int)VcsCountryColumn.NationalCode)];
			//entity.NationalCode = (Convert.IsDBNull(reader["NationalCode"]))?string.Empty:(System.String)reader["NationalCode"];
			entity.CreateUser = (reader.IsDBNull(((int)VcsCountryColumn.CreateUser)))?null:(System.String)reader[((int)VcsCountryColumn.CreateUser)];
			//entity.CreateUser = (Convert.IsDBNull(reader["CreateUser"]))?string.Empty:(System.String)reader["CreateUser"];
			entity.CreateDate = (System.DateTime)reader[((int)VcsCountryColumn.CreateDate)];
			//entity.CreateDate = (Convert.IsDBNull(reader["CreateDate"]))?DateTime.MinValue:(System.DateTime)reader["CreateDate"];
			entity.IsDisabled = (System.Boolean)reader[((int)VcsCountryColumn.IsDisabled)];
			//entity.IsDisabled = (Convert.IsDBNull(reader["IsDisabled"]))?false:(System.Boolean)reader["IsDisabled"];
			entity.Remark = (reader.IsDBNull(((int)VcsCountryColumn.Remark)))?null:(System.String)reader[((int)VcsCountryColumn.Remark)];
			//entity.Remark = (Convert.IsDBNull(reader["Remark"]))?string.Empty:(System.String)reader["Remark"];
			reader.Close();
	
			entity.AcceptChanges();
		}
		
		/*
		/// <summary>
		/// Refreshes the <see cref="VcsCountry"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="VcsCountry"/> object.</param>
		protected static void RefreshEntity(DataSet dataSet, VcsCountry entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.CountryName = (Convert.IsDBNull(dataRow["CountryName"]))?string.Empty:(System.String)dataRow["CountryName"];
			entity.CitizenName = (Convert.IsDBNull(dataRow["CitizenName"]))?string.Empty:(System.String)dataRow["CitizenName"];
			entity.NationalCode = (Convert.IsDBNull(dataRow["NationalCode"]))?string.Empty:(System.String)dataRow["NationalCode"];
			entity.CreateUser = (Convert.IsDBNull(dataRow["CreateUser"]))?string.Empty:(System.String)dataRow["CreateUser"];
			entity.CreateDate = (Convert.IsDBNull(dataRow["CreateDate"]))?DateTime.MinValue:(System.DateTime)dataRow["CreateDate"];
			entity.IsDisabled = (Convert.IsDBNull(dataRow["IsDisabled"]))?false:(System.Boolean)dataRow["IsDisabled"];
			entity.Remark = (Convert.IsDBNull(dataRow["Remark"]))?string.Empty:(System.String)dataRow["Remark"];
			entity.AcceptChanges();
		}
		*/
			
		#endregion Helper Functions
	}//end class

	#region VcsCountryFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="VcsCountry"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VcsCountryFilterBuilder : SqlFilterBuilder<VcsCountryColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VcsCountryFilterBuilder class.
		/// </summary>
		public VcsCountryFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the VcsCountryFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public VcsCountryFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the VcsCountryFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public VcsCountryFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion VcsCountryFilterBuilder

	#region VcsCountryParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="VcsCountry"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VcsCountryParameterBuilder : ParameterizedSqlFilterBuilder<VcsCountryColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VcsCountryParameterBuilder class.
		/// </summary>
		public VcsCountryParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the VcsCountryParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public VcsCountryParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the VcsCountryParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public VcsCountryParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion VcsCountryParameterBuilder
	
	#region VcsCountrySortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="VcsCountry"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class VcsCountrySortBuilder : SqlSortBuilder<VcsCountryColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VcsCountrySqlSortBuilder class.
		/// </summary>
		public VcsCountrySortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion VcsCountrySortBuilder

} // end namespace
