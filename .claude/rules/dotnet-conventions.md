# C#/.NET Conventions

## Naming
- `PascalCase` for classes, methods, properties, events
- `_camelCase` for private fields (with underscore)
- `camelCase` for parameters and local variables
- `IPrefix` for interfaces (e.g., `IUserService`)
- `TSuffix` not used for generics — prefer descriptive names (`TEntity`, `TResult`)
- Suffixes: `Controller`, `Service`, `Repository`, `Handler`, `Middleware`

## LINQ
- Prefer method syntax (`.Where()`, `.Select()`) over query syntax
- Use `Any()` instead of `Count() > 0`
- No LINQ in hot path loops — it allocates memory
- `FirstOrDefault` with an explicit fallback when possible
- Chain calls on separate lines when there are more than 2

## Async
- Suffix async methods with `Async` (e.g., `GetUserAsync`)
- Always use `await` — never `.Result` or `.Wait()` (causes deadlocks)
- `ConfigureAwait(false)` in libraries, not in apps
- Return `Task` not `void` for async methods (except event handlers)
- Use `CancellationToken` for long-running operations

## Dispose Pattern
- Implement `IDisposable` when the class owns unmanaged resources
- Use `using` or `using var` for disposable objects
- No need for a finalizer if there are only managed resources
- For injected services, let the DI container handle the dispose

## General
- One file per class/interface/enum
- Regions (`#region`) should be avoided — they hide code that should be refactored
- `record` for DTOs and immutable value objects
- `sealed` by default on classes not designed for inheritance
- Prefer composition over inheritance
