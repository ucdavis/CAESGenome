CREATE TABLE [dbo].[UserJobBacterialClone]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [SequenceDirection] VARCHAR(15) NOT NULL, 
    [Primer1Id] INT NOT NULL, 
    [Primer2Id] INT NULL, 
    [StrainId] INT NOT NULL, 
    [VectorId] INT NOT NULL, 
    [AntibioticId] INT NOT NULL, 
    CONSTRAINT [FK_UserJobBacterialClone_Primers1] FOREIGN KEY ([Primer1Id]) REFERENCES [Primers]([Id]),
	CONSTRAINT [FK_UserJobBacterialClone_Primers2] FOREIGN KEY ([Primer2Id]) REFERENCES [Primers]([Id]),
	CONSTRAINT [FK_UserJobBacterialClone_Strains] FOREIGN KEY ([StrainId]) REFERENCES [Strains]([Id]),
	CONSTRAINT [FK_UserJobBacterialClone_Vectors] FOREIGN KEY ([VectorId]) REFERENCES [Vectors]([Id]),
	CONSTRAINT [FK_UserJobBacterialClone_Antibiotics] FOREIGN KEY ([AntibioticId]) REFERENCES [Antibiotics]([Id])
)
