CREATE TABLE [dbo].[EquipmentHiGro]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [EquipmentReservationId] INT NOT NULL, 
    [Temperature] INT NOT NULL, 
    [Speed] INT NOT NULL, 
    [NumPlates] INT NOT NULL, 
    [WellType] varchar(15) NOT NULL, 
    CONSTRAINT [FK_EquipmentHiGro_EquipmentReservations] FOREIGN KEY ([EquipmentReservationId]) REFERENCES [EquipmentReservations]([Id])
)
