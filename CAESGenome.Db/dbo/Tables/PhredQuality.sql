CREATE TABLE [dbo].[PhredQualities] (
    [Id]                INT      IDENTITY (1, 1) NOT NULL,
    [BarcodeId]         INT      NOT NULL,
    [WellColumn]          INT      NOT NULL,
    [WellRow]           CHAR      NOT NULL,
    [Start]             INT      NOT NULL,
    [End]               INT      NOT NULL,
    [DateTimeSubmitted] DATETIME NOT NULL,
    CONSTRAINT [PK_PhredQuality] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PhredQuality_Barcodes] FOREIGN KEY ([BarcodeId]) REFERENCES [dbo].[Barcodes] ([Id])
);

