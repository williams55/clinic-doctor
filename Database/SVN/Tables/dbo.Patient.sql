CREATE TABLE [dbo].[Patient]
(
[PatientCode] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[FirstName] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[LastName] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[MemberType] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[HomePhone] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[WorkPhone] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CellPhone] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Avatar] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CompanyCode] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Birthdate] [datetime] NULL,
[IsFemale] [bit] NOT NULL CONSTRAINT [DF_Patient_IsFemale] DEFAULT ((0)),
[Title] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Note] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IsDisabled] [bit] NOT NULL CONSTRAINT [DF_Patient_IsDisabled] DEFAULT ((0)),
[CreateUser] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CreateDate] [datetime] NOT NULL CONSTRAINT [DF_Patient_CreateDate] DEFAULT (getdate()),
[UpdateUser] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[UpdateDate] [datetime] NOT NULL CONSTRAINT [DF_Patient_UpdateDate] DEFAULT (getdate())
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Patient] ADD CONSTRAINT [PK_Patient] PRIMARY KEY CLUSTERED  ([PatientCode]) ON [PRIMARY]
GO
