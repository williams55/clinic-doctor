CREATE TABLE [dbo].[DoctorRoom]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[Username] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[RoomId] [int] NOT NULL,
[Priority] [int] NOT NULL CONSTRAINT [DF_UnitRoom_Priority] DEFAULT ((0)),
[IsDisabled] [bit] NOT NULL CONSTRAINT [DF_UnitRoom_IsDisabled] DEFAULT ((0)),
[CreateUser] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CreateDate] [datetime] NOT NULL CONSTRAINT [DF_UnitRoom_CreateDate] DEFAULT (getdate()),
[UpdateUser] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[UpdateDate] [datetime] NOT NULL CONSTRAINT [DF_UnitRoom_UpdateDate] DEFAULT (getdate())
) ON [PRIMARY]
ALTER TABLE [dbo].[DoctorRoom] WITH NOCHECK ADD
CONSTRAINT [FK_DoctorRoom_Room] FOREIGN KEY ([RoomId]) REFERENCES [dbo].[Room] ([Id])
ALTER TABLE [dbo].[DoctorRoom] ADD
CONSTRAINT [FK_DoctorRoom_Users] FOREIGN KEY ([Username]) REFERENCES [dbo].[Users] ([Username])
GO
ALTER TABLE [dbo].[DoctorRoom] ADD CONSTRAINT [PK_UnitRoom] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
