-- =============================================
-- Stored Procedure: crud_select_login
-- Description: Get user by username for login authentication
-- =============================================

CREATE PROCEDURE crud_select_login
    @Username NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        Username,
        Email,
        Password,
        FullName
    FROM [User]
    WHERE Username = @Username;
END;
GO
