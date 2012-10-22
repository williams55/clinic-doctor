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
	/// This class is the base class for any <see cref="VcsPatientProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract class VcsPatientProviderBaseCore : EntityViewProviderBase<VcsPatient>
	{
		#region Custom Methods
		
		
		#region _VCSPatient_GetByPatientCode
		
		/// <summary>
		///	This method wrap the '_VCSPatient_GetByPatientCode' stored procedure. 
		/// </summary>
		/// <param name="patientCode"> A <c>System.String</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="VList&lt;VcsPatient&gt;"/> instance.</returns>
		public VList<VcsPatient> GetByPatientCode(System.String patientCode)
		{
			return GetByPatientCode(null, 0, int.MaxValue , patientCode);
		}
		
		/// <summary>
		///	This method wrap the '_VCSPatient_GetByPatientCode' stored procedure. 
		/// </summary>
		/// <param name="patientCode"> A <c>System.String</c> instance.</param>
		/// <param name="start">Row number at which to start reading.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="VList&lt;VcsPatient&gt;"/> instance.</returns>
		public VList<VcsPatient> GetByPatientCode(int start, int pageLength, System.String patientCode)
		{
			return GetByPatientCode(null, start, pageLength , patientCode);
		}
				
		/// <summary>
		///	This method wrap the '_VCSPatient_GetByPatientCode' stored procedure. 
		/// </summary>
		/// <param name="patientCode"> A <c>System.String</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <returns>A <see cref="VList&lt;VcsPatient&gt;"/> instance.</returns>
		public VList<VcsPatient> GetByPatientCode(TransactionManager transactionManager, System.String patientCode)
		{
			return GetByPatientCode(transactionManager, 0, int.MaxValue , patientCode);
		}
		
		/// <summary>
		///	This method wrap the '_VCSPatient_GetByPatientCode' stored procedure. 
		/// </summary>
		/// <param name="patientCode"> A <c>System.String</c> instance.</param>
		/// <param name="start">Row number at which to start reading.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="VList&lt;VcsPatient&gt;"/> instance.</returns>
		public abstract VList<VcsPatient> GetByPatientCode(TransactionManager transactionManager, int start, int pageLength, System.String patientCode);
		
		#endregion

		
		#region _VCSPatient_Update
		
		/// <summary>
		///	This method wrap the '_VCSPatient_Update' stored procedure. 
		/// </summary>
		/// <param name="patientCode"> A <c>System.String</c> instance.</param>
		/// <param name="firstName"> A <c>System.String</c> instance.</param>
		/// <param name="middleName"> A <c>System.String</c> instance.</param>
		/// <param name="lastName"> A <c>System.String</c> instance.</param>
		/// <param name="dateOfBirth"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="sex"> A <c>System.String</c> instance.</param>
		/// <param name="nationality"> A <c>System.String</c> instance.</param>
		/// <param name="companyCode"> A <c>System.String</c> instance.</param>
		/// <param name="homePhone"> A <c>System.String</c> instance.</param>
		/// <param name="mobilePhone"> A <c>System.String</c> instance.</param>
		/// <param name="updateUser"> A <c>System.String</c> instance.</param>
		/// <param name="apptRemark"> A <c>System.String</c> instance.</param>
		/// <param name="isDisabled"> A <c>System.Boolean?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void Update(System.String patientCode, System.String firstName, System.String middleName, System.String lastName, System.DateTime? dateOfBirth, System.String sex, System.String nationality, System.String companyCode, System.String homePhone, System.String mobilePhone, System.String updateUser, System.String apptRemark, System.Boolean? isDisabled)
		{
			 Update(null, 0, int.MaxValue , patientCode, firstName, middleName, lastName, dateOfBirth, sex, nationality, companyCode, homePhone, mobilePhone, updateUser, apptRemark, isDisabled);
		}
		
		/// <summary>
		///	This method wrap the '_VCSPatient_Update' stored procedure. 
		/// </summary>
		/// <param name="patientCode"> A <c>System.String</c> instance.</param>
		/// <param name="firstName"> A <c>System.String</c> instance.</param>
		/// <param name="middleName"> A <c>System.String</c> instance.</param>
		/// <param name="lastName"> A <c>System.String</c> instance.</param>
		/// <param name="dateOfBirth"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="sex"> A <c>System.String</c> instance.</param>
		/// <param name="nationality"> A <c>System.String</c> instance.</param>
		/// <param name="companyCode"> A <c>System.String</c> instance.</param>
		/// <param name="homePhone"> A <c>System.String</c> instance.</param>
		/// <param name="mobilePhone"> A <c>System.String</c> instance.</param>
		/// <param name="updateUser"> A <c>System.String</c> instance.</param>
		/// <param name="apptRemark"> A <c>System.String</c> instance.</param>
		/// <param name="isDisabled"> A <c>System.Boolean?</c> instance.</param>
		/// <param name="start">Row number at which to start reading.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void Update(int start, int pageLength, System.String patientCode, System.String firstName, System.String middleName, System.String lastName, System.DateTime? dateOfBirth, System.String sex, System.String nationality, System.String companyCode, System.String homePhone, System.String mobilePhone, System.String updateUser, System.String apptRemark, System.Boolean? isDisabled)
		{
			 Update(null, start, pageLength , patientCode, firstName, middleName, lastName, dateOfBirth, sex, nationality, companyCode, homePhone, mobilePhone, updateUser, apptRemark, isDisabled);
		}
				
		/// <summary>
		///	This method wrap the '_VCSPatient_Update' stored procedure. 
		/// </summary>
		/// <param name="patientCode"> A <c>System.String</c> instance.</param>
		/// <param name="firstName"> A <c>System.String</c> instance.</param>
		/// <param name="middleName"> A <c>System.String</c> instance.</param>
		/// <param name="lastName"> A <c>System.String</c> instance.</param>
		/// <param name="dateOfBirth"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="sex"> A <c>System.String</c> instance.</param>
		/// <param name="nationality"> A <c>System.String</c> instance.</param>
		/// <param name="companyCode"> A <c>System.String</c> instance.</param>
		/// <param name="homePhone"> A <c>System.String</c> instance.</param>
		/// <param name="mobilePhone"> A <c>System.String</c> instance.</param>
		/// <param name="updateUser"> A <c>System.String</c> instance.</param>
		/// <param name="apptRemark"> A <c>System.String</c> instance.</param>
		/// <param name="isDisabled"> A <c>System.Boolean?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void Update(TransactionManager transactionManager, System.String patientCode, System.String firstName, System.String middleName, System.String lastName, System.DateTime? dateOfBirth, System.String sex, System.String nationality, System.String companyCode, System.String homePhone, System.String mobilePhone, System.String updateUser, System.String apptRemark, System.Boolean? isDisabled)
		{
			 Update(transactionManager, 0, int.MaxValue , patientCode, firstName, middleName, lastName, dateOfBirth, sex, nationality, companyCode, homePhone, mobilePhone, updateUser, apptRemark, isDisabled);
		}
		
		/// <summary>
		///	This method wrap the '_VCSPatient_Update' stored procedure. 
		/// </summary>
		/// <param name="patientCode"> A <c>System.String</c> instance.</param>
		/// <param name="firstName"> A <c>System.String</c> instance.</param>
		/// <param name="middleName"> A <c>System.String</c> instance.</param>
		/// <param name="lastName"> A <c>System.String</c> instance.</param>
		/// <param name="dateOfBirth"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="sex"> A <c>System.String</c> instance.</param>
		/// <param name="nationality"> A <c>System.String</c> instance.</param>
		/// <param name="companyCode"> A <c>System.String</c> instance.</param>
		/// <param name="homePhone"> A <c>System.String</c> instance.</param>
		/// <param name="mobilePhone"> A <c>System.String</c> instance.</param>
		/// <param name="updateUser"> A <c>System.String</c> instance.</param>
		/// <param name="apptRemark"> A <c>System.String</c> instance.</param>
		/// <param name="isDisabled"> A <c>System.Boolean?</c> instance.</param>
		/// <param name="start">Row number at which to start reading.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public abstract void Update(TransactionManager transactionManager, int start, int pageLength, System.String patientCode, System.String firstName, System.String middleName, System.String lastName, System.DateTime? dateOfBirth, System.String sex, System.String nationality, System.String companyCode, System.String homePhone, System.String mobilePhone, System.String updateUser, System.String apptRemark, System.Boolean? isDisabled);
		
		#endregion

		
		#region _VCSPatient_Insert
		
		/// <summary>
		///	This method wrap the '_VCSPatient_Insert' stored procedure. 
		/// </summary>
		/// <param name="patientCode"> A <c>System.String</c> instance.</param>
		/// <param name="firstName"> A <c>System.String</c> instance.</param>
		/// <param name="middleName"> A <c>System.String</c> instance.</param>
		/// <param name="lastName"> A <c>System.String</c> instance.</param>
		/// <param name="dateOfBirth"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="sex"> A <c>System.String</c> instance.</param>
		/// <param name="nationality"> A <c>System.String</c> instance.</param>
		/// <param name="companyCode"> A <c>System.String</c> instance.</param>
		/// <param name="homePhone"> A <c>System.String</c> instance.</param>
		/// <param name="mobilePhone"> A <c>System.String</c> instance.</param>
		/// <param name="createUser"> A <c>System.String</c> instance.</param>
		/// <param name="apptRemark"> A <c>System.String</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="VList&lt;VcsPatient&gt;"/> instance.</returns>
		public VList<VcsPatient> Insert(System.String patientCode, System.String firstName, System.String middleName, System.String lastName, System.DateTime? dateOfBirth, System.String sex, System.String nationality, System.String companyCode, System.String homePhone, System.String mobilePhone, System.String createUser, System.String apptRemark)
		{
			return Insert(null, 0, int.MaxValue , patientCode, firstName, middleName, lastName, dateOfBirth, sex, nationality, companyCode, homePhone, mobilePhone, createUser, apptRemark);
		}
		
		/// <summary>
		///	This method wrap the '_VCSPatient_Insert' stored procedure. 
		/// </summary>
		/// <param name="patientCode"> A <c>System.String</c> instance.</param>
		/// <param name="firstName"> A <c>System.String</c> instance.</param>
		/// <param name="middleName"> A <c>System.String</c> instance.</param>
		/// <param name="lastName"> A <c>System.String</c> instance.</param>
		/// <param name="dateOfBirth"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="sex"> A <c>System.String</c> instance.</param>
		/// <param name="nationality"> A <c>System.String</c> instance.</param>
		/// <param name="companyCode"> A <c>System.String</c> instance.</param>
		/// <param name="homePhone"> A <c>System.String</c> instance.</param>
		/// <param name="mobilePhone"> A <c>System.String</c> instance.</param>
		/// <param name="createUser"> A <c>System.String</c> instance.</param>
		/// <param name="apptRemark"> A <c>System.String</c> instance.</param>
		/// <param name="start">Row number at which to start reading.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="VList&lt;VcsPatient&gt;"/> instance.</returns>
		public VList<VcsPatient> Insert(int start, int pageLength, System.String patientCode, System.String firstName, System.String middleName, System.String lastName, System.DateTime? dateOfBirth, System.String sex, System.String nationality, System.String companyCode, System.String homePhone, System.String mobilePhone, System.String createUser, System.String apptRemark)
		{
			return Insert(null, start, pageLength , patientCode, firstName, middleName, lastName, dateOfBirth, sex, nationality, companyCode, homePhone, mobilePhone, createUser, apptRemark);
		}
				
		/// <summary>
		///	This method wrap the '_VCSPatient_Insert' stored procedure. 
		/// </summary>
		/// <param name="patientCode"> A <c>System.String</c> instance.</param>
		/// <param name="firstName"> A <c>System.String</c> instance.</param>
		/// <param name="middleName"> A <c>System.String</c> instance.</param>
		/// <param name="lastName"> A <c>System.String</c> instance.</param>
		/// <param name="dateOfBirth"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="sex"> A <c>System.String</c> instance.</param>
		/// <param name="nationality"> A <c>System.String</c> instance.</param>
		/// <param name="companyCode"> A <c>System.String</c> instance.</param>
		/// <param name="homePhone"> A <c>System.String</c> instance.</param>
		/// <param name="mobilePhone"> A <c>System.String</c> instance.</param>
		/// <param name="createUser"> A <c>System.String</c> instance.</param>
		/// <param name="apptRemark"> A <c>System.String</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <returns>A <see cref="VList&lt;VcsPatient&gt;"/> instance.</returns>
		public VList<VcsPatient> Insert(TransactionManager transactionManager, System.String patientCode, System.String firstName, System.String middleName, System.String lastName, System.DateTime? dateOfBirth, System.String sex, System.String nationality, System.String companyCode, System.String homePhone, System.String mobilePhone, System.String createUser, System.String apptRemark)
		{
			return Insert(transactionManager, 0, int.MaxValue , patientCode, firstName, middleName, lastName, dateOfBirth, sex, nationality, companyCode, homePhone, mobilePhone, createUser, apptRemark);
		}
		
		/// <summary>
		///	This method wrap the '_VCSPatient_Insert' stored procedure. 
		/// </summary>
		/// <param name="patientCode"> A <c>System.String</c> instance.</param>
		/// <param name="firstName"> A <c>System.String</c> instance.</param>
		/// <param name="middleName"> A <c>System.String</c> instance.</param>
		/// <param name="lastName"> A <c>System.String</c> instance.</param>
		/// <param name="dateOfBirth"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="sex"> A <c>System.String</c> instance.</param>
		/// <param name="nationality"> A <c>System.String</c> instance.</param>
		/// <param name="companyCode"> A <c>System.String</c> instance.</param>
		/// <param name="homePhone"> A <c>System.String</c> instance.</param>
		/// <param name="mobilePhone"> A <c>System.String</c> instance.</param>
		/// <param name="createUser"> A <c>System.String</c> instance.</param>
		/// <param name="apptRemark"> A <c>System.String</c> instance.</param>
		/// <param name="start">Row number at which to start reading.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="VList&lt;VcsPatient&gt;"/> instance.</returns>
		public abstract VList<VcsPatient> Insert(TransactionManager transactionManager, int start, int pageLength, System.String patientCode, System.String firstName, System.String middleName, System.String lastName, System.DateTime? dateOfBirth, System.String sex, System.String nationality, System.String companyCode, System.String homePhone, System.String mobilePhone, System.String createUser, System.String apptRemark);
		
		#endregion

		
		#endregion

		#region Helper Functions
		
		/*
		///<summary>
		/// Fill an VList&lt;VcsPatient&gt; From a DataSet
		///</summary>
		/// <param name="dataSet">the DataSet</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Start row</param>
		/// <param name="pagelen">number of row.</param>
		///<returns><see chref="VList&lt;VcsPatient&gt;"/></returns>
		protected static VList&lt;VcsPatient&gt; Fill(DataSet dataSet, VList<VcsPatient> rows, int start, int pagelen)
		{
			if (dataSet.Tables.Count == 1)
			{
				return Fill(dataSet.Tables[0], rows, start, pagelen);
			}
			else
			{
				return new VList<VcsPatient>();
			}	
		}
		
		
		///<summary>
		/// Fill an VList&lt;VcsPatient&gt; From a DataTable
		///</summary>
		/// <param name="dataTable">the DataTable that hold the data.</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Start row</param>
		/// <param name="pagelen">number of row.</param>
		///<returns><see chref="VList<VcsPatient>"/></returns>
		protected static VList&lt;VcsPatient&gt; Fill(DataTable dataTable, VList<VcsPatient> rows, int start, int pagelen)
		{
			int recordnum = 0;
			
			System.Collections.IEnumerator dataRows =  dataTable.Rows.GetEnumerator();
			
			while (dataRows.MoveNext() && (pagelen != 0))
			{
				if(recordnum >= start)
				{
					DataRow row = (DataRow)dataRows.Current;
				
					VcsPatient c = new VcsPatient();
					c.PatientCode = (Convert.IsDBNull(row["PatientCode"]))?string.Empty:(System.String)row["PatientCode"];
					c.FirstName = (Convert.IsDBNull(row["FirstName"]))?string.Empty:(System.String)row["FirstName"];
					c.MiddleName = (Convert.IsDBNull(row["MiddleName"]))?string.Empty:(System.String)row["MiddleName"];
					c.LastName = (Convert.IsDBNull(row["LastName"]))?string.Empty:(System.String)row["LastName"];
					c.DateOfBirth = (Convert.IsDBNull(row["DateOfBirth"]))?DateTime.MinValue:(System.DateTime)row["DateOfBirth"];
					c.Sex = (Convert.IsDBNull(row["Sex"]))?string.Empty:(System.String)row["Sex"];
					c.MemberType = (Convert.IsDBNull(row["MemberType"]))?string.Empty:(System.String)row["MemberType"];
					c.MembershipSosNumber = (Convert.IsDBNull(row["MembershipSOSNumber"]))?string.Empty:(System.String)row["MembershipSOSNumber"];
					c.MembershipSosExpDate = (Convert.IsDBNull(row["MembershipSOSExpDate"]))?DateTime.MinValue:(System.DateTime?)row["MembershipSOSExpDate"];
					c.Nationality = (Convert.IsDBNull(row["Nationality"]))?string.Empty:(System.String)row["Nationality"];
					c.HomeStreet = (Convert.IsDBNull(row["HomeStreet"]))?string.Empty:(System.String)row["HomeStreet"];
					c.HomeWard = (Convert.IsDBNull(row["HomeWard"]))?string.Empty:(System.String)row["HomeWard"];
					c.HomeDistrict = (Convert.IsDBNull(row["HomeDistrict"]))?string.Empty:(System.String)row["HomeDistrict"];
					c.HomeCity = (Convert.IsDBNull(row["HomeCity"]))?string.Empty:(System.String)row["HomeCity"];
					c.HomeCountry = (Convert.IsDBNull(row["HomeCountry"]))?string.Empty:(System.String)row["HomeCountry"];
					c.CompanyCode = (Convert.IsDBNull(row["CompanyCode"]))?string.Empty:(System.String)row["CompanyCode"];
					c.BillingAddress = (Convert.IsDBNull(row["BillingAddress"]))?string.Empty:(System.String)row["BillingAddress"];
					c.HomePhone = (Convert.IsDBNull(row["HomePhone"]))?string.Empty:(System.String)row["HomePhone"];
					c.MobilePhone = (Convert.IsDBNull(row["MobilePhone"]))?string.Empty:(System.String)row["MobilePhone"];
					c.CompanyPhone = (Convert.IsDBNull(row["CompanyPhone"]))?string.Empty:(System.String)row["CompanyPhone"];
					c.Fax = (Convert.IsDBNull(row["Fax"]))?string.Empty:(System.String)row["Fax"];
					c.EmailAddress = (Convert.IsDBNull(row["EmailAddress"]))?string.Empty:(System.String)row["EmailAddress"];
					c.CreateUser = (Convert.IsDBNull(row["CreateUser"]))?string.Empty:(System.String)row["CreateUser"];
					c.CreateDate = (Convert.IsDBNull(row["CreateDate"]))?DateTime.MinValue:(System.DateTime)row["CreateDate"];
					c.ValidCorporate = (Convert.IsDBNull(row["ValidCorporate"]))?false:(System.Boolean)row["ValidCorporate"];
					c.DefaultPaymentMode = (Convert.IsDBNull(row["DefaultPaymentMode"]))?string.Empty:(System.String)row["DefaultPaymentMode"];
					c.InsuranceCardNumber = (Convert.IsDBNull(row["InsuranceCardNumber"]))?string.Empty:(System.String)row["InsuranceCardNumber"];
					c.InsuranceCardExpDate = (Convert.IsDBNull(row["InsuranceCardExpDate"]))?DateTime.MinValue:(System.DateTime?)row["InsuranceCardExpDate"];
					c.IsDisabled = (Convert.IsDBNull(row["IsDisabled"]))?false:(System.Boolean)row["IsDisabled"];
					c.UpdateUser = (Convert.IsDBNull(row["UpdateUser"]))?string.Empty:(System.String)row["UpdateUser"];
					c.UpdateDate = (Convert.IsDBNull(row["UpdateDate"]))?DateTime.MinValue:(System.DateTime)row["UpdateDate"];
					c.ApptRemark = (Convert.IsDBNull(row["ApptRemark"]))?string.Empty:(System.String)row["ApptRemark"];
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
		/// Fill an <see cref="VList&lt;VcsPatient&gt;"/> From a DataReader.
		///</summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Start row</param>
		/// <param name="pageLength">number of row.</param>
		///<returns>a <see cref="VList&lt;VcsPatient&gt;"/></returns>
		protected VList<VcsPatient> Fill(IDataReader reader, VList<VcsPatient> rows, int start, int pageLength)
		{
			int recordnum = 0;
			while (reader.Read() && (pageLength != 0))
			{
				if(recordnum >= start)
				{
					VcsPatient entity = null;
					if (DataRepository.Provider.UseEntityFactory)
					{
						entity = EntityManager.CreateViewEntity<VcsPatient>("VcsPatient",  DataRepository.Provider.EntityCreationalFactoryType); 
					}
					else
					{
						entity = new VcsPatient();
					}
					
					entity.SuppressEntityEvents = true;

					entity.PatientCode = (System.String)reader[((int)VcsPatientColumn.PatientCode)];
					//entity.PatientCode = (Convert.IsDBNull(reader["PatientCode"]))?string.Empty:(System.String)reader["PatientCode"];
					entity.FirstName = (System.String)reader[((int)VcsPatientColumn.FirstName)];
					//entity.FirstName = (Convert.IsDBNull(reader["FirstName"]))?string.Empty:(System.String)reader["FirstName"];
					entity.MiddleName = (reader.IsDBNull(((int)VcsPatientColumn.MiddleName)))?null:(System.String)reader[((int)VcsPatientColumn.MiddleName)];
					//entity.MiddleName = (Convert.IsDBNull(reader["MiddleName"]))?string.Empty:(System.String)reader["MiddleName"];
					entity.LastName = (System.String)reader[((int)VcsPatientColumn.LastName)];
					//entity.LastName = (Convert.IsDBNull(reader["LastName"]))?string.Empty:(System.String)reader["LastName"];
					entity.DateOfBirth = (System.DateTime)reader[((int)VcsPatientColumn.DateOfBirth)];
					//entity.DateOfBirth = (Convert.IsDBNull(reader["DateOfBirth"]))?DateTime.MinValue:(System.DateTime)reader["DateOfBirth"];
					entity.Sex = (System.String)reader[((int)VcsPatientColumn.Sex)];
					//entity.Sex = (Convert.IsDBNull(reader["Sex"]))?string.Empty:(System.String)reader["Sex"];
					entity.MemberType = (System.String)reader[((int)VcsPatientColumn.MemberType)];
					//entity.MemberType = (Convert.IsDBNull(reader["MemberType"]))?string.Empty:(System.String)reader["MemberType"];
					entity.MembershipSosNumber = (reader.IsDBNull(((int)VcsPatientColumn.MembershipSosNumber)))?null:(System.String)reader[((int)VcsPatientColumn.MembershipSosNumber)];
					//entity.MembershipSosNumber = (Convert.IsDBNull(reader["MembershipSOSNumber"]))?string.Empty:(System.String)reader["MembershipSOSNumber"];
					entity.MembershipSosExpDate = (reader.IsDBNull(((int)VcsPatientColumn.MembershipSosExpDate)))?null:(System.DateTime?)reader[((int)VcsPatientColumn.MembershipSosExpDate)];
					//entity.MembershipSosExpDate = (Convert.IsDBNull(reader["MembershipSOSExpDate"]))?DateTime.MinValue:(System.DateTime?)reader["MembershipSOSExpDate"];
					entity.Nationality = (System.String)reader[((int)VcsPatientColumn.Nationality)];
					//entity.Nationality = (Convert.IsDBNull(reader["Nationality"]))?string.Empty:(System.String)reader["Nationality"];
					entity.HomeStreet = (reader.IsDBNull(((int)VcsPatientColumn.HomeStreet)))?null:(System.String)reader[((int)VcsPatientColumn.HomeStreet)];
					//entity.HomeStreet = (Convert.IsDBNull(reader["HomeStreet"]))?string.Empty:(System.String)reader["HomeStreet"];
					entity.HomeWard = (reader.IsDBNull(((int)VcsPatientColumn.HomeWard)))?null:(System.String)reader[((int)VcsPatientColumn.HomeWard)];
					//entity.HomeWard = (Convert.IsDBNull(reader["HomeWard"]))?string.Empty:(System.String)reader["HomeWard"];
					entity.HomeDistrict = (reader.IsDBNull(((int)VcsPatientColumn.HomeDistrict)))?null:(System.String)reader[((int)VcsPatientColumn.HomeDistrict)];
					//entity.HomeDistrict = (Convert.IsDBNull(reader["HomeDistrict"]))?string.Empty:(System.String)reader["HomeDistrict"];
					entity.HomeCity = (reader.IsDBNull(((int)VcsPatientColumn.HomeCity)))?null:(System.String)reader[((int)VcsPatientColumn.HomeCity)];
					//entity.HomeCity = (Convert.IsDBNull(reader["HomeCity"]))?string.Empty:(System.String)reader["HomeCity"];
					entity.HomeCountry = (reader.IsDBNull(((int)VcsPatientColumn.HomeCountry)))?null:(System.String)reader[((int)VcsPatientColumn.HomeCountry)];
					//entity.HomeCountry = (Convert.IsDBNull(reader["HomeCountry"]))?string.Empty:(System.String)reader["HomeCountry"];
					entity.CompanyCode = (reader.IsDBNull(((int)VcsPatientColumn.CompanyCode)))?null:(System.String)reader[((int)VcsPatientColumn.CompanyCode)];
					//entity.CompanyCode = (Convert.IsDBNull(reader["CompanyCode"]))?string.Empty:(System.String)reader["CompanyCode"];
					entity.BillingAddress = (reader.IsDBNull(((int)VcsPatientColumn.BillingAddress)))?null:(System.String)reader[((int)VcsPatientColumn.BillingAddress)];
					//entity.BillingAddress = (Convert.IsDBNull(reader["BillingAddress"]))?string.Empty:(System.String)reader["BillingAddress"];
					entity.HomePhone = (reader.IsDBNull(((int)VcsPatientColumn.HomePhone)))?null:(System.String)reader[((int)VcsPatientColumn.HomePhone)];
					//entity.HomePhone = (Convert.IsDBNull(reader["HomePhone"]))?string.Empty:(System.String)reader["HomePhone"];
					entity.MobilePhone = (reader.IsDBNull(((int)VcsPatientColumn.MobilePhone)))?null:(System.String)reader[((int)VcsPatientColumn.MobilePhone)];
					//entity.MobilePhone = (Convert.IsDBNull(reader["MobilePhone"]))?string.Empty:(System.String)reader["MobilePhone"];
					entity.CompanyPhone = (reader.IsDBNull(((int)VcsPatientColumn.CompanyPhone)))?null:(System.String)reader[((int)VcsPatientColumn.CompanyPhone)];
					//entity.CompanyPhone = (Convert.IsDBNull(reader["CompanyPhone"]))?string.Empty:(System.String)reader["CompanyPhone"];
					entity.Fax = (reader.IsDBNull(((int)VcsPatientColumn.Fax)))?null:(System.String)reader[((int)VcsPatientColumn.Fax)];
					//entity.Fax = (Convert.IsDBNull(reader["Fax"]))?string.Empty:(System.String)reader["Fax"];
					entity.EmailAddress = (reader.IsDBNull(((int)VcsPatientColumn.EmailAddress)))?null:(System.String)reader[((int)VcsPatientColumn.EmailAddress)];
					//entity.EmailAddress = (Convert.IsDBNull(reader["EmailAddress"]))?string.Empty:(System.String)reader["EmailAddress"];
					entity.CreateUser = (reader.IsDBNull(((int)VcsPatientColumn.CreateUser)))?null:(System.String)reader[((int)VcsPatientColumn.CreateUser)];
					//entity.CreateUser = (Convert.IsDBNull(reader["CreateUser"]))?string.Empty:(System.String)reader["CreateUser"];
					entity.CreateDate = (System.DateTime)reader[((int)VcsPatientColumn.CreateDate)];
					//entity.CreateDate = (Convert.IsDBNull(reader["CreateDate"]))?DateTime.MinValue:(System.DateTime)reader["CreateDate"];
					entity.ValidCorporate = (System.Boolean)reader[((int)VcsPatientColumn.ValidCorporate)];
					//entity.ValidCorporate = (Convert.IsDBNull(reader["ValidCorporate"]))?false:(System.Boolean)reader["ValidCorporate"];
					entity.DefaultPaymentMode = (System.String)reader[((int)VcsPatientColumn.DefaultPaymentMode)];
					//entity.DefaultPaymentMode = (Convert.IsDBNull(reader["DefaultPaymentMode"]))?string.Empty:(System.String)reader["DefaultPaymentMode"];
					entity.InsuranceCardNumber = (reader.IsDBNull(((int)VcsPatientColumn.InsuranceCardNumber)))?null:(System.String)reader[((int)VcsPatientColumn.InsuranceCardNumber)];
					//entity.InsuranceCardNumber = (Convert.IsDBNull(reader["InsuranceCardNumber"]))?string.Empty:(System.String)reader["InsuranceCardNumber"];
					entity.InsuranceCardExpDate = (reader.IsDBNull(((int)VcsPatientColumn.InsuranceCardExpDate)))?null:(System.DateTime?)reader[((int)VcsPatientColumn.InsuranceCardExpDate)];
					//entity.InsuranceCardExpDate = (Convert.IsDBNull(reader["InsuranceCardExpDate"]))?DateTime.MinValue:(System.DateTime?)reader["InsuranceCardExpDate"];
					entity.IsDisabled = (System.Boolean)reader[((int)VcsPatientColumn.IsDisabled)];
					//entity.IsDisabled = (Convert.IsDBNull(reader["IsDisabled"]))?false:(System.Boolean)reader["IsDisabled"];
					entity.UpdateUser = (reader.IsDBNull(((int)VcsPatientColumn.UpdateUser)))?null:(System.String)reader[((int)VcsPatientColumn.UpdateUser)];
					//entity.UpdateUser = (Convert.IsDBNull(reader["UpdateUser"]))?string.Empty:(System.String)reader["UpdateUser"];
					entity.UpdateDate = (System.DateTime)reader[((int)VcsPatientColumn.UpdateDate)];
					//entity.UpdateDate = (Convert.IsDBNull(reader["UpdateDate"]))?DateTime.MinValue:(System.DateTime)reader["UpdateDate"];
					entity.ApptRemark = (reader.IsDBNull(((int)VcsPatientColumn.ApptRemark)))?null:(System.String)reader[((int)VcsPatientColumn.ApptRemark)];
					//entity.ApptRemark = (Convert.IsDBNull(reader["ApptRemark"]))?string.Empty:(System.String)reader["ApptRemark"];
					entity.Remark = (reader.IsDBNull(((int)VcsPatientColumn.Remark)))?null:(System.String)reader[((int)VcsPatientColumn.Remark)];
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
		/// Refreshes the <see cref="VcsPatient"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="VcsPatient"/> object to refresh.</param>
		protected void RefreshEntity(IDataReader reader, VcsPatient entity)
		{
			reader.Read();
			entity.PatientCode = (System.String)reader[((int)VcsPatientColumn.PatientCode)];
			//entity.PatientCode = (Convert.IsDBNull(reader["PatientCode"]))?string.Empty:(System.String)reader["PatientCode"];
			entity.FirstName = (System.String)reader[((int)VcsPatientColumn.FirstName)];
			//entity.FirstName = (Convert.IsDBNull(reader["FirstName"]))?string.Empty:(System.String)reader["FirstName"];
			entity.MiddleName = (reader.IsDBNull(((int)VcsPatientColumn.MiddleName)))?null:(System.String)reader[((int)VcsPatientColumn.MiddleName)];
			//entity.MiddleName = (Convert.IsDBNull(reader["MiddleName"]))?string.Empty:(System.String)reader["MiddleName"];
			entity.LastName = (System.String)reader[((int)VcsPatientColumn.LastName)];
			//entity.LastName = (Convert.IsDBNull(reader["LastName"]))?string.Empty:(System.String)reader["LastName"];
			entity.DateOfBirth = (System.DateTime)reader[((int)VcsPatientColumn.DateOfBirth)];
			//entity.DateOfBirth = (Convert.IsDBNull(reader["DateOfBirth"]))?DateTime.MinValue:(System.DateTime)reader["DateOfBirth"];
			entity.Sex = (System.String)reader[((int)VcsPatientColumn.Sex)];
			//entity.Sex = (Convert.IsDBNull(reader["Sex"]))?string.Empty:(System.String)reader["Sex"];
			entity.MemberType = (System.String)reader[((int)VcsPatientColumn.MemberType)];
			//entity.MemberType = (Convert.IsDBNull(reader["MemberType"]))?string.Empty:(System.String)reader["MemberType"];
			entity.MembershipSosNumber = (reader.IsDBNull(((int)VcsPatientColumn.MembershipSosNumber)))?null:(System.String)reader[((int)VcsPatientColumn.MembershipSosNumber)];
			//entity.MembershipSosNumber = (Convert.IsDBNull(reader["MembershipSOSNumber"]))?string.Empty:(System.String)reader["MembershipSOSNumber"];
			entity.MembershipSosExpDate = (reader.IsDBNull(((int)VcsPatientColumn.MembershipSosExpDate)))?null:(System.DateTime?)reader[((int)VcsPatientColumn.MembershipSosExpDate)];
			//entity.MembershipSosExpDate = (Convert.IsDBNull(reader["MembershipSOSExpDate"]))?DateTime.MinValue:(System.DateTime?)reader["MembershipSOSExpDate"];
			entity.Nationality = (System.String)reader[((int)VcsPatientColumn.Nationality)];
			//entity.Nationality = (Convert.IsDBNull(reader["Nationality"]))?string.Empty:(System.String)reader["Nationality"];
			entity.HomeStreet = (reader.IsDBNull(((int)VcsPatientColumn.HomeStreet)))?null:(System.String)reader[((int)VcsPatientColumn.HomeStreet)];
			//entity.HomeStreet = (Convert.IsDBNull(reader["HomeStreet"]))?string.Empty:(System.String)reader["HomeStreet"];
			entity.HomeWard = (reader.IsDBNull(((int)VcsPatientColumn.HomeWard)))?null:(System.String)reader[((int)VcsPatientColumn.HomeWard)];
			//entity.HomeWard = (Convert.IsDBNull(reader["HomeWard"]))?string.Empty:(System.String)reader["HomeWard"];
			entity.HomeDistrict = (reader.IsDBNull(((int)VcsPatientColumn.HomeDistrict)))?null:(System.String)reader[((int)VcsPatientColumn.HomeDistrict)];
			//entity.HomeDistrict = (Convert.IsDBNull(reader["HomeDistrict"]))?string.Empty:(System.String)reader["HomeDistrict"];
			entity.HomeCity = (reader.IsDBNull(((int)VcsPatientColumn.HomeCity)))?null:(System.String)reader[((int)VcsPatientColumn.HomeCity)];
			//entity.HomeCity = (Convert.IsDBNull(reader["HomeCity"]))?string.Empty:(System.String)reader["HomeCity"];
			entity.HomeCountry = (reader.IsDBNull(((int)VcsPatientColumn.HomeCountry)))?null:(System.String)reader[((int)VcsPatientColumn.HomeCountry)];
			//entity.HomeCountry = (Convert.IsDBNull(reader["HomeCountry"]))?string.Empty:(System.String)reader["HomeCountry"];
			entity.CompanyCode = (reader.IsDBNull(((int)VcsPatientColumn.CompanyCode)))?null:(System.String)reader[((int)VcsPatientColumn.CompanyCode)];
			//entity.CompanyCode = (Convert.IsDBNull(reader["CompanyCode"]))?string.Empty:(System.String)reader["CompanyCode"];
			entity.BillingAddress = (reader.IsDBNull(((int)VcsPatientColumn.BillingAddress)))?null:(System.String)reader[((int)VcsPatientColumn.BillingAddress)];
			//entity.BillingAddress = (Convert.IsDBNull(reader["BillingAddress"]))?string.Empty:(System.String)reader["BillingAddress"];
			entity.HomePhone = (reader.IsDBNull(((int)VcsPatientColumn.HomePhone)))?null:(System.String)reader[((int)VcsPatientColumn.HomePhone)];
			//entity.HomePhone = (Convert.IsDBNull(reader["HomePhone"]))?string.Empty:(System.String)reader["HomePhone"];
			entity.MobilePhone = (reader.IsDBNull(((int)VcsPatientColumn.MobilePhone)))?null:(System.String)reader[((int)VcsPatientColumn.MobilePhone)];
			//entity.MobilePhone = (Convert.IsDBNull(reader["MobilePhone"]))?string.Empty:(System.String)reader["MobilePhone"];
			entity.CompanyPhone = (reader.IsDBNull(((int)VcsPatientColumn.CompanyPhone)))?null:(System.String)reader[((int)VcsPatientColumn.CompanyPhone)];
			//entity.CompanyPhone = (Convert.IsDBNull(reader["CompanyPhone"]))?string.Empty:(System.String)reader["CompanyPhone"];
			entity.Fax = (reader.IsDBNull(((int)VcsPatientColumn.Fax)))?null:(System.String)reader[((int)VcsPatientColumn.Fax)];
			//entity.Fax = (Convert.IsDBNull(reader["Fax"]))?string.Empty:(System.String)reader["Fax"];
			entity.EmailAddress = (reader.IsDBNull(((int)VcsPatientColumn.EmailAddress)))?null:(System.String)reader[((int)VcsPatientColumn.EmailAddress)];
			//entity.EmailAddress = (Convert.IsDBNull(reader["EmailAddress"]))?string.Empty:(System.String)reader["EmailAddress"];
			entity.CreateUser = (reader.IsDBNull(((int)VcsPatientColumn.CreateUser)))?null:(System.String)reader[((int)VcsPatientColumn.CreateUser)];
			//entity.CreateUser = (Convert.IsDBNull(reader["CreateUser"]))?string.Empty:(System.String)reader["CreateUser"];
			entity.CreateDate = (System.DateTime)reader[((int)VcsPatientColumn.CreateDate)];
			//entity.CreateDate = (Convert.IsDBNull(reader["CreateDate"]))?DateTime.MinValue:(System.DateTime)reader["CreateDate"];
			entity.ValidCorporate = (System.Boolean)reader[((int)VcsPatientColumn.ValidCorporate)];
			//entity.ValidCorporate = (Convert.IsDBNull(reader["ValidCorporate"]))?false:(System.Boolean)reader["ValidCorporate"];
			entity.DefaultPaymentMode = (System.String)reader[((int)VcsPatientColumn.DefaultPaymentMode)];
			//entity.DefaultPaymentMode = (Convert.IsDBNull(reader["DefaultPaymentMode"]))?string.Empty:(System.String)reader["DefaultPaymentMode"];
			entity.InsuranceCardNumber = (reader.IsDBNull(((int)VcsPatientColumn.InsuranceCardNumber)))?null:(System.String)reader[((int)VcsPatientColumn.InsuranceCardNumber)];
			//entity.InsuranceCardNumber = (Convert.IsDBNull(reader["InsuranceCardNumber"]))?string.Empty:(System.String)reader["InsuranceCardNumber"];
			entity.InsuranceCardExpDate = (reader.IsDBNull(((int)VcsPatientColumn.InsuranceCardExpDate)))?null:(System.DateTime?)reader[((int)VcsPatientColumn.InsuranceCardExpDate)];
			//entity.InsuranceCardExpDate = (Convert.IsDBNull(reader["InsuranceCardExpDate"]))?DateTime.MinValue:(System.DateTime?)reader["InsuranceCardExpDate"];
			entity.IsDisabled = (System.Boolean)reader[((int)VcsPatientColumn.IsDisabled)];
			//entity.IsDisabled = (Convert.IsDBNull(reader["IsDisabled"]))?false:(System.Boolean)reader["IsDisabled"];
			entity.UpdateUser = (reader.IsDBNull(((int)VcsPatientColumn.UpdateUser)))?null:(System.String)reader[((int)VcsPatientColumn.UpdateUser)];
			//entity.UpdateUser = (Convert.IsDBNull(reader["UpdateUser"]))?string.Empty:(System.String)reader["UpdateUser"];
			entity.UpdateDate = (System.DateTime)reader[((int)VcsPatientColumn.UpdateDate)];
			//entity.UpdateDate = (Convert.IsDBNull(reader["UpdateDate"]))?DateTime.MinValue:(System.DateTime)reader["UpdateDate"];
			entity.ApptRemark = (reader.IsDBNull(((int)VcsPatientColumn.ApptRemark)))?null:(System.String)reader[((int)VcsPatientColumn.ApptRemark)];
			//entity.ApptRemark = (Convert.IsDBNull(reader["ApptRemark"]))?string.Empty:(System.String)reader["ApptRemark"];
			entity.Remark = (reader.IsDBNull(((int)VcsPatientColumn.Remark)))?null:(System.String)reader[((int)VcsPatientColumn.Remark)];
			//entity.Remark = (Convert.IsDBNull(reader["Remark"]))?string.Empty:(System.String)reader["Remark"];
			reader.Close();
	
			entity.AcceptChanges();
		}
		
		/*
		/// <summary>
		/// Refreshes the <see cref="VcsPatient"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="VcsPatient"/> object.</param>
		protected static void RefreshEntity(DataSet dataSet, VcsPatient entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.PatientCode = (Convert.IsDBNull(dataRow["PatientCode"]))?string.Empty:(System.String)dataRow["PatientCode"];
			entity.FirstName = (Convert.IsDBNull(dataRow["FirstName"]))?string.Empty:(System.String)dataRow["FirstName"];
			entity.MiddleName = (Convert.IsDBNull(dataRow["MiddleName"]))?string.Empty:(System.String)dataRow["MiddleName"];
			entity.LastName = (Convert.IsDBNull(dataRow["LastName"]))?string.Empty:(System.String)dataRow["LastName"];
			entity.DateOfBirth = (Convert.IsDBNull(dataRow["DateOfBirth"]))?DateTime.MinValue:(System.DateTime)dataRow["DateOfBirth"];
			entity.Sex = (Convert.IsDBNull(dataRow["Sex"]))?string.Empty:(System.String)dataRow["Sex"];
			entity.MemberType = (Convert.IsDBNull(dataRow["MemberType"]))?string.Empty:(System.String)dataRow["MemberType"];
			entity.MembershipSosNumber = (Convert.IsDBNull(dataRow["MembershipSOSNumber"]))?string.Empty:(System.String)dataRow["MembershipSOSNumber"];
			entity.MembershipSosExpDate = (Convert.IsDBNull(dataRow["MembershipSOSExpDate"]))?DateTime.MinValue:(System.DateTime?)dataRow["MembershipSOSExpDate"];
			entity.Nationality = (Convert.IsDBNull(dataRow["Nationality"]))?string.Empty:(System.String)dataRow["Nationality"];
			entity.HomeStreet = (Convert.IsDBNull(dataRow["HomeStreet"]))?string.Empty:(System.String)dataRow["HomeStreet"];
			entity.HomeWard = (Convert.IsDBNull(dataRow["HomeWard"]))?string.Empty:(System.String)dataRow["HomeWard"];
			entity.HomeDistrict = (Convert.IsDBNull(dataRow["HomeDistrict"]))?string.Empty:(System.String)dataRow["HomeDistrict"];
			entity.HomeCity = (Convert.IsDBNull(dataRow["HomeCity"]))?string.Empty:(System.String)dataRow["HomeCity"];
			entity.HomeCountry = (Convert.IsDBNull(dataRow["HomeCountry"]))?string.Empty:(System.String)dataRow["HomeCountry"];
			entity.CompanyCode = (Convert.IsDBNull(dataRow["CompanyCode"]))?string.Empty:(System.String)dataRow["CompanyCode"];
			entity.BillingAddress = (Convert.IsDBNull(dataRow["BillingAddress"]))?string.Empty:(System.String)dataRow["BillingAddress"];
			entity.HomePhone = (Convert.IsDBNull(dataRow["HomePhone"]))?string.Empty:(System.String)dataRow["HomePhone"];
			entity.MobilePhone = (Convert.IsDBNull(dataRow["MobilePhone"]))?string.Empty:(System.String)dataRow["MobilePhone"];
			entity.CompanyPhone = (Convert.IsDBNull(dataRow["CompanyPhone"]))?string.Empty:(System.String)dataRow["CompanyPhone"];
			entity.Fax = (Convert.IsDBNull(dataRow["Fax"]))?string.Empty:(System.String)dataRow["Fax"];
			entity.EmailAddress = (Convert.IsDBNull(dataRow["EmailAddress"]))?string.Empty:(System.String)dataRow["EmailAddress"];
			entity.CreateUser = (Convert.IsDBNull(dataRow["CreateUser"]))?string.Empty:(System.String)dataRow["CreateUser"];
			entity.CreateDate = (Convert.IsDBNull(dataRow["CreateDate"]))?DateTime.MinValue:(System.DateTime)dataRow["CreateDate"];
			entity.ValidCorporate = (Convert.IsDBNull(dataRow["ValidCorporate"]))?false:(System.Boolean)dataRow["ValidCorporate"];
			entity.DefaultPaymentMode = (Convert.IsDBNull(dataRow["DefaultPaymentMode"]))?string.Empty:(System.String)dataRow["DefaultPaymentMode"];
			entity.InsuranceCardNumber = (Convert.IsDBNull(dataRow["InsuranceCardNumber"]))?string.Empty:(System.String)dataRow["InsuranceCardNumber"];
			entity.InsuranceCardExpDate = (Convert.IsDBNull(dataRow["InsuranceCardExpDate"]))?DateTime.MinValue:(System.DateTime?)dataRow["InsuranceCardExpDate"];
			entity.IsDisabled = (Convert.IsDBNull(dataRow["IsDisabled"]))?false:(System.Boolean)dataRow["IsDisabled"];
			entity.UpdateUser = (Convert.IsDBNull(dataRow["UpdateUser"]))?string.Empty:(System.String)dataRow["UpdateUser"];
			entity.UpdateDate = (Convert.IsDBNull(dataRow["UpdateDate"]))?DateTime.MinValue:(System.DateTime)dataRow["UpdateDate"];
			entity.ApptRemark = (Convert.IsDBNull(dataRow["ApptRemark"]))?string.Empty:(System.String)dataRow["ApptRemark"];
			entity.Remark = (Convert.IsDBNull(dataRow["Remark"]))?string.Empty:(System.String)dataRow["Remark"];
			entity.AcceptChanges();
		}
		*/
			
		#endregion Helper Functions
	}//end class

	#region VcsPatientFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="VcsPatient"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VcsPatientFilterBuilder : SqlFilterBuilder<VcsPatientColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VcsPatientFilterBuilder class.
		/// </summary>
		public VcsPatientFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the VcsPatientFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public VcsPatientFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the VcsPatientFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public VcsPatientFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion VcsPatientFilterBuilder

	#region VcsPatientParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="VcsPatient"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VcsPatientParameterBuilder : ParameterizedSqlFilterBuilder<VcsPatientColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VcsPatientParameterBuilder class.
		/// </summary>
		public VcsPatientParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the VcsPatientParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public VcsPatientParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the VcsPatientParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public VcsPatientParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion VcsPatientParameterBuilder
	
	#region VcsPatientSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="VcsPatient"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class VcsPatientSortBuilder : SqlSortBuilder<VcsPatientColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VcsPatientSqlSortBuilder class.
		/// </summary>
		public VcsPatientSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion VcsPatientSortBuilder

} // end namespace
