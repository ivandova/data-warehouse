create table dwh_log (
	start datetime2(3) not null,
	[end] datetime2(3) not null,
	duration int not null,
	path nvarchar(200) not null,
	error nvarchar(max),
	record_count int,
	index idx_start (start),
	index idx_path (path)
)
