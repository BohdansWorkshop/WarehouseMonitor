# 📦 WarehouseMonitor

[![.NET Core](https://img.shields.io/badge/.NET-8.0-blueviolet.svg)](https://dotnet.microsoft.com/download)
[![Architecture](https://img.shields.io/badge/Architecture-Clean-green.svg)](#-architecture)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

**WarehouseMonitor** is a comprehensive Warehouse Management System (WMS) built with a focus on enterprise-grade standards: high maintainability, scalability, and strict architectural integrity.

---

## 🎯 Project Purpose
This repository serves as a technical bridge, demonstrating the application of high-level engineering principles.

---

## 🏗️ Architecture

The project strictly follows **Clean Architecture** (Onion Architecture) principles to ensure the core business logic remains decoupled from external dependencies and infrastructure.

* **Domain Layer:** Enterprise logic and rich domain models (DDD approach).
* **Application Layer:** Implements **CQRS** via **MediatR**. Business flows are handled by decoupled Command/Query handlers.
* **Infrastructure Layer:** Data persistence using **Entity Framework Core** and external service integrations.
* **WebUI (API):** Clean RESTful API with automated documentation and centralized error handling.

---

## 🛠️ Technology Stack

| Component | Technology |
| :--- | :--- |
| **Framework** | .NET 8 (LTS) |
| **Data Access** | Entity Framework Core (Code First) |
| **Database** | MS SQL Server / PostgreSQL |
| **Communication** | MediatR (CQRS Pattern) |
| **Validation** | FluentValidation (via MediatR Pipeline) |
| **Testing** | xUnit, Moq, FluentAssertions |
| **API Documentation** | Swagger / OpenAPI |

---

## ✨ Key Technical Highlights

- ✅ **MediatR Pipeline Behaviors:** Centralized validation and logging without polluting controllers or handlers.
- ✅ **Global Exception Handling:** Middleware-based error management for system-wide stability.
- ✅ **Decoupled Architecture:** Ensuring the system is easy to test and resistant to "breaking changes" in external libraries.
- ✅ **SOLID & Clean Code:** Focus on readability and long-term maintainability, reflecting 8+ years of production experience.

---

## 🚀 Quick Start

1. **Clone the repo:**
   ```bash
   git clone [https://github.com/BohdansWorkshop/WarehouseMonitor.git](https://github.com/BohdansWorkshop/WarehouseMonitor.git)
