/*
Add missing indexes determined by using nHibernate Profiler and SQL Server Management Studio Query Execution Plan Anaylizer.
*/

/*
Missing Index Details from SQLQuery1.sql - donbot.cgf (AESDEAN\taylor (67))
The Query Processor estimates that implementing the following index could improve the query cost by 88.1379%.
*/

USE [cgf]
GO
CREATE NONCLUSTERED INDEX BarcodeFiles_BarcodeId_IDX
ON [dbo].[BarcodeFiles] ([BarcodeId])

GO

/*
Missing Index Details from SQLQuery3.sql - donbot.cgf (AESDEAN\taylor (54))
The Query Processor estimates that implementing the following index could improve the query cost by 78.8827%.
*/


USE [cgf]
GO
CREATE NONCLUSTERED INDEX BarcodeFiles_UploadedValidated_IDX
ON [dbo].[BarcodeFiles] ([Uploaded],[Validated])
INCLUDE ([BarcodeId])
GO


/*
Missing Index Details from Add missing index to barcode files 2.sql - donbot.cgf (AESDEAN\taylor (78))
The Query Processor estimates that implementing the following index could improve the query cost by 67.0177%.
*/


USE [cgf]
GO
CREATE NONCLUSTERED INDEX Barcodes_DateTimeValidated_IDX
ON [dbo].[Barcodes] ([DateTimeValidated])
INCLUDE ([Id])
GO


/*
Missing Index Details from SQLQuery1.sql - donbot.master (AESDEAN\taylor (64))
The Query Processor estimates that implementing the following index could improve the query cost by 40.5169%.
*/


USE [cgf]
GO
CREATE NONCLUSTERED INDEX [UserJobs_IsOpen_w_LastUpdate_CVDX]
ON [dbo].[UserJobs] ([IsOpen])
INCLUDE ([LastUpdate])
GO







