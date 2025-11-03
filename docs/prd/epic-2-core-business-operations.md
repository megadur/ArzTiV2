# Epic 2: Core Business Operations

**Epic Goal:** Implement comprehensive prescription status management including bulk operations, detailed error reporting, and status update capabilities. This epic delivers the core business functionality that enables ARZ systems to manage prescription lifecycles efficiently.

### Story 2.1: Pharmacy-Specific Prescription Status Retrieval
As an ARZ system, I want to retrieve the status of new prescriptions for a specific pharmacy, so that I can handle pharmacy-specific processing workflows.

**Acceptance Criteria:**
1. `GET /rezept/status` endpoint implemented.
2. Pharmacy-specific filtering via an optional pharmacy ID parameter.
3. If no pharmacy ID is provided, the context is resolved from the authenticated tenant.
4. The query is optimized for efficient, pharmacy-specific data access.
5. The response includes all necessary prescription metadata relevant to the pharmacy.

### Story 2.2: Individual Prescription Status Updates
As an ARZ system, I want to update the status of a single prescription, so that I can modify its state during processing.

**Acceptance Criteria:**
1. `PATCH /rezepte/{uuid}/status` endpoint for single prescription updates.
2. The prescription's UUID is passed in the URL path.
3. The endpoint supports updating the main status (e.g., `ABGELEHNT`) and optionally accepts status information fields (`regel_treffer_code`, `check_level`) in the request body.
4. Ownership of the prescription is validated against the requesting tenant.
5. A detailed audit trail is created for all modifications.
6. Updates are transactional with rollback capability on failure.
7. The response clearly indicates success or failure, providing reasons for any errors.

### Story 2.3: Bulk Status Retrieval by UUID
As an ARZ system, I want to get status information for multiple prescriptions by their UUIDs, so that I can efficiently track their states and handle any failures.

**Acceptance Criteria:**
1. `POST /rezepte/status-bulk` endpoint implemented.
2. Accepts a list of prescription UUIDs in the request body.
3. Returns detailed status information for each requested prescription.
4. Includes error details (`regel_treffer_code`, `check_level`) when available.
5. Handles partial success scenarios gracefully (e.g., some UUIDs found, others not).
6. The query is performance-optimized for bulk lookups (up to 1000 UUIDs).
7. Provides clear error messages for UUIDs that are not found or are inaccessible to the tenant.

### Story 2.4: Bulk Status Update to 'Billed'
As an ARZ system, I want to update the status of multiple prescriptions to `ABGERECHNET` at once, so that I can efficiently mark them as processed during the billing run.

**Acceptance Criteria:**
1. `POST /rezepte/bulk/mark-as-billed` endpoint for the specific bulk update.
2. Accepts an array of prescription UUIDs in the request body.
3. The operation sets the status of all specified prescriptions to `ABGERECHNET`.
4. The entire operation is atomic: all updates succeed, or all fail and are rolled back.
5. The response provides a detailed summary, including success/failure counts and specific error messages.
6. The endpoint is performance-optimized for high-volume billing scenarios.
7. Detailed audit logs are generated for all status changes.

