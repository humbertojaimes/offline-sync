--Run this query on master
CREATE LOGIN syncoffline WITH password = 'N3tconf@2021';
GO

--open another query connection and run this query on the selected database
--DROP USER syncoffline;
CREATE USER syncoffline FOR LOGIN syncoffline 
WITH DEFAULT_SCHEMA = dbo;
GO
--give permissions as owner on the selected database
ALTER ROLE db_owner ADD MEMBER [syncoffline];
GO

--server azure sql database
/*
Server: onlysql.database.windows.net
user: syncoffline
pass: N3tconf@2021
database: ventas
*/
