﻿CREATE TABLE [dbo].[Stages]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(50) NOT NULL IDENTITY, 
    [Order] INT NOT NULL, 
    [JobTypeId] INT NOT NULL, 
    CONSTRAINT [FK_Stages_JobTypes] FOREIGN KEY ([JobTypeId]) REFERENCES [JobTypes]([Id])
)
