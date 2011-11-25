SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Functionality]') AND type in (N'U'))
BEGIN
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
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Functionality]') AND name = N'IX_Functionality_Id_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Functionality_Id_IsDisabled] ON [dbo].[Functionality] 
(
	[Id] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Functionality]') AND name = N'IX_Functionality_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Functionality_IsDisabled] ON [dbo].[Functionality] 
(
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND type in (N'U'))
BEGIN
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
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND name = N'IX_Customer_Id_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Customer_Id_IsDisabled] ON [dbo].[Customer] 
(
	[Id] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND name = N'IX_Customer_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Customer_IsDisabled] ON [dbo].[Customer] 
(
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND name = N'IX_Customer_IsFemale')
CREATE NONCLUSTERED INDEX [IX_Customer_IsFemale] ON [dbo].[Customer] 
(
	[IsFemale] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND name = N'IX_Customer_IsFemale_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Customer_IsFemale_IsDisabled] ON [dbo].[Customer] 
(
	[IsFemale] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Status]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Status](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NULL,
	[ColorCode] [nvarchar](10) NULL,
	[Note] [nvarchar](500) NULL,
	[StatusType] [nvarchar](50) NULL,
	[IsDisabled] [bit] NULL,
	[CreateUser] [nvarchar](200) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateUser] [nvarchar](200) NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Status]') AND name = N'IX_Status_Id_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Status_Id_IsDisabled] ON [dbo].[Status] 
(
	[Id] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Status]') AND name = N'IX_Status_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Status_IsDisabled] ON [dbo].[Status] 
(
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Status]') AND name = N'IX_Status_StatusType')
CREATE NONCLUSTERED INDEX [IX_Status_StatusType] ON [dbo].[Status] 
(
	[StatusType] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Status]') AND name = N'IX_Status_StatusType_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Status_StatusType_IsDisabled] ON [dbo].[Status] 
(
	[StatusType] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RosterType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RosterType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NULL,
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
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RosterType]') AND name = N'IX_RosterType_Id_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_RosterType_Id_IsDisabled] ON [dbo].[RosterType] 
(
	[Id] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RosterType]') AND name = N'IX_RosterType_IsBooked')
CREATE NONCLUSTERED INDEX [IX_RosterType_IsBooked] ON [dbo].[RosterType] 
(
	[IsBooked] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RosterType]') AND name = N'IX_RosterType_IsBooked_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_RosterType_IsBooked_IsDisabled] ON [dbo].[RosterType] 
(
	[IsBooked] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RosterType]') AND name = N'IX_RosterType_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_RosterType_IsDisabled] ON [dbo].[RosterType] 
(
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role]') AND type in (N'U'))
BEGIN
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
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Role]') AND name = N'IX_Role_Id_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Role_Id_IsDisabled] ON [dbo].[Role] 
(
	[Id] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Role]') AND name = N'IX_Role_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Role_IsDisabled] ON [dbo].[Role] 
(
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Group]') AND type in (N'U'))
BEGIN
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
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Group]') AND name = N'IX_Group_Id_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Group_Id_IsDisabled] ON [dbo].[Group] 
(
	[Id] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Group]') AND name = N'IX_Group_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Group_IsDisabled] ON [dbo].[Group] 
(
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorFunc]') AND type in (N'U'))
BEGIN
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
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorFunc]') AND name = N'IX_DoctorFunc_DoctorId')
CREATE NONCLUSTERED INDEX [IX_DoctorFunc_DoctorId] ON [dbo].[DoctorFunc] 
(
	[DoctorId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorFunc]') AND name = N'IX_DoctorFunc_DoctorId_FuncId')
CREATE NONCLUSTERED INDEX [IX_DoctorFunc_DoctorId_FuncId] ON [dbo].[DoctorFunc] 
(
	[DoctorId] ASC,
	[FuncId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorFunc]') AND name = N'IX_DoctorFunc_DoctorId_FuncId_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_DoctorFunc_DoctorId_FuncId_IsDisabled] ON [dbo].[DoctorFunc] 
(
	[DoctorId] ASC,
	[FuncId] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorFunc]') AND name = N'IX_DoctorFunc_DoctorId_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_DoctorFunc_DoctorId_IsDisabled] ON [dbo].[DoctorFunc] 
