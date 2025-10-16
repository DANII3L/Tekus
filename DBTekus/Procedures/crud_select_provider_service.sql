CREATE PROCEDURE crud_select_provider_service
    @Id INT = NULL,
    @ProviderId INT = NULL,
    @ServiceId INT = NULL,
    @Filter NVARCHAR(255) = NULL,
    @PageNumber INT = 1,
    @PageSize INT = 10
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @sql NVARCHAR(MAX);
    DECLARE @whereClause NVARCHAR(MAX) = N'
        AND (@Id IS NULL OR ps.Id = @Id)
        AND (@ProviderId IS NULL OR ps.ProviderId = @ProviderId)
        AND (@ServiceId IS NULL OR ps.ServiceId = @ServiceId)
    ';

    IF @Filter IS NULL
        SET @Filter = N'';

    SET @sql = N'
    ;WITH ProviderServicesFiltrados AS (
        SELECT
            ps.Id,
            ps.ProviderId,
            ps.ServiceId,
            s.Name AS ServiceName,
            s.HourlyRate
        FROM ProviderService ps
        INNER JOIN Service s ON ps.ServiceId = s.Id
        WHERE 1=1
        ' + @whereClause + N'
        ' + @Filter + N'
    )
    SELECT *,
           (SELECT COUNT(*) FROM ProviderServicesFiltrados) AS TotalRecords
    FROM ProviderServicesFiltrados
    ORDER BY ServiceName ASC
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;
    ';

    EXEC sp_executesql @sql,
        N'@Id INT, @ProviderId INT, @ServiceId INT, @Filter NVARCHAR(255), @PageNumber INT, @PageSize INT',
        @Id = @Id,
        @ProviderId = @ProviderId,
        @ServiceId = @ServiceId,
        @Filter = @Filter,
        @PageNumber = @PageNumber,
        @PageSize = @PageSize;
END;
GO

