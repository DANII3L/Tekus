CREATE PROCEDURE crud_delete_provider_service
    @Id INT
AS
BEGIN
    BEGIN TRANSACTION;
    
    BEGIN TRY
        DELETE FROM ProviderServiceCountry 
        WHERE ProviderServiceId = @Id;
        
        DELETE FROM ProviderService 
        WHERE Id = @Id;
        
        COMMIT TRANSACTION;
        
        SELECT @@ROWCOUNT AS DeletedRows;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        
        THROW;
    END CATCH
END;
GO

