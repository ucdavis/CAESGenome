CREATE TABLE [dbo].[Stages]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(50) NOT NULL, 
    [Order] INT NOT NULL, 
    [JobTypeId] INT NOT NULL, 
    [IsComplete] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_Stages_JobTypes] FOREIGN KEY ([JobTypeId]) REFERENCES [JobTypes]([Id])
)
