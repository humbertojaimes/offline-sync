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

--Ejecucion del SP de Sincronización
EXECUTE dbo.usp_VentasTiendasSyncOfflineHashID @VTSOLAPP 
GO

--SNAPSHOT
DECLARE @VTSOLAPP VentasTiendasSyncOfflineApp
EXECUTE dbo.usp_VentasTiendasSyncOfflineHashID @VTSOLAPP 
GO


--No existen cambios
DECLARE @VTSOLAPP VentasTiendasSyncOfflineApp
--Estos registro están correctos, los restantes se mandan como inserts
INSERT INTO @VTSOLAPP 
SELECT ID, VT.VentasTiendasHashID
FROM VentasTiendas VT
EXECUTE dbo.usp_VentasTiendasSyncOfflineHashID @VTSOLAPP 
GO
