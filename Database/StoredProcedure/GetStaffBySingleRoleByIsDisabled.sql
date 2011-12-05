-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Phat
-- Create date: 03/12/2011
-- Description:	Get Staff list follow Roles
-- =============================================
CREATE PROCEDURE [GetStaffBySingleRoleByIsDisabled]
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
		  ,[GroupId]
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
	  WHERE	(@SingleRole = '-1' OR [Roles] LIKE '%' + @SingleRole + '%')
	  AND	[IsDisabled] = @IsDisabled
	  ORDER BY [LastName]
END
GO
