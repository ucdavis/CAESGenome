CREATE TABLE [dbo].[UserJobQbotGridding]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [VectorId] INT NOT NULL, 
    [StrainId] INT NOT NULL, 
    [NumSourcePlates] INT NOT NULL, 
    [CopiesMembrane] INT NOT NULL, 
    [Pattern] VARCHAR(50) NOT NULL, 
    [UserJobId] INT NOT NULL,
	CONSTRAINT [FK_UserJobQbotGridding_Strains] FOREIGN KEY ([StrainId]) REFERENCES [Strains]([Id]),
	CONSTRAINT [FK_UserJobQbotGridding_Vectors] FOREIGN KEY ([VectorId]) REFERENCES [Vectors]([Id]),
	CONSTRAINT [FK_UserJobQbotGridding_UserJobs] FOREIGN KEY ([UserJobId]) REFERENCES [UserJobs] ([Id])
)
