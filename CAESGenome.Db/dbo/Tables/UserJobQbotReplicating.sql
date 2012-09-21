CREATE TABLE [dbo].[UserJobQbotReplicating]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [VectorId] INT NOT NULL, 
    [StrainId] INT NOT NULL, 
    [NumSourcePlates] INT NOT NULL, 
    [DestinationPlateType] VARCHAR(10) NOT NULL, 
    [Replication] INT NOT NULL, 
    [UserJobId] INT NOT NULL,
	CONSTRAINT [FK_UserJobQbotReplicating_Strains] FOREIGN KEY ([StrainId]) REFERENCES [Strains]([Id]),
	CONSTRAINT [FK_UserJobQbotReplicating_Vectors] FOREIGN KEY ([VectorId]) REFERENCES [Vectors]([Id]),
	CONSTRAINT [FK_UserJobQbotReplicating_UserJobs] FOREIGN KEY ([UserJobId]) REFERENCES [UserJobs] ([Id])
)
