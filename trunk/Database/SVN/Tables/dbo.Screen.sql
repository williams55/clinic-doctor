CREATE TABLE [dbo].[Screen]
(
[ScreenCode] [varchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ScreenName] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PriorityIndex] [int] NOT NULL CONSTRAINT [DF_Screen_PriorityIndex] DEFAULT ((1)),
[IsDisabled] [bit] NOT NULL CONSTRAINT [DF_Screen_IsDisabled] DEFAULT ((0)),
[CreateUser] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CreateDate] [datetime] NOT NULL CONSTRAINT [DF_Screen_CreateDate] DEFAULT (getdate()),
[UpdateUser] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[UpdateDate] [datetime] NOT NULL CONSTRAINT [DF_Screen_UpdateDate] DEFAULT (getdate())
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Screen] ADD CONSTRAINT [PK_Screen] PRIMARY KEY CLUSTERED  ([ScreenCode]) ON [PRIMARY]
GO
EXEC sp_addextendedproperty N'MS_Description', N'Define screen for role detail', 'SCHEMA', N'dbo', 'TABLE', N'Screen', NULL, NULL
GO
EXEC sp_addextendedproperty N'MS_Description', N'Link name of screen. 
Ex: Status, Appointment...', 'SCHEMA', N'dbo', 'TABLE', N'Screen', 'COLUMN', N'ScreenCode'
GO
