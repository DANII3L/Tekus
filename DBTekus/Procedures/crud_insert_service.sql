CREATE PROCEDURE crud_insert_service
    @Name NVARCHAR(255),
    @HourlyRate DECIMAL(18,2)
AS
BEGIN
    INSERT INTO Service (Name, HourlyRate)
    VALUES (@Name, @HourlyRate);
    
    SELECT 
        Id,
        Name,
        HourlyRate
    FROM Service
    WHERE Id = SCOPE_IDENTITY();
END;
GO

