CREATE TABLE [dbo].[Primers] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [Name]      VARCHAR (50)  NOT NULL,
    [ShortName] VARCHAR (255) NOT NULL,
    [Supplied]  BIT           NOT NULL,
    [Sequence]  VARCHAR (MAX) NULL,
    CONSTRAINT [PK_Primers] PRIMARY KEY CLUSTERED ([Id] ASC)
);

