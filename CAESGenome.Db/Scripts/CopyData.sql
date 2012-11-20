/*
  Wipe any existing data, so we don't get conflicting keys
*/

delete from barcodefiles
delete from Barcodes
delete from userjobplates
delete from userjobs
delete from UserProfilesXRechargeAccounts

/*
Delete the job tables
*/
delete from UserProfilesXRechargeAccounts
delete from UserJobBacterialClone
delete from userjobdna
delete from UserJobQbotColonyPicking
delete from UserJobQbotGridding
delete from UserJobQbotReplicating
delete from UserJobSubLibrary
delete from UserJobUserRun
delete from UserJobsGenotypingXDyes
delete from UserJobGenotyping


/*
Copy the Data
*/

update cgfold.dbo.recharge set valid = 'no' where rechargeid in (286, 293)

--insert into cgf.dbo.UserProfilesXRechargeAccounts (UserProfileId, RechargeAccountId)
--select distinct userid, rechargeid from cgfold.dbo.useracct 
--where lower(valid) = 'yes'
--  and userid in ( select userid from cgfold.dbo.[user] )
--  and rechargeid in ( select rechargeid from cgfold.dbo.recharge )

set identity_insert cgf.dbo.userjobbacterialclone on
insert into cgf.dbo.UserJobBacterialClone (id, SequenceDirection, Primer1Id, primer2id, StrainId, VectorId, AntibioticId)
select id, 	case when Seq_Direction = 'F' then 'Forward' else 'Backward' end
	, primer1, primer2
	, strainid, vectorid, antibioticID
from cgfold.dbo.submission_userjob_bacterialclone
set identity_insert cgf.dbo.userjobbacterialclone off

set identity_insert cgf.dbo.userjobdna on
insert into cgf.dbo.UserJobDna (id, SequenceDirection, Primer1Id, primer2id)
select id, case when Seq_Direction = 'F' then 'Forward' else 'Backward' end,
	primer1, primer2
from cgfold.dbo.submission_userjob_dna
set identity_insert cgf.dbo.userjobdna off

set identity_insert cgf.dbo.userjobqbotcolonypicking ON
insert into cgf.dbo.UserJobQbotColonyPicking (id, vectorid, strainid, NumQTrays, NumGlycerol, Concentration, [Replication], NumColonies)
select id, vectorid, hostid , numOfQTrays, cast(numOfGlycerol as float), concentration, [replication], numOfColonies
from cgfold.dbo.submission_userjob_qbot_colonypicking
set identity_insert cgf.dbo.userjobqbotcolonypicking OFF

set identity_insert cgf.dbo.userjobqbotgridding ON
insert into UserJobQbotGridding (id, vectorid, strainid, NumMembraneCopies, GriddingPattern)
select id, vectorid, hostid, copiesOfMembranes, 'FourXFour' pattern
from cgfold.dbo.submission_userjob_qbot_gridding
set identity_insert cgf.dbo.userjobqbotgridding OFF

set identity_insert cgf.dbo.userjobqbotreplicating on
insert into cgf.dbo.UserJobQbotReplicating (id, vectorid, strainid, NumSourcePlates, DestinationPlateType, NumCopies)
select id, vectorID, hostid, numOfSourcePlates
	,  case when plateTypeDestination = 384 then 'ThreeEightyFour'
	        when plateTypeDestination = 96 then 'NinetySix'
			end 
	, [replication]
from cgfold.dbo.submission_userjob_qbot_replicating
set identity_insert cgf.dbo.userjobqbotreplicating off

set identity_insert cgf.dbo.userjobsublibrary on
insert into cgf.dbo.UserJobSubLibrary (id, SampleType, DnaConcentration, GenomeSize, Coverage, vectorid, AntibioticId)
select id, typeOfSamples, concentrationOfDNA, insertGenomeSize, coverage, vectorID, antibioticID 
from cgfold.dbo.submission_userjob_sublibrary
set identity_insert cgf.dbo.userjobsublibrary off

set identity_insert cgf.dbo.userjobuserrun on
insert into dbo.UserJobUserRun (id, SequenceDirection, dyeid)
select id, 
	case when Seq_Direction = 'F' then 'Forward'
	else 'Backward' end
	, dyeid
from cgfold.dbo.submission_userjob_userrun
set identity_insert cgf.dbo.userjobuserrun off

set identity_insert cgf.dbo.userjobgenotyping on

declare @cur cursor, @id int, @dyeids varchar(50)
set @cur = cursor for select id, dyeids from cgfold.dbo.submission_userjob_userrun_genotyping

open @cur
fetch next from @cur into @id, @dyeids

while (@@FETCH_STATUS = 0)
begin
	
	insert into cgf.dbo.UserJobGenotyping (id) values (@id)

	if (@dyeids <> '')
	begin
	insert into cgf.dbo.UserJobsGenotypingXDyes (UserJobGenotypingId, dyeid)
	select @id, name from cgfold.dbo.splitstring(@dyeids)
	end

	fetch next from @cur into @id, @dyeids

