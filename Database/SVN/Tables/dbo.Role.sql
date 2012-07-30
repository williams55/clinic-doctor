CREATE TABLE [dbo].[Role]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Title] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Note] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IsLocked] [bit] NOT NULL CONSTRAINT [DF_Roles_IsLocked] DEFAULT ((0)),
[IsDisabled] [bit] NOT NULL CONSTRAINT [DF_Roles_IsDisabled] DEFAULT ((0)),
[CreateUser] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CreateDate] [datetime] NOT NULL CONSTRAINT [DF_Roles_CreateDate] DEFAULT (getdate()),
[UpdateUser] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[UpdateDate] [datetime] NOT NULL CONSTRAINT [DF_Roles_UpdateDate] DEFAULT (getdate())
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Role] ADD CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
EXEC sp_addextendedproperty N'MS_Description', N'Role of user', 'SCHEMA', N'dbo', 'TABLE', N'Role', NULL, NULL
GO
EXEC sp_addextendedproperty N'MS_Description', N'You can CRUD if it''s false (Admin, Manager...) These group is set by developer or database administrator', 'SCHEMA', N'dbo', 'TABLE', N'Role', 'COLUMN', N'IsLocked'
GO
