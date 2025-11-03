# ARZ_TI 3 Performance Enhancement - Projekt-Brief

## Zusammenfassung

**Projektname:** ARZ_TI 3 Greenfield Performance Enhancement  
**Projekttyp:** Legacy API Modernisierung & Performance-Optimierung  
**Dauer:** Oktober 2025 (Architektur-Phase)  
**Status:** ✅ Architektur vollständig, bereit für Entwicklung  
**Geschäftlicher Impact:** 90% Performance-Verbesserung (5-10s → <1s Antwortzeiten)  

# Deutsche Dokumentation - ArzTiV2

## Navigation

- **[Epic 1 Zusammenfassung](epic-1-zusammenfassung.md)** - Deutsche Übersicht der Grundlagen-Infrastruktur
- **[Epic Roadmap Prognose](epic-roadmap-prognose.md)** - Vollständige 12-16 Wochen Projektplanung
- **[Projekt Brief](projekt-brief-neu.md)** - Ursprüngliche Projektanforderungen

---

## ARZ_TI Version 2.0

## Allgemeine und technische Vorbedingungen zur ARZ_TI Schnittstelle

 - Es muss sehr großen Wert auf die Performance und Reaktionszeit der Schnittstelle gelegt werden.
 - Bei der Namensgebung der Endpunkte sollte im Vorfeld schon eine Versionierung mit einbezogen werden
 - Es muss eine definierte Unterscheidung von Test-, Staging- und Live-Systemen vorhanden sein
 - Jede neue Version muss aussagekräftig dokumentiert werden zu Neuerungen und Änderungen.
 - Um eine optimale Nutzbarkeit in den einzelnen ARZs gewährleisten zu können sollte die Entwicklung in C# .Net (.Net 8.0) erfolgen
 - Die ARZ_TI 2.0 sollte als OpenAPI 3.x (REST) Schnittstelle entwickelt werden


#### Anmerkungen Felix

- obige Vorbedingungen kann ich unterstützen
- Performance/Reaktionszeit ist für uns nicht ganz so wichtig, ebenso ist mir .NET egal


<br>

## Neue und alte Funktionalitäten der ARZ_TI Schnittstelle

Die Prio bitte nach der MoSCoW(+) Methode angeben

<br>

 > &#128997; **Must have** (**M**): Diese Anforderungen sind ***unverzichtbar*** für das Projekt. Ohne sie kann das Projektziel nicht erreicht werden.

 > &#128999; **Should have** (**S**): Wichtig, aber ***nicht kritisch***; diese Anforderungen haben hohe Priorität, können aber bei Bedarf zurückgestellt werden.
 
 > &#128998; **Could have** (**C**): ***Wünschenswerte*** Anforderungen, die einen Mehrwert darstellen, aber nicht unbedingt erforderlich sind (Nice-to-have).

 > &#129002; **Won’t have** (**W**): Anforderungen, die für den aktuellen Projektzyklus nicht berücksichtigt werden, die aber möglicherweise in zukünftigen Phasen / Versionen in Betracht gezogen werden können.

 > &#129003; **Never have** (**N**): Anforderungen, die nicht berücksichtigt werden sollen. Diese Anforderungen werden nur der Vollständig halber angegeben um zu dokumentieren, dass diese bedacht wurden.

<br>

