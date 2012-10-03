
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[_VCSPatient_Insert]
	-- Add the parameters for the stored procedure here
	@PatientCode nvarchar(11)
	,@FirstName nvarchar(30)
	,@MiddleName nvarchar(30)
	,@LastName nvarchar(30)
	,@DateOfBirth datetime
	,@Sex char(1)
	,@MemberType nvarchar(50)
	,@Nationality nvarchar(50)
	,@HomeStreet nvarchar(100)
	,@HomeWard nvarchar(50)
	,@HomeDistrict nvarchar(50)
	,@HomeCity nvarchar(50)
	,@HomeCountry nvarchar(50)
	,@WorkStreet nvarchar(100)
	,@WorkWard nvarchar(50)
	,@WorkDistrict nvarchar(50)
	,@WorkCity nvarchar(50)
	,@WorkCountry nvarchar(50)
	,@CompanyCode nchar(9)
	,@BillingAddress nvarchar(50)
	,@HomePhone nvarchar(50)
	,@MobilePhone nvarchar(50)
	,@CompanyPhone nvarchar(50)
	,@Fax nvarchar(50)
	,@EmailAddress nvarchar(50)
	,@CreateDate datetime
	,@UpdateUser nvarchar(50)
	,@UpdateDate datetime
	,@Remark nvarchar(250)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @LocationCode NVARCHAR(5)
	DECLARE @TmpLastName NVARCHAR(30)
	DECLARE @TmpPatientCode NVARCHAR(11)
	SET @LocationCode=@PatientCode
	SET @TmpLastName=LEFT(@LastName, 1)
	EXEC @PatientCode = VCS.dbo.CreatePatientCode @Location = @LocationCode, -- nvarchar(4)
			@LastName = @TmpLastName -- nvarchar(30)

    -- Insert statements for procedure here
	INSERT INTO [VCS].[dbo].[Patient]
			   ([PatientCode]
			   ,[FirstName]
			   ,[MiddleName]
			   ,[LastName]
			   ,[DateOfBirth]
			   ,[Sex]
			   ,[MemberType]
			   ,[Nationality]
			   ,[HomeStreet]
			   ,[HomeWard]
			   ,[HomeDistrict]
			   ,[HomeCity]
			   ,[HomeCountry]
			   ,[WorkStreet]
			   ,[WorkWard]
			   ,[WorkDistrict]
			   ,[WorkCity]
			   ,[WorkCountry]
			   ,[CompanyCode]
			   ,[BillingAddress]
			   ,[HomePhone]
			   ,[MobilePhone]
			   ,[CompanyPhone]
			   ,[Fax]
			   ,[EmailAddress]
			   ,[CreateDate]
			   ,[UpdateUser]
			   ,[UpdateDate]
			   ,[Remark])
		 VALUES
			   (@PatientCode
,@FirstName
,@MiddleName
,@LastName
,@DateOfBirth
,@Sex
,@MemberType
,@Nationality
,@HomeStreet
,@HomeWard
,@HomeDistrict
,@HomeCity
,@HomeCountry
,@WorkStreet
,@WorkWard
,@WorkDistrict
,@WorkCity
,@WorkCountry
,@CompanyCode
,@BillingAddress
,@HomePhone
,@MobilePhone
,@CompanyPhone
,@Fax
,@EmailAddress
,GETDATE()
,@UpdateUser
,GETDATE()
,@Remark
)

	SELECT * FROM [VCS].[dbo].[Patient] WHERE [PatientCode] = @PatientCode

END
GO
