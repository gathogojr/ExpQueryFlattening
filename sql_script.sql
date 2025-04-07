USE [SalesDB]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sales]') AND type in (N'U'))
ALTER TABLE [dbo].[Sales] DROP CONSTRAINT IF EXISTS [FK_Sales_Products]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sales]') AND type in (N'U'))
ALTER TABLE [dbo].[Sales] DROP CONSTRAINT IF EXISTS [FK_Sales_Customers]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Products]') AND type in (N'U'))
ALTER TABLE [dbo].[Products] DROP CONSTRAINT IF EXISTS [FK_Products_Categories]
GO
/****** Object:  Table [dbo].[Sales]    Script Date: 07-Apr-25 9:55:37 AM ******/
DROP TABLE IF EXISTS [dbo].[Sales]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 07-Apr-25 9:55:37 AM ******/
DROP TABLE IF EXISTS [dbo].[Products]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 07-Apr-25 9:55:37 AM ******/
DROP TABLE IF EXISTS [dbo].[Customers]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 07-Apr-25 9:55:37 AM ******/
DROP TABLE IF EXISTS [dbo].[Categories]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 07-Apr-25 9:55:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [varchar](50) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 07-Apr-25 9:55:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[Id] [varchar](50) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 07-Apr-25 9:55:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [varchar](50) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[CategoryId] [varchar](50) NOT NULL,
	[TaxRate] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sales]    Script Date: 07-Apr-25 9:55:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sales](
	[Id] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[Quarter] [varchar](50) NOT NULL,
	[CustomerId] [varchar](50) NOT NULL,
	[ProductId] [varchar](50) NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Sales] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (N'PG1', N'Food')
GO
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (N'PG2', N'Non-Food')
GO
INSERT [dbo].[Customers] ([Id], [Name]) VALUES (N'C1', N'Joe')
GO
INSERT [dbo].[Customers] ([Id], [Name]) VALUES (N'C2', N'Sue')
GO
INSERT [dbo].[Customers] ([Id], [Name]) VALUES (N'C3', N'Sue')
GO
INSERT [dbo].[Customers] ([Id], [Name]) VALUES (N'C4', N'Luc')
GO
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [TaxRate]) VALUES (N'P1', N'Sugar', N'PG1', CAST(0.06 AS Decimal(18, 2)))
GO
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [TaxRate]) VALUES (N'P2', N'Coffee', N'PG1', CAST(0.06 AS Decimal(18, 2)))
GO
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [TaxRate]) VALUES (N'P3', N'Paper', N'PG2', CAST(0.14 AS Decimal(18, 2)))
GO
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [TaxRate]) VALUES (N'P4', N'Pencil', N'PG2', CAST(0.14 AS Decimal(18, 2)))
GO
INSERT [dbo].[Sales] ([Id], [Year], [Quarter], [CustomerId], [ProductId], [Amount]) VALUES (1, 2022, N'2022-1', N'C1', N'P3', CAST(1.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Sales] ([Id], [Year], [Quarter], [CustomerId], [ProductId], [Amount]) VALUES (2, 2022, N'2022-2', N'C1', N'P1', CAST(2.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Sales] ([Id], [Year], [Quarter], [CustomerId], [ProductId], [Amount]) VALUES (3, 2022, N'2022-3', N'C1', N'P2', CAST(4.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Sales] ([Id], [Year], [Quarter], [CustomerId], [ProductId], [Amount]) VALUES (4, 2022, N'2022-1', N'C2', N'P2', CAST(8.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Sales] ([Id], [Year], [Quarter], [CustomerId], [ProductId], [Amount]) VALUES (5, 2022, N'2022-4', N'C2', N'P3', CAST(4.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Sales] ([Id], [Year], [Quarter], [CustomerId], [ProductId], [Amount]) VALUES (6, 2022, N'2022-2', N'C3', N'P1', CAST(2.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Sales] ([Id], [Year], [Quarter], [CustomerId], [ProductId], [Amount]) VALUES (7, 2022, N'2022-3', N'C3', N'P3', CAST(1.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Sales] ([Id], [Year], [Quarter], [CustomerId], [ProductId], [Amount]) VALUES (8, 2022, N'2022-4', N'C3', N'P3', CAST(2.00 AS Decimal(18, 2)))
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Categories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Categories]
GO
ALTER TABLE [dbo].[Sales]  WITH CHECK ADD  CONSTRAINT [FK_Sales_Customers] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
GO
ALTER TABLE [dbo].[Sales] CHECK CONSTRAINT [FK_Sales_Customers]
GO
ALTER TABLE [dbo].[Sales]  WITH CHECK ADD  CONSTRAINT [FK_Sales_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[Sales] CHECK CONSTRAINT [FK_Sales_Products]
GO
