CREATE TABLE [dbo].[VisitedProducts](
	[UserId] [nvarchar](255) NOT NULL,
	[ProductId] [int] NOT NULL,
	[TimesOfVisit] [int] NOT NULL,
 CONSTRAINT [PK_VisitedProducts] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[VisitedProducts]  WITH CHECK ADD  CONSTRAINT [FK_VisitedProducts_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[VisitedProducts] CHECK CONSTRAINT [FK_VisitedProducts_Product]
GO

ALTER TABLE [dbo].[VisitedProducts]  WITH CHECK ADD  CONSTRAINT [FK_VisitedProducts_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Customer] ([Email])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[VisitedProducts] CHECK CONSTRAINT [FK_VisitedProducts_User]
GO

