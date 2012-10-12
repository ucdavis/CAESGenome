CREATE TABLE [dbo].[Departments]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(100) NOT NULL, 
    [UniversityId] INT NOT NULL, 
    CONSTRAINT [FK_Departments_Universities] FOREIGN KEY ([UniversityId]) REFERENCES [Universities]([Id])
)
