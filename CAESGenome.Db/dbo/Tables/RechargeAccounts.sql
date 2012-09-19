CREATE TABLE [dbo].[RechargeAccounts]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [AccountNum] VARCHAR(50) NOT NULL, 
    [Description] VARCHAR(100) NULL, 
    [Start] DATE NOT NULL, 
    [End] DATE NULL, 
    [IsValid] BIT NOT NULL DEFAULT 1
)
