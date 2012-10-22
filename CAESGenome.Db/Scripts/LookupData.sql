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
INSERT INTO [dbo].[Antibiotics] (Id, Name) VALUES (12, 'unknown');
INSERT INTO [dbo].[Antibiotics] (Id, Name) VALUES (11, 'unknown');
INSERT INTO [dbo].[Antibiotics] (Id, Name) VALUES (13, 'unknown');
INSERT INTO [dbo].[Antibiotics] (Id, Name) VALUES (4, 'unknown');
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
INSERT INTO [dyes] (Id, Name, Supplied, GenoTyping) VALUES (4, 'n/a', 0, 0);
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
INSERT INTO [dbo].[JobTypes] (Id, Name, DisplayName, StandardSequencing, DnaSequencing, CustomSequencing, Genotyping, Qbot) VALUES (1, 'Bacterial Clone', '<strong>Bacterial Clone submission</strong> (Plasmid Sequencing)', 1, 0, 0, 0, 0);
INSERT INTO [dbo].[JobTypes] (Id, Name, DisplayName, StandardSequencing, DnaSequencing, CustomSequencing, Genotyping, Qbot) VALUES (2, 'PCR Product', '', 0, 1, 0, 0, 0);
INSERT INTO [dbo].[JobTypes] (Id, Name, DisplayName, StandardSequencing, DnaSequencing, CustomSequencing, Genotyping, Qbot) VALUES (3, 'User-Run Sequencing', '<strong>User-Run Sequencing Reaction</strong> (Ready-to-run Samples)', 1, 0, 0, 0, 0);
INSERT INTO [dbo].[JobTypes] (Id, Name, DisplayName, StandardSequencing, DnaSequencing, CustomSequencing, Genotyping, Qbot) VALUES (4, 'Purified DNA', '', 0, 1, 0, 0, 0);
INSERT INTO [dbo].[JobTypes] (Id, Name, DisplayName, StandardSequencing, DnaSequencing, CustomSequencing, Genotyping, Qbot) VALUES (5, 'sublibrary', '<strong>Sublibrary - BAC</strong> (Submit new job for each sample - sample unit is one).', 0, 0, 1, 0, 0);
INSERT INTO [dbo].[JobTypes] (Id, Name, DisplayName, StandardSequencing, DnaSequencing, CustomSequencing, Genotyping, Qbot) VALUES (11, 'User-run Genotyping', 'User-Run Genotyping Reaction', 0, 0, 0, 1, 0);
INSERT INTO [dbo].[JobTypes] (Id, Name, DisplayName, StandardSequencing, DnaSequencing, CustomSequencing, Genotyping, Qbot) VALUES (21, 'Q-Bot Colony Picking', '<strong>Q-Bot Colony Picking</strong>: The CAES Genomics Facility is equipped with a Genetix Q_Bot, which can pick colonies or lambda plaques at a rate of 3,000 per hour. The robot imaging system and software select colonies based on colony uniformity and Blue/White selection. The library is arrayed into 96- or 384- well plates (your choice - two replicates are made for the client to keep). The original copies are stored at -80 degree C. We can accept glycerol stock of your transformation mix if we know your titre.', 0, 0, 0, 0, 1);
INSERT INTO [dbo].[JobTypes] (Id, Name, DisplayName, StandardSequencing, DnaSequencing, CustomSequencing, Genotyping, Qbot) VALUES (22, 'Q-Bot Plate Replicating', '<strong>Q-Bot Plate Replicating</strong>: Libraries accepted in 96/384-well microplates are replicated/rearrayed using a Genetix Q-Bot. Unless otherwise indicated the copies are made in freezer medium for long-term storage. We also expand libraries from 384 to 96-well format and vice versa.', 0, 0, 0, 0, 1);
INSERT INTO [dbo].[JobTypes] (Id, Name, DisplayName, StandardSequencing, DnaSequencing, CustomSequencing, Genotyping, Qbot) VALUES (23, 'Q-Bot Gridding', '<strong>Q-Bot Gridding</strong>: Gene libraries accepted in 96/384-well plates are arrayed onto 22 X 22 cm positively charged nylon membranes. The spot patterns are 9,216 (3X3 pattern), 18,432 (4X4 pattern) or 27,648 (5X5 pattern). The membranes are processed to lyse the bacterial cells and cross-link the DNA. Up to 15 identical membranes can be produced in one arraying run. The arrays are suitable for radioactive or non-radioactive screening procedures and can be stripped and re-probed several times. PCR products and plasmid DNA are also accepted for arraying.', 0, 0, 0, 0, 1);
INSERT INTO [dbo].[JobTypes] (Id, Name, DisplayName, StandardSequencing, DnaSequencing, CustomSequencing, Genotyping, Qbot) VALUES (24, 'DNA Submission', '<strong>DNA submission</strong> (Purified Plasmid or PCR Product Sequencing)', 1, 0, 0, 0, 0);
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
INSERT INTO [dbo].[Vectors] (Id, Name, Antibiotic1Id, Antibiotic2Id, VectorTypeId) VALUES (3, 'Unknown', 4, 4, 5);
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