(
	[DoctorId] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorFunc]') AND name = N'IX_DoctorFunc_FuncId')
CREATE NONCLUSTERED INDEX [IX_DoctorFunc_FuncId] ON [dbo].[DoctorFunc] 
(
	[FuncId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorFunc]') AND name = N'IX_DoctorFunc_FuncId_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_DoctorFunc_FuncId_IsDisabled] ON [dbo].[DoctorFunc] 
(
	[FuncId] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorFunc]') AND name = N'IX_DoctorFunc_Id_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_DoctorFunc_Id_IsDisabled] ON [dbo].[DoctorFunc] 
(
	[Id] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorFunc]') AND name = N'IX_DoctorFunc_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_DoctorFunc_IsDisabled] ON [dbo].[DoctorFunc] 
(
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoomFunc]') AND type in (N'U'))
BEGIN
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
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RoomFunc]') AND name = N'IX_RoomFunc_FuncId')
CREATE NONCLUSTERED INDEX [IX_RoomFunc_FuncId] ON [dbo].[RoomFunc] 
(
	[FuncId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RoomFunc]') AND name = N'IX_RoomFunc_FuncId_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_RoomFunc_FuncId_IsDisabled] ON [dbo].[RoomFunc] 
(
	[FuncId] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RoomFunc]') AND name = N'IX_RoomFunc_Id_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_RoomFunc_Id_IsDisabled] ON [dbo].[RoomFunc] 
(
	[Id] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RoomFunc]') AND name = N'IX_RoomFunc_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_RoomFunc_IsDisabled] ON [dbo].[RoomFunc] 
(
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RoomFunc]') AND name = N'IX_RoomFunc_RoomId')
CREATE NONCLUSTERED INDEX [IX_RoomFunc_RoomId] ON [dbo].[RoomFunc] 
(
	[RoomId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RoomFunc]') AND name = N'IX_RoomFunc_RoomId_FuncId')
CREATE NONCLUSTERED INDEX [IX_RoomFunc_RoomId_FuncId] ON [dbo].[RoomFunc] 
(
	[RoomId] ASC,
	[FuncId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RoomFunc]') AND name = N'IX_RoomFunc_RoomId_FuncId_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_RoomFunc_RoomId_FuncId_IsDisabled] ON [dbo].[RoomFunc] 
(
	[RoomId] ASC,
	[FuncId] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RoomFunc]') AND name = N'IX_RoomFunc_RoomId_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_RoomFunc_RoomId_IsDisabled] ON [dbo].[RoomFunc] 
(
	[RoomId] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Content]') AND type in (N'U'))
BEGIN
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
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Content]') AND name = N'IX_Content_FuncId')
CREATE NONCLUSTERED INDEX [IX_Content_FuncId] ON [dbo].[Content] 
(
	[FuncId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Content]') AND name = N'IX_Content_FuncId_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Content_FuncId_IsDisabled] ON [dbo].[Content] 
(
	[FuncId] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Content]') AND name = N'IX_Content_Id_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Content_Id_IsDisabled] ON [dbo].[Content] 
(
	[Id] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Content]') AND name = N'IX_Content_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Content_IsDisabled] ON [dbo].[Content] 
(
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Appointment]') AND type in (N'U'))
BEGIN
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
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Appointment]') AND name = N'IX_Appointment_ContentId')
CREATE NONCLUSTERED INDEX [IX_Appointment_ContentId] ON [dbo].[Appointment] 
(
	[ContentId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Appointment]') AND name = N'IX_Appointment_ContentId_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Appointment_ContentId_IsDisabled] ON [dbo].[Appointment] 
(
	[ContentId] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Appointment]') AND name = N'IX_Appointment_CustomerId')
