CREATE PROCEDURE crud_delete_provider_service_country
    @ProviderServiceId INT
AS
BEGIN
    DELETE FROM ProviderServiceCountry 
    WHERE ProviderServiceId = @ProviderServiceId;
    
    SELECT @@ROWCOUNT AS DeletedRows;
END;
GO

