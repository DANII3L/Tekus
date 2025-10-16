CREATE PROCEDURE crud_insert_provider
    @Nit NVARCHAR(50),
    @Name NVARCHAR(255),
    @Email NVARCHAR(255)
AS
BEGIN
    INSERT INTO Provider (Nit, Name, Email)
    VALUES (@Nit, @Name, @Email);
    
    SELECT 
        Id,
        Nit,
        Name,
        Email
    FROM Provider
    WHERE Id = SCOPE_IDENTITY();
END;
GO

