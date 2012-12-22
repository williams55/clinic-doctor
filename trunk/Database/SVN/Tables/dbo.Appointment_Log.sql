CREATE TABLE [dbo].[Appointment_Log]
(
[LogId] [bigint] NOT NULL IDENTITY(1, 1),
[Id] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[UpdateUser] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[UpdateDate] [datetime] NOT NULL,
[LogMessage] [nvarchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Appointment_Log] ADD CONSTRAINT [PK_Appointment_Log] PRIMARY KEY CLUSTERED  ([LogId]) ON [PRIMARY]
GO
