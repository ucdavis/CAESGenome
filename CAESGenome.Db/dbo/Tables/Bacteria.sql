CREATE TABLE [dbo].[Bacteria] (
    [Id]   INT          IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Bacteria] PRIMARY KEY CLUSTERED ([Id] ASC)
);

