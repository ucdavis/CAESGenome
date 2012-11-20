declare @cursor cursor
declare @barcode int, @cellchar char(1), @cellnum int, @start int, @end int, @datetimesubmitted datetime

set @cursor = cursor for
	select top 100 barcode, cellchar, cellnum, start, [end], datetimesubmitted
	from cgfold.dbo.quality_phred

open @cursor

fetch next from @cursor into @barcode, @cellchar, @cellnum, @start, @end, @datetimesubmitted

while(@@FETCH_STATUS = 0)
begin

	update barcodefiles
	set [start] = @start, [end] = @end, datetimeuploaded = @datetimesubmitted, datetimevalidated = @datetimesubmitted
	where barcode = @barcode, wellrow = @cellchar, wellcolumn= @cellnum, 

	fetch next from @cursor into @barcode, @cellchar, @cellnum, @start, @end, @datetimesubmitted

end

close @cursor
deallocate @cursor