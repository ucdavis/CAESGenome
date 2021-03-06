﻿CREATE TABLE [dbo].[EquipmentReservations]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NOT NULL, 
    [EquipmentId] INT NOT NULL, 
    [DateSubmitted] DATETIME NOT NULL DEFAULT getdate(), 
    [Start] DATETIME NOT NULL, 
    [End] DATETIME NOT NULL, 
    [Cancelled] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_EquipmentReservations_Equipments] FOREIGN KEY ([EquipmentId]) REFERENCES [Equipments]([Id])
)
