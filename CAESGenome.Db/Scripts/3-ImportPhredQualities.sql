/*
Updates barcode files with the new phred quality values from the intermediate database
----------------------------------------------------*/
SET NOCOUNT ON
declare @cursor cursor
declare @barcode int, @cellchar char(1), @cellnum int, @start int, @end int, @datetimesubmitted datetime

set @cursor = cursor for
	select top 100 percent barcode, cellchar, cellnum, start, [end], replace(datetimesubmitted, '.0000000', '.000') datetimesubmitted
	from cgfold.dbo.quality_phred

open @cursor

fetch next from @cursor into @barcode, @cellchar, @cellnum, @start, @end, @datetimesubmitted
DECLARE @rowcount bigint = 0
while(@@FETCH_STATUS <> -1)
begin

	update cgf.dbo.barcodefiles
	set [start] = @start, [end] = @end, datetimeuploaded = @datetimesubmitted, datetimevalidated = @datetimesubmitted
	where barcodeid = @barcode and wellrow = @cellchar and wellcolumn= @cellnum

	SELECT @rowcount = @rowcount + 1
	PRINT @rowcount

	fetch next from @cursor into @barcode, @cellchar, @cellnum, @start, @end, @datetimesubmitted

end

close @cursor
deallocate @cursor



