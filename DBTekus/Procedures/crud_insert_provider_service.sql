CREATE PROCEDURE crud_insert_provider_service
    @ProviderId INT,
    @ServiceId INT
AS
BEGIN
    INSERT INTO ProviderService (ProviderId, ServiceId)
    VALUES (@ProviderId, @ServiceId);
    
    SELECT 
        ps.Id,
        ps.ProviderId,
        ps.ServiceId,
        p.Name AS ProviderName,
        p.Nit AS ProviderNit,
        p.Email AS ProviderEmail,
        s.Name AS ServiceName,
        s.HourlyRate
    FROM ProviderService ps
    INNER JOIN Provider p ON ps.ProviderId = p.Id
    INNER JOIN Service s ON ps.ServiceId = s.Id
    WHERE ps.Id = SCOPE_IDENTITY();
END;
GO

