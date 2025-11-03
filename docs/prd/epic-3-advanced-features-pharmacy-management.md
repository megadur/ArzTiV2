# Epic 3: Advanced Features & Pharmacy Management

**Epic Goal:** Implement advanced prescription attributes, pharmacy management capabilities, and E-Rezept versioning to complete the comprehensive feature set defined in the requirements.

### Story 3.1: Enhanced Status Attributes and Error Tracking
As an ARZ system, I want detailed error information and additional status attributes, so that I can provide precise feedback on prescription validation and processing states.

**Acceptance Criteria:**
1. The `regel_treffer_code` and `check_level` attributes are included in prescription responses where applicable (Should-have).
2. The `status_abfrage_datum` and `status_abfrage_zeit` attributes are included to indicate when a status was queried (Could-have).
3. API responses are enhanced to include specific validation context for errors.
4. Backward compatibility with existing response formats is maintained.
5. API documentation is updated to define the new attributes.

### Story 3.2: Additional Prescription Attributes
As an ARZ system, I want to receive additional prescription attributes like delivery date and AVS system data, so that I can maintain a comprehensive audit trail.

**Acceptance Criteria:**
1. The prescription data model is extended to include the delivery date (`Einlieferungsdatum`) and AVS system information (Should-have).
2. This information is included in relevant API responses.
3. The system gracefully handles cases where this optional data is not available.

### Story 3.3: E-Rezept UUID Modification for Versioning
As an ARZ system, I want to modify an E-Rezept UUID, so that I can create a new version of a prescription when its status changes.

**Acceptance Criteria:**
1. A mechanism is implemented to change the UUID of an E-Rezept, effectively creating a new version (Should-have).
2. A clear version history is maintained for each prescription that undergoes a UUID change.
3. An audit trail is created for all UUID modifications.
4. The system ensures that any new UUID is unique.

### Story 3.4: Pharmacy Management API
As an ARZ administrator, I want to list and manage pharmacies, so that I can maintain pharmacy relationships effectively.

**Acceptance Criteria:**
1. A `GET /apotheke` endpoint is implemented to list all pharmacies associated with an ARZ (Could-have).
2. The list provides comprehensive details, including IK, name, address, and contact information.
3. The endpoint supports filtering and pagination.
4. The response includes pharmacy login credentials, permission status, and activity timestamps (first/last data transmission).
5. The system tracks the authorization status for the APO_TI use case and the overall pharmacy status (active/locked).
6. Further endpoints for "Erweiterte Apotheken-Verwaltung" (e.g., creating/updating logins) are considered for future implementation.

