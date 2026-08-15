// TemplateApp minimal API.
// tpl:if environments
using Microsoft.Extensions.Options;
using TemplateApp;
// tpl:endif

var builder = WebApplication.CreateBuilder(args);
// tpl:if environments

// Bound and validated when the process starts, so a broken profile fails
// here rather than on the first request that happened to read it.
builder.Services
    .AddOptions<AppOptions>()
    .Bind(builder.Configuration.GetSection(AppOptions.SectionName))
    .ValidateDataAnnotations()
    .ValidateOnStart();
// tpl:endif

var app = builder.Build();

// tpl:if environments
app.MapGet("/healthz", (IOptions<AppOptions> options) =>
    Results.Ok(new { status = "ok", environment = options.Value.Environment }));
// tpl:endif
// tpl:if !environments
app.MapGet("/healthz", () => Results.Ok(new { status = "ok" }));
// tpl:endif
app.MapGet("/api/hello/{name}", (string name) => Results.Ok(new { message = $"Hello, {name}!" }));

app.Run();

// Exposed so the test project's WebApplicationFactory can boot this app.
public partial class Program;
