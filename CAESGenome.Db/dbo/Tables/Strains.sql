CREATE TABLE [dbo].[Strains] (
    [Id]         INT        IDENTITY (1, 1) NOT NULL,
    [Name]       VARCHAR(100) NOT NULL,
    [Supplied]   BIT        NOT NULL,
    [BacteriaId] INT        NOT NULL,
    CONSTRAINT [PK_Strains] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Strains_Bacteria] FOREIGN KEY ([BacteriaId]) REFERENCES [dbo].[Bacteria] ([Id])
);

