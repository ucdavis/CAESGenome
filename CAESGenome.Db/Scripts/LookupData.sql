--
--	Antibiotics table
--
SET IDENTITY_INSERT [dbo].[Antibiotics] ON
INSERT INTO [dbo].[Antibiotics] ([Id], [Name]) VALUES (1, 'ampicillin');
INSERT INTO [dbo].[Antibiotics] (Id, Name) VALUES (2, 'chloramphenical');
INSERT INTO [dbo].[Antibiotics] (Id, Name) VALUES (3, 'kanamycin');
INSERT INTO [dbo].[Antibiotics] (Id, Name) VALUES (6, 'other (please supply)');
INSERT INTO [dbo].[Antibiotics] (Id, Name) VALUES (10, 'carbenicillin');
INSERT INTO [dbo].[Antibiotics] (Id, Name) VALUES (5, 'not given');
SET IDENTITY_INSERT [dbo].[Antibiotics] OFF

--
--  Bacteria Table
--
SET IDENTITY_INSERT [dbo].[Bacteria] ON
INSERT INTO [bacteria] ([Id], [Name]) VALUES (1, 'E. coli');
SET IDENTITY_INSERT [dbo].[Bacteria] OFF

--
-- Vector Types
--
SET IDENTITY_INSERT [dbo].[VectorTypes] ON
INSERT INTO [dbo].[VectorTypes] (Id, Name) VALUES (1, 'Cosmid');
INSERT INTO [dbo].[VectorTypes] (Id, Name) VALUES (2, 'Phage');
INSERT INTO [dbo].[VectorTypes] (Id, Name) VALUES (3, 'Plasmid');
INSERT INTO [dbo].[VectorTypes] (Id, Name) VALUES (4, 'YAC');
INSERT INTO [dbo].[VectorTypes] (Id, Name) VALUES (5, 'other');
INSERT INTO [dbo].[VectorTypes] (Id, Name) VALUES (10, 'BAC');
SET IDENTITY_INSERT [dbo].[VectorTypes] OFF
--
-- Dyes
--
SET IDENTITY_INSERT [dbo].[Dyes] ON
INSERT INTO [dyes] (Id, Name, Supplied, GenoTyping) VALUES (1, 'ET DYEnamic', 0, 0);
INSERT INTO [dyes] (Id, Name, Supplied, GenoTyping) VALUES (2, 'Big Dye 2.0', 0, 0);
INSERT INTO [dyes] (Id, Name, Supplied, GenoTyping) VALUES (3, 'Big Dye 3.0', 0, 0);
INSERT INTO [dyes] (Id, Name, Supplied, GenoTyping) VALUES (5, 'HET', 0, 0);
INSERT INTO [dyes] (Id, Name, Supplied, GenoTyping) VALUES (6, 'FAM', 0, 0);
INSERT INTO [dyes] (Id, Name, Supplied, GenoTyping) VALUES (7, 'TET', 0, 0);
INSERT INTO [dyes] (Id, Name, Supplied, GenoTyping) VALUES (8, 'ROX', 0, 0);
INSERT INTO [dyes] (Id, Name, Supplied, GenoTyping) VALUES (9, 'none', 0, 0);
INSERT INTO [dyes] (Id, Name, Supplied, GenoTyping) VALUES (10, 'Big Dye 3.1', 1, 0);
INSERT INTO [dyes] (Id, Name, Supplied, GenoTyping) VALUES (11, 'VIC', 0, 0);
INSERT INTO [dyes] (Id, Name, Supplied, GenoTyping) VALUES (12, 'NED', 0, 0);
INSERT INTO [dyes] (Id, Name, Supplied, GenoTyping) VALUES (13, 'PET', 0, 0);
INSERT INTO [dyes] (Id, Name, Supplied, GenoTyping) VALUES (14, 'LIZ', 0, 0);
INSERT INTO [dyes] (Id, Name, Supplied, GenoTyping) VALUES (15, 'HEX', 0, 0);
INSERT INTO [dyes] (Id, Name, Supplied, GenoTyping) VALUES (16, 'TAMRA', 0, 0);
-- Data from Dyes_Genotyping
INSERT INTO [dyes] (Id, Name, Supplied, GenoTyping) VALUES (21, 'FAM', 1, 1);
INSERT INTO [dyes] (Id, Name, Supplied, GenoTyping) VALUES (22, 'ROX', 1, 1);
INSERT INTO [dyes] (Id, Name, Supplied, GenoTyping) VALUES (23, 'TET', 1, 1);
INSERT INTO [dyes] (Id, Name, Supplied, GenoTyping) VALUES (24, 'VIC', 1, 1);
INSERT INTO [dyes] (Id, Name, Supplied, GenoTyping) VALUES (25, 'NED', 1, 1);
INSERT INTO [dyes] (Id, Name, Supplied, GenoTyping) VALUES (26, 'PET', 1, 1);
INSERT INTO [dyes] (Id, Name, Supplied, GenoTyping) VALUES (27, 'LIZ', 1, 1);
INSERT INTO [dyes] (Id, Name, Supplied, GenoTyping) VALUES (28, 'HEX', 1, 1);
INSERT INTO [dyes] (Id, Name, Supplied, GenoTyping) VALUES (29, 'TAMRA', 1, 1);
SET IDENTITY_INSERT [dbo].[Dyes] OFF

