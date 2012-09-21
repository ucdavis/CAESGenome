CREATE TABLE [dbo].[UserJobSubLibrary]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [SampleType] VARCHAR(3) NOT NULL, 
    [DnaConcentration] FLOAT NULL, 
    [GenomeSize] FLOAT NULL, 
    [Coverage] INT NULL, 
    [VectorId] INT NULL, 
    [AntibioticId] INT NOT NULL, 
    [UserJobId] INT NOT NULL ,
	CONSTRAINT [FK_UserJobSubLibrary_Antibiotics] FOREIGN KEY ([AntibioticID]) REFERENCES [Antibiotics]([Id]),
	CONSTRAINT [FK_UserJobSubLibrary_Vectors] FOREIGN KEY ([VectorId]) REFERENCES [Vectors]([Id]),
	CONSTRAINT [FK_UserJobSubLibrary_UserJobs] FOREIGN KEY ([UserJobId]) REFERENCES [UserJobs] ([Id])
)
