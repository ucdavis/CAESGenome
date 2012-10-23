CREATE TABLE [dbo].[BarcodeFiles]
(
	[Id] INT NOT NULL PRIMARY KEY,
    [ResultFile] VARBINARY(MAX) NOT NULL, 
    [ResultFileName] VARCHAR(100) NOT NULL, 
    [ResultContentType] VARCHAR(50) NOT NULL, 
    [ValidationFile] VARBINARY(MAX) NULL, 
    [ValidationFileName] VARCHAR(100) NULL, 
    [ValidationContentType] VARCHAR(50) NULL, 
    [Column] INT NOT NULL, 
    [Row] CHAR NOT NULL, 
    [BarcodeId] INT NOT NULL, 
    CONSTRAINT [FK_BarcodeFiles_Barcodes] FOREIGN KEY ([BarcodeId]) REFERENCES [Barcodes]([Id])
	
)
