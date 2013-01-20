CREATE TABLE [dbo].[MessageConfig]
(
[MessageKey] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[MessageValue] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Description] [nvarchar] (1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MessageConfig] ADD CONSTRAINT [PK_MessageConfig] PRIMARY KEY CLUSTERED  ([MessageKey]) ON [PRIMARY]
GO
