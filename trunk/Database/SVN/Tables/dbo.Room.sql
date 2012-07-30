CREATE TABLE [dbo].[Room]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Title] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Note] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ServicesId] [int] NULL,
[IsDisabled] [bit] NOT NULL CONSTRAINT [DF_Room_IsDisabled] DEFAULT ((0)),
[CreateUser] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CreateDate] [datetime] NOT NULL CONSTRAINT [DF_Room_CreateDate] DEFAULT (getdate()),
[UpdateUser] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[UpdateDate] [datetime] NOT NULL CONSTRAINT [DF_Room_UpdateDate] DEFAULT (getdate())
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Room] ADD CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Room] ADD CONSTRAINT [FK_Room_Procedure] FOREIGN KEY ([ServicesId]) REFERENCES [dbo].[Services] ([Id])
GO
EXEC sp_addextendedproperty N'MS_Description', N'A room can have many procedures. They are seperated by semi-comma [;]
For example: XRay;MRI', 'SCHEMA', N'dbo', 'TABLE', N'Room', 'COLUMN', N'ServicesId'
GO
