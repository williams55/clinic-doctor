/****** Object:  ForeignKey [FK_Appointment_AppointmentGroup]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_AppointmentGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment] DROP CONSTRAINT [FK_Appointment_AppointmentGroup]
GO
/****** Object:  ForeignKey [FK_Appointment_Patient]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Patient]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment] DROP CONSTRAINT [FK_Appointment_Patient]
GO
/****** Object:  ForeignKey [FK_Appointment_Procedure]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Procedure]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment] DROP CONSTRAINT [FK_Appointment_Procedure]
GO
/****** Object:  ForeignKey [FK_Appointment_Room]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Room]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment] DROP CONSTRAINT [FK_Appointment_Room]
GO
/****** Object:  ForeignKey [FK_Appointment_Status]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Status]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment] DROP CONSTRAINT [FK_Appointment_Status]
GO
/****** Object:  ForeignKey [FK_Appointment_Users]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment] DROP CONSTRAINT [FK_Appointment_Users]
GO
/****** Object:  ForeignKey [FK_AppointmentGroup_Unit]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AppointmentGroup_Unit]') AND parent_object_id = OBJECT_ID(N'[dbo].[AppointmentGroup]'))
ALTER TABLE [dbo].[AppointmentGroup] DROP CONSTRAINT [FK_AppointmentGroup_Unit]
GO
/****** Object:  ForeignKey [FK_DoctorRoom_Room]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoctorRoom_Room]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoom]'))
ALTER TABLE [dbo].[DoctorRoom] DROP CONSTRAINT [FK_DoctorRoom_Room]
GO
/****** Object:  ForeignKey [FK_DoctorRoom_User]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoctorRoom_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoom]'))
ALTER TABLE [dbo].[DoctorRoom] DROP CONSTRAINT [FK_DoctorRoom_User]
GO
/****** Object:  ForeignKey [FK_DoctorService_Services]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoctorService_Services]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorService]'))
ALTER TABLE [dbo].[DoctorService] DROP CONSTRAINT [FK_DoctorService_Services]
GO
/****** Object:  ForeignKey [FK_DoctorService_Users]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoctorService_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorService]'))
ALTER TABLE [dbo].[DoctorService] DROP CONSTRAINT [FK_DoctorService_Users]
GO
/****** Object:  ForeignKey [FK_GroupRole_Role]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GroupRole_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupRole]'))
ALTER TABLE [dbo].[GroupRole] DROP CONSTRAINT [FK_GroupRole_Role]
GO
/****** Object:  ForeignKey [FK_GroupRole_UserGroup]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GroupRole_UserGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupRole]'))
ALTER TABLE [dbo].[GroupRole] DROP CONSTRAINT [FK_GroupRole_UserGroup]
GO
/****** Object:  ForeignKey [FK_RoleDetail_Role]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoleDetail_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleDetail]'))
ALTER TABLE [dbo].[RoleDetail] DROP CONSTRAINT [FK_RoleDetail_Role]
GO
/****** Object:  ForeignKey [FK_RoleDetail_Screen]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoleDetail_Screen]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleDetail]'))
ALTER TABLE [dbo].[RoleDetail] DROP CONSTRAINT [FK_RoleDetail_Screen]
GO
/****** Object:  ForeignKey [FK_Room_Procedure]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Room_Procedure]') AND parent_object_id = OBJECT_ID(N'[dbo].[Room]'))
ALTER TABLE [dbo].[Room] DROP CONSTRAINT [FK_Room_Procedure]
GO
/****** Object:  ForeignKey [FK_Roster_Room]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Roster_Room]') AND parent_object_id = OBJECT_ID(N'[dbo].[Roster]'))
ALTER TABLE [dbo].[Roster] DROP CONSTRAINT [FK_Roster_Room]
GO
/****** Object:  ForeignKey [FK_Roster_RosterType]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Roster_RosterType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Roster]'))
ALTER TABLE [dbo].[Roster] DROP CONSTRAINT [FK_Roster_RosterType]
GO
/****** Object:  ForeignKey [FK_Roster_Users]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Roster_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Roster]'))
ALTER TABLE [dbo].[Roster] DROP CONSTRAINT [FK_Roster_Users]
GO
/****** Object:  ForeignKey [FK_UserRole_Role]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserRole_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole]'))
ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [FK_UserRole_Role]
GO
/****** Object:  ForeignKey [FK_UserRole_User]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserRole_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole]'))
ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [FK_UserRole_User]
GO
/****** Object:  ForeignKey [FK_Users_UserGroup]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Users_UserGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_Users_UserGroup]
GO
/****** Object:  Table [dbo].[Roster]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Roster]') AND type in (N'U'))
DROP TABLE [dbo].[Roster]
GO
/****** Object:  Table [dbo].[DoctorRoom]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoom]') AND type in (N'U'))
DROP TABLE [dbo].[DoctorRoom]
GO
/****** Object:  Table [dbo].[DoctorService]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorService]') AND type in (N'U'))
DROP TABLE [dbo].[DoctorService]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserRole]') AND type in (N'U'))
DROP TABLE [dbo].[UserRole]
GO
/****** Object:  Table [dbo].[Appointment]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Appointment]') AND type in (N'U'))
DROP TABLE [dbo].[Appointment]
GO
/****** Object:  Table [dbo].[AppointmentGroup]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AppointmentGroup]') AND type in (N'U'))
DROP TABLE [dbo].[AppointmentGroup]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
DROP TABLE [dbo].[Users]
GO
/****** Object:  Table [dbo].[RoleDetail]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleDetail]') AND type in (N'U'))
DROP TABLE [dbo].[RoleDetail]
GO
/****** Object:  Table [dbo].[Room]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Room]') AND type in (N'U'))
DROP TABLE [dbo].[Room]
GO
/****** Object:  Table [dbo].[GroupRole]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GroupRole]') AND type in (N'U'))
DROP TABLE [dbo].[GroupRole]
GO
/****** Object:  Table [dbo].[LogHistory]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LogHistory]') AND type in (N'U'))
DROP TABLE [dbo].[LogHistory]
GO
/****** Object:  Table [dbo].[Patient]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Patient]') AND type in (N'U'))
DROP TABLE [dbo].[Patient]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role]') AND type in (N'U'))
DROP TABLE [dbo].[Role]
GO
/****** Object:  Table [dbo].[RosterType]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RosterType]') AND type in (N'U'))
DROP TABLE [dbo].[RosterType]
GO
/****** Object:  Table [dbo].[Screen]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Screen]') AND type in (N'U'))
DROP TABLE [dbo].[Screen]
GO
/****** Object:  Table [dbo].[Services]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Services]') AND type in (N'U'))
DROP TABLE [dbo].[Services]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Status]') AND type in (N'U'))
DROP TABLE [dbo].[Status]
GO
/****** Object:  Table [dbo].[tblSettings]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblSettings]') AND type in (N'U'))
DROP TABLE [dbo].[tblSettings]
GO
/****** Object:  Table [dbo].[Units]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Units]') AND type in (N'U'))
DROP TABLE [dbo].[Units]
GO
/****** Object:  Table [dbo].[UserGroup]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserGroup]') AND type in (N'U'))
DROP TABLE [dbo].[UserGroup]
GO
/****** Object:  Default [DF_Appointment_IsComplete]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Appointment_IsComplete]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Appointment_IsComplete]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Appointment] DROP CONSTRAINT [DF_Appointment_IsComplete]
END


End
GO
/****** Object:  Default [DF_Appointment_IsDisabled]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Appointment_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Appointment_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Appointment] DROP CONSTRAINT [DF_Appointment_IsDisabled]
END


End
GO
/****** Object:  Default [DF_Appointment_CreateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Appointment_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Appointment_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Appointment] DROP CONSTRAINT [DF_Appointment_CreateDate]
END


End
GO
/****** Object:  Default [DF_Appointment_UpdateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Appointment_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Appointment_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Appointment] DROP CONSTRAINT [DF_Appointment_UpdateDate]
END


End
GO
/****** Object:  Default [DF_AppointmentGroup_PriorityIndex]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_AppointmentGroup_PriorityIndex]') AND parent_object_id = OBJECT_ID(N'[dbo].[AppointmentGroup]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_AppointmentGroup_PriorityIndex]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[AppointmentGroup] DROP CONSTRAINT [DF_AppointmentGroup_PriorityIndex]
END


End
GO
/****** Object:  Default [DF_AppointmentGroup_IsDisabled]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_AppointmentGroup_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[AppointmentGroup]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_AppointmentGroup_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[AppointmentGroup] DROP CONSTRAINT [DF_AppointmentGroup_IsDisabled]
END


End
GO
/****** Object:  Default [DF_AppointmentGroup_CreateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_AppointmentGroup_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[AppointmentGroup]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_AppointmentGroup_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[AppointmentGroup] DROP CONSTRAINT [DF_AppointmentGroup_CreateDate]
END


End
GO
/****** Object:  Default [DF_AppointmentGroup_UpdateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_AppointmentGroup_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[AppointmentGroup]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_AppointmentGroup_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[AppointmentGroup] DROP CONSTRAINT [DF_AppointmentGroup_UpdateDate]
END


End
GO
/****** Object:  Default [DF_UnitRoom_Priority]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UnitRoom_Priority]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoom]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UnitRoom_Priority]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorRoom] DROP CONSTRAINT [DF_UnitRoom_Priority]
END


End
GO
/****** Object:  Default [DF_UnitRoom_IsDisabled]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UnitRoom_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoom]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UnitRoom_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorRoom] DROP CONSTRAINT [DF_UnitRoom_IsDisabled]
END


End
GO
/****** Object:  Default [DF_UnitRoom_CreateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UnitRoom_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoom]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UnitRoom_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorRoom] DROP CONSTRAINT [DF_UnitRoom_CreateDate]
END


End
GO
/****** Object:  Default [DF_UnitRoom_UpdateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UnitRoom_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoom]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UnitRoom_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorRoom] DROP CONSTRAINT [DF_UnitRoom_UpdateDate]
END


End
GO
/****** Object:  Default [DF_DoctorService_IsDisabled]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_DoctorService_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorService]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_DoctorService_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorService] DROP CONSTRAINT [DF_DoctorService_IsDisabled]
END


End
GO
/****** Object:  Default [DF_DoctorService_CreateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_DoctorService_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorService]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_DoctorService_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorService] DROP CONSTRAINT [DF_DoctorService_CreateDate]
END


End
GO
/****** Object:  Default [DF_DoctorService_UpdateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_DoctorService_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorService]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_DoctorService_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorService] DROP CONSTRAINT [DF_DoctorService_UpdateDate]
END


End
GO
/****** Object:  Default [DF_GroupRole_IsDisabled]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_GroupRole_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupRole]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GroupRole_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GroupRole] DROP CONSTRAINT [DF_GroupRole_IsDisabled]
END


End
GO
/****** Object:  Default [DF_GroupRole_CreateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_GroupRole_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupRole]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GroupRole_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GroupRole] DROP CONSTRAINT [DF_GroupRole_CreateDate]
END


End
GO
/****** Object:  Default [DF_GroupRole_UpdateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_GroupRole_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupRole]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GroupRole_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GroupRole] DROP CONSTRAINT [DF_GroupRole_UpdateDate]
END


End
GO
/****** Object:  Default [DF_Patient_IsFemale]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Patient_IsFemale]') AND parent_object_id = OBJECT_ID(N'[dbo].[Patient]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Patient_IsFemale]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Patient] DROP CONSTRAINT [DF_Patient_IsFemale]
END


End
GO
/****** Object:  Default [DF_Patient_IsDisabled]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Patient_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Patient]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Patient_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Patient] DROP CONSTRAINT [DF_Patient_IsDisabled]
END


End
GO
/****** Object:  Default [DF_Patient_CreateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Patient_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Patient]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Patient_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Patient] DROP CONSTRAINT [DF_Patient_CreateDate]
END


End
GO
/****** Object:  Default [DF_Patient_UpdateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Patient_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Patient]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Patient_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Patient] DROP CONSTRAINT [DF_Patient_UpdateDate]
END


End
GO
/****** Object:  Default [DF_Roles_IsLocked]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Roles_IsLocked]') AND parent_object_id = OBJECT_ID(N'[dbo].[Role]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Roles_IsLocked]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Role] DROP CONSTRAINT [DF_Roles_IsLocked]
END


End
GO
/****** Object:  Default [DF_Roles_IsDisabled]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Roles_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Role]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Roles_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Role] DROP CONSTRAINT [DF_Roles_IsDisabled]
END


End
GO
/****** Object:  Default [DF_Roles_CreateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Roles_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Role]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Roles_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Role] DROP CONSTRAINT [DF_Roles_CreateDate]
END


End
GO
/****** Object:  Default [DF_Roles_UpdateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Roles_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Role]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Roles_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Role] DROP CONSTRAINT [DF_Roles_UpdateDate]
END


End
GO
/****** Object:  Default [DF_RoleDetail_IsDisabled]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RoleDetail_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleDetail]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RoleDetail_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RoleDetail] DROP CONSTRAINT [DF_RoleDetail_IsDisabled]
END


End
GO
/****** Object:  Default [DF_RoleDetail_CreateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RoleDetail_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleDetail]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RoleDetail_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RoleDetail] DROP CONSTRAINT [DF_RoleDetail_CreateDate]
END


End
GO
/****** Object:  Default [DF_RoleDetail_UpdateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RoleDetail_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleDetail]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RoleDetail_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RoleDetail] DROP CONSTRAINT [DF_RoleDetail_UpdateDate]
END


End
GO
/****** Object:  Default [DF_Room_IsDisabled]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Room_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Room]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Room_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Room] DROP CONSTRAINT [DF_Room_IsDisabled]
END


End
GO
/****** Object:  Default [DF_Room_CreateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Room_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Room]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Room_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Room] DROP CONSTRAINT [DF_Room_CreateDate]
END


End
GO
/****** Object:  Default [DF_Room_UpdateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Room_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Room]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Room_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Room] DROP CONSTRAINT [DF_Room_UpdateDate]
END


End
GO
/****** Object:  Default [DF_Roster_IsDisabled]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Roster_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Roster]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Roster_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Roster] DROP CONSTRAINT [DF_Roster_IsDisabled]
END


End
GO
/****** Object:  Default [DF_Roster_CreateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Roster_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Roster]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Roster_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Roster] DROP CONSTRAINT [DF_Roster_CreateDate]
END


End
GO
/****** Object:  Default [DF_Roster_UpdateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Roster_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Roster]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Roster_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Roster] DROP CONSTRAINT [DF_Roster_UpdateDate]
END


End
GO
/****** Object:  Default [DF_RosterType_IsBooked]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RosterType_IsBooked]') AND parent_object_id = OBJECT_ID(N'[dbo].[RosterType]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RosterType_IsBooked]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RosterType] DROP CONSTRAINT [DF_RosterType_IsBooked]
END


End
GO
/****** Object:  Default [DF_RosterType_IsDisabled]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RosterType_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[RosterType]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RosterType_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RosterType] DROP CONSTRAINT [DF_RosterType_IsDisabled]
END


End
GO
/****** Object:  Default [DF_RosterType_CreateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RosterType_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[RosterType]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RosterType_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RosterType] DROP CONSTRAINT [DF_RosterType_CreateDate]
END


End
GO
/****** Object:  Default [DF_RosterType_UpdateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RosterType_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[RosterType]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RosterType_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RosterType] DROP CONSTRAINT [DF_RosterType_UpdateDate]
END


End
GO
/****** Object:  Default [DF_Screen_IsDisabled]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Screen_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Screen]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Screen_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Screen] DROP CONSTRAINT [DF_Screen_IsDisabled]
END


End
GO
/****** Object:  Default [DF_Screen_CreateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Screen_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Screen]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Screen_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Screen] DROP CONSTRAINT [DF_Screen_CreateDate]
END


End
GO
/****** Object:  Default [DF_Screen_UpdateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Screen_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Screen]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Screen_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Screen] DROP CONSTRAINT [DF_Screen_UpdateDate]
END


End
GO
/****** Object:  Default [DF_Procedure_IsDisabled]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Procedure_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Services]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Procedure_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Services] DROP CONSTRAINT [DF_Procedure_IsDisabled]
END


End
GO
/****** Object:  Default [DF_Procedure_CreateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Procedure_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Services]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Procedure_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Services] DROP CONSTRAINT [DF_Procedure_CreateDate]
END


End
GO
/****** Object:  Default [DF_Procedure_UpdateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Procedure_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Services]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Procedure_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Services] DROP CONSTRAINT [DF_Procedure_UpdateDate]
END


End
GO
/****** Object:  Default [DF_Status_PriorityIndex_1]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Status_PriorityIndex_1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Status]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Status_PriorityIndex_1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Status] DROP CONSTRAINT [DF_Status_PriorityIndex_1]
END


End
GO
/****** Object:  Default [DF_Status_IsDisabled]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Status_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Status]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Status_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Status] DROP CONSTRAINT [DF_Status_IsDisabled]
END


End
GO
/****** Object:  Default [DF_Status_CreateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Status_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Status]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Status_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Status] DROP CONSTRAINT [DF_Status_CreateDate]
END


End
GO
/****** Object:  Default [DF_Status_UpdateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Status_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Status]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Status_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Status] DROP CONSTRAINT [DF_Status_UpdateDate]
END


End
GO
/****** Object:  Default [DF_tblSettings_id]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblSettings_id]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblSettings]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblSettings_id]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblSettings] DROP CONSTRAINT [DF_tblSettings_id]
END


End
GO
/****** Object:  Default [DF_Units_PriorityIndex]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Units_PriorityIndex]') AND parent_object_id = OBJECT_ID(N'[dbo].[Units]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Units_PriorityIndex]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Units] DROP CONSTRAINT [DF_Units_PriorityIndex]
END


End
GO
/****** Object:  Default [DF_Unit_IsDisabled]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Unit_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Units]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Unit_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Units] DROP CONSTRAINT [DF_Unit_IsDisabled]
END


End
GO
/****** Object:  Default [DF_Unit_CreateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Unit_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Units]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Unit_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Units] DROP CONSTRAINT [DF_Unit_CreateDate]
END


End
GO
/****** Object:  Default [DF_Unit_UpdateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Unit_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Units]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Unit_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Units] DROP CONSTRAINT [DF_Unit_UpdateDate]
END


End
GO
/****** Object:  Default [DF_UserGroup_IsLocked]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UserGroup_IsLocked]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserGroup]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserGroup_IsLocked]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserGroup] DROP CONSTRAINT [DF_UserGroup_IsLocked]
END


End
GO
/****** Object:  Default [DF_UserGroup_IsDisabled]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UserGroup_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserGroup]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserGroup_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserGroup] DROP CONSTRAINT [DF_UserGroup_IsDisabled]
END


End
GO
/****** Object:  Default [DF_UserGroup_CreateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UserGroup_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserGroup]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserGroup_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserGroup] DROP CONSTRAINT [DF_UserGroup_CreateDate]
END


End
GO
/****** Object:  Default [DF_UserGroup_UpdateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UserGroup_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserGroup]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserGroup_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserGroup] DROP CONSTRAINT [DF_UserGroup_UpdateDate]
END


End
GO
/****** Object:  Default [DF_UserRole_IsDisabled]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UserRole_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserRole_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [DF_UserRole_IsDisabled]
END


End
GO
/****** Object:  Default [DF_UserRole_CreateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UserRole_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserRole_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [DF_UserRole_CreateDate]
END


End
GO
/****** Object:  Default [DF_UserRole_UpdateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UserRole_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserRole_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [DF_UserRole_UpdateDate]
END


End
GO
/****** Object:  Default [DF_Table_1_IsLocked]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Table_1_IsLocked]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table_1_IsLocked]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Users] DROP CONSTRAINT [DF_Table_1_IsLocked]
END


End
GO
/****** Object:  Default [DF_Users_IsFemale]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Users_IsFemale]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Users_IsFemale]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Users] DROP CONSTRAINT [DF_Users_IsFemale]
END


End
GO
/****** Object:  Default [DF_User_IsDisabled]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_User_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_User_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Users] DROP CONSTRAINT [DF_User_IsDisabled]
END


End
GO
/****** Object:  Default [DF_User_CreateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_User_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_User_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Users] DROP CONSTRAINT [DF_User_CreateDate]
END


End
GO
/****** Object:  Default [DF_User_UpdateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_User_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_User_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Users] DROP CONSTRAINT [DF_User_UpdateDate]
END


End
GO
/****** Object:  Table [dbo].[UserGroup]    Script Date: 07/17/2012 22:23:54 ******/
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
/****** Object:  Table [dbo].[Units]    Script Date: 07/17/2012 22:23:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Units]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Units](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Note] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PriorityIndex] [int] NOT NULL,
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
SET IDENTITY_INSERT [dbo].[Units] ON
INSERT [dbo].[Units] ([Id], [Title], [Note], [PriorityIndex], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (1, N'First Floor', N'First Floor', 1, 0, N'GOWU', CAST(0x0000A08800BBE192 AS DateTime), N'GOWU', CAST(0x0000A08800BBE192 AS DateTime))
INSERT [dbo].[Units] ([Id], [Title], [Note], [PriorityIndex], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (2, N'Second Floor', N'Second Floor', 1, 0, N'GOWU', CAST(0x0000A08800C206A2 AS DateTime), N'GOWU', CAST(0x0000A08800C206A2 AS DateTime))
SET IDENTITY_INSERT [dbo].[Units] OFF
/****** Object:  Table [dbo].[tblSettings]    Script Date: 07/17/2012 22:23:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblSettings]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblSettings](
	[ID] [uniqueidentifier] NOT NULL,
	[Type] [tinyint] NOT NULL,
	[Code] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ValueString] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ValueBinary] [varbinary](5000) NULL,
 CONSTRAINT [PK_tblSettings] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[tblSettings] ([ID], [Type], [Code], [ValueString], [ValueBinary]) VALUES (N'4fffcbbe-86ca-429e-a729-066ae17790d6', 0, N'TIME_LEFT_RMIND_APPOINTMENT', N'15', NULL)
INSERT [dbo].[tblSettings] ([ID], [Type], [Code], [ValueString], [ValueBinary]) VALUES (N'44131fb2-5fba-4ed5-862a-286bff4a4cbc', 0, N'MINUTE_STEP', N'5', NULL)
INSERT [dbo].[tblSettings] ([ID], [Type], [Code], [ValueString], [ValueBinary]) VALUES (N'c590be27-da4b-419b-9138-3987ff5ba767', 0, N'ROSTER_PREFIX', N'OST', NULL)
INSERT [dbo].[tblSettings] ([ID], [Type], [Code], [ValueString], [ValueBinary]) VALUES (N'fda005c4-f591-4b0d-a4e0-6d51e8f89545', 0, N'MAX_HOUR', N'24', NULL)
INSERT [dbo].[tblSettings] ([ID], [Type], [Code], [ValueString], [ValueBinary]) VALUES (N'3e1802a7-70fa-494f-85e8-75a614623d69', 0, N'GET_PAGED_LENGTH', N'10000', NULL)
INSERT [dbo].[tblSettings] ([ID], [Type], [Code], [ValueString], [ValueBinary]) VALUES (N'322f79ed-4b28-455a-b5ac-9a45aea5c758', 0, N'USER_PREFIX', N'USR', NULL)
INSERT [dbo].[tblSettings] ([ID], [Type], [Code], [ValueString], [ValueBinary]) VALUES (N'2787e4e3-4648-4bfd-8aa2-b5e080301368', 0, N'MAX_MINUTE', N'60', NULL)
INSERT [dbo].[tblSettings] ([ID], [Type], [Code], [ValueString], [ValueBinary]) VALUES (N'7262c162-f884-447c-990e-c3b6b5409ecf', 0, N'PATIENT_PREFIX', N'PTN', NULL)
INSERT [dbo].[tblSettings] ([ID], [Type], [Code], [ValueString], [ValueBinary]) VALUES (N'c775c0e6-547f-4175-a0d7-f9343a5cd0dc', 0, N'APPOINTMENT_PREFIX', N'PPT', NULL)
/****** Object:  Table [dbo].[Status]    Script Date: 07/17/2012 22:23:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Status]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Status](
	[Id] [int] IDENTITY(1,1) NOT NULL,
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
/****** Object:  Table [dbo].[Services]    Script Date: 07/17/2012 22:23:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Services]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Services](
	[Id] [int] IDENTITY(1,1) NOT NULL,
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
SET IDENTITY_INSERT [dbo].[Services] ON
INSERT [dbo].[Services] ([Id], [Title], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (1, N'Thần kinh', N'Thần kinh', 0, N'GOWU', CAST(0x0000A08800C50A5D AS DateTime), N'GOWU', CAST(0x0000A08800C50A5D AS DateTime))
INSERT [dbo].[Services] ([Id], [Title], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (2, N'Răng hàm mặt', N'Răng hàm mặt', 0, N'GOWU', CAST(0x0000A08800C518D0 AS DateTime), N'GOWU', CAST(0x0000A08800C518D0 AS DateTime))
INSERT [dbo].[Services] ([Id], [Title], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (3, N'Da liễu', N'Da liễu', 0, N'GOWU', CAST(0x0000A08800C52F86 AS DateTime), N'GOWU', CAST(0x0000A08800C52F86 AS DateTime))
SET IDENTITY_INSERT [dbo].[Services] OFF
/****** Object:  Table [dbo].[Screen]    Script Date: 07/17/2012 22:23:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Screen]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Screen](
	[ScreenCode] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ScreenName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Screen] PRIMARY KEY CLUSTERED 
(
	[ScreenCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Screen', N'COLUMN',N'ScreenCode'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Link name of screen. 
Ex: Status, Appointment...' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Screen', @level2type=N'COLUMN',@level2name=N'ScreenCode'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Screen', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Define screen for role detail' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Screen'
GO
INSERT [dbo].[Screen] ([ScreenCode], [ScreenName], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'Roster', N'Roster', 0, N'GOWU', CAST(0x0000A08C014B3213 AS DateTime), N'GOWU', CAST(0x0000A08C014B3213 AS DateTime))
/****** Object:  Table [dbo].[RosterType]    Script Date: 07/17/2012 22:23:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RosterType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RosterType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[IsBooked] [bit] NOT NULL,
	[Note] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_RosterType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [IX_RosterType_Id_IsBooked] UNIQUE NONCLUSTERED 
(
	[Id] ASC,
	[IsBooked] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [IX_RosterType_Id_IsBooked_IsDisabled] UNIQUE NONCLUSTERED 
(
	[Id] ASC,
	[IsBooked] ASC,
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [IX_RosterType_Id_IsDisabled] UNIQUE NONCLUSTERED 
(
	[Id] ASC,
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[RosterType] ON
INSERT [dbo].[RosterType] ([Id], [Title], [IsBooked], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (1, N'Khám bệnh', 1, NULL, 0, N'GOWU', CAST(0x0000A0880113A4D1 AS DateTime), N'GOWU', CAST(0x0000A0880113A4D1 AS DateTime))
INSERT [dbo].[RosterType] ([Id], [Title], [IsBooked], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (2, N'Công tác', 0, NULL, 0, N'GOWU', CAST(0x0000A0880113AD74 AS DateTime), N'GOWU', CAST(0x0000A0880113AD74 AS DateTime))
SET IDENTITY_INSERT [dbo].[RosterType] OFF
/****** Object:  Table [dbo].[Role]    Script Date: 07/17/2012 22:23:54 ******/
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
/****** Object:  Table [dbo].[Patient]    Script Date: 07/17/2012 22:23:54 ******/
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
	[Avatar] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
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
/****** Object:  Table [dbo].[LogHistory]    Script Date: 07/17/2012 22:23:54 ******/
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
/****** Object:  Table [dbo].[GroupRole]    Script Date: 07/17/2012 22:23:54 ******/
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
/****** Object:  Table [dbo].[Room]    Script Date: 07/17/2012 22:23:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Room]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Room](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Note] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ServicesId] [int] NULL,
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
/****** Object:  Table [dbo].[RoleDetail]    Script Date: 07/17/2012 22:23:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleDetail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RoleDetail](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NULL,
	[ScreenCode] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
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
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'RoleDetail', N'COLUMN',N'ScreenCode'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'What screen role can access' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleDetail', @level2type=N'COLUMN',@level2name=N'ScreenCode'
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
SET IDENTITY_INSERT [dbo].[RoleDetail] ON
INSERT [dbo].[RoleDetail] ([Id], [RoleId], [ScreenCode], [Crud], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (1, 1, N'Roster', N'CRUD', 0, N'GOWU', CAST(0x0000A08C015DDE9E AS DateTime), N'GOWU', CAST(0x0000A08C015DDE9E AS DateTime))
SET IDENTITY_INSERT [dbo].[RoleDetail] OFF
/****** Object:  Table [dbo].[Users]    Script Date: 07/17/2012 22:23:54 ******/
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
/****** Object:  Table [dbo].[AppointmentGroup]    Script Date: 07/17/2012 22:23:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AppointmentGroup]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AppointmentGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Note] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PriorityIndex] [int] NOT NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UpdateDate] [datetime] NOT NULL,
	[UnitId] [int] NULL,
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
SET IDENTITY_INSERT [dbo].[AppointmentGroup] ON
INSERT [dbo].[AppointmentGroup] ([Id], [Title], [Note], [PriorityIndex], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate], [UnitId]) VALUES (1, N'First Group', N'First Group', 1, 0, NULL, CAST(0x0000A08800C09952 AS DateTime), NULL, CAST(0x0000A08800C09952 AS DateTime), 1)
INSERT [dbo].[AppointmentGroup] ([Id], [Title], [Note], [PriorityIndex], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate], [UnitId]) VALUES (2, N'X-Ray', N'X-Ray', 1, 0, N'GOWU', CAST(0x0000A08800C3A148 AS DateTime), N'GOWU', CAST(0x0000A08800C3A148 AS DateTime), 2)
SET IDENTITY_INSERT [dbo].[AppointmentGroup] OFF
/****** Object:  Table [dbo].[Appointment]    Script Date: 07/17/2012 22:23:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Appointment]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Appointment](
	[Id] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PatientId] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DoctorId] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[RoomId] [int] NULL,
	[ServicesId] [int] NULL,
	[StatusId] [int] NULL,
	[AppointmentGroupId] [int] NULL,
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
/****** Object:  Table [dbo].[UserRole]    Script Date: 07/17/2012 22:23:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserRole]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserRole](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[RoleId] [int] NULL,
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
SET IDENTITY_INSERT [dbo].[UserRole] ON
INSERT [dbo].[UserRole] ([Id], [UserId], [RoleId], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (1, N'USR0001', 1, 0, N'GOWU', CAST(0x0000A08C015DAC0D AS DateTime), N'GOWU', CAST(0x0000A08C015DAC0D AS DateTime))
SET IDENTITY_INSERT [dbo].[UserRole] OFF
/****** Object:  Table [dbo].[DoctorService]    Script Date: 07/17/2012 22:23:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorService]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DoctorService](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DoctorId] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ServiceId] [int] NOT NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_DoctorService] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[DoctorService] ON
INSERT [dbo].[DoctorService] ([Id], [DoctorId], [ServiceId], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (2, N'USR0001', 1, 0, N'GOWU', CAST(0x0000A08800C8F706 AS DateTime), N'GOWU', CAST(0x0000A08800C8F706 AS DateTime))
INSERT [dbo].[DoctorService] ([Id], [DoctorId], [ServiceId], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (3, N'USR0002', 3, 0, N'GOWU', CAST(0x0000A08800C93A6B AS DateTime), N'GOWU', CAST(0x0000A08800C93A6B AS DateTime))
SET IDENTITY_INSERT [dbo].[DoctorService] OFF
/****** Object:  Table [dbo].[DoctorRoom]    Script Date: 07/17/2012 22:23:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoom]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DoctorRoom](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DoctorId] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[RoomId] [int] NULL,
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
/****** Object:  Table [dbo].[Roster]    Script Date: 07/17/2012 22:23:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Roster]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Roster](
	[Id] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DoctorId] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[RoomId] [int] NULL,
	[RosterTypeId] [int] NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[Note] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Roster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120708001', N'USR0001', NULL, 1, CAST(0x0000A08A00000000 AS DateTime), CAST(0x0000A08A00C5C100 AS DateTime), N'Test', 0, N'GOWU', CAST(0x0000A08801564B95 AS DateTime), N'GOWU', CAST(0x0000A08801564B95 AS DateTime))
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120708002', N'USR0001', NULL, 1, CAST(0x0000A0A000AD08E0 AS DateTime), CAST(0x0000A0A000EC34C0 AS DateTime), N'Repeat', 0, N'GOWU', CAST(0x0000A0880156E06B AS DateTime), N'GOWU', CAST(0x0000A0880156E06B AS DateTime))
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120708003', N'USR0001', NULL, 1, CAST(0x0000A0A500AD08E0 AS DateTime), CAST(0x0000A0A500EC34C0 AS DateTime), N'Repeat', 0, N'GOWU', CAST(0x0000A0880156E06E AS DateTime), N'GOWU', CAST(0x0000A0880156E06E AS DateTime))
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120708004', N'USR0001', NULL, 1, CAST(0x0000A0A600AD08E0 AS DateTime), CAST(0x0000A0A600EC34C0 AS DateTime), N'Repeat', 0, N'GOWU', CAST(0x0000A0880156E077 AS DateTime), N'GOWU', CAST(0x0000A0880156E077 AS DateTime))
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120708005', N'USR0001', NULL, 1, CAST(0x0000A0A700AD08E0 AS DateTime), CAST(0x0000A0A700EC34C0 AS DateTime), N'Repeat', 0, N'GOWU', CAST(0x0000A0880156E07A AS DateTime), N'GOWU', CAST(0x0000A0880156E07A AS DateTime))
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120708006', N'USR0001', NULL, 1, CAST(0x0000A0AC00AD08E0 AS DateTime), CAST(0x0000A0AC00EC34C0 AS DateTime), N'Repeat', 0, N'GOWU', CAST(0x0000A0880156E080 AS DateTime), N'GOWU', CAST(0x0000A0880156E080 AS DateTime))
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120708007', N'USR0001', NULL, 1, CAST(0x0000A0AD00AD08E0 AS DateTime), CAST(0x0000A0AD00EC34C0 AS DateTime), N'Repeat', 0, N'GOWU', CAST(0x0000A0880156E083 AS DateTime), N'GOWU', CAST(0x0000A0880156E083 AS DateTime))
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120708008', N'USR0001', NULL, 1, CAST(0x0000A0AE00AD08E0 AS DateTime), CAST(0x0000A0AE00EC34C0 AS DateTime), N'Repeat', 0, N'GOWU', CAST(0x0000A0880156E08A AS DateTime), N'GOWU', CAST(0x0000A0880156E08A AS DateTime))
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120708009', N'USR0001', NULL, 1, CAST(0x0000A0B300AD08E0 AS DateTime), CAST(0x0000A0B300EC34C0 AS DateTime), N'Repeat', 0, N'GOWU', CAST(0x0000A0880156E093 AS DateTime), N'GOWU', CAST(0x0000A0880156E093 AS DateTime))
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120708010', N'USR0001', NULL, 1, CAST(0x0000A0B400AD08E0 AS DateTime), CAST(0x0000A0B400EC34C0 AS DateTime), N'Repeat', 0, N'GOWU', CAST(0x0000A0880156E099 AS DateTime), N'GOWU', CAST(0x0000A0880156E099 AS DateTime))
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120708011', N'USR0001', NULL, 1, CAST(0x0000A0B500AD08E0 AS DateTime), CAST(0x0000A0B500EC34C0 AS DateTime), N'Repeat', 0, N'GOWU', CAST(0x0000A0880156E0A0 AS DateTime), N'GOWU', CAST(0x0000A0880156E0A0 AS DateTime))
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120708012', N'USR0001', NULL, 1, CAST(0x0000A0BA00AD08E0 AS DateTime), CAST(0x0000A0BA00EC34C0 AS DateTime), N'Repeat', 0, N'GOWU', CAST(0x0000A0880156E0A4 AS DateTime), N'GOWU', CAST(0x0000A0880156E0A4 AS DateTime))
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120708013', N'USR0001', NULL, 1, CAST(0x0000A0BB00AD08E0 AS DateTime), CAST(0x0000A0BB00EC34C0 AS DateTime), N'Repeat', 0, N'GOWU', CAST(0x0000A0880156E0A8 AS DateTime), N'GOWU', CAST(0x0000A0880156E0A8 AS DateTime))
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120708014', N'USR0001', NULL, 1, CAST(0x0000A0BC00AD08E0 AS DateTime), CAST(0x0000A0BC00EC34C0 AS DateTime), N'Repeat', 0, N'GOWU', CAST(0x0000A0880156E0AD AS DateTime), N'GOWU', CAST(0x0000A0880156E0AD AS DateTime))
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120708015', N'USR0001', NULL, 2, CAST(0x0000A08A00C5C100 AS DateTime), CAST(0x0000A08B00000000 AS DateTime), N'Công tác Hà Nội', 0, N'GOWU', CAST(0x0000A0880157696D AS DateTime), N'GOWU', CAST(0x0000A0880157696D AS DateTime))
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120708016', N'USR0002', NULL, 1, CAST(0x0000A08900C5C100 AS DateTime), CAST(0x0000A08A00000000 AS DateTime), N'', 0, N'GOWU', CAST(0x0000A0880180E787 AS DateTime), N'GOWU', CAST(0x0000A0880180E787 AS DateTime))
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120708017', N'USR0002', NULL, 1, CAST(0x0000A08B00000000 AS DateTime), CAST(0x0000A08B00C5C100 AS DateTime), N'', 0, N'GOWU', CAST(0x0000A08801820253 AS DateTime), N'GOWU', CAST(0x0000A08801820253 AS DateTime))
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120712001', N'USR0002', NULL, 1, CAST(0x0000A0A10099CF00 AS DateTime), CAST(0x0000A0A100AA49C0 AS DateTime), N'', 0, N'GOWU', CAST(0x0000A08C017B4217 AS DateTime), N'GOWU', CAST(0x0000A08C017B4217 AS DateTime))
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120712002', N'USR0002', NULL, 1, CAST(0x0000A0A20099CF00 AS DateTime), CAST(0x0000A0A200AA49C0 AS DateTime), N'', 0, N'GOWU', CAST(0x0000A08C017B4229 AS DateTime), N'GOWU', CAST(0x0000A08C017B4229 AS DateTime))
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120712003', N'USR0002', NULL, 1, CAST(0x0000A0A30099CF00 AS DateTime), CAST(0x0000A0A300AA49C0 AS DateTime), N'', 0, N'GOWU', CAST(0x0000A08C017B422C AS DateTime), N'GOWU', CAST(0x0000A08C017B422C AS DateTime))
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120712004', N'USR0002', NULL, 1, CAST(0x0000A0A80099CF00 AS DateTime), CAST(0x0000A0A800AA49C0 AS DateTime), N'', 0, N'GOWU', CAST(0x0000A08C017B422F AS DateTime), N'GOWU', CAST(0x0000A08C017B422F AS DateTime))
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120712005', N'USR0002', NULL, 1, CAST(0x0000A0A90099CF00 AS DateTime), CAST(0x0000A0A900AA49C0 AS DateTime), N'', 0, N'GOWU', CAST(0x0000A08C017B4231 AS DateTime), N'GOWU', CAST(0x0000A08C017B4231 AS DateTime))
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120712006', N'USR0002', NULL, 1, CAST(0x0000A0AA0099CF00 AS DateTime), CAST(0x0000A0AA00AA49C0 AS DateTime), N'', 0, N'GOWU', CAST(0x0000A08C017B4233 AS DateTime), N'GOWU', CAST(0x0000A08C017B4233 AS DateTime))
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120712007', N'USR0002', NULL, 1, CAST(0x0000A0AF0099CF00 AS DateTime), CAST(0x0000A0AF00AA49C0 AS DateTime), N'', 0, N'GOWU', CAST(0x0000A08C017B4235 AS DateTime), N'GOWU', CAST(0x0000A08C017B4235 AS DateTime))
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120712008', N'USR0002', NULL, 1, CAST(0x0000A0B00099CF00 AS DateTime), CAST(0x0000A0B000AA49C0 AS DateTime), N'', 0, N'GOWU', CAST(0x0000A08C017B4237 AS DateTime), N'GOWU', CAST(0x0000A08C017B4237 AS DateTime))
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120712009', N'USR0002', NULL, 1, CAST(0x0000A0B10099CF00 AS DateTime), CAST(0x0000A0B100AA49C0 AS DateTime), N'', 0, N'GOWU', CAST(0x0000A08C017B4239 AS DateTime), N'GOWU', CAST(0x0000A08C017B4239 AS DateTime))
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120712010', N'USR0002', NULL, 1, CAST(0x0000A0B60099CF00 AS DateTime), CAST(0x0000A0B600AA49C0 AS DateTime), N'', 0, N'GOWU', CAST(0x0000A08C017B423B AS DateTime), N'GOWU', CAST(0x0000A08C017B423B AS DateTime))
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120712011', N'USR0002', NULL, 1, CAST(0x0000A0B70099CF00 AS DateTime), CAST(0x0000A0B700AA49C0 AS DateTime), N'', 0, N'GOWU', CAST(0x0000A08C017B423D AS DateTime), N'GOWU', CAST(0x0000A08C017B423D AS DateTime))
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120712012', N'USR0002', NULL, 1, CAST(0x0000A0B80099CF00 AS DateTime), CAST(0x0000A0B800AA49C0 AS DateTime), N'', 0, N'GOWU', CAST(0x0000A08C017B423F AS DateTime), N'GOWU', CAST(0x0000A08C017B423F AS DateTime))
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120712013', N'USR0002', NULL, 1, CAST(0x0000A0BD0099CF00 AS DateTime), CAST(0x0000A0BD00AA49C0 AS DateTime), N'', 0, N'GOWU', CAST(0x0000A08C017B4241 AS DateTime), N'GOWU', CAST(0x0000A08C017B4241 AS DateTime))
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120712014', N'USR0002', NULL, 1, CAST(0x0000A0BE0099CF00 AS DateTime), CAST(0x0000A0BE00AA49C0 AS DateTime), N'', 0, N'GOWU', CAST(0x0000A08C017B4244 AS DateTime), N'GOWU', CAST(0x0000A08C017B4244 AS DateTime))
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120712015', N'USR0001', NULL, 1, CAST(0x0000A0A20099CF00 AS DateTime), CAST(0x0000A0A200AA49C0 AS DateTime), N'', 0, N'GOWU', CAST(0x0000A08C017B65E6 AS DateTime), N'GOWU', CAST(0x0000A08C017B65E6 AS DateTime))
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120712016', N'USR0002', NULL, 1, CAST(0x0000A09000000000 AS DateTime), CAST(0x0000A0900023B4A0 AS DateTime), N'', 0, N'GOWU', CAST(0x0000A08C017E3978 AS DateTime), N'GOWU', CAST(0x0000A08E015473D9 AS DateTime))
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120714001', N'USR0002', NULL, 1, CAST(0x0000A08F00B80560 AS DateTime), CAST(0x0000A08F00C5C100 AS DateTime), N'', 0, N'GOWU', CAST(0x0000A08E01560C8B AS DateTime), N'GOWU', CAST(0x0000A08E015970F5 AS DateTime))
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120715001', N'USR0002', NULL, 1, CAST(0x0000A09000C5C100 AS DateTime), CAST(0x0000A09100000000 AS DateTime), N'', 0, N'GOWU', CAST(0x0000A08F00D84267 AS DateTime), N'GOWU', CAST(0x0000A08F00D84267 AS DateTime))
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120715002', N'USR0002', NULL, 1, CAST(0x0000A09100C5C100 AS DateTime), CAST(0x0000A09200000000 AS DateTime), N'', 0, N'GOWU', CAST(0x0000A08F00DB3F42 AS DateTime), N'GOWU', CAST(0x0000A08F00DB3F42 AS DateTime))
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120715003', N'USR0002', NULL, 1, CAST(0x0000A09100000000 AS DateTime), CAST(0x0000A09100C5C100 AS DateTime), N'', 0, N'GOWU', CAST(0x0000A08F00DB8594 AS DateTime), N'GOWU', CAST(0x0000A08F00DB8956 AS DateTime))
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120715004', N'USR0001', NULL, 1, CAST(0x0000A09100000000 AS DateTime), CAST(0x0000A09100099CF0 AS DateTime), N'', 1, N'GOWU', CAST(0x0000A08F00DBE137 AS DateTime), N'GOWU', CAST(0x0000A08F0104ED40 AS DateTime))
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120715005', N'USR0001', NULL, 1, CAST(0x0000A09000C5C100 AS DateTime), CAST(0x0000A09000DA5A70 AS DateTime), N'', 1, N'GOWU', CAST(0x0000A08F00DC96BD AS DateTime), N'GOWU', CAST(0x0000A08F01052BD4 AS DateTime))
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120715006', N'USR0001', NULL, 1, CAST(0x0000A09200C5C100 AS DateTime), CAST(0x0000A09200CDFE60 AS DateTime), N'', 1, N'GOWU', CAST(0x0000A08F00DC96C3 AS DateTime), N'GOWU', CAST(0x0000A08F01054D11 AS DateTime))
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120715007', N'USR0001', NULL, 1, CAST(0x0000A09200C5C100 AS DateTime), CAST(0x0000A09300000000 AS DateTime), N'', 1, N'GOWU', CAST(0x0000A08F01055928 AS DateTime), N'GOWU', CAST(0x0000A08F0106B240 AS DateTime))
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120715008', N'USR0001', NULL, 1, CAST(0x0000A09100000000 AS DateTime), CAST(0x0000A09100C5C100 AS DateTime), N'', 1, N'GOWU', CAST(0x0000A08F01068D61 AS DateTime), N'GOWU', CAST(0x0000A08F0106BC19 AS DateTime))
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120715009', N'USR0001', NULL, 1, CAST(0x0000A09500000000 AS DateTime), CAST(0x0000A09500C5C100 AS DateTime), N'', 0, N'GOWU', CAST(0x0000A08F0106A1F7 AS DateTime), N'GOWU', CAST(0x0000A08F0106A1F7 AS DateTime))
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120715010', N'USR0001', NULL, 1, CAST(0x0000A09C00000000 AS DateTime), CAST(0x0000A09C00C5C100 AS DateTime), N'', 0, N'GOWU', CAST(0x0000A08F0106A1FC AS DateTime), N'GOWU', CAST(0x0000A08F0106A1FC AS DateTime))
INSERT [dbo].[Roster] ([Id], [DoctorId], [RoomId], [RosterTypeId], [StartTime], [EndTime], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'OST120715011', N'USR0001', NULL, 1, CAST(0x0000A0A400000000 AS DateTime), CAST(0x0000A0A401716A50 AS DateTime), N'', 0, N'GOWU', CAST(0x0000A08F013B788F AS DateTime), N'GOWU', CAST(0x0000A08F013B788F AS DateTime))
/****** Object:  Default [DF_Appointment_IsComplete]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Appointment_IsComplete]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Appointment_IsComplete]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Appointment] ADD  CONSTRAINT [DF_Appointment_IsComplete]  DEFAULT ((0)) FOR [IsComplete]
END


End
GO
/****** Object:  Default [DF_Appointment_IsDisabled]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Appointment_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Appointment_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Appointment] ADD  CONSTRAINT [DF_Appointment_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_Appointment_CreateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Appointment_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Appointment_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Appointment] ADD  CONSTRAINT [DF_Appointment_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_Appointment_UpdateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Appointment_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Appointment_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Appointment] ADD  CONSTRAINT [DF_Appointment_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_AppointmentGroup_PriorityIndex]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_AppointmentGroup_PriorityIndex]') AND parent_object_id = OBJECT_ID(N'[dbo].[AppointmentGroup]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_AppointmentGroup_PriorityIndex]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[AppointmentGroup] ADD  CONSTRAINT [DF_AppointmentGroup_PriorityIndex]  DEFAULT ((1)) FOR [PriorityIndex]
END


End
GO
/****** Object:  Default [DF_AppointmentGroup_IsDisabled]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_AppointmentGroup_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[AppointmentGroup]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_AppointmentGroup_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[AppointmentGroup] ADD  CONSTRAINT [DF_AppointmentGroup_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_AppointmentGroup_CreateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_AppointmentGroup_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[AppointmentGroup]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_AppointmentGroup_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[AppointmentGroup] ADD  CONSTRAINT [DF_AppointmentGroup_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_AppointmentGroup_UpdateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_AppointmentGroup_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[AppointmentGroup]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_AppointmentGroup_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[AppointmentGroup] ADD  CONSTRAINT [DF_AppointmentGroup_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_UnitRoom_Priority]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UnitRoom_Priority]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoom]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UnitRoom_Priority]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorRoom] ADD  CONSTRAINT [DF_UnitRoom_Priority]  DEFAULT ((0)) FOR [Priority]
END


End
GO
/****** Object:  Default [DF_UnitRoom_IsDisabled]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UnitRoom_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoom]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UnitRoom_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorRoom] ADD  CONSTRAINT [DF_UnitRoom_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_UnitRoom_CreateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UnitRoom_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoom]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UnitRoom_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorRoom] ADD  CONSTRAINT [DF_UnitRoom_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_UnitRoom_UpdateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UnitRoom_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoom]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UnitRoom_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorRoom] ADD  CONSTRAINT [DF_UnitRoom_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_DoctorService_IsDisabled]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_DoctorService_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorService]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_DoctorService_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorService] ADD  CONSTRAINT [DF_DoctorService_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_DoctorService_CreateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_DoctorService_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorService]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_DoctorService_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorService] ADD  CONSTRAINT [DF_DoctorService_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_DoctorService_UpdateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_DoctorService_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorService]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_DoctorService_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorService] ADD  CONSTRAINT [DF_DoctorService_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_GroupRole_IsDisabled]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_GroupRole_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupRole]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GroupRole_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GroupRole] ADD  CONSTRAINT [DF_GroupRole_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_GroupRole_CreateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_GroupRole_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupRole]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GroupRole_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GroupRole] ADD  CONSTRAINT [DF_GroupRole_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_GroupRole_UpdateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_GroupRole_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupRole]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GroupRole_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GroupRole] ADD  CONSTRAINT [DF_GroupRole_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_Patient_IsFemale]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Patient_IsFemale]') AND parent_object_id = OBJECT_ID(N'[dbo].[Patient]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Patient_IsFemale]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Patient] ADD  CONSTRAINT [DF_Patient_IsFemale]  DEFAULT ((0)) FOR [IsFemale]
END


End
GO
/****** Object:  Default [DF_Patient_IsDisabled]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Patient_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Patient]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Patient_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Patient] ADD  CONSTRAINT [DF_Patient_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_Patient_CreateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Patient_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Patient]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Patient_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Patient] ADD  CONSTRAINT [DF_Patient_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_Patient_UpdateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Patient_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Patient]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Patient_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Patient] ADD  CONSTRAINT [DF_Patient_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_Roles_IsLocked]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Roles_IsLocked]') AND parent_object_id = OBJECT_ID(N'[dbo].[Role]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Roles_IsLocked]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Roles_IsLocked]  DEFAULT ((0)) FOR [IsLocked]
END


End
GO
/****** Object:  Default [DF_Roles_IsDisabled]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Roles_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Role]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Roles_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Roles_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_Roles_CreateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Roles_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Role]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Roles_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Roles_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_Roles_UpdateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Roles_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Role]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Roles_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Roles_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_RoleDetail_IsDisabled]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RoleDetail_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleDetail]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RoleDetail_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RoleDetail] ADD  CONSTRAINT [DF_RoleDetail_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_RoleDetail_CreateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RoleDetail_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleDetail]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RoleDetail_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RoleDetail] ADD  CONSTRAINT [DF_RoleDetail_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_RoleDetail_UpdateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RoleDetail_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleDetail]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RoleDetail_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RoleDetail] ADD  CONSTRAINT [DF_RoleDetail_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_Room_IsDisabled]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Room_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Room]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Room_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Room] ADD  CONSTRAINT [DF_Room_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_Room_CreateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Room_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Room]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Room_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Room] ADD  CONSTRAINT [DF_Room_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_Room_UpdateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Room_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Room]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Room_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Room] ADD  CONSTRAINT [DF_Room_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_Roster_IsDisabled]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Roster_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Roster]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Roster_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Roster] ADD  CONSTRAINT [DF_Roster_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_Roster_CreateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Roster_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Roster]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Roster_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Roster] ADD  CONSTRAINT [DF_Roster_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_Roster_UpdateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Roster_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Roster]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Roster_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Roster] ADD  CONSTRAINT [DF_Roster_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_RosterType_IsBooked]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RosterType_IsBooked]') AND parent_object_id = OBJECT_ID(N'[dbo].[RosterType]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RosterType_IsBooked]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RosterType] ADD  CONSTRAINT [DF_RosterType_IsBooked]  DEFAULT ((1)) FOR [IsBooked]
END


End
GO
/****** Object:  Default [DF_RosterType_IsDisabled]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RosterType_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[RosterType]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RosterType_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RosterType] ADD  CONSTRAINT [DF_RosterType_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_RosterType_CreateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RosterType_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[RosterType]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RosterType_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RosterType] ADD  CONSTRAINT [DF_RosterType_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_RosterType_UpdateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RosterType_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[RosterType]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RosterType_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RosterType] ADD  CONSTRAINT [DF_RosterType_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_Screen_IsDisabled]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Screen_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Screen]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Screen_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Screen] ADD  CONSTRAINT [DF_Screen_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_Screen_CreateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Screen_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Screen]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Screen_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Screen] ADD  CONSTRAINT [DF_Screen_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_Screen_UpdateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Screen_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Screen]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Screen_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Screen] ADD  CONSTRAINT [DF_Screen_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_Procedure_IsDisabled]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Procedure_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Services]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Procedure_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Services] ADD  CONSTRAINT [DF_Procedure_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_Procedure_CreateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Procedure_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Services]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Procedure_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Services] ADD  CONSTRAINT [DF_Procedure_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_Procedure_UpdateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Procedure_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Services]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Procedure_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Services] ADD  CONSTRAINT [DF_Procedure_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_Status_PriorityIndex_1]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Status_PriorityIndex_1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Status]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Status_PriorityIndex_1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Status] ADD  CONSTRAINT [DF_Status_PriorityIndex_1]  DEFAULT ((1)) FOR [PriorityIndex]
END


End
GO
/****** Object:  Default [DF_Status_IsDisabled]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Status_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Status]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Status_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Status] ADD  CONSTRAINT [DF_Status_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_Status_CreateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Status_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Status]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Status_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Status] ADD  CONSTRAINT [DF_Status_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_Status_UpdateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Status_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Status]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Status_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Status] ADD  CONSTRAINT [DF_Status_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_tblSettings_id]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblSettings_id]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblSettings]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblSettings_id]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblSettings] ADD  CONSTRAINT [DF_tblSettings_id]  DEFAULT (newid()) FOR [ID]
END


End
GO
/****** Object:  Default [DF_Units_PriorityIndex]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Units_PriorityIndex]') AND parent_object_id = OBJECT_ID(N'[dbo].[Units]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Units_PriorityIndex]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Units] ADD  CONSTRAINT [DF_Units_PriorityIndex]  DEFAULT ((1)) FOR [PriorityIndex]
END


End
GO
/****** Object:  Default [DF_Unit_IsDisabled]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Unit_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Units]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Unit_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Units] ADD  CONSTRAINT [DF_Unit_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_Unit_CreateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Unit_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Units]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Unit_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Units] ADD  CONSTRAINT [DF_Unit_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_Unit_UpdateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Unit_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Units]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Unit_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Units] ADD  CONSTRAINT [DF_Unit_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_UserGroup_IsLocked]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UserGroup_IsLocked]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserGroup]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserGroup_IsLocked]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserGroup] ADD  CONSTRAINT [DF_UserGroup_IsLocked]  DEFAULT ((0)) FOR [IsLocked]
END


End
GO
/****** Object:  Default [DF_UserGroup_IsDisabled]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UserGroup_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserGroup]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserGroup_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserGroup] ADD  CONSTRAINT [DF_UserGroup_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_UserGroup_CreateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UserGroup_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserGroup]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserGroup_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserGroup] ADD  CONSTRAINT [DF_UserGroup_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_UserGroup_UpdateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UserGroup_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserGroup]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserGroup_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserGroup] ADD  CONSTRAINT [DF_UserGroup_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_UserRole_IsDisabled]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UserRole_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserRole_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserRole] ADD  CONSTRAINT [DF_UserRole_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_UserRole_CreateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UserRole_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserRole_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserRole] ADD  CONSTRAINT [DF_UserRole_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_UserRole_UpdateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UserRole_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserRole_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserRole] ADD  CONSTRAINT [DF_UserRole_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_Table_1_IsLocked]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Table_1_IsLocked]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table_1_IsLocked]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Table_1_IsLocked]  DEFAULT ((0)) FOR [UserGroupId]
END


End
GO
/****** Object:  Default [DF_Users_IsFemale]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Users_IsFemale]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Users_IsFemale]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsFemale]  DEFAULT ((0)) FOR [IsFemale]
END


End
GO
/****** Object:  Default [DF_User_IsDisabled]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_User_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_User_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_User_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_User_CreateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_User_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_User_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_User_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_User_UpdateDate]    Script Date: 07/17/2012 22:23:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_User_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_User_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_User_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  ForeignKey [FK_Appointment_AppointmentGroup]    Script Date: 07/17/2012 22:23:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_AppointmentGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_AppointmentGroup] FOREIGN KEY([AppointmentGroupId])
REFERENCES [dbo].[AppointmentGroup] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_AppointmentGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_AppointmentGroup]
GO
/****** Object:  ForeignKey [FK_Appointment_Patient]    Script Date: 07/17/2012 22:23:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Patient]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Patient] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patient] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Patient]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_Patient]
GO
/****** Object:  ForeignKey [FK_Appointment_Procedure]    Script Date: 07/17/2012 22:23:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Procedure]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Procedure] FOREIGN KEY([ServicesId])
REFERENCES [dbo].[Services] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Procedure]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_Procedure]
GO
/****** Object:  ForeignKey [FK_Appointment_Room]    Script Date: 07/17/2012 22:23:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Room]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Room] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Room]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_Room]
GO
/****** Object:  ForeignKey [FK_Appointment_Status]    Script Date: 07/17/2012 22:23:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Status]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Status] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Status] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Status]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_Status]
GO
/****** Object:  ForeignKey [FK_Appointment_Users]    Script Date: 07/17/2012 22:23:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Users] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[Users] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_Users]
GO
/****** Object:  ForeignKey [FK_AppointmentGroup_Unit]    Script Date: 07/17/2012 22:23:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AppointmentGroup_Unit]') AND parent_object_id = OBJECT_ID(N'[dbo].[AppointmentGroup]'))
ALTER TABLE [dbo].[AppointmentGroup]  WITH CHECK ADD  CONSTRAINT [FK_AppointmentGroup_Unit] FOREIGN KEY([UnitId])
REFERENCES [dbo].[Units] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AppointmentGroup_Unit]') AND parent_object_id = OBJECT_ID(N'[dbo].[AppointmentGroup]'))
ALTER TABLE [dbo].[AppointmentGroup] CHECK CONSTRAINT [FK_AppointmentGroup_Unit]
GO
/****** Object:  ForeignKey [FK_DoctorRoom_Room]    Script Date: 07/17/2012 22:23:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoctorRoom_Room]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoom]'))
ALTER TABLE [dbo].[DoctorRoom]  WITH CHECK ADD  CONSTRAINT [FK_DoctorRoom_Room] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoctorRoom_Room]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoom]'))
ALTER TABLE [dbo].[DoctorRoom] CHECK CONSTRAINT [FK_DoctorRoom_Room]
GO
/****** Object:  ForeignKey [FK_DoctorRoom_User]    Script Date: 07/17/2012 22:23:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoctorRoom_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoom]'))
ALTER TABLE [dbo].[DoctorRoom]  WITH CHECK ADD  CONSTRAINT [FK_DoctorRoom_User] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[Users] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoctorRoom_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoom]'))
ALTER TABLE [dbo].[DoctorRoom] CHECK CONSTRAINT [FK_DoctorRoom_User]
GO
/****** Object:  ForeignKey [FK_DoctorService_Services]    Script Date: 07/17/2012 22:23:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoctorService_Services]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorService]'))
ALTER TABLE [dbo].[DoctorService]  WITH CHECK ADD  CONSTRAINT [FK_DoctorService_Services] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[Services] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoctorService_Services]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorService]'))
ALTER TABLE [dbo].[DoctorService] CHECK CONSTRAINT [FK_DoctorService_Services]
GO
/****** Object:  ForeignKey [FK_DoctorService_Users]    Script Date: 07/17/2012 22:23:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoctorService_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorService]'))
ALTER TABLE [dbo].[DoctorService]  WITH CHECK ADD  CONSTRAINT [FK_DoctorService_Users] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[Users] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoctorService_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorService]'))
ALTER TABLE [dbo].[DoctorService] CHECK CONSTRAINT [FK_DoctorService_Users]
GO
/****** Object:  ForeignKey [FK_GroupRole_Role]    Script Date: 07/17/2012 22:23:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GroupRole_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupRole]'))
ALTER TABLE [dbo].[GroupRole]  WITH CHECK ADD  CONSTRAINT [FK_GroupRole_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GroupRole_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupRole]'))
ALTER TABLE [dbo].[GroupRole] CHECK CONSTRAINT [FK_GroupRole_Role]
GO
/****** Object:  ForeignKey [FK_GroupRole_UserGroup]    Script Date: 07/17/2012 22:23:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GroupRole_UserGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupRole]'))
ALTER TABLE [dbo].[GroupRole]  WITH CHECK ADD  CONSTRAINT [FK_GroupRole_UserGroup] FOREIGN KEY([GroupId])
REFERENCES [dbo].[UserGroup] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GroupRole_UserGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupRole]'))
ALTER TABLE [dbo].[GroupRole] CHECK CONSTRAINT [FK_GroupRole_UserGroup]
GO
/****** Object:  ForeignKey [FK_RoleDetail_Role]    Script Date: 07/17/2012 22:23:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoleDetail_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleDetail]'))
ALTER TABLE [dbo].[RoleDetail]  WITH CHECK ADD  CONSTRAINT [FK_RoleDetail_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoleDetail_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleDetail]'))
ALTER TABLE [dbo].[RoleDetail] CHECK CONSTRAINT [FK_RoleDetail_Role]
GO
/****** Object:  ForeignKey [FK_RoleDetail_Screen]    Script Date: 07/17/2012 22:23:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoleDetail_Screen]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleDetail]'))
ALTER TABLE [dbo].[RoleDetail]  WITH CHECK ADD  CONSTRAINT [FK_RoleDetail_Screen] FOREIGN KEY([ScreenCode])
REFERENCES [dbo].[Screen] ([ScreenCode])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoleDetail_Screen]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleDetail]'))
ALTER TABLE [dbo].[RoleDetail] CHECK CONSTRAINT [FK_RoleDetail_Screen]
GO
/****** Object:  ForeignKey [FK_Room_Procedure]    Script Date: 07/17/2012 22:23:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Room_Procedure]') AND parent_object_id = OBJECT_ID(N'[dbo].[Room]'))
ALTER TABLE [dbo].[Room]  WITH CHECK ADD  CONSTRAINT [FK_Room_Procedure] FOREIGN KEY([ServicesId])
REFERENCES [dbo].[Services] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Room_Procedure]') AND parent_object_id = OBJECT_ID(N'[dbo].[Room]'))
ALTER TABLE [dbo].[Room] CHECK CONSTRAINT [FK_Room_Procedure]
GO
/****** Object:  ForeignKey [FK_Roster_Room]    Script Date: 07/17/2012 22:23:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Roster_Room]') AND parent_object_id = OBJECT_ID(N'[dbo].[Roster]'))
ALTER TABLE [dbo].[Roster]  WITH CHECK ADD  CONSTRAINT [FK_Roster_Room] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Roster_Room]') AND parent_object_id = OBJECT_ID(N'[dbo].[Roster]'))
ALTER TABLE [dbo].[Roster] CHECK CONSTRAINT [FK_Roster_Room]
GO
/****** Object:  ForeignKey [FK_Roster_RosterType]    Script Date: 07/17/2012 22:23:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Roster_RosterType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Roster]'))
ALTER TABLE [dbo].[Roster]  WITH CHECK ADD  CONSTRAINT [FK_Roster_RosterType] FOREIGN KEY([RosterTypeId])
REFERENCES [dbo].[RosterType] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Roster_RosterType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Roster]'))
ALTER TABLE [dbo].[Roster] CHECK CONSTRAINT [FK_Roster_RosterType]
GO
/****** Object:  ForeignKey [FK_Roster_Users]    Script Date: 07/17/2012 22:23:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Roster_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Roster]'))
ALTER TABLE [dbo].[Roster]  WITH CHECK ADD  CONSTRAINT [FK_Roster_Users] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[Users] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Roster_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Roster]'))
ALTER TABLE [dbo].[Roster] CHECK CONSTRAINT [FK_Roster_Users]
GO
/****** Object:  ForeignKey [FK_UserRole_Role]    Script Date: 07/17/2012 22:23:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserRole_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole]'))
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserRole_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole]'))
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_Role]
GO
/****** Object:  ForeignKey [FK_UserRole_User]    Script Date: 07/17/2012 22:23:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserRole_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole]'))
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserRole_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole]'))
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_User]
GO
/****** Object:  ForeignKey [FK_Users_UserGroup]    Script Date: 07/17/2012 22:23:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Users_UserGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_UserGroup] FOREIGN KEY([UserGroupId])
REFERENCES [dbo].[UserGroup] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Users_UserGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_UserGroup]
GO