|Alt \| Neu|Funktion|Prio<br>M \| S \| C \| W \| N|Beschreibung der Anforderung bzw. Funktion|REST Endpunkt|
|:---:|---|:---:|---|---|
| A |Abruf aller neuen Rezepte| &#128997; **M** |Abruf aller neuen Rezepte mit oder ohne Daten eines Typs. <br>- Beschränkung auf eine ARZ Datenbank <br>- Unabhängig zur Apotheke|GET \| /rezept|
| A |Abruf aller neuen Rezepte mit oder ohne Dateneiner Apotheke| &#128997; **M** |Abruf aller neuen Rezepte mit oder ohne Daten eines Typs. <br>- Beschränkung auf eine ARZ Datenbank <br>- Unabhängig zur Apotheke|GET \| /rezept/status|
| A |Ändern eines Rezeptes| &#128997; **M** |Ändern verschiedener Attribute eines Rezeptes <br>- Beschränkung auf eine ARZ Datenbank <br>- Auswahl über Rezept UUID|PATCH \| /rezepte/{uuid}/status|
| A |Abruf des Status mehrerer Rezepte| &#128997; **M** |Abruf des Status mehrerer Rezepte inklusive der zugehörigen Fehlermeldungen (Statusinfo). <br>- Beschränkung auf eine ARZ Datenbank <br>- Auswahl über Rezept UUIDs im Body|POST \| /rezepte/status-bulk|
| **N**&#10024; |Zusätzliche Attribute zum Status eines Rezeptes| &#128999; **S** | - Statusinfo - Fehler-Kenner aus der Prüfung - `regel_treffer_code` <br>- Statusinfo - Prüfebene in der Fehler erkannt wurde - `check_level`| - |
| **N**&#10024; |Zusätzliche Attribute zum Status eines Rezeptes| &#128998; **C** | - Status - `status_abfrage_datum`, `status_abfrage_zeit`| - |
| A |Bulk-Änderung: Status auf 'ABGERECHNET'| &#128997; **M** |Ändern des Rezept-Status mehrerer Rezepte auf `ABGERECHNET`. <br>- Beschränkung auf eine ARZ Datenbank <br>- Auswahl über Rezept UUIDs <br>- Dedizierter Endpunkt für den Abrechnungslauf|POST \| /rezepte/bulk/mark-as-billed|
| | | | | |
| **N**&#10024; |Übertragung weitere Attribute pro Rezept| &#128999; **S** | - Datum/Zeit der Einlieferung des Rezepts (nur letzte Lieferung möglich)<br>- Daten zum AVS System (Software-Hersteller, -Name, -Version)| - |
| **N**&#10024; |Ändern einer E-Rezept UUID| &#128999; **S** | - Ändern einer E-Rezept UUID um eine neue Version des E-Rezepts bei Status-Info Änderungen zu erzeugen. | - |
| | | | | |
| A |Liste aller Apotheken eines ARZs| &#128998; **C** | - Liefert eine Liste aller Apotheken eines ARZs| GET \| /apotheke|
| **N**&#10024; |Zusätzliche Attribute zu einer Apotheke| &#128998; **C** | - gesperrt (Apotheke ist gesperrt T\|F) <br>- Login_Id und freigegeben <br>- freigegebene APO_TI Usecase| - |
| **N**&#10024; |Apotheke neu anlegen| &#128998; **C** | - Apotheke mit umfangreichen Attributen neu anlegen <br>- Fehlermeldung, wenn eine Apotheke mit dieser IK-Nr. schon existiert| - |
| **N**&#10024; |Apotheke aktualisieren| &#128998; **C** | - Aktualisieren aller Attributen einer Apotheke| - |
| **N**&#10024; |Login zu einer Apotheke neu anlegen| &#128998; **C** | - Login zu einer Apotheke mit umfangreichen Attributen neu anlegen <br>- Inklusive der Angaben für welchen APO_TI Usecase die Apotheke freigeschaltet ist| - |
| **N**&#10024; |Login zu einer Apotheke aktualisieren| &#128998; **C** | - Aktualisieren aller Attribute zum Login einer Apotheke <br>- Inklusive der Angaben für welchen APO_TI Usecase die Apotheke freigeschaltet ist <br>- Neu setzen eines Login-Passworts| - |
| A |Umfangreiche Daten zu einer Apotheke| &#128998; **C** | - Sehr detaillierte Angaben zu einer Apotheke <br>- IK, Name, Adresse, Inhaber, Login-ID, Login-Passwort| - |
| **N**&#10024; |Zusätzliche Daten zu einer Apotheke| &#128998; **C** | - Wann war die erste Datenübertragung <br> - Wann war die letzte Datenübertragung | - |
| | | | | |
| **N**&#10024; |Löschen einer Apotheke| &#129003; **N** | - Eine Apotheke zu der Rezepte vorhanden sind darf nicht gelöscht werden| - |

