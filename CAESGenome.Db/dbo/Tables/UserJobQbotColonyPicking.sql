CREATE TABLE [dbo].[UserJobQbotColonyPicking]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [VectorId] INT NOT NULL, 
    [StrainId] INT NOT NULL, 
    [NumQTrays] INT NOT NULL, 
    [NumGlycerol] INT NOT NULL, 
    [Concentration] VARCHAR(50) NOT NULL, 
    [Replication] INT NOT NULL, 
    [NumColonies] INT NOT NULL, 
	CONSTRAINT [FK_UserJobQbotColonyPicking_Strains] FOREIGN KEY ([StrainId]) REFERENCES [Strains]([Id]),
	CONSTRAINT [FK_UserJobQbotColonyPicking_Vectors] FOREIGN KEY ([VectorId]) REFERENCES [Vectors]([Id])
)
