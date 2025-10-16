CREATE OR ALTER PROCEDURE crud_select_provider_service_country
    @Id INT = NULL,
    @ProviderServiceId INT = NULL,
    @CountryId NVARCHAR(50) = NULL,
    @Filter NVARCHAR(255) = NULL,
    @PageNumber INT = 1,
    @PageSize INT = 10
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @sql NVARCHAR(MAX);
    DECLARE @whereClause NVARCHAR(MAX) = N'
        AND (@Id IS NULL OR psc.Id = @Id)
        AND (@ProviderServiceId IS NULL OR psc.ProviderServiceId = @ProviderServiceId)
        AND (@CountryId IS NULL OR psc.CountryId = @CountryId)
    ';

    IF @Filter IS NULL
        SET @Filter = N'';

    SET @sql = N'
    ;WITH ProviderServiceCountriesFiltrados AS (
        SELECT
            psc.Id,
            psc.ProviderServiceId,
            psc.CountryId
        FROM ProviderServiceCountry psc
        INNER JOIN ProviderService ps ON psc.ProviderServiceId = ps.Id
        WHERE 1=1
        ' + @whereClause + N'
        ' + @Filter + N'
    )
    SELECT *,
           (SELECT COUNT(*) FROM ProviderServiceCountriesFiltrados) AS TotalRecords
    FROM ProviderServiceCountriesFiltrados
    ORDER BY ProviderServiceId, CountryId ASC
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;
    ';

    EXEC sp_executesql @sql,
        N'@Id INT, @ProviderServiceId INT, @CountryId NVARCHAR(50), @Filter NVARCHAR(255), @PageNumber INT, @PageSize INT',
        @Id = @Id,
        @ProviderServiceId = @ProviderServiceId,
        @CountryId = @CountryId,
        @Filter = @Filter,
        @PageNumber = @PageNumber,
        @PageSize = @PageSize;
END;
GO

