CREATE TABLE [dbo].[UserProfilesXRechargeAccounts]
(
	[UserId] INT NOT NULL , 
    [RechargeAccountId] INT NOT NULL, 
    PRIMARY KEY ([UserId], [RechargeAccountId]), 
    CONSTRAINT [FK_UserProfilesXRechargeAccounts_UserProfiles] FOREIGN KEY ([UserId]) REFERENCES [UserProfile]([UserId]), 
    CONSTRAINT [FK_UserProfilesXRechargeAccounts_RechargeAccounts] FOREIGN KEY ([RechargeAccountId]) REFERENCES [RechargeAccounts]([Id])
)
