--Script para crear el SP de Sincronización offline
--Se debe de crear primero el objeto tipo tabla

DROP TYPE VentasTiendasSyncOfflineApp;
GO
CREATE TYPE VentasTiendasSyncOfflineApp
   AS TABLE
      ( [ID] [int],
        [VentasTiendasHashID] VARBINARY(8000));
GO

--Se procede a ejecutar la creación/modificación del procedimiento almacenado
DROP PROCEDURE dbo.usp_VentasTiendasSyncOfflineHashID
GO

CREATE OR ALTER PROCEDURE dbo.usp_VentasTiendasSyncOfflineHashID
   @VTSOLAPP VentasTiendasSyncOfflineApp READONLY
      AS
      SET NOCOUNT ON
      --Leemos la tabla que viene como parametro
      --Se comparan ID & HASH-ID vs tabla
      
      --Creamos la tabla temp para regresarla como salida
      CREATE TABLE #VentasTiendasSyncOfflineBD
        ([ID] [int],
        [FECHA] [varchar](8),
        [TIENDA] [int],
        [ARTICULO] [int],
        [VENTA] [int],
        [VentasTiendasHashID] [VARBINARY](8000),  
        [TipoMovimiento] [varchar](20) )

      --Escenario 1, ID no existe, se borra
      INSERT INTO #VentasTiendasSyncOfflineBD
      SELECT VTSO.ID, NULL, NULL, NULL, NULL, VTSO.VentasTiendasHashID, 'DELETE' 
      FROM VentasTiendas VT
      RIGHT JOIN @VTSOLAPP VTSO 
      ON VT.ID = VTSO.ID
      WHERE VT.ID IS NULL
      --Escenario 2, ID existe, pero el hash es distinto, manda la bd
      INSERT INTO #VentasTiendasSyncOfflineBD
      SELECT VT.ID, VT.FECHA, VT.TIENDA, VT.ARTICULO, VT.VENTA, VT.VentasTiendasHashID,'UPDATE'
      FROM VentasTiendas VT
      INNER JOIN @VTSOLAPP VTSO 
      ON VT.ID = VTSO.ID
      WHERE VT.VentasTiendasHashID <> VTSO.VentasTiendasHashID
      --Escenario 3, regreso solo los IDs que tiene la BD como nuevos
      INSERT INTO #VentasTiendasSyncOfflineBD
      SELECT VT.ID, VT.FECHA, VT.TIENDA, VT.ARTICULO, VT.VENTA, VT.VentasTiendasHashID,'INSERT'
      FROM VentasTiendas VT
      LEFT JOIN @VTSOLAPP VTSO 
      ON VT.ID = VTSO.ID
      WHERE VTSO.ID IS NULL

      SELECT * FROM #VentasTiendasSyncOfflineBD
      DROP TABLE #VentasTiendasSyncOfflineBD
      SET NOCOUNT OFF
GO
