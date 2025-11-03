# Epic 1: Grundlagen & Kern-Infrastruktur

**Epic-Ziel:** Grundlegende Projektinfrastruktur einschließlich Authentifizierung, Mandantenfähigkeit und Kern-Rezeptabruffunktionen etablieren. Dieses Epic liefert das grundlegende API-Framework mit wesentlichen Rezeptzugriffsfunktionen, die sofortigen Wert für ARZ-Systeme zum Abrufen neuer Rezepte bieten.

### Story 1.1: Projektaufbau und Basis-Authentifizierung
Als ARZ-Systementwickler möchte ich sicheren API-Zugang mit Basis-Authentifizierung, damit ich sicher auf Rezeptdaten mit ordnungsgemäßen Anmeldeinformationen zugreifen kann.

**Akzeptanzkriterien:**
1. .NET 8.0 Web-API-Projekt mit ordnungsgemäßer Lösungsstruktur erstellt
2. Basis-Authentifizierungs-Middleware implementiert und konfiguriert
3. Authentifizierungs-Handler validiert Anmeldeinformationen gegen ARZ-Systemdatenbank
4. Alle API-Endpunkte erfordern Authentifizierung
5. Ordnungsgemäße HTTP-Statuscodes für Authentifizierungsfehler zurückgegeben (401 Unauthorized)
6. Integrationstests verifizieren Authentifizierungs-Erfolgs- und Fehlschlag-Szenarien

### Story 1.2: Mandantenfähigkeit und Verbindungsmanagement
Als ARZ-System möchte ich, dass die API automatisch meine Datenbankverbindung auflöst, damit ich nur auf die Rezeptdaten meiner Organisation zugreifen kann.

**Akzeptanzkriterien:**
1. Client-Identifikationsmechanismus implementiert (über Header oder Authentifizierung)
2. ArzSw-Datenbankintegration für Verbindungszeichenfolgen-Auflösung
3. Dynamische DbContext-Erstellung mit aufgelösten Verbindungszeichenfolgen
4. Mandantenisolation verifiziert - Clients können nur auf ihre eigenen Daten zugreifen
5. Connection Pooling für mehrere Mandanten-Szenarien optimiert
6. Fehlerbehandlung für ungültige oder fehlende Mandantenkonfigurationen

### Story 1.3: Basic Prescription Retrieval API
As an ARZ system, I want to retrieve all new prescriptions for my organization, so that I can begin processing them for insurance mediation.

**Acceptance Criteria:**
1. GET /v2/rezept endpoint implemented with versioning
2. Prescription filtering by TransferArz=false (new prescriptions only)
3. Support for all three prescription types (eMuster16, P-Rezept, E-Rezept)
4. Basic pagination implemented (page and pageSize parameters)
5. Prescription data includes essential fields: UUID, XML request data, type identifiers
6. Response includes proper metadata (total count, pagination info)
7. Error handling and logging for all failure scenarios

### Story 1.4: OpenAPI Documentation and Basic Monitoring
As an ARZ system developer, I want comprehensive API documentation, so that I can understand and integrate with the endpoints effectively.

**Acceptance Criteria:**
1. OpenAPI 3.x specification generated and accessible via Swagger UI
2. All endpoints documented with parameters, responses, and examples
3. Authentication requirements clearly documented
4. Basic request/response logging implemented
5. Health check endpoint available for monitoring
6. Environment configuration (Test/Staging/Live) clearly identified in responses