--
-- Equipment
--
SET IDENTITY_INSERT [dbo].[Equipments] ON
INSERT INTO [dbo].[Equipments] (Id, Name, Operator, IsReservable) VALUES (1, 'Q-Bot', 'User_notShown', 0)
INSERT INTO [dbo].[Equipments] (Id, Name, Operator, IsReservable) VALUES (2, 'Tetrad', 'staff only', 0)
INSERT INTO [dbo].[Equipments] (Id, Name, Operator, IsReservable) VALUES (3, 'Sequencer 28', 'staff', 0)
INSERT INTO [dbo].[Equipments] (Id, Name, Operator, IsReservable) VALUES (4, 'Sequencer 22', 'staff', 0)
INSERT INTO [dbo].[Equipments] (Id, Name, Operator, IsReservable, [Message]) VALUES (5, 'Bio Rad Gene Pulser', 'User', 0, 'You can drop in to use the Gene Pulser 30 minutes or less.')
INSERT INTO [dbo].[Equipments] (Id, Name, Operator, IsReservable) VALUES (6, 'Centrifuge', 'staff', 0)
INSERT INTO [dbo].[Equipments] (Id, Name, Operator, IsReservable) VALUES (7, 'Confocal Microscope', 'User', 1)
INSERT INTO [dbo].[Equipments] (Id, Name, Operator, IsReservable, [Message]) VALUES (8, 'Hydro Shear', 'User', 0, 'Contact Jongmin (754-6490) to use this equipment.')
INSERT INTO [dbo].[Equipments] (Id, Name, Operator, IsReservable) VALUES (9, 'I Cycler', 'Staff', 0)
INSERT INTO [dbo].[Equipments] (Id, Name, Operator, IsReservable) VALUES (10, 'MJPCR', 'staff', 0)
INSERT INTO [dbo].[Equipments] (Id, Name, Operator, IsReservable, [Message]) VALUES (11, 'Speed Vac', 'User', 0, 'Please contact Noelia (754-6616) to reserve this equipment.')
INSERT INTO [dbo].[Equipments] (Id, Name, Operator, IsReservable, [Message]) VALUES (12, 'Gel Imager', 'User', 0, 'You can drop in to use the Gel Imager. No Sign Up needed.')
INSERT INTO [dbo].[Equipments] (Id, Name, Operator, IsReservable, [Message]) VALUES (13, 'Hi Gro', 'User', 0, 'Contact Jongmin (754-6490) to use this equipment.')
INSERT INTO [dbo].[Equipments] (Id, Name, Operator, IsReservable) VALUES (14, '3730', 'staff', 0)
SET IDENTITY_INSERT [dbo].[Equipments] OFF

-- Universities
SET IDENTITY_INSERT [dbo].[universities] ON
INSERT INTO [universities] ([id], [name], [address], [city], [state], [zip], [country]) VALUES (1, 'University of California, Davis', 'One Shields Ave', 'Davis', 'CA', '95616', 'USA');
INSERT INTO [universities] ([id], [name], [address], [city], [state], [zip], [country]) VALUES (2, 'Tuskegee University', NULL, 'Tuskegee', 'AL', '36088', 'US');
INSERT INTO [universities] ([id], [name], [address], [city], [state], [zip], [country]) VALUES (3, 'Washington State University', NULL, 'Pullman', 'WA', '99164', 'US');
INSERT INTO [universities] ([id], [name], [address], [city], [state], [zip], [country]) VALUES (4, 'Vanderbilt University', '2301 Vanderbilt Place', 'Nashville', 'TN', '37235-7703', 'US');
INSERT INTO [universities] ([id], [name], [address], [city], [state], [zip], [country]) VALUES (5, 'USDA-ARS@Salinas', '1636 E. Alisal St.', 'Salinas', 'CA', '93905', 'USA');
INSERT INTO [universities] ([id], [name], [address], [city], [state], [zip], [country]) VALUES (6, 'n/a', 'n/a', 'n/a', 'CA', '95616', 'USA');
SET IDENTITY_INSERT [dbo].[universities] OFF

