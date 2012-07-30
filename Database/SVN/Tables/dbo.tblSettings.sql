CREATE TABLE [dbo].[tblSettings]
(
[ID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_tblSettings_id] DEFAULT (newid()),
[Type] [tinyint] NOT NULL,
[Code] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ValueString] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ValueBinary] [varbinary] (5000) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[tblSettings] ADD CONSTRAINT [PK_tblSettings] PRIMARY KEY CLUSTERED  ([ID]) ON [PRIMARY]
GO
