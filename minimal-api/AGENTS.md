# AGENTS

Operating contract for AI agents and automation helpers working in this project.

## Mission

- Keep this API minimal: top-level `Program.cs` endpoints; introduce controllers or layers only when the app actually needs them.

## Core Rules

- Nullable and implicit usings stay enabled; fix warnings rather than suppressing them.
- Endpoints return `Results.*` with proper status codes.
- Every endpoint gets a `WebApplicationFactory` test in the test project.
- Package versions stay aligned with the target framework (net9.0).
- Update docs in the same change when behavior or process changes.

## Required Checks Before Finishing

- `dotnet build` compiles clean.
- `dotnet test` passes.

## Safe Change Workflow

1. Read the affected files fully before editing.
2. Make the smallest change that solves the task.
3. Build and test, then review the diff with git before committing.
