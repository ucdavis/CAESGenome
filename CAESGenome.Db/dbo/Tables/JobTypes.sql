﻿CREATE TABLE [dbo].[JobTypes] (
    [Id]   INT          IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (50) NOT NULL,
    [DisplayName] VARCHAR(MAX) NULL, 
    [StandardSequencing] BIT NOT NULL DEFAULT 0, 
	[DnaSequencing] BIT NOT NULL Default 0,
    [CustomSequencing] BIT NOT NULL DEFAULT 0, 
    [Genotyping] BIT NOT NULL DEFAULT 0, 
    [Qbot] BIT NOT NULL DEFAULT 0, 
    [HasWebPlates] BIT NOT NULL DEFAULT 1, 
    [HasPlateSubmission] BIT NOT NULL DEFAULT 1, 
    [HasRca] BIT NOT NULL DEFAULT 0, 
    [HasSequencing] BIT NOT NULL DEFAULT 0, 
    [Has3730xl] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [PK_JobTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

