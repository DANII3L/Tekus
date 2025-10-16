CREATE PROCEDURE crud_insert_provider_service_country
    @ProviderServiceId INT,
    @CountryId NVARCHAR(50)
AS
BEGIN
    INSERT INTO ProviderServiceCountry (ProviderServiceId, CountryId)
    VALUES (@ProviderServiceId, @CountryId);
    
    SELECT 
        psc.Id,
        psc.ProviderServiceId,
        psc.CountryId,
        ps.ProviderId,
        ps.ServiceId,
        p.Name AS ProviderName,
        p.Nit AS ProviderNit,
        p.Email AS ProviderEmail,
        s.Name AS ServiceName,
        s.HourlyRate
    FROM ProviderServiceCountry psc
    INNER JOIN ProviderService ps ON psc.ProviderServiceId = ps.Id
    INNER JOIN Provider p ON ps.ProviderId = p.Id
    INNER JOIN Service s ON ps.ServiceId = s.Id
    WHERE psc.Id = SCOPE_IDENTITY();
END;
GO

