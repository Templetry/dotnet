# Templetry parent: dotnet

.NET templates for [Templetry](https://github.com/Templetry). One **parent repo**, multiple **forms** — each form is a subdirectory that compiles on its own and carries its own `template.yml` ([ADR-0011](https://github.com/Templetry/wiki/blob/main/adr/0011-template-forms.md)).

| Form | What it is | Status |
|---|---|---|
| [`minimal-api/`](minimal-api/) | Minimal API — C# top-level program, xUnit with WebApplicationFactory | ✅ ready |

## Usage

```sh
templetry init dotnet/minimal-api --out ./my-svc --set "project_name=My Service"
```

Forms are **chosen**, not combined. Inside a form, the manifest's features are freely combinable.
