/*
--run at on-premises environments
CREATE DATABASE NetConf2021
GO

USE ventas
GO
*/
--DROP TABLE [dbo].[Articulos]
CREATE TABLE [dbo].[Articulos](
	[ID] [int] NOT NULL,
	[EAN] [bigint] NOT NULL,
	[DESCRIPCION] [varchar](100) NOT NULL,
	[ArticuloHashID] AS HASHBYTES('SHA2_256', CONVERT(varchar(20),ID) + CONVERT(varchar(20),EAN) + DESCRIPCION)
 CONSTRAINT [PK_Articulos] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[EAN] ASC
)
) ON [PRIMARY]
GO

--DROP TABLE [dbo].[Tiendas]
CREATE TABLE [dbo].[Tiendas](
	[ID] [int] NOT NULL,
	[TIENDA] [varchar](50) NOT NULL,
	[DIRECCION] [varchar](100) NOT NULL,
	[TiendaHashID] AS HASHBYTES('SHA2_256', CONVERT(varchar(20),ID) + TIENDA + DIRECCION),
 CONSTRAINT [PK_Tiendas] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)
) ON [PRIMARY]
GO

--DROP TABLE [dbo].[VentasTiendas]
CREATE TABLE [dbo].[VentasTiendas](
	[ID] [int] IDENTITY NOT NULL,
	[FECHA] [varchar](8) NOT NULL,
	[TIENDA] [int] NOT NULL,
	[ARTICULO] [int] NOT NULL,
	[VENTA] [int] NOT NULL,
    [VentasTiendasHashID] AS HASHBYTES('SHA2_256', FECHA + CONVERT(varchar(20),TIENDA) + CONVERT(varchar(20),ARTICULO) + CONVERT(varchar(20),VENTA)),
 CONSTRAINT [PK_VentasTiendas] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)
) ON [PRIMARY]
GO

--------------------------------------------
--SECOND PART
--Run this section when you start to talk about Sync-offline
--------------------------------------------
--DROP TABLE [dbo].[CAT_TABLAS]
CREATE TABLE [dbo].[CAT_TABLAS](
	[ID_CAT_TABLA] [int] NOT NULL,
	[NOMBRE_TABLA] [varchar](100) NOT NULL,
	[FECHA_SYNC] [DATETIME],
 CONSTRAINT [PK_CAT_TABLAS] PRIMARY KEY CLUSTERED 
(
	[ID_CAT_TABLA] ASC,
	[NOMBRE_TABLA] ASC
)
) ON [PRIMARY]
GO
INSERT INTO CAT_TABLAS VALUES
(1, 'Articulos', GETDATE()),
(2, 'Tiendas', GETDATE()),
(3, 'VentasTiendas', GETDATE())
GO


















CREATE TABLE [dbo].[CAT_CONFLICTOS](
	[ID_CAT_CONFLICTOS] [int] NOT NULL,
	[NOMBRECONFLICTO]  [varchar](100) NOT NULL,
 CONSTRAINT [PK_CAT_CONFLICTOS] PRIMARY KEY CLUSTERED 
(
	[ID_CAT_CONFLICTOS] ASC,
	[NOMBRECONFLICTO] ASC
)
) ON [PRIMARY]
GO

INSERT INTO CAT_CONFLICTOS VALUES
(1, 'Registro duplicado'),
(2, 'Registro no existente'),
(3, 'Volver a cargar la tabla')
GO



CREATE TABLE [dbo].[SYNCOFFLINE](
	[ID_SYNCOFFLINE] [int] IDENTITY NOT NULL,
	[ID_TABLA] [int] NOT NULL,
	[FECHASYNC] [DATETIME] NOT NULL,
 CONSTRAINT [PK_SYNCOFFLINE] PRIMARY KEY CLUSTERED 
(
	[ID_SYNCOFFLINE] ASC
)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[RESOLVER_CONFLICTOS](
	[ID_RESOLVER_CONFLICTOS] [int] IDENTITY NOT NULL,
	[ID_CAT_TABLA] [int] NOT NULL,
	[ID_CAT_CONFLICTOS] [int] NOT NULL,
	[RESUELTO] [varchar](20) NOT NULL,
 CONSTRAINT [PK_RESOLVER_CONFLICTOS] PRIMARY KEY CLUSTERED 
(
	[ID_RESOLVER_CONFLICTOS] ASC
)
) ON [PRIMARY]
GO


