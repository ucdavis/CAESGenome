set identity_insert barcode on
insert into barcode (id, plateID, plateSubID, primerID, stage, sourceBarcode, [date], done)
select id, plateID, platesubid, primerID, stage, sourceBarcode, [date], done
from cgflims...barcode
set identity_insert barcode off

insert into quality_phred (Id, Barcode, CellChar, CellNum, Start, [End], Datesubmitted, DateTimeSubmitted)
select * 
from cgflims...quality_phred

set identity_insert submission_userjob_bacterialclone on
insert into submission_userjob_bacterialclone (id, Seq_Direction, Primer1, Primer2, StrainID, VectorID, antibioticID)
select * 
from cgflims...submission_userjob_bacterialclone
set identity_insert submission_userjob_bacterialclone off

set identity_insert submission_userjob_dna on
insert into submission_userjob_dna (id, Seq_Direction, Primer1, Primer2)
select * 
from cgflims...submission_userjob_dna
set identity_insert submission_userjob_dna off

set identity_insert submission_userjob_qbot_colonypicking on
insert into submission_userjob_qbot_colonypicking ([id]
      ,[vectorID],[hostID],[numOfQTrays],[numOfGlycerol]
      ,[concentration],[replication],[numOfColonies])
select * 
from cgflims...submission_userjob_qbot_colonypicking
set identity_insert submission_userjob_qbot_colonypicking off

set identity_insert submission_userjob_qbot_gridding on
insert into submission_userjob_qbot_gridding ([id]
      ,[vectorID],[hostID],[numOfSourcePlates],[copiesOfMembranes]
      ,[pattern])
select * 
from cgflims...submission_userjob_qbot_gridding
set identity_insert submission_userjob_qbot_gridding off

set identity_insert submission_userjob_qbot_replicating on
insert into submission_userjob_qbot_replicating ([id]
      ,[vectorID],[hostID],[numOfSourcePlates],[plateTypeDestination]
      ,[replication])
select * 
from cgflims...submission_userjob_qbot_replicating
set identity_insert submission_userjob_qbot_replicating off

set identity_insert submission_userjob_sublibrary on
insert into submission_userjob_sublibrary ([id]
      ,[typeOfSamples],[concentrationOfDNA],[insertGenomeSize]
      ,[coverage],[vectorID],[antibioticID])
select * 
from cgflims...submission_userjob_sublibrary
set identity_insert submission_userjob_sublibrary off

set identity_insert submission_userjob_userrun on
insert into submission_userjob_userrun ([id],[Seq_Direction],[DyeID])
select * 
from cgflims...submission_userjob_userrun
set identity_insert submission_userjob_userrun off

set identity_insert submission_userjob_userrun_genotyping on
insert into submission_userjob_userrun_genotyping ([id],[dyeIDs])
select * 
from cgflims...submission_userjob_userrun_genotyping
set identity_insert submission_userjob_userrun_genotyping off

set identity_insert submission_userjobs on
insert into submission_userjobs ([id],[dateSubmitted]
      ,[uid],[accountid],[JobName],[submissionType],[HowManyPlates]
      ,[plateType],[Comment],[id2],[done],[statusDate])
select * 
from cgflims...submission_userjobs
set identity_insert submission_userjobs off

set identity_insert submission_userplates on
insert into submission_userplates ([id],[JobID],[plateName])
select * 
from cgflims...submission_userplates
set identity_insert submission_userplates off