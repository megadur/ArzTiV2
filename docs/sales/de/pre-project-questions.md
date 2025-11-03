# ARZ_TI 3 - Wichtige Vorprojekt-Fragen

## ��� **KRITISCHE FRAGEN (Must-have vor Vertragsabschluss)**

### **1. Datenbank & Performance**
#### **Mengengerüst (Skalierungsanforderungen)**
```
FRAGE: Welche Datenmengen erwarten Sie?
├── Anzahl Rezepte pro Tag/Monat/Jahr?
├── Anzahl gleichzeitiger Benutzer (Concurrent Users)?
├── Anzahl Apotheken pro ARZ?
├── Durchschnittliche Anzahl Rezepte pro Apotheke?
├── Peak-Zeiten: Wann sind die höchsten Lasten?
└── Wachstumserwartung für nächste 3-5 Jahre?

WARUM KRITISCH:
- Bestimmt Caching-Strategie
- Beeinflusst Database-Sharding Entscheidungen
- Definiert Hardware-Requirements
- Kostenauswirkung auf Cloud-Infrastructure
```

#### **Performance-Ziele (konkrete SLAs)**
```
FRAGE: Was sind Ihre konkreten Performance-Anforderungen?
├── Ziel-Antwortzeit für einzelne Rezept-Abfrage?
├── Ziel-Antwortzeit für Bulk-Operationen (1000+ Rezepte)?
├── Akzeptable Downtime pro Monat?
├── Verfügbarkeits-SLA (99.9%, 99.95%, 99.99%)?
├── Disaster Recovery Anforderungen (RTO/RPO)?
└── Load Testing Szenarien definieren?

WARUM KRITISCH:
- Definiert Architektur-Komplexität
- Beeinflusst Infrastruktur-Kosten erheblich
- Bestimmt Monitoring/Alerting Requirements
- Rechtliche SLA-Verpflichtungen
```

### **2. Security & Compliance**
#### **Sicherheitsanforderungen**
```
FRAGE: Welche Sicherheits-Compliance ist erforderlich?
├── DSGVO-Anforderungen: Welche persönlichen Daten?
├── Pharma-Compliance: eMuster16, E-Rezept Standards?
├── Audit-Anforderungen: Welche Logs müssen gespeichert werden?
├── Datenschutz: Wie lange werden Daten aufbewahrt?
├── Verschlüsselung: TLS 1.3 ausreichend oder zusätzliche Encryption?
└── Netzwerk-Security: VPN, Firewall, IP-Whitelisting?

WARUM KRITISCH:
- Compliance-Verletzungen = Projektrisiko
- Beeinflusst Datenmodell (Anonymisierung, Retention)
- Bestimmt Logging/Audit-Architektur
- Rechtliche Haftungsrisiken
```

#### **Netzwerk & Zugriff**
```
FRAGE: Wie ist die Netzwerk-Architektur?
├── Internet-Zugriff oder private Netzwerke?
├── VPN-Verbindungen zwischen ARZ und Apotheken?
├── Firewall-Regeln: Welche Ports/Protokolle erlaubt?
├── SSL-Zertifikate: Self-signed oder CA-signed?
├── Load Balancer: Vorhanden oder Teil der Lösung?
└── CDN-Requirements für Performance?

WARUM KRITISCH:
- Bestimmt Deployment-Architektur
- Beeinflusst SSL/TLS-Implementation
- Network-Latency Auswirkungen auf Performance
- Kostenauswirkung Infrastructure
```

### **3. Integration & Daten**
#### **Legacy System Details**
```
FRAGE: Wie sieht die aktuelle Datenstruktur aus?
├── PostgreSQL Schema: Können Sie ERD bereitstellen?
├── Datenqualität: Gibt es bekannte Probleme?
├── Migration: Müssen Altdaten bereinigt werden?
├── Referenzdaten: Welche Master-Data gibt es?
├── Business Rules: Welche Validierungen sind kritisch?
└── Daten-Ownership: Wer ist für Datenqualität verantwortlich?

WARUM KRITISCH:
- Unbekannte Datenprobleme = Projektrisiko
- Komplexe Migrationen kosten Zeit/Geld
- Datenmodell-Anpassungen können aufwändig werden
- Hidden Complexity in Legacy-Logik
```

#### **Client-Integration**
```
FRAGE: Wie sollen Clients die API verwenden?
├── Welche Client-Technologien (C#, PHP, Python, JavaScript)?
├── Authentication: Basic Auth ausreichend oder OAuth2 erforderlich?
├── API-Versioning: Wie handhaben Sie Breaking Changes?
├── Rate Limiting: Benötigt für DoS-Schutz?
├── Offline-Capabilities: Müssen Clients offline arbeiten können?
└── Real-time Updates: WebSockets oder Polling?

WARUM KRITISCH:
- Client-Requirements bestimmen API-Design
- Breaking Changes = Support-Aufwand
- Rate Limiting beeinflusst Architektur
- Real-time Features = zusätzliche Komplexität
```

### **4. Operations & Deployment**
#### **Hosting & Infrastructure**
```
FRAGE: Wo soll das System gehostet werden?
├── Cloud (AWS, Azure, GCP) oder On-Premise?
├── Container-Platform (Kubernetes, Docker Swarm, Plain Docker)?
├── Database-Hosting: Managed Service oder selbst verwaltet?
├── Monitoring: Welche Tools verwenden Sie bereits?
├── Backup-Strategie: Wie oft, wie lange aufbewahren?
└── Disaster Recovery: Geographical Redundancy erforderlich?

WARUM KRITISCH:
- Hosting-Kosten können Projekt-Budget sprengen
- Platform-Choice beeinflusst Deployment-Complexity
- Managed Services vs. Self-hosting Aufwand
- Compliance-Anforderungen für Hosting-Location
```

