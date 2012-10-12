CREATE TABLE [dbo].[Universities]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(50) NOT NULL, 
    [Address] VARCHAR(100) NULL, 
    [City] VARCHAR(100) NOT NULL, 
    [State] CHAR(2) NOT NULL, 
    [Zip] VARCHAR(10) NOT NULL, 
    [Country] CHAR(3) NOT NULL
)
