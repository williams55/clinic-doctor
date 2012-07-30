CREATE TABLE [dbo].[LogHistory]
(
[Id] [uniqueidentifier] NOT NULL,
[ActionId] [int] NULL,
[ActionDescription] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CreateUser] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CreateDate] [datetime] NULL,
[IP] [varchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[SystemID] [varchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Browser] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[OS] [varchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
EXEC sp_addextendedproperty N'MS_Description', N'Nội dung thao tác', 'SCHEMA', N'dbo', 'TABLE', N'LogHistory', 'COLUMN', N'ActionDescription'
GO
EXEC sp_addextendedproperty N'MS_Description', N'Mã thao tác. Được định nghĩa chi tiết trong constants của code
Nạp tiền, Tạo doanh nghiệp, Sửa thông tin doanh nghiệp...', 'SCHEMA', N'dbo', 'TABLE', N'LogHistory', 'COLUMN', N'ActionId'
GO
