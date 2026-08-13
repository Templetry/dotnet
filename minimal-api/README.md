# TemplateApp

.NET minimal API generated with [Templetry](https://github.com/Templetry): C# top-level program, xUnit tests through `WebApplicationFactory`, optional Dockerfile.

```sh
dotnet run --project src/TemplateApp
dotnet test
docker build -t template-app .   # docker feature
```

Routes: `GET /healthz` · `GET /api/hello/{name}`.
