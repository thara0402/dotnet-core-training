USE [Shop]
GO

SET IDENTITY_INSERT [dbo].[Product] ON
INSERT [dbo].[Product] ([ProductID], [Desc], [Name], [Price]) VALUES (1, N'Apple. New MacBook', N'MacBook Pro', CAST(230000.000000 AS Decimal(28, 6)))
INSERT [dbo].[Product] ([ProductID], [Desc], [Name], [Price]) VALUES (2, N'Microsoft. New Surface', N'Surface Laptop 3', CAST(300000.000000 AS Decimal(28, 6)))
INSERT [dbo].[Product] ([ProductID], [Desc], [Name], [Price]) VALUES (3, N'Lenovo. New Laptop', N'ThinkPad X1 Carbon', CAST(220000.000000 AS Decimal(28, 6)))
SET IDENTITY_INSERT [dbo].[Product] OFF
