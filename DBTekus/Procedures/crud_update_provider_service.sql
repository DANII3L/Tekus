CREATE PROCEDURE crud_update_provider_service
    @Id INT,
    @ProviderId INT,
    @ServiceId INT
AS
BEGIN
    UPDATE ProviderService 
    SET 
        ProviderId = @ProviderId,
        ServiceId = @ServiceId
    WHERE Id = @Id;
    
    SELECT 
        ps.Id,
        ps.ProviderId,
        ps.ServiceId,
        s.Name AS ServiceName,
        s.HourlyRate
    FROM ProviderService ps
    INNER JOIN Service s ON ps.ServiceId = s.Id
    WHERE ps.Id = @Id;
END;
GO

