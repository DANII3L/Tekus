CREATE TABLE ProviderService (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ProviderId INT NOT NULL,
    ServiceId INT NOT NULL,
    
    CONSTRAINT FK_ProviderService_Provider FOREIGN KEY (ProviderId) 
        REFERENCES Provider(Id) ON DELETE CASCADE,
    CONSTRAINT FK_ProviderService_Service FOREIGN KEY (ServiceId) 
        REFERENCES Service(Id) ON DELETE CASCADE,
    CONSTRAINT UQ_ProviderService_ProviderService UNIQUE (ProviderId, ServiceId)
);

CREATE INDEX IX_ProviderService_ProviderId ON ProviderService(ProviderId);
CREATE INDEX IX_ProviderService_ServiceId ON ProviderService(ServiceId);

INSERT INTO ProviderService (ProviderId, ServiceId) VALUES
(1, 1), (1, 2), (1, 3),
(2, 4), (2, 5), (2, 6), (2, 10),
(3, 5), (3, 6), (3, 7),
(4, 1), (4, 3), (4, 8), (4, 9),
(5, 1), (5, 2), (5, 9);

