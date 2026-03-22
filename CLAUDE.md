# Instructions — C#/.NET Project

## Context
C#/.NET application — web API, service, or desktop app.

## Stack
- Runtime: .NET 8+
- Language: C# 12+
- Tests: xUnit, Moq
- Formatting: dotnet format, .editorconfig
- Packages: NuGet

## Commands
- `dotnet build` — Compile the project
- `dotnet test` — Run the tests
- `dotnet run` — Launch the application
- `dotnet format` — Format code according to editorconfig

## Architecture
- APIs: Controllers → Services → Repositories
- Desktop: MVVM (Model-View-ViewModel)
- Dependency injection via the built-in container
- Configuration with `appsettings.json` + environment variables

## Conventions
- PascalCase for methods, properties, and classes
- `_camelCase` for private fields
- Async/await everywhere for I/O operations
- Nullable reference types enabled (`<Nullable>enable</Nullable>`)
- Interfaces for injected dependencies
- Use `var` when the type is obvious from the right side; explicitly specify the type otherwise
- DTOs separated from domain entities

## Security
- Never log sensitive data (passwords, tokens, PII)
- Validate all incoming DTOs with Data Annotations or FluentValidation
- Secrets come from environment variables or Secret Manager, never hardcoded
- Use prepared parameters for SQL queries
- HTTPS required in production

## Structure
```
src/
├── Controllers/       # API endpoints
├── Services/          # Business logic
├── Repositories/      # Data access
├── Models/            # Domain entities
├── DTOs/              # Transfer objects
├── Middleware/         # Custom middleware
└── Extensions/        # Extension methods
tests/
├── Unit/
└── Integration/
```
