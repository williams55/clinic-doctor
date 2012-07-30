CREATE TABLE [dbo].[DoctorRoom]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[DoctorId] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[RoomId] [int] NULL,
[Priority] [int] NOT NULL CONSTRAINT [DF_UnitRoom_Priority] DEFAULT ((0)),
[IsDisabled] [bit] NOT NULL CONSTRAINT [DF_UnitRoom_IsDisabled] DEFAULT ((0)),
[CreateUser] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CreateDate] [datetime] NOT NULL CONSTRAINT [DF_UnitRoom_CreateDate] DEFAULT (getdate()),
[UpdateUser] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[UpdateDate] [datetime] NOT NULL CONSTRAINT [DF_UnitRoom_UpdateDate] DEFAULT (getdate())
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DoctorRoom] ADD CONSTRAINT [PK_UnitRoom] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DoctorRoom] ADD CONSTRAINT [FK_DoctorRoom_User] FOREIGN KEY ([DoctorId]) REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[DoctorRoom] ADD CONSTRAINT [FK_DoctorRoom_Room] FOREIGN KEY ([RoomId]) REFERENCES [dbo].[Room] ([Id])
GO
