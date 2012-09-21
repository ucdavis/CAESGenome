CREATE TABLE [dbo].[UserJobUserRun]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [SequenceDirection] VARCHAR(15) NOT NULL, 
    [DyeId] INT NOT NULL, 
    [UserJobId] INT NOT NULL,
	CONSTRAINT [FK_UserJobUserRun_UserJobs] FOREIGN KEY ([UserJobId]) REFERENCES [UserJobs] ([Id]), 
    CONSTRAINT [FK_UserJobUserRun_Dyes] FOREIGN KEY ([DyeId]) REFERENCES [Dyes]([Id])

)
