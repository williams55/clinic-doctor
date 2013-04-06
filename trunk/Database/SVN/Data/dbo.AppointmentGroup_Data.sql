SET IDENTITY_INSERT [dbo].[AppointmentGroup] ON
INSERT INTO [dbo].[AppointmentGroup] ([Id], [Title], [Note], [PriorityIndex], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate], [UnitId]) VALUES (1, N'First Group', N'First Group', 1, 0, NULL, '2012-07-08 11:41:13.873', NULL, '2012-07-08 11:41:13.873', 1)
INSERT INTO [dbo].[AppointmentGroup] ([Id], [Title], [Note], [PriorityIndex], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate], [UnitId]) VALUES (2, N'X-Ray', N'X-Ray', 1, 0, N'GOWU', '2012-07-08 11:52:16.027', N'GOWU', '2012-07-08 11:52:16.027', 2)
SET IDENTITY_INSERT [dbo].[AppointmentGroup] OFF
