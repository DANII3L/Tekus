-- =============================================
-- Table: Service
-- Description: Stores service information
-- =============================================

CREATE TABLE Service (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    HourlyRate DECIMAL(18,2) NOT NULL
);

CREATE INDEX IX_Service_Name ON Service(Name);

-- =============================================
-- Insert 10 services
-- =============================================

INSERT INTO Service (Name, HourlyRate) VALUES
('Web Development', 75.00),
('Mobile App Development', 85.00),
('UI/UX Design', 65.00),
('Database Administration', 80.00),
('Cloud Infrastructure Setup', 95.00),
('DevOps Engineering', 90.00),
('Cybersecurity Consulting', 100.00),
('Data Analysis', 70.00),
('Quality Assurance Testing', 60.00),
('Technical Support', 45.00);
