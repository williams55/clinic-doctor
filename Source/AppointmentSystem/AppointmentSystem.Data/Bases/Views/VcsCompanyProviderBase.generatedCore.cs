﻿#region Using directives

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
	/// This class is the base class for any <see cref="VcsCompanyProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract class VcsCompanyProviderBaseCore : EntityViewProviderBase<VcsCompany>
	{
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions
		
		/*
		///<summary>
		/// Fill an VList&lt;VcsCompany&gt; From a DataSet
		///</summary>
		/// <param name="dataSet">the DataSet</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Start row</param>
		/// <param name="pagelen">number of row.</param>
		///<returns><see chref="VList&lt;VcsCompany&gt;"/></returns>
		protected static VList&lt;VcsCompany&gt; Fill(DataSet dataSet, VList<VcsCompany> rows, int start, int pagelen)
		{
			if (dataSet.Tables.Count == 1)
			{
				return Fill(dataSet.Tables[0], rows, start, pagelen);
			}
			else
			{
				return new VList<VcsCompany>();
			}	
		}
		
		
		///<summary>
		/// Fill an VList&lt;VcsCompany&gt; From a DataTable
		///</summary>
		/// <param name="dataTable">the DataTable that hold the data.</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Start row</param>
		/// <param name="pagelen">number of row.</param>
		///<returns><see chref="VList<VcsCompany>"/></returns>
		protected static VList&lt;VcsCompany&gt; Fill(DataTable dataTable, VList<VcsCompany> rows, int start, int pagelen)
		{
			int recordnum = 0;
			
			System.Collections.IEnumerator dataRows =  dataTable.Rows.GetEnumerator();
			
			while (dataRows.MoveNext() && (pagelen != 0))
			{
				if(recordnum >= start)
				{
					DataRow row = (DataRow)dataRows.Current;
				
					VcsCompany c = new VcsCompany();
					c.CompanyCode = (Convert.IsDBNull(row["CompanyCode"]))?string.Empty:(System.String)row["CompanyCode"];
					c.CompanyName = (Convert.IsDBNull(row["CompanyName"]))?string.Empty:(System.String)row["CompanyName"];
					c.Address = (Convert.IsDBNull(row["Address"]))?string.Empty:(System.String)row["Address"];
					c.BillingAddress = (Convert.IsDBNull(row["BillingAddress"]))?string.Empty:(System.String)row["BillingAddress"];
					c.CompanyTel = (Convert.IsDBNull(row["CompanyTel"]))?string.Empty:(System.String)row["CompanyTel"];
					c.CompanyTel2 = (Convert.IsDBNull(row["CompanyTel2"]))?string.Empty:(System.String)row["CompanyTel2"];
					c.TaxNumber = (Convert.IsDBNull(row["TAXNumber"]))?string.Empty:(System.String)row["TAXNumber"];
					c.AccountCode = (Convert.IsDBNull(row["AccountCode"]))?string.Empty:(System.String)row["AccountCode"];
					c.Attn = (Convert.IsDBNull(row["Attn"]))?string.Empty:(System.String)row["Attn"];
					c.AttnEmail = (Convert.IsDBNull(row["AttnEmail"]))?string.Empty:(System.String)row["AttnEmail"];
					c.AttnPhone = (Convert.IsDBNull(row["AttnPhone"]))?string.Empty:(System.String)row["AttnPhone"];
					c.PaymentMode = (Convert.IsDBNull(row["PaymentMode"]))?string.Empty:(System.String)row["PaymentMode"];
					c.CreateUser = (Convert.IsDBNull(row["CreateUser"]))?string.Empty:(System.String)row["CreateUser"];
					c.CreateDate = (Convert.IsDBNull(row["CreateDate"]))?DateTime.MinValue:(System.DateTime)row["CreateDate"];
					c.TypeOfServices = (Convert.IsDBNull(row["TypeOfServices"]))?string.Empty:(System.String)row["TypeOfServices"];
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
		/// Fill an <see cref="VList&lt;VcsCompany&gt;"/> From a DataReader.
		///</summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Start row</param>
		/// <param name="pageLength">number of row.</param>
		///<returns>a <see cref="VList&lt;VcsCompany&gt;"/></returns>
		protected VList<VcsCompany> Fill(IDataReader reader, VList<VcsCompany> rows, int start, int pageLength)
		{
			int recordnum = 0;
			while (reader.Read() && (pageLength != 0))
			{
				if(recordnum >= start)
				{
					VcsCompany entity = null;
					if (DataRepository.Provider.UseEntityFactory)
					{
						entity = EntityManager.CreateViewEntity<VcsCompany>("VcsCompany",  DataRepository.Provider.EntityCreationalFactoryType); 
					}
					else
					{
						entity = new VcsCompany();
					}
					
					entity.SuppressEntityEvents = true;

					entity.CompanyCode = (System.String)reader[((int)VcsCompanyColumn.CompanyCode)];
					//entity.CompanyCode = (Convert.IsDBNull(reader["CompanyCode"]))?string.Empty:(System.String)reader["CompanyCode"];
					entity.CompanyName = (System.String)reader[((int)VcsCompanyColumn.CompanyName)];
					//entity.CompanyName = (Convert.IsDBNull(reader["CompanyName"]))?string.Empty:(System.String)reader["CompanyName"];
					entity.Address = (reader.IsDBNull(((int)VcsCompanyColumn.Address)))?null:(System.String)reader[((int)VcsCompanyColumn.Address)];
					//entity.Address = (Convert.IsDBNull(reader["Address"]))?string.Empty:(System.String)reader["Address"];
					entity.BillingAddress = (reader.IsDBNull(((int)VcsCompanyColumn.BillingAddress)))?null:(System.String)reader[((int)VcsCompanyColumn.BillingAddress)];
					//entity.BillingAddress = (Convert.IsDBNull(reader["BillingAddress"]))?string.Empty:(System.String)reader["BillingAddress"];
					entity.CompanyTel = (reader.IsDBNull(((int)VcsCompanyColumn.CompanyTel)))?null:(System.String)reader[((int)VcsCompanyColumn.CompanyTel)];
					//entity.CompanyTel = (Convert.IsDBNull(reader["CompanyTel"]))?string.Empty:(System.String)reader["CompanyTel"];
					entity.CompanyTel2 = (reader.IsDBNull(((int)VcsCompanyColumn.CompanyTel2)))?null:(System.String)reader[((int)VcsCompanyColumn.CompanyTel2)];
					//entity.CompanyTel2 = (Convert.IsDBNull(reader["CompanyTel2"]))?string.Empty:(System.String)reader["CompanyTel2"];
					entity.TaxNumber = (reader.IsDBNull(((int)VcsCompanyColumn.TaxNumber)))?null:(System.String)reader[((int)VcsCompanyColumn.TaxNumber)];
					//entity.TaxNumber = (Convert.IsDBNull(reader["TAXNumber"]))?string.Empty:(System.String)reader["TAXNumber"];
					entity.AccountCode = (reader.IsDBNull(((int)VcsCompanyColumn.AccountCode)))?null:(System.String)reader[((int)VcsCompanyColumn.AccountCode)];
					//entity.AccountCode = (Convert.IsDBNull(reader["AccountCode"]))?string.Empty:(System.String)reader["AccountCode"];
					entity.Attn = (reader.IsDBNull(((int)VcsCompanyColumn.Attn)))?null:(System.String)reader[((int)VcsCompanyColumn.Attn)];
					//entity.Attn = (Convert.IsDBNull(reader["Attn"]))?string.Empty:(System.String)reader["Attn"];
					entity.AttnEmail = (reader.IsDBNull(((int)VcsCompanyColumn.AttnEmail)))?null:(System.String)reader[((int)VcsCompanyColumn.AttnEmail)];
					//entity.AttnEmail = (Convert.IsDBNull(reader["AttnEmail"]))?string.Empty:(System.String)reader["AttnEmail"];
					entity.AttnPhone = (reader.IsDBNull(((int)VcsCompanyColumn.AttnPhone)))?null:(System.String)reader[((int)VcsCompanyColumn.AttnPhone)];
					//entity.AttnPhone = (Convert.IsDBNull(reader["AttnPhone"]))?string.Empty:(System.String)reader["AttnPhone"];
					entity.PaymentMode = (reader.IsDBNull(((int)VcsCompanyColumn.PaymentMode)))?null:(System.String)reader[((int)VcsCompanyColumn.PaymentMode)];
					//entity.PaymentMode = (Convert.IsDBNull(reader["PaymentMode"]))?string.Empty:(System.String)reader["PaymentMode"];
					entity.CreateUser = (reader.IsDBNull(((int)VcsCompanyColumn.CreateUser)))?null:(System.String)reader[((int)VcsCompanyColumn.CreateUser)];
					//entity.CreateUser = (Convert.IsDBNull(reader["CreateUser"]))?string.Empty:(System.String)reader["CreateUser"];
					entity.CreateDate = (System.DateTime)reader[((int)VcsCompanyColumn.CreateDate)];
					//entity.CreateDate = (Convert.IsDBNull(reader["CreateDate"]))?DateTime.MinValue:(System.DateTime)reader["CreateDate"];
					entity.TypeOfServices = (reader.IsDBNull(((int)VcsCompanyColumn.TypeOfServices)))?null:(System.String)reader[((int)VcsCompanyColumn.TypeOfServices)];
					//entity.TypeOfServices = (Convert.IsDBNull(reader["TypeOfServices"]))?string.Empty:(System.String)reader["TypeOfServices"];
					entity.IsDisabled = (System.Boolean)reader[((int)VcsCompanyColumn.IsDisabled)];
					//entity.IsDisabled = (Convert.IsDBNull(reader["IsDisabled"]))?false:(System.Boolean)reader["IsDisabled"];
					entity.Remark = (reader.IsDBNull(((int)VcsCompanyColumn.Remark)))?null:(System.String)reader[((int)VcsCompanyColumn.Remark)];
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
		/// Refreshes the <see cref="VcsCompany"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="VcsCompany"/> object to refresh.</param>
		protected void RefreshEntity(IDataReader reader, VcsCompany entity)
		{
			reader.Read();
			entity.CompanyCode = (System.String)reader[((int)VcsCompanyColumn.CompanyCode)];
			//entity.CompanyCode = (Convert.IsDBNull(reader["CompanyCode"]))?string.Empty:(System.String)reader["CompanyCode"];
			entity.CompanyName = (System.String)reader[((int)VcsCompanyColumn.CompanyName)];
			//entity.CompanyName = (Convert.IsDBNull(reader["CompanyName"]))?string.Empty:(System.String)reader["CompanyName"];
			entity.Address = (reader.IsDBNull(((int)VcsCompanyColumn.Address)))?null:(System.String)reader[((int)VcsCompanyColumn.Address)];
			//entity.Address = (Convert.IsDBNull(reader["Address"]))?string.Empty:(System.String)reader["Address"];
			entity.BillingAddress = (reader.IsDBNull(((int)VcsCompanyColumn.BillingAddress)))?null:(System.String)reader[((int)VcsCompanyColumn.BillingAddress)];
			//entity.BillingAddress = (Convert.IsDBNull(reader["BillingAddress"]))?string.Empty:(System.String)reader["BillingAddress"];
			entity.CompanyTel = (reader.IsDBNull(((int)VcsCompanyColumn.CompanyTel)))?null:(System.String)reader[((int)VcsCompanyColumn.CompanyTel)];
			//entity.CompanyTel = (Convert.IsDBNull(reader["CompanyTel"]))?string.Empty:(System.String)reader["CompanyTel"];
			entity.CompanyTel2 = (reader.IsDBNull(((int)VcsCompanyColumn.CompanyTel2)))?null:(System.String)reader[((int)VcsCompanyColumn.CompanyTel2)];
			//entity.CompanyTel2 = (Convert.IsDBNull(reader["CompanyTel2"]))?string.Empty:(System.String)reader["CompanyTel2"];
			entity.TaxNumber = (reader.IsDBNull(((int)VcsCompanyColumn.TaxNumber)))?null:(System.String)reader[((int)VcsCompanyColumn.TaxNumber)];
			//entity.TaxNumber = (Convert.IsDBNull(reader["TAXNumber"]))?string.Empty:(System.String)reader["TAXNumber"];
			entity.AccountCode = (reader.IsDBNull(((int)VcsCompanyColumn.AccountCode)))?null:(System.String)reader[((int)VcsCompanyColumn.AccountCode)];
			//entity.AccountCode = (Convert.IsDBNull(reader["AccountCode"]))?string.Empty:(System.String)reader["AccountCode"];
			entity.Attn = (reader.IsDBNull(((int)VcsCompanyColumn.Attn)))?null:(System.String)reader[((int)VcsCompanyColumn.Attn)];
			//entity.Attn = (Convert.IsDBNull(reader["Attn"]))?string.Empty:(System.String)reader["Attn"];
			entity.AttnEmail = (reader.IsDBNull(((int)VcsCompanyColumn.AttnEmail)))?null:(System.String)reader[((int)VcsCompanyColumn.AttnEmail)];
			//entity.AttnEmail = (Convert.IsDBNull(reader["AttnEmail"]))?string.Empty:(System.String)reader["AttnEmail"];
			entity.AttnPhone = (reader.IsDBNull(((int)VcsCompanyColumn.AttnPhone)))?null:(System.String)reader[((int)VcsCompanyColumn.AttnPhone)];
			//entity.AttnPhone = (Convert.IsDBNull(reader["AttnPhone"]))?string.Empty:(System.String)reader["AttnPhone"];
			entity.PaymentMode = (reader.IsDBNull(((int)VcsCompanyColumn.PaymentMode)))?null:(System.String)reader[((int)VcsCompanyColumn.PaymentMode)];
			//entity.PaymentMode = (Convert.IsDBNull(reader["PaymentMode"]))?string.Empty:(System.String)reader["PaymentMode"];
			entity.CreateUser = (reader.IsDBNull(((int)VcsCompanyColumn.CreateUser)))?null:(System.String)reader[((int)VcsCompanyColumn.CreateUser)];
			//entity.CreateUser = (Convert.IsDBNull(reader["CreateUser"]))?string.Empty:(System.String)reader["CreateUser"];
			entity.CreateDate = (System.DateTime)reader[((int)VcsCompanyColumn.CreateDate)];
			//entity.CreateDate = (Convert.IsDBNull(reader["CreateDate"]))?DateTime.MinValue:(System.DateTime)reader["CreateDate"];
			entity.TypeOfServices = (reader.IsDBNull(((int)VcsCompanyColumn.TypeOfServices)))?null:(System.String)reader[((int)VcsCompanyColumn.TypeOfServices)];
			//entity.TypeOfServices = (Convert.IsDBNull(reader["TypeOfServices"]))?string.Empty:(System.String)reader["TypeOfServices"];
			entity.IsDisabled = (System.Boolean)reader[((int)VcsCompanyColumn.IsDisabled)];
			//entity.IsDisabled = (Convert.IsDBNull(reader["IsDisabled"]))?false:(System.Boolean)reader["IsDisabled"];
			entity.Remark = (reader.IsDBNull(((int)VcsCompanyColumn.Remark)))?null:(System.String)reader[((int)VcsCompanyColumn.Remark)];
			//entity.Remark = (Convert.IsDBNull(reader["Remark"]))?string.Empty:(System.String)reader["Remark"];
			reader.Close();
	
			entity.AcceptChanges();
		}
		
		/*
		/// <summary>
		/// Refreshes the <see cref="VcsCompany"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="VcsCompany"/> object.</param>
		protected static void RefreshEntity(DataSet dataSet, VcsCompany entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.CompanyCode = (Convert.IsDBNull(dataRow["CompanyCode"]))?string.Empty:(System.String)dataRow["CompanyCode"];
			entity.CompanyName = (Convert.IsDBNull(dataRow["CompanyName"]))?string.Empty:(System.String)dataRow["CompanyName"];
			entity.Address = (Convert.IsDBNull(dataRow["Address"]))?string.Empty:(System.String)dataRow["Address"];
			entity.BillingAddress = (Convert.IsDBNull(dataRow["BillingAddress"]))?string.Empty:(System.String)dataRow["BillingAddress"];
			entity.CompanyTel = (Convert.IsDBNull(dataRow["CompanyTel"]))?string.Empty:(System.String)dataRow["CompanyTel"];
			entity.CompanyTel2 = (Convert.IsDBNull(dataRow["CompanyTel2"]))?string.Empty:(System.String)dataRow["CompanyTel2"];
			entity.TaxNumber = (Convert.IsDBNull(dataRow["TAXNumber"]))?string.Empty:(System.String)dataRow["TAXNumber"];
			entity.AccountCode = (Convert.IsDBNull(dataRow["AccountCode"]))?string.Empty:(System.String)dataRow["AccountCode"];
			entity.Attn = (Convert.IsDBNull(dataRow["Attn"]))?string.Empty:(System.String)dataRow["Attn"];
			entity.AttnEmail = (Convert.IsDBNull(dataRow["AttnEmail"]))?string.Empty:(System.String)dataRow["AttnEmail"];
			entity.AttnPhone = (Convert.IsDBNull(dataRow["AttnPhone"]))?string.Empty:(System.String)dataRow["AttnPhone"];
			entity.PaymentMode = (Convert.IsDBNull(dataRow["PaymentMode"]))?string.Empty:(System.String)dataRow["PaymentMode"];
			entity.CreateUser = (Convert.IsDBNull(dataRow["CreateUser"]))?string.Empty:(System.String)dataRow["CreateUser"];
			entity.CreateDate = (Convert.IsDBNull(dataRow["CreateDate"]))?DateTime.MinValue:(System.DateTime)dataRow["CreateDate"];
			entity.TypeOfServices = (Convert.IsDBNull(dataRow["TypeOfServices"]))?string.Empty:(System.String)dataRow["TypeOfServices"];
			entity.IsDisabled = (Convert.IsDBNull(dataRow["IsDisabled"]))?false:(System.Boolean)dataRow["IsDisabled"];
			entity.Remark = (Convert.IsDBNull(dataRow["Remark"]))?string.Empty:(System.String)dataRow["Remark"];
			entity.AcceptChanges();
		}
		*/
			
		#endregion Helper Functions
	}//end class

	#region VcsCompanyFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="VcsCompany"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VcsCompanyFilterBuilder : SqlFilterBuilder<VcsCompanyColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VcsCompanyFilterBuilder class.
		/// </summary>
		public VcsCompanyFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the VcsCompanyFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public VcsCompanyFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the VcsCompanyFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public VcsCompanyFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion VcsCompanyFilterBuilder

	#region VcsCompanyParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="VcsCompany"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VcsCompanyParameterBuilder : ParameterizedSqlFilterBuilder<VcsCompanyColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VcsCompanyParameterBuilder class.
		/// </summary>
		public VcsCompanyParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the VcsCompanyParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public VcsCompanyParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the VcsCompanyParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public VcsCompanyParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion VcsCompanyParameterBuilder
	
	#region VcsCompanySortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="VcsCompany"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class VcsCompanySortBuilder : SqlSortBuilder<VcsCompanyColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VcsCompanySqlSortBuilder class.
		/// </summary>
		public VcsCompanySortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion VcsCompanySortBuilder

} // end namespace
