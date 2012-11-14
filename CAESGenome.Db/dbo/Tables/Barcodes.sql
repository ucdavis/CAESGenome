CREATE TABLE [dbo].[Barcodes] (
    [Id]            INT  IDENTITY (1, 1) NOT NULL,
    [UserJobPlateId]       INT  NULL,
    [SubPlateId]    INT  NULL,
    [PrimerId]      INT  NULL,
    [StageId]         char(4)  NULL,
    [SourceBarcodeId] INT  NULL,
    [DateCreated]          DATETIME NULL,
    [Done]          BIT  NOT NULL DEFAULT 0,
    
    [AllowDownload] BIT NOT NULL DEFAULT 0, 
    [DateTimeValidated] DATETIME NULL, 
    CONSTRAINT [PK_Barcodes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Barcodes_Primers] FOREIGN KEY ([PrimerId]) REFERENCES [dbo].[Primers] ([Id]), 
    CONSTRAINT [FK_Barcodes_Barcodes] FOREIGN KEY ([SourceBarcodeId]) REFERENCES [Barcodes]([Id]), 
    CONSTRAINT [FK_Barcodes_Stages] FOREIGN KEY ([StageId]) REFERENCES [Stages]([Id])
);