end

close @cur
deallocate @cur

set identity_insert cgf.dbo.userjobgenotyping off

insert into RechargeAccounts (AccountNum, Description, start, [end], IsValid, UserProfileId)
select distinct 'na', 'n/a', getdate(), getdate(), 0, uid from cgfold.dbo.submission_userjobs where accountid = '' and submissionType <> 0
go
  
insert into UserProfilesXRechargeAccounts (UserProfileId, RechargeAccountId)
select UserProfileId, id from RechargeAccounts where AccountNum = 'na'
go

set identity_insert cgf.dbo.userjobs on
insert into cgf.dbo.UserJobs (id, userid, RechargeAccountId, name, jobtypeid, NumberPlates, platetype, comments, isopen
	, LastUpdate, DateTimeCreated
	, UserJobBacterialCloneId, UserJobDnaId, UserJobSublibraryId, UserJobUserRunId
	, UserJobQbotColonyPickingId, UserJobQbotReplicatingId, UserJobQbotGriddingId
	, UserJobGenotypingId)
select distinct 
	id, uid, rechargeid, jobname, submissionType, HowManyPlates, plateType, Comment
	, cast(case when done = 2 or done = 1 then 0 else 1 end as bit) IsOpen
	, statusdate lastupdate, dateSubmitted datetimecreated
	, cast(case when submissionType = 1 then id2 else null end as bit) bacterialclone
	, cast(case when submissionType = 4 or submissionType = 2 then id2 else null end as bit) dna
	, cast(case when submissionType = 5 then id2 else null end as bit) sublibrary
	, cast(case when submissionType = 3 then id2 else null end as bit) userrun
	, cast(case when submissionType = 21 then id2 else null end as bit) colonypicking
	, cast(case when submissionType = 22 then id2 else null end as bit) replicating
	, cast(case when submissionType = 23 then id2 else null end as bit) gridding
	, cast(case when submissionType = 11 then id2 else null end as bit) genotyping
from cgfold.dbo.submission_userjobs suj
	left outer join (
		select userid, recharge.rechargeid, accountnum from cgfold.dbo.useracct
		inner join cgfold.dbo.recharge on useracct.rechargeid = recharge.rechargeid
	) accts on suj.uid = accts.userid and suj.accountid = accts.accountnum
where submissionType <> 0
  and rechargeid is not null
  and id not in (469 , 3107, 3130, 3549, 3623, 3686, 3705, 3713, 3996, 4458, 4476, 4520)
  and [datesubmitted] is not null

insert into cgf.dbo.UserJobs (id, userid, RechargeAccountId, name, jobtypeid, NumberPlates, platetype, comments, isopen
, LastUpdate, DateTimeCreated
, UserJobBacterialCloneId, UserJobDnaId, UserJobSublibraryId, UserJobUserRunId
, UserJobQbotColonyPickingId, UserJobQbotReplicatingId, UserJobQbotGriddingId
, UserJobGenotypingId)
select distinct 
	id, uid, rechargeid, jobname, submissionType, HowManyPlates, plateType, Comment
	, cast(case when done = 2 then 0 else 1 end as bit) IsOpen
	, statusdate lastupdate, dateSubmitted datetimecreated
	, cast(case when submissionType = 1 then id2 else null end as bit) bacterialclone
	, cast(case when submissionType = 4 or submissionType = 2 then id2 else null end as bit) dna
	, cast(case when submissionType = 5 then id2 else null end as bit) sublibrary
	, cast(case when submissionType = 3 then id2 else null end as bit) userrun
	, cast(case when submissionType = 21 then id2 else null end as bit) colonypicking
	, cast(case when submissionType = 22 then id2 else null end as bit) replicating
	, cast(case when submissionType = 23 then id2 else null end as bit) gridding
	, cast(case when submissionType = 11 then id2 else null end as bit) genotyping
from cgfold.dbo.submission_userjobs suj
	left outer join (
		select userid, recharge.rechargeid, accountnum from cgfold.dbo.useracct
		inner join cgfold.dbo.recharge on useracct.rechargeid = recharge.rechargeid
		where recharge.valid = 'yes'
	) accts on suj.uid = accts.userid and suj.accountid = accts.accountnum
where submissionType <> 0
  and rechargeid is not null
  and id in (469 , 3107, 3130, 3549, 3623, 3686, 3705, 3713, 3996, 4458, 4476, 4520)
  and [datesubmitted] is not null

set identity_insert cgf.dbo.userjobs off

set identity_insert cgf.dbo.userjobs on
insert into cgf.dbo.UserJobs (id, userid, RechargeAccountId, name, jobtypeid, NumberPlates, platetype, comments, isopen
	, LastUpdate, DateTimeCreated
	, UserJobBacterialCloneId, UserJobDnaId, UserJobSublibraryId, UserJobUserRunId
	, UserJobQbotColonyPickingId, UserJobQbotReplicatingId, UserJobQbotGriddingId
	, UserJobGenotypingId)
