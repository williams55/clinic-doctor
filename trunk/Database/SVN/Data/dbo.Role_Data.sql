SET IDENTITY_INSERT [dbo].[Role] ON
INSERT INTO [dbo].[Role] ([Id], [Title], [Note], [IsLocked], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (1, N'Roster Role', NULL, 0, 0, N'GOWU', '2012-08-29 22:27:43.950', N'GOWU', '2012-08-29 22:27:43.950')
SET IDENTITY_INSERT [dbo].[Role] OFF
SET IDENTITY_INSERT [dbo].[Role] ON
INSERT INTO [dbo].[Role] ([Id], [Title], [Note], [IsLocked], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (2, N'Test Role', NULL, 0, 1, N'GOWU', '2012-08-29 22:22:07.543', N'GOWU', '2012-10-10 14:03:32.447')
INSERT INTO [dbo].[Role] ([Id], [Title], [Note], [IsLocked], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (3, N'Test abc', NULL, 0, 0, N'GOWU', '2012-08-29 23:49:14.807', N'GOWU', '2012-10-07 20:45:18.657')
SET IDENTITY_INSERT [dbo].[Role] OFF
