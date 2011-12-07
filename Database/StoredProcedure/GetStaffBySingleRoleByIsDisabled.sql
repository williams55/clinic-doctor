USE [ClinicDoctor]
GO
/****** Object:  StoredProcedure [dbo].[GetStaffBySingleRoleByIsDisabled]    Script Date: 12/07/2011 21:52:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Phat
-- Create date: 03/12/2011
-- Description: Get Staff list follow Roles
-- =============================================
CREATE PROCEDURE [dbo].[GetStaffBySingleRoleByIsDisabled]
 -- Add the parameters for the stored procedure here
 @SingleRole nvarchar(20) = '',
 @IsDisabled bit = false
AS
BEGIN
 -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
 SET NOCOUNT ON;

    -- Insert statements for procedure here
 SELECT [Id]
    ,[FirstName]
    ,[LastName]
    ,[ShortName]
    ,[UserName]
    ,[Address]
    ,[HomePhone]
    ,[WorkPhone]
    ,[CellPhone]
    ,[Birthdate]
    ,[IsFemale]
    ,[Title]
    ,[Note]
    ,[Roles]
    ,[IsDisabled]
    ,[CreateUser]
    ,[CreateDate]
    ,[UpdateUser]
    ,[UpdateDate]
   FROM [dbo].[Staff]
   WHERE (@SingleRole = '-1' OR [Roles] LIKE '%' + @SingleRole + '%')
   AND [IsDisabled] = @IsDisabled
   ORDER BY [LastName]
END