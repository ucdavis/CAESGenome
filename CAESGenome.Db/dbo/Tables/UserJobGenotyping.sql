CREATE TABLE [dbo].[UserJobGenotyping]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [DyeId] INT NOT NULL, 
    CONSTRAINT [FK_UserJobGenotyping_Dyes] FOREIGN KEY ([DyeId]) REFERENCES [Dyes]([Id])
)
