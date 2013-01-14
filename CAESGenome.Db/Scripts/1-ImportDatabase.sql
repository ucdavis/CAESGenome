/* Import data freom the old database into our intermediate database
	This is a straight up copy from the old db to new, one more script to import into production
----------------------------------------------------------------------*/

use cgfold

truncate table barcode
truncate table departments
truncate table pi
truncate table quality_phred
truncate table recharge
truncate table staff
truncate table submission_userjob_bacterialclone
truncate table submission_userjob_dna
truncate table submission_userjob_qbot_colonypicking
truncate table submission_userjob_qbot_gridding
truncate table submission_userjob_qbot_replicating
truncate table submission_userjob_sublibrary
truncate table submission_userjob_userrun
truncate table submission_userjob_userrun_genotyping
truncate table submission_userjobs
truncate table submission_userplates
truncate table [user]
truncate table useracct


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

-- ------------------------------------------------------------------
-- Account Info Database
-- ------------------------------------------------------------------

set identity_insert [user] on
insert into [user] (userid, username, lastname, firstname, title, email, passwords, phonenum, valid, datejoined, piid)
select * 
from cgfaccountinfo...[user]
set identity_insert [user] off

set identity_insert departments on
insert into departments (departmentid, department)
select * from cgfaccountinfo...departments
set identity_insert departments off

set identity_insert pi on
insert into pi ([piid],[pilastname],[pifirstname],[piemail],[pifax]
      ,[piphonenum],[pititle],[pidatejoined],[piusername]
      ,[pipassword],[universityid],[departmentid])
select * from cgfaccountinfo...pi
set identity_insert pi off

set identity_insert recharge on
insert into recharge ([rechargeid],[accountnum],[valid],[description]
      ,[datestart],[dateend],[piid])
select * from cgfaccountinfo...recharge
set identity_insert recharge off

insert into staff ([staffid],[stafffirst],[stafflast],[staffemail]
      ,[stafftitle],[staffpassword])
select * from cgfaccountinfo...staff

set identity_insert useracct on
insert into useracct ([useracctid],[valid],[rechargeid],[userid])
select * from cgfaccountinfo...useracct
set identity_insert useracct off