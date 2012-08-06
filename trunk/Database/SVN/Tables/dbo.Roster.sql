CREATE TABLE [dbo].[Roster]
(
[Id] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[DoctorId] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[RoomId] [int] NULL,
[RosterTypeId] [int] NOT NULL,
[StartTime] [datetime] NOT NULL,
[EndTime] [datetime] NOT NULL,
[Note] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IsDisabled] [bit] NOT NULL CONSTRAINT [DF_Roster_IsDisabled] DEFAULT ((0)),
[CreateUser] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CreateDate] [datetime] NOT NULL CONSTRAINT [DF_Roster_CreateDate] DEFAULT (getdate()),
[UpdateUser] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[UpdateDate] [datetime] NOT NULL CONSTRAINT [DF_Roster_UpdateDate] DEFAULT (getdate())
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Roster] ADD CONSTRAINT [PK_Roster] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Roster] ADD CONSTRAINT [FK_Roster_Users] FOREIGN KEY ([DoctorId]) REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Roster] ADD CONSTRAINT [FK_Roster_Room] FOREIGN KEY ([RoomId]) REFERENCES [dbo].[Room] ([Id])
GO
ALTER TABLE [dbo].[Roster] ADD CONSTRAINT [FK_Roster_RosterType] FOREIGN KEY ([RosterTypeId]) REFERENCES [dbo].[RosterType] ([Id])
GO