// TemplateApp minimal API.
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/healthz", () => Results.Ok(new { status = "ok" }));
app.MapGet("/api/hello/{name}", (string name) => Results.Ok(new { message = $"Hello, {name}!" }));

app.Run();

// Exposed so the test project's WebApplicationFactory can boot this app.
public partial class Program;