--
-- Equipments
--
SET IDENTITY_INSERT [dbo].[Equipments] ON
INSERT INTO [dbo].[equipments] (Id, Name, Operator) VALUES (1, 'Q-Bot', 'User_notShown');
INSERT INTO [dbo].[equipments] (Id, Name, Operator) VALUES (2, 'Tetrad', 'staff only');
INSERT INTO [dbo].[equipments] (Id, Name, Operator) VALUES (3, 'Sequencer28', 'staff');
INSERT INTO [dbo].[equipments] (Id, Name, Operator) VALUES (4, 'Sequencer22', 'staff');
INSERT INTO [dbo].[equipments] (Id, Name, Operator) VALUES (5, 'BioRadGenePulser', 'User');
INSERT INTO [dbo].[equipments] (Id, Name, Operator) VALUES (6, 'Centrifuge', 'staff');
INSERT INTO [dbo].[equipments] (Id, Name, Operator) VALUES (7, 'Confocal_Microscope', 'User');
INSERT INTO [dbo].[equipments] (Id, Name, Operator) VALUES (8, 'HydroShear', 'User');
INSERT INTO [dbo].[equipments] (Id, Name, Operator) VALUES (9, 'I_Cycler', 'Staff');
INSERT INTO [dbo].[equipments] (Id, Name, Operator) VALUES (10, 'MJPCR', 'staff');
INSERT INTO [dbo].[equipments] (Id, Name, Operator) VALUES (11, 'SpeedVac', 'User');
INSERT INTO [dbo].[equipments] (Id, Name, Operator) VALUES (12, 'Gel Imager', 'User');
INSERT INTO [dbo].[equipments] (Id, Name, Operator) VALUES (13, 'Hi Gro', 'User');
INSERT INTO [dbo].[equipments] (Id, Name, Operator) VALUES (14, '3730', 'staff');
SET IDENTITY_INSERT [dbo].[Equipments] OFF

--
-- Job Types
--
SET IDENTITY_INSERT [dbo].[JobTypes] ON
INSERT INTO [dbo].[JobTypes] (Id, Name) VALUES (1, 'Bacterial Clone');
INSERT INTO [dbo].[JobTypes] (Id, Name) VALUES (4, 'Purified DNA');
INSERT INTO [dbo].[JobTypes] (Id, Name) VALUES (3, 'User-Run Sequencing');
INSERT INTO [dbo].[JobTypes] (Id, Name) VALUES (2, 'PCR Product');
INSERT INTO [dbo].[JobTypes] (Id, Name) VALUES (11, 'User-run Genotyping');
INSERT INTO [dbo].[JobTypes] (Id, Name) VALUES (5, 'sublibrary');
INSERT INTO [dbo].[JobTypes] (Id, Name) VALUES (21, 'Q-Bot Colony Picking');
INSERT INTO [dbo].[JobTypes] (Id, Name) VALUES (22, 'Q-Bot Plate Replicating');
INSERT INTO [dbo].[JobTypes] (Id, Name) VALUES (23, 'Q-Bot Gridding');
SET IDENTITY_INSERT [dbo].[JobTypes] OFF