CREATE NONCLUSTERED INDEX [IX_Appointment_CustomerId] ON [dbo].[Appointment] 
(
	[CustomerId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Appointment]') AND name = N'IX_Appointment_CustomerId_ContentId_DoctorId_RoomId_StatusId')
CREATE NONCLUSTERED INDEX [IX_Appointment_CustomerId_ContentId_DoctorId_RoomId_StatusId] ON [dbo].[Appointment] 
(
	[CustomerId] ASC,
	[ContentId] ASC,
	[DoctorId] ASC,
	[RoomId] ASC,
	[StatusId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Appointment]') AND name = N'IX_Appointment_CustomerId_ContentId_DoctorId_RoomId_StatusId_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Appointment_CustomerId_ContentId_DoctorId_RoomId_StatusId_IsDisabled] ON [dbo].[Appointment] 
(
	[CustomerId] ASC,
	[ContentId] ASC,
	[DoctorId] ASC,
	[RoomId] ASC,
	[StatusId] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Appointment]') AND name = N'IX_Appointment_CustomerId_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Appointment_CustomerId_IsDisabled] ON [dbo].[Appointment] 
(
	[CustomerId] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Appointment]') AND name = N'IX_Appointment_DoctorId')
CREATE NONCLUSTERED INDEX [IX_Appointment_DoctorId] ON [dbo].[Appointment] 
(
	[DoctorId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Appointment]') AND name = N'IX_Appointment_DoctorId_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Appointment_DoctorId_IsDisabled] ON [dbo].[Appointment] 
(
	[DoctorId] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Appointment]') AND name = N'IX_Appointment_Id_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Appointment_Id_IsDisabled] ON [dbo].[Appointment] 
(
	[Id] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Appointment]') AND name = N'IX_Appointment_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Appointment_IsDisabled] ON [dbo].[Appointment] 
(
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Appointment]') AND name = N'IX_Appointment_RoomId')
CREATE NONCLUSTERED INDEX [IX_Appointment_RoomId] ON [dbo].[Appointment] 
(
	[RoomId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Appointment]') AND name = N'IX_Appointment_RoomId_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Appointment_RoomId_IsDisabled] ON [dbo].[Appointment] 
(
	[RoomId] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Appointment]') AND name = N'IX_Appointment_StatusId')
CREATE NONCLUSTERED INDEX [IX_Appointment_StatusId] ON [dbo].[Appointment] 
(
	[StatusId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Appointment]') AND name = N'IX_Appointment_StatusId_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Appointment_StatusId_IsDisabled] ON [dbo].[Appointment] 
(
	[StatusId] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoster]') AND type in (N'U'))
BEGIN
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
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoster]') AND name = N'IX_DoctorRoster_DoctorId')
CREATE NONCLUSTERED INDEX [IX_DoctorRoster_DoctorId] ON [dbo].[DoctorRoster] 
(
	[DoctorId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoster]') AND name = N'IX_DoctorRoster_DoctorId_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_DoctorRoster_DoctorId_IsDisabled] ON [dbo].[DoctorRoster] 
(
	[DoctorId] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoster]') AND name = N'IX_DoctorRoster_DoctorId_RosterId')
CREATE NONCLUSTERED INDEX [IX_DoctorRoster_DoctorId_RosterId] ON [dbo].[DoctorRoster] 
(
	[DoctorId] ASC,
	[RosterId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoster]') AND name = N'IX_DoctorRoster_DoctorId_RosterId_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_DoctorRoster_DoctorId_RosterId_IsDisabled] ON [dbo].[DoctorRoster] 
(
	[DoctorId] ASC,
	[RosterId] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoster]') AND name = N'IX_DoctorRoster_Id_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_DoctorRoster_Id_IsDisabled] ON [dbo].[DoctorRoster] 
(
	[Id] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoster]') AND name = N'IX_DoctorRoster_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_DoctorRoster_IsDisabled] ON [dbo].[DoctorRoster] 
