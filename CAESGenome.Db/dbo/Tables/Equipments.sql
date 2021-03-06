﻿CREATE TABLE [dbo].[Equipments] (
    [Id]       INT          IDENTITY (1, 1) NOT NULL,
    [Name]     VARCHAR (50) NOT NULL,
    [Operator] VARCHAR (50) NOT NULL,
    [IsReservable] BIT NOT NULL DEFAULT 0, 
    [Message] VARCHAR(MAX) NULL, 
    CONSTRAINT [PK_Equipments] PRIMARY KEY CLUSTERED ([Id] ASC)
);

