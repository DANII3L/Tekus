-- =============================================
-- Table: User
-- Description: Stores user information for authentication
-- =============================================

CREATE TABLE [User] (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(100) NOT NULL UNIQUE,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL,
    FullName NVARCHAR(255) NOT NULL
);

CREATE INDEX IX_User_Username ON [User](Username);
CREATE INDEX IX_User_Email ON [User](Email);

-- Ejemplo de usuario con contraseña "password123"
INSERT INTO [User] (Username, Email, Password, FullName) VALUES
('user_argon2', 'user_argon2@tekus.com', 'Q62zmMIYelrnwA1UmN/MMoUBBKn2VLR5NHEgh8JmmMM88aOsze2eS5deM+81+a4a', 'User Argon2');

-- Verificar usuarios insertados
SELECT Id, Username, Email, FullName, LEN(Password) as PasswordLength 
FROM [User] 
WHERE Username LIKE '%argon2%';