(
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoster]') AND name = N'IX_DoctorRoster_RosterId')
CREATE NONCLUSTERED INDEX [IX_DoctorRoster_RosterId] ON [dbo].[DoctorRoster] 
(
	[RosterId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoster]') AND name = N'IX_DoctorRoster_RosterId_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_DoctorRoster_RosterId_IsDisabled] ON [dbo].[DoctorRoster] 
(
	[RosterId] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoom]') AND type in (N'U'))
BEGIN
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
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoom]') AND name = N'IX_DoctorRoom_DoctorId')
CREATE NONCLUSTERED INDEX [IX_DoctorRoom_DoctorId] ON [dbo].[DoctorRoom] 
(
	[DoctorId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoom]') AND name = N'IX_DoctorRoom_DoctorId_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_DoctorRoom_DoctorId_IsDisabled] ON [dbo].[DoctorRoom] 
(
	[DoctorId] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoom]') AND name = N'IX_DoctorRoom_DoctorId_RoomId')
CREATE NONCLUSTERED INDEX [IX_DoctorRoom_DoctorId_RoomId] ON [dbo].[DoctorRoom] 
(
	[DoctorId] ASC,
	[RoomId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoom]') AND name = N'IX_DoctorRoom_DoctorId_RoomId_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_DoctorRoom_DoctorId_RoomId_IsDisabled] ON [dbo].[DoctorRoom] 
(
	[DoctorId] ASC,
	[RoomId] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoom]') AND name = N'IX_DoctorRoom_Id_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_DoctorRoom_Id_IsDisabled] ON [dbo].[DoctorRoom] 
(
	[Id] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoom]') AND name = N'IX_DoctorRoom_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_DoctorRoom_IsDisabled] ON [dbo].[DoctorRoom] 
(
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoom]') AND name = N'IX_DoctorRoom_RoomId')
CREATE NONCLUSTERED INDEX [IX_DoctorRoom_RoomId] ON [dbo].[DoctorRoom] 
(
	[RoomId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DoctorRoom]') AND name = N'IX_DoctorRoom_RoomId_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_DoctorRoom_RoomId_IsDisabled] ON [dbo].[DoctorRoom] 
(
	[RoomId] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NurseAppointment]') AND type in (N'U'))
