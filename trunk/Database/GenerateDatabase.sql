CREATE DATABASE [ClinicDoctor]
GO

USE [ClinicDoctor]
GO
/****** Object:  Table [dbo].[Room]    Script Date: 11/20/2011 21:21:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Room](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NULL,
	[Note] [nvarchar](500) NULL,
	[Status] [nvarchar](100) NULL,
	[IsDisabled] [bit] NULL,
	[CreateUser] [nvarchar](200) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateUser] [nvarchar](200) NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 11/20/2011 21:21:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NULL,
	[Note] [nvarchar](500) NULL,
	[IsDisabled] [bit] NULL,
	[CreateUser] [nvarchar](200) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateUser] [nvarchar](200) NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Group]    Script Date: 11/20/2011 21:21:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Group](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NULL,
	[IsDisabled] [bit] NULL,
	[CreateUser] [nvarchar](200) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateUser] [nvarchar](200) NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Functionality]    Script Date: 11/20/2011 21:21:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Functionality](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NULL,
	[Note] [nvarchar](500) NULL,
	[IsDisabled] [bit] NULL,
	[CreateUser] [nvarchar](200) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateUser] [nvarchar](200) NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_Functionality] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 11/20/2011 21:21:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NULL,
	[ColorCode] [nvarchar](10) NULL,
	[Note] [nvarchar](500) NULL,
	[IsDisabled] [bit] NULL,
	[CreateUser] [nvarchar](200) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateUser] [nvarchar](200) NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 11/20/2011 21:21:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](200) NULL,
	[LastName] [nvarchar](200) NULL,
	[Address] [nvarchar](500) NULL,
	[HomePhone] [nvarchar](20) NULL,
	[WorkPhone] [nvarchar](20) NULL,
	[CellPhone] [nvarchar](20) NULL,
	[Birthdate] [datetime] NULL,
	[IsFemale] [bit] NULL,
	[Title] [nvarchar](10) NULL,
	[Note] [nvarchar](500) NULL,
	[IsDisabled] [bit] NULL,
	[CreateUser] [nvarchar](200) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateUser] [nvarchar](200) NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RosterType]    Script Date: 11/20/2011 21:21:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RosterType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IsBooked] [bit] NULL,
	[Note] [nvarchar](500) NULL,
	[IsDisabled] [bit] NULL,
	[CreateUser] [nvarchar](200) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateUser] [nvarchar](200) NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_RosterType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roster]    Script Date: 11/20/2011 21:21:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RosterTypeId] [int] NULL,
	[Note] [nvarchar](500) NULL,
	[IsDisabled] [bit] NULL,
	[CreateUser] [nvarchar](200) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateUser] [nvarchar](200) NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_Roster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoomFunc]    Script Date: 11/20/2011 21:21:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoomFunc](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoomId] [int] NULL,
	[FuncId] [int] NULL,
	[IsDisabled] [bit] NULL,
	[CreateUser] [nvarchar](200) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateUser] [nvarchar](200) NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_RoomFunc] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Content]    Script Date: 11/20/2011 21:21:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Content](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NULL,
	[FuncId] [int] NULL,
	[Note] [nvarchar](500) NULL,
	[IsDisabled] [bit] NULL,
	[CreateUser] [nvarchar](200) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateUser] [nvarchar](200) NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_Content] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Staff]    Script Date: 11/20/2011 21:21:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staff](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](200) NULL,
	[LastName] [nvarchar](200) NULL,
	[ShortName] [nvarchar](50) NULL,
	[GroupId] [int] NOT NULL,
	[UserName] [nvarchar](200) NULL,
	[Address] [nvarchar](500) NULL,
	[HomePhone] [nvarchar](20) NULL,
	[WorkPhone] [nvarchar](20) NULL,
	[CellPhone] [nvarchar](20) NULL,
	[Birthdate] [datetime] NULL,
	[IsFemale] [bit] NULL,
	[Title] [nvarchar](10) NULL,
	[Note] [nvarchar](500) NULL,
	[IsDisabled] [bit] NULL,
	[CreateUser] [nvarchar](200) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateUser] [nvarchar](200) NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_Staff] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GroupRoles]    Script Date: 11/20/2011 21:21:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupRoles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GroupId] [int] NULL,
	[RoleId] [int] NULL,
	[IsDisabled] [bit] NULL,
	[CreateUser] [nvarchar](200) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateUser] [nvarchar](200) NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_GroupRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StaffRoles]    Script Date: 11/20/2011 21:21:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StaffRoles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StaffId] [int] NULL,
	[RoleId] [int] NULL,
	[IsDisabled] [bit] NULL,
	[CreateUser] [nvarchar](200) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateUser] [nvarchar](200) NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_StaffRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Appointment]    Script Date: 11/20/2011 21:21:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appointment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NULL,
	[ContentId] [int] NULL,
	[DoctorId] [int] NULL,
	[RoomId] [int] NULL,
	[StatusId] [int] NULL,
	[Note] [nvarchar](500) NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[IsDisabled] [bit] NULL,
	[CreateUser] [nvarchar](200) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateUser] [nvarchar](200) NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_Appointment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DoctorRoster]    Script Date: 11/20/2011 21:21:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DoctorRoster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DoctorId] [int] NULL,
	[RosterId] [int] NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[Note] [nvarchar](500) NULL,
	[IsDisabled] [bit] NULL,
	[CreateUser] [nvarchar](200) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateUser] [nvarchar](200) NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_DoctorRoster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DoctorRoom]    Script Date: 11/20/2011 21:21:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DoctorRoom](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DoctorId] [int] NULL,
	[RoomId] [int] NULL,
	[PriorityIndex] [int] NULL,
	[IsDisabled] [bit] NULL,
	[CreateUser] [nvarchar](200) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateUser] [nvarchar](200) NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_DoctorRoom] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DoctorFunc]    Script Date: 11/20/2011 21:21:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DoctorFunc](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DoctorId] [int] NULL,
	[FuncId] [int] NULL,
	[IsDisabled] [bit] NULL,
	[CreateUser] [nvarchar](200) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateUser] [nvarchar](200) NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_DoctorFunc] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NurseAppointment]    Script Date: 11/20/2011 21:21:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NurseAppointment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AppointmentId] [int] NULL,
	[NurseId] [int] NULL,
	[IsDisabled] [bit] NULL,
	[CreateUser] [nvarchar](200) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateUser] [nvarchar](200) NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_NurseAppointment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_Appointment_Content]    Script Date: 11/20/2011 21:21:32 ******/
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Content] FOREIGN KEY([ContentId])
REFERENCES [dbo].[Content] ([Id])
GO
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_Content]
GO
/****** Object:  ForeignKey [FK_Appointment_Customer]    Script Date: 11/20/2011 21:21:32 ******/
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_Customer]
GO
/****** Object:  ForeignKey [FK_Appointment_Room]    Script Date: 11/20/2011 21:21:32 ******/
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Room] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([Id])
GO
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_Room]
GO
/****** Object:  ForeignKey [FK_Appointment_Staff]    Script Date: 11/20/2011 21:21:32 ******/
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Staff] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[Staff] ([Id])
GO
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_Staff]
GO
/****** Object:  ForeignKey [FK_Appointment_Status]    Script Date: 11/20/2011 21:21:32 ******/
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Status] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Status] ([Id])
GO
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_Status]
GO
/****** Object:  ForeignKey [FK_Content_Functionality]    Script Date: 11/20/2011 21:21:32 ******/
ALTER TABLE [dbo].[Content]  WITH CHECK ADD  CONSTRAINT [FK_Content_Functionality] FOREIGN KEY([FuncId])
REFERENCES [dbo].[Functionality] ([Id])
GO
ALTER TABLE [dbo].[Content] CHECK CONSTRAINT [FK_Content_Functionality]
GO
/****** Object:  ForeignKey [FK_DoctorFunc_Functionality]    Script Date: 11/20/2011 21:21:32 ******/
ALTER TABLE [dbo].[DoctorFunc]  WITH CHECK ADD  CONSTRAINT [FK_DoctorFunc_Functionality] FOREIGN KEY([FuncId])
REFERENCES [dbo].[Functionality] ([Id])
GO
ALTER TABLE [dbo].[DoctorFunc] CHECK CONSTRAINT [FK_DoctorFunc_Functionality]
GO
/****** Object:  ForeignKey [FK_DoctorFunc_Staff]    Script Date: 11/20/2011 21:21:32 ******/
ALTER TABLE [dbo].[DoctorFunc]  WITH CHECK ADD  CONSTRAINT [FK_DoctorFunc_Staff] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[Staff] ([Id])
GO
ALTER TABLE [dbo].[DoctorFunc] CHECK CONSTRAINT [FK_DoctorFunc_Staff]
GO
/****** Object:  ForeignKey [FK_DoctorRoom_Room]    Script Date: 11/20/2011 21:21:32 ******/
ALTER TABLE [dbo].[DoctorRoom]  WITH CHECK ADD  CONSTRAINT [FK_DoctorRoom_Room] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([Id])
GO
ALTER TABLE [dbo].[DoctorRoom] CHECK CONSTRAINT [FK_DoctorRoom_Room]
GO
/****** Object:  ForeignKey [FK_DoctorRoom_Staff]    Script Date: 11/20/2011 21:21:32 ******/
ALTER TABLE [dbo].[DoctorRoom]  WITH CHECK ADD  CONSTRAINT [FK_DoctorRoom_Staff] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[Staff] ([Id])
GO
ALTER TABLE [dbo].[DoctorRoom] CHECK CONSTRAINT [FK_DoctorRoom_Staff]
GO
/****** Object:  ForeignKey [FK_DoctorRoster_Roster]    Script Date: 11/20/2011 21:21:32 ******/
ALTER TABLE [dbo].[DoctorRoster]  WITH CHECK ADD  CONSTRAINT [FK_DoctorRoster_Roster] FOREIGN KEY([RosterId])
REFERENCES [dbo].[Roster] ([Id])
GO
ALTER TABLE [dbo].[DoctorRoster] CHECK CONSTRAINT [FK_DoctorRoster_Roster]
GO
/****** Object:  ForeignKey [FK_DoctorRoster_Staff]    Script Date: 11/20/2011 21:21:32 ******/
ALTER TABLE [dbo].[DoctorRoster]  WITH CHECK ADD  CONSTRAINT [FK_DoctorRoster_Staff] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[Staff] ([Id])
GO
ALTER TABLE [dbo].[DoctorRoster] CHECK CONSTRAINT [FK_DoctorRoster_Staff]
GO
/****** Object:  ForeignKey [FK_GroupRoles_Group]    Script Date: 11/20/2011 21:21:32 ******/
ALTER TABLE [dbo].[GroupRoles]  WITH CHECK ADD  CONSTRAINT [FK_GroupRoles_Group] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Group] ([Id])
GO
ALTER TABLE [dbo].[GroupRoles] CHECK CONSTRAINT [FK_GroupRoles_Group]
GO
/****** Object:  ForeignKey [FK_GroupRoles_Role]    Script Date: 11/20/2011 21:21:32 ******/
ALTER TABLE [dbo].[GroupRoles]  WITH CHECK ADD  CONSTRAINT [FK_GroupRoles_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[GroupRoles] CHECK CONSTRAINT [FK_GroupRoles_Role]
GO
/****** Object:  ForeignKey [FK_NurseAppointment_Appointment]    Script Date: 11/20/2011 21:21:32 ******/
ALTER TABLE [dbo].[NurseAppointment]  WITH CHECK ADD  CONSTRAINT [FK_NurseAppointment_Appointment] FOREIGN KEY([AppointmentId])
REFERENCES [dbo].[Appointment] ([Id])
GO
ALTER TABLE [dbo].[NurseAppointment] CHECK CONSTRAINT [FK_NurseAppointment_Appointment]
GO
/****** Object:  ForeignKey [FK_NurseAppointment_Staff]    Script Date: 11/20/2011 21:21:32 ******/
ALTER TABLE [dbo].[NurseAppointment]  WITH CHECK ADD  CONSTRAINT [FK_NurseAppointment_Staff] FOREIGN KEY([NurseId])
REFERENCES [dbo].[Staff] ([Id])
GO
ALTER TABLE [dbo].[NurseAppointment] CHECK CONSTRAINT [FK_NurseAppointment_Staff]
GO
/****** Object:  ForeignKey [FK_RoomFunc_Functionality]    Script Date: 11/20/2011 21:21:32 ******/
ALTER TABLE [dbo].[RoomFunc]  WITH CHECK ADD  CONSTRAINT [FK_RoomFunc_Functionality] FOREIGN KEY([FuncId])
REFERENCES [dbo].[Functionality] ([Id])
GO
ALTER TABLE [dbo].[RoomFunc] CHECK CONSTRAINT [FK_RoomFunc_Functionality]
GO
/****** Object:  ForeignKey [FK_RoomFunc_Room]    Script Date: 11/20/2011 21:21:32 ******/
ALTER TABLE [dbo].[RoomFunc]  WITH CHECK ADD  CONSTRAINT [FK_RoomFunc_Room] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([Id])
GO
ALTER TABLE [dbo].[RoomFunc] CHECK CONSTRAINT [FK_RoomFunc_Room]
GO
/****** Object:  ForeignKey [FK_Roster_RosterType]    Script Date: 11/20/2011 21:21:32 ******/
ALTER TABLE [dbo].[Roster]  WITH CHECK ADD  CONSTRAINT [FK_Roster_RosterType] FOREIGN KEY([RosterTypeId])
REFERENCES [dbo].[RosterType] ([Id])
GO
ALTER TABLE [dbo].[Roster] CHECK CONSTRAINT [FK_Roster_RosterType]
GO
/****** Object:  ForeignKey [FK_Staff_Group]    Script Date: 11/20/2011 21:21:32 ******/
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [FK_Staff_Group] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Group] ([Id])
GO
ALTER TABLE [dbo].[Staff] CHECK CONSTRAINT [FK_Staff_Group]
GO
/****** Object:  ForeignKey [FK_StaffRoles_Role]    Script Date: 11/20/2011 21:21:32 ******/
ALTER TABLE [dbo].[StaffRoles]  WITH CHECK ADD  CONSTRAINT [FK_StaffRoles_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[StaffRoles] CHECK CONSTRAINT [FK_StaffRoles_Role]
GO
/****** Object:  ForeignKey [FK_StaffRoles_Staff]    Script Date: 11/20/2011 21:21:32 ******/
ALTER TABLE [dbo].[StaffRoles]  WITH CHECK ADD  CONSTRAINT [FK_StaffRoles_Staff] FOREIGN KEY([StaffId])
REFERENCES [dbo].[Staff] ([Id])
GO
ALTER TABLE [dbo].[StaffRoles] CHECK CONSTRAINT [FK_StaffRoles_Staff]
GO
