CREATE TABLE [dbo].[UserJobQbotReplicating]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [VectorId] INT NOT NULL, 
    [StrainId] INT NOT NULL, 
    [NumSourcePlates] INT NOT NULL, 
    [DestinationPlateType] VARCHAR(20) NOT NULL, 
    [NumCopies] INT NOT NULL, 
	CONSTRAINT [FK_UserJobQbotReplicating_Strains] FOREIGN KEY ([StrainId]) REFERENCES [Strains]([Id]),
	CONSTRAINT [FK_UserJobQbotReplicating_Vectors] FOREIGN KEY ([VectorId]) REFERENCES [Vectors]([Id])
)
