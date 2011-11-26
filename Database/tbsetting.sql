
GO
/****** Object:  Table [dbo].[tblSettings]    Script Date: 11/26/2011 14:25:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblSettings](
	[ID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_tblSettings_id]  DEFAULT (newid()),
	[Type] [tinyint] NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[ValueString] [ntext] NULL,
	[ValueBinary] [varbinary](5000) NULL,
 CONSTRAINT [PK_tblSettings] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF