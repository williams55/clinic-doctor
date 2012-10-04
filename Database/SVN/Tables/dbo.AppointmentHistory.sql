CREATE TABLE [dbo].[AppointmentHistory]
(
[Guid] [uniqueidentifier] NOT NULL,
[AppointmentId] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Note] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CreateUser] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CreateDate] [datetime] NOT NULL CONSTRAINT [DF_AppointmentHistory_CreateDate] DEFAULT (getdate())
) ON [PRIMARY]
ALTER TABLE [dbo].[AppointmentHistory] ADD
CONSTRAINT [FK_AppointmentHistory_Appointment] FOREIGN KEY ([AppointmentId]) REFERENCES [dbo].[Appointment] ([Id])
GO
ALTER TABLE [dbo].[AppointmentHistory] ADD CONSTRAINT [PK_AppointmentHistory] PRIMARY KEY CLUSTERED  ([Guid]) ON [PRIMARY]
GO
