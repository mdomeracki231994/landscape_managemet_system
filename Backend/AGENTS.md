# AGENTS.md — Backend

This file guides agents contributing to the Landscape Management System backend.

## Scope
- Applies to the entire directory tree rooted at `Backend/`.
- If another `AGENTS.md` appears deeper, it overrides this file within its folder.

## Tech Stack
- Language/Runtime: C# on .NET 8 (ASP.NET Core Web API)
- Entry point: `Program.cs`
- Project file: `Backend.csproj`
- HTTP: `http://localhost:5103` (dev)
- HTTPS: `https://localhost:7108` (dev)
- API docs: Swagger UI at `/swagger` in Development

## Run / Build
- Prereq: Install .NET 8 SDK.
- Restore deps: `dotnet restore`
- Build: `dotnet build`
- Run (dev): `dotnet run`
- Swagger: open `https://localhost:7108/swagger` (or `http://localhost:5103/swagger`)

## Project Structure (current)
- `Program.cs`: host + middleware + routing
- `Controllers/WeatherForecastController.cs`: sample controller
- `WeatherForecast.cs`: sample model
- `appsettings*.json`: configuration
- `Properties/launchSettings.json`: dev profiles/ports

## Conventions
- C# style: follow standard .NET naming (PascalCase for types/methods, camelCase for locals/params).
- API versioning: not set up yet; prefer `/api/<area>/<resource>` routes when adding new controllers.
- Logging: use `ILogger<T>`; do not `Console.WriteLine` in production paths.
- Configuration: read via `IConfiguration`; do not hardcode secrets.
- Minimal changes: keep diffs focused; avoid renames/moves unless required.
- Do not add licenses/headers unless requested.

## Agent Workflow
- File edits: use minimal, targeted patches.
- Do not commit or create branches unless the user asks.
- If adding dependencies, update `Backend.csproj` and mention why.
- If introducing breaking changes, call them out and provide migration notes.
- Validate by building locally (`dotnet build`) and, when applicable, by running (`dotnet run`).

## Configuration & Secrets
- Store secrets in user/dev environment or user‑provided secret store; do not commit secrets.
- `appsettings.Development.json` is safe for non‑secret dev settings only.
- If adding new settings, document keys and defaults in this file.

## Testing
- No tests exist yet. If you add tests, place them in a sibling test project (e.g., `Backend.Tests`) and keep them focused.
- Prefer unit tests for controllers/services over end‑to‑end unless requested.

## Data Layer
- Database: PostgreSQL (see `ConnectionStrings:Default`).
- Access: Dapper with a lightweight repository pattern.
- Connection management: `IDbConnectionFactory` -> `NpgsqlConnectionFactory`.
- Schema: starter SQL in `Sql/001_init.sql` (apply manually or via your tool of choice).
- Note: EF Core is not used currently; add only if requested.

## API Guidelines
- Controllers should be thin; move logic into services.
- Return proper HTTP status codes and problem details for errors.
- Use DTOs for request/response models; do not expose domain models directly.
- Add OpenAPI annotations when helpful; keep Swagger usable.

## Performance & Security
- Validate inputs; avoid over‑binding.
- Use cancellation tokens for async endpoints where applicable.
- Do not expose stack traces in production responses.

## When In Doubt
- Ask for clarification about domain concepts (landscapes, jobs, crews, scheduling, billing) before modeling.
- Propose a small plan, then implement incrementally.

---
Maintainers: Update this file as the backend evolves (persistence, auth, CI, etc.).
