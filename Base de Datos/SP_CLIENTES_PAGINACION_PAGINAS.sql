﻿USE [PruebaTecnicaPlg]
GO
/****** Object:  StoredProcedure [dbo].[CLIENTES_PAGINACION_PAGINAS]    Script Date: 27-12-2024 20:23:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[CLIENTES_PAGINACION_PAGINAS]
	@PageSize 			as int = 10,
	@PageIndex 			as int = 1,
    	@campo_orden		as varchar(100),
	@orden				as varchar(15),
	@Nombre				as varchar(100)		= NULL,
	@Pais				as varchar(50)		= NULL

AS
	/*Find the @PK type*/
	DECLARE @PKTable	varchar(100)
	DECLARE @PKName 	varchar(100)
	DECLARE @type 		varchar(100)
	DECLARE @prec 		int

	DECLARE @strPageSize 	varchar(50)
	DECLARE @strStartRow 	varchar(50)
	DECLARE @strFilter 		varchar(8000) 
	DECLARE @Sort 			varchar(200)

	DECLARE @sql       	nvarchar(4000)                   
	DECLARE @paramlist	nvarchar(4000)          


	SET @PKTable = 'Clientes'
	SET @PKName = 'ClienteID'

	SELECT @type=t.name, @prec=c.prec
	FROM sysobjects o 
	JOIN syscolumns c on o.id=c.id
	JOIN systypes t on c.xusertype=t.xusertype
	WHERE o.name = @PKTable AND c.name = @PKName

	IF CHARINDEX('char', @type) > 0
	   SET @type = @type + '(' + CAST(@prec AS varchar) + ')'

	/*Default Page Number*/
	IF @PageIndex < 1
		SET @PageIndex = 1

	/*Set paging variables.*/
	SET @strPageSize = CAST(@PageSize AS varchar(50))
	SET @strStartRow = CAST(((@PageIndex - 1)*@PageSize + 1) AS varchar(50))


	SELECT @strFilter = ''

	IF @Nombre IS NOT NULL
		SELECT @strFilter = @strFilter + ' AND Nombre LIKE ''%''+@xNombre+''%'' '

	IF @Pais IS NOT NULL
		SELECT @strFilter = @strFilter + ' AND Nombre LIKE ''%''+@xPais+''%'' '

	SELECT @Sort = 
	CASE @campo_orden
		WHEN 'OrdNombre'				THEN ' ORDER BY Nombre' 
		WHEN 'OrdPais'					THEN ' ORDER BY Pais' 
		ELSE ' ORDER BY FechaCreacion'
	END

	SELECT @Sort = @Sort + CASE @orden
		WHEN 'Ascendente'	THEN ' ASC' 
		WHEN 'Descendente'	THEN ' DESC' 
		ELSE ''
	END

	SELECT @sql ='DECLARE @PageSize int
	SET @PageSize =  ' + @strPageSize + '

	DECLARE @PK ' + @type + '
	DECLARE @tblPK TABLE (
							PK  ' + @type + ' NOT NULL PRIMARY KEY
						)
	DECLARE PagingCursor CURSOR DYNAMIC READ_ONLY FOR
	SELECT ClienteID FROM Clientes WITH (NOLOCK) WHERE 1 = 1 ' + @strFilter + @Sort + '

	OPEN PagingCursor
	FETCH RELATIVE ' + @strStartRow + ' FROM PagingCursor INTO @PK

	SET NOCOUNT ON

	WHILE @PageSize > 0 AND @@FETCH_STATUS = 0
	BEGIN
		INSERT @tblPK (PK)  VALUES (@PK)
		FETCH NEXT FROM PagingCursor INTO @PK
		SET @PageSize = @PageSize - 1
	END

	CLOSE       PagingCursor
	DEALLOCATE  PagingCursor

	SELECT 
		ClienteID,
		Nombre,
		Email,
		Telefono,
		Pais,
		FechaCreacion
	FROM Clientes WITH (NOLOCK) 
	JOIN @tblPK tblPK ON ClienteID = tblPK.PK WHERE  1 = 1 '+ @strFilter + @Sort

	--PRINT @sql

	SELECT @paramlist ='
		@xNombre			varchar(100),
		@xPais				varchar(50)'

	EXEC sp_executesql @sql, @paramlist, 
	@Nombre,
	@Pais
