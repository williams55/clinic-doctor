/****** Object:  ForeignKey [FK_Appointment_AppointmentGroup]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_AppointmentGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment] DROP CONSTRAINT [FK_Appointment_AppointmentGroup]
GO
/****** Object:  ForeignKey [FK_Appointment_Patient]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Patient]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment] DROP CONSTRAINT [FK_Appointment_Patient]
GO
/****** Object:  ForeignKey [FK_Appointment_Procedure]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Procedure]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment] DROP CONSTRAINT [FK_Appointment_Procedure]
GO
/****** Object:  ForeignKey [FK_Appointment_Room]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Room]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment] DROP CONSTRAINT [FK_Appointment_Room]
GO
/****** Object:  ForeignKey [FK_Appointment_Status]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Status]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment] DROP CONSTRAINT [FK_Appointment_Status]
GO
/****** Object:  ForeignKey [FK_AppointmentGroup_Unit]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AppointmentGroup_Unit]') AND parent_object_id = OBJECT_ID(N'[dbo].[AppointmentGroup]'))
ALTER TABLE [dbo].[AppointmentGroup] DROP CONSTRAINT [FK_AppointmentGroup_Unit]
GO
/****** Object:  ForeignKey [FK_DoctorRoom_Room]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoctorRoom_Room]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoom]'))
ALTER TABLE [dbo].[DoctorRoom] DROP CONSTRAINT [FK_DoctorRoom_Room]
GO
/****** Object:  ForeignKey [FK_DoctorRoom_User]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoctorRoom_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoom]'))
ALTER TABLE [dbo].[DoctorRoom] DROP CONSTRAINT [FK_DoctorRoom_User]
GO
/****** Object:  ForeignKey [FK_GroupRole_Role]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GroupRole_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupRole]'))
ALTER TABLE [dbo].[GroupRole] DROP CONSTRAINT [FK_GroupRole_Role]
GO
/****** Object:  ForeignKey [FK_GroupRole_UserGroup]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GroupRole_UserGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupRole]'))
ALTER TABLE [dbo].[GroupRole] DROP CONSTRAINT [FK_GroupRole_UserGroup]
GO
/****** Object:  ForeignKey [FK_RoleDetail_Role]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoleDetail_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleDetail]'))
ALTER TABLE [dbo].[RoleDetail] DROP CONSTRAINT [FK_RoleDetail_Role]
GO
/****** Object:  ForeignKey [FK_Room_Procedure]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Room_Procedure]') AND parent_object_id = OBJECT_ID(N'[dbo].[Room]'))
ALTER TABLE [dbo].[Room] DROP CONSTRAINT [FK_Room_Procedure]
GO
/****** Object:  ForeignKey [FK_UserRole_Role]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserRole_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole]'))
ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [FK_UserRole_Role]
GO
/****** Object:  ForeignKey [FK_UserRole_User]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserRole_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole]'))
ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [FK_UserRole_User]
GO
/****** Object:  Table [dbo].[DoctorRoom]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoom]') AND type in (N'U'))
DROP TABLE [dbo].[DoctorRoom]
GO
/****** Object:  Table [dbo].[Appointment]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Appointment]') AND type in (N'U'))
DROP TABLE [dbo].[Appointment]
GO
/****** Object:  Table [dbo].[AppointmentGroup]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AppointmentGroup]') AND type in (N'U'))
DROP TABLE [dbo].[AppointmentGroup]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserRole]') AND type in (N'U'))
DROP TABLE [dbo].[UserRole]
GO
/****** Object:  Table [dbo].[GroupRole]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GroupRole]') AND type in (N'U'))
DROP TABLE [dbo].[GroupRole]
GO
/****** Object:  Table [dbo].[RoleDetail]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleDetail]') AND type in (N'U'))
DROP TABLE [dbo].[RoleDetail]
GO
/****** Object:  Table [dbo].[Room]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Room]') AND type in (N'U'))
DROP TABLE [dbo].[Room]
GO
/****** Object:  Table [dbo].[Services]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Services]') AND type in (N'U'))
DROP TABLE [dbo].[Services]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Status]') AND type in (N'U'))
DROP TABLE [dbo].[Status]
GO
/****** Object:  Table [dbo].[Units]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Units]') AND type in (N'U'))
DROP TABLE [dbo].[Units]
GO
/****** Object:  Table [dbo].[UserGroup]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserGroup]') AND type in (N'U'))
DROP TABLE [dbo].[UserGroup]
GO
/****** Object:  Table [dbo].[LogHistory]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LogHistory]') AND type in (N'U'))
DROP TABLE [dbo].[LogHistory]
GO
/****** Object:  Table [dbo].[Patient]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Patient]') AND type in (N'U'))
DROP TABLE [dbo].[Patient]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role]') AND type in (N'U'))
DROP TABLE [dbo].[Role]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
DROP TABLE [dbo].[Users]
GO
/****** Object:  Default [DF_Appointment_IsComplete]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Appointment_IsComplete]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Appointment_IsComplete]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Appointment] DROP CONSTRAINT [DF_Appointment_IsComplete]
END


End
GO
/****** Object:  Default [DF_Appointment_IsDisabled]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Appointment_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Appointment_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Appointment] DROP CONSTRAINT [DF_Appointment_IsDisabled]
END


End
GO
/****** Object:  Default [DF_Appointment_CreateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Appointment_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Appointment_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Appointment] DROP CONSTRAINT [DF_Appointment_CreateDate]
END


End
GO
/****** Object:  Default [DF_Appointment_UpdateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Appointment_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Appointment_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Appointment] DROP CONSTRAINT [DF_Appointment_UpdateDate]
END


End
GO
/****** Object:  Default [DF_AppointmentGroup_IsDisabled]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_AppointmentGroup_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[AppointmentGroup]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_AppointmentGroup_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[AppointmentGroup] DROP CONSTRAINT [DF_AppointmentGroup_IsDisabled]
END


End
GO
/****** Object:  Default [DF_AppointmentGroup_CreateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_AppointmentGroup_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[AppointmentGroup]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_AppointmentGroup_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[AppointmentGroup] DROP CONSTRAINT [DF_AppointmentGroup_CreateDate]
END


End
GO
/****** Object:  Default [DF_AppointmentGroup_UpdateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_AppointmentGroup_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[AppointmentGroup]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_AppointmentGroup_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[AppointmentGroup] DROP CONSTRAINT [DF_AppointmentGroup_UpdateDate]
END


End
GO
/****** Object:  Default [DF_UnitRoom_Priority]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UnitRoom_Priority]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoom]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UnitRoom_Priority]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorRoom] DROP CONSTRAINT [DF_UnitRoom_Priority]
END


End
GO
/****** Object:  Default [DF_UnitRoom_IsDisabled]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UnitRoom_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoom]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UnitRoom_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorRoom] DROP CONSTRAINT [DF_UnitRoom_IsDisabled]
END


End
GO
/****** Object:  Default [DF_UnitRoom_CreateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UnitRoom_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoom]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UnitRoom_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorRoom] DROP CONSTRAINT [DF_UnitRoom_CreateDate]
END


End
GO
/****** Object:  Default [DF_UnitRoom_UpdateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UnitRoom_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoom]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UnitRoom_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorRoom] DROP CONSTRAINT [DF_UnitRoom_UpdateDate]
END


End
GO
/****** Object:  Default [DF_GroupRole_IsDisabled]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_GroupRole_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupRole]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GroupRole_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GroupRole] DROP CONSTRAINT [DF_GroupRole_IsDisabled]
END


End
GO
/****** Object:  Default [DF_GroupRole_CreateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_GroupRole_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupRole]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GroupRole_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GroupRole] DROP CONSTRAINT [DF_GroupRole_CreateDate]
END


End
GO
/****** Object:  Default [DF_GroupRole_UpdateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_GroupRole_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupRole]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GroupRole_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GroupRole] DROP CONSTRAINT [DF_GroupRole_UpdateDate]
END


End
GO
/****** Object:  Default [DF_Patient_IsFemale]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Patient_IsFemale]') AND parent_object_id = OBJECT_ID(N'[dbo].[Patient]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Patient_IsFemale]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Patient] DROP CONSTRAINT [DF_Patient_IsFemale]
END


End
GO
/****** Object:  Default [DF_Patient_IsDisabled]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Patient_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Patient]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Patient_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Patient] DROP CONSTRAINT [DF_Patient_IsDisabled]
END


End
GO
/****** Object:  Default [DF_Patient_CreateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Patient_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Patient]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Patient_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Patient] DROP CONSTRAINT [DF_Patient_CreateDate]
END


End
GO
/****** Object:  Default [DF_Patient_UpdateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Patient_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Patient]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Patient_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Patient] DROP CONSTRAINT [DF_Patient_UpdateDate]
END


End
GO
/****** Object:  Default [DF_Roles_IsLocked]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Roles_IsLocked]') AND parent_object_id = OBJECT_ID(N'[dbo].[Role]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Roles_IsLocked]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Role] DROP CONSTRAINT [DF_Roles_IsLocked]
END


End
GO
/****** Object:  Default [DF_Roles_IsDisabled]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Roles_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Role]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Roles_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Role] DROP CONSTRAINT [DF_Roles_IsDisabled]
END


End
GO
/****** Object:  Default [DF_Roles_CreateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Roles_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Role]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Roles_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Role] DROP CONSTRAINT [DF_Roles_CreateDate]
END


End
GO
/****** Object:  Default [DF_Roles_UpdateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Roles_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Role]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Roles_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Role] DROP CONSTRAINT [DF_Roles_UpdateDate]
END


End
GO
/****** Object:  Default [DF_RoleDetail_IsDisabled]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RoleDetail_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleDetail]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RoleDetail_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RoleDetail] DROP CONSTRAINT [DF_RoleDetail_IsDisabled]
END


End
GO
/****** Object:  Default [DF_RoleDetail_CreateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RoleDetail_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleDetail]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RoleDetail_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RoleDetail] DROP CONSTRAINT [DF_RoleDetail_CreateDate]
END


End
GO
/****** Object:  Default [DF_RoleDetail_UpdateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RoleDetail_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleDetail]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RoleDetail_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RoleDetail] DROP CONSTRAINT [DF_RoleDetail_UpdateDate]
END


End
GO
/****** Object:  Default [DF_Room_IsDisabled]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Room_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Room]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Room_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Room] DROP CONSTRAINT [DF_Room_IsDisabled]
END


End
GO
/****** Object:  Default [DF_Room_CreateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Room_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Room]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Room_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Room] DROP CONSTRAINT [DF_Room_CreateDate]
END


End
GO
/****** Object:  Default [DF_Room_UpdateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Room_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Room]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Room_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Room] DROP CONSTRAINT [DF_Room_UpdateDate]
END


End
GO
/****** Object:  Default [DF_Procedure_IsDisabled]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Procedure_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Services]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Procedure_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Services] DROP CONSTRAINT [DF_Procedure_IsDisabled]
END


End
GO
/****** Object:  Default [DF_Procedure_CreateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Procedure_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Services]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Procedure_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Services] DROP CONSTRAINT [DF_Procedure_CreateDate]
END


End
GO
/****** Object:  Default [DF_Procedure_UpdateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Procedure_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Services]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Procedure_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Services] DROP CONSTRAINT [DF_Procedure_UpdateDate]
END


End
GO
/****** Object:  Default [DF_Status_PriorityIndex_1]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Status_PriorityIndex_1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Status]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Status_PriorityIndex_1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Status] DROP CONSTRAINT [DF_Status_PriorityIndex_1]
END


End
GO
/****** Object:  Default [DF_Status_IsDisabled]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Status_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Status]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Status_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Status] DROP CONSTRAINT [DF_Status_IsDisabled]
END


End
GO
/****** Object:  Default [DF_Status_CreateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Status_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Status]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Status_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Status] DROP CONSTRAINT [DF_Status_CreateDate]
END


End
GO
/****** Object:  Default [DF_Status_UpdateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Status_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Status]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Status_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Status] DROP CONSTRAINT [DF_Status_UpdateDate]
END


End
GO
/****** Object:  Default [DF_Unit_Users]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Unit_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Units]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Unit_Users]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Units] DROP CONSTRAINT [DF_Unit_Users]
END


End
GO
/****** Object:  Default [DF_Unit_IsDisabled]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Unit_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Units]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Unit_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Units] DROP CONSTRAINT [DF_Unit_IsDisabled]
END


End
GO
/****** Object:  Default [DF_Unit_CreateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Unit_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Units]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Unit_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Units] DROP CONSTRAINT [DF_Unit_CreateDate]
END


End
GO
/****** Object:  Default [DF_Unit_UpdateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Unit_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Units]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Unit_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Units] DROP CONSTRAINT [DF_Unit_UpdateDate]
END


End
GO
/****** Object:  Default [DF_UserGroup_IsLocked]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UserGroup_IsLocked]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserGroup]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserGroup_IsLocked]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserGroup] DROP CONSTRAINT [DF_UserGroup_IsLocked]
END


End
GO
/****** Object:  Default [DF_UserGroup_IsDisabled]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UserGroup_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserGroup]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserGroup_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserGroup] DROP CONSTRAINT [DF_UserGroup_IsDisabled]
END


End
GO
/****** Object:  Default [DF_UserGroup_CreateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UserGroup_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserGroup]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserGroup_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserGroup] DROP CONSTRAINT [DF_UserGroup_CreateDate]
END


End
GO
/****** Object:  Default [DF_UserGroup_UpdateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UserGroup_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserGroup]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserGroup_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserGroup] DROP CONSTRAINT [DF_UserGroup_UpdateDate]
END


End
GO
/****** Object:  Default [DF_UserRole_IsDisabled]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UserRole_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserRole_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [DF_UserRole_IsDisabled]
END


End
GO
/****** Object:  Default [DF_UserRole_CreateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UserRole_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserRole_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [DF_UserRole_CreateDate]
END


End
GO
/****** Object:  Default [DF_UserRole_UpdateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UserRole_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserRole_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [DF_UserRole_UpdateDate]
END


End
GO
/****** Object:  Default [DF_Table_1_IsLocked]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Table_1_IsLocked]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table_1_IsLocked]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Users] DROP CONSTRAINT [DF_Table_1_IsLocked]
END


End
GO
/****** Object:  Default [DF_User_IsDisabled]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_User_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_User_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Users] DROP CONSTRAINT [DF_User_IsDisabled]
END


End
GO
/****** Object:  Default [DF_User_CreateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_User_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_User_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Users] DROP CONSTRAINT [DF_User_CreateDate]
END


End
GO
/****** Object:  Default [DF_User_UpdateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_User_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_User_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Users] DROP CONSTRAINT [DF_User_UpdateDate]
END


End
GO
/****** Object:  Table [dbo].[Users]    Script Date: 07/05/2012 19:24:28 ******/
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
	[DisplayName] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Email] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Note] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UserGroupId] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
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
/****** Object:  Table [dbo].[Role]    Script Date: 07/05/2012 19:24:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Role](
	[Id] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
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
/****** Object:  Table [dbo].[Patient]    Script Date: 07/05/2012 19:24:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Patient]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Patient](
	[Id] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[FirstName] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[LastName] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Address] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[HomePhone] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[WorkPhone] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CellPhone] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Birthdate] [datetime] NULL,
	[IsFemale] [bit] NOT NULL,
	[Title] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Note] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Patient] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[LogHistory]    Script Date: 07/05/2012 19:24:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LogHistory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[LogHistory](
	[Id] [uniqueidentifier] NOT NULL,
	[ActionId] [int] NULL,
	[ActionDescription] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreateDate] [datetime] NULL,
	[IP] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SystemID] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Browser] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OS] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'LogHistory', N'COLUMN',N'ActionId'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Mã thao tác. Được định nghĩa chi tiết trong constants của code
Nạp tiền, Tạo doanh nghiệp, Sửa thông tin doanh nghiệp...' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LogHistory', @level2type=N'COLUMN',@level2name=N'ActionId'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'LogHistory', N'COLUMN',N'ActionDescription'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nội dung thao tác' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LogHistory', @level2type=N'COLUMN',@level2name=N'ActionDescription'
GO
/****** Object:  Table [dbo].[UserGroup]    Script Date: 07/05/2012 19:24:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserGroup]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserGroup](
	[Id] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Title] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
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
/****** Object:  Table [dbo].[Units]    Script Date: 07/05/2012 19:24:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Units]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Units](
	[Id] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Title] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Note] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Users] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Unit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Units', N'COLUMN',N'Title'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Dr, Mr, Ms...' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Units', @level2type=N'COLUMN',@level2name=N'Title'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Units', N'COLUMN',N'Users'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'This unit include what user.
It''s seperated by semi-comma [;]
Ex: DrSeuss;DrGreen' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Units', @level2type=N'COLUMN',@level2name=N'Users'
GO
/****** Object:  Table [dbo].[Status]    Script Date: 07/05/2012 19:24:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Status]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Status](
	[Id] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Title] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ColorCode] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PriorityIndex] [int] NOT NULL,
	[Note] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Services]    Script Date: 07/05/2012 19:24:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Services]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Services](
	[Id] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Title] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Note] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Procedure] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Services', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedures, Services...' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Services'
GO
/****** Object:  Table [dbo].[Room]    Script Date: 07/05/2012 19:24:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Room]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Room](
	[Id] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Title] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Note] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ServicesId] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Room', N'COLUMN',N'ServicesId'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'A room can have many procedures. They are seperated by semi-comma [;]
For example: XRay;MRI' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Room', @level2type=N'COLUMN',@level2name=N'ServicesId'
GO
/****** Object:  Table [dbo].[RoleDetail]    Script Date: 07/05/2012 19:24:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleDetail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RoleDetail](
	[Id] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[RoleId] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Screen] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Crud] [varchar](4) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_RoleDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'RoleDetail', N'COLUMN',N'Screen'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'What screen role can access' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleDetail', @level2type=N'COLUMN',@level2name=N'Screen'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'RoleDetail', N'COLUMN',N'Crud'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Define what action user can do.
C: Create
R: Read
U: Update
D: Delete' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleDetail', @level2type=N'COLUMN',@level2name=N'Crud'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'RoleDetail', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Define detail for role' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleDetail'
GO
/****** Object:  Table [dbo].[GroupRole]    Script Date: 07/05/2012 19:24:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GroupRole]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GroupRole](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[GroupId] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[RoleId] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
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
/****** Object:  Table [dbo].[UserRole]    Script Date: 07/05/2012 19:24:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserRole]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserRole](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[RoleId] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'UserRole', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Define user have what roles' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserRole'
GO
/****** Object:  Table [dbo].[AppointmentGroup]    Script Date: 07/05/2012 19:24:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AppointmentGroup]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AppointmentGroup](
	[Id] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Title] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Note] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UpdateDate] [datetime] NOT NULL,
	[UnitId] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_AppointmentGroup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'AppointmentGroup', N'COLUMN',N'UnitId'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Define current unit belongs to what tab.
It''s seperated by semi-comma [;]
Ex: 1stFloor;2ndFloor' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AppointmentGroup', @level2type=N'COLUMN',@level2name=N'UnitId'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'AppointmentGroup', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'This table group follow floor or something like that. For example, 1st Floor is a group and there are some staffs [Doctors, ProcedureGroup]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AppointmentGroup'
GO
/****** Object:  Table [dbo].[Appointment]    Script Date: 07/05/2012 19:24:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Appointment]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Appointment](
	[Id] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PatientId] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[UserName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[RoomId] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ServicesId] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[StatusId] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AppointmentGroupId] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Note] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[IsComplete] [bit] NOT NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Appointment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Appointment', N'COLUMN',N'ServicesId'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'What do patient wanna be served' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Appointment', @level2type=N'COLUMN',@level2name=N'ServicesId'
GO
/****** Object:  Table [dbo].[DoctorRoom]    Script Date: 07/05/2012 19:24:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoom]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DoctorRoom](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DoctorId] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[RoomId] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Priority] [int] NOT NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_UnitRoom] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Default [DF_Appointment_IsComplete]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Appointment_IsComplete]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Appointment_IsComplete]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Appointment] ADD  CONSTRAINT [DF_Appointment_IsComplete]  DEFAULT ((0)) FOR [IsComplete]
END


End
GO
/****** Object:  Default [DF_Appointment_IsDisabled]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Appointment_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Appointment_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Appointment] ADD  CONSTRAINT [DF_Appointment_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_Appointment_CreateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Appointment_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Appointment_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Appointment] ADD  CONSTRAINT [DF_Appointment_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_Appointment_UpdateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Appointment_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Appointment_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Appointment] ADD  CONSTRAINT [DF_Appointment_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_AppointmentGroup_IsDisabled]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_AppointmentGroup_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[AppointmentGroup]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_AppointmentGroup_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[AppointmentGroup] ADD  CONSTRAINT [DF_AppointmentGroup_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_AppointmentGroup_CreateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_AppointmentGroup_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[AppointmentGroup]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_AppointmentGroup_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[AppointmentGroup] ADD  CONSTRAINT [DF_AppointmentGroup_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_AppointmentGroup_UpdateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_AppointmentGroup_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[AppointmentGroup]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_AppointmentGroup_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[AppointmentGroup] ADD  CONSTRAINT [DF_AppointmentGroup_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_UnitRoom_Priority]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UnitRoom_Priority]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoom]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UnitRoom_Priority]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorRoom] ADD  CONSTRAINT [DF_UnitRoom_Priority]  DEFAULT ((0)) FOR [Priority]
END


End
GO
/****** Object:  Default [DF_UnitRoom_IsDisabled]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UnitRoom_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoom]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UnitRoom_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorRoom] ADD  CONSTRAINT [DF_UnitRoom_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_UnitRoom_CreateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UnitRoom_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoom]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UnitRoom_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorRoom] ADD  CONSTRAINT [DF_UnitRoom_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_UnitRoom_UpdateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UnitRoom_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoom]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UnitRoom_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorRoom] ADD  CONSTRAINT [DF_UnitRoom_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_GroupRole_IsDisabled]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_GroupRole_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupRole]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GroupRole_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GroupRole] ADD  CONSTRAINT [DF_GroupRole_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_GroupRole_CreateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_GroupRole_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupRole]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GroupRole_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GroupRole] ADD  CONSTRAINT [DF_GroupRole_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_GroupRole_UpdateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_GroupRole_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupRole]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GroupRole_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GroupRole] ADD  CONSTRAINT [DF_GroupRole_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_Patient_IsFemale]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Patient_IsFemale]') AND parent_object_id = OBJECT_ID(N'[dbo].[Patient]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Patient_IsFemale]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Patient] ADD  CONSTRAINT [DF_Patient_IsFemale]  DEFAULT ((0)) FOR [IsFemale]
END


End
GO
/****** Object:  Default [DF_Patient_IsDisabled]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Patient_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Patient]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Patient_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Patient] ADD  CONSTRAINT [DF_Patient_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_Patient_CreateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Patient_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Patient]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Patient_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Patient] ADD  CONSTRAINT [DF_Patient_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_Patient_UpdateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Patient_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Patient]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Patient_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Patient] ADD  CONSTRAINT [DF_Patient_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_Roles_IsLocked]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Roles_IsLocked]') AND parent_object_id = OBJECT_ID(N'[dbo].[Role]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Roles_IsLocked]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Roles_IsLocked]  DEFAULT ((0)) FOR [IsLocked]
END


End
GO
/****** Object:  Default [DF_Roles_IsDisabled]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Roles_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Role]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Roles_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Roles_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_Roles_CreateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Roles_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Role]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Roles_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Roles_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_Roles_UpdateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Roles_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Role]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Roles_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Roles_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_RoleDetail_IsDisabled]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RoleDetail_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleDetail]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RoleDetail_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RoleDetail] ADD  CONSTRAINT [DF_RoleDetail_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_RoleDetail_CreateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RoleDetail_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleDetail]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RoleDetail_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RoleDetail] ADD  CONSTRAINT [DF_RoleDetail_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_RoleDetail_UpdateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RoleDetail_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleDetail]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RoleDetail_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RoleDetail] ADD  CONSTRAINT [DF_RoleDetail_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_Room_IsDisabled]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Room_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Room]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Room_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Room] ADD  CONSTRAINT [DF_Room_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_Room_CreateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Room_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Room]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Room_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Room] ADD  CONSTRAINT [DF_Room_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_Room_UpdateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Room_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Room]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Room_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Room] ADD  CONSTRAINT [DF_Room_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_Procedure_IsDisabled]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Procedure_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Services]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Procedure_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Services] ADD  CONSTRAINT [DF_Procedure_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_Procedure_CreateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Procedure_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Services]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Procedure_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Services] ADD  CONSTRAINT [DF_Procedure_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_Procedure_UpdateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Procedure_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Services]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Procedure_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Services] ADD  CONSTRAINT [DF_Procedure_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_Status_PriorityIndex_1]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Status_PriorityIndex_1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Status]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Status_PriorityIndex_1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Status] ADD  CONSTRAINT [DF_Status_PriorityIndex_1]  DEFAULT ((1)) FOR [PriorityIndex]
END


End
GO
/****** Object:  Default [DF_Status_IsDisabled]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Status_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Status]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Status_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Status] ADD  CONSTRAINT [DF_Status_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_Status_CreateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Status_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Status]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Status_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Status] ADD  CONSTRAINT [DF_Status_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_Status_UpdateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Status_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Status]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Status_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Status] ADD  CONSTRAINT [DF_Status_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_Unit_Users]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Unit_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Units]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Unit_Users]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Units] ADD  CONSTRAINT [DF_Unit_Users]  DEFAULT ((0)) FOR [Users]
END


End
GO
/****** Object:  Default [DF_Unit_IsDisabled]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Unit_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Units]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Unit_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Units] ADD  CONSTRAINT [DF_Unit_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_Unit_CreateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Unit_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Units]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Unit_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Units] ADD  CONSTRAINT [DF_Unit_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_Unit_UpdateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Unit_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Units]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Unit_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Units] ADD  CONSTRAINT [DF_Unit_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_UserGroup_IsLocked]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UserGroup_IsLocked]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserGroup]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserGroup_IsLocked]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserGroup] ADD  CONSTRAINT [DF_UserGroup_IsLocked]  DEFAULT ((0)) FOR [IsLocked]
END


End
GO
/****** Object:  Default [DF_UserGroup_IsDisabled]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UserGroup_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserGroup]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserGroup_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserGroup] ADD  CONSTRAINT [DF_UserGroup_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_UserGroup_CreateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UserGroup_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserGroup]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserGroup_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserGroup] ADD  CONSTRAINT [DF_UserGroup_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_UserGroup_UpdateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UserGroup_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserGroup]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserGroup_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserGroup] ADD  CONSTRAINT [DF_UserGroup_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_UserRole_IsDisabled]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UserRole_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserRole_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserRole] ADD  CONSTRAINT [DF_UserRole_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_UserRole_CreateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UserRole_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserRole_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserRole] ADD  CONSTRAINT [DF_UserRole_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_UserRole_UpdateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UserRole_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserRole_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserRole] ADD  CONSTRAINT [DF_UserRole_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_Table_1_IsLocked]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Table_1_IsLocked]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table_1_IsLocked]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Table_1_IsLocked]  DEFAULT ((0)) FOR [UserGroupId]
END


End
GO
/****** Object:  Default [DF_User_IsDisabled]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_User_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_User_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_User_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_User_CreateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_User_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_User_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_User_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_User_UpdateDate]    Script Date: 07/05/2012 19:24:28 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_User_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_User_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_User_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  ForeignKey [FK_Appointment_AppointmentGroup]    Script Date: 07/05/2012 19:24:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_AppointmentGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_AppointmentGroup] FOREIGN KEY([AppointmentGroupId])
REFERENCES [dbo].[AppointmentGroup] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_AppointmentGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_AppointmentGroup]
GO
/****** Object:  ForeignKey [FK_Appointment_Patient]    Script Date: 07/05/2012 19:24:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Patient]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Patient] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patient] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Patient]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_Patient]
GO
/****** Object:  ForeignKey [FK_Appointment_Procedure]    Script Date: 07/05/2012 19:24:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Procedure]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Procedure] FOREIGN KEY([ServicesId])
REFERENCES [dbo].[Services] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Procedure]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_Procedure]
GO
/****** Object:  ForeignKey [FK_Appointment_Room]    Script Date: 07/05/2012 19:24:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Room]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Room] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Room]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_Room]
GO
/****** Object:  ForeignKey [FK_Appointment_Status]    Script Date: 07/05/2012 19:24:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Status]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Status] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Status] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Status]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_Status]
GO
/****** Object:  ForeignKey [FK_AppointmentGroup_Unit]    Script Date: 07/05/2012 19:24:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AppointmentGroup_Unit]') AND parent_object_id = OBJECT_ID(N'[dbo].[AppointmentGroup]'))
ALTER TABLE [dbo].[AppointmentGroup]  WITH CHECK ADD  CONSTRAINT [FK_AppointmentGroup_Unit] FOREIGN KEY([UnitId])
REFERENCES [dbo].[Units] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AppointmentGroup_Unit]') AND parent_object_id = OBJECT_ID(N'[dbo].[AppointmentGroup]'))
ALTER TABLE [dbo].[AppointmentGroup] CHECK CONSTRAINT [FK_AppointmentGroup_Unit]
GO
/****** Object:  ForeignKey [FK_DoctorRoom_Room]    Script Date: 07/05/2012 19:24:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoctorRoom_Room]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoom]'))
ALTER TABLE [dbo].[DoctorRoom]  WITH CHECK ADD  CONSTRAINT [FK_DoctorRoom_Room] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoctorRoom_Room]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoom]'))
ALTER TABLE [dbo].[DoctorRoom] CHECK CONSTRAINT [FK_DoctorRoom_Room]
GO
/****** Object:  ForeignKey [FK_DoctorRoom_User]    Script Date: 07/05/2012 19:24:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoctorRoom_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoom]'))
ALTER TABLE [dbo].[DoctorRoom]  WITH CHECK ADD  CONSTRAINT [FK_DoctorRoom_User] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[Users] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoctorRoom_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoom]'))
ALTER TABLE [dbo].[DoctorRoom] CHECK CONSTRAINT [FK_DoctorRoom_User]
GO
/****** Object:  ForeignKey [FK_GroupRole_Role]    Script Date: 07/05/2012 19:24:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GroupRole_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupRole]'))
ALTER TABLE [dbo].[GroupRole]  WITH CHECK ADD  CONSTRAINT [FK_GroupRole_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GroupRole_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupRole]'))
ALTER TABLE [dbo].[GroupRole] CHECK CONSTRAINT [FK_GroupRole_Role]
GO
/****** Object:  ForeignKey [FK_GroupRole_UserGroup]    Script Date: 07/05/2012 19:24:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GroupRole_UserGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupRole]'))
ALTER TABLE [dbo].[GroupRole]  WITH CHECK ADD  CONSTRAINT [FK_GroupRole_UserGroup] FOREIGN KEY([GroupId])
REFERENCES [dbo].[UserGroup] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GroupRole_UserGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupRole]'))
ALTER TABLE [dbo].[GroupRole] CHECK CONSTRAINT [FK_GroupRole_UserGroup]
GO
/****** Object:  ForeignKey [FK_RoleDetail_Role]    Script Date: 07/05/2012 19:24:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoleDetail_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleDetail]'))
ALTER TABLE [dbo].[RoleDetail]  WITH CHECK ADD  CONSTRAINT [FK_RoleDetail_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoleDetail_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleDetail]'))
ALTER TABLE [dbo].[RoleDetail] CHECK CONSTRAINT [FK_RoleDetail_Role]
GO
/****** Object:  ForeignKey [FK_Room_Procedure]    Script Date: 07/05/2012 19:24:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Room_Procedure]') AND parent_object_id = OBJECT_ID(N'[dbo].[Room]'))
ALTER TABLE [dbo].[Room]  WITH CHECK ADD  CONSTRAINT [FK_Room_Procedure] FOREIGN KEY([ServicesId])
REFERENCES [dbo].[Services] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Room_Procedure]') AND parent_object_id = OBJECT_ID(N'[dbo].[Room]'))
ALTER TABLE [dbo].[Room] CHECK CONSTRAINT [FK_Room_Procedure]
GO
/****** Object:  ForeignKey [FK_UserRole_Role]    Script Date: 07/05/2012 19:24:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserRole_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole]'))
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserRole_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole]'))
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_Role]
GO
/****** Object:  ForeignKey [FK_UserRole_User]    Script Date: 07/05/2012 19:24:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserRole_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole]'))
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserRole_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole]'))
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_User]
GO
