CREATE TABLE [dbo].[UserGroup]
(
[Id] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Title] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Note] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Roles] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IsLocked] [bit] NOT NULL CONSTRAINT [DF_UserGroup_IsLocked] DEFAULT ((0)),
[IsDisabled] [bit] NOT NULL CONSTRAINT [DF_UserGroup_IsDisabled] DEFAULT ((0)),
[CreateUser] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CreateDate] [datetime] NOT NULL CONSTRAINT [DF_UserGroup_CreateDate] DEFAULT (getdate()),
[UpdateUser] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[UpdateDate] [datetime] NOT NULL CONSTRAINT [DF_UserGroup_UpdateDate] DEFAULT (getdate())
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UserGroup] ADD CONSTRAINT [PK_UserGroup] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
EXEC sp_addextendedproperty N'MS_Description', N'User group: Frontdesk, Doctor, Supervisor, Manager, Admin...', 'SCHEMA', N'dbo', 'TABLE', N'UserGroup', NULL, NULL
GO
EXEC sp_addextendedproperty N'MS_Description', N'You can CRUD if it''s false (Admin, Manager...) These group is set by developer or database administrator', 'SCHEMA', N'dbo', 'TABLE', N'UserGroup', 'COLUMN', N'IsLocked'
GO
EXEC sp_addextendedproperty N'MS_Description', N'Roles of user group. A group can have many roles, they is seperated by semi-comma [;]
For example: CreateRoster;CreateAppointMent', 'SCHEMA', N'dbo', 'TABLE', N'UserGroup', 'COLUMN', N'Roles'
GO
