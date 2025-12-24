# StudentAPI

ASP.NET Web API project demonstrating a clean three-layer architecture with a console client.

## Architecture
Three-layer architecture:
- Presentation Layer (ASP.NET Web API)
- Business Layer (Domain logic and state management)
- Data Access Layer (ADO.NET & SQL Server)

## Server
- ASP.NET Web API
- RESTful endpoints

### Endpoints
- GET /api/Students/All
- GET /api/Students/Passed
- GET /api/Students/Failed
- GET /api/Students/AverageGrade
- GET /api/Students/{id}
- POST /api/Students
- PUT /api/Students/{id}
- DELETE /api/Students/{id}

## Client
- C# Console Application
- Communicates with the API using HttpClient

## Data Storage
- SQL Server database
- CRUD operations implemented using Stored Procedures
- Data accessed through a dedicated Data Access Layer

## Design Highlights
- Clean separation of concerns
- DTO-based data transfer between layers
- State-based Save logic (Add / Update)
- No business logic inside API controllers

## Purpose
Demonstration project showcasing:
- Client–Server communication
- Layered architecture
- SQL Server integration
- Maintainable and scalable backend design