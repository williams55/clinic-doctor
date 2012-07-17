/****** Object:  ForeignKey [FK_Users_UserGroup]    Script Date: 07/17/2012 10:59:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Users_UserGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_Users_UserGroup]
GO
/****** Object:  ForeignKey [FK_GroupRole_Role]    Script Date: 07/17/2012 10:59:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GroupRole_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupRole]'))
ALTER TABLE [dbo].[GroupRole] DROP CONSTRAINT [FK_GroupRole_Role]
GO
/****** Object:  ForeignKey [FK_GroupRole_UserGroup]    Script Date: 07/17/2012 10:59:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GroupRole_UserGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupRole]'))
ALTER TABLE [dbo].[GroupRole] DROP CONSTRAINT [FK_GroupRole_UserGroup]
GO
/****** Object:  Table [dbo].[GroupRole]    Script Date: 07/17/2012 10:59:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GroupRole]') AND type in (N'U'))
DROP TABLE [dbo].[GroupRole]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 07/17/2012 10:59:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
DROP TABLE [dbo].[Users]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 07/17/2012 10:59:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role]') AND type in (N'U'))
DROP TABLE [dbo].[Role]
GO
/****** Object:  Table [dbo].[UserGroup]    Script Date: 07/17/2012 10:59:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserGroup]') AND type in (N'U'))
DROP TABLE [dbo].[UserGroup]
GO
/****** Object:  Default [DF_UserGroup_IsLocked]    Script Date: 07/17/2012 10:59:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UserGroup_IsLocked]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserGroup]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserGroup_IsLocked]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserGroup] DROP CONSTRAINT [DF_UserGroup_IsLocked]
END


End
GO
/****** Object:  Default [DF_UserGroup_IsDisabled]    Script Date: 07/17/2012 10:59:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UserGroup_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserGroup]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserGroup_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserGroup] DROP CONSTRAINT [DF_UserGroup_IsDisabled]
END


End
GO
/****** Object:  Default [DF_UserGroup_CreateDate]    Script Date: 07/17/2012 10:59:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UserGroup_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserGroup]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserGroup_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserGroup] DROP CONSTRAINT [DF_UserGroup_CreateDate]
END


End
GO
/****** Object:  Default [DF_UserGroup_UpdateDate]    Script Date: 07/17/2012 10:59:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UserGroup_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserGroup]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserGroup_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserGroup] DROP CONSTRAINT [DF_UserGroup_UpdateDate]
END


End
GO
/****** Object:  Default [DF_Table_1_IsLocked]    Script Date: 07/17/2012 10:59:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Table_1_IsLocked]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table_1_IsLocked]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Users] DROP CONSTRAINT [DF_Table_1_IsLocked]
END


End
GO
/****** Object:  Default [DF_Users_IsFemale]    Script Date: 07/17/2012 10:59:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Users_IsFemale]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Users_IsFemale]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Users] DROP CONSTRAINT [DF_Users_IsFemale]
END


End
GO
/****** Object:  Default [DF_User_IsDisabled]    Script Date: 07/17/2012 10:59:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_User_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_User_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Users] DROP CONSTRAINT [DF_User_IsDisabled]
END


End
GO
/****** Object:  Default [DF_User_CreateDate]    Script Date: 07/17/2012 10:59:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_User_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_User_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Users] DROP CONSTRAINT [DF_User_CreateDate]
END


End
GO
/****** Object:  Default [DF_User_UpdateDate]    Script Date: 07/17/2012 10:59:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_User_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_User_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Users] DROP CONSTRAINT [DF_User_UpdateDate]
END


End
GO
/****** Object:  Default [DF_GroupRole_IsDisabled]    Script Date: 07/17/2012 10:59:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_GroupRole_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupRole]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GroupRole_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GroupRole] DROP CONSTRAINT [DF_GroupRole_IsDisabled]
END


End
GO
/****** Object:  Default [DF_GroupRole_CreateDate]    Script Date: 07/17/2012 10:59:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_GroupRole_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupRole]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GroupRole_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GroupRole] DROP CONSTRAINT [DF_GroupRole_CreateDate]
END


End
GO
/****** Object:  Default [DF_GroupRole_UpdateDate]    Script Date: 07/17/2012 10:59:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_GroupRole_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupRole]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GroupRole_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GroupRole] DROP CONSTRAINT [DF_GroupRole_UpdateDate]
END


End
GO
/****** Object:  Default [DF_Roles_IsLocked]    Script Date: 07/17/2012 10:59:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Roles_IsLocked]') AND parent_object_id = OBJECT_ID(N'[dbo].[Role]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Roles_IsLocked]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Role] DROP CONSTRAINT [DF_Roles_IsLocked]
END


End
GO
/****** Object:  Default [DF_Roles_IsDisabled]    Script Date: 07/17/2012 10:59:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Roles_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Role]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Roles_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Role] DROP CONSTRAINT [DF_Roles_IsDisabled]
END


End
GO
/****** Object:  Default [DF_Roles_CreateDate]    Script Date: 07/17/2012 10:59:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Roles_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Role]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Roles_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Role] DROP CONSTRAINT [DF_Roles_CreateDate]
END


End
GO
/****** Object:  Default [DF_Roles_UpdateDate]    Script Date: 07/17/2012 10:59:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Roles_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Role]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Roles_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Role] DROP CONSTRAINT [DF_Roles_UpdateDate]
END


End
GO
/****** Object:  Table [dbo].[UserGroup]    Script Date: 07/17/2012 10:59:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserGroup]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserGroup](
	[Id] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Title] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Note] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Roles] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsLocked] [bit] NOT NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_UserGroup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'UserGroup', N'COLUMN',N'Roles'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Roles of user group. A group can have many roles, they is seperated by semi-comma [;]
For example: CreateRoster;CreateAppointMent' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserGroup', @level2type=N'COLUMN',@level2name=N'Roles'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'UserGroup', N'COLUMN',N'IsLocked'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'You can CRUD if it''s false (Admin, Manager...) These group is set by developer or database administrator' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserGroup', @level2type=N'COLUMN',@level2name=N'IsLocked'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'UserGroup', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'User group: Frontdesk, Doctor, Supervisor, Manager, Admin...' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserGroup'
GO
INSERT [dbo].[UserGroup] ([Id], [Title], [Note], [Roles], [IsLocked], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'Doctor', N'Doctor', NULL, NULL, 0, 0, N'GOWU', CAST(0x0000A08F015B8134 AS DateTime), N'GOWU', CAST(0x0000A08F015B8134 AS DateTime))
/****** Object:  Table [dbo].[Role]    Script Date: 07/17/2012 10:59:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Note] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsLocked] [bit] NOT NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Role', N'COLUMN',N'IsLocked'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'You can CRUD if it''s false (Admin, Manager...) These group is set by developer or database administrator' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role', @level2type=N'COLUMN',@level2name=N'IsLocked'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Role', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Role of user' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role'
GO
SET IDENTITY_INSERT [dbo].[Role] ON
INSERT [dbo].[Role] ([Id], [Title], [Note], [IsLocked], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (1, N'Roster Role', NULL, 0, 0, N'GOWU', CAST(0x0000A08C015D8CC5 AS DateTime), N'GOWU', CAST(0x0000A08C015D8CC5 AS DateTime))
SET IDENTITY_INSERT [dbo].[Role] OFF
/****** Object:  Table [dbo].[Users]    Script Date: 07/17/2012 10:59:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Users](
	[Id] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Username] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Title] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Firstname] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Lastname] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DisplayName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CellPhone] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Email] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Avatar] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Note] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UserGroupId] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[IsFemale] [bit] NOT NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [IX_User] UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Users', N'COLUMN',N'Title'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Dr, Mr, Ms...' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users', @level2type=N'COLUMN',@level2name=N'Title'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Users', N'COLUMN',N'UserGroupId'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'This user belongs what groups. It''s seperated by semi-comma' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users', @level2type=N'COLUMN',@level2name=N'UserGroupId'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Users', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'User login to System' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users'
GO
INSERT [dbo].[Users] ([Id], [Username], [Title], [Firstname], [Lastname], [DisplayName], [CellPhone], [Email], [Avatar], [Note], [UserGroupId], [IsFemale], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'USR0001', N'GOWU', N'Dr', NULL, NULL, N'Vo Phat', NULL, N'votienphat@gmail.com', NULL, NULL, N'Doctor', 0, 0, NULL, CAST(0x0000A08800C86473 AS DateTime), NULL, CAST(0x0000A08800C86473 AS DateTime))
INSERT [dbo].[Users] ([Id], [Username], [Title], [Firstname], [Lastname], [DisplayName], [CellPhone], [Email], [Avatar], [Note], [UserGroupId], [IsFemale], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'USR0002', N'WIN7', N'Dr', NULL, NULL, N'Tuan', NULL, N'tuan@gmail.com', NULL, NULL, N'Doctor', 0, 0, N'GOWU', CAST(0x0000A08800C887F8 AS DateTime), N'GOWU', CAST(0x0000A08800C887F8 AS DateTime))
/****** Object:  Table [dbo].[GroupRole]    Script Date: 07/17/2012 10:59:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GroupRole]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GroupRole](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[GroupId] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[RoleId] [int] NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_GroupRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'GroupRole', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Define group have what roles' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GroupRole'
GO
/****** Object:  Default [DF_UserGroup_IsLocked]    Script Date: 07/17/2012 10:59:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UserGroup_IsLocked]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserGroup]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserGroup_IsLocked]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserGroup] ADD  CONSTRAINT [DF_UserGroup_IsLocked]  DEFAULT ((0)) FOR [IsLocked]
END


End
GO
/****** Object:  Default [DF_UserGroup_IsDisabled]    Script Date: 07/17/2012 10:59:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UserGroup_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserGroup]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserGroup_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserGroup] ADD  CONSTRAINT [DF_UserGroup_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_UserGroup_CreateDate]    Script Date: 07/17/2012 10:59:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UserGroup_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserGroup]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserGroup_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserGroup] ADD  CONSTRAINT [DF_UserGroup_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_UserGroup_UpdateDate]    Script Date: 07/17/2012 10:59:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UserGroup_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserGroup]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserGroup_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserGroup] ADD  CONSTRAINT [DF_UserGroup_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_Table_1_IsLocked]    Script Date: 07/17/2012 10:59:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Table_1_IsLocked]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table_1_IsLocked]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Table_1_IsLocked]  DEFAULT ((0)) FOR [UserGroupId]
END


End
GO
/****** Object:  Default [DF_Users_IsFemale]    Script Date: 07/17/2012 10:59:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Users_IsFemale]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Users_IsFemale]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsFemale]  DEFAULT ((0)) FOR [IsFemale]
END


End
GO
/****** Object:  Default [DF_User_IsDisabled]    Script Date: 07/17/2012 10:59:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_User_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_User_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_User_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_User_CreateDate]    Script Date: 07/17/2012 10:59:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_User_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_User_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_User_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_User_UpdateDate]    Script Date: 07/17/2012 10:59:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_User_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_User_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_User_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_GroupRole_IsDisabled]    Script Date: 07/17/2012 10:59:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_GroupRole_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupRole]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GroupRole_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GroupRole] ADD  CONSTRAINT [DF_GroupRole_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_GroupRole_CreateDate]    Script Date: 07/17/2012 10:59:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_GroupRole_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupRole]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GroupRole_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GroupRole] ADD  CONSTRAINT [DF_GroupRole_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_GroupRole_UpdateDate]    Script Date: 07/17/2012 10:59:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_GroupRole_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupRole]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GroupRole_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GroupRole] ADD  CONSTRAINT [DF_GroupRole_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_Roles_IsLocked]    Script Date: 07/17/2012 10:59:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Roles_IsLocked]') AND parent_object_id = OBJECT_ID(N'[dbo].[Role]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Roles_IsLocked]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Roles_IsLocked]  DEFAULT ((0)) FOR [IsLocked]
END


End
GO
/****** Object:  Default [DF_Roles_IsDisabled]    Script Date: 07/17/2012 10:59:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Roles_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Role]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Roles_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Roles_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_Roles_CreateDate]    Script Date: 07/17/2012 10:59:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Roles_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Role]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Roles_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Roles_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_Roles_UpdateDate]    Script Date: 07/17/2012 10:59:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Roles_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Role]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Roles_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Roles_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  ForeignKey [FK_Users_UserGroup]    Script Date: 07/17/2012 10:59:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Users_UserGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_UserGroup] FOREIGN KEY([UserGroupId])
REFERENCES [dbo].[UserGroup] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Users_UserGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_UserGroup]
GO
/****** Object:  ForeignKey [FK_GroupRole_Role]    Script Date: 07/17/2012 10:59:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GroupRole_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupRole]'))
ALTER TABLE [dbo].[GroupRole]  WITH CHECK ADD  CONSTRAINT [FK_GroupRole_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GroupRole_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupRole]'))
ALTER TABLE [dbo].[GroupRole] CHECK CONSTRAINT [FK_GroupRole_Role]
GO
/****** Object:  ForeignKey [FK_GroupRole_UserGroup]    Script Date: 07/17/2012 10:59:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GroupRole_UserGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupRole]'))
ALTER TABLE [dbo].[GroupRole]  WITH CHECK ADD  CONSTRAINT [FK_GroupRole_UserGroup] FOREIGN KEY([GroupId])
REFERENCES [dbo].[UserGroup] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GroupRole_UserGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupRole]'))
ALTER TABLE [dbo].[GroupRole] CHECK CONSTRAINT [FK_GroupRole_UserGroup]
GO
