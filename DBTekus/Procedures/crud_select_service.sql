-- =============================================
-- Stored Procedure: crud_select_service
-- Description: Get services with pagination and filters
-- =============================================

CREATE PROCEDURE crud_select_service
    @Id INT = NULL,
    @Name NVARCHAR(255) = NULL,
    @Filter NVARCHAR(255) = NULL,
    @PageNumber INT = 1,
    @PageSize INT = 10
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @sql NVARCHAR(MAX);
    DECLARE @whereClause NVARCHAR(MAX) = N'
        AND (@Id IS NULL OR Id = @Id)
        AND (@Name IS NULL OR Name LIKE N''%'' + @Name + N''%'')
    ';
    IF @Filter IS NULL
        SET @Filter = N'';

    SET @sql = N'
    ;WITH ServicesFiltrados AS (
        SELECT
            Id,
            Name,
            HourlyRate
        FROM Service
        WHERE 1=1
        ' + @whereClause + N'
        ' + @Filter + N'
    )
    SELECT *,
           (SELECT COUNT(*) FROM ServicesFiltrados) AS TotalRecords
    FROM ServicesFiltrados
    ORDER BY Name ASC
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;
    ';

    EXEC sp_executesql @sql,
        N'@Id INT, @Name NVARCHAR(255), @Filter NVARCHAR(255), @PageNumber INT, @PageSize INT',
        @Id = @Id,
        @Name = @Name,
        @Filter = @Filter,
        @PageNumber = @PageNumber,
        @PageSize = @PageSize;
END;
GO

