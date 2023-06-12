/*
select [ID], [VentasTiendasHashID] into #ventastiendas 
from ventastiendas
Where 1 = 2

select * from #ventastiendas 
drop table #ventastiendas 
select top 100 * from ventastiendas
select MAX(ID)  from ventastiendas
GO
*/


DECLARE @VTSOLAPP VentasTiendasSyncOfflineApp
--Este registro no existe, se debe de mandar a borrar
INSERT INTO @VTSOLAPP 
SELECT 18601, VT.VentasTiendasHashID
FROM VentasTiendas VT
WHERE ID = 100

--Este registro el HASHID es diferente, se hace update
INSERT INTO @VTSOLAPP VALUES (1, CONVERT(varbinary(8000), 'abcdefg'))

--Estos registro están correctos, los restantes se mandan como inserts
INSERT INTO @VTSOLAPP 
SELECT ID, VT.VentasTiendasHashID
FROM VentasTiendas VT
WHERE ID >= 100 AND ID <= 17000

--SELECT * FROM @VTSOLAPP

    CREATE TABLE #VentasTiendasSyncOfflineBD
        ([ID] [int],
        [FECHA] [varchar](8),
        [TIENDA] [int],
        [ARTICULO] [int],
        [VENTA] [int],
        [VentasTiendasHashID] [VARBINARY](8000),  
        [TipoMovimiento] [varchar](20) );

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



