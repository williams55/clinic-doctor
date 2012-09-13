CREATE TABLE [dbo].[Appointment]
(
[Id] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[PatientCode] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Username] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[RoomId] [int] NULL,
[ServicesId] [int] NULL,
[StatusId] [varchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[AppointmentGroupId] [int] NULL,
[Note] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
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
ALTER TABLE [dbo].[Appointment] ADD
CONSTRAINT [FK_Appointment_Roster] FOREIGN KEY ([RosterId]) REFERENCES [dbo].[Roster] ([Id])
ALTER TABLE [dbo].[Appointment] ADD
CONSTRAINT [FK_Appointment_Users] FOREIGN KEY ([Username]) REFERENCES [dbo].[Users] ([Username])
GO
ALTER TABLE [dbo].[Appointment] ADD CONSTRAINT [PK_Appointment] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Appointment] ADD CONSTRAINT [FK_Appointment_AppointmentGroup] FOREIGN KEY ([AppointmentGroupId]) REFERENCES [dbo].[AppointmentGroup] ([Id])
GO

ALTER TABLE [dbo].[Appointment] ADD CONSTRAINT [FK_Appointment_Patient] FOREIGN KEY ([PatientCode]) REFERENCES [dbo].[Patient] ([PatientCode])
GO
ALTER TABLE [dbo].[Appointment] ADD CONSTRAINT [FK_Appointment_Room] FOREIGN KEY ([RoomId]) REFERENCES [dbo].[Room] ([Id])
GO
ALTER TABLE [dbo].[Appointment] ADD CONSTRAINT [FK_Appointment_Procedure] FOREIGN KEY ([ServicesId]) REFERENCES [dbo].[Services] ([Id])
GO
ALTER TABLE [dbo].[Appointment] ADD CONSTRAINT [FK_Appointment_Status] FOREIGN KEY ([StatusId]) REFERENCES [dbo].[Status] ([Id])
GO
EXEC sp_addextendedproperty N'MS_Description', N'What do patient wanna be served', 'SCHEMA', N'dbo', 'TABLE', N'Appointment', 'COLUMN', N'ServicesId'
GO