# Allgemeine Funktionalitäten
- Mandantenfähigkeit
  - Mögliche Umsetzung <br>- Über Logindaten (identisch zur aktuellen Version) <br>- Oder, über einen Mandanten-Key bzw. API-Key der dann über die DB arzsw_db (Tabellen arzsw_mandant, arzsw_benutzer, arzsw_datenbank) die notwendigen Zugriffsdaten ermittelt
- Health-Endpunkt
  - unbedingt notwendig - evtl. mit Rückgabe der Version, ...
- Mengengerüst
- Verfügbarkeit

#### Anmerkungen Felix

- Einfache Möglichkeit, einen lokalen ArzTI-Server zu starten ohne große Anforderungen an die Umgebung (psql-DB + Linux?) für Testing bzw. eigene CI-Pipeline
- jeder API-Endpunkt macht eine Sache, Felder der Server-Antwort sind immer gleich, unabhängig von den  Eingabeparametern (Beschleunigungen oder Optimierungen ggf. anderer Endpunkt)
- Es wäre hilfreich, wenn die E-Rezept-ID immer in der Antwort enthalten wäre, selbst bei Verwendung von UUIDs
- Der ArzTI-Server sollte nie raten oder unerwartete/unsinnige Eingabeparameter ignorieren, sondern bei jeder Unstimmigkeit einen Fehlercode (Status 400/500) zurückgegeben ("immer schnell und laut schreien statt ignorieren"). Strikte Prüfung aller Eingabeparameter.
- Es wäre schön, wenn der Server immer sicherstellt, dass alle in der Anfrage angegebenen UUIDs (und nur die!) auch in der Antwort auftauchen (sonst Fehler!)

---

## Use Cases Felix

🟥 M:
- Abruf von Rezepten, ggf. gefiltert nach Typ, ARZ-Status, Rezeptstatus, Apotheke (ggf. Proxy IK): `GET /rezept/StatusUUId`
- Setze nur ARZ-Status von Rezepten über UUID
- Setze nur REZ-Status (+ ggf. Fehlerinformation) über UUID
- Download E-Rezeptdaten (mehrere) über UUID

🟦 C:
- Möglichkeit, ArzTI-UUID(s) über E-Rezept-ID zu ermitteln (Testing, Wawi schickt Test-Rezept mit bekannter E-Rezept-ID)
- Aufräumen der Datenbank über ArzTI

## Technische Architektur-Übersicht

### Technology Stack 
```
Runtime:              .NET 8.0 LTS (Neueste Performance-Optimierungen)
API Framework:        ASP.NET Core Minimal APIs (30-40% schneller als Controller)
Architektur-Pattern:  Clean Architecture
Datenzugriff:         EF Core (Bestehende v2-Infrastruktur wird genutzt)
Datenbank:            PostgreSQL (Bestehende Schema beibehalten)
Caching-Strategie:    Selektives In-Memory-Caching für Stammdaten
Authentifizierung:    Basic Auth (Optimiert für geschlossene Netzwerke)
Deployment:           manuell, später Container-First mit Docker + Kubernetes
```

### Performance Engineering Ansatz
- **Ziel-Metriken:** <5 Sekunden Antwortzeit für 1000+ Datensätze
- **Datenbank-Optimierung:** Optimierte EF Core-Abfragen und gezielte Index-Erstellung
- **Connection Management:** Mandanten-spezifische Connection-Pools
- **Monitoring:** Real-time Performance-Metriken mit Application Insights (?), Health, Version, BuildID

### Sicherheits-Architektur
- **Basic Authentication:** Vereinfacht für geschlossene pharmazeutische Netzwerke
- **Mandanten-Isolation:** Komplette Trennung zwischen ARZ-Systemen
- **Compliance:** (?)
- **Netzwerk-Sicherheit:** TLS 1.3, Netzwerksegemtierung

## Offene Fragen 

### 1. Datenbank & Performance
#### Mengengerüst (Skalierungsanforderungen)
```
FRAGE: Welche Datenmengen?
├── Anzahl Rezepte pro 1.000/Min mit Daten Tag/Monat/Jahr?
├── Wartungsfenster 3Uhr-6Uhr, Abrechnungsperiode ist kritisch
├── Anzahl gleichzeitiger ARZs ca. 10 (Concurrent Users)?
├── Anzahl Apotheken insgesamtgesamt 300-400?
├── Durchschnittliche Anzahl Rezepte pro Tag - 17.000?
├── Peak-Zeiten: Wann sind die höchsten Lasten?
└── Wachstumserwartung für nächste 3-5 Jahre?
```

