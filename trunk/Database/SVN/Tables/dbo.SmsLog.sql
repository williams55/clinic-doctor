CREATE TABLE [dbo].[SmsLog]
(
[Id] [uniqueidentifier] NOT NULL,
[SmsId] [bigint] NOT NULL,
[Message] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Mobile] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Title] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[SendTime] [datetime] NOT NULL,
[RealSendTime] [datetime] NULL,
[IsSent] [bit] NOT NULL CONSTRAINT [DF_SmsLog_IsSent] DEFAULT ((0)),
[IsDisabled] [bit] NOT NULL CONSTRAINT [DF_SmsLog_IsDisabled] DEFAULT ((0)),
[CreateUser] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CreateDate] [datetime] NOT NULL CONSTRAINT [DF_SmsLog_CreateDate] DEFAULT (getdate()),
[UpdateUser] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[UpdateDate] [datetime] NOT NULL CONSTRAINT [DF_SmsLog_UpdateDate] DEFAULT (getdate())
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SmsLog] ADD CONSTRAINT [PK_SmsLog] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SmsLog] ADD CONSTRAINT [FK_SmsLog_Sms] FOREIGN KEY ([SmsId]) REFERENCES [dbo].[Sms] ([Id])
GO
EXEC sp_addextendedproperty N'MS_Description', N'Sms này đã được gửi chưa', 'SCHEMA', N'dbo', 'TABLE', N'SmsLog', 'COLUMN', N'IsSent'
GO
EXEC sp_addextendedproperty N'MS_Description', N'Thời gian thật khi gửi tin nhắn', 'SCHEMA', N'dbo', 'TABLE', N'SmsLog', 'COLUMN', N'RealSendTime'
GO
EXEC sp_addextendedproperty N'MS_Description', N'Thời gian mong muốn gửi tin nhắn', 'SCHEMA', N'dbo', 'TABLE', N'SmsLog', 'COLUMN', N'SendTime'
GO
