CREATE PROCEDURE crud_update_service
    @Id INT,
    @Name NVARCHAR(255),
    @HourlyRate DECIMAL(18,2)
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE Service 
    SET 
        Name = @Name,
        HourlyRate = @HourlyRate
    WHERE Id = @Id;
    
    SELECT 
        Id,
        Name,
        HourlyRate
    FROM Service
    WHERE Id = @Id;
END;
GO
