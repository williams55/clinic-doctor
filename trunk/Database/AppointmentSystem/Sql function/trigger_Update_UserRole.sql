--trigger add role for user when create user
alter trigger add_rolegroup_user 
on Users after Update
as
	SET NOCOUNT ON
	declare @UserId nvarchar(50),
			@GroupUserId nvarchar(20),
			@CreateUser nvarchar(200),
			@CreateDate datetime,
			@RoleId int
	select @UserId=i.Id,@GroupUserId=i.UserGroupId,@CreateUser=i.CreateUser,@CreateDate=i.CreateDate from Inserted i
	if not exists (select * from UserId where UserId=@UserId and GroupUserId=@GroupUserId)	
	begin
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
	end
go