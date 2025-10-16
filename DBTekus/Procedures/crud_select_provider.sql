-- =============================================
-- Stored Procedure: crud_select_provider
-- Description: Get providers with pagination and filters
-- =============================================

CREATE PROCEDURE crud_select_provider
    @Id INT = NULL,
    @Name NVARCHAR(255) = NULL,
    @Nit NVARCHAR(50) = NULL,
    @Email NVARCHAR(255) = NULL,
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
          AND (@Nit IS NULL OR Nit LIKE N''%'' + @Nit + N''%'')
          AND (@Email IS NULL OR Email LIKE N''%'' + @Email + N''%'')
    ';
    IF @Filter IS NULL
        SET @Filter = N'';

    SET @sql = N'
    ;WITH ProvidersFiltrados AS (
        SELECT
            Id,
            Nit,
            Name,
            Email
        FROM Provider
        WHERE 1=1
        ' + @whereClause + N'
        ' + @Filter + N'
    )
    SELECT *,
           (SELECT COUNT(*) FROM ProvidersFiltrados) AS TotalRecords
    FROM ProvidersFiltrados
    ORDER BY Name ASC
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;
    ';

    EXEC sp_executesql @sql,
        N'@Id INT, @Name NVARCHAR(255), @Nit NVARCHAR(50), @Email NVARCHAR(255), @Filter NVARCHAR(255), @PageNumber INT, @PageSize INT',
        @Id = @Id,
        @Name = @Name,
        @Nit = @Nit,
        @Email = @Email,
        @Filter = @Filter,
        @PageNumber = @PageNumber,
        @PageSize = @PageSize;
END;
GO
