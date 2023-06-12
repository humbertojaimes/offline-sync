--Run this query on master
CREATE LOGIN netconf WITH password = 'N3tconf@2021';
GO

--open another query connection and run this query on the selected database
--DROP USER netconf;
CREATE USER netconf FOR LOGIN netconf 
WITH DEFAULT_SCHEMA = dbo;
GO
--give permissions as owner on the selected database
ALTER ROLE db_owner ADD MEMBER [netconf];
GO

--server azure sql database
/*
Server: onlysql.database.windows.net
user: netconf
pass: N3tconf@2021
database: asistentes
*/