--
-- Primers
--
SET IDENTITY_INSERT [dbo].[primers] ON
INSERT INTO [dbo].[primers] (Id, Name, Shortname, Supplied, Sequence) VALUES (1, 'M13 forward', 'M13f', 1, 'GTA AAA CGA CGG CCA GT');
INSERT INTO [dbo].[primers] (Id, Name, Shortname, Supplied, Sequence) VALUES (2, 'M13 reverse', 'M13r', 1, 'CAG GAA ACA GCT ATC AC');
INSERT INTO [dbo].[primers] (Id, Name, Shortname, Supplied, Sequence) VALUES (3, 'T7', 'T7', 1, 'AAT ACG ACT CAC TAT AG');
INSERT INTO [dbo].[primers] (Id, Name, Shortname, Supplied, Sequence) VALUES (4, 'T3', 'T3', 1, 'ATT AAC CCT CACTAA AG');
INSERT INTO [dbo].[primers] (Id, Name, Shortname, Supplied, Sequence) VALUES (5, 'SP6', 'SP6', 1, 'ATT TAG GTG ACA CTA TA');
INSERT INTO [dbo].[primers] (Id, Name, Shortname, Supplied, Sequence) VALUES (6, 'will supply', 'willSupply', 1, NULL);
INSERT INTO [dbo].[primers] (Id, Name, Shortname, Supplied, Sequence) VALUES (10, 'DNRf', '', 0, NULL);
INSERT INTO [dbo].[primers] (Id, Name, Shortname, Supplied, Sequence) VALUES (11, 'DNRR', '', 0, NULL);
INSERT INTO [dbo].[primers] (Id, Name, Shortname, Supplied, Sequence) VALUES (12, 'DNRF', '', 0, NULL);
INSERT INTO [dbo].[primers] (Id, Name, Shortname, Supplied, Sequence) VALUES (13, 'WSCF', '', 0, 'TCCGAGATCTGGACGAGC');
INSERT INTO [dbo].[primers] (Id, Name, Shortname, Supplied, Sequence) VALUES (14, 'pDNRF', '', 0, 'ACGGTACCGGACATATGCC');
INSERT INTO [dbo].[primers] (Id, Name, Shortname, Supplied, Sequence) VALUES (15, 'pDNRR', '', 0, 'GCCAAACGAATGGTCTAG');
INSERT INTO [dbo].[primers] (Id, Name, Shortname, Supplied, Sequence) VALUES (16, 'pDNR', '', 0, NULL);
INSERT INTO [dbo].[primers] (Id, Name, Shortname, Supplied, Sequence) VALUES (17, 'WSCR', '', 0, 'TAATACGACTCACTATAGGG');
INSERT INTO [dbo].[primers] (Id, Name, Shortname, Supplied, Sequence) VALUES (19, 'custom', '', 0, NULL);
INSERT INTO [dbo].[primers] (Id, Name, Shortname, Supplied, Sequence) VALUES (23, 'WSC-F', '', 0, 'TCCGAGATCTGGACGAGC');
INSERT INTO [dbo].[primers] (Id, Name, Shortname, Supplied, Sequence) VALUES (27, 'SL1', '', 0, NULL);
INSERT INTO [dbo].[primers] (Id, Name, Shortname, Supplied, Sequence) VALUES (29, 'SL2', '', 0, NULL);
INSERT INTO [dbo].[primers] (Id, Name, Shortname, Supplied, Sequence) VALUES (31, '5''SMART', '', 0, NULL);
INSERT INTO [dbo].[primers] (Id, Name, Shortname, Supplied, Sequence) VALUES (33, 'Various custom primers', '', 0, NULL);
INSERT INTO [dbo].[primers] (Id, Name, Shortname, Supplied, Sequence) VALUES (35, 'universal fwd', '', 0, NULL);
INSERT INTO [dbo].[primers] (Id, Name, Shortname, Supplied, Sequence) VALUES (37, 'universal rev', '', 0, NULL);
SET IDENTITY_INSERT [dbo].[primers] OFF

