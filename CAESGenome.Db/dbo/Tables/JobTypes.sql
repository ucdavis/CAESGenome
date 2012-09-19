CREATE TABLE [dbo].[JobTypes] (
    [Id]   INT          IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_JobTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

