CREATE TABLE [dbo].[UserJobsGenotypingXDyes]
(
	[UserJobGenotypingId] INT NOT NULL , 
    [DyeId] INT NOT NULL, 
    PRIMARY KEY ([UserJobGenotypingId], [DyeId]), 
    CONSTRAINT [FK_UserJobsGenotypingXDyes_UserJobGenotyping] FOREIGN KEY ([UserJobGenotypingId]) REFERENCES [UserJobGenotyping]([Id]), 
    CONSTRAINT [FK_UserJobsGenotypingXDyes_Dyes] FOREIGN KEY ([DyeId]) REFERENCES [Dyes]([Id])
)
