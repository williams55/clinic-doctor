CREATE TABLE [dbo].[AppointmentGroup]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Title] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Note] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PriorityIndex] [int] NOT NULL CONSTRAINT [DF_AppointmentGroup_PriorityIndex] DEFAULT ((1)),
[IsDisabled] [bit] NOT NULL CONSTRAINT [DF_AppointmentGroup_IsDisabled] DEFAULT ((0)),
[CreateUser] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CreateDate] [datetime] NOT NULL CONSTRAINT [DF_AppointmentGroup_CreateDate] DEFAULT (getdate()),
[UpdateUser] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[UpdateDate] [datetime] NOT NULL CONSTRAINT [DF_AppointmentGroup_UpdateDate] DEFAULT (getdate()),
[UnitId] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AppointmentGroup] ADD CONSTRAINT [PK_AppointmentGroup] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AppointmentGroup] ADD CONSTRAINT [FK_AppointmentGroup_Unit] FOREIGN KEY ([UnitId]) REFERENCES [dbo].[Units] ([Id])
GO
EXEC sp_addextendedproperty N'MS_Description', N'This table group follow floor or something like that. For example, 1st Floor is a group and there are some staffs [Doctors, ProcedureGroup]', 'SCHEMA', N'dbo', 'TABLE', N'AppointmentGroup', NULL, NULL
GO
EXEC sp_addextendedproperty N'MS_Description', N'Define current unit belongs to what tab.
It''s seperated by semi-comma [;]
Ex: 1stFloor;2ndFloor', 'SCHEMA', N'dbo', 'TABLE', N'AppointmentGroup', 'COLUMN', N'UnitId'
GO
