CREATE PROCEDURE crud_update_provider_service_country
    @Id INT,
    @ProviderServiceId INT,
    @CountryId NVARCHAR(50)
AS
BEGIN
    UPDATE ProviderServiceCountry 
    SET 
        ProviderServiceId = @ProviderServiceId,
        CountryId = @CountryId
    WHERE Id = @Id;
    
    SELECT 
        Id,
        ProviderServiceId,
        CountryId
    FROM ProviderServiceCountry
    WHERE Id = @Id;
END;
GO

