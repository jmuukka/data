-- This is for SQL Server

create table Test
(
	Id int not null identity,
	RequireString20 nvarchar(20) not null,
	NullableString10 nvarchar(10) null,
	RequireDateTime datetime2 not null,
	NullableDateTime datetime2 null,
	NullableBytes varbinary(MAX) null,
	NullableVariant sql_variant null
);

