create table dwh_synchronize (
	path nvarchar(200) not null primary key,
	last_update datetime2(3)
)
