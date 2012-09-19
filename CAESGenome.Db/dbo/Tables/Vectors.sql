CREATE TABLE [dbo].[Vectors] (
    [Id]            INT          IDENTITY (1, 1) NOT NULL,
    [Name]          VARCHAR (50) NOT NULL,
    [Antibiotic1Id] INT          NOT NULL,
    [Antibiotic2Id] INT          NOT NULL,
    [VectorTypeId]  INT          NOT NULL,
    CONSTRAINT [PK_Vectors] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Vectors_Antibiotics] FOREIGN KEY ([Antibiotic1Id]) REFERENCES [dbo].[Antibiotics] ([Id]),
    CONSTRAINT [FK_Vectors_Antibiotics1] FOREIGN KEY ([Antibiotic2Id]) REFERENCES [dbo].[Antibiotics] ([Id]),
    CONSTRAINT [FK_Vectors_VectorTypes] FOREIGN KEY ([VectorTypeId]) REFERENCES [dbo].[VectorTypes] ([Id])
);

