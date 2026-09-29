# DriveMate

DriveMate is a driver-on-demand platform that connects customers with drivers who can drive the customer's own vehicle for a requested period of time.

## Overview

The platform provides separate experiences for customers and drivers.

### Customers

- Create an account and sign in
- Request a driver
- Specify date, time, and duration
- View and manage driver requests
- Track request status

### Drivers

- Create a driver account and sign in
- View available driver requests
- Accept driver requests
- Manage assigned requests

## Architecture

DriveMate follows a Clean Architecture approach with clear separation of responsibilities.

```text
DriveMate
│
├── DriveMate.Server          # ASP.NET Core Web API
├── DriveMate.Domain          # Domain models and entities
├── DriveMate.Application     # Application logic, DTOs and interfaces
├── DriveMate.Infrastructure  # Database and external implementations
└── DriveMate.Client          # Angular frontend
```

## Technology Stack

### Backend

- C#
- ASP.NET Core Web API
- .NET
- Dapper
- JWT Authentication

### Frontend

- Angular
- TypeScript
- HTML
- CSS

### Database

- PostgreSQL
- Supabase

### Tools

- Git
- GitHub
- Visual Studio Code
- Postman

---

## Getting Started

### Prerequisites

- .NET SDK
- Node.js
- npm
- Angular CLI

### Backend

```bash
cd DriveMate.Server
dotnet restore
dotnet run
```

## Author
### Vignesh Nukala
### Software Engineer | .NET | Angular | Full-Stack Development