--
-- Strains
--
SET IDENTITY_INSERT [dbo].[strains] ON
INSERT INTO [dbo].[Strains] (Id, Name, Supplied, BacteriaId) VALUES (1, 'DH5alpha', 1, 1);
INSERT INTO [dbo].[Strains] (Id, Name, Supplied, BacteriaId) VALUES (2, 'DH10B', 1, 1);
INSERT INTO [dbo].[Strains] (Id, Name, Supplied, BacteriaId) VALUES (3, 'JM109', 1, 1);
INSERT INTO [dbo].[Strains] (Id, Name, Supplied, BacteriaId) VALUES (4, 'HB101', 1, 1);
INSERT INTO [dbo].[Strains] (Id, Name, Supplied, BacteriaId) VALUES (5, 'Top 10', 1, 1);
INSERT INTO [dbo].[Strains] (Id, Name, Supplied, BacteriaId) VALUES (6, 'other', 1, 1);
INSERT INTO [dbo].[Strains] (Id, Name, Supplied, BacteriaId) VALUES (10, 'xl2-blu', 0, 1);
INSERT INTO [dbo].[Strains] (Id, Name, Supplied, BacteriaId) VALUES (20, 'transformax epi 300', 0, 1);
INSERT INTO [dbo].[Strains] (Id, Name, Supplied, BacteriaId) VALUES (12, 'xl10', 0, 1);
INSERT INTO [dbo].[Strains] (Id, Name, Supplied, BacteriaId) VALUES (18, 'nm522', 0, 1);
INSERT INTO [dbo].[Strains] (Id, Name, Supplied, BacteriaId) VALUES (14, 'transformax epi 300 electrocompetent', 0, 1);
INSERT INTO [dbo].[Strains] (Id, Name, Supplied, BacteriaId) VALUES (15, 'XL10-Gold', NULL, 1);
INSERT INTO [dbo].[Strains] (Id, Name, Supplied, BacteriaId) VALUES (28, 'ep1300', 0, 1);
INSERT INTO [dbo].[Strains] (Id, Name, Supplied, BacteriaId) VALUES (33, 'newHost', 0, 1);
INSERT INTO [dbo].[Strains] (Id, Name, Supplied, BacteriaId) VALUES (34, 'Mach1-T1R', 0, 1);
INSERT INTO [dbo].[Strains] (Id, Name, Supplied, BacteriaId) VALUES (35, 'What is this?', 0, 1);
INSERT INTO [dbo].[Strains] (Id, Name, Supplied, BacteriaId) VALUES (36, '?', 0, 1);
INSERT INTO [dbo].[Strains] (Id, Name, Supplied, BacteriaId) VALUES (37, 'XL1-Blue supercompetent cells', 0, 1);
INSERT INTO [dbo].[Strains] (Id, Name, Supplied, BacteriaId) VALUES (38, 'Ben', 0, 1);
INSERT INTO [dbo].[Strains] (Id, Name, Supplied, BacteriaId) VALUES (39, 'DW31?', 0, 1);
INSERT INTO [dbo].[Strains] (Id, Name, Supplied, BacteriaId) VALUES (40, 'E.cloni 10G  supreme Electrocompetent cells (lucig', 0, 1);
INSERT INTO [dbo].[Strains] (Id, Name, Supplied, BacteriaId) VALUES (41, 'What\\\\\\''s this? It\\\\\\''s E. coli with a vector', 0, 1);
INSERT INTO [dbo].[Strains] (Id, Name, Supplied, BacteriaId) VALUES (42, 'StrataClone SoloPack', 0, 1);
INSERT INTO [dbo].[Strains] (Id, Name, Supplied, BacteriaId) VALUES (43, 'StrataClone SoloPack', 0, 1);
INSERT INTO [dbo].[Strains] (Id, Name, Supplied, BacteriaId) VALUES (44, 'StrataClone SoloPack cells', 0, 1);
INSERT INTO [dbo].[Strains] (Id, Name, Supplied, BacteriaId) VALUES (45, 'StrataClone SoloPack', 0, 1);
INSERT INTO [dbo].[Strains] (Id, Name, Supplied, BacteriaId) VALUES (46, 'p-BlueScript in E.coli', 0, 1);
INSERT INTO [dbo].[Strains] (Id, Name, Supplied, BacteriaId) VALUES (47, 'n/a', 0, 1);
INSERT INTO [dbo].[Strains] (Id, Name, Supplied, BacteriaId) VALUES (48, 'StrataClone SoloPack Competent Cells', 0, 1);
INSERT INTO [dbo].[Strains] (Id, Name, Supplied, BacteriaId) VALUES (49, 'StrataClone SoloPack cells', 0, 1);
INSERT INTO [dbo].[Strains] (Id, Name, Supplied, BacteriaId) VALUES (50, 'StrataClone SoloPack', 0, 1);
INSERT INTO [dbo].[Strains] (Id, Name, Supplied, BacteriaId) VALUES (51, 'StrataClone Competent Cells', 0, 1);
INSERT INTO [dbo].[Strains] (Id, Name, Supplied, BacteriaId) VALUES (52, 'StrataClone Competent Cells', 0, 1);
INSERT INTO [dbo].[Strains] (Id, Name, Supplied, BacteriaId) VALUES (53, 'StrataClone Competent Cells', 0, 1);
SET IDENTITY_INSERT [dbo].[strains] OFF



