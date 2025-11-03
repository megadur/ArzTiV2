# Technische Annahmen

### Repository-Struktur
**Entscheidung: Monorepo**
- Einzelnes Repository mit allen ARZ_TI 2.0-Komponenten
- Gemeinsame Modelle und DTOs für verschiedene Rezepttypen
- Zentralisierte Konfigurations- und Deployment-Skripte

### Service-Architektur
**Entscheidung: Monolithische API mit Microservices-Mustern**
- Einzelne deploybare .NET 8.0 Web-API-Anwendung
- Interne Trennung der Belange über verschiedene Service-Schichten
- Database-per-Tenant-Muster über dynamische Verbindungszeichenfolgen
- Horizontale Skalierungsfähigkeit durch Load Balancing ist nicht vorgesehen

### Testanforderungen
**Entscheidung: Unit + Integration + Performance Testing**
- Unit-Tests für alle Geschäftslogik- und Repository-Schichten
- Integrationstests für API-Endpunkte mit Datenbankinteraktionen
- Performance-Tests speziell für 1M+ Datensatz-Szenarien
- Lasttests für gleichzeitige Benutzerszenarios (5+ Benutzer)

### Zusätzliche technische Annahmen und Anfragen (aktualisiert 2025-10-19)
- **Datenbankperformance**: Kritische Indizes müssen für Antwortzeiten unter einer Sekunde erstellt werden
- **Tenant-Database Access**: EF Core bleibt für alle Mandanten-Datenbanken (Übernahme aus v2), keine Dapper-Migration
- **Caching-Strategie**: kein Redis/Multi-Level-Caching
- **Connection Pooling**: Optimiertes Datenbankverbindungsmanagement pro Tenant für hohe Parallelität
- **Monitoring**: Application Performance Monitoring (APM) für Produktionsumgebungen
- **Authentifizierung**: Zunächst Basic Authentication, mit der Möglichkeit zum Upgrade auf OAuth/JWT
- **API-Dokumentation**: OpenAPI 3.x-Spezifikation mit umfassenden Beispielen und Schemas
- **KI-unterstützte Entwicklung**: Effizienzsteigerung und Kostenreduktion durch KI-Tools (z.B. GitHub Copilot, automatisierte Tests)