select distinct 
	id, uid, null, jobname, submissionType, HowManyPlates, plateType, Comment
	, cast(case when done = 0 then 1 else 0 end as bit) IsOpen
	, statusdate lastupdate, dateSubmitted datetimecreated
	, cast(case when submissionType = 1 then id2 else null end as bit) bacterialclone
	, cast(case when submissionType = 4 or submissionType = 2 then id2 else null end as bit) dna
	, cast(case when submissionType = 5 then id2 else null end as bit) sublibrary
	, cast(case when submissionType = 3 then id2 else null end as bit) userrun
	, cast(case when submissionType = 21 then id2 else null end as bit) colonypicking
	, cast(case when submissionType = 22 then id2 else null end as bit) replicating
	, cast(case when submissionType = 23 then id2 else null end as bit) gridding
	, cast(case when submissionType = 11 then id2 else null end as bit) genotyping
from cgfold.dbo.submission_userjobs suj
where submissionType <> 0
  and suj.accountid = ''
  and [datesubmitted] is not null
set identity_insert cgf.dbo.userjobs off

set identity_insert cgf.dbo.userjobplates on
insert into cgf.dbo.UserJobPlates (id, userjobid, name)
select id, jobid, plateName from cgfold.dbo.submission_userplates
where jobid in (select id from cgfold.dbo.submission_userjobs where submissionType <> 0)
  and jobid in (select id from userjobs)
set identity_insert cgf.dbo.userjobplates off

set identity_insert cgf.dbo.barcodes on

insert into cgf.dbo.barcodes (id, UserJobPlateId, SubPlateId, PrimerId, stageid, SourceBarcodeId, DateCreated, done)
select bc.id, plateid, plateSubID
	, case when primerid = 0 then null else primerid end primerid
	, case	-- bacterial clone
			when submissionType = 1 and stage = 0 then 'BCWP'
			when submissionType = 1 and stage = 1 then 'BCPS'
			when submissionType = 1 and stage = 2 then 'BCRC'
			when submissionType = 1 and stage = 3 then 'BCSR'
			when submissionType = 1 and stage = 4 then 'BC37'
			-- pcr product
			when submissionType = 2 and stage = 0 then 'PPWP'
			when submissionType = 2 and stage = 1 then 'PPPS'
			when submissionType = 2 and stage = 2 then 'PPSR'
			when submissionType = 2 and stage = 3 then 'PP37'
			-- user run sequencing
			when submissionType = 3 and stage = 0 then 'URWP'
			when submissionType = 3 and stage = 1 then 'URPS'
			when submissionType = 3 and stage = 2 then 'UR37'
			-- purified dna
			when submissionType = 4 and stage = 0 then 'PDWP'
			when submissionType = 4 and stage = 1 then 'PDPS'
			when submissionType = 4 and stage = 2 then 'PDSR'
			when submissionType = 4 and stage = 3 then 'PD37'
			-- sublibrary
			when submissionType = 5 and stage = 0 then 'SLWP'
			when submissionType = 5 and stage = 1 then 'SLPS'
			when submissionType = 5 and stage = 2 then 'SL37'
			-- genotyping
			when submissionType = 11 and stage = 0 then 'UGWP'
			when submissionType = 11 and stage = 1 then 'UGPS'
			when submissionType = 11 and stage = 2 then 'UG37'
			-- qbot colonypicking
			when submissionType = 21 and stage = 0 then 'QPWP'
			when submissionType = 21 and stage = 1 then 'QPPS'
			when submissionType = 21 and stage = 2 then 'QP37'
			-- qbot replication
			when submissionType = 22 and stage = 0 then 'QRWP'
			when submissionType = 22 and stage = 1 then 'QRPS'
			when submissionType = 22 and stage = 2 then 'QR37'
			-- qbot gridding
			when submissionType = 23 and stage = 0 then 'QGWP'
			when submissionType = 23 and stage = 1 then 'QGPS'
			when submissionType = 23 and stage = 2 then 'QG37'
	  end stage
	, case when sourceBarcode = 0 then null else sourceBarcode end sourceBarcode, [date]
	, cast (bc.done as bit) done
from cgfold.dbo.barcode bc
	inner join cgfold.dbo.submission_userplates up on bc.plateID = up.id
	inner join cgfold.dbo.submission_userjobs uj on up.JobID = uj.id

set identity_insert cgf.dbo.barcodes off


-- imports quality analysis data, this blows, since you have to import using excel with 65k rows at a time
set identity_insert barcodefiles on

insert into barcodefiles (id, WellColumn, WellRow, BarcodeId, Uploaded, start, [end], DateTimeUploaded, DateTimeValidated)
select id, cellnum, cellchar, barcode, 0, start, [end], datetimesubmitted, datetimesubmitted
from cgfold.dbo.quality_phred
where id is not null
  and barcode in (select id from barcodes )

set identity_insert barcodefiles off