CREATE TABLE [dbo].[PhredQualityStats] (
    [Id]                INT      IDENTITY (1, 1) NOT NULL,
    [BarcodeId]         INT      NOT NULL,
    [QualityTotal]      INT      NOT NULL,
    [WellTotal]         INT      NOT NULL,
    [DateTimeSubmitted] DATETIME NOT NULL,
    [UserDownloadable]  BIT      NOT NULL,
    CONSTRAINT [PK_QualityPhredStats] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PhredQualityStats_Barcodes] FOREIGN KEY ([BarcodeId]) REFERENCES [dbo].[Barcodes] ([Id])
);

