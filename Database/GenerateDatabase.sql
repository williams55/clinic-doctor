USE [ClinicDoctor]
GO
/****** Object:  Table [dbo].[Group]    Script Date: 11/26/2011 11:26:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Group](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Functionality]    Script Date: 11/26/2011 11:26:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Functionality](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Note] [nvarchar](500) NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Functionality] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Room]    Script Date: 11/26/2011 11:26:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Room](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Note] [nvarchar](500) NULL,
	[Status] [nvarchar](100) NOT NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 11/26/2011 11:26:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Note] [nvarchar](500) NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 11/26/2011 11:26:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](200) NOT NULL,
	[LastName] [nvarchar](200) NOT NULL,
	[Address] [nvarchar](500) NULL,
	[HomePhone] [nvarchar](20) NULL,
	[WorkPhone] [nvarchar](20) NULL,
	[CellPhone] [nvarchar](20) NULL,
	[Birthdate] [datetime] NULL,
	[IsFemale] [bit] NOT NULL,
	[Title] [nvarchar](10) NULL,
	[Note] [nvarchar](500) NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 11/26/2011 11:26:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[ColorCode] [nvarchar](10) NOT NULL,
	[Note] [nvarchar](500) NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RosterType]    Script Date: 11/26/2011 11:26:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RosterType](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[IsBooked] [bit] NOT NULL,
	[Note] [nvarchar](500) NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_RosterType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roster]    Script Date: 11/26/2011 11:26:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roster](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[RosterTypeId] [bigint] NOT NULL,
	[Note] [nvarchar](500) NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Roster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoomFunc]    Script Date: 11/26/2011 11:26:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoomFunc](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[RoomId] [bigint] NOT NULL,
	[FuncId] [bigint] NOT NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_RoomFunc] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Staff]    Script Date: 11/26/2011 11:26:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staff](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](200) NOT NULL,
	[LastName] [nvarchar](200) NOT NULL,
	[ShortName] [nvarchar](50) NOT NULL,
	[GroupId] [bigint] NOT NULL,
	[UserName] [nvarchar](200) NOT NULL,
	[Address] [nvarchar](500) NULL,
	[HomePhone] [nvarchar](20) NULL,
	[WorkPhone] [nvarchar](20) NULL,
	[CellPhone] [nvarchar](20) NULL,
	[Birthdate] [datetime] NULL,
	[IsFemale] [bit] NOT NULL,
	[Title] [nvarchar](10) NULL,
	[Note] [nvarchar](500) NULL,
	[Roles] [nvarchar](200) NOT NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Staff] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Content]    Script Date: 11/26/2011 11:26:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Content](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[FuncId] [bigint] NOT NULL,
	[Note] [nvarchar](500) NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Content] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DoctorRoster]    Script Date: 11/26/2011 11:26:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DoctorRoster](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DoctorId] [bigint] NOT NULL,
	[RosterId] [bigint] NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[Note] [nvarchar](500) NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_DoctorRoster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DoctorRoom]    Script Date: 11/26/2011 11:26:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DoctorRoom](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DoctorId] [bigint] NOT NULL,
	[RoomId] [bigint] NOT NULL,
	[PriorityIndex] [int] NOT NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_DoctorRoom] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DoctorFunc]    Script Date: 11/26/2011 11:26:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DoctorFunc](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DoctorId] [bigint] NOT NULL,
	[FuncId] [bigint] NOT NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_DoctorFunc] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Appointment]    Script Date: 11/26/2011 11:26:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appointment](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CustomerId] [bigint] NOT NULL,
	[ContentId] [bigint] NOT NULL,
	[DoctorId] [bigint] NULL,
	[RoomId] [bigint] NULL,
	[StatusId] [bigint] NULL,
	[Note] [nvarchar](500) NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Appointment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NurseAppointment]    Script Date: 11/26/2011 11:26:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NurseAppointment](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[AppointmentId] [bigint] NOT NULL,
	[NurseId] [bigint] NOT NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreateUser] [nvarchar](200) NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateUser] [nvarchar](200) NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_NurseAppointment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Default [DF_Appointment_IsDisabled]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[Appointment] ADD  CONSTRAINT [DF_Appointment_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
GO
/****** Object:  Default [DF_Appointment_CreateDate]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[Appointment] ADD  CONSTRAINT [DF_Appointment_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF_Appointment_UpdateDate]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[Appointment] ADD  CONSTRAINT [DF_Appointment_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
GO
/****** Object:  Default [DF_Content_IsDisabled]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[Content] ADD  CONSTRAINT [DF_Content_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
GO
/****** Object:  Default [DF_Content_CreateDate]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[Content] ADD  CONSTRAINT [DF_Content_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF_Content_UpdateDate]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[Content] ADD  CONSTRAINT [DF_Content_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
GO
/****** Object:  Default [DF_Customer_IsFemale]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_IsFemale]  DEFAULT ((0)) FOR [IsFemale]
GO
/****** Object:  Default [DF_Customer_IsDisabled]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
GO
/****** Object:  Default [DF_Customer_CreateDate]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF_Customer_UpdateDate]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
GO
/****** Object:  Default [DF_DoctorFunc_IsDisabled]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[DoctorFunc] ADD  CONSTRAINT [DF_DoctorFunc_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
GO
/****** Object:  Default [DF_DoctorFunc_CreateDate]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[DoctorFunc] ADD  CONSTRAINT [DF_DoctorFunc_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF_DoctorFunc_UpdateDate]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[DoctorFunc] ADD  CONSTRAINT [DF_DoctorFunc_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
GO
/****** Object:  Default [DF_DoctorRoom_PriorityIndex]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[DoctorRoom] ADD  CONSTRAINT [DF_DoctorRoom_PriorityIndex]  DEFAULT ((1)) FOR [PriorityIndex]
GO
/****** Object:  Default [DF_DoctorRoom_IsDisabled]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[DoctorRoom] ADD  CONSTRAINT [DF_DoctorRoom_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
GO
/****** Object:  Default [DF_DoctorRoom_CreateDate]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[DoctorRoom] ADD  CONSTRAINT [DF_DoctorRoom_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF_DoctorRoom_UpdateDate]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[DoctorRoom] ADD  CONSTRAINT [DF_DoctorRoom_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
GO
/****** Object:  Default [DF_DoctorRoster_IsDisabled]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[DoctorRoster] ADD  CONSTRAINT [DF_DoctorRoster_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
GO
/****** Object:  Default [DF_DoctorRoster_CreateDate]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[DoctorRoster] ADD  CONSTRAINT [DF_DoctorRoster_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF_DoctorRoster_UpdateDate]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[DoctorRoster] ADD  CONSTRAINT [DF_DoctorRoster_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
GO
/****** Object:  Default [DF_Functionality_IsDisabled]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[Functionality] ADD  CONSTRAINT [DF_Functionality_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
GO
/****** Object:  Default [DF_Functionality_CreateDate]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[Functionality] ADD  CONSTRAINT [DF_Functionality_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF_Functionality_UpdateDate]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[Functionality] ADD  CONSTRAINT [DF_Functionality_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
GO
/****** Object:  Default [DF_Group_IsDisabled]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[Group] ADD  CONSTRAINT [DF_Group_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
GO
/****** Object:  Default [DF_Group_CreateDate]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[Group] ADD  CONSTRAINT [DF_Group_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF_Group_UpdateDate]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[Group] ADD  CONSTRAINT [DF_Group_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
GO
/****** Object:  Default [DF_NurseAppointment_IsDisabled]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[NurseAppointment] ADD  CONSTRAINT [DF_NurseAppointment_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
GO
/****** Object:  Default [DF_NurseAppointment_CreateDate]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[NurseAppointment] ADD  CONSTRAINT [DF_NurseAppointment_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF_NurseAppointment_UpdateDate]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[NurseAppointment] ADD  CONSTRAINT [DF_NurseAppointment_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
GO
/****** Object:  Default [DF_Role_IsDisabled]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Role_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
GO
/****** Object:  Default [DF_Role_CreateDate]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Role_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF_Role_UpdateDate]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Role_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
GO
/****** Object:  Default [DF_Room_IsDisabled]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[Room] ADD  CONSTRAINT [DF_Room_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
GO
/****** Object:  Default [DF_Room_CreateDate]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[Room] ADD  CONSTRAINT [DF_Room_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF_Room_UpdateDate]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[Room] ADD  CONSTRAINT [DF_Room_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
GO
/****** Object:  Default [DF_RoomFunc_IsDisabled]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[RoomFunc] ADD  CONSTRAINT [DF_RoomFunc_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
GO
/****** Object:  Default [DF_RoomFunc_CreateDate]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[RoomFunc] ADD  CONSTRAINT [DF_RoomFunc_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF_RoomFunc_UpdateDate]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[RoomFunc] ADD  CONSTRAINT [DF_RoomFunc_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
GO
/****** Object:  Default [DF_Roster_IsDisabled]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[Roster] ADD  CONSTRAINT [DF_Roster_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
GO
/****** Object:  Default [DF_Roster_CreateDate]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[Roster] ADD  CONSTRAINT [DF_Roster_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF_Roster_UpdateDate]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[Roster] ADD  CONSTRAINT [DF_Roster_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
GO
/****** Object:  Default [DF_RosterType_IsBooked]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[RosterType] ADD  CONSTRAINT [DF_RosterType_IsBooked]  DEFAULT ((1)) FOR [IsBooked]
GO
/****** Object:  Default [DF_RosterType_IsDisabled]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[RosterType] ADD  CONSTRAINT [DF_RosterType_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
GO
/****** Object:  Default [DF_RosterType_CreateDate]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[RosterType] ADD  CONSTRAINT [DF_RosterType_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF_RosterType_UpdateDate]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[RosterType] ADD  CONSTRAINT [DF_RosterType_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
GO
/****** Object:  Default [DF_Staff_IsFemale]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[Staff] ADD  CONSTRAINT [DF_Staff_IsFemale]  DEFAULT ((0)) FOR [IsFemale]
GO
/****** Object:  Default [DF_Staff_IsDisabled]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[Staff] ADD  CONSTRAINT [DF_Staff_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
GO
/****** Object:  Default [DF_Staff_CreateDate]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[Staff] ADD  CONSTRAINT [DF_Staff_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF_Staff_UpdateDate]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[Staff] ADD  CONSTRAINT [DF_Staff_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
GO
/****** Object:  Default [DF_Status_IsDisabled]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[Status] ADD  CONSTRAINT [DF_Status_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
GO
/****** Object:  Default [DF_Status_CreateDate]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[Status] ADD  CONSTRAINT [DF_Status_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF_Status_UpdateDate]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[Status] ADD  CONSTRAINT [DF_Status_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
GO
/****** Object:  ForeignKey [FK_Appointment_Content]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Content] FOREIGN KEY([ContentId])
REFERENCES [dbo].[Content] ([Id])
GO
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_Content]
GO
/****** Object:  ForeignKey [FK_Appointment_Customer]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_Customer]
GO
/****** Object:  ForeignKey [FK_Appointment_Room]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Room] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([Id])
GO
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_Room]
GO
/****** Object:  ForeignKey [FK_Appointment_Staff]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Staff] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[Staff] ([Id])
GO
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_Staff]
GO
/****** Object:  ForeignKey [FK_Appointment_Status]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Status] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Status] ([Id])
GO
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_Status]
GO
/****** Object:  ForeignKey [FK_Content_Functionality]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[Content]  WITH CHECK ADD  CONSTRAINT [FK_Content_Functionality] FOREIGN KEY([FuncId])
REFERENCES [dbo].[Functionality] ([Id])
GO
ALTER TABLE [dbo].[Content] CHECK CONSTRAINT [FK_Content_Functionality]
GO
/****** Object:  ForeignKey [FK_DoctorFunc_Functionality]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[DoctorFunc]  WITH CHECK ADD  CONSTRAINT [FK_DoctorFunc_Functionality] FOREIGN KEY([FuncId])
REFERENCES [dbo].[Functionality] ([Id])
GO
ALTER TABLE [dbo].[DoctorFunc] CHECK CONSTRAINT [FK_DoctorFunc_Functionality]
GO
/****** Object:  ForeignKey [FK_DoctorFunc_Staff]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[DoctorFunc]  WITH CHECK ADD  CONSTRAINT [FK_DoctorFunc_Staff] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[Staff] ([Id])
GO
ALTER TABLE [dbo].[DoctorFunc] CHECK CONSTRAINT [FK_DoctorFunc_Staff]
GO
/****** Object:  ForeignKey [FK_DoctorRoom_Room]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[DoctorRoom]  WITH CHECK ADD  CONSTRAINT [FK_DoctorRoom_Room] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([Id])
GO
ALTER TABLE [dbo].[DoctorRoom] CHECK CONSTRAINT [FK_DoctorRoom_Room]
GO
/****** Object:  ForeignKey [FK_DoctorRoom_Staff]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[DoctorRoom]  WITH CHECK ADD  CONSTRAINT [FK_DoctorRoom_Staff] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[Staff] ([Id])
GO
ALTER TABLE [dbo].[DoctorRoom] CHECK CONSTRAINT [FK_DoctorRoom_Staff]
GO
/****** Object:  ForeignKey [FK_DoctorRoster_Roster]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[DoctorRoster]  WITH CHECK ADD  CONSTRAINT [FK_DoctorRoster_Roster] FOREIGN KEY([RosterId])
REFERENCES [dbo].[Roster] ([Id])
GO
ALTER TABLE [dbo].[DoctorRoster] CHECK CONSTRAINT [FK_DoctorRoster_Roster]
GO
/****** Object:  ForeignKey [FK_DoctorRoster_Staff]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[DoctorRoster]  WITH CHECK ADD  CONSTRAINT [FK_DoctorRoster_Staff] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[Staff] ([Id])
GO
ALTER TABLE [dbo].[DoctorRoster] CHECK CONSTRAINT [FK_DoctorRoster_Staff]
GO
/****** Object:  ForeignKey [FK_NurseAppointment_Appointment]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[NurseAppointment]  WITH CHECK ADD  CONSTRAINT [FK_NurseAppointment_Appointment] FOREIGN KEY([AppointmentId])
REFERENCES [dbo].[Appointment] ([Id])
GO
ALTER TABLE [dbo].[NurseAppointment] CHECK CONSTRAINT [FK_NurseAppointment_Appointment]
GO
/****** Object:  ForeignKey [FK_NurseAppointment_Staff]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[NurseAppointment]  WITH CHECK ADD  CONSTRAINT [FK_NurseAppointment_Staff] FOREIGN KEY([NurseId])
REFERENCES [dbo].[Staff] ([Id])
GO
ALTER TABLE [dbo].[NurseAppointment] CHECK CONSTRAINT [FK_NurseAppointment_Staff]
GO
/****** Object:  ForeignKey [FK_RoomFunc_Functionality]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[RoomFunc]  WITH CHECK ADD  CONSTRAINT [FK_RoomFunc_Functionality] FOREIGN KEY([FuncId])
REFERENCES [dbo].[Functionality] ([Id])
GO
ALTER TABLE [dbo].[RoomFunc] CHECK CONSTRAINT [FK_RoomFunc_Functionality]
GO
/****** Object:  ForeignKey [FK_RoomFunc_Room]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[RoomFunc]  WITH CHECK ADD  CONSTRAINT [FK_RoomFunc_Room] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([Id])
GO
ALTER TABLE [dbo].[RoomFunc] CHECK CONSTRAINT [FK_RoomFunc_Room]
GO
/****** Object:  ForeignKey [FK_Roster_RosterType]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[Roster]  WITH CHECK ADD  CONSTRAINT [FK_Roster_RosterType] FOREIGN KEY([RosterTypeId])
REFERENCES [dbo].[RosterType] ([Id])
GO
ALTER TABLE [dbo].[Roster] CHECK CONSTRAINT [FK_Roster_RosterType]
GO
/****** Object:  ForeignKey [FK_Staff_Group]    Script Date: 11/26/2011 11:26:38 ******/
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [FK_Staff_Group] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Group] ([Id])
GO
ALTER TABLE [dbo].[Staff] CHECK CONSTRAINT [FK_Staff_Group]
GO
