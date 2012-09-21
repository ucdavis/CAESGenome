CREATE TABLE [dbo].[EquipmentHiGro]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [EquipmentReservationId] INT NOT NULL, 
    [Temperature] INT NOT NULL, 
    [Speed] INT NOT NULL, 
    [NumPlates] INT NOT NULL, 
    [WellTypeId] INT NOT NULL, 
    CONSTRAINT [FK_EquipmentHiGro_WellTypes] FOREIGN KEY ([WellTypeId]) REFERENCES [WellTypes]([Id]), 
    CONSTRAINT [FK_EquipmentHiGro_EquipmentReservations] FOREIGN KEY ([EquipmentReservationId]) REFERENCES [EquipmentReservations]([Id])
)
