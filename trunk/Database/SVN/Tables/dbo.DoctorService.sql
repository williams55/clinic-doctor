CREATE TABLE [dbo].[DoctorService]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[DoctorId] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ServiceId] [int] NOT NULL,
[IsDisabled] [bit] NOT NULL CONSTRAINT [DF_DoctorService_IsDisabled] DEFAULT ((0)),
[CreateUser] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CreateDate] [datetime] NOT NULL CONSTRAINT [DF_DoctorService_CreateDate] DEFAULT (getdate()),
[UpdateUser] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[UpdateDate] [datetime] NOT NULL CONSTRAINT [DF_DoctorService_UpdateDate] DEFAULT (getdate())
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DoctorService] ADD CONSTRAINT [PK_DoctorService] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DoctorService] ADD CONSTRAINT [FK_DoctorService_Users] FOREIGN KEY ([DoctorId]) REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[DoctorService] ADD CONSTRAINT [FK_DoctorService_Services] FOREIGN KEY ([ServiceId]) REFERENCES [dbo].[Services] ([Id])
GO
