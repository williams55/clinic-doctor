USE [ClinicDoctor]
GO
/****** Object:  StoredProcedure [dbo].[GetStaffBySingleRoleByIsDisabled]    Script Date: 12/15/2011 22:05:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Phat
-- Create date: 03/12/2011
-- Description: Get Staff list follow Roles
-- =============================================
CREATE PROCEDURE [dbo].[GetAvailableStaffsForAppointment]
 -- Add the parameters for the stored procedure here
 @AppointmentId nvarchar(20) = '',
 @FuncId bigint,
 @StartTime datetime,
 @EndTime datetime
AS
BEGIN
 -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
 SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT	Dr.[Id]
		, Dr.[DoctorUserName]
		, Dr.[DoctorShortName]
		, [Appointment].[DoctorUserName]
		, CASE
			WHEN [Appointment].[DoctorUserName] IS NOT NULL THEN '1'
			WHEN Dr.[IsSelected] IS NULL THEN '2'
			WHEN Dr.[IsBooked] = 'False' THEN '3'
			ELSE '0'
		  END AS [Status]
FROM	(SELECT	[DoctorFunc].[Id]
				, [DoctorFunc].[DoctorUserName]
				, [DoctorFunc].[DoctorShortName]
				, [DoctorRoster].[DoctorUserName] AS IsSelected
				, [DoctorRoster].[IsBooked]
		 FROM	[DoctorFunc]
		 LEFT JOIN [DoctorRoster] ON [DoctorFunc].[DoctorUserName] = [DoctorRoster].[DoctorUserName]
		 AND	[DoctorRoster].[StartTime] <= @StartTime
		 AND	[DoctorRoster].[EndTime] >= @EndTime
		 AND	[DoctorRoster].[IsDisabled] = 'false'
		 AND	@StartTime < @EndTime
		 WHERE	[DoctorFunc].[FuncId] = @FuncId
		 AND	[DoctorFunc].[IsDisabled] = 'false'
		 GROUP BY [DoctorFunc].[Id]
				, [DoctorFunc].[DoctorUserName]
				, [DoctorFunc].[DoctorShortName]
				, [DoctorRoster].[DoctorUserName]
				, [DoctorRoster].[IsBooked]) Dr
 LEFT JOIN [Appointment] ON Dr.[DoctorUserName] = [Appointment].[DoctorUserName]
 AND	[Appointment].[Id] <> @AppointmentId
 AND	(([Appointment].[StartTime] < @EndTime AND [Appointment].[StartTime] >= @StartTime)
 OR		([Appointment].[EndTime] > @StartTime AND [Appointment].[EndTime] <= @EndTime))
 AND	[Appointment].[IsDisabled] = 'false'
 ORDER BY Dr.[DoctorShortName] ASC
END