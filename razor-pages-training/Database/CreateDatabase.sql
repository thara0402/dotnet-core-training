USE [master]
GO

DECLARE @proc smallint
DECLARE sysproc_cur CURSOR FAST_FORWARD FOR
SELECT spid FROM master..sysprocesses WITH(NOLOCK)

OPEN sysproc_cur
FETCH NEXT FROM sysproc_cur INTO @proc
WHILE (@@FETCH_STATUS <> -1)
BEGIN
   EXEC('KILL ' + @proc)
   FETCH NEXT FROM sysproc_cur INTO @proc
END
CLOSE sysproc_cur
DEALLOCATE sysproc_cur
 
if exists (select name from sys.databases where name = 'Shop')
    drop database [Shop]
 
declare @device_directory nvarchar(520)
select @device_directory = substring(filename, 1, charindex(N'master.mdf', lower(filename)) - 1)
from master.dbo.sysaltfiles 
where dbid = 1 AND fileid = 1
 
execute ('create database [Shop] on primary
( name = ''Shop'', filename = ''' + @device_directory + 'Shop.mdf'', size = 8192KB, maxsize = unlimited, filegrowth = 1024KB)
log on
( name = ''Shop_log'', filename = ''' + @device_directory + 'Shop.ldf'' , size = 1024KB , maxsize = 2048GB , filegrowth = 10%)')
GO

USE [Shop]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductID] [bigint] IDENTITY(1,1) NOT NULL,
	[Desc] [nvarchar](256) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Price] [decimal](28, 6) NOT NULL,
	[TS] [timestamp] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[OrderID] [bigint] IDENTITY(1,1) NOT NULL,
	[LastUpdate] [datetime] NOT NULL,
	[UserID] [nvarchar](256) NOT NULL,
	[TS] [timestamp] NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Basket](
	[BasketID] [bigint] IDENTITY(1,1) NOT NULL,
	[LastUpdate] [datetime] NOT NULL,
	[UserID] [nvarchar](256) NOT NULL,
	[TS] [timestamp] NULL,
 CONSTRAINT [PK_Basket] PRIMARY KEY CLUSTERED 
(
	[BasketID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BasketItem](
	[BasketItemID] [bigint] IDENTITY(1,1) NOT NULL,
	[BasketID] [bigint] NOT NULL,
	[ProductID] [bigint] NOT NULL,
	[Quantity] [int] NOT NULL,
	[UnitPrice] [decimal](28, 6) NOT NULL,
	[TS] [timestamp] NULL,
 CONSTRAINT [PK_BasketItem] PRIMARY KEY CLUSTERED 
(
	[BasketItemID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItem](
	[OrderItemID] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderID] [bigint] NOT NULL,
	[ProductID] [bigint] NOT NULL,
	[Quantity] [int] NOT NULL,
	[UnitPrice] [decimal](28, 6) NOT NULL,
	[TS] [timestamp] NULL,
 CONSTRAINT [PK_OrderItem] PRIMARY KEY CLUSTERED 
(
	[OrderItemID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


ALTER TABLE [dbo].[Product] ADD  DEFAULT ((0)) FOR [Price]
GO

ALTER TABLE [dbo].[BasketItem]  WITH CHECK ADD  CONSTRAINT [FK_BasketItem_Basket_Shop_Basket] FOREIGN KEY([BasketID])
REFERENCES [dbo].[Basket] ([BasketID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BasketItem] CHECK CONSTRAINT [FK_BasketItem_Basket_Shop_Basket]
GO

ALTER TABLE [dbo].[BasketItem]  WITH CHECK ADD  CONSTRAINT [FK_BasketItem_Product_Shop_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ProductID])
GO
ALTER TABLE [dbo].[BasketItem] CHECK CONSTRAINT [FK_BasketItem_Product_Shop_Product]
GO

ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD  CONSTRAINT [FK_OrderItem_Order_Shop_Order] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Order] ([OrderID])
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [FK_OrderItem_Order_Shop_Order]
GO

ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD  CONSTRAINT [FK_OrderItem_Product_Shop_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ProductID])
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [FK_OrderItem_Product_Shop_Product]
GO
