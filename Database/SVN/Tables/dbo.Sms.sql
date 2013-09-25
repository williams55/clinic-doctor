CREATE TABLE [dbo].[Sms]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[Message] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[SmsType] [tinyint] NOT NULL,
[SendTime] [datetime] NOT NULL,
[IsSendNow] [bit] NOT NULL CONSTRAINT [DF_Sms_IsSendNow] DEFAULT ((0)),
[IsSent] [bit] NOT NULL CONSTRAINT [DF_Sms_IsSent] DEFAULT ((0)),
[SendingTimes] [int] NOT NULL CONSTRAINT [DF_Table_1_SendingTime] DEFAULT ((0)),
[AppointmentId] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Note] [nvarchar] (1024) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IsDisabled] [bit] NOT NULL CONSTRAINT [DF_Sms_IsDisabled] DEFAULT ((0)),
[CreateUser] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CreateDate] [datetime] NOT NULL CONSTRAINT [DF_Sms_CreateDate] DEFAULT (getdate()),
[UpdateUser] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[UpdateDate] [datetime] NOT NULL CONSTRAINT [DF_Sms_UpdateDate] DEFAULT (getdate())
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Sms] ADD CONSTRAINT [PK_Sms] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Sms] ADD CONSTRAINT [FK_Sms_Appointment] FOREIGN KEY ([AppointmentId]) REFERENCES [dbo].[Appointment] ([Id])
GO
EXEC sp_addextendedproperty N'MS_Description', N'Id của appointment nếu đó là loại của appointment', 'SCHEMA', N'dbo', 'TABLE', N'Sms', 'COLUMN', N'AppointmentId'
GO
EXEC sp_addextendedproperty N'MS_Description', N'Có gửi ngay hay không. Nếu không sẽ được gửi bằng windows service', 'SCHEMA', N'dbo', 'TABLE', N'Sms', 'COLUMN', N'IsSendNow'
GO
EXEC sp_addextendedproperty N'MS_Description', N'Sms này đã được gửi chưa', 'SCHEMA', N'dbo', 'TABLE', N'Sms', 'COLUMN', N'IsSent'
GO
EXEC sp_addextendedproperty N'MS_Description', N'Số lần SMS này được gửi. Vì có thể gửi lại', 'SCHEMA', N'dbo', 'TABLE', N'Sms', 'COLUMN', N'SendingTimes'
GO
EXEC sp_addextendedproperty N'MS_Description', N'Thời gian gửi tin nhắn', 'SCHEMA', N'dbo', 'TABLE', N'Sms', 'COLUMN', N'SendTime'
GO
EXEC sp_addextendedproperty N'MS_Description', N'Loại SMS: 1. SMS từ appointment, 2. SMS khác [do admin tự tạo]', 'SCHEMA', N'dbo', 'TABLE', N'Sms', 'COLUMN', N'SmsType'
GO
