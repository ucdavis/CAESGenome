﻿CREATE TABLE [dbo].[UserJobs]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NOT NULL, 
    [RechargeAccountId] INT NOT NULL, 
    [Name] VARCHAR(100) NOT NULL, 
    [JobTypeId] INT NOT NULL, 
    [NumberPlates] INT NOT NULL, 
    [PlateType] VARCHAR(100) NULL, 
    [Comments] VARCHAR(MAX) NULL, 
    [StageId] CHAR(4) NULL, 
	[IsOpen]	bit not null default 1,
    [LastUpdate] DATETIME NOT NULL DEFAULT getdate(), 
    [DateTimeCreated] DATETIME NOT NULL DEFAULT getdate(), 
    [UserJobBacterialCloneId] INT NULL, 
    [UserJobDnaId] INT NULL, 
    [UserJobSublibraryId] INT NULL, 
    [UserJobUserRunId] INT NULL, 
	[UserJobQbotColonyPickingId] INT NULL, 
    [UserJobQbotGriddingId] INT NULL, 
    [UserJobQbotReplicatingId] INT NULL, 
    CONSTRAINT [FK_UserJobs_UserProfile] FOREIGN KEY ([UserId]) REFERENCES [UserProfile]([UserId]), 
    CONSTRAINT [FK_UserJobs_JobTypes] FOREIGN KEY ([JobTypeId]) REFERENCES [JobTypes]([Id]), 
    CONSTRAINT [FK_UserJobs_Stages] FOREIGN KEY ([StageId]) REFERENCES [Stages]([Id]), 
    CONSTRAINT [FK_UserJobs_UserJobBacterialClone] FOREIGN KEY ([UserJobBacterialCloneId]) REFERENCES [UserJobBacterialClone]([Id]), 
    CONSTRAINT [FK_UserJobs_UserJobDna] FOREIGN KEY ([UserJobDnaId]) REFERENCES [UserJobDna]([Id]), 
    CONSTRAINT [FK_UserJobs_UserJobSublibrary] FOREIGN KEY ([UserJobSublibraryId]) REFERENCES [UserJobSublibrary]([Id]), 
    CONSTRAINT [FK_UserJobs_UserJobUserRun] FOREIGN KEY ([UserJobUserRunId]) REFERENCES [UserJobUserRun]([Id]), 
    CONSTRAINT [FK_UserJobs_UserJobQbotColonyPicking] FOREIGN KEY ([UserJobQbotColonyPickingId]) REFERENCES [UserJobQbotColonyPicking]([Id]), 
    CONSTRAINT [FK_UserJobs_UserJobQbotGridding] FOREIGN KEY ([UserJobQbotGriddingId]) REFERENCES [UserJobQbotGridding]([Id]), 
    CONSTRAINT [FK_UserJobs_UserJobQbotReplicating] FOREIGN KEY ([UserJobQbotReplicatingId]) REFERENCES [UserJobQbotReplicating]([Id])
)
