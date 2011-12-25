USE [ClinicDoctor]
GO
/****** Object:  StoredProcedure [dbo].[GetStaffBySingleRoleByIsDisabled]    Script Date: 12/14/2011 21:13:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Phat
-- Create date: 03/12/2011
-- Description: Get Staff list follow Roles
-- =============================================
CREATE PROCEDURE [dbo].[GetRoomByDoctorUserNameByIsDisabledWithOrderPriority]
 -- Add the parameters for the stored procedure here
 @DoctorUserName nvarchar(200) = '',
 @FuncId bigint,
 @IsDisabled bit = false
AS
BEGIN
 -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
 SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT	Room.*
FROM	(SELECT [Room].[Id]
				,[Room].[Title]
				,[Room].[Note]
				,[Room].[Status]
				,[Room].[IsDisabled]
				,[Room].[CreateUser]
				,[Room].[CreateDate]
				,[Room].[UpdateUser]
				,[Room].[UpdateDate]
		FROM [dbo].[Room]
		RIGHT JOIN [dbo].[RoomFunc] ON [Room].[Id] = [RoomFunc].[RoomId]
		WHERE	[RoomFunc].[FuncId] = @FuncId
		AND		[Room].[IsDisabled] = @IsDisabled ) Room
   LEFT JOIN [dbo].[DoctorRoom] ON Room.[Id] = [DoctorRoom].[RoomId] 
   AND [DoctorRoom].[DoctorUserName] = @DoctorUserName
   AND [DoctorRoom].[IsDisabled] = @IsDisabled
   ORDER BY [DoctorRoom].[PriorityIndex] ASC
END