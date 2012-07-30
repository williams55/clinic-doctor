CREATE TABLE [dbo].[Users]
(
[Id] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Username] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Title] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Firstname] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Lastname] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[DisplayName] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CellPhone] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Email] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Avatar] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Note] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[UserGroupId] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_Table_1_IsLocked] DEFAULT ((0)),
[IsFemale] [bit] NOT NULL CONSTRAINT [DF_Users_IsFemale] DEFAULT ((0)),
[IsDisabled] [bit] NOT NULL CONSTRAINT [DF_User_IsDisabled] DEFAULT ((0)),
[CreateUser] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CreateDate] [datetime] NOT NULL CONSTRAINT [DF_User_CreateDate] DEFAULT (getdate()),
[UpdateUser] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[UpdateDate] [datetime] NOT NULL CONSTRAINT [DF_User_UpdateDate] DEFAULT (getdate())
) ON [PRIMARY]
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
--trigger add role for user when create user
CREATE trigger [dbo].[add_rolegroup_user] 
on [dbo].[Users] after Insert
as
	SET NOCOUNT ON
	declare @UserId nvarchar(50),
			@GroupUserId nvarchar(20),
			@CreateUser nvarchar(200),
			@CreateDate datetime,
			@RoleId int
	select @UserId=i.Id,@GroupUserId=i.UserGroupId,@CreateUser=i.CreateUser,@CreateDate=i.CreateDate from Inserted i
	print(@UserId)
	declare cGroupRole cursor for
			select RoleId from GroupRole where GroupId=@GroupUserId and IsDisabled='false'
	open cGroupRole
	fetch next from cGroupRole into @RoleId
	while @@fetch_status=0
		begin
			if not exists(select Id from UserRole ur where ur.UserId=@UserId and ur.RoleId=@RoleId and IsDisabled='false' )
				begin
					
					insert into UserRole(UserId,RoleId,IsDisabled,CreateUser,CreateDate,UpdateUser,UpdateDate)
							values(@UserId,@RoleId,'false',@CreateUser,@CreateDate,@CreateUser,@CreateDate)				
				end
				fetch next from cGroupRole into @RoleId
		end
	close cGroupRole
	Deallocate cGroupRole
GO
ALTER TABLE [dbo].[Users] ADD CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users] ADD CONSTRAINT [IX_User] UNIQUE NONCLUSTERED  ([Username]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users] ADD CONSTRAINT [FK_Users_UserGroup] FOREIGN KEY ([UserGroupId]) REFERENCES [dbo].[UserGroup] ([Id])
GO
EXEC sp_addextendedproperty N'MS_Description', N'User login to System', 'SCHEMA', N'dbo', 'TABLE', N'Users', NULL, NULL
GO
EXEC sp_addextendedproperty N'MS_Description', N'Dr, Mr, Ms...', 'SCHEMA', N'dbo', 'TABLE', N'Users', 'COLUMN', N'Title'
GO
EXEC sp_addextendedproperty N'MS_Description', N'This user belongs what groups. It''s seperated by semi-comma', 'SCHEMA', N'dbo', 'TABLE', N'Users', 'COLUMN', N'UserGroupId'
GO
