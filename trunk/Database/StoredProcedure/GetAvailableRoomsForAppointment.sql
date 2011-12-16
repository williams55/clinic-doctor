set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go

-- =============================================
-- Author:  Phat
-- Create date: 03/12/2011
-- Description: Get Staff list follow Roles
-- =============================================
CREATE PROCEDURE [dbo].[GetAvailableRoomsForAppointment]
 -- Add the parameters for the stored procedure here
 @DoctorUserName nvarchar(200) = '',
 @FuncId bigint
AS
BEGIN
 -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
 SET NOCOUNT ON;

DECLARE @IsDisabled bit
SET @IsDisabled = 'false'

    -- Insert statements for procedure here
SELECT	[Room].[RoomId]
		, [Room].[RoomTitle]
		, [Room].[Status]
		, [Room].[IsDisabled]
FROM	(SELECT	[RoomFunc].[RoomId]
				,[RoomFunc].[RoomTitle]
				,[Room].[Status]
				,CASE
					WHEN	[Appointment].[RoomId] IS NULL 
					THEN	'False'
					ELSE	'True'
				  END AS [IsDisabled]
		FROM	[dbo].[RoomFunc]
		LEFT JOIN [dbo].[Room] ON [RoomFunc].[RoomId] = [Room].[Id]
		AND		[Room].[IsDisabled] = @IsDisabled
		LEFT JOIN [dbo].[Appointment] ON [RoomFunc].[RoomId] = [Appointment].[RoomId]
		WHERE	[RoomFunc].[FuncId] = @FuncId
		AND		[RoomFunc].[IsDisabled] = @IsDisabled) [Room]
   LEFT JOIN [dbo].[DoctorRoom] ON [Room].[RoomId] = [DoctorRoom].[RoomId] 
   AND [DoctorRoom].[DoctorUserName] = @DoctorUserName
   AND [DoctorRoom].[IsDisabled] = @IsDisabled
   ORDER BY [DoctorRoom].[PriorityIndex] ASC
			, [Room].[RoomTitle] ASC

END