--
-- Vectors
--
SET IDENTITY_INSERT [dbo].[Vectors] ON
INSERT INTO [dbo].[Vectors] (Id, Name, Antibiotic1Id, Antibiotic2Id, VectorTypeId) VALUES (1, 'pBlueScript II SK', 1, 6, 3);
INSERT INTO [dbo].[Vectors] (Id, Name, Antibiotic1Id, Antibiotic2Id, VectorTypeId) VALUES (2, 'pUC18', 1, 6, 3);
INSERT INTO [dbo].[Vectors] (Id, Name, Antibiotic1Id, Antibiotic2Id, VectorTypeId) VALUES (4, 'pCR2.1-TOPO', 12, 6, 3);
INSERT INTO [dbo].[Vectors] (Id, Name, Antibiotic1Id, Antibiotic2Id, VectorTypeId) VALUES (5, 'Other', 6, 6, 5);
INSERT INTO [dbo].[Vectors] (Id, Name, Antibiotic1Id, Antibiotic2Id, VectorTypeId) VALUES (6, 'pBeloBAC11', 2, 6, 3);
INSERT INTO [dbo].[Vectors] (Id, Name, Antibiotic1Id, Antibiotic2Id, VectorTypeId) VALUES (7, 'pUC19', 1, 6, 3);
INSERT INTO [dbo].[Vectors] (Id, Name, Antibiotic1Id, Antibiotic2Id, VectorTypeId) VALUES (8, 'pBlueScript SK', 1, 6, 3);
INSERT INTO [dbo].[Vectors] (Id, Name, Antibiotic1Id, Antibiotic2Id, VectorTypeId) VALUES (9, 'pBlueScript KS', 1, 6, 3);
INSERT INTO [dbo].[Vectors] (Id, Name, Antibiotic1Id, Antibiotic2Id, VectorTypeId) VALUES (10, 'pCR4-TOPO', 12, 6, 3);
INSERT INTO [dbo].[Vectors] (Id, Name, Antibiotic1Id, Antibiotic2Id, VectorTypeId) VALUES (86, 'pSmart-Kan', 3, 6, 3);
INSERT INTO [dbo].[Vectors] (Id, Name, Antibiotic1Id, Antibiotic2Id, VectorTypeId) VALUES (21, 'pTriplEx2', 1, 6, 3);
INSERT INTO [dbo].[Vectors] (Id, Name, Antibiotic1Id, Antibiotic2Id, VectorTypeId) VALUES (22, 'pBAD', 1, 6, 3);
INSERT INTO [dbo].[Vectors] (Id, Name, Antibiotic1Id, Antibiotic2Id, VectorTypeId) VALUES (23, 'pDNR', 13, 6, 3);
INSERT INTO [dbo].[Vectors] (Id, Name, Antibiotic1Id, Antibiotic2Id, VectorTypeId) VALUES (24, 'pGEM-Teasy', 1, 6, 3);
INSERT INTO [dbo].[Vectors] (Id, Name, Antibiotic1Id, Antibiotic2Id, VectorTypeId) VALUES (25, 'pBiBAC', 3, 6, 10);
INSERT INTO [dbo].[Vectors] (Id, Name, Antibiotic1Id, Antibiotic2Id, VectorTypeId) VALUES (84, 'pEZSeq-Amp', 1, 6, 3);
INSERT INTO [dbo].[Vectors] (Id, Name, Antibiotic1Id, Antibiotic2Id, VectorTypeId) VALUES (28, 'pBACe3.6', 2, 6, 10);
INSERT INTO [dbo].[Vectors] (Id, Name, Antibiotic1Id, Antibiotic2Id, VectorTypeId) VALUES (29, 'pWE15', 1, 6, 1);
INSERT INTO [dbo].[Vectors] (Id, Name, Antibiotic1Id, Antibiotic2Id, VectorTypeId) VALUES (31, 'pAD22', 5, 6, 1);
INSERT INTO [dbo].[Vectors] (Id, Name, Antibiotic1Id, Antibiotic2Id, VectorTypeId) VALUES (85, 'pEZSeq-Kan', 3, 6, 3);
INSERT INTO [dbo].[Vectors] (Id, Name, Antibiotic1Id, Antibiotic2Id, VectorTypeId) VALUES (33, 'pBelloBAC11', 2, 6, 10);
INSERT INTO [dbo].[Vectors] (Id, Name, Antibiotic1Id, Antibiotic2Id, VectorTypeId) VALUES (83, 'pSmart-Amp', 1, 6, 3);
INSERT INTO [dbo].[Vectors] (Id, Name, Antibiotic1Id, Antibiotic2Id, VectorTypeId) VALUES (42, 'pCC1BAC', 2, 6, 10);
INSERT INTO [dbo].[Vectors] (Id, Name, Antibiotic1Id, Antibiotic2Id, VectorTypeId) VALUES (87, 'pTAR BAC 4', 1, 2, 10);
INSERT INTO [dbo].[Vectors] (Id, Name, Antibiotic1Id, Antibiotic2Id, VectorTypeId) VALUES (58, 'pECSBAC4', 2, 6, 10);
INSERT INTO [dbo].[Vectors] (Id, Name, Antibiotic1Id, Antibiotic2Id, VectorTypeId) VALUES (88, 'pBRcDNA-SFI-ab', 1, 6, 3);
INSERT INTO [dbo].[Vectors] (Id, Name, Antibiotic1Id, Antibiotic2Id, VectorTypeId) VALUES (89, 'TOPO  TA', 1, 6, 3);
INSERT INTO [dbo].[Vectors] (Id, Name, Antibiotic1Id, Antibiotic2Id, VectorTypeId) VALUES (90, 'pBR322', 11, 6, 3);
INSERT INTO [dbo].[Vectors] (Id, Name, Antibiotic1Id, Antibiotic2Id, VectorTypeId) VALUES (91, 'pBK-CMV', 3, 6, 3);
INSERT INTO [dbo].[Vectors] (Id, Name, Antibiotic1Id, Antibiotic2Id, VectorTypeId) VALUES (92, 'pBiBAC', 3, 6, 3);
INSERT INTO [dbo].[Vectors] (Id, Name, Antibiotic1Id, Antibiotic2Id, VectorTypeId) VALUES (93, 'pINDIGO451', 2, 6, 3);
INSERT INTO [dbo].[Vectors] (Id, Name, Antibiotic1Id, Antibiotic2Id, VectorTypeId) VALUES (94, 'pDONR207', 3, 6, 3);
INSERT INTO [dbo].[Vectors] (Id, Name, Antibiotic1Id, Antibiotic2Id, VectorTypeId) VALUES (95, 'pBRcDNA-Sfi-AB', 1, 6, 3);
INSERT INTO [dbo].[Vectors] (Id, Name, Antibiotic1Id, Antibiotic2Id, VectorTypeId) VALUES (96, 'pENTR-JW', 4, 6, 3);
INSERT INTO [dbo].[Vectors] (Id, Name, Antibiotic1Id, Antibiotic2Id, VectorTypeId) VALUES (97, '', 2, 1, 10);
SET IDENTITY_INSERT [dbo].[Vectors] OFF

