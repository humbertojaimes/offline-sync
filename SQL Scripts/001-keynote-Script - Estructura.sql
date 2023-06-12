 /*
Script to create an Azure SQL database
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
