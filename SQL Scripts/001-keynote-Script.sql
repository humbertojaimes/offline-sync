 /*
Script to create and Azure SQL database
Script to generate the first inserts (only for testing)
For the demo only create the tables not run the inserts (inserts only if Humberto or The GodFather need it)
Develop by Jesus Gil | Dr Rudo SQL
*/

/*
--Run only for on-premises environment
Create Database Asistentes
GO
*/

USE Asistentes
GO

IF OBJECT_ID ('dbo.Asistentes', 'U') IS NOT NULL  
   DROP TABLE Asistentes;
GO 
Create Table Asistentes
(
ID_asistente   INT IDENTITY(1,1) PRIMARY KEY,
Nombre         VARCHAR(100),
FechaAlta      DATE,
HoraAlta       TIME
)
GO

/*
--Run to demostrate how work date & time data type in SQL Server
SELECT GETDATE() AS DATETIME
SELECT CONVERT(varchar(10), GETDATE(), 101) AS DATE
SELECT CONVERT(varchar(10), GETDATE(), 108) AS TIME
--assign to variables
DECLARE @Fecha DATE, @Tiempo TIME
SELECT @Fecha = CONVERT(varchar(10), GETDATE(), 101)
SELECT @Tiempo = CONVERT(varchar(10), GETDATE(), 108)
SELECT @Fecha as SoloFecha, @Tiempo as SoloTiempo
*/


/*
--Run only for demo, after need it, drop and recreate to start the row in 1
INSERT INTO Asistentes VALUES ('Luis Beltran',CONVERT(varchar(10), GETDATE(), 101), CONVERT(varchar(10), GETDATE(), 108))
INSERT INTO Asistentes VALUES ('Humberto Jaimes',CONVERT(varchar(10), GETDATE(), 101), CONVERT(varchar(10), GETDATE(), 108))
INSERT INTO Asistentes VALUES ('Cristian González',CONVERT(varchar(10), GETDATE(), 101), CONVERT(varchar(10), GETDATE(), 108))
INSERT INTO Asistentes VALUES ('Jesus Gil',CONVERT(varchar(10), GETDATE(), 101), CONVERT(varchar(10), GETDATE(), 108))

SELECT * FROM Asistentes
*/



IF OBJECT_ID ('dbo.Sesion', 'U') IS NOT NULL  
   DROP TABLE Sesion;
GO 
Create Table Sesion
(
ID_sesion		   INT IDENTITY(1,1) PRIMARY KEY,
NombreSesion		VARCHAR(100),
FechadelaSesion   DATE,
HoraInicio        TIME,
HoraFin           TIME  
)
GO

/*
--Run only for demo, after need it, drop and recreate to start from scratch
INSERT INTO Sesion VALUES ('KEYNOTE', '11/20/2021', '09:00:00', '09:59:59')
INSERT INTO Sesion VALUES ('Creacion y configuracion de BDs Relacionales y No Relacionales', '11/20/2021', '10:00:00', '10:59:59')
INSERT INTO Sesion VALUES ('Construcción de APIs con escritura a Bases de datos con contenedores', '11/20/2021', '11:00:00', '11:59:59')
INSERT INTO Sesion VALUES ('Despliegue de APIs con App Service y uso de FrontEnd Mobile y Web para consumo', '11/20/2021', '12:00:00', '12:59:59')
GO

SELECT * FROM Sesion

*/



IF OBJECT_ID ('dbo.AsistentesPorSesion', 'U') IS NOT NULL  
   DROP TABLE AsistentesPorSesion;
GO 
Create Table AsistentesPorSesion
(
ID_asistentesxSesion	INT IDENTITY(1,1) Primary Key,
ID_asistente		   INT,
ID_sesion		      INT
)
GO




/*
--Run only for demo, after need it, drop and recreate to start from scratch
INSERT INTO AsistentesPorSesion VALUES ('1','1')
INSERT INTO AsistentesPorSesion VALUES ('2','1')
INSERT INTO AsistentesPorSesion VALUES ('3','2')
INSERT INTO AsistentesPorSesion VALUES ('4','2')
INSERT INTO AsistentesPorSesion VALUES ('1','3')
INSERT INTO AsistentesPorSesion VALUES ('2','3')
GO

SELECT * FROM AsistentesPorSesion
*/



/*
--All the queries
SELECT * FROM Asistentes
SELECT * FROM Sesion
SELECT * FROM AsistentesPorSesion

*/