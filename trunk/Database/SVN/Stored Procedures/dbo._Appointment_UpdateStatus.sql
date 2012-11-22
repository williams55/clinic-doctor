
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		PhatVT
-- Create date: Nov 20 2012
-- Description:	Ham doi status cua appointment.
--				Tra ve 0 neu khong co row duoc update
--				Tra ve > 0 neu co row duoc update
--				Out la Note, tra ve Note cua appointment
-- =============================================
CREATE PROCEDURE [dbo].[_Appointment_UpdateStatus]
	-- Add the parameters for the stored procedure here
	@PatientCode nchar(11), 
	@StatusId nvarchar(20), 
	@UpdateUser nvarchar(200), 
	@Result INT OUT, 
	@Id nvarchar(20) OUT, 
	@Note nvarchar(500) OUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	DECLARE @dt DATETIME
	SET @dt = GETDATE()
	SET @dt = CAST(CAST(YEAR(@dt) AS varchar) + '-' 
		+ CAST(MONTH(@dt) AS varchar) + '-' 
		+ CAST(DAY(@dt) AS varchar)
		+ ' 23:59:59' AS DATETIME)
	
	-- Lay thong tin dua vao patient code
	-- Lay appt dau tien thoa dieu kien thoi gian hien tai 
	-- chenh lech 15mins so voi thoi gian bat dau/ket thuc
	SELECT	TOP 1
			@Note = Note
			,@Id = Id
	FROM	dbo.Appointment
	WHERE	PatientCode = @PatientCode
	AND		IsDisabled = 'False'
	AND		DATEADD(minute, 15, EndTime) >= GETDATE()
	AND		DATEADD(minute, -15, StartTime) <= GETDATE()
	ORDER BY EndTime

	-- Tien hanh cap nhat status
	UPDATE	dbo.Appointment
	SET		StatusId = @StatusId
			,UpdateUser = @UpdateUser
			,UpdateDate = GETDATE()
	WHERE	Id = @Id
	
	SET @Result = @@ROWCOUNT
END
GO
