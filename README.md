# 🕯️ CursedVibes API
![CI/CD](https://github.com/shakana0/CursedVibes/actions/workflows/deploy.yml/badge.svg)

The backend powering an interactive storytelling experience - mysterious, emotional, and evolving.  

Currently under development ⚙️  

---

## 🌌 Background  
**CursedVibes API** serves as the foundation for a narrative-driven game experience.  
It supports CRUD and search operations **characters** and secure **scene retrieval** from **Azure Blob Storage**. 
The project follows **Clean Architecture** principles to keep the codebase modular, maintainable, and easy to extend as the story grows.
The API powers the frontend companion, **CursedVibesRealm**, built with Next.js and TypeScript

---

## 🔧 Tech Stack  

| Technology | Purpose |
|-------------|----------|
| **ASP.NET Core WebAPI** | Robust and scalable framework for building RESTful APIs |
| **Azure App Service** | Hosts and manages the API in a scalable cloud environment |
| **Azure Blob Storage** | Secure and efficient storage for media and story scenes |
| **Azure SQL Database** | Reliable cloud-based data persistence |
| **Azure Key Vault** | Securely stores and manages secrets, keys, and connection strings |
| **Entra ID (Azure AD)** | Authentication and access control |
| **Entity Framework Core** | Simplifies database operations through ORM |
| **MediatR & CQRS** | Implements clean CQRS architecture and decoupled request handling |
| **AutoMapper** | Handles object mapping to keep code clean |
| **FluentValidation** | Provides structured and expressive validation for models |
| **xUnit & Moq** | Used for unit testing and mocking dependencies in the Application layer |
| **EF Core InMemory** | Enables lightweight integration testing of repositories in the Infrastructure layer |

---
## 🔁 CI/CD Pipeline  

This project uses **GitHub Actions** for automated build, test, and deployment to **Azure App Service**.

### ⚙️ Workflow Overview
- **Trigger:** Runs on every push to `main`  
- **Steps:**  
  1. Checkout code  
  2. Setup .NET 9 SDK  
  3. Restore dependencies  
  4. Build and publish WebAPI  
  5. Run unit tests  
  6. Zip publish artefact  
  7. Deploy to Azure via `az webapp deployment source config-zip`

**Azure Target:** ☁️  
- Deployment target: **Azure App Service**

---
## 🔁 Branch Flow

- `dev`: Active development, testing, and CI validation
- `main`: Stable production branch, triggers full deployment

Workflows:
- `.github/workflows/test.yml`: Runs on `dev`, tests only
- `.github/workflows/deploy.yml`: Runs on `main`, full CI/CD
---
## 🏗️ Clean Architecture Overview
A visual representation of the system’s layered structure — from the Web API down to the Domain and Infrastructure layers.

<img src="./assets/CursedVibed-Diagram.png" width="500" alt="Clean Architecture layers showing Web API, Application, Domain, Infrastructure, Tests, and CI/CD pipeline" />

It illustrates how each layer interacts according to the Clean Architecture principles, ensuring a clear separation of concerns and testability.

---
## 🧩 Character Domain Model (ER Diagram)

A visual representation of the Character entity and its related value objects within the domain layer.

<img src="./assets/character-erdiagram.png" width="400" alt="ER diagram showing Character entity with value object Stats" />

It shows how the core game logic — including stats like Strength, Agility, Intelligence, and Luck — is structured in the database and modeled according to DDD Aggregate principles.

---
## 🧪 API Playground (Swagger UI)
A visual overview of all available endpoints. Fully testable via Swagger for smooth development and debugging.

<img src="./assets/swagger.jpeg" width="400" alt="Screenshot of endpoints in Swagger" />

---
## 🚀 Getting Started  

Run the API locally:  
```bash
dotnet restore

dotnet build

dotnet run --project WebAPI

The API will be available at:
👉 https://localhost:54916/swagger
```

## 🧪 Testing
To run tests:
```bash
dotnet test
``` 

---

## 🧠 Project Goals

- Build a modular and extendable backend for interactive storytelling

- Experiment with clean architecture and CQRS patterns

- Integrate secure Azure services for a production-grade experience

---

## 🧩 Roadmap (WIP)

- [ ] Add scene creation and editing endpoints
- [ ] Implement an AI-powered chat feature to enhance interactive storytelling
- [ ] Implement authentication & role-based access
- [ ] Expand world-building entities (places, events, relationships)
- [ ] Connect with CursedVibesRealm frontend

