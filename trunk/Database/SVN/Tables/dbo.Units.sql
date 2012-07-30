CREATE TABLE [dbo].[Units]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Title] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Note] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PriorityIndex] [int] NOT NULL CONSTRAINT [DF_Units_PriorityIndex] DEFAULT ((1)),
[IsDisabled] [bit] NOT NULL CONSTRAINT [DF_Unit_IsDisabled] DEFAULT ((0)),
[CreateUser] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CreateDate] [datetime] NOT NULL CONSTRAINT [DF_Unit_CreateDate] DEFAULT (getdate()),
[UpdateUser] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[UpdateDate] [datetime] NOT NULL CONSTRAINT [DF_Unit_UpdateDate] DEFAULT (getdate())
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Units] ADD CONSTRAINT [PK_Unit] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
EXEC sp_addextendedproperty N'MS_Description', N'Dr, Mr, Ms...', 'SCHEMA', N'dbo', 'TABLE', N'Units', 'COLUMN', N'Title'
GO
