/****** Object:  ForeignKey [FK_Appointment_Content]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Content]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment] DROP CONSTRAINT [FK_Appointment_Content]
GO
/****** Object:  ForeignKey [FK_Appointment_Customer]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Customer]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment] DROP CONSTRAINT [FK_Appointment_Customer]
GO
/****** Object:  ForeignKey [FK_Appointment_Room]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Room]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment] DROP CONSTRAINT [FK_Appointment_Room]
GO
/****** Object:  ForeignKey [FK_Appointment_Staff]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Staff]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment] DROP CONSTRAINT [FK_Appointment_Staff]
GO
/****** Object:  ForeignKey [FK_Appointment_Staff1]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Staff1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment] DROP CONSTRAINT [FK_Appointment_Staff1]
GO
/****** Object:  ForeignKey [FK_Appointment_Status]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Status]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment] DROP CONSTRAINT [FK_Appointment_Status]
GO
/****** Object:  ForeignKey [FK_Content_Functionality]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Content_Functionality]') AND parent_object_id = OBJECT_ID(N'[dbo].[Content]'))
ALTER TABLE [dbo].[Content] DROP CONSTRAINT [FK_Content_Functionality]
GO
/****** Object:  ForeignKey [FK_DoctorFunc_Functionality]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoctorFunc_Functionality]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorFunc]'))
ALTER TABLE [dbo].[DoctorFunc] DROP CONSTRAINT [FK_DoctorFunc_Functionality]
GO
/****** Object:  ForeignKey [FK_DoctorFunc_Staff]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoctorFunc_Staff]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorFunc]'))
ALTER TABLE [dbo].[DoctorFunc] DROP CONSTRAINT [FK_DoctorFunc_Staff]
GO
/****** Object:  ForeignKey [FK_DoctorRoom_Room]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoctorRoom_Room]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoom]'))
ALTER TABLE [dbo].[DoctorRoom] DROP CONSTRAINT [FK_DoctorRoom_Room]
GO
/****** Object:  ForeignKey [FK_DoctorRoom_Staff]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoctorRoom_Staff]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoom]'))
ALTER TABLE [dbo].[DoctorRoom] DROP CONSTRAINT [FK_DoctorRoom_Staff]
GO
/****** Object:  ForeignKey [FK_DoctorRoster_RosterType]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoctorRoster_RosterType]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoster]'))
ALTER TABLE [dbo].[DoctorRoster] DROP CONSTRAINT [FK_DoctorRoster_RosterType]
GO
/****** Object:  ForeignKey [FK_DoctorRoster_Staff]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoctorRoster_Staff]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoster]'))
ALTER TABLE [dbo].[DoctorRoster] DROP CONSTRAINT [FK_DoctorRoster_Staff]
GO
/****** Object:  ForeignKey [FK_RoomFunc_Functionality]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoomFunc_Functionality]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoomFunc]'))
ALTER TABLE [dbo].[RoomFunc] DROP CONSTRAINT [FK_RoomFunc_Functionality]
GO
/****** Object:  ForeignKey [FK_RoomFunc_Room]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoomFunc_Room]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoomFunc]'))
ALTER TABLE [dbo].[RoomFunc] DROP CONSTRAINT [FK_RoomFunc_Room]
GO
/****** Object:  Table [dbo].[Appointment]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Appointment]') AND type in (N'U'))
DROP TABLE [dbo].[Appointment]
GO
/****** Object:  Table [dbo].[Content]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Content]') AND type in (N'U'))
DROP TABLE [dbo].[Content]
GO
/****** Object:  Table [dbo].[DoctorFunc]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorFunc]') AND type in (N'U'))
DROP TABLE [dbo].[DoctorFunc]
GO
/****** Object:  Table [dbo].[DoctorRoom]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoom]') AND type in (N'U'))
DROP TABLE [dbo].[DoctorRoom]
GO
/****** Object:  Table [dbo].[DoctorRoster]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoster]') AND type in (N'U'))
DROP TABLE [dbo].[DoctorRoster]
GO
/****** Object:  Table [dbo].[RoomFunc]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoomFunc]') AND type in (N'U'))
DROP TABLE [dbo].[RoomFunc]
GO
/****** Object:  StoredProcedure [dbo].[GetStaffBySingleRoleByIsDisabled]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetStaffBySingleRoleByIsDisabled]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetStaffBySingleRoleByIsDisabled]
GO
/****** Object:  Table [dbo].[Room]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Room]') AND type in (N'U'))
DROP TABLE [dbo].[Room]
GO
/****** Object:  Table [dbo].[RosterType]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RosterType]') AND type in (N'U'))
DROP TABLE [dbo].[RosterType]
GO
/****** Object:  Table [dbo].[Staff]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Staff]') AND type in (N'U'))
DROP TABLE [dbo].[Staff]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Status]') AND type in (N'U'))
DROP TABLE [dbo].[Status]
GO
/****** Object:  Table [dbo].[tblSettings]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblSettings]') AND type in (N'U'))
DROP TABLE [dbo].[tblSettings]
GO
/****** Object:  Table [dbo].[Functionality]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Functionality]') AND type in (N'U'))
DROP TABLE [dbo].[Functionality]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND type in (N'U'))
DROP TABLE [dbo].[Customer]
GO
/****** Object:  Default [DF_Appointment_IsComplete]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Appointment_IsComplete]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Appointment_IsComplete]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Appointment] DROP CONSTRAINT [DF_Appointment_IsComplete]
END


End
GO
/****** Object:  Default [DF_Appointment_IsDisabled]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Appointment_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Appointment_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Appointment] DROP CONSTRAINT [DF_Appointment_IsDisabled]
END


End
GO
/****** Object:  Default [DF_Appointment_CreateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Appointment_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Appointment_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Appointment] DROP CONSTRAINT [DF_Appointment_CreateDate]
END


End
GO
/****** Object:  Default [DF_Appointment_UpdateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Appointment_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Appointment_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Appointment] DROP CONSTRAINT [DF_Appointment_UpdateDate]
END


End
GO
/****** Object:  Default [DF_Content_IsDisabled]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Content_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Content]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Content_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Content] DROP CONSTRAINT [DF_Content_IsDisabled]
END


End
GO
/****** Object:  Default [DF_Content_CreateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Content_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Content]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Content_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Content] DROP CONSTRAINT [DF_Content_CreateDate]
END


End
GO
/****** Object:  Default [DF_Content_UpdateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Content_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Content]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Content_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Content] DROP CONSTRAINT [DF_Content_UpdateDate]
END


End
GO
/****** Object:  Default [DF_Customer_IsFemale]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Customer_IsFemale]') AND parent_object_id = OBJECT_ID(N'[dbo].[Customer]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Customer_IsFemale]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Customer] DROP CONSTRAINT [DF_Customer_IsFemale]
END


End
GO
/****** Object:  Default [DF_Customer_IsDisabled]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Customer_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Customer]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Customer_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Customer] DROP CONSTRAINT [DF_Customer_IsDisabled]
END


End
GO
/****** Object:  Default [DF_Customer_CreateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Customer_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Customer]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Customer_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Customer] DROP CONSTRAINT [DF_Customer_CreateDate]
END


End
GO
/****** Object:  Default [DF_Customer_UpdateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Customer_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Customer]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Customer_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Customer] DROP CONSTRAINT [DF_Customer_UpdateDate]
END


End
GO
/****** Object:  Default [DF_DoctorFunc_IsDisabled]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_DoctorFunc_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorFunc]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_DoctorFunc_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorFunc] DROP CONSTRAINT [DF_DoctorFunc_IsDisabled]
END


End
GO
/****** Object:  Default [DF_DoctorFunc_CreateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_DoctorFunc_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorFunc]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_DoctorFunc_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorFunc] DROP CONSTRAINT [DF_DoctorFunc_CreateDate]
END


End
GO
/****** Object:  Default [DF_DoctorFunc_UpdateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_DoctorFunc_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorFunc]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_DoctorFunc_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorFunc] DROP CONSTRAINT [DF_DoctorFunc_UpdateDate]
END


End
GO
/****** Object:  Default [DF_DoctorRoom_PriorityIndex]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_DoctorRoom_PriorityIndex]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoom]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_DoctorRoom_PriorityIndex]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorRoom] DROP CONSTRAINT [DF_DoctorRoom_PriorityIndex]
END


End
GO
/****** Object:  Default [DF_DoctorRoom_IsDisabled]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_DoctorRoom_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoom]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_DoctorRoom_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorRoom] DROP CONSTRAINT [DF_DoctorRoom_IsDisabled]
END


End
GO
/****** Object:  Default [DF_DoctorRoom_CreateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_DoctorRoom_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoom]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_DoctorRoom_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorRoom] DROP CONSTRAINT [DF_DoctorRoom_CreateDate]
END


End
GO
/****** Object:  Default [DF_DoctorRoom_UpdateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_DoctorRoom_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoom]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_DoctorRoom_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorRoom] DROP CONSTRAINT [DF_DoctorRoom_UpdateDate]
END


End
GO
/****** Object:  Default [DF_DoctorRoster_IsBooked]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_DoctorRoster_IsBooked]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoster]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_DoctorRoster_IsBooked]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorRoster] DROP CONSTRAINT [DF_DoctorRoster_IsBooked]
END


End
GO
/****** Object:  Default [DF_DoctorRoster_IsComplete]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_DoctorRoster_IsComplete]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoster]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_DoctorRoster_IsComplete]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorRoster] DROP CONSTRAINT [DF_DoctorRoster_IsComplete]
END


End
GO
/****** Object:  Default [DF_DoctorRoster_IsDisabled]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_DoctorRoster_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoster]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_DoctorRoster_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorRoster] DROP CONSTRAINT [DF_DoctorRoster_IsDisabled]
END


End
GO
/****** Object:  Default [DF_DoctorRoster_CreateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_DoctorRoster_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoster]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_DoctorRoster_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorRoster] DROP CONSTRAINT [DF_DoctorRoster_CreateDate]
END


End
GO
/****** Object:  Default [DF_DoctorRoster_UpdateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_DoctorRoster_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoster]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_DoctorRoster_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorRoster] DROP CONSTRAINT [DF_DoctorRoster_UpdateDate]
END


End
GO
/****** Object:  Default [DF_Functionality_IsDisabled]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Functionality_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Functionality]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Functionality_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Functionality] DROP CONSTRAINT [DF_Functionality_IsDisabled]
END


End
GO
/****** Object:  Default [DF_Functionality_CreateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Functionality_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Functionality]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Functionality_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Functionality] DROP CONSTRAINT [DF_Functionality_CreateDate]
END


End
GO
/****** Object:  Default [DF_Functionality_UpdateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Functionality_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Functionality]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Functionality_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Functionality] DROP CONSTRAINT [DF_Functionality_UpdateDate]
END


End
GO
/****** Object:  Default [DF_Room_IsDisabled]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Room_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Room]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Room_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Room] DROP CONSTRAINT [DF_Room_IsDisabled]
END


End
GO
/****** Object:  Default [DF_Room_CreateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Room_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Room]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Room_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Room] DROP CONSTRAINT [DF_Room_CreateDate]
END


End
GO
/****** Object:  Default [DF_Room_UpdateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Room_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Room]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Room_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Room] DROP CONSTRAINT [DF_Room_UpdateDate]
END


End
GO
/****** Object:  Default [DF_RoomFunc_IsDisabled]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RoomFunc_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoomFunc]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RoomFunc_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RoomFunc] DROP CONSTRAINT [DF_RoomFunc_IsDisabled]
END


End
GO
/****** Object:  Default [DF_RoomFunc_CreateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RoomFunc_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoomFunc]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RoomFunc_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RoomFunc] DROP CONSTRAINT [DF_RoomFunc_CreateDate]
END


End
GO
/****** Object:  Default [DF_RoomFunc_UpdateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RoomFunc_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoomFunc]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RoomFunc_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RoomFunc] DROP CONSTRAINT [DF_RoomFunc_UpdateDate]
END


End
GO
/****** Object:  Default [DF_RosterType_IsBooked]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RosterType_IsBooked]') AND parent_object_id = OBJECT_ID(N'[dbo].[RosterType]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RosterType_IsBooked]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RosterType] DROP CONSTRAINT [DF_RosterType_IsBooked]
END


End
GO
/****** Object:  Default [DF_RosterType_IsDisabled]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RosterType_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[RosterType]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RosterType_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RosterType] DROP CONSTRAINT [DF_RosterType_IsDisabled]
END


End
GO
/****** Object:  Default [DF_RosterType_CreateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RosterType_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[RosterType]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RosterType_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RosterType] DROP CONSTRAINT [DF_RosterType_CreateDate]
END


End
GO
/****** Object:  Default [DF_RosterType_UpdateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RosterType_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[RosterType]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RosterType_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RosterType] DROP CONSTRAINT [DF_RosterType_UpdateDate]
END


End
GO
/****** Object:  Default [DF_Staff_IsFemale]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Staff_IsFemale]') AND parent_object_id = OBJECT_ID(N'[dbo].[Staff]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Staff_IsFemale]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Staff] DROP CONSTRAINT [DF_Staff_IsFemale]
END


End
GO
/****** Object:  Default [DF_Staff_IsDisabled]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Staff_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Staff]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Staff_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Staff] DROP CONSTRAINT [DF_Staff_IsDisabled]
END


End
GO
/****** Object:  Default [DF_Staff_CreateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Staff_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Staff]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Staff_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Staff] DROP CONSTRAINT [DF_Staff_CreateDate]
END


End
GO
/****** Object:  Default [DF_Staff_UpdateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Staff_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Staff]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Staff_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Staff] DROP CONSTRAINT [DF_Staff_UpdateDate]
END


End
GO
/****** Object:  Default [DF_Status_IsDisabled]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Status_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Status]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Status_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Status] DROP CONSTRAINT [DF_Status_IsDisabled]
END


End
GO
/****** Object:  Default [DF_Status_CreateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Status_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Status]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Status_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Status] DROP CONSTRAINT [DF_Status_CreateDate]
END


End
GO
/****** Object:  Default [DF_Status_UpdateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Status_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Status]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Status_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Status] DROP CONSTRAINT [DF_Status_UpdateDate]
END


End
GO
/****** Object:  Default [DF_tblSettings_id]    Script Date: 12/13/2011 23:00:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblSettings_id]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblSettings]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblSettings_id]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblSettings] DROP CONSTRAINT [DF_tblSettings_id]
END


End
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 12/13/2011 23:00:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Customer](
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
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [IX_Customer_Id_IsDisabled] UNIQUE NONCLUSTERED 
(
	[Id] ASC,
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND name = N'IX_Customer_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Customer_IsDisabled] ON [dbo].[Customer] 
(
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND name = N'IX_Customer_IsFemale')
CREATE NONCLUSTERED INDEX [IX_Customer_IsFemale] ON [dbo].[Customer] 
(
	[IsFemale] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND name = N'IX_Customer_IsFemale_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Customer_IsFemale_IsDisabled] ON [dbo].[Customer] 
(
	[IsFemale] ASC,
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
INSERT [dbo].[Customer] ([Id], [FirstName], [LastName], [Address], [HomePhone], [WorkPhone], [CellPhone], [Birthdate], [IsFemale], [Title], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'PTT0001', N'Wayne', N'Rooney', NULL, NULL, NULL, NULL, NULL, 0, N'Mr', NULL, 0, NULL, CAST(0x00009FB701639D4E AS DateTime), NULL, CAST(0x00009FB701639D4E AS DateTime))
INSERT [dbo].[Customer] ([Id], [FirstName], [LastName], [Address], [HomePhone], [WorkPhone], [CellPhone], [Birthdate], [IsFemale], [Title], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'PTT0002', N'Cris', N'Ronaldo', NULL, NULL, NULL, NULL, NULL, 0, N'Mr', NULL, 0, NULL, CAST(0x00009FB70163B099 AS DateTime), NULL, CAST(0x00009FB70163B099 AS DateTime))
INSERT [dbo].[Customer] ([Id], [FirstName], [LastName], [Address], [HomePhone], [WorkPhone], [CellPhone], [Birthdate], [IsFemale], [Title], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'PTT0003', N'Angelina', N'Jolie', NULL, NULL, NULL, NULL, NULL, 0, N'Ms', NULL, 0, NULL, CAST(0x00009FB70163C03A AS DateTime), NULL, CAST(0x00009FB70163C03A AS DateTime))
/****** Object:  Table [dbo].[Functionality]    Script Date: 12/13/2011 23:00:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Functionality]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Functionality](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Note] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Functionality] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [IX_Functionality_Id_IsDisabled] UNIQUE NONCLUSTERED 
(
	[Id] ASC,
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Functionality]') AND name = N'IX_Functionality_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Functionality_IsDisabled] ON [dbo].[Functionality] 
(
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
SET IDENTITY_INSERT [dbo].[Functionality] ON
INSERT [dbo].[Functionality] ([Id], [Title], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (1, N'Thần kinh', N'', 0, N'GOWU', CAST(0x00009FAE0007ED88 AS DateTime), N'GOWU', CAST(0x00009FAE0007ED88 AS DateTime))
INSERT [dbo].[Functionality] ([Id], [Title], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (2, N'Răng hàm mặt', NULL, 0, N'GOWU', CAST(0x00009FB101192D41 AS DateTime), N'GOWU', CAST(0x00009FB101192D41 AS DateTime))
INSERT [dbo].[Functionality] ([Id], [Title], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (3, N'Ngoại', NULL, 0, N'GOWU', CAST(0x00009FB101194178 AS DateTime), N'GOWU', CAST(0x00009FB101194178 AS DateTime))
SET IDENTITY_INSERT [dbo].[Functionality] OFF
/****** Object:  Table [dbo].[tblSettings]    Script Date: 12/13/2011 23:00:29 ******/
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
INSERT [dbo].[tblSettings] ([ID], [Type], [Code], [ValueString], [ValueBinary]) VALUES (N'1caecf1e-5c69-4aeb-a3c4-076d8cb53016', 0, N'ROLES', N'Admin;Manager;Doctor;Nurse;Receptionist', NULL)
INSERT [dbo].[tblSettings] ([ID], [Type], [Code], [ValueString], [ValueBinary]) VALUES (N'44131fb2-5fba-4ed5-862a-286bff4a4cbc', 0, N'MINUTE_STEP', N'15', NULL)
INSERT [dbo].[tblSettings] ([ID], [Type], [Code], [ValueString], [ValueBinary]) VALUES (N'5a359e4a-eda5-44ba-8d8b-2b5985968cea', 0, N'COMPLETE_COLOR', N'#BBBBBB', NULL)
INSERT [dbo].[tblSettings] ([ID], [Type], [Code], [ValueString], [ValueBinary]) VALUES (N'c590be27-da4b-419b-9138-3987ff5ba767', 0, N'ROSTER_PREFIX', N'ost', NULL)
INSERT [dbo].[tblSettings] ([ID], [Type], [Code], [ValueString], [ValueBinary]) VALUES (N'fda005c4-f591-4b0d-a4e0-6d51e8f89545', 0, N'MAX_HOUR', N'24', NULL)
INSERT [dbo].[tblSettings] ([ID], [Type], [Code], [ValueString], [ValueBinary]) VALUES (N'2787e4e3-4648-4bfd-8aa2-b5e080301368', 0, N'MAX_MINUTE', N'60', NULL)
INSERT [dbo].[tblSettings] ([ID], [Type], [Code], [ValueString], [ValueBinary]) VALUES (N'c1e88cbe-fba2-4976-9f55-c0e4077de035', 0, N'UNCOMPLETE_COLOR', N'#A8D4FF', NULL)
/****** Object:  Table [dbo].[Status]    Script Date: 12/13/2011 23:00:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Status]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Status](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ColorCode] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Note] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [IX_Status_Id_IsDisabled] UNIQUE NONCLUSTERED 
(
	[Id] ASC,
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Status]') AND name = N'IX_Status_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Status_IsDisabled] ON [dbo].[Status] 
(
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
SET IDENTITY_INSERT [dbo].[Status] ON
INSERT [dbo].[Status] ([Id], [Title], [ColorCode], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (3, N'Cancel', N'#FF0000', N'Cancel', 0, N'GOWU', CAST(0x00009FA800000000 AS DateTime), N'GOWU', CAST(0x00009FA800000000 AS DateTime))
INSERT [dbo].[Status] ([Id], [Title], [ColorCode], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (4, N'Complete', N'#ABCDEF', NULL, 0, N'GOWU', CAST(0x00009FA800000000 AS DateTime), N'GOWU', CAST(0x00009FA800000000 AS DateTime))
SET IDENTITY_INSERT [dbo].[Status] OFF
/****** Object:  Table [dbo].[Staff]    Script Date: 12/13/2011 23:00:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Staff]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Staff](
	[Id] [bigint] NOT NULL,
	[FirstName] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[LastName] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ShortName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[UserName] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Address] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[HomePhone] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[WorkPhone] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CellPhone] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Birthdate] [datetime] NULL,
	[IsFemale] [bit] NOT NULL,
	[Title] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Note] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Roles] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Staff] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [IX_Staff_Id_IsDisabled] UNIQUE NONCLUSTERED 
(
	[Id] ASC,
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [IX_Staff_UserName] UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [IX_Staff_UserName_IsDisabled] UNIQUE NONCLUSTERED 
(
	[UserName] ASC,
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Staff]') AND name = N'IX_Staff_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Staff_IsDisabled] ON [dbo].[Staff] 
(
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Staff]') AND name = N'IX_Staff_IsFemale')
CREATE NONCLUSTERED INDEX [IX_Staff_IsFemale] ON [dbo].[Staff] 
(
	[IsFemale] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Staff]') AND name = N'IX_Staff_IsFemale_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Staff_IsFemale_IsDisabled] ON [dbo].[Staff] 
(
	[IsFemale] ASC,
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
INSERT [dbo].[Staff] ([Id], [FirstName], [LastName], [ShortName], [UserName], [Address], [HomePhone], [WorkPhone], [CellPhone], [Birthdate], [IsFemale], [Title], [Note], [Roles], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (4, N'Vo', N'Phat', N'Phát', N'GOWU', N'TpHCM', NULL, NULL, NULL, NULL, 0, N'Dr', NULL, N'doctor;admin;manager', 0, N'GOWU', CAST(0x00009FA800000000 AS DateTime), N'GOWU', CAST(0x00009FA800000000 AS DateTime))
INSERT [dbo].[Staff] ([Id], [FirstName], [LastName], [ShortName], [UserName], [Address], [HomePhone], [WorkPhone], [CellPhone], [Birthdate], [IsFemale], [Title], [Note], [Roles], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (5, N'Vo', N'Phat', N'Freeman', N'phatvt', N'TpHCM', NULL, NULL, NULL, NULL, 0, N'Dr', NULL, N'doctor;admin;manager', 0, N'GOWU', CAST(0x00009FA800000000 AS DateTime), N'GOWU', CAST(0x00009FA800000000 AS DateTime))
INSERT [dbo].[Staff] ([Id], [FirstName], [LastName], [ShortName], [UserName], [Address], [HomePhone], [WorkPhone], [CellPhone], [Birthdate], [IsFemale], [Title], [Note], [Roles], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (6, N'huy', N'lam', N'huy', N'lamhuy', N'TpHCM', NULL, NULL, NULL, NULL, 0, N'Dr', NULL, N'doctor', 0, N'GOWU', CAST(0x00009FA800000000 AS DateTime), N'GOWU', CAST(0x00009FA800000000 AS DateTime))
/****** Object:  Table [dbo].[RosterType]    Script Date: 12/13/2011 23:00:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RosterType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RosterType](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[IsBooked] [bit] NOT NULL,
	[ColorCode] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
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
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RosterType]') AND name = N'IX_RosterType_IsBooked')
CREATE NONCLUSTERED INDEX [IX_RosterType_IsBooked] ON [dbo].[RosterType] 
(
	[IsBooked] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RosterType]') AND name = N'IX_RosterType_IsBooked_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_RosterType_IsBooked_IsDisabled] ON [dbo].[RosterType] 
(
	[IsBooked] ASC,
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RosterType]') AND name = N'IX_RosterType_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_RosterType_IsDisabled] ON [dbo].[RosterType] 
(
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
SET IDENTITY_INSERT [dbo].[RosterType] ON
INSERT [dbo].[RosterType] ([Id], [Title], [IsBooked], [ColorCode], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (3, N'Khám bệnh', 1, N'#7AFF51', N'Khám bệnh', 0, N'GOWU', CAST(0x00009FA800000000 AS DateTime), N'GOWU', CAST(0x00009FA800000000 AS DateTime))
INSERT [dbo].[RosterType] ([Id], [Title], [IsBooked], [ColorCode], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (4, N'Công tác', 0, N'#D574DC', N'Công tác', 0, N'GOWU', CAST(0x00009FA801545FBA AS DateTime), N'GOWU', CAST(0x00009FA801545FBA AS DateTime))
SET IDENTITY_INSERT [dbo].[RosterType] OFF
/****** Object:  Table [dbo].[Room]    Script Date: 12/13/2011 23:00:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Room]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Room](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Note] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Status] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [IX_Room_Id_IsDisabled] UNIQUE NONCLUSTERED 
(
	[Id] ASC,
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [IX_Room_Id_Status] UNIQUE NONCLUSTERED 
(
	[Id] ASC,
	[Status] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [IX_Room_Id_Status_IsDisabled] UNIQUE NONCLUSTERED 
(
	[Id] ASC,
	[Status] ASC,
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Room]') AND name = N'IX_Room_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Room_IsDisabled] ON [dbo].[Room] 
(
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Room]') AND name = N'IX_Room_Status')
CREATE NONCLUSTERED INDEX [IX_Room_Status] ON [dbo].[Room] 
(
	[Status] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Room]') AND name = N'IX_Room_Status_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Room_Status_IsDisabled] ON [dbo].[Room] 
(
	[Status] ASC,
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
/****** Object:  StoredProcedure [dbo].[GetStaffBySingleRoleByIsDisabled]    Script Date: 12/13/2011 23:00:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetStaffBySingleRoleByIsDisabled]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:  Phat
-- Create date: 03/12/2011
-- Description: Get Staff list follow Roles
-- =============================================
CREATE PROCEDURE [dbo].[GetStaffBySingleRoleByIsDisabled]
 -- Add the parameters for the stored procedure here
 @SingleRole nvarchar(20) = '''',
 @IsDisabled bit = false
AS
BEGIN
 -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
 SET NOCOUNT ON;

    -- Insert statements for procedure here
 SELECT [Id]
    ,[FirstName]
    ,[LastName]
    ,[ShortName]
    ,[UserName]
    ,[Address]
    ,[HomePhone]
    ,[WorkPhone]
    ,[CellPhone]
    ,[Birthdate]
    ,[IsFemale]
    ,[Title]
    ,[Note]
    ,[Roles]
    ,[IsDisabled]
    ,[CreateUser]
    ,[CreateDate]
    ,[UpdateUser]
    ,[UpdateDate]
   FROM [dbo].[Staff]
   WHERE (@SingleRole = ''-1'' OR [Roles] LIKE ''%'' + @SingleRole + ''%'')
   AND [IsDisabled] = @IsDisabled
   ORDER BY [LastName]
END' 
END
GO
/****** Object:  Table [dbo].[RoomFunc]    Script Date: 12/13/2011 23:00:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoomFunc]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RoomFunc](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[RoomId] [bigint] NOT NULL,
	[RoomTitle] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[FuncId] [bigint] NOT NULL,
	[FuncTitle] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_RoomFunc] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [IX_RoomFunc_Id_IsDisabled] UNIQUE NONCLUSTERED 
(
	[Id] ASC,
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [IX_RoomFunc_RoomId_FuncId] UNIQUE NONCLUSTERED 
(
	[RoomId] ASC,
	[FuncId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [IX_RoomFunc_RoomId_FuncId_IsDisabled] UNIQUE NONCLUSTERED 
(
	[RoomId] ASC,
	[FuncId] ASC,
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RoomFunc]') AND name = N'IX_RoomFunc_FuncId')
CREATE NONCLUSTERED INDEX [IX_RoomFunc_FuncId] ON [dbo].[RoomFunc] 
(
	[FuncId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RoomFunc]') AND name = N'IX_RoomFunc_FuncId_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_RoomFunc_FuncId_IsDisabled] ON [dbo].[RoomFunc] 
(
	[FuncId] ASC,
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RoomFunc]') AND name = N'IX_RoomFunc_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_RoomFunc_IsDisabled] ON [dbo].[RoomFunc] 
(
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RoomFunc]') AND name = N'IX_RoomFunc_RoomId')
CREATE NONCLUSTERED INDEX [IX_RoomFunc_RoomId] ON [dbo].[RoomFunc] 
(
	[RoomId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RoomFunc]') AND name = N'IX_RoomFunc_RoomId_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_RoomFunc_RoomId_IsDisabled] ON [dbo].[RoomFunc] 
(
	[RoomId] ASC,
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
/****** Object:  Table [dbo].[DoctorRoster]    Script Date: 12/13/2011 23:00:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoster]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DoctorRoster](
	[Id] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DoctorUserName] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DoctorShortName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[RosterTypeId] [bigint] NOT NULL,
	[RosterTypeTitle] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ColorCode] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsBooked] [bit] NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[Note] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsComplete] [bit] NOT NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_DoctorRoster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [IX_DoctorRoster_Id_IsComplete] UNIQUE NONCLUSTERED 
(
	[Id] ASC,
	[IsComplete] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [IX_DoctorRoster_Id_IsComplete_IsDisabled] UNIQUE NONCLUSTERED 
(
	[Id] ASC,
	[IsComplete] ASC,
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [IX_DoctorRoster_Id_IsDisabled] UNIQUE NONCLUSTERED 
(
	[Id] ASC,
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoster]') AND name = N'IX_DoctorRoster_DoctorUserName')
CREATE NONCLUSTERED INDEX [IX_DoctorRoster_DoctorUserName] ON [dbo].[DoctorRoster] 
(
	[DoctorUserName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoster]') AND name = N'IX_DoctorRoster_DoctorUserName_IsComplete')
CREATE NONCLUSTERED INDEX [IX_DoctorRoster_DoctorUserName_IsComplete] ON [dbo].[DoctorRoster] 
(
	[DoctorUserName] ASC,
	[IsComplete] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoster]') AND name = N'IX_DoctorRoster_DoctorUserName_IsComplete_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_DoctorRoster_DoctorUserName_IsComplete_IsDisabled] ON [dbo].[DoctorRoster] 
(
	[DoctorUserName] ASC,
	[IsComplete] ASC,
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoster]') AND name = N'IX_DoctorRoster_DoctorUserName_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_DoctorRoster_DoctorUserName_IsDisabled] ON [dbo].[DoctorRoster] 
(
	[DoctorUserName] ASC,
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoster]') AND name = N'IX_DoctorRoster_DoctorUserName_RosterTypeId')
CREATE NONCLUSTERED INDEX [IX_DoctorRoster_DoctorUserName_RosterTypeId] ON [dbo].[DoctorRoster] 
(
	[DoctorUserName] ASC,
	[RosterTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoster]') AND name = N'IX_DoctorRoster_DoctorUserName_RosterTypeId_IsComplete')
CREATE NONCLUSTERED INDEX [IX_DoctorRoster_DoctorUserName_RosterTypeId_IsComplete] ON [dbo].[DoctorRoster] 
(
	[DoctorUserName] ASC,
	[RosterTypeId] ASC,
	[IsComplete] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoster]') AND name = N'IX_DoctorRoster_DoctorUserName_RosterTypeId_IsComplete_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_DoctorRoster_DoctorUserName_RosterTypeId_IsComplete_IsDisabled] ON [dbo].[DoctorRoster] 
(
	[DoctorUserName] ASC,
	[RosterTypeId] ASC,
	[IsComplete] ASC,
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoster]') AND name = N'IX_DoctorRoster_DoctorUserName_RosterTypeId_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_DoctorRoster_DoctorUserName_RosterTypeId_IsDisabled] ON [dbo].[DoctorRoster] 
(
	[DoctorUserName] ASC,
	[RosterTypeId] ASC,
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoster]') AND name = N'IX_DoctorRoster_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_DoctorRoster_IsDisabled] ON [dbo].[DoctorRoster] 
(
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoster]') AND name = N'IX_DoctorRoster_RosterTypeId')
CREATE NONCLUSTERED INDEX [IX_DoctorRoster_RosterTypeId] ON [dbo].[DoctorRoster] 
(
	[RosterTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoster]') AND name = N'IX_DoctorRoster_RosterTypeId_IsComplete')
CREATE NONCLUSTERED INDEX [IX_DoctorRoster_RosterTypeId_IsComplete] ON [dbo].[DoctorRoster] 
(
	[RosterTypeId] ASC,
	[IsComplete] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoster]') AND name = N'IX_DoctorRoster_RosterTypeId_IsComplete_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_DoctorRoster_RosterTypeId_IsComplete_IsDisabled] ON [dbo].[DoctorRoster] 
(
	[RosterTypeId] ASC,
	[IsComplete] ASC,
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
INSERT [dbo].[DoctorRoster] ([Id], [DoctorUserName], [DoctorShortName], [RosterTypeId], [RosterTypeTitle], [ColorCode], [IsBooked], [StartTime], [EndTime], [Note], [IsComplete], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'Rost111204001', N'phatvt', N'Freeman', 3, N'Khám bệnh', N'#BBBBBB', 1, CAST(0x00009FB100000000 AS DateTime), CAST(0x00009FB101876350 AS DateTime), N'dđd', 1, 0, N'LAMHUY', CAST(0x00009FAF00BD1C3E AS DateTime), N'GOWU', CAST(0x00009FB001644AD0 AS DateTime))
INSERT [dbo].[DoctorRoster] ([Id], [DoctorUserName], [DoctorShortName], [RosterTypeId], [RosterTypeTitle], [ColorCode], [IsBooked], [StartTime], [EndTime], [Note], [IsComplete], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'Rost111206001', N'GOWU', N'Phát', 4, N'Công tác', N'#BBBBBB', 1, CAST(0x00009FB400000000 AS DateTime), CAST(0x00009FB4007779F0 AS DateTime), N'test change', 1, 0, N'phatvt', CAST(0x00009FB10124BEEC AS DateTime), N'phatvt', CAST(0x00009FB1012D8B46 AS DateTime))
INSERT [dbo].[DoctorRoster] ([Id], [DoctorUserName], [DoctorShortName], [RosterTypeId], [RosterTypeTitle], [ColorCode], [IsBooked], [StartTime], [EndTime], [Note], [IsComplete], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'Rost111206002', N'phatvt', N'Freeman', 3, N'Khám bệnh', N'#7AFF51', 1, CAST(0x00009FB300000000 AS DateTime), CAST(0x00009FB300083D60 AS DateTime), N'bvnvg', 0, 1, N'phatvt', CAST(0x00009FB1012607B7 AS DateTime), N'phatvt', CAST(0x00009FB101287A08 AS DateTime))
INSERT [dbo].[DoctorRoster] ([Id], [DoctorUserName], [DoctorShortName], [RosterTypeId], [RosterTypeTitle], [ColorCode], [IsBooked], [StartTime], [EndTime], [Note], [IsComplete], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'Rost111207001', N'phatvt', N'Freeman', 3, N'Khám bệnh', N'#7AFF51', 1, CAST(0x00009FCD006B1DE0 AS DateTime), CAST(0x00009FCD009C8E20 AS DateTime), N'Khám định kỳ', 0, 0, N'phatvt', CAST(0x00009FB200C11A27 AS DateTime), N'phatvt', CAST(0x00009FB200C11A27 AS DateTime))
INSERT [dbo].[DoctorRoster] ([Id], [DoctorUserName], [DoctorShortName], [RosterTypeId], [RosterTypeTitle], [ColorCode], [IsBooked], [StartTime], [EndTime], [Note], [IsComplete], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'Rost111207002', N'phatvt', N'Freeman', 3, N'Khám bệnh', N'#7AFF51', 1, CAST(0x00009FCE006B1DE0 AS DateTime), CAST(0x00009FCE009C8E20 AS DateTime), N'Khám định kỳ', 0, 0, N'phatvt', CAST(0x00009FB200C11A35 AS DateTime), N'phatvt', CAST(0x00009FB200C11A35 AS DateTime))
INSERT [dbo].[DoctorRoster] ([Id], [DoctorUserName], [DoctorShortName], [RosterTypeId], [RosterTypeTitle], [ColorCode], [IsBooked], [StartTime], [EndTime], [Note], [IsComplete], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'Rost111207003', N'phatvt', N'Freeman', 3, N'Khám bệnh', N'#7AFF51', 1, CAST(0x00009FD0006B1DE0 AS DateTime), CAST(0x00009FD0009C8E20 AS DateTime), N'Khám định kỳ', 0, 0, N'phatvt', CAST(0x00009FB200C11A35 AS DateTime), N'phatvt', CAST(0x00009FB200C11A35 AS DateTime))
INSERT [dbo].[DoctorRoster] ([Id], [DoctorUserName], [DoctorShortName], [RosterTypeId], [RosterTypeTitle], [ColorCode], [IsBooked], [StartTime], [EndTime], [Note], [IsComplete], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'Rost111207004', N'phatvt', N'Freeman', 3, N'Khám bệnh', N'#7AFF51', 1, CAST(0x00009FD4006B1DE0 AS DateTime), CAST(0x00009FD4009C8E20 AS DateTime), N'Khám định kỳ', 0, 0, N'phatvt', CAST(0x00009FB200C11A3A AS DateTime), N'phatvt', CAST(0x00009FB200C11A3A AS DateTime))
INSERT [dbo].[DoctorRoster] ([Id], [DoctorUserName], [DoctorShortName], [RosterTypeId], [RosterTypeTitle], [ColorCode], [IsBooked], [StartTime], [EndTime], [Note], [IsComplete], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'Rost111207005', N'phatvt', N'Freeman', 3, N'Khám bệnh', N'#7AFF51', 1, CAST(0x00009FD5006B1DE0 AS DateTime), CAST(0x00009FD5009C8E20 AS DateTime), N'Khám định kỳ', 0, 0, N'phatvt', CAST(0x00009FB200C11A3F AS DateTime), N'phatvt', CAST(0x00009FB200C11A3F AS DateTime))
INSERT [dbo].[DoctorRoster] ([Id], [DoctorUserName], [DoctorShortName], [RosterTypeId], [RosterTypeTitle], [ColorCode], [IsBooked], [StartTime], [EndTime], [Note], [IsComplete], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'Rost111207006', N'phatvt', N'Freeman', 3, N'Khám bệnh', N'#7AFF51', 1, CAST(0x00009FD7006B1DE0 AS DateTime), CAST(0x00009FD7009C8E20 AS DateTime), N'Khám định kỳ', 0, 0, N'phatvt', CAST(0x00009FB200C11A48 AS DateTime), N'phatvt', CAST(0x00009FB200C11A48 AS DateTime))
INSERT [dbo].[DoctorRoster] ([Id], [DoctorUserName], [DoctorShortName], [RosterTypeId], [RosterTypeTitle], [ColorCode], [IsBooked], [StartTime], [EndTime], [Note], [IsComplete], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'Rost111207007', N'phatvt', N'Freeman', 3, N'Khám bệnh', N'#7AFF51', 1, CAST(0x00009FDB006B1DE0 AS DateTime), CAST(0x00009FDB009C8E20 AS DateTime), N'Khám định kỳ', 0, 0, N'phatvt', CAST(0x00009FB200C11A48 AS DateTime), N'phatvt', CAST(0x00009FB200C11A48 AS DateTime))
INSERT [dbo].[DoctorRoster] ([Id], [DoctorUserName], [DoctorShortName], [RosterTypeId], [RosterTypeTitle], [ColorCode], [IsBooked], [StartTime], [EndTime], [Note], [IsComplete], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'Rost111207008', N'phatvt', N'Freeman', 3, N'Khám bệnh', N'#7AFF51', 1, CAST(0x00009FDC006B1DE0 AS DateTime), CAST(0x00009FDC009C8E20 AS DateTime), N'Khám định kỳ', 0, 0, N'phatvt', CAST(0x00009FB200C11A4D AS DateTime), N'phatvt', CAST(0x00009FB200C11A4D AS DateTime))
INSERT [dbo].[DoctorRoster] ([Id], [DoctorUserName], [DoctorShortName], [RosterTypeId], [RosterTypeTitle], [ColorCode], [IsBooked], [StartTime], [EndTime], [Note], [IsComplete], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'Rost111207009', N'phatvt', N'Freeman', 3, N'Khám bệnh', N'#7AFF51', 1, CAST(0x00009FDE006B1DE0 AS DateTime), CAST(0x00009FDE009C8E20 AS DateTime), N'Khám định kỳ', 0, 0, N'phatvt', CAST(0x00009FB200C11A52 AS DateTime), N'phatvt', CAST(0x00009FB200C11A52 AS DateTime))
INSERT [dbo].[DoctorRoster] ([Id], [DoctorUserName], [DoctorShortName], [RosterTypeId], [RosterTypeTitle], [ColorCode], [IsBooked], [StartTime], [EndTime], [Note], [IsComplete], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'Rost111207010', N'phatvt', N'Freeman', 3, N'Khám bệnh', N'#7AFF51', 1, CAST(0x00009FE2006B1DE0 AS DateTime), CAST(0x00009FE2009C8E20 AS DateTime), N'Khám định kỳ', 0, 0, N'phatvt', CAST(0x00009FB200C11A52 AS DateTime), N'phatvt', CAST(0x00009FB200C11A52 AS DateTime))
INSERT [dbo].[DoctorRoster] ([Id], [DoctorUserName], [DoctorShortName], [RosterTypeId], [RosterTypeTitle], [ColorCode], [IsBooked], [StartTime], [EndTime], [Note], [IsComplete], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'Rost111207011', N'phatvt', N'Freeman', 3, N'Khám bệnh', N'#7AFF51', 1, CAST(0x00009FE3006B1DE0 AS DateTime), CAST(0x00009FE3009C8E20 AS DateTime), N'Khám định kỳ', 0, 0, N'phatvt', CAST(0x00009FB200C11A56 AS DateTime), N'phatvt', CAST(0x00009FB200C11A56 AS DateTime))
INSERT [dbo].[DoctorRoster] ([Id], [DoctorUserName], [DoctorShortName], [RosterTypeId], [RosterTypeTitle], [ColorCode], [IsBooked], [StartTime], [EndTime], [Note], [IsComplete], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'Rost111207012', N'phatvt', N'Freeman', 3, N'Khám bệnh', N'#7AFF51', 1, CAST(0x00009FE5006B1DE0 AS DateTime), CAST(0x00009FE5009C8E20 AS DateTime), N'Khám định kỳ', 0, 0, N'phatvt', CAST(0x00009FB200C11A5B AS DateTime), N'phatvt', CAST(0x00009FB200C11A5B AS DateTime))
INSERT [dbo].[DoctorRoster] ([Id], [DoctorUserName], [DoctorShortName], [RosterTypeId], [RosterTypeTitle], [ColorCode], [IsBooked], [StartTime], [EndTime], [Note], [IsComplete], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'Rost111207013', N'phatvt', N'Freeman', 3, N'Khám bệnh', N'#7AFF51', 1, CAST(0x00009FE9006B1DE0 AS DateTime), CAST(0x00009FE9009C8E20 AS DateTime), N'Khám định kỳ', 0, 0, N'phatvt', CAST(0x00009FB200C11A5B AS DateTime), N'phatvt', CAST(0x00009FB200C11A5B AS DateTime))
INSERT [dbo].[DoctorRoster] ([Id], [DoctorUserName], [DoctorShortName], [RosterTypeId], [RosterTypeTitle], [ColorCode], [IsBooked], [StartTime], [EndTime], [Note], [IsComplete], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (N'Rost111207014', N'phatvt', N'Freeman', 3, N'Khám bệnh', N'#BBBBBB', 1, CAST(0x00009FB400E297D0 AS DateTime), CAST(0x00009FB401391C40 AS DateTime), N'', 1, 0, N'phatvt', CAST(0x00009FB200C43576 AS DateTime), N'phatvt', CAST(0x00009FB200C43576 AS DateTime))
/****** Object:  Table [dbo].[DoctorRoom]    Script Date: 12/13/2011 23:00:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoom]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DoctorRoom](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DoctorUserName] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DoctorShortName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[RoomId] [bigint] NOT NULL,
	[RoomTitle] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PriorityIndex] [int] NOT NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_DoctorRoom] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [IX_DoctorRoom_Id_IsDisabled] UNIQUE NONCLUSTERED 
(
	[Id] ASC,
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoom]') AND name = N'IX_DoctorRoom_DoctorUserName')
CREATE NONCLUSTERED INDEX [IX_DoctorRoom_DoctorUserName] ON [dbo].[DoctorRoom] 
(
	[DoctorUserName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoom]') AND name = N'IX_DoctorRoom_DoctorUserName_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_DoctorRoom_DoctorUserName_IsDisabled] ON [dbo].[DoctorRoom] 
(
	[DoctorUserName] ASC,
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoom]') AND name = N'IX_DoctorRoom_DoctorUserName_RoomId')
CREATE NONCLUSTERED INDEX [IX_DoctorRoom_DoctorUserName_RoomId] ON [dbo].[DoctorRoom] 
(
	[DoctorUserName] ASC,
	[RoomId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoom]') AND name = N'IX_DoctorRoom_DoctorUserName_RoomId_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_DoctorRoom_DoctorUserName_RoomId_IsDisabled] ON [dbo].[DoctorRoom] 
(
	[DoctorUserName] ASC,
	[RoomId] ASC,
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoom]') AND name = N'IX_DoctorRoom_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_DoctorRoom_IsDisabled] ON [dbo].[DoctorRoom] 
(
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoom]') AND name = N'IX_DoctorRoom_RoomId')
CREATE NONCLUSTERED INDEX [IX_DoctorRoom_RoomId] ON [dbo].[DoctorRoom] 
(
	[RoomId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoom]') AND name = N'IX_DoctorRoom_RoomId_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_DoctorRoom_RoomId_IsDisabled] ON [dbo].[DoctorRoom] 
(
	[RoomId] ASC,
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
/****** Object:  Table [dbo].[DoctorFunc]    Script Date: 12/13/2011 23:00:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorFunc]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DoctorFunc](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DoctorUserName] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DoctorShortName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FuncId] [bigint] NOT NULL,
	[FuncTitle] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_DoctorFunc] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [IX_DoctorFunc_Id_IsDisabled] UNIQUE NONCLUSTERED 
(
	[Id] ASC,
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorFunc]') AND name = N'IX_DoctorFunc_DoctorUserName')
CREATE NONCLUSTERED INDEX [IX_DoctorFunc_DoctorUserName] ON [dbo].[DoctorFunc] 
(
	[DoctorUserName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorFunc]') AND name = N'IX_DoctorFunc_DoctorUserName_FuncId')
CREATE NONCLUSTERED INDEX [IX_DoctorFunc_DoctorUserName_FuncId] ON [dbo].[DoctorFunc] 
(
	[DoctorUserName] ASC,
	[FuncId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorFunc]') AND name = N'IX_DoctorFunc_DoctorUserName_FuncId_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_DoctorFunc_DoctorUserName_FuncId_IsDisabled] ON [dbo].[DoctorFunc] 
(
	[DoctorUserName] ASC,
	[FuncId] ASC,
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorFunc]') AND name = N'IX_DoctorFunc_DoctorUserName_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_DoctorFunc_DoctorUserName_IsDisabled] ON [dbo].[DoctorFunc] 
(
	[DoctorUserName] ASC,
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorFunc]') AND name = N'IX_DoctorFunc_FuncId')
CREATE NONCLUSTERED INDEX [IX_DoctorFunc_FuncId] ON [dbo].[DoctorFunc] 
(
	[FuncId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorFunc]') AND name = N'IX_DoctorFunc_FuncId_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_DoctorFunc_FuncId_IsDisabled] ON [dbo].[DoctorFunc] 
(
	[FuncId] ASC,
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorFunc]') AND name = N'IX_DoctorFunc_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_DoctorFunc_IsDisabled] ON [dbo].[DoctorFunc] 
(
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
SET IDENTITY_INSERT [dbo].[DoctorFunc] ON
INSERT [dbo].[DoctorFunc] ([Id], [DoctorUserName], [DoctorShortName], [FuncId], [FuncTitle], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (1, N'phatvt', N'Freeman', 1, N'Thần kinh', 0, N'GOWU', CAST(0x00009FB10119A5AE AS DateTime), N'GOWU', CAST(0x00009FB10119A5AE AS DateTime))
INSERT [dbo].[DoctorFunc] ([Id], [DoctorUserName], [DoctorShortName], [FuncId], [FuncTitle], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (2, N'GOWU', N'Phát', 2, N'Răng hàm mặt', 0, N'GOWU', CAST(0x00009FB10119C734 AS DateTime), N'GOWU', CAST(0x00009FB10119C734 AS DateTime))
SET IDENTITY_INSERT [dbo].[DoctorFunc] OFF
/****** Object:  Table [dbo].[Content]    Script Date: 12/13/2011 23:00:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Content]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Content](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[FuncId] [bigint] NOT NULL,
	[FuncTitle] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Note] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Content] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [IX_Content_Id_IsDisabled] UNIQUE NONCLUSTERED 
(
	[Id] ASC,
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Content]') AND name = N'IX_Content_FuncId')
CREATE NONCLUSTERED INDEX [IX_Content_FuncId] ON [dbo].[Content] 
(
	[FuncId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Content]') AND name = N'IX_Content_FuncId_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Content_FuncId_IsDisabled] ON [dbo].[Content] 
(
	[FuncId] ASC,
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Content]') AND name = N'IX_Content_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Content_IsDisabled] ON [dbo].[Content] 
(
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
SET IDENTITY_INSERT [dbo].[Content] ON
INSERT [dbo].[Content] ([Id], [Title], [FuncId], [FuncTitle], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (1, N'Đau đầu', 1, N'Thần kinh', NULL, 0, N'GOWU', CAST(0x00009FB800000000 AS DateTime), N'GOWU', CAST(0x00009FB800000000 AS DateTime))
INSERT [dbo].[Content] ([Id], [Title], [FuncId], [FuncTitle], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (2, N'Đau tim', 1, N'Thần kinh', NULL, 0, N'GOWU', CAST(0x00009FB800000000 AS DateTime), N'GOWU', CAST(0x00009FB800000000 AS DateTime))
INSERT [dbo].[Content] ([Id], [Title], [FuncId], [FuncTitle], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (3, N'Nứu', 2, N'Răng hàm mặt', NULL, 0, N'GOWU', CAST(0x00009FB800000000 AS DateTime), N'GOWU', CAST(0x00009FB800000000 AS DateTime))
INSERT [dbo].[Content] ([Id], [Title], [FuncId], [FuncTitle], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (4, N'Trám răng', 2, N'Răng hàm mặt', NULL, 0, N'GOWU', CAST(0x00009FB800000000 AS DateTime), N'GOWU', CAST(0x00009FB800000000 AS DateTime))
INSERT [dbo].[Content] ([Id], [Title], [FuncId], [FuncTitle], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (5, N'Da liễu', 3, N'Ngoại', NULL, 0, N'GOWU', CAST(0x00009FB800000000 AS DateTime), N'GOWU', CAST(0x00009FB800000000 AS DateTime))
INSERT [dbo].[Content] ([Id], [Title], [FuncId], [FuncTitle], [Note], [IsDisabled], [CreateUser], [CreateDate], [UpdateUser], [UpdateDate]) VALUES (6, N'Thấp khớp', 3, N'Ngoại', NULL, 0, N'GOWU', CAST(0x00009FB800000000 AS DateTime), N'GOWU', CAST(0x00009FB800000000 AS DateTime))
SET IDENTITY_INSERT [dbo].[Content] OFF
/****** Object:  Table [dbo].[Appointment]    Script Date: 12/13/2011 23:00:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Appointment]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Appointment](
	[Id] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CustomerId] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CustomerName] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContentId] [bigint] NOT NULL,
	[ContentTitle] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DoctorUsername] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DoctorShortName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[RoomId] [bigint] NULL,
	[RoomTitle] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NurseUsername] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NurseShortName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[StatusId] [bigint] NULL,
	[StatusTitle] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Note] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[ColorCode] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[IsComplete] [bit] NOT NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Appointment_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [IX_Appointment_Id_IsDisabled] UNIQUE NONCLUSTERED 
(
	[Id] ASC,
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Appointment]') AND name = N'IX_Appointment_ContentId')
CREATE NONCLUSTERED INDEX [IX_Appointment_ContentId] ON [dbo].[Appointment] 
(
	[ContentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Appointment]') AND name = N'IX_Appointment_ContentId_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Appointment_ContentId_IsDisabled] ON [dbo].[Appointment] 
(
	[ContentId] ASC,
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Appointment]') AND name = N'IX_Appointment_CustomerId')
CREATE NONCLUSTERED INDEX [IX_Appointment_CustomerId] ON [dbo].[Appointment] 
(
	[CustomerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Appointment]') AND name = N'IX_Appointment_CustomerId_ContentId')
CREATE NONCLUSTERED INDEX [IX_Appointment_CustomerId_ContentId] ON [dbo].[Appointment] 
(
	[CustomerId] ASC,
	[ContentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Appointment]') AND name = N'IX_Appointment_CustomerId_ContentId_IsComplete')
CREATE NONCLUSTERED INDEX [IX_Appointment_CustomerId_ContentId_IsComplete] ON [dbo].[Appointment] 
(
	[CustomerId] ASC,
	[ContentId] ASC,
	[IsComplete] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Appointment]') AND name = N'IX_Appointment_CustomerId_ContentId_IsComplete_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Appointment_CustomerId_ContentId_IsComplete_IsDisabled] ON [dbo].[Appointment] 
(
	[CustomerId] ASC,
	[ContentId] ASC,
	[IsComplete] ASC,
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Appointment]') AND name = N'IX_Appointment_CustomerId_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Appointment_CustomerId_IsDisabled] ON [dbo].[Appointment] 
(
	[CustomerId] ASC,
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Appointment]') AND name = N'IX_Appointment_DoctorUsername')
CREATE NONCLUSTERED INDEX [IX_Appointment_DoctorUsername] ON [dbo].[Appointment] 
(
	[DoctorUsername] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Appointment]') AND name = N'IX_Appointment_DoctorUsername_IsComplete_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Appointment_DoctorUsername_IsComplete_IsDisabled] ON [dbo].[Appointment] 
(
	[DoctorUsername] ASC,
	[IsComplete] ASC,
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Appointment]') AND name = N'IX_Appointment_DoctorUsername_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Appointment_DoctorUsername_IsDisabled] ON [dbo].[Appointment] 
(
	[DoctorUsername] ASC,
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Appointment]') AND name = N'IX_Appointment_DoctorUsername_StatusId')
CREATE NONCLUSTERED INDEX [IX_Appointment_DoctorUsername_StatusId] ON [dbo].[Appointment] 
(
	[DoctorUsername] ASC,
	[StatusId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Appointment]') AND name = N'IX_Appointment_DoctorUsername_StatusId_IsComplete')
CREATE NONCLUSTERED INDEX [IX_Appointment_DoctorUsername_StatusId_IsComplete] ON [dbo].[Appointment] 
(
	[DoctorUsername] ASC,
	[StatusId] ASC,
	[IsComplete] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Appointment]') AND name = N'IX_Appointment_DoctorUsername_StatusId_IsComplete_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Appointment_DoctorUsername_StatusId_IsComplete_IsDisabled] ON [dbo].[Appointment] 
(
	[DoctorUsername] ASC,
	[StatusId] ASC,
	[IsComplete] ASC,
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Appointment]') AND name = N'IX_Appointment_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Appointment_IsDisabled] ON [dbo].[Appointment] 
(
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Appointment]') AND name = N'IX_Appointment_RoomId')
CREATE NONCLUSTERED INDEX [IX_Appointment_RoomId] ON [dbo].[Appointment] 
(
	[RoomId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Appointment]') AND name = N'IX_Appointment_RoomId_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Appointment_RoomId_IsDisabled] ON [dbo].[Appointment] 
(
	[RoomId] ASC,
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Appointment]') AND name = N'IX_Appointment_StatusId')
CREATE NONCLUSTERED INDEX [IX_Appointment_StatusId] ON [dbo].[Appointment] 
(
	[StatusId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Appointment]') AND name = N'IX_Appointment_StatusId_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Appointment_StatusId_IsDisabled] ON [dbo].[Appointment] 
(
	[StatusId] ASC,
	[IsDisabled] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
/****** Object:  Default [DF_Appointment_IsComplete]    Script Date: 12/13/2011 23:00:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Appointment_IsComplete]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Appointment_IsComplete]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Appointment] ADD  CONSTRAINT [DF_Appointment_IsComplete]  DEFAULT ((0)) FOR [IsComplete]
END


End
GO
/****** Object:  Default [DF_Appointment_IsDisabled]    Script Date: 12/13/2011 23:00:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Appointment_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Appointment_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Appointment] ADD  CONSTRAINT [DF_Appointment_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_Appointment_CreateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Appointment_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Appointment_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Appointment] ADD  CONSTRAINT [DF_Appointment_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_Appointment_UpdateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Appointment_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Appointment_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Appointment] ADD  CONSTRAINT [DF_Appointment_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_Content_IsDisabled]    Script Date: 12/13/2011 23:00:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Content_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Content]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Content_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Content] ADD  CONSTRAINT [DF_Content_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_Content_CreateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Content_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Content]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Content_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Content] ADD  CONSTRAINT [DF_Content_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_Content_UpdateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Content_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Content]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Content_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Content] ADD  CONSTRAINT [DF_Content_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_Customer_IsFemale]    Script Date: 12/13/2011 23:00:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Customer_IsFemale]') AND parent_object_id = OBJECT_ID(N'[dbo].[Customer]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Customer_IsFemale]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_IsFemale]  DEFAULT ((0)) FOR [IsFemale]
END


End
GO
/****** Object:  Default [DF_Customer_IsDisabled]    Script Date: 12/13/2011 23:00:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Customer_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Customer]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Customer_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_Customer_CreateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Customer_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Customer]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Customer_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_Customer_UpdateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Customer_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Customer]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Customer_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_DoctorFunc_IsDisabled]    Script Date: 12/13/2011 23:00:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_DoctorFunc_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorFunc]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_DoctorFunc_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorFunc] ADD  CONSTRAINT [DF_DoctorFunc_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_DoctorFunc_CreateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_DoctorFunc_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorFunc]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_DoctorFunc_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorFunc] ADD  CONSTRAINT [DF_DoctorFunc_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_DoctorFunc_UpdateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_DoctorFunc_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorFunc]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_DoctorFunc_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorFunc] ADD  CONSTRAINT [DF_DoctorFunc_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_DoctorRoom_PriorityIndex]    Script Date: 12/13/2011 23:00:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_DoctorRoom_PriorityIndex]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoom]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_DoctorRoom_PriorityIndex]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorRoom] ADD  CONSTRAINT [DF_DoctorRoom_PriorityIndex]  DEFAULT ((1)) FOR [PriorityIndex]
END


End
GO
/****** Object:  Default [DF_DoctorRoom_IsDisabled]    Script Date: 12/13/2011 23:00:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_DoctorRoom_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoom]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_DoctorRoom_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorRoom] ADD  CONSTRAINT [DF_DoctorRoom_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_DoctorRoom_CreateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_DoctorRoom_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoom]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_DoctorRoom_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorRoom] ADD  CONSTRAINT [DF_DoctorRoom_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_DoctorRoom_UpdateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_DoctorRoom_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoom]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_DoctorRoom_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorRoom] ADD  CONSTRAINT [DF_DoctorRoom_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_DoctorRoster_IsBooked]    Script Date: 12/13/2011 23:00:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_DoctorRoster_IsBooked]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoster]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_DoctorRoster_IsBooked]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorRoster] ADD  CONSTRAINT [DF_DoctorRoster_IsBooked]  DEFAULT ((1)) FOR [IsBooked]
END


End
GO
/****** Object:  Default [DF_DoctorRoster_IsComplete]    Script Date: 12/13/2011 23:00:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_DoctorRoster_IsComplete]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoster]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_DoctorRoster_IsComplete]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorRoster] ADD  CONSTRAINT [DF_DoctorRoster_IsComplete]  DEFAULT ((0)) FOR [IsComplete]
END


End
GO
/****** Object:  Default [DF_DoctorRoster_IsDisabled]    Script Date: 12/13/2011 23:00:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_DoctorRoster_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoster]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_DoctorRoster_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorRoster] ADD  CONSTRAINT [DF_DoctorRoster_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_DoctorRoster_CreateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_DoctorRoster_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoster]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_DoctorRoster_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorRoster] ADD  CONSTRAINT [DF_DoctorRoster_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_DoctorRoster_UpdateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_DoctorRoster_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoster]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_DoctorRoster_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DoctorRoster] ADD  CONSTRAINT [DF_DoctorRoster_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_Functionality_IsDisabled]    Script Date: 12/13/2011 23:00:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Functionality_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Functionality]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Functionality_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Functionality] ADD  CONSTRAINT [DF_Functionality_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_Functionality_CreateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Functionality_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Functionality]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Functionality_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Functionality] ADD  CONSTRAINT [DF_Functionality_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_Functionality_UpdateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Functionality_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Functionality]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Functionality_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Functionality] ADD  CONSTRAINT [DF_Functionality_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_Room_IsDisabled]    Script Date: 12/13/2011 23:00:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Room_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Room]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Room_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Room] ADD  CONSTRAINT [DF_Room_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_Room_CreateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Room_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Room]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Room_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Room] ADD  CONSTRAINT [DF_Room_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_Room_UpdateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Room_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Room]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Room_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Room] ADD  CONSTRAINT [DF_Room_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_RoomFunc_IsDisabled]    Script Date: 12/13/2011 23:00:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RoomFunc_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoomFunc]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RoomFunc_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RoomFunc] ADD  CONSTRAINT [DF_RoomFunc_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_RoomFunc_CreateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RoomFunc_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoomFunc]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RoomFunc_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RoomFunc] ADD  CONSTRAINT [DF_RoomFunc_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_RoomFunc_UpdateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RoomFunc_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoomFunc]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RoomFunc_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RoomFunc] ADD  CONSTRAINT [DF_RoomFunc_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_RosterType_IsBooked]    Script Date: 12/13/2011 23:00:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RosterType_IsBooked]') AND parent_object_id = OBJECT_ID(N'[dbo].[RosterType]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RosterType_IsBooked]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RosterType] ADD  CONSTRAINT [DF_RosterType_IsBooked]  DEFAULT ((1)) FOR [IsBooked]
END


End
GO
/****** Object:  Default [DF_RosterType_IsDisabled]    Script Date: 12/13/2011 23:00:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RosterType_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[RosterType]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RosterType_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RosterType] ADD  CONSTRAINT [DF_RosterType_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_RosterType_CreateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RosterType_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[RosterType]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RosterType_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RosterType] ADD  CONSTRAINT [DF_RosterType_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_RosterType_UpdateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RosterType_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[RosterType]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RosterType_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RosterType] ADD  CONSTRAINT [DF_RosterType_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_Staff_IsFemale]    Script Date: 12/13/2011 23:00:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Staff_IsFemale]') AND parent_object_id = OBJECT_ID(N'[dbo].[Staff]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Staff_IsFemale]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Staff] ADD  CONSTRAINT [DF_Staff_IsFemale]  DEFAULT ((0)) FOR [IsFemale]
END


End
GO
/****** Object:  Default [DF_Staff_IsDisabled]    Script Date: 12/13/2011 23:00:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Staff_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Staff]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Staff_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Staff] ADD  CONSTRAINT [DF_Staff_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_Staff_CreateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Staff_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Staff]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Staff_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Staff] ADD  CONSTRAINT [DF_Staff_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_Staff_UpdateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Staff_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Staff]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Staff_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Staff] ADD  CONSTRAINT [DF_Staff_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_Status_IsDisabled]    Script Date: 12/13/2011 23:00:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Status_IsDisabled]') AND parent_object_id = OBJECT_ID(N'[dbo].[Status]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Status_IsDisabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Status] ADD  CONSTRAINT [DF_Status_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
END


End
GO
/****** Object:  Default [DF_Status_CreateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Status_CreateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Status]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Status_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Status] ADD  CONSTRAINT [DF_Status_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END


End
GO
/****** Object:  Default [DF_Status_UpdateDate]    Script Date: 12/13/2011 23:00:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Status_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Status]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Status_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Status] ADD  CONSTRAINT [DF_Status_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END


End
GO
/****** Object:  Default [DF_tblSettings_id]    Script Date: 12/13/2011 23:00:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblSettings_id]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblSettings]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblSettings_id]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblSettings] ADD  CONSTRAINT [DF_tblSettings_id]  DEFAULT (newid()) FOR [ID]
END


End
GO
/****** Object:  ForeignKey [FK_Appointment_Content]    Script Date: 12/13/2011 23:00:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Content]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Content] FOREIGN KEY([ContentId])
REFERENCES [dbo].[Content] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Content]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_Content]
GO
/****** Object:  ForeignKey [FK_Appointment_Customer]    Script Date: 12/13/2011 23:00:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Customer]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Customer]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_Customer]
GO
/****** Object:  ForeignKey [FK_Appointment_Room]    Script Date: 12/13/2011 23:00:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Room]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Room] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Room]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_Room]
GO
/****** Object:  ForeignKey [FK_Appointment_Staff]    Script Date: 12/13/2011 23:00:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Staff]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Staff] FOREIGN KEY([DoctorUsername])
REFERENCES [dbo].[Staff] ([UserName])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Staff]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_Staff]
GO
/****** Object:  ForeignKey [FK_Appointment_Staff1]    Script Date: 12/13/2011 23:00:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Staff1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Staff1] FOREIGN KEY([NurseUsername])
REFERENCES [dbo].[Staff] ([UserName])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Staff1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_Staff1]
GO
/****** Object:  ForeignKey [FK_Appointment_Status]    Script Date: 12/13/2011 23:00:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Status]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Status] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Status] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Status]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_Status]
GO
/****** Object:  ForeignKey [FK_Content_Functionality]    Script Date: 12/13/2011 23:00:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Content_Functionality]') AND parent_object_id = OBJECT_ID(N'[dbo].[Content]'))
ALTER TABLE [dbo].[Content]  WITH CHECK ADD  CONSTRAINT [FK_Content_Functionality] FOREIGN KEY([FuncId])
REFERENCES [dbo].[Functionality] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Content_Functionality]') AND parent_object_id = OBJECT_ID(N'[dbo].[Content]'))
ALTER TABLE [dbo].[Content] CHECK CONSTRAINT [FK_Content_Functionality]
GO
/****** Object:  ForeignKey [FK_DoctorFunc_Functionality]    Script Date: 12/13/2011 23:00:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoctorFunc_Functionality]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorFunc]'))
ALTER TABLE [dbo].[DoctorFunc]  WITH CHECK ADD  CONSTRAINT [FK_DoctorFunc_Functionality] FOREIGN KEY([FuncId])
REFERENCES [dbo].[Functionality] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoctorFunc_Functionality]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorFunc]'))
ALTER TABLE [dbo].[DoctorFunc] CHECK CONSTRAINT [FK_DoctorFunc_Functionality]
GO
/****** Object:  ForeignKey [FK_DoctorFunc_Staff]    Script Date: 12/13/2011 23:00:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoctorFunc_Staff]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorFunc]'))
ALTER TABLE [dbo].[DoctorFunc]  WITH CHECK ADD  CONSTRAINT [FK_DoctorFunc_Staff] FOREIGN KEY([DoctorUserName])
REFERENCES [dbo].[Staff] ([UserName])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoctorFunc_Staff]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorFunc]'))
ALTER TABLE [dbo].[DoctorFunc] CHECK CONSTRAINT [FK_DoctorFunc_Staff]
GO
/****** Object:  ForeignKey [FK_DoctorRoom_Room]    Script Date: 12/13/2011 23:00:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoctorRoom_Room]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoom]'))
ALTER TABLE [dbo].[DoctorRoom]  WITH CHECK ADD  CONSTRAINT [FK_DoctorRoom_Room] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoctorRoom_Room]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoom]'))
ALTER TABLE [dbo].[DoctorRoom] CHECK CONSTRAINT [FK_DoctorRoom_Room]
GO
/****** Object:  ForeignKey [FK_DoctorRoom_Staff]    Script Date: 12/13/2011 23:00:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoctorRoom_Staff]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoom]'))
ALTER TABLE [dbo].[DoctorRoom]  WITH CHECK ADD  CONSTRAINT [FK_DoctorRoom_Staff] FOREIGN KEY([DoctorUserName])
REFERENCES [dbo].[Staff] ([UserName])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoctorRoom_Staff]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoom]'))
ALTER TABLE [dbo].[DoctorRoom] CHECK CONSTRAINT [FK_DoctorRoom_Staff]
GO
/****** Object:  ForeignKey [FK_DoctorRoster_RosterType]    Script Date: 12/13/2011 23:00:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoctorRoster_RosterType]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoster]'))
ALTER TABLE [dbo].[DoctorRoster]  WITH CHECK ADD  CONSTRAINT [FK_DoctorRoster_RosterType] FOREIGN KEY([RosterTypeId])
REFERENCES [dbo].[RosterType] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoctorRoster_RosterType]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoster]'))
ALTER TABLE [dbo].[DoctorRoster] CHECK CONSTRAINT [FK_DoctorRoster_RosterType]
GO
/****** Object:  ForeignKey [FK_DoctorRoster_Staff]    Script Date: 12/13/2011 23:00:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoctorRoster_Staff]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoster]'))
ALTER TABLE [dbo].[DoctorRoster]  WITH CHECK ADD  CONSTRAINT [FK_DoctorRoster_Staff] FOREIGN KEY([DoctorUserName])
REFERENCES [dbo].[Staff] ([UserName])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoctorRoster_Staff]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoster]'))
ALTER TABLE [dbo].[DoctorRoster] CHECK CONSTRAINT [FK_DoctorRoster_Staff]
GO
/****** Object:  ForeignKey [FK_RoomFunc_Functionality]    Script Date: 12/13/2011 23:00:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoomFunc_Functionality]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoomFunc]'))
ALTER TABLE [dbo].[RoomFunc]  WITH CHECK ADD  CONSTRAINT [FK_RoomFunc_Functionality] FOREIGN KEY([FuncId])
REFERENCES [dbo].[Functionality] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoomFunc_Functionality]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoomFunc]'))
ALTER TABLE [dbo].[RoomFunc] CHECK CONSTRAINT [FK_RoomFunc_Functionality]
GO
/****** Object:  ForeignKey [FK_RoomFunc_Room]    Script Date: 12/13/2011 23:00:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoomFunc_Room]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoomFunc]'))
ALTER TABLE [dbo].[RoomFunc]  WITH CHECK ADD  CONSTRAINT [FK_RoomFunc_Room] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoomFunc_Room]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoomFunc]'))
ALTER TABLE [dbo].[RoomFunc] CHECK CONSTRAINT [FK_RoomFunc_Room]
GO
