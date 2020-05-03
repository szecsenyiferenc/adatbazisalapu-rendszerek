CREATE TABLE [dbo].[ProductOnStorage](
	[StorageId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_ProductOnStorage] PRIMARY KEY CLUSTERED 
(
	[StorageId] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ProductOnStorage]  WITH CHECK ADD  CONSTRAINT [FK_ProductOnStorage_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[ProductOnStorage] CHECK CONSTRAINT [FK_ProductOnStorage_Product]
GO

ALTER TABLE [dbo].[ProductOnStorage]  WITH CHECK ADD  CONSTRAINT [FK_ProductOnStorage_Storage] FOREIGN KEY([StorageId])
REFERENCES [dbo].[Storage] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[ProductOnStorage] CHECK CONSTRAINT [FK_ProductOnStorage_Storage]
GO

