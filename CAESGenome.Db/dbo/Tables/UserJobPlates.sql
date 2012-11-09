CREATE TABLE [dbo].[UserJobPlates]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserJobId] INT NOT NULL, 
    [Name] VARCHAR(50) NOT NULL, 
    [Completed] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_UserJobPlates_UserJobs] FOREIGN KEY ([UserJobId]) REFERENCES [UserJobs]([Id])
)
