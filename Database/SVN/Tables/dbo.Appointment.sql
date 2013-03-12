CREATE TABLE [dbo].[Appointment]
(
[Id] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[PatientCode] [nchar] (11) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Username] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[RoomId] [int] NULL,
[ServicesId] [int] NULL,
[StatusId] [varchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[AppointmentGroupId] [int] NULL,
[Note] [nvarchar] (1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[StartTime] [datetime] NULL,
[EndTime] [datetime] NULL,
[RosterId] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IsComplete] [bit] NOT NULL CONSTRAINT [DF_Appointment_IsComplete] DEFAULT ((0)),
[IsDisabled] [bit] NOT NULL CONSTRAINT [DF_Appointment_IsDisabled] DEFAULT ((0)),
[CreateUser] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CreateDate] [datetime] NOT NULL CONSTRAINT [DF_Appointment_CreateDate] DEFAULT (getdate()),
[UpdateUser] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[UpdateDate] [datetime] NOT NULL CONSTRAINT [DF_Appointment_UpdateDate] DEFAULT (getdate())
) ON [PRIMARY]
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [dbo].[trgInsertHistory]
   ON  [dbo].[Appointment]
   AFTER UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	declare @OldStatus varchar(20)
	declare @NewStatus varchar(20)
	DECLARE @AppointmentId nvarchar(20)
	DECLARE @UpdateUser nvarchar(200)
	
	select @AppointmentId=i.Id
			,@NewStatus = i.StatusId
			,@UpdateUser = i.UpdateUser
	from inserted i;
	
	select @OldStatus=d.StatusId
	from deleted d
	WHERE d.Id = @AppointmentId
	
	IF @OldStatus <> @NewStatus
	BEGIN
		INSERT INTO dbo.AppointmentHistory
		        ( Guid ,
		          AppointmentId ,
		          Note ,
		          CreateUser ,
		          CreateDate
		        )
		VALUES  ( NEWID() , -- Guid - uniqueidentifier
		          @AppointmentId , -- AppointmentId - nvarchar(20)
		          N'User ' + @UpdateUser + ' change appointment ' + @AppointmentId + ' from status ' + @OldStatus + ' to ' + @NewStatus , -- Note - nvarchar(500)
		          @UpdateUser , -- CreateUser - nvarchar(200)
		          GETDATE()  -- CreateDate - datetime
		        )
	END

END
GO

ALTER TABLE [dbo].[Appointment] ADD
CONSTRAINT [FK_Appointment_Roster] FOREIGN KEY ([RosterId]) REFERENCES [dbo].[Roster] ([Id])
ALTER TABLE [dbo].[Appointment] ADD
CONSTRAINT [FK_Appointment_Users] FOREIGN KEY ([Username]) REFERENCES [dbo].[Users] ([Username])
GO
ALTER TABLE [dbo].[Appointment] ADD CONSTRAINT [PK_Appointment] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Appointment] ADD CONSTRAINT [FK_Appointment_AppointmentGroup] FOREIGN KEY ([AppointmentGroupId]) REFERENCES [dbo].[AppointmentGroup] ([Id])
GO

ALTER TABLE [dbo].[Appointment] ADD CONSTRAINT [FK_Appointment_Room] FOREIGN KEY ([RoomId]) REFERENCES [dbo].[Room] ([Id])
GO
ALTER TABLE [dbo].[Appointment] ADD CONSTRAINT [FK_Appointment_Procedure] FOREIGN KEY ([ServicesId]) REFERENCES [dbo].[Services] ([Id])
GO
ALTER TABLE [dbo].[Appointment] ADD CONSTRAINT [FK_Appointment_Status] FOREIGN KEY ([StatusId]) REFERENCES [dbo].[Status] ([Id])
GO
EXEC sp_addextendedproperty N'MS_Description', N'What do patient wanna be served', 'SCHEMA', N'dbo', 'TABLE', N'Appointment', 'COLUMN', N'ServicesId'
GO
