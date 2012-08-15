CREATE TABLE [dbo].[RosterType]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Title] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[IsBooked] [bit] NOT NULL CONSTRAINT [DF_RosterType_IsBooked] DEFAULT ((1)),
[ColorCode] [varchar] (7) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Note] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IsDisabled] [bit] NOT NULL CONSTRAINT [DF_RosterType_IsDisabled] DEFAULT ((0)),
[CreateUser] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CreateDate] [datetime] NOT NULL CONSTRAINT [DF_RosterType_CreateDate] DEFAULT (getdate()),
[UpdateUser] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[UpdateDate] [datetime] NOT NULL CONSTRAINT [DF_RosterType_UpdateDate] DEFAULT (getdate())
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[RosterType] ADD CONSTRAINT [PK_RosterType] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[RosterType] ADD CONSTRAINT [IX_RosterType_Id_IsBooked] UNIQUE NONCLUSTERED  ([Id], [IsBooked]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[RosterType] ADD CONSTRAINT [IX_RosterType_Id_IsBooked_IsDisabled] UNIQUE NONCLUSTERED  ([Id], [IsBooked], [IsDisabled]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[RosterType] ADD CONSTRAINT [IX_RosterType_Id_IsDisabled] UNIQUE NONCLUSTERED  ([Id], [IsDisabled]) ON [PRIMARY]
GO
