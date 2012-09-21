CREATE TABLE [dbo].[UserProfilesXRechargeAccounts]
(
	[UserProfileId] INT NOT NULL , 
    [RechargeAccountId] INT NOT NULL, 
    PRIMARY KEY ([UserProfileId], [RechargeAccountId]), 
    CONSTRAINT [FK_UserProfilesXRechargeAccounts_UserProfiles] FOREIGN KEY ([UserProfileId]) REFERENCES [UserProfile]([UserId]), 
    CONSTRAINT [FK_UserProfilesXRechargeAccounts_RechargeAccounts] FOREIGN KEY ([RechargeAccountId]) REFERENCES [RechargeAccounts]([Id])
)