--
-- Roles
--
INSERT INTO [dbo].[webpages_Roles] (RoleName) VALUES ('Admin');
INSERT INTO [dbo].[webpages_Roles] (RoleName) VALUES ('PI');
INSERT INTO [dbo].[webpages_Roles] (RoleName) VALUES ('Staff');
INSERT INTO [dbo].[webpages_Roles] (RoleName) VALUES ('User');

--
-- Stages
--
INSERT INTO [dbo].[Stages] (Id, Name, [Order], JobTypeId, IsComplete) VALUES ('BCWP', 'Web Submitted Plates', 1, 1, 0)
INSERT INTO [dbo].[Stages] (Id, Name, [Order], JobTypeId, IsComplete) VALUES ('BCPS', 'Plate Submissions', 2, 1, 0)
INSERT INTO [dbo].[Stages] (Id, Name, [Order], JobTypeId, IsComplete) VALUES ('BCRC', 'RCA', 3, 1, 0)
INSERT INTO [dbo].[Stages] (Id, Name, [Order], JobTypeId, IsComplete) VALUES ('BCSR', 'Sequencing Reaction', 4, 1, 0)
INSERT INTO [dbo].[Stages] (Id, Name, [Order], JobTypeId, IsComplete) VALUES ('BC37', '3730xl', 5, 1, 1)
INSERT INTO [dbo].[Stages] (Id, Name, [Order], JobTypeId, IsComplete) VALUES ('PPWP', 'Web Submitted Plates', 1, 2, 0)
INSERT INTO [dbo].[Stages] (Id, Name, [Order], JobTypeId, IsComplete) VALUES ('PPPS', 'Plate Submission', 2, 2, 0)
INSERT INTO [dbo].[Stages] (Id, Name, [Order], JobTypeId, IsComplete) VALUES ('PPSR', 'Sequencing Reaction', 3, 2, 0)
INSERT INTO [dbo].[Stages] (Id, Name, [Order], JobTypeId, IsComplete) VALUES ('PP37', '3730xl', 4, 2, 1)
INSERT INTO [dbo].[Stages] (Id, Name, [Order], JobTypeId, IsComplete) VALUES ('URWP', 'Web Submitted Plates', 1, 3, 0)
INSERT INTO [dbo].[Stages] (Id, Name, [Order], JobTypeId, IsComplete) VALUES ('URPS', 'Plate Submission', 2, 3, 0)
INSERT INTO [dbo].[Stages] (Id, Name, [Order], JobTypeId, IsComplete) VALUES ('UR37', '3730xl', 3, 3, 1)
INSERT INTO [dbo].[Stages] (Id, Name, [Order], JobTypeId, IsComplete) VALUES ('PDWP', 'Web Submitted Plates', 1, 4, 0)
INSERT INTO [dbo].[Stages] (Id, Name, [Order], JobTypeId, IsComplete) VALUES ('PDPS', 'Plate Submission', 2, 4, 0)
INSERT INTO [dbo].[Stages] (Id, Name, [Order], JobTypeId, IsComplete) VALUES ('PDSR', 'Sequencing Reaction', 3, 4, 0)
INSERT INTO [dbo].[Stages] (Id, Name, [Order], JobTypeId, IsComplete) VALUES ('PD37', '3730xl', 4, 4, 1)
INSERT INTO [dbo].[Stages] (Id, Name, [Order], JobTypeId, IsComplete) VALUES ('SLWP', 'Web Submitted Plates', 1, 5, 0)
INSERT INTO [dbo].[Stages] (Id, Name, [Order], JobTypeId, IsComplete) VALUES ('SLPS', 'Plate Submission', 2, 5, 1)
INSERT INTO [dbo].[Stages] (Id, Name, [Order], JobTypeId, IsComplete) VALUES ('UGWP', 'Web Submitted Plates', 1, 11, 0)
INSERT INTO [dbo].[Stages] (Id, Name, [Order], JobTypeId, IsComplete) VALUES ('UGPS', 'Plate Submission', 2, 11, 0)
INSERT INTO [dbo].[Stages] (Id, Name, [Order], JobTypeId, IsComplete) VALUES ('UG37', '3730xl', 3, 11, 1)

