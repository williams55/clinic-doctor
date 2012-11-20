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
	@Id nvarchar(20), 
	@StatusId nvarchar(20), 
	@UpdateUser nvarchar(200), 
	@Note nvarchar(500) OUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	SELECT @Note = Note
	FROM	dbo.Appointment
	WHERE	Id = @Id
	AND		IsDisabled = 'False'

    -- Insert statements for procedure here
	UPDATE	dbo.Appointment
	SET		StatusId = @StatusId
			,UpdateUser = @UpdateUser
			,UpdateDate = GETDATE()
	WHERE	Id = @Id
	AND		IsDisabled = 'False'
	
	RETURN @@ROWCOUNT
END
GO