-- Departments
SET IDENTITY_INSERT [dbo].[departments] ON
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (1, 'Agricultural and Resource Economics', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (2, 'Agronomy & Range Science', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (3, 'Animal Science', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (4, 'Biological and Agricultural Engineering', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (5, 'Entomology', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (6, 'Environmental Horticulture', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (7, 'Environmental Toxicology', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (8, 'Food Science & Technology', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (9, 'Nematology', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (10, 'Nutrition', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (11, 'Plant Pathology', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (12, 'Pomology', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (13, 'Vegetable Crops', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (14, 'Viticulture and Enology', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (15, 'Wildlife, Fish and Conservation Biology', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (16, 'College of Agriculture and Environmental Science', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (17, 'UCDMed-Anesthesiology', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (18, 'UCDMed-Pain Medicine', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (19, 'UCDMed-Biological Chemistry', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (20, 'UCDMed-Cardiovascular Medicine', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (21, 'UCDMed-Cell Biology & Human Anatomy', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (22, 'UCDMed-Dermatology', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (23, 'UCDMed-Endocrinology, Clinical Nutrition & Vascular Medicine', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (24, 'UCDMed-Epidemiology & Preventive Medicine', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (25, 'UCDMed-Emergency Medicine', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (26, 'UCDMed-Family & Community Medicine', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (27, 'UCDMed-General Medicine', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (28, 'UCDMed-Hematology & Oncology', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (29, 'UCDMed-Human Physiology', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (30, 'UCDMed-Infectious Diseases', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (31, 'UCDMed-Internal Medicine', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (32, 'UCDMed-Medical Microbiology & Immunology', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (33, 'UCDMed-Medical Pharmacology & Tocicology', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (34, 'UCDMed-Neurological Surery', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (35, 'UCDMed-Neurology', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (36, 'UCDMed-Obstetrics & Gynecology', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (37, 'UCDMed-Opthalmology', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (38, 'UCDMed-Orthopaedic Surgery', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (39, 'UCDMed-Otolaryngology', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (40, 'UCDMed-Cleft and Craniofacial Program', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (41, 'UCDMed-Pathology', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (42, 'UCDMed-Pediatrics', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (43, 'UCDMed-Phycial Medicine and Rehabilitation', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (44, 'UCDMed-Psychiatry', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (45, 'UCDMed-Reproductive Endocrinology & Infertility', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (46, 'UCDMed-Radiation Oncology', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (47, 'UCDMed-Surgery', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (48, 'UCDMed-Urology', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (49, 'UCD Veterinarian School', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (55, 'Molecular and Cellular Biology', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (56, 'Plant Biology', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (59, 'Evolution and Ecology', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (58, 'VM Pathology, Micro & Immun', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (60, 'CAES Genomics Facility', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (61, 'Anthropology', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (62, 'Vet Genetics Laboratory', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (63, 'Plant genetics and genomics', 2);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (64, 'CROP AND SOIL SCIENCES', 3);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (65, 'Plant Sciences', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (66, 'Center of Excellence for Nutritional Genomics', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (67, 'USDA AGRICULTURAL RESEARCH SERVICE', 5);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (68, 'VM CTR COMPARATIVE MEDICINE', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (69, 'Dept. of Anatomy, Physiology and Cell Biology', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (70, 'UCDHS: OPHTHALMOLOGY & VISION SCIENCE', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (71, 'UCDMed-Biochemistry and Molecular Medicine', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (72, 'VM Population Health and Reproduction', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (73, 'VM California Animal Health and Food Safety Laboratory System, San Bernardino Branch', 1);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (74, 'Biomedical engineering', 1);

INSERT INTO [departments] ([id], [name], [universityId]) VALUES (75, 'n/a', 6);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (76, 'Plant Sciences', 6);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (77, 'Anthropology', 4);
INSERT INTO [departments] ([id], [name], [universityId]) VALUES (78, 'n/a', 1);
SET IDENTITY_INSERT [dbo].[departments] OFF