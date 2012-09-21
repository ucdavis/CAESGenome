CREATE TABLE [dbo].[UserJobGenotyping]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [DyeId] INT NOT NULL, 
    [UserJobId] INT NOT NULL,
	CONSTRAINT [FK_UserJobGenotyping_UserJobs] FOREIGN KEY ([UserJobId]) REFERENCES [UserJobs] ([Id]), 
    CONSTRAINT [FK_UserJobGenotyping_Dyes] FOREIGN KEY ([DyeId]) REFERENCES [Dyes]([Id])
)