INSERT INTO [dbo].[Stages] (Id, Name, [Order], JobTypeId, IsComplete) VALUES ('QPWP', 'Web Submitted Plates', 1, 21, 0)
INSERT INTO [dbo].[Stages] (Id, Name, [Order], JobTypeId, IsComplete) VALUES ('QPPS', 'Plate Submission', 2, 21, 1)
INSERT INTO [dbo].[Stages] (Id, Name, [Order], JobTypeId, IsComplete) VALUES ('QRWP', 'Web Submitted Plates', 1, 22, 0)
INSERT INTO [dbo].[Stages] (Id, Name, [Order], JobTypeId, IsComplete) VALUES ('QRPS', 'Plate Submission', 2, 22, 1)
INSERT INTO [dbo].[Stages] (Id, Name, [Order], JobTypeId, IsComplete) VALUES ('QGWP', 'Web Submitted Plates', 1, 23, 0)
INSERT INTO [dbo].[Stages] (Id, Name, [Order], JobTypeId, IsComplete) VALUES ('QGPS', 'Plate Submission', 2, 23, 1)


INSERT INTO [dbo].[Stages] (Id, Name, [Order], JobTypeId, IsComplete) VALUES ('', '', 1, 1, 0)
INSERT INTO [dbo].[Stages] (Id, Name, [Order], JobTypeId, IsComplete) VALUES ('', '', 1, 1, 0)
INSERT INTO [dbo].[Stages] (Id, Name, [Order], JobTypeId, IsComplete) VALUES ('', '', 1, 1, 0)
