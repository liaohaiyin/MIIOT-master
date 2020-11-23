USE [MIT]
GO
/****** Object:  Table [dbo].[ApplyDetail]    Script Date: 2020/11/4 18:25:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplyDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AID] [int] NULL,
	[ProductNo] [nvarchar](30) NULL,
	[ProductName] [nvarchar](30) NULL,
	[ProductType] [nvarchar](50) NULL,
	[FactoryName] [nvarchar](30) NULL,
	[Unit] [nvarchar](10) NULL,
	[VerifyNum] [int] NULL,
	[CheckNum] [int] NULL,
 CONSTRAINT [PK_ApplyDetaialInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ApplyInfo]    Script Date: 2020/11/4 18:25:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplyInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SortNo] [int] NULL,
	[ApplyName] [nvarchar](50) NULL,
	[OfficeName] [nvarchar](50) NULL,
	[StoreName] [nvarchar](50) NULL,
	[OutputName] [nvarchar](50) NULL,
	[Operator] [nvarchar](30) NULL,
 CONSTRAINT [PK_ApplyInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BillsInfo]    Script Date: 2020/11/4 18:25:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillsInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SortNo] [int] NULL,
	[BillsName] [nvarchar](50) NULL,
	[HospitalName] [nvarchar](50) NULL,
	[SupplierName] [nvarchar](50) NULL,
	[StoreName] [nvarchar](50) NULL,
	[Operator] [nvarchar](30) NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CargoDetail]    Script Date: 2020/11/4 18:25:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CargoDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CID] [int] NOT NULL,
	[ProductNo] [nvarchar](30) NULL,
	[ProductName] [nvarchar](30) NULL,
	[ProductType] [nvarchar](50) NULL,
	[FactoryName] [nvarchar](30) NULL,
	[Unit] [nvarchar](10) NULL,
	[OutputNum] [int] NULL,
	[CheckNum] [int] NULL,
	[Price] [int] NULL,
	[BatchNo] [nvarchar](30) NULL,
	[CreateDate] [date] NULL,
	[VerifyDate] [date] NULL,
 CONSTRAINT [PK_CargoDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CargoInfo]    Script Date: 2020/11/4 18:25:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CargoInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SortNo] [int] NULL,
	[CargoName] [nvarchar](50) NULL,
	[SupplierName] [nvarchar](50) NULL,
	[OutputName] [nvarchar](50) NULL,
	[Operator] [nvarchar](30) NULL,
 CONSTRAINT [PK_CargoInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LibraryDetail]    Script Date: 2020/11/4 18:25:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LibraryDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LID] [int] NOT NULL,
	[ProductNo] [nvarchar](30) NULL,
	[ProductName] [nvarchar](30) NULL,
	[ProductType] [nvarchar](50) NULL,
	[FactoryName] [nvarchar](30) NULL,
	[Unit] [nvarchar](10) NULL,
	[OutputNum] [int] NULL,
	[CheckNum] [int] NULL,
	[BatchNo] [nvarchar](30) NULL,
	[CreateDate] [date] NULL,
	[VerifyDate] [date] NULL,
 CONSTRAINT [PK_LibraryDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LibraryInfo]    Script Date: 2020/11/4 18:25:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LibraryInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SortNo] [int] NULL,
	[LibraryName] [nvarchar](50) NULL,
	[LogoutName] [nvarchar](50) NULL,
	[OutputName] [nvarchar](50) NULL,
	[StoreName] [nvarchar](50) NULL,
	[Operator] [nvarchar](30) NULL,
 CONSTRAINT [PK_LibraryInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MenuInfo]    Script Date: 2020/11/4 18:25:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MenuInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ParentID] [int] NULL,
	[SortNo] [int] NULL,
	[Name] [varchar](50) NULL,
	[Path] [varchar](200) NULL,
	[Param] [varchar](200) NULL,
	[Icon] [varchar](max) NULL,
	[PermissionInfoCode] [varchar](36) NULL,
	[Remark] [varchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ModuleInfo]    Script Date: 2020/11/4 18:25:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ModuleInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[AssemblyString] [varchar](200) NULL,
	[SystemType] [int] NULL,
	[Remark] [varchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductInfo]    Script Date: 2020/11/4 18:25:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BID] [int] NOT NULL,
	[ProductNo] [nvarchar](30) NULL,
	[ProductName] [nvarchar](30) NULL,
	[ProductType] [nvarchar](50) NULL,
	[FactoryName] [nvarchar](30) NULL,
	[Unit] [nvarchar](10) NULL,
	[VerifyNum] [int] NULL,
	[CheckNum] [int] NULL,
	[Price] [float] NULL,
	[BatchNo] [nvarchar](30) NULL,
	[CreateDate] [date] NULL,
	[VerifyDate] [date] NULL,
 CONSTRAINT [PK_ProductInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserInfo]    Script Date: 2020/11/4 18:25:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[UserInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LoginName] [varchar](50) NULL,
	[Pwd] [varchar](50) NULL,
	[Name] [varchar](50) NULL,
	[UserStatus] [int] NULL,
	[Remark] [varchar](200) NULL,
	[DelFlag] [bit] NULL,
	[RoleInfoID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[ViewApplyDetail]    Script Date: 2020/11/4 18:25:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewApplyDetail]
AS
SELECT   dbo.ApplyDetail.ID, dbo.ApplyDetail.AID, dbo.ApplyDetail.ProductNo, dbo.ApplyDetail.ProductName, 
                dbo.ApplyDetail.ProductType, dbo.ApplyDetail.FactoryName, dbo.ApplyDetail.Unit, dbo.ApplyDetail.VerifyNum, 
                dbo.ApplyDetail.CheckNum, dbo.ApplyInfo.ApplyName
FROM      dbo.ApplyDetail INNER JOIN
                dbo.ApplyInfo ON dbo.ApplyDetail.AID = dbo.ApplyInfo.ID

GO
/****** Object:  View [dbo].[ViewApplyInfo]    Script Date: 2020/11/4 18:25:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewApplyInfo]
AS
SELECT   ID, SortNo, ApplyName, OfficeName, StoreName, OutputName, Operator
FROM      dbo.ApplyInfo

GO
/****** Object:  View [dbo].[ViewBillsInfo]    Script Date: 2020/11/4 18:25:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewBillsInfo]
AS
SELECT   ID, SortNo, BillsName, HospitalName, SupplierName, StoreName, Operator
FROM      dbo.BillsInfo

GO
/****** Object:  View [dbo].[ViewCargoDetail]    Script Date: 2020/11/4 18:25:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewCargoDetail]
AS
SELECT   dbo.CargoInfo.CargoName, dbo.CargoDetail.ID, dbo.CargoDetail.CID, dbo.CargoDetail.ProductNo, 
                dbo.CargoDetail.ProductName, dbo.CargoDetail.ProductType, dbo.CargoDetail.FactoryName, dbo.CargoDetail.Unit, 
                dbo.CargoDetail.OutputNum, dbo.CargoDetail.CheckNum, dbo.CargoDetail.Price, dbo.CargoDetail.BatchNo, 
                dbo.CargoDetail.CreateDate, dbo.CargoDetail.VerifyDate
FROM      dbo.CargoDetail INNER JOIN
                dbo.CargoInfo ON dbo.CargoDetail.CID = dbo.CargoInfo.ID

GO
/****** Object:  View [dbo].[ViewCargoInfo]    Script Date: 2020/11/4 18:25:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewCargoInfo]
AS
SELECT   ID, SortNo, CargoName, SupplierName, OutputName, Operator
FROM      dbo.CargoInfo

GO
/****** Object:  View [dbo].[ViewLibraryDetail]    Script Date: 2020/11/4 18:25:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewLibraryDetail]
AS
SELECT   dbo.LibraryDetail.ID, dbo.LibraryDetail.LID, dbo.LibraryDetail.ProductNo, dbo.LibraryDetail.ProductName, 
                dbo.LibraryDetail.ProductType, dbo.LibraryDetail.FactoryName, dbo.LibraryDetail.Unit, dbo.LibraryDetail.OutputNum, 
                dbo.LibraryDetail.CheckNum, dbo.LibraryDetail.BatchNo, dbo.LibraryDetail.CreateDate, dbo.LibraryDetail.VerifyDate, 
                dbo.LibraryInfo.LibraryName
FROM      dbo.LibraryDetail INNER JOIN
                dbo.LibraryInfo ON dbo.LibraryDetail.LID = dbo.LibraryInfo.ID

GO
/****** Object:  View [dbo].[ViewLibraryInfo]    Script Date: 2020/11/4 18:25:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewLibraryInfo]
AS
SELECT   ID, SortNo, LibraryName, LogoutName, OutputName, StoreName, Operator
FROM      dbo.LibraryInfo

GO
/****** Object:  View [dbo].[ViewProfuctInfo]    Script Date: 2020/11/4 18:25:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewProfuctInfo]
AS
SELECT   dbo.ProductInfo.ID, dbo.ProductInfo.BID, dbo.ProductInfo.ProductNo, dbo.ProductInfo.ProductName, 
                dbo.ProductInfo.ProductType, dbo.ProductInfo.FactoryName, dbo.ProductInfo.Unit, dbo.ProductInfo.VerifyNum, 
                dbo.ProductInfo.CheckNum, dbo.ProductInfo.Price, dbo.ProductInfo.BatchNo, dbo.ProductInfo.CreateDate, 
                dbo.ProductInfo.VerifyDate, dbo.BillsInfo.BillsName
FROM      dbo.ProductInfo INNER JOIN
                dbo.BillsInfo ON dbo.ProductInfo.BID = dbo.BillsInfo.ID

GO
SET IDENTITY_INSERT [dbo].[ApplyDetail] ON 

INSERT [dbo].[ApplyDetail] ([ID], [AID], [ProductNo], [ProductName], [ProductType], [FactoryName], [Unit], [VerifyNum], [CheckNum]) VALUES (1, 1, N'2.55.04.32004R', N'氯霉素试剂', N'BC5380/5390/M-53LH/(500mL×4瓶/箱)', N'广州国药', N'盒', 100, 100)
INSERT [dbo].[ApplyDetail] ([ID], [AID], [ProductNo], [ProductName], [ProductType], [FactoryName], [Unit], [VerifyNum], [CheckNum]) VALUES (2, 1, N'GYSSJGG001200', N'氢氧化钠溶液', N'100ML/袋', N'广州国药', N'袋', 10, 9)
SET IDENTITY_INSERT [dbo].[ApplyDetail] OFF
SET IDENTITY_INSERT [dbo].[ApplyInfo] ON 

INSERT [dbo].[ApplyInfo] ([ID], [SortNo], [ApplyName], [OfficeName], [StoreName], [OutputName], [Operator]) VALUES (1, 1, N'YS20200305001', N'检验科', N'1号冰箱', N'冷库', N'李四')
INSERT [dbo].[ApplyInfo] ([ID], [SortNo], [ApplyName], [OfficeName], [StoreName], [OutputName], [Operator]) VALUES (2, 2, N'YS20200305002', N'化验科', N'2号冰箱', N'冷库1', N'ABC')
SET IDENTITY_INSERT [dbo].[ApplyInfo] OFF
SET IDENTITY_INSERT [dbo].[BillsInfo] ON 

INSERT [dbo].[BillsInfo] ([ID], [SortNo], [BillsName], [HospitalName], [SupplierName], [StoreName], [Operator]) VALUES (1, 1, N'YS20200305001', N'深圳北大医院', N'广州国药', N'冷库', N'Admin')
INSERT [dbo].[BillsInfo] ([ID], [SortNo], [BillsName], [HospitalName], [SupplierName], [StoreName], [Operator]) VALUES (2, 2, N'YS20200305002', N'深圳南山医院', N'深圳国药', N'冷库1', N'Admin')
INSERT [dbo].[BillsInfo] ([ID], [SortNo], [BillsName], [HospitalName], [SupplierName], [StoreName], [Operator]) VALUES (3, 3, N'YS20200305003', N'深圳南大医院', N'东莞国药', N'冷库2', N'Admin')
INSERT [dbo].[BillsInfo] ([ID], [SortNo], [BillsName], [HospitalName], [SupplierName], [StoreName], [Operator]) VALUES (4, 4, N'YS20200305004', N'深圳科大医院', N'佛山国药', N'冷库3', N'Admin')
INSERT [dbo].[BillsInfo] ([ID], [SortNo], [BillsName], [HospitalName], [SupplierName], [StoreName], [Operator]) VALUES (5, 5, N'YS20200305005', N'深圳北大医院', N'广州国药', N'冷库', N'Admin')
INSERT [dbo].[BillsInfo] ([ID], [SortNo], [BillsName], [HospitalName], [SupplierName], [StoreName], [Operator]) VALUES (7, 6, N'YS20200305006', N'深圳北大医院', N'广州国药', N'冷库', N'Admin')
INSERT [dbo].[BillsInfo] ([ID], [SortNo], [BillsName], [HospitalName], [SupplierName], [StoreName], [Operator]) VALUES (9, 7, N'YS20200305007', N'深圳北大医院', N'广州国药', N'冷库', N'Admin')
INSERT [dbo].[BillsInfo] ([ID], [SortNo], [BillsName], [HospitalName], [SupplierName], [StoreName], [Operator]) VALUES (10, 8, N'YS20200305008', N'深圳北大医院', N'广州国药', N'冷库', N'Admin')
INSERT [dbo].[BillsInfo] ([ID], [SortNo], [BillsName], [HospitalName], [SupplierName], [StoreName], [Operator]) VALUES (11, 9, N'YS20200305009', N'深圳北大医院', N'广州国药', N'冷库', N'Admin')
INSERT [dbo].[BillsInfo] ([ID], [SortNo], [BillsName], [HospitalName], [SupplierName], [StoreName], [Operator]) VALUES (12, 10, N'YS202003050010', N'深圳北大医院', N'广州国药', N'冷库', N'Admin')
INSERT [dbo].[BillsInfo] ([ID], [SortNo], [BillsName], [HospitalName], [SupplierName], [StoreName], [Operator]) VALUES (13, 11, N'YS202003050011', N'深圳北大医院', N'广州国药', N'冷库', N'Admin')
SET IDENTITY_INSERT [dbo].[BillsInfo] OFF
SET IDENTITY_INSERT [dbo].[CargoDetail] ON 

INSERT [dbo].[CargoDetail] ([ID], [CID], [ProductNo], [ProductName], [ProductType], [FactoryName], [Unit], [OutputNum], [CheckNum], [Price], [BatchNo], [CreateDate], [VerifyDate]) VALUES (1, 1, N'GYSSJGG001200', N'氢氧化钠溶液168', N'100ML/袋', N'升东药店', N'袋', 2, 2, 80, N'A001888', CAST(0x54400B00 AS Date), CAST(0x72400B00 AS Date))
INSERT [dbo].[CargoDetail] ([ID], [CID], [ProductNo], [ProductName], [ProductType], [FactoryName], [Unit], [OutputNum], [CheckNum], [Price], [BatchNo], [CreateDate], [VerifyDate]) VALUES (2, 1, N'32004R', N'氯霉素试剂9', N'(500mL×4瓶/箱)', N'广湖国药', N'袋', 1, 6, 90, N'A001', CAST(0x9F3A0B00 AS Date), CAST(0x54400B00 AS Date))
SET IDENTITY_INSERT [dbo].[CargoDetail] OFF
SET IDENTITY_INSERT [dbo].[CargoInfo] ON 

INSERT [dbo].[CargoInfo] ([ID], [SortNo], [CargoName], [SupplierName], [OutputName], [Operator]) VALUES (1, 1, N'YS20200305001', N'广佛国药', N'冷库', N'李1')
INSERT [dbo].[CargoInfo] ([ID], [SortNo], [CargoName], [SupplierName], [OutputName], [Operator]) VALUES (2, 2, N'YS20200305008', N'动佛国药', N'冷库888', N'GG')
SET IDENTITY_INSERT [dbo].[CargoInfo] OFF
SET IDENTITY_INSERT [dbo].[LibraryDetail] ON 

INSERT [dbo].[LibraryDetail] ([ID], [LID], [ProductNo], [ProductName], [ProductType], [FactoryName], [Unit], [OutputNum], [CheckNum], [BatchNo], [CreateDate], [VerifyDate]) VALUES (1, 1, N'2.55.04.32004R', N'氯霉素试剂', N'BC5380/5390/M-53LH/(500mL×4瓶/箱)', N'广州国药', N'盒', 6, 6, N'2019112600', CAST(0x72400B00 AS Date), CAST(0x72400B00 AS Date))
INSERT [dbo].[LibraryDetail] ([ID], [LID], [ProductNo], [ProductName], [ProductType], [FactoryName], [Unit], [OutputNum], [CheckNum], [BatchNo], [CreateDate], [VerifyDate]) VALUES (2, 1, N'123456111', N'医用酒精', N'100ML/瓶', N'广州国药', N'瓶', 0, 1, N'202003001', CAST(0x08350B00 AS Date), CAST(0x76360B00 AS Date))
SET IDENTITY_INSERT [dbo].[LibraryDetail] OFF
SET IDENTITY_INSERT [dbo].[LibraryInfo] ON 

INSERT [dbo].[LibraryInfo] ([ID], [SortNo], [LibraryName], [LogoutName], [OutputName], [StoreName], [Operator]) VALUES (1, 1, N'YS20200305001', N'检验科', N'1号冰箱', N'冷库', N'Guest')
INSERT [dbo].[LibraryInfo] ([ID], [SortNo], [LibraryName], [LogoutName], [OutputName], [StoreName], [Operator]) VALUES (2, 2, N'YS20200305002', N'专科', N'小冰箱', N'冷库22', N'MM')
SET IDENTITY_INSERT [dbo].[LibraryInfo] OFF
SET IDENTITY_INSERT [dbo].[MenuInfo] ON 

INSERT [dbo].[MenuInfo] ([ID], [ParentID], [SortNo], [Name], [Path], [Param], [Icon], [PermissionInfoCode], [Remark]) VALUES (1, -1, 1, N'验收管理', N'/MIIOT.DiagManager;Component/Pages/WorkAcceptInfoPage.xaml', NULL, N'Icon-Home', NULL, NULL)
INSERT [dbo].[MenuInfo] ([ID], [ParentID], [SortNo], [Name], [Path], [Param], [Icon], [PermissionInfoCode], [Remark]) VALUES (3, -1, 2, N'申领管理', N'/MIIOT.DiagManager;Component/Pages/ApplyBudgetPage.xaml', NULL, N'Icon-Record', NULL, NULL)
INSERT [dbo].[MenuInfo] ([ID], [ParentID], [SortNo], [Name], [Path], [Param], [Icon], [PermissionInfoCode], [Remark]) VALUES (4, -1, 3, N'退库管理', N'/MIIOT.DiagManager;Component/Pages/FallbackLibraryPage.xaml', NULL, N'Icon-Money', NULL, NULL)
INSERT [dbo].[MenuInfo] ([ID], [ParentID], [SortNo], [Name], [Path], [Param], [Icon], [PermissionInfoCode], [Remark]) VALUES (5, -1, 4, N'退货管理', N'/MIIOT.DiagManager;Component/Pages/FallBackCargoPage.xaml', NULL, N'Icon-Visitor', NULL, NULL)
INSERT [dbo].[MenuInfo] ([ID], [ParentID], [SortNo], [Name], [Path], [Param], [Icon], [PermissionInfoCode], [Remark]) VALUES (6, -1, 5, N'重打标签', N'/MIIOT.DiagManager;Component/Pages/PrintLabelPage.xaml', NULL, N'Icon-Report', NULL, NULL)
INSERT [dbo].[MenuInfo] ([ID], [ParentID], [SortNo], [Name], [Path], [Param], [Icon], [PermissionInfoCode], [Remark]) VALUES (7, -1, 6, N'领用', N'/MIIOT.DiagManager;Component/Pages/CommMenuPage.xaml', NULL, N'Icon-Save', NULL, NULL)
SET IDENTITY_INSERT [dbo].[MenuInfo] OFF
SET IDENTITY_INSERT [dbo].[ProductInfo] ON 

INSERT [dbo].[ProductInfo] ([ID], [BID], [ProductNo], [ProductName], [ProductType], [FactoryName], [Unit], [VerifyNum], [CheckNum], [Price], [BatchNo], [CreateDate], [VerifyDate]) VALUES (1, 1, N'2.55.04.32004R', N'氯霉素试剂', N'BC5380/5390/M-53LH/(500mL×4瓶/箱)', N'广州国药', N'盒', 100, 100, 100, N'2019112600', CAST(0x72400B00 AS Date), CAST(0xE0410B00 AS Date))
INSERT [dbo].[ProductInfo] ([ID], [BID], [ProductNo], [ProductName], [ProductType], [FactoryName], [Unit], [VerifyNum], [CheckNum], [Price], [BatchNo], [CreateDate], [VerifyDate]) VALUES (2, 1, N'GYSSJGG001200', N'氢氧化钠溶液', N'100ML/袋', N'广州国药', N'袋', 9, 10, 10, N'2019112655', CAST(0x72400B00 AS Date), CAST(0x78400B00 AS Date))
SET IDENTITY_INSERT [dbo].[ProductInfo] OFF
SET IDENTITY_INSERT [dbo].[UserInfo] ON 

INSERT [dbo].[UserInfo] ([ID], [LoginName], [Pwd], [Name], [UserStatus], [Remark], [DelFlag], [RoleInfoID]) VALUES (1, N'admin', N'xXjfuiPQpFE=', N'admin', 0, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[UserInfo] OFF
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ApplyDetail"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 146
               Right = 207
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ApplyInfo"
            Begin Extent = 
               Top = 0
               Left = 345
               Bottom = 140
               Right = 509
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewApplyDetail'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewApplyDetail'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ApplyInfo"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 146
               Right = 202
            End
            DisplayFlags = 280
            TopColumn = 3
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewApplyInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewApplyInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "BillsInfo"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 146
               Right = 214
            End
            DisplayFlags = 280
            TopColumn = 3
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1905
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewBillsInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewBillsInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "CargoDetail"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 146
               Right = 207
            End
            DisplayFlags = 280
            TopColumn = 9
         End
         Begin Table = "CargoInfo"
            Begin Extent = 
               Top = 6
               Left = 245
               Bottom = 146
               Right = 417
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 15
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewCargoDetail'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewCargoDetail'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "CargoInfo"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 146
               Right = 210
            End
            DisplayFlags = 280
            TopColumn = 2
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewCargoInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewCargoInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "LibraryDetail"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 146
               Right = 207
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "LibraryInfo"
            Begin Extent = 
               Top = 6
               Left = 245
               Bottom = 146
               Right = 410
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewLibraryDetail'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewLibraryDetail'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "LibraryInfo"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 146
               Right = 203
            End
            DisplayFlags = 280
            TopColumn = 3
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewLibraryInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewLibraryInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ProductInfo"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 146
               Right = 207
            End
            DisplayFlags = 280
            TopColumn = 9
         End
         Begin Table = "BillsInfo"
            Begin Extent = 
               Top = 6
               Left = 245
               Bottom = 219
               Right = 431
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 12
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewProfuctInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewProfuctInfo'
GO
