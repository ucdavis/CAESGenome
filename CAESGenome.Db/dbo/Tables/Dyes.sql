CREATE TABLE [dbo].[Dyes] (
    [Id]         INT          IDENTITY (1, 1) NOT NULL,
    [Name]       VARCHAR (50) NOT NULL,
    [Supplied]   BIT          NOT NULL,
    [Genotyping] BIT          NOT NULL,
    CONSTRAINT [PK_Dyes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

