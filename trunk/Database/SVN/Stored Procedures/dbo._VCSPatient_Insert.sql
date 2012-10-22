
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
	@PatientCode nchar(11)
	,@FirstName nvarchar(30)
	,@MiddleName nvarchar(30)
	,@LastName nvarchar(30)
	,@DateOfBirth datetime
	,@Sex char(1)
	,@Nationality nvarchar(50)
	,@CompanyCode nchar(9)
	,@HomePhone nvarchar(50)
	,@MobilePhone nvarchar(50)
	,@CreateUser nvarchar(50)
	,@ApptRemark nvarchar(250)
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
				   ,[Nationality]
				   ,[CompanyCode]
				   ,[HomePhone]
				   ,[MobilePhone]
				   ,[CreateUser]
				   ,[CreateDate]
				   ,[UpdateUser]
				   ,[UpdateDate]
				   ,[ApptRemark])
		 VALUES
			   (@PatientCode
				,@FirstName
				,@MiddleName
				,@LastName
				,@DateOfBirth
				,@Sex
				,@Nationality
				,@CompanyCode
				,@HomePhone
				,@MobilePhone
				,@CreateUser
				,GETDATE()
				,@CreateUser
				,GETDATE()
				,@ApptRemark
				)

	SELECT * FROM [VCS].[dbo].[Patient] WHERE [PatientCode] = @PatientCode

END
GO