BEGIN
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
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[NurseAppointment]') AND name = N'IX_NurseAppointment_AppointmentId')
CREATE NONCLUSTERED INDEX [IX_NurseAppointment_AppointmentId] ON [dbo].[NurseAppointment] 
(
	[AppointmentId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[NurseAppointment]') AND name = N'IX_NurseAppointment_AppointmentId_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_NurseAppointment_AppointmentId_IsDisabled] ON [dbo].[NurseAppointment] 
(
	[AppointmentId] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[NurseAppointment]') AND name = N'IX_NurseAppointment_AppointmentId_NurseId')
CREATE NONCLUSTERED INDEX [IX_NurseAppointment_AppointmentId_NurseId] ON [dbo].[NurseAppointment] 
(
	[AppointmentId] ASC,
	[NurseId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[NurseAppointment]') AND name = N'IX_NurseAppointment_AppointmentId_NurseId_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_NurseAppointment_AppointmentId_NurseId_IsDisabled] ON [dbo].[NurseAppointment] 
(
	[AppointmentId] ASC,
	[NurseId] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[NurseAppointment]') AND name = N'IX_NurseAppointment_Id_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_NurseAppointment_Id_IsDisabled] ON [dbo].[NurseAppointment] 
(
	[Id] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[NurseAppointment]') AND name = N'IX_NurseAppointment_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_NurseAppointment_IsDisabled] ON [dbo].[NurseAppointment] 
(
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[NurseAppointment]') AND name = N'IX_NurseAppointment_NurseId')
CREATE NONCLUSTERED INDEX [IX_NurseAppointment_NurseId] ON [dbo].[NurseAppointment] 
(
	[NurseId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[NurseAppointment]') AND name = N'IX_NurseAppointment_NurseId_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_NurseAppointment_NurseId_IsDisabled] ON [dbo].[NurseAppointment] 
(
	[NurseId] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StaffRoles]') AND type in (N'U'))
BEGIN
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
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[StaffRoles]') AND name = N'IX_StaffRoles_Id_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_StaffRoles_Id_IsDisabled] ON [dbo].[StaffRoles] 
(
	[Id] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[StaffRoles]') AND name = N'IX_StaffRoles_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_StaffRoles_IsDisabled] ON [dbo].[StaffRoles] 
(
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[StaffRoles]') AND name = N'IX_StaffRoles_RoleId')
CREATE NONCLUSTERED INDEX [IX_StaffRoles_RoleId] ON [dbo].[StaffRoles] 
(
	[RoleId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[StaffRoles]') AND name = N'IX_StaffRoles_RoleId_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_StaffRoles_RoleId_IsDisabled] ON [dbo].[StaffRoles] 
(
	[RoleId] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[StaffRoles]') AND name = N'IX_StaffRoles_StaffId')
CREATE NONCLUSTERED INDEX [IX_StaffRoles_StaffId] ON [dbo].[StaffRoles] 
(
	[StaffId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[StaffRoles]') AND name = N'IX_StaffRoles_StaffId_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_StaffRoles_StaffId_IsDisabled] ON [dbo].[StaffRoles] 
(
	[StaffId] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[StaffRoles]') AND name = N'IX_StaffRoles_StaffId_RoleId')
CREATE NONCLUSTERED INDEX [IX_StaffRoles_StaffId_RoleId] ON [dbo].[StaffRoles] 
(
	[StaffId] ASC,
	[RoleId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[StaffRoles]') AND name = N'IX_StaffRoles_StaffId_RoleId_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_StaffRoles_StaffId_RoleId_IsDisabled] ON [dbo].[StaffRoles] 
(
	[StaffId] ASC,
	[RoleId] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Room]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Room](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NULL,
	[Note] [nvarchar](500) NULL,
	[StatusId] [int] NULL,
	[IsDisabled] [bit] NULL,
	[CreateUser] [nvarchar](200) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateUser] [nvarchar](200) NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Room]') AND name = N'IX_Room_Id_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Room_Id_IsDisabled] ON [dbo].[Room] 
(
	[Id] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Room]') AND name = N'IX_Room_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Room_IsDisabled] ON [dbo].[Room] 
(
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Room]') AND name = N'IX_Room_StatusId')
CREATE NONCLUSTERED INDEX [IX_Room_StatusId] ON [dbo].[Room] 
(
	[StatusId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Room]') AND name = N'IX_Room_StatusId_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Room_StatusId_IsDisabled] ON [dbo].[Room] 
(
	[StatusId] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Roster]') AND type in (N'U'))
BEGIN
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
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Roster]') AND name = N'IX_Roster_Id_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Roster_Id_IsDisabled] ON [dbo].[Roster] 
(
	[Id] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Roster]') AND name = N'IX_Roster_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Roster_IsDisabled] ON [dbo].[Roster] 
(
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Roster]') AND name = N'IX_Roster_RosterTypeId')
CREATE NONCLUSTERED INDEX [IX_Roster_RosterTypeId] ON [dbo].[Roster] 
(
	[RosterTypeId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Roster]') AND name = N'IX_Roster_RosterTypeId_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Roster_RosterTypeId_IsDisabled] ON [dbo].[Roster] 
(
	[RosterTypeId] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GroupRoles]') AND type in (N'U'))
BEGIN
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
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[GroupRoles]') AND name = N'IX_GroupRoles_GroupId')
CREATE NONCLUSTERED INDEX [IX_GroupRoles_GroupId] ON [dbo].[GroupRoles] 
(
	[GroupId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[GroupRoles]') AND name = N'IX_GroupRoles_GroupId_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_GroupRoles_GroupId_IsDisabled] ON [dbo].[GroupRoles] 
(
	[GroupId] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[GroupRoles]') AND name = N'IX_GroupRoles_GroupId_RoleId')
CREATE NONCLUSTERED INDEX [IX_GroupRoles_GroupId_RoleId] ON [dbo].[GroupRoles] 
(
	[GroupId] ASC,
	[RoleId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[GroupRoles]') AND name = N'IX_GroupRoles_GroupId_RoleId_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_GroupRoles_GroupId_RoleId_IsDisabled] ON [dbo].[GroupRoles] 
(
	[GroupId] ASC,
	[RoleId] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[GroupRoles]') AND name = N'IX_GroupRoles_Id_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_GroupRoles_Id_IsDisabled] ON [dbo].[GroupRoles] 
(
	[Id] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[GroupRoles]') AND name = N'IX_GroupRoles_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_GroupRoles_IsDisabled] ON [dbo].[GroupRoles] 
(
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[GroupRoles]') AND name = N'IX_GroupRoles_RoleId')
CREATE NONCLUSTERED INDEX [IX_GroupRoles_RoleId] ON [dbo].[GroupRoles] 
(
	[RoleId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[GroupRoles]') AND name = N'IX_GroupRoles_RoleId_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_GroupRoles_RoleId_IsDisabled] ON [dbo].[GroupRoles] 
(
	[RoleId] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Staff]') AND type in (N'U'))
BEGIN
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
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_Staff_UserName] UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_Staff_UserName_IsDisabled] UNIQUE NONCLUSTERED 
(
	[UserName] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Staff]') AND name = N'IX_Staff_GroupId')
CREATE NONCLUSTERED INDEX [IX_Staff_GroupId] ON [dbo].[Staff] 
(
	[GroupId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Staff]') AND name = N'IX_Staff_GroupId_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Staff_GroupId_IsDisabled] ON [dbo].[Staff] 
(
	[GroupId] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Staff]') AND name = N'IX_Staff_GroupId_IsFemale')
CREATE NONCLUSTERED INDEX [IX_Staff_GroupId_IsFemale] ON [dbo].[Staff] 
(
	[GroupId] ASC,
	[IsFemale] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Staff]') AND name = N'IX_Staff_GroupId_IsFemale_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Staff_GroupId_IsFemale_IsDisabled] ON [dbo].[Staff] 
(
	[GroupId] ASC,
	[IsFemale] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Staff]') AND name = N'IX_Staff_Id_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Staff_Id_IsDisabled] ON [dbo].[Staff] 
(
	[Id] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Staff]') AND name = N'IX_Staff_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Staff_IsDisabled] ON [dbo].[Staff] 
(
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Staff]') AND name = N'IX_Staff_IsFemale')
CREATE NONCLUSTERED INDEX [IX_Staff_IsFemale] ON [dbo].[Staff] 
(
	[IsFemale] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Staff]') AND name = N'IX_Staff_IsFemale_IsDisabled')
CREATE NONCLUSTERED INDEX [IX_Staff_IsFemale_IsDisabled] ON [dbo].[Staff] 
(
	[IsFemale] ASC,
	[IsDisabled] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoctorFunc_Functionality]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorFunc]'))
ALTER TABLE [dbo].[DoctorFunc]  WITH CHECK ADD  CONSTRAINT [FK_DoctorFunc_Functionality] FOREIGN KEY([FuncId])
REFERENCES [dbo].[Functionality] ([Id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoctorFunc_Staff]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorFunc]'))
ALTER TABLE [dbo].[DoctorFunc]  WITH CHECK ADD  CONSTRAINT [FK_DoctorFunc_Staff] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[Staff] ([Id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoomFunc_Functionality]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoomFunc]'))
ALTER TABLE [dbo].[RoomFunc]  WITH CHECK ADD  CONSTRAINT [FK_RoomFunc_Functionality] FOREIGN KEY([FuncId])
REFERENCES [dbo].[Functionality] ([Id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoomFunc_Room]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoomFunc]'))
ALTER TABLE [dbo].[RoomFunc]  WITH CHECK ADD  CONSTRAINT [FK_RoomFunc_Room] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([Id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Content_Functionality]') AND parent_object_id = OBJECT_ID(N'[dbo].[Content]'))
ALTER TABLE [dbo].[Content]  WITH CHECK ADD  CONSTRAINT [FK_Content_Functionality] FOREIGN KEY([FuncId])
REFERENCES [dbo].[Functionality] ([Id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Content]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Content] FOREIGN KEY([ContentId])
REFERENCES [dbo].[Content] ([Id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Customer]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Room]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Room] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([Id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Staff]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Staff] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[Staff] ([Id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Status]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Status] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Status] ([Id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoctorRoster_Roster]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoster]'))
ALTER TABLE [dbo].[DoctorRoster]  WITH CHECK ADD  CONSTRAINT [FK_DoctorRoster_Roster] FOREIGN KEY([RosterId])
REFERENCES [dbo].[Roster] ([Id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoctorRoster_Staff]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoster]'))
ALTER TABLE [dbo].[DoctorRoster]  WITH CHECK ADD  CONSTRAINT [FK_DoctorRoster_Staff] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[Staff] ([Id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoctorRoom_Room]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoom]'))
ALTER TABLE [dbo].[DoctorRoom]  WITH CHECK ADD  CONSTRAINT [FK_DoctorRoom_Room] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([Id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DoctorRoom_Staff]') AND parent_object_id = OBJECT_ID(N'[dbo].[DoctorRoom]'))
ALTER TABLE [dbo].[DoctorRoom]  WITH CHECK ADD  CONSTRAINT [FK_DoctorRoom_Staff] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[Staff] ([Id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_NurseAppointment_Appointment]') AND parent_object_id = OBJECT_ID(N'[dbo].[NurseAppointment]'))
ALTER TABLE [dbo].[NurseAppointment]  WITH CHECK ADD  CONSTRAINT [FK_NurseAppointment_Appointment] FOREIGN KEY([AppointmentId])
REFERENCES [dbo].[Appointment] ([Id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_NurseAppointment_Staff]') AND parent_object_id = OBJECT_ID(N'[dbo].[NurseAppointment]'))
ALTER TABLE [dbo].[NurseAppointment]  WITH CHECK ADD  CONSTRAINT [FK_NurseAppointment_Staff] FOREIGN KEY([NurseId])
REFERENCES [dbo].[Staff] ([Id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StaffRoles_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[StaffRoles]'))
ALTER TABLE [dbo].[StaffRoles]  WITH CHECK ADD  CONSTRAINT [FK_StaffRoles_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StaffRoles_Staff]') AND parent_object_id = OBJECT_ID(N'[dbo].[StaffRoles]'))
ALTER TABLE [dbo].[StaffRoles]  WITH CHECK ADD  CONSTRAINT [FK_StaffRoles_Staff] FOREIGN KEY([StaffId])
REFERENCES [dbo].[Staff] ([Id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Room_Status]') AND parent_object_id = OBJECT_ID(N'[dbo].[Room]'))
ALTER TABLE [dbo].[Room]  WITH CHECK ADD  CONSTRAINT [FK_Room_Status] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Status] ([Id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Roster_RosterType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Roster]'))
ALTER TABLE [dbo].[Roster]  WITH CHECK ADD  CONSTRAINT [FK_Roster_RosterType] FOREIGN KEY([RosterTypeId])
REFERENCES [dbo].[RosterType] ([Id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GroupRoles_Group]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupRoles]'))
ALTER TABLE [dbo].[GroupRoles]  WITH CHECK ADD  CONSTRAINT [FK_GroupRoles_Group] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Group] ([Id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GroupRoles_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupRoles]'))
ALTER TABLE [dbo].[GroupRoles]  WITH CHECK ADD  CONSTRAINT [FK_GroupRoles_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Staff_Group]') AND parent_object_id = OBJECT_ID(N'[dbo].[Staff]'))
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [FK_Staff_Group] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Group] ([Id])
