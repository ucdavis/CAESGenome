CREATE TABLE [dbo].[UserProfile] (
    [UserId]   INT           IDENTITY (1, 1) NOT NULL,
    [UserName] NVARCHAR (56) NOT NULL,
    [FirstName] VARCHAR(50) NOT NULL, 
    [LastName] VARCHAR(50) NOT NULL, 
	[Title]	varchar(50) NULL,
    [Phone] VARCHAR(20) NULL, 
    [Fax] VARCHAR(15) NULL, 
    [DateCreated] DATETIME NOT NULL DEFAULT getdate(), 
    [UniversityId] INT NULL, 
    [DepartmentId] INT NULL, 
    [ParentUserId] INT NULL, 
    PRIMARY KEY CLUSTERED ([UserId] ASC),
    UNIQUE NONCLUSTERED ([UserName] ASC), 
    CONSTRAINT [FK_UserProfile_Universities] FOREIGN KEY ([UniversityId]) REFERENCES [Universities]([Id]), 
    CONSTRAINT [FK_UserProfile_Departments] FOREIGN KEY ([DepartmentId]) REFERENCES [Departments]([Id])
);

