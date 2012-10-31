CREATE TABLE [dbo].[BarcodeFiles]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [WellColumn] INT NOT NULL, 
    [WellRow] CHAR NOT NULL, 
    [BarcodeId] INT NOT NULL, 
    [Uploaded] BIT NOT NULL DEFAULT 1, 
    [Validated] BIT NOT NULL DEFAULT 0, 
    [Start] INT NULL, 
    [End] INT NULL, 
    [DateTimeUploaded] DATETIME2 NOT NULL DEFAULT getdate(), 
    [DateTimeValidated] DATETIME2 NULL, 
    CONSTRAINT [FK_BarcodeFiles_Barcodes] FOREIGN KEY ([BarcodeId]) REFERENCES [Barcodes]([Id])
	
)
