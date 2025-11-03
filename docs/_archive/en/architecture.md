# ARZ_TI 3 Greenfield Architecture

## Table of Contents

- [Introduction](#introduction)
- [Enhancement Scope and Integration Strategy](#enhancement-scope-and-integration-strategy)
- [Tech Stack](#tech-stack)
- [Data Models and Schema Changes](#data-models-and-schema-changes)
- [Component Architecture](#component-architecture)
- [API Design and Endpoints](#api-design-and-endpoints)
- [Security Architecture](#security-architecture)
- [Deployment and Infrastructure](#deployment-and-infrastructure)
- [Testing Strategy](#testing-strategy)
- [Risk Assessment and Mitigation](#risk-assessment-and-mitigation)
- [Summary](#summary)

---

## Introduction

This document outlines the **greenfield architectural approach** for building a new high-performance ARZ_TI 3 prescription management API using the existing PostgreSQL database. The goal is to achieve 90% performance improvement (from 5-10s to <1s response times) while maintaining German pharmaceutical compliance and leveraging the existing comprehensive database schema.

**Greenfield Approach Rationale:**
Rather than enhancing the existing brownfield system, this greenfield approach allows us to build an entirely new API optimized for performance while integrating with the proven PostgreSQL database infrastructure.

### Project Overview
- **Architecture Type:** Greenfield API with Existing Database Integration
- **Performance Target:** 90% improvement (5-10s → <1s response times)
- **Database Strategy:** Leverage existing PostgreSQL schema with performance-optimized access patterns
- **Compliance Requirement:** Full German pharmaceutical standards (eMuster16, P-Rezept, E-Rezept)
- **Audience:** Closed internal pharmaceutical networks (Basic Auth sufficient)

### Existing Database Analysis

#### Current Database Infrastructure
- **Database Platform:** PostgreSQL with comprehensive prescription schema
- **Key Entities:** 20+ prescription-related tables (ErSenderezepteEmuster16, ErSenderezepteErezept, ErSenderezeptePrezept)
- **Multitenant Design:** Tenant-specific connection strings with ApoTi/ArzSw databases
- **Data Volume:** 1M+ prescription records requiring efficient handling
- **Regulatory Compliance:** Existing schema meets German pharmaceutical standards

#### Performance Bottleneck Analysis
- **Current Issue:** Entity Framework queries taking 5-10 seconds for 1000 records
- **Root Cause:** ORM overhead and non-optimized query patterns
- **Solution Approach:** Direct database access with hybrid ORM strategy

### Architecture Principles
1. **Performance-First Design:** Every component optimized for <1s response times
2. **Database Schema Preservation:** No changes to existing proven database structure
3. **Security Simplification:** Basic Auth optimized for closed pharmaceutical networks
4. **Regulatory Compliance:** Built-in German pharmaceutical compliance
5. **Scalability Foundation:** Support for 1M+ records with room for growth

### Change Log
| Change | Date | Version | Description | Author |
|--------|------|---------|-------------|--------|
| Initial Greenfield Architecture | 2025-10-19 | v1.0 | Complete new API architecture with existing database | Winston (Architect) |
| Security Architecture Update | 2025-10-19 | v1.1 | Updated to Basic Auth only for closed audience | Sarah (Product Owner) |

