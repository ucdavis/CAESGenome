CREATE TABLE [dbo].[UserJobDna]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [SequenceDirection] VARCHAR(15) NOT NULL, 
    [Primer1Id] INT NOT NULL, 
    [Primer2Id] INT NULL,
    CONSTRAINT [FK_UserJobDna_Primers1] FOREIGN KEY ([Primer1Id]) REFERENCES [Primers]([Id]),
	CONSTRAINT [FK_UserJobDna_Primers2] FOREIGN KEY ([Primer2Id]) REFERENCES [Primers]([Id])
)
