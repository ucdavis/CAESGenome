CREATE TABLE [dbo].[UserJobQbotGridding]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [VectorId] INT NOT NULL, 
    [StrainId] INT NOT NULL, 
    [NumMembraneCopies] INT NOT NULL, 
    [GriddingPattern] VARCHAR(50) NOT NULL, 
	CONSTRAINT [FK_UserJobQbotGridding_Strains] FOREIGN KEY ([StrainId]) REFERENCES [Strains]([Id]),
	CONSTRAINT [FK_UserJobQbotGridding_Vectors] FOREIGN KEY ([VectorId]) REFERENCES [Vectors]([Id])
)