#### Performance-Ziele (konkrete SLAs)
```
FRAGE: Was sind die  konkreten Performance-Anforderungen?
├── Ziel-Antwortzeit für einzelne Rezept-Abfrage?
├── Ziel-Antwortzeit für Bulk-Operationen (1000+ Rezepte)?
├── Akzeptable Downtime pro Monat?
├── Verfügbarkeits-SLA (99.9%, 99.95%, 99.99%)?
├── Disaster Recovery Anforderungen (RTO/RPO)?
└── Load Testing Szenarien definieren?

```

### 2. Security & Compliance
#### Sicherheitsanforderungen
```
FRAGE: Welche Sicherheits-Compliance ist erforderlich?
├── DSGVO-Anforderungen: Gibt es  persönlichen Daten?
├── Audit-Anforderungen: Welche Logs müssen gespeichert werden? pro Arz, detaillierte Fehlermeldung
├── Verschlüsselung: TLS 1.3 ausreichend oder zusätzliche Encryption?
└── Netzwerk-Security: VPN, Firewall,
```

### 3. Integration & Daten
#### Client-Integration
```
FRAGE: Wie sollen Clients die API verwenden?
├── Welche Client-Technologien (C#, PHP, Python, JavaScript)?
├── Authentication: Basic Auth ausreichend oder OAuth2 erforderlich?
└── API-Versioning: Wie handhaben wir Breaking Changes?
```

### 4. Operations & Deployment
#### Hosting & Infrastructure
```
FRAGE: Wo soll das System gehostet werden?
├── On-Premise: IIS, Kestrel, Nginx?
├── Container-Platform (Kubernetes, Docker Swarm, Plain Docker)?
└── Monitoring: Welche Tools verwenden wir bereits?
```

#### DevOps & Maintenance
```
FRAGE: Wie soll das DevOps-Setup aussehen?
├── ZIP-Archive: Bisher wurden ZIP Dateien kopiert und im IIS referenziert?
├── CI/CD-Pipeline: GitLab, GitHub Actions, Azure DevOps?
├── Monitoring: Prometheus, Application Insights, Datadog?
├── Log Management: ELK Stack, Splunk, Azure Monitor?
├── Alerting: Teams, Email?
└── Maintenance Windows: Wann können Updates deployed werden?
```

## PROJEKT-RISIKO FRAGEN

### 5. Organisatorische Risiken
#### Stakeholder & Entscheidungen
```
FRAGE: Wer sind die Key-Stakeholder?
├── Wie erfolgt die Abnahme?
├── Change Request Process etabliert?
└── Testing/QA Team verfügbar?
```

#### Timeline & Dependencies
```
FRAGE: Gibt es externe Dependencies?
├── Go-Live Deadline: Hart oder flexibel?
├── Abhängigkeiten zu anderen Projekten?
├── Regulatory Deadlines (Gesetzesänderungen)?
├── Parallel laufende Projekte mit Ressourcen-Overlap?
├── Urlaubs-/Feiertage die Timeline beeinflussen?
└── Pilot-Phase vor Production-Rollout geplant?
```

### 6. Budget & Vertrag
#### Finanzierung
```
FRAGE: Wie ist das Budget strukturiert?
├── Fixed Price oder Time & Material?
├── Change Request Budget reserviert?
├── Lizenz-Kosten für Tools: keine?
├── Support & Maintenance Budget post-Go-Live?
└── Penalty Clauses für Delays definiert?
```

## NICE-TO-KNOW FRAGEN (für Optimierung)

### 7. Zukünftige Anforderungen
```
FRAGE: Welche zukünftigen Erweiterungen sind geplant?
├── Neue API-Endpoints in nächsten 12 Monaten?
├── Integration mit anderen Systemen geplant?
├── Analytics/Reporting Requirements?
├── Machine Learning/AI Use Cases?
└── Multi-Tenancy für andere ARZs?
```

