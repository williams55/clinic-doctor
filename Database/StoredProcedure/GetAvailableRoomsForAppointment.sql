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
 @AppointmentId nvarchar(20) = '''',
 @DoctorUserName nvarchar(200) = '''',
 @FuncId bigint,
 @StartTime datetime,
 @EndTime datetime
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
FROM	(SELECT	[RoomFunc].[RoomId]
				,[RoomFunc].[RoomTitle]
				,CASE
					WHEN	[Appointment].[RoomId] IS NOT NULL 
					THEN	'1'
					ELSE	[Room].[Status]
				  END AS [Status]
		FROM	[dbo].[RoomFunc]
		LEFT JOIN [dbo].[Room] ON [RoomFunc].[RoomId] = [Room].[Id]
		AND		[Room].[IsDisabled] = @IsDisabled
		LEFT JOIN [dbo].[Appointment] ON [RoomFunc].[RoomId] = [Appointment].[RoomId]
		AND	[Appointment].[Id] <> @AppointmentId
		AND	(([Appointment].[StartTime] < @EndTime AND [Appointment].[StartTime] >= @StartTime)
		OR		([Appointment].[EndTime] > @StartTime AND [Appointment].[EndTime] <= @EndTime))
		AND	[Appointment].[IsDisabled] = @IsDisabled
		WHERE	[RoomFunc].[FuncId] = @FuncId
		AND		[RoomFunc].[IsDisabled] = @IsDisabled) [Room]
   LEFT JOIN [dbo].[DoctorRoom] ON [Room].[RoomId] = [DoctorRoom].[RoomId] 
   AND [DoctorRoom].[DoctorUserName] = @DoctorUserName
   AND [DoctorRoom].[IsDisabled] = @IsDisabled
   ORDER BY [DoctorRoom].[PriorityIndex] ASC
			, [Room].[RoomTitle] ASC

END