CREATE PROCEDURE crud_update_provider
    @Id INT,
    @Nit NVARCHAR(50),
    @Name NVARCHAR(255),
    @Email NVARCHAR(255)
AS
BEGIN
    UPDATE Provider 
    SET 
        Nit = @Nit,
        Name = @Name,
        Email = @Email
    WHERE Id = @Id;
    
    SELECT 
        Id,
        Nit,
        Name,
        Email
    FROM Provider
    WHERE Id = @Id;
END;
GO

