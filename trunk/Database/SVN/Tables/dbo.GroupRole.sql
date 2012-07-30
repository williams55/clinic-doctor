CREATE TABLE [dbo].[GroupRole]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[GroupId] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[RoleId] [int] NULL,
[IsDisabled] [bit] NOT NULL CONSTRAINT [DF_GroupRole_IsDisabled] DEFAULT ((0)),
[CreateUser] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CreateDate] [datetime] NOT NULL CONSTRAINT [DF_GroupRole_CreateDate] DEFAULT (getdate()),
[UpdateUser] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[UpdateDate] [datetime] NOT NULL CONSTRAINT [DF_GroupRole_UpdateDate] DEFAULT (getdate())
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[GroupRole] ADD CONSTRAINT [PK_GroupRole] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[GroupRole] ADD CONSTRAINT [FK_GroupRole_UserGroup] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[UserGroup] ([Id])
GO
ALTER TABLE [dbo].[GroupRole] ADD CONSTRAINT [FK_GroupRole_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([Id])
GO
EXEC sp_addextendedproperty N'MS_Description', N'Define group have what roles', 'SCHEMA', N'dbo', 'TABLE', N'GroupRole', NULL, NULL
GO
