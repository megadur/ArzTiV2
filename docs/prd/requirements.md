# Requirements

## Functional Requirements

**FR1:** The system shall provide `GET /v2/rezept` to retrieve all new prescriptions of a specific type, limited to one ARZ database, independent of pharmacy (Priority: M)

**FR2:** The system shall provide `GET /v2/rezept/status` to retrieve all new prescriptions of a specific type for a pharmacy, limited to one ARZ database (Priority: M)

**FR3:** The system shall provide `PATCH /v2/rezept/status` to change various attributes of a prescription, limited to one ARZ database, selected by prescription UUID (Priority: M)

**FR4:** The system shall provide `PATCH /v2/rezept/statusuuid` to retrieve the status of multiple prescriptions including associated error messages (status info), limited to one ARZ database, independent of pharmacy (Priority: M)

**FR5:** The system shall provide `PATCH /v2/rezept/status` for bulk status updates to change the prescription status of multiple prescriptions at once, limited to one ARZ database, selected by prescription UUIDs, to enable fast status changes during billing operations to `ABGERECHNET` (Priority: M)

**FR6:** The system shall include additional status attributes: error identifier from validation (`regel_treffer_code`) and validation level where error was detected (`check_level`) (Priority: S)

**FR7:** The system shall support transmission of additional attributes per prescription: date/time of prescription submission and AVS system data (software manufacturer, name, version) (Priority: S)

**FR8:** The system shall enable modification of e-prescription UUIDs to create new versions of e-prescriptions when status info changes (Priority: S)

**FR9:** The system shall include additional status attributes: status query timestamps (`status_abfrage_datum`, `status_abfrage_zeit`) (Priority: C)

**FR10:** The system shall provide `GET /v2/apotheke` to list all pharmacies of an ARZ (Priority: C)

**FR11:** The system shall include additional pharmacy attributes: block status, Login_Id and release status, approved APO_TI use cases (Priority: C)

**FR12:** The system shall provide comprehensive pharmacy data: detailed pharmacy information including IK, name, address, owner, login ID, login password (Priority: C)

**FR13:** The system shall track additional pharmacy data: timestamps of first data transmission and last data transmission (Priority: C)

## Non-Functional Requirements

**NFR1:** Great emphasis must be placed on the performance and response time of the interface

**NFR2:** Endpoint naming should include versioning from the outset

**NFR3:** There must be a defined distinction between test, staging, and live systems

**NFR4:** Every new version must be meaningfully documented regarding new features and changes

**NFR5:** Development must be done in C# .NET (.NET 8.0) to ensure optimal usability in individual ARZ systems

**NFR6:** ARZ_TI 2.0 should be developed as an OpenAPI 3.x (REST) interface

**NFR7:** The system must efficiently process datasets of 1,000,000+ prescription records with optimized bulk operations

**NFR8:** API response times must be under one second for all critical prescription operations

**NFR9:** The system must ensure transactional integrity for all prescription status updates

**NFR10:** The system must provide comprehensive audit logs for all prescription access and modifications

## Priority Legend
- **M** = Must have (Critical)
- **S** = Should have (Important)
- **C** = Could have (Nice to have)
