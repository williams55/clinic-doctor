CREATE TABLE [dbo].[RoleDetail]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[RoleId] [int] NULL,
[ScreenCode] [varchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Crud] [varchar] (4) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IsDisabled] [bit] NOT NULL CONSTRAINT [DF_RoleDetail_IsDisabled] DEFAULT ((0)),
[CreateUser] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CreateDate] [datetime] NOT NULL CONSTRAINT [DF_RoleDetail_CreateDate] DEFAULT (getdate()),
[UpdateUser] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[UpdateDate] [datetime] NOT NULL CONSTRAINT [DF_RoleDetail_UpdateDate] DEFAULT (getdate())
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[RoleDetail] ADD CONSTRAINT [PK_RoleDetail] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[RoleDetail] ADD CONSTRAINT [FK_RoleDetail_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[RoleDetail] ADD CONSTRAINT [FK_RoleDetail_Screen] FOREIGN KEY ([ScreenCode]) REFERENCES [dbo].[Screen] ([ScreenCode])
GO
EXEC sp_addextendedproperty N'MS_Description', N'Define detail for role', 'SCHEMA', N'dbo', 'TABLE', N'RoleDetail', NULL, NULL
GO
EXEC sp_addextendedproperty N'MS_Description', N'Define what action user can do.
C: Create
R: Read
U: Update
D: Delete', 'SCHEMA', N'dbo', 'TABLE', N'RoleDetail', 'COLUMN', N'Crud'
GO
EXEC sp_addextendedproperty N'MS_Description', N'What screen role can access', 'SCHEMA', N'dbo', 'TABLE', N'RoleDetail', 'COLUMN', N'ScreenCode'
GO
