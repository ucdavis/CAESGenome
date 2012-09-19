CREATE TABLE [dbo].[Departments]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(50) NOT NULL, 
    [UniversityId] INT NOT NULL, 
    CONSTRAINT [FK_Departments_Universities] FOREIGN KEY ([UniversityId]) REFERENCES [Universities]([Id])
)
