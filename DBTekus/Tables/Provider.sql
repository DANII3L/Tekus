-- =============================================
-- Table: Provider
-- Description: Stores provider information
-- =============================================

CREATE TABLE Provider (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nit NVARCHAR(50) NOT NULL UNIQUE,
    Name NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) NOT NULL
);

CREATE INDEX IX_Provider_Email ON Provider(Email);
CREATE INDEX IX_Provider_Name ON Provider(Name);
CREATE INDEX IX_Provider_Nit ON Provider(Nit);

-- =============================================
-- Insert 10 providers
-- =============================================

INSERT INTO Provider (Nit, Name, Email) VALUES
('900123456-7', 'Tech Solutions Inc', 'contact@techsolutions.com'),
('900234567-8', 'Global IT Services', 'info@globalit.com'),
('900345678-9', 'Cloud Systems Corp', 'sales@cloudsystems.com'),
('900456789-0', 'Digital Innovations LLC', 'hello@digitalinn.com'),
('900567890-1', 'Software Experts Group', 'contact@softwareexperts.com'),
('900678901-2', 'Data Analytics Pro', 'info@dataanalytics.com'),
('900789012-3', 'Web Development Studio', 'studio@webdev.com'),
('900890123-4', 'Mobile Apps Factory', 'factory@mobileapps.com'),
('900901234-5', 'Cyber Security Plus', 'security@cybersec.com'),
('900012345-6', 'AI Technology Partners', 'partners@aitech.com');
