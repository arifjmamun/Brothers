USE [Brothers_DB]
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([CategoryId], [CategoryName]) VALUES (1, N'Movies')
INSERT [dbo].[Categories] ([CategoryId], [CategoryName]) VALUES (2, N'Softwares')
INSERT [dbo].[Categories] ([CategoryId], [CategoryName]) VALUES (3, N'Music Video')
INSERT [dbo].[Categories] ([CategoryId], [CategoryName]) VALUES (4, N'Music')
INSERT [dbo].[Categories] ([CategoryId], [CategoryName]) VALUES (5, N'TV Series')
INSERT [dbo].[Categories] ([CategoryId], [CategoryName]) VALUES (6, N'Others')
SET IDENTITY_INSERT [dbo].[Categories] OFF
SET IDENTITY_INSERT [dbo].[SubCategories] ON 

INSERT [dbo].[SubCategories] ([SubCategoryId], [SubCategoryName]) VALUES (1, N'Hindi')
INSERT [dbo].[SubCategories] ([SubCategoryId], [SubCategoryName]) VALUES (2, N'English')
INSERT [dbo].[SubCategories] ([SubCategoryId], [SubCategoryName]) VALUES (3, N'Windows')
INSERT [dbo].[SubCategories] ([SubCategoryId], [SubCategoryName]) VALUES (4, N'Bangla')
INSERT [dbo].[SubCategories] ([SubCategoryId], [SubCategoryName]) VALUES (5, N'Kolkata')
INSERT [dbo].[SubCategories] ([SubCategoryId], [SubCategoryName]) VALUES (6, N'Tamil/Telugu')
SET IDENTITY_INSERT [dbo].[SubCategories] OFF
