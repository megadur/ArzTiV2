# ArzTI v3 - Korrigierter Architektur-Kontext

## Gesamtsystem-Überblick (ArzSW Proxy)

### **Vollständiger Datenfluss:**
```
1. Apotheken senden Rezepte → ApoTI API (bestehend)
2. ApoTI speichert in mandantenspezifische PostgreSQL DBs
3. ARZ Rechenzentren fragen Rezepte ab → ArzTI API (UNSER PROJEKT)
4. ARZ führt Kassenabrechnung durch (extern)
5. ARZ sendet Status-Updates zurück → ArzTI API (UNSER PROJEKT)
6. Status wird in mandantenspezifischen DBs aktualisiert
```

### **Multi-Tenant Architektur:**
```
Mandant = ARZ Rechenzentrum
├── ARZ Nord → eigene PostgreSQL DB
├── ARZ Süd → eigene PostgreSQL DB  
├── ARZ West → eigene PostgreSQL DB
└── ARZ Ost → eigene PostgreSQL DB

ArzTI API muss:
├── Mandanten-Erkennung (Tenant Identification)
├── Database-Routing pro Mandant
├── Mandanten-Isolation (Security)
└── Mandanten-spezifische Konfiguration
```

## Projekt-Scope: NUR ArzTI API v3

### **WAS wir entwickeln:**
```
ArzTI API v3 (Greenfield):
├── GET /v3/rezepte - Rezept-Abruf für ARZ
├── PATCH /v3/rezepte/status - Status-Updates von ARZ
├── GET /v3/apotheken - Apotheken-Informationen
├── Mandanten-Management (ARZ Identification)
├── Performance-Optimierung (90% Verbesserung)
└── Basic Authentication (geschlossenes Netzwerk)
```

### **WAS bereits existiert (nicht unser Scope):**
```
Bestehende Komponenten:
├── ApoTI API (Apotheken → System)
├── PostgreSQL Databases (mandantenspezifisch)
├── Rezept-Daten (bereits vorhanden)
├── Apotheken-Stammdaten (bereits vorhanden)
└── Legacy ArzTI v1/v2 (wird ersetzt)
```

## Multi-Tenancy Implications für ArzTI v3

### **Tenant-Identification Strategy:**
```
Optionen für ARZ-Erkennung:
├── Subdomain: arz-nord.arzti.com/v3/rezepte
├── Header: X-Tenant-Id: ARZ_NORD
├── URL Path: /v3/arz-nord/rezepte
└── Basic Auth Username: user@arz-nord
```

### **Database-Routing:**
```
Pro ARZ-Request:
├── Tenant-ID extrahieren
├── Entsprechende DB-Connection auswählen
├── Mandanten-spezifische Queries
└── Response ohne Cross-Tenant Data Leaks
```

### **Performance-Herausforderungen Multi-Tenant:**
```
Complexity-Faktoren:
├── Connection Pooling pro Mandant
├── Caching-Strategien pro Tenant
├── Bulk-Operations nur innerhalb Mandant
├── Performance-Monitoring pro ARZ
└── Scaling bei unterschiedlichen ARZ-Größen
```

## Korrigierte Architektur-Entscheidungen

### **Database Access Pattern:**
```
VORHER (falsche Annahme):
- Eine zentrale DB mit allen Daten
- Einfache Repository Pattern

JETZT (korrekt):
- Multi-Tenant DB Strategy erforderlich
- Tenant-Aware Repository Pattern
- Database-Routing Middleware
- Mandanten-Isolation kritisch
```

### **Caching Strategy (angepasst):**
```
Multi-Tenant Caching:
├── L1 Cache: Memory (tenant-segregated)
├── L2 Cache: Redis (tenant-keyed)
├── L3 Cache: Database (bereits mandantenspezifisch)
└── Cache-Invalidation pro Tenant
```

### **Security Considerations (erweitert):**
```
Mandanten-Isolation:
├── Tenant-ID Validation bei jedem Request
├── Database-Connection pro Mandant
├── Audit-Logs mandantenspezifisch
├── Rate-Limiting pro ARZ
└── Monitoring/Alerting pro Tenant
```

## Auswirkungen auf Entwicklungsaufwand

### **Zusätzliche Komplexität:**
```
Multi-Tenancy Features:
├── Tenant-Resolution Middleware (+20 Stunden)
├── Multi-DB Connection Factory (+15 Stunden)
├── Tenant-Aware Repository Pattern (+25 Stunden)
├── Mandanten-spezifische Konfiguration (+10 Stunden)
├── Cross-Tenant Security Testing (+15 Stunden)
└── Tenant-Monitoring Setup (+10 Stunden)

Zusätzlicher Aufwand: ~95 Stunden (2.5 Wochen)
```

### **Korrigierte Gesamtschätzung:**
```
Original KI-optimierte Schätzung: 305 Stunden
Multi-Tenancy Overhead:           +95 Stunden
Überarbeitete Base-Entwicklung:   400 Stunden

Mit Risk Buffer (25%):            500 Stunden
Timeline:                         12-13 Wochen
Kosten:                          €62,000 (statt €55,600)
```

## Neue kritische Fragen für Client

### **Multi-Tenancy spezifisch:**
```
FRAGE: Wie ist die ARZ-Mandanten-Struktur?
├── Wie viele ARZ Rechenzentren insgesamt?
├── Unterschiedliche Datenmengen pro ARZ?
├── Separate DB-Server pro ARZ oder Schema-Trennung?
├── ARZ-Identifikation: Wie sollen wir ARZ erkennen?
├── Cross-ARZ Operationen: Jemals erforderlich?
└── ARZ-spezifische Konfigurationen/Features?

FRAGE: Database-Setup Details?
├── Ein PostgreSQL-Server mit mehreren DBs?
├── Separate PostgreSQL-Instanzen pro ARZ?
├── Connection-String Patterns pro Mandant?
├── Database-Namenskonventionen?
└── Mandanten-Migration: Neue ARZ hinzufügen?
```

## Competitive Advantage durch Multi-Tenancy

### **Positioning gegenüber Kunde:**
```
Warum Multi-Tenant-Expertise wertvoll ist:
├── Skalierung: Einfaches Hinzufügen neuer ARZ
├── Isolation: Garantierte Datentrennung
├── Performance: Optimierung pro Mandant möglich
├── Compliance: Mandantenspezifische Audit-Trails
└── Cost-Efficiency: Shared Infrastructure bei Isolation
```

**WICHTIGE ERKENNTNIS: Multi-Tenancy macht das Projekt anspruchsvoller, aber auch wertvoller für den Kunden!** ���
