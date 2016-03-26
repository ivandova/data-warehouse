create table dwh_log (
	start datetime2(3) not null,
	[end] datetime2(3) not null,
	duration int not null,
	requested nvarchar(max) not null,
	status int not null,
	response nvarchar(max) not null,
	record_count int not null,
	index idx_start (start),
	index idx_status (status)
)
