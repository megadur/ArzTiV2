# Anforderungen

### Funktionale Anforderungen

**FA1:** Das System soll `GET /v2/rezept` bereitstellen, um alle neuen Rezepte eines bestimmten Typs abzurufen, beschränkt auf eine ARZ-Datenbank, unabhängig von der Apotheke (Priorität: M)

**FA2:** Das System soll `GET /v2/rezept/status` bereitstellen, um alle neuen Rezepte eines bestimmten Typs für eine Apotheke abzurufen, beschränkt auf eine ARZ-Datenbank (Priorität: M)

**FA3:** Das System soll `PATCH /v2/rezept/status` bereitstellen, um verschiedene Attribute eines Rezepts zu ändern, beschränkt auf eine ARZ-Datenbank, ausgewählt über Rezept-UUID (Priorität: M)

**FA4:** Das System soll `PATCH /v2/rezept/statusuuid` bereitstellen, um den Status mehrerer Rezepte inklusive zugehöriger Fehlermeldungen (Statusinfo) abzurufen, beschränkt auf eine ARZ-Datenbank, unabhängig von der Apotheke (Priorität: M)

**FA5:** Das System soll `PATCH /v2/rezept/status` für Bulk-Status-Updates bereitstellen, um den Rezeptstatus mehrerer Rezepte auf einmal zu ändern, beschränkt auf eine ARZ-Datenbank, ausgewählt über Rezept-UUIDs, um schnelle Statusänderungen während Abrechnungsoperationen auf `ABGERECHNET` zu ermöglichen (Priorität: M)

**FA6:** Das System soll zusätzliche Statusattribute einschließen: Fehler-Kenner aus der Prüfung (`regel_treffer_code`) und Prüfebene, in der der Fehler erkannt wurde (`check_level`) (Priorität: S)

**FA7:** Das System soll die Übertragung zusätzlicher Attribute pro Rezept unterstützen: Datum/Zeit der Einlieferung des Rezepts und AVS-Systemdaten (Software-Hersteller, -Name, -Version) (Priorität: S)

**FA8:** Das System soll die Änderung von E-Rezept-UUIDs ermöglichen, um neue Versionen des E-Rezepts bei Status-Info-Änderungen zu erzeugen (Priorität: S)

**FA9:** Das System soll zusätzliche Statusattribute einschließen: Status-Abfrage-Zeitstempel (`status_abfrage_datum`, `status_abfrage_zeit`) (Priorität: C)

**FA10:** Das System soll `GET /v2/apotheke` bereitstellen, um alle Apotheken eines ARZs aufzulisten (Priorität: C)

**FA11:** Das System soll zusätzliche Apothekenattribute einschließen: Sperrstatus, Login_Id und Freigabestatus, freigegebene APO_TI-Anwendungsfälle (Priorität: C)

**FA12:** Das System soll umfassende Apothekendaten bereitstellen: detaillierte Apothekenangaben einschließlich IK, Name, Adresse, Inhaber, Login-ID, Login-Passwort (Priorität: C)

**FA13:** Das System soll zusätzliche Apothekendaten verfolgen: Zeitstempel der ersten Datenübertragung und der letzten Datenübertragung (Priorität: C)

### Nicht-funktionale Anforderungen

**NFA1:** Es muss sehr großen Wert auf die Performance und Reaktionszeit der Schnittstelle gelegt werden

**NFA2:** Bei der Namensgebung der Endpunkte sollte im Vorfeld schon eine Versionierung mit einbezogen werden

**NFA3:** Es muss eine definierte Unterscheidung von Test-, Staging- und Live-Systemen vorhanden sein

**NFA4:** Jede neue Version muss aussagekräftig dokumentiert werden zu Neuerungen und Änderungen

**NFA5:** Die Entwicklung muss in C# .NET (.NET 8.0) erfolgen, um eine optimale Nutzbarkeit in den einzelnen ARZs zu gewährleisten

**NFA6:** ARZ_TI 2.0 sollte als OpenAPI 3.x (REST) Schnittstelle entwickelt werden

**NFA7:** Das System muss Datensätze von 1.000.000+ Rezeptdatensätzen mit optimierten Bulk-Operationen effizient verarbeiten

**NFA8:** API-Antwortzeiten müssen unter einer Sekunde für alle kritischen Rezeptoperationen liegen

**NFA9:** Das System muss Transaktionsintegrität bei allen Rezeptstatus-Updates gewährleisten

**NFA10:** Das System muss umfassende Audit-Protokolle für alle Rezeptzugriffe und -änderungen bereitstellen
