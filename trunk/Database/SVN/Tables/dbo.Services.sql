CREATE TABLE [dbo].[Services]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Title] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ShortTitle] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Note] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PriorityIndex] [int] NOT NULL CONSTRAINT [DF_Services_PriorityIndex] DEFAULT ((1)),
[IsDisabled] [bit] NOT NULL CONSTRAINT [DF_Procedure_IsDisabled] DEFAULT ((0)),
[CreateUser] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CreateDate] [datetime] NOT NULL CONSTRAINT [DF_Procedure_CreateDate] DEFAULT (getdate()),
[UpdateUser] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[UpdateDate] [datetime] NOT NULL CONSTRAINT [DF_Procedure_UpdateDate] DEFAULT (getdate())
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Services] ADD CONSTRAINT [PK_Procedure] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
EXEC sp_addextendedproperty N'MS_Description', N'Procedures, Services...', 'SCHEMA', N'dbo', 'TABLE', N'Services', NULL, NULL
GO
