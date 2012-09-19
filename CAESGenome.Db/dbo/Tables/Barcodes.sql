CREATE TABLE [dbo].[Barcodes] (
    [Id]            INT  IDENTITY (1, 1) NOT NULL,
    [PlateId]       INT  NULL,
    [PlateSubId]    INT  NULL,
    [PrimerId]      INT  NULL,
    [Stage]         INT  NULL,
    [SourceBarcode] INT  NULL,
    [Date]          DATE NULL,
    [Done]          BIT  NULL,
    CONSTRAINT [PK_Barcodes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Barcodes_Primers] FOREIGN KEY ([PrimerId]) REFERENCES [dbo].[Primers] ([Id])
);

