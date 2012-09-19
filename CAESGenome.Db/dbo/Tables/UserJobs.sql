CREATE TABLE [dbo].[UserJobs]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [UserId] INT NOT NULL, 
    [AccountId] INT NOT NULL, 
    [Name] VARCHAR(100) NOT NULL, 
    [JobTypeId] INT NOT NULL, 
    [NumberPlates] INT NOT NULL, 
    [PlateType] VARCHAR(100) NOT NULL, 
    [Comments] VARCHAR(MAX) NULL, 
    [StatusCodeId] CHAR(2) NOT NULL, 
    [LastUpdate] DATETIME NOT NULL DEFAULT getdate(), 
    [DateTimeCreated] DATETIME NOT NULL DEFAULT getdate(), 
    CONSTRAINT [FK_UserJobs_UserProfile] FOREIGN KEY ([UserId]) REFERENCES [UserProfile]([UserId]), 
    CONSTRAINT [FK_UserJobs_JobTypes] FOREIGN KEY ([JobTypeId]) REFERENCES [JobTypes]([Id]), 
    CONSTRAINT [FK_UserJobs_StatusCodes] FOREIGN KEY ([StatusCodeId]) REFERENCES [StatusCodes]([Id])
)
