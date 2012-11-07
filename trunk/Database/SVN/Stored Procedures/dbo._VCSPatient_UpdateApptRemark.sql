SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[_VCSPatient_UpdateApptRemark]
	-- Add the parameters for the stored procedure here
	@PatientCode nchar(11)
	,@UpdateUser nvarchar(50)
	,@ApptRemark nvarchar(250)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
UPDATE [VCS].[dbo].[Patient]
   SET [UpdateUser] = @UpdateUser
      ,[UpdateDate] = GETDATE()
      ,[ApptRemark] = @ApptRemark
 WHERE [PatientCode] = @PatientCode
 
	RETURN @@ROWCOUNT

END
GO