#### **DevOps & Maintenance**
```
FRAGE: Wie ist Ihr DevOps-Setup?
├── CI/CD-Pipeline: GitLab, GitHub Actions, Azure DevOps?
├── Infrastructure as Code: Terraform, ARM Templates?
├── Monitoring: Prometheus, Application Insights, Datadog?
├── Log Management: ELK Stack, Splunk, Azure Monitor?
├── Alerting: PagerDuty, Teams, Email?
└── Maintenance Windows: Wann können Updates deployed werden?

WARUM KRITISCH:
- DevOps-Komplexität beeinflusst Kosten
- Monitoring-Requirements definieren Architektur
- Maintenance-Windows beeinflussen Deployment-Strategie
- Tool-Integration kann aufwändig werden
```

## ⚠️ **PROJEKT-RISIKO FRAGEN**

### **5. Organisatorische Risiken**
#### **Stakeholder & Entscheidungen**
```
FRAGE: Wer sind die Key-Stakeholder?
├── Wer hat finale Abnahme-Autorität?
├── Technical Decision Maker identifiziert?
├── Budget-Approval Process definiert?
├── Change Request Process etabliert?
├── Testing/QA Team verfügbar?
└── End-User Training wer übernimmt?

WARUM KRITISCH:
- Unklare Entscheidungswege = Projektverzögerung
- Missing Stakeholder Buy-in = Scope Creep
- Keine QA-Resources = Qualitätsprobleme
- Training-Aufwand oft unterschätzt
```

#### **Timeline & Dependencies**
```
FRAGE: Gibt es externe Dependencies?
├── Go-Live Deadline: Hart oder flexibel?
├── Abhängigkeiten zu anderen Projekten?
├── Regulatory Deadlines (Gesetzesänderungen)?
├── Parallel laufende Projekte mit Ressourcen-Overlap?
├── Urlaubs-/Feiertage die Timeline beeinflussen?
└── Pilot-Phase vor Production-Rollout geplant?

WARUM KRITISCH:
- Harte Deadlines = Qualitäts-Risiko
- Dependencies können Projekt blockieren
- Ressourcen-Konflikte = Verzögerungen
- Regulatory Changes = Scope Changes
```

### **6. Budget & Vertrag**
#### **Financial Constraints**
```
FRAGE: Wie ist das Budget strukturiert?
├── Fixed Price oder Time & Material?
├── Change Request Budget reserviert?
├── Infrastructure-Kosten: Kunde oder Provider?
├── Lizenz-Kosten für Tools: Wer trägt sie?
├── Support & Maintenance Budget post-Go-Live?
└── Penalty Clauses für Delays definiert?

WARUM KRITISCH:
- Fixed Price = Scope-Risiko für Provider
- Infrastructure-Kosten können explodieren
- Tool-Lizenzen oft vergessen
- Support-Kosten unterschätzt
```

## ��� **NICE-TO-KNOW FRAGEN (für Optimierung)**

### **7. Future Requirements**
```
FRAGE: Welche zukünftigen Erweiterungen sind geplant?
├── Neue API-Endpoints in nächsten 12 Monaten?
├── Integration mit anderen Systemen geplant?
├── Mobile App Development geplant?
├── Analytics/Reporting Requirements?
├── Machine Learning/AI Use Cases?
└── Multi-Tenancy für andere ARZs?

WARUM NÜTZLICH:
- Future-proofing Architecture
- Vermeidung teurer Refactoring
- Competitive Advantage durch Vorbereitung
```

### **8. Team & Skills**
```
FRAGE: Welche internen Ressourcen sind verfügbar?
├── .NET Developers für Knowledge Transfer?
├── Database Administrators verfügbar?
├── DevOps Engineers für Infrastructure?
├── QA/Testing Team für User Acceptance Testing?
├── Technical Writers für Documentation?
└── Support Team für Post-Go-Live Issues?

WARUM NÜTZLICH:
- Knowledge Transfer Planung
- Skill Gap Identification
- Support Model Definition
```

## ��� **EMPFOHLENER FRAGENKATALOG für Client-Meeting**

### **Priorität 1: Vor Kostenvoranschlag (Diese Woche)**
1. **Mengengerüst**: Rezepte/Tag, Concurrent Users, Growth
2. **Performance-SLAs**: Konkrete Antwortzeiten, Verfügbarkeit
3. **Security**: DSGVO, Pharma-Compliance, Audit-Requirements
4. **Hosting**: Cloud vs. On-Premise, Infrastructure-Budget
5. **Timeline**: Go-Live Deadline, Dependencies, Flexibilität

### **Priorität 2: Vor Projektstart**
6. **Database Schema**: ERD, Datenqualität, Migration-Effort
7. **Client Integration**: Technologies, Auth-Requirements
8. **DevOps**: CI/CD, Monitoring, Deployment-Process
9. **Stakeholder**: Decision Makers, Approval Process
10. **Budget**: Fixed vs. T&M, Change Request Process

### **Priorität 3: Nice-to-know**
11. **Future Plans**: Roadmap, Extensions, Integrations
12. **Team**: Internal Resources, Skills, Support

**EMPFEHLUNG: Planen Sie 2-3 Stunden Workshop für Priorität 1+2 Fragen!** ���
