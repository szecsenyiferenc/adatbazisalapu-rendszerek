CREATE TABLE [dbo].[PurchasedProducts](
	[CartId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_PurchasedProducts] PRIMARY KEY CLUSTERED 
(
	[CartId] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[PurchasedProducts]  WITH CHECK ADD  CONSTRAINT [FK_PurchasedProducts_Cart] FOREIGN KEY([CartId])
REFERENCES [dbo].[Cart] ([CartId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[PurchasedProducts] CHECK CONSTRAINT [FK_PurchasedProducts_Cart]
GO

ALTER TABLE [dbo].[PurchasedProducts]  WITH CHECK ADD  CONSTRAINT [FK_PurchasedProducts_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[PurchasedProducts] CHECK CONSTRAINT [FK_PurchasedProducts_Product]
GO

