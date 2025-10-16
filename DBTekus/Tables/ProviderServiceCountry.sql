CREATE TABLE ProviderServiceCountry (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ProviderServiceId INT NOT NULL,
    CountryId NVARCHAR(50) NOT NULL,
    
    CONSTRAINT FK_ProviderServiceCountry_ProviderService FOREIGN KEY (ProviderServiceId) 
        REFERENCES ProviderService(Id) ON DELETE CASCADE,
    CONSTRAINT UQ_ProviderServiceCountry_ProviderServiceCountry UNIQUE (ProviderServiceId, CountryId)
);

CREATE INDEX IX_ProviderServiceCountry_ProviderServiceId ON ProviderServiceCountry(ProviderServiceId);
CREATE INDEX IX_ProviderServiceCountry_CountryId ON ProviderServiceCountry(CountryId);

INSERT INTO ProviderServiceCountry (ProviderServiceId, CountryId) VALUES
(1, 'CO'), (1, 'US'), (1, 'MX'),
(2, 'CO'), (2, 'US'),
(3, 'CO'), (3, 'US'), (3, 'MX'), (3, 'BR'),
(4, 'CO'), (4, 'AR'),
(5, 'CO'), (5, 'US'), (5, 'CL'),
(6, 'US'), (6, 'BR'),
(8, 'CO'), (8, 'US'), (8, 'MX'), (8, 'BR');

