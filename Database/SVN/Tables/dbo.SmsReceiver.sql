CREATE TABLE [dbo].[SmsReceiver]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[Mobile] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[FirstName] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[LastName] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[UserType] [tinyint] NOT NULL,
[SmsId] [bigint] NOT NULL,
[IsSent] [bit] NOT NULL CONSTRAINT [DF_SmsReceiver_IsSent] DEFAULT ((0)),
[SendingTimes] [int] NOT NULL CONSTRAINT [DF_SmsReceiver_SendingTimes] DEFAULT ((0)),
[Note] [nvarchar] (1024) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IsDisabled] [bit] NOT NULL CONSTRAINT [DF_SmsReceiver_IsDisabled] DEFAULT ((0)),
[CreateUser] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CreateDate] [datetime] NOT NULL CONSTRAINT [DF_SmsReceiver_CreateDate] DEFAULT (getdate()),
[UpdateUser] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[UpdateDate] [datetime] NOT NULL CONSTRAINT [DF_SmsReceiver_UpdateDate] DEFAULT (getdate())
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SmsReceiver] ADD CONSTRAINT [PK_SmsReceiver] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SmsReceiver] ADD CONSTRAINT [FK_SmsReceiver_Sms] FOREIGN KEY ([SmsId]) REFERENCES [dbo].[Sms] ([Id])
GO
EXEC sp_addextendedproperty N'MS_Description', N'Sms này đã được gửi chưa', 'SCHEMA', N'dbo', 'TABLE', N'SmsReceiver', 'COLUMN', N'IsSent'
GO
EXEC sp_addextendedproperty N'MS_Description', N'Số lần SMS này được gửi. Vì có thể gửi lại', 'SCHEMA', N'dbo', 'TABLE', N'SmsReceiver', 'COLUMN', N'SendingTimes'
GO
EXEC sp_addextendedproperty N'MS_Description', N'Loại người dùng nhận tin: 1. Doctor, 2: Patient', 'SCHEMA', N'dbo', 'TABLE', N'SmsReceiver', 'COLUMN', N'UserType'
GO
