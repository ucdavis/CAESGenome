CREATE TABLE [dbo].[BarcodeFiles]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [Column] INT NOT NULL, 
    [Row] CHAR NOT NULL, 
    [BarcodeId] INT NOT NULL, 
    [Uploaded] BIT NOT NULL DEFAULT 1, 
    [Validated] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_BarcodeFiles_Barcodes] FOREIGN KEY ([BarcodeId]) REFERENCES [Barcodes]([Id])
	
)
