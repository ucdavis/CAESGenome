CREATE TABLE [dbo].[UserJobUserRun]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [SequenceDirection] VARCHAR(15) NOT NULL, 
    [DyeId] INT NOT NULL, 
    CONSTRAINT [FK_UserJobUserRun_Dyes] FOREIGN KEY ([DyeId]) REFERENCES [Dyes]([Id])

)
