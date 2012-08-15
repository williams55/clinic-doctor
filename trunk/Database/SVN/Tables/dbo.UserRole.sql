CREATE TABLE [dbo].[UserRole]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[Username] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[RoleId] [int] NULL,
[IsDisabled] [bit] NOT NULL CONSTRAINT [DF_UserRole_IsDisabled] DEFAULT ((0)),
[CreateUser] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CreateDate] [datetime] NOT NULL CONSTRAINT [DF_UserRole_CreateDate] DEFAULT (getdate()),
[UpdateUser] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[UpdateDate] [datetime] NOT NULL CONSTRAINT [DF_UserRole_UpdateDate] DEFAULT (getdate())
) ON [PRIMARY]
ALTER TABLE [dbo].[UserRole] ADD
CONSTRAINT [FK_UserRole_Users] FOREIGN KEY ([Username]) REFERENCES [dbo].[Users] ([Username])
GO
ALTER TABLE [dbo].[UserRole] ADD CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UserRole] ADD CONSTRAINT [FK_UserRole_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([Id])
GO

EXEC sp_addextendedproperty N'MS_Description', N'Define user have what roles', 'SCHEMA', N'dbo', 'TABLE', N'UserRole', NULL, NULL
GO
