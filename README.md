# Templetry parent: dotnet

.NET templates for [Templetry](https://github.com/Templetry). One **parent repo**, multiple **forms** — each form is a subdirectory that compiles on its own and carries its own `template.yml` ([ADR-0011](https://github.com/Templetry/wiki/blob/main/adr/0011-template-forms.md)).

| Form | What it is | Status |
|---|---|---|
| [`minimal-api/`](minimal-api/) | Minimal API — C# top-level program, xUnit with WebApplicationFactory | ✅ ready |
| [`razor-web/`](razor-web/) | Razor Pages web app — shared layout, static files, server-side validation | 🚧 awaiting first green CI |

## Usage

```sh
templetry init dotnet/minimal-api --out ./my-svc --set "project_name=My Service"
templetry init dotnet/razor-web  --out ./my-site --set "project_name=My Site"
```

Forms are **chosen**, not combined. Inside a form, the manifest's features are freely combinable.
