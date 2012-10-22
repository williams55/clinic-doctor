
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[_VCSPatient_Update]
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
	,@UpdateUser nvarchar(50)
	,@ApptRemark nvarchar(250)
	,@IsDisabled bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
UPDATE [VCS].[dbo].[Patient]
   SET [FirstName] = @FirstName
      ,[MiddleName] = @MiddleName
      ,[LastName] = @LastName
      ,[DateOfBirth] = @DateOfBirth
      ,[Sex] = @Sex
      ,[Nationality] = @Nationality
      ,[CompanyCode] = @CompanyCode
      ,[HomePhone] = @HomePhone
      ,[MobilePhone] = @MobilePhone
      ,[UpdateUser] = @UpdateUser
      ,[UpdateDate] = GETDATE()
      ,[ApptRemark] = @ApptRemark
      ,[IsDisabled] = @IsDisabled
 WHERE [PatientCode] = @PatientCode
 
	RETURN @@ROWCOUNT

END
GO